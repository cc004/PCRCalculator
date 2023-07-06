using PCRCaculator;
using PCRCaculator.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    public static BaseData CalcUniqueValue(this unique_equip_enhance_rate[] rates, int level)
    {
        BaseData s = rates[0].baseData.GetBaseData();
        
        foreach (var rate in rates)
        {
            if (level < rate.min_lv) break;
            s += rate.GetBaseData() * ((rate.max_lv == -1 ? level : Math.Min(rate.max_lv, level)) - rate.min_lv + 1);
        }

        return BaseData.Round(BaseData.CeilToInt(s));
    }
}
