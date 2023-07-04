using SQLite4Unity3d;
using System.Collections.Generic;

namespace Elements
{
	public class MasterUnitSkillDataRf
	{
		[Table("unit_skill_data_rf")]
		public class UnitSkillDataRf
		{
			public int id { get; set; }

			public int skill_id { get; set; }

			public int rf_skill_id { get; set; }

			public int min_lv { get; set; }

			public int max_lv { get; set; }
		}

		public List<UnitSkillDataRf> GetListWithSkillId(int skill_id)
		{
			return dict.TryGetValue(skill_id, out var val) ? val : new List<UnitSkillDataRf>();
		}

		public Dictionary<int, List<UnitSkillDataRf>> dict;
	}
}

