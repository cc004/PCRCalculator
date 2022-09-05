using System.Collections.Generic;

namespace Elements
{
	public class MasterUnitSkillDataRf : AbstractMasterData
	{
		public class UnitSkillDataRf
		{
			protected int _id;

			protected int _skill_id;

			protected int _rf_skill_id;

			protected int _min_lv;

			protected int _max_lv;

			public int id => _id;

			public int skill_id => _skill_id;

			public int rf_skill_id => _rf_skill_id;

			public int min_lv => _min_lv;

			public int max_lv => _max_lv;

			public UnitSkillDataRf(int id = 0, int skill_id = 0, int rf_skill_id = 0, int min_lv = 0, int max_lv = 0)
			{
				_id = id;
				_skill_id = skill_id;
				_rf_skill_id = rf_skill_id;
				_min_lv = min_lv;
				_max_lv = max_lv;
			}
		}

		//public const string TABLE_NAME = "unit_skill_data_rf";





		//public static MasterUnitSkillDataRf Instance = new MasterUnitSkillDataRf();
		
		

		

		public List<UnitSkillDataRf> GetListWithSkillId(int skill_id)
		{
			return new List<UnitSkillDataRf>();
		}

		public List<UnitSkillDataRf> MaybeListWithSkillId(int skill_id)
		{
			List<UnitSkillDataRf> listWithSkillId = GetListWithSkillId(skill_id);
			if (listWithSkillId.Count <= 0)
			{
				return null;
			}
			return listWithSkillId;
		}

		

		public List<UnitSkillDataRf> GetListWithRfSkillId(int rf_skill_id)
		{
			return new List<UnitSkillDataRf>();

		}

		public List<UnitSkillDataRf> MaybeListWithRfSkillId(int rf_skill_id)
		{
			List<UnitSkillDataRf> listWithRfSkillId = GetListWithRfSkillId(rf_skill_id);
			if (listWithRfSkillId.Count <= 0)
			{
				return null;
			}
			return listWithRfSkillId;
		}

		

		

		
	}
}

