// Decompiled with JetBrains decompiler
// Type: Elements.AttackSealData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;

namespace Elements
{
    public class AttackSealData
    {
        public float LimitTime { get; set; }

        public eStateIconType IconType { get; set; }

        public int ActionId { get; set; }

        public bool OnlyCritical { get; set; }

        public void AddSeal(UnitCtrl _target)
        {
            SealData seal = _target.SealDictionary[this.IconType];
            if (seal.GetCurrentCount() == 0)
            {
                _target.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target, this.IconType, true);
                _target.MyOnChangeAbnormalState?.Invoke(_target, this.IconType, true, this.LimitTime, "NaN");
            }
            seal.AddSeal(this.LimitTime, _target, this.IconType, 1);
        }
    }
}
