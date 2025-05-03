using System;
using System.ComponentModel;

namespace Elements
{
	// Token: 0x02000EFA RID: 3834
	public enum eStateIconType
	{
        // Token: 0x040087DD RID: 34781
		[Description("关闭所有")]
        NONE = 0,
        // Token: 0x040087DE RID: 34782
        [Description("物攻BUFF")]
        BUFF_PHYSICAL_ATK = 1,
        // Token: 0x040087DF RID: 34783
        [Description("物防BUFF")]
        BUFF_PHYSICAL_DEF = 2,
        // Token: 0x040087E0 RID: 34784
        [Description("魔攻BUFF")]
        BUFF_MAGIC_ATK = 3,
        // Token: 0x040087E1 RID: 34785
        [Description("魔防BUFF")]
        BUFF_MAGIC_DEF = 4,
        // Token: 0x040087E2 RID: 34786
        [Description("闪避BUFF")]
        BUFF_DODGE = 5,
        // Token: 0x040087E3 RID: 34787
        [Description("暴击BUFF")]
        BUFF_CRITICAL = 6,
        // Token: 0x040087E4 RID: 34788
        [Description("TP上升BUFF")]
        BUFF_ENERGY_RECOVERY = 7,
        // Token: 0x040087E5 RID: 34789
        [Description("HP回复BUFF")]
        BUFF_HP_RECOVERY = 8,
        // Token: 0x040087E6 RID: 34790
        [Description("加速BUFF")]
        HASTE = 9,
        // Token: 0x040087E7 RID: 34791
        [Description("无敌")]
        NO_DAMAGE = 10,
        // Token: 0x040087E8 RID: 34792
        [Description("生命吸收")]
        BUFF_LIFE_STEAL = 11,
        // Token: 0x040087E9 RID: 34793
        [Description("生命吸收+1次\n妹法/松鼠")]
        BUFF_ADD_LIFE_STEAL = 12,
        // Token: 0x040087EA RID: 34794
        [Description("物攻DEBUFF")]
        DEBUFF_PHYSICAL_ATK = 13,
        // Token: 0x040087EB RID: 34795
        [Description("物防DEBUFF")]
        DEBUFF_PHYSICAL_DEF = 14,
        // Token: 0x040087EC RID: 34796
        [Description("魔攻DEBUFF")]
        DEBUFF_MAGIC_ATK = 15,
        // Token: 0x040087ED RID: 34797
        [Description("魔防DEBUFF")]
        DEBUFF_MAGIC_DEF = 16,
        // Token: 0x040087EE RID: 34798
        [Description("闪避DEBUFF")]
        DEBUFF_DODGE = 17,
        // Token: 0x040087EF RID: 34799
        [Description("暴击DEBUFF")]
        DEBUFF_CRITICAL = 18,
        // Token: 0x040087F0 RID: 34800
        [Description("TP上升DEBUFF")]
        DEBUFF_ENERGY_RECOVERY = 19,
        // Token: 0x040087F1 RID: 34801
        [Description("HP回复DEBUFF")]
        DEBUFF_HP_RECOVERY = 20,
        // Token: 0x040087F2 RID: 34802
        [Description("生命吸收DEBUFF")]
        DEBUFF_LIFE_STEAL = 21,
        // Token: 0x040087F3 RID: 34803
        [Description("减速DEBUFF")]
        SLOW = 22,
		// Token: 0x040087F4 RID: 34804
		[Description("禁用UB")]
        UB_DISABLE = 23,
        // Token: 0x040087F5 RID: 34805
        [Description("物理屏障")]
        PHYSICS_BARRIAR = 24,
        // Token: 0x040087F6 RID: 34806
        [Description("魔法屏障")]
        MAGIC_BARRIAR = 25,
        // Token: 0x040087F7 RID: 34807
        [Description("吸收物理伤害屏障")]
        PHYSICAS_DRAIN_BARRIAR = 26,
        // Token: 0x040087F8 RID: 34808
        [Description("吸收魔法伤害屏障")]
        MAGIC_DRAIN_BARRIAR = 27,
        // Token: 0x040087F9 RID: 34809
        [Description("物理/魔法伤害无效化屏障")]
        BOTH_BARRIAR = 28,
        // Token: 0x040087FA RID: 34810
        [Description("吸收物理/魔法伤害屏障")]
        BOTH_DRAIN_BARRIAR = 29,
        // Token: 0x040087FB RID: 34811
        DEBUF_BARRIAR = 30,//不明，无用
        // Token: 0x040087FC RID: 34812		
        [Description("反射伤害\n露/圣电")]
        STRIKE_BACK = 31,
        // Token: 0x040087FD RID: 34813
        [Description("麻痹")]
        PARALISYS = 32,
        // Token: 0x040087FE RID: 34814
        [Description("中毒")]
        SLIP_DAMAGE = 33,
        // Token: 0x040087FF RID: 34815
        [Description("致盲")]
        PHYSICS_DARK = 34,
        // Token: 0x04008800 RID: 34816
        [Description("沉默")]
        SILENCE = 35,
        // Token: 0x04008801 RID: 34817
        [Description("魅惑")]
        CONVERT = 36,
        // Token: 0x04008802 RID: 34818
        [Description("挑衅")]
        DECOY = 37,
        // Token: 0x04008803 RID: 34819
        [Description("烧伤")]
        BURN = 38,
        // Token: 0x04008804 RID: 34820
        [Description("诅咒")]
        CURSE = 39,
        // Token: 0x04008805 RID: 34821
        [Description("冻结")]
        FREEZE = 40,
        // Token: 0x04008806 RID: 34822
        [Description("束缚")]
        CHAINED = 41,
        // Token: 0x04008807 RID: 34823
        [Description("睡眠")]
        SLEEP = 42,
        // Token: 0x04008808 RID: 34824
        [Description("眩晕")]
        STUN = 43,
        // Token: 0x04008809 RID: 34825
        [Description("石化")]
        STONE = 44,
        // Token: 0x0400880A RID: 34826
        [Description("拘留")]
        DETAIN = 45,
        // Token: 0x0400880B RID: 34827
        [Description("持续生命回复")]
        REGENERATION = 46,
        // Token: 0x0400880C RID: 34828
        [Description("移动速度DEBUFF")]
        DEBUFF_MOVE_SPEED = 47,
        // Token: 0x0400880D RID: 34829
        [Description("物理闪避\n克总")]
        PHYSICS_DODGE = 48,
        // Token: 0x0400880E RID: 34830
        [Description("混乱")]
        CONFUSION = 49,
		// Token: 0x0400880F RID: 34831
		[Description("英灵的加护\n安")]
        HEROIC_SPIRIT_SEAL = 50,
        // Token: 0x04008810 RID: 34832
        [Description("猛毒")]
        VENOM = 51,
		// Token: 0x04008811 RID: 34833
		[Description("闪避次数\n拉姆")]
        COUNT_BLIND = 52,
        // Token: 0x04008812 RID: 34834
        [Description("回复生命无效并造成伤害")]
        INHIBIT_HEAL = 53,
        // Token: 0x04008813 RID: 34835
        [Description("恐慌")]
        FEAR = 54,
		// Token: 0x04008814 RID: 34836
		// [Description("吸魂\n(非会战)")]
        // SOUL_EAT = 56,
		// Token: 0x04008815 RID: 34837
		[Description("畏缩\n华哥")]
        CHLOE = 57,
		// Token: 0x04008816 RID: 34838
		// [Description("火焰果\n(非会战)")]
        // FIRE_NUTS,
		// Token: 0x04008817 RID: 34839
		[Description("畏惧\n(非会战)")]
        AWE = 59,
		// Token: 0x04008818 RID: 34840
		[Description("朋友\n露娜")]
        LUNA = 60,
		// Token: 0x04008819 RID: 34841
		[Description("硬币\n圣克")]
        CHRISTINA = 61,
        // Token: 0x0400881A RID: 34842
        [Description("持续回复TP")]
        TP_REGENERATION,
        // Token: 0x0400881B RID: 34843
        // [Description("外遇星\n(非会战)")]
        // CHEATING_STAR,
        // Token: 0x0400881C RID: 34844
        // [Description("驯鹿\n(非会战)")]
        // TONAKAI,
        // Token: 0x0400881D RID: 34845
        [Description("赌咒\n大眼")]
        HEX = 65,
        // Token: 0x0400881E RID: 34846
        [Description("昏迷（类似击晕）")]
        FAINT = 66,
        // Token: 0x0400881F RID: 34847
        [Description("物理爆伤BUFF")]
        BUFF_PHYSICAL_CRITICAL_DAMAGE = 67,
        // Token: 0x04008820 RID: 34848
        [Description("物理爆伤DEBUFF")]
        DEBUFF_PHYSICAL_CRITICAL_DAMAGE = 68,
        // Token: 0x04008821 RID: 34849
        [Description("魔法爆伤BUFF")]
        BUFF_MAGIC_CRITICAL_DAMAGE = 69,
        // Token: 0x04008822 RID: 34850
        [Description("魔法爆伤DEBUFF")]
        DEBUFF_MAGIC_CRITICAL_DAMAGE = 70,
        // Token: 0x04008823 RID: 34851
        COMPENSATION = 71,
        // Token: 0x04008824 RID: 34852
        [Description("骑士的加护\n公吃")]
        KNIGHT_GUARD,
        // Token: 0x04008825 RID: 34853
        [Description("减轻物理伤害")]
        CUT_ATK_DAMAGE,
        // Token: 0x04008826 RID: 34854
        [Description("减轻魔法伤害")]
        CUT_MGC_DAMAGE,
        // Token: 0x04008827 RID: 34855
        [Description("减轻所有伤害")]
        CUT_ALL_DAMAGE = 75,
        // Token: 0x04008828 RID: 34856
        [Description("切噜")]
        CHIERU = 76,
        // Token: 0x04008829 RID: 34857
        [Description("风之刃\n剑圣")]
        REI = 77,
        // Token: 0x0400882A RID: 34858
        [Description("物理对数盾")]
        LOG_ATK_BARRIER = 78,
        // Token: 0x0400882B RID: 34859
        [Description("魔法对数盾")]
        LOG_MGC_BARRIER = 79,
        // Token: 0x0400882C RID: 34860
        [Description("对数盾")]
        LOG_ALL_BARRIER = 80,
        // Token: 0x0400882D RID: 34861
        [Description("行动时间停止\n路人兔")]
        PAUSE_ACTION = 81,
        // Token: 0x0400882E RID: 34862
        [Description("命中BUFF")]
        BUFF_ACCURACY = 83,
        // Token: 0x0400882F RID: 34863
        [Description("命中DEBUFF")]
        DEBUFF_ACCURACY = 84,
        // Token: 0x04008830 RID: 34864
        // BOSS_BUFF,
        // Token: 0x04008831 RID: 34865
        [Description("UB沉默")]
        UB_SILENCE = 86,
        // Token: 0x04008832 RID: 34866
        // [Description("变身天使/n(非会战)")]
        // CUPID,
        // Token: 0x04008833 RID: 34867
        [Description("最大生命值DEBUFF")]
        DEBUFF_MAX_HP = 88,
        // Token: 0x04008834 RID: 34868
        [Description("魔法黑暗")]
        MAGIC_DARK = 89,
        // Token: 0x04008835 RID: 34869
        [Description("瓜虎")]
        MATSURI = 90,
        // Token: 0x04008836 RID: 34870
        [Description("治疗下降")]
        HEAL_DOWN = 91,
        // Token: 0x04008837 RID: 34871
        [Description("圣夜的光辉\n圣哈")]
        AKINO_CHRISTMAS = 92,
        // Token: 0x04008838 RID: 34872
        // NPC_STUN = 93,
        // Token: 0x04008839 RID: 34873
        [Description("爆伤抗性BUFF")]
        BUFF_RECEIVE_CRITICAL_DAMAGE = 94,
        // Token: 0x0400883A RID: 34874
        [Description("爆伤抗性DEBUFF")]
        DEBUFF_RECEIVE_CRITICAL_DAMAGE = 95,
        // Token: 0x0400883B RID: 34875
        [Description("降低受到的回复量")]
        DECREASE_HEAL = 96,
        // Token: 0x0400883C RID: 34876
        [Description("冰龙之印\n雪菲")]
        SHEFI = 97,
        // Token: 0x0400883D RID: 34877
        [Description("学习时间\n学优")]
        SCHOOL_FESTIVAL_YUNI = 98,
        // Token: 0x0400883E RID: 34878
        // [Description("圣华计数器")]
        // SCHOOL_FESTIVAL_CHLOE,
        // Token: 0x0400883F RID: 34879
        [Description("行动中毒/工菜")]
        POISON_BY_BEHAVIOUR = 100,
        // Token: 0x04008840 RID: 34880
        [Description("固定物理白甲")]
        ADDITIONAL_BUFF_PHYSICAL_DEF = 101,
        // Token: 0x04008841 RID: 34881
        // [Description("CRYSTALIZE\n结晶\n愤怒军团的兰法")]
        // CRYSTALIZE = 102,
        // Token: 0x04008842 RID: 34882
        [Description("伤害限制")]
        DAMAGE_LIMIT = 103,
        // Token: 0x04008843 RID: 34883
        [Description("固定魔法白甲")]
        ADDITIONAL_BUFF_MAGIC_DEF = 104,
        // Token: 0x04008844 RID: 34884
        // MAGIC_CHARACTER_OF_WISDOM,
        // Token: 0x04008845 RID: 34885
        [Description("力之魔\n绿魔")]
        MAGIC_CHARACTER_OF_POWER = 106,
        // Token: 0x04008846 RID: 34886
        [Description("龙之眼\n龙妈")]
        DETECT_WEAKNESS = 107,
        // Token: 0x04008847 RID: 34887
        [Description("百分比提升其受到的伤害")]
        DEBUFF_RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT = 108,
        // Token: 0x04008848 RID: 34888
        [Description("百分比提升其受到的物理伤害")]
        DEBUFF_RECEIVE_PHYSICAL_DAMAGE_PERCENT = 109,
        // Token: 0x04008849 RID: 34889
        [Description("百分比提升其受到的魔法伤害")]
        DEBUFF_RECEIVE_MAGIC_DAMAGE_PERCENT = 110,
        // Token: 0x0400884A RID: 34890
        [Description("超晶")]
        LABYRISTA_OVERLOAD = 111,
        // Token: 0x0400884B RID: 34891
        [Description("剑之刻印\n春流夏")]
        SWORD_SEAL = 112,
        // Token: 0x0400884C RID: 34892
        [Description("灵锚刻印\n海忍")]
        PHANTOMCORE_WEDGE = 113,
        // Token: 0x0400884D RID: 34893
        [Description("隐秘\n盗妹/盗锤")]
        SPY = 114,
        // Token: 0x0400884E RID: 34894
        [Description("幸福一刻\n野骑")]
        HAPPY_MOMENT = 115,
        // Token: 0x0400884F RID: 34895
        [Description("水刃加护\n水怜")]
        SEA_GOD_PROTECTION = 116,
        // Token: 0x04008850 RID: 34896
        [Description("蝶舞烙印\nscw")]
        BLUE_MAGIC_SEAL = 117,
        // Token: 0x04008851 RID: 34897
        [Description("软绵绵羊毛\n屁狐")]
        SHEEP = 118,
        // Token: 0x04008852 RID: 34898
        [Description("永夜加护\n伊莉亚")]
        TWILIGHT_GUARD = 119,
        // Token: 0x04008853 RID: 34899
        [Description("灵力\n忍")]
        PSYCHIC_POWER = 120,
        // Token: 0x04008854 RID: 34900
        // [Description("月之光印\n（非会战）")]
        // CELESTIAL_BODIES = 121,
        // Token: 0x04008855 RID: 34901
        [Description("霸瞳")]
        KAISER_INSIGHT_CARVED_SEAL = 122,
        // Token: 0x04008856 RID: 34902
        // [Description("点赞\n（非会战）")]
        // LIKE,
        // Token: 0x04008857 RID: 34903
        [Description("减轻TP减少效果")]
        ENERGY_DAMAGE_REDUCE = 124,
        // Token: 0x04008858 RID: 34904
        [Description("611")]
        SAGITTARIUS_CARVED_SEAL = 125,
        // Token: 0x04008859 RID: 34905
        [Description("友情魔印\n龙安")]
        ANNE_AND_GLARE_CARVED_SEAL = 126,
        // Token: 0x0400885A RID: 34906
        [Description("朦胧\n春月")]
        MITSUKI_NY_CARVED_SEAL = 127,
	    // Token: 0x0400885B RID: 34907
	    BLACK_FRAME = 128,
        // Token: 0x0400885C RID: 34908
        [Description("免疫行动不能状态")]
        UNABLE_STATE_GUARD = 129,
	    // Token: 0x0400885D RID: 34909
        [Description("情谊的证明\n513")]
	    MUIMI_ANNIVERSARY_CARVED_SEAL = 130,
        // Token: 0x0400885E RID: 34910
        [Description("PSI★蓄力\n美空")]
        MISORA_CARVED_SEAL = 131,
	    [Description("飞行")]
	    FLIGHT = 132,
	    [Description("万象印记\n法鸡")]
        DJEETA_WITCH = 133,
	    [Description("美貌\n礼雪")]
        LIXUE = 134,
        [Description("电池\n机娘")]
        JINIAN = 135,
        [Description("莱莱界雷\n(受影响的物理角色)")]
        WORLD_LIGHTNING = 137,
        [Description("狼牙咆哮\n狼")]
        WOLF = 139,
        [Description("标记\n火电")]
        HUODIAN = 140,
        [Description("射手座D5标记")]
        SHESHOUZUO = 143,
        [Description("噗吉靠垫\n圣锤")]
        SHENGCHUI = 145,//机制没完善用不了
        [Description("限制TP回复")]
        LIMIT_ENERGY_RECOVER_RATE = 150,
        [Description("额外加速")]
        EWAIJIASU = 152,
        [Description("额外减速")]
        EWAIJIANSU = 153,
        [Description("毒伤提升")]
        SLIP_DAMAGE_UP = 154,
        [Description("堕天使的加护\n可璃亚")]
        DUOTIANSHI = 141,//机制没完善用不了
        // Token: 0x0400885F RID: 34911
        EX_PASSIVE_1 = 999,
        // Token: 0x04008860 RID: 34912
        NUM = 1000,
        [Description("重叠加速\n花凛513机娘")]
	    SPEED_OVERLAP,
	    // Token: 0x04008861 RID: 34913
	    INVALID_VALUE = -1,
	}
}
