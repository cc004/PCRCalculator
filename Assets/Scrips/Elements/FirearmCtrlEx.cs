// Decompiled with JetBrains decompiler
// Type: Elements.FirearmCtrlEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using UnityEngine;

namespace Elements
{
  [ExecuteAlways]
  public class FirearmCtrlEx : FirearmCtrl
  {
    protected override Vector3 getHeadBonePos(BasePartsData _target) => BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(_target.GetStateBone(), _target.Owner.RotateCenter, _target.GetBottomLossyScale());
  }
}
