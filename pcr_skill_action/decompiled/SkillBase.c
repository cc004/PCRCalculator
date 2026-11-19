/* ==== SkillBase::.ctor  @ 0x14b5fd0  size=1 ==== */

void SkillBase::.ctor(longlong param_1)

{
  longlong lVar1;
  undefined4 uVar2;
  undefined4 uVar3;
  undefined4 uVar4;
  undefined4 uVar5;
  undefined8 uVar6;
  
  if (cRam000000018ab6268c == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187c4fb10);
    sub_8032f6d0(0x187b5a698);
    sub_8032f6d0(0x187ba2888);
    sub_8032f6d0(0x187b598d0);
    sub_8032f6d0(0x187b6a578);
    sub_8032f6d0(0x187b413a0);
    sub_8032f6d0(0x187bae4a0);
    sub_8032f6d0(0x187c192c0);
    sub_8032f6d0(0x187b85ff0);
    sub_8032f6d0(0x187c19590);
    sub_8032f6d0(0x187c24590);
    sub_8032f6d0(0x187bd1e20);
    sub_8032f6d0(0x187c31708);
    sub_8032f6d0(0x187bf1860);
    sub_8032f6d0(0x187c44958);
    sub_8032f6d0(0x187c1e600);
    sub_8032f6d0(0x187c20d30);
    sub_8032f6d0(0x187bfde00);
    sub_8032f6d0(0x187bce130);
    sub_8032f6d0(0x187bc0d40);
    sub_8032f6d0(0x187be2298);
    sub_8032f6d0(0x187c1bed8);
    sub_8032f6d0(0x187bc32c0);
    sub_8032f6d0(0x187b9e1a0);
    sub_8032f6d0(0x187bc3478);
    cRam000000018ab6268c = '\x01';
  }
  uVar6 = sub_8032f970(uRam0000000187be2298);
  sub_849de5b0(uVar6,uRam0000000187c19590);
  *(undefined8 *)(param_1 + 0x20) = uVar6;
  sub_8032e8d0(param_1 + 0x20,uVar6);
  uVar6 = sub_8032f970(uRam0000000187bc0d40);
  sub_849de5b0(uVar6,uRam0000000187b6a578);
  *(undefined8 *)(param_1 + 0x28) = uVar6;
  sub_8032e8d0(param_1 + 0x28,uVar6);
  uVar6 = sub_8032f970(uRam0000000187bfde00);
  sub_849de5b0(uVar6,uRam0000000187b85ff0);
  *(undefined8 *)(param_1 + 0x30) = uVar6;
  sub_8032e8d0(param_1 + 0x30,uVar6);
  uVar6 = sub_8032f970(uRam0000000187c1e600);
  sub_849de5b0(uVar6,uRam0000000187c24590);
  *(undefined8 *)(param_1 + 0x38) = uVar6;
  sub_8032e8d0(param_1 + 0x38,uVar6);
  uVar6 = sub_8032f970(uRam0000000187c44958);
  sub_849de5b0(uVar6,uRam0000000187bd1e20);
  *(undefined8 *)(param_1 + 0x40) = uVar6;
  sub_8032e8d0(param_1 + 0x40,uVar6);
  uVar6 = sub_8032f970(uRam0000000187bc3478);
  ZoomEffect::.ctor(uVar6,0);
  *(undefined8 *)(param_1 + 0x48) = uVar6;
  sub_8032e8d0(param_1 + 0x48,uVar6);
  uVar6 = sub_8032f970(uRam0000000187bc32c0);
  SlowEffect::.ctor(uVar6,0);
  *(undefined8 *)(param_1 + 0x50) = uVar6;
  sub_8032e8d0(param_1 + 0x50,uVar6);
  uVar6 = sub_8032f970(uRam0000000187bce130);
  sub_849de5b0(uVar6,uRam0000000187bae4a0);
  *(undefined8 *)(param_1 + 0x60) = uVar6;
  sub_8032e8d0(param_1 + 0x60,uVar6);
  uVar6 = sub_8032f970(uRam0000000187c1bed8);
  sub_849de5b0(uVar6,uRam0000000187c192c0);
  *(undefined8 *)(param_1 + 0x68) = uVar6;
  sub_8032e8d0(param_1 + 0x68,uVar6);
  uVar6 = sub_8032f970(uRam0000000187ba2888);
  sub_85660990(uVar6,uRam0000000187c4fb10);
  *(undefined8 *)(param_1 + 0x88) = uVar6;
  sub_8032e8d0(param_1 + 0x88,uVar6);
  uVar6 = sub_8032f970(uRam0000000187c20d30);
  sub_849de5b0(uVar6,uRam0000000187c31708);
  *(undefined8 *)(param_1 + 0x98) = uVar6;
  sub_8032e8d0(param_1 + 0x98,uVar6);
  uVar6 = sub_8032f970(uRam0000000187c20d30);
  sub_849de5b0(uVar6,uRam0000000187c31708);
  *(undefined8 *)(param_1 + 0xa0) = uVar6;
  sub_8032e8d0(param_1 + 0xa0,uVar6);
  if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b41650);
  }
  lVar1 = *(longlong *)(lRam0000000187b41650 + 0xb8);
  uVar2 = *(undefined4 *)(lVar1 + 0x70);
  uVar3 = *(undefined4 *)(lVar1 + 0x74);
  uVar4 = *(undefined4 *)(lVar1 + 0x78);
  uVar5 = *(undefined4 *)(lVar1 + 0x7c);
  *(undefined4 *)(param_1 + 200) = 0x3ff33333;
  *(undefined4 *)(param_1 + 0xcc) = 0x3dcccccd;
  *(undefined4 *)(param_1 + 0xb8) = uVar2;
  *(undefined4 *)(param_1 + 0xbc) = uVar3;
  *(undefined4 *)(param_1 + 0xc0) = uVar4;
  *(undefined4 *)(param_1 + 0xc4) = uVar5;
  *(undefined1 *)(param_1 + 0xfc) = 1;
  uVar6 = sub_8032f970(uRam0000000187b598d0);
  sub_85660990(uVar6,uRam0000000187b5a698);
  *(undefined8 *)(param_1 + 0x108) = uVar6;
  sub_8032e8d0(param_1 + 0x108,uVar6);
  *(undefined1 *)(param_1 + 0x118) = 1;
  uVar6 = sub_8032f970(uRam0000000187bf1860);
  sub_849de5b0(uVar6,uRam0000000187b413a0);
  *(undefined8 *)(param_1 + 0x138) = uVar6;
  sub_8032e8d0(param_1 + 0x138,uVar6);
  *(undefined1 *)(param_1 + 0x168) = 1;
  uVar6 = sub_8032f970(uRam0000000187b9e1a0);
  SubUbVoiceTimingSet::.ctor(uVar6,0);
  *(undefined8 *)(param_1 + 0x178) = uVar6;
  sub_8032e8d0(param_1 + 0x178,uVar6);
  uVar6 = sub_8032f970(uRam0000000187b9e1a0);
  SubUbVoiceTimingSet::.ctor(uVar6,0);
  *(undefined8 *)(param_1 + 0x180) = uVar6;
  sub_8032e8d0(param_1 + 0x180,uVar6);
  uVar6 = sub_8032f970(uRam0000000187b9e1a0);
  SubUbVoiceTimingSet::.ctor(uVar6,0);
  *(undefined8 *)(param_1 + 0x188) = uVar6;
  sub_8032e8d0(param_1 + 0x188,uVar6);
  uVar6 = sub_8032f970(uRam0000000187b9e1a0);
  SubUbVoiceTimingSet::.ctor(uVar6,0);
  *(undefined8 *)(param_1 + 400) = uVar6;
  sub_8032e8d0(param_1 + 400,uVar6);
  return;
}



/* ==== SkillBase::CloneAsSkill  @ 0x14b5ce0  size=1 ==== */

undefined8 SkillBase::CloneAsSkill(undefined8 param_1)

{
  undefined8 uVar1;
  
  if (cRam000000018ab62689 == '\0') {
    sub_8032f6d0(0x187b778e8);
    cRam000000018ab62689 = '\x01';
  }
  uVar1 = sub_8032f970(uRam0000000187b778e8);
  Skill::.ctor(uVar1,param_1,0);
  return uVar1;
}



/* ==== SkillBase::ReadySkill  @ 0x14b5d40  size=1 ==== */

void SkillBase::ReadySkill(longlong param_1)

{
  longlong lVar1;
  longlong lVar2;
  code *pcVar3;
  longlong lVar4;
  undefined8 uVar5;
  int iVar6;
  int iVar7;
  
  if (cRam000000018ab6268b == '\0') {
    sub_8032f6d0(0x187c4fcc0);
    sub_8032f6d0(0x187b5a848);
    sub_8032f6d0(0x187b59590);
    sub_8032f6d0(0x187b7fff8);
    sub_8032f6d0(0x187b86358);
    sub_8032f6d0(0x187b73698);
    sub_8032f6d0(0x187b86510);
    sub_8032f6d0(0x187b73858);
    cRam000000018ab6268b = '\x01';
  }
  *(undefined8 *)(param_1 + 0x158) = 0;
  if (*(longlong *)(param_1 + 0x88) != 0) {
    sub_85531bb0(*(longlong *)(param_1 + 0x88),uRam0000000187c4fcc0);
    if (*(longlong *)(param_1 + 0x108) != 0) {
      sub_85531bb0(*(longlong *)(param_1 + 0x108),uRam0000000187b5a848);
      lVar4 = *(longlong *)(param_1 + 0x148);
      *(undefined8 *)(param_1 + 0x110) = 0;
      if (lVar4 != 0) {
        iVar7 = *(int *)(lVar4 + 0x18);
        *(int *)(lVar4 + 0x1c) = *(int *)(lVar4 + 0x1c) + 1;
        *(undefined4 *)(lVar4 + 0x18) = 0;
        if (0 < iVar7) {
          Array::Clear(*(undefined8 *)(lVar4 + 0x10),0,iVar7,0);
        }
        iVar7 = 0;
        lVar4 = *(longlong *)(param_1 + 0x30);
        while (lVar4 != 0) {
          if (*(int *)(lVar4 + 0x18) <= iVar7) {
            return;
          }
          if ((*(longlong *)(param_1 + 0x30) == 0) ||
             (lVar4 = sub_849ed2d0(*(longlong *)(param_1 + 0x30),iVar7,uRam0000000187b86510),
             lVar4 == 0)) break;
          lVar1 = *(longlong *)(lVar4 + 0xb0);
          iVar6 = 0;
          if (lVar1 == 0) break;
          for (; iVar6 < *(int *)(lVar1 + 0x18); iVar6 = iVar6 + 1) {
            lVar2 = *(longlong *)(lVar4 + 0xa8);
            uVar5 = sub_849ed2d0(lVar1,iVar6,uRam0000000187b73858);
            if (lVar2 == 0) goto code_r0x0001814b5f0d;
            sub_856756f0(lVar2,uVar5,0,uRam0000000187b59590);
          }
          iVar7 = iVar7 + 1;
          lVar4 = *(longlong *)(param_1 + 0x30);
        }
      }
    }
  }
code_r0x0001814b5f0d:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== SkillBase::SetLevel  @ 0x14b5f20  size=1 ==== */

void SkillBase::SetLevel(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  code *pcVar2;
  longlong *plVar3;
  int iVar4;
  
  if (cRam000000018ab6268a == '\0') {
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab6268a = '\x01';
  }
  lVar1 = *(longlong *)(param_1 + 0x20);
  iVar4 = 0;
  *(undefined4 *)(param_1 + 0xd8) = param_2;
  while (lVar1 != 0) {
    if (*(int *)(lVar1 + 0x18) <= iVar4) {
      return;
    }
    if ((*(longlong *)(param_1 + 0x20) == 0) ||
       (plVar3 = (longlong *)sub_849ed2d0(*(longlong *)(param_1 + 0x20),iVar4,uRam0000000187c19e18)
       , plVar3 == (longlong *)0x0)) break;
    (**(code **)(*plVar3 + 0x188))(plVar3,param_2,*(undefined8 *)(*plVar3 + 400));
    iVar4 = iVar4 + 1;
    lVar1 = *(longlong *)(param_1 + 0x20);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillBase::get_AbsorberValue  @ 0x4b4890  size=8 ==== */

undefined8 SkillBase::get_AbsorberValue(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x170);
}



/* ==== SkillBase::get_AccumulatedLifeSteal  @ 0x483ce0  size=8 ==== */

undefined8 SkillBase::get_AccumulatedLifeSteal(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x110);
}



/* ==== SkillBase::get_AccumulatedTpRecovery  @ 0x572f60  size=8 ==== */

undefined8 SkillBase::get_AccumulatedTpRecovery(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x108);
}



/* ==== SkillBase::get_ActionParameters  @ 0x437810  size=5 ==== */

undefined8 SkillBase::get_ActionParameters(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x20);
}



/* ==== SkillBase::get_AlreadyAddAttackSealForAllEnemy  @ 0x14b64f0  size=8 ==== */

undefined1 SkillBase::get_AlreadyAddAttackSealForAllEnemy(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x123);
}



/* ==== SkillBase::get_AlreadyAddAttackSelfSeal  @ 0x14b6500  size=8 ==== */

undefined1 SkillBase::get_AlreadyAddAttackSelfSeal(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x121);
}



/* ==== SkillBase::get_AlreadyExexActionByHit  @ 0x14b6510  size=8 ==== */

undefined1 SkillBase::get_AlreadyExexActionByHit(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x122);
}



/* ==== SkillBase::get_AweValue  @ 0x14b6520  size=9 ==== */

undefined8 SkillBase::get_AweValue(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x160);
}



/* ==== SkillBase::get_BonusId  @ 0x600350  size=7 ==== */

undefined4 SkillBase::get_BonusId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x198);
}



/* ==== SkillBase::get_CastTime  @ 0x8d6780  size=6 ==== */

undefined4 SkillBase::get_CastTime(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x70);
}



/* ==== SkillBase::get_CountBlind  @ 0x147ce30  size=8 ==== */

undefined1 SkillBase::get_CountBlind(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x128);
}



/* ==== SkillBase::get_CountBlindType  @ 0xf84f30  size=7 ==== */

undefined4 SkillBase::get_CountBlindType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 300);
}



/* ==== SkillBase::get_CriticalPartsList  @ 0x56fdf0  size=8 ==== */

undefined8 SkillBase::get_CriticalPartsList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x150);
}



/* ==== SkillBase::get_DamagedPartsList  @ 0x56fde0  size=8 ==== */

undefined8 SkillBase::get_DamagedPartsList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x148);
}



/* ==== SkillBase::get_DefeatByThisSkill  @ 0x6cb280  size=8 ==== */

undefined1 SkillBase::get_DefeatByThisSkill(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x120);
}



/* ==== SkillBase::get_DefeatEnemyCount  @ 0x5d8330  size=7 ==== */

undefined4 SkillBase::get_DefeatEnemyCount(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x11c);
}



/* ==== SkillBase::get_EffectBranchId  @ 0xa10ad0  size=7 ==== */

undefined4 SkillBase::get_EffectBranchId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x130);
}



/* ==== SkillBase::get_EffectObjs  @ 0x438c10  size=8 ==== */

undefined8 SkillBase::get_EffectObjs(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x98);
}



/* ==== SkillBase::get_HasAttack  @ 0x6bd050  size=8 ==== */

undefined1 SkillBase::get_HasAttack(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x140);
}



/* ==== SkillBase::get_HasParentIndexes  @ 0x44ad50  size=8 ==== */

undefined8 SkillBase::get_HasParentIndexes(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xa8);
}



/* ==== SkillBase::get_IsLifeStealEnabled  @ 0x5b31f0  size=8 ==== */

undefined1 SkillBase::get_IsLifeStealEnabled(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x168);
}



/* ==== SkillBase::get_IsModeChange  @ 0xc8b360  size=8 ==== */

undefined1 SkillBase::get_IsModeChange(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xf4);
}



/* ==== SkillBase::get_LifeSteal  @ 0x5c35a0  size=7 ==== */

undefined4 SkillBase::get_LifeSteal(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x124);
}



/* ==== SkillBase::get_LoopEffectAlreadyDone  @ 0x14b6530  size=8 ==== */

undefined1 SkillBase::get_LoopEffectAlreadyDone(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x141);
}



/* ==== SkillBase::get_LoopEffectObjs  @ 0x438c50  size=8 ==== */

undefined8 SkillBase::get_LoopEffectObjs(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xa0);
}



/* ==== SkillBase::get_OwnerReturnPosition  @ 0x14b6540  size=25 ==== */

undefined8 * SkillBase::get_OwnerReturnPosition(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0xf0);
  *param_1 = *(undefined8 *)(param_2 + 0xe8);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== SkillBase::get_ProtectParts  @ 0x438c20  size=8 ==== */

undefined8 SkillBase::get_ProtectParts(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x88);
}



/* ==== SkillBase::get_SkillNum  @ 0x5e9990  size=7 ==== */

undefined4 SkillBase::get_SkillNum(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x94);
}



/* ==== SkillBase::get_TotalDamage  @ 0x509f90  size=8 ==== */

undefined8 SkillBase::get_TotalDamage(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x158);
}



/* ==== SkillBase::get_UnionBurstCoolDownTime  @ 0x13b8870  size=6 ==== */

undefined4 SkillBase::get_UnionBurstCoolDownTime(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x74);
}



/* ==== SkillBase::get_UseAccumulatedDamage  @ 0x8b93b0  size=8 ==== */

undefined1 SkillBase::get_UseAccumulatedDamage(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x118);
}



/* ==== SkillBase::set_AbsorberValue  @ 0x538a50  size=8 ==== */

void SkillBase::set_AbsorberValue(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0x170) = param_2;
  return;
}



/* ==== SkillBase::set_AccumulatedLifeSteal  @ 0x14b6560  size=8 ==== */

void SkillBase::set_AccumulatedLifeSteal(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0x110) = param_2;
  return;
}



/* ==== SkillBase::set_AccumulatedTpRecovery  @ 0x573850  size=19 ==== */

void SkillBase::set_AccumulatedTpRecovery(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x108) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x108U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar4 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar4 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== SkillBase::set_ActionParameters  @ 0x437880  size=13 ==== */

void SkillBase::set_ActionParameters(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x20) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x20U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar4 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar4 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== SkillBase::set_AlreadyAddAttackSealForAllEnemy  @ 0x14b6570  size=7 ==== */

void SkillBase::set_AlreadyAddAttackSealForAllEnemy(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x123) = param_2;
  return;
}



/* ==== SkillBase::set_AlreadyAddAttackSelfSeal  @ 0x14b6580  size=7 ==== */

void SkillBase::set_AlreadyAddAttackSelfSeal(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x121) = param_2;
  return;
}



/* ==== SkillBase::set_AlreadyExexActionByHit  @ 0x14b6590  size=7 ==== */

void SkillBase::set_AlreadyExexActionByHit(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x122) = param_2;
  return;
}



/* ==== SkillBase::set_AweValue  @ 0x14b65a0  size=9 ==== */

void SkillBase::set_AweValue(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0x160) = param_2;
  return;
}



/* ==== SkillBase::set_BonusId  @ 0x600390  size=7 ==== */

void SkillBase::set_BonusId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x198) = param_2;
  return;
}



/* ==== SkillBase::set_CastTime  @ 0x14b1170  size=6 ==== */

void SkillBase::set_CastTime(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x70) = param_2;
  return;
}



/* ==== SkillBase::set_CountBlind  @ 0x5f5660  size=7 ==== */

void SkillBase::set_CountBlind(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x128) = param_2;
  return;
}



/* ==== SkillBase::set_CountBlindType  @ 0xf88770  size=7 ==== */

void SkillBase::set_CountBlindType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 300) = param_2;
  return;
}



/* ==== SkillBase::set_CriticalPartsList  @ 0x571b40  size=19 ==== */

void SkillBase::set_CriticalPartsList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x150) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x150U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar4 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar4 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== SkillBase::set_DamagedPartsList  @ 0x571b20  size=19 ==== */

void SkillBase::set_DamagedPartsList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x148) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x148U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar4 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar4 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== SkillBase::set_DefeatByThisSkill  @ 0x14b65b0  size=7 ==== */

void SkillBase::set_DefeatByThisSkill(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x120) = param_2;
  return;
}



/* ==== SkillBase::set_DefeatEnemyCount  @ 0x5d8db0  size=7 ==== */

void SkillBase::set_DefeatEnemyCount(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x11c) = param_2;
  return;
}



/* ==== SkillBase::set_EffectBranchId  @ 0x560b40  size=7 ==== */

void SkillBase::set_EffectBranchId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x130) = param_2;
  return;
}



/* ==== SkillBase::set_HasAttack  @ 0x116fc50  size=7 ==== */

void SkillBase::set_HasAttack(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x140) = param_2;
  return;
}



/* ==== SkillBase::set_HasParentIndexes  @ 0x44ad70  size=19 ==== */

void SkillBase::set_HasParentIndexes(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xa8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xa8U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar4 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar4 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== SkillBase::set_IsLifeStealEnabled  @ 0x5b3200  size=7 ==== */

void SkillBase::set_IsLifeStealEnabled(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x168) = param_2;
  return;
}



/* ==== SkillBase::set_IsModeChange  @ 0xc8c280  size=7 ==== */

void SkillBase::set_IsModeChange(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xf4) = param_2;
  return;
}



/* ==== SkillBase::set_LifeSteal  @ 0x5c5260  size=7 ==== */

void SkillBase::set_LifeSteal(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x124) = param_2;
  return;
}



/* ==== SkillBase::set_LoopEffectAlreadyDone  @ 0x14b65c0  size=7 ==== */

void SkillBase::set_LoopEffectAlreadyDone(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x141) = param_2;
  return;
}



/* ==== SkillBase::set_OwnerReturnPosition  @ 0x14b65d0  size=22 ==== */

void SkillBase::set_OwnerReturnPosition(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0xe8) = *param_2;
  *(undefined4 *)(param_1 + 0xf0) = uVar1;
  return;
}



/* ==== SkillBase::set_ProtectParts  @ 0x438e40  size=19 ==== */

void SkillBase::set_ProtectParts(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x88) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x88U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar4 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar4 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== SkillBase::set_SkillNum  @ 0x5e9b50  size=7 ==== */

void SkillBase::set_SkillNum(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x94) = param_2;
  return;
}



/* ==== SkillBase::set_TotalDamage  @ 0x14b65f0  size=8 ==== */

void SkillBase::set_TotalDamage(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0x158) = param_2;
  return;
}



/* ==== SkillBase::set_UnionBurstCoolDownTime  @ 0x14b6600  size=6 ==== */

void SkillBase::set_UnionBurstCoolDownTime(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x74) = param_2;
  return;
}



/* ==== SkillBase::set_UseAccumulatedDamage  @ 0x8ba2b0  size=7 ==== */

void SkillBase::set_UseAccumulatedDamage(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x118) = param_2;
  return;
}



