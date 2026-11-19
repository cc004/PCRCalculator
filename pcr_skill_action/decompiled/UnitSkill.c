/* ==== UnitSkill::.ctor  @ 0x14a7150  size=1 ==== */

void UnitSkill::.ctor(longlong param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4)

{
  undefined8 uVar1;
  longlong lVar2;
  undefined8 unaff_RBX;
  
  if (cRam000000018ab6257c == '\0') {
    sub_8032f6d0(0x187b67f50);
    sub_8032f6d0(0x187c329c0);
    sub_8032f6d0(0x187bc0670);
    sub_8032f6d0(0x187c20ee0);
    sub_8032f6d0(0x187b78a40);
    cRam000000018ab6257c = '\x01';
  }
  *(undefined4 *)(param_1 + 0x34) = 0x65;
  uVar1 = sub_8032f970(uRam0000000187c20ee0);
  sub_849de5b0(uVar1,uRam0000000187c329c0);
  *(undefined8 *)(param_1 + 0x58) = uVar1;
  sub_8032e8d0(param_1 + 0x58,uVar1);
  uVar1 = sub_8032f970(uRam0000000187c20ee0);
  sub_849de5b0(uVar1,uRam0000000187c329c0);
  *(undefined8 *)(param_1 + 0x60) = uVar1;
  sub_8032e8d0(param_1 + 0x60,uVar1);
  lVar2 = sub_8032f970(uRam0000000187b78a40);
  if (cRam000000018ab6259e == '\0') {
    sub_8032f6d0(0x187b94a20);
    sub_8032f6d0(0x187b67ba8);
    cRam000000018ab6259e = '\x01';
  }
  uVar1 = sub_8032f970(uRam0000000187b67ba8);
  sub_855de2e0(uVar1,uRam0000000187b94a20);
  *(undefined8 *)(lVar2 + 0x10) = uVar1;
  sub_8032e8d0(lVar2 + 0x10,uVar1);
  sub_80443e50(lVar2,0);
  *(longlong *)(param_1 + 0x68) = lVar2;
  sub_8032e8d0(param_1 + 0x68,lVar2);
  uVar1 = sub_8032f970(uRam0000000187bc0670);
  sub_849de5b0(uVar1,uRam0000000187b67f50);
  *(undefined8 *)(param_1 + 0x78) = uVar1;
  sub_8032e8d0(param_1 + 0x78,uVar1);
  if (cRam000000018ab8a5bc == '\0') {
    sub_8032f6d0(0x187b5e978,0,param_3,param_4,unaff_RBX);
    cRam000000018ab8a5bc = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  return;
}



/* ==== UnitSkill::<ConnectSkillLevelUpApi>b__47_0  @ 0x14a6c10  size=1 ==== */

void UnitSkill::<ConnectSkillLevelUpApi>b__47_0(longlong param_1)

{
  code *pcVar1;
  int iVar2;
  longlong lVar3;
  
  if (cRam000000018ab6257e == '\0') {
    sub_8032f6d0(0x187b40b28);
    cRam000000018ab6257e = '\x01';
  }
  lVar3 = *(longlong *)(lRam0000000187b40b28 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  if (**(longlong **)(lVar3 + 0xb8) != 0) {
    if (*(int *)(**(longlong **)(lVar3 + 0xb8) + 0x128) != 7) {
      return;
    }
    lVar3 = *(longlong *)(param_1 + 0x68);
    *(undefined1 *)(param_1 + 0x70) = 0;
    if (lVar3 != 0) {
      if (cRam000000018ab6259d == '\0') {
        sub_8032f6d0(0x187b94d88);
        cRam000000018ab6259d = '\x01';
      }
      lVar3 = *(longlong *)(lVar3 + 0x10);
      if (lVar3 != 0) {
        sub_85531bb0(lVar3,uRam0000000187b94d88);
        UnitSkill::CreateAllSkillLevelInfo(param_1,0);
        lVar3 = *(longlong *)(lRam0000000187b40b28 + 0x20);
        if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
          lVar3 = sub_8032d010(lVar3);
        }
        lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
        if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
          lVar3 = sub_8032d010(lVar3);
        }
        if (**(longlong **)(lVar3 + 0xb8) != 0) {
          ViewManager::OpenMissionNoticeWindow(**(longlong **)(lVar3 + 0xb8),0,0);
          if (*(longlong *)(param_1 + 0x48) != 0) {
            *(undefined4 *)(*(longlong *)(param_1 + 0x48) + 0x6c0) = 0;
            lVar3 = *(longlong *)(param_1 + 0x50);
            if (*(longlong *)(param_1 + 0x48) != 0) {
              iVar2 = UserData::get_TotalGold(*(longlong *)(param_1 + 0x48),0);
              if ((*(longlong *)(param_1 + 0x48) != 0) && (lVar3 != 0)) {
                CustomUILabel::SetTextCurrency(lVar3,(longlong)
                                    (iVar2 - *(int *)(*(longlong *)(param_1 + 0x48) + 0x6c0)),0);
                UnitSkill::UpdateSkillSet(param_1,1,0);
                UnitSkill::callSkillLevelUpApiFinishCallback(param_1,0);
                return;
              }
            }
          }
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitSkill::<onClickSkillEnhance>b__42_0  @ 0x14a6db0  size=1 ==== */

void UnitSkill::<onClickSkillEnhance>b__42_0(longlong param_1)

{
  undefined8 uVar1;
  undefined8 uVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  code *pcVar7;
  undefined4 uVar8;
  undefined4 uVar9;
  longlong lVar10;
  longlong lVar11;
  undefined8 uStack_58;
  undefined4 uStack_50;
  
  if (cRam000000018ab6257d == '\0') {
    sub_8032f6d0(0x187b3dd28);
    cRam000000018ab6257d = '\x01';
  }
  lVar10 = *(longlong *)(lRam0000000187b3dd28 + 0x20);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  lVar10 = *(longlong *)(*(longlong *)(lVar10 + 0xc0) + 0x10);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  if (**(longlong **)(lVar10 + 0xb8) == 0) {
    sub_8032f9d0();
    pcVar7 = (code *)swi(3);
    (*pcVar7)();
    return;
  }
  SaveDataManager::SetUnitContentRelease(**(longlong **)(lVar10 + 0xb8),1,1);
  if (cRam000000018ab62570 == '\0') {
    sub_8032f6d0(0x187b35d58,1,0);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187c05928);
    cRam000000018ab62570 = '\x01';
  }
  lVar10 = *(longlong *)(lRam0000000187c16f00 + 0x20);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  lVar10 = *(longlong *)(*(longlong *)(lVar10 + 0xc0) + 0x10);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  if ((**(longlong **)(lVar10 + 0xb8) != 0) &&
     (lVar10 = *(longlong *)(**(longlong **)(lVar10 + 0xb8) + 0x90), lVar10 != 0)) {
    lVar10 = sub_855bdd60(lVar10,*(undefined4 *)(param_1 + 0x30),uRam0000000187b35d58);
    uVar9 = *(undefined4 *)(param_1 + 0x30);
    if ((lVar10 != 0) && (lVar11 = *(longlong *)(lVar10 + 0x10), lVar11 != 0)) {
      uVar1 = *(undefined8 *)(lVar11 + 0x34);
      uVar8 = *(undefined4 *)(lVar11 + 0x3c);
      if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_58 = uVar1;
      uStack_50 = uVar8;
      uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_58,0);
      if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar9 = UnitUtility::GetConversionUnit(uVar9,uVar8,0);
      lVar11 = *(longlong *)(lRam0000000187b3c708 + 0x20);
      if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
        lVar11 = sub_8032d010(lVar11);
      }
      lVar11 = *(longlong *)(*(longlong *)(lVar11 + 0xc0) + 0x10);
      if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
        lVar11 = sub_8032d010(lVar11);
      }
      if (((**(longlong **)(lVar11 + 0xb8) != 0) &&
          (lVar11 = *(longlong *)(**(longlong **)(lVar11 + 0xb8) + 0x510), lVar11 != 0)) &&
         (lVar11 = EPFCIGDAPHC::CBBMJADBDGF(lVar11,uVar9,0), lVar11 != 0)) {
        uVar1 = *(undefined8 *)(lVar11 + 0x10);
        uVar2 = *(undefined8 *)(lVar11 + 0x28);
        uVar3 = *(undefined8 *)(lVar11 + 0x18);
        lVar11 = *(longlong *)(lVar10 + 0x10);
        if (lVar11 != 0) {
          uVar4 = *(undefined8 *)(lVar11 + 0x68);
          uVar5 = *(undefined8 *)(lVar11 + 0x70);
          uStack_58 = *(undefined8 *)(lVar11 + 0x78);
          uVar6 = *(undefined8 *)(*(longlong *)(lVar10 + 0x10) + 0x80);
          if ((*(longlong *)(param_1 + 0x20) != 0) &&
             (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x30), lVar11 != 0)) {
            uVar9 = sub_8046d800(lVar11,0);
            *(undefined1 *)(param_1 + 0x90) = 1;
            if (*(longlong *)(param_1 + 0x20) != 0) {
              PartsUnitSkillSlot::SetData(*(longlong *)(param_1 + 0x20),*(undefined8 *)(lVar10 + 0x10),uVar1,uVar2
                            ,uVar3,uVar4,uVar5,uStack_58,uVar6,0);
              UIProgressBar::set_value(lVar11,uVar9,0);
              *(undefined1 *)(param_1 + 0x90) = 0;
              return;
            }
          }
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar7 = (code *)swi(3);
  (*pcVar7)();
  return;
}



/* ==== UnitSkill::ClearSkillLevelupInfo  @ 0x14a5f90  size=1 ==== */

void UnitSkill::ClearSkillLevelupInfo(longlong param_1)

{
  int iVar1;
  longlong lVar2;
  longlong lVar3;
  code *pcVar4;
  
  lVar2 = *(longlong *)(param_1 + 0x68);
  if (lVar2 != 0) {
    if (cRam000000018ab6259d == '\0') {
      sub_8032f6d0(0x187b94d88);
      cRam000000018ab6259d = '\x01';
    }
    lVar2 = *(longlong *)(lVar2 + 0x10);
    if (lVar2 != 0) {
      iVar1 = *(int *)(lVar2 + 0x20);
      if (0 < iVar1) {
        lVar3 = *(longlong *)(lVar2 + 0x10);
        if (lVar3 == 0) {
          sub_8032f9d0();
          pcVar4 = (code *)swi(3);
          (*pcVar4)();
          return;
        }
        Array::Clear(lVar3,0,*(undefined4 *)(lVar3 + 0x18),0);
        *(undefined4 *)(lVar2 + 0x20) = 0;
        *(undefined4 *)(lVar2 + 0x28) = 0;
        *(undefined4 *)(lVar2 + 0x24) = 0xffffffff;
        Array::Clear(*(undefined8 *)(lVar2 + 0x18),0,iVar1,0);
      }
      *(int *)(lVar2 + 0x2c) = *(int *)(lVar2 + 0x2c) + 1;
      return;
    }
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitSkill::ConnectSkillLevelUpApi  @ 0x14a5fe0  size=1 ==== */

void UnitSkill::ConnectSkillLevelUpApi(longlong param_1,undefined8 param_2)

{
  uint uVar1;
  longlong lVar2;
  code *pcVar3;
  longlong lVar4;
  longlong lVar5;
  undefined8 uVar6;
  int iVar7;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab62579 == '\0') {
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187b682c8);
    sub_8032f6d0(0x187c34f38);
    sub_8032f6d0(0x187b382e8);
    sub_8032f6d0(0x187b388b0);
    cRam000000018ab62579 = '\x01';
  }
  lVar5 = lRam0000000187b682c8;
  lVar4 = *(longlong *)(param_1 + 0x78);
  if (lVar4 != 0) {
    *(int *)(lVar4 + 0x1c) = *(int *)(lVar4 + 0x1c) + 1;
    lVar2 = *(longlong *)(lVar4 + 0x10);
    uVar1 = *(uint *)(lVar4 + 0x18);
    if (lVar2 != 0) {
      if (uVar1 < *(uint *)(lVar2 + 0x18)) {
        *(uint *)(lVar4 + 0x18) = uVar1 + 1;
        if (*(uint *)(lVar2 + 0x18) <= uVar1) {
          sub_8032f9c0();
          pcVar3 = (code *)swi(3);
          (*pcVar3)();
          return;
        }
        *(undefined8 *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = param_2;
        sub_8032e8d0(lVar2 + 0x20 + (longlong)(int)uVar1 * 8);
      }
      else {
        sub_84a97470(lVar4,param_2,
                      *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
      }
      if (*(char *)(param_1 + 0x70) != '\0') {
        return;
      }
      if ((*(longlong *)(param_1 + 0x68) != 0) && (lVar4 = SkillLevelUpTemporaryStorage::MakeProcessingInfoList(), lVar4 != 0)) {
        if (*(int *)(lVar4 + 0x18) < 1) {
          if (cRam000000018ab6257a == '\0') {
            sub_8032f6d0(0x187b68488);
            sub_8032f6d0(0x187b68b78);
            sub_8032f6d0(0x187b68d30);
            cRam000000018ab6257a = '\x01';
          }
          lVar4 = *(longlong *)(param_1 + 0x78);
          iVar7 = 0;
          do {
            if (lVar4 == 0) {
code_r0x0001814a739a:
              sub_8032f9d0();
              pcVar3 = (code *)swi(3);
              (*pcVar3)();
              return;
            }
            if (*(int *)(lVar4 + 0x18) <= iVar7) {
              lVar4 = *(longlong *)(param_1 + 0x78);
              if (lVar4 != 0) {
                iVar7 = *(int *)(lVar4 + 0x18);
                *(int *)(lVar4 + 0x1c) = *(int *)(lVar4 + 0x1c) + 1;
                *(undefined4 *)(lVar4 + 0x18) = 0;
                if (0 < iVar7) {
                  Array::Clear(*(undefined8 *)(lVar4 + 0x10),0,iVar7,0,unaff_RDI);
                }
                return;
              }
              goto code_r0x0001814a739a;
            }
            if (lVar4 == 0) goto code_r0x0001814a739a;
            uVar6 = sub_849ed2d0(lVar4,iVar7,uRam0000000187b68d30);
            EventExtension::Call(uVar6,0);
            lVar4 = *(longlong *)(param_1 + 0x78);
            iVar7 = iVar7 + 1;
          } while( true );
        }
        *(undefined1 *)(param_1 + 0x70) = 1;
        lVar5 = sub_80003d20(uRam0000000187b382e8);
        if (lVar5 != 0) {
          ApiManager::AddSkillLevelUpPostParam(lVar5,*(undefined4 *)(param_1 + 0x30),lVar4,0,0);
          uVar6 = sub_8032f970(uRam0000000187c32f48);
          sub_8044b290(uVar6,param_1,uRam0000000187b388b0,0);
          ApiManager::Exec(lVar5,uVar6,0,0,0);
          return;
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::CreateAllSkillLevelInfo  @ 0x14a6190  size=1 ==== */

void UnitSkill::CreateAllSkillLevelInfo(longlong param_1)

{
  int iVar1;
  uint uVar2;
  longlong lVar3;
  code *pcVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 uVar9;
  longlong lVar10;
  undefined8 uStack_18;
  undefined4 uStack_10;
  
  if (cRam000000018ab62571 == '\0') {
    sub_8032f6d0(0x187b35d58);
    sub_8032f6d0(0x187c32ee0);
    sub_8032f6d0(0x187c33090);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187c05928);
    cRam000000018ab62571 = '\x01';
  }
  lVar7 = *(longlong *)(lRam0000000187c16f00 + 0x20);
  if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
    lVar7 = sub_8032d010(lVar7);
  }
  lVar7 = *(longlong *)(*(longlong *)(lVar7 + 0xc0) + 0x10);
  if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
    lVar7 = sub_8032d010(lVar7);
  }
  if ((**(longlong **)(lVar7 + 0xb8) != 0) &&
     (lVar7 = *(longlong *)(**(longlong **)(lVar7 + 0xb8) + 0x90), lVar7 != 0)) {
    lVar7 = sub_855bdd60(lVar7,*(undefined4 *)(param_1 + 0x30),uRam0000000187b35d58);
    if (lVar7 != 0) {
      lVar10 = *(longlong *)(param_1 + 0x58);
      lVar7 = *(longlong *)(lVar7 + 0x10);
      if (lVar10 != 0) {
        iVar1 = *(int *)(lVar10 + 0x18);
        *(int *)(lVar10 + 0x1c) = *(int *)(lVar10 + 0x1c) + 1;
        *(undefined4 *)(lVar10 + 0x18) = 0;
        if (0 < iVar1) {
          Array::Clear(*(undefined8 *)(lVar10 + 0x10),0,iVar1,0);
        }
        if ((lVar7 != 0) && (*(longlong *)(param_1 + 0x58) != 0)) {
          sub_84a97390(*(longlong *)(param_1 + 0x58),*(undefined8 *)(lVar7 + 0x68),
                              uRam0000000187c32ee0);
          if (*(longlong *)(param_1 + 0x58) != 0) {
            sub_84a97390(*(longlong *)(param_1 + 0x58),*(undefined8 *)(lVar7 + 0x70),
                                uRam0000000187c32ee0);
            if (*(longlong *)(param_1 + 0x58) != 0) {
              sub_84a97390(*(longlong *)(param_1 + 0x58),*(undefined8 *)(lVar7 + 0x78),
                                  uRam0000000187c32ee0);
              uStack_18 = *(undefined8 *)(lVar7 + 0x34);
              uVar6 = *(undefined4 *)(param_1 + 0x30);
              uStack_10 = *(undefined4 *)(lVar7 + 0x3c);
              if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uVar5 = ObscuredInt::FHOALPFLEEC(&uStack_18,0);
              if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uVar6 = UnitUtility::GetConversionUnit(uVar6,uVar5,0);
              lVar7 = *(longlong *)(lRam0000000187b3c708 + 0x20);
              if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
                lVar7 = sub_8032d010(lVar7);
              }
              lVar7 = *(longlong *)(*(longlong *)(lVar7 + 0xc0) + 0x10);
              if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
                lVar7 = sub_8032d010(lVar7);
              }
              if ((**(longlong **)(lVar7 + 0xb8) != 0) &&
                 (lVar7 = *(longlong *)(**(longlong **)(lVar7 + 0xb8) + 0x510), lVar7 != 0)) {
                lVar7 = EPFCIGDAPHC::CBBMJADBDGF(lVar7,uVar6,0);
                if (lVar7 != 0) {
                  lVar10 = *(longlong *)(param_1 + 0x60);
                  lVar8 = *(longlong *)(lVar7 + 0x10);
                  lVar3 = *(longlong *)(lVar7 + 0x28);
                  lVar7 = *(longlong *)(lVar7 + 0x18);
                  if (lVar10 != 0) {
                    iVar1 = *(int *)(lVar10 + 0x18);
                    *(int *)(lVar10 + 0x1c) = *(int *)(lVar10 + 0x1c) + 1;
                    *(undefined4 *)(lVar10 + 0x18) = 0;
                    if (0 < iVar1) {
                      Array::Clear(*(undefined8 *)(lVar10 + 0x10),0,iVar1,0);
                    }
                    if (lVar8 != 0) {
                      uVar6 = sub_84a610f0(lVar8,0,uRam0000000187b446b8);
                      UnitSkill::createMasterData(param_1,0x65,uVar6);
                      if (lVar3 != 0) {
                        uVar6 = sub_84a610f0(lVar3,0,uRam0000000187b446b8);
                        UnitSkill::createMasterData(param_1,0xc9,uVar6,0);
                        uVar6 = sub_84a610f0(lVar3,1,uRam0000000187b446b8);
                        UnitSkill::createMasterData(param_1,0xca,uVar6,0);
                        if (lVar7 != 0) {
                          uVar6 = sub_84a610f0(lVar7,0,uRam0000000187b446b8);
                          if (cRam000000018ab62572 == '\0') {
                            sub_8032f6d0(0x187c32d28,0x12d,uVar6,0);
                            sub_8032f6d0(0x187c33248);
                            sub_8032f6d0(0x187b60fa0);
                            sub_8032f6d0(0x187b92ff0);
                            sub_8032f6d0(0x187b78350);
                            sub_8032f6d0(0x187be3678);
                            sub_8032f6d0(0x187c2ca48);
                            cRam000000018ab62572 = '\x01';
                          }
                          lVar7 = sub_8032f970(uRam0000000187c2ca48);
                          sub_80443e50(lVar7,0);
                          if (lVar7 != 0) {
                            *(undefined4 *)(lVar7 + 0x10) = uVar6;
                            lVar8 = sub_8032f970(uRam0000000187b78350);
                            SkillLevelInfo::.ctor(lVar8,0);
                            lVar10 = *(longlong *)(param_1 + 0x58);
                            uVar9 = sub_8032f970(uRam0000000187b92ff0);
                            sub_84cacb40(uVar9,lVar7,uRam0000000187be3678,0);
                            if (lVar10 != 0) {
                              lVar10 = sub_84a8c920(lVar10,uVar9,uRam0000000187c33248);
                              if (lVar10 == 0) {
                                if (lVar8 == 0) goto code_r0x0001814a75c2;
                                SkillLevelInfo::SetSkillId(lVar8,*(undefined4 *)(lVar7 + 0x10),0);
                                uVar6 = 0;
                              }
                              else {
                                uStack_18 = *(undefined8 *)(lVar10 + 0x10);
                                uVar6 = *(undefined4 *)(lVar10 + 0x18);
                                if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                  sub_8032fa70();
                                }
                                uStack_10 = uVar6;
                                uVar6 = ObscuredInt::FHOALPFLEEC(&uStack_18,0);
                                if (lVar8 == 0) goto code_r0x0001814a75c2;
                                SkillLevelInfo::SetSkillId(lVar8,uVar6,0);
                                uStack_18 = *(undefined8 *)(lVar10 + 0x1c);
                                uStack_10 = *(undefined4 *)(lVar10 + 0x24);
                                uVar6 = ObscuredInt::FHOALPFLEEC(&uStack_18,0);
                              }
                              SkillLevelInfo::SetSkillLevel(lVar8,uVar6,0);
                              lVar10 = lRam0000000187c32d28;
                              lVar7 = *(longlong *)(param_1 + 0x60);
                              if (lVar7 != 0) {
                                *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
                                lVar3 = *(longlong *)(lVar7 + 0x10);
                                uVar2 = *(uint *)(lVar7 + 0x18);
                                if (lVar3 != 0) {
                                  if (uVar2 < *(uint *)(lVar3 + 0x18)) {
                                    *(uint *)(lVar7 + 0x18) = uVar2 + 1;
                                    if (*(uint *)(lVar3 + 0x18) <= uVar2) {
                                      sub_8032f9c0();
                                      pcVar4 = (code *)swi(3);
                                      (*pcVar4)();
                                      return;
                                    }
                                    *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar2 * 8) = lVar8;
                                    sub_8032e8d0(lVar3 + 0x20 + (longlong)(int)uVar2 * 8,
                                                        lVar8);
                                  }
                                  else {
                                    sub_84a97470(lVar7,lVar8,
                                                  *(undefined8 *)
                                                   (*(longlong *)
                                                     (*(longlong *)(lVar10 + 0x20) + 0xc0) + 0x70));
                                  }
                                  return;
                                }
                              }
                            }
                          }
code_r0x0001814a75c2:
                          sub_8032f9d0();
                          pcVar4 = (code *)swi(3);
                          (*pcVar4)();
                          return;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitSkill::Initialize  @ 0x14a64f0  size=1 ==== */

void UnitSkill::Initialize(longlong param_1,undefined4 param_2,undefined8 param_3,undefined8 param_4,
                  undefined8 param_5,longlong param_6)

{
  int iVar1;
  uint uVar2;
  longlong lVar3;
  code *pcVar4;
  char cVar5;
  undefined4 uVar6;
  undefined4 uVar7;
  longlong lVar8;
  undefined8 uVar9;
  undefined8 uVar10;
  undefined8 uVar11;
  longlong lVar12;
  longlong lVar13;
  
  if (cRam000000018ab6256f == '\0') {
    sub_8032f6d0(0x187c350b8);
    sub_8032f6d0(0x187b51880);
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187b9bd50);
    sub_8032f6d0(0x187c2f5f0);
    sub_8032f6d0(0x187c050b0);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187c2d2d0);
    sub_8032f6d0(0x187b38dc8);
    sub_8032f6d0(0x187b38f80);
    sub_8032f6d0(0x187b39130);
    sub_8032f6d0(0x187b39650);
    sub_8032f6d0(0x187b39800);
    sub_8032f6d0(0x187b399b8);
    sub_8032f6d0(0x187b39b68);
    cRam000000018ab6256f = '\x01';
  }
  lVar8 = *(longlong *)(lRam0000000187c16f00 + 0x20);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = sub_8032d010(lVar8);
  }
  lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = sub_8032d010(lVar8);
  }
  *(undefined8 *)(param_1 + 0x48) = **(undefined8 **)(lVar8 + 0xb8);
  sub_8032e8d0(param_1 + 0x48);
  *(undefined4 *)(param_1 + 0x30) = param_2;
  *(undefined8 *)(param_1 + 0x98) = param_4;
  sub_8032e8d0(param_1 + 0x98,param_4);
  if (*(longlong *)(param_1 + 0x48) != 0) {
    *(undefined4 *)(*(longlong *)(param_1 + 0x48) + 0x6c0) = 0;
    *(undefined8 *)(param_1 + 0xa0) = param_3;
    sub_8032e8d0(param_1 + 0xa0,param_3);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar5 = Object::op_Inequality(param_5,0,0);
    if (cVar5 != '\0') {
      *(undefined8 *)(param_1 + 0x50) = param_5;
      sub_8032e8d0(param_1 + 0x50,param_5);
    }
    if (param_6 != 0) {
      *(longlong *)(param_1 + 0x80) = param_6;
      sub_8032e8d0(param_1 + 0x80);
    }
    lVar8 = *(longlong *)(param_1 + 0x20);
    uVar9 = sub_8032f970(uRam0000000187c350b8);
    sub_81032a90(uVar9,param_1,uRam0000000187b38f80,0);
    if (lVar8 != 0) {
      *(undefined8 *)(lVar8 + 0x78) = uVar9;
      sub_8032e8d0(lVar8 + 0x78,uVar9);
      lVar8 = *(longlong *)(param_1 + 0x20);
      uVar9 = sub_8032f970(uRam0000000187b51880);
      sub_80afad00(uVar9,param_1,uRam0000000187b39130,0);
      if (lVar8 != 0) {
        *(undefined8 *)(lVar8 + 0x68) = uVar9;
        sub_8032e8d0(lVar8 + 0x68,uVar9);
        lVar8 = *(longlong *)(param_1 + 0x20);
        uVar9 = sub_8032f970(uRam0000000187b51880);
        sub_80afad00(uVar9,param_1,uRam0000000187b38dc8,0);
        if (lVar8 != 0) {
          *(undefined8 *)(lVar8 + 0x70) = uVar9;
          sub_8032e8d0(lVar8 + 0x70,uVar9);
          lVar8 = *(longlong *)(param_1 + 0x20);
          uVar9 = sub_8032f970(uRam0000000187c32f48);
          sub_8044b290(uVar9,param_1,uRam0000000187b39800,0);
          uVar10 = sub_8032f970(uRam0000000187c32f48);
          sub_8044b290(uVar10,param_1,uRam0000000187b399b8,0);
          uVar11 = sub_8032f970(uRam0000000187c32f48);
          sub_8044b290(uVar11,param_1,uRam0000000187b39b68,0);
          lVar12 = sub_8032f970(uRam0000000187c2d2d0);
          sub_80443e50(lVar12,0);
          *(undefined8 *)(lVar12 + 0x10) = uVar9;
          sub_8032e8d0(lVar12 + 0x10,uVar9);
          *(undefined8 *)(lVar12 + 0x18) = uVar10;
          sub_8032e8d0(lVar12 + 0x18,uVar10);
          *(undefined8 *)(lVar12 + 0x20) = uVar11;
          sub_8032e8d0(lVar12 + 0x20,uVar11);
          if (lVar8 != 0) {
            *(longlong *)(lVar8 + 0x80) = lVar12;
            sub_8032e8d0(lVar8 + 0x80,lVar12);
            if (*(longlong *)(param_1 + 0x28) != 0) {
              PartsSkillupAnim::Stop(*(longlong *)(param_1 + 0x28),0);
              UnitSkill::UpdateSkillSet(param_1,0,0);
              if ((*(longlong *)(param_1 + 0x20) != 0) &&
                 (lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x30), lVar8 != 0)) {
                lVar8 = *(longlong *)(lVar8 + 0x68);
                uVar9 = sub_8032f970(uRam0000000187b9bd50);
                sub_8044b290(uVar9,param_1,uRam0000000187b39650,0);
                uVar10 = sub_8032f970(uRam0000000187c2f5f0);
                EventDelegate::.ctor(uVar10,uVar9,0);
                lVar12 = lRam0000000187c050b0;
                if (lVar8 != 0) {
                  *(int *)(lVar8 + 0x1c) = *(int *)(lVar8 + 0x1c) + 1;
                  lVar13 = *(longlong *)(lVar8 + 0x10);
                  uVar2 = *(uint *)(lVar8 + 0x18);
                  if (lVar13 != 0) {
                    if (uVar2 < *(uint *)(lVar13 + 0x18)) {
                      *(uint *)(lVar8 + 0x18) = uVar2 + 1;
                      if (*(uint *)(lVar13 + 0x18) <= uVar2) {
                        sub_8032f9c0();
                        pcVar4 = (code *)swi(3);
                        (*pcVar4)();
                        return;
                      }
                      *(undefined8 *)(lVar13 + 0x20 + (longlong)(int)uVar2 * 8) = uVar10;
                      sub_8032e8d0(lVar13 + (longlong)(int)uVar2 * 8 + 0x20,uVar10);
                    }
                    else {
                      sub_84a97470(lVar8,uVar10,
                                    *(undefined8 *)
                                     (*(longlong *)(*(longlong *)(lVar12 + 0x20) + 0xc0) + 0x70));
                    }
                    if (cRam000000018ab62571 == '\0') {
                      sub_8032f6d0(0x187b35d58,0);
                      sub_8032f6d0(0x187c32ee0);
                      sub_8032f6d0(0x187c33090);
                      sub_8032f6d0(0x187b446b8);
                      sub_8032f6d0(0x187b3c708);
                      sub_8032f6d0(0x187b60fa0);
                      sub_8032f6d0(0x187c16f00);
                      sub_8032f6d0(0x187c05928);
                      cRam000000018ab62571 = '\x01';
                    }
                    lVar8 = *(longlong *)(lRam0000000187c16f00 + 0x20);
                    if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
                      lVar8 = sub_8032d010(lVar8);
                    }
                    lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
                    if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
                      lVar8 = sub_8032d010(lVar8);
                    }
                    if ((**(longlong **)(lVar8 + 0xb8) != 0) &&
                       (lVar8 = *(longlong *)(**(longlong **)(lVar8 + 0xb8) + 0x90), lVar8 != 0)) {
                      lVar8 = sub_855bdd60(lVar8,*(undefined4 *)(param_1 + 0x30),
                                            uRam0000000187b35d58);
                      if (lVar8 != 0) {
                        lVar12 = *(longlong *)(param_1 + 0x58);
                        lVar8 = *(longlong *)(lVar8 + 0x10);
                        if (lVar12 != 0) {
                          iVar1 = *(int *)(lVar12 + 0x18);
                          *(int *)(lVar12 + 0x1c) = *(int *)(lVar12 + 0x1c) + 1;
                          *(undefined4 *)(lVar12 + 0x18) = 0;
                          if (0 < iVar1) {
                            Array::Clear(*(undefined8 *)(lVar12 + 0x10),0,iVar1,0);
                          }
                          if ((lVar8 != 0) && (*(longlong *)(param_1 + 0x58) != 0)) {
                            sub_84a97390(*(longlong *)(param_1 + 0x58),
                                                *(undefined8 *)(lVar8 + 0x68),uRam0000000187c32ee0);
                            if (*(longlong *)(param_1 + 0x58) != 0) {
                              sub_84a97390(*(longlong *)(param_1 + 0x58),
                                                  *(undefined8 *)(lVar8 + 0x70),uRam0000000187c32ee0
                                                 );
                              if (*(longlong *)(param_1 + 0x58) != 0) {
                                sub_84a97390(*(longlong *)(param_1 + 0x58),
                                                    *(undefined8 *)(lVar8 + 0x78),
                                                    uRam0000000187c32ee0);
                                uVar7 = *(undefined4 *)(param_1 + 0x30);
                                if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                  sub_8032fa70();
                                }
                                uVar6 = ObscuredInt::FHOALPFLEEC(&stack0xffffffffffffffe8,0);
                                if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
                                  sub_8032fa70();
                                }
                                uVar7 = UnitUtility::GetConversionUnit(uVar7,uVar6,0);
                                lVar8 = *(longlong *)(lRam0000000187b3c708 + 0x20);
                                if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
                                  lVar8 = sub_8032d010(lVar8);
                                }
                                lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
                                if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
                                  lVar8 = sub_8032d010(lVar8);
                                }
                                if ((**(longlong **)(lVar8 + 0xb8) != 0) &&
                                   (lVar8 = *(longlong *)(**(longlong **)(lVar8 + 0xb8) + 0x510),
                                   lVar8 != 0)) {
                                  lVar8 = EPFCIGDAPHC::CBBMJADBDGF(lVar8,uVar7,0);
                                  if (lVar8 != 0) {
                                    lVar12 = *(longlong *)(param_1 + 0x60);
                                    lVar13 = *(longlong *)(lVar8 + 0x10);
                                    lVar3 = *(longlong *)(lVar8 + 0x28);
                                    lVar8 = *(longlong *)(lVar8 + 0x18);
                                    if (lVar12 != 0) {
                                      iVar1 = *(int *)(lVar12 + 0x18);
                                      *(int *)(lVar12 + 0x1c) = *(int *)(lVar12 + 0x1c) + 1;
                                      *(undefined4 *)(lVar12 + 0x18) = 0;
                                      if (0 < iVar1) {
                                        Array::Clear(*(undefined8 *)(lVar12 + 0x10),0,iVar1,0);
                                      }
                                      if (lVar13 != 0) {
                                        uVar7 = sub_84a610f0(lVar13,0,uRam0000000187b446b8);
                                        UnitSkill::createMasterData(param_1,0x65,uVar7);
                                        if (lVar3 != 0) {
                                          uVar7 = sub_84a610f0(lVar3,0,uRam0000000187b446b8);
                                          UnitSkill::createMasterData(param_1,0xc9,uVar7,0);
                                          uVar7 = sub_84a610f0(lVar3,1,uRam0000000187b446b8);
                                          UnitSkill::createMasterData(param_1,0xca,uVar7,0);
                                          if (lVar8 != 0) {
                                            uVar7 = sub_84a610f0(lVar8,0,uRam0000000187b446b8);
                                            if (cRam000000018ab62572 == '\0') {
                                              sub_8032f6d0(0x187c32d28,0x12d,uVar7,0);
                                              sub_8032f6d0(0x187c33248);
                                              sub_8032f6d0(0x187b60fa0);
                                              sub_8032f6d0(0x187b92ff0);
                                              sub_8032f6d0(0x187b78350);
                                              sub_8032f6d0(0x187be3678);
                                              sub_8032f6d0(0x187c2ca48);
                                              cRam000000018ab62572 = '\x01';
                                            }
                                            lVar8 = sub_8032f970(uRam0000000187c2ca48);
                                            sub_80443e50(lVar8,0);
                                            if (lVar8 != 0) {
                                              *(undefined4 *)(lVar8 + 0x10) = uVar7;
                                              lVar13 = sub_8032f970(uRam0000000187b78350);
                                              SkillLevelInfo::.ctor(lVar13,0);
                                              lVar12 = *(longlong *)(param_1 + 0x58);
                                              uVar9 = sub_8032f970(uRam0000000187b92ff0);
                                              sub_84cacb40(uVar9,lVar8,uRam0000000187be3678,0);
                                              if (lVar12 != 0) {
                                                lVar12 = sub_84a8c920(lVar12,uVar9,
                                                                       uRam0000000187c33248);
                                                if (lVar12 == 0) {
                                                  if (lVar13 == 0) goto code_r0x0001814a75c2;
                                                  SkillLevelInfo::SetSkillId(lVar13,*(undefined4 *)(lVar8 + 0x10)
                                                                ,0);
                                                  uVar7 = 0;
                                                }
                                                else {
                                                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                                    sub_8032fa70();
                                                  }
                                                  uVar7 = ObscuredInt::FHOALPFLEEC(&stack0xffffffffffffffe8,0);
                                                  if (lVar13 == 0) goto code_r0x0001814a75c2;
                                                  SkillLevelInfo::SetSkillId(lVar13,uVar7,0);
                                                  uVar7 = ObscuredInt::FHOALPFLEEC(&stack0xffffffffffffffe8,0);
                                                }
                                                SkillLevelInfo::SetSkillLevel(lVar13,uVar7,0);
                                                lVar12 = lRam0000000187c32d28;
                                                lVar8 = *(longlong *)(param_1 + 0x60);
                                                if (lVar8 != 0) {
                                                  *(int *)(lVar8 + 0x1c) =
                                                       *(int *)(lVar8 + 0x1c) + 1;
                                                  lVar3 = *(longlong *)(lVar8 + 0x10);
                                                  uVar2 = *(uint *)(lVar8 + 0x18);
                                                  if (lVar3 != 0) {
                                                    if (uVar2 < *(uint *)(lVar3 + 0x18)) {
                                                      *(uint *)(lVar8 + 0x18) = uVar2 + 1;
                                                      if (*(uint *)(lVar3 + 0x18) <= uVar2) {
                                                        sub_8032f9c0();
                                                        pcVar4 = (code *)swi(3);
                                                        (*pcVar4)();
                                                        return;
                                                      }
                                                      *(longlong *)
                                                       (lVar3 + 0x20 + (longlong)(int)uVar2 * 8) =
                                                           lVar13;
                                                      sub_8032e8d0(lVar3 + 0x20 +
                                                                          (longlong)(int)uVar2 * 8,
                                                                          lVar13);
                                                    }
                                                    else {
                                                      sub_84a97470(lVar8,lVar13,
                                                                    *(undefined8 *)
                                                                     (*(longlong *)
                                                                       (*(longlong *)(lVar12 + 0x20)
                                                                       + 0xc0) + 0x70));
                                                    }
                                                    return;
                                                  }
                                                }
                                              }
                                            }
code_r0x0001814a75c2:
                                            sub_8032f9d0();
                                            pcVar4 = (code *)swi(3);
                                            (*pcVar4)();
                                            return;
                                          }
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                    sub_8032f9d0();
                    pcVar4 = (code *)swi(3);
                    (*pcVar4)();
                    return;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitSkill::OnApplicationPause  @ 0x14a6980  size=10 ==== */

void UnitSkill::OnApplicationPause(longlong param_1)

{
  uint uVar1;
  longlong lVar2;
  code *pcVar3;
  longlong lVar4;
  longlong lVar5;
  undefined8 uVar6;
  int iVar7;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab62579 == '\0') {
    sub_8032f6d0(0x187c32f48,0,0);
    sub_8032f6d0(0x187b682c8);
    sub_8032f6d0(0x187c34f38);
    sub_8032f6d0(0x187b382e8);
    sub_8032f6d0(0x187b388b0);
    cRam000000018ab62579 = '\x01';
  }
  lVar5 = lRam0000000187b682c8;
  lVar4 = *(longlong *)(param_1 + 0x78);
  if (lVar4 != 0) {
    *(int *)(lVar4 + 0x1c) = *(int *)(lVar4 + 0x1c) + 1;
    lVar2 = *(longlong *)(lVar4 + 0x10);
    uVar1 = *(uint *)(lVar4 + 0x18);
    if (lVar2 != 0) {
      if (uVar1 < *(uint *)(lVar2 + 0x18)) {
        *(uint *)(lVar4 + 0x18) = uVar1 + 1;
        if (*(uint *)(lVar2 + 0x18) <= uVar1) {
          sub_8032f9c0();
          pcVar3 = (code *)swi(3);
          (*pcVar3)();
          return;
        }
        *(undefined8 *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = 0;
        sub_8032e8d0(lVar2 + 0x20 + (longlong)(int)uVar1 * 8);
      }
      else {
        sub_84a97470(lVar4,0,*(undefined8 *)
                               (*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
      }
      if (*(char *)(param_1 + 0x70) != '\0') {
        return;
      }
      if ((*(longlong *)(param_1 + 0x68) != 0) && (lVar4 = SkillLevelUpTemporaryStorage::MakeProcessingInfoList(), lVar4 != 0)) {
        if (*(int *)(lVar4 + 0x18) < 1) {
          if (cRam000000018ab6257a == '\0') {
            sub_8032f6d0(0x187b68488);
            sub_8032f6d0(0x187b68b78);
            sub_8032f6d0(0x187b68d30);
            cRam000000018ab6257a = '\x01';
          }
          lVar4 = *(longlong *)(param_1 + 0x78);
          iVar7 = 0;
          do {
            if (lVar4 == 0) {
code_r0x0001814a739a:
              sub_8032f9d0();
              pcVar3 = (code *)swi(3);
              (*pcVar3)();
              return;
            }
            if (*(int *)(lVar4 + 0x18) <= iVar7) {
              lVar4 = *(longlong *)(param_1 + 0x78);
              if (lVar4 != 0) {
                iVar7 = *(int *)(lVar4 + 0x18);
                *(int *)(lVar4 + 0x1c) = *(int *)(lVar4 + 0x1c) + 1;
                *(undefined4 *)(lVar4 + 0x18) = 0;
                if (0 < iVar7) {
                  Array::Clear(*(undefined8 *)(lVar4 + 0x10),0,iVar7,0,unaff_RDI);
                }
                return;
              }
              goto code_r0x0001814a739a;
            }
            if (lVar4 == 0) goto code_r0x0001814a739a;
            uVar6 = sub_849ed2d0(lVar4,iVar7,uRam0000000187b68d30);
            EventExtension::Call(uVar6,0);
            lVar4 = *(longlong *)(param_1 + 0x78);
            iVar7 = iVar7 + 1;
          } while( true );
        }
        *(undefined1 *)(param_1 + 0x70) = 1;
        lVar5 = sub_80003d20(uRam0000000187b382e8);
        if (lVar5 != 0) {
          ApiManager::AddSkillLevelUpPostParam(lVar5,*(undefined4 *)(param_1 + 0x30),lVar4,0,0);
          uVar6 = sub_8032f970(uRam0000000187c32f48);
          sub_8044b290(uVar6,param_1,uRam0000000187b388b0,0);
          ApiManager::Exec(lVar5,uVar6,0,0,0);
          return;
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::OnDisable  @ 0x14a6990  size=1 ==== */

void UnitSkill::OnDisable(longlong param_1)

{
  undefined8 uVar1;
  longlong lVar2;
  code *pcVar3;
  char cVar4;
  
  if (cRam000000018ab6256e == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab6256e = '\x01';
  }
  uVar1 = *(undefined8 *)(param_1 + 0x88);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar4 = Object::op_Inequality(uVar1,0,0);
  if (cVar4 != '\0') {
    if (*(longlong *)(param_1 + 0x88) == 0) goto code_r0x0001814a6a76;
    DelayExecManager::Clear(*(longlong *)(param_1 + 0x88),0);
  }
  if (*(longlong *)(param_1 + 0x80) != 0) {
    uVar1 = *(undefined8 *)(*(longlong *)(param_1 + 0x80) + 0x10);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar4 = Object::op_Inequality(uVar1,0,0);
    if (cVar4 != '\0') {
      if ((*(longlong *)(param_1 + 0x80) == 0) ||
         (lVar2 = *(longlong *)(*(longlong *)(param_1 + 0x80) + 0x10), lVar2 == 0)) {
code_r0x0001814a6a76:
        sub_8032f9d0();
        pcVar3 = (code *)swi(3);
        (*pcVar3)();
        return;
      }
      UnitDetailEffectManager::Stop(lVar2,4);
      UnitDetailEffectManager::Stop(lVar2,5);
    }
  }
  return;
}



/* ==== UnitSkill::Start  @ 0x14a6a80  size=1 ==== */

void UnitSkill::Start(longlong param_1)

{
  longlong *plVar1;
  code *pcVar2;
  longlong lVar3;
  undefined8 uVar4;
  longlong lVar5;
  longlong lVar6;
  uint uVar7;
  
  if (cRam000000018ab6256d == '\0') {
    sub_8032f6d0(0x187baa098);
    sub_8032f6d0(0x187b382e8);
    sub_8032f6d0(0x187b38ed0);
    sub_8032f6d0(0x187c16f00);
    cRam000000018ab6256d = '\x01';
  }
  lVar3 = *(longlong *)(lRam0000000187b38ed0 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  *(undefined8 *)(param_1 + 0x38) = **(undefined8 **)(lVar3 + 0xb8);
  sub_8032e8d0(param_1 + 0x38);
  lVar3 = *(longlong *)(lRam0000000187b382e8 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  *(undefined8 *)(param_1 + 0x40) = **(undefined8 **)(lVar3 + 0xb8);
  sub_8032e8d0(param_1 + 0x40);
  lVar3 = *(longlong *)(lRam0000000187c16f00 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  *(undefined8 *)(param_1 + 0x48) = **(undefined8 **)(lVar3 + 0xb8);
  sub_8032e8d0(param_1 + 0x48);
  lVar3 = Component::get_gameObject(param_1,0);
  if (lVar3 != 0) {
    uVar4 = sub_847c6b70(lVar3,uRam0000000187baa098);
    *(undefined8 *)(param_1 + 0x88) = uVar4;
    sub_8032e8d0(param_1 + 0x88,uVar4);
    lVar3 = *(longlong *)(param_1 + 0x88);
    if (lVar3 != 0) {
      if (cRam000000018ab7b586 == '\0') {
        sub_8032f6d0(0x187c14170);
        sub_8032f6d0(0x187c4c5a8);
        cRam000000018ab7b586 = '\x01';
      }
      uVar4 = sub_8032e960(uRam0000000187c14170,0x10);
      *(undefined8 *)(lVar3 + 0x20) = uVar4;
      sub_8032e8d0(lVar3 + 0x20,uVar4);
      uVar7 = 0;
      lVar5 = *(longlong *)(lVar3 + 0x20);
      while (lVar5 != 0) {
        if (*(int *)(lVar5 + 0x18) <= (int)uVar7) {
          return;
        }
        plVar1 = *(longlong **)(lVar3 + 0x20);
        lVar5 = sub_8032f970(uRam0000000187c4c5a8);
        sub_80443e50(lVar5,0);
        if (plVar1 == (longlong *)0x0) break;
        if ((lVar5 != 0) &&
           (lVar6 = sub_8032e8f0(lVar5,*(undefined8 *)(*plVar1 + 0x40)), lVar6 == 0)) {
          uVar4 = sub_8032f220();
          sub_8032f990(uVar4,0);
          pcVar2 = (code *)swi(3);
          (*pcVar2)();
          return;
        }
        if (*(uint *)(plVar1 + 3) <= uVar7) {
          sub_8032f9c0();
          pcVar2 = (code *)swi(3);
          (*pcVar2)();
          return;
        }
        plVar1[(longlong)(int)uVar7 + 4] = lVar5;
        sub_8032e8d0(plVar1 + (longlong)(int)uVar7 + 4);
        uVar7 = uVar7 + 1;
        lVar5 = *(longlong *)(lVar3 + 0x20);
      }
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitSkill::UpdateSkillSet  @ 0x14a6e50  size=1 ==== */

void UnitSkill::UpdateSkillSet(longlong param_1,char param_2)

{
  undefined8 uVar1;
  undefined8 uVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  code *pcVar7;
  undefined4 uVar8;
  undefined4 uVar9;
  longlong lVar10;
  longlong lVar11;
  undefined8 uStack_58;
  undefined4 uStack_50;
  
  if (cRam000000018ab62570 == '\0') {
    sub_8032f6d0(0x187b35d58);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187c05928);
    cRam000000018ab62570 = '\x01';
  }
  lVar10 = *(longlong *)(lRam0000000187c16f00 + 0x20);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  lVar10 = *(longlong *)(*(longlong *)(lVar10 + 0xc0) + 0x10);
  if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
    lVar10 = sub_8032d010(lVar10);
  }
  if ((**(longlong **)(lVar10 + 0xb8) != 0) &&
     (lVar10 = *(longlong *)(**(longlong **)(lVar10 + 0xb8) + 0x90), lVar10 != 0)) {
    lVar10 = sub_855bdd60(lVar10,*(undefined4 *)(param_1 + 0x30),uRam0000000187b35d58);
    uVar9 = *(undefined4 *)(param_1 + 0x30);
    if ((lVar10 != 0) && (lVar11 = *(longlong *)(lVar10 + 0x10), lVar11 != 0)) {
      uVar1 = *(undefined8 *)(lVar11 + 0x34);
      uVar8 = *(undefined4 *)(lVar11 + 0x3c);
      if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_58 = uVar1;
      uStack_50 = uVar8;
      uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_58,0);
      if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar9 = UnitUtility::GetConversionUnit(uVar9,uVar8,0);
      lVar11 = *(longlong *)(lRam0000000187b3c708 + 0x20);
      if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
        lVar11 = sub_8032d010(lVar11);
      }
      lVar11 = *(longlong *)(*(longlong *)(lVar11 + 0xc0) + 0x10);
      if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
        lVar11 = sub_8032d010(lVar11);
      }
      if ((**(longlong **)(lVar11 + 0xb8) != 0) &&
         (lVar11 = *(longlong *)(**(longlong **)(lVar11 + 0xb8) + 0x510), lVar11 != 0)) {
        lVar11 = EPFCIGDAPHC::CBBMJADBDGF(lVar11,uVar9,0);
        if (lVar11 != 0) {
          uVar1 = *(undefined8 *)(lVar11 + 0x10);
          uVar2 = *(undefined8 *)(lVar11 + 0x28);
          uVar3 = *(undefined8 *)(lVar11 + 0x18);
          lVar11 = *(longlong *)(lVar10 + 0x10);
          if (lVar11 != 0) {
            uVar4 = *(undefined8 *)(lVar11 + 0x68);
            uVar5 = *(undefined8 *)(lVar11 + 0x70);
            uStack_58 = *(undefined8 *)(lVar11 + 0x78);
            uVar6 = *(undefined8 *)(*(longlong *)(lVar10 + 0x10) + 0x80);
            if ((*(longlong *)(param_1 + 0x20) != 0) &&
               (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x30), lVar11 != 0)) {
              uVar9 = sub_8046d800(lVar11,0);
              *(undefined1 *)(param_1 + 0x90) = 1;
              if (*(longlong *)(param_1 + 0x20) != 0) {
                PartsUnitSkillSlot::SetData(*(longlong *)(param_1 + 0x20),*(undefined8 *)(lVar10 + 0x10),uVar1,
                              uVar2,uVar3,uVar4,uVar5,uStack_58,uVar6,0);
                if (param_2 != '\0') {
                  UIProgressBar::set_value(lVar11,uVar9,0);
                }
                *(undefined1 *)(param_1 + 0x90) = 0;
                return;
              }
            }
          }
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar7 = (code *)swi(3);
  (*pcVar7)();
  return;
}



/* ==== UnitSkill::callSkillLevelUpApiFinishCallback  @ 0x14a72e0  size=1 ==== */

void UnitSkill::callSkillLevelUpApiFinishCallback(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  undefined8 uVar3;
  int iVar4;
  
  if (cRam000000018ab6257a == '\0') {
    sub_8032f6d0(0x187b68488);
    sub_8032f6d0(0x187b68b78);
    sub_8032f6d0(0x187b68d30);
    cRam000000018ab6257a = '\x01';
  }
  lVar1 = *(longlong *)(param_1 + 0x78);
  iVar4 = 0;
  do {
    if (lVar1 == 0) {
code_r0x0001814a739a:
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
    if (*(int *)(lVar1 + 0x18) <= iVar4) {
      lVar1 = *(longlong *)(param_1 + 0x78);
      if (lVar1 != 0) {
        iVar4 = *(int *)(lVar1 + 0x18);
        *(int *)(lVar1 + 0x1c) = *(int *)(lVar1 + 0x1c) + 1;
        *(undefined4 *)(lVar1 + 0x18) = 0;
        if (0 < iVar4) {
          Array::Clear(*(undefined8 *)(lVar1 + 0x10),0,iVar4,0);
        }
        return;
      }
      goto code_r0x0001814a739a;
    }
    if (lVar1 == 0) goto code_r0x0001814a739a;
    uVar3 = sub_849ed2d0(lVar1,iVar4,uRam0000000187b68d30);
    EventExtension::Call(uVar3,0);
    lVar1 = *(longlong *)(param_1 + 0x78);
    iVar4 = iVar4 + 1;
  } while( true );
}



/* ==== UnitSkill::createMasterData  @ 0x14a73a0  size=1 ==== */

void UnitSkill::createMasterData(longlong param_1,undefined8 param_2,undefined4 param_3)

{
  uint uVar1;
  longlong lVar2;
  code *pcVar3;
  undefined4 uVar4;
  longlong lVar5;
  longlong lVar6;
  undefined8 uVar7;
  longlong lVar8;
  undefined8 uStack_18;
  undefined4 uStack_10;
  
  if (cRam000000018ab62572 == '\0') {
    sub_8032f6d0(0x187c32d28);
    sub_8032f6d0(0x187c33248);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b92ff0);
    sub_8032f6d0(0x187b78350);
    sub_8032f6d0(0x187be3678);
    sub_8032f6d0(0x187c2ca48);
    cRam000000018ab62572 = '\x01';
  }
  lVar5 = sub_8032f970(uRam0000000187c2ca48);
  sub_80443e50(lVar5,0);
  if (lVar5 != 0) {
    *(undefined4 *)(lVar5 + 0x10) = param_3;
    lVar6 = sub_8032f970(uRam0000000187b78350);
    SkillLevelInfo::.ctor(lVar6,0);
    lVar8 = *(longlong *)(param_1 + 0x58);
    uVar7 = sub_8032f970(uRam0000000187b92ff0);
    sub_84cacb40(uVar7,lVar5,uRam0000000187be3678,0);
    if (lVar8 != 0) {
      lVar8 = sub_84a8c920(lVar8,uVar7,uRam0000000187c33248);
      if (lVar8 == 0) {
        if (lVar6 == 0) goto code_r0x0001814a75c2;
        SkillLevelInfo::SetSkillId(lVar6,*(undefined4 *)(lVar5 + 0x10),0);
        uVar4 = 0;
      }
      else {
        uStack_18 = *(undefined8 *)(lVar8 + 0x10);
        uVar4 = *(undefined4 *)(lVar8 + 0x18);
        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_10 = uVar4;
        uVar4 = ObscuredInt::FHOALPFLEEC(&uStack_18,0);
        if (lVar6 == 0) goto code_r0x0001814a75c2;
        SkillLevelInfo::SetSkillId(lVar6,uVar4,0);
        uStack_18 = *(undefined8 *)(lVar8 + 0x1c);
        uStack_10 = *(undefined4 *)(lVar8 + 0x24);
        uVar4 = ObscuredInt::FHOALPFLEEC(&uStack_18,0);
      }
      SkillLevelInfo::SetSkillLevel(lVar6,uVar4,0);
      lVar8 = lRam0000000187c32d28;
      lVar5 = *(longlong *)(param_1 + 0x60);
      if (lVar5 != 0) {
        *(int *)(lVar5 + 0x1c) = *(int *)(lVar5 + 0x1c) + 1;
        lVar2 = *(longlong *)(lVar5 + 0x10);
        uVar1 = *(uint *)(lVar5 + 0x18);
        if (lVar2 != 0) {
          if (uVar1 < *(uint *)(lVar2 + 0x18)) {
            *(uint *)(lVar5 + 0x18) = uVar1 + 1;
            if (*(uint *)(lVar2 + 0x18) <= uVar1) {
              sub_8032f9c0();
              pcVar3 = (code *)swi(3);
              (*pcVar3)();
              return;
            }
            *(longlong *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = lVar6;
            sub_8032e8d0(lVar2 + 0x20 + (longlong)(int)uVar1 * 8,lVar6);
          }
          else {
            sub_84a97470(lVar5,lVar6,
                          *(undefined8 *)(*(longlong *)(*(longlong *)(lVar8 + 0x20) + 0xc0) + 0x70))
            ;
          }
          return;
        }
      }
    }
  }
code_r0x0001814a75c2:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::get_AllSkillInfoList  @ 0x14a75d0  size=1 ==== */

longlong UnitSkill::get_AllSkillInfoList(longlong param_1)

{
  undefined4 uVar1;
  longlong lVar2;
  longlong lVar3;
  code *pcVar4;
  char cVar5;
  undefined4 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 uVar9;
  longlong lVar10;
  int iVar11;
  undefined8 uVar12;
  longlong alStackX_10 [2];
  undefined8 uStackX_20;
  undefined8 uStack_48;
  undefined4 uStack_40;
  
  lVar10 = *(longlong *)(param_1 + 0x68);
  if (lVar10 == 0) {
    sub_8032f9d0();
    pcVar4 = (code *)swi(3);
    lVar10 = (*pcVar4)();
    return lVar10;
  }
  lVar3 = *(longlong *)(param_1 + 0x60);
  if (cRam000000018ab6259c == '\0') {
    sub_8032f6d0(0x187b950f0,lVar3,0);
    sub_8032f6d0(0x187c346b8);
    sub_8032f6d0(0x187c34500);
    sub_8032f6d0(0x187c33918);
    sub_8032f6d0(0x187c33ac8);
    sub_8032f6d0(0x187c21248);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b786c8);
    cRam000000018ab6259c = '\x01';
  }
  iVar11 = 0;
  alStackX_10[0] = 0;
  lVar7 = sub_8032f970(uRam0000000187c21248);
  sub_849de5b0(lVar7,uRam0000000187c34500);
  if (lVar3 != 0) {
    while( true ) {
      if (*(int *)(lVar3 + 0x18) <= iVar11) {
        return lVar7;
      }
      lVar8 = sub_849ed2d0(lVar3,iVar11,uRam0000000187c33ac8);
      alStackX_10[0] = 0;
      uVar12 = 0x65;
      if (iVar11 == 1) {
        uVar12 = 0xc9;
      }
      else if (iVar11 == 2) {
        uVar12 = 0xca;
      }
      else if (iVar11 == 3) {
        uVar12 = 0x12d;
      }
      lVar2 = *(longlong *)(lVar10 + 0x10);
      if (lVar2 == 0) break;
      cVar5 = sub_855faac0(lVar2,uVar12,alStackX_10,uRam0000000187b950f0);
      if (cVar5 == '\0') {
        if (lVar8 == 0) break;
        uStackX_20 = *(undefined8 *)(lVar8 + 0x1c);
        uVar6 = *(undefined4 *)(lVar8 + 0x24);
        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_48 = uStackX_20;
        uStack_40 = uVar6;
        uVar6 = ObscuredInt::FHOALPFLEEC(&uStack_48,0);
        uVar9 = sub_8032f970(uRam0000000187b786c8);
        sub_805feae0(uVar9,uVar12,0,uVar6,0);
        if (lVar7 == 0) break;
        sub_80002460(lVar7,uVar9,uRam0000000187c346b8);
        iVar11 = iVar11 + 1;
      }
      else {
        if (alStackX_10[0] == 0) break;
        uVar6 = *(undefined4 *)(alStackX_10[0] + 0x14);
        uVar1 = *(undefined4 *)(alStackX_10[0] + 0x10);
        uVar9 = sub_8032f970(uRam0000000187b786c8);
        sub_805feae0(uVar9,uVar12,uVar6,uVar1,0);
        if (lVar7 == 0) break;
        sub_80002460(lVar7,uVar9,uRam0000000187c346b8);
        iVar11 = iVar11 + 1;
      }
    }
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  lVar10 = (*pcVar4)();
  return lVar10;
}



/* ==== UnitSkill::get_GetTutorialSkillButton  @ 0x14a7600  size=1 ==== */

undefined8 UnitSkill::get_GetTutorialSkillButton(longlong param_1)

{
  code *pcVar1;
  undefined8 uVar2;
  
  if (*(longlong *)(param_1 + 0x20) != 0) {
    return *(undefined8 *)(*(longlong *)(param_1 + 0x20) + 0xa0);
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar2 = (*pcVar1)();
  return uVar2;
}



/* ==== UnitSkill::get_SkillInfoList  @ 0x14a7620  size=1 ==== */

longlong UnitSkill::get_SkillInfoList(longlong param_1)

{
  undefined4 uVar1;
  undefined4 uVar2;
  uint uVar3;
  longlong lVar4;
  code *pcVar5;
  char cVar6;
  longlong lVar7;
  undefined8 uVar8;
  longlong lVar9;
  undefined8 uStack_88;
  undefined8 uStack_80;
  ulonglong uStack_78;
  longlong lStack_70;
  undefined8 uStack_68;
  ulonglong uStack_58;
  longlong lStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  ulonglong uStack_38;
  undefined *puStack_30;
  undefined8 uStack_28;
  
  lVar9 = *(longlong *)(param_1 + 0x68);
  if (lVar9 == 0) {
    puStack_30 = &UNK_1814a763d;
    sub_8032f9d0();
    pcVar5 = (code *)swi(3);
    lVar9 = (*pcVar5)();
    return lVar9;
  }
  if (cRam000000018ab6259b == '\0') {
    sub_8032f6d0(0x187b94f40,0);
    sub_8032f6d0(0x187b66f20);
    sub_8032f6d0(0x187b670d8);
    sub_8032f6d0(0x187b67298);
    sub_8032f6d0(0x187c23368);
    sub_8032f6d0(0x187c23518);
    sub_8032f6d0(0x187c346b8);
    sub_8032f6d0(0x187c34500);
    sub_8032f6d0(0x187c21248);
    sub_8032f6d0(0x187b786c8);
    cRam000000018ab6259b = '\x01';
  }
  lVar7 = sub_8032f970(uRam0000000187c21248);
  sub_849de5b0(lVar7,uRam0000000187c34500);
  lVar9 = *(longlong *)(lVar9 + 0x10);
  if (lVar9 == 0) {
    sub_8032f9d0();
code_r0x00018149352f:
    sub_8032f9c0();
code_r0x000181493535:
    sub_8032f9d0();
code_r0x00018149353a:
    sub_8032f9d0();
  }
  else {
    sub_855337a0(&uStack_48,lVar9,uRam0000000187b94f40);
    uStack_88 = uStack_48;
    uStack_80 = uStack_40;
    uStack_78 = uStack_38;
    lStack_70 = (longlong)puStack_30;
    uStack_68 = uStack_28;
    while( true ) {
      cVar6 = sub_85760f30(&uStack_88,uRam0000000187b670d8);
      if (cVar6 == '\0') {
        sub_80443e50(&uStack_88,uRam0000000187b66f20);
        return lVar7;
      }
      uStack_58 = uStack_78;
      lStack_50 = lStack_70;
      if (lStack_70 == 0) break;
      uVar1 = *(undefined4 *)(lStack_70 + 0x14);
      uVar2 = *(undefined4 *)(lStack_70 + 0x10);
      uVar8 = sub_8032f970(uRam0000000187b786c8);
      sub_805feae0(uVar8,uStack_58 & 0xffffffff,uVar1,uVar2,0);
      lVar9 = lRam0000000187c346b8;
      if (lVar7 == 0) goto code_r0x00018149353a;
      *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
      lVar4 = *(longlong *)(lVar7 + 0x10);
      uVar3 = *(uint *)(lVar7 + 0x18);
      if (lVar4 == 0) goto code_r0x000181493535;
      if (uVar3 < *(uint *)(lVar4 + 0x18)) {
        *(uint *)(lVar7 + 0x18) = uVar3 + 1;
        if (*(uint *)(lVar4 + 0x18) <= uVar3) goto code_r0x00018149352f;
        *(undefined8 *)(lVar4 + 0x20 + (longlong)(int)uVar3 * 8) = uVar8;
        sub_8032e8d0(lVar4 + (longlong)(int)uVar3 * 8 + 0x20,uVar8);
      }
      else {
        sub_84a97470(lVar7,uVar8,
                      *(undefined8 *)(*(longlong *)(*(longlong *)(lVar9 + 0x20) + 0xc0) + 0x70));
      }
    }
  }
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar5 = (code *)swi(3);
  lVar9 = (*pcVar5)();
  return lVar9;
}



/* ==== UnitSkill::get_connectRankTempData  @ 0x438c50  size=8 ==== */

undefined8 UnitSkill::get_connectRankTempData(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xa0);
}



/* ==== UnitSkill::onClickFreeEnhance  @ 0x14a7640  size=1 ==== */

void UnitSkill::onClickFreeEnhance(longlong param_1,longlong param_2)

{
  bool bVar1;
  code *pcVar2;
  char cVar3;
  undefined4 uVar4;
  longlong lVar5;
  longlong lVar6;
  undefined8 uVar7;
  int iVar8;
  longlong lStackX_20;
  
  if (cRam000000018ab62576 == '\0') {
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187b35d58);
    sub_8032f6d0(0x187b38b68);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187be39e8);
    sub_8032f6d0(0x187c2cdb0);
    sub_8032f6d0(0x187c05928);
    cRam000000018ab62576 = '\x01';
  }
  lStackX_20 = 0;
  lVar5 = sub_8032f970(uRam0000000187c2cdb0);
  sub_80443e50(lVar5,0);
  if (lVar5 != 0) {
    *(longlong *)(lVar5 + 0x18) = param_1;
    sub_8032e8d0(lVar5 + 0x18,param_1);
    if (param_2 != 0) {
      UnitSkill::updateSkillLevelInfo(param_1,*(undefined4 *)(param_2 + 0x14),*(undefined4 *)(param_2 + 0x18),0);
      lVar6 = *(longlong *)(lRam0000000187c16f00 + 0x20);
      if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
        lVar6 = sub_8032d010(lVar6);
      }
      lVar6 = *(longlong *)(*(longlong *)(lVar6 + 0xc0) + 0x10);
      if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
        lVar6 = sub_8032d010(lVar6);
      }
      if ((**(longlong **)(lVar6 + 0xb8) != 0) &&
         (lVar6 = *(longlong *)(**(longlong **)(lVar6 + 0xb8) + 0x90), lVar6 != 0)) {
        lVar6 = sub_855bdd60(lVar6,*(undefined4 *)(param_1 + 0x30),uRam0000000187b35d58);
        if (lVar6 != 0) {
          *(undefined8 *)(lVar5 + 0x20) = *(undefined8 *)(lVar6 + 0x10);
          sub_8032e8d0(lVar5 + 0x20);
          lVar6 = *(longlong *)(lRam0000000187b3c708 + 0x20);
          if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
            lVar6 = sub_8032d010(lVar6);
          }
          if ((*(byte *)(*(longlong *)(*(longlong *)(lVar6 + 0xc0) + 0x10) + 0x135) & 1) == 0) {
            sub_8032d010();
          }
          *(undefined8 *)(lVar5 + 0x10) = **(undefined8 **)(lRam000000018ab8e1a0 + 0xb8);
          sub_8032e8d0(lVar5 + 0x10);
          if (*(longlong *)(param_1 + 0x48) != 0) {
            cVar3 = UserData::IsActiveFreeParameter(*(longlong *)(param_1 + 0x48),*(undefined4 *)(param_1 + 0x30),0);
            if (cVar3 == '\0') {
              bVar1 = true;
            }
            else {
              iVar8 = *(int *)(param_2 + 0x1c) + *(int *)(param_2 + 0x18);
              uVar4 = *(undefined4 *)(param_2 + 0x14);
              if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uVar4 = UnitUtility::GetSkillIdConsiderOverride(uVar4,iVar8,0);
              lVar6 = *(longlong *)(lRam0000000187b3c708 + 0x20);
              if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
                lVar6 = sub_8032d010(lVar6);
              }
              lVar6 = *(longlong *)(*(longlong *)(lVar6 + 0xc0) + 0x10);
              if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
                lVar6 = sub_8032d010(lVar6);
              }
              if ((**(longlong **)(lVar6 + 0xb8) == 0) ||
                 (lVar6 = *(longlong *)(**(longlong **)(lVar6 + 0xb8) + 0x3d8), lVar6 == 0))
              goto code_r0x0001814a7afe;
              lVar6 = HOEABFDCBIG::BKEJKNOJCEE(lVar6,uVar4,0);
              if (lVar6 == 0) goto code_r0x0001814a7afe;
              uVar7 = OLNDAJKNIAG::HLGHEKAFNCA(lVar6,0);
              *(undefined8 *)(lVar5 + 0x10) = uVar7;
              sub_8032e8d0(lVar5 + 0x10,uVar7);
              if (*(longlong *)(param_1 + 0x48) == 0) goto code_r0x0001814a7afe;
              UserData::TryGetGrowthParameter(*(longlong *)(param_1 + 0x48),*(undefined4 *)(param_1 + 0x30),
                            &lStackX_20,0);
              if (lStackX_20 == 0) goto code_r0x0001814a7afe;
              bVar1 = *(int *)(lStackX_20 + 0x20) <= iVar8;
            }
            uVar4 = *(undefined4 *)(param_1 + 0x30);
            uVar7 = *(undefined8 *)(param_1 + 0xa0);
            if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
              sub_8032fa70();
            }
            UnitUtility::UpdateUnitNotificationSkillTargetUnit(uVar4,0,0,uVar7,0);
            UnitUtility::CheckSkillNoticeInfo(*(undefined8 *)(lVar5 + 0x20),0);
            EventExtension::Call(*(undefined8 *)(param_1 + 0x98),0);
            if (*(longlong *)(param_1 + 0x68) != 0) {
              SkillLevelUpTemporaryStorage::LevelUp(*(longlong *)(param_1 + 0x68),*(undefined4 *)(param_2 + 0x10),
                            *(undefined4 *)(param_2 + 0x1c),*(undefined4 *)(param_2 + 0x18),0);
              lVar6 = *(longlong *)(param_1 + 0x80);
              if (lVar6 != 0) {
                *(longlong *)(lVar6 + 0x28) = param_2;
                sub_8032e8d0(lVar6 + 0x28,param_2);
                if ((*(longlong *)(param_1 + 0x80) != 0) &&
                   (lVar6 = *(longlong *)(*(longlong *)(param_1 + 0x80) + 0x30), lVar6 != 0)) {
                  (**(code **)(lVar6 + 0x18))
                            (*(undefined8 *)(lVar6 + 0x40),*(undefined4 *)(param_1 + 0x30),
                             *(undefined8 *)(lVar6 + 0x28));
                  if ((bVar1) && (*(char *)(param_2 + 0x38) != '\0')) {
                    lVar6 = sub_80003d20(lRam0000000187b38b68);
                    if (lVar6 == 0) goto code_r0x0001814a7afe;
                    BlockLayerManager::SetBlockCommon(lVar6,1,0);
                    uVar7 = sub_8032f970(uRam0000000187c32f48);
                    sub_8044b290(uVar7,lVar5,uRam0000000187be39e8,0);
                    if (cRam000000018ab62577 == '\0') {
                      sub_8032f6d0(0x187c2d118);
                      cRam000000018ab62577 = '\x01';
                    }
                    lVar5 = sub_8032f970(uRam0000000187c2d118);
                    sub_80443e50(lVar5,0);
                    *(undefined4 *)(lVar5 + 0x10) = 0;
                    *(longlong *)(lVar5 + 0x20) = param_1;
                    sub_8032e8d0(lVar5 + 0x20,param_1);
                    *(undefined8 *)(lVar5 + 0x28) = uVar7;
                    sub_8032e8d0(lVar5 + 0x28,uVar7);
                    sub_86209420(param_1,lVar5,0);
                  }
                  else {
                    lVar5 = *(longlong *)(lRam0000000187b38b68 + 0x20);
                    if ((*(byte *)(lVar5 + 0x135) & 1) == 0) {
                      lVar5 = sub_8032d010(lVar5);
                    }
                    lVar5 = *(longlong *)(*(longlong *)(lVar5 + 0xc0) + 0x10);
                    if ((*(byte *)(lVar5 + 0x135) & 1) == 0) {
                      lVar5 = sub_8032d010(lVar5);
                    }
                    if (**(longlong **)(lVar5 + 0xb8) == 0) goto code_r0x0001814a7afe;
                    BlockLayerManager::SetBlockCommon(**(longlong **)(lVar5 + 0xb8),0,0);
                    UnitSkill::UpdateSkillSet(param_1,1,0);
                  }
                  return;
                }
              }
            }
          }
        }
      }
    }
  }
code_r0x0001814a7afe:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitSkill::onClickSetButton  @ 0x14a7b10  size=1 ==== */

void UnitSkill::onClickSetButton(longlong param_1,undefined4 param_2)

{
  undefined4 uVar1;
  longlong lVar2;
  code *pcVar3;
  undefined8 uVar4;
  
  if (cRam000000018ab62573 == '\0') {
    sub_8032f6d0(0x187b6c3e8);
    sub_8032f6d0(0x187b392e8);
    cRam000000018ab62573 = '\x01';
  }
  lVar2 = *(longlong *)(param_1 + 0x38);
  uVar1 = *(undefined4 *)(param_1 + 0x30);
  *(undefined4 *)(param_1 + 0x34) = param_2;
  uVar4 = sub_8032f970(uRam0000000187b6c3e8);
  sub_856f10f0(uVar4,param_1,uRam0000000187b392e8,0);
  if (lVar2 != 0) {
    DialogManager::OpenFreeSkillSelect(lVar2,uVar1,param_2,uVar4,0);
    return;
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::onClickSkillEnhance  @ 0x14a7bd0  size=1 ==== */

void UnitSkill::onClickSkillEnhance(longlong param_1,longlong param_2)

{
  undefined4 uVar1;
  int iVar2;
  code *pcVar3;
  char cVar4;
  int iVar5;
  undefined8 uVar6;
  longlong lVar7;
  
  if (cRam000000018ab62575 == '\0') {
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187b35d58);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187be9a60);
    sub_8032f6d0(0x187b38a60);
    sub_8032f6d0(0x187c05928);
    cRam000000018ab62575 = '\x01';
  }
  if ((param_2 == 0) || (*(longlong *)(param_1 + 0x68) == 0)) goto code_r0x0001814a7e9d;
  SkillLevelUpTemporaryStorage::LevelUp(*(longlong *)(param_1 + 0x68),*(undefined4 *)(param_2 + 0x10),
                *(undefined4 *)(param_2 + 0x1c),*(undefined4 *)(param_2 + 0x18),0);
  if (*(int *)(lRam0000000187be9a60 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar4 = TutorialManager::get_IsSkillEnhanceTutorial(0);
  if (cVar4 == '\0') {
    lVar7 = *(longlong *)(param_1 + 0x48);
    if (lVar7 == 0) goto code_r0x0001814a7e9d;
    *(int *)(lVar7 + 0x6c0) = *(int *)(lVar7 + 0x6c0) + *(int *)(param_2 + 0x20);
    lVar7 = *(longlong *)(param_1 + 0x50);
    if (*(longlong *)(param_1 + 0x48) == 0) goto code_r0x0001814a7e9d;
    iVar5 = UserData::get_TotalGold(*(longlong *)(param_1 + 0x48),0);
    if ((*(longlong *)(param_1 + 0x48) == 0) || (lVar7 == 0)) goto code_r0x0001814a7e9d;
    CustomUILabel::SetTextCurrency(lVar7,(longlong)(iVar5 - *(int *)(*(longlong *)(param_1 + 0x48) + 0x6c0)),0);
    UnitSkill::updateSkillLevelInfo(param_1,*(undefined4 *)(param_2 + 0x14),*(undefined4 *)(param_2 + 0x18),0);
    UnitSkill::UpdateSkillSet(param_1,1,0);
  }
  else {
    UnitSkill::updateSkillLevelInfo(param_1,*(undefined4 *)(param_2 + 0x14),*(undefined4 *)(param_2 + 0x18),0);
    uVar6 = sub_8032f970(uRam0000000187c32f48);
    sub_8044b290(uVar6,param_1,uRam0000000187b38a60,0);
    UnitSkill::ConnectSkillLevelUpApi(param_1,uVar6,0);
  }
  uVar1 = *(undefined4 *)(param_1 + 0x30);
  if (*(longlong *)(param_1 + 0x48) != 0) {
    iVar5 = UserData::get_TotalGold(*(longlong *)(param_1 + 0x48),0);
    if (*(longlong *)(param_1 + 0x48) != 0) {
      iVar2 = *(int *)(*(longlong *)(param_1 + 0x48) + 0x6c0);
      uVar6 = *(undefined8 *)(param_1 + 0xa0);
      if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
        sub_8032fa70();
      }
      UnitUtility::UpdateUnitNotificationSkillTargetUnit(uVar1,iVar5 - iVar2,0,uVar6,0);
      lVar7 = *(longlong *)(lRam0000000187c16f00 + 0x20);
      if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
        lVar7 = sub_8032d010(lVar7);
      }
      lVar7 = *(longlong *)(*(longlong *)(lVar7 + 0xc0) + 0x10);
      if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
        lVar7 = sub_8032d010(lVar7);
      }
      if ((**(longlong **)(lVar7 + 0xb8) != 0) &&
         (lVar7 = *(longlong *)(**(longlong **)(lVar7 + 0xb8) + 0x90), lVar7 != 0)) {
        lVar7 = sub_855bdd60(lVar7,*(undefined4 *)(param_1 + 0x30),uRam0000000187b35d58);
        if (lVar7 != 0) {
          UnitUtility::CheckSkillNoticeInfo(*(undefined8 *)(lVar7 + 0x10),0);
          EventExtension::Call(*(undefined8 *)(param_1 + 0x98),0);
          lVar7 = *(longlong *)(param_1 + 0x80);
          if (lVar7 != 0) {
            *(longlong *)(lVar7 + 0x28) = param_2;
            sub_8032e8d0(lVar7 + 0x28,param_2);
            if ((*(longlong *)(param_1 + 0x80) != 0) &&
               (lVar7 = *(longlong *)(*(longlong *)(param_1 + 0x80) + 0x30), lVar7 != 0)) {
                    /* WARNING: Could not recover jumptable at 0x0001814a7e99. Too many branches */
                    /* WARNING: Treating indirect jump as call */
              (**(code **)(lVar7 + 0x18))
                        (*(undefined8 *)(lVar7 + 0x40),*(undefined4 *)(param_1 + 0x30),
                         *(undefined8 *)(lVar7 + 0x28));
              return;
            }
          }
        }
      }
    }
  }
code_r0x0001814a7e9d:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::onFreeSkillSelect  @ 0x14a7eb0  size=1 ==== */

void UnitSkill::onFreeSkillSelect(longlong param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
  uint uVar1;
  longlong lVar2;
  code *pcVar3;
  longlong lVar4;
  longlong lVar5;
  undefined8 uVar6;
  int iVar7;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab62574 == '\0') {
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187be3830);
    sub_8032f6d0(0x187c2cc00);
    cRam000000018ab62574 = '\x01';
  }
  lVar5 = sub_8032f970(uRam0000000187c2cc00);
  sub_80443e50(lVar5,0);
  if (lVar5 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  *(undefined4 *)(lVar5 + 0x10) = param_4;
  *(longlong *)(lVar5 + 0x18) = param_1;
  sub_8032e8d0(lVar5 + 0x18,param_1);
  *(undefined4 *)(lVar5 + 0x20) = param_2;
  *(undefined4 *)(lVar5 + 0x24) = param_3;
  uVar6 = sub_8032f970(uRam0000000187c32f48);
  sub_8044b290(uVar6,lVar5,uRam0000000187be3830,0);
  if (cRam000000018ab62579 == '\0') {
    sub_8032f6d0(0x187c32f48,uVar6,0);
    sub_8032f6d0(0x187b682c8);
    sub_8032f6d0(0x187c34f38);
    sub_8032f6d0(0x187b382e8);
    sub_8032f6d0(0x187b388b0);
    cRam000000018ab62579 = '\x01';
  }
  lVar4 = lRam0000000187b682c8;
  lVar5 = *(longlong *)(param_1 + 0x78);
  if (lVar5 != 0) {
    *(int *)(lVar5 + 0x1c) = *(int *)(lVar5 + 0x1c) + 1;
    lVar2 = *(longlong *)(lVar5 + 0x10);
    uVar1 = *(uint *)(lVar5 + 0x18);
    if (lVar2 != 0) {
      if (uVar1 < *(uint *)(lVar2 + 0x18)) {
        *(uint *)(lVar5 + 0x18) = uVar1 + 1;
        if (*(uint *)(lVar2 + 0x18) <= uVar1) {
          sub_8032f9c0();
          pcVar3 = (code *)swi(3);
          (*pcVar3)();
          return;
        }
        *(undefined8 *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = uVar6;
        sub_8032e8d0(lVar2 + 0x20 + (longlong)(int)uVar1 * 8);
      }
      else {
        sub_84a97470(lVar5,uVar6,
                      *(undefined8 *)(*(longlong *)(*(longlong *)(lVar4 + 0x20) + 0xc0) + 0x70));
      }
      if (*(char *)(param_1 + 0x70) != '\0') {
        return;
      }
      if ((*(longlong *)(param_1 + 0x68) != 0) && (lVar5 = SkillLevelUpTemporaryStorage::MakeProcessingInfoList(), lVar5 != 0)) {
        if (*(int *)(lVar5 + 0x18) < 1) {
          if (cRam000000018ab6257a == '\0') {
            sub_8032f6d0(0x187b68488);
            sub_8032f6d0(0x187b68b78);
            sub_8032f6d0(0x187b68d30);
            cRam000000018ab6257a = '\x01';
          }
          lVar5 = *(longlong *)(param_1 + 0x78);
          iVar7 = 0;
          do {
            if (lVar5 == 0) {
code_r0x0001814a739a:
              sub_8032f9d0();
              pcVar3 = (code *)swi(3);
              (*pcVar3)();
              return;
            }
            if (*(int *)(lVar5 + 0x18) <= iVar7) {
              lVar5 = *(longlong *)(param_1 + 0x78);
              if (lVar5 != 0) {
                iVar7 = *(int *)(lVar5 + 0x18);
                *(int *)(lVar5 + 0x1c) = *(int *)(lVar5 + 0x1c) + 1;
                *(undefined4 *)(lVar5 + 0x18) = 0;
                if (0 < iVar7) {
                  Array::Clear(*(undefined8 *)(lVar5 + 0x10),0,iVar7,0,unaff_RDI);
                }
                return;
              }
              goto code_r0x0001814a739a;
            }
            if (lVar5 == 0) goto code_r0x0001814a739a;
            uVar6 = sub_849ed2d0(lVar5,iVar7,uRam0000000187b68d30);
            EventExtension::Call(uVar6,0);
            lVar5 = *(longlong *)(param_1 + 0x78);
            iVar7 = iVar7 + 1;
          } while( true );
        }
        *(undefined1 *)(param_1 + 0x70) = 1;
        lVar4 = sub_80003d20(uRam0000000187b382e8);
        if (lVar4 != 0) {
          ApiManager::AddSkillLevelUpPostParam(lVar4,*(undefined4 *)(param_1 + 0x30),lVar5,0,0);
          uVar6 = sub_8032f970(uRam0000000187c32f48);
          sub_8044b290(uVar6,param_1,uRam0000000187b388b0,0);
          ApiManager::Exec(lVar4,uVar6,0,0,0);
          return;
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::onRequestSetFree  @ 0x14a7fa0  size=1 ==== */

void UnitSkill::onRequestSetFree(longlong param_1)

{
  UnitSkill::Initialize(param_1,*(undefined4 *)(param_1 + 0x30),*(undefined8 *)(param_1 + 0xa0),
                *(undefined8 *)(param_1 + 0x98),0,0,0);
  return;
}



/* ==== UnitSkill::onScrollValueChange  @ 0x14a7fd0  size=1 ==== */

void UnitSkill::onScrollValueChange(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  undefined8 uVar5;
  
  if (*(char *)(param_1 + 0x90) != '\0') {
    return;
  }
  lVar1 = *(longlong *)(param_1 + 0x28);
  if (lVar1 == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  if (cRam000000018ab62423 == '\0') {
    sub_8032f6d0(0x187b5e978,0);
    sub_8032f6d0(0x187b8f5a8);
    sub_8032f6d0(0x187b8fac8);
    cRam000000018ab62423 = '\x01';
  }
  lVar4 = *(longlong *)(lVar1 + 0x30);
  *(undefined4 *)(lVar1 + 0x28) = 0;
  while (lVar4 != 0) {
    if (*(int *)(lVar4 + 0x20) < 1) {
      return;
    }
    if (*(longlong *)(lVar1 + 0x30) == 0) break;
    lVar4 = sub_84cda610(*(longlong *)(lVar1 + 0x30),uRam0000000187b8f5a8);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar3 = Object::op_Inequality(lVar4,0,0);
    if (cVar3 != '\0') {
      if (lVar4 == 0) break;
      uVar5 = Component::get_gameObject(lVar4,0);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      Object::Destroy(uVar5,0);
    }
    lVar4 = *(longlong *)(lVar1 + 0x30);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitSkill::set_connectRankTempData  @ 0x438e80  size=19 ==== */

void UnitSkill::set_connectRankTempData(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xa0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xa0U >> 0xc);
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



/* ==== UnitSkill::skillUpAnimationChangeUnitMotion  @ 0x14a8000  size=1 ==== */

void UnitSkill::skillUpAnimationChangeUnitMotion(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  
  lVar1 = *(longlong *)(param_1 + 0x80);
  if ((lVar1 != 0) && (*(longlong *)(lVar1 + 0x18) != 0)) {
    cVar3 = sub_85a26dd0(*(undefined8 *)(*(longlong *)(lVar1 + 0x18) + 0x130),
                                *(undefined8 *)(lVar1 + 0x20),0);
    if (cVar3 == '\0') {
      return;
    }
    if (*(longlong *)(lVar1 + 0x18) != 0) {
      UnitSpineController::PlayAnime(*(longlong *)(lVar1 + 0x18),7,0);
      if (*(longlong *)(lVar1 + 0x18) != 0) {
        UnitSpineController::EntryAnime(*(longlong *)(lVar1 + 0x18),1,1,0,0);
        return;
      }
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitSkill::skillUpAnimationPlayEffect  @ 0x14a8080  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001814a8121: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814a827d: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x0001814a8126) */
/* WARNING: Removing unreachable block (ram,0x0001814a8147) */
/* WARNING: Removing unreachable block (ram,0x0001814a8154) */
/* WARNING: Removing unreachable block (ram,0x0001814a8165) */
/* WARNING: Removing unreachable block (ram,0x0001814a8172) */
/* WARNING: Removing unreachable block (ram,0x0001814a8349) */
/* WARNING: Removing unreachable block (ram,0x0001814a817c) */
/* WARNING: Removing unreachable block (ram,0x0001814a8189) */
/* WARNING: Removing unreachable block (ram,0x0001814a8196) */
/* WARNING: Removing unreachable block (ram,0x0001814a81b5) */
/* WARNING: Removing unreachable block (ram,0x0001814a81bd) */
/* WARNING: Removing unreachable block (ram,0x0001814a81d1) */
/* WARNING: Removing unreachable block (ram,0x0001814a81d9) */
/* WARNING: Removing unreachable block (ram,0x0001814a81ec) */
/* WARNING: Removing unreachable block (ram,0x0001814a81fc) */
/* WARNING: Removing unreachable block (ram,0x0001814a8214) */
/* WARNING: Removing unreachable block (ram,0x0001814a8221) */
/* WARNING: Removing unreachable block (ram,0x0001814a8244) */
/* WARNING: Removing unreachable block (ram,0x0001814a8251) */
/* WARNING: Removing unreachable block (ram,0x0001814a8282) */
/* WARNING: Removing unreachable block (ram,0x0001814a828f) */
/* WARNING: Removing unreachable block (ram,0x0001814a82a8) */
/* WARNING: Removing unreachable block (ram,0x0001814a82b5) */
/* WARNING: Removing unreachable block (ram,0x0001814a82c2) */
/* WARNING: Removing unreachable block (ram,0x0001814a82e2) */
/* WARNING: Removing unreachable block (ram,0x0001814a82eb) */
/* WARNING: Removing unreachable block (ram,0x0001814a8319) */
/* WARNING: Removing unreachable block (ram,0x000183e63ca0) */
/* WARNING: Removing unreachable block (ram,0x000183e63d21) */
/* WARNING: Removing unreachable block (ram,0x000183a23ad0) */
/* WARNING: Removing unreachable block (ram,0x000183a23ae4) */
/* WARNING: Removing unreachable block (ram,0x000183a23ad8) */
/* WARNING: Removing unreachable block (ram,0x000183e63cb2) */
/* WARNING: Removing unreachable block (ram,0x000183e63cbb) */
/* WARNING: Removing unreachable block (ram,0x000183e63cc3) */
/* WARNING: Removing unreachable block (ram,0x000183e63cd3) */
/* WARNING: Removing unreachable block (ram,0x000183e63ce0) */
/* WARNING: Removing unreachable block (ram,0x000183e63d2f) */
/* WARNING: Removing unreachable block (ram,0x000183e63ce5) */
/* WARNING: Removing unreachable block (ram,0x000183e63d34) */
/* WARNING: Removing unreachable block (ram,0x000183e63ceb) */
/* WARNING: Removing unreachable block (ram,0x000183e63d3a) */
/* WARNING: Removing unreachable block (ram,0x000183e63cf8) */
/* WARNING: Removing unreachable block (ram,0x000183e63d04) */
/* WARNING: Removing unreachable block (ram,0x000183e63cfe) */

ulonglong UnitSkill::skillUpAnimationPlayEffect(undefined8 param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  code *pcVar3;
  longlong lVar4;
  ulonglong uVar5;
  uint uVar6;
  bool bVar7;
  
  if (cRam000000018ab6257b == '\0') {
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187b35d58);
    sub_8032f6d0(0x187c4d368);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187be3ba0);
    sub_8032f6d0(0x187c2cf68);
    cRam000000018ab6257b = '\x01';
  }
  lVar4 = sub_8032f970(uRam0000000187c2cf68);
  uVar5 = sub_80443e50(lVar4,0);
  if (lVar4 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar5 = (*pcVar3)();
    return uVar5;
  }
  *(undefined8 *)(lVar4 + 0x18) = param_1;
  if (iRam000000018ab8fbd8 != 0) {
    uVar6 = (uint)(lVar4 + 0x18U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar6 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar2 = *puVar1;
      LOCK();
      uVar5 = *puVar1;
      bVar7 = uVar2 == uVar5;
      if (bVar7) {
        *puVar1 = uVar2 | 1L << (uVar6 & 0x3f);
        uVar5 = uVar2;
      }
      UNLOCK();
    } while (!bVar7);
  }
  return uVar5;
}



/* ==== UnitSkill::skillUpAnimationStopAllEffect  @ 0x14a8350  size=1 ==== */

void UnitSkill::skillUpAnimationStopAllEffect(longlong param_1)

{
  longlong lVar1;
  longlong lVar2;
  undefined8 uVar3;
  longlong lVar4;
  code *pcVar5;
  char cVar6;
  uint uVar7;
  
  if ((*(longlong *)(param_1 + 0x80) == 0) ||
     (lVar4 = *(longlong *)(*(longlong *)(param_1 + 0x80) + 0x10), lVar4 == 0)) {
    sub_8032f9d0();
    pcVar5 = (code *)swi(3);
    (*pcVar5)();
    return;
  }
  UnitDetailEffectManager::Stop(lVar4,4);
  lVar1 = *(longlong *)(lVar4 + 0x40);
  if (lVar1 != 0) {
    if (*(uint *)(lVar1 + 0x18) < 6) {
code_r0x000181498c76:
      sub_8032f9c0();
      pcVar5 = (code *)swi(3);
      (*pcVar5)();
      return;
    }
    if (*(longlong *)(lVar1 + 0x48) != 0) {
      lVar1 = *(longlong *)(*(longlong *)(lVar1 + 0x48) + 0x28);
      uVar7 = 0;
      if (lVar1 != 0) {
        while( true ) {
          if (*(int *)(lVar1 + 0x18) <= (int)uVar7) {
            return;
          }
          if (*(uint *)(lVar1 + 0x18) <= uVar7) break;
          lVar2 = *(longlong *)(lVar1 + 0x20 + (longlong)(int)uVar7 * 8);
          if (lVar2 == 0) goto code_r0x000181498c7c;
          uVar3 = *(undefined8 *)(lVar2 + 0x10);
          if (cRam000000018ab625a7 == '\0') {
            sub_8032f6d0(0x187b5e978);
            cRam000000018ab625a7 = '\x01';
          }
          if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
            sub_8032fa70();
          }
          cVar6 = Object::op_Inequality(uVar3,0,0);
          if (cVar6 != '\0') {
            UnitDetailEffectManager::initEffect(lVar4);
          }
          uVar7 = uVar7 + 1;
        }
        goto code_r0x000181498c76;
      }
    }
  }
code_r0x000181498c7c:
  sub_8032f9d0();
  pcVar5 = (code *)swi(3);
  (*pcVar5)();
  return;
}



/* ==== UnitSkill::updateGoldLabel  @ 0x14a83a0  size=1 ==== */

void UnitSkill::updateGoldLabel(longlong param_1)

{
  longlong *plVar1;
  longlong lVar2;
  code *pcVar3;
  char cVar4;
  int iVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  longlong lVar8;
  undefined4 uStack_18;
  undefined4 uStack_14;
  undefined4 uStack_10;
  undefined4 uStack_c;
  
  plVar1 = *(longlong **)(param_1 + 0x50);
  if (*(longlong *)(param_1 + 0x48) != 0) {
    iVar5 = UserData::get_TotalGold(*(longlong *)(param_1 + 0x48),0);
    if ((*(longlong *)(param_1 + 0x48) != 0) && (plVar1 != (longlong *)0x0)) {
      lVar8 = (longlong)(iVar5 - *(int *)(*(longlong *)(param_1 + 0x48) + 0x6c0));
      if (cRam000000018ab7b539 == '\0') {
        sub_8032f6d0(0x187c2ac60,lVar8,0);
        sub_8032f6d0(0x187c38198);
        cRam000000018ab7b539 = '\x01';
      }
      if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar6 = ExtensionMethods::ToStringCurrency(lVar8,0);
      uVar7 = sub_80003600(uRam0000000187c2ac60);
      if (cRam000000018ab7b535 == '\0') {
        sub_8032f6d0(0x187bef030,uVar6,uVar7,0);
        cRam000000018ab7b535 = '\x01';
      }
      lVar8 = String::Format(uVar6,uVar7,0);
      if (lVar8 != 0) {
        iVar5 = String::IndexOf(lVar8,uRam0000000187bef030,0);
        if (-1 < iVar5) {
          uStack_c = *(undefined4 *)((longlong)plVar1 + 0xa4);
          uStack_18 = 0x3f800000;
          uStack_14 = 0x3f800000;
          uStack_10 = 0x3f800000;
          UIWidget::set_color(plVar1,&uStack_18,0);
        }
        uVar6 = (**(code **)(*plVar1 + 0x3a8))(plVar1,*(undefined8 *)(*plVar1 + 0x3b0));
        cVar4 = String::op_Inequality(uVar6,lVar8,0);
        lVar2 = *plVar1;
        if (cVar4 == '\0') {
          (**(code **)(lVar2 + 0x328))(plVar1,*(undefined8 *)(lVar2 + 0x330));
        }
        else {
          (**(code **)(lVar2 + 0x3b8))(plVar1,lVar8,*(undefined8 *)(lVar2 + 0x3c0));
        }
        *(undefined1 *)(plVar1 + 0x53) = 1;
        return;
      }
      sub_8032f9d0();
      pcVar3 = (code *)swi(3);
      (*pcVar3)();
      return;
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::updateSkillLevelInfo  @ 0x14a8400  size=1 ==== */

void UnitSkill::updateSkillLevelInfo(longlong param_1,int param_2,int param_3)

{
  undefined8 uVar1;
  undefined4 uVar2;
  code *pcVar3;
  int iVar4;
  longlong lVar5;
  int iVar6;
  undefined8 uStack_28;
  undefined4 uStack_20;
  
  if (cRam000000018ab62578 == '\0') {
    sub_8032f6d0(0x187c33918);
    sub_8032f6d0(0x187c33ac8);
    sub_8032f6d0(0x187b60fa0);
    cRam000000018ab62578 = '\x01';
  }
  iVar6 = 0;
  lVar5 = *(longlong *)(param_1 + 0x58);
  while (lVar5 != 0) {
    if (*(int *)(lVar5 + 0x18) <= iVar6) {
      return;
    }
    if ((*(longlong *)(param_1 + 0x58) == 0) ||
       (lVar5 = sub_849ed2d0(*(longlong *)(param_1 + 0x58),iVar6,uRam0000000187c33ac8), lVar5 == 0)
       ) break;
    uVar1 = *(undefined8 *)(lVar5 + 0x10);
    uVar2 = *(undefined4 *)(lVar5 + 0x18);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_28 = uVar1;
    uStack_20 = uVar2;
    iVar4 = ObscuredInt::FHOALPFLEEC(&uStack_28,0);
    if (iVar4 == param_2) {
      uVar1 = *(undefined8 *)(lVar5 + 0x1c);
      uVar2 = *(undefined4 *)(lVar5 + 0x24);
      if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_28 = uVar1;
      uStack_20 = uVar2;
      iVar6 = ObscuredInt::FHOALPFLEEC(&uStack_28,0);
      SkillLevelInfo::SetSkillLevel(lVar5,iVar6 + param_3,0);
      return;
    }
    iVar6 = iVar6 + 1;
    lVar5 = *(longlong *)(param_1 + 0x58);
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitSkill::waitFinishSkillupEffect  @ 0x14a8560  size=1 ==== */

longlong UnitSkill::waitFinishSkillupEffect(undefined8 param_1,undefined8 param_2)

{
  longlong lVar1;
  
  if (cRam000000018ab62577 == '\0') {
    sub_8032f6d0(0x187c2d118);
    cRam000000018ab62577 = '\x01';
  }
  lVar1 = sub_8032f970(uRam0000000187c2d118);
  sub_80443e50(lVar1,0);
  *(undefined4 *)(lVar1 + 0x10) = 0;
  *(undefined8 *)(lVar1 + 0x20) = param_1;
  sub_8032e8d0(lVar1 + 0x20,param_1);
  *(undefined8 *)(lVar1 + 0x28) = param_2;
  sub_8032e8d0(lVar1 + 0x28,param_2);
  return lVar1;
}



