// Decompiled with JetBrains decompiler
// Type: Elements.StatusParamShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll



//using LitJson;

namespace Elements
{
    public class StatusParamShort
    {
        public long Hp { get; private set; }

        public int Atk { get; private set; }

        public int Def { get; private set; }

        public int Matk { get; private set; }

        public int Mdef { get; private set; }

        public int Crt { get; private set; }

        public int Mcrt { get; private set; }

        public int Hrec { get; private set; }

        public int Erec { get; private set; }

        public int HrecRate { get; private set; }

        public int Pnt { get; private set; }

        public int Mpnt { get; private set; }

        public int LifeSteal { get; private set; }

        public int Dodge { get; private set; }

        public int ErecRate { get; private set; }

        public int EredRate { get; private set; }

        public int Accuracy { get; private set; }

        /*public void SetHp(long _hp) => this.Hp = (long) _hp;

        public void SetAtk(int _atk) => this.Atk = (int) _atk;

        public void SetDef(int _def) => this.Def = (int) _def;

        public void SetMatk(int _matk) => this.Matk = (int) _matk;

        public void SetMdef(int _mdef) => this.Mdef = (int) _mdef;

        public void SetCrt(int _crt) => this.Crt = (int) _crt;

        public void SetMcrt(int _mcrt) => this.Mcrt = (int) _mcrt;

        public void SetHrec(int _hrec) => this.Hrec = (int) _hrec;

        public void SetErec(int _erec) => this.Erec = (int) _erec;

        public void SetHrecRate(int _hrecRate) => this.HrecRate = (int) _hrecRate;

        public void SetPnt(int _pnt) => this.Pnt = (int) _pnt;

        public void SetMpnt(int _mpnt) => this.Mpnt = (int) _mpnt;

        public void SetLifeSteal(int _lifeSteal) => this.LifeSteal = (int) _lifeSteal;

        public void SetDodge(int _dodge) => this.Dodge = (int) _dodge;

        public void SetErecRate(int _erecRate) => this.ErecRate = (int) _erecRate;

        public void SetEredRate(int _eredRate) => this.EredRate = (int) _eredRate;

        public void SetAccuracy(int _accuracy) => this.Accuracy = (int) _accuracy;

        private void initializeStatusParamShort()
        {
          this.Hp = (long) 0L;
          this.Atk = (int) 0;
          this.Def = (int) 0;
          this.Matk = (int) 0;
          this.Mdef = (int) 0;
          this.Crt = (int) 0;
          this.Mcrt = (int) 0;
          this.Hrec = (int) 0;
          this.Erec = (int) 0;
          this.HrecRate = (int) 0;
          this.Pnt = (int) 0;
          this.Mpnt = (int) 0;
          this.LifeSteal = (int) 0;
          this.Dodge = (int) 0;
          this.ErecRate = (int) 0;
          this.EredRate = (int) 0;
          this.Accuracy = (int) 0;
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
            this.Hp = (long) _json["hp"].ToLong();
          if (_json.Keys.Contains("atk"))
            this.Atk = (int) _json["atk"].ToInt();
          if (_json.Keys.Contains("def"))
            this.Def = (int) _json["def"].ToInt();
          if (_json.Keys.Contains("matk"))
            this.Matk = (int) _json["matk"].ToInt();
          if (_json.Keys.Contains("mdef"))
            this.Mdef = (int) _json["mdef"].ToInt();
          if (_json.Keys.Contains("crt"))
            this.Crt = (int) _json["crt"].ToInt();
          if (_json.Keys.Contains("mcrt"))
            this.Mcrt = (int) _json["mcrt"].ToInt();
          if (_json.Keys.Contains("hrec"))
            this.Hrec = (int) _json["hrec"].ToInt();
          if (_json.Keys.Contains("erec"))
            this.Erec = (int) _json["erec"].ToInt();
          if (_json.Keys.Contains("hrec_rate"))
            this.HrecRate = (int) _json["hrec_rate"].ToInt();
          if (_json.Keys.Contains("pnt"))
            this.Pnt = (int) _json["pnt"].ToInt();
          if (_json.Keys.Contains("mpnt"))
            this.Mpnt = (int) _json["mpnt"].ToInt();
          if (_json.Keys.Contains("life_steal"))
            this.LifeSteal = (int) _json["life_steal"].ToInt();
          if (_json.Keys.Contains("dodge"))
            this.Dodge = (int) _json["dodge"].ToInt();
          if (_json.Keys.Contains("erec_rate"))
            this.ErecRate = (int) _json["erec_rate"].ToInt();
          if (_json.Keys.Contains("ered_rate"))
            this.EredRate = (int) _json["ered_rate"].ToInt();
          if (!_json.Keys.Contains("accuracy"))
            return;
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
