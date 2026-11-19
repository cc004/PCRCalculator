namespace Elements
{
    /// <summary>
    /// action_type 113 UNABLE_STATE_GUARD_FOR_EXTRA_EFFECT。
    /// 与 100 UNABLE_STATE_GUARD 共用同一套「不可状态耐性」逻辑，但：
    /// 1) 图标用 UNABLE_STATE_GUARD_EXTRA_EFFECT(165)；
    /// 2) 耐性数据存放在 UnitCtrl.UnableStateGuardForExtraEffectData（与 100 的 UnableStateGuardData 分离）。
    /// </summary>
    public class UnableStateGuardForExtraEffectAction : UnableStateGuardAction
    {
        protected override eStateIconType IconType
        {
            get { return eStateIconType.UNABLE_STATE_GUARD_EXTRA_EFFECT; }
        }

        protected override SealData GetSealData(UnitCtrl _owner)
        {
            return _owner == null ? null : _owner.UnableStateGuardForExtraEffectData;
        }

        protected override void SetSealData(UnitCtrl _owner, SealData _data)
        {
            if (_owner != null) _owner.UnableStateGuardForExtraEffectData = _data;
        }
    }
}
