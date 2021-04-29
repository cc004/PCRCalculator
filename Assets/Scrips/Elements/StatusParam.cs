// Decompiled with JetBrains decompiler
// Type: Elements.StatusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using LitJson;

namespace Elements
{
    public class StatusParam
    {
        public ObscuredLong Hp { get; private set; }

        public ObscuredInt Atk { get; private set; }

        public ObscuredInt Def { get; private set; }

        public ObscuredInt MagicStr { get; private set; }

        public ObscuredInt MagicDef { get; private set; }

        public ObscuredInt PhysicalCritical { get; private set; }

        public ObscuredInt MagicCritical { get; private set; }

        public ObscuredInt WaveHpRecovery { get; private set; }

        public ObscuredInt WaveEnergyRecovery { get; private set; }

        public ObscuredInt HpRecoveryRate { get; private set; }

        public ObscuredInt PhysicalPenetrate { get; private set; }

        public ObscuredInt MagicPenetrate { get; private set; }

        public ObscuredInt LifeSteal { get; private set; }

        public ObscuredInt Dodge { get; private set; }

        public ObscuredInt EnergyReduceRate { get; private set; }

        public ObscuredInt EnergyRecoveryRate { get; private set; }

        public ObscuredInt Accuracy { get; private set; }

        /*public void SetHp(long _hp) => this.Hp = (ObscuredLong) _hp;

        public void SetAtk(int _atk) => this.Atk = (ObscuredInt) _atk;

        public void SetDef(int _def) => this.Def = (ObscuredInt) _def;

        public void SetMagicStr(int _magicStr) => this.MagicStr = (ObscuredInt) _magicStr;

        public void SetMagicDef(int _magicDef) => this.MagicDef = (ObscuredInt) _magicDef;

        public void SetPhysicalCritical(int _physicalCritical) => this.PhysicalCritical = (ObscuredInt) _physicalCritical;

        public void SetMagicCritical(int _magicCritical) => this.MagicCritical = (ObscuredInt) _magicCritical;

        public void SetWaveHpRecovery(int _waveHpRecovery) => this.WaveHpRecovery = (ObscuredInt) _waveHpRecovery;

        public void SetWaveEnergyRecovery(int _waveEnergyRecovery) => this.WaveEnergyRecovery = (ObscuredInt) _waveEnergyRecovery;

        public void SetHpRecoveryRate(int _hpRecoveryRate) => this.HpRecoveryRate = (ObscuredInt) _hpRecoveryRate;

        public void SetPhysicalPenetrate(int _physicalPenetrate) => this.PhysicalPenetrate = (ObscuredInt) _physicalPenetrate;

        public void SetMagicPenetrate(int _magicPenetrate) => this.MagicPenetrate = (ObscuredInt) _magicPenetrate;

        public void SetLifeSteal(int _lifeSteal) => this.LifeSteal = (ObscuredInt) _lifeSteal;

        public void SetDodge(int _dodge) => this.Dodge = (ObscuredInt) _dodge;

        public void SetEnergyReduceRate(int _energyReduceRate) => this.EnergyReduceRate = (ObscuredInt) _energyReduceRate;

        public void SetEnergyRecoveryRate(int _energyRecoveryRate) => this.EnergyRecoveryRate = (ObscuredInt) _energyRecoveryRate;

        public void SetAccuracy(int _accuracy) => this.Accuracy = (ObscuredInt) _accuracy;

        private void initializeStatusParam()
        {
          this.Hp = (ObscuredLong) 0L;
          this.Atk = (ObscuredInt) 0;
          this.Def = (ObscuredInt) 0;
          this.MagicStr = (ObscuredInt) 0;
          this.MagicDef = (ObscuredInt) 0;
          this.PhysicalCritical = (ObscuredInt) 0;
          this.MagicCritical = (ObscuredInt) 0;
          this.WaveHpRecovery = (ObscuredInt) 0;
          this.WaveEnergyRecovery = (ObscuredInt) 0;
          this.HpRecoveryRate = (ObscuredInt) 0;
          this.PhysicalPenetrate = (ObscuredInt) 0;
          this.MagicPenetrate = (ObscuredInt) 0;
          this.LifeSteal = (ObscuredInt) 0;
          this.Dodge = (ObscuredInt) 0;
          this.EnergyReduceRate = (ObscuredInt) 0;
          this.EnergyRecoveryRate = (ObscuredInt) 0;
          this.Accuracy = (ObscuredInt) 0;
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
          this.Hp = (ObscuredLong) _json["hp"].ToLong();
          this.Atk = (ObscuredInt) _json["atk"].ToInt();
          this.Def = (ObscuredInt) _json["def"].ToInt();
          this.MagicStr = (ObscuredInt) _json["magic_str"].ToInt();
          this.MagicDef = (ObscuredInt) _json["magic_def"].ToInt();
          this.PhysicalCritical = (ObscuredInt) _json["physical_critical"].ToInt();
          this.MagicCritical = (ObscuredInt) _json["magic_critical"].ToInt();
          this.WaveHpRecovery = (ObscuredInt) _json["wave_hp_recovery"].ToInt();
          this.WaveEnergyRecovery = (ObscuredInt) _json["wave_energy_recovery"].ToInt();
          this.HpRecoveryRate = (ObscuredInt) _json["hp_recovery_rate"].ToInt();
          this.PhysicalPenetrate = (ObscuredInt) _json["physical_penetrate"].ToInt();
          this.MagicPenetrate = (ObscuredInt) _json["magic_penetrate"].ToInt();
          this.LifeSteal = (ObscuredInt) _json["life_steal"].ToInt();
          this.Dodge = (ObscuredInt) _json["dodge"].ToInt();
          this.EnergyReduceRate = (ObscuredInt) _json["energy_reduce_rate"].ToInt();
          this.EnergyRecoveryRate = (ObscuredInt) _json["energy_recovery_rate"].ToInt();
          this.Accuracy = (ObscuredInt) _json["accuracy"].ToInt();
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
