// Decompiled with JetBrains decompiler
// Type: Elements.AttackSealData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;

namespace Elements
{
    public class AttackSealData
    {
        public enum eExecConditionType
        {
            DAMAGE_ONCE = 1,
            TARGET = 2,
            DAMAGE = 3,
            CRITICAL = 4,
            HIT = 5
        }
        public float LimitTime { get; set; }
        public float SealEffectTime { get; set; }
        public eStateIconType IconType { get; set; }
        public int ActionId { get; set; }
        public AttackSealData.eExecConditionType ExecConditionType { get; set; }


        public void AddSeal(UnitCtrl _target)
        {
            SealData seal = _target.SealDictionary[IconType];
            if (seal.GetCurrentCount() == 0)
            {
                _target.OnChangeState.Call(_target, IconType, true);
            }

            seal.AddSeal(LimitTime, _target, IconType, 1);
        }
    }

    // Namespace: Elements
    public class AttackSealDataForAllEnemy // TypeDefIndex: 1461
    {
        // Fields
        public float LimitTime; // 0x10
        public float SealEffectTime; // 0x14
        public eStateIconType iconType; // 0x1C
        public int maxCount; // 0x20
        public bool isDisplayCount; // 0x24

        // Properties
        public AttackSealData.eExecConditionType ExecConditionType { get; set; }

        // RVA: 0x121B3E0 Offset: 0x121B3E0 VA: 0x7FFD9D2BB3E0
        public void AddSeal(UnitCtrl _target)
        {
            if (!_target.SealDictionary.ContainsKey(iconType))
            {
                SealData sealData = new SealData
                {
                    Max = maxCount,
                    DisplayCount = isDisplayCount
                };
                _target.SealDictionary.Add(iconType, sealData);
            }

            SealData seal = _target.SealDictionary[iconType];
            if (seal.GetCurrentCount() == 0)
            {
                _target.OnChangeState.Call(_target, iconType, true);
            }

            seal.AddSeal(LimitTime, _target, iconType, 1);
        }
    }

}
