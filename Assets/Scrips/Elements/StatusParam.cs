// Decompiled with JetBrains decompiler
// Type: Elements.StatusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll



//using LitJson;

namespace Elements
{
    public class StatusParam
    {
        public long Hp { get; private set; }

        public int Atk { get; private set; }

        public int Def { get; private set; }

        public int MagicStr { get; private set; }

        public int MagicDef { get; private set; }

        public int PhysicalCritical { get; private set; }

        public int MagicCritical { get; private set; }

        public int WaveHpRecovery { get; private set; }

        public int WaveEnergyRecovery { get; private set; }

        public int HpRecoveryRate { get; private set; }

        public int PhysicalPenetrate { get; private set; }

        public int MagicPenetrate { get; private set; }

        public int LifeSteal { get; private set; }

        public int Dodge { get; private set; }

        public int EnergyReduceRate { get; private set; }

        public int EnergyRecoveryRate { get; private set; }

        public int Accuracy { get; private set; }

        /*public void SetHp(long _hp) => this.Hp = (long) _hp;

        public void SetAtk(int _atk) => this.Atk = (int) _atk;

        public void SetDef(int _def) => this.Def = (int) _def;

        public void SetMagicStr(int _magicStr) => this.MagicStr = (int) _magicStr;

        public void SetMagicDef(int _magicDef) => this.MagicDef = (int) _magicDef;

        public void SetPhysicalCritical(int _physicalCritical) => this.PhysicalCritical = (int) _physicalCritical;

        public void SetMagicCritical(int _magicCritical) => this.MagicCritical = (int) _magicCritical;

        public void SetWaveHpRecovery(int _waveHpRecovery) => this.WaveHpRecovery = (int) _waveHpRecovery;

        public void SetWaveEnergyRecovery(int _waveEnergyRecovery) => this.WaveEnergyRecovery = (int) _waveEnergyRecovery;

        public void SetHpRecoveryRate(int _hpRecoveryRate) => this.HpRecoveryRate = (int) _hpRecoveryRate;

        public void SetPhysicalPenetrate(int _physicalPenetrate) => this.PhysicalPenetrate = (int) _physicalPenetrate;

        public void SetMagicPenetrate(int _magicPenetrate) => this.MagicPenetrate = (int) _magicPenetrate;

        public void SetLifeSteal(int _lifeSteal) => this.LifeSteal = (int) _lifeSteal;

        public void SetDodge(int _dodge) => this.Dodge = (int) _dodge;

        public void SetEnergyReduceRate(int _energyReduceRate) => this.EnergyReduceRate = (int) _energyReduceRate;

        public void SetEnergyRecoveryRate(int _energyRecoveryRate) => this.EnergyRecoveryRate = (int) _energyRecoveryRate;

        public void SetAccuracy(int _accuracy) => this.Accuracy = (int) _accuracy;

        private void initializeStatusParam()
        {
          this.Hp = (long) 0L;
          this.Atk = (int) 0;
          this.Def = (int) 0;
          this.MagicStr = (int) 0;
          this.MagicDef = (int) 0;
          this.PhysicalCritical = (int) 0;
          this.MagicCritical = (int) 0;
          this.WaveHpRecovery = (int) 0;
          this.WaveEnergyRecovery = (int) 0;
          this.HpRecoveryRate = (int) 0;
          this.PhysicalPenetrate = (int) 0;
          this.MagicPenetrate = (int) 0;
          this.LifeSteal = (int) 0;
          this.Dodge = (int) 0;
          this.EnergyReduceRate = (int) 0;
          this.EnergyRecoveryRate = (int) 0;
          this.Accuracy = (int) 0;
        }

        public StatusParam() => this.initializeStatusParam();

        public StatusParam(JsonData _json)
        {
          this.initializeStatusParam();
          this.ParseStatusParam(_json);
        }

        public void ParseStatusParam(JsonData _json)
        {
          if (_json.Count == 0)
            return;
          this.Hp = (long) _json["hp"].ToLong();
          this.Atk = (int) _json["atk"].ToInt();
          this.Def = (int) _json["def"].ToInt();
          this.MagicStr = (int) _json["magic_str"].ToInt();
          this.MagicDef = (int) _json["magic_def"].ToInt();
          this.PhysicalCritical = (int) _json["physical_critical"].ToInt();
          this.MagicCritical = (int) _json["magic_critical"].ToInt();
          this.WaveHpRecovery = (int) _json["wave_hp_recovery"].ToInt();
          this.WaveEnergyRecovery = (int) _json["wave_energy_recovery"].ToInt();
          this.HpRecoveryRate = (int) _json["hp_recovery_rate"].ToInt();
          this.PhysicalPenetrate = (int) _json["physical_penetrate"].ToInt();
          this.MagicPenetrate = (int) _json["magic_penetrate"].ToInt();
          this.LifeSteal = (int) _json["life_steal"].ToInt();
          this.Dodge = (int) _json["dodge"].ToInt();
          this.EnergyReduceRate = (int) _json["energy_reduce_rate"].ToInt();
          this.EnergyRecoveryRate = (int) _json["energy_recovery_rate"].ToInt();
          this.Accuracy = (int) _json["accuracy"].ToInt();
        }

        public long GetParam(eParamType _paramType)
        {
          switch (_paramType)
          {
            case eParamType.HP:
              return (long) this.Hp;
            case eParamType.ATK:
              return (long) (int) this.Atk;
            case eParamType.DEF:
              return (long) (int) this.Def;
            case eParamType.MAGIC_ATK:
              return (long) (int) this.MagicStr;
            case eParamType.MAGIC_DEF:
              return (long) (int) this.MagicDef;
            case eParamType.PHYSICAL_CRITICAL:
              return (long) (int) this.PhysicalCritical;
            case eParamType.MAGIC_CRITICAL:
              return (long) (int) this.MagicCritical;
            case eParamType.DODGE:
              return (long) (int) this.Dodge;
            case eParamType.LIFE_STEAL:
              return (long) (int) this.LifeSteal;
            case eParamType.WAVE_HP_RECOVERY:
              return (long) (int) this.WaveHpRecovery;
            case eParamType.WAVE_ENERGY_RECOVERY:
              return (long) (int) this.WaveEnergyRecovery;
            case eParamType.PHYSICAL_PENETRATE:
              return (long) (int) this.PhysicalPenetrate;
            case eParamType.MAGIC_PENETRATE:
              return (long) (int) this.MagicPenetrate;
            case eParamType.ENERGY_RECOVERY_RATE:
              return (long) (int) this.EnergyRecoveryRate;
            case eParamType.HP_RECOVERY_RATE:
              return (long) (int) this.HpRecoveryRate;
            case eParamType.ENERGY_REDUCE_RATE:
              return (long) (int) this.EnergyReduceRate;
            case eParamType.ACCURACY:
              return (long) (int) this.Accuracy;
            default:
              return 0;
          }
        }*/
    }
}
