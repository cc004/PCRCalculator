using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Framework.Threading;
using PCRCaculator;
using PCRCaculator.Guild;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Elements.Battle
{

    public class SemanUBMagicAtkFitness : SemanUBFitness
    {
        int tp = 0;
        public SemanUBMagicAtkFitness(int tp)
        {
            this.tp = tp;
        }
        override public long GetFitness()
        {
            List<UnitCtrl> playerUnits = MyGameCtrl.Instance.playerUnitCtrl.Take(5).ToList();
            float totalMagicAtk = playerUnits.Select(unit => (float)unit.MagicStr).Sum();
            bool ok = playerUnits.Where(unit => unit.EnergyRecoveryRate < tp).Count() == 0;
            int tpSum = playerUnits.Select(unit => unit.EnergyRecoveryRate).Sum();
            return (long)totalMagicAtk * (ok ? 1 : 0) + tpSum;
        }
    }
    public class SemanUBPhysicAtkFitness : SemanUBFitness
    {
        int tp = 0;
        public SemanUBPhysicAtkFitness(int tp)
        {
            this.tp = tp;
        }
        override public long GetFitness()
        {
            List<UnitCtrl> playerUnits = MyGameCtrl.Instance.playerUnitCtrl;
            float totalPhysicAtk = playerUnits.Select(unit => (float)unit.Atk).Sum();
            bool ok = playerUnits.Where(unit => unit.EnergyRecoveryRate < tp).Count() == 0;
            int tpSum = playerUnits.Select(unit => unit.EnergyRecoveryRate).Sum();
            return (long)totalPhysicAtk * (ok ? 1 : 0) + tpSum;
        }
    }
    public class SemanUBExpectDamageFitness : SemanUBFitness
    {
        override public long GetFitness()
        {
            long damage = GuildCalculator.Instance.GetTotalDamageExpect();
            if (BattleManager.Instance.battleResult == eBattleResult.WIN)
            {
                damage = (long)(BattleManager.Instance.BattleLeftTime * 1000000000);
            }
            return damage;
        }
    }
    public class SemanUBRealDamageFitness : SemanUBFitness
    {
        override public long GetFitness()
        {
            long damage = GuildCalculator.Instance.GetTotalDamage();
            if (BattleManager.Instance.battleResult == eBattleResult.WIN)
            {
                damage = (long)(BattleManager.Instance.BattleLeftTime * 1000000000);
            }
            return damage;
        }
    }
    abstract public class SemanUBFitness : IFitness
    {
        abstract public long GetFitness();
        public double Evaluate(IChromosome chromosome)
        {
            var genes = chromosome.GetGenes();
            var mainManager = MainManager.Instance;
            var semanUb = new List<List<int>>();
            for (int i = 0; i < genes.Length; i++)
            {
                var gene = genes[i].Value as List<int>;
                semanUb.Add(GAUBOptimization.fixPreGenes[i].Concat(gene).ToList());
            }
            mainManager.GuildBattleData.SemanUBExecTimeList = semanUb;
            mainManager.GuildBattleData.skipping = true;
            MyGameCtrl.Instance.isGAEvaluateFinished = false;
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                MyGameCtrl.Instance.ReStart();
            });
            while (!MyGameCtrl.Instance.isGAEvaluateFinished)
            {
                Thread.Sleep(100);
            }
            var UBNums = BattleManager.Instance.semanubmanager.UBNums.Select((x, i) => Math.Max(0, x - GAUBOptimization.fixPreGenes[i].Count)).ToList();
            for (int i = 0; i < semanUb.Count; i++)
            {
                var gene = chromosome.GetGene(i).Value as List<int>;
                while (gene.Count <= UBNums[i])
                {
                    gene.Add(0);
                }
            }
            ((SemanUBChromosome)chromosome).UbNums = UBNums;
            long damage = GetFitness();
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                GAUBOptimization.instance.damageText.text = damage.ToString("N0");
            });
            ((SemanUBChromosome)chromosome).totalDamage = damage;
            return damage;
        }
    }


    public class SemanUBChromosome : ChromosomeBase
    {
        public static List<List<int>> useValue = new List<List<int>>();
        public static readonly int[][] setValue = new int[][]
        {
            new int[] {
                0, 10900, 11100, 12100, 13100, 14100, 15100, 
            },
            new int[] {
                0, 10100, 11900, 12100, 13100, 14100, 15100, 
            },
            new int[] {
                0, 10100, 11100, 12900, 13100, 14100, 15100, 
            },
            new int[] {
                0, 10100, 11100, 12100, 13900, 14100, 15100, 
            },
            new int[] {
                0, 10100, 11100, 12100, 13100, 14900, 15100, 
            },
        };

        public static readonly int[][] ownSkillValue = new int[][]
        {
            new int[] {
                    10000, 10200, 10300, 10010, 10210, 10310, 10220, 10320,
            },
            new int[] {
                    11000, 11200, 11300, 11010, 11210, 11310, 11220, 11320,
            },
            new int[] {
                    12000, 12200, 12300, 12010, 12210, 12310, 12220, 12320,
            },
            new int[] {
                    13000, 13200, 13300, 13010, 13210, 13310, 13220, 13320,
            },
            new int[] {
                    14000, 14200, 14300, 14010, 14210, 14310, 14220, 14320,
            },
        };
        public static readonly int[][] otherSkillValue = new int[][]
        {
            new int[] {
                    11000, 11200, 11300, 11010, 11210, 11310,
                    12000, 12200, 12300, 12010, 12210, 12310,
                    13000, 13200, 13300, 13010, 13210, 13310,
                    14000, 14200, 14300, 14010, 14210, 14310,
                    15000, 15200, 15300, 15010, 15210, 15310,
            },
            new int[] {
                    10000, 10200, 10300, 10010, 10210, 10310,
                    12000, 12200, 12300, 12010, 12210, 12310,
                    13000, 13200, 13300, 13010, 13210, 13310,
                    14000, 14200, 14300, 14010, 14210, 14310,
                    15000, 15200, 15300, 15010, 15210, 15310,
            },
            new int[] {
                    10000, 10200, 10300, 10010, 10210, 10310,
                    11000, 11200, 11300, 11010, 11210, 11310,
                    13000, 13200, 13300, 13010, 13210, 13310,
                    14000, 14200, 14300, 14010, 14210, 14310,
                    15000, 15200, 15300, 15010, 15210, 15310,
            },
            new int[] {
                    10000, 10200, 10300, 10010, 10210, 10310,
                    11000, 11200, 11300, 11010, 11210, 11310,
                    12000, 12200, 12300, 12010, 12210, 12310,
                    14000, 14200, 14300, 14010, 14210, 14310,
                    15000, 15200, 15300, 15010, 15210, 15310,
            },
            new int[] {
                    10000, 10200, 10300, 10010, 10210, 10310,
                    11000, 11200, 11300, 11010, 11210, 11310,
                    12000, 12200, 12300, 12010, 12210, 12310,
                    13000, 13200, 13300, 13010, 13210, 13310,
                    15000, 15200, 15300, 15010, 15210, 15310,
            },
        };

        private readonly int UBNum = 0;
        public long totalDamage = 0;

        public List<int> UbNums = new List<int>();

        public SemanUBChromosome(int ubNum) : base(5)
        {
            UBNum = ubNum;
            CreateGenes();
        }

        private static readonly List<int> reusableList = new List<int>();

        public override Gene GenerateGene(int geneIndex)
        {
            var random = RandomizationProvider.Current;
            var listIndex = useValue[geneIndex];
            reusableList.Clear();
            for (int i = 0; i < UBNum; i++)
            {
                reusableList.Add(listIndex[random.GetInt(0, listIndex.Count)]);
            }
            return new Gene(new List<int>(reusableList));
        }

        public override IChromosome CreateNew()
        {
            return new SemanUBChromosome(UBNum);
        }

        protected override void CreateGenes()
        {
            for (int i = 0; i < Length; i++)
            {
                ReplaceGene(i, GenerateGene(i));
            }
            UbNums = new List<int> { 0, 0, 0, 0, 0 };
        }

        public override IChromosome Clone()
        {
            var clone = base.Clone() as SemanUBChromosome;
            clone.totalDamage = totalDamage;
            clone.UbNums = new List<int>(UbNums);
            return clone;
        }
    }

    public class SemanUBCrossover : CrossoverBase
    {
        public SemanUBCrossover() : base(2, 1)
        {
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var parent1 = parents[0] as SemanUBChromosome;
            var parent2 = parents[1] as SemanUBChromosome;

            var child = parent1.CreateNew() as SemanUBChromosome;

            var totalDamage1 = parent1.totalDamage;
            var totalDamage2 = parent2.totalDamage;
            var totalDamageSum = totalDamage1 + totalDamage2;
            var random = RandomizationProvider.Current;

            for (int i = 0; i < parent1.Length; i++)
            {
                var gene1 = parent1.GetGene(i).Value as List<int>;
                var gene2 = parent2.GetGene(i).Value as List<int>;

                var newGene = new List<int>();
                int minLength = Math.Min(parent1.UbNums[i] + 1, parent2.UbNums[i] + 1);

                int prefix = random.GetInt(0, minLength);
                for (int j = 0; j < prefix; j++)
                {
                    newGene.Add(totalDamage1 > totalDamage2 ? gene1[j] : gene2[j]);
                }

                for (int j = prefix; j < minLength; j++)
                {
                    var probability = random.GetDouble();
                    newGene.Add(probability < (double)totalDamage1 / totalDamageSum ? gene1[j] : gene2[j]);
                }

                child.UbNums[i] = minLength - 1;
                child.ReplaceGene(i, new Gene(newGene));
            }

            return new List<IChromosome> { child };
        }
    }

    public class SemanUBMutation : MutationBase
    {
        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            var semanChromosome = chromosome as SemanUBChromosome;
            var random = RandomizationProvider.Current;

            for (int i = 0; i < semanChromosome.Length; i++)
            {
                var gene = semanChromosome.GetGene(i).Value as List<int>;
                var listIndex = SemanUBChromosome.useValue[i];

                if (random.GetDouble() < 0.5)
                {
                    int pos = random.GetInt(0, semanChromosome.UbNums[i] + 1);
                    gene[pos] = listIndex[random.GetInt(0, listIndex.Count)];
                    semanChromosome.ReplaceGene(i, new Gene(gene));
                }

            }
        }

    }

    public class SemanPopulation : Population
    {
        private IList<IChromosome> m_preloadChromosome;

        public SemanPopulation(int minSize, int maxSize, IList<IChromosome> preloadChromosomes)
            : base(minSize, maxSize, preloadChromosomes.FirstOrDefault())
        {
            m_preloadChromosome = preloadChromosomes;
        }

        public override void CreateInitialGeneration()
        {
            Generations = new List<Generation>();
            GenerationsNumber = 0;

            foreach (var c in m_preloadChromosome)
            {
                c.ValidateGenes();
            }

            while (m_preloadChromosome.Count < MinSize)
            {
                var c = m_preloadChromosome[0].CreateNew();
                c.ValidateGenes();
                m_preloadChromosome.Add(c);
            }

            CreateNewGeneration(m_preloadChromosome);
        }
    }
    public class SemanUBOperatorsStrategy : IOperatorsStrategy 
    {
        public IList<IChromosome> Cross(IPopulation population, ICrossover crossover, float crossoverProbability, IList<IChromosome> parents)
        {
            var minSize = population.MinSize;
            var offspring = new List<IChromosome>(minSize);

            for (int i = 0; i < minSize; i += crossover.ParentsNumber)
            {

            }

            return offspring;
        }

        public void Mutate(IMutation mutation, float mutationProbability, IList<IChromosome> chromosomes)
        {
            for (int i = 0; i < chromosomes.Count; i++)
            {
                mutation.Mutate(chromosomes[i], mutationProbability);
            }
        }
    }

    public class GAUBOptimization : MonoBehaviour
    {
        static public List<List<int>> fixPreGenes = new List<List<int>>();
        public GameObject GAInfo;
        private long bestDamage;
        private List<List<int>> bestGenes = new List<List<int>>();
        private GeneticAlgorithm ga;
        private Thread gaThread;
        public Text bestDamageText;
        public Text GenerationsNumberText;
        public List<Text> bestGenesTexts;
        public Text historyTexts;
        public Text damageText;

        public InputField populationMin;
        public InputField populationMax;
        public InputField ubDelay;
        public InputField tpDemand;
        public List<InputField> fixUb;
        public List<TMP_InputField> DIYSemanUb;
        public Toggle considerUB;
        public Toggle considerSkill;
        public Toggle considerAll;
        public ToggleGroup fitnessToggleGroup;
        public Toggle considerRealDamage;
        public Toggle considerExpectDamage;
        public Toggle considerPhysicAtk;
        public Toggle considerMagicAtk; 

        static public GAUBOptimization instance;

        private void Awake()
        {
            instance = this;
        }

        public void Show()
        {
            GAInfo.SetActive(true);
            for (int i = 0; i < 5; i++)
            {
                var raw = MyGameCtrl.Instance.tempData.SemanUBExecTimeList.ElementAtOrDefault(i) ?? new List<int>();
                bestGenesTexts[i].text = string.Join("\n", raw);
            }
        }

        public void Update()
        {
            if (ga == null)
            {
                return;
            }
            GenerationsNumberText.text = ga.GenerationsNumber.ToString();
        }

        public void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                if (SemanUBChromosome.useValue.Count <= i)
                {
                    SemanUBChromosome.useValue.Add(new List<int>());
                }
                else
                {
                    SemanUBChromosome.useValue[i].Clear();
                }
                if (considerUB.isOn)
                {
                    SemanUBChromosome.useValue[i].AddRange(SemanUBChromosome.setValue[i]);
                }
                if (considerSkill.isOn)
                {
                    SemanUBChromosome.useValue[i].AddRange(SemanUBChromosome.ownSkillValue[i]);
                }
                if (considerAll.isOn)
                {
                    SemanUBChromosome.useValue[i].AddRange(SemanUBChromosome.otherSkillValue[i]);
                }
                int dealy = ubDelay.text == "" ? 0 : int.Parse(ubDelay.text);
                for (int j = 1; j <= dealy; j++)
                {
                    SemanUBChromosome.useValue[i].Add(j);
                }
                if (DIYSemanUb[i].text == "")
                {
                    continue;
                }
                var DIYSemanUbList = DIYSemanUb[i].text.Split('\n').Select(x => int.Parse(x)).ToList();
                SemanUBChromosome.useValue[i].AddRange(DIYSemanUbList);
            }
            var populationMinValue = int.Parse(populationMin.text);
            var populationMaxValue = int.Parse(populationMax.text);

            bestDamage = 0;
            bestGenes.Clear();
            historyTexts.text = "";
            MyGameCtrl.Instance.IsGAUbMode = true;
            var selection = new EliteSelection();
            var crossover = new SemanUBCrossover();
            var mutation = new SemanUBMutation();
            IFitness fitness = null;
            if (considerRealDamage.isOn)
            {
                fitness = new SemanUBRealDamageFitness();
            }
            else if (considerExpectDamage.isOn)
            {
                fitness = new SemanUBExpectDamageFitness();
            }
            else if (considerPhysicAtk.isOn)
            {
                fitness = new SemanUBPhysicAtkFitness(int.Parse(tpDemand.text));
            }
            else if (considerMagicAtk.isOn)
            {
                fitness = new SemanUBMagicAtkFitness(int.Parse(tpDemand.text));
            }
            var allSetChr = new SemanUBChromosome(2);
            for (int i = 0; i < 5; i++)
            {
                allSetChr.ReplaceGene(i, new Gene(new List<int>() { 0 }));
            }
            var curChr = allSetChr.CreateNew() as SemanUBChromosome;
            fixPreGenes.Clear();
            for (int i = 0; i < 5; i++)
            {
                var raw = MyGameCtrl.Instance.tempData.SemanUBExecTimeList.ElementAtOrDefault(i) ?? new List<int>();
                fixUb[i].text = Math.Min(int.Parse(fixUb[i].text), raw.Count).ToString();
                int fixNum = int.Parse(fixUb[i].text);
                fixPreGenes.Add(raw.Take(fixNum).ToList());
                curChr.ReplaceGene(i, new Gene(raw.Skip(fixNum).ToList()));
            }
            var preChromosome = new List<IChromosome>() { allSetChr, curChr };
            var population = new SemanPopulation(populationMinValue, populationMaxValue, preChromosome);

            ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            // ga.OperatorsStrategy = new SemanUBOperatorsStrategy();
            ga.Termination = new TimeEvolvingTermination(System.TimeSpan.FromHours(10));
            // ga.Termination = new GenerationNumberTermination(100);

            ga.TaskExecutor = new LinearTaskExecutor();

            ga.GenerationRan += delegate
            {
                var bestChromosome = (SemanUBChromosome)ga.BestChromosome;
                var totalDamage = bestChromosome.totalDamage;
                var damageStr = totalDamage.ToString("N0");
                if (totalDamage > bestDamage)
                {
                    var genes = new List<List<int>>();
                    for (int i = 0; i < bestChromosome.Length; i++)
                    {
                        var gene = bestChromosome.GetGene(i).Value as List<int>;
                        genes.Add(fixPreGenes[i].Concat(gene).ToList());
                    }
                    bestDamage = totalDamage;
                    bestGenes = genes;
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        for (int i = 0; i < genes.Count; i++)
                        {
                            bestGenesTexts[i].text = string.Join("\n", genes[i]);
                        }
                        bestDamageText.text = damageStr;
                        Save();
                    });
                }
                List<long> damages = new List<long>();
                foreach (var chromosome in ga.Population.CurrentGeneration.Chromosomes)
                {
                    damages.Add(((SemanUBChromosome)chromosome).totalDamage);
                }
                var minDamage = damages.Min().ToString("N0");
                var maxDamage = damages.Max().ToString("N0");
                var avgDamage = damages.Average();
                var std = (damages.Select(x => Math.Pow(x - avgDamage, 2)).Sum() / damages.Count).ToString("E");
                var avgDamageText = avgDamage.ToString("N0");
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    var time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var newHistoryEntry = $"{time}/{ga.GenerationsNumber}: [{minDamage}, {maxDamage}] {avgDamageText}";
                    var historyLines = historyTexts.text.Split('\n').ToList();
                    historyLines.Insert(0, newHistoryEntry);
                    if (historyLines.Count() > 20)
                    {
                        historyLines.RemoveAt(historyLines.Count() - 1);
                    }
                    historyTexts.text = string.Join("\n", historyLines);
                });
            };

            gaThread = new Thread(() => ga.Start());
            gaThread.Start();
        }

        public void Save()
        {
            var guildSettingData = MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup();
            guildSettingData.SemanUBExecTimeData = bestGenes;
            GuildManager.SaveSettingData(MyGameCtrl.Instance.tempData.SettingData);
            MainManager.Instance.WindowMessage("保存成功");
        }

        public void StopGA()
        {
            if (ga == null)
            {
                return;
            }
            MyGameCtrl.Instance.IsGAUbMode = false;
            ga.Stop();
            ga = null;
            gaThread.Abort();
        }

        private void OnDestroy()
        {
            StopGA();
        }

    }
}