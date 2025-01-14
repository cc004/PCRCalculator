﻿using System;
using System.ComponentModel;

namespace Elements
{
	// Token: 0x02000EFA RID: 3834
	public enum eStateIconType
	{
		[Description("关闭所有")]
		// Token: 0x040087DD RID: 34781
		NONE,
		// Token: 0x040087DE RID: 34782
		BUFF_PHYSICAL_ATK,
		// Token: 0x040087DF RID: 34783
		BUFF_PHYSICAL_DEF,
		// Token: 0x040087E0 RID: 34784
		BUFF_MAGIC_ATK,
		// Token: 0x040087E1 RID: 34785
		BUFF_MAGIC_DEF,
		// Token: 0x040087E2 RID: 34786
		BUFF_DODGE,
		// Token: 0x040087E3 RID: 34787
		BUFF_CRITICAL,
		// Token: 0x040087E4 RID: 34788
		BUFF_ENERGY_RECOVERY,
		// Token: 0x040087E5 RID: 34789
		[Description("Hp回复buff")]
    BUFF_HP_RECOVERY,
		// Token: 0x040087E6 RID: 34790
		HASTE,
		// Token: 0x040087E7 RID: 34791
		NO_DAMAGE,
		// Token: 0x040087E8 RID: 34792
		BUFF_LIFE_STEAL,
    // Token: 0x040087E9 RID: 34793
    [Description("Hp吸收+1次\n妹法ub/松鼠ub")]
    BUFF_ADD_LIFE_STEAL,
		// Token: 0x040087EA RID: 34794
		DEBUFF_PHYSICAL_ATK,
		// Token: 0x040087EB RID: 34795
		DEBUFF_PHYSICAL_DEF,
		// Token: 0x040087EC RID: 34796
		DEBUFF_MAGIC_ATK,
		// Token: 0x040087ED RID: 34797
		DEBUFF_MAGIC_DEF,
		// Token: 0x040087EE RID: 34798
		DEBUFF_DODGE,
		// Token: 0x040087EF RID: 34799
		DEBUFF_CRITICAL,
		// Token: 0x040087F0 RID: 34800
		DEBUFF_ENERGY_RECOVERY,
		// Token: 0x040087F1 RID: 34801
		[Description("Hp回复debuff")]
    DEBUFF_HP_RECOVERY,
		// Token: 0x040087F2 RID: 34802
		DEBUFF_LIFE_STEAL,
		// Token: 0x040087F3 RID: 34803
		SLOW,
		// Token: 0x040087F4 RID: 34804
		[Description("禁用ub")]
    UB_DISABLE,
		// Token: 0x040087F5 RID: 34805
		PHYSICS_BARRIAR,
		// Token: 0x040087F6 RID: 34806
		MAGIC_BARRIAR,
		// Token: 0x040087F7 RID: 34807
		PHYSICS_DRAIN_BARRIAR,
		// Token: 0x040087F8 RID: 34808
		MAGIC_DRAIN_BARRIAR,
		// Token: 0x040087F9 RID: 34809
		BOTH_BARRIAR,
		// Token: 0x040087FA RID: 34810
		BOTH_DRAIN_BARRIAR,
		// Token: 0x040087FB RID: 34811
		DEBUF_BARRIAR,
    // Token: 0x040087FC RID: 34812		
    [Description("反击\n露sk1/圣电ub")]
    STRIKE_BACK,
		// Token: 0x040087FD RID: 34813
		PARALISYS,
		// Token: 0x040087FE RID: 34814
		SLIP_DAMAGE,
		// Token: 0x040087FF RID: 34815
		PHYSICS_DARK,
		// Token: 0x04008800 RID: 34816
		SILENCE,
		// Token: 0x04008801 RID: 34817
		CONVERT,
		// Token: 0x04008802 RID: 34818
		DECOY,
		// Token: 0x04008803 RID: 34819
		BURN,
		// Token: 0x04008804 RID: 34820
		CURSE,
		// Token: 0x04008805 RID: 34821
		FREEZE,
		// Token: 0x04008806 RID: 34822
		CHAINED,
		// Token: 0x04008807 RID: 34823
		SLEEP,
		// Token: 0x04008808 RID: 34824
		STUN,
		// Token: 0x04008809 RID: 34825
		STONE,
		// Token: 0x0400880A RID: 34826
		DETAIN,
		// Token: 0x0400880B RID: 34827
		REGENERATION,
		// Token: 0x0400880C RID: 34828
		DEBUFF_MOVE_SPEED,
		// Token: 0x0400880D RID: 34829
		PHYSICS_DODGE,
		// Token: 0x0400880E RID: 34830
		CONFUSION,
		// Token: 0x0400880F RID: 34831
		[Description("英灵的加护\n安sk2")]
    HEROIC_SPIRIT_SEAL,
		// Token: 0x04008810 RID: 34832
		VENOM,
		// Token: 0x04008811 RID: 34833
		[Description("黑暗次数\n拉姆sk2")]
    COUNT_BLIND,
		// Token: 0x04008812 RID: 34834
		INHIBIT_HEAL,
		// Token: 0x04008813 RID: 34835
		FEAR,
		// Token: 0x04008814 RID: 34836
		[Description("吸魂\n(非会战)")]
    SOUL_EAT = 56,
		// Token: 0x04008815 RID: 34837
		[Description("畏缩\n克罗依sk1")]
    CHLOE,
		// Token: 0x04008816 RID: 34838
		[Description("火焰果\n(非会战)")]
    FIRE_NUTS,
		// Token: 0x04008817 RID: 34839
		[Description("反物质兽\n(非会战)")]
    AWE,
		// Token: 0x04008818 RID: 34840
		[Description("朋友\n露娜sk1")]
    LUNA,
		// Token: 0x04008819 RID: 34841
		[Description("硬币\n圣克ub")]
    CHRISTINA,
		// Token: 0x0400881A RID: 34842
		TP_REGENERATION,
    // Token: 0x0400881B RID: 34843
    [Description("外遇星\n(非会战)")]
    CHEATING_STAR,
    // Token: 0x0400881C RID: 34844
    [Description("驯鹿\n(非会战)")]
    TONAKAI,
		// Token: 0x0400881D RID: 34845
		HEX,
		// Token: 0x0400881E RID: 34846
		FAINT,
		// Token: 0x0400881F RID: 34847
		BUFF_PHYSICAL_CRITICAL_DAMAGE,
		// Token: 0x04008820 RID: 34848
		DEBUFF_PHYSICAL_CRITICAL_DAMAGE,
		// Token: 0x04008821 RID: 34849
		BUFF_MAGIC_CRITICAL_DAMAGE,
		// Token: 0x04008822 RID: 34850
		DEBUFF_MAGIC_CRITICAL_DAMAGE,
		// Token: 0x04008823 RID: 34851
		COMPENSATION,
    // Token: 0x04008824 RID: 34852
    [Description("骑士的加护\n高达ub")]
    KNIGHT_GUARD,
		// Token: 0x04008825 RID: 34853
		CUT_ATK_DAMAGE,
		// Token: 0x04008826 RID: 34854
		CUT_MGC_DAMAGE,
		// Token: 0x04008827 RID: 34855
		CUT_ALL_DAMAGE,
    // Token: 0x04008828 RID: 34856
    [Description("切噜")]
    CHIERU,
    // Token: 0x04008829 RID: 34857
    [Description("风之刃\n剑圣ub")]
    REI,
		// Token: 0x0400882A RID: 34858
		LOG_ATK_BARRIER,
		// Token: 0x0400882B RID: 34859
		LOG_MGC_BARRIER,
		// Token: 0x0400882C RID: 34860
		LOG_ALL_BARRIER,
		// Token: 0x0400882D RID: 34861
		PAUSE_ACTION,
		// Token: 0x0400882E RID: 34862
		BUFF_ACCURACY = 83,
		// Token: 0x0400882F RID: 34863
		DEBUFF_ACCURACY,
		// Token: 0x04008830 RID: 34864
		BOSS_BUFF,
		// Token: 0x04008831 RID: 34865
		UB_SILENCE,
    // Token: 0x04008832 RID: 34866
    [Description("CUPID-丘比特\n（不明）")]
    CUPID,
    // Token: 0x04008833 RID: 34867
    [Description("最大Hp值debuff")]
    DEBUFF_MAX_HP,
		// Token: 0x04008834 RID: 34868
		MAGIC_DARK,
    // Token: 0x04008835 RID: 34869
    [Description("瓜虎ub层数")]
    MATSURI,
		// Token: 0x04008836 RID: 34870
		HEAL_DOWN,
    // Token: 0x04008837 RID: 34871
    [Description("圣夜的光辉\n圣哈sk1")]
    AKINO_CHRISTMAS,
		// Token: 0x04008838 RID: 34872
		NPC_STUN,
		// Token: 0x04008839 RID: 34873
		BUFF_RECEIVE_CRITICAL_DAMAGE,
		// Token: 0x0400883A RID: 34874
		DEBUFF_RECEIVE_CRITICAL_DAMAGE,
    // Token: 0x0400883B RID: 34875
    [Description("减少治疗")]
    DECREASE_HEAL,
    // Token: 0x0400883C RID: 34876
    [Description("冰龙之印\n雪菲ub")]
    SHEFI,
    // Token: 0x0400883D RID: 34877
    [Description("学习时间\n学优ub")]
    SCHOOL_FESTIVAL_YUNI,
    // Token: 0x0400883E RID: 34878
    [Description("圣华计数器")]
    SCHOOL_FESTIVAL_CHLOE,
    // Token: 0x0400883F RID: 34879
    [Description("行为中毒")]
    POISON_BY_BEHAVIOUR,
    // Token: 0x04008840 RID: 34880
    [Description("额外物防buff")]
    ADDITIONAL_BUFF_PHYSICAL_DEF,
    // Token: 0x04008841 RID: 34881
    [Description("CRYSTALIZE\n结晶（不明）")]
    CRYSTALIZE,
    // Token: 0x04008842 RID: 34882
    [Description("伤害限制")]
    DAMAGE_LIMIT,
    // Token: 0x04008843 RID: 34883
    [Description("额外魔防buff")]
    ADDITIONAL_BUFF_MAGIC_DEF,
		// Token: 0x04008844 RID: 34884
		MAGIC_CHARACTER_OF_WISDOM,
		// Token: 0x04008845 RID: 34885
		MAGIC_CHARACTER_OF_POWER,
    // Token: 0x04008846 RID: 34886
    [Description("发现弱点")]
    DETECT_WEAKNESS,
    // Token: 0x04008847 RID: 34887
    DEBUFF_RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT,
		// Token: 0x04008848 RID: 34888
		DEBUFF_RECEIVE_PHYSICAL_DAMAGE_PERCENT,
		// Token: 0x04008849 RID: 34889
		DEBUFF_RECEIVE_MAGIC_DAMAGE_PERCENT,
    // Token: 0x0400884A RID: 34890
    [Description("超晶ub标记")]
    LABYRISTA_OVERLOAD,
    // Token: 0x0400884B RID: 34891
    [Description("剑之刻印\n春流夏sp1")]
    SWORD_SEAL,
    // Token: 0x0400884C RID: 34892
    [Description("灵锚刻印\n海忍sp1")]
    PHANTOMCORE_WEDGE,
    // Token: 0x0400884D RID: 34893
    [Description("隐身\n路人妹sk1")]
    SPY,
    // Token: 0x0400884E RID: 34894
    [Description("幸福一刻\n野骑sk1")]
    HAPPY_MOMENT,
    // Token: 0x0400884F RID: 34895
    [Description("水刃加护\n水怜ub")]
    SEA_GOD_PROTECTION,
    // Token: 0x04008850 RID: 34896
    [Description("蝶舞烙印\n水仓唯sp1")]
    BLUE_MAGIC_SEAL,
    // Token: 0x04008851 RID: 34897
    [Description("软绵绵羊毛\n屁狐sk1")]
    SHEEP,
    // Token: 0x04008852 RID: 34898
    [Description("永夜加护\n伊莉亚ub")]
    TWILIGHT_GUARD,
    // Token: 0x04008853 RID: 34899
    [Description("灵力\n忍ub")]
    PSYCHIC_POWER,
    // Token: 0x04008854 RID: 34900
    [Description("月之光印\n（非会战）")]
    CELESTIAL_BODIES,
    // Token: 0x04008855 RID: 34901
    [Description("霸瞳sk2标记")]
    KAISER_INSIGHT_CARVED_SEAL,
    // Token: 0x04008856 RID: 34902
    [Description("点赞\n（非会战）")]
    LIKE,
    // Token: 0x04008857 RID: 34903
    [Description("减少Tp损失")]
    ENERGY_DAMAGE_REDUCE,
    // Token: 0x04008858 RID: 34904
    [Description("矛衣未ub标记")]
    SAGITTARIUS_CARVED_SEAL,
    // Token: 0x04008859 RID: 34905
    [Description("友情的魔印\n龙安sk2")]
    ANNE_AND_GLARE_CARVED_SEAL,
    // Token: 0x0400885A RID: 34906
    [Description("春月sk1标记")]
    MITSUKI_NY_CARVED_SEAL,
		// Token: 0x0400885B RID: 34907
		BLACK_FRAME,
		// Token: 0x0400885C RID: 34908
		UNABLE_STATE_GUARD,
		// Token: 0x0400885D RID: 34909
    [Description("羁绊之证\n513-ub")]
		MUIMI_ANNIVERSARY_CARVED_SEAL,
    // Token: 0x0400885E RID: 34910
    [Description("PSI★蓄力\n美空sp1")]
    MISORA_CARVED_SEAL,
		// Token: 0x0400885F RID: 34911
		EX_PASSIVE_1 = 999,
		[Description("重叠加速")]
		SPEED_OVERLAP,
		// Token: 0x04008860 RID: 34912
		NUM,
		// Token: 0x04008861 RID: 34913
		INVALID_VALUE = -1
	}
}
