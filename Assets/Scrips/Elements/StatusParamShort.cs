// Decompiled with JetBrains decompiler
// Type: Elements.StatusParamShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using LitJson;

namespace Elements
{
    public class StatusParamShort
    {
        public ObscuredLong Hp { get; private set; }

        public ObscuredInt Atk { get; private set; }

        public ObscuredInt Def { get; private set; }

        public ObscuredInt Matk { get; private set; }

        public ObscuredInt Mdef { get; private set; }

        public ObscuredInt Crt { get; private set; }

        public ObscuredInt Mcrt { get; private set; }

        public ObscuredInt Hrec { get; private set; }

        public ObscuredInt Erec { get; private set; }

        public ObscuredInt HrecRate { get; private set; }

        public ObscuredInt Pnt { get; private set; }

        public ObscuredInt Mpnt { get; private set; }

        public ObscuredInt LifeSteal { get; private set; }

        public ObscuredInt Dodge { get; private set; }

        public ObscuredInt ErecRate { get; private set; }

        public ObscuredInt EredRate { get; private set; }

        public ObscuredInt Accuracy { get; private set; }

        /*public void SetHp(long _hp) => this.Hp = (ObscuredLong) _hp;

        public void SetAtk(int _atk) => this.Atk = (ObscuredInt) _atk;

        public void SetDef(int _def) => this.Def = (ObscuredInt) _def;

        public void SetMatk(int _matk) => this.Matk = (ObscuredInt) _matk;

        public void SetMdef(int _mdef) => this.Mdef = (ObscuredInt) _mdef;

        public void SetCrt(int _crt) => this.Crt = (ObscuredInt) _crt;

        public void SetMcrt(int _mcrt) => this.Mcrt = (ObscuredInt) _mcrt;

        public void SetHrec(int _hrec) => this.Hrec = (ObscuredInt) _hrec;

        public void SetErec(int _erec) => this.Erec = (ObscuredInt) _erec;

        public void SetHrecRate(int _hrecRate) => this.HrecRate = (ObscuredInt) _hrecRate;

        public void SetPnt(int _pnt) => this.Pnt = (ObscuredInt) _pnt;

        public void SetMpnt(int _mpnt) => this.Mpnt = (ObscuredInt) _mpnt;

        public void SetLifeSteal(int _lifeSteal) => this.LifeSteal = (ObscuredInt) _lifeSteal;

        public void SetDodge(int _dodge) => this.Dodge = (ObscuredInt) _dodge;

        public void SetErecRate(int _erecRate) => this.ErecRate = (ObscuredInt) _erecRate;

        public void SetEredRate(int _eredRate) => this.EredRate = (ObscuredInt) _eredRate;

        public void SetAccuracy(int _accuracy) => this.Accuracy = (ObscuredInt) _accuracy;

        private void initializeStatusParamShort()
        {
          this.Hp = (ObscuredLong) 0L;
          this.Atk = (ObscuredInt) 0;
          this.Def = (ObscuredInt) 0;
          this.Matk = (ObscuredInt) 0;
          this.Mdef = (ObscuredInt) 0;
          this.Crt = (ObscuredInt) 0;
          this.Mcrt = (ObscuredInt) 0;
          this.Hrec = (ObscuredInt) 0;
          this.Erec = (ObscuredInt) 0;
          this.HrecRate = (ObscuredInt) 0;
          this.Pnt = (ObscuredInt) 0;
          this.Mpnt = (ObscuredInt) 0;
          this.LifeSteal = (ObscuredInt) 0;
          this.Dodge = (ObscuredInt) 0;
          this.ErecRate = (ObscuredInt) 0;
          this.EredRate = (ObscuredInt) 0;
          this.Accuracy = (ObscuredInt) 0;
        }

        public StatusParamShort() => this.initializeStatusParamShort();

        public StatusParamShort(JsonData _json)
        {
          this.initializeStatusParamShort();
          this.ParseStatusParamShort(_json);
        }

        public void ParseStatusParamShort(JsonData _json)
        {
          if (_json.Count == 0)
            return;
          if (_json.Keys.Contains("hp"))
            this.Hp = (ObscuredLong) _json["hp"].ToLong();
          if (_json.Keys.Contains("atk"))
            this.Atk = (ObscuredInt) _json["atk"].ToInt();
          if (_json.Keys.Contains("def"))
            this.Def = (ObscuredInt) _json["def"].ToInt();
          if (_json.Keys.Contains("matk"))
            this.Matk = (ObscuredInt) _json["matk"].ToInt();
          if (_json.Keys.Contains("mdef"))
            this.Mdef = (ObscuredInt) _json["mdef"].ToInt();
          if (_json.Keys.Contains("crt"))
            this.Crt = (ObscuredInt) _json["crt"].ToInt();
          if (_json.Keys.Contains("mcrt"))
            this.Mcrt = (ObscuredInt) _json["mcrt"].ToInt();
          if (_json.Keys.Contains("hrec"))
            this.Hrec = (ObscuredInt) _json["hrec"].ToInt();
          if (_json.Keys.Contains("erec"))
            this.Erec = (ObscuredInt) _json["erec"].ToInt();
          if (_json.Keys.Contains("hrec_rate"))
            this.HrecRate = (ObscuredInt) _json["hrec_rate"].ToInt();
          if (_json.Keys.Contains("pnt"))
            this.Pnt = (ObscuredInt) _json["pnt"].ToInt();
          if (_json.Keys.Contains("mpnt"))
            this.Mpnt = (ObscuredInt) _json["mpnt"].ToInt();
          if (_json.Keys.Contains("life_steal"))
            this.LifeSteal = (ObscuredInt) _json["life_steal"].ToInt();
          if (_json.Keys.Contains("dodge"))
            this.Dodge = (ObscuredInt) _json["dodge"].ToInt();
          if (_json.Keys.Contains("erec_rate"))
            this.ErecRate = (ObscuredInt) _json["erec_rate"].ToInt();
          if (_json.Keys.Contains("ered_rate"))
            this.EredRate = (ObscuredInt) _json["ered_rate"].ToInt();
          if (!_json.Keys.Contains("accuracy"))
            return;
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
              return (long) (int) this.Matk;
            case eParamType.MAGIC_DEF:
              return (long) (int) this.Mdef;
            case eParamType.PHYSICAL_CRITICAL:
              return (long) (int) this.Crt;
            case eParamType.MAGIC_CRITICAL:
              return (long) (int) this.Mcrt;
            case eParamType.DODGE:
              return (long) (int) this.Dodge;
            case eParamType.LIFE_STEAL:
              return (long) (int) this.LifeSteal;
            case eParamType.WAVE_HP_RECOVERY:
              return (long) (int) this.Hrec;
            case eParamType.WAVE_ENERGY_RECOVERY:
              return (long) (int) this.Erec;
            case eParamType.PHYSICAL_PENETRATE:
              return (long) (int) this.Pnt;
            case eParamType.MAGIC_PENETRATE:
              return (long) (int) this.Mpnt;
            case eParamType.ENERGY_RECOVERY_RATE:
              return (long) (int) this.ErecRate;
            case eParamType.HP_RECOVERY_RATE:
              return (long) (int) this.HrecRate;
            case eParamType.ENERGY_REDUCE_RATE:
              return (long) (int) this.EredRate;
            case eParamType.ACCURACY:
              return (long) (int) this.Accuracy;
            default:
              return 0;
          }
        }*/
    }
}
