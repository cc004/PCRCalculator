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
using ExcelHelper;
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
            long tpSum = playerUnits.Select(unit => unit.EnergyRecoveryRate).Sum();
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
            long tpSum = playerUnits.Select(unit => unit.EnergyRecoveryRate).Sum();
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
        public Func<List<List<int>>, (double Fitness, List<int> UbCounts)> RemoteEvaluation;
        public CancellationToken CancellationToken;
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
            CancellationToken.ThrowIfCancellationRequested();
            List<int> counts;
            double damage;
            if (RemoteEvaluation != null)
            {
                var response = RemoteEvaluation(semanUb);
                CancellationToken.ThrowIfCancellationRequested();
                counts = response.UbCounts;
                damage = response.Fitness;
            }
            else
            {
                mainManager.GuildBattleData.SemanUBExecTimeList = semanUb;
                mainManager.GuildBattleData.skipping = true;
                MyGameCtrl.Instance.isGAEvaluateFinished = false;
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    if (!CancellationToken.IsCancellationRequested)
                        MyGameCtrl.Instance.ReStart();
                });
                while (!MyGameCtrl.Instance.isGAEvaluateFinished)
                {
                    CancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(100);
                }
                counts = BattleManager.Instance.semanubmanager.UBNums;
                damage = GetFitness();
            }
            var UBNums = counts.Select((x, i) => Math.Max(0, x - GAUBOptimization.fixPreGenes[i].Count)).ToList();
            for (int i = 0; i < semanUb.Count; i++)
            {
                var gene = chromosome.GetGene(i).Value as List<int>;
                while (gene.Count <= UBNums[i])
                {
                    gene.Add(0);
                }
            }
            ((SemanUBChromosome)chromosome).UbNums = UBNums;
            var owner = GAUBOptimization.instance;
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                if (owner != null && !CancellationToken.IsCancellationRequested)
                    owner.damageText.text = damage.ToString("N0");
            });
            ((SemanUBChromosome)chromosome).totalDamage = damage;
            return damage;
        }
    }


    public class SemanUBChromosome : ChromosomeBase
    {
        public static List<List<int>> useValue = new List<List<int>>();

        public static readonly int[][] autoValue = new int[][]
        {
            new int[] {
                10900,
            },
            new int[] {
                11900,
            },
            new int[] {
                12900,
            },
            new int[] {
                13900,
            },
            new int[] {
                14900,
            },
        };
        public static readonly int[][] setValue = new int[][]
        {
            new int[] {
                0, 11100, 12100, 13100, 14100, 15100, 
            },
            new int[] {
                0, 10100, 12100, 13100, 14100, 15100, 
            },
            new int[] {
                0, 10100, 11100, 13100, 14100, 15100, 
            },
            new int[] {
                0, 10100, 11100, 12100, 14100, 15100, 
            },
            new int[] {
                0, 10100, 11100, 12100, 13100, 15100, 
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
        public double totalDamage = 0;

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
                    newGene.Add(probability < totalDamage1 / totalDamageSum ? gene1[j] : gene2[j]);
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
        private double bestDamage;
        private List<List<int>> bestGenes = new List<List<int>>();
        private GeneticAlgorithm ga;
        private Thread gaThread;
        private CancellationTokenSource runCancellation;
        private RemoteSimulator remoteSimulator;
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
        public Toggle useRemote;
        public Toggle considerSet;
        public Toggle considerAuto;
        public Toggle considerSkill;
        public Toggle considerAll;
        public ToggleGroup fitnessToggleGroup;
        public Toggle considerRealDamage;
        public Toggle considerExpectDamage;
        public Toggle considerPhysicAtk;
        public Toggle considerMagicAtk;
        public GameObject GAInfoPrefab;

        static public GAUBOptimization instance;

        private void Awake()
        {
            instance = this;
            AddOfflineToggle();
        }

        private readonly Vector3[] panelCorners = new Vector3[4];
        private RectTransform layoutContent;
        private Canvas layoutCanvas;

        private void RefreshPanelLayout()
        {
            // The scene also owns a GA component; only a UI instance has panel inputs.
            if (populationMin == null)
                return;
            if (layoutContent == null)
                layoutContent = transform.Find("Layout.Content") as RectTransform;
            if (layoutContent == null)
                return;
            if (layoutCanvas == null)
                layoutCanvas = GetComponentInParent<Canvas>();
            var panel = (RectTransform)transform;
            var content = layoutContent;
            var canvas = layoutCanvas;
            var corners = panelCorners;
            var parent = panel.parent as UnityEngine.RectTransform;
            if (parent == null || canvas == null) return;
            var canvasRect = (UnityEngine.RectTransform)canvas.rootCanvas.transform;
            canvasRect.GetWorldCorners(corners);
            var min = (UnityEngine.Vector2)parent.InverseTransformPoint(corners[0]);
            var max = (UnityEngine.Vector2)parent.InverseTransformPoint(corners[2]);
            panel.anchorMin = UnityEngine.Vector2.zero;
            panel.anchorMax = UnityEngine.Vector2.one;
            panel.localScale = UnityEngine.Vector3.one;
            panel.offsetMin = min - parent.rect.min;
            panel.offsetMax = max - parent.rect.max;
            var size = panel.rect.size;
            if (size.x <= 0 || size.y <= 0) return;
            // Keep the settings and history usable when the Canvas becomes short or narrow.
            var scale = UnityEngine.Mathf.Min(1f, size.x / 1000f, size.y / 640f);
            content.anchorMin = content.anchorMax = new UnityEngine.Vector2(.5f, .5f);
            content.pivot = new UnityEngine.Vector2(.5f, .5f);
            content.anchoredPosition = UnityEngine.Vector2.zero;
            content.sizeDelta = size / scale;
            content.localScale = new UnityEngine.Vector3(scale, scale, 1);
        }

        private void AddOfflineToggle()
        {
            // The scene also has this component; only the instantiated panel has inputs.
            if (populationMin == null || considerSet == null || useRemote != null)
                return;
            useRemote = Instantiate(considerSet, transform, false);
            useRemote.name = "useRemote";
            useRemote.group = null;
            useRemote.onValueChanged = new Toggle.ToggleEvent();
            useRemote.isOn = true;
            useRemote.interactable = true;
            var rect = (RectTransform)useRemote.transform;
            rect.anchorMin = rect.anchorMax = new Vector2(1, 1);
            rect.anchoredPosition = new Vector2(-196, -326);
            rect.sizeDelta = new Vector2(130, 24);
            useRemote.GetComponentInChildren<Text>().text = "脱机优化";
        }

        private int ParseInt(InputField inputField, int defaultValue)
        {
            int ret = string.IsNullOrEmpty(inputField.text) ? defaultValue : int.Parse(inputField.text);
            inputField.text = ret.ToString();
            return ret;
        }

        public void Show()
        {
            if (!MyGameCtrl.Instance.IsSemanMode)
            {
                MainManager.Instance.WindowMessage("仅在语义UB模式下可用");
                return;
            }
            GameObject a = Instantiate(GAInfoPrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            var gaubOptimization = a.GetComponent<GAUBOptimization>();
            if (gaubOptimization != null && gaubOptimization.bestGenesTexts != null && gaubOptimization.bestGenesTexts.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    var raw = MyGameCtrl.Instance.tempData.SemanUBExecTimeList.ElementAtOrDefault(i) ?? new List<int>();
                    gaubOptimization.bestGenesTexts[i].text = string.Join("\n", raw);
                }
            }
            bool asmExportEnabled = ExcelHelper.ExcelHelper.AsmExportEnabled;
            gaubOptimization.ubDelay.SetActive(asmExportEnabled);
            gaubOptimization.considerSkill.SetActive(asmExportEnabled);
            gaubOptimization.considerAll.SetActive(asmExportEnabled);
        }
        public void Exit()
        {
            Destroy(gameObject);
        }

        public void Update()
        {
            RefreshPanelLayout();
            if (useRemote != null)
                useRemote.interactable = gaThread == null || !gaThread.IsAlive;
            if (ga == null || !ga.IsRunning)
            {
                return;
            }
            GenerationsNumberText.text = ga.GenerationsNumber.ToString();
        }

        public void Run()
        {
            StartOptimization(useRemote != null && useRemote.isOn);
        }

        private void StartOptimization(bool offline)
        {
            if (gaThread != null && gaThread.IsAlive)
            {
                MainManager.Instance.WindowMessage("优化正在运行或停止中，请稍后重试。");
                return;
            }
            try
            {
                Run(offline);
            }
            catch (Exception ex)
            {
                StopGA();
                UnityEngine.Debug.LogException(ex);
                MainManager.Instance.WindowMessage("无法启动优化：" + ex.Message);
            }
        }

        private void Run(bool offline)
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
                if (considerSet.isOn)
                {
                    SemanUBChromosome.useValue[i].AddRange(SemanUBChromosome.setValue[i]);
                }
                if (considerAuto.isOn)
                {
                    SemanUBChromosome.useValue[i].AddRange(SemanUBChromosome.autoValue[i]);
                }
                if (considerSkill.isOn)
                {
                    SemanUBChromosome.useValue[i].AddRange(SemanUBChromosome.ownSkillValue[i]);
                }
                if (considerAll.isOn)
                {
                    SemanUBChromosome.useValue[i].AddRange(SemanUBChromosome.otherSkillValue[i]);
                }
                int dealy = ParseInt(ubDelay, 0);
                for (int j = 1; j <= dealy; j++)
                {
                    SemanUBChromosome.useValue[i].Add(j);
                }
                if (!string.IsNullOrWhiteSpace(DIYSemanUb[i].text))
                {
                    var DIYSemanUbList = DIYSemanUb[i].text.Split('\n')
                        .Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
                    SemanUBChromosome.useValue[i].AddRange(DIYSemanUbList);
                }
                if (SemanUBChromosome.useValue[i].Count == 0)
                    throw new ArgumentException("请为每个角色选择至少一种 UB 条件。");
            }
            var populationMinValue = ParseInt(populationMin, 50);
            var populationMaxValue = ParseInt(populationMax, 1000);

            bestDamage = 0;
            bestGenes.Clear();
            bestDamageText.text = "0";
            historyTexts.text = "";
            var selection = new EliteSelection();
            var crossover = new SemanUBCrossover();
            var mutation = new SemanUBMutation();
            SemanUBFitness fitness = null;
            SemanOptimizationTarget target = SemanOptimizationTarget.None;
            var tpDemandValue = considerPhysicAtk.isOn || considerMagicAtk.isOn
                ? ParseInt(tpDemand, 0) : 0;
            if (considerRealDamage.isOn)
            {
                fitness = new SemanUBRealDamageFitness();
                target = SemanOptimizationTarget.RealDamage;
            }
            else if (considerExpectDamage.isOn)
            {
                fitness = new SemanUBExpectDamageFitness();
                target = SemanOptimizationTarget.ExpectedDamage;
            }
            else if (considerPhysicAtk.isOn)
            {
                fitness = new SemanUBPhysicAtkFitness(tpDemandValue);
                target = SemanOptimizationTarget.PhysicalAttack;
            }
            else if (considerMagicAtk.isOn)
            {
                fitness = new SemanUBMagicAtkFitness(tpDemandValue);
                target = SemanOptimizationTarget.MagicAttack;
            }
            if (fitness == null)
                throw new ArgumentException("请选择优化目标。");
            var allSetChr = new SemanUBChromosome(2);
            for (int i = 0; i < 5; i++)
            {
                allSetChr.ReplaceGene(i, new Gene(new List<int>() { 0 }));
            }
            var curChr = allSetChr.CreateNew() as SemanUBChromosome;
            fixPreGenes.Clear();
            for (int i = 0; i < 5; i++)
            {
                var raw = MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup().SemanUBExecTimeData.ElementAtOrDefault(i) ?? new List<int>();
                fixUb[i].text = Math.Max(0, Math.Min(ParseInt(fixUb[i], 0), raw.Count)).ToString();
                int fixNum = int.Parse(fixUb[i].text);
                fixPreGenes.Add(raw.Take(fixNum).ToList());
                curChr.ReplaceGene(i, new Gene(raw.Skip(fixNum).ToList()));
            }
            var preChromosome = new List<IChromosome>() { allSetChr, curChr };
            var population = new SemanPopulation(populationMinValue, populationMaxValue, preChromosome);

            ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            // ga.OperatorsStrategy = new SemanUBOperatorsStrategy();
            ga.Termination = new TimeEvolvingTermination(System.TimeSpan.FromHours(24 * 30));
            // ga.Termination = new GenerationNumberTermination(100);

            ga.TaskExecutor = new LinearTaskExecutor();

            runCancellation?.Dispose();
            runCancellation = new CancellationTokenSource();
            var cancellation = runCancellation;
            fitness.CancellationToken = cancellation.Token;
            if (offline)
            {
                remoteSimulator = new RemoteSimulator();
                remoteSimulator.CreateRequest(MainManager.Instance.GuildBattleData);
                var simulator = remoteSimulator;
                fitness.RemoteEvaluation = sequence => simulator.SimulateSeman(sequence, target,
                    tpDemandValue, cancellation.Token).GetAwaiter().GetResult();
            }
            MyGameCtrl.Instance.IsGAUbMode = true;

            ga.GenerationRan += delegate
            {
                if (cancellation.IsCancellationRequested)
                    return;
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
                        if (this == null || cancellation.IsCancellationRequested)
                            return;
                        for (int i = 0; i < genes.Count; i++)
                        {
                            bestGenesTexts[i].text = string.Join("\n", genes[i]);
                        }
                        bestDamageText.text = damageStr;
                        Save();
                    });
                }
                List<double> damages = new List<double>();
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
                    if (this == null || cancellation.IsCancellationRequested)
                        return;
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

            var algorithm = ga;
            var childServer = remoteSimulator;
            gaThread = new Thread(() =>
            {
                Exception error = null;
                bool aborting = false;
                try
                {
                    algorithm.Start();
                }
                catch (ThreadAbortException)
                {
                    aborting = true;
                }
                catch (Exception ex)
                {
                    if (!cancellation.IsCancellationRequested)
                    {
                        error = ex;
                    }
                }
                finally
                {
                    try { childServer?.Dispose(); }
                    catch (Exception ex) { if (error == null) error = ex; }
                    // Never touch Unity APIs from an aborting/reloading worker.
                    if (!aborting && !cancellation.IsCancellationRequested && !OfflineServerManager.IsClosing)
                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        if (this == null || cancellation.IsCancellationRequested || ga != algorithm)
                            return;
                        if (remoteSimulator == childServer)
                            remoteSimulator = null;
                        if (MyGameCtrl.Instance != null)
                            MyGameCtrl.Instance.IsGAUbMode = false;
                        if (error != null)
                        {
                            UnityEngine.Debug.LogException(error);
                            MainManager.Instance.WindowMessage("优化失败：" + error.GetBaseException().Message);
                        }
                    });
                }
            }) { IsBackground = true };
            gaThread.Start();
        }

        public void Save()
        {
            var guildSettingData = MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup();
            guildSettingData.SemanUBExecTimeData.Clear();
            guildSettingData.SemanUBExecTimeData.AddRange(bestGenes);
            GuildManager.SaveSettingData(MyGameCtrl.Instance.tempData.SettingData);
            MainManager.Instance.WindowMessage("保存成功");
        }

        public void StopGA()
        {
            runCancellation?.Cancel();
            if (ga != null && ga.IsRunning)
                ga.Stop();
            remoteSimulator?.Dispose();
            remoteSimulator = null;
            var worker = gaThread;
            if (worker != null && worker != Thread.CurrentThread && worker.IsAlive)
                worker.Join(2000);
            if (worker != null && !worker.IsAlive && gaThread == worker)
                gaThread = null;
            if (MyGameCtrl.Instance != null)
                MyGameCtrl.Instance.IsGAUbMode = false;
        }

        public void CopyPopulation()
        {
            var chromosomes = ga.Population.CurrentGeneration.Chromosomes;
            string text = "";
            foreach (var chromosome in chromosomes)
            {
                var genes = new List<List<int>>();
                for (int i = 0; i < chromosome.Length; i++)
                {
                    var gene = chromosome.GetGene(i).Value as List<int>;
                    genes.Add(gene);
                }
                text += string.Join(" ", genes.Select(gene => string.Join(",", gene)));
                text += " " + ((SemanUBChromosome)chromosome).totalDamage;
                text += "\n";
            }
            GUIUtility.systemCopyBuffer = text;
            MainManager.Instance.WindowMessage("已复制到剪贴板");
        }

        private void OnDestroy()
        {
            StopGA();
        }

        private void OnApplicationQuit()
        {
            StopGA();
        }

    }
}
