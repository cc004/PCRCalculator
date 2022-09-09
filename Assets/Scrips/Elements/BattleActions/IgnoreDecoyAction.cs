
namespace Elements
{
  public class IgnoreDecoyAction : ActionParameter
  {
    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      IgnoreDecoy = true;
      foreach (int actionChildrenIndex in ActionChildrenIndexes)
        _skill.ActionParameters[actionChildrenIndex].IgnoreDecoy = true;
    }
  }
}
