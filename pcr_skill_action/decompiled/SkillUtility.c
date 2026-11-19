/* ==== SkillUtility::.ctor  @ 0x4383d0  size=7 ==== */

void SkillUtility::_ctor(void)

{
  return;
}



/* ==== SkillUtility::ConvertSkillType  @ 0x15cbaa0  size=43 ==== */

undefined8 SkillUtility::ConvertSkillType(int param_1)

{
  if (param_1 == 0) {
    return 1;
  }
  if ((param_1 != 1) && (param_1 != 2)) {
    if (param_1 != 3) {
      return 0xffffffff;
    }
    return 3;
  }
  return 2;
}



/* ==== SkillUtility::CreateSkillLevelInfo  @ 0x15cbb70  size=65 ==== */

longlong SkillUtility::CreateSkillLevelInfo(int param_1,undefined4 param_2,undefined4 param_3)

{
  code *pcVar1;
  longlong lVar2;
  undefined8 uVar3;
  
  if ((param_1 != 0) && (param_1 != 1)) {
    if (param_1 == 2) {
      uVar3 = 1;
      goto SkillUtility::CreateSkillLevelInfo;
    }
    if (param_1 != 3) {
      return 0;
    }
  }
  uVar3 = 0;
SkillUtility::CreateSkillLevelInfo:
  if (cRam000000018ab62f3e == '\0') {
    sub_8032f6d0(0x187b78350,param_3,uVar3,0);
    cRam000000018ab62f3e = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187b78350);
  SkillLevelInfo::.ctor(lVar2,0);
  if (lVar2 != 0) {
    SkillLevelInfo::SetSkillId(lVar2,param_2,0);
    SkillLevelInfo::SetSkillLevel(lVar2,param_3,0);
    SkillLevelInfo::SetSlotNumber(lVar2,uVar3,0);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== SkillUtility::CreateSkillLevelInfo  @ 0x15cbad0  size=1 ==== */

longlong SkillUtility::CreateSkillLevelInfo(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab62f3e == '\0') {
    sub_8032f6d0(0x187b78350);
    cRam000000018ab62f3e = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187b78350);
  SkillLevelInfo::.ctor(lVar2,0);
  if (lVar2 != 0) {
    SkillLevelInfo::SetSkillId(lVar2,param_1,0);
    SkillLevelInfo::SetSkillLevel(lVar2,param_2,0);
    SkillLevelInfo::SetSlotNumber(lVar2,param_3,0);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== SkillUtility::CreateSpSkillData  @ 0x15cbbc0  size=1 ==== */

longlong SkillUtility::CreateSpSkillData(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4,
                      undefined8 param_5,undefined8 param_6)

{
  uint uVar1;
  int iVar2;
  code *pcVar3;
  undefined4 uVar4;
  longlong lVar5;
  longlong lVar6;
  undefined8 uVar7;
  longlong lVar8;
  longlong lVar9;
  int iVar10;
  
  if (cRam000000018ab62f3f == '\0') {
    sub_8032f6d0(0x187c25580);
    sub_8032f6d0(0x187c253c8);
    sub_8032f6d0(0x187c25730);
    sub_8032f6d0(0x187c258e8);
    sub_8032f6d0(0x187b32c68);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b7eb08);
    cRam000000018ab62f3f = '\x01';
  }
  lVar5 = sub_8032f970(uRam0000000187b32c68);
  sub_849de5b0(lVar5,uRam0000000187c253c8);
  lVar6 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
    lVar6 = sub_8032d010(lVar6);
  }
  lVar6 = *(longlong *)(*(longlong *)(lVar6 + 0xc0) + 0x10);
  if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
    lVar6 = sub_8032d010(lVar6);
  }
  if (**(longlong **)(lVar6 + 0xb8) != 0) {
    lVar6 = *(longlong *)(**(longlong **)(lVar6 + 0xb8) + 0x400);
    uVar4 = SkillUtility::searchNormalSkillId(param_1,param_4,0,0);
    if (lVar6 == 0) goto code_r0x0001815cbf73;
    uVar7 = FGOIOBFANCB::BKEJKNOJCEE(lVar6,uVar4,0);
    lVar8 = lRam0000000187c25580;
    if (lVar5 == 0) goto code_r0x0001815cbf73;
    *(int *)(lVar5 + 0x1c) = *(int *)(lVar5 + 0x1c) + 1;
    lVar9 = *(longlong *)(lVar5 + 0x10);
    uVar1 = *(uint *)(lVar5 + 0x18);
    if (lVar9 == 0) goto code_r0x0001815cbf73;
    if (uVar1 < *(uint *)(lVar9 + 0x18)) {
      *(uint *)(lVar5 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar9 + 0x18) <= uVar1) goto code_r0x0001815cbf79;
      *(undefined8 *)(lVar9 + 0x20 + (longlong)(int)uVar1 * 8) = uVar7;
      sub_8032e8d0(lVar9 + (longlong)(int)uVar1 * 8 + 0x20,uVar7);
    }
    else {
      sub_84a97470(lVar5,uVar7,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar8 + 0x20) + 0xc0) + 0x70));
    }
    uVar4 = SkillUtility::searchNormalSkillId(param_2,param_5,0,0);
    uVar7 = FGOIOBFANCB::BKEJKNOJCEE(lVar6,uVar4,0);
    lVar9 = lRam0000000187c25580;
    lVar8 = *(longlong *)(lVar5 + 0x10);
    *(int *)(lVar5 + 0x1c) = *(int *)(lVar5 + 0x1c) + 1;
    uVar1 = *(uint *)(lVar5 + 0x18);
    if (lVar8 == 0) goto code_r0x0001815cbf73;
    if (uVar1 < *(uint *)(lVar8 + 0x18)) {
      *(uint *)(lVar5 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar8 + 0x18) <= uVar1) goto code_r0x0001815cbf79;
      *(undefined8 *)(lVar8 + 0x20 + (longlong)(int)uVar1 * 8) = uVar7;
      sub_8032e8d0(lVar8 + (longlong)(int)uVar1 * 8 + 0x20,uVar7);
    }
    else {
      sub_84a97470(lVar5,uVar7,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar9 + 0x20) + 0xc0) + 0x70));
    }
    uVar4 = SkillUtility::searchNormalSkillId(param_2,param_5,1);
    uVar7 = FGOIOBFANCB::BKEJKNOJCEE(lVar6,uVar4,0);
    lVar9 = lRam0000000187c25580;
    lVar8 = *(longlong *)(lVar5 + 0x10);
    *(int *)(lVar5 + 0x1c) = *(int *)(lVar5 + 0x1c) + 1;
    uVar1 = *(uint *)(lVar5 + 0x18);
    if (lVar8 == 0) goto code_r0x0001815cbf73;
    if (uVar1 < *(uint *)(lVar8 + 0x18)) {
      *(uint *)(lVar5 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar8 + 0x18) <= uVar1) goto code_r0x0001815cbf79;
      *(undefined8 *)(lVar8 + 0x20 + (longlong)(int)uVar1 * 8) = uVar7;
      sub_8032e8d0(lVar8 + (longlong)(int)uVar1 * 8 + 0x20,uVar7);
    }
    else {
      sub_84a97470(lVar5,uVar7,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar9 + 0x20) + 0xc0) + 0x70));
    }
    uVar4 = SkillUtility::searchNormalSkillId(param_3,param_6,0,0);
    uVar7 = FGOIOBFANCB::BKEJKNOJCEE(lVar6,uVar4,0);
    lVar8 = lRam0000000187c25580;
    lVar6 = *(longlong *)(lVar5 + 0x10);
    *(int *)(lVar5 + 0x1c) = *(int *)(lVar5 + 0x1c) + 1;
    uVar1 = *(uint *)(lVar5 + 0x18);
    if (lVar6 != 0) {
      if (uVar1 < *(uint *)(lVar6 + 0x18)) {
        *(uint *)(lVar5 + 0x18) = uVar1 + 1;
        if (*(uint *)(lVar6 + 0x18) <= uVar1) {
code_r0x0001815cbf79:
          sub_8032f9c0();
          pcVar3 = (code *)swi(3);
          lVar6 = (*pcVar3)();
          return lVar6;
        }
        *(undefined8 *)(lVar6 + 0x20 + (longlong)(int)uVar1 * 8) = uVar7;
        sub_8032e8d0(lVar6 + (longlong)(int)uVar1 * 8 + 0x20,uVar7);
      }
      else {
        sub_84a97470(lVar5,uVar7,
                      *(undefined8 *)(*(longlong *)(*(longlong *)(lVar8 + 0x20) + 0xc0) + 0x70));
      }
      lVar6 = sub_8032f970(uRam0000000187b7eb08);
      SpSkillData::.ctor(lVar6,0);
      iVar10 = 0;
      do {
        if (*(int *)(lVar5 + 0x18) <= iVar10) {
          return lVar6;
        }
        lVar8 = sub_849ed2d0(lVar5,iVar10,uRam0000000187c258e8);
        if (lVar8 != 0) {
          iVar2 = *(int *)(lVar8 + 0x10);
          if (iVar2 == 1) {
            if (lVar6 == 0) break;
            lVar9 = *(longlong *)(lVar6 + 0x10);
          }
          else if (iVar2 == 2) {
            if (lVar6 == 0) break;
            lVar9 = *(longlong *)(lVar6 + 0x18);
          }
          else {
            if (iVar2 != 3) goto code_r0x0001815cbf53;
            if (lVar6 == 0) break;
            lVar9 = *(longlong *)(lVar6 + 0x20);
          }
          if (lVar9 == 0) break;
          sub_80002460(lVar9,lVar8,lRam0000000187c25580);
        }
code_r0x0001815cbf53:
        iVar10 = iVar10 + 1;
      } while( true );
    }
  }
code_r0x0001815cbf73:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  lVar6 = (*pcVar3)();
  return lVar6;
}



/* ==== SkillUtility::GetBarrierEffectText  @ 0x15cbf80  size=46 ==== */

undefined8 SkillUtility::GetBarrierEffectText(undefined4 param_1,char param_2)

{
  undefined8 uVar1;
  
  switch(param_1) {
  case 3:
  case 4:
  case 6:
    return 0x66df4a38;
  }
  uVar1 = 0x633eff9d;
  if (param_2 != '\0') {
    uVar1 = 0x52590d73;
  }
  return uVar1;
}



/* ==== SkillUtility::GetBarrierTypeText  @ 0x15cbfd0  size=35 ==== */

undefined8 SkillUtility::GetBarrierTypeText(undefined4 param_1)

{
  switch(param_1) {
  default:
    return 0x51fc0252;
  case 2:
  case 4:
    return 0x73808119;
  case 5:
  case 6:
    return 0x589e1a22;
  }
}



/* ==== SkillUtility::GetDamageAttributeText  @ 0x15cc020  size=17 ==== */

undefined8 SkillUtility::GetDamageAttributeText(int param_1)

{
  undefined8 uVar1;
  
  uVar1 = 0x51fc0252;
  if (param_1 != 1) {
    uVar1 = 0x73808119;
  }
  return uVar1;
}



/* ==== SkillUtility::GetDurationDodgeText  @ 0x15cc040  size=17 ==== */

undefined8 SkillUtility::GetDurationDodgeText(int param_1)

{
  undefined8 uVar1;
  
  uVar1 = 0x7bfc37b8;
  if (param_1 != 1) {
    uVar1 = 0x633eff9d;
  }
  return uVar1;
}



/* ==== SkillUtility::GetSkillData  @ 0x15cc060  size=1 ==== */

longlong SkillUtility::GetSkillData(undefined4 param_1,undefined4 param_2)

{
  uint uVar1;
  code *pcVar2;
  undefined4 uVar3;
  longlong lVar4;
  longlong lVar5;
  longlong lVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 uVar9;
  int iVar10;
  undefined8 uStack_38;
  undefined4 uStack_30;
  
  if (cRam000000018ab62f3b == '\0') {
    sub_8032f6d0(0x187c330f8);
    sub_8032f6d0(0x187b690a8);
    sub_8032f6d0(0x187b68ef0);
    sub_8032f6d0(0x187ba38a0);
    sub_8032f6d0(0x187ba3a58);
    sub_8032f6d0(0x187bc0820);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b77c60);
    cRam000000018ab62f3b = '\x01';
  }
  lVar4 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar4 + 0x135) & 1) == 0) {
    lVar4 = sub_8032d010(lVar4);
  }
  lVar4 = *(longlong *)(*(longlong *)(lVar4 + 0xc0) + 0x10);
  if ((*(byte *)(lVar4 + 0x135) & 1) == 0) {
    lVar4 = sub_8032d010(lVar4);
  }
  if ((**(longlong **)(lVar4 + 0xb8) != 0) &&
     (lVar4 = *(longlong *)(**(longlong **)(lVar4 + 0xb8) + 0x3d8), lVar4 != 0)) {
    lVar4 = HOEABFDCBIG::BKEJKNOJCEE(lVar4,param_1,0);
    if (lVar4 == 0) {
      return 0;
    }
    lVar5 = sub_8032f970(uRam0000000187b77c60);
    SkillData::.ctor(lVar5,0);
    if (lVar5 != 0) {
      sub_8043abe0(lVar5,param_1,0);
      lVar6 = sub_8032f970(uRam0000000187bc0820);
      sub_849de5b0(lVar6,uRam0000000187b68ef0);
      iVar10 = 0;
      while (*(longlong *)(lVar4 + 0x28) != 0) {
        if (*(int *)(*(longlong *)(lVar4 + 0x28) + 0x18) <= iVar10) {
          sub_80437900(lVar5,lVar6,0);
          return lVar5;
        }
        lVar7 = sub_8032f970(uRam0000000187c330f8);
        sub_815d3cc0(lVar7,0);
        if ((*(longlong *)(lVar4 + 0x28) == 0) ||
           (lVar8 = sub_849ed2d0(*(longlong *)(lVar4 + 0x28),iVar10,uRam0000000187ba3a58),
           lVar8 == 0)) break;
        uVar9 = *(undefined8 *)(lVar8 + 0x14);
        uVar3 = *(undefined4 *)(lVar8 + 0x1c);
        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_38 = uVar9;
        uStack_30 = uVar3;
        uVar3 = ObscuredInt::FHOALPFLEEC(&uStack_38,0);
        if (lVar7 == 0) break;
        sub_8043abe0(lVar7,uVar3,0);
        uVar9 = SkillUtility::getSkillActionValue(*(undefined4 *)(lVar7 + 0x10),param_2,0);
        sub_80437900(lVar7,uVar9,0);
        if (lVar6 == 0) break;
        *(int *)(lVar6 + 0x1c) = *(int *)(lVar6 + 0x1c) + 1;
        lVar8 = *(longlong *)(lVar6 + 0x10);
        uVar1 = *(uint *)(lVar6 + 0x18);
        if (lVar8 == 0) break;
        if (uVar1 < *(uint *)(lVar8 + 0x18)) {
          *(uint *)(lVar6 + 0x18) = uVar1 + 1;
          if (*(uint *)(lVar8 + 0x18) <= uVar1) {
            sub_8032f9c0();
            pcVar2 = (code *)swi(3);
            lVar4 = (*pcVar2)();
            return lVar4;
          }
          *(longlong *)(lVar8 + 0x20 + (longlong)(int)uVar1 * 8) = lVar7;
          sub_8032e8d0(lVar8 + (longlong)(int)uVar1 * 8 + 0x20);
          iVar10 = iVar10 + 1;
        }
        else {
          sub_84a97470(lVar6);
          iVar10 = iVar10 + 1;
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  lVar4 = (*pcVar2)();
  return lVar4;
}



/* ==== SkillUtility::GetSkillEffectValue  @ 0x15cc350  size=1 ==== */

longlong SkillUtility::GetSkillEffectValue(undefined4 param_1,int param_2,int param_3)

{
  code *pcVar1;
  double dVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  char cVar5;
  undefined2 uVar6;
  int iVar7;
  int iVar8;
  undefined4 uVar9;
  longlong lVar10;
  longlong lVar11;
  undefined8 uVar12;
  longlong lVar13;
  longlong lVar14;
  longlong lVar15;
  undefined8 uVar16;
  float fVar17;
  float fVar18;
  longlong alStack_f8 [2];
  undefined8 uStack_e8;
  undefined8 uStack_e0;
  undefined8 uStack_d8;
  undefined8 uStack_d0;
  int iStack_c8;
  longlong lStack_c0;
  longlong lStack_b8;
  float afStack_b0 [2];
  undefined4 uStack_a8;
  undefined2 uStack_a4;
  undefined8 uStack_98;
  undefined4 uStack_90;
  undefined8 uStack_88;
  undefined4 uStack_80;
  undefined8 uStack_78;
  undefined4 uStack_70;
  
  if (cRam000000018ab62f3a == '\0') {
    sub_8032f6d0(0x187bafc50);
    sub_8032f6d0(0x187b447d0);
    sub_8032f6d0(0x187bc2ae8);
    sub_8032f6d0(0x187c2b448);
    sub_8032f6d0(0x187c4b4c0);
    sub_8032f6d0(0x187c2b0e0);
    sub_8032f6d0(0x187c4afa8);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187c1f708);
    sub_8032f6d0(0x187c25b88);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60c28);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b61850);
    sub_8032f6d0(0x187b61d80);
    sub_8032f6d0(0x187b77e20);
    sub_8032f6d0(0x187c05928);
    cRam000000018ab62f3a = '\x01';
  }
  lStack_c0 = 0;
  lVar10 = sub_8032f970(uRam0000000187c25b88);
  sub_849de5b0(lVar10,uRam0000000187c4afa8);
  lVar11 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
    lVar11 = sub_8032d010(lVar11);
  }
  lVar11 = *(longlong *)(*(longlong *)(lVar11 + 0xc0) + 0x10);
  if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
    lVar11 = sub_8032d010(lVar11);
  }
  if (**(longlong **)(lVar11 + 0xb8) != 0) {
    lVar11 = *(longlong *)(**(longlong **)(lVar11 + 0xb8) + 0x3d8);
    if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iVar7 = UnitUtility::GetSkillIdConsiderOverride(param_1,param_2,0);
    iVar8 = UnitUtility::GetSkillIdConsiderOverride(param_1,param_2 + param_3,0);
    if (iVar7 == iVar8) {
      if (lVar11 != 0) {
        lVar11 = HOEABFDCBIG::CBBMJADBDGF(lVar11,iVar7,0);
        if (lVar11 == 0) {
          return lVar10;
        }
        lVar11 = *(longlong *)(lVar11 + 0x18);
        lVar13 = sub_80003d20(lRam0000000187b3c708);
        if (lVar13 != 0) {
          lVar13 = *(longlong *)(lVar13 + 0x3c8);
          iVar7 = 0;
          alStack_f8[0] = 0;
          lStack_b8 = lVar13;
          if (lVar11 != 0) {
            iStack_c8 = param_3 + param_2;
            do {
              if (*(int *)(lVar11 + 0x18) <= iVar7) {
                return lVar10;
              }
              uVar9 = sub_84a610f0(lVar11,iVar7,uRam0000000187b446b8);
              if ((lVar13 == 0) || (lVar14 = sub_82af5a30(lVar13,0), lVar14 == 0)) break;
              cVar5 = sub_855b8440(lVar14,uVar9,alStack_f8,uRam0000000187bc2ae8);
              if (cVar5 != '\0') {
                if (alStack_f8[0] == 0) break;
                uVar9 = *(undefined4 *)(alStack_f8[0] + 0x2c);
                uVar6 = *(undefined2 *)(alStack_f8[0] + 0x30);
                if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
                  sub_8032fa70();
                }
                uStack_a8 = uVar9;
                uStack_a4 = uVar6;
                uVar6 = ObscuredUShort::FHOALPFLEEC(&uStack_a8,0);
                if (*(int *)(lRam0000000187b77e20 + 0xe4) == 0) {
                  sub_8032fa70(lRam0000000187b77e20);
                }
                lStack_c0 = 0;
                if (**(longlong **)(lRam0000000187b77e20 + 0xb8) == 0) break;
                cVar5 = sub_855faac0(**(longlong **)(lRam0000000187b77e20 + 0xb8),uVar6,&lStack_c0,
                                      uRam0000000187b447d0);
                lVar13 = lStack_b8;
                if (cVar5 != '\0') {
                  if (alStack_f8[0] == 0) break;
                  lVar13 = sub_8032f970(uRam0000000187c1f708);
                  sub_849de5b0(lVar13,uRam0000000187c2b0e0);
                  if ((lVar13 == 0) || (sub_80006f00(lVar13), alStack_f8[0] == 0)) break;
                  uVar12 = *(undefined8 *)(alStack_f8[0] + 0x58);
                  uVar16 = *(undefined8 *)(alStack_f8[0] + 0x60);
                  uVar3 = *(undefined8 *)(alStack_f8[0] + 0x68);
                  uVar4 = *(undefined8 *)(alStack_f8[0] + 0x70);
                  if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
                    sub_8032fa70();
                  }
                  uStack_e8 = uVar12;
                  uStack_e0 = uVar16;
                  uStack_d8 = uVar3;
                  uStack_d0 = uVar4;
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0x78);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0x80);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0x88);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0x90);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0x98);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0xa0);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0xa8);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0xb0);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0xb8);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0xc0);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 200);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0xd0);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0xd8);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0xe0);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0xe8);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0xf0);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0xf8);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0x100);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0x108);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0x110);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0x118);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0x120);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0x128);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0x130);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
                  lVar14 = sub_8032f970(uRam0000000187c1f708);
                  sub_849de5b0(lVar14,uRam0000000187c2b0e0);
                  if ((lVar14 == 0) || (sub_80006f00(lVar14), alStack_f8[0] == 0)) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0x58);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0x60);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0x68);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0x70);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar14,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0x78);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0x80);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0x88);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0x90);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar14,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0x98);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0xa0);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0xa8);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0xb0);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar14,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0xb8);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0xc0);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 200);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0xd0);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar14,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0xd8);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0xe0);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0xe8);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0xf0);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar14,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0xf8);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0x100);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0x108);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0x110);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar14,(float)dVar2,uRam0000000187c2b448);
                  if (alStack_f8[0] == 0) break;
                  uStack_e8 = *(undefined8 *)(alStack_f8[0] + 0x118);
                  uStack_e0 = *(undefined8 *)(alStack_f8[0] + 0x120);
                  uStack_d8 = *(undefined8 *)(alStack_f8[0] + 0x128);
                  uStack_d0 = *(undefined8 *)(alStack_f8[0] + 0x130);
                  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_e8,0);
                  sub_80006f00(lVar14,(float)dVar2,uRam0000000187c2b448);
                  lVar15 = sub_8032f970(uRam0000000187c1f708);
                  sub_849de5b0(lVar15,uRam0000000187c2b0e0);
                  if (alStack_f8[0] == 0) break;
                  uVar12 = *(undefined8 *)(alStack_f8[0] + 0x34);
                  uVar9 = *(undefined4 *)(alStack_f8[0] + 0x3c);
                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                    sub_8032fa70();
                  }
                  uStack_98 = uVar12;
                  uStack_90 = uVar9;
                  ObscuredInt::FHOALPFLEEC(&uStack_98,0);
                  if (lVar15 == 0) break;
                  sub_80006f00(lVar15);
                  sub_80006f00(lVar15);
                  sub_80006f00(lVar15);
                  if (alStack_f8[0] == 0) break;
                  uStack_88 = *(undefined8 *)(alStack_f8[0] + 0x40);
                  uStack_80 = *(undefined4 *)(alStack_f8[0] + 0x48);
                  ObscuredInt::FHOALPFLEEC(&uStack_88,0);
                  sub_80006f00(lVar15);
                  if (alStack_f8[0] == 0) break;
                  uStack_78 = *(undefined8 *)(alStack_f8[0] + 0x4c);
                  uStack_70 = *(undefined4 *)(alStack_f8[0] + 0x54);
                  ObscuredInt::FHOALPFLEEC(&uStack_78,0);
                  sub_80006f00(lVar15);
                  if ((lStack_c0 == 0) ||
                     (fVar17 = (float)(**(code **)(lStack_c0 + 0x18))
                                                (*(undefined8 *)(lStack_c0 + 0x40),lVar13,lVar15,1,
                                                 *(undefined8 *)(lStack_c0 + 0x28)), lStack_c0 == 0)
                     ) break;
                  fVar18 = (float)(**(code **)(lStack_c0 + 0x18))
                                            (*(undefined8 *)(lStack_c0 + 0x40),lVar14,lVar15,1,
                                             *(undefined8 *)(lStack_c0 + 0x28));
                  lVar13 = lStack_b8;
                  if (fVar18 - fVar17 != 0.0) {
                    if (alStack_f8[0] == 0) break;
                    uVar12 = *(undefined8 *)(alStack_f8[0] + 0x188);
                    if (*(int *)(lRam0000000187b61850 + 0xe4) == 0) {
                      sub_8032fa70();
                    }
                    uVar12 = ObscuredString::FHOALPFLEEC(uVar12,0);
                    cVar5 = String::IsNullOrEmpty(uVar12,0);
                    lVar13 = lStack_b8;
                    if (cVar5 == '\0') {
                      if (alStack_f8[0] == 0) {
code_r0x0001815cce1f:
                        sub_8032f9d0();
                        pcVar1 = (code *)swi(3);
                        lVar11 = (*pcVar1)();
                        return lVar11;
                      }
                      uVar12 = *(undefined8 *)(alStack_f8[0] + 0x188);
                      if (*(int *)(lRam0000000187b61850 + 0xe4) == 0) {
                        sub_8032fa70();
                      }
                      uVar12 = ObscuredString::FHOALPFLEEC(uVar12,0);
                      afStack_b0[0] = fVar18 - fVar17;
                      uVar16 = sub_8032e8a0(uRam000000018ab8e188,afStack_b0);
                      uVar12 = String::Format(uVar12,uVar16,0);
                      if (lVar10 == 0) goto code_r0x0001815cce1f;
                      sub_80002460(lVar10,uVar12,uRam0000000187c4b4c0);
                      lVar13 = lStack_b8;
                    }
                  }
                }
              }
              iVar7 = iVar7 + 1;
            } while( true );
          }
        }
      }
    }
    else {
      lVar11 = sub_8032f970(uRam0000000187c25b88);
      sub_849de5b0(lVar11,uRam0000000187c4afa8);
      if (*(int *)(lRam0000000187bafc50 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar12 = ConstText::Name(0x5de1f60c,0);
      if (lVar11 != 0) {
        sub_80002460(lVar11,uVar12,uRam0000000187c4b4c0);
        return lVar11;
      }
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar11 = (*pcVar1)();
  return lVar11;
}



/* ==== SkillUtility::GetSkillId  @ 0x15cce30  size=1 ==== */

undefined8 SkillUtility::GetSkillId(int param_1,longlong param_2)

{
  code *pcVar1;
  undefined8 uVar2;
  undefined4 uVar3;
  undefined8 uStack_18;
  undefined4 uStack_10;
  
  if (cRam000000018ab62f3d == '\0') {
    sub_8032f6d0(0x187b60fa0);
    cRam000000018ab62f3d = '\x01';
  }
  if (param_1 == 0) {
    if (param_2 == 0) goto code_r0x0001815ccf0d;
    uVar2 = *(undefined8 *)(param_2 + 0x74);
    uVar3 = *(undefined4 *)(param_2 + 0x7c);
  }
  else if (param_1 == 1) {
    if (param_2 == 0) goto code_r0x0001815ccf0d;
    uVar2 = *(undefined8 *)(param_2 + 0x80);
    uVar3 = *(undefined4 *)(param_2 + 0x88);
  }
  else if (param_1 == 2) {
    if (param_2 == 0) goto code_r0x0001815ccf0d;
    uVar2 = *(undefined8 *)(param_2 + 0x8c);
    uVar3 = *(undefined4 *)(param_2 + 0x94);
  }
  else {
    if (param_1 != 3) {
      return 0;
    }
    if (param_2 == 0) {
code_r0x0001815ccf0d:
      sub_8032f9d0();
      pcVar1 = (code *)swi(3);
      uVar2 = (*pcVar1)();
      return uVar2;
    }
    uVar2 = *(undefined8 *)(param_2 + 0xf8);
    uVar3 = *(undefined4 *)(param_2 + 0x100);
  }
  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uStack_18 = uVar2;
  uStack_10 = uVar3;
  uVar2 = ObscuredInt::FHOALPFLEEC(&uStack_18,0);
  return uVar2;
}



/* ==== SkillUtility::GetSkillTextIconSpriteName  @ 0x15ccf20  size=1 ==== */

longlong SkillUtility::GetSkillTextIconSpriteName(int param_1)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  longlong alStackX_18 [2];
  undefined8 uStack_28;
  undefined8 uStack_20;
  undefined4 uStack_18;
  
  if (cRam000000018ab62f36 == '\0') {
    sub_8032f6d0(0x187bafc50);
    sub_8032f6d0(0x187b54818);
    sub_8032f6d0(0x187b549d0);
    sub_8032f6d0(0x187b54b80);
    cRam000000018ab62f36 = '\x01';
  }
  if (param_1 == 0) {
    return lRam0000000187b54b80;
  }
  if (param_1 - 1U < 2) {
    return lRam0000000187b549d0;
  }
  if (2 < param_1) {
    return lRam0000000187b54818;
  }
  if (*(int *)(lRam0000000187bafc50 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (cRam000000018ab641d0 == '\0') {
    sub_8032f6d0(0x187bafc50,0);
    sub_8032f6d0(0x187b9e6d0);
    sub_8032f6d0(0x187c0d748);
    sub_8032f6d0(0x187bb5bb8);
    sub_8032f6d0(0x187bef820);
    sub_8032f6d0(0x187b32510);
    cRam000000018ab641d0 = '\x01';
  }
  alStackX_18[0] = 0;
  lVar3 = *(longlong *)(lRam0000000187c0d748 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = **(longlong **)(lVar3 + 0xb8);
  if (*(int *)(lRam0000000187bafc50 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187bafc50);
  }
  if (**(longlong **)(lRam0000000187bafc50 + 0xb8) != 0) {
    cVar2 = sub_855faac0(**(longlong **)(lRam0000000187bafc50 + 0xb8),0x63afd0e3,alStackX_18,
                          uRam0000000187b9e6d0);
    if (cVar2 == '\0') {
      if (lVar3 == 0) {
        uStack_28 = uRam0000000187b32510;
        uStack_20 = 0xffffffffffffffff;
        uStack_18 = 0x63afd0e3;
        lVar3 = Enum::ToString(&uStack_28,0);
        alStackX_18[0] = lRam0000000187bef820;
        if (lVar3 != 0) {
          alStackX_18[0] = lVar3;
        }
      }
      else {
        lVar3 = sub_8170e8f0(lVar3);
        if (lVar3 == 0) goto code_r0x00018180f693;
        cVar2 = sub_855faac0(lVar3,0x63afd0e3,alStackX_18,uRam0000000187b9e6d0);
        if (cVar2 == '\0') {
          return lRam0000000187bef820;
        }
      }
    }
    lVar3 = alStackX_18[0];
    if (*(int *)(lRam0000000187bb5bb8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar3 = TextUtil::ReplaceNewLine(lVar3,0);
    return lVar3;
  }
code_r0x00018180f693:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar3 = (*pcVar1)();
  return lVar3;
}



/* ==== SkillUtility::GetSkillTextIconSpriteName  @ 0x15ccfd0  size=1 ==== */

undefined8 SkillUtility::GetSkillTextIconSpriteName(int param_1)

{
  if (cRam000000018ab62f37 == '\0') {
    sub_8032f6d0(0x187b54818);
    sub_8032f6d0(0x187b549d0);
    sub_8032f6d0(0x187b54b80);
    cRam000000018ab62f37 = '\x01';
  }
  if (param_1 == 1) {
    return uRam0000000187b54b80;
  }
  if (param_1 == 2) {
    return uRam0000000187b549d0;
  }
  if (param_1 != 3) {
    return 0;
  }
  return uRam0000000187b54818;
}



/* ==== SkillUtility::GetSkillTextIconSpriteName  @ 0x15cd050  size=1 ==== */

longlong SkillUtility::GetSkillTextIconSpriteName(int param_1)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  longlong alStackX_18 [2];
  undefined8 uStack_28;
  undefined8 uStack_20;
  undefined4 uStack_18;
  
  if (cRam000000018ab62f38 == '\0') {
    sub_8032f6d0(0x187bafc50);
    sub_8032f6d0(0x187b54818);
    sub_8032f6d0(0x187b549d0);
    sub_8032f6d0(0x187b54b80);
    cRam000000018ab62f38 = '\x01';
  }
  if (param_1 == 0x65) {
    return lRam0000000187b54b80;
  }
  if (param_1 - 0xc9U < 2) {
    return lRam0000000187b549d0;
  }
  if (param_1 - 0x12dU < 3) {
    return lRam0000000187b54818;
  }
  if (*(int *)(lRam0000000187bafc50 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (cRam000000018ab641d0 == '\0') {
    sub_8032f6d0(0x187bafc50,0);
    sub_8032f6d0(0x187b9e6d0);
    sub_8032f6d0(0x187c0d748);
    sub_8032f6d0(0x187bb5bb8);
    sub_8032f6d0(0x187bef820);
    sub_8032f6d0(0x187b32510);
    cRam000000018ab641d0 = '\x01';
  }
  alStackX_18[0] = 0;
  lVar3 = *(longlong *)(lRam0000000187c0d748 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = **(longlong **)(lVar3 + 0xb8);
  if (*(int *)(lRam0000000187bafc50 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187bafc50);
  }
  if (**(longlong **)(lRam0000000187bafc50 + 0xb8) != 0) {
    cVar2 = sub_855faac0(**(longlong **)(lRam0000000187bafc50 + 0xb8),0x63afd0e3,alStackX_18,
                          uRam0000000187b9e6d0);
    if (cVar2 == '\0') {
      if (lVar3 == 0) {
        uStack_28 = uRam0000000187b32510;
        uStack_20 = 0xffffffffffffffff;
        uStack_18 = 0x63afd0e3;
        lVar3 = Enum::ToString(&uStack_28,0);
        alStackX_18[0] = lRam0000000187bef820;
        if (lVar3 != 0) {
          alStackX_18[0] = lVar3;
        }
      }
      else {
        lVar3 = sub_8170e8f0(lVar3);
        if (lVar3 == 0) goto code_r0x00018180f693;
        cVar2 = sub_855faac0(lVar3,0x63afd0e3,alStackX_18,uRam0000000187b9e6d0);
        if (cVar2 == '\0') {
          return lRam0000000187bef820;
        }
      }
    }
    lVar3 = alStackX_18[0];
    if (*(int *)(lRam0000000187bb5bb8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar3 = TextUtil::ReplaceNewLine(lVar3,0);
    return lVar3;
  }
code_r0x00018180f693:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar3 = (*pcVar1)();
  return lVar3;
}



/* ==== SkillUtility::GetSkillTypeLabel  @ 0x15cd100  size=84 ==== */

undefined8 SkillUtility::GetSkillTypeLabel(int param_1)

{
  undefined8 uVar1;
  
  if (param_1 < 0x66) {
    if (param_1 != -1) {
      uVar1 = 0x63afd0e3;
      if (param_1 == 0x65) {
        uVar1 = 0x7d595cf9;
      }
      return uVar1;
    }
  }
  else if (1 < param_1 - 0xc9U) {
    if (param_1 - 0x12dU < 3) {
      return 0x5729f528;
    }
    if (param_1 - 0x191U < 3) {
      return 0x578d62e9;
    }
    return 0x63afd0e3;
  }
  return 0x42f0bc25;
}



/* ==== SkillUtility::GetStatusTypeBuff  @ 0x15cd160  size=52 ==== */

undefined8 SkillUtility::GetStatusTypeBuff(int param_1)

{
  switch(param_1 / 10) {
  case 1:
    return 0x50062ade;
  case 2:
    return 0x611be150;
  case 3:
    return 0x766fdd84;
  case 4:
    return 0x557408d9;
  case 5:
    return 0x776217dc;
  case 6:
    return 0x46ad68d0;
  case 7:
    return 0x7fa5cc43;
  case 8:
    return 0x5203732a;
  case 9:
    return 0x71f239b2;
  default:
    return 0x63afd0e3;
  }
}



/* ==== SkillUtility::GetStatusTypePassive  @ 0x15cd1f0  size=35 ==== */

undefined8 SkillUtility::GetStatusTypePassive(undefined4 param_1)

{
  switch(param_1) {
  case 1:
    return 0x5f6c002b;
  case 2:
    return 0x50062ade;
  case 3:
    return 0x611be150;
  case 4:
    return 0x766fdd84;
  case 5:
    return 0x557408d9;
  case 6:
    return 0x46ad68d0;
  case 7:
    return 0x7fa5cc43;
  case 8:
    return 0x776217dc;
  case 9:
    return 0x71f239b2;
  case 10:
    return 0x78a131fb;
  case 0xb:
    return 0x4588342b;
  default:
    return 0x63afd0e3;
  case 0xe:
    return 0x5203732a;
  case 0xf:
    return 0x7e302e9d;
  case 0x10:
    return 0x5c42701c;
  case 0x11:
    return 0x6248c77c;
  }
}



/* ==== SkillUtility::GetTargetBuffType  @ 0x15cd2c0  size=17 ==== */

undefined8 SkillUtility::GetTargetBuffType(int param_1)

{
  undefined8 uVar1;
  
  uVar1 = 0x61023b8c;
  if (param_1 != 3) {
    uVar1 = 0x45d3f5dd;
  }
  return uVar1;
}



/* ==== SkillUtility::GetTargetPassiveType  @ 0x15cd2e0  size=27 ==== */

undefined8 SkillUtility::GetTargetPassiveType(int param_1)

{
  undefined8 uVar1;
  
  if (param_1 != 0) {
    uVar1 = 0x4d8cc309;
    if (param_1 != 1) {
      uVar1 = 0x6ccf8845;
    }
    return uVar1;
  }
  return 0x7526b2cd;
}



/* ==== SkillUtility::GetUpDownText  @ 0x15cd300  size=40 ==== */

undefined8 SkillUtility::GetUpDownText(float param_1,char param_2)

{
  undefined8 uVar1;
  
  if (param_1 <= 0.0) {
    uVar1 = 0x62541513;
    if (param_2 != '\0') {
      uVar1 = 0x724b1b28;
    }
    return uVar1;
  }
  uVar1 = 0x50f94df0;
  if (param_2 != '\0') {
    uVar1 = 0x65750846;
  }
  return uVar1;
}



/* ==== SkillUtility::GetValueFormat  @ 0x15cd330  size=17 ==== */

undefined8 SkillUtility::GetValueFormat(int param_1)

{
  undefined8 uVar1;
  
  uVar1 = 0x4e5e5856;
  if (param_1 != 1) {
    uVar1 = 0x6cfa0b57;
  }
  return uVar1;
}



/* ==== SkillUtility::SearchSkillLevelInfo  @ 0x15cd350  size=1 ==== */

undefined8 SkillUtility::SearchSkillLevelInfo(int param_1,longlong param_2)

{
  code *pcVar1;
  undefined4 uVar2;
  int iVar3;
  longlong lVar4;
  undefined8 uVar5;
  int iVar6;
  int iVar7;
  int iVar8;
  undefined8 uStack_28;
  undefined4 uStack_20;
  
  if (cRam000000018ab62f41 == '\0') {
    sub_8032f6d0(0x187c33918);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187c33ac8);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    cRam000000018ab62f41 = '\x01';
  }
  lVar4 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar4 + 0x135) & 1) == 0) {
    lVar4 = sub_8032d010(lVar4);
  }
  lVar4 = *(longlong *)(*(longlong *)(lVar4 + 0xc0) + 0x10);
  if ((*(byte *)(lVar4 + 0x135) & 1) == 0) {
    lVar4 = sub_8032d010(lVar4);
  }
  if ((**(longlong **)(lVar4 + 0xb8) != 0) &&
     (lVar4 = *(longlong *)(**(longlong **)(lVar4 + 0xb8) + 0x510), param_2 != 0)) {
    uVar5 = *(undefined8 *)(param_2 + 0x10);
    uVar2 = *(undefined4 *)(param_2 + 0x18);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_28 = uVar5;
    uStack_20 = uVar2;
    uVar2 = ObscuredInt::FHOALPFLEEC(&uStack_28,0);
    if (lVar4 != 0) {
      lVar4 = EPFCIGDAPHC::CBBMJADBDGF(lVar4,uVar2,0);
      iVar6 = 0;
      iVar8 = 0;
      iVar7 = 0;
      while (*(longlong *)(param_2 + 0x68) != 0) {
        if (*(int *)(*(longlong *)(param_2 + 0x68) + 0x18) <= iVar7) goto code_r0x0001815cd4f0;
        if ((lVar4 == 0) || (*(longlong *)(lVar4 + 0x10) == 0)) break;
        iVar3 = sub_84a610f0(*(longlong *)(lVar4 + 0x10),iVar7,uRam0000000187b446b8);
        if (iVar3 == param_1) {
          lVar4 = *(longlong *)(param_2 + 0x68);
          iVar8 = iVar7;
          goto joined_r0x0001815cd56b;
        }
        iVar7 = iVar7 + 1;
      }
    }
  }
code_r0x0001815cd57b:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar5 = (*pcVar1)();
  return uVar5;
code_r0x0001815cd4f0:
  if (*(longlong *)(param_2 + 0x70) == 0) goto code_r0x0001815cd57b;
  if (*(int *)(*(longlong *)(param_2 + 0x70) + 0x18) <= iVar8) goto code_r0x0001815cd52f;
  if ((lVar4 == 0) || (*(longlong *)(lVar4 + 0x28) == 0)) goto code_r0x0001815cd57b;
  iVar7 = sub_84a610f0(*(longlong *)(lVar4 + 0x28),iVar8,uRam0000000187b446b8);
  if (iVar7 == param_1) {
    lVar4 = *(longlong *)(param_2 + 0x70);
    goto joined_r0x0001815cd56b;
  }
  iVar8 = iVar8 + 1;
  goto code_r0x0001815cd4f0;
code_r0x0001815cd52f:
  if (*(longlong *)(param_2 + 0x78) == 0) goto code_r0x0001815cd57b;
  if (*(int *)(*(longlong *)(param_2 + 0x78) + 0x18) <= iVar6) {
    return 0;
  }
  if ((lVar4 == 0) || (*(longlong *)(lVar4 + 0x18) == 0)) goto code_r0x0001815cd57b;
  iVar8 = sub_84a610f0(*(longlong *)(lVar4 + 0x18),iVar6,uRam0000000187b446b8);
  if (iVar8 == param_1) {
    lVar4 = *(longlong *)(param_2 + 0x78);
    iVar8 = iVar6;
joined_r0x0001815cd56b:
    if (lVar4 != 0) {
      uVar5 = sub_849ed2d0(lVar4,iVar8,uRam0000000187c33ac8);
      return uVar5;
    }
    goto code_r0x0001815cd57b;
  }
  iVar6 = iVar6 + 1;
  goto code_r0x0001815cd52f;
}



/* ==== SkillUtility::SetSubExplain  @ 0x15cd590  size=1 ==== */

longlong SkillUtility::SetSubExplain(undefined4 param_1)

{
  code *pcVar1;
  double dVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  char cVar6;
  undefined2 uVar7;
  undefined4 uVar8;
  longlong lVar9;
  longlong lVar10;
  longlong lVar11;
  longlong lVar12;
  longlong lVar13;
  undefined8 uVar14;
  longlong *plVar15;
  int iVar16;
  char in_stack_00000028;
  longlong alStack_e8 [2];
  undefined8 uStack_d8;
  undefined8 uStack_d0;
  undefined8 uStack_c8;
  undefined8 uStack_c0;
  undefined4 auStack_b8 [4];
  undefined4 uStack_a8;
  undefined2 uStack_a4;
  longlong lStack_98;
  longlong lStack_90;
  undefined8 uStack_88;
  undefined4 uStack_80;
  undefined8 uStack_78;
  undefined4 uStack_70;
  undefined8 uStack_68;
  undefined4 uStack_60;
  
  if (cRam000000018ab62f39 == '\0') {
    sub_8032f6d0(0x187b447d0);
    sub_8032f6d0(0x187bc2ae8);
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187c2b448);
    sub_8032f6d0(0x187c4b4c0);
    sub_8032f6d0(0x187c2b0e0);
    sub_8032f6d0(0x187c4afa8);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187c1f708);
    sub_8032f6d0(0x187c25b88);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187bf7ae8);
    sub_8032f6d0(0x187b60c28);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b61850);
    sub_8032f6d0(0x187b61d80);
    sub_8032f6d0(0x187b77e20);
    cRam000000018ab62f39 = '\x01';
  }
  iVar16 = 0;
  lStack_98 = 0;
  lVar9 = sub_8032f970(uRam0000000187c25b88);
  sub_849de5b0(lVar9,uRam0000000187c4afa8);
  lVar10 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  lVar10 = *(longlong *)(*(longlong *)(lVar10 + 0xc0) + 0x10);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  lVar10 = **(longlong **)(lVar10 + 0xb8);
  if ((lVar10 != 0) && (*(longlong *)(lVar10 + 0x3d8) != 0)) {
    lVar11 = HOEABFDCBIG::CBBMJADBDGF(*(longlong *)(lVar10 + 0x3d8),param_1,0);
    if (lVar11 != 0) {
      lVar11 = *(longlong *)(lVar11 + 0x18);
      lVar13 = *(longlong *)(lVar10 + 0x3c8);
      alStack_e8[0] = 0;
      lVar10 = *(longlong *)(lVar10 + 0x1cb0);
      lStack_90 = lVar13;
      if (lVar11 == 0) goto code_r0x0001815cdd8d;
      for (; iVar16 < *(int *)(lVar11 + 0x18); iVar16 = iVar16 + 1) {
        uVar8 = sub_84a610f0(lVar11,iVar16,uRam0000000187b446b8);
        if ((lVar13 == 0) || (lVar12 = sub_82af5a30(lVar13,0), lVar12 == 0))
        goto code_r0x0001815cdd8d;
        cVar6 = sub_855b8440(lVar12,uVar8,alStack_e8,uRam0000000187bc2ae8);
        if (cVar6 != '\0') {
          if (in_stack_00000028 != '\0') {
            if (lVar10 == 0) goto code_r0x0001815cdd8d;
            lVar13 = IKGBNKHMJIG::BKEJKNOJCEE(lVar10,uVar8,0);
            if (lVar13 != 0) {
              alStack_e8[0] = GAMCMFJBHME::FHOALPFLEEC(lVar13,0);
            }
          }
          if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
          uVar8 = *(undefined4 *)(alStack_e8[0] + 0x2c);
          uVar7 = *(undefined2 *)(alStack_e8[0] + 0x30);
          if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uStack_a8 = uVar8;
          uStack_a4 = uVar7;
          uVar7 = ObscuredUShort::FHOALPFLEEC(&uStack_a8,0);
          if (*(int *)(lRam0000000187b77e20 + 0xe4) == 0) {
            sub_8032fa70(lRam0000000187b77e20);
          }
          lStack_98 = 0;
          if (**(longlong **)(lRam0000000187b77e20 + 0xb8) == 0) goto code_r0x0001815cdd8d;
          cVar6 = sub_855faac0(**(longlong **)(lRam0000000187b77e20 + 0xb8),uVar7,&lStack_98,
                                uRam0000000187b447d0);
          lVar13 = lStack_90;
          if (cVar6 != '\0') {
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            lVar13 = sub_8032f970(uRam0000000187c1f708);
            sub_849de5b0(lVar13,uRam0000000187c2b0e0);
            if ((lVar13 == 0) || (sub_80006f00(lVar13), alStack_e8[0] == 0))
            goto code_r0x0001815cdd8d;
            uVar14 = *(undefined8 *)(alStack_e8[0] + 0x58);
            uVar3 = *(undefined8 *)(alStack_e8[0] + 0x60);
            uVar4 = *(undefined8 *)(alStack_e8[0] + 0x68);
            uVar5 = *(undefined8 *)(alStack_e8[0] + 0x70);
            if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
              sub_8032fa70();
            }
            uStack_d8 = uVar14;
            uStack_d0 = uVar3;
            uStack_c8 = uVar4;
            uStack_c0 = uVar5;
            dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d8,0);
            sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_d8 = *(undefined8 *)(alStack_e8[0] + 0x78);
            uStack_d0 = *(undefined8 *)(alStack_e8[0] + 0x80);
            uStack_c8 = *(undefined8 *)(alStack_e8[0] + 0x88);
            uStack_c0 = *(undefined8 *)(alStack_e8[0] + 0x90);
            dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d8,0);
            sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_d8 = *(undefined8 *)(alStack_e8[0] + 0x98);
            uStack_d0 = *(undefined8 *)(alStack_e8[0] + 0xa0);
            uStack_c8 = *(undefined8 *)(alStack_e8[0] + 0xa8);
            uStack_c0 = *(undefined8 *)(alStack_e8[0] + 0xb0);
            dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d8,0);
            sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_d8 = *(undefined8 *)(alStack_e8[0] + 0xb8);
            uStack_d0 = *(undefined8 *)(alStack_e8[0] + 0xc0);
            uStack_c8 = *(undefined8 *)(alStack_e8[0] + 200);
            uStack_c0 = *(undefined8 *)(alStack_e8[0] + 0xd0);
            dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d8,0);
            sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_d8 = *(undefined8 *)(alStack_e8[0] + 0xd8);
            uStack_d0 = *(undefined8 *)(alStack_e8[0] + 0xe0);
            uStack_c8 = *(undefined8 *)(alStack_e8[0] + 0xe8);
            uStack_c0 = *(undefined8 *)(alStack_e8[0] + 0xf0);
            dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d8,0);
            sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_d8 = *(undefined8 *)(alStack_e8[0] + 0xf8);
            uStack_d0 = *(undefined8 *)(alStack_e8[0] + 0x100);
            uStack_c8 = *(undefined8 *)(alStack_e8[0] + 0x108);
            uStack_c0 = *(undefined8 *)(alStack_e8[0] + 0x110);
            dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d8,0);
            sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_d8 = *(undefined8 *)(alStack_e8[0] + 0x118);
            uStack_d0 = *(undefined8 *)(alStack_e8[0] + 0x120);
            uStack_c8 = *(undefined8 *)(alStack_e8[0] + 0x128);
            uStack_c0 = *(undefined8 *)(alStack_e8[0] + 0x130);
            dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d8,0);
            sub_80006f00(lVar13,(float)dVar2,uRam0000000187c2b448);
            lVar12 = sub_8032f970(uRam0000000187c1f708);
            sub_849de5b0(lVar12,uRam0000000187c2b0e0);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uVar14 = *(undefined8 *)(alStack_e8[0] + 0x34);
            uVar8 = *(undefined4 *)(alStack_e8[0] + 0x3c);
            if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
              sub_8032fa70();
            }
            uStack_88 = uVar14;
            uStack_80 = uVar8;
            ObscuredInt::FHOALPFLEEC(&uStack_88,0);
            if (lVar12 == 0) goto code_r0x0001815cdd8d;
            sub_80006f00(lVar12);
            sub_80006f00(lVar12);
            sub_80006f00(lVar12);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_78 = *(undefined8 *)(alStack_e8[0] + 0x40);
            uStack_70 = *(undefined4 *)(alStack_e8[0] + 0x48);
            ObscuredInt::FHOALPFLEEC(&uStack_78,0);
            sub_80006f00(lVar12);
            if (alStack_e8[0] == 0) goto code_r0x0001815cdd8d;
            uStack_68 = *(undefined8 *)(alStack_e8[0] + 0x4c);
            uStack_60 = *(undefined4 *)(alStack_e8[0] + 0x54);
            ObscuredInt::FHOALPFLEEC(&uStack_68,0);
            sub_80006f00(lVar12);
            if ((lStack_98 == 0) ||
               (uVar8 = (**(code **)(lStack_98 + 0x18))
                                  (*(undefined8 *)(lStack_98 + 0x40),lVar13,lVar12,0,
                                   *(undefined8 *)(lStack_98 + 0x28)), alStack_e8[0] == 0))
            goto code_r0x0001815cdd8d;
            uVar14 = *(undefined8 *)(alStack_e8[0] + 0x180);
            if (*(int *)(lRam0000000187b61850 + 0xe4) == 0) {
              sub_8032fa70();
            }
            uVar14 = ObscuredString::FHOALPFLEEC(uVar14,0);
            if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar6 = ExtensionMethods::IsNullOrEmpty(uVar14,0);
            lVar13 = lStack_90;
            if (cVar6 == '\0') {
              plVar15 = (longlong *)sub_8032e960(uRam0000000187bf7ae8,1);
              auStack_b8[0] = uVar8;
              lVar13 = sub_8032e8a0(uRam000000018ab8e188,auStack_b8);
              if (plVar15 == (longlong *)0x0) {
code_r0x0001815cdda9:
                sub_8032f9d0();
                pcVar1 = (code *)swi(3);
                lVar10 = (*pcVar1)();
                return lVar10;
              }
              if ((lVar13 != 0) &&
                 (lVar12 = sub_8032e8f0(lVar13,*(undefined8 *)(*plVar15 + 0x40)), lVar12 == 0
                 )) {
                uVar14 = sub_8032f220();
                sub_8032f990(uVar14,0);
                pcVar1 = (code *)swi(3);
                lVar10 = (*pcVar1)();
                return lVar10;
              }
              if ((int)plVar15[3] == 0) {
                sub_8032f9c0();
                pcVar1 = (code *)swi(3);
                lVar10 = (*pcVar1)();
                return lVar10;
              }
              plVar15[4] = lVar13;
              sub_8032e8d0(plVar15 + 4,lVar13);
              if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uVar14 = sub_83d99c70(uVar14,plVar15,0);
              if (lVar9 == 0) goto code_r0x0001815cdda9;
              sub_80002460(lVar9,uVar14,uRam0000000187c4b4c0);
              lVar13 = lStack_90;
            }
          }
        }
      }
    }
    return lVar9;
  }
code_r0x0001815cdd8d:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar10 = (*pcVar1)();
  return lVar10;
}



/* ==== SkillUtility::getSkillActionValue  @ 0x15cddb0  size=1 ==== */

longlong SkillUtility::getSkillActionValue(undefined4 param_1,int param_2)

{
  code *pcVar1;
  double dVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  undefined2 uVar7;
  longlong lVar8;
  longlong lVar9;
  double dVar10;
  undefined4 uStack_78;
  undefined2 uStack_74;
  undefined8 uStack_68;
  undefined8 uStack_60;
  undefined8 uStack_58;
  undefined8 uStack_50;
  
  if (cRam000000018ab62f3c == '\0') {
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60c28);
    sub_8032f6d0(0x187b61d80);
    sub_8032f6d0(0x187b77aa8);
    cRam000000018ab62f3c = '\x01';
  }
  lVar8 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = sub_8032d010(lVar8);
  }
  lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = sub_8032d010(lVar8);
  }
  if ((**(longlong **)(lVar8 + 0xb8) == 0) ||
     (lVar8 = *(longlong *)(**(longlong **)(lVar8 + 0xb8) + 0x3c8), lVar8 == 0)) {
code_r0x0001815ce6f3:
    sub_8032f9d0();
    pcVar1 = (code *)swi(3);
    lVar8 = (*pcVar1)();
    return lVar8;
  }
  lVar8 = PFPEHIDOELN::BKEJKNOJCEE(lVar8,param_1,0);
  if (lVar8 == 0) {
    return 0;
  }
  lVar9 = sub_8032f970(uRam0000000187b77aa8);
  SkillActionValue::.ctor(lVar9,0);
  uStack_68 = *(undefined8 *)(lVar8 + 0x58);
  uStack_60 = *(undefined8 *)(lVar8 + 0x60);
  uStack_58 = *(undefined8 *)(lVar8 + 0x68);
  uStack_50 = *(undefined8 *)(lVar8 + 0x70);
  if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
    sub_8032fa70();
  }
  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
  if (lVar9 == 0) goto code_r0x0001815ce6f3;
  sub_804df340(lVar9,(float)dVar2,0);
  uStack_68 = *(undefined8 *)(lVar8 + 0x78);
  uStack_60 = *(undefined8 *)(lVar8 + 0x80);
  uStack_58 = *(undefined8 *)(lVar8 + 0x88);
  uStack_50 = *(undefined8 *)(lVar8 + 0x90);
  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
  sub_807c2ca0(lVar9,(float)dVar2,0);
  uStack_68 = *(undefined8 *)(lVar8 + 0x98);
  uStack_60 = *(undefined8 *)(lVar8 + 0xa0);
  uStack_58 = *(undefined8 *)(lVar8 + 0xa8);
  uStack_50 = *(undefined8 *)(lVar8 + 0xb0);
  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
  sub_804df310(lVar9,(float)dVar2,0);
  uStack_68 = *(undefined8 *)(lVar8 + 0xb8);
  uStack_60 = *(undefined8 *)(lVar8 + 0xc0);
  uStack_58 = *(undefined8 *)(lVar8 + 200);
  uStack_50 = *(undefined8 *)(lVar8 + 0xd0);
  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
  sub_804df350(lVar9,(float)dVar2,0);
  uStack_68 = *(undefined8 *)(lVar8 + 0xd8);
  uStack_60 = *(undefined8 *)(lVar8 + 0xe0);
  uStack_58 = *(undefined8 *)(lVar8 + 0xe8);
  uStack_50 = *(undefined8 *)(lVar8 + 0xf0);
  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
  sub_807c3660(lVar9,(float)dVar2,0);
  uStack_68 = *(undefined8 *)(lVar8 + 0xf8);
  uStack_60 = *(undefined8 *)(lVar8 + 0x100);
  uStack_58 = *(undefined8 *)(lVar8 + 0x108);
  uStack_50 = *(undefined8 *)(lVar8 + 0x110);
  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
  sub_80aec570(lVar9,(float)dVar2,0);
  uStack_68 = *(undefined8 *)(lVar8 + 0x118);
  uStack_60 = *(undefined8 *)(lVar8 + 0x120);
  uStack_58 = *(undefined8 *)(lVar8 + 0x128);
  uStack_50 = *(undefined8 *)(lVar8 + 0x130);
  dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
  sub_807c3110(lVar9,(float)dVar2,0);
  uStack_78 = *(undefined4 *)(lVar8 + 0x2c);
  uStack_74 = *(undefined2 *)(lVar8 + 0x30);
  if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uVar7 = ObscuredUShort::FHOALPFLEEC(&uStack_78,0);
  switch(uVar7) {
  case 1:
  case 8:
  case 9:
  case 0xb:
  case 0xc:
  case 0xd:
  case 0x19:
  case 0x20:
  case 0x26:
  case 0x27:
  case 0x28:
  case 0x2f:
  case 0x48:
  case 0x49:
  case 0x4f:
  case 0x50:
  case 0x53:
  case 0x60:
  case 99:
  case 100:
  case 0x71:
  case 0x75:
    uVar3 = *(undefined8 *)(lVar8 + 0x58);
    uVar4 = *(undefined8 *)(lVar8 + 0x60);
    uVar5 = *(undefined8 *)(lVar8 + 0x68);
    uVar6 = *(undefined8 *)(lVar8 + 0x70);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x78);
    uStack_60 = *(undefined8 *)(lVar8 + 0x80);
    uStack_58 = *(undefined8 *)(lVar8 + 0x88);
    uStack_50 = *(undefined8 *)(lVar8 + 0x90);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_804df340(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x98);
    uStack_60 = *(undefined8 *)(lVar8 + 0xa0);
    uStack_58 = *(undefined8 *)(lVar8 + 0xa8);
    uStack_50 = *(undefined8 *)(lVar8 + 0xb0);
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0xb8);
    uStack_60 = *(undefined8 *)(lVar8 + 0xc0);
    uStack_58 = *(undefined8 *)(lVar8 + 200);
    uStack_50 = *(undefined8 *)(lVar8 + 0xd0);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_804df310(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    break;
  case 4:
  case 10:
  case 0x18:
  case 0x1d:
  case 0x1f:
  case 0x22:
  case 0x32:
  case 0x66:
    uVar3 = *(undefined8 *)(lVar8 + 0x78);
    uVar4 = *(undefined8 *)(lVar8 + 0x80);
    uVar5 = *(undefined8 *)(lVar8 + 0x88);
    uVar6 = *(undefined8 *)(lVar8 + 0x90);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x98);
    uStack_60 = *(undefined8 *)(lVar8 + 0xa0);
    uStack_58 = *(undefined8 *)(lVar8 + 0xa8);
    uStack_50 = *(undefined8 *)(lVar8 + 0xb0);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_807c2ca0(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    goto code_r0x0001815ce65e;
  case 5:
  case 6:
  case 0xe:
  case 0x10:
  case 0x11:
  case 0x12:
  case 0x13:
  case 0x14:
  case 0x15:
  case 0x21:
  case 0x2e:
  case 0x31:
  case 0x61:
  case 0x68:
    uVar3 = *(undefined8 *)(lVar8 + 0x58);
    uVar4 = *(undefined8 *)(lVar8 + 0x60);
    uVar5 = *(undefined8 *)(lVar8 + 0x68);
    uVar6 = *(undefined8 *)(lVar8 + 0x70);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x78);
    uStack_60 = *(undefined8 *)(lVar8 + 0x80);
    uStack_58 = *(undefined8 *)(lVar8 + 0x88);
    uStack_50 = *(undefined8 *)(lVar8 + 0x90);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_804df340(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    break;
  case 0xf:
  case 0x1a:
  case 0x5a:
  case 0x5b:
  case 0x62:
  case 0x73:
  case 0x79:
    uVar3 = *(undefined8 *)(lVar8 + 0x78);
    uVar4 = *(undefined8 *)(lVar8 + 0x80);
    uVar5 = *(undefined8 *)(lVar8 + 0x88);
    uVar6 = *(undefined8 *)(lVar8 + 0x90);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x98);
    uStack_60 = *(undefined8 *)(lVar8 + 0xa0);
    uStack_58 = *(undefined8 *)(lVar8 + 0xa8);
    uStack_50 = *(undefined8 *)(lVar8 + 0xb0);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    goto code_r0x0001815ce5b8;
  case 0x1b:
  case 0x4a:
    uVar3 = *(undefined8 *)(lVar8 + 0xb8);
    uVar4 = *(undefined8 *)(lVar8 + 0xc0);
    uVar5 = *(undefined8 *)(lVar8 + 200);
    uVar6 = *(undefined8 *)(lVar8 + 0xd0);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (dVar2 == 1.0) {
      uVar3 = *(undefined8 *)(lVar8 + 0x78);
      uVar4 = *(undefined8 *)(lVar8 + 0x80);
      uVar5 = *(undefined8 *)(lVar8 + 0x88);
      uVar6 = *(undefined8 *)(lVar8 + 0x90);
      if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_68 = uVar3;
      uStack_60 = uVar4;
      uStack_58 = uVar5;
      uStack_50 = uVar6;
      dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
      uStack_68 = *(undefined8 *)(lVar8 + 0x98);
      uStack_60 = *(undefined8 *)(lVar8 + 0xa0);
      uStack_58 = *(undefined8 *)(lVar8 + 0xa8);
      uStack_50 = *(undefined8 *)(lVar8 + 0xb0);
      dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
      sub_807c2ca0(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
      return lVar9;
    }
    uVar3 = *(undefined8 *)(lVar8 + 0xb8);
    uVar4 = *(undefined8 *)(lVar8 + 0xc0);
    uVar5 = *(undefined8 *)(lVar8 + 200);
    uVar6 = *(undefined8 *)(lVar8 + 0xd0);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (dVar2 != 0.0) {
      return lVar9;
    }
    uVar3 = *(undefined8 *)(lVar8 + 0x78);
    uVar4 = *(undefined8 *)(lVar8 + 0x80);
    uVar5 = *(undefined8 *)(lVar8 + 0x88);
    uVar6 = *(undefined8 *)(lVar8 + 0x90);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x98);
    uStack_60 = *(undefined8 *)(lVar8 + 0xa0);
    uStack_58 = *(undefined8 *)(lVar8 + 0xa8);
    uStack_50 = *(undefined8 *)(lVar8 + 0xb0);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    dVar2 = dVar2 + 1.0;
code_r0x0001815ce5b8:
    sub_807c2ca0(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    break;
  case 0x24:
  case 0x25:
  case 0x30:
    uVar3 = *(undefined8 *)(lVar8 + 0x58);
    uVar4 = *(undefined8 *)(lVar8 + 0x60);
    uVar5 = *(undefined8 *)(lVar8 + 0x68);
    uVar6 = *(undefined8 *)(lVar8 + 0x70);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x78);
    uStack_60 = *(undefined8 *)(lVar8 + 0x80);
    uStack_58 = *(undefined8 *)(lVar8 + 0x88);
    uStack_50 = *(undefined8 *)(lVar8 + 0x90);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_804df340(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x98);
    uStack_60 = *(undefined8 *)(lVar8 + 0xa0);
    uStack_58 = *(undefined8 *)(lVar8 + 0xa8);
    uStack_50 = *(undefined8 *)(lVar8 + 0xb0);
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0xb8);
    uStack_60 = *(undefined8 *)(lVar8 + 0xc0);
    uStack_58 = *(undefined8 *)(lVar8 + 200);
    uStack_50 = *(undefined8 *)(lVar8 + 0xd0);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_804df310(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0xd8);
    uStack_60 = *(undefined8 *)(lVar8 + 0xe0);
    uStack_58 = *(undefined8 *)(lVar8 + 0xe8);
    uStack_50 = *(undefined8 *)(lVar8 + 0xf0);
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0xf8);
    uStack_60 = *(undefined8 *)(lVar8 + 0x100);
    uStack_58 = *(undefined8 *)(lVar8 + 0x108);
    uStack_50 = *(undefined8 *)(lVar8 + 0x110);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_807c3660(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
    break;
  case 0x84:
    uVar3 = *(undefined8 *)(lVar8 + 0x58);
    uVar4 = *(undefined8 *)(lVar8 + 0x60);
    uVar5 = *(undefined8 *)(lVar8 + 0x68);
    uVar6 = *(undefined8 *)(lVar8 + 0x70);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar5;
    uStack_50 = uVar6;
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0x78);
    uStack_60 = *(undefined8 *)(lVar8 + 0x80);
    uStack_58 = *(undefined8 *)(lVar8 + 0x88);
    uStack_50 = *(undefined8 *)(lVar8 + 0x90);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_804df340(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
code_r0x0001815ce65e:
    uStack_68 = *(undefined8 *)(lVar8 + 0xb8);
    uStack_60 = *(undefined8 *)(lVar8 + 0xc0);
    uStack_58 = *(undefined8 *)(lVar8 + 200);
    uStack_50 = *(undefined8 *)(lVar8 + 0xd0);
    dVar10 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    uStack_68 = *(undefined8 *)(lVar8 + 0xd8);
    uStack_60 = *(undefined8 *)(lVar8 + 0xe0);
    uStack_58 = *(undefined8 *)(lVar8 + 0xe8);
    uStack_50 = *(undefined8 *)(lVar8 + 0xf0);
    dVar2 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    sub_804df350(lVar9,(float)dVar2 * (float)param_2 + (float)dVar10,0);
  }
  return lVar9;
}



/* ==== SkillUtility::searchNormalSkillId  @ 0x15ce7b0  size=1 ==== */

ulonglong SkillUtility::searchNormalSkillId(longlong param_1,longlong param_2,uint param_3)

{
  undefined8 uVar1;
  undefined4 uVar2;
  code *pcVar3;
  int iVar4;
  int iVar5;
  longlong lVar6;
  ulonglong uVar7;
  undefined8 uStack_28;
  undefined4 uStack_20;
  
  if (cRam000000018ab62f40 == '\0') {
    sub_8032f6d0(0x187c33918);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187c33ac8);
    sub_8032f6d0(0x187b60fa0);
    cRam000000018ab62f40 = '\x01';
  }
  if (param_2 != 0) {
    if ((int)param_3 < *(int *)(param_2 + 0x18)) {
      lVar6 = sub_849ed2d0(param_2,param_3,uRam0000000187c33ac8);
      if (lVar6 != 0) {
        if (param_1 == 0) goto code_r0x0001815ce909;
        iVar4 = sub_84a610f0(param_1,param_3,uRam0000000187b446b8);
        uStack_28 = *(undefined8 *)(lVar6 + 0x10);
        uStack_20 = *(undefined4 *)(lVar6 + 0x18);
        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
          sub_8032fa70();
        }
        iVar5 = ObscuredInt::FHOALPFLEEC(&uStack_28,0);
        if (iVar4 != iVar5) {
          uVar1 = *(undefined8 *)(lVar6 + 0x10);
          uVar2 = *(undefined4 *)(lVar6 + 0x18);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uStack_28 = uVar1;
          uStack_20 = uVar2;
          uVar7 = ObscuredInt::FHOALPFLEEC(&uStack_28,0);
          return uVar7;
        }
      }
    }
    if (param_1 != 0) {
      if (*(uint *)(param_1 + 0x18) <= param_3) {
        ThrowHelper::ThrowArgumentOutOfRange_IndexException(0,param_3,uRam0000000187b446b8);
      }
      lVar6 = *(longlong *)(param_1 + 0x10);
      if (lVar6 != 0) {
        if (param_3 < *(uint *)(lVar6 + 0x18)) {
          return (ulonglong)*(uint *)(lVar6 + 0x20 + (longlong)(int)param_3 * 4);
        }
        sub_8032f9c0();
        pcVar3 = (code *)swi(3);
        uVar7 = (*pcVar3)();
        return uVar7;
      }
      sub_8032f9d0();
      pcVar3 = (code *)swi(3);
      uVar7 = (*pcVar3)();
      return uVar7;
    }
  }
code_r0x0001815ce909:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar7 = (*pcVar3)();
  return uVar7;
}



