# action_type 全表（IAGKLCGAFII）

> 数据来源：BepInEx 插件 `PcrApiDump.FieldDump` 在进程内经 `il2cpp_field_static_get_value` 读到的枚举字面量（`enums.csv`）。
> 本游戏 `metadata` header 被加壳器抹掉，Il2CppDumper / Cpp2IL 跑不了；这些值是从运行时的 il2cpp 结构直接取得的，属运行时真值。

## 它在哪里

| 位置 | 说明 |
|---|---|
| `FEFJGFLKNLK.OKCMLNJFFDB` **+0x60** | 运行时「行动参数」对象上的 action_type（就是老汇编里的 `param_2 + 0x60`） |
| `Elements.ActionParameterOnPrefab.ActionType` **+0x14** | 预制体/master 数据侧同一个枚举 |
| `Elements.DamageData.<ActionType>` **+0x78** / `<AilmentActionType>` **+0x94** | 伤害/异常状态记录里也用 |
| `Elements.ForceHitData.<ActionType>` **+0x18** | 强制命中判定 |

## 消费点

```
Elements.UnitActionController::ExecAction    RVA 0x14BDA70   (段长 11,992 字符)
  (FEFJGFLKNLK _action, Elements.Skill _skill, Elements.BasePartsData _target,
   System.Int32 _num, System.Single _starttime, System.Boolean _execChildNoFrame,
   Elements.BasePartsData _protectedTarget, System.Boolean _ignoreProtect)
```

- 反汇编里 action_type 一律读作 `*(int*)(_action + 0x60)`，不形成 `switch`（il2cpp 用 4 字节相对跳转表，Ghidra 只标 `caseD_`）。
- 真正的分派落在几个 Cygames 自混淆的静态方法上（见 `SKILL_BATTLE_REVERSE.md` §6.2）：
  `FEFJGFLKNLK::JFPNODDNPBF`(0x37B2930) / `MNLBJHHAJHG`(0x37B2D90) / `OLLAIGFLEBN`(0x37B3470) / `MGIFJCBCKB::IFDPIEEMDKD`(0x3932570) / `ODPPNGHCIBJ`(0x3932A10)

## 全表（125 个具名成员）

| value | name | value | name | value | name |
|---:|---|---:|---|---:|---|
| 1 | `ATTACK` | 2 | `MOVE` | 3 | `KNOCK` |
| 4 | `HEAL` | 5 | `CURE` | 6 | `BARRIER` |
| 7 | `REFLEXIVE` | 8 | `CHANGE_SPEED` | 9 | `SLIP_DAMAGE` |
| 10 | `BUFF_DEBUFF` | 11 | `CHARM` | 12 | `BLIND` |
| 13 | `SILENCE` | 14 | `MODE_CHANGE` | 15 | `SUMMON` |
| 16 | `CHARGE_ENERGY` | 17 | `TRIGER` | 18 | `DAMAGE_CHARGE` |
| 19 | `CHARGE` | 20 | `DECOY` | 21 | `NO_DAMAGE` |
| 22 | `CHANGE_PATTERN` | 23 | `IF_FOR_CHILDREN` | 24 | `REVIVAL` |
| 25 | `CONTINUOUS_ATTACK` | 26 | `GIVE_VALUE_AS_ADDITIVE` | 27 | `GIVE_VALUE_AS_MULTIPLE` |
| 28 | `IF_FOR_ALL` | 29 | `SEARCH_AREA_CHANGE` | 30 | `DESTROY` |
| 31 | `CONTINUOUS_ATTACK_NEARBY` | 32 | `ENCHANT_LIFE_STEAL` | 33 | `ENCHANT_STRIKE_BACK` |
| 34 | `ACCUMULATIVE_DAMAGE` | 35 | `SEAL` | 36 | `ATTACK_FIELD` |
| 37 | `HEAL_FIELD` | 38 | `CHANGE_PARAMETER_FIELD` | 39 | `ABNORMAL_STATE_FIELD` |
| 40 | `KETSUBAN` | 41 | `UB_CHANGE_TIME` | 42 | `LOOP_TRIGGER` |
| 43 | `IF_HAS_TARGET` | 44 | `WAVE_START_IDLE` | 45 | `SKILL_EXEC_COUNT` |
| 46 | `RATIO_DAMAGE` | 47 | `UPPER_LIMIT_ATTACK` | 48 | `REGENERATION` |
| 49 | `BUFF_DEBUFF_CLEAR` | 50 | `LOOP_MOTION_BUFF_DEBUFF` | 51 | `DIVISION` |
| 52 | `CHANGE_BODY_WIDTH` | 53 | `IF_EXISTS_FIELD_FOR_ALL` | 54 | `STEALTH` |
| 55 | `MOVE_PARTS` | 56 | `COUNT_BLIND` | 57 | `COUNT_DOWN` |
| 58 | `STOP_FIELD` | 59 | `INHIBIT_HEAL` | 60 | `ATTACK_SEAL` |
| 61 | `FEAR` | 62 | `AWE` | 63 | `LOOP_MOTION_REPEAT` |
| 69 | `TOAD` | 70 | `FORCE_HP_CHANGE` | 71 | `KNGHT_GUARD` |
| 72 | `DAMAGE_CUT` | 73 | `LOG_BARRIER` | 74 | `GIVE_VALUE_AS_DIVIDE` |
| 75 | `ACTION_BY_HIT_COUNT` | 76 | `HEAL_DOWN` | 77 | `PASSIVE_SEAL` |
| 78 | `PASSIVE_DAMAGE_UP` | 79 | `DAMAGE_BY_ATTACK` | 80 | `DAMAGE_LIMIT` |
| 81 | `SPECIAL_IDLE` | 82 | `CHANGE_RESIST_ID` | 83 | `CHANGE_SPEED_OVERLAP` |
| 90 | `PASSIVE` | 91 | `PASSIVE_INTERMITTENT` | 92 | `CHANGE_ENERGY_RECOVERY_RATIO_BY_DAMAGE` |
| 93 | `IGNORE_DECOY` | 94 | `EFFECT` | 95 | `SPY` |
| 96 | `CHARGE_ENERGY_FIELD` | 97 | `CHARGE_ENERGY_BY_DAMAGE` | 98 | `ENERGY_DAMAGE_REDUCE` |
| 99 | `CHANGE_SPEED_OVERWRITE_FIELD` | 100 | `UNABLE_STATE_GUARD` | 101 | `ATTACK_SEAL_FOR_ALL_ENEMY` |
| 102 | `ACCUMULATIVE_DAMAGE_FOR_ALL_ENEMY` | 103 | `COPY_ATK_PARAM` | 104 | `EVERY_ATTACK_CRITICAL` |
| 105 | `ENVIRONMENT` | 106 | `PROTECT` | 107 | `CHANGE_CRITICAL_REFERENCE` |
| 108 | `IF_CONTAINS_UNIT_GROUP` | 109 | `LIMIT_ENERGY_RECOVER_RATE` | 110 | `SLIP_DAMAGE_UP` |
| 111 | `ENCHANT_TRIGGER` | 112 | `ADD_ABNORMAL_EFFECT_TIME` | 113 | `UNABLE_STATE_GUARD_FOR_EXTRA_EFFECT` |
| 114 | `ALL_UNIT_SEAL_DAMAGE_CUT` | 115 | `SEAL_BUFF_DEBUFF` | 116 | `TAUNT` |
| 117 | `FORCE_CHANGE_SPEED` | 118 | `GUTS` | 119 | `ABNORMAL_STATE_FORCE_HIT` |
| 121 | `AUTO_REVIVAL` | 122 | `HP_LOWER_LIMIT` | 123 | `DAMAGE_CUT_FOR_SPECIAL_NULLIFICATION` |
| 124 | `ENCHANT_PROTECT` | 125 | `ETHEREALIZATION` | 126 | `TALENT_BONUS_ENHANCE_BUFF_DEBUFF` |
| 127 | `GIVE_VALUE_AS_LABYRINTH_ADDITIVE` | 128 | `EXTEND_SLIP_DAMAGE` | 129 | `CONVERT_DAMAGE_OVER_TIME` |
| 130 | `REDUCE_DAMAGE_BY_HIGHER_DEF` | 131 | `IF_CONTAINS_CHARA_TYPE_GROUP` | 132 | `TRANSCEND_DAMAGE` |
| 133 | `ENCHANT_UB_TRIGGER_SEAL` | 134 | `FOLLOW_UP_DAMAGE` | 135 | `CHANGE_SKILL_DAMAGE_RATE` |
| 901 | `EX_START_PASSIVE` | 902 | `EX_CONDITION_PASSIVE` |  |

## 缺口

- 未出现的数值：`0`（无）、`64–68`、`84–89`、`120` —— 枚举里没有对应成员（保留/未用）。
- `901 EX_START_PASSIVE` / `902 EX_CONDITION_PASSIVE` 是 EX 技能专用的两个特殊值。

## 怎么用它算

1. 从 master 数据取行动参数（`action_type` 列，注意表名/列名被混淆成 `v1_<hash>`）。
2. 按上表把数字换成名字（例：`1 = ATTACK`、`9 = SLIP_DAMAGE`、`10 = BUFF_DEBUFF`、`47 = UPPER_LIMIT_ATTACK`）。
3. 在 `_il2cpp\out\UnitActionController.c` 的 `ExecAction` 里按 `+0x60` 的取值跟分支，落到上表对应的处理段。
4. 具体数值（伤害/回复/护盾量）在对应的 `SkillActionValue` / `ActionParameterOnPrefabDetail` 里，字段偏移见 `_il2cpp\meta\fields.csv`。

