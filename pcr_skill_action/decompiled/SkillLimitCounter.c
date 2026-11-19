/* ==== SkillLimitCounter::.ctor  @ 0x1e464c0  size=1 ==== */

void SkillLimitCounter::_ctor(longlong param_1)

{
  sub_80443e50(param_1,0);
  *(undefined8 *)(param_1 + 0x10) = 0;
  return;
}



/* ==== SkillLimitCounter::.ctor  @ 0x52db10  size=1 ==== */

void SkillLimitCounter::_ctor(longlong param_1,undefined4 param_2,undefined4 param_3)

{
  sub_80443e50(param_1,0);
  *(undefined4 *)(param_1 + 0x10) = param_2;
  *(undefined4 *)(param_1 + 0x14) = param_3;
  return;
}



/* ==== SkillLimitCounter::.ctor  @ 0x2691090  size=1 ==== */

void SkillLimitCounter::.ctor(longlong param_1,longlong param_2)

{
  code *pcVar1;
  int iVar2;
  undefined4 uVar3;
  longlong lVar4;
  bool bVar5;
  
  sub_80443e50(param_1,0);
  bVar5 = cRam000000018ab6c7d3 == '\0';
  *(undefined8 *)(param_1 + 0x10) = 0;
  if (bVar5) {
    sub_8032f6d0(0x187b65160);
    sub_8032f6d0(0x187c23968);
    cRam000000018ab6c7d3 = '\x01';
  }
  if (param_2 != 0) {
    iVar2 = sub_83a5a960(param_2,0);
    if (iVar2 == 0) {
      return;
    }
    lVar4 = JsonData::get_Item(param_2,uRam0000000187c23968,0);
    if (lVar4 != 0) {
      uVar3 = JsonData::ToInt(lVar4,0);
      *(undefined4 *)(param_1 + 0x10) = uVar3;
      lVar4 = JsonData::get_Item(param_2,uRam0000000187b65160,0);
      if (lVar4 != 0) {
        uVar3 = JsonData::ToInt(lVar4,0);
        *(undefined4 *)(param_1 + 0x14) = uVar3;
        return;
      }
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillLimitCounter::ParseSkillLimitCounter  @ 0x2690fe0  size=1 ==== */

void SkillLimitCounter::ParseSkillLimitCounter(longlong param_1,longlong param_2)

{
  code *pcVar1;
  int iVar2;
  undefined4 uVar3;
  longlong lVar4;
  
  if (cRam000000018ab6c7d3 == '\0') {
    sub_8032f6d0(0x187b65160);
    sub_8032f6d0(0x187c23968);
    cRam000000018ab6c7d3 = '\x01';
  }
  if (param_2 != 0) {
    iVar2 = sub_83a5a960(param_2,0);
    if (iVar2 == 0) {
      return;
    }
    lVar4 = JsonData::get_Item(param_2,uRam0000000187c23968,0);
    if (lVar4 != 0) {
      uVar3 = JsonData::ToInt(lVar4,0);
      *(undefined4 *)(param_1 + 0x10) = uVar3;
      lVar4 = JsonData::get_Item(param_2,uRam0000000187b65160,0);
      if (lVar4 != 0) {
        uVar3 = JsonData::ToInt(lVar4,0);
        *(undefined4 *)(param_1 + 0x14) = uVar3;
        return;
      }
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillLimitCounter::SetCounter  @ 0x465610  size=4 ==== */

void SkillLimitCounter::SetCounter(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x14) = param_2;
  return;
}



/* ==== SkillLimitCounter::SetSkillId  @ 0x43abe0  size=4 ==== */

void SkillLimitCounter::SetSkillId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x10) = param_2;
  return;
}



/* ==== SkillLimitCounter::get_counter  @ 0x465520  size=4 ==== */

undefined4 SkillLimitCounter::get_counter(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x14);
}



/* ==== SkillLimitCounter::get_skill_id  @ 0x437320  size=4 ==== */

undefined4 SkillLimitCounter::get_skill_id(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x10);
}



/* ==== SkillLimitCounter::initializeSkillLimitCounter  @ 0x1e464e0  size=7 ==== */

void SkillLimitCounter::initializeSkillLimitCounter(longlong param_1)

{
  *(undefined8 *)(param_1 + 0x10) = 0;
  return;
}



/* ==== SkillLimitCounter::set_counter  @ 0x465610  size=4 ==== */

void SkillLimitCounter::set_counter(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x14) = param_2;
  return;
}



/* ==== SkillLimitCounter::set_skill_id  @ 0x43abe0  size=4 ==== */

void SkillLimitCounter::set_skill_id(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x10) = param_2;
  return;
}



