// Decompiled with JetBrains decompiler
// Type: Elements.UnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

//using LitJson;

namespace Elements
{
  public class UnitParam
  {
    public StatusParam BaseParam { get; private set; }

    public StatusParam EquipParam { get; private set; }

    /*public void SetBaseParam(StatusParam _baseParam) => this.BaseParam = _baseParam;

    public void SetEquipParam(StatusParam _equipParam) => this.EquipParam = _equipParam;

    private void initializeUnitParam()
    {
      this.BaseParam = (StatusParam) null;
      this.EquipParam = (StatusParam) null;
    }

    public UnitParam() => this.initializeUnitParam();

    public UnitParam(JsonData _json)
    {
      this.initializeUnitParam();
      this.ParseUnitParam(_json);
    }

    public void ParseUnitParam(JsonData _json)
    {
      if (_json.Count == 0)
        return;
      if (_json.Keys.Contains("base_param"))
      {
        JsonData _json1 = _json["base_param"];
        if (_json1 != null)
          this.BaseParam = new StatusParam(_json1);
      }
      if (!_json.Keys.Contains("equip_param"))
        return;
      JsonData _json2 = _json["equip_param"];
      if (_json2 == null)
        return;
      this.EquipParam = new StatusParam(_json2);
    }*/
  }
}
