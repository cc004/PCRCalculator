// Decompiled with JetBrains decompiler
// Type: Elements.SpineDefine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Spine;

namespace Elements
{
	// Token: 0x02001E46 RID: 7750
	public enum eRoomPersonality
	{
		// Token: 0x0400DE21 RID: 56865
		DEF,
		// Token: 0x0400DE22 RID: 56866
		NML,
		// Token: 0x0400DE23 RID: 56867
		KYAPI,
		// Token: 0x0400DE24 RID: 56868
		KPT,
		// Token: 0x0400DE25 RID: 56869
		STYK,
		// Token: 0x0400DE26 RID: 56870
		KNMN,
		// Token: 0x0400DE27 RID: 56871
		JUN,
		// Token: 0x0400DE28 RID: 56872
		NEBBIA,
		// Token: 0x0400DE29 RID: 56873
		TRACK,
		// Token: 0x0400DE2A RID: 56874
		PIPPLE,
		// Token: 0x0400DE2B RID: 56875
		MAX
	}
	// Token: 0x02001E48 RID: 7752
	public enum eRoomAnimeTiming
	{
		// Token: 0x0400DE2D RID: 56877
		NONE,
		// Token: 0x0400DE2E RID: 56878
		IN,
		// Token: 0x0400DE2F RID: 56879
		MID,
		// Token: 0x0400DE30 RID: 56880
		OUT,
		// Token: 0x0400DE31 RID: 56881
		MAX
	}
	// Token: 0x02001E49 RID: 7753
	public enum eRoomAnimeDir
	{
		// Token: 0x0400DE33 RID: 56883
		NONE = -1,
		// Token: 0x0400DE34 RID: 56884
		N,
		// Token: 0x0400DE35 RID: 56885
		B,
		// Token: 0x0400DE36 RID: 56886
		L,
		// Token: 0x0400DE37 RID: 56887
		R
	}
    // Token: 0x02000EC2 RID: 3778
	public class SpineDefine
	{
		// Token: 0x060091DA RID: 37338 RVA: 0x001DFE98 File Offset: 0x004DFE98
		public static eSpineCharacterAnimeId GetCharacterAnimeId(string _idName)
		{
			eSpineCharacterAnimeId result;
			try
			{
				result = (eSpineCharacterAnimeId)Enum.Parse(typeof(eSpineCharacterAnimeId), _idName);
			}
			catch (ArgumentException)
			{
				result = eSpineCharacterAnimeId.NONE;
			}
			return result;
		}

		// Token: 0x060091DB RID: 37339 RVA: 0x001DFED4 File Offset: 0x004DFED4
		public static eResourceId GetResourceId(eSpineBinaryAnimationId spineBinaryAnimationId)
		{
			return SpineDefine.spineBinaryAnimationDic[spineBinaryAnimationId];
		}

		// Token: 0x060091DC RID: 37340 RVA: 0x001DFEE4 File Offset: 0x004DFEE4
		public static string GetSkinName(eSpineSkinId spineSkinId)
		{
			return SpineDefine.spineSkinDic[spineSkinId];
		}

		// Token: 0x060091DD RID: 37341 RVA: 0x001DFEF4 File Offset: 0x004DFEF4
		public static eSpineSkinId GetSkinId(string _spineSkin)
		{
			foreach (KeyValuePair<eSpineSkinId, string> keyValuePair in SpineDefine.spineSkinDic)
			{
				if (keyValuePair.Value == _spineSkin.ToLower())
				{
					return keyValuePair.Key;
				}
			}
			return eSpineSkinId.NORMAL;
		}

		// Token: 0x060091DE RID: 37342 RVA: 0x001DFF60 File Offset: 0x004DFF60
		public static string GetAnimeName(eSpineCharacterAnimeId animeId, int unitId = -1, int weaponId = -1, int index1 = -1, int index2 = -1, int index3 = -1)
		{
			if (UnitUtility.JudgeIsGuest(unitId))
			{
				weaponId = 0;
			}
			if (animeId == eSpineCharacterAnimeId.RUN && UnitUtility.IsEnemyUnit(unitId))
			{
				animeId = eSpineCharacterAnimeId.WALK;
			}
			string animeName = SpineConverterDefine.DataDic[animeId].AnimeName;
			switch (animeId)
			{
			case eSpineCharacterAnimeId.IDLE:
			case eSpineCharacterAnimeId.IDLE_NO_WEAPON:
			case eSpineCharacterAnimeId.IDLE_STAND_BY:
			case eSpineCharacterAnimeId.RUN_GAME_START:
			case eSpineCharacterAnimeId.WALK:
			case eSpineCharacterAnimeId.JOY_SHORT:
			case eSpineCharacterAnimeId.JOY_LONG:
			case eSpineCharacterAnimeId.COOP_STAND_BY:
			case eSpineCharacterAnimeId.STAND_BY:
			case eSpineCharacterAnimeId.RUN:
			case eSpineCharacterAnimeId.LANDING:
			case eSpineCharacterAnimeId.LOADING:
			case eSpineCharacterAnimeId.IDLE_SKIP:
			case eSpineCharacterAnimeId.DAMAGE_SKIP:
			case eSpineCharacterAnimeId.ANNIHILATION:
			case eSpineCharacterAnimeId.IDLE_MAP:
			case eSpineCharacterAnimeId.IDLE_MAP_S:
			case eSpineCharacterAnimeId.DAMEGE:
			case eSpineCharacterAnimeId.GAME_START:
			case eSpineCharacterAnimeId.IDLE_RESULT:
			case eSpineCharacterAnimeId.RACE_ENEMY:
				if (index1 == -1)
				{
					if (weaponId == 0)
					{
						return string.Format("{0:D6}_" + animeName, unitId);
					}
					return string.Format("{0:D2}_" + animeName, weaponId);
				}
				else
				{
					if (weaponId == 0)
					{
						return string.Format("{0:D6}_{1:D1}_" + animeName, unitId, index1);
					}
					return string.Format("{0:D2}_{1:D1}_" + animeName, unitId, index1);
				}
				break;
			case eSpineCharacterAnimeId.IDLE_MULTI_TARGET:
			case eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET:
				return string.Format("{0:D6}_{2}{1:D1}", unitId, index2, animeName);
			case eSpineCharacterAnimeId.JOY_RESULT:
				if (index1 == -1)
				{
					return string.Format("{0:D6}_" + animeName, unitId);
				}
				return string.Format("{0:D6}_{1:D1}_" + animeName, unitId, index1);
			case eSpineCharacterAnimeId.SMILE:
			case eSpineCharacterAnimeId.RARITYUP_POSING:
			case eSpineCharacterAnimeId.MANA_IDLE:
			case eSpineCharacterAnimeId.MANA_JUMP:
			case eSpineCharacterAnimeId.STAMINA_BOW:
			case eSpineCharacterAnimeId.FRIEND_HIGHTOUCH:
			case eSpineCharacterAnimeId.RUN_JUMP:
			case eSpineCharacterAnimeId.RUN_HIGH_JUMP:
			case eSpineCharacterAnimeId.NO_WEAPON_IDLE:
			case eSpineCharacterAnimeId.NO_WEAPON_JOY_SHORT:
			case eSpineCharacterAnimeId.NO_WEAPON_RUN:
			case eSpineCharacterAnimeId.NO_WEAPON_RUN_SUPER:
			case eSpineCharacterAnimeId.RACE_EAT_JUN:
			case eSpineCharacterAnimeId.RACE_EAT_NORMAL:
			case eSpineCharacterAnimeId.RACE_RUN_SPRING_JUMP:
			case eSpineCharacterAnimeId.RACE_BALLOON_FLYING_DOWN:
			case eSpineCharacterAnimeId.RACE_BALLOON_FLYING_LOOP:
			case eSpineCharacterAnimeId.RACE_BALLOON_FLYING_UP:
			case eSpineCharacterAnimeId.RACE_BALLOON_IN:
			case eSpineCharacterAnimeId.RACE_BALLOON_OUT:
			case eSpineCharacterAnimeId.RACE_RUN:
			case eSpineCharacterAnimeId.DEAR_IDOL:
			case eSpineCharacterAnimeId.DEAR_JUMP:
			case eSpineCharacterAnimeId.DEAR_SMILE:
				return string.Format("{0:D6}_" + animeName, (weaponId == 0) ? unitId : 0);
			case eSpineCharacterAnimeId.SLEEP:
				return string.Format("{0:D6}_{1}_{2:D1}", unitId, animeName, index2);
			case eSpineCharacterAnimeId.DUNGEON_IDLE_SKIP:
			case eSpineCharacterAnimeId.STORY_EYE_IDLE:
			case eSpineCharacterAnimeId.STORY_EYE_BLINK:
			case eSpineCharacterAnimeId.STORY_EYE_OPEN:
			case eSpineCharacterAnimeId.STORY_EYE_CLOSE:
			case eSpineCharacterAnimeId.STORY_MOUTH_IDLE:
			case eSpineCharacterAnimeId.STORY_MOUTH_TALK:
			case eSpineCharacterAnimeId.STORY_MOUTH_OPEN:
			case eSpineCharacterAnimeId.STORY_MOUTH_CLOSE:
				return animeName;
			case eSpineCharacterAnimeId.EVENT_BANNER_MOTION:
			case eSpineCharacterAnimeId.EVENT_BANNER_MOTION_CIRCLE:
			case eSpineCharacterAnimeId.FKE_IDLE:
			case eSpineCharacterAnimeId.EKD_GLD_01:
			case eSpineCharacterAnimeId.EKD_GLD_02:
			case eSpineCharacterAnimeId.EKD_GLD_03:
			case eSpineCharacterAnimeId.EKD_GLD_04:
			case eSpineCharacterAnimeId.EKD_GLD_05:
			case eSpineCharacterAnimeId.EKD_GLD_06:
			case eSpineCharacterAnimeId.EKD_IDLE_ANGER:
			case eSpineCharacterAnimeId.EKD_IDLE_JOY:
			case eSpineCharacterAnimeId.EKD_IDLE_JOY2:
			case eSpineCharacterAnimeId.EKD_IDLE_NORMAL:
			case eSpineCharacterAnimeId.EKD_IDLE_SAD:
			case eSpineCharacterAnimeId.EKD_IDLE_SURPRISE1:
			case eSpineCharacterAnimeId.EKD_IDLE_SURPRISE2:
			case eSpineCharacterAnimeId.EKD_OTHER_01:
			case eSpineCharacterAnimeId.EKD_OTHER_02:
			case eSpineCharacterAnimeId.EKD_OTHER_03:
			case eSpineCharacterAnimeId.EKD_OTHER_04:
			case eSpineCharacterAnimeId.EKD_OTHER_05:
			case eSpineCharacterAnimeId.EKD_OTHER_06:
			case eSpineCharacterAnimeId.EKD_OTHER_07:
			case eSpineCharacterAnimeId.EKD_OTHER_08:
			case eSpineCharacterAnimeId.EKD_OTHER_09:
			case eSpineCharacterAnimeId.EKD_OTHER_10:
			case eSpineCharacterAnimeId.EKD_PARTY_01:
			case eSpineCharacterAnimeId.EKD_PARTY_02:
			case eSpineCharacterAnimeId.EKD_PARTY_03:
			case eSpineCharacterAnimeId.EKD_PARTY_04:
			case eSpineCharacterAnimeId.EKD_PARTY_05:
			case eSpineCharacterAnimeId.EKD_PARTY_06:
			case eSpineCharacterAnimeId.EKD_PARTY_07:
			case eSpineCharacterAnimeId.EKD_PARTY_08:
			case eSpineCharacterAnimeId.EKD_PARTY_09:
			case eSpineCharacterAnimeId.EKD_PARTY_10:
			case eSpineCharacterAnimeId.EKD_PSN_01:
			case eSpineCharacterAnimeId.EKD_PSN_02:
			case eSpineCharacterAnimeId.EKD_PSN_03:
			case eSpineCharacterAnimeId.EKD_PSN_04:
			case eSpineCharacterAnimeId.EKD_PSN_05:
			case eSpineCharacterAnimeId.EKD_PSN_06:
			case eSpineCharacterAnimeId.EKD_PSN_07:
			case eSpineCharacterAnimeId.EKD_PSN_08:
			case eSpineCharacterAnimeId.EKD_PSN_09:
			case eSpineCharacterAnimeId.EKD_PSN_10:
			case eSpineCharacterAnimeId.EKD_RUN:
			case eSpineCharacterAnimeId.EKD_SMILE:
			case eSpineCharacterAnimeId.EKD_SURPRISE:
			case eSpineCharacterAnimeId.EKD_TALK_ANGER:
			case eSpineCharacterAnimeId.EKD_TALK_JOY:
			case eSpineCharacterAnimeId.EKD_TALK_JOY2:
			case eSpineCharacterAnimeId.EKD_TALK_NORMAL:
			case eSpineCharacterAnimeId.EKD_TALK_SAD:
			case eSpineCharacterAnimeId.EKD_TALK_SURPRISE1:
			case eSpineCharacterAnimeId.EKD_TALK_SURPRISE2:
				return animeName;
			case eSpineCharacterAnimeId.AWAKE_MAP:
			case eSpineCharacterAnimeId.AWAKE_MAP_RELEASE:
			case eSpineCharacterAnimeId.AWAKE:
			case eSpineCharacterAnimeId.ATTACK_SKIPQUEST:
			case eSpineCharacterAnimeId.AWAKE_RESULT:
				if (weaponId == 0)
				{
					return string.Format("{0:D6}_" + animeName, unitId);
				}
				return string.Format("{0:D2}_" + animeName, weaponId);
			case eSpineCharacterAnimeId.ATTACK:
				if (index3 != -1)
				{
					return string.Format("{0:D6}_{1:D1}_" + animeName, unitId, index3);
				}
				if (weaponId == 0)
				{
					return string.Format("{0:D6}_" + animeName, unitId);
				}
				return string.Format("{0:D2}_" + animeName, weaponId);
			case eSpineCharacterAnimeId.DIE:
			case eSpineCharacterAnimeId.LOSE:
			case eSpineCharacterAnimeId.DEFEAT:
			case eSpineCharacterAnimeId.DIE_LOOP:
				if (index1 != -1 && index2 != -1)
				{
					return string.Format("{0:D6}_{1:D1}_" + animeName + "_{2:D1}", unitId, index1, index2);
				}
				if (index1 != -1)
				{
					return string.Format("{0:D6}_" + animeName + "_{1:D1}", unitId, index1);
				}
				if (index2 != -1)
				{
					return string.Format("{0:D6}_{1:D1}_" + animeName, unitId, index2);
				}
				if (weaponId == 0)
				{
					return string.Format("{0:D6}_" + animeName, unitId);
				}
				return string.Format("{0:D2}_" + animeName, weaponId);
			case eSpineCharacterAnimeId.SKILL:
			case eSpineCharacterAnimeId.SPECIAL_SKILL:
			case eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION:
			case eSpineCharacterAnimeId.SKILL_EVOLUTION:
			case eSpineCharacterAnimeId.SUB_UNION_BURST:
			case eSpineCharacterAnimeId.SUMMON:
				if (index3 == -1)
				{
					string text = string.Format("{0:D6}_" + animeName + "{1:D1}", unitId, index1);
					if (index2 == -1)
					{
						return text;
					}
					return string.Format("{0}_{1:D1}", text, index2);
				}
				else
				{
					string text2 = string.Format("{0:D6}_{2:D1}_" + animeName + "{1:D1}", unitId, index1, index3);
					if (index2 == -1)
					{
						return text2;
					}
					return string.Format("{0}_{1:D1}", text2, index2);
				}
				break;
			case eSpineCharacterAnimeId.PRINCESS_SKILL:
				if (index2 == -1)
				{
					if (index3 == -1)
					{
						return string.Format("{0}_p_{1}_skill", unitId, index1);
					}
					return string.Format("{0}_{1}_p_{2}_skill", unitId, index3, index1);
				}
				else
				{
					if (index3 == -1)
					{
						return string.Format("{0}_p_{1}_skill_{2}", unitId, index1, index2);
					}
					return string.Format("{0}_{1}_p_{2}_skill_{3}", new object[]
					{
						unitId,
						index3,
						index1,
						index2
					});
				}
				break;
			case eSpineCharacterAnimeId.PRINCESS_SKILL_EVOLUTION:
				if (index2 == -1)
				{
					if (index3 == -1)
					{
						return string.Format("{0}_p_{1}_skill_evolution", unitId, index1);
					}
					return string.Format("{0}_{1}_p_{2}_skill_evolution", unitId, index3, index1);
				}
				else
				{
					if (index3 == -1)
					{
						return string.Format("{0}_p_{1}_skill_evolution_{2}", unitId, index1, index2);
					}
					return string.Format("{0}_{1}_p_{2}_skill_evolution_{3}", new object[]
					{
						unitId,
						index3,
						index1,
						index2
					});
				}
				break;
			case eSpineCharacterAnimeId.COMBINE_JOY_RESULT:
				return string.Format("{0:D6}" + animeName + "{1:D6}", unitId, index1);
			case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT:
			case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_REVERSE:
			case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_WALK:
			case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK:
			case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_IDLE:
			case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_SUMMON:
			case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_DIE:
			case eSpineCharacterAnimeId.TREASURE_EFFECT01:
			case eSpineCharacterAnimeId.TREASURE_EFFECT02:
			case eSpineCharacterAnimeId.TREASURE_EFFECT03:
			case eSpineCharacterAnimeId.TREASURE_EFFECT04:
			case eSpineCharacterAnimeId.TREASURE_EFFECT05:
			case eSpineCharacterAnimeId.TREASURE_EFFECT06:
			case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT01:
			case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT02:
			case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT03:
			case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT04:
			case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT05:
			case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT06:
				return string.Format("{0:D7}_{1}", index1, animeName);
			}
			return string.Empty;
		}

		// Token: 0x060091E0 RID: 37344 RVA: 0x001E0BA0 File Offset: 0x004E0BA0
		public static string GetRoomItemAnimeName(int _animeId, eRoomPersonality _personality, int _animeIdx, eRoomAnimeTiming _animeTiming, eRoomAnimeDir _dirType, bool _isRandom, bool _isUniqueAnime)
		{
			string animeName = SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_ITEM].AnimeName;
			string text = string.Empty;
			if (_isUniqueAnime)
			{
				text = string.Format("_{0:D2}_unique", _animeIdx);
			}
			else if (_isRandom)
			{
				text = string.Format("{0}{1:D2}", SpineDefine.roomAnimePersonalityStrings[(int)_personality], _animeIdx);
			}
			else
			{
				text = SpineDefine.roomAnimePersonalityStrings[(int)_personality];
			}
			string text2 = SpineDefine.roomAnimeTimingStrings[(int)_animeTiming];
			string text3 = SpineDefine.roomAnimeDirStrings[(int)_dirType];
			return string.Format("{0}{1:D6}{2}{3}{4}", new object[]
			{
				animeName,
				_animeId,
				text,
				text2,
				text3
			});
		}

		// Token: 0x060091E1 RID: 37345 RVA: 0x001E0C40 File Offset: 0x004E0C40
		public static string GetRoomSyncItemAnimeName(int _animeId, eRoomPersonality _personality, int _patternIndex, int _animeIdx, eRoomAnimeTiming _animeTiming, bool _isRandom)
		{
			string animeName = SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_SYNC].AnimeName;
			string text = string.Empty;
			if (_isRandom)
			{
				text = string.Format("{0}{1:D2}", SpineDefine.roomAnimePersonalityStrings[(int)_personality], _animeIdx);
			}
			else
			{
				text = SpineDefine.roomAnimePersonalityStrings[(int)_personality];
			}
			string text2 = SpineDefine.roomAnimeTimingStrings[(int)_animeTiming];
			string text3 = SpineDefine.roomAnimePatternStrings[_patternIndex];
			return string.Format("{0}{1:D6}{2}{3}{4}", new object[]
			{
				animeName,
				_animeId,
				text3,
				text,
				text2
			});
		}

		// Token: 0x060091E2 RID: 37346 RVA: 0x001E0CC8 File Offset: 0x004E0CC8
		public static string GetRoomTrackItemAnimeName(int _animeId, eRoomPersonality _personality, int _animeIdx, bool _isRandom)
		{
			string animeName = SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_TRACK].AnimeName;
			string arg = string.Empty;
			if (_isRandom)
			{
				arg = string.Format("{0}{1:D2}", SpineDefine.roomAnimePersonalityStrings[(int)_personality], _animeIdx);
			}
			else
			{
				arg = SpineDefine.roomAnimePersonalityStrings[(int)_personality];
			}
			return string.Format("{0}{1:D6}{2}", animeName, _animeId, arg);
		}

		// Token: 0x060091E3 RID: 37347 RVA: 0x001E0D28 File Offset: 0x004E0D28
		public static string GetRoomAppearItemAnimeName(int _roomItemId)
		{
			return string.Format("{0}{1:D6}{2}{3}{4}", new object[]
			{
				SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_ITEM].AnimeName,
				_roomItemId,
				"_appear",
				SpineDefine.roomAnimeTimingStrings[2],
				SpineDefine.roomAnimeDirStrings[0]
			});
		}

		// Token: 0x060091E4 RID: 37348 RVA: 0x001E0D84 File Offset: 0x004E0D84
		public static string GetRoomAppearRelationCharaAnimeName(int _roomItemId)
		{
			return string.Format("{0}{1:D6}{2}{3}{4}", new object[]
			{
				SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_ITEM].AnimeName,
				_roomItemId,
				SpineDefine.roomAnimePersonalityStrings[0],
				SpineDefine.roomAnimeTimingStrings[2],
				SpineDefine.roomAnimeDirStrings[0]
			});
		}

		// Token: 0x060091E5 RID: 37349 RVA: 0x001E0DE0 File Offset: 0x004E0DE0
		public SpineDefine()
		{
		}

		// Token: 0x060091E6 RID: 37350 RVA: 0x001E0DE8 File Offset: 0x004E0DE8
		// Note: this type is marked as 'beforefieldinit'.
		static SpineDefine()
		{
		}

		// Token: 0x04008A76 RID: 35446
		public const int SPINE_UNIT_ANIME_COMMON_BATTLE_INDEX_START = 1;

		// Token: 0x04008A77 RID: 35447
		public const int SPINE_UNIT_ANIME_COMMON_BATTLE_INDEX_END = 8;

		// Token: 0x04008A78 RID: 35448
		public const float SPINE_POSITION_EPSILON = 0.01f;

		// Token: 0x04008A79 RID: 35449
		public const int EYE_ANIMATION_PLAY_TRACK = 0;

		// Token: 0x04008A7A RID: 35450
		public const int MOUTH_ANIMATION_PLAY_TRACK = 1;

		// Token: 0x04008A7B RID: 35451
		public const int INVALID_VALUE = -1;

		// Token: 0x04008A7C RID: 35452
		private static Dictionary<eSpineBinaryAnimationId, eResourceId> spineBinaryAnimationDic = new Dictionary<eSpineBinaryAnimationId, eResourceId>
		{
			{
				eSpineBinaryAnimationId.COMMON_BATTLE,
				eResourceId.SPINE_UNIT_ANIME_COMMON_BATTLE
			},
			{
				eSpineBinaryAnimationId.COMMON_ROOM,
				eResourceId.ROOM_SPINEUNIT_ANIMATION_COMMON
			},
			{
				eSpineBinaryAnimationId.UNIQUE_ROOM,
				eResourceId.ROOM_SPINEUNIT_ANIMATION_UNIQUE
			},
			{
				eSpineBinaryAnimationId.BATTLE,
				eResourceId.SPINE_UNIT_ANIME_BATTLE
			},
			{
				eSpineBinaryAnimationId.ROOM_ITEM,
				eResourceId.ROOM_SPINEUNIT_ANIMATION_EACH
			},
			{
				eSpineBinaryAnimationId.ROOM_ITEM_UNIQUE,
				eResourceId.ROOM_SPINEUNIT_ANIMATION_EACH_UNIQUE
			},
			{
				eSpineBinaryAnimationId.ROOM_SYNC,
				eResourceId.ROOM_SYNC_ANIME
			},
			{
				eSpineBinaryAnimationId.ROOM_SYNC_UNIQUE,
				eResourceId.ROOM_SYNC_ANIME_UNIQUE
			},
			{
				eSpineBinaryAnimationId.ROOM_TRACK,
				eResourceId.ROOM_TRACK_ANIME
			},
			{
				eSpineBinaryAnimationId.ROOM_TRACK_UNIQUE,
				eResourceId.ROOM_TRACK_ANIME_UNIQUE
			},
			{
				eSpineBinaryAnimationId.EVENT,
				eResourceId.SPINE_UNIT_ANIME_EVENT
			},
			{
				eSpineBinaryAnimationId.EVENT_STORY,
				eResourceId.SPINE_UNIT_ANIME_EVENT_STORY
			},
			{
				eSpineBinaryAnimationId.POSING,
				eResourceId.SPINE_UNIT_ANIME_POSING
			},
			{
				eSpineBinaryAnimationId.SMILE,
				eResourceId.SPINE_UNIT_ANIME_SMILE
			},
			{
				eSpineBinaryAnimationId.LOADING,
				eResourceId.SPINE_UNIT_ANIME_LOADING
			},
			{
				eSpineBinaryAnimationId.DEFEAT,
				eResourceId.SPINE_UNIT_ANIME_DEFEAT
			},
			{
				eSpineBinaryAnimationId.MAP,
				eResourceId.SPINE_UNIT_ANIME_MAP
			},
			{
				eSpineBinaryAnimationId.MINIGAME,
				eResourceId.SPINE_UNIT_ANIME_MINIGAME
			},
			{
				eSpineBinaryAnimationId.RUN_JUMP,
				eResourceId.SPINE_UNIT_ANIME_RUN_JUMP
			},
			{
				eSpineBinaryAnimationId.NO_WEAPON,
				eResourceId.SPINE_UNIT_ANIME_NO_WEAPON
			},
			{
				eSpineBinaryAnimationId.RACE,
				eResourceId.SPINE_UNIT_ANIME_RACE
			},
			{
				eSpineBinaryAnimationId.COMBINE_JOY_RESULT,
				eResourceId.SPINE_UNIT_ANIME_COMBINE_JOY_RESULT
			},
			{
				eSpineBinaryAnimationId.DEAR,
				eResourceId.SPINE_UNIT_ANIME_DEAR
			},
			{
				eSpineBinaryAnimationId.EKD,
				eResourceId.SPINE_UNIT_ANIME_EKD
			},
			{
				eSpineBinaryAnimationId.NAVI,
				eResourceId.SPINE_UNIT_ANIME_NAVI
			},
			{
				eSpineBinaryAnimationId.OMP,
				eResourceId.SPINE_UNIT_ANIME_OMP
			},
			{
				eSpineBinaryAnimationId.UEK,
				eResourceId.SPINE_UNIT_ANIME_UEK
			},
			{
				eSpineBinaryAnimationId.NYX,
				eResourceId.SPINE_UNIT_ANIME_NYX
			},
			{
				eSpineBinaryAnimationId.PKB,
				eResourceId.SPINE_UNIT_ANIME_PKB
			},
			{
				eSpineBinaryAnimationId.BLB_DRAMA,
				eResourceId.SPINE_UNIT_ANIME_BIRTHDAY_LOGINBONUS_DRAMA
			}
		};

		// Token: 0x04008A7D RID: 35453
		public static readonly eSpineSkinId[] ProductQuantitySkinArray = new eSpineSkinId[]
		{
			eSpineSkinId.QUANTITY_EMPTY,
			eSpineSkinId.QUANTITY_HALF,
			eSpineSkinId.QUANTITY_MAX
		};

		// Token: 0x04008A7E RID: 35454
		private static Dictionary<eSpineSkinId, string> spineSkinDic = new Dictionary<eSpineSkinId, string>
		{
			{
				eSpineSkinId.ANGER,
				"anger"
			},
			{
				eSpineSkinId.JOY,
				"joy"
			},
			{
				eSpineSkinId.NORMAL,
				"normal"
			},
			{
				eSpineSkinId.SAD,
				"sad"
			},
			{
				eSpineSkinId.SHY,
				"shy"
			},
			{
				eSpineSkinId.SURPRISED,
				"surprised"
			},
			{
				eSpineSkinId.SPECIAL_A,
				"special_a"
			},
			{
				eSpineSkinId.SPECIAL_B,
				"special_b"
			},
			{
				eSpineSkinId.SPECIAL_C,
				"special_c"
			},
			{
				eSpineSkinId.SPECIAL_D,
				"special_d"
			},
			{
				eSpineSkinId.SPECIAL_E,
				"special_e"
			},
			{
				eSpineSkinId.DEFAULT,
				"default"
			},
			{
				eSpineSkinId.QUANTITY_MAX,
				"max"
			},
			{
				eSpineSkinId.QUANTITY_EMPTY,
				"empty"
			},
			{
				eSpineSkinId.QUANTITY_HALF,
				"half"
			}
		};

		// Token: 0x04008A7F RID: 35455
		private static readonly string[] roomAnimePersonalityStrings = new string[]
		{
			"_def",
			"_nml",
			"_kyapi",
			"_kpt",
			"_styk",
			"_knmn",
			"_jun",
			"_nebbia",
			"_track",
			"_pipple"
		};

		// Token: 0x04008A80 RID: 35456
		private static readonly string[] roomAnimeTimingStrings = new string[]
		{
			"",
			"_in",
			"_mid",
			"_out"
		};

		// Token: 0x04008A81 RID: 35457
		private static readonly string[] roomAnimeDirStrings = new string[]
		{
			"_N",
			"_B",
			"_L",
			"_R"
		};

		// Token: 0x04008A82 RID: 35458
		private static readonly string[] roomAnimePatternStrings = new string[]
		{
			"_A",
			"_B",
			"_C",
			"_D",
			"_E",
			"_F",
			"_G",
			"_H",
			"_I",
			"_J"
		};

		// Token: 0x02000EC3 RID: 3779
		public enum eSpecialSleepAnimeId
		{
			// Token: 0x04008A84 RID: 35460
			START,
			// Token: 0x04008A85 RID: 35461
			LOOP,
			// Token: 0x04008A86 RID: 35462
			RELEASE
		}
	}
}
