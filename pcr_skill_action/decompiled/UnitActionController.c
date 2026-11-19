/* ==== UnitActionController::.ctor  @ 0xfa9350  size=11 ==== */

void UnitActionController::_ctor(longlong param_1)

{
  *(undefined1 *)(param_1 + 0x30) = 1;
  if (cRam000000018ab8a5bc == '\0') {
    sub_8032f6d0(0x187b5e978,0);
    cRam000000018ab8a5bc = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  return;
}



/* ==== UnitActionController::<searchTargetUnit>b__121_4  @ 0x14c3d40  size=1 ==== */

bool UnitActionController::<searchTargetUnit>b__121_4(longlong param_1,longlong param_2,undefined8 param_3,undefined8 param_4)

{
  longlong lVar1;
  code *pcVar2;
  undefined1 uVar3;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab626d7 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab626d7 = '\x01';
  }
  lVar1 = *(longlong *)(param_1 + 0x90);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (cRam000000018ab8a7f6 == '\0') {
    sub_8032f6d0(0x187b5e978,lVar1,0,param_4,unaff_RDI);
    cRam000000018ab8a7f6 = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b5e978);
  }
  if (cRam000000018ab8a7d0 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab8a7d0 = '\x01';
  }
  if (lVar1 == 0 && param_2 == 0) {
    return false;
  }
  if (lVar1 != 0) {
    if (param_2 != 0) {
      return param_2 != lVar1;
    }
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70(lRam0000000187b5e978);
    }
    return *(longlong *)(lVar1 + 0x10) != 0;
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b5e978);
  }
  if (param_2 != 0) {
    return *(longlong *)(param_2 + 0x10) != 0;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return (bool)uVar3;
}



/* ==== UnitActionController::<searchTargetUnit>b__121_5  @ 0x14c3da0  size=1 ==== */

bool UnitActionController::<searchTargetUnit>b__121_5(longlong param_1,longlong param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  char cVar3;
  undefined1 uVar4;
  
  if (cRam000000018ab626d8 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab626d8 = '\x01';
  }
  uVar1 = *(undefined8 *)(param_1 + 0x90);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = Object::op_Inequality(param_2,uVar1,0);
  if (cVar3 == '\0') {
    return false;
  }
  if (param_2 != 0) {
    return *(int *)(param_2 + 0x4b0) == 1;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar4 = (*pcVar2)();
  return (bool)uVar4;
}



/* ==== UnitActionController::AddBlackoutTarget  @ 0x14bb540  size=1 ==== */

void UnitActionController::AddBlackoutTarget(longlong param_1,undefined4 param_2)

{
  char cVar1;
  int iVar2;
  int iVar3;
  undefined8 uVar4;
  code *pcVar5;
  undefined4 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 *puVar9;
  longlong lVar10;
  undefined8 uVar11;
  longlong *plVar12;
  longlong lVar13;
  int iVar14;
  int iVar15;
  int iVar16;
  double dVar17;
  float fVar18;
  undefined1 uStackX_8;
  undefined1 uStackX_10;
  ulonglong in_stack_fffffffffffffec8;
  int iStack_118;
  float fStack_108;
  float fStack_104;
  undefined8 uStack_d8;
  undefined4 uStack_d0;
  undefined8 uStack_c8;
  float fStack_c0;
  undefined1 auStack_b8 [16];
  undefined1 auStack_a8 [16];
  undefined1 auStack_98 [16];
  undefined1 auStack_88 [16];
  undefined1 auStack_78 [64];
  
  if (cRam000000018ab626a9 == '\0') {
    sub_8032f6d0(0x187b40048);
    sub_8032f6d0(0x187c421b8);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b446b8);
    cRam000000018ab626a9 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar7 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    iStack_118 = 0;
    if ((lVar7 != 0) && (*(longlong *)(lVar7 + 0x20) != 0)) {
      iVar2 = *(int *)(*(longlong *)(lVar7 + 0x20) + 0x18);
      if (0 < iVar2) {
        do {
          if ((*(longlong *)(lVar7 + 0x20) == 0) ||
             (lVar8 = sub_849ed2d0(*(longlong *)(lVar7 + 0x20),iStack_118,uRam0000000187c19e18),
             lVar8 == 0)) goto code_r0x0001814bbc5c;
          if ((*(char *)(lVar8 + 0x86) == '\0') && (*(char *)(lVar8 + 0x84) == '\0')) {
            if (*(longlong *)(param_1 + 0x20) == 0) goto code_r0x0001814bbc5c;
            puVar9 = (undefined8 *)FixedTransform::get_position(auStack_b8,*(longlong *)(param_1 + 0x20),0);
            in_stack_fffffffffffffec8 = CONCAT71((int7)(in_stack_fffffffffffffec8 >> 8),1);
            uStack_d8 = *puVar9;
            uStack_d0 = *(undefined4 *)(puVar9 + 1);
            UnitActionController::searchAndSortTarget(param_1,lVar7,lVar8,&uStack_d8,in_stack_fffffffffffffec8,1,0);
            iVar15 = 0;
            while( true ) {
              if (*(int *)(lVar8 + 0x1c) <= iVar15) break;
              lVar10 = *(longlong *)(lVar8 + 0x50);
              if (lVar10 == 0) goto code_r0x0001814bbc5c;
              if (iVar15 < *(int *)(lVar10 + 0x18)) {
                lVar10 = sub_849ed2d0(lVar10,iVar15,uRam0000000187b812d0);
                if (lVar10 == 0) goto code_r0x0001814bbc5c;
                in_stack_fffffffffffffec8 = *(ulonglong *)(lVar10 + 0x68);
                if (*(int *)(lVar8 + 0x10) == 1) {
                  if ((in_stack_fffffffffffffec8 == 0) ||
                     (*(longlong *)(in_stack_fffffffffffffec8 + 0xb0) == 0))
                  goto code_r0x0001814bbc5c;
                  sub_804f1ed0(*(longlong *)(in_stack_fffffffffffffec8 + 0xb0),1,0);
                }
                lVar10 = sub_814a9080(param_1,0);
                uVar4 = *(undefined8 *)(param_1 + 0x90);
                if ((*(longlong *)(lVar8 + 0x50) == 0) ||
                   (uVar11 = sub_849ed2d0(*(longlong *)(lVar8 + 0x50),iVar15,uRam0000000187b812d0),
                   lVar10 == 0)) goto code_r0x0001814bbc5c;
                sub_80012050(2,uRam0000000187b93630,lVar10,uVar4,in_stack_fffffffffffffec8,uVar11);
                if (*(int *)(lVar8 + 0x60) == 7) {
                  iVar16 = *(int *)(lVar8 + 0x2c);
                  fVar18 = 0.0;
                  uStackX_8 = 1;
                  uStackX_10 = 0;
                  if (iVar16 == 1) {
                    if ((*(longlong *)(lVar8 + 0x50) == 0) ||
                       (plVar12 = (longlong *)
                                  sub_849ed2d0(*(longlong *)(lVar8 + 0x50),0,uRam0000000187b812d0),
                       plVar12 == (longlong *)0x0)) goto code_r0x0001814bbc5c;
                    puVar9 = (undefined8 *)(**(code **)(*plVar12 + 0x178))(auStack_78,plVar12);
code_r0x0001814bb9ce:
                    fVar18 = *(float *)(puVar9 + 1);
                    fStack_108 = (float)*puVar9;
                    fStack_104 = (float)((ulonglong)*puVar9 >> 0x20);
                    if ((plVar12[0xd] == 0) || (*(longlong *)(plVar12[0xd] + 0x38) == 0))
                    goto code_r0x0001814bbc5c;
                    uStackX_10 = CLKMBEGHOLA::BHNFPDMBOAM();
                  }
                  else if (iVar16 == 2) {
                    if (*(longlong *)(param_1 + 0x20) == 0) goto code_r0x0001814bbc5c;
                    puVar9 = (undefined8 *)FixedTransform::get_position(auStack_88,*(longlong *)(param_1 + 0x20),0)
                    ;
                    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814bbc5c;
                    cVar1 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x165);
                    uVar4 = *puVar9;
                    fVar18 = *(float *)(puVar9 + 1);
                    if (*(longlong *)(lVar8 + 0x58) == 0) goto code_r0x0001814bbc5c;
                    dVar17 = (double)sub_855f06a0(*(longlong *)(lVar8 + 0x58),0,
                                                   uRam0000000187b40048);
                    fStack_108 = (float)uVar4;
                    iVar16 = -1;
                    if (cVar1 == '\0') {
                      iVar16 = 1;
                    }
                    fVar18 = fVar18 + 0.0;
                    fStack_108 = fStack_108 + ((float)dVar17 * (float)iVar16) / 540.0;
                    fStack_104 = (float)((ulonglong)uVar4 >> 0x20);
                    fStack_104 = fStack_104 + 0.0;
                    uStackX_10 = 0;
                  }
                  else if (iVar16 == 3) {
                    if (*(longlong *)(param_1 + 0x20) == 0) goto code_r0x0001814bbc5c;
                    puVar9 = (undefined8 *)FixedTransform::get_position(auStack_98,*(longlong *)(param_1 + 0x20),0)
                    ;
                    uStackX_10 = 0;
                    fVar18 = *(float *)(puVar9 + 1);
                    fStack_108 = (float)*puVar9;
                    fStack_104 = (float)((ulonglong)*puVar9 >> 0x20);
                  }
                  else {
                    if (iVar16 == 4) {
                      if ((*(longlong *)(lVar8 + 0x50) != 0) &&
                         (plVar12 = (longlong *)
                                    sub_849ed2d0(*(longlong *)(lVar8 + 0x50),0,uRam0000000187b812d0
                                                 ), plVar12 != (longlong *)0x0)) {
                        puVar9 = (undefined8 *)(**(code **)(*plVar12 + 0x178))(auStack_a8,plVar12);
                        uStackX_8 = 0;
                        goto code_r0x0001814bb9ce;
                      }
                      goto code_r0x0001814bbc5c;
                    }
                    fStack_108 = 0.0;
                    fStack_104 = 0.0;
                  }
                  iVar16 = 0;
                  if (*(longlong *)(lVar8 + 0x88) == 0) goto code_r0x0001814bbc5c;
                  iVar3 = *(int *)(*(longlong *)(lVar8 + 0x88) + 0x18);
                  if (0 < iVar3) {
                    do {
                      lVar10 = *(longlong *)(lVar7 + 0x20);
                      if (((*(longlong *)(lVar8 + 0x88) == 0) ||
                          (uVar6 = sub_84a610f0(*(longlong *)(lVar8 + 0x88),iVar16,
                                                 uRam0000000187b446b8), lVar10 == 0)) ||
                         (lVar10 = sub_849ed2d0(lVar10,uVar6,uRam0000000187c19e18), lVar10 == 0))
                      goto code_r0x0001814bbc5c;
                      *(undefined1 *)(lVar10 + 0x98) = uStackX_10;
                      in_stack_fffffffffffffec8 = in_stack_fffffffffffffec8 & 0xffffffffffffff00;
                      uStack_c8 = CONCAT44(fStack_104,fStack_108);
                      fStack_c0 = fVar18;
                      UnitActionController::searchAndSortTarget(param_1,lVar7,lVar10,&uStack_c8,in_stack_fffffffffffffec8,
                                    uStackX_8,0);
                      for (iVar14 = 0; iVar14 < *(int *)(lVar10 + 0x1c); iVar14 = iVar14 + 1) {
                        lVar13 = *(longlong *)(lVar10 + 0x50);
                        if (lVar13 == 0) goto code_r0x0001814bbc5c;
                        if (iVar14 == *(int *)(lVar13 + 0x18)) break;
                        lVar13 = sub_849ed2d0(lVar13,iVar14,uRam0000000187b812d0);
                        if (lVar13 == 0) goto code_r0x0001814bbc5c;
                        in_stack_fffffffffffffec8 = *(ulonglong *)(lVar13 + 0x68);
                        if (*(int *)(lVar8 + 0x10) == 1) {
                          if ((in_stack_fffffffffffffec8 == 0) ||
                             (*(longlong *)(in_stack_fffffffffffffec8 + 0xb0) == 0))
                          goto code_r0x0001814bbc5c;
                          sub_804f1ed0(*(longlong *)(in_stack_fffffffffffffec8 + 0xb0),1,0);
                        }
                        lVar13 = sub_814a9080(param_1,0);
                        uVar4 = *(undefined8 *)(param_1 + 0x90);
                        if ((*(longlong *)(lVar10 + 0x50) == 0) ||
                           (uVar11 = sub_849ed2d0(*(longlong *)(lVar10 + 0x50),iVar14,
                                                   uRam0000000187b812d0), lVar13 == 0))
                        goto code_r0x0001814bbc5c;
                        sub_80012050(2,uRam0000000187b93630,lVar13,uVar4,in_stack_fffffffffffffec8,
                                      uVar11);
                      }
                      iVar16 = iVar16 + 1;
                    } while (iVar16 < iVar3);
                  }
                }
              }
              iVar15 = iVar15 + 1;
            }
            lVar8 = sub_814a9080(param_1,0);
            if (lVar8 == 0) goto code_r0x0001814bbc5c;
            in_stack_fffffffffffffec8 = in_stack_fffffffffffffec8 & 0xffffffff00000000;
            sub_800158b0(0x15,uRam0000000187b93630,lVar8,lVar7,in_stack_fffffffffffffec8);
          }
          iStack_118 = iStack_118 + 1;
        } while (iStack_118 < iVar2);
      }
      return;
    }
  }
code_r0x0001814bbc5c:
  sub_8032f9d0();
  pcVar5 = (code *)swi(3);
  (*pcVar5)();
  return;
}



/* ==== UnitActionController::AppendCoroutine  @ 0x14bbc70  size=1 ==== */

void UnitActionController::AppendCoroutine(undefined8 param_1,undefined8 param_2,undefined4 param_3,undefined8 param_4)

{
  longlong lVar1;
  code *pcVar2;
  
  if (cRam000000018ab626c1 == '\0') {
    sub_8032f6d0(0x187b93630);
    cRam000000018ab626c1 = '\x01';
  }
  if (cRam000000018ab626da == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab626da = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
  if (lVar1 != 0) {
    sub_800110c0(0x11,uRam0000000187b93630,lVar1,param_2,param_3,param_4);
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::CancelAction  @ 0x14bbd10  size=1 ==== */

void UnitActionController::CancelAction(longlong param_1,undefined4 param_2)

{
  int iVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  undefined8 uVar5;
  longlong *plVar6;
  longlong lVar7;
  
  if (cRam000000018ab626bd == '\0') {
    sub_8032f6d0(0x187c421b8);
    sub_8032f6d0(0x187c4ad38);
    sub_8032f6d0(0x187c324a8);
    sub_8032f6d0(0x187c32658);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab626bd = '\x01';
  }
  if (((*(longlong *)(param_1 + 0xb0) == 0) ||
      (lVar4 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8), lVar4 == 0
      )) || (*(longlong *)(lVar4 + 0x98) == 0)) {
code_r0x0001814bbf27:
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  iVar1 = *(int *)(*(longlong *)(lVar4 + 0x98) + 0x18);
joined_r0x0001814bbdb0:
  do {
    iVar1 = iVar1 + -1;
    if (iVar1 < 0) {
      *(undefined1 *)(lVar4 + 0x84) = 1;
      return;
    }
    if (*(longlong *)(lVar4 + 0x98) == 0) goto code_r0x0001814bbf27;
    uVar5 = sub_849ed2d0(*(longlong *)(lVar4 + 0x98),iVar1,uRam0000000187c32658);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar3 = Object::op_Inequality(uVar5,0,0);
  } while (cVar3 == '\0');
  if (*(longlong *)(lVar4 + 0x98) == 0) goto code_r0x0001814bbf27;
  plVar6 = (longlong *)sub_849ed2d0(*(longlong *)(lVar4 + 0x98),iVar1,uRam0000000187c32658);
  if (plVar6 != (longlong *)0x0) {
    if ((*(byte *)(lRam0000000187c4ad38 + 0x130) <= *(byte *)(*plVar6 + 0x130)) &&
       (*(longlong *)
         (*(longlong *)(*plVar6 + 200) + -8 + (ulonglong)*(byte *)(lRam0000000187c4ad38 + 0x130) * 8
         ) == lRam0000000187c4ad38)) goto joined_r0x0001814bbdb0;
  }
  if ((*(longlong *)(lVar4 + 0x98) == 0) ||
     (lVar7 = sub_849ed2d0(*(longlong *)(lVar4 + 0x98),iVar1,uRam0000000187c32658), lVar7 == 0))
  goto code_r0x0001814bbf27;
  if (*(char *)(lVar7 + 0x50) == '\0') {
    if ((*(longlong *)(lVar4 + 0x98) == 0) ||
       (lVar7 = sub_849ed2d0(*(longlong *)(lVar4 + 0x98),iVar1,uRam0000000187c32658), lVar7 == 0))
    goto code_r0x0001814bbf27;
    *(undefined1 *)(lVar7 + 0xb4) = 1;
    if ((*(longlong *)(lVar4 + 0x98) == 0) ||
       (lVar7 = sub_849ed2d0(*(longlong *)(lVar4 + 0x98),iVar1,uRam0000000187c32658), lVar7 == 0))
    goto code_r0x0001814bbf27;
    lVar7 = *(longlong *)(lVar7 + 0x88);
    if ((*(longlong *)(lVar4 + 0x98) == 0) ||
       (uVar5 = sub_849ed2d0(*(longlong *)(lVar4 + 0x98),iVar1,uRam0000000187c32658), lVar7 == 0))
    goto code_r0x0001814bbf27;
    (**(code **)(lVar7 + 0x18))(*(undefined8 *)(lVar7 + 0x40),uVar5,*(undefined8 *)(lVar7 + 0x28));
  }
  goto joined_r0x0001814bbdb0;
}



/* ==== UnitActionController::CreateExSkill  @ 0x14bbf30  size=1 ==== */

longlong UnitActionController::CreateExSkill(longlong param_1,ulonglong param_2,undefined8 param_3,undefined8 param_4)

{
  undefined4 uVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  ulonglong uVar4;
  ulonglong uVar5;
  code *pcVar6;
  char cVar7;
  undefined4 uVar8;
  uint uVar9;
  longlong lVar10;
  longlong lVar11;
  undefined8 uVar12;
  longlong lVar13;
  longlong lVar14;
  undefined8 *puVar15;
  ulonglong uVar16;
  undefined8 uVar17;
  IMAGE_DOS_HEADER *pIVar18;
  longlong *plVar19;
  IMAGE_DOS_HEADER *pIVar20;
  ushort uVar21;
  longlong lVar22;
  IMAGE_DOS_HEADER *pIVar23;
  IMAGE_DOS_HEADER *unaff_R14;
  undefined8 uStack_c8;
  undefined8 *puStack_c0;
  longlong lStack_b8;
  longlong lStack_b0;
  longlong lStack_a8;
  longlong lStack_a0;
  undefined8 uStack_98;
  undefined8 uStack_90;
  undefined8 uStack_88;
  undefined8 uStack_80;
  undefined8 uStack_78;
  undefined8 uStack_68;
  undefined8 uStack_60;
  undefined8 uStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  
  param_2 = param_2 & 0xffffffff;
  if (cRam000000018ab626cf == '\0') {
    sub_8032f6d0(0x187b2e848);
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187b3cee0);
    sub_8032f6d0(0x187c37648);
    sub_8032f6d0(0x187be6780);
    sub_8032f6d0(0x187be6938);
    sub_8032f6d0(0x187be6af0);
    sub_8032f6d0(0x187b45ab0);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187bc7428);
    sub_8032f6d0(0x187bc75e0);
    sub_8032f6d0(0x187c19748);
    sub_8032f6d0(0x187c19590);
    sub_8032f6d0(0x187ba38a0);
    sub_8032f6d0(0x187ba3a58);
    sub_8032f6d0(0x187be2298);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187c440d8);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b778e8);
    sub_8032f6d0(0x187bd2008);
    sub_8032f6d0(0x187bd21c0);
    sub_8032f6d0(0x187bd2378);
    sub_8032f6d0(0x187bd2530);
    sub_8032f6d0(0x187c20828);
    sub_8032f6d0(0x187bd26e8);
    sub_8032f6d0(0x187bd28a0);
    sub_8032f6d0(0x187c209e0);
    cRam000000018ab626cf = '\x01';
  }
  uStack_98 = 0;
  uStack_90 = 0;
  uStack_88 = 0;
  uStack_80 = 0;
  uStack_78 = 0;
  lVar10 = sub_8032f970(uRam0000000187c20828);
  sub_804383d0(lVar10,0);
  lStack_a8 = lVar10;
  if (lVar10 != 0) {
    *(undefined8 *)(lVar10 + 0x10) = param_4;
    sub_8032e8d0(lVar10 + 0x10,param_4);
    *(longlong *)(lVar10 + 0x18) = param_1;
    sub_8032e8d0(lVar10 + 0x18,param_1);
    lVar11 = sub_8032f970(uRam0000000187b778e8);
    lStack_b0 = lVar11;
    if (cRam000000018ab62687 == '\0') {
      sub_8032f6d0(0x187c30b18);
      sub_8032f6d0(0x187c209c8);
      cRam000000018ab62687 = '\x01';
    }
    uVar12 = sub_8032f970(uRam0000000187c209c8);
    sub_849de5b0(uVar12,uRam0000000187c30b18);
    *(undefined8 *)(lVar11 + 0x1a0) = uVar12;
    sub_8032e8d0(lVar11 + 0x1a0,uVar12);
    SkillBase::.ctor(lVar11,0);
    lStack_a0 = lVar11;
    uVar12 = sub_8032f970(uRam0000000187be2298);
    sub_849de5b0(uVar12,uRam0000000187c19590);
    *(undefined8 *)(lVar11 + 0x20) = uVar12;
    sub_8032e8d0(lVar11 + 0x20,uVar12);
    lVar13 = *(longlong *)(lRam0000000187b3c708 + 0x20);
    if ((*(byte *)(lVar13 + 0x135) & 1) == 0) {
      lVar13 = sub_8032d010(lVar13);
    }
    lVar13 = *(longlong *)(*(longlong *)(lVar13 + 0xc0) + 0x10);
    if ((*(byte *)(lVar13 + 0x135) & 1) == 0) {
      lVar13 = sub_8032d010(lVar13);
    }
    lVar13 = **(longlong **)(lVar13 + 0xb8);
    if ((lVar13 != 0) && (*(longlong *)(lVar13 + 0x3d8) != 0)) {
      lVar14 = HOEABFDCBIG::CBBMJADBDGF(*(longlong *)(lVar13 + 0x3d8),param_2,0);
      lStack_b8 = *(longlong *)(lVar13 + 0x1cb0);
      pIVar23 = (IMAGE_DOS_HEADER *)0x0;
      unaff_R14 = pIVar23;
      if (lVar14 != 0) {
        while (lVar13 = *(longlong *)(lVar14 + 0x28), lVar13 != 0) {
          if (*(int *)(lVar13 + 0x18) <= (int)unaff_R14) {
            if (*(longlong *)(param_1 + 0x90) != 0) {
              lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
              if ((*(longlong *)(lVar10 + 0x10) != 0) &&
                 (lVar13 = *(longlong *)(*(longlong *)(lVar10 + 0x10) + 0x28), lVar13 != 0)) {
                sub_855337a0(&uStack_68,lVar13,uRam0000000187b3cee0);
                uStack_98 = uStack_68;
                uStack_90 = uStack_60;
                uStack_88 = uStack_58;
                uStack_80 = uStack_50;
                uStack_78 = uStack_48;
                uStack_c8 = 0;
                puStack_c0 = &uStack_98;
                goto code_r0x0001814bc4c0;
              }
            }
            break;
          }
          param_2 = sub_849ed2d0(lVar13,unaff_R14,uRam0000000187ba3a58);
          if (cRam000000018ab626da == '\0') {
            sub_8032f6d0(0x187bfaba0);
            cRam000000018ab626da = '\x01';
          }
          plVar19 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
          if (plVar19 == (longlong *)0x0) break;
          lVar13 = *plVar19;
          uVar21 = 0;
          if (*(ushort *)(lVar13 + 0x12e) != 0) {
            do {
              if (*(longlong *)(*(longlong *)(lVar13 + 0xb0) + (ulonglong)uVar21 * 0x10) ==
                  lRam0000000187b93630) {
                puVar15 = (undefined8 *)
                          ((longlong)
                           (*(int *)(*(longlong *)(lVar13 + 0xb0) + 8 + (ulonglong)uVar21 * 0x10) +
                           0x1b) * 0x10 + 0x138 + lVar13);
                goto code_r0x0001814bc319;
              }
              uVar21 = uVar21 + 1;
            } while (uVar21 < *(ushort *)(lVar13 + 0x12e));
          }
          puVar15 = (undefined8 *)sub_8032c930(plVar19,lRam0000000187b93630,0x1b);
code_r0x0001814bc319:
          cVar7 = (*(code *)*puVar15)(plVar19,puVar15[1]);
          if (cVar7 == '\0') {
code_r0x0001814bc3e5:
            lVar22 = *(longlong *)(lVar11 + 0x20);
            uVar16 = param_2;
          }
          else {
            if (*(longlong *)(param_1 + 0x90) == 0) break;
            if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x178) != '\0') goto code_r0x0001814bc3e5;
            if (param_2 == 0) break;
            uVar12 = *(undefined8 *)(param_2 + 0x14);
            uVar8 = *(undefined4 *)(param_2 + 0x1c);
            if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
              sub_8032fa70();
            }
            puStack_c0 = (undefined8 *)CONCAT44(puStack_c0._4_4_,uVar8);
            uStack_c8 = uVar12;
            uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_c8,0);
            if (lStack_b8 == 0) break;
            lVar13 = IKGBNKHMJIG::BKEJKNOJCEE(lStack_b8,uVar8,0);
            if (lVar13 == 0) goto code_r0x0001814bc3e5;
            lVar22 = *(longlong *)(lVar11 + 0x20);
            uVar16 = GAMCMFJBHME::FHOALPFLEEC(lVar13,0);
          }
          uVar12 = UnitActionController::createExEquipActionParameter(param_1,uVar16,param_3,*(undefined8 *)(lVar10 + 0x10),0);
          if (lVar22 == 0) break;
          sub_80002460(lVar22,uVar12,uRam0000000187c19748);
          unaff_R14 = (IMAGE_DOS_HEADER *)(ulonglong)((int)unaff_R14 + 1);
        }
      }
    }
  }
  goto code_r0x0001814bcb49;
code_r0x0001814bc4c0:
  unaff_R14 = &IMAGE_DOS_HEADER_180000000;
  cVar7 = sub_85760f30(&uStack_98,uRam0000000187be6938);
  if (cVar7 == '\0') goto code_r0x0001814bca45;
  param_2 = sub_8032f970(uRam0000000187c209e0);
  sub_804383d0(param_2,0);
  if (param_2 == 0) goto code_r0x0001814bcbe5;
  *(longlong *)(param_2 + 0x20) = lVar10;
  sub_8032e8d0(param_2 + 0x20,lVar10);
  *(undefined4 *)(param_2 + 0x10) = (undefined4)uStack_88;
  *(undefined4 *)(param_2 + 0x14) = uStack_88._4_4_;
  *(undefined4 *)(param_2 + 0x18) = (undefined4)uStack_80;
  *(undefined4 *)(param_2 + 0x1c) = uStack_80._4_4_;
  sub_8032e8d0(param_2 + 0x18,0);
  if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814bcbe0;
  unaff_R14 = *(IMAGE_DOS_HEADER **)(*(longlong *)(param_2 + 0x20) + 0x10);
  if (unaff_R14 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bcbdb;
  if (*(int *)(unaff_R14->e_program + 0xc) != -1) {
    if (*(longlong *)(param_2 + 0x18) == 0) goto code_r0x0001814bcb4f;
    if (*(int *)(*(longlong *)(param_2 + 0x18) + 0x14) == -1) {
      uVar8 = 0xffffffff;
    }
    else {
      uVar8 = *(undefined4 *)(*(longlong *)(*(longlong *)(param_2 + 0x20) + 0x10) + 0x4c);
      uVar1 = *(undefined4 *)(*(longlong *)(param_2 + 0x18) + 0x14);
      if (*(int *)(lRam0000000187c440d8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar8 = Math::Max(uVar8,uVar1,0);
    }
    *(undefined4 *)(unaff_R14->e_program + 0xc) = uVar8;
  }
  unaff_R14 = &IMAGE_DOS_HEADER_180000000;
  if (lVar11 == 0) goto code_r0x0001814bcbd6;
  if (*(longlong *)(param_2 + 0x18) == 0) goto code_r0x0001814bcbd1;
  sub_847b7a70(*(undefined8 *)(lVar11 + 0x2b0),
                *(undefined4 *)(*(longlong *)(param_2 + 0x18) + 0x2c),1,uRam0000000187b45ab0);
  uVar9 = *(int *)(param_2 + 0x10) - 1;
  if (uVar9 < 8) {
    uVar16 = (ulonglong)*(uint *)(&UNK_1814bcbf4 + (longlong)(int)uVar9 * 4) + 0x180000000;
    pIVar18 = pIVar23;
    switch(*(int *)(param_2 + 0x10)) {
    case 1:
      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814bcb67;
      lVar13 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x70);
      if (lVar13 == 0) goto code_r0x0001814bcb62;
      uVar12 = *(undefined8 *)(lVar13 + 0xe0);
      unaff_R14 = (IMAGE_DOS_HEADER *)0x0;
      if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814bcb5d;
      unaff_R14 = *(IMAGE_DOS_HEADER **)(*(longlong *)(param_2 + 0x20) + 0x28);
      if (unaff_R14 == (IMAGE_DOS_HEADER *)0x0) {
        lVar14 = *(longlong *)(param_2 + 0x20);
        unaff_R14 = (IMAGE_DOS_HEADER *)sub_8032f970(uRam0000000187c32f48);
        sub_8044b290(unaff_R14,lVar14,uRam0000000187bd21c0,0);
        *(IMAGE_DOS_HEADER **)(lVar14 + 0x28) = unaff_R14;
        sub_8032e8d0(lVar14 + 0x28,unaff_R14);
      }
      pIVar20 = (IMAGE_DOS_HEADER *)Delegate::Combine(uVar12,unaff_R14,0);
      if (pIVar20 != (IMAGE_DOS_HEADER *)0x0) {
        uVar2._0_1_ = pIVar20->e_magic[0];
        uVar2._1_1_ = pIVar20->e_magic[1];
        uVar2._2_2_ = pIVar20->e_cblp;
        uVar2._4_2_ = pIVar20->e_cp;
        uVar2._6_2_ = pIVar20->e_crlc;
        if (uVar2 == uRam0000000187c32f48) {
          pIVar18 = pIVar20;
        }
        if (pIVar18 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bcb55;
      }
      *(IMAGE_DOS_HEADER **)(lVar13 + 0xe0) = pIVar18;
      lVar13 = lVar13 + 0xe0;
      break;
    case 2:
      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814bcb7e;
      lVar13 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x70);
      if (lVar13 == 0) goto code_r0x0001814bcb79;
      uVar12 = *(undefined8 *)(lVar13 + 0x88);
      unaff_R14 = (IMAGE_DOS_HEADER *)0x0;
      if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814bcb74;
      unaff_R14 = *(IMAGE_DOS_HEADER **)(*(longlong *)(param_2 + 0x20) + 0x30);
      if (unaff_R14 == (IMAGE_DOS_HEADER *)0x0) {
        lVar14 = *(longlong *)(param_2 + 0x20);
        unaff_R14 = (IMAGE_DOS_HEADER *)sub_8032f970(uRam0000000187c37648);
        EEHFLEJGCIP::.ctor(unaff_R14,lVar14,uRam0000000187bd2378,0);
        *(IMAGE_DOS_HEADER **)(lVar14 + 0x30) = unaff_R14;
        sub_8032e8d0(lVar14 + 0x30,unaff_R14);
      }
      pIVar20 = (IMAGE_DOS_HEADER *)Delegate::Combine(uVar12,unaff_R14,0);
      pIVar18 = (IMAGE_DOS_HEADER *)0x0;
      if (pIVar20 != (IMAGE_DOS_HEADER *)0x0) {
        uVar3._0_1_ = pIVar20->e_magic[0];
        uVar3._1_1_ = pIVar20->e_magic[1];
        uVar3._2_2_ = pIVar20->e_cblp;
        uVar3._4_2_ = pIVar20->e_cp;
        uVar3._6_2_ = pIVar20->e_crlc;
        pIVar18 = pIVar23;
        if (uVar3 == uRam0000000187c37648) {
          pIVar18 = pIVar20;
        }
        if (pIVar18 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bcb6c;
      }
code_r0x0001814bc77e:
      *(IMAGE_DOS_HEADER **)(lVar13 + 0x88) = pIVar18;
      lVar13 = lVar13 + 0x88;
      break;
    case 3:
      unaff_R14 = (IMAGE_DOS_HEADER *)0x0;
      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814bcb90;
      unaff_R14 = *(IMAGE_DOS_HEADER **)(*(longlong *)(param_1 + 0x90) + 0x70);
      if (unaff_R14 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bcb8b;
      uVar12._0_2_ = unaff_R14[1].e_sp;
      uVar12._2_2_ = unaff_R14[1].e_csum;
      uVar12._4_2_ = unaff_R14[1].e_ip;
      uVar12._6_2_ = unaff_R14[1].e_cs;
      uVar17 = sub_8032f970(uRam0000000187c37648);
      EEHFLEJGCIP::.ctor(uVar17,param_2,uRam0000000187bd26e8,0);
      pIVar20 = (IMAGE_DOS_HEADER *)Delegate::Combine(uVar12,uVar17,0);
      if (pIVar20 != (IMAGE_DOS_HEADER *)0x0) {
        uVar4._0_1_ = pIVar20->e_magic[0];
        uVar4._1_1_ = pIVar20->e_magic[1];
        uVar4._2_2_ = pIVar20->e_cblp;
        uVar4._4_2_ = pIVar20->e_cp;
        uVar4._6_2_ = pIVar20->e_crlc;
        if (uVar4 == uRam0000000187c37648) {
          pIVar18 = pIVar20;
        }
        if (pIVar18 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bcb83;
      }
      *(IMAGE_DOS_HEADER **)&unaff_R14[1].e_sp = pIVar18;
      sub_8032e8d0(&unaff_R14[1].e_sp,pIVar18);
    default:
      goto code_r0x0001814bc4c0;
    case 5:
      uVar16 = param_2;
      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814bcba7;
      lVar13 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x70);
      if (lVar13 == 0) goto code_r0x0001814bcba2;
      uVar12 = *(undefined8 *)(lVar13 + 0x88);
      unaff_R14 = (IMAGE_DOS_HEADER *)0x0;
      if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814bcb9d;
      unaff_R14 = *(IMAGE_DOS_HEADER **)(*(longlong *)(param_2 + 0x20) + 0x38);
      if (unaff_R14 == (IMAGE_DOS_HEADER *)0x0) {
        lVar14 = *(longlong *)(param_2 + 0x20);
        unaff_R14 = (IMAGE_DOS_HEADER *)sub_8032f970(uRam0000000187c37648);
        EEHFLEJGCIP::.ctor(unaff_R14,lVar14,uRam0000000187bd2530,0);
        *(IMAGE_DOS_HEADER **)(lVar14 + 0x38) = unaff_R14;
        sub_8032e8d0(lVar14 + 0x38,unaff_R14);
      }
      pIVar20 = (IMAGE_DOS_HEADER *)Delegate::Combine(uVar12,unaff_R14,0);
      if (pIVar20 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bc77e;
      uVar5._0_1_ = pIVar20->e_magic[0];
      uVar5._1_1_ = pIVar20->e_magic[1];
      uVar5._2_2_ = pIVar20->e_cblp;
      uVar5._4_2_ = pIVar20->e_cp;
      uVar5._6_2_ = pIVar20->e_crlc;
      if (uVar5 == uRam0000000187c37648) {
        pIVar18 = pIVar20;
      }
      if (pIVar18 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bcb95;
      *(IMAGE_DOS_HEADER **)(lVar13 + 0x88) = pIVar18;
      lVar13 = lVar13 + 0x88;
      break;
    case 7:
      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814bcbbc;
      lVar13 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd0);
      if (lVar13 == 0) goto code_r0x0001814bcbb7;
      uVar12 = *(undefined8 *)(lVar13 + 0x100);
      uVar17 = sub_8032f970(pIRam0000000187b2e848);
      sub_84f4e300(uVar17,param_2,uRam0000000187bd28a0,0);
      uVar16 = Delegate::Combine(uVar12,uVar17);
      unaff_R14 = pIRam0000000187b2e848;
      if ((uVar16 != 0) &&
         (pIVar18 = (IMAGE_DOS_HEADER *)sub_8032e8f0(uVar16,pIRam0000000187b2e848),
         pIVar18 == (IMAGE_DOS_HEADER *)0x0)) goto code_r0x0001814bcbac;
      *(IMAGE_DOS_HEADER **)(lVar13 + 0x100) = pIVar18;
      sub_8032e8d0(lVar13 + 0x100,pIVar18);
      param_2 = uVar16;
      goto code_r0x0001814bc4c0;
    case 8:
      uVar12 = *(undefined8 *)(param_1 + 0xa0);
      if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814bcbcc;
      lVar13 = *(longlong *)(*(longlong *)(param_2 + 0x20) + 0x20);
      if (lVar13 == 0) {
        lVar14 = *(longlong *)(param_2 + 0x20);
        lVar13 = sub_8032f970(uRam0000000187c32f48);
        sub_8044b290(lVar13,lVar14,uRam0000000187bd2008,0);
        *(longlong *)(lVar14 + 0x20) = lVar13;
        sub_8032e8d0(lVar14 + 0x20,lVar13);
      }
      pIVar20 = (IMAGE_DOS_HEADER *)Delegate::Combine(uVar12,lVar13);
      if (pIVar20 != (IMAGE_DOS_HEADER *)0x0) {
        uVar16._0_1_ = pIVar20->e_magic[0];
        uVar16._1_1_ = pIVar20->e_magic[1];
        uVar16._2_2_ = pIVar20->e_cblp;
        uVar16._4_2_ = pIVar20->e_cp;
        uVar16._6_2_ = pIVar20->e_crlc;
        if (uVar16 == uRam0000000187c32f48) {
          pIVar18 = pIVar20;
        }
        uVar16 = uRam0000000187c32f48;
        if (pIVar18 == (IMAGE_DOS_HEADER *)0x0) goto code_r0x0001814bcbc1;
      }
      *(IMAGE_DOS_HEADER **)(param_1 + 0xa0) = pIVar18;
      sub_8032e8d0(param_1 + 0xa0);
      goto code_r0x0001814bc4c0;
    }
    sub_8032e8d0(lVar13,pIVar18);
  }
  goto code_r0x0001814bc4c0;
code_r0x0001814bca45:
  sub_80443e50(&uStack_98,uRam0000000187be6780);
  lVar11 = lStack_b0;
  lVar10 = *(longlong *)(lVar10 + 0x10);
  if (lVar10 != 0) {
    *(undefined4 *)(lVar10 + 0x50) = *(undefined4 *)(lVar10 + 0x4c);
    UnitActionController::DependActionSolve(param_1,lStack_b0,0);
    if (cRam000000018ab6269d == '\0') {
      sub_8032f6d0(0x187c19c60);
      sub_8032f6d0(0x187c19e18);
      cRam000000018ab6269d = '\x01';
    }
    if (lVar11 != 0) {
      while (lVar10 = *(longlong *)(lVar11 + 0x20), lVar10 != 0) {
        if (*(int *)(lVar10 + 0x18) <= (int)pIVar23) {
          UnitActionController::ExecActionOnStart(param_1,lVar11,0);
          return lVar11;
        }
        plVar19 = (longlong *)sub_849ed2d0(lVar10,pIVar23,uRam0000000187c19e18);
        if (plVar19 == (longlong *)0x0) break;
        (**(code **)(*plVar19 + 0x248))(plVar19,*(undefined8 *)(param_1 + 0x90));
        pIVar23 = (IMAGE_DOS_HEADER *)(ulonglong)((int)pIVar23 + 1);
      }
    }
  }
code_r0x0001814bcb49:
  sub_8032f9d0();
code_r0x0001814bcb4f:
  pIVar20 = (IMAGE_DOS_HEADER *)sub_8032f9d0();
code_r0x0001814bcb55:
  sub_8032e920(pIVar20);
code_r0x0001814bcb5d:
  sub_8032f9d0();
code_r0x0001814bcb62:
  sub_8032f9d0();
code_r0x0001814bcb67:
  pIVar20 = (IMAGE_DOS_HEADER *)sub_8032f9d0();
code_r0x0001814bcb6c:
  sub_8032e920(pIVar20);
code_r0x0001814bcb74:
  sub_8032f9d0();
code_r0x0001814bcb79:
  sub_8032f9d0();
code_r0x0001814bcb7e:
  pIVar20 = (IMAGE_DOS_HEADER *)sub_8032f9d0();
code_r0x0001814bcb83:
  sub_8032e920(pIVar20);
code_r0x0001814bcb8b:
  sub_8032f9d0();
code_r0x0001814bcb90:
  pIVar20 = (IMAGE_DOS_HEADER *)sub_8032f9d0();
code_r0x0001814bcb95:
  sub_8032e920(pIVar20);
code_r0x0001814bcb9d:
  sub_8032f9d0();
code_r0x0001814bcba2:
  sub_8032f9d0();
  uVar16 = param_2;
code_r0x0001814bcba7:
  sub_8032f9d0();
code_r0x0001814bcbac:
  sub_8032e920(uVar16,unaff_R14);
code_r0x0001814bcbb7:
  sub_8032f9d0();
code_r0x0001814bcbbc:
  pIVar20 = (IMAGE_DOS_HEADER *)sub_8032f9d0();
code_r0x0001814bcbc1:
  sub_8032e920(pIVar20,uVar16);
code_r0x0001814bcbcc:
  sub_8032f9d0();
code_r0x0001814bcbd1:
  sub_8032f9d0();
code_r0x0001814bcbd6:
  sub_8032f9d0();
code_r0x0001814bcbdb:
  sub_8032f9d0();
code_r0x0001814bcbe0:
  sub_8032f9d0();
code_r0x0001814bcbe5:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar6 = (code *)swi(3);
  lVar10 = (*pcVar6)();
  return lVar10;
}



/* ==== UnitActionController::CreateNormalPrefabWithTargetMotion  @ 0x14bcc20  size=1 ==== */

void UnitActionController::CreateNormalPrefabWithTargetMotion(longlong param_1,longlong param_2,int param_3,undefined1 param_4,char param_5,
                  undefined1 param_6)

{
  int iVar1;
  code *pcVar2;
  byte bVar3;
  longlong lVar4;
  longlong lVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  byte bVar8;
  int iVar9;
  ulonglong in_stack_ffffffffffffffa8;
  
  if (cRam000000018ab626ac == '\0') {
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187b86358);
    sub_8032f6d0(0x187b86510);
    cRam000000018ab626ac = '\x01';
  }
  iVar9 = 0;
  if ((param_2 != 0) && (*(longlong *)(param_2 + 0x30) != 0)) {
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x30) + 0x18);
    if (0 < iVar1) {
      do {
        if ((*(longlong *)(param_2 + 0x30) == 0) ||
           (lVar4 = sub_849ed2d0(*(longlong *)(param_2 + 0x30),iVar9,uRam0000000187b86510),
           lVar4 == 0)) goto code_r0x0001814bce3f;
        if (*(int *)(lVar4 + 0x4c) == param_3) {
          if ((*(int *)(lVar4 + 0x50) == 0) || (*(int *)(lVar4 + 0x50) - 2U < 3)) {
            bVar8 = 1;
          }
          else {
            bVar8 = 0;
          }
          lVar5 = sub_814a9080(param_1,0);
          if (lVar5 == 0) goto code_r0x0001814bce3f;
          bVar3 = sub_80002270(0x18,uRam0000000187b93630,lVar5);
          if ((bVar8 & bVar3) == 0) {
            if (param_5 == '\0') {
              if (*(float *)(param_2 + 0xb0) <= 0.0) {
                uVar7 = 0;
              }
              else {
                uVar7 = *(undefined8 *)(param_1 + 0x90);
              }
              uVar6 = UnitActionController::createNormalPrefabWithDelay(param_1,lVar4,param_2,param_4,
                                    in_stack_ffffffffffffffa8 & 0xffffffffffffff00,param_6,param_6,0
                                   );
              in_stack_ffffffffffffffa8 = 0;
              UnitActionController::AppendCoroutine(param_1,uVar6,bVar8 ^ 1,uVar7,0);
            }
            else {
              lVar5 = sub_814a9080(param_1,0);
              in_stack_ffffffffffffffa8 = in_stack_ffffffffffffffa8 & 0xffffffffffffff00;
              uVar7 = UnitActionController::createNormalPrefabWithDelay(param_1,lVar4,param_2,param_4,in_stack_ffffffffffffffa8,0,0,0);
              if (lVar5 == 0) goto code_r0x0001814bce3f;
              sub_8000d7b0(0x12,uRam0000000187b93630,lVar5,uVar7);
            }
          }
        }
        iVar9 = iVar9 + 1;
      } while (iVar9 < iVar1);
    }
    return;
  }
code_r0x0001814bce3f:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::DependActionSolve  @ 0x14bce50  size=1 ==== */

void UnitActionController::DependActionSolve(undefined8 param_1,longlong param_2)

{
  int iVar1;
  undefined8 uVar2;
  code *pcVar3;
  char cVar4;
  undefined1 uVar5;
  undefined4 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 uVar9;
  int iVar10;
  int iVar11;
  undefined8 *puVar12;
  int iVar13;
  undefined8 uStack_60;
  undefined8 *puStack_58;
  longlong lStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  longlong lStack_38;
  
  if (cRam000000018ab62699 == '\0') {
    sub_8032f6d0(0x187b7ae48);
    sub_8032f6d0(0x187bb79f8);
    sub_8032f6d0(0x187bbb740);
    sub_8032f6d0(0x187bd0620);
    sub_8032f6d0(0x187b694b0);
    sub_8032f6d0(0x187b69670);
    sub_8032f6d0(0x187b69828);
    sub_8032f6d0(0x187c0c550);
    sub_8032f6d0(0x187bb7a88);
    sub_8032f6d0(0x187b418c0);
    sub_8032f6d0(0x187c311e8);
    sub_8032f6d0(0x187b413a0);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187bf1860);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187bcee30);
    sub_8032f6d0();
    cRam000000018ab62699 = '\x01';
  }
  uStack_48 = 0;
  uStack_40 = 0;
  lStack_38 = 0;
  iVar13 = 0;
  if (param_2 != 0) {
    while (lVar7 = *(longlong *)(param_2 + 0x20), lVar7 != 0) {
      if (*(int *)(lVar7 + 0x18) <= iVar13) {
        uVar9 = sub_8032f970(uRam0000000187bf1860);
        sub_849de5b0(uVar9,uRam0000000187b413a0);
        *(undefined8 *)(param_2 + 0xa8) = uVar9;
        sub_8032e8d0(param_2 + 0xa8,uVar9);
        iVar13 = 0;
        if (*(longlong *)(param_2 + 0x20) != 0) {
          iVar10 = *(int *)(*(longlong *)(param_2 + 0x20) + 0x18);
          if (0 < iVar10) goto code_r0x0001814bd0d0;
          goto code_r0x0001814bd1b6;
        }
        break;
      }
      lVar7 = sub_849ed2d0(lVar7,iVar13,uRam0000000187c19e18);
      if (lVar7 == 0) break;
      if (*(int *)(lVar7 + 0x80) != 0) {
        iVar10 = 0;
        if (*(longlong *)(param_2 + 0x20) == 0) break;
        iVar11 = *(int *)(*(longlong *)(param_2 + 0x20) + 0x18);
        if (0 < iVar11) {
          do {
            if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814bd3e0;
            lVar7 = sub_849ed2d0(*(longlong *)(param_2 + 0x20),iVar10,uRam0000000187c19e18);
            if (((*(longlong *)(param_2 + 0x20) == 0) ||
                (lVar8 = sub_849ed2d0(*(longlong *)(param_2 + 0x20),iVar13,uRam0000000187c19e18),
                lVar8 == 0)) || (lVar7 == 0)) goto code_r0x0001814bd3e0;
            if (*(int *)(lVar8 + 0x80) == *(int *)(lVar7 + 0x38)) {
              if (*(longlong *)(lVar7 + 0x88) == 0) goto code_r0x0001814bd3e0;
              sub_80002bb0(*(longlong *)(lVar7 + 0x88),iVar13,uRam0000000187b418c0);
            }
            iVar10 = iVar10 + 1;
          } while (iVar10 < iVar11);
        }
      }
      iVar13 = iVar13 + 1;
    }
  }
  goto code_r0x0001814bd3e0;
  while (iVar13 = iVar13 + 1, iVar13 < iVar10) {
code_r0x0001814bd0d0:
    puVar12 = (undefined8 *)(param_2 + 0xa8);
    if ((*(longlong *)(param_2 + 0x20) == 0) ||
       (lVar7 = sub_849ed2d0(*(longlong *)(param_2 + 0x20),iVar13,uRam0000000187c19e18), lVar7 == 0
       )) goto code_r0x0001814bd3e0;
    uVar9 = sub_847b36a0(*puVar12,*(undefined8 *)(lVar7 + 0x88),uRam0000000187bd0620);
    uVar9 = sub_847b1ce0(uVar9,uRam0000000187bb79f8);
    *puVar12 = uVar9;
    sub_8032e8d0(puVar12,uVar9);
    if (*(int *)(lVar7 + 0x60) == 7) {
      iVar11 = 0;
      if (*(longlong *)(lVar7 + 0x88) == 0) goto code_r0x0001814bd3e0;
      iVar1 = *(int *)(*(longlong *)(lVar7 + 0x88) + 0x18);
      if (0 < iVar1) {
        do {
          lVar8 = *(longlong *)(param_2 + 0x20);
          if (((*(longlong *)(lVar7 + 0x88) == 0) ||
              (uVar6 = sub_84a610f0(*(longlong *)(lVar7 + 0x88),iVar11,uRam0000000187b446b8),
              lVar8 == 0)) || (lVar8 = sub_849ed2d0(lVar8,uVar6,uRam0000000187c19e18), lVar8 == 0))
          goto code_r0x0001814bd3e0;
          *(undefined1 *)(lVar8 + 0x86) = 1;
          iVar11 = iVar11 + 1;
        } while (iVar11 < iVar1);
      }
    }
  }
code_r0x0001814bd1b6:
  uVar9 = *(undefined8 *)(param_2 + 0x30);
  if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
    sub_8032fa70();
  }
  lVar7 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x18);
  if (lVar7 == 0) {
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uVar2 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
    lVar7 = sub_8032f970(uRam0000000187c0c550);
    sub_8594c2e0(lVar7,uVar2,uRam0000000187bcee30,0);
    *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x18) = lVar7;
    sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x18,lVar7);
  }
  uVar9 = sub_847a5fd0(uVar9,lVar7,uRam0000000187b7ae48);
  lVar7 = sub_847b2640(uVar9,uRam0000000187bbb740);
  UnitActionController::UpdateEffectRunTimeData(param_1,param_2,lVar7,1,0);
  if (lVar7 != 0) {
    sub_849e2810(&uStack_60,lVar7,uRam0000000187c311e8);
    lStack_38 = lStack_50;
    uStack_60 = 0;
    puStack_58 = &uStack_48;
code_r0x0001814bd2e0:
    do {
      cVar4 = sub_8572bc70(&uStack_48,uRam0000000187b69670);
      lVar7 = lStack_38;
      if (cVar4 == '\0') {
        sub_80443e50(&uStack_48,uRam0000000187b694b0);
        return;
      }
      if (lStack_38 == 0) goto code_r0x0001814bd3d4;
      uVar9 = *(undefined8 *)(lStack_38 + 0x10);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar4 = Object::op_Equality(uVar9,0,0);
    } while (cVar4 != '\0');
    if (lVar7 != 0) {
      if (*(longlong *)(lVar7 + 0x10) != 0) {
        uVar9 = sub_847c74a0(*(longlong *)(lVar7 + 0x10),uRam0000000187bb7a88);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uVar5 = Object::op_Inequality(uVar9,0);
        *(undefined1 *)(lVar7 + 0xb8) = uVar5;
        goto code_r0x0001814bd2e0;
      }
      sub_8032f9d0();
    }
    sub_8032f9d0();
code_r0x0001814bd3d4:
    sub_8032f9d0();
    sub_8032b6b0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
code_r0x0001814bd3e0:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitActionController::ExecAction  @ 0x14bda70  size=1 ==== */

void UnitActionController::ExecAction(longlong param_1,longlong *param_2,longlong param_3,ulonglong param_4,int param_5
                  ,undefined4 param_6,undefined1 param_7,ulonglong param_8,char param_9)

{
  undefined8 uVar1;
  code *pcVar2;
  bool bVar3;
  undefined1 uVar4;
  char cVar5;
  char cVar6;
  longlong lVar7;
  undefined8 uVar8;
  longlong lVar9;
  undefined8 uVar10;
  undefined8 uVar11;
  undefined8 uVar12;
  ulonglong uVar13;
  ulonglong uVar14;
  ulonglong uVar15;
  bool bVar16;
  undefined4 uVar17;
  undefined8 in_stack_ffffffffffffff58;
  undefined4 uVar18;
  undefined8 in_stack_ffffffffffffff60;
  undefined8 uVar19;
  longlong lVar20;
  undefined4 uVar21;
  ulonglong uVar22;
  ulonglong uStack_70;
  undefined4 uVar23;
  
  if (cRam000000018ab626b0 == '\0') {
    sub_8032f6d0(0x187b3fb28);
    sub_8032f6d0(0x187b2c8a0);
    sub_8032f6d0(0x187c4fe78);
    sub_8032f6d0(0x187b57018);
    sub_8032f6d0(0x187bfda18);
    sub_8032f6d0(0x187b3f978);
    sub_8032f6d0(0x187b2ca58);
    sub_8032f6d0(0x187bf7300);
    sub_8032f6d0(0x187b57380);
    sub_8032f6d0(0x187b2c1d0);
    sub_8032f6d0(0x187b44ce8);
    sub_8032f6d0(0x187b2c388);
    sub_8032f6d0(0x187bdde68);
    sub_8032f6d0(0x187ba6e58);
    sub_8032f6d0(0x187b43c50);
    sub_8032f6d0(0x187bf8138);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187b77e20);
    sub_8032f6d0(0x187bd0ed8);
    sub_8032f6d0(0x187c1fa88);
    cRam000000018ab626b0 = '\x01';
  }
  lVar7 = sub_8032f970(uRam0000000187c1fa88);
  sub_804383d0(lVar7,0);
  if (lVar7 == 0) goto code_r0x0001814be424;
  *(longlong *)(lVar7 + 0x28) = param_1;
  sub_8032e8d0(lVar7 + 0x28,param_1);
  if (param_2 == (longlong *)0x0) goto code_r0x0001814be424;
  *(longlong *)(lVar7 + 0x10) = param_2[0x2e];
  sub_8032e8d0(lVar7 + 0x10);
  *(longlong *)(lVar7 + 0x18) = param_2[0x2f];
  sub_8032e8d0(lVar7 + 0x18);
  *(longlong *)(lVar7 + 0x20) = param_2[0x32];
  sub_8032e8d0(lVar7 + 0x20);
  uVar8 = sub_8032f970(uRam0000000187bdde68);
  sub_85575ea0(uVar8,uRam0000000187bfda18);
  lVar9 = sub_8032f970(uRam0000000187b43c50);
  sub_8595b5f0(lVar9,lVar7,uRam0000000187bd0ed8,0);
  uVar10 = sub_8032f970(uRam0000000187bf8138);
  sub_804383d0(uVar10,0);
  lVar7 = sub_8032f970(uRam0000000187ba6e58);
  sub_855de480(lVar7,uVar10,uRam0000000187b3f978);
  if ((lVar9 == 0) ||
     (uVar17 = (**(code **)(lVar9 + 0x18))
                         (*(undefined8 *)(lVar9 + 0x40),param_2,0,*(undefined8 *)(lVar9 + 0x28)),
     lVar7 == 0)) goto code_r0x0001814be424;
  sub_855e0980(lVar7,0,uVar17,uRam0000000187b3fb28);
  uVar17 = (**(code **)(lVar9 + 0x18))
                     (*(undefined8 *)(lVar9 + 0x40),param_2,1,*(undefined8 *)(lVar9 + 0x28));
  sub_855e0980(lVar7,1,uVar17,uRam0000000187b3fb28);
  uVar17 = (**(code **)(lVar9 + 0x18))
                     (*(undefined8 *)(lVar9 + 0x40),param_2,2,*(undefined8 *)(lVar9 + 0x28));
  sub_855e0980(lVar7,2,uVar17,uRam0000000187b3fb28);
  uVar17 = (**(code **)(lVar9 + 0x18))
                     (*(undefined8 *)(lVar9 + 0x40),param_2,3,*(undefined8 *)(lVar9 + 0x28));
  sub_855e0980(lVar7,3,uVar17,uRam0000000187b3fb28);
  uVar17 = (**(code **)(lVar9 + 0x18))
                     (*(undefined8 *)(lVar9 + 0x40),param_2,4,*(undefined8 *)(lVar9 + 0x28));
  sub_855e0980(lVar7,4,uVar17,uRam0000000187b3fb28);
  uVar17 = (**(code **)(lVar9 + 0x18))
                     (*(undefined8 *)(lVar9 + 0x40),param_2,5,*(undefined8 *)(lVar9 + 0x28));
  sub_855e0980(lVar7,5,uVar17,uRam0000000187b3fb28);
  uVar17 = (**(code **)(lVar9 + 0x18))
                     (*(undefined8 *)(lVar9 + 0x40),param_2,6,*(undefined8 *)(lVar9 + 0x28));
  sub_855e0980(lVar7,6,uVar17,uRam0000000187b3fb28);
  if (param_4 == 0) goto code_r0x0001814be424;
  uVar4 = FEFJGFLKNLK::JFPNODDNPBF(param_2,*(undefined8 *)(param_4 + 0x68),param_5,0);
  if (((param_2[0x25] == 0) ||
      (lVar9 = sub_85685bc0(param_2[0x25],*(undefined8 *)(param_4 + 0x68),uRam0000000187b57380),
      lVar9 == 0)) || (lVar9 = sub_855bdd60(lVar9,param_5,uRam0000000187bf7300), lVar9 == 0))
  goto code_r0x0001814be424;
  if ((int)param_2[0xc] == 0xc) {
    uVar17 = (undefined4)param_2[6];
  }
  else {
    uVar17 = *(undefined4 *)((longlong)param_2 + 0x2c);
  }
  uVar14 = 0;
  uVar19 = CONCAT71((int7)((ulonglong)in_stack_ffffffffffffff60 >> 8),*(int *)(lVar9 + 0x14) == 1);
  uVar10 = CONCAT71((int7)((ulonglong)in_stack_ffffffffffffff58 >> 8),uVar4);
  uStack_70 = 0;
  uVar13 = param_4;
  cVar5 = BasePartsData::ResistStatus(param_4,(int)param_2[0xc],uVar17,*(undefined8 *)(param_1 + 0x90),uVar10,
                        uVar19,param_4,0);
  uVar18 = (undefined4)((ulonglong)uVar10 >> 0x20);
  uVar21 = (undefined4)((ulonglong)uVar19 >> 0x20);
  uVar23 = (undefined4)(uVar13 >> 0x20);
  uVar13 = param_4;
  uVar15 = uVar14;
  if (cVar5 == '\0') {
    cVar5 = FEFJGFLKNLK::OLLAIGFLEBN(param_2,*(undefined8 *)(param_4 + 0x68),param_5,0);
    uVar18 = (undefined4)((ulonglong)uVar10 >> 0x20);
    uVar21 = (undefined4)((ulonglong)uVar19 >> 0x20);
    if ((cVar5 == '\0') && (param_8 == 0)) {
      uVar15 = 0;
    }
    else {
      bVar16 = param_8 != 0;
      if (bVar16) {
        *(undefined1 *)(param_2 + 0x26) = 1;
      }
      if (*(longlong *)(param_4 + 0x68) == 0) goto code_r0x0001814be424;
      lVar9 = *(longlong *)(*(longlong *)(param_4 + 0x68) + 0x38);
      cVar5 = *(char *)((longlong)param_2 + 0x1a6);
      bVar3 = false;
      if (*(int *)(lRam0000000187b77e20 + 0xe4) == 0) {
        sub_8032fa70(lRam0000000187b77e20);
      }
      lVar20 = *(longlong *)(*(longlong *)(lRam0000000187b77e20 + 0xb8) + 0x30);
      if (lVar20 == 0) goto code_r0x0001814be424;
      cVar6 = sub_855f08a0(lVar20,(int)param_2[0xc],uRam0000000187b44ce8);
      if (((cVar6 != '\0') && ((int)param_2[2] == 1)) && (param_9 == '\0' && cVar5 == '\0')) {
        if (((lVar9 == 0) || (lVar20 = *(longlong *)(lVar9 + 0x2a0), param_3 == 0)) ||
           (*(longlong *)(param_3 + 0x88) == 0)) goto code_r0x0001814be424;
        cVar5 = sub_85653020(*(longlong *)(param_3 + 0x88),param_4,uRam0000000187c4fe78);
        uVar18 = (undefined4)((ulonglong)uVar10 >> 0x20);
        if (cVar5 == '\0') {
          if (lVar20 != 0) {
            cVar5 = MGIFKJCBCKB::IFDPIEEMDKD(lVar20,0);
            uVar18 = (undefined4)((ulonglong)uVar10 >> 0x20);
            if (cVar5 != '\0') {
              uVar1 = *(undefined8 *)(param_4 + 0x68);
              uVar11 = sub_814a9040(param_1,0);
              uVar12 = sub_814a9080(param_1,0);
              uVar19 = 0;
              uVar10 = CONCAT44(uVar18,(int)param_2[0xc]);
              MGIFKJCBCKB::ODPPNGHCIBJ(lVar20,uVar1,uVar11,uVar12,uVar10,0);
              uVar13 = *(ulonglong *)(lVar20 + 0x30);
              if (*(longlong *)(param_3 + 0x88) == 0) goto code_r0x0001814be424;
              sub_85685e10(*(longlong *)(param_3 + 0x88),param_4,uVar13,uRam0000000187b2c388);
              goto code_r0x0001814be0fa;
            }
          }
        }
        else {
          uVar1 = *(undefined8 *)(param_4 + 0x68);
          uVar11 = sub_814a9040(param_1,0);
          uVar12 = sub_814a9080(param_1,0);
          if (lVar20 == 0) goto code_r0x0001814be424;
          uVar19 = 0;
          uVar10 = CONCAT44(uVar18,(int)param_2[0xc]);
          MGIFKJCBCKB::ODPPNGHCIBJ(lVar20,uVar1,uVar11,uVar12,uVar10,0);
          if (*(longlong *)(param_3 + 0x88) == 0) goto code_r0x0001814be424;
          uVar13 = sub_85685bc0(*(longlong *)(param_3 + 0x88),param_4,uRam0000000187b2c1d0);
code_r0x0001814be0fa:
          bVar3 = true;
          bVar16 = false;
          param_2[0x37] = param_4;
          sub_8032e8d0(param_2 + 0x37,param_4);
          *(undefined1 *)(param_2 + 0x26) = 1;
          uStack_70 = param_4;
          if (uVar13 == 0) goto code_r0x0001814be424;
        }
        if (param_2[0x25] == 0) goto code_r0x0001814be424;
        cVar5 = sub_85653020(param_2[0x25],*(undefined8 *)(uVar13 + 0x68),uRam0000000187b57018);
        if (cVar5 == '\0') {
          FEFJGFLKNLK::MNLBJHHAJHG(param_2,*(undefined8 *)(uVar13 + 0x68),param_5,0);
        }
      }
      uVar18 = (undefined4)((ulonglong)uVar10 >> 0x20);
      if (bVar16) {
        uStack_70 = param_8;
        if ((lVar9 == 0) || (lVar9 = *(longlong *)(lVar9 + 0x2a8), param_8 == 0))
        goto code_r0x0001814be424;
        uVar1 = *(undefined8 *)(param_8 + 0x68);
        if (cRam000000018ab626db == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626db = '\x01';
        }
        uVar11 = *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x18);
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        if (lVar9 == 0) goto code_r0x0001814be424;
        uVar19 = 0;
        uVar10 = CONCAT44(uVar18,(int)param_2[0xc]);
        MGIFKJCBCKB::ODPPNGHCIBJ(lVar9,uVar1,uVar11,
                      *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8),uVar10,0);
      }
      uVar15 = uStack_70;
      if (bVar3) {
        if (uVar13 == 0) goto code_r0x0001814be424;
        uVar19 = CONCAT71((int7)((ulonglong)uVar19 >> 8),1);
        uVar10 = CONCAT71((int7)((ulonglong)uVar10 >> 8),uVar4);
        uVar22 = uVar13;
        cVar5 = BasePartsData::ResistStatus(uVar13,(int)param_2[0xc],uVar17,*(undefined8 *)(uVar13 + 0x68),uVar10,
                              uVar19,uVar13,0);
        uVar18 = (undefined4)((ulonglong)uVar10 >> 0x20);
        uVar21 = (undefined4)((ulonglong)uVar19 >> 0x20);
        uVar23 = (undefined4)(uVar22 >> 0x20);
        if (cVar5 != '\0') goto code_r0x0001814be2c0;
      }
      lVar9 = param_1;
      lVar20 = param_3;
      (**(code **)(*param_2 + 600))
                (param_2,*(undefined8 *)(param_1 + 0x90),uVar13,param_5,param_1,param_3,
                 CONCAT44(uVar23,param_6),uVar8,lVar7,*(undefined8 *)(*param_2 + 0x260));
      uVar18 = (undefined4)((ulonglong)lVar9 >> 0x20);
      uVar21 = (undefined4)((ulonglong)lVar20 >> 0x20);
      *(undefined1 *)(param_2 + 0x26) = 0;
    }
  }
code_r0x0001814be2c0:
  if ((int)param_2[0xc] != 7) {
    if (param_2[0x24] == 0) goto code_r0x0001814be424;
    cVar5 = sub_85653020(param_2[0x24],uVar13,uRam0000000187b2c8a0);
    if (cVar5 != '\0') {
      if (param_2[0xd] == 0) {
code_r0x0001814be424:
        sub_8032f9d0();
        pcVar2 = (code *)swi(3);
        (*pcVar2)();
        return;
      }
      if (param_5 != *(int *)(param_2[0xd] + 0x18) + -1) {
        return;
      }
      if (param_2[0x24] == 0) goto code_r0x0001814be424;
      cVar5 = sub_85675470(param_2[0x24],uVar13,uRam0000000187b2ca58);
      if (cVar5 == '\0') {
        while( true ) {
          lVar7 = param_2[0x11];
          if (lVar7 == 0) break;
          if (*(int *)(lVar7 + 0x18) <= (int)uVar14) {
            return;
          }
          if (param_3 == 0) break;
          lVar9 = *(longlong *)(param_3 + 0x20);
          uVar17 = sub_84a610f0(lVar7,uVar14,uRam0000000187b446b8);
          if ((lVar9 == 0) || (lVar7 = sub_849ed2d0(lVar9,uVar17,uRam0000000187c19e18), lVar7 == 0)
             ) break;
          if (*(longlong *)(lVar7 + 0xd0) != 0) {
            lVar7 = *(longlong *)(lVar7 + 0xd0);
            (**(code **)(lVar7 + 0x18))(*(undefined8 *)(lVar7 + 0x40),*(undefined8 *)(lVar7 + 0x28))
            ;
          }
          uVar14 = (ulonglong)((int)uVar14 + 1);
        }
        goto code_r0x0001814be424;
      }
    }
    UnitActionController::ExecChildrenAction(param_1,param_2,param_3,uVar13,CONCAT44(uVar18,param_5),CONCAT44(uVar21,param_6),
                  uVar8,param_7,uVar15,0);
  }
  return;
}



/* ==== UnitActionController::ExecActionOnStart  @ 0x14bd520  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001814bd6d2: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814bd729: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x0001814bd6d7) */
/* WARNING: Removing unreachable block (ram,0x0001814bd6e4) */
/* WARNING: Removing unreachable block (ram,0x0001814bd718) */
/* WARNING: Removing unreachable block (ram,0x0001814bd72e) */

ulonglong UnitActionController::ExecActionOnStart(longlong param_1,longlong param_2)

{
  ulonglong *puVar1;
  code *pcVar2;
  longlong *plVar3;
  longlong lVar4;
  undefined8 uVar5;
  ulonglong uVar6;
  ulonglong uVar7;
  int iVar8;
  uint uVar9;
  bool bVar10;
  
  if (cRam000000018ab6269b == '\0') {
    sub_8032f6d0(0x187b695b8);
    sub_8032f6d0(0x187b58b58);
    sub_8032f6d0(0x187b59568);
    sub_8032f6d0(0x187b715a8);
    sub_8032f6d0(0x187b7f908);
    sub_8032f6d0(0x187b86358);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b86510);
    sub_8032f6d0(0x187c32990);
    sub_8032f6d0(0x187bc5830);
    cRam000000018ab6269b = '\x01';
  }
  iVar8 = 0;
  if (param_2 != 0) {
    while (lVar4 = *(longlong *)(param_2 + 0x20), lVar4 != 0) {
      if (*(int *)(lVar4 + 0x18) <= iVar8) {
        lVar4 = *(longlong *)(param_2 + 0x30);
        if (lVar4 == 0) break;
        if (*(int *)(lVar4 + 0x18) < 1) {
          uVar5 = sub_8032f970(uRam0000000187bc5830);
          uVar6 = sub_849de5b0(uVar5,uRam0000000187b7f908);
          uVar7 = param_2 + 0x148;
          *(undefined8 *)(param_2 + 0x148) = uVar5;
        }
        else {
          lVar4 = sub_849ed2d0(lVar4,0,uRam0000000187b86510);
          uVar5 = sub_8032f970(uRam0000000187b59568);
          uVar6 = sub_85660990(uVar5,uRam0000000187b58b58);
          if (lVar4 == 0) break;
          uVar7 = lVar4 + 0xa8;
          *(undefined8 *)(lVar4 + 0xa8) = uVar5;
        }
        if (iRam000000018ab8fbd8 != 0) {
          uVar9 = (uint)(uVar7 >> 0xc);
          puVar1 = (ulonglong *)((ulonglong)((uVar9 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
          do {
            uVar7 = *puVar1;
            LOCK();
            uVar6 = *puVar1;
            bVar10 = uVar7 == uVar6;
            if (bVar10) {
              *puVar1 = uVar7 | 1L << (uVar9 & 0x3f);
              uVar6 = uVar7;
            }
            UNLOCK();
          } while (!bVar10);
        }
        return uVar6;
      }
      plVar3 = (longlong *)sub_849ed2d0(lVar4,iVar8,uRam0000000187c19e18);
      if (plVar3 == (longlong *)0x0) break;
      if ((*(byte *)(lRam0000000187b695b8 + 0x130) <= *(byte *)(*plVar3 + 0x130)) &&
         (*(longlong *)
           (*(longlong *)(*plVar3 + 200) + -8 +
           (ulonglong)*(byte *)(lRam0000000187b695b8 + 0x130) * 8) == lRam0000000187b695b8)) {
        *(undefined1 *)(param_2 + 0x140) = 1;
      }
      sub_80015bc0(8,plVar3,param_2,*(undefined8 *)(param_1 + 0x90),param_1);
      iVar8 = iVar8 + 1;
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar7 = (*pcVar2)();
  return uVar7;
}



/* ==== UnitActionController::ExecActionOnStart  @ 0x14bd3f0  size=1 ==== */

void UnitActionController::ExecActionOnStart(longlong param_1)

{
  code *pcVar1;
  char cVar2;
  undefined8 uStack_60;
  undefined8 uStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  undefined8 uStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab626c9 == '\0') {
    sub_8032f6d0(0x187c42000);
    sub_8032f6d0(0x187c0ed88);
    sub_8032f6d0(0x187c0ef40);
    sub_8032f6d0(0x187c0f0f0);
    sub_8032f6d0(0x187bd6490);
    cRam000000018ab626c9 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    sub_855337a0(&uStack_38,*(longlong *)(param_1 + 0xb0),uRam0000000187c42000);
    uStack_60 = uStack_38;
    uStack_58 = uStack_30;
    uStack_50 = uStack_28;
    uStack_48 = uStack_20;
    uStack_40 = uStack_18;
    while( true ) {
      cVar2 = sub_85760f30(&uStack_60,uRam0000000187c0ef40);
      if (cVar2 == '\0') break;
      UnitActionController::ExecActionOnStart(param_1,uStack_48,0);
    }
    sub_80443e50(&uStack_60,uRam0000000187c0ed88);
    return;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitActionController::ExecActionOnWaveStart  @ 0x14bd790  size=1 ==== */

void UnitActionController::ExecActionOnWaveStart(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  longlong lVar3;
  char cVar4;
  longlong *plVar5;
  int iVar6;
  undefined8 uStack_60;
  undefined8 uStack_58;
  undefined8 uStack_50;
  longlong lStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  undefined8 uStack_28;
  longlong lStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab626ca == '\0') {
    sub_8032f6d0(0x187c42000);
    sub_8032f6d0(0x187c0ed88);
    sub_8032f6d0(0x187c0ef40);
    sub_8032f6d0(0x187c0f0f0);
    sub_8032f6d0(0x187bd6490);
    cRam000000018ab626ca = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  sub_855337a0(&uStack_38,*(longlong *)(param_1 + 0xb0),uRam0000000187c42000);
  uStack_60 = uStack_38;
  uStack_58 = uStack_30;
  uStack_50 = uStack_28;
  lStack_48 = lStack_20;
  uStack_40 = uStack_18;
code_r0x0001814bd851:
  cVar4 = sub_85760f30(&uStack_60,uRam0000000187c0ef40);
  lVar3 = lStack_48;
  if (cVar4 == '\0') {
    sub_80443e50(&uStack_60,uRam0000000187c0ed88);
    return;
  }
  if (cRam000000018ab6269c == '\0') {
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab6269c = '\x01';
  }
  iVar6 = 0;
  if (lVar3 == 0) {
code_r0x0001814bd951:
    sub_8032f9d0();
    sub_8032b6b0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  do {
    lVar1 = *(longlong *)(lVar3 + 0x20);
    if (lVar1 == 0) {
code_r0x0001814bd94c:
      sub_8032f9d0();
      goto code_r0x0001814bd951;
    }
    if (*(int *)(lVar1 + 0x18) <= iVar6) goto code_r0x0001814bd851;
    plVar5 = (longlong *)sub_849ed2d0(lVar1,iVar6,uRam0000000187c19e18);
    if (plVar5 == (longlong *)0x0) {
      sub_8032f9d0(0);
      goto code_r0x0001814bd94c;
    }
    (**(code **)(*plVar5 + 0x228))
              (plVar5,lVar3,*(undefined8 *)(param_1 + 0x90),param_1,*(undefined8 *)(*plVar5 + 0x230)
              );
    iVar6 = iVar6 + 1;
  } while( true );
}



/* ==== UnitActionController::ExecActionWithDelayAndTarget  @ 0x14bd970  size=1 ==== */

longlong UnitActionController::ExecActionWithDelayAndTarget(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4,
                      undefined4 param_5,undefined1 param_6,undefined1 param_7,undefined1 param_8,
                      undefined8 param_9)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626af == '\0') {
    sub_8032f6d0(0x187c210b0);
    cRam000000018ab626af = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c210b0);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x40) = param_1;
    sub_8032e8d0(lVar2 + 0x40,param_1);
    *(undefined8 *)(lVar2 + 0x20) = param_2;
    sub_8032e8d0(lVar2 + 0x20,param_2);
    *(undefined8 *)(lVar2 + 0x38) = param_3;
    sub_8032e8d0(lVar2 + 0x38,param_3);
    *(undefined8 *)(lVar2 + 0x28) = param_4;
    sub_8032e8d0(lVar2 + 0x28,param_4);
    *(undefined1 *)(lVar2 + 0x31) = param_6;
    *(undefined1 *)(lVar2 + 0x30) = param_7;
    *(undefined1 *)(lVar2 + 0x48) = param_8;
    *(undefined4 *)(lVar2 + 0x34) = param_5;
    *(undefined8 *)(lVar2 + 0x50) = param_9;
    sub_8032e8d0(lVar2 + 0x50);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::ExecChildrenAction  @ 0x14be430  size=1 ==== */

void UnitActionController::ExecChildrenAction(longlong param_1,longlong param_2,longlong param_3,ulonglong param_4,int param_5,
                  uint param_6,longlong param_7,char param_8,ulonglong param_9)

{
  int iVar1;
  longlong lVar2;
  code *pcVar3;
  longlong *plVar4;
  char cVar5;
  char cVar6;
  undefined4 uVar7;
  longlong lVar8;
  undefined8 uVar9;
  ulonglong uVar10;
  ulonglong uVar11;
  ulonglong uVar12;
  ulonglong in_stack_ffffffffffffff08;
  uint in_stack_ffffffffffffff10;
  ulonglong in_stack_ffffffffffffff20;
  ulonglong in_stack_ffffffffffffff28;
  uint uStack_c8;
  ulonglong uStack_c0;
  undefined8 uStack_a0;
  undefined8 uStack_98;
  longlong *plStack_90;
  longlong lStack_88;
  undefined4 uStack_78;
  undefined4 uStack_74;
  undefined4 uStack_70;
  undefined4 uStack_6c;
  longlong *plStack_68;
  
  if (cRam000000018ab626b1 == '\0') {
    sub_8032f6d0(0x187bfe0f8);
    sub_8032f6d0(0x187b44ce8);
    sub_8032f6d0(0x187bfe990);
    sub_8032f6d0(0x187b4f980);
    sub_8032f6d0(0x187b4fb30);
    sub_8032f6d0(0x187b4fce8);
    sub_8032f6d0(0x187b71920);
    sub_8032f6d0(0x187b71e50);
    sub_8032f6d0(0x187b9e928);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187b77e20);
    cRam000000018ab626b1 = '\x01';
  }
  uStack_a0 = 0;
  uStack_98 = 0;
  plStack_90 = (longlong *)0x0;
  uVar12 = 0;
  uStack_c8 = 0;
  if ((param_2 == 0) || (*(longlong *)(param_2 + 0x88) == 0)) {
code_r0x0001814bea9c:
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  iVar1 = *(int *)(*(longlong *)(param_2 + 0x88) + 0x18);
  uVar10 = uVar12;
code_r0x0001814be570:
  if (iVar1 <= (int)uVar10) {
    return;
  }
  uStack_c0 = 0;
  if (param_3 == 0) goto code_r0x0001814bea9c;
  lVar8 = *(longlong *)(param_3 + 0x20);
  if ((*(longlong *)(param_2 + 0x88) == 0) ||
     (uVar7 = sub_84a610f0(*(longlong *)(param_2 + 0x88),uVar10,uRam0000000187b446b8), lVar8 == 0))
  goto code_r0x0001814bea9c;
  lVar8 = sub_849ed2d0(lVar8,uVar7,uRam0000000187c19e18);
  lStack_88 = lVar8;
  if (param_9 == 0) {
    uStack_c0 = 0;
    uVar10 = param_4;
    if (lVar8 == 0) goto code_r0x0001814bea9c;
  }
  else {
    if (lVar8 == 0) goto code_r0x0001814bea9c;
    cVar6 = *(char *)(lVar8 + 0x1a6);
    if (*(int *)(lRam0000000187b77e20 + 0xe4) == 0) {
      sub_8032fa70(lRam0000000187b77e20);
    }
    lVar2 = *(longlong *)(*(longlong *)(lRam0000000187b77e20 + 0xb8) + 0x30);
    if (lVar2 == 0) goto code_r0x0001814bea9c;
    cVar5 = sub_855f08a0(lVar2,*(undefined4 *)(lVar8 + 0x60),uRam0000000187b44ce8);
    uVar10 = param_9;
    if (cVar5 != '\0' && cVar6 == '\0') {
      uStack_c0 = param_9;
      uVar10 = param_4;
    }
  }
  param_4 = uVar10;
  if ((*(int *)(lVar8 + 0x60) != 0xe) || (*(int *)(lVar8 + 0x2c) != 3)) {
    if (param_7 == 0) goto code_r0x0001814bea9c;
    cVar6 = sub_85568500(param_7,*(undefined4 *)(lVar8 + 0x38),uRam0000000187bfe0f8);
    if ((cVar6 == '\0') ||
       (cVar6 = sub_85579340(param_7,*(undefined4 *)(lVar8 + 0x38),uRam0000000187bfe990),
       cVar6 != '\0')) {
      if (*(int *)(param_2 + 0x60) == 1) {
        if (*(longlong *)(param_2 + 0x68) == 0) goto code_r0x0001814bea9c;
        if (param_5 != *(int *)(*(longlong *)(param_2 + 0x68) + 0x18) + -1)
        goto code_r0x0001814bea4f;
      }
      if (*(int *)(lVar8 + 0x14) == 0x2a) {
        if ((uVar10 == 0) || (*(longlong *)(uVar10 + 0x68) == 0)) goto code_r0x0001814bea9c;
        cVar6 = UnitCtrl::get_IsPartsBoss(*(longlong *)(uVar10 + 0x68),0);
        if (cVar6 != '\0') {
          if (*(longlong *)(lVar8 + 0x1a8) == 0) goto code_r0x0001814bea9c;
          cVar6 = sub_84a98ab0(*(longlong *)(lVar8 + 0x1a8),*(undefined8 *)(uVar10 + 0x68),
                                uRam0000000187b71e50);
          if (cVar6 == '\0') {
            if (((*(longlong *)(uVar10 + 0x68) != 0) &&
                (lVar2 = *(longlong *)(*(longlong *)(uVar10 + 0x68) + 0x80), lVar2 != 0)) &&
               (lVar2 = *(longlong *)(lVar2 + 0x18), lVar2 != 0)) {
              sub_849e2810(&uStack_78,lVar2,uRam0000000187b9e928);
              uStack_a0 = CONCAT44(uStack_74,uStack_78);
              uStack_98 = CONCAT44(uStack_6c,uStack_70);
              plStack_90 = plStack_68;
code_r0x0001814be7b0:
              do {
                cVar6 = sub_8572bc70(&uStack_a0,uRam0000000187b4fb30);
                plVar4 = plStack_90;
                if (cVar6 == '\0') goto code_r0x0001814be8c9;
                if (plStack_90 == (longlong *)0x0) goto code_r0x0001814bea90;
                cVar6 = (**(code **)(*plStack_90 + 0x398))
                                  (plStack_90,*(undefined8 *)(*plStack_90 + 0x3a0));
                if (cVar6 != '\0') {
                  if (param_8 == '\0') {
                    in_stack_ffffffffffffff20 = in_stack_ffffffffffffff20 & 0xffffffffffffff00;
                    in_stack_ffffffffffffff10 = in_stack_ffffffffffffff10 & 0xffffff00;
                    in_stack_ffffffffffffff28 = uStack_c0;
                    uVar9 = UnitActionController::ExecActionWithDelayAndTarget(param_1,lVar8,param_3,plVar4,param_6,
                                          in_stack_ffffffffffffff10,1,in_stack_ffffffffffffff20,
                                          uStack_c0,0);
                    uVar11 = uVar12;
                    if (0.0 < *(float *)(param_3 + 0xb0)) {
                      uVar11 = *(ulonglong *)(param_1 + 0x90);
                    }
                    if (param_1 != 0) {
                      in_stack_ffffffffffffff08 = 0;
                      UnitActionController::AppendCoroutine(param_1,uVar9,1,uVar11,0);
                      goto code_r0x0001814be7b0;
                    }
                  }
                  else {
                    if (lVar8 != 0) {
                      FEFJGFLKNLK::MNLBJHHAJHG(lVar8,*(undefined8 *)(uVar10 + 0x68),0,0);
                      in_stack_ffffffffffffff28 = in_stack_ffffffffffffff28 & 0xffffffffffffff00;
                      in_stack_ffffffffffffff08 = in_stack_ffffffffffffff08 & 0xffffffff00000000;
                      in_stack_ffffffffffffff10 = param_6;
                      in_stack_ffffffffffffff20 = uStack_c0;
                      UnitActionController::ExecAction(param_1,lVar8,param_3,uVar10,in_stack_ffffffffffffff08,param_6,1
                                    ,uStack_c0,in_stack_ffffffffffffff28,0);
                      goto code_r0x0001814be7b0;
                    }
                    sub_8032f9d0();
                  }
                  sub_8032f9d0();
code_r0x0001814bea90:
                  sub_8032f9d0();
                  sub_8032b6b0();
                  pcVar3 = (code *)swi(3);
                  (*pcVar3)();
                  return;
                }
              } while( true );
            }
            goto code_r0x0001814bea9c;
          }
        }
      }
      else {
        if (param_8 != '\0') {
          if (uVar10 == 0) goto code_r0x0001814bea9c;
          FEFJGFLKNLK::MNLBJHHAJHG(lVar8,*(undefined8 *)(uVar10 + 0x68),0,0);
          in_stack_ffffffffffffff28 = in_stack_ffffffffffffff28 & 0xffffffffffffff00;
          in_stack_ffffffffffffff08 = in_stack_ffffffffffffff08 & 0xffffffff00000000;
          in_stack_ffffffffffffff10 = param_6;
          UnitActionController::ExecAction(param_1,lVar8,param_3,uVar10,in_stack_ffffffffffffff08,param_6,1,uStack_c0,
                        in_stack_ffffffffffffff28,0);
          uStack_c8 = uStack_c8 + 1;
          uVar10 = (ulonglong)uStack_c8;
          in_stack_ffffffffffffff20 = uStack_c0;
          goto code_r0x0001814be570;
        }
        in_stack_ffffffffffffff20 = in_stack_ffffffffffffff20 & 0xffffffffffffff00;
        in_stack_ffffffffffffff10 = in_stack_ffffffffffffff10 & 0xffffff00;
        uVar9 = UnitActionController::ExecActionWithDelayAndTarget(param_1,lVar8,param_3,uVar10,param_6,in_stack_ffffffffffffff10,1,
                              in_stack_ffffffffffffff20,uStack_c0,0);
        uVar10 = uVar12;
        if (0.0 < *(float *)(param_3 + 0xb0)) {
          uVar10 = *(ulonglong *)(param_1 + 0x90);
        }
        if (param_1 == 0) goto code_r0x0001814bea9c;
        in_stack_ffffffffffffff08 = 0;
        UnitActionController::AppendCoroutine(param_1,uVar9,1,uVar10,0);
        in_stack_ffffffffffffff28 = uStack_c0;
      }
    }
  }
code_r0x0001814bea4f:
  uStack_c8 = uStack_c8 + 1;
  uVar10 = (ulonglong)uStack_c8;
  goto code_r0x0001814be570;
code_r0x0001814be8c9:
  sub_80443e50(&uStack_a0,uRam0000000187b4f980);
  if ((uVar10 == 0) || (*(longlong *)(lVar8 + 0x1a8) == 0)) goto code_r0x0001814bea9c;
  sub_80002460(*(longlong *)(lVar8 + 0x1a8),*(undefined8 *)(uVar10 + 0x68),uRam0000000187b71920);
  uStack_c8 = uStack_c8 + 1;
  uVar10 = (ulonglong)uStack_c8;
  goto code_r0x0001814be570;
}



/* ==== UnitActionController::ExecExSkill  @ 0x14beab0  size=1 ==== */

void UnitActionController::ExecExSkill(longlong param_1,longlong param_2)

{
  code *pcVar1;
  undefined8 uVar2;
  char cVar3;
  longlong lVar4;
  int iVar5;
  undefined1 auStackX_20 [8];
  undefined8 uStack_60;
  undefined8 uStack_58;
  longlong *plStack_50;
  undefined4 uStack_48;
  undefined4 uStack_44;
  undefined4 uStack_40;
  undefined4 uStack_3c;
  longlong *plStack_38;
  
  if (cRam000000018ab626d0 == '\0') {
    sub_8032f6d0(0x187c4c378);
    sub_8032f6d0(0x187c4c530);
    sub_8032f6d0(0x187c4c6e0);
    sub_8032f6d0(0x187b42140);
    sub_8032f6d0(0x187c19ab0);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab626d0 = '\x01';
  }
  auStackX_20[0] = 0;
  iVar5 = 0;
  UnitActionController::actionStartFirstProcess(param_1,param_2,auStackX_20,0,1,0);
  if (param_2 != 0) {
    SkillBase::ReadySkill(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar4 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar4 != 0)) {
      uVar2 = CLKMBEGHOLA::ANKFGOIFFHH(lVar4,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar2;
      if (*(longlong *)(param_2 + 0x20) != 0) {
        sub_849e2810(&uStack_60,*(longlong *)(param_2 + 0x20),uRam0000000187c19ab0);
        uStack_48 = (undefined4)uStack_60;
        uStack_44 = uStack_60._4_4_;
        uStack_40 = (undefined4)uStack_58;
        uStack_3c = uStack_58._4_4_;
        plStack_38 = plStack_50;
        uStack_60 = 0;
        uStack_58 = &uStack_48;
        while( true ) {
          cVar3 = sub_8572bc70(&uStack_48,uRam0000000187c4c530);
          if (cVar3 == '\0') break;
          if (plStack_38 == (longlong *)0x0) {
            sub_8032f9d0();
            sub_8032b6b0();
            goto code_r0x0001814bedff;
          }
          (**(code **)(*plStack_38 + 0x218))
                    (plStack_38,*(undefined8 *)(param_1 + 0x90),param_1,param_2,
                     *(undefined8 *)(*plStack_38 + 0x220));
        }
        sub_80443e50(&uStack_48,uRam0000000187c4c378);
        if (*(longlong *)(param_2 + 0x20) != 0) {
          sub_849e2810(&uStack_60,*(longlong *)(param_2 + 0x20),uRam0000000187c19ab0);
          uStack_48 = (undefined4)uStack_60;
          uStack_44 = uStack_60._4_4_;
          uStack_40 = (undefined4)uStack_58;
          uStack_3c = uStack_58._4_4_;
          plStack_38 = plStack_50;
          uStack_60 = 0;
          uStack_58 = &uStack_48;
          while( true ) {
            cVar3 = sub_8572bc70(&uStack_48,uRam0000000187c4c530);
            if (cVar3 == '\0') break;
            if (plStack_38 == (longlong *)0x0) {
code_r0x0001814bedff:
              sub_8032f9d0();
              sub_8032b6b0();
              pcVar1 = (code *)swi(3);
              (*pcVar1)();
              return;
            }
            if ((int)plStack_38[0xc] == 0x1c) {
              UnitActionController::execActionConsiderTargetNum(param_1,param_2,plStack_38,0);
            }
          }
          sub_80443e50(&uStack_48,uRam0000000187c4c378);
          while (*(longlong *)(param_2 + 0x20) != 0) {
            if (*(int *)(*(longlong *)(param_2 + 0x20) + 0x18) <= iVar5) {
              return;
            }
            lVar4 = sub_849ed2d0(*(undefined8 *)(param_2 + 0x20),iVar5,uRam0000000187c19e18);
            if (lVar4 == 0) break;
            if ((*(int *)(lVar4 + 0x60) != 0x1c) && (*(char *)(lVar4 + 0xe8) == '\0')) {
              if (*(longlong *)(param_2 + 0xa8) == 0) break;
              cVar3 = sub_84a7f390(*(longlong *)(param_2 + 0xa8),iVar5,uRam0000000187b42140);
              if (((cVar3 == '\0') || (*(char *)(lVar4 + 0x86) != '\0')) &&
                 (*(char *)(lVar4 + 0x85) == '\0')) {
                UnitActionController::execActionConsiderTargetNum(param_1,param_2,lVar4,0);
              }
            }
            iVar5 = iVar5 + 1;
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



/* ==== UnitActionController::ExecSkillWithTargetId  @ 0x14bee20  size=1 ==== */

void UnitActionController::ExecSkillWithTargetId(longlong param_1,longlong param_2,int param_3)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  longlong *plVar4;
  int iVar5;
  undefined8 uVar6;
  undefined1 auStackX_10 [8];
  ulonglong in_stack_ffffffffffffffd8;
  
  if (cRam000000018ab626d1 == '\0') {
    sub_8032f6d0(0x187b42140);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab626d1 = '\x01';
  }
  iVar5 = 0;
  auStackX_10[0] = 0;
  UnitActionController::actionStartFirstProcess(param_1,param_2,auStackX_10,1,in_stack_ffffffffffffffd8 & 0xffffffffffffff00,0);
  if (param_2 != 0) {
    SkillBase::ReadySkill(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar1 != 0)) {
      uVar6 = CLKMBEGHOLA::ANKFGOIFFHH(lVar1,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar6;
      while( true ) {
        lVar1 = *(longlong *)(param_2 + 0x20);
        if (lVar1 == 0) break;
        if (*(int *)(lVar1 + 0x18) <= iVar5) {
          return;
        }
        plVar4 = (longlong *)sub_849ed2d0(lVar1,iVar5,uRam0000000187c19e18);
        if (plVar4 == (longlong *)0x0) break;
        (**(code **)(*plVar4 + 0x218))
                  (plVar4,*(undefined8 *)(param_1 + 0x90),param_1,param_2,
                   *(undefined8 *)(*plVar4 + 0x220));
        if (*(longlong *)(param_2 + 0xa8) == 0) break;
        cVar3 = sub_84a7f390(*(longlong *)(param_2 + 0xa8),iVar5,uRam0000000187b42140);
        if ((((cVar3 == '\0') || (*(char *)((longlong)plVar4 + 0x86) != '\0')) &&
            (*(char *)((longlong)plVar4 + 0x85) == '\0')) && (param_3 == (int)plVar4[7])) {
          UnitActionController::execActionConsiderTargetNum(param_1,param_2,plVar4,0);
        }
        iVar5 = iVar5 + 1;
      }
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::ExecSkillWithTargetParts  @ 0x14bf390  size=1 ==== */

void UnitActionController::ExecSkillWithTargetParts(longlong param_1,longlong param_2,longlong param_3)

{
  longlong lVar1;
  code *pcVar2;
  undefined8 uVar3;
  longlong *plVar4;
  char cVar5;
  longlong lVar6;
  int iVar7;
  undefined8 in_stack_fffffffffffffef8;
  ulonglong uVar8;
  ulonglong uVar9;
  undefined1 auStack_d8 [4];
  int iStack_d4;
  undefined8 uStack_d0;
  undefined8 uStack_c8;
  longlong *plStack_c0;
  undefined8 uStack_b8;
  undefined8 uStack_b0;
  longlong lStack_a8;
  undefined8 uStack_a0;
  undefined4 *puStack_98;
  undefined4 uStack_90;
  undefined4 uStack_8c;
  undefined4 uStack_88;
  undefined4 uStack_84;
  longlong lStack_80;
  undefined4 uStack_78;
  undefined4 uStack_74;
  undefined4 uStack_70;
  undefined4 uStack_6c;
  longlong *plStack_68;
  
  if (cRam000000018ab626d4 == '\0') {
    sub_8032f6d0(0x187c4c378);
    sub_8032f6d0(0x187c2fce0);
    sub_8032f6d0(0x187c2fe90);
    sub_8032f6d0(0x187c4c530);
    sub_8032f6d0(0x187c30048);
    sub_8032f6d0(0x187c4c6e0);
    sub_8032f6d0(0x187b42140);
    sub_8032f6d0(0x187b806e0);
    sub_8032f6d0(0x187c19ab0);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab626d4 = '\x01';
  }
  auStack_d8[0] = 0;
  uStack_b8 = 0;
  uStack_b0 = 0;
  lStack_a8 = 0;
  iVar7 = 0;
  uVar9 = 0;
  uVar8 = CONCAT71((int7)((ulonglong)in_stack_fffffffffffffef8 >> 8),1);
  UnitActionController::actionStartFirstProcess(param_1,param_2,auStack_d8,0,uVar8,0);
  if (param_2 != 0) {
    SkillBase::ReadySkill(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar6 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar6 != 0)) {
      uVar3 = CLKMBEGHOLA::ANKFGOIFFHH(lVar6,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar3;
      if (*(longlong *)(param_2 + 0x20) != 0) {
        sub_849e2810(&uStack_d0,*(longlong *)(param_2 + 0x20),uRam0000000187c19ab0);
        uStack_78 = (undefined4)uStack_d0;
        uStack_74 = uStack_d0._4_4_;
        uStack_70 = (undefined4)uStack_c8;
        uStack_6c = uStack_c8._4_4_;
        plStack_68 = plStack_c0;
        uStack_a0 = 0;
        puStack_98 = &uStack_78;
code_r0x0001814bf530:
        cVar5 = sub_8572bc70(&uStack_78,uRam0000000187c4c530);
        plVar4 = plStack_68;
        if (cVar5 != '\0') {
          if (plStack_68 == (longlong *)0x0) goto code_r0x0001814bf9db;
          uVar8 = *(ulonglong *)(*plStack_68 + 0x220);
          (**(code **)(*plStack_68 + 0x218))
                    (plStack_68,*(undefined8 *)(param_1 + 0x90),param_1,param_2,uVar8);
          if (plVar4 != (longlong *)0x0) goto code_r0x0001814bf58f;
          goto code_r0x0001814bf9d6;
        }
        sub_80443e50(puStack_98,uRam0000000187c4c378);
        while( true ) {
          lVar6 = *(longlong *)(param_2 + 0x20);
          iStack_d4 = iVar7;
          if (lVar6 == 0) break;
          if (*(int *)(lVar6 + 0x18) <= iVar7) {
            return;
          }
          lVar6 = sub_849ed2d0(lVar6,iVar7,uRam0000000187c19e18);
          if (lVar6 == 0) break;
          if ((*(int *)(lVar6 + 0x60) != 0x1c) && (*(char *)(lVar6 + 0xe8) == '\0')) {
            if (*(longlong *)(param_2 + 0xa8) == 0) break;
            cVar5 = sub_84a7f390(*(longlong *)(param_2 + 0xa8),iVar7,uRam0000000187b42140);
            if (((cVar5 == '\0') || (*(char *)(lVar6 + 0x86) != '\0')) &&
               (*(char *)(lVar6 + 0x85) == '\0')) {
              if (*(int *)(lVar6 + 0x10) == 1) {
                if (((param_3 == 0) || (*(longlong *)(param_3 + 0x68) == 0)) ||
                   (lVar1 = *(longlong *)(*(longlong *)(param_3 + 0x68) + 0x38), lVar1 == 0)) break;
                if (*(char *)(lVar1 + 0x260) == '\0') {
                  cVar5 = CLKMBEGHOLA::BGPIDFLHLGC(lVar1,*(undefined8 *)(param_1 + 0x90),0);
                  if (cVar5 == '\0') {
                    FEFJGFLKNLK::MNLBJHHAJHG(lVar6,*(undefined8 *)(param_3 + 0x68),0,0);
                    uVar9 = uVar9 & 0xffffffff00000000;
                    uVar8 = uVar8 & 0xffffffff00000000;
                    UnitActionController::ExecAction(param_1,lVar6,param_2,param_3,uVar8,uVar9,1,0,0,0);
                  }
                }
              }
              else {
                if (*(longlong *)(lVar6 + 0x50) == 0) break;
                sub_849e2810(&uStack_90,*(longlong *)(lVar6 + 0x50),uRam0000000187b806e0);
                uStack_b8 = CONCAT44(uStack_8c,uStack_90);
                uStack_b0 = CONCAT44(uStack_84,uStack_88);
                lStack_a8 = lStack_80;
                uStack_d0 = 0;
                uStack_c8 = &uStack_b8;
                while( true ) {
                  cVar5 = sub_8572bc70(&uStack_b8,uRam0000000187c2fe90);
                  lVar1 = lStack_a8;
                  if (cVar5 == '\0') break;
                  if (lStack_a8 == 0) goto code_r0x0001814bf9e7;
                  FEFJGFLKNLK::MNLBJHHAJHG(lVar6,*(undefined8 *)(lStack_a8 + 0x68),0,0);
                  uVar9 = uVar9 & 0xffffffff00000000;
                  uVar8 = uVar8 & 0xffffffff00000000;
                  UnitActionController::ExecAction(param_1,lVar6,param_2,lVar1,uVar8,uVar9,1,0,0,0);
                }
                sub_80443e50(&uStack_b8,uRam0000000187c2fce0);
              }
            }
          }
          iVar7 = iVar7 + 1;
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
code_r0x0001814bf58f:
  if ((int)plVar4[0xc] == 0x1c) {
    if ((int)plVar4[2] == 1) {
      if (param_3 != 0) {
        FEFJGFLKNLK::MNLBJHHAJHG(plVar4,*(undefined8 *)(param_3 + 0x68),0,0);
        uVar9 = uVar9 & 0xffffffff00000000;
        uVar8 = uVar8 & 0xffffffff00000000;
        UnitActionController::ExecAction(param_1,plVar4,param_2,param_3,uVar8,uVar9,1,0,0,0);
        goto code_r0x0001814bf530;
      }
    }
    else {
      if (plVar4[10] != 0) {
        sub_849e2810(&uStack_90,plVar4[10],uRam0000000187b806e0);
        uStack_b8 = CONCAT44(uStack_8c,uStack_90);
        uStack_b0 = CONCAT44(uStack_84,uStack_88);
        lStack_a8 = lStack_80;
        uStack_d0 = 0;
        uStack_c8 = &uStack_b8;
        while( true ) {
          cVar5 = sub_8572bc70(&uStack_b8,uRam0000000187c2fe90);
          lVar6 = lStack_a8;
          if (cVar5 == '\0') break;
          if (lStack_a8 == 0) {
            sub_8032f9d0();
            sub_8032b6b0();
            goto code_r0x0001814bf9cc;
          }
          FEFJGFLKNLK::MNLBJHHAJHG(plVar4,*(undefined8 *)(lStack_a8 + 0x68),0,0);
          uVar9 = uVar9 & 0xffffffff00000000;
          uVar8 = uVar8 & 0xffffffff00000000;
          UnitActionController::ExecAction(param_1,plVar4,param_2,lVar6,uVar8,uVar9,1,0,0,0);
        }
        sub_80443e50(&uStack_b8,uRam0000000187c2fce0);
        goto code_r0x0001814bf530;
      }
code_r0x0001814bf9cc:
      sub_8032f9d0();
    }
    sub_8032f9d0();
code_r0x0001814bf9d6:
    sub_8032f9d0();
code_r0x0001814bf9db:
    sub_8032f9d0();
    sub_8032b6b0();
code_r0x0001814bf9e7:
    sub_8032f9d0();
    sub_8032b6b0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  goto code_r0x0001814bf530;
}



/* ==== UnitActionController::ExecSkillWithTargetPartsAndActionList  @ 0x14befa0  size=1 ==== */

void UnitActionController::ExecSkillWithTargetPartsAndActionList(longlong param_1,longlong param_2,longlong param_3,longlong param_4,char param_5)

{
  longlong lVar1;
  code *pcVar2;
  undefined8 uVar3;
  int iVar4;
  char cVar5;
  longlong *plVar6;
  ulonglong uVar7;
  ulonglong uVar8;
  undefined1 auStack_98 [4];
  int iStack_94;
  undefined8 uStack_88;
  undefined8 *puStack_80;
  undefined8 uStack_78;
  undefined8 uStack_70;
  longlong lStack_68;
  undefined4 uStack_60;
  undefined4 uStack_5c;
  undefined4 uStack_58;
  undefined4 uStack_54;
  longlong lStack_50;
  
  if (cRam000000018ab626d3 == '\0') {
    sub_8032f6d0(0x187c2fce0);
    sub_8032f6d0(0x187c2fe90);
    sub_8032f6d0(0x187c30048);
    sub_8032f6d0(0x187b42140);
    sub_8032f6d0(0x187b806e0);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab626d3 = '\x01';
  }
  auStack_98[0] = 0;
  uStack_78 = 0;
  uStack_70 = 0;
  lStack_68 = 0;
  uVar8 = 0;
  UnitActionController::actionStartFirstProcess(param_1,param_2,auStack_98,1,1,0);
  if (param_2 != 0) {
    SkillBase::ReadySkill(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar1 != 0)) {
      uVar3 = CLKMBEGHOLA::ANKFGOIFFHH(lVar1,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar3;
      iStack_94 = 0;
      if (param_4 != 0) {
        do {
          iVar4 = iStack_94;
          if (*(int *)(param_4 + 0x18) <= iStack_94) {
            return;
          }
          plVar6 = (longlong *)sub_849ed2d0(param_4,iStack_94,uRam0000000187c19e18);
          if (plVar6 == (longlong *)0x0) break;
          uVar7 = *(ulonglong *)(*plVar6 + 0x220);
          (**(code **)(*plVar6 + 0x218))
                    (plVar6,*(undefined8 *)(param_1 + 0x90),param_1,param_2,uVar7);
          if (*(longlong *)(param_2 + 0xa8) == 0) break;
          cVar5 = sub_84a7f390(*(longlong *)(param_2 + 0xa8),iVar4,uRam0000000187b42140);
          if (((cVar5 == '\0') || (*(char *)((longlong)plVar6 + 0x86) != '\0')) &&
             (*(char *)((longlong)plVar6 + 0x85) == '\0')) {
            if ((int)plVar6[2] == 1) {
code_r0x0001814bf2bd:
              if (((param_3 == 0) || (*(longlong *)(param_3 + 0x68) == 0)) ||
                 (lVar1 = *(longlong *)(*(longlong *)(param_3 + 0x68) + 0x38), lVar1 == 0)) break;
              cVar5 = CLKMBEGHOLA::BGPIDFLHLGC(lVar1,*(undefined8 *)(param_1 + 0x90),0);
            }
            else {
              if (param_5 != '\0') {
                if (plVar6[10] != 0) {
                  sub_849e2810(&uStack_60,plVar6[10],uRam0000000187b806e0);
                  uStack_78 = CONCAT44(uStack_5c,uStack_60);
                  uStack_70 = CONCAT44(uStack_54,uStack_58);
                  lStack_68 = lStack_50;
                  uStack_88 = 0;
                  puStack_80 = &uStack_78;
                  while (cVar5 = sub_8572bc70(&uStack_78,uRam0000000187c2fe90), lVar1 = lStack_68,
                        cVar5 != '\0') {
                    if (lStack_68 == 0) {
                      sub_8032f9d0();
                      sub_8032b6b0();
                      pcVar2 = (code *)swi(3);
                      (*pcVar2)();
                      return;
                    }
                    FEFJGFLKNLK::MNLBJHHAJHG(plVar6,*(undefined8 *)(lStack_68 + 0x68),0,0);
                    uVar8 = uVar8 & 0xffffffff00000000;
                    uVar7 = uVar7 & 0xffffffff00000000;
                    UnitActionController::ExecAction(param_1,plVar6,param_2,lVar1,uVar7,uVar8,1,0,0,0);
                  }
                  sub_80443e50(&uStack_78,uRam0000000187c2fce0);
                  goto code_r0x0001814bf352;
                }
                break;
              }
              if ((int)plVar6[2] == 1) goto code_r0x0001814bf2bd;
              cVar5 = '\0';
              if (param_3 == 0) break;
            }
            lVar1 = *(longlong *)(param_3 + 0x68);
            if ((lVar1 == 0) || (*(longlong *)(lVar1 + 0x38) == 0)) break;
            if (cVar5 == '\0' && *(char *)(*(longlong *)(lVar1 + 0x38) + 0x260) == '\0') {
              FEFJGFLKNLK::MNLBJHHAJHG(plVar6,lVar1,0,0);
              uVar8 = uVar8 & 0xffffffff00000000;
              UnitActionController::ExecAction(param_1,plVar6,param_2,param_3,uVar7 & 0xffffffff00000000,uVar8,1,0,0,0)
              ;
            }
          }
code_r0x0001814bf352:
          iStack_94 = iVar4 + 1;
        } while( true );
      }
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::ExecUnitActionNoDelay  @ 0x14bfa00  size=1 ==== */

void UnitActionController::ExecUnitActionNoDelay(undefined8 param_1,longlong param_2,undefined8 param_3)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  int iVar4;
  
  if (cRam000000018ab626ae == '\0') {
    sub_8032f6d0(0x187b57018);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b812d0);
    cRam000000018ab626ae = '\x01';
  }
  if ((param_2 != 0) && (*(longlong *)(param_2 + 0x50) != 0)) {
    if (*(int *)(*(longlong *)(param_2 + 0x50) + 0x18) == 0) {
      UnitActionController::initWhenNoTarget(param_1,param_2,param_3,0);
    }
    iVar4 = 0;
    while (lVar3 = *(longlong *)(param_2 + 0x50), lVar3 != 0) {
      if ((*(int *)(lVar3 + 0x18) <= iVar4) ||
         (((*(int *)(param_2 + 0x1c) <= iVar4 && (*(int *)(param_2 + 0x1c) != 0)) && (iVar4 != 0))))
      {
        return;
      }
      lVar3 = sub_849ed2d0(lVar3,iVar4,uRam0000000187b812d0);
      if ((lVar3 == 0) || (*(longlong *)(param_2 + 0x128) == 0)) break;
      cVar2 = sub_85653020(*(longlong *)(param_2 + 0x128),*(undefined8 *)(lVar3 + 0x68),
                            uRam0000000187b57018);
      if (cVar2 == '\0') {
        FEFJGFLKNLK::MNLBJHHAJHG(param_2,*(undefined8 *)(lVar3 + 0x68),0,0);
      }
      UnitActionController::ExecAction(param_1,param_2,param_3,lVar3,0,0,0,0,0,0);
      iVar4 = iVar4 + 1;
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitActionController::ExecUnitActionWithDelay  @ 0x14bfb90  size=1 ==== */

void UnitActionController::ExecUnitActionWithDelay(longlong param_1,longlong *param_2,longlong param_3,undefined1 param_4,
                  undefined1 param_5,undefined1 param_6)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  undefined8 uVar4;
  int iVar5;
  longlong *plVar6;
  longlong *plVar7;
  longlong *plVar8;
  ulonglong in_stack_ffffffffffffff98;
  undefined4 uVar9;
  
  if (cRam000000018ab626ad == '\0') {
    sub_8032f6d0(0x187b644c8);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b812d0);
    cRam000000018ab626ad = '\x01';
  }
  if ((param_2 != (longlong *)0x0) && (param_2[10] != 0)) {
    if (*(int *)(param_2[10] + 0x18) == 0) {
      UnitActionController::initWhenNoTarget(param_1,param_2,param_3,0);
    }
    uVar9 = (undefined4)(in_stack_ffffffffffffff98 >> 0x20);
    plVar8 = (longlong *)0x0;
    plVar7 = plVar8;
    if ((int)param_2[0xc] == 0x19) {
      if (((*(longlong *)(param_1 + 0x90) == 0) ||
          (lVar3 = UnitCtrl::GetCurrentSpineCtrl(*(longlong *)(param_1 + 0x90),0), param_3 == 0)) || (lVar3 == 0))
      goto code_r0x0001814bfe19;
      in_stack_ffffffffffffff98 = CONCAT44(uVar9,0xffffffff);
      cVar2 = BattleUnitBaseSpineController::IsAnimation(lVar3,*(undefined4 *)(param_3 + 0x80),*(undefined4 *)(param_3 + 0x94),1,
                            in_stack_ffffffffffffff98,0);
      if (cVar2 != '\0') {
        if ((*(byte *)(*param_2 + 0x130) < *(byte *)(lRam0000000187b644c8 + 0x130)) ||
           (lVar3 = *(longlong *)(*param_2 + 200),
           *(longlong *)(lVar3 + -8 + (ulonglong)*(byte *)(lRam0000000187b644c8 + 0x130) * 8) !=
           lRam0000000187b644c8)) goto code_r0x0001814bfe19;
        plVar6 = plVar8;
        if (*(longlong *)(lVar3 + -8 + (ulonglong)*(byte *)(lRam0000000187b644c8 + 0x130) * 8) ==
            lRam0000000187b644c8) {
          plVar6 = param_2;
        }
        uVar4 = HJPKGAFJNMM::JNJGJMDBECB(plVar6,param_3,*(undefined8 *)(param_1 + 0x90),param_1,0);
        plVar6 = plVar8;
        if (0.0 < *(float *)(param_3 + 0xb0)) {
          plVar6 = *(longlong **)(param_1 + 0x90);
        }
        in_stack_ffffffffffffff98 = 0;
        UnitActionController::AppendCoroutine(param_1,uVar4,1,plVar6,0);
      }
      *(undefined4 *)(param_2 + 0x1f) = 0;
    }
    while (lVar3 = param_2[10], lVar3 != 0) {
      iVar5 = (int)plVar7;
      if ((*(int *)(lVar3 + 0x18) <= iVar5) ||
         (((*(int *)((longlong)param_2 + 0x1c) <= iVar5 && (*(int *)((longlong)param_2 + 0x1c) != 0)
           ) && (iVar5 != 0)))) {
        return;
      }
      uVar4 = sub_849ed2d0(lVar3,plVar7,uRam0000000187b812d0);
      uVar4 = UnitActionController::ExecActionWithDelayAndTarget(param_1,param_2,param_3,uVar4,
                            in_stack_ffffffffffffff98 & 0xffffffff00000000,param_4,param_5,param_6,0
                            ,0);
      if (param_3 == 0) break;
      plVar7 = plVar8;
      if (0.0 < *(float *)(param_3 + 0xb0)) {
        plVar7 = *(longlong **)(param_1 + 0x90);
      }
      if (param_1 == 0) break;
      in_stack_ffffffffffffff98 = 0;
      UnitActionController::AppendCoroutine(param_1,uVar4,1,plVar7,0);
      plVar7 = (longlong *)(ulonglong)(iVar5 + 1);
    }
  }
code_r0x0001814bfe19:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitActionController::GetAnimeId  @ 0x14bfe20  size=1 ==== */

ulonglong UnitActionController::GetAnimeId(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab626c6 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626c6 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      return (ulonglong)*(uint *)(lVar2 + 0x80);
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return uVar3;
}



/* ==== UnitActionController::GetAttackSide  @ 0x14bfe90  size=1 ==== */

undefined8 UnitActionController::GetAttackSide(undefined4 param_1,longlong param_2)

{
  code *pcVar1;
  undefined8 uVar2;
  
  switch(param_1) {
  case 1:
  case 4:
  case 7:
    break;
  default:
    return 0;
  }
  if (param_2 != 0) {
    uVar2 = 1;
    if (*(char *)(param_2 + 0x165) != '\0') {
      uVar2 = 0xffffffff;
    }
    return uVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar2 = (*pcVar1)();
  return uVar2;
}



/* ==== UnitActionController::GetForceCutinOff  @ 0x14bff00  size=1 ==== */

ulonglong UnitActionController::GetForceCutinOff(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab626cc == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626cc = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      return (ulonglong)*(byte *)(lVar2 + 0xd4);
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return uVar3;
}



/* ==== UnitActionController::GetIsSkillPrincessForm  @ 0x14bff70  size=1 ==== */

ulonglong UnitActionController::GetIsSkillPrincessForm(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab626cd == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626cd = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      return (ulonglong)*(byte *)(lVar2 + 0x10);
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return uVar3;
}



/* ==== UnitActionController::GetPrincessFormMovieData  @ 0x14bffd0  size=1 ==== */

undefined8 UnitActionController::GetPrincessFormMovieData(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  undefined8 uVar3;
  
  if (cRam000000018ab626ce == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626ce = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      return *(undefined8 *)(lVar2 + 0x18);
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return uVar3;
}



/* ==== UnitActionController::GetSkillMotionType  @ 0x14c0030  size=1 ==== */

ulonglong UnitActionController::GetSkillMotionType(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab626c4 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626c4 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      return (ulonglong)*(uint *)(lVar2 + 0xf8);
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return uVar3;
}



/* ==== UnitActionController::GetSkillNum  @ 0x14c00a0  size=1 ==== */

ulonglong UnitActionController::GetSkillNum(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab626c5 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626c5 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      return (ulonglong)*(uint *)(lVar2 + 0x94);
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return uVar3;
}



/* ==== UnitActionController::HasNextAnime  @ 0x14c0110  size=1 ==== */

undefined8 UnitActionController::HasNextAnime(longlong param_1,undefined4 param_2)

{
  int iVar1;
  undefined4 uVar2;
  code *pcVar3;
  longlong lVar4;
  undefined8 uVar5;
  
  if (cRam000000018ab626c2 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626c2 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar4 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar4 != 0) {
      if (*(char *)(lVar4 + 0x10) != '\0') {
        return 1;
      }
      iVar1 = *(int *)(lVar4 + 0x80);
      uVar2 = *(undefined4 *)(lVar4 + 0x94);
      if ((((iVar1 - 0x29U & 0xfffffffc) != 0) || (iVar1 == 0x2a)) && (iVar1 != 0x2f)) {
        return 0;
      }
      if (*(longlong *)(param_1 + 0x90) != 0) {
        lVar4 = UnitCtrl::GetCurrentSpineCtrl(*(longlong *)(param_1 + 0x90),0);
        if (lVar4 != 0) {
          uVar5 = BattleUnitBaseSpineController::IsAnimation(lVar4,iVar1,uVar2,1,0xffffffff,0);
          return uVar5;
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar5 = (*pcVar3)();
  return uVar5;
}



/* ==== UnitActionController::Initialize  @ 0x14c02c0  size=1 ==== */

void UnitActionController::Initialize(longlong param_1,longlong param_2,longlong param_3,char param_4,longlong param_5)

{
  float fVar1;
  double dVar2;
  code *pcVar3;
  ulonglong uVar4;
  char cVar5;
  int iVar6;
  undefined4 uVar7;
  int iVar8;
  int iVar9;
  undefined8 uVar10;
  longlong *plVar11;
  longlong lVar12;
  longlong lVar13;
  undefined8 uVar14;
  longlong lVar15;
  ulonglong uVar16;
  longlong lVar17;
  uint uVar18;
  float fVar19;
  undefined4 auStack_f8 [2];
  undefined8 uStack_f0;
  undefined8 uStack_e8;
  undefined8 *puStack_e0;
  ulonglong uStack_d8;
  longlong lStack_d0;
  undefined8 uStack_c8;
  undefined8 uStack_b8;
  undefined4 uStack_b0;
  undefined8 uStack_a8;
  undefined8 *puStack_a0;
  ulonglong uStack_98;
  longlong lStack_90;
  undefined8 uStack_88;
  undefined8 uStack_80;
  undefined8 *puStack_78;
  ulonglong uStack_70;
  longlong lStack_68;
  
  if (cRam000000018ab62697 == '\0') {
    sub_8032f6d0(0x187c21b68);
    sub_8032f6d0(0x187c332b0);
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187b802a8);
    sub_8032f6d0(0x187b3fb28);
    sub_8032f6d0(0x187c1c1c8);
    sub_8032f6d0(0x187c41e50);
    sub_8032f6d0(0x187c42000);
    sub_8032f6d0(0x187c1c898);
    sub_8032f6d0(0x187bfc580);
    sub_8032f6d0(0x187c41c98);
    sub_8032f6d0(0x187b3f978);
    sub_8032f6d0(0x187c1baf8);
    sub_8032f6d0(0x187c1cf68);
    sub_8032f6d0(0x187c421b8);
    sub_8032f6d0(0x187c42368);
    sub_8032f6d0(0x187ba6e58);
    sub_8032f6d0(0x187bea258);
    sub_8032f6d0(0x187be3458);
    sub_8032f6d0(0x187b58ff8);
    sub_8032f6d0(0x187bc4128);
    sub_8032f6d0(0x187c0ed88);
    sub_8032f6d0(0x187c00080);
    sub_8032f6d0(0x187c0ef40);
    sub_8032f6d0(0x187c00238);
    sub_8032f6d0(0x187c0f0f0);
    sub_8032f6d0(0x187c003f0);
    sub_8032f6d0(0x187c3c750);
    sub_8032f6d0(0x187bfcff0);
    sub_8032f6d0(0x187bf8138);
    sub_8032f6d0(0x187bd2340);
    sub_8032f6d0(0x187bd62d8);
    sub_8032f6d0(0x187bd6490);
    sub_8032f6d0(0x187bd24f8);
    sub_8032f6d0(0x187c19748);
    sub_8032f6d0(0x187b69958);
    sub_8032f6d0(0x187b69b10);
    sub_8032f6d0(0x187b69798);
    sub_8032f6d0(0x187b413a0);
    sub_8032f6d0(0x187b695e0);
    sub_8032f6d0(0x187b7f908);
    sub_8032f6d0(0x187c19590);
    sub_8032f6d0(0x187b6a200);
    sub_8032f6d0(0x187c307b0);
    sub_8032f6d0(0x187c31950);
    sub_8032f6d0(0x187b69cd0);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c30e80);
    sub_8032f6d0(0x187b6a3c0);
    sub_8032f6d0(0x187c31b08);
    sub_8032f6d0(0x187c30968);
    sub_8032f6d0(0x187b69e88);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187bf1860);
    sub_8032f6d0(0x187bc09d8);
    sub_8032f6d0(0x187be2298);
    sub_8032f6d0(0x187bc5830);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187c013d8);
    sub_8032f6d0(0x187bf48b8);
    sub_8032f6d0(0x187bf4a70);
    sub_8032f6d0(0x187bf4c28);
    sub_8032f6d0(0x187bf4de0);
    sub_8032f6d0(0x187bcefe8);
    sub_8032f6d0(0x187bcf1a0);
    sub_8032f6d0(0x187c1f720);
    sub_8032f6d0(0x187bfaba0);
    sub_8032f6d0(0x187c05928);
    cRam000000018ab62697 = '\x01';
  }
  uStack_a8 = 0;
  puStack_a0 = (undefined8 *)0x0;
  uStack_98 = 0;
  lStack_90 = 0;
  uStack_88 = 0;
  uStack_80 = 0;
  puStack_78 = (undefined8 *)0x0;
  uStack_70 = 0;
  lStack_68 = 0;
  if (**(longlong **)(lRam0000000187bfaba0 + 0xb8) == 0) {
    uVar10 = sub_847c7d00(param_1,uRam0000000187bfcff0);
    **(undefined8 **)(lRam0000000187bfaba0 + 0xb8) = uVar10;
    sub_8032e8d0(*(undefined8 *)(lRam0000000187bfaba0 + 0xb8),uVar10);
    if (**(longlong **)(lRam0000000187bfaba0 + 0xb8) == 0) goto code_r0x0001814c1db1;
    uVar10 = sub_84670860(**(longlong **)(lRam0000000187bfaba0 + 0xb8),uRam0000000187bf4c28);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) = uVar10;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8,uVar10);
    if (**(longlong **)(lRam0000000187bfaba0 + 0xb8) == 0) goto code_r0x0001814c1db1;
    uVar10 = sub_84670860(**(longlong **)(lRam0000000187bfaba0 + 0xb8),uRam0000000187bf48b8);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x10) = uVar10;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x10,uVar10);
    if (**(longlong **)(lRam0000000187bfaba0 + 0xb8) == 0) goto code_r0x0001814c1db1;
    uVar10 = sub_84670860(**(longlong **)(lRam0000000187bfaba0 + 0xb8),uRam0000000187bf4a70);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x18) = uVar10;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x18,uVar10);
    if (**(longlong **)(lRam0000000187bfaba0 + 0xb8) == 0) goto code_r0x0001814c1db1;
    uVar10 = sub_84670860(**(longlong **)(lRam0000000187bfaba0 + 0xb8),uRam0000000187bf4de0);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20) = uVar10;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20,uVar10);
  }
  *(longlong *)(param_1 + 0x90) = param_2;
  sub_8032e8d0(param_1 + 0x90,param_2);
  if (param_2 != 0) {
    uVar10 = FixedTransformMonoBehavior::get_transform(param_2,0);
    *(undefined8 *)(param_1 + 0x20) = uVar10;
    sub_8032e8d0(param_1 + 0x20,uVar10);
    uVar10 = sub_8032f970(uRam0000000187bea258);
    sub_85575ea0(uVar10,uRam0000000187c41c98);
    *(undefined8 *)(param_1 + 0xb0) = uVar10;
    sub_8032e8d0(param_1 + 0xb0,uVar10);
    if (*(longlong *)(param_1 + 0x38) != 0) {
      *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x80) = 0x22;
      if (param_4 == '\0') {
        if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c1db1;
        uVar7 = *(undefined4 *)(*(longlong *)(param_1 + 0x90) + 0x11c);
      }
      else {
        if (param_5 == 0) goto code_r0x0001814c1db1;
        uVar7 = *(undefined4 *)(param_5 + 0x11c);
      }
      if (*(longlong *)(param_1 + 0x38) != 0) {
        *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x100) = uVar7;
        if (((*(longlong *)(param_1 + 0x90) != 0) &&
            (lVar12 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar12 != 0)) &&
           (*(longlong *)(param_1 + 0x38) != 0)) {
          *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x7c) = *(undefined4 *)(lVar12 + 0x18);
          if (*(longlong *)(param_1 + 0x38) != 0) {
            *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x78) = 1;
            plVar11 = (longlong *)sub_8032f970(uRam0000000187c21b68);
            ABGOKNKJADC::.ctor(plVar11,0);
            if (plVar11 != (longlong *)0x0) {
              (**(code **)(*plVar11 + 0x1d8))(plVar11,*(undefined8 *)(*plVar11 + 0x1e0));
              *(undefined4 *)(plVar11 + 2) = 1;
              *(undefined8 *)((longlong)plVar11 + 0x14) = 3;
              *(undefined4 *)((longlong)plVar11 + 0x1c) = 1;
              if ((*(longlong *)(param_1 + 0x90) != 0) &&
                 (lVar12 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar12 != 0)) {
                *(undefined4 *)(plVar11 + 4) = *(undefined4 *)(lVar12 + 0x18);
                *(undefined4 *)((longlong)plVar11 + 0x24) = 1;
                *(undefined4 *)(plVar11 + 0xc) = 1;
                uVar10 = sub_8032f970(uRam0000000187bf8138);
                sub_804383d0(uVar10,0);
                lVar12 = sub_8032f970(uRam0000000187ba6e58);
                sub_855de480(lVar12,uVar10,uRam0000000187b3f978);
                if (lVar12 != 0) {
                  sub_855e0980(lVar12,0,0,uRam0000000187b3fb28);
                  sub_855e0980(lVar12,2,0x3ff0000000000000,uRam0000000187b3fb28);
                  plVar11[0xb] = lVar12;
                  sub_8032e8d0(plVar11 + 0xb,lVar12);
                  *(undefined4 *)((longlong)plVar11 + 0x2c) = *(undefined4 *)(param_2 + 0x4b0);
                  auStack_f8[0] = 0;
                  if (*(char *)(param_1 + 0x30) != '\0') {
                    if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
                      sub_8032fa70(lRam0000000187b41650);
                    }
                    lVar12 = *(longlong *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 0x58);
                    if (param_4 == '\0') {
                      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c1db1;
                      uVar7 = *(undefined4 *)(*(longlong *)(param_1 + 0x90) + 0x120);
                    }
                    else {
                      if (param_5 == 0) goto code_r0x0001814c1db1;
                      uVar7 = *(undefined4 *)(param_5 + 0x120);
                    }
                    if (lVar12 == 0) goto code_r0x0001814c1db1;
                    cVar5 = sub_855eb450(lVar12,uVar7,auStack_f8,uRam0000000187bfc580);
                    if (cVar5 != '\0') {
                      lVar12 = sub_8032f970(uRam0000000187bc09d8);
                      sub_849de5b0(lVar12,uRam0000000187b695e0);
                      lVar13 = sub_8032f970(uRam0000000187c332b0);
                      *(undefined8 *)(lVar13 + 0x18) = 0x3ff0000000000000;
                      *(undefined4 *)(lVar13 + 0x20) = 0x3f800000;
                      sub_80443e50(lVar13,0);
                      *(undefined8 *)(lVar13 + 0x18) = 0x3ff0000000000000;
                      *(undefined4 *)(lVar13 + 0x14) = 0;
                      if (lVar12 != 0) {
                        sub_80002460(lVar12,lVar13,uRam0000000187b69958);
                        plVar11[0xe] = lVar12;
                        sub_8032e8d0(plVar11 + 0xe,lVar12);
                        lVar12 = sub_8032e960(uRam0000000187c013d8,1);
                        if (lVar12 != 0) {
                          if (*(int *)(lVar12 + 0x18) == 0) {
code_r0x0001814c1db7:
                            sub_8032f9c0();
code_r0x0001814c1dbd:
                            sub_8032f9d0();
code_r0x0001814c1dc3:
                            sub_8032f9d0();
code_r0x0001814c1dc8:
                            sub_8032f9d0();
code_r0x0001814c1dcd:
                            sub_8032f9d0();
code_r0x0001814c1dd2:
                            sub_8032f9d0();
code_r0x0001814c1dd7:
                            sub_8032f9d0();
code_r0x0001814c1ddc:
                            sub_8032f9d0();
code_r0x0001814c1de1:
                            sub_8032f9d0();
code_r0x0001814c1de6:
                            sub_8032f9d0();
code_r0x0001814c1deb:
                            sub_8032f9d0();
code_r0x0001814c1df0:
                            sub_8032f9d0();
code_r0x0001814c1df5:
                            sub_8032f9d0();
code_r0x0001814c1dfa:
                            sub_8032f9d0();
                            sub_8032b6b0();
code_r0x0001814c1e06:
                            sub_8032f9d0();
code_r0x0001814c1e0b:
                            sub_8032f9d0();
code_r0x0001814c1e10:
                            sub_8032f9d0();
code_r0x0001814c1e15:
                            sub_8032f9d0();
                            sub_8032b6b0();
                            pcVar3 = (code *)swi(3);
                            (*pcVar3)();
                            return;
                          }
                          *(undefined4 *)(lVar12 + 0x20) = auStack_f8[0];
                          plVar11[0xd] = lVar12;
                          sub_8032e8d0(plVar11 + 0xd,lVar12);
                          plVar11[0xf] = 0x3ff0000000000000;
code_r0x0001814c0eeb:
                          lVar12 = sub_8032f970(uRam0000000187bf1860);
                          sub_849de5b0(lVar12,uRam0000000187b413a0);
                          plVar11[0x11] = lVar12;
                          sub_8032e8d0(plVar11 + 0x11,lVar12);
                          uVar7 = *(undefined4 *)(param_2 + 0x488);
                          if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
                            sub_8032fa70();
                          }
                          iVar6 = UnitUtility::GetCharaId(uVar7,0);
                          *(int *)(plVar11 + 7) = iVar6 * 10000;
                          lVar12 = sub_8032f970(uRam0000000187bc5830);
                          sub_849de5b0(lVar12,uRam0000000187b7f908);
                          plVar11[10] = lVar12;
                          sub_8032e8d0(plVar11 + 10,lVar12);
                          lVar12 = *(longlong *)(param_1 + 0x38);
                          uVar10 = sub_8032f970(uRam0000000187be2298);
                          sub_849de5b0(uVar10,uRam0000000187c19590);
                          if (lVar12 != 0) {
                            *(undefined8 *)(lVar12 + 0x20) = uVar10;
                            sub_8032e8d0(lVar12 + 0x20,uVar10);
                            lVar12 = lRam0000000187c19748;
                            if ((*(longlong *)(param_1 + 0x38) != 0) &&
                               (lVar13 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x20),
                               lVar13 != 0)) {
                              *(int *)(lVar13 + 0x1c) = *(int *)(lVar13 + 0x1c) + 1;
                              lVar15 = *(longlong *)(lVar13 + 0x10);
                              uVar18 = *(uint *)(lVar13 + 0x18);
                              if (lVar15 != 0) {
                                if (uVar18 < *(uint *)(lVar15 + 0x18)) {
                                  *(uint *)(lVar13 + 0x18) = uVar18 + 1;
                                  if (*(uint *)(lVar15 + 0x18) <= uVar18) goto code_r0x0001814c1db7;
                                  *(longlong **)(lVar15 + 0x20 + (longlong)(int)uVar18 * 8) =
                                       plVar11;
                                  sub_8032e8d0(lVar15 + 0x20 + (longlong)(int)uVar18 * 8,
                                                      plVar11);
                                }
                                else {
                                  sub_84a97470(lVar13,plVar11,
                                                *(undefined8 *)
                                                 (*(longlong *)(*(longlong *)(lVar12 + 0x20) + 0xc0)
                                                 + 0x70));
                                }
                                if (*(longlong *)(param_1 + 0xb0) != 0) {
                                  sub_855ac5e0(*(longlong *)(param_1 + 0xb0),1,
                                                *(undefined8 *)(param_1 + 0x38),uRam0000000187c41e50
                                               );
                                  UnitActionController::DependActionSolve(param_1,*(undefined8 *)(param_1 + 0x38),0);
                                  UnitActionController::ExecActionOnStart(param_1,*(undefined8 *)(param_1 + 0x38),0);
                                  if (param_4 != '\0') {
                                    return;
                                  }
                                  if (param_3 != 0) {
                                    lVar12 = *(longlong *)(param_3 + 0x38);
                                    if (*(longlong *)(param_1 + 0x40) != 0) {
                                      if (*(int *)(*(longlong *)(param_1 + 0x40) + 0x18) != 0) {
                                        if (lVar12 == 0) goto code_r0x0001814c1db1;
                                        lVar13 = *(longlong *)(lVar12 + 0x10);
                                        lVar15 = *(longlong *)(param_1 + 0xb0);
                                        if (lVar13 == 0) goto code_r0x0001814c1db1;
                                        uVar7 = sub_84a610f0(lVar13,0,uRam0000000187b446b8);
                                        if ((*(longlong *)(param_1 + 0x40) == 0) ||
                                           (uVar10 = sub_849ed2d0(*(longlong *)(param_1 + 0x40),0,
                                                                   uRam0000000187c30968),
                                           lVar15 == 0)) goto code_r0x0001814c1db1;
                                        sub_855ac5e0(lVar15,uVar7,uVar10,uRam0000000187c41e50);
                                        iVar6 = sub_84a610f0(lVar13,0,uRam0000000187b446b8);
                                        *(bool *)(param_1 + 0xb9) = iVar6 != 0;
                                        if ((*(longlong *)(param_1 + 0x40) == 0) ||
                                           (lVar13 = sub_849ed2d0(*(longlong *)(param_1 + 0x40),0,
                                                                   uRam0000000187c30968),
                                           lVar13 == 0)) goto code_r0x0001814c1db1;
                                        *(undefined4 *)(lVar13 + 0x94) = 0;
                                      }
                                      if ((*(longlong *)(param_1 + 0x60) != 0) && (lVar12 != 0)) {
                                        if (*(int *)(*(longlong *)(param_1 + 0x60) + 0x18) != 0) {
                                          if (*(longlong *)(lVar12 + 0x48) == 0)
                                          goto code_r0x0001814c1db1;
                                          uVar7 = sub_84a610f0(*(longlong *)(lVar12 + 0x48),0,
                                                                uRam0000000187b446b8);
                                          lVar13 = *(longlong *)(param_1 + 0xb0);
                                          if ((*(longlong *)(param_1 + 0x60) == 0) ||
                                             (uVar10 = sub_849ed2d0(*(longlong *)(param_1 + 0x60),0
                                                                     ,uRam0000000187c30968),
                                             lVar13 == 0)) goto code_r0x0001814c1db1;
                                          sub_855ac5e0(lVar13,uVar7,uVar10,uRam0000000187c41e50);
                                          if ((*(longlong *)(param_1 + 0x60) == 0) ||
                                             (lVar13 = sub_849ed2d0(*(longlong *)(param_1 + 0x60),0
                                                                     ,uRam0000000187c30968),
                                             lVar13 == 0)) goto code_r0x0001814c1db1;
                                          *(undefined4 *)(lVar13 + 0x94) = 0;
                                        }
                                        lVar13 = *(longlong *)(lVar12 + 0x28);
                                        iVar6 = 0;
                                        if (lVar13 != 0) {
                                          iVar9 = *(int *)(lVar13 + 0x18);
                                          if (0 < iVar9) {
                                            do {
                                              iVar8 = sub_84a610f0(lVar13,iVar6,
                                                                    uRam0000000187b446b8);
                                              if (iVar8 != 0) {
                                                if ((*(longlong *)(param_1 + 0x48) == 0) ||
                                                   (lVar15 = sub_849ed2d0(*(longlong *)
                                                                            (param_1 + 0x48),iVar6,
                                                                           uRam0000000187c30968),
                                                   lVar15 == 0)) goto code_r0x0001814c1db1;
                                                *(int *)(lVar15 + 0x94) = iVar6 + 1;
                                                lVar15 = *(longlong *)(param_1 + 0xb0);
                                                uVar7 = sub_84a610f0(lVar13,iVar6,
                                                                      uRam0000000187b446b8);
                                                if ((*(longlong *)(param_1 + 0x48) == 0) ||
                                                   (uVar10 = sub_849ed2d0(*(longlong *)
                                                                            (param_1 + 0x48),iVar6,
                                                                           uRam0000000187c30968),
                                                   lVar15 == 0)) goto code_r0x0001814c1db1;
                                                sub_855ac5e0(lVar15,uVar7,uVar10,
                                                              uRam0000000187c41e50);
                                              }
                                              iVar6 = iVar6 + 1;
                                            } while (iVar6 < iVar9);
                                          }
                                          lVar13 = *(longlong *)(lVar12 + 0x50);
                                          iVar6 = 0;
                                          if (lVar13 != 0) {
                                            for (; iVar6 < *(int *)(lVar13 + 0x18);
                                                iVar6 = iVar6 + 1) {
                                              iVar9 = sub_84a610f0(lVar13,iVar6,
                                                                    uRam0000000187b446b8);
                                              if (iVar9 != 0) {
                                                if ((*(longlong *)(param_1 + 0x68) == 0) ||
                                                   (lVar15 = sub_849ed2d0(*(longlong *)
                                                                            (param_1 + 0x68),iVar6,
                                                                           uRam0000000187c30968),
                                                   lVar15 == 0)) goto code_r0x0001814c1db1;
                                                *(int *)(lVar15 + 0x94) = iVar6 + 1;
                                                lVar15 = *(longlong *)(param_1 + 0xb0);
                                                uVar7 = sub_84a610f0(lVar13,iVar6,
                                                                      uRam0000000187b446b8);
                                                if ((*(longlong *)(param_1 + 0x68) == 0) ||
                                                   (uVar10 = sub_849ed2d0(*(longlong *)
                                                                            (param_1 + 0x68),iVar6,
                                                                           uRam0000000187c30968),
                                                   lVar15 == 0)) goto code_r0x0001814c1db1;
                                                sub_855ac5e0(lVar15,uVar7,uVar10,
                                                              uRam0000000187c41e50);
                                              }
                                            }
                                            lVar13 = *(longlong *)(lVar12 + 0x58);
                                            iVar6 = 0;
                                            if (lVar13 != 0) {
                                              for (; iVar6 < *(int *)(lVar13 + 0x18);
                                                  iVar6 = iVar6 + 1) {
                                                iVar9 = sub_84a610f0(lVar13,iVar6,
                                                                      uRam0000000187b446b8);
                                                if (iVar9 != 0) {
                                                  if ((*(longlong *)(param_1 + 0x78) == 0) ||
                                                     (lVar15 = sub_849ed2d0(*(longlong *)
                                                                              (param_1 + 0x78),iVar6
                                                                             ,uRam0000000187c30968),
                                                     lVar15 == 0)) goto code_r0x0001814c1db1;
                                                  *(int *)(lVar15 + 0x94) = iVar6 + 1;
                                                  lVar15 = *(longlong *)(param_1 + 0xb0);
                                                  uVar7 = sub_84a610f0(lVar13,iVar6,
                                                                        uRam0000000187b446b8);
                                                  if ((*(longlong *)(param_1 + 0x78) == 0) ||
                                                     (uVar10 = sub_849ed2d0(*(longlong *)
                                                                              (param_1 + 0x78),iVar6
                                                                             ,uRam0000000187c30968),
                                                     lVar15 == 0)) goto code_r0x0001814c1db1;
                                                  sub_855ac5e0(lVar15,uVar7,uVar10,
                                                                uRam0000000187c41e50);
                                                }
                                              }
                                              lVar13 = *(longlong *)(lVar12 + 0x30);
                                              iVar6 = 0;
                                              if (lVar13 != 0) {
                                                iVar9 = *(int *)(lVar13 + 0x18);
                                                if (0 < iVar9) {
                                                  do {
                                                    iVar8 = sub_84a610f0(lVar13,iVar6,
                                                                          uRam0000000187b446b8);
                                                    if (iVar8 == 0) break;
                                                    if ((*(longlong *)(param_1 + 0x50) == 0) ||
                                                       (lVar15 = sub_849ed2d0(*(longlong *)
                                                                                (param_1 + 0x50),
                                                                               iVar6,
                                                  uRam0000000187c30968), lVar15 == 0))
                                                  goto code_r0x0001814c1db1;
                                                  *(int *)(lVar15 + 0x94) = iVar6 + 1;
                                                  lVar15 = *(longlong *)(param_1 + 0xb0);
                                                  uVar7 = sub_84a610f0(lVar13,iVar6,
                                                                        uRam0000000187b446b8);
                                                  if ((*(longlong *)(param_1 + 0x50) == 0) ||
                                                     (uVar10 = sub_849ed2d0(*(longlong *)
                                                                              (param_1 + 0x50),iVar6
                                                                             ,uRam0000000187c30968),
                                                     lVar15 == 0)) goto code_r0x0001814c1db1;
                                                  sub_855ac5e0(lVar15,uVar7,uVar10,
                                                                uRam0000000187c41e50);
                                                  iVar6 = iVar6 + 1;
                                                  } while (iVar6 < iVar9);
                                                }
                                                lVar13 = *(longlong *)(lVar12 + 0x38);
                                                iVar6 = 0;
                                                if (lVar13 != 0) {
                                                  while ((iVar6 < *(int *)(lVar13 + 0x18) &&
                                                         (iVar9 = sub_84a610f0(lVar13,iVar6,
                                                                                uRam0000000187b446b8
                                                                               ), iVar9 != 0))) {
                                                    if ((*(longlong *)(param_1 + 0x58) == 0) ||
                                                       (lVar15 = sub_849ed2d0(*(longlong *)
                                                                                (param_1 + 0x58),
                                                                               iVar6,
                                                  uRam0000000187c30968), lVar15 == 0))
                                                  goto code_r0x0001814c1db1;
                                                  *(int *)(lVar15 + 0x94) = iVar6 + 1;
                                                  lVar15 = *(longlong *)(param_1 + 0xb0);
                                                  if ((*(longlong *)(param_1 + 0x58) == 0) ||
                                                     (uVar10 = sub_849ed2d0(*(longlong *)
                                                                              (param_1 + 0x58),iVar6
                                                                             ,uRam0000000187c30968),
                                                     lVar15 == 0)) goto code_r0x0001814c1db1;
                                                  sub_855ac5e0(lVar15,iVar9,uVar10,
                                                                uRam0000000187c41e50);
                                                  iVar6 = iVar6 + 1;
                                                  }
                                                  lVar13 = *(longlong *)(lVar12 + 0x40);
                                                  iVar6 = 0;
                                                  if (lVar13 != 0) {
                                                    while ((iVar6 < *(int *)(lVar13 + 0x18) &&
                                                           (iVar9 = sub_84a610f0(lVar13,iVar6,
                                                                                                                                                                    
                                                  uRam0000000187b446b8), iVar9 != 0))) {
                                                    if ((*(longlong *)(param_1 + 0x80) == 0) ||
                                                       (lVar15 = sub_849ed2d0(*(longlong *)
                                                                                (param_1 + 0x80),
                                                                               iVar6,
                                                  uRam0000000187c30968), lVar15 == 0))
                                                  goto code_r0x0001814c1db1;
                                                  *(int *)(lVar15 + 0x94) = iVar6 + 1;
                                                  lVar15 = *(longlong *)(param_1 + 0xb0);
                                                  if ((*(longlong *)(param_1 + 0x80) == 0) ||
                                                     (uVar10 = sub_849ed2d0(*(longlong *)
                                                                              (param_1 + 0x80),iVar6
                                                                             ,uRam0000000187c30968),
                                                     lVar15 == 0)) goto code_r0x0001814c1db1;
                                                  sub_855ac5e0(lVar15,iVar9,uVar10,
                                                                uRam0000000187c41e50);
                                                  iVar6 = iVar6 + 1;
                                                  }
                                                  lVar13 = *(longlong *)(lVar12 + 0x60);
                                                  iVar6 = 0;
                                                  if (lVar13 != 0) {
                                                    while ((iVar6 < *(int *)(lVar13 + 0x18) &&
                                                           (iVar9 = sub_84a610f0(lVar13,iVar6,
                                                                                                                                                                    
                                                  uRam0000000187b446b8), iVar9 != 0))) {
                                                    if ((*(longlong *)(param_1 + 0x70) == 0) ||
                                                       (lVar15 = sub_849ed2d0(*(longlong *)
                                                                                (param_1 + 0x70),
                                                                               iVar6,
                                                  uRam0000000187c30968), lVar15 == 0))
                                                  goto code_r0x0001814c1db1;
                                                  *(int *)(lVar15 + 0x94) = iVar6;
                                                  lVar15 = *(longlong *)(param_1 + 0xb0);
                                                  if ((*(longlong *)(param_1 + 0x70) == 0) ||
                                                     (uVar10 = sub_849ed2d0(*(longlong *)
                                                                              (param_1 + 0x70),iVar6
                                                                             ,uRam0000000187c30968),
                                                     lVar15 == 0)) goto code_r0x0001814c1db1;
                                                  sub_855ac5e0(lVar15,iVar9,uVar10,
                                                                uRam0000000187c41e50);
                                                  iVar6 = iVar6 + 1;
                                                  }
                                                  uVar16 = sub_8032f970(uRam0000000187be3458)
                                                  ;
                                                  sub_85575ea0(uVar16,uRam0000000187c1baf8);
                                                  uStack_f0 = uVar16;
                                                  if (*(longlong *)(param_1 + 0xb0) != 0) {
                                                    sub_855337a0(&uStack_e8,
                                                                  *(longlong *)(param_1 + 0xb0),
                                                                  uRam0000000187c42000);
                                                    uStack_a8 = uStack_e8;
                                                    puStack_a0 = puStack_e0;
                                                    uStack_98 = uStack_d8;
                                                    lStack_90 = lStack_d0;
                                                    uStack_88 = uStack_c8;
                                                    uStack_e8 = 0;
                                                    puStack_e0 = &uStack_a8;
code_r0x0001814c17a0:
                                                    cVar5 = sub_85760f30(&uStack_a8,
                                                                          uRam0000000187c0ef40);
                                                    lVar13 = lStack_90;
                                                    uVar4 = uStack_98;
                                                    if (cVar5 != '\0') {
                                                      if (((int)uStack_98 != 1) &&
                                                         ((int)uStack_98 != 0)) {
                                                        if (*(longlong *)(param_1 + 0x90) == 0)
                                                        goto code_r0x0001814c1dfa;
                                                        lVar15 = *(longlong *)
                                                                  (*(longlong *)(param_1 + 0x90) +
                                                                  0x500);
                                                        if (lVar15 == 0) goto code_r0x0001814c1df5;
                                                        iVar6 = sub_855ac210(lVar15,uStack_98 &
                                                                                     0xffffffff);
                                                        if (iVar6 != 0) {
                                                          lVar15 = sub_80003d20(
                                                  lRam0000000187b3c708);
                                                  if (lVar15 == 0) goto code_r0x0001814c1df0;
                                                  if (*(longlong *)(lVar15 + 0x3d8) == 0)
                                                  goto code_r0x0001814c1deb;
                                                  uVar10 = HOEABFDCBIG::CBBMJADBDGF(*(longlong *)
                                                                                (lVar15 + 0x3d8),
                                                                               uVar4 & 0xffffffff,0)
                                                  ;
                                                  UnitActionController::setSkillParameter(param_1,lVar13,uVar10,0xffffffff,0);
                                                  if (*(longlong *)(param_1 + 0x90) == 0)
                                                  goto code_r0x0001814c1de6;
                                                  lVar15 = *(longlong *)
                                                            (*(longlong *)(param_1 + 0x90) + 0x500);
                                                  if (lVar15 == 0) goto code_r0x0001814c1de1;
                                                  uVar7 = sub_855ac210(lVar15,uVar4 & 0xffffffff,
                                                                        uRam0000000187c1cf68);
                                                  if (lVar13 == 0) goto code_r0x0001814c1ddc;
                                                  SkillBase::SetLevel(lVar13,uVar7,0);
                                                  lVar15 = sub_80003d20(lRam0000000187b3c708);
                                                  if (lVar15 == 0) goto code_r0x0001814c1dd7;
                                                  if (*(longlong *)(lVar15 + 0x418) == 0)
                                                  goto code_r0x0001814c1dd2;
                                                  uVar10 = GAFPNJAKNOL::CAMFLOFGAAN(*(longlong *)
                                                                          (lVar15 + 0x418),
                                                                         uVar4 & 0xffffffff,0);
                                                  if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
                                                    sub_8032fa70();
                                                  }
                                                  lVar15 = *(longlong *)
                                                            (*(longlong *)
                                                              (lRam0000000187c1f720 + 0xb8) + 0x10);
                                                  if (lVar15 == 0) {
                                                    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0)
                                                    {
                                                      sub_8032fa70();
                                                    }
                                                    uVar14 = **(undefined8 **)
                                                               (lRam0000000187c1f720 + 0xb8);
                                                    lVar15 = sub_8032f970(
                                                  uRam0000000187c3c750);
                                                  sub_8594e530(lVar15,uVar14,uRam0000000187bcf1a0,0
                                                               );
                                                  *(longlong *)
                                                   (*(longlong *)(lRam0000000187c1f720 + 0xb8) +
                                                   0x10) = lVar15;
                                                  sub_8032e8d0(*(longlong *)
                                                                       (lRam0000000187c1f720 + 0xb8)
                                                                      + 0x10,lVar15);
                                                  }
                                                  uVar10 = sub_847a0cb0(uVar10,lVar15);
                                                  lVar15 = sub_847b2640(uVar10,uRam0000000187bc4128
                                                                        );
                                                  iVar9 = 0;
                                                  if (lVar15 == 0) goto code_r0x0001814c1dcd;
                                                  for (; iVar9 < *(int *)(lVar15 + 0x18);
                                                      iVar9 = iVar9 + 1) {
                                                    lVar17 = sub_849ed2d0(lVar15,iVar9);
                                                    if (lVar17 == 0) goto code_r0x0001814c1dc8;
                                                    uVar10 = *(undefined8 *)(lVar17 + 0x34);
                                                    uVar7 = *(undefined4 *)(lVar17 + 0x3c);
                                                    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0)
                                                    {
                                                      sub_8032fa70();
                                                    }
                                                    uStack_b8 = uVar10;
                                                    uStack_b0 = uVar7;
                                                    iVar8 = ObscuredInt::FHOALPFLEEC(&uStack_b8,0);
                                                    if (iVar6 < iVar8) {
                                                      if (iVar9 != 0) {
                                                        if (uVar16 == 0) goto code_r0x0001814c1dc3;
                                                        sub_8559d660(uVar16,*(undefined4 *)
                                                                              (lVar13 + 0x78),
                                                                      iVar9 + -1,
                                                                      uRam0000000187c1c1c8);
                                                      }
                                                      break;
                                                    }
                                                    if (*(int *)(lVar15 + 0x18) == iVar9 + 1) {
                                                      if (uVar16 == 0) goto code_r0x0001814c1dbd;
                                                      sub_8559d660(uVar16,*(undefined4 *)
                                                                            (lVar13 + 0x78),iVar9,
                                                                    uRam0000000187c1c1c8);
                                                    }
                                                  }
                                                  }
                                                  }
                                                  goto code_r0x0001814c17a0;
                                                  }
                                                  sub_80443e50(&uStack_a8,uRam0000000187c0ed88);
                                                  lVar13 = *(longlong *)
                                                            (lRam0000000187b3c708 + 0x20);
                                                  if ((*(byte *)(lVar13 + 0x135) & 1) == 0) {
                                                    lVar13 = sub_8032d010(lVar13);
                                                  }
                                                  lVar13 = *(longlong *)
                                                            (*(longlong *)(lVar13 + 0xc0) + 0x10);
                                                  if ((*(byte *)(lVar13 + 0x135) & 1) == 0) {
                                                    lVar13 = sub_8032d010(lVar13);
                                                  }
                                                  if (**(longlong **)(lVar13 + 0xb8) != 0) {
                                                    lVar13 = *(longlong *)
                                                              (**(longlong **)(lVar13 + 0xb8) +
                                                              0x100);
                                                    if ((*(longlong *)(param_1 + 0x90) != 0) &&
                                                       (lVar13 != 0)) {
                                                      lVar13 = BMIKLONGBAF::BKEJKNOJCEE(lVar13,*(undefined4 *)
                                                                                     (*(longlong *)
                                                                                       (param_1 +
                                                                                       0x90) + 0x488
                                                                                     ),0);
                                                      if (lVar13 == 0) {
                                                        if (uVar16 == 0) goto code_r0x0001814c1db1;
                                                        sub_85557680(&uStack_e8,uVar16,
                                                                      uRam0000000187c1c898);
                                                        uStack_80 = uStack_e8;
                                                        puStack_78 = puStack_e0;
                                                        uStack_70 = uStack_d8;
                                                        lStack_68 = lStack_d0;
                                                        uStack_e8 = 0;
                                                        puStack_e0 = &uStack_80;
                                                        while (cVar5 = sub_85747e60(&uStack_80,
                                                                                                                                                                          
                                                  uRam0000000187c00238), uVar16 = uStack_70,
                                                  cVar5 != '\0') {
                                                    uStack_f0 = uStack_70;
                                                    lVar13 = *(longlong *)(param_1 + 0xb0);
                                                    if (lVar13 == 0) goto code_r0x0001814c1e15;
                                                    lVar15 = sub_855bdd60(lVar13,uStack_70 &
                                                                                  0xffffffff,
                                                                           uRam0000000187c421b8);
                                                    if (lVar15 == 0) goto code_r0x0001814c1e10;
                                                    if (*(longlong *)(lVar15 + 0x1a0) == 0)
                                                    goto code_r0x0001814c1e0b;
                                                    lVar15 = sub_849ed2d0(*(longlong *)
                                                                            (lVar15 + 0x1a0),
                                                                           uStack_f0._4_4_,
                                                                           uRam0000000187c30e80);
                                                    if (lVar15 == 0) goto code_r0x0001814c1e06;
                                                    if (cRam000000018ab62689 == '\0') {
                                                      sub_8032f6d0(0x187b778e8);
                                                      cRam000000018ab62689 = '\x01';
                                                    }
                                                    uVar10 = sub_8032f970(
                                                  uRam0000000187b778e8);
                                                  Skill::.ctor(uVar10,lVar15,0);
                                                  sub_855bde00(lVar13,uVar16 & 0xffffffff,uVar10,
                                                                uRam0000000187c42368);
                                                  }
                                                  sub_80443e50(&uStack_80,uRam0000000187c00080);
                                                  }
                                                  if (*(longlong *)(param_1 + 0x40) != 0) {
                                                    if (*(int *)(*(longlong *)(param_1 + 0x40) +
                                                                0x18) != 0) {
                                                      if ((lVar12 == 0) ||
                                                         (*(longlong *)(lVar12 + 0x10) == 0))
                                                      goto code_r0x0001814c1db1;
                                                      uVar7 = sub_84a610f0(*(longlong *)
                                                                             (lVar12 + 0x10),0,
                                                                            uRam0000000187b446b8);
                                                      if (cRam000000018ab626a4 == '\0') {
                                                        sub_8032f6d0(0x187c421b8);
                                                        cRam000000018ab626a4 = '\x01';
                                                      }
                                                      if ((*(longlong *)(param_1 + 0xb0) == 0) ||
                                                         (lVar13 = sub_855bdd60(*(longlong *)
                                                                                  (param_1 + 0xb0),
                                                                                 uVar7,
                                                  uRam0000000187c421b8), lVar13 == 0))
                                                  goto code_r0x0001814c1db1;
                                                  if (*(char *)(lVar13 + 0x10) != '\0') {
                                                    *(undefined4 *)(lVar13 + 0xd0) = 0;
                                                  }
                                                  }
                                                  if (*(longlong *)(param_1 + 0x60) != 0) {
                                                    if (*(int *)(*(longlong *)(param_1 + 0x60) +
                                                                0x18) == 0) {
                                                      return;
                                                    }
                                                    if ((lVar12 != 0) &&
                                                       (*(longlong *)(lVar12 + 0x48) != 0)) {
                                                      uVar7 = sub_84a610f0(*(longlong *)
                                                                             (lVar12 + 0x48),0,
                                                                            uRam0000000187b446b8);
                                                      if (cRam000000018ab626a4 == '\0') {
                                                        sub_8032f6d0(0x187c421b8);
                                                        cRam000000018ab626a4 = '\x01';
                                                      }
                                                      if ((*(longlong *)(param_1 + 0xb0) != 0) &&
                                                         (lVar12 = sub_855bdd60(*(longlong *)
                                                                                  (param_1 + 0xb0),
                                                                                 uVar7,
                                                  uRam0000000187c421b8), lVar12 != 0)) {
                                                    if (*(char *)(lVar12 + 0x10) == '\0') {
                                                      return;
                                                    }
                                                    *(undefined4 *)(lVar12 + 0xd0) = 0;
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
                      goto code_r0x0001814c1db1;
                    }
                  }
                  lVar12 = *(longlong *)(param_1 + 0x28);
                  if (lVar12 != 0) {
                    uVar10 = *(undefined8 *)(lVar12 + 0x20);
                    uVar14 = sub_8032f970(uRam0000000187bc09d8);
                    sub_84aa4390(uVar14,uVar10,uRam0000000187b69798);
                    *(undefined8 *)(lVar12 + 0x18) = uVar14;
                    sub_8032e8d0(lVar12 + 0x18,uVar14);
                    iVar6 = 0;
                    if (*(longlong *)(lVar12 + 0x28) != 0) {
                      iVar9 = *(int *)(*(longlong *)(lVar12 + 0x28) + 0x18);
                      if (0 < iVar9) {
                        do {
                          if (*(longlong *)(lVar12 + 0x28) == 0) goto code_r0x0001814c1db1;
                          lVar13 = sub_849ed2d0(*(longlong *)(lVar12 + 0x28),iVar6,
                                                 uRam0000000187b6a3c0);
                          iVar8 = 0;
                          if (lVar13 == 0) goto code_r0x0001814c1db1;
                          for (; iVar8 < *(int *)(lVar13 + 0x1c); iVar8 = iVar8 + 1) {
                            if (*(int *)(lVar13 + 0x20) == 0) {
                              lVar15 = *(longlong *)(lVar12 + 0x18);
                              lVar17 = sub_8032f970(uRam0000000187c332b0);
                              *(undefined8 *)(lVar17 + 0x18) = 0x3ff0000000000000;
                              *(undefined4 *)(lVar17 + 0x20) = 0x3f800000;
                              sub_80443e50(lVar17,0);
                              *(float *)(lVar17 + 0x10) =
                                   (float)iVar8 * *(float *)(lVar13 + 0x14) +
                                   *(float *)(lVar13 + 0x10);
code_r0x0001814c0d0e:
                              *(undefined4 *)(lVar17 + 0x14) = 1;
                              if (lVar15 == 0) goto code_r0x0001814c1db1;
                              sub_80002460(lVar15);
                            }
                            else if (*(int *)(lVar13 + 0x20) == 1) {
                              lVar15 = *(longlong *)(lVar12 + 0x18);
                              lVar17 = sub_8032f970(uRam0000000187c332b0);
                              *(undefined8 *)(lVar17 + 0x18) = 0x3ff0000000000000;
                              *(undefined4 *)(lVar17 + 0x20) = 0x3f800000;
                              sub_80443e50(lVar17,0);
                              fVar1 = *(float *)(lVar13 + 0x10);
                              if (*(longlong *)(lVar13 + 0x28) != 0) {
                                fVar19 = (float)AnimationCurve::Evaluate(*(longlong *)(lVar13 + 0x28),
                                                              (float)iVar8 /
                                                              (float)*(int *)(lVar13 + 0x1c),0);
                                *(float *)(lVar17 + 0x10) =
                                     (float)*(int *)(lVar13 + 0x1c) * fVar19 *
                                     *(float *)(lVar13 + 0x14) + fVar1;
                                goto code_r0x0001814c0d0e;
                              }
                              goto code_r0x0001814c1db1;
                            }
                          }
                          iVar6 = iVar6 + 1;
                        } while (iVar6 < iVar9);
                      }
                      lVar13 = *(longlong *)(lVar12 + 0x18);
                      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
                        sub_8032fa70();
                      }
                      lVar15 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 8);
                      if (lVar15 == 0) {
                        if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
                          sub_8032fa70();
                        }
                        uVar10 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
                        lVar15 = sub_8032f970(uRam0000000187b802a8);
                        sub_854e7780(lVar15,uVar10,uRam0000000187bcefe8,0);
                        *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 8) = lVar15;
                        sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 8,lVar15);
                      }
                      if (lVar13 != 0) {
                        sub_84a9fb90(lVar13,lVar15,uRam0000000187b69b10);
                        plVar11[0xe] = *(longlong *)(lVar12 + 0x18);
                        sub_8032e8d0(plVar11 + 0xe);
                        if (*(longlong *)(lVar12 + 0x18) != 0) {
                          lVar13 = sub_8032e960(uRam0000000187c013d8,
                                                       *(undefined4 *)
                                                        (*(longlong *)(lVar12 + 0x18) + 0x18));
                          plVar11[0xd] = lVar13;
                          sub_8032e8d0(plVar11 + 0xd,lVar13);
                          uVar18 = 0;
                          while (lVar13 = *(longlong *)(lVar12 + 0x18), lVar13 != 0) {
                            if (*(int *)(lVar13 + 0x18) <= (int)uVar18) goto code_r0x0001814c0eeb;
                            dVar2 = (double)plVar11[0xf];
                            lVar13 = sub_849ed2d0(lVar13,uVar18,uRam0000000187b69e88);
                            if (lVar13 == 0) break;
                            plVar11[0xf] = (longlong)(dVar2 + *(double *)(lVar13 + 0x18));
                            lVar13 = plVar11[0xd];
                            if (((*(longlong *)(lVar12 + 0x18) == 0) ||
                                (lVar15 = sub_849ed2d0(*(longlong *)(lVar12 + 0x18),uVar18,
                                                        uRam0000000187b69e88), lVar15 == 0)) ||
                               (lVar13 == 0)) break;
                            if (*(uint *)(lVar13 + 0x18) <= uVar18) goto code_r0x0001814c1db7;
                            *(undefined4 *)(lVar13 + 0x20 + (longlong)(int)uVar18 * 4) =
                                 *(undefined4 *)(lVar15 + 0x10);
                            uVar18 = uVar18 + 1;
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
  }
code_r0x0001814c1db1:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitActionController::InitializeAction  @ 0x14c0210  size=1 ==== */

void UnitActionController::InitializeAction(longlong param_1,longlong param_2)

{
  longlong lVar1;
  code *pcVar2;
  longlong *plVar3;
  int iVar4;
  
  if (cRam000000018ab6269d == '\0') {
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab6269d = '\x01';
  }
  iVar4 = 0;
  if (param_2 != 0) {
    while (lVar1 = *(longlong *)(param_2 + 0x20), lVar1 != 0) {
      if (*(int *)(lVar1 + 0x18) <= iVar4) {
        return;
      }
      plVar3 = (longlong *)sub_849ed2d0(lVar1,iVar4,uRam0000000187c19e18);
      if (plVar3 == (longlong *)0x0) break;
      (**(code **)(*plVar3 + 0x248))
                (plVar3,*(undefined8 *)(param_1 + 0x90),*(undefined8 *)(*plVar3 + 0x250));
      iVar4 = iVar4 + 1;
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::IsLoopMotionPlaying  @ 0x14c1e30  size=1 ==== */

undefined8 UnitActionController::IsLoopMotionPlaying(longlong param_1,undefined4 param_2)

{
  undefined4 uVar1;
  undefined4 uVar2;
  code *pcVar3;
  char cVar4;
  longlong lVar5;
  longlong lVar6;
  longlong lVar7;
  undefined8 uVar8;
  ulonglong uVar9;
  ulonglong uVar10;
  longlong lVar11;
  
  if (cRam000000018ab626c3 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626c3 = '\x01';
  }
  if ((*(longlong *)(param_1 + 0xb0) != 0) &&
     (lVar5 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8), lVar5 != 0)
     ) {
    uVar1 = *(undefined4 *)(lVar5 + 0x80);
    uVar2 = *(undefined4 *)(lVar5 + 0x94);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar5 = UnitCtrl::GetCurrentSpineCtrl(*(longlong *)(param_1 + 0x90),0), lVar5 != 0)) {
      lVar5 = SkeletonAnimation::get_AnimationName(lVar5,0);
      if ((*(longlong *)(param_1 + 0x90) != 0) &&
         (lVar6 = UnitCtrl::GetCurrentSpineCtrl(*(longlong *)(param_1 + 0x90),0), lVar6 != 0)) {
        lVar6 = BattleUnitBaseSpineController::ConvertAnimeIdToAnimeName(lVar6,uVar1,uVar2,1,0xffffffff,0);
        if (lVar5 == lVar6) {
          return 1;
        }
        if (((lVar5 != 0) && (lVar6 != 0)) && (*(int *)(lVar5 + 0x10) == *(int *)(lVar6 + 0x10))) {
          lVar6 = lVar6 + 0x14;
          lVar11 = (longlong)*(int *)(lVar5 + 0x10) * 2;
          lVar5 = lVar5 + 0x14;
          if (lVar5 != lVar6) {
            lVar7 = sub_85b3b780(0,0,lVar11,0);
            uVar8 = sub_841018f0(lVar11,0);
            uVar9 = sub_841018f0(uVar8,0);
            if (7 < uVar9) {
              lVar11 = IntPtr::op_Subtraction(uVar8,8);
              uVar9 = sub_841018f0(lVar11,0);
              uVar10 = sub_841018f0(lVar7,0);
              if (uVar10 < uVar9) {
                do {
                  cVar4 = sub_83a56b20(*(undefined8 *)(lVar7 + lVar5),
                                              *(undefined8 *)(lVar7 + lVar6),0);
                  if (cVar4 != '\0') {
                    return 0;
                  }
                  lVar7 = sub_848d0b80(lVar7,8);
                  uVar9 = sub_841018f0(lVar11,0);
                  uVar10 = sub_841018f0(lVar7,0);
                } while (uVar10 < uVar9);
              }
              uVar8 = sub_80444220(*(undefined8 *)(lVar11 + lVar5),
                                          *(undefined8 *)(lVar11 + lVar6),0);
              return uVar8;
            }
            uVar9 = sub_841018f0(uVar8,0);
            uVar10 = sub_841018f0(lVar7,0);
            if (uVar10 < uVar9) {
              do {
                if (*(char *)(lVar7 + lVar5) != *(char *)(lVar7 + lVar6)) {
                  return 0;
                }
                lVar7 = sub_848d0b80(lVar7,1);
                uVar9 = sub_841018f0(uVar8,0);
                uVar10 = sub_841018f0(lVar7,0);
              } while (uVar10 < uVar9);
            }
          }
          return 1;
        }
        return 0;
      }
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar8 = (*pcVar3)();
  return uVar8;
}



/* ==== UnitActionController::IsModeChange  @ 0x14c1f30  size=1 ==== */

ulonglong UnitActionController::IsModeChange(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab626c7 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626c7 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      return (ulonglong)*(byte *)(lVar2 + 0xf4);
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return uVar3;
}



/* ==== UnitActionController::OnDestroy  @ 0x14c1fa0  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001814c1fb9: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814c1fd7: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814c1ff5: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814c2013: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814c2031: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814c2055: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814c207f: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x0001814c205a) */
/* WARNING: Removing unreachable block (ram,0x0001814c2036) */
/* WARNING: Removing unreachable block (ram,0x0001814c2018) */
/* WARNING: Removing unreachable block (ram,0x0001814c1ffa) */
/* WARNING: Removing unreachable block (ram,0x0001814c1fdc) */
/* WARNING: Removing unreachable block (ram,0x0001814c1fbe) */
/* WARNING: Removing unreachable block (ram,0x0001814c2084) */

void UnitActionController::OnDestroy(longlong param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x28) = 0;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x28U >> 0xc);
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



/* ==== UnitActionController::SearchTargetByAction  @ 0x14c20b0  size=1 ==== */

void UnitActionController::SearchTargetByAction(longlong param_1,undefined4 param_2)

{
  undefined8 uVar1;
  char cVar2;
  int iVar3;
  int iVar4;
  code *pcVar5;
  undefined1 uVar6;
  undefined4 uVar7;
  longlong lVar8;
  longlong lVar9;
  undefined8 *puVar10;
  longlong *plVar11;
  longlong lVar12;
  int iVar13;
  int iVar14;
  double dVar15;
  float fVar16;
  undefined1 uStackX_10;
  int iStackX_20;
  float fStack_108;
  float fStack_104;
  undefined8 uStack_d8;
  undefined4 uStack_d0;
  undefined8 uStack_c8;
  float fStack_c0;
  undefined1 auStack_b8 [16];
  undefined1 auStack_a8 [16];
  undefined1 auStack_98 [16];
  undefined1 auStack_88 [16];
  undefined1 auStack_78 [64];
  
  if (cRam000000018ab626aa == '\0') {
    sub_8032f6d0(0x187b40048);
    sub_8032f6d0(0x187c421b8);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b446b8);
    cRam000000018ab626aa = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar8 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    iStackX_20 = 0;
    if ((lVar8 != 0) && (*(longlong *)(lVar8 + 0x20) != 0)) {
      iVar3 = *(int *)(*(longlong *)(lVar8 + 0x20) + 0x18);
      if (0 < iVar3) {
        do {
          if ((*(longlong *)(lVar8 + 0x20) == 0) ||
             (lVar9 = sub_849ed2d0(*(longlong *)(lVar8 + 0x20),iStackX_20,uRam0000000187c19e18),
             lVar9 == 0)) goto code_r0x0001814c25d5;
          if (*(char *)(lVar9 + 0x86) == '\0') {
            if (*(longlong *)(param_1 + 0x20) == 0) goto code_r0x0001814c25d5;
            puVar10 = (undefined8 *)FixedTransform::get_position(auStack_b8,*(longlong *)(param_1 + 0x20),0);
            uStack_d8 = *puVar10;
            uStack_d0 = *(undefined4 *)(puVar10 + 1);
            UnitActionController::searchAndSortTarget(param_1,lVar8,lVar9,&uStack_d8,1,1,0);
            *(undefined1 *)(lVar9 + 0x1a4) = 0;
            for (iVar13 = 0; iVar13 < *(int *)(lVar9 + 0x1c); iVar13 = iVar13 + 1) {
              lVar12 = *(longlong *)(lVar9 + 0x50);
              if (lVar12 == 0) goto code_r0x0001814c25d5;
              if ((iVar13 < *(int *)(lVar12 + 0x18)) && (*(int *)(lVar9 + 0x60) == 7)) {
                iVar14 = *(int *)(lVar9 + 0x2c);
                fVar16 = 0.0;
                uVar6 = 0;
                uStackX_10 = 1;
                if (iVar14 == 1) {
                  if ((lVar12 == 0) ||
                     (plVar11 = (longlong *)sub_849ed2d0(lVar12,0,uRam0000000187b812d0),
                     plVar11 == (longlong *)0x0)) goto code_r0x0001814c25d5;
                  puVar10 = (undefined8 *)
                            (**(code **)(*plVar11 + 0x178))
                                      (auStack_78,plVar11,*(undefined8 *)(*plVar11 + 0x180));
code_r0x0001814c2451:
                  fVar16 = *(float *)(puVar10 + 1);
                  fStack_108 = (float)*puVar10;
                  fStack_104 = (float)((ulonglong)*puVar10 >> 0x20);
                  if ((plVar11[0xd] == 0) ||
                     (lVar12 = *(longlong *)(plVar11[0xd] + 0x38), lVar12 == 0))
                  goto code_r0x0001814c25d5;
                  uVar6 = CLKMBEGHOLA::BHNFPDMBOAM(lVar12,0x47);
                }
                else if (iVar14 == 2) {
                  if (*(longlong *)(param_1 + 0x20) == 0) goto code_r0x0001814c25d5;
                  puVar10 = (undefined8 *)FixedTransform::get_position(auStack_88,*(longlong *)(param_1 + 0x20),0);
                  if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c25d5;
                  cVar2 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x165);
                  uVar1 = *puVar10;
                  fVar16 = *(float *)(puVar10 + 1);
                  if (*(longlong *)(lVar9 + 0x58) == 0) goto code_r0x0001814c25d5;
                  dVar15 = (double)sub_855f06a0(*(longlong *)(lVar9 + 0x58),0);
                  fStack_108 = (float)uVar1;
                  iVar14 = -1;
                  if (cVar2 == '\0') {
                    iVar14 = 1;
                  }
                  fVar16 = fVar16 + 0.0;
                  fStack_108 = fStack_108 + ((float)dVar15 * (float)iVar14) / 540.0;
                  fStack_104 = (float)((ulonglong)uVar1 >> 0x20);
                  fStack_104 = fStack_104 + 0.0;
                }
                else if (iVar14 == 3) {
                  if (*(longlong *)(param_1 + 0x20) == 0) goto code_r0x0001814c25d5;
                  puVar10 = (undefined8 *)FixedTransform::get_position(auStack_98,*(longlong *)(param_1 + 0x20),0);
                  fStack_108 = (float)*puVar10;
                  fStack_104 = (float)((ulonglong)*puVar10 >> 0x20);
                  fVar16 = *(float *)(puVar10 + 1);
                }
                else {
                  if (iVar14 == 4) {
                    if ((lVar12 != 0) &&
                       (plVar11 = (longlong *)sub_849ed2d0(lVar12,0,uRam0000000187b812d0),
                       plVar11 != (longlong *)0x0)) {
                      puVar10 = (undefined8 *)(**(code **)(*plVar11 + 0x178))(auStack_a8,plVar11);
                      uStackX_10 = 0;
                      goto code_r0x0001814c2451;
                    }
                    goto code_r0x0001814c25d5;
                  }
                  fStack_108 = 0.0;
                  fStack_104 = 0.0;
                }
                iVar14 = 0;
                if (*(longlong *)(lVar9 + 0x88) == 0) goto code_r0x0001814c25d5;
                iVar4 = *(int *)(*(longlong *)(lVar9 + 0x88) + 0x18);
                if (0 < iVar4) {
                  do {
                    lVar12 = *(longlong *)(lVar8 + 0x20);
                    if (((*(longlong *)(lVar9 + 0x88) == 0) ||
                        (uVar7 = sub_84a610f0(*(longlong *)(lVar9 + 0x88),iVar14,
                                               uRam0000000187b446b8), lVar12 == 0)) ||
                       (lVar12 = sub_849ed2d0(lVar12,uVar7,uRam0000000187c19e18), lVar12 == 0))
                    goto code_r0x0001814c25d5;
                    *(undefined1 *)(lVar12 + 0x98) = uVar6;
                    uStack_c8 = CONCAT44(fStack_104,fStack_108);
                    fStack_c0 = fVar16;
                    UnitActionController::searchAndSortTarget(param_1,lVar8,lVar12,&uStack_c8,0,uStackX_10,0);
                    iVar14 = iVar14 + 1;
                    *(undefined1 *)(lVar12 + 0x1a4) = 0;
                  } while (iVar14 < iVar4);
                }
              }
            }
          }
          iStackX_20 = iStackX_20 + 1;
        } while (iStackX_20 < iVar3);
      }
      return;
    }
  }
code_r0x0001814c25d5:
  sub_8032f9d0();
  pcVar5 = (code *)swi(3);
  (*pcVar5)();
  return;
}



/* ==== UnitActionController::SetAttackValue  @ 0x14c25e0  size=1 ==== */

void UnitActionController::SetAttackValue(longlong param_1,longlong param_2)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  undefined8 uStack_60;
  undefined8 uStack_58;
  longlong lStack_50;
  undefined4 uStack_48;
  undefined4 uStack_44;
  undefined4 uStack_40;
  undefined4 uStack_3c;
  longlong lStack_38;
  
  if (cRam000000018ab626d6 == '\0') {
    sub_8032f6d0(0x187b401f8);
    sub_8032f6d0(0x187c4c378);
    sub_8032f6d0(0x187c4c530);
    sub_8032f6d0(0x187c4c6e0);
    sub_8032f6d0(0x187c19ab0);
    cRam000000018ab626d6 = '\x01';
  }
  if ((*(longlong *)(param_1 + 0x38) == 0) ||
     (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x20), lVar1 == 0)) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  sub_849e2810(&uStack_60,lVar1,uRam0000000187c19ab0);
  uStack_48 = (undefined4)uStack_60;
  uStack_44 = uStack_60._4_4_;
  uStack_40 = (undefined4)uStack_58;
  uStack_3c = uStack_58._4_4_;
  lStack_38 = lStack_50;
  uStack_60 = 0;
  uStack_58 = &uStack_48;
  while( true ) {
    cVar3 = sub_8572bc70(&uStack_48,uRam0000000187c4c530);
    if (cVar3 == '\0') {
      sub_80443e50(&uStack_48,uRam0000000187c4c378);
      return;
    }
    if (lStack_38 == 0) break;
    if (*(longlong *)(lStack_38 + 0x58) == 0) {
      sub_8032f9d0();
      break;
    }
    sub_855f0a20(*(longlong *)(lStack_38 + 0x58),2,(double)param_2 / 10000.0 + 1.0,
                  uRam0000000187b401f8);
  }
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::SetBonusId  @ 0x14c2760  size=1 ==== */

void UnitActionController::SetBonusId(longlong param_1,undefined4 param_2,undefined4 param_3)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626c8 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626c8 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      *(undefined4 *)(lVar2 + 0x198) = param_3;
      return;
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitActionController::StartAction  @ 0x14c3a20  size=1 ==== */

undefined8 UnitActionController::StartAction(longlong param_1,int param_2)

{
  int iVar1;
  undefined4 uVar2;
  undefined8 uVar3;
  code *pcVar4;
  undefined8 uVar5;
  char cVar6;
  char cVar7;
  undefined4 uVar8;
  undefined4 uVar9;
  undefined4 uVar10;
  longlong lVar11;
  longlong lVar12;
  undefined8 *puVar13;
  longlong lVar14;
  longlong *plVar15;
  longlong lVar16;
  undefined8 uVar17;
  longlong lVar18;
  ushort uVar19;
  ushort uVar20;
  int iVar21;
  ulonglong uVar22;
  int iVar23;
  uint uVar24;
  ulonglong uVar26;
  bool bVar27;
  char acStackX_18 [16];
  undefined4 uVar30;
  undefined8 uVar28;
  ulonglong uVar29;
  undefined8 in_stack_ffffffffffffff38;
  ulonglong uVar31;
  undefined8 uVar32;
  ulonglong in_stack_ffffffffffffff50;
  undefined8 uStack_78;
  undefined8 uStack_70;
  ulonglong uVar25;
  
  if (cRam000000018ab6269f == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab6269f = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) == 0) {
    sub_8032f9d0();
    pcVar4 = (code *)swi(3);
    uVar17 = (*pcVar4)();
    return uVar17;
  }
  lVar18 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
  if (cRam000000018ab626a0 == '\0') {
    sub_8032f6d0(0x187b8aab0);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187b94060);
    sub_8032f6d0(0x187b42140);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b73698);
    sub_8032f6d0(0x187c24e18);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187c24fc8);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b73858);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187bb0fd0);
    cRam000000018ab626a0 = '\x01';
  }
  iVar21 = 0;
  uVar31 = 0;
  acStackX_18[0] = '\0';
  uVar29 = CONCAT71((int7)((ulonglong)in_stack_ffffffffffffff38 >> 8),1);
  UnitActionController::actionStartFirstProcess(param_1,lVar18,acStackX_18,0,uVar29,0);
  if (lVar18 == 0) goto code_r0x0001814c3a0c;
  lVar16 = *(longlong *)(lVar18 + 0xa8);
  if (0.0 < *(float *)(lVar18 + 0xb0)) {
    while( true ) {
      uVar30 = (undefined4)(uVar29 >> 0x20);
      iVar23 = 0;
      lVar11 = *(longlong *)(lVar18 + 0x20);
      if (lVar11 == 0) break;
      if (*(int *)(lVar11 + 0x18) <= iVar21) {
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        lVar11 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
        if (lVar11 != 0) {
          uVar29 = CONCAT44(uVar30,*(undefined4 *)(lVar18 + 0xd0));
          sub_800158b0(0x15,lRam0000000187b93630,lVar11,lVar18,uVar29);
          goto code_r0x0001814c2c96;
        }
        break;
      }
      lVar11 = sub_849ed2d0(lVar11,iVar21,uRam0000000187c19e18);
      if (lVar16 == 0) break;
      cVar6 = sub_84a7f390(lVar16,iVar21,uRam0000000187b42140);
      if (cVar6 == '\0') {
code_r0x0001814c2a5e:
        if (lVar11 == 0) break;
        if (*(char *)(lVar11 + 0x84) == '\0') {
          if (*(longlong *)(lVar11 + 0x50) == 0) break;
          iVar1 = *(int *)(*(longlong *)(lVar11 + 0x50) + 0x18);
          if (0 < iVar1) {
            do {
              if (((*(int *)(lVar11 + 0x1c) <= iVar23) && (*(int *)(lVar11 + 0x1c) != 0)) &&
                 (iVar23 != 0)) break;
              if ((*(longlong *)(lVar11 + 0x50) == 0) ||
                 (lVar12 = sub_849ed2d0(*(longlong *)(lVar11 + 0x50),iVar23,uRam0000000187b812d0),
                 lVar12 == 0)) goto code_r0x0001814c3a0c;
              uVar25 = *(ulonglong *)(lVar12 + 0x68);
              if (*(int *)(lVar11 + 0x10) == 1) {
                if ((uVar25 == 0) || (*(longlong *)(uVar25 + 0xb0) == 0)) goto code_r0x0001814c3a0c;
                sub_804f1ed0(*(longlong *)(uVar25 + 0xb0),1,0);
              }
              lVar12 = sub_814a9080(param_1,0);
              uVar17 = *(undefined8 *)(param_1 + 0x90);
              if ((*(longlong *)(lVar11 + 0x50) == 0) ||
                 (uVar31 = sub_849ed2d0(*(longlong *)(lVar11 + 0x50),iVar23,uRam0000000187b812d0),
                 lVar12 == 0)) goto code_r0x0001814c3a0c;
              uVar29 = uVar25;
              sub_80012050(2,lRam0000000187b93630,lVar12,uVar17,uVar25,uVar31);
              uVar17 = *(undefined8 *)(param_1 + 0x90);
              if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
                sub_8032fa70();
              }
              cVar6 = Object::op_Inequality(uVar17,uVar25,0);
              if ((cVar6 != '\0') && (*(char *)(lVar18 + 0x90) != '\0')) {
                if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
                if (param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
                  lVar12 = sub_814a9080(param_1,0);
                  if (lVar12 == 0) goto code_r0x0001814c3a0c;
                  cVar6 = sub_80002270(0xb,lRam0000000187b93630,lVar12);
                  if (cVar6 != '\0') goto code_r0x0001814c2c10;
                }
                if ((uVar25 == 0) || (lVar12 = UnitCtrl::GetCurrentSpineCtrl(uVar25,0), lVar12 == 0))
                goto code_r0x0001814c3a0c;
                uStack_78 = 0x3f8000003f800000;
                uStack_70 = 0x3f800000;
                SpineController::set_CurColor(lVar12,&uStack_78,0);
              }
code_r0x0001814c2c10:
              iVar23 = iVar23 + 1;
            } while (iVar23 < iVar1);
          }
        }
      }
      else {
        if (lVar11 == 0) break;
        if (*(char *)(lVar11 + 0x86) != '\0') goto code_r0x0001814c2a5e;
      }
      iVar21 = iVar21 + 1;
    }
    goto code_r0x0001814c3a0c;
  }
code_r0x0001814c2c96:
  bVar27 = false;
  if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
  cVar6 = '\x01';
  if (param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    cVar7 = '\x01';
    if (*(char *)(lVar18 + 0xb4) == '\0') {
      cVar7 = *(char *)(lVar18 + 0x10);
    }
    uVar30 = *(undefined4 *)(lVar18 + 0xb0);
    uVar17 = *(undefined8 *)(param_1 + 0x90);
    plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
    if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
    lVar11 = *plVar15;
    uVar19 = 0;
    uVar3 = *(undefined8 *)(lVar18 + 0xb8);
    uVar5 = *(undefined8 *)(lVar18 + 0xc0);
    if (*(ushort *)(lVar11 + 0x12e) != 0) {
      do {
        if (*(longlong *)(*(longlong *)(lVar11 + 0xb0) + (ulonglong)uVar19 * 0x10) ==
            lRam0000000187b93630) {
          puVar13 = (undefined8 *)
                    ((longlong)*(int *)(*(longlong *)(lVar11 + 0xb0) + 8 + (ulonglong)uVar19 * 0x10)
                     * 0x10 + 0x138 + lVar11);
          goto code_r0x0001814c2d76;
        }
        uVar19 = uVar19 + 1;
      } while (uVar19 < *(ushort *)(lVar11 + 0x12e));
    }
    puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b93630,0);
code_r0x0001814c2d76:
    uVar32 = puVar13[1];
    uVar28 = CONCAT71((int7)(uVar29 >> 8),cVar7 != '\0');
    uStack_78 = uVar3;
    uStack_70 = uVar5;
    (*(code *)*puVar13)(plVar15,uVar17,uVar30,&uStack_78,uVar28,uVar32);
    uVar30 = (undefined4)((ulonglong)uVar32 >> 0x20);
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    lVar11 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar11 == 0)) goto code_r0x0001814c3a0c;
    uVar31 = CONCAT44(uVar30,*(undefined4 *)(lVar18 + 0xd0));
    uVar29 = CONCAT71((int7)((ulonglong)uVar28 >> 8),1);
    sub_80015960(1,lRam0000000187b93630,lVar11,*(undefined4 *)(lVar18 + 0x78),uVar29,uVar31,
                  *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0xd0));
    if (cRam000000018ab626dc == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626dc = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20) == 0)
    goto code_r0x0001814c3a0c;
    sub_80002360(1,lRam0000000187b94060);
    if (*(longlong *)(lVar18 + 0x50) == 0) goto code_r0x0001814c3a0c;
    if (*(char *)(*(longlong *)(lVar18 + 0x50) + 0x10) != '\0') {
      if (cRam000000018ab626dc == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626dc = '\x01';
      }
      uVar17 = *(undefined8 *)(lVar18 + 0x50);
      uVar3 = *(undefined8 *)(param_1 + 0x90);
      uVar30 = *(undefined4 *)(lVar18 + 0xd0);
      plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20);
      if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
      lVar11 = *plVar15;
      uVar31 = 0;
      if (*(ushort *)(lVar11 + 0x12e) != 0) {
        do {
          if (*(longlong *)(*(longlong *)(lVar11 + 0xb0) + uVar31 * 0x10) == lRam0000000187b94060) {
            puVar13 = (undefined8 *)
                      ((longlong)*(int *)(*(longlong *)(lVar11 + 0xb0) + 8 + uVar31 * 0x10) * 0x10 +
                       0x138 + lVar11);
            goto code_r0x0001814c2f16;
          }
          uVar19 = (short)uVar31 + 1;
          uVar31 = (ulonglong)uVar19;
        } while (uVar19 < *(ushort *)(lVar11 + 0x12e));
      }
      puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b94060,0);
code_r0x0001814c2f16:
      uVar31 = puVar13[1];
      uVar29 = uVar29 & 0xffffffffffffff00;
      (*(code *)*puVar13)(plVar15,uVar17,uVar3,uVar30,uVar29,uVar31);
    }
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
    cVar7 = UnitCtrl::get_IsPlayerUnit(*(longlong *)(param_1 + 0x90),0);
    bVar27 = cVar7 != '\0';
  }
  uVar25 = 0;
  lVar11 = *(longlong *)(param_1 + 0x90);
  if (lVar11 == 0) goto code_r0x0001814c3a0c;
  if ((*(char *)(lVar11 + 0x1a8) != '\0') && (param_2 != *(int *)(lVar11 + 0x4b8))) {
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    lVar11 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar11 == 0)) goto code_r0x0001814c3a0c;
    uVar31 = uVar31 & 0xffffffff00000000;
    uVar29 = uVar29 & 0xffffffffffffff00;
    sub_80015960(1,lRam0000000187b93630,lVar11,*(undefined4 *)(lVar18 + 0x78),uVar29,uVar31,
                  *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0xd0));
  }
  if (bVar27) {
code_r0x0001814c3012:
    lVar11 = *(longlong *)(lRam0000000187bb0fd0 + 0x20);
    if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
      lVar11 = sub_8032d010(lVar11);
    }
    lVar11 = *(longlong *)(*(longlong *)(lVar11 + 0xc0) + 0x10);
    if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
      lVar11 = sub_8032d010(lVar11);
    }
    if (**(longlong **)(lVar11 + 0xb8) == 0) goto code_r0x0001814c3a0c;
    lVar11 = *(longlong *)(**(longlong **)(lVar11 + 0xb8) + 200);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar11 == 0)) goto code_r0x0001814c3a0c;
    PartsSkillWindow::Indicate(lVar11,*(undefined8 *)(lVar18 + 0xe0),
                  *(undefined1 *)(*(longlong *)(param_1 + 0x90) + 0x1a8),0);
  }
  else {
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
    cVar6 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x1a8);
    if (cVar6 != '\0') goto code_r0x0001814c3012;
  }
  if (*(longlong *)(param_1 + 0x90) != 0) {
    lVar11 = UnitCtrl::GetCurrentSpineCtrl(*(longlong *)(param_1 + 0x90),0);
    cVar7 = acStackX_18[0];
    if (*(char *)(lVar18 + 0x58) != '\0') {
      cVar7 = '\0';
    }
    if (*(longlong *)(param_1 + 0x90) != 0) {
      lVar12 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
      if (cVar7 == '\0') {
        if (lVar12 == 0) goto code_r0x0001814c3a0c;
        cVar7 = CLKMBEGHOLA::BOGEAFNKHMI(lVar12,0);
        if ((cVar7 == '\0') || (*(char *)(lVar12 + 0x1f8) != '\0')) {
          uVar22 = uVar25;
          if (cVar6 == '\0') {
            if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
            UnitCtrl::IndicateSkillName(*(longlong *)(param_1 + 0x90),*(undefined8 *)(lVar18 + 0xe0),0);
          }
          while (lVar14 = *(longlong *)(lVar18 + 0x38), lVar14 != 0) {
            if (*(int *)(lVar14 + 0x18) <= (int)uVar22) {
              if (cRam000000018ab626bf == '\0') {
                sub_8032f6d0(0x187bd2190);
                sub_8032f6d0(0x187bd2348);
                cRam000000018ab626bf = '\x01';
              }
              if (*(longlong *)(lVar18 + 0x40) != 0) {
                iVar21 = *(int *)(*(longlong *)(lVar18 + 0x40) + 0x18);
                if (iVar21 < 1) goto code_r0x0001814c3321;
                goto code_r0x0001814c3251;
              }
              break;
            }
            lVar14 = sub_849ed2d0(lVar14,uVar22,uRam0000000187c24fc8);
            if (lVar14 == 0) break;
            if (*(int *)(lVar14 + 0x1c) == 0) {
              if (*(longlong *)(lVar18 + 0x38) == 0) break;
              uVar17 = sub_849ed2d0(*(longlong *)(lVar18 + 0x38),uVar22,uRam0000000187c24fc8);
              uVar17 = UnitActionController::StartShakeWithDelay(param_1,uVar17,lVar18,1,0);
              uVar26 = uVar25;
              if (0.0 < *(float *)(lVar18 + 0xb0)) {
                uVar26 = *(ulonglong *)(param_1 + 0x90);
              }
              uVar29 = 0;
              UnitActionController::AppendCoroutine(param_1,uVar17,0,uVar26,0);
            }
            uVar22 = (ulonglong)((int)uVar22 + 1);
          }
          goto code_r0x0001814c3a0c;
        }
      }
      uVar30 = (undefined4)(uVar29 >> 0x20);
      if (*(longlong *)(param_1 + 0x90) != 0) {
        if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x1a8) == '\0') {
          return 0;
        }
        if (lVar12 != 0) {
          cVar6 = CLKMBEGHOLA::GMKFGFKMFCE(lVar12,0);
          if (cVar6 != '\0') {
            return 0;
          }
          if (*(char *)(lVar18 + 0x10) != '\0') {
            return 0;
          }
          uVar2 = *(undefined4 *)(lVar18 + 0x80);
          uVar8 = *(undefined4 *)(lVar18 + 0x94);
          if ((*(longlong *)(param_1 + 0x90) != 0) &&
             (uVar10 = UnitCtrl::get_MotionPrefix(*(longlong *)(param_1 + 0x90),0), lVar11 != 0)) {
            uVar31 = 0;
            uVar17 = CONCAT44(uVar30,uVar10);
            cVar6 = BattleUnitBaseSpineController::IsAnimation(lVar11,uVar2,uVar8,0xffffffff,uVar17,0);
            uVar30 = (undefined4)((ulonglong)uVar17 >> 0x20);
            if (cVar6 == '\0') {
              return 0;
            }
            lVar16 = *(longlong *)(param_1 + 0x90);
            if (lVar16 != 0) {
              lVar11 = *(longlong *)(lVar16 + 0x48);
              uVar2 = *(undefined4 *)(lVar18 + 0x80);
              uVar8 = *(undefined4 *)(lVar18 + 0x94);
              uVar10 = UnitCtrl::get_MotionPrefix(lVar16,0);
              if (lVar11 != 0) {
                uVar31 = uVar31 & 0xffffffffffffff00;
                uVar29 = CONCAT44(uVar30,uVar10);
                OBAJFDJFDJG::LLIPNABPJGC(lVar11,uVar2,uVar8,0xffffffff,uVar29,uVar31,0,
                              in_stack_ffffffffffffff50 & 0xffffffffffffff00,0,0,0);
                UnitActionController::CreateNormalPrefabWithTargetMotion(param_1,lVar18,0,1,uVar29 & 0xffffffffffffff00,
                              uVar31 & 0xffffffffffffff00,0);
                return 0;
              }
            }
          }
        }
      }
    }
  }
  goto code_r0x0001814c3a0c;
  while( true ) {
    uVar17 = sub_849ed2d0(*(longlong *)(lVar18 + 0x40),uVar25,uRam0000000187bd2348);
    if (cRam000000018ab626c0 == '\0') {
      sub_8032f6d0(0x187c21e50);
      cRam000000018ab626c0 = '\x01';
    }
    lVar14 = sub_8032f970(uRam0000000187c21e50);
    sub_80445940(lVar14,0,0);
    if (lVar14 == 0) goto code_r0x0001814c3a0c;
    *(longlong *)(lVar14 + 0x38) = param_1;
    sub_8032e8d0(lVar14 + 0x38,param_1);
    *(undefined8 *)(lVar14 + 0x28) = uVar17;
    sub_8032e8d0(lVar14 + 0x28,uVar17);
    *(longlong *)(lVar14 + 0x30) = lVar18;
    sub_8032e8d0(lVar14 + 0x30,lVar18);
    *(undefined1 *)(lVar14 + 0x20) = 1;
    if (*(float *)(lVar18 + 0xb0) <= 0.0) {
      uVar17 = 0;
    }
    else {
      uVar17 = *(undefined8 *)(param_1 + 0x90);
    }
    uVar29 = 0;
    UnitActionController::AppendCoroutine(param_1,lVar14,0,uVar17,0);
    uVar24 = (int)uVar25 + 1;
    uVar25 = (ulonglong)uVar24;
    if (iVar21 <= (int)uVar24) break;
code_r0x0001814c3251:
    if (*(longlong *)(lVar18 + 0x40) == 0) goto code_r0x0001814c3a0c;
  }
code_r0x0001814c3321:
  if ((*(int *)(lVar18 + 0x80) != 0) && (*(char *)(lVar18 + 0x10) == '\0')) {
    uVar30 = *(undefined4 *)(lVar18 + 0x80);
    uVar2 = *(undefined4 *)(lVar18 + 0x94);
    if ((*(longlong *)(param_1 + 0x90) == 0) ||
       (uVar8 = UnitCtrl::get_MotionPrefix(*(longlong *)(param_1 + 0x90),0), lVar11 == 0))
    goto code_r0x0001814c3a0c;
    uVar31 = 0;
    uVar29 = CONCAT44((int)(uVar29 >> 0x20),uVar8);
    cVar6 = BattleUnitBaseSpineController::IsAnimation(lVar11,uVar30,uVar2,0xffffffff,uVar29,0);
    if (cVar6 != '\0') {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd8), lVar11 == 0))
      goto code_r0x0001814c3a0c;
      if (*(char *)(lVar11 + 0x28) == '\0') {
        uVar30 = *(undefined4 *)(lVar18 + 0x80);
        uVar2 = *(undefined4 *)(lVar18 + 0x94);
        lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x48);
        uVar8 = UnitCtrl::get_MotionPrefix(*(longlong *)(param_1 + 0x90),0);
        if (lVar11 == 0) goto code_r0x0001814c3a0c;
        uVar31 = uVar31 & 0xffffffffffffff00;
        uVar29 = CONCAT44((int)(uVar29 >> 0x20),uVar8);
        OBAJFDJFDJG::LLIPNABPJGC(lVar11,uVar30,uVar2,0xffffffff,uVar29,uVar31,0,
                      in_stack_ffffffffffffff50 & 0xffffffffffffff00,*(undefined4 *)(lVar18 + 0xd0),
                      0,0);
      }
      else {
        sub_8043b700(lVar11,0,0);
        uVar10 = (undefined4)(uVar29 >> 0x20);
        lVar11 = *(longlong *)(param_1 + 0x90);
        uVar30 = *(undefined4 *)(lVar18 + 0xd0);
        uVar2 = *(undefined4 *)(lVar18 + 0x80);
        uVar8 = *(undefined4 *)(lVar18 + 0x94);
        if (lVar11 == 0) goto code_r0x0001814c3a0c;
        uVar9 = UnitCtrl::get_MotionPrefix(lVar11,0);
        uVar31 = 0;
        uVar29 = CONCAT44(uVar10,uVar9);
        UnitCtrl::RestartPlayAnimeCoroutine(lVar11,uVar30,uVar2,uVar8,uVar29,0);
      }
    }
  }
  uVar25 = 0;
  SkillBase::ReadySkill(lVar18,0);
  if (*(longlong *)(param_1 + 0x90) != 0) {
    uVar17 = CLKMBEGHOLA::ANKFGOIFFHH(lVar12,param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8),
                           param_2 == 1,0);
    *(undefined8 *)(lVar18 + 0x160) = uVar17;
    uVar22 = uVar25;
    while (lVar11 = *(longlong *)(lVar18 + 0x20), lVar11 != 0) {
      uVar26 = uVar25;
      if (*(int *)(lVar11 + 0x18) <= (int)uVar22) goto code_r0x0001814c3595;
      plVar15 = (longlong *)sub_849ed2d0(lVar11,uVar22,uRam0000000187c19e18);
      if (plVar15 == (longlong *)0x0) break;
      uVar29 = *(ulonglong *)(*plVar15 + 0x220);
      (**(code **)(*plVar15 + 0x218))(plVar15,*(undefined8 *)(param_1 + 0x90),param_1,lVar18,uVar29)
      ;
      if (lVar16 == 0) break;
      cVar6 = sub_84a7f390(lVar16,uVar22,uRam0000000187b42140);
      if (((cVar6 == '\0') || (*(char *)((longlong)plVar15 + 0x86) != '\0')) &&
         (*(char *)((longlong)plVar15 + 0x85) == '\0')) {
        uVar31 = uVar31 & 0xffffffffffffff00;
        uVar29 = CONCAT71((int7)(uVar29 >> 8),1);
        UnitActionController::ExecUnitActionWithDelay(param_1,plVar15,lVar18,1,uVar29,uVar31,0);
      }
      uVar22 = (ulonglong)((int)uVar22 + 1);
    }
  }
  goto code_r0x0001814c3a0c;
code_r0x0001814c3595:
  if (cRam000000018ab626da == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab626da = '\x01';
  }
  plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
  if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
  lVar16 = *plVar15;
  uVar19 = 0;
  if (*(ushort *)(lVar16 + 0x12e) != 0) {
    uVar20 = uVar19;
    do {
      if (*(longlong *)(*(longlong *)(lVar16 + 0xb0) + (ulonglong)uVar20 * 0x10) ==
          lRam0000000187b93630) {
        puVar13 = (undefined8 *)
                  ((longlong)
                   (*(int *)(*(longlong *)(lVar16 + 0xb0) + 8 + (ulonglong)uVar20 * 0x10) + 9) *
                   0x10 + 0x138 + lVar16);
        goto code_r0x0001814c3619;
      }
      uVar20 = uVar20 + 1;
    } while (uVar20 < *(ushort *)(lVar16 + 0x12e));
  }
  puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b93630,9);
code_r0x0001814c3619:
  lVar16 = (*(code *)*puVar13)(plVar15,puVar13[1]);
  if (lVar16 == 0) goto code_r0x0001814c3a0c;
  if (*(int *)(lVar16 + 0x18) <= (int)uVar26) {
    uVar31 = uVar31 & 0xffffffffffffff00;
    uVar29 = uVar29 & 0xffffffffffffff00;
    UnitActionController::CreateNormalPrefabWithTargetMotion(param_1,lVar18,0,1,uVar29,uVar31,0);
    if (*(longlong *)(lVar18 + 0x48) != 0) {
      if (*(char *)(*(longlong *)(lVar18 + 0x48) + 0x10) == '\0') goto code_r0x0001814c384b;
      if (cRam000000018ab626dd == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626dd = '\x01';
      }
      lVar16 = *(longlong *)(param_1 + 0x90);
      uVar17 = *(undefined8 *)(lVar18 + 0x48);
      uVar30 = *(undefined4 *)(lVar18 + 0xd0);
      iVar21 = *(int *)(lVar18 + 0x78);
      plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x10);
      if ((lVar16 != 0) && (iVar23 = *(int *)(lVar16 + 0x4b8), plVar15 != (longlong *)0x0)) {
        lVar11 = *plVar15;
        if (*(ushort *)(lVar11 + 0x12e) == 0) goto code_r0x0001814c380b;
        goto code_r0x0001814c37f0;
      }
    }
    goto code_r0x0001814c3a0c;
  }
  if (cRam000000018ab626da == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab626da = '\x01';
  }
  plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
  if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
  lVar16 = *plVar15;
  if (*(ushort *)(lVar16 + 0x12e) != 0) {
    do {
      if (*(longlong *)(*(longlong *)(lVar16 + 0xb0) + (ulonglong)uVar19 * 0x10) ==
          lRam0000000187b93630) {
        puVar13 = (undefined8 *)
                  ((longlong)
                   (*(int *)(*(longlong *)(lVar16 + 0xb0) + 8 + (ulonglong)uVar19 * 0x10) + 9) *
                   0x10 + 0x138 + lVar16);
        goto code_r0x0001814c36b7;
      }
      uVar19 = uVar19 + 1;
    } while (uVar19 < *(ushort *)(lVar16 + 0x12e));
  }
  puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b93630,9);
code_r0x0001814c36b7:
  lVar16 = (*(code *)*puVar13)(plVar15,puVar13[1]);
  if ((lVar16 == 0) || (lVar16 = sub_849ed2d0(lVar16,uVar26,uRam0000000187b73858), lVar16 == 0))
  goto code_r0x0001814c3a0c;
  *(undefined1 *)(lVar16 + 0x250) = 0;
  uVar26 = (ulonglong)((int)uVar26 + 1);
  goto code_r0x0001814c3595;
  while (uVar19 = (short)uVar25 + 1, uVar25 = (ulonglong)uVar19,
        uVar19 < *(ushort *)(lVar11 + 0x12e)) {
code_r0x0001814c37f0:
    if (*(longlong *)(*(longlong *)(lVar11 + 0xb0) + uVar25 * 0x10) == lRam0000000187b8aab0) {
      puVar13 = (undefined8 *)
                ((longlong)*(int *)(*(longlong *)(lVar11 + 0xb0) + 8 + uVar25 * 0x10) * 0x10 + 0x138
                + lVar11);
      goto code_r0x0001814c3816;
    }
  }
code_r0x0001814c380b:
  puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b8aab0,0);
code_r0x0001814c3816:
  (*(code *)*puVar13)(plVar15,uVar17,lVar16,uVar30,uVar29 & 0xffffffffffffff00,
                      uVar31 & 0xffffffffffffff00,iVar21 != iVar23,puVar13[1]);
code_r0x0001814c384b:
  if ((*(longlong *)(param_1 + 0x90) != 0) &&
     (lVar16 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xc0), lVar16 != 0)) {
    IDFKMEFJKOD::CNBJILCDDBO(lVar16,*(undefined8 *)(lVar18 + 0x60),*(undefined4 *)(lVar18 + 0xd0),0);
    if (param_2 == 1) {
      EventExtension::Call(*(undefined8 *)(param_1 + 0xa0),0);
    }
    return 1;
  }
code_r0x0001814c3a0c:
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  uVar17 = (*pcVar4)();
  return uVar17;
}



/* ==== UnitActionController::StartAction  @ 0x14c2890  size=1 ==== */

undefined8 UnitActionController::StartAction(longlong param_1,int param_2,longlong param_3)

{
  int iVar1;
  undefined4 uVar2;
  undefined8 uVar3;
  code *pcVar4;
  undefined8 uVar5;
  char cVar6;
  char cVar7;
  undefined4 uVar8;
  undefined4 uVar9;
  undefined4 uVar10;
  longlong lVar11;
  longlong lVar12;
  undefined8 *puVar13;
  longlong lVar14;
  longlong *plVar15;
  longlong lVar16;
  undefined8 uVar17;
  ushort uVar18;
  ushort uVar19;
  int iVar20;
  ulonglong uVar21;
  int iVar22;
  uint uVar23;
  ulonglong uVar25;
  bool bVar26;
  char acStackX_18 [16];
  undefined8 in_stack_ffffffffffffff38;
  undefined4 uVar29;
  undefined8 uVar27;
  ulonglong uVar28;
  ulonglong uVar30;
  undefined8 uVar31;
  ulonglong in_stack_ffffffffffffff50;
  undefined8 uStack_78;
  undefined8 uStack_70;
  ulonglong uVar24;
  
  if (cRam000000018ab626a0 == '\0') {
    sub_8032f6d0(0x187b8aab0);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187b94060);
    sub_8032f6d0(0x187b42140);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b73698);
    sub_8032f6d0(0x187c24e18);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187c24fc8);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b73858);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187bb0fd0);
    cRam000000018ab626a0 = '\x01';
  }
  iVar20 = 0;
  uVar30 = 0;
  acStackX_18[0] = '\0';
  uVar28 = CONCAT71((int7)((ulonglong)in_stack_ffffffffffffff38 >> 8),1);
  UnitActionController::actionStartFirstProcess(param_1,param_3,acStackX_18,0,uVar28,0);
  if (param_3 == 0) goto code_r0x0001814c3a0c;
  lVar16 = *(longlong *)(param_3 + 0xa8);
  if (0.0 < *(float *)(param_3 + 0xb0)) {
    while( true ) {
      uVar29 = (undefined4)(uVar28 >> 0x20);
      iVar22 = 0;
      lVar11 = *(longlong *)(param_3 + 0x20);
      if (lVar11 == 0) break;
      if (*(int *)(lVar11 + 0x18) <= iVar20) {
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        lVar11 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
        if (lVar11 != 0) {
          uVar28 = CONCAT44(uVar29,*(undefined4 *)(param_3 + 0xd0));
          sub_800158b0(0x15,lRam0000000187b93630,lVar11,param_3,uVar28);
          goto code_r0x0001814c2c96;
        }
        break;
      }
      lVar11 = sub_849ed2d0(lVar11,iVar20,uRam0000000187c19e18);
      if (lVar16 == 0) break;
      cVar6 = sub_84a7f390(lVar16,iVar20,uRam0000000187b42140);
      if (cVar6 == '\0') {
code_r0x0001814c2a5e:
        if (lVar11 == 0) break;
        if (*(char *)(lVar11 + 0x84) == '\0') {
          if (*(longlong *)(lVar11 + 0x50) == 0) break;
          iVar1 = *(int *)(*(longlong *)(lVar11 + 0x50) + 0x18);
          if (0 < iVar1) {
            do {
              if (((*(int *)(lVar11 + 0x1c) <= iVar22) && (*(int *)(lVar11 + 0x1c) != 0)) &&
                 (iVar22 != 0)) break;
              if ((*(longlong *)(lVar11 + 0x50) == 0) ||
                 (lVar12 = sub_849ed2d0(*(longlong *)(lVar11 + 0x50),iVar22,uRam0000000187b812d0),
                 lVar12 == 0)) goto code_r0x0001814c3a0c;
              uVar24 = *(ulonglong *)(lVar12 + 0x68);
              if (*(int *)(lVar11 + 0x10) == 1) {
                if ((uVar24 == 0) || (*(longlong *)(uVar24 + 0xb0) == 0)) goto code_r0x0001814c3a0c;
                sub_804f1ed0(*(longlong *)(uVar24 + 0xb0),1,0);
              }
              lVar12 = sub_814a9080(param_1,0);
              uVar17 = *(undefined8 *)(param_1 + 0x90);
              if ((*(longlong *)(lVar11 + 0x50) == 0) ||
                 (uVar30 = sub_849ed2d0(*(longlong *)(lVar11 + 0x50),iVar22,uRam0000000187b812d0),
                 lVar12 == 0)) goto code_r0x0001814c3a0c;
              uVar28 = uVar24;
              sub_80012050(2,lRam0000000187b93630,lVar12,uVar17,uVar24,uVar30);
              uVar17 = *(undefined8 *)(param_1 + 0x90);
              if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
                sub_8032fa70();
              }
              cVar6 = Object::op_Inequality(uVar17,uVar24,0);
              if ((cVar6 != '\0') && (*(char *)(param_3 + 0x90) != '\0')) {
                if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
                if (param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
                  lVar12 = sub_814a9080(param_1,0);
                  if (lVar12 == 0) goto code_r0x0001814c3a0c;
                  cVar6 = sub_80002270(0xb,lRam0000000187b93630,lVar12);
                  if (cVar6 != '\0') goto code_r0x0001814c2c10;
                }
                if ((uVar24 == 0) || (lVar12 = UnitCtrl::GetCurrentSpineCtrl(uVar24,0), lVar12 == 0))
                goto code_r0x0001814c3a0c;
                uStack_78 = 0x3f8000003f800000;
                uStack_70 = 0x3f800000;
                SpineController::set_CurColor(lVar12,&uStack_78,0);
              }
code_r0x0001814c2c10:
              iVar22 = iVar22 + 1;
            } while (iVar22 < iVar1);
          }
        }
      }
      else {
        if (lVar11 == 0) break;
        if (*(char *)(lVar11 + 0x86) != '\0') goto code_r0x0001814c2a5e;
      }
      iVar20 = iVar20 + 1;
    }
    goto code_r0x0001814c3a0c;
  }
code_r0x0001814c2c96:
  bVar26 = false;
  if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
  cVar6 = '\x01';
  if (param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    cVar7 = '\x01';
    if (*(char *)(param_3 + 0xb4) == '\0') {
      cVar7 = *(char *)(param_3 + 0x10);
    }
    uVar29 = *(undefined4 *)(param_3 + 0xb0);
    uVar17 = *(undefined8 *)(param_1 + 0x90);
    plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
    if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
    lVar11 = *plVar15;
    uVar18 = 0;
    uVar3 = *(undefined8 *)(param_3 + 0xb8);
    uVar5 = *(undefined8 *)(param_3 + 0xc0);
    if (*(ushort *)(lVar11 + 0x12e) != 0) {
      do {
        if (*(longlong *)(*(longlong *)(lVar11 + 0xb0) + (ulonglong)uVar18 * 0x10) ==
            lRam0000000187b93630) {
          puVar13 = (undefined8 *)
                    ((longlong)*(int *)(*(longlong *)(lVar11 + 0xb0) + 8 + (ulonglong)uVar18 * 0x10)
                     * 0x10 + 0x138 + lVar11);
          goto code_r0x0001814c2d76;
        }
        uVar18 = uVar18 + 1;
      } while (uVar18 < *(ushort *)(lVar11 + 0x12e));
    }
    puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b93630,0);
code_r0x0001814c2d76:
    uVar31 = puVar13[1];
    uVar27 = CONCAT71((int7)(uVar28 >> 8),cVar7 != '\0');
    uStack_78 = uVar3;
    uStack_70 = uVar5;
    (*(code *)*puVar13)(plVar15,uVar17,uVar29,&uStack_78,uVar27,uVar31);
    uVar29 = (undefined4)((ulonglong)uVar31 >> 0x20);
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    lVar11 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar11 == 0)) goto code_r0x0001814c3a0c;
    uVar30 = CONCAT44(uVar29,*(undefined4 *)(param_3 + 0xd0));
    uVar28 = CONCAT71((int7)((ulonglong)uVar27 >> 8),1);
    sub_80015960(1,lRam0000000187b93630,lVar11,*(undefined4 *)(param_3 + 0x78),uVar28,uVar30,
                  *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0xd0));
    if (cRam000000018ab626dc == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626dc = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20) == 0)
    goto code_r0x0001814c3a0c;
    sub_80002360(1,lRam0000000187b94060);
    if (*(longlong *)(param_3 + 0x50) == 0) goto code_r0x0001814c3a0c;
    if (*(char *)(*(longlong *)(param_3 + 0x50) + 0x10) != '\0') {
      if (cRam000000018ab626dc == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626dc = '\x01';
      }
      uVar17 = *(undefined8 *)(param_3 + 0x50);
      uVar3 = *(undefined8 *)(param_1 + 0x90);
      uVar29 = *(undefined4 *)(param_3 + 0xd0);
      plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20);
      if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
      lVar11 = *plVar15;
      uVar30 = 0;
      if (*(ushort *)(lVar11 + 0x12e) != 0) {
        do {
          if (*(longlong *)(*(longlong *)(lVar11 + 0xb0) + uVar30 * 0x10) == lRam0000000187b94060) {
            puVar13 = (undefined8 *)
                      ((longlong)*(int *)(*(longlong *)(lVar11 + 0xb0) + 8 + uVar30 * 0x10) * 0x10 +
                       0x138 + lVar11);
            goto code_r0x0001814c2f16;
          }
          uVar18 = (short)uVar30 + 1;
          uVar30 = (ulonglong)uVar18;
        } while (uVar18 < *(ushort *)(lVar11 + 0x12e));
      }
      puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b94060,0);
code_r0x0001814c2f16:
      uVar30 = puVar13[1];
      uVar28 = uVar28 & 0xffffffffffffff00;
      (*(code *)*puVar13)(plVar15,uVar17,uVar3,uVar29,uVar28,uVar30);
    }
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
    cVar7 = UnitCtrl::get_IsPlayerUnit(*(longlong *)(param_1 + 0x90),0);
    bVar26 = cVar7 != '\0';
  }
  uVar24 = 0;
  lVar11 = *(longlong *)(param_1 + 0x90);
  if (lVar11 == 0) goto code_r0x0001814c3a0c;
  if ((*(char *)(lVar11 + 0x1a8) != '\0') && (param_2 != *(int *)(lVar11 + 0x4b8))) {
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    lVar11 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar11 == 0)) goto code_r0x0001814c3a0c;
    uVar30 = uVar30 & 0xffffffff00000000;
    uVar28 = uVar28 & 0xffffffffffffff00;
    sub_80015960(1,lRam0000000187b93630,lVar11,*(undefined4 *)(param_3 + 0x78),uVar28,uVar30,
                  *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0xd0));
  }
  if (bVar26) {
code_r0x0001814c3012:
    lVar11 = *(longlong *)(lRam0000000187bb0fd0 + 0x20);
    if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
      lVar11 = sub_8032d010(lVar11);
    }
    lVar11 = *(longlong *)(*(longlong *)(lVar11 + 0xc0) + 0x10);
    if ((*(byte *)(lVar11 + 0x135) & 1) == 0) {
      lVar11 = sub_8032d010(lVar11);
    }
    if (**(longlong **)(lVar11 + 0xb8) == 0) goto code_r0x0001814c3a0c;
    lVar11 = *(longlong *)(**(longlong **)(lVar11 + 0xb8) + 200);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar11 == 0)) goto code_r0x0001814c3a0c;
    PartsSkillWindow::Indicate(lVar11,*(undefined8 *)(param_3 + 0xe0),
                  *(undefined1 *)(*(longlong *)(param_1 + 0x90) + 0x1a8),0);
  }
  else {
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
    cVar6 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x1a8);
    if (cVar6 != '\0') goto code_r0x0001814c3012;
  }
  if (*(longlong *)(param_1 + 0x90) != 0) {
    lVar11 = UnitCtrl::GetCurrentSpineCtrl(*(longlong *)(param_1 + 0x90),0);
    cVar7 = acStackX_18[0];
    if (*(char *)(param_3 + 0x58) != '\0') {
      cVar7 = '\0';
    }
    if (*(longlong *)(param_1 + 0x90) != 0) {
      lVar12 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
      if (cVar7 == '\0') {
        if (lVar12 == 0) goto code_r0x0001814c3a0c;
        cVar7 = CLKMBEGHOLA::BOGEAFNKHMI(lVar12,0);
        if ((cVar7 == '\0') || (*(char *)(lVar12 + 0x1f8) != '\0')) {
          uVar21 = uVar24;
          if (cVar6 == '\0') {
            if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c3a0c;
            UnitCtrl::IndicateSkillName(*(longlong *)(param_1 + 0x90),*(undefined8 *)(param_3 + 0xe0),0);
          }
          while (lVar14 = *(longlong *)(param_3 + 0x38), lVar14 != 0) {
            if (*(int *)(lVar14 + 0x18) <= (int)uVar21) {
              if (cRam000000018ab626bf == '\0') {
                sub_8032f6d0(0x187bd2190);
                sub_8032f6d0(0x187bd2348);
                cRam000000018ab626bf = '\x01';
              }
              if (*(longlong *)(param_3 + 0x40) != 0) {
                iVar20 = *(int *)(*(longlong *)(param_3 + 0x40) + 0x18);
                if (iVar20 < 1) goto code_r0x0001814c3321;
                goto code_r0x0001814c3251;
              }
              break;
            }
            lVar14 = sub_849ed2d0(lVar14,uVar21,uRam0000000187c24fc8);
            if (lVar14 == 0) break;
            if (*(int *)(lVar14 + 0x1c) == 0) {
              if (*(longlong *)(param_3 + 0x38) == 0) break;
              uVar17 = sub_849ed2d0(*(longlong *)(param_3 + 0x38),uVar21,uRam0000000187c24fc8);
              uVar17 = UnitActionController::StartShakeWithDelay(param_1,uVar17,param_3,1,0);
              uVar25 = uVar24;
              if (0.0 < *(float *)(param_3 + 0xb0)) {
                uVar25 = *(ulonglong *)(param_1 + 0x90);
              }
              uVar28 = 0;
              UnitActionController::AppendCoroutine(param_1,uVar17,0,uVar25,0);
            }
            uVar21 = (ulonglong)((int)uVar21 + 1);
          }
          goto code_r0x0001814c3a0c;
        }
      }
      uVar29 = (undefined4)(uVar28 >> 0x20);
      if (*(longlong *)(param_1 + 0x90) != 0) {
        if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x1a8) == '\0') {
          return 0;
        }
        if (lVar12 != 0) {
          cVar6 = CLKMBEGHOLA::GMKFGFKMFCE(lVar12,0);
          if (cVar6 != '\0') {
            return 0;
          }
          if (*(char *)(param_3 + 0x10) != '\0') {
            return 0;
          }
          uVar2 = *(undefined4 *)(param_3 + 0x80);
          uVar8 = *(undefined4 *)(param_3 + 0x94);
          if ((*(longlong *)(param_1 + 0x90) != 0) &&
             (uVar10 = UnitCtrl::get_MotionPrefix(*(longlong *)(param_1 + 0x90),0), lVar11 != 0)) {
            uVar30 = 0;
            uVar17 = CONCAT44(uVar29,uVar10);
            cVar6 = BattleUnitBaseSpineController::IsAnimation(lVar11,uVar2,uVar8,0xffffffff,uVar17,0);
            uVar29 = (undefined4)((ulonglong)uVar17 >> 0x20);
            if (cVar6 == '\0') {
              return 0;
            }
            lVar16 = *(longlong *)(param_1 + 0x90);
            if (lVar16 != 0) {
              lVar11 = *(longlong *)(lVar16 + 0x48);
              uVar2 = *(undefined4 *)(param_3 + 0x80);
              uVar8 = *(undefined4 *)(param_3 + 0x94);
              uVar10 = UnitCtrl::get_MotionPrefix(lVar16,0);
              if (lVar11 != 0) {
                uVar30 = uVar30 & 0xffffffffffffff00;
                uVar28 = CONCAT44(uVar29,uVar10);
                OBAJFDJFDJG::LLIPNABPJGC(lVar11,uVar2,uVar8,0xffffffff,uVar28,uVar30,0,
                              in_stack_ffffffffffffff50 & 0xffffffffffffff00,0,0,0);
                UnitActionController::CreateNormalPrefabWithTargetMotion(param_1,param_3,0,1,uVar28 & 0xffffffffffffff00,
                              uVar30 & 0xffffffffffffff00,0);
                return 0;
              }
            }
          }
        }
      }
    }
  }
  goto code_r0x0001814c3a0c;
  while( true ) {
    uVar17 = sub_849ed2d0(*(longlong *)(param_3 + 0x40),uVar24,uRam0000000187bd2348);
    if (cRam000000018ab626c0 == '\0') {
      sub_8032f6d0(0x187c21e50);
      cRam000000018ab626c0 = '\x01';
    }
    lVar14 = sub_8032f970(uRam0000000187c21e50);
    sub_80445940(lVar14,0,0);
    if (lVar14 == 0) goto code_r0x0001814c3a0c;
    *(longlong *)(lVar14 + 0x38) = param_1;
    sub_8032e8d0(lVar14 + 0x38,param_1);
    *(undefined8 *)(lVar14 + 0x28) = uVar17;
    sub_8032e8d0(lVar14 + 0x28,uVar17);
    *(longlong *)(lVar14 + 0x30) = param_3;
    sub_8032e8d0(lVar14 + 0x30,param_3);
    *(undefined1 *)(lVar14 + 0x20) = 1;
    if (*(float *)(param_3 + 0xb0) <= 0.0) {
      uVar17 = 0;
    }
    else {
      uVar17 = *(undefined8 *)(param_1 + 0x90);
    }
    uVar28 = 0;
    UnitActionController::AppendCoroutine(param_1,lVar14,0,uVar17,0);
    uVar23 = (int)uVar24 + 1;
    uVar24 = (ulonglong)uVar23;
    if (iVar20 <= (int)uVar23) break;
code_r0x0001814c3251:
    if (*(longlong *)(param_3 + 0x40) == 0) goto code_r0x0001814c3a0c;
  }
code_r0x0001814c3321:
  if ((*(int *)(param_3 + 0x80) != 0) && (*(char *)(param_3 + 0x10) == '\0')) {
    uVar29 = *(undefined4 *)(param_3 + 0x80);
    uVar2 = *(undefined4 *)(param_3 + 0x94);
    if ((*(longlong *)(param_1 + 0x90) == 0) ||
       (uVar8 = UnitCtrl::get_MotionPrefix(*(longlong *)(param_1 + 0x90),0), lVar11 == 0))
    goto code_r0x0001814c3a0c;
    uVar30 = 0;
    uVar28 = CONCAT44((int)(uVar28 >> 0x20),uVar8);
    cVar6 = BattleUnitBaseSpineController::IsAnimation(lVar11,uVar29,uVar2,0xffffffff,uVar28,0);
    if (cVar6 != '\0') {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd8), lVar11 == 0))
      goto code_r0x0001814c3a0c;
      if (*(char *)(lVar11 + 0x28) == '\0') {
        uVar29 = *(undefined4 *)(param_3 + 0x80);
        uVar2 = *(undefined4 *)(param_3 + 0x94);
        lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x48);
        uVar8 = UnitCtrl::get_MotionPrefix(*(longlong *)(param_1 + 0x90),0);
        if (lVar11 == 0) goto code_r0x0001814c3a0c;
        uVar30 = uVar30 & 0xffffffffffffff00;
        uVar28 = CONCAT44((int)(uVar28 >> 0x20),uVar8);
        OBAJFDJFDJG::LLIPNABPJGC(lVar11,uVar29,uVar2,0xffffffff,uVar28,uVar30,0,
                      in_stack_ffffffffffffff50 & 0xffffffffffffff00,*(undefined4 *)(param_3 + 0xd0)
                      ,0,0);
      }
      else {
        sub_8043b700(lVar11,0,0);
        uVar10 = (undefined4)(uVar28 >> 0x20);
        lVar11 = *(longlong *)(param_1 + 0x90);
        uVar29 = *(undefined4 *)(param_3 + 0xd0);
        uVar2 = *(undefined4 *)(param_3 + 0x80);
        uVar8 = *(undefined4 *)(param_3 + 0x94);
        if (lVar11 == 0) goto code_r0x0001814c3a0c;
        uVar9 = UnitCtrl::get_MotionPrefix(lVar11,0);
        uVar30 = 0;
        uVar28 = CONCAT44(uVar10,uVar9);
        UnitCtrl::RestartPlayAnimeCoroutine(lVar11,uVar29,uVar2,uVar8,uVar28,0);
      }
    }
  }
  uVar24 = 0;
  SkillBase::ReadySkill(param_3,0);
  if (*(longlong *)(param_1 + 0x90) != 0) {
    uVar17 = CLKMBEGHOLA::ANKFGOIFFHH(lVar12,param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8),
                           param_2 == 1,0);
    *(undefined8 *)(param_3 + 0x160) = uVar17;
    uVar21 = uVar24;
    while (lVar11 = *(longlong *)(param_3 + 0x20), lVar11 != 0) {
      uVar25 = uVar24;
      if (*(int *)(lVar11 + 0x18) <= (int)uVar21) goto code_r0x0001814c3595;
      plVar15 = (longlong *)sub_849ed2d0(lVar11,uVar21,uRam0000000187c19e18);
      if (plVar15 == (longlong *)0x0) break;
      uVar28 = *(ulonglong *)(*plVar15 + 0x220);
      (**(code **)(*plVar15 + 0x218))
                (plVar15,*(undefined8 *)(param_1 + 0x90),param_1,param_3,uVar28);
      if (lVar16 == 0) break;
      cVar6 = sub_84a7f390(lVar16,uVar21,uRam0000000187b42140);
      if (((cVar6 == '\0') || (*(char *)((longlong)plVar15 + 0x86) != '\0')) &&
         (*(char *)((longlong)plVar15 + 0x85) == '\0')) {
        uVar30 = uVar30 & 0xffffffffffffff00;
        uVar28 = CONCAT71((int7)(uVar28 >> 8),1);
        UnitActionController::ExecUnitActionWithDelay(param_1,plVar15,param_3,1,uVar28,uVar30,0);
      }
      uVar21 = (ulonglong)((int)uVar21 + 1);
    }
  }
  goto code_r0x0001814c3a0c;
code_r0x0001814c3595:
  if (cRam000000018ab626da == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab626da = '\x01';
  }
  plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
  if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
  lVar16 = *plVar15;
  uVar18 = 0;
  if (*(ushort *)(lVar16 + 0x12e) != 0) {
    uVar19 = uVar18;
    do {
      if (*(longlong *)(*(longlong *)(lVar16 + 0xb0) + (ulonglong)uVar19 * 0x10) ==
          lRam0000000187b93630) {
        puVar13 = (undefined8 *)
                  ((longlong)
                   (*(int *)(*(longlong *)(lVar16 + 0xb0) + 8 + (ulonglong)uVar19 * 0x10) + 9) *
                   0x10 + 0x138 + lVar16);
        goto code_r0x0001814c3619;
      }
      uVar19 = uVar19 + 1;
    } while (uVar19 < *(ushort *)(lVar16 + 0x12e));
  }
  puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b93630,9);
code_r0x0001814c3619:
  lVar16 = (*(code *)*puVar13)(plVar15,puVar13[1]);
  if (lVar16 == 0) goto code_r0x0001814c3a0c;
  if (*(int *)(lVar16 + 0x18) <= (int)uVar25) {
    uVar30 = uVar30 & 0xffffffffffffff00;
    uVar28 = uVar28 & 0xffffffffffffff00;
    UnitActionController::CreateNormalPrefabWithTargetMotion(param_1,param_3,0,1,uVar28,uVar30,0);
    if (*(longlong *)(param_3 + 0x48) != 0) {
      if (*(char *)(*(longlong *)(param_3 + 0x48) + 0x10) == '\0') goto code_r0x0001814c384b;
      if (cRam000000018ab626dd == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626dd = '\x01';
      }
      lVar16 = *(longlong *)(param_1 + 0x90);
      uVar17 = *(undefined8 *)(param_3 + 0x48);
      uVar29 = *(undefined4 *)(param_3 + 0xd0);
      iVar20 = *(int *)(param_3 + 0x78);
      plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x10);
      if ((lVar16 != 0) && (iVar22 = *(int *)(lVar16 + 0x4b8), plVar15 != (longlong *)0x0)) {
        lVar11 = *plVar15;
        if (*(ushort *)(lVar11 + 0x12e) == 0) goto code_r0x0001814c380b;
        goto code_r0x0001814c37f0;
      }
    }
    goto code_r0x0001814c3a0c;
  }
  if (cRam000000018ab626da == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab626da = '\x01';
  }
  plVar15 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
  if (plVar15 == (longlong *)0x0) goto code_r0x0001814c3a0c;
  lVar16 = *plVar15;
  if (*(ushort *)(lVar16 + 0x12e) != 0) {
    do {
      if (*(longlong *)(*(longlong *)(lVar16 + 0xb0) + (ulonglong)uVar18 * 0x10) ==
          lRam0000000187b93630) {
        puVar13 = (undefined8 *)
                  ((longlong)
                   (*(int *)(*(longlong *)(lVar16 + 0xb0) + 8 + (ulonglong)uVar18 * 0x10) + 9) *
                   0x10 + 0x138 + lVar16);
        goto code_r0x0001814c36b7;
      }
      uVar18 = uVar18 + 1;
    } while (uVar18 < *(ushort *)(lVar16 + 0x12e));
  }
  puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b93630,9);
code_r0x0001814c36b7:
  lVar16 = (*(code *)*puVar13)(plVar15,puVar13[1]);
  if ((lVar16 == 0) || (lVar16 = sub_849ed2d0(lVar16,uVar25,uRam0000000187b73858), lVar16 == 0))
  goto code_r0x0001814c3a0c;
  *(undefined1 *)(lVar16 + 0x250) = 0;
  uVar25 = (ulonglong)((int)uVar25 + 1);
  goto code_r0x0001814c3595;
  while (uVar18 = (short)uVar24 + 1, uVar24 = (ulonglong)uVar18,
        uVar18 < *(ushort *)(lVar11 + 0x12e)) {
code_r0x0001814c37f0:
    if (*(longlong *)(*(longlong *)(lVar11 + 0xb0) + uVar24 * 0x10) == lRam0000000187b8aab0) {
      puVar13 = (undefined8 *)
                ((longlong)*(int *)(*(longlong *)(lVar11 + 0xb0) + 8 + uVar24 * 0x10) * 0x10 + 0x138
                + lVar11);
      goto code_r0x0001814c3816;
    }
  }
code_r0x0001814c380b:
  puVar13 = (undefined8 *)sub_8032c930(plVar15,lRam0000000187b8aab0,0);
code_r0x0001814c3816:
  (*(code *)*puVar13)(plVar15,uVar17,lVar16,uVar29,uVar28 & 0xffffffffffffff00,
                      uVar30 & 0xffffffffffffff00,iVar20 != iVar22,puVar13[1]);
code_r0x0001814c384b:
  if ((*(longlong *)(param_1 + 0x90) != 0) &&
     (lVar16 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xc0), lVar16 != 0)) {
    IDFKMEFJKOD::CNBJILCDDBO(lVar16,*(undefined8 *)(param_3 + 0x60),*(undefined4 *)(param_3 + 0xd0),0);
    if (param_2 == 1) {
      EventExtension::Call(*(undefined8 *)(param_1 + 0xa0),0);
    }
    return 1;
  }
code_r0x0001814c3a0c:
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  uVar17 = (*pcVar4)();
  return uVar17;
}



/* ==== UnitActionController::StartActionWithOutCutIn  @ 0x14c27d0  size=1 ==== */

longlong UnitActionController::StartActionWithOutCutIn(undefined8 param_1,undefined8 param_2,undefined4 param_3,undefined8 param_4)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626a5 == '\0') {
    sub_8032f6d0(0x187c21260);
    cRam000000018ab626a5 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c21260);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    *(undefined8 *)(lVar2 + 0x28) = param_2;
    sub_8032e8d0(lVar2 + 0x28,param_2);
    *(undefined4 *)(lVar2 + 0x38) = param_3;
    *(undefined8 *)(lVar2 + 0x30) = param_4;
    sub_8032e8d0(lVar2 + 0x30,param_4);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::StartAnnihilationSkillAnimation  @ 0x14c3a90  size=1 ==== */

longlong UnitActionController::StartAnnihilationSkillAnimation(undefined8 param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626a6 == '\0') {
    sub_8032f6d0(0x187c21418);
    cRam000000018ab626a6 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c21418);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    *(undefined4 *)(lVar2 + 0x28) = param_2;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::StartShakeWithDelay  @ 0x14c3b10  size=1 ==== */

longlong UnitActionController::StartShakeWithDelay(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined1 param_4)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626be == '\0') {
    sub_8032f6d0(0x187c215c8);
    cRam000000018ab626be = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c215c8);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x38) = param_1;
    sub_8032e8d0(lVar2 + 0x38,param_1);
    *(undefined8 *)(lVar2 + 0x28) = param_2;
    sub_8032e8d0(lVar2 + 0x28,param_2);
    *(undefined8 *)(lVar2 + 0x30) = param_3;
    sub_8032e8d0(lVar2 + 0x30,param_3);
    *(undefined1 *)(lVar2 + 0x20) = param_4;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::StaticRelease  @ 0x14c3bd0  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001814c3c15: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001814c3c6b: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x0001814c3c1a) */
/* WARNING: Removing unreachable block (ram,0x0001814c3c70) */

ulonglong UnitActionController::StaticRelease(void)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  if (cRam000000018ab62696 == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab62696 = '\x01';
  }
  uVar3 = uRam0000000187bfaba0;
  **(undefined8 **)(uRam0000000187bfaba0 + 0xb8) = 0;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(*(ulonglong *)(uRam0000000187bfaba0 + 0xb8) >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar4 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar2 = *puVar1;
      LOCK();
      uVar3 = *puVar1;
      bVar5 = uVar2 == uVar3;
      if (bVar5) {
        *puVar1 = uVar2 | 1L << (uVar4 & 0x3f);
        uVar3 = uVar2;
      }
      UNLOCK();
    } while (!bVar5);
  }
  return uVar3;
}



/* ==== UnitActionController::StopSlowEffect  @ 0x14c3cd0  size=1 ==== */

void UnitActionController::StopSlowEffect(void)

{
  longlong lVar1;
  longlong *plVar2;
  code *pcVar3;
  ushort uVar4;
  undefined8 *puVar5;
  
  if (cRam000000018ab626a8 == '\0') {
    sub_8032f6d0(0x187b94060);
    cRam000000018ab626a8 = '\x01';
  }
  if (cRam000000018ab626dc == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab626dc = '\x01';
  }
  plVar2 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20);
  if (plVar2 != (longlong *)0x0) {
    uVar4 = 0;
    lVar1 = *plVar2;
    if (*(ushort *)(lVar1 + 0x12e) != 0) {
      do {
        if (*(longlong *)(*(longlong *)(lVar1 + 0xb0) + (ulonglong)uVar4 * 0x10) ==
            lRam0000000187b94060) {
          puVar5 = (undefined8 *)
                   ((longlong)
                    (*(int *)(*(longlong *)(lVar1 + 0xb0) + 8 + (ulonglong)uVar4 * 0x10) + 1) * 0x10
                    + 0x138 + lVar1);
                    /* WARNING: Could not recover jumptable at 0x0001800023e5. Too many branches */
                    /* WARNING: Treating indirect jump as call */
          (*(code *)*puVar5)(plVar2,puVar5[1]);
          return;
        }
        uVar4 = uVar4 + 1;
      } while (uVar4 < *(ushort *)(lVar1 + 0x12e));
    }
    puVar5 = (undefined8 *)sub_8032c930(plVar2);
                    /* WARNING: Could not recover jumptable at 0x0001800023b9. Too many branches */
                    /* WARNING: Treating indirect jump as call */
    (*(code *)*puVar5)(plVar2,puVar5[1]);
    return;
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitActionController::UpdateBranchMotion  @ 0x14c3e30  size=1 ==== */

longlong UnitActionController::UpdateBranchMotion(undefined8 param_1,undefined8 param_2,undefined8 param_3)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626cb == '\0') {
    sub_8032f6d0(0x187c21780);
    cRam000000018ab626cb = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c21780);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x28) = param_1;
    sub_8032e8d0(lVar2 + 0x28,param_1);
    *(undefined8 *)(lVar2 + 0x20) = param_3;
    sub_8032e8d0(lVar2 + 0x20,param_3);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::UpdateEffectRunTimeData  @ 0x14c3ec0  size=1 ==== */

void UnitActionController::UpdateEffectRunTimeData(undefined8 param_1,longlong param_2,longlong param_3,char param_4)

{
  int iVar1;
  int iVar2;
  longlong lVar3;
  longlong lVar4;
  longlong *plVar5;
  code *pcVar6;
  longlong lVar7;
  undefined8 uVar8;
  int iVar9;
  longlong *plVar10;
  
  if (cRam000000018ab6269a == '\0') {
    sub_8032f6d0(0x187c198f8);
    sub_8032f6d0(0x187c313a0);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c31550);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b53068);
    sub_8032f6d0(0x187b891b0);
    sub_8032f6d0(0x187bd2dc8);
    sub_8032f6d0(0x187bd2f80);
    sub_8032f6d0(0x187c20d48);
    cRam000000018ab6269a = '\x01';
  }
  if (param_4 != '\0') {
    iVar9 = 0;
    if ((param_2 == 0) || (*(longlong *)(param_2 + 0x20) == 0)) goto code_r0x0001814c4271;
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x20) + 0x18);
    if (0 < iVar1) {
      do {
        if ((*(longlong *)(param_2 + 0x20) == 0) ||
           (lVar7 = sub_849ed2d0(*(longlong *)(param_2 + 0x20),iVar9,uRam0000000187c19e18),
           lVar7 == 0)) goto code_r0x0001814c4271;
        iVar9 = iVar9 + 1;
        *(undefined1 *)(lVar7 + 0x85) = 0;
      } while (iVar9 < iVar1);
    }
  }
  iVar9 = 0;
  if (param_3 != 0) {
    iVar1 = *(int *)(param_3 + 0x18);
    if (0 < iVar1) {
      do {
        lVar7 = sub_8032f970(uRam0000000187c20d48);
        sub_804383d0(lVar7,0);
        uVar8 = sub_849ed2d0(param_3,iVar9,uRam0000000187c31550);
        if (lVar7 == 0) goto code_r0x0001814c4271;
        *(undefined8 *)(lVar7 + 0x10) = uVar8;
        sub_8032e8d0(lVar7 + 0x10,uVar8);
        lVar3 = *(longlong *)(lVar7 + 0x10);
        if (param_2 == 0) goto code_r0x0001814c4271;
        lVar4 = *(longlong *)(param_2 + 0x20);
        uVar8 = sub_8032f970(uRam0000000187b891b0);
        sub_84cacb40(uVar8,lVar7,uRam0000000187bd2dc8,0);
        if ((lVar4 == 0) || (uVar8 = sub_84a8c920(lVar4,uVar8,uRam0000000187c198f8), lVar3 == 0))
        goto code_r0x0001814c4271;
        *(undefined8 *)(lVar3 + 0x38) = uVar8;
        sub_8032e8d0(lVar3 + 0x38);
        lVar3 = *(longlong *)(lVar7 + 0x10);
        if (lVar3 == 0) goto code_r0x0001814c4271;
        if (*(longlong *)(lVar3 + 0x38) == 0) {
          if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814c4271;
          uVar8 = sub_849ed2d0(*(longlong *)(param_2 + 0x20),0,uRam0000000187c19e18);
          *(undefined8 *)(lVar3 + 0x38) = uVar8;
          sub_8032e8d0(lVar3 + 0x38);
        }
        if (*(longlong **)(lVar7 + 0x10) != (longlong *)0x0) {
          lVar3 = **(longlong **)(lVar7 + 0x10);
          if ((*(byte *)(lRam0000000187b53068 + 0x130) <= *(byte *)(lVar3 + 0x130)) &&
             (*(longlong *)
               (*(longlong *)(lVar3 + 200) + -8 +
               (ulonglong)*(byte *)(lRam0000000187b53068 + 0x130) * 8) == lRam0000000187b53068)) {
            plVar5 = *(longlong **)(lVar7 + 0x10);
            if (*(longlong *)
                 (*(longlong *)(*plVar5 + 200) + -8 +
                 (ulonglong)*(byte *)(lRam0000000187b53068 + 0x130) * 8) != lRam0000000187b53068)
            goto code_r0x0001814c4271;
            plVar10 = (longlong *)0x0;
            if (*(longlong *)
                 (*(longlong *)(*plVar5 + 200) + -8 +
                 (ulonglong)*(byte *)(lRam0000000187b53068 + 0x130) * 8) == lRam0000000187b53068) {
              plVar10 = plVar5;
            }
            UnitActionController::UpdateEffectRunTimeData(param_1,param_2,plVar10[0x18],0,0);
          }
        }
        if (*(longlong *)(lVar7 + 0x10) == 0) goto code_r0x0001814c4271;
        iVar2 = *(int *)(*(longlong *)(lVar7 + 0x10) + 0x50);
        if ((iVar2 == 1) || (iVar2 - 5U < 2)) {
          lVar3 = *(longlong *)(lVar7 + 0x10);
          if (*(int *)(param_2 + 0x78) == 1) {
            if (*(longlong *)(param_2 + 0x20) == 0) goto code_r0x0001814c4271;
            uVar8 = sub_849ed2d0(*(longlong *)(param_2 + 0x20),0,uRam0000000187c19e18);
            *(undefined8 *)(lVar3 + 0x40) = uVar8;
          }
          else {
            if (*(int *)(lVar3 + 0x48) == -1) goto code_r0x0001814c4243;
            lVar4 = *(longlong *)(param_2 + 0x20);
            uVar8 = sub_8032f970(uRam0000000187b891b0);
            sub_84cacb40(uVar8,lVar7,uRam0000000187bd2f80,0);
            if (lVar4 == 0) goto code_r0x0001814c4271;
            uVar8 = sub_84a8c920(lVar4,uVar8,uRam0000000187c198f8);
            *(undefined8 *)(lVar3 + 0x40) = uVar8;
          }
          sub_8032e8d0(lVar3 + 0x40);
          if ((*(longlong *)(lVar7 + 0x10) == 0) ||
             (lVar7 = *(longlong *)(*(longlong *)(lVar7 + 0x10) + 0x40), lVar7 == 0))
          goto code_r0x0001814c4271;
          *(undefined1 *)(lVar7 + 0x85) = 1;
        }
code_r0x0001814c4243:
        iVar9 = iVar9 + 1;
      } while (iVar9 < iVar1);
    }
    return;
  }
code_r0x0001814c4271:
  sub_8032f9d0();
  pcVar6 = (code *)swi(3);
  (*pcVar6)();
  return;
}



/* ==== UnitActionController::actionCancelAndGetCancel  @ 0x14c4280  size=1 ==== */

undefined1 UnitActionController::actionCancelAndGetCancel(longlong param_1,int param_2)

{
  longlong lVar1;
  longlong lVar2;
  code *pcVar3;
  char cVar4;
  undefined1 uVar5;
  
  if (*(longlong *)(param_1 + 0x90) != 0) {
    lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd0);
    lVar2 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
    if (lVar2 != 0) {
      cVar4 = CLKMBEGHOLA::DIPBIJJPKCO(lVar2,0);
      if (cVar4 == '\0') {
        if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar1 == 0)) goto code_r0x0001814c4335;
        cVar4 = DEBLMMJMGID::OHLAHJFLIBP(lVar1,param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)
                                    ,0xffffffff);
        if (cVar4 == '\0') {
          return 0;
        }
      }
      else if (lVar1 == 0) goto code_r0x0001814c4335;
      *(undefined1 *)(lVar1 + 0xce) = 0;
      *(undefined1 *)(lVar1 + 200) = 0;
      UnitActionController::CancelAction(param_1,param_2,0);
      return 1;
    }
  }
code_r0x0001814c4335:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar5 = (*pcVar3)();
  return uVar5;
}



/* ==== UnitActionController::actionStartFirstProcess  @ 0x14c4340  size=1 ==== */

void UnitActionController::actionStartFirstProcess(longlong param_1,longlong param_2,undefined1 *param_3,char param_4,char param_5)

{
  char cVar1;
  code *pcVar2;
  undefined1 uVar3;
  int iVar4;
  undefined4 uVar5;
  undefined8 *puVar6;
  undefined8 uVar7;
  longlong lVar8;
  longlong *plVar9;
  longlong lVar10;
  ushort uVar11;
  int iVar12;
  int iVar13;
  double dVar14;
  float fVar15;
  float fVar16;
  undefined1 uStackX_10;
  float fStack_140;
  float fStack_13c;
  undefined8 uStack_118;
  undefined4 uStack_110;
  undefined8 uStack_108;
  float fStack_100;
  undefined1 auStack_f8 [16];
  undefined1 auStack_e8 [16];
  undefined1 auStack_d8 [16];
  undefined1 auStack_c8 [16];
  undefined1 auStack_b8 [128];
  
  if (cRam000000018ab626a1 == '\0') {
    sub_8032f6d0(0x187b97150);
    sub_8032f6d0(0x187b2d490);
    sub_8032f6d0(0x187b40048);
    sub_8032f6d0(0x187ba2da0);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b446b8);
    cRam000000018ab626a1 = '\x01';
  }
  if (param_2 != 0) {
    if (0.0 < *(float *)(param_2 + 0x74)) {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd8), lVar8 == 0))
      goto code_r0x0001814c4ba2;
      *(undefined4 *)(lVar8 + 0x1c) = *(undefined4 *)(param_2 + 0x74);
    }
    iVar12 = 0;
    *(undefined4 *)(param_2 + 0x11c) = 0;
    *(undefined8 *)(param_2 + 0x120) = 0;
    if (param_5 != '\0') {
      *(undefined1 *)(param_2 + 0x84) = 0;
    }
    *(undefined4 *)(param_2 + 0x130) = 0;
    *(undefined1 *)(param_2 + 0x141) = 0;
    lVar8 = *(longlong *)(param_1 + 0x20);
    *(undefined1 *)(param_1 + 0xb8) = 0;
    if (lVar8 != 0) {
      iVar13 = *(int *)(lVar8 + 0x10);
      iVar4 = *(int *)(lVar8 + 0x14);
      *(undefined1 *)(param_2 + 0x128) = 0;
      *(ulonglong *)(param_2 + 0xe8) = CONCAT44((float)iVar4 / 100.0,(float)iVar13 / 100.0);
      *(undefined4 *)(param_2 + 0xf0) = 0;
      if (cRam000000018ab626da == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626da = '\x01';
      }
      plVar9 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
      if (plVar9 != (longlong *)0x0) {
        lVar8 = *plVar9;
        uVar11 = 0;
        if (*(ushort *)(lVar8 + 0x12e) != 0) {
          do {
            if (*(longlong *)(*(longlong *)(lVar8 + 0xb0) + (ulonglong)uVar11 * 0x10) ==
                lRam0000000187b93630) {
              puVar6 = (undefined8 *)
                       ((longlong)
                        (*(int *)(*(longlong *)(lVar8 + 0xb0) + 8 + (ulonglong)uVar11 * 0x10) + 0x17
                        ) * 0x10 + 0x138 + lVar8);
              goto code_r0x0001814c45a9;
            }
            uVar11 = uVar11 + 1;
          } while (uVar11 < *(ushort *)(lVar8 + 0x12e));
        }
        puVar6 = (undefined8 *)sub_8032c930(plVar9,lRam0000000187b93630,0x17);
code_r0x0001814c45a9:
        uVar7 = (*(code *)*puVar6)(plVar9,puVar6[1]);
        *(undefined8 *)(param_2 + 0x170) = uVar7;
        if (*(longlong *)(param_1 + 0x90) != 0) {
          if ((*(char *)(param_2 + 0x140) != '\0') && (*(char *)(param_2 + 0x168) != '\0')) {
            lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
            if (lVar8 == 0) goto code_r0x0001814c4ba2;
            CLKMBEGHOLA::BEHMPIFNHCI(lVar8,param_2,0);
          }
          *param_3 = 1;
          while( true ) {
            iVar13 = 0;
            lVar8 = *(longlong *)(param_2 + 0x20);
            if (lVar8 == 0) break;
            if (*(int *)(lVar8 + 0x18) <= iVar12) {
              return;
            }
            lVar8 = sub_849ed2d0(lVar8,iVar12,uRam0000000187c19e18);
            if (param_4 == '\0') {
code_r0x0001814c4675:
              uVar7 = sub_8032f970(uRam0000000187ba2da0);
              sub_85660990(uVar7,uRam0000000187b2d490);
              if (lVar8 == 0) break;
              *(undefined8 *)(lVar8 + 0xf0) = uVar7;
              sub_8032e8d0(lVar8 + 0xf0,uVar7);
              *(undefined1 *)(lVar8 + 0xe8) = 0;
              *(undefined8 *)(lVar8 + 0x170) = 0;
              sub_8032e8d0(lVar8 + 0x170,0);
              *(undefined8 *)(lVar8 + 0x178) = 0;
              sub_8032e8d0(lVar8 + 0x178,0);
              *(undefined8 *)(lVar8 + 400) = 0;
              sub_8032e8d0(lVar8 + 400,0);
              if (*(longlong *)(lVar8 + 0x150) == 0) break;
              sub_85531bb0(*(longlong *)(lVar8 + 0x150),uRam0000000187b97150);
              if (*(char *)(lVar8 + 0x86) != '\0') goto code_r0x0001814c4b26;
              if (*(char *)(lVar8 + 0x1a4) == '\0') {
                if (*(longlong *)(param_1 + 0x20) == 0) break;
                puVar6 = (undefined8 *)FixedTransform::get_position(auStack_f8,*(longlong *)(param_1 + 0x20),0);
                uStack_118 = *puVar6;
                uStack_110 = *(undefined4 *)(puVar6 + 1);
                UnitActionController::searchAndSortTarget(param_1,param_2,lVar8,&uStack_118,0,1,0);
              }
              *(undefined1 *)(lVar8 + 0x1a4) = 0;
              if (*(int *)(lVar8 + 0x60) == 7) {
                iVar4 = *(int *)(lVar8 + 0x2c);
                fVar15 = 0.0;
                uVar3 = 0;
                uStackX_10 = 1;
                fVar16 = 0.0;
                if (iVar4 == 1) {
                  lVar10 = *(longlong *)(lVar8 + 0x50);
                  if (lVar10 == 0) break;
                  if (*(int *)(lVar10 + 0x18) == 0) {
code_r0x0001814c4b0d:
                    UnitActionController::clearChildrenTarget(param_1,lVar8,param_2,0);
                    goto code_r0x0001814c4b26;
                  }
                  plVar9 = (longlong *)sub_849ed2d0(lVar10,0,uRam0000000187b812d0);
                  if (plVar9 == (longlong *)0x0) break;
                  puVar6 = (undefined8 *)
                           (**(code **)(*plVar9 + 0x178))
                                     (auStack_b8,plVar9,*(undefined8 *)(*plVar9 + 0x180));
code_r0x0001814c49d1:
                  fVar15 = *(float *)(puVar6 + 1);
                  fStack_140 = (float)*puVar6;
                  fStack_13c = (float)((ulonglong)*puVar6 >> 0x20);
                  if ((plVar9[0xd] == 0) ||
                     (lVar10 = *(longlong *)(plVar9[0xd] + 0x38), lVar10 == 0)) break;
                  uVar3 = CLKMBEGHOLA::BHNFPDMBOAM(lVar10,0x47);
                }
                else if (iVar4 == 2) {
                  if (*(longlong *)(param_1 + 0x20) == 0) break;
                  puVar6 = (undefined8 *)FixedTransform::get_position(auStack_c8,*(longlong *)(param_1 + 0x20),0);
                  if (*(longlong *)(param_1 + 0x90) == 0) break;
                  cVar1 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x165);
                  uVar7 = *puVar6;
                  fVar15 = *(float *)(puVar6 + 1);
                  if (*(longlong *)(lVar8 + 0x58) == 0) break;
                  dVar14 = (double)sub_855f06a0(*(longlong *)(lVar8 + 0x58),0,uRam0000000187b40048)
                  ;
                  fStack_140 = (float)uVar7;
                  fVar15 = fVar15 + 0.0;
                  iVar4 = -1;
                  if (cVar1 == '\0') {
                    iVar4 = 1;
                  }
                  fStack_13c = (float)((ulonglong)uVar7 >> 0x20);
                  fStack_13c = fStack_13c + 0.0;
                  fStack_140 = fStack_140 + (float)((dVar14 * (double)iVar4) / 540.0);
                }
                else if (iVar4 == 3) {
                  if (*(longlong *)(param_1 + 0x20) == 0) break;
                  puVar6 = (undefined8 *)FixedTransform::get_position(auStack_d8,*(longlong *)(param_1 + 0x20),0);
                  fStack_140 = (float)*puVar6;
                  fStack_13c = (float)((ulonglong)*puVar6 >> 0x20);
                  fVar15 = *(float *)(puVar6 + 1);
                  if (*(longlong *)(param_1 + 0x90) == 0) break;
                  if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x165) == '\0') {
                    fVar16 = 1.0;
                  }
                  else {
                    fVar16 = -1.0;
                  }
                  if (*(longlong *)(lVar8 + 0x58) == 0) break;
                  dVar14 = (double)sub_855f06a0(*(longlong *)(lVar8 + 0x58),0,uRam0000000187b40048)
                  ;
                  fVar16 = (float)dVar14 * fVar16;
                }
                else {
                  if (iVar4 == 4) {
                    lVar10 = *(longlong *)(lVar8 + 0x50);
                    if (lVar10 != 0) {
                      if (*(int *)(lVar10 + 0x18) == 0) goto code_r0x0001814c4b0d;
                      plVar9 = (longlong *)sub_849ed2d0(lVar10,0,uRam0000000187b812d0);
                      if (plVar9 != (longlong *)0x0) {
                        puVar6 = (undefined8 *)
                                 (**(code **)(*plVar9 + 0x178))
                                           (auStack_e8,plVar9,*(undefined8 *)(*plVar9 + 0x180));
                        uStackX_10 = 0;
                        goto code_r0x0001814c49d1;
                      }
                    }
                    break;
                  }
                  fStack_140 = 0.0;
                  fStack_13c = 0.0;
                }
                if (*(longlong *)(lVar8 + 0x88) == 0) break;
                iVar4 = *(int *)(*(longlong *)(lVar8 + 0x88) + 0x18);
                if (0 < iVar4) {
                  do {
                    lVar10 = *(longlong *)(param_2 + 0x20);
                    if (((*(longlong *)(lVar8 + 0x88) == 0) ||
                        (uVar5 = sub_84a610f0(*(longlong *)(lVar8 + 0x88),iVar13,
                                               uRam0000000187b446b8), lVar10 == 0)) ||
                       (lVar10 = sub_849ed2d0(lVar10,uVar5,uRam0000000187c19e18), lVar10 == 0))
                    goto code_r0x0001814c4ba2;
                    *(float *)(lVar10 + 0x28) = fVar16;
                    *(undefined1 *)(lVar10 + 0x98) = uVar3;
                    uStack_108 = CONCAT44(fStack_13c,fStack_140);
                    fStack_100 = fVar15;
                    UnitActionController::searchAndSortTarget(param_1,param_2,lVar10,&uStack_108,0,uStackX_10,0);
                    iVar13 = iVar13 + 1;
                  } while (iVar13 < iVar4);
                }
              }
              if (*(longlong *)(lVar8 + 0x50) == 0) break;
              if (*(int *)(*(longlong *)(lVar8 + 0x50) + 0x18) == 0) goto code_r0x0001814c4b26;
              iVar12 = iVar12 + 1;
              *param_3 = 0;
            }
            else {
              if (lVar8 == 0) break;
              if (*(char *)(lVar8 + 0xe9) != '\0') goto code_r0x0001814c4675;
code_r0x0001814c4b26:
              iVar12 = iVar12 + 1;
            }
          }
        }
      }
    }
  }
code_r0x0001814c4ba2:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::clearChildrenTarget  @ 0x14c4bb0  size=1 ==== */

void UnitActionController::clearChildrenTarget(undefined8 param_1,longlong param_2,longlong param_3)

{
  int iVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  undefined8 uStack_40;
  undefined8 uStack_38;
  ulonglong uStack_30;
  undefined4 uStack_28;
  undefined4 uStack_24;
  undefined4 uStack_20;
  undefined4 uStack_1c;
  ulonglong uStack_18;
  
  if (cRam000000018ab626a2 == '\0') {
    sub_8032f6d0(0x187b3b288);
    sub_8032f6d0(0x187b3b438);
    sub_8032f6d0(0x187b3b5f0);
    sub_8032f6d0(0x187b7fff8);
    sub_8032f6d0(0x187b42d30);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab626a2 = '\x01';
  }
  if ((param_2 == 0) || (*(longlong *)(param_2 + 0x88) == 0)) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  sub_84a55540(&uStack_40,*(longlong *)(param_2 + 0x88),uRam0000000187b42d30);
  uStack_28 = (undefined4)uStack_40;
  uStack_24 = uStack_40._4_4_;
  uStack_20 = (undefined4)uStack_38;
  uStack_1c = uStack_38._4_4_;
  uStack_18 = uStack_30;
  uStack_40 = 0;
  uStack_38 = &uStack_28;
  while( true ) {
    cVar3 = sub_85723320(&uStack_28,uRam0000000187b3b438);
    if (cVar3 == '\0') {
      sub_80443e50(&uStack_28,uRam0000000187b3b288);
      return;
    }
    if (param_3 == 0) goto code_r0x0001814c4d24;
    if (*(longlong *)(param_3 + 0x20) == 0) break;
    lVar4 = sub_849ed2d0(*(longlong *)(param_3 + 0x20),uStack_18 & 0xffffffff,uRam0000000187c19e18)
    ;
    if (lVar4 == 0) {
code_r0x0001814c4d1a:
      sub_8032f9d0();
      break;
    }
    lVar4 = *(longlong *)(lVar4 + 0x50);
    if (lVar4 == 0) {
      sub_8032f9d0();
      goto code_r0x0001814c4d1a;
    }
    *(int *)(lVar4 + 0x1c) = *(int *)(lVar4 + 0x1c) + 1;
    iVar1 = *(int *)(lVar4 + 0x18);
    *(undefined4 *)(lVar4 + 0x18) = 0;
    if (0 < iVar1) {
      Array::Clear(*(undefined8 *)(lVar4 + 0x10),0,iVar1,0);
    }
  }
  sub_8032f9d0();
code_r0x0001814c4d24:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::convertPriorityPattern  @ 0x14c4d40  size=28 ==== */

uint UnitActionController::convertPriorityPattern(undefined8 param_1,uint param_2)

{
  if ((int)param_2 < 1000) {
    return param_2;
  }
  return (param_2 / 1000) * 1000;
}



/* ==== UnitActionController::createActionValue  @ 0x14c4d60  size=1 ==== */

void UnitActionController::createActionValue(longlong param_1,undefined8 param_2,longlong param_3,undefined4 param_4,
                  longlong param_5,longlong param_6)

{
  double dVar1;
  longlong lVar2;
  code *pcVar3;
  bool bVar4;
  char cVar5;
  undefined2 uVar6;
  ushort uVar7;
  undefined4 uVar8;
  int iVar9;
  int iVar10;
  longlong lVar11;
  undefined8 uVar12;
  longlong *plVar13;
  longlong lVar14;
  undefined8 uVar15;
  longlong lVar16;
  longlong lVar17;
  longlong *plVar18;
  uint uVar19;
  longlong *plVar20;
  float fVar21;
  float fVar22;
  int iStackX_20;
  int iStack_88;
  undefined2 uStack_84;
  longlong lStack_78;
  undefined4 uStack_70;
  undefined8 uStack_68;
  undefined8 uStack_60;
  undefined8 uStack_58;
  undefined8 uStack_50;
  
  if (cRam000000018ab6269e == '\0') {
    sub_8032f6d0(0x187c332b0);
    sub_8032f6d0(0x187c337c8);
    sub_8032f6d0(0x187c33618);
    sub_8032f6d0(0x187c33980);
    sub_8032f6d0(0x187b802a8);
    sub_8032f6d0(0x187b3fb28);
    sub_8032f6d0(0x187b45050);
    sub_8032f6d0(0x187b45208);
    sub_8032f6d0(0x187b3f978);
    sub_8032f6d0(0x187b45a88);
    sub_8032f6d0(0x187b453b8);
    sub_8032f6d0(0x187b45570);
    sub_8032f6d0(0x187ba6e58);
    sub_8032f6d0(0x187c3dbc8);
    sub_8032f6d0(0x187bf8138);
    sub_8032f6d0(0x187b6ac68);
    sub_8032f6d0(0x187c19748);
    sub_8032f6d0(0x187b6a738);
    sub_8032f6d0(0x187b69958);
    sub_8032f6d0(0x187b6a8f0);
    sub_8032f6d0(0x187b69b10);
    sub_8032f6d0(0x187b69798);
    sub_8032f6d0(0x187b413a0);
    sub_8032f6d0(0x187b695e0);
    sub_8032f6d0(0x187b7f908);
    sub_8032f6d0(0x187b6a200);
    sub_8032f6d0(0x187b6ae28);
    sub_8032f6d0(0x187b69cd0);
    sub_8032f6d0(0x187b6a3c0);
    sub_8032f6d0(0x187b6afe0);
    sub_8032f6d0(0x187b69e88);
    sub_8032f6d0(0x187bf1860);
    sub_8032f6d0(0x187bc09d8);
    sub_8032f6d0(0x187bc5830);
    sub_8032f6d0(0x187b3d4a8);
    sub_8032f6d0(0x187b60c28);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b61d80);
    sub_8032f6d0(0x187b81c38);
    sub_8032f6d0(0x187c013d8);
    sub_8032f6d0(0x187b77e20);
    sub_8032f6d0(0x187bcfa38);
    sub_8032f6d0(0x187bd3138);
    sub_8032f6d0(0x187c20ef8);
    sub_8032f6d0(0x187c1f720);
    cRam000000018ab6269e = '\x01';
  }
  lVar11 = sub_8032f970(uRam0000000187c20ef8);
  sub_804383d0(lVar11,0);
  if (lVar11 != 0) {
    *(undefined4 *)(lVar11 + 0x10) = param_4;
    if (*(int *)(lRam0000000187b77e20 + 0xe4) == 0) {
      sub_8032fa70(lRam0000000187b77e20);
    }
    lVar14 = *(longlong *)(*(longlong *)(lRam0000000187b77e20 + 0xb8) + 8);
    if (param_3 == 0) goto code_r0x0001814c5f8b;
    iVar9 = *(int *)(param_3 + 0x2c);
    uVar6 = *(undefined2 *)(param_3 + 0x30);
    if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iStack_88 = iVar9;
    uStack_84 = uVar6;
    uVar6 = ObscuredUShort::FHOALPFLEEC(&iStack_88,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    uVar12 = sub_855ffe50(lVar14,uVar6,uRam0000000187b45a88);
    plVar13 = (longlong *)Activator::CreateInstance(uVar12,0);
    if (plVar13 == (longlong *)0x0) {
      plVar20 = (longlong *)0x0;
    }
    else {
      if ((*(byte *)(*plVar13 + 0x130) < *(byte *)(lRam0000000187c3dbc8 + 0x130)) ||
         (*(longlong *)
           (*(longlong *)(*plVar13 + 200) + -8 +
           (ulonglong)*(byte *)(lRam0000000187c3dbc8 + 0x130) * 8) != lRam0000000187c3dbc8)) {
        bVar4 = false;
      }
      else {
        bVar4 = true;
      }
      plVar20 = (longlong *)0x0;
      if (bVar4) {
        plVar20 = plVar13;
      }
    }
    lVar14 = *(longlong *)(param_3 + 0x15c);
    uVar8 = *(undefined4 *)(param_3 + 0x164);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lStack_78 = lVar14;
    uStack_70 = uVar8;
    uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
    if (plVar20 == (longlong *)0x0) goto code_r0x0001814c5f8b;
    *(undefined4 *)((longlong)plVar20 + 0x14) = uVar8;
    lStack_78 = *(longlong *)(param_3 + 0x168);
    uStack_70 = *(undefined4 *)(param_3 + 0x170);
    uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
    *(undefined4 *)(plVar20 + 3) = uVar8;
    lStack_78 = *(longlong *)(param_3 + 0x174);
    uStack_70 = *(undefined4 *)(param_3 + 0x17c);
    uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
    *(undefined4 *)((longlong)plVar20 + 0x1c) = uVar8;
    lStack_78 = *(longlong *)(param_3 + 0x150);
    uStack_70 = *(undefined4 *)(param_3 + 0x158);
    iVar9 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
    if (iVar9 < 1) {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar14 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar14 == 0))
      goto code_r0x0001814c5f8b;
      fVar21 = *(float *)(lVar14 + 0x18);
    }
    else {
      lVar14 = *(longlong *)(param_3 + 0x150);
      uVar8 = *(undefined4 *)(param_3 + 0x158);
      if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
        sub_8032fa70();
      }
      lStack_78 = lVar14;
      uStack_70 = uVar8;
      iVar9 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
      fVar21 = (float)iVar9;
    }
    *(float *)(plVar20 + 4) = fVar21;
    iVar9 = *(int *)(param_3 + 0x2c);
    uVar6 = *(undefined2 *)(param_3 + 0x30);
    if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iStack_88 = iVar9;
    uStack_84 = uVar6;
    uVar7 = ObscuredUShort::FHOALPFLEEC(&iStack_88,0);
    *(uint *)(plVar20 + 0xc) = (uint)uVar7;
    lVar14 = *(longlong *)(param_3 + 0x34);
    uVar8 = *(undefined4 *)(param_3 + 0x3c);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lStack_78 = lVar14;
    uStack_70 = uVar8;
    uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
    *(undefined4 *)((longlong)plVar20 + 0x2c) = uVar8;
    lStack_78 = *(longlong *)(param_3 + 0x40);
    uStack_70 = *(undefined4 *)(param_3 + 0x48);
    uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
    *(undefined4 *)(plVar20 + 6) = uVar8;
    lStack_78 = *(longlong *)(param_3 + 0x4c);
    uStack_70 = *(undefined4 *)(param_3 + 0x54);
    uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
    *(undefined4 *)((longlong)plVar20 + 0x34) = uVar8;
    uVar12 = sub_8032f970(uRam0000000187bf8138);
    sub_804383d0(uVar12,0);
    lVar14 = sub_8032f970(uRam0000000187ba6e58);
    sub_855de480(lVar14,uVar12,uRam0000000187b3f978);
    plVar20[0xb] = lVar14;
    sub_8032e8d0(plVar20 + 0xb,lVar14);
    lVar14 = plVar20[0xb];
    uStack_68 = *(undefined8 *)(param_3 + 0x58);
    uStack_60 = *(undefined8 *)(param_3 + 0x60);
    uStack_58 = *(undefined8 *)(param_3 + 0x68);
    uStack_50 = *(undefined8 *)(param_3 + 0x70);
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    sub_855e0980(lVar14,0,uVar8,uRam0000000187b3fb28);
    uStack_68 = *(undefined8 *)(param_3 + 0x78);
    uStack_60 = *(undefined8 *)(param_3 + 0x80);
    lVar14 = plVar20[0xb];
    uStack_58 = *(undefined8 *)(param_3 + 0x88);
    uStack_50 = *(undefined8 *)(param_3 + 0x90);
    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    sub_855e0980(lVar14,1,uVar8,uRam0000000187b3fb28);
    uStack_68 = *(undefined8 *)(param_3 + 0x98);
    uStack_60 = *(undefined8 *)(param_3 + 0xa0);
    lVar14 = plVar20[0xb];
    uStack_58 = *(undefined8 *)(param_3 + 0xa8);
    uStack_50 = *(undefined8 *)(param_3 + 0xb0);
    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    sub_855e0980(lVar14,2,uVar8,uRam0000000187b3fb28);
    uStack_68 = *(undefined8 *)(param_3 + 0xb8);
    uStack_60 = *(undefined8 *)(param_3 + 0xc0);
    lVar14 = plVar20[0xb];
    uStack_58 = *(undefined8 *)(param_3 + 200);
    uStack_50 = *(undefined8 *)(param_3 + 0xd0);
    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    sub_855e0980(lVar14,3,uVar8,uRam0000000187b3fb28);
    uStack_68 = *(undefined8 *)(param_3 + 0xd8);
    uStack_60 = *(undefined8 *)(param_3 + 0xe0);
    lVar14 = plVar20[0xb];
    uStack_58 = *(undefined8 *)(param_3 + 0xe8);
    uStack_50 = *(undefined8 *)(param_3 + 0xf0);
    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    sub_855e0980(lVar14,4,uVar8,uRam0000000187b3fb28);
    uStack_68 = *(undefined8 *)(param_3 + 0xf8);
    uStack_60 = *(undefined8 *)(param_3 + 0x100);
    lVar14 = plVar20[0xb];
    uStack_58 = *(undefined8 *)(param_3 + 0x108);
    uStack_50 = *(undefined8 *)(param_3 + 0x110);
    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    sub_855e0980(lVar14,5,uVar8,uRam0000000187b3fb28);
    uStack_68 = *(undefined8 *)(param_3 + 0x118);
    uStack_60 = *(undefined8 *)(param_3 + 0x120);
    lVar14 = plVar20[0xb];
    uStack_58 = *(undefined8 *)(param_3 + 0x128);
    uStack_50 = *(undefined8 *)(param_3 + 0x130);
    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_68,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    sub_855e0980(lVar14,6,uVar8,uRam0000000187b3fb28);
    plVar20[0x33] = param_3;
    sub_8032e8d0(plVar20 + 0x33,param_3);
    iVar9 = 0;
    if (param_6 == 0) goto code_r0x0001814c5f8b;
    cVar5 = sub_855e0af0(param_6,*(undefined4 *)(lVar11 + 0x10),uRam0000000187b45208);
    if (cVar5 == '\0') {
      sub_855e0a60(param_6,*(undefined4 *)(lVar11 + 0x10),1,uRam0000000187b45050);
    }
    else {
      iVar9 = sub_855f0800(param_6,*(undefined4 *)(lVar11 + 0x10),uRam0000000187b453b8);
      uVar8 = *(undefined4 *)(lVar11 + 0x10);
      iVar10 = sub_855f0800(param_6,uVar8,uRam0000000187b453b8);
      sub_855f0a50(param_6,uVar8,iVar10 + 1,uRam0000000187b45570);
    }
    if (param_5 == 0) goto code_r0x0001814c5f8b;
    lVar14 = *(longlong *)(param_5 + 0x28);
    uVar12 = sub_8032f970(uRam0000000187b81c38);
    sub_84cacb40(uVar12,lVar11,uRam0000000187bd3138,0);
    if (lVar14 == 0) goto code_r0x0001814c5f8b;
    lVar11 = sub_84a8c920(lVar14,uVar12,uRam0000000187b6a8f0);
    lStack_78 = lVar11;
    if (lVar11 == 0) {
      lVar11 = sub_8032f970(uRam0000000187c33618);
      lStack_78 = lVar11;
      if (cRam000000018ab6260a == '\0') {
        sub_8032f6d0(0x187c3e3c0);
        sub_8032f6d0(0x187b6aab0);
        sub_8032f6d0(0x187bc0ef0);
        cRam000000018ab6260a = '\x01';
      }
      *(undefined4 *)(lVar11 + 0x14) = 1;
      uVar12 = sub_8032f970(uRam0000000187bc0ef0);
      sub_849de5b0(uVar12,uRam0000000187b6aab0);
      *(undefined8 *)(lVar11 + 0x18) = uVar12;
      sub_8032e8d0(lVar11 + 0x18,uVar12);
      uVar12 = sub_8032f970(uRam0000000187c3e3c0);
      AnimationCurve::.ctor(uVar12,0);
      *(undefined8 *)(lVar11 + 0x20) = uVar12;
      sub_8032e8d0(lVar11 + 0x20,uVar12);
      uVar12 = sub_8032f970(uRam0000000187c3e3c0);
      AnimationCurve::.ctor(uVar12,0);
      *(undefined8 *)(lVar11 + 0x28) = uVar12;
      sub_8032e8d0(lVar11 + 0x28,uVar12);
      sub_80443e50(lVar11,0);
      *(int *)(lVar11 + 0x14) = (int)plVar20[0xc];
      lVar17 = lRam0000000187b6a738;
      lVar14 = *(longlong *)(param_5 + 0x28);
      if (lVar14 == 0) goto code_r0x0001814c5f8b;
      *(int *)(lVar14 + 0x1c) = *(int *)(lVar14 + 0x1c) + 1;
      lVar16 = *(longlong *)(lVar14 + 0x10);
      uVar19 = *(uint *)(lVar14 + 0x18);
      if (lVar16 == 0) goto code_r0x0001814c5f8b;
      if (uVar19 < *(uint *)(lVar16 + 0x18)) {
        *(uint *)(lVar14 + 0x18) = uVar19 + 1;
        if (*(uint *)(lVar16 + 0x18) <= uVar19) goto code_r0x0001814c5f91;
        *(longlong *)(lVar16 + 0x20 + (longlong)(int)uVar19 * 8) = lVar11;
        sub_8032e8d0(lVar16 + 0x20 + (longlong)(int)uVar19 * 8,lVar11);
      }
      else {
        sub_84a97470(lVar14,lVar11,
                      *(undefined8 *)(*(longlong *)(*(longlong *)(lVar17 + 0x20) + 0xc0) + 0x70));
      }
    }
    if ((lVar11 != 0) && (lVar14 = *(longlong *)(lVar11 + 0x18), lVar14 != 0)) {
      if (iVar9 < *(int *)(lVar14 + 0x18)) {
        lVar14 = sub_849ed2d0(lVar14,iVar9,uRam0000000187b6afe0);
        if (lVar14 == 0) goto code_r0x0001814c5f8b;
        uVar12 = *(undefined8 *)(lVar14 + 0x20);
        uVar15 = sub_8032f970(uRam0000000187bc09d8);
        sub_84aa4390(uVar15,uVar12,uRam0000000187b69798);
        *(undefined8 *)(lVar14 + 0x18) = uVar15;
        sub_8032e8d0(lVar14 + 0x18,uVar15);
        iStackX_20 = 0;
        if (*(longlong *)(lVar14 + 0x28) == 0) goto code_r0x0001814c5f8b;
        iStack_88 = *(int *)(*(longlong *)(lVar14 + 0x28) + 0x18);
        if (0 < iStack_88) {
          do {
            iVar9 = 0;
            if ((*(longlong *)(lVar14 + 0x28) == 0) ||
               (lVar11 = sub_849ed2d0(*(longlong *)(lVar14 + 0x28),iStackX_20,uRam0000000187b6a3c0)
               , lVar11 == 0)) goto code_r0x0001814c5f8b;
            for (; iVar9 < *(int *)(lVar11 + 0x1c); iVar9 = iVar9 + 1) {
              if (*(int *)(lVar11 + 0x20) == 0) {
                lVar17 = *(longlong *)(lVar14 + 0x18);
                lVar16 = sub_8032f970(uRam0000000187c332b0);
                *(undefined8 *)(lVar16 + 0x18) = 0x3ff0000000000000;
                *(undefined4 *)(lVar16 + 0x20) = 0x3f800000;
                sub_80443e50(lVar16,0);
                *(float *)(lVar16 + 0x10) =
                     (float)iVar9 * *(float *)(lVar11 + 0x14) + *(float *)(lVar11 + 0x10);
code_r0x0001814c58cb:
                *(undefined4 *)(lVar16 + 0x14) = 1;
                if (lVar17 == 0) goto code_r0x0001814c5f8b;
                sub_80002460(lVar17);
              }
              else if (*(int *)(lVar11 + 0x20) == 1) {
                lVar17 = *(longlong *)(lVar14 + 0x18);
                lVar16 = sub_8032f970(uRam0000000187c332b0);
                *(undefined8 *)(lVar16 + 0x18) = 0x3ff0000000000000;
                *(undefined4 *)(lVar16 + 0x20) = 0x3f800000;
                sub_80443e50(lVar16,0);
                fVar21 = *(float *)(lVar11 + 0x10);
                if (*(longlong *)(lVar11 + 0x28) != 0) {
                  fVar22 = (float)AnimationCurve::Evaluate(*(longlong *)(lVar11 + 0x28),
                                                (float)iVar9 / (float)*(int *)(lVar11 + 0x1c),0);
                  *(float *)(lVar16 + 0x10) =
                       (float)*(int *)(lVar11 + 0x1c) * fVar22 * *(float *)(lVar11 + 0x14) + fVar21;
                  goto code_r0x0001814c58cb;
                }
                goto code_r0x0001814c5f8b;
              }
            }
            iStackX_20 = iStackX_20 + 1;
          } while (iStackX_20 < iStack_88);
        }
        lVar11 = *(longlong *)(lVar14 + 0x18);
        if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
          sub_8032fa70();
        }
        lVar17 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x20);
        if (lVar17 == 0) {
          if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uVar12 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
          lVar17 = sub_8032f970(uRam0000000187b802a8);
          sub_854e7780(lVar17,uVar12,uRam0000000187bcfa38,0);
          *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x20) = lVar17;
          sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x20,lVar17);
        }
        if (lVar11 == 0) goto code_r0x0001814c5f8b;
        sub_84a9fb90(lVar11,lVar17,uRam0000000187b69b10);
        lVar11 = lStack_78;
      }
      else {
        lVar14 = sub_8032f970(uRam0000000187c337c8);
        if (cRam000000018ab6260b == '\0') {
          sub_8032f6d0(0x187b695e0);
          sub_8032f6d0(0x187b6a048);
          sub_8032f6d0(0x187b85ff0);
          sub_8032f6d0(0x187bc0b88);
          sub_8032f6d0(0x187bfde00);
          sub_8032f6d0(0x187bc09d8);
          cRam000000018ab6260b = '\x01';
        }
        uVar12 = sub_8032f970(uRam0000000187bc09d8);
        sub_849de5b0(uVar12,uRam0000000187b695e0);
        *(undefined8 *)(lVar14 + 0x20) = uVar12;
        sub_8032e8d0(lVar14 + 0x20,uVar12);
        uVar12 = sub_8032f970(uRam0000000187bc0b88);
        sub_849de5b0(uVar12,uRam0000000187b6a048);
        *(undefined8 *)(lVar14 + 0x28) = uVar12;
        sub_8032e8d0(lVar14 + 0x28,uVar12);
        uVar12 = sub_8032f970(uRam0000000187bfde00);
        sub_849de5b0(uVar12,uRam0000000187b85ff0);
        *(undefined8 *)(lVar14 + 0x38) = uVar12;
        sub_8032e8d0(lVar14 + 0x38,uVar12);
        uVar12 = sub_8032f970(uRam0000000187bfde00);
        sub_849de5b0(uVar12,uRam0000000187b85ff0);
        *(undefined8 *)(lVar14 + 0x40) = uVar12;
        sub_8032e8d0(lVar14 + 0x40,uVar12);
        sub_80443e50(lVar14,0);
        uVar12 = sub_8032f970(uRam0000000187bc09d8);
        sub_849de5b0(uVar12,uRam0000000187b695e0);
        *(undefined8 *)(lVar14 + 0x18) = uVar12;
        sub_8032e8d0(lVar14 + 0x18,uVar12);
        lVar16 = lRam0000000187b6ac68;
        lVar17 = *(longlong *)(lVar11 + 0x18);
        if (lVar17 == 0) goto code_r0x0001814c5f8b;
        *(int *)(lVar17 + 0x1c) = *(int *)(lVar17 + 0x1c) + 1;
        lVar2 = *(longlong *)(lVar17 + 0x10);
        uVar19 = *(uint *)(lVar17 + 0x18);
        if (lVar2 == 0) goto code_r0x0001814c5f8b;
        if (uVar19 < *(uint *)(lVar2 + 0x18)) {
          *(uint *)(lVar17 + 0x18) = uVar19 + 1;
          if (*(uint *)(lVar2 + 0x18) <= uVar19) {
code_r0x0001814c5f91:
            sub_8032f9c0();
            pcVar3 = (code *)swi(3);
            (*pcVar3)();
            return;
          }
          *(longlong *)(lVar2 + 0x20 + (longlong)(int)uVar19 * 8) = lVar14;
          sub_8032e8d0(lVar2 + ((longlong)(int)uVar19 + 4) * 8,lVar14);
        }
        else {
          sub_84a97470(lVar17,lVar14,
                        *(undefined8 *)(*(longlong *)(*(longlong *)(lVar16 + 0x20) + 0xc0) + 0x70));
        }
        if (lVar14 == 0) goto code_r0x0001814c5f8b;
      }
      plVar13 = (longlong *)0x0;
      plVar20[0xe] = *(longlong *)(lVar14 + 0x18);
      sub_8032e8d0(plVar20 + 0xe);
      if (*(longlong *)(lVar14 + 0x18) != 0) {
        lVar17 = sub_8032e960(uRam0000000187c013d8,
                                     *(undefined4 *)(*(longlong *)(lVar14 + 0x18) + 0x18));
        plVar20[0xd] = lVar17;
        sub_8032e8d0(plVar20 + 0xd,lVar17);
        plVar20[0x22] = *(longlong *)(lVar11 + 0x20);
        sub_8032e8d0(plVar20 + 0x22);
        plVar20[0x23] = *(longlong *)(lVar11 + 0x28);
        sub_8032e8d0(plVar20 + 0x23);
        *(undefined4 *)(plVar20 + 0x34) = *(undefined4 *)(lVar11 + 0x30);
        plVar20[0x14] = *(longlong *)(lVar14 + 0x38);
        sub_8032e8d0(plVar20 + 0x14);
        plVar20[0x15] = *(longlong *)(lVar14 + 0x40);
        sub_8032e8d0(plVar20 + 0x15);
        if (*(longlong *)(lVar14 + 0x18) != 0) {
          iVar9 = *(int *)(*(longlong *)(lVar14 + 0x18) + 0x18);
          plVar18 = plVar13;
          if (0 < iVar9) {
            do {
              dVar1 = (double)plVar20[0xf];
              if (*(longlong *)(lVar14 + 0x18) == 0) goto code_r0x0001814c5f8b;
              uVar19 = (uint)plVar18;
              lVar11 = sub_849ed2d0(*(longlong *)(lVar14 + 0x18),plVar18,uRam0000000187b69e88);
              if (lVar11 == 0) goto code_r0x0001814c5f8b;
              plVar20[0xf] = (longlong)(dVar1 + *(double *)(lVar11 + 0x18));
              lVar11 = plVar20[0xd];
              if (((*(longlong *)(lVar14 + 0x18) == 0) ||
                  (lVar17 = sub_849ed2d0(*(longlong *)(lVar14 + 0x18),plVar18,uRam0000000187b69e88)
                  , lVar17 == 0)) || (lVar11 == 0)) goto code_r0x0001814c5f8b;
              if (*(uint *)(lVar11 + 0x18) <= uVar19) goto code_r0x0001814c5f91;
              *(undefined4 *)(lVar11 + 0x20 + (longlong)(int)uVar19 * 4) =
                   *(undefined4 *)(lVar17 + 0x10);
              plVar18 = (longlong *)(ulonglong)(uVar19 + 1);
              lVar11 = lStack_78;
            } while ((int)(uVar19 + 1) < iVar9);
          }
          *(undefined4 *)(plVar20 + 0x10) = *(undefined4 *)(param_3 + 0x10);
          lVar14 = *(longlong *)(param_3 + 0x14);
          uVar8 = *(undefined4 *)(param_3 + 0x1c);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          lStack_78 = lVar14;
          uStack_70 = uVar8;
          uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
          *(undefined4 *)(plVar20 + 7) = uVar8;
          plVar18 = plVar13;
          while (lVar14 = plVar20[0xe], lVar14 != 0) {
            if (*(int *)(lVar14 + 0x18) <= (int)plVar18) {
code_r0x0001814c5e23:
              lVar14 = sub_8032f970(uRam0000000187bc5830);
              sub_849de5b0(lVar14,uRam0000000187b7f908);
              plVar20[10] = lVar14;
              sub_8032e8d0(plVar20 + 10,lVar14);
              lVar14 = sub_8032f970(uRam0000000187bf1860);
              sub_849de5b0(lVar14,uRam0000000187b413a0);
              plVar20[0x11] = lVar14;
              sub_8032e8d0(plVar20 + 0x11,lVar14);
              lVar14 = *(longlong *)(param_3 + 0x138);
              uVar8 = *(undefined4 *)(param_3 + 0x140);
              if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                sub_8032fa70();
              }
              lStack_78 = lVar14;
              uStack_70 = uVar8;
              uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
              *(undefined4 *)(plVar20 + 2) = uVar8;
              lStack_78 = *(longlong *)(param_3 + 0x144);
              uStack_70 = *(undefined4 *)(param_3 + 0x14c);
              uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_78,0);
              *(undefined4 *)((longlong)plVar20 + 0x24) = uVar8;
              *(undefined1 *)((longlong)plVar20 + 0x84) = *(undefined1 *)(lVar11 + 0x34);
              lVar14 = lRam0000000187c19748;
              lVar11 = *(longlong *)(param_5 + 0x20);
              if (lVar11 != 0) {
                *(int *)(lVar11 + 0x1c) = *(int *)(lVar11 + 0x1c) + 1;
                lVar17 = *(longlong *)(lVar11 + 0x10);
                uVar19 = *(uint *)(lVar11 + 0x18);
                if (lVar17 != 0) {
                  if (uVar19 < *(uint *)(lVar17 + 0x18)) {
                    *(uint *)(lVar11 + 0x18) = uVar19 + 1;
                    if (*(uint *)(lVar17 + 0x18) <= uVar19) goto code_r0x0001814c5f91;
                    *(longlong **)(lVar17 + 0x20 + (longlong)(int)uVar19 * 8) = plVar20;
                    sub_8032e8d0(lVar17 + 0x20 + (longlong)(int)uVar19 * 8,plVar20);
                  }
                  else {
                    sub_84a97470(lVar11,plVar20,
                                  *(undefined8 *)
                                   (*(longlong *)(*(longlong *)(lVar14 + 0x20) + 0xc0) + 0x70));
                  }
                  return;
                }
              }
              break;
            }
            lVar14 = sub_849ed2d0(lVar14,plVar18,uRam0000000187b69e88);
            if (lVar14 == 0) break;
            if ((*(int *)(lVar14 + 0x14) == 3) || (*(int *)(lVar14 + 0x14) == 4)) {
              lVar14 = *(longlong *)(lRam0000000187b3d4a8 + 0x20);
              if ((*(byte *)(lVar14 + 0x135) & 1) == 0) {
                lVar14 = sub_8032d010(lVar14);
              }
              lVar14 = *(longlong *)(*(longlong *)(lVar14 + 0xc0) + 0x10);
              if ((*(byte *)(lVar14 + 0x135) & 1) == 0) {
                lVar14 = sub_8032d010(lVar14);
              }
              if (**(longlong **)(lVar14 + 0xb8) != 0) {
                plVar18 = (longlong *)
                          ResourceManager::LoadResourceImmediately(**(longlong **)(lVar14 + 0xb8),0x507dbe19,
                                        (longlong)(int)plVar20[7],0,0);
                if (plVar18 != (longlong *)0x0) {
                  if ((*(byte *)(*plVar18 + 0x130) < *(byte *)(lRam0000000187c33980 + 0x130)) ||
                     (*(longlong *)
                       (*(longlong *)(*plVar18 + 200) + -8 +
                       (ulonglong)*(byte *)(lRam0000000187c33980 + 0x130) * 8) !=
                      lRam0000000187c33980)) {
                    bVar4 = false;
                  }
                  else {
                    bVar4 = true;
                  }
                  if (bVar4) {
                    plVar13 = plVar18;
                  }
                }
                plVar20[8] = (longlong)plVar13;
                sub_8032e8d0(plVar20 + 8,plVar13);
                goto code_r0x0001814c5e23;
              }
              break;
            }
            plVar18 = (longlong *)(ulonglong)((int)plVar18 + 1);
          }
        }
      }
    }
  }
code_r0x0001814c5f8b:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitActionController::createExEquipActionParameter  @ 0x14c5fa0  size=1 ==== */

longlong * UnitActionController::createExEquipActionParameter(longlong param_1,longlong param_2,longlong param_3,longlong param_4)

{
  uint uVar1;
  longlong lVar2;
  code *pcVar3;
  bool bVar4;
  undefined6 uVar5;
  undefined2 uVar6;
  ushort uVar7;
  undefined4 uVar8;
  int iVar9;
  undefined4 uVar10;
  undefined8 uVar11;
  longlong *plVar12;
  longlong lVar13;
  longlong lVar14;
  longlong lVar15;
  longlong *plVar16;
  double dVar17;
  double dVar18;
  double dVar19;
  float fVar20;
  undefined8 uStack_a8;
  undefined4 uStack_a0;
  undefined8 uStack_98;
  undefined8 uStack_90;
  undefined8 uStack_88;
  undefined8 uStack_80;
  
  if (cRam000000018ab626d5 == '\0') {
    sub_8032f6d0(0x187c332b0);
    sub_8032f6d0(0x187b3fb28);
    sub_8032f6d0(0x187b3cb78);
    sub_8032f6d0(0x187b3f978);
    sub_8032f6d0(0x187b40048);
    sub_8032f6d0(0x187b45a88);
    sub_8032f6d0(0x187ba6e58);
    sub_8032f6d0(0x187c3dbc8);
    sub_8032f6d0(0x187bf8138);
    sub_8032f6d0(0x187c162f0);
    sub_8032f6d0(0x187b69958);
    sub_8032f6d0(0x187b413a0);
    sub_8032f6d0(0x187b695e0);
    sub_8032f6d0(0x187b85ff0);
    sub_8032f6d0(0x187b7f908);
    sub_8032f6d0(0x187bf1860);
    sub_8032f6d0(0x187bfde00);
    sub_8032f6d0(0x187bc09d8);
    sub_8032f6d0(0x187bc5830);
    sub_8032f6d0(0x187b60c28);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b61d80);
    sub_8032f6d0(0x187c013d8);
    sub_8032f6d0(0x187b77e20);
    cRam000000018ab626d5 = '\x01';
  }
  if (*(int *)(lRam0000000187b77e20 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b77e20);
  }
  lVar13 = *(longlong *)(*(longlong *)(lRam0000000187b77e20 + 0xb8) + 8);
  if (param_2 != 0) {
    uVar5 = *(undefined6 *)(param_2 + 0x2c);
    if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_a8 = CONCAT26(uStack_a8._6_2_,uVar5);
    uVar6 = ObscuredUShort::FHOALPFLEEC(&uStack_a8,0);
    if (lVar13 != 0) {
      uVar11 = sub_855ffe50(lVar13,uVar6,uRam0000000187b45a88);
      plVar12 = (longlong *)Activator::CreateInstance(uVar11,0);
      if (plVar12 != (longlong *)0x0) {
        if ((*(byte *)(*plVar12 + 0x130) < *(byte *)(lRam0000000187c3dbc8 + 0x130)) ||
           (*(longlong *)
             (*(longlong *)(*plVar12 + 200) + -8 +
             (ulonglong)*(byte *)(lRam0000000187c3dbc8 + 0x130) * 8) != lRam0000000187c3dbc8)) {
          bVar4 = false;
        }
        else {
          bVar4 = true;
        }
        plVar16 = (longlong *)0x0;
        if (bVar4) {
          plVar16 = plVar12;
        }
        if (plVar16 != (longlong *)0x0) {
          *(undefined1 *)(plVar16 + 9) = 1;
          uVar11 = *(undefined8 *)(param_2 + 0x15c);
          uVar8 = *(undefined4 *)(param_2 + 0x164);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uStack_a8 = uVar11;
          uStack_a0 = uVar8;
          uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          *(undefined4 *)((longlong)plVar16 + 0x14) = uVar8;
          uStack_a8 = *(undefined8 *)(param_2 + 0x168);
          uStack_a0 = *(undefined4 *)(param_2 + 0x170);
          uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          *(undefined4 *)(plVar16 + 3) = uVar8;
          uStack_a8 = *(undefined8 *)(param_2 + 0x174);
          uStack_a0 = *(undefined4 *)(param_2 + 0x17c);
          uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          *(undefined4 *)((longlong)plVar16 + 0x1c) = uVar8;
          uStack_a8 = *(undefined8 *)(param_2 + 0x150);
          uStack_a0 = *(undefined4 *)(param_2 + 0x158);
          iVar9 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          if (iVar9 < 1) {
            if ((*(longlong *)(param_1 + 0x90) == 0) ||
               (lVar13 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar13 == 0))
            goto code_r0x0001814c6ac3;
            fVar20 = *(float *)(lVar13 + 0x18);
          }
          else {
            uVar11 = *(undefined8 *)(param_2 + 0x150);
            uVar8 = *(undefined4 *)(param_2 + 0x158);
            if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
              sub_8032fa70();
            }
            uStack_a8 = uVar11;
            uStack_a0 = uVar8;
            iVar9 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
            fVar20 = (float)iVar9;
          }
          *(float *)(plVar16 + 4) = fVar20;
          uVar5 = *(undefined6 *)(param_2 + 0x2c);
          if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uStack_a8 = CONCAT26(uStack_a8._6_2_,uVar5);
          uVar7 = ObscuredUShort::FHOALPFLEEC(&uStack_a8,0);
          *(uint *)(plVar16 + 0xc) = (uint)uVar7;
          uVar11 = *(undefined8 *)(param_2 + 0x34);
          uVar8 = *(undefined4 *)(param_2 + 0x3c);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uStack_a8 = uVar11;
          uStack_a0 = uVar8;
          uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          *(undefined4 *)((longlong)plVar16 + 0x2c) = uVar8;
          uStack_a8 = *(undefined8 *)(param_2 + 0x40);
          uStack_a0 = *(undefined4 *)(param_2 + 0x48);
          uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          *(undefined4 *)(plVar16 + 6) = uVar8;
          uStack_a8 = *(undefined8 *)(param_2 + 0x4c);
          uStack_a0 = *(undefined4 *)(param_2 + 0x54);
          uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          *(undefined4 *)((longlong)plVar16 + 0x34) = uVar8;
          uVar11 = sub_8032f970(uRam0000000187bf8138);
          sub_804383d0(uVar11,0);
          lVar13 = sub_8032f970(uRam0000000187ba6e58);
          sub_855de480(lVar13,uVar11,uRam0000000187b3f978);
          plVar16[0xb] = lVar13;
          sub_8032e8d0(plVar16 + 0xb,lVar13);
          lVar13 = plVar16[0xb];
          uStack_98 = *(undefined8 *)(param_2 + 0x58);
          uStack_90 = *(undefined8 *)(param_2 + 0x60);
          uStack_88 = *(undefined8 *)(param_2 + 0x68);
          uStack_80 = *(undefined8 *)(param_2 + 0x70);
          if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_98,0);
          if (lVar13 != 0) {
            sub_855e0980(lVar13,0,uVar8,uRam0000000187b3fb28);
            uStack_98 = *(undefined8 *)(param_2 + 0x78);
            uStack_90 = *(undefined8 *)(param_2 + 0x80);
            lVar13 = plVar16[0xb];
            uStack_88 = *(undefined8 *)(param_2 + 0x88);
            uStack_80 = *(undefined8 *)(param_2 + 0x90);
            uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_98,0);
            if (lVar13 != 0) {
              sub_855e0980(lVar13,1,uVar8,uRam0000000187b3fb28);
              uStack_98 = *(undefined8 *)(param_2 + 0x98);
              uStack_90 = *(undefined8 *)(param_2 + 0xa0);
              lVar13 = plVar16[0xb];
              uStack_88 = *(undefined8 *)(param_2 + 0xa8);
              uStack_80 = *(undefined8 *)(param_2 + 0xb0);
              uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_98,0);
              if (lVar13 != 0) {
                sub_855e0980(lVar13,2,uVar8,uRam0000000187b3fb28);
                uStack_98 = *(undefined8 *)(param_2 + 0xb8);
                uStack_90 = *(undefined8 *)(param_2 + 0xc0);
                lVar13 = plVar16[0xb];
                uStack_88 = *(undefined8 *)(param_2 + 200);
                uStack_80 = *(undefined8 *)(param_2 + 0xd0);
                uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_98,0);
                if (lVar13 != 0) {
                  sub_855e0980(lVar13,3,uVar8,uRam0000000187b3fb28);
                  uStack_98 = *(undefined8 *)(param_2 + 0xd8);
                  uStack_90 = *(undefined8 *)(param_2 + 0xe0);
                  lVar13 = plVar16[0xb];
                  uStack_88 = *(undefined8 *)(param_2 + 0xe8);
                  uStack_80 = *(undefined8 *)(param_2 + 0xf0);
                  uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_98,0);
                  if (lVar13 != 0) {
                    sub_855e0980(lVar13,4,uVar8,uRam0000000187b3fb28);
                    uStack_98 = *(undefined8 *)(param_2 + 0xf8);
                    uStack_90 = *(undefined8 *)(param_2 + 0x100);
                    lVar13 = plVar16[0xb];
                    uStack_88 = *(undefined8 *)(param_2 + 0x108);
                    uStack_80 = *(undefined8 *)(param_2 + 0x110);
                    uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_98,0);
                    if (lVar13 != 0) {
                      sub_855e0980(lVar13,5,uVar8,uRam0000000187b3fb28);
                      uStack_98 = *(undefined8 *)(param_2 + 0x118);
                      uStack_90 = *(undefined8 *)(param_2 + 0x120);
                      lVar13 = plVar16[0xb];
                      uStack_88 = *(undefined8 *)(param_2 + 0x128);
                      uStack_80 = *(undefined8 *)(param_2 + 0x130);
                      uVar8 = ObscuredDouble::FHOALPFLEEC(&uStack_98,0);
                      if (lVar13 != 0) {
                        sub_855e0980(lVar13,6,uVar8,uRam0000000187b3fb28);
                        plVar16[0x33] = param_2;
                        sub_8032e8d0(plVar16 + 0x33,param_2);
                        lVar13 = sub_8032f970(uRam0000000187bfde00);
                        sub_849de5b0(lVar13,uRam0000000187b85ff0);
                        plVar16[0x14] = lVar13;
                        sub_8032e8d0(plVar16 + 0x14,lVar13);
                        lVar14 = sub_8032f970(uRam0000000187bc09d8);
                        sub_849de5b0(lVar14,uRam0000000187b695e0);
                        lVar15 = sub_8032f970(uRam0000000187c332b0);
                        *(undefined8 *)(lVar15 + 0x18) = 0x3ff0000000000000;
                        *(undefined4 *)(lVar15 + 0x20) = 0x3f800000;
                        sub_80443e50(lVar15,0);
                        *(undefined4 *)(lVar15 + 0x14) = 0;
                        *(undefined8 *)(lVar15 + 0x18) = 0x3ff0000000000000;
                        lVar13 = lRam0000000187b69958;
                        if (lVar14 != 0) {
                          *(int *)(lVar14 + 0x1c) = *(int *)(lVar14 + 0x1c) + 1;
                          lVar2 = *(longlong *)(lVar14 + 0x10);
                          uVar1 = *(uint *)(lVar14 + 0x18);
                          if (lVar2 != 0) {
                            if (uVar1 < *(uint *)(lVar2 + 0x18)) {
                              *(uint *)(lVar14 + 0x18) = uVar1 + 1;
                              if (*(uint *)(lVar2 + 0x18) <= uVar1) {
                                sub_8032f9c0();
                                pcVar3 = (code *)swi(3);
                                plVar12 = (longlong *)(*pcVar3)();
                                return plVar12;
                              }
                              *(longlong *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = lVar15;
                              sub_8032e8d0(lVar2 + (longlong)(int)uVar1 * 8 + 0x20,lVar15);
                            }
                            else {
                              sub_84a97470(lVar14,lVar15,
                                            *(undefined8 *)
                                             (*(longlong *)(*(longlong *)(lVar13 + 0x20) + 0xc0) +
                                             0x70));
                            }
                            plVar16[0xe] = lVar14;
                            sub_8032e8d0(plVar16 + 0xe,lVar14);
                            plVar16[0xf] = 0x3ff0000000000000;
                            lVar13 = sub_8032e960(uRam0000000187c013d8,1);
                            plVar16[0xd] = lVar13;
                            sub_8032e8d0(plVar16 + 0xd,lVar13);
                            if (param_3 != 0) {
                              plVar16[0x22] = *(longlong *)(param_3 + 0x18);
                              sub_8032e8d0(plVar16 + 0x22);
                              plVar16[0x23] = *(longlong *)(param_3 + 0x20);
                              sub_8032e8d0(plVar16 + 0x23);
                              *(undefined4 *)(plVar16 + 0x34) = *(undefined4 *)(param_3 + 0x28);
                              *(undefined4 *)(plVar16 + 0x10) = *(undefined4 *)(param_2 + 0x10);
                              uStack_a8 = *(undefined8 *)(param_2 + 0x14);
                              uStack_a0 = *(undefined4 *)(param_2 + 0x1c);
                              uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
                              *(undefined4 *)(plVar16 + 7) = uVar8;
                              lVar13 = sub_8032f970(uRam0000000187bc5830);
                              sub_849de5b0(lVar13,uRam0000000187b7f908);
                              plVar16[10] = lVar13;
                              sub_8032e8d0(plVar16 + 10,lVar13);
                              lVar13 = sub_8032f970(uRam0000000187bf1860);
                              sub_849de5b0(lVar13,uRam0000000187b413a0);
                              plVar16[0x11] = lVar13;
                              sub_8032e8d0(plVar16 + 0x11,lVar13);
                              uStack_a8 = *(undefined8 *)(param_2 + 0x138);
                              uStack_a0 = *(undefined4 *)(param_2 + 0x140);
                              uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
                              *(undefined4 *)(plVar16 + 2) = uVar8;
                              uStack_a8 = *(undefined8 *)(param_2 + 0x144);
                              uStack_a0 = *(undefined4 *)(param_2 + 0x14c);
                              uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
                              *(undefined4 *)((longlong)plVar16 + 0x24) = uVar8;
                              if ((int)plVar16[0xc] == 0x385) {
                                if (param_4 != 0) {
                                  lVar13 = *(longlong *)(param_4 + 0x28);
                                  lVar14 = sub_8032f970(uRam0000000187c162f0);
                                  sub_804383d0(lVar14,0);
                                  if (lVar13 != 0) {
                                    uVar8 = 0;
code_r0x0001814c6a6f:
                                    sub_855f0b70(lVar13,uVar8,lVar14,uRam0000000187b3cb78);
                                    return plVar16;
                                  }
                                }
                              }
                              else {
                                if ((int)plVar16[0xc] != 0x386) {
                                  return plVar16;
                                }
                                if (plVar16[0xb] != 0) {
                                  dVar17 = (double)sub_855f06a0(plVar16[0xb],3,uRam0000000187b40048
                                                                );
                                  if (param_4 != 0) {
                                    if (0.0 < (float)dVar17) {
                                      *(undefined1 *)(param_4 + 0x54) = 1;
                                    }
                                    lVar13 = *(longlong *)(param_4 + 0x28);
                                    uVar8 = *(undefined4 *)((longlong)plVar16 + 0x2c);
                                    lVar14 = sub_8032f970(uRam0000000187c162f0);
                                    sub_804383d0(lVar14,0);
                                    if (lVar14 != 0) {
                                      *(int *)(lVar14 + 0x10) = (int)plVar16[6];
                                      *(undefined4 *)(lVar14 + 0x14) =
                                           *(undefined4 *)((longlong)plVar16 + 0x34);
                                      if (plVar16[0xb] != 0) {
                                        dVar18 = (double)sub_855f06a0(plVar16[0xb],0,
                                                                       uRam0000000187b40048);
                                        if (plVar16[0xb] != 0) {
                                          dVar19 = (double)sub_855f06a0(plVar16[0xb],1,
                                                                         uRam0000000187b40048);
                                          if ((*(longlong *)(param_1 + 0x90) != 0) &&
                                             (lVar15 = *(longlong *)
                                                        (*(longlong *)(param_1 + 0x90) + 0x88),
                                             lVar15 != 0)) {
                                            uVar10 = *(undefined4 *)(lVar15 + 0x428);
                                            uVar11 = *(undefined8 *)(lVar15 + 0x420);
                                            if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                              sub_8032fa70();
                                            }
                                            uStack_a8 = uVar11;
                                            uStack_a0 = uVar10;
                                            iVar9 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
                                            fVar20 = (float)((double)iVar9 * dVar19 + dVar18);
                                            if (fVar20 < 0.0) {
                                              fVar20 = 0.0;
                                            }
                                            else if (1.0 < fVar20) {
                                              fVar20 = 1.0;
                                            }
                                            *(float *)(lVar14 + 0x18) = fVar20;
                                            if (plVar16[0xb] != 0) {
                                              uVar11 = sub_855f06a0(plVar16[0xb],2,
                                                                     uRam0000000187b40048);
                                              *(undefined8 *)(lVar14 + 0x20) = uVar11;
                                              *(float *)(lVar14 + 0x28) = (float)dVar17;
                                              if (plVar16[0xb] != 0) {
                                                uVar11 = sub_855f06a0(plVar16[0xb],4,
                                                                       uRam0000000187b40048);
                                                uVar10 = BattleUtil::DoubleToInt(uVar11,0);
                                                *(undefined4 *)(lVar14 + 0x2c) = uVar10;
                                                if (lVar13 != 0) goto code_r0x0001814c6a6f;
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
code_r0x0001814c6ac3:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  plVar12 = (longlong *)(*pcVar3)();
  return plVar12;
}



/* ==== UnitActionController::createNormalEffectPrefab  @ 0x14c6ad0  size=1 ==== */

/* WARNING: Instruction at (ram,0x0001814c700d) overlaps instruction at (ram,0x0001814c700c)
    */

void UnitActionController::createNormalEffectPrefab(longlong param_1,byte *param_2,longlong param_3,undefined8 param_4,
                  longlong param_5)

{
  byte bVar1;
  float fVar2;
  undefined4 uVar3;
  longlong lVar4;
  ulonglong uVar5;
  int *piVar6;
  longlong lVar7;
  undefined8 uVar8;
  code *pcVar9;
  char cVar10;
  longlong lVar11;
  char *pcVar12;
  longlong lVar13;
  byte bVar14;
  ulonglong uVar15;
  undefined1 *puVar16;
  undefined1 *puVar17;
  undefined4 *puVar18;
  byte *pbVar19;
  undefined8 uVar20;
  byte bVar21;
  longlong *unaff_R12;
  undefined4 unaff_XMM7_Da;
  undefined4 unaff_XMM7_Db;
  float unaff_XMM11_Da;
  char in_stack_00000050;
  undefined1 auStack_148 [56];
  char acStack_110 [200];
  undefined4 uStack_48;
  undefined4 uStack_44;
  undefined1 auStack_40 [24];
  
  puVar18 = &uStack_48;
  puVar16 = auStack_148;
  lVar13 = param_3;
  uVar20 = param_4;
  if (cRam000000018ab626b5 == '\0') {
    sub_8032f6d0(0x187c21b68);
    sub_8032f6d0(0x187c2f828);
    sub_8032f6d0(0x187b33e08);
    sub_8032f6d0(0x187b355e0);
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187b65e38);
    sub_8032f6d0(0x187bcd128);
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187c4ad38);
    sub_8032f6d0(0x187b42b50);
    sub_8032f6d0(0x187b5f350);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187c318b8);
    sub_8032f6d0(0x187c19748);
    sub_8032f6d0(0x187c31038);
    sub_8032f6d0(0x187c19590);
    sub_8032f6d0(0x187b9aea8);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b9b058);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187c20b78);
    sub_8032f6d0(0x187be2298);
    sub_8032f6d0(0x187b53068);
    sub_8032f6d0(0x187bd1090);
    sub_8032f6d0(0x187c1fc40);
    sub_8032f6d0(0x187b2fec0);
    cRam000000018ab626b5 = '\x01';
  }
  bVar21 = (byte)uVar20;
  uStack_48 = unaff_XMM7_Da;
  uStack_44 = unaff_XMM7_Db;
  lVar11 = sub_8032f970(uRam0000000187c1fc40);
  sub_804383d0(lVar11,0);
  puVar17 = auStack_148;
  if (lVar11 == 0) goto code_r0x0001814c817a;
  *(longlong *)(lVar11 + 0x10) = param_1;
  sub_8032e8d0(lVar11 + 0x10,param_1);
  *(byte **)(lVar11 + 0x18) = param_2;
  sub_8032e8d0(lVar11 + 0x18,param_2);
  *(longlong *)(lVar11 + 0x20) = param_3;
  sub_8032e8d0(lVar11 + 0x20,param_3);
  uVar15 = lVar11 + 0x28;
  *(longlong *)(lVar11 + 0x28) = param_5;
  sub_8032e8d0();
  lVar4 = *(longlong *)(lVar11 + 0x18);
  puVar17 = auStack_148;
  if (lVar4 == 0) goto code_r0x0001814c817a;
  if (*(int *)(lVar4 + 0x50) == 6) {
    if (*(longlong **)(lVar4 + 0x40) == (longlong *)0x0) {
code_r0x0001814c6e49:
      param_2 = *(byte **)(lVar11 + 0x18);
      puVar17 = auStack_148;
      if (*(longlong *)(lVar11 + 0x28) == 0) goto code_r0x0001814c817a;
      param_3 = *(longlong *)(*(longlong *)(lVar11 + 0x28) + 0x68);
      if (cRam000000018ab62646 == '\0') {
        sub_8032f6d0(0x187b58d08);
        sub_8032f6d0(0x187b58ec0);
        sub_8032f6d0(0x187b59228);
        sub_8032f6d0(0x187b59590);
        sub_8032f6d0(0x187b71920);
        cRam000000018ab62646 = '\x01';
      }
      puVar17 = auStack_148;
      if (*(longlong *)(param_2 + 0xa8) == 0) goto code_r0x0001814c817a;
      cVar10 = sub_85653020(*(longlong *)(param_2 + 0xa8),param_3,uRam0000000187b58ec0);
      lVar13 = *(longlong *)(param_2 + 0xa8);
      param_5 = param_3;
      if (cVar10 == '\0') {
        puVar17 = auStack_148;
        if (lVar13 == 0) goto code_r0x0001814c817a;
        uVar20 = uRam0000000187b58d08;
        sub_856640d0(lVar13,param_3,1);
        bVar21 = (byte)uVar20;
        uVar15 = *(ulonglong *)(param_2 + 0xb0);
        puVar17 = auStack_148;
        if (uVar15 == 0) goto code_r0x0001814c817a;
        lVar13 = lRam0000000187b71920;
        sub_80002460();
      }
      else {
        puVar17 = auStack_148;
        if (lVar13 == 0) goto code_r0x0001814c817a;
        uVar20 = uRam0000000187b59228;
        cVar10 = sub_85675470(lVar13,param_3,uRam0000000187b59228);
        if (cVar10 != '\0') {
          return;
        }
        uVar15 = *(ulonglong *)(param_2 + 0xa8);
        puVar17 = auStack_148;
        if (uVar15 == 0) goto code_r0x0001814c817a;
        lVar13 = CONCAT71((int7)((ulonglong)uVar20 >> 8),1);
        uVar20 = uRam0000000187b59590;
        sub_856756f0();
        bVar21 = (byte)uVar20;
      }
    }
    else {
      lVar13 = **(longlong **)(lVar4 + 0x40);
      if ((*(byte *)(lVar13 + 0x130) < *(byte *)(lRam0000000187c21b68 + 0x130)) ||
         (uVar15 = (ulonglong)*(byte *)(lRam0000000187c21b68 + 0x130),
         param_5 = lRam0000000187c21b68,
         *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != lRam0000000187c21b68)) {
        lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
        if ((*(byte *)(lVar13 + 0x130) < *(byte *)(lRam0000000187b355e0 + 0x130)) ||
           (uVar15 = (ulonglong)*(byte *)(lRam0000000187b355e0 + 0x130),
           param_5 = lRam0000000187b355e0,
           *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != lRam0000000187b355e0)) {
          lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
          if ((*(byte *)(lVar13 + 0x130) < *(byte *)(lRam0000000187b5f350 + 0x130)) ||
             (uVar15 = (ulonglong)*(byte *)(lRam0000000187b5f350 + 0x130),
             param_5 = lRam0000000187b5f350,
             *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != lRam0000000187b5f350))
          {
            lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
            if ((*(byte *)(lVar13 + 0x130) < *(byte *)(lRam0000000187b65e38 + 0x130)) ||
               (uVar15 = (ulonglong)*(byte *)(lRam0000000187b65e38 + 0x130),
               param_5 = lRam0000000187b65e38,
               *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != lRam0000000187b65e38)
               ) {
              lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
              if ((*(byte *)(lVar13 + 0x130) < *(byte *)(lRam0000000187b42b50 + 0x130)) ||
                 (uVar15 = (ulonglong)*(byte *)(lRam0000000187b42b50 + 0x130),
                 param_5 = lRam0000000187b42b50,
                 *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) !=
                 lRam0000000187b42b50)) {
                lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
                if ((*(byte *)(lVar13 + 0x130) < *(byte *)(lRam0000000187bcd128 + 0x130)) ||
                   (uVar15 = (ulonglong)*(byte *)(lRam0000000187bcd128 + 0x130),
                   param_5 = lRam0000000187bcd128,
                   *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) !=
                   lRam0000000187bcd128)) goto code_r0x0001814c6e49;
              }
            }
          }
        }
      }
    }
  }
  uVar5 = *(ulonglong *)(lVar11 + 0x18);
  puVar17 = auStack_148;
  if (uVar5 == 0) goto code_r0x0001814c817a;
  if (*(int *)(uVar5 + 0xa0) != 0) {
    puVar17 = auStack_148;
    if (*(longlong *)(lVar11 + 0x20) == 0) goto code_r0x0001814c817a;
    uVar15 = uVar5;
    if (*(int *)(uVar5 + 0xa0) != *(int *)(*(longlong *)(lVar11 + 0x20) + 0x130)) {
      return;
    }
  }
  if (*(int *)(*(longlong *)(lVar11 + 0x18) + 0x4c) == 1) {
    puVar17 = auStack_148;
    if (*(longlong *)(lVar11 + 0x20) == 0) goto code_r0x0001814c817a;
    if (*(char *)(*(longlong *)(lVar11 + 0x20) + 0x141) != '\0') {
      return;
    }
  }
  if ((in_stack_00000050 != '\0') && (*(char *)(param_1 + 0x9c) != '\0')) {
    return;
  }
  piVar6 = *(int **)(lVar11 + 0x18);
  if ((((char)piVar6[0x2e] == '\0') && (piVar6[0x14] != 1)) && (piVar6[0x14] != 6)) {
    pcVar12 = (char *)sub_814a9080(param_1,0);
    puVar17 = auStack_148;
    if (pcVar12 == (char *)0x0) goto code_r0x0001814c817a;
    cVar10 = (char)pcVar12;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    *pcVar12 = *pcVar12 + cVar10;
    lVar13 = *(longlong *)(lVar11 + 0x18);
    puVar17 = auStack_148;
    pbVar19 = param_2;
    if (lVar13 == 0) goto code_r0x0001814c817a;
  }
  else {
    cVar10 = (char)piVar6;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    *(char *)piVar6 = (char)*piVar6 + cVar10;
    pbVar19 = (byte *)(param_3 + 0x46 + uVar15 * 4);
    bVar1 = *pbVar19;
    bVar14 = (byte)uVar15;
    *pbVar19 = *pbVar19 + bVar14;
    acStack_110[0x4d] = (acStack_110[0x4d] - bVar14) - CARRY1(bVar1,bVar14);
    *param_2 = *param_2 >> 4 | *param_2 << 4;
    pbVar19 = param_2 + 4;
    uVar3 = in((short)param_5);
    *(undefined4 *)param_2 = uVar3;
    *piVar6 = *piVar6 + (int)piVar6;
    *(char *)((longlong)piVar6 + -0x75) = *(char *)((longlong)piVar6 + -0x75) + bVar14;
    *(byte *)((longlong)piVar6 + -0x7b) = *(byte *)((longlong)piVar6 + -0x7b) & bVar21;
    puVar18 = (undefined4 *)CONCAT44(uStack_44,uStack_48);
    puVar16 = auStack_40;
  }
  lVar4 = *(longlong *)(lVar11 + 0x20);
  puVar17 = puVar16;
  if ((lVar4 != 0) && (unaff_R12 != (longlong *)0x0)) {
    lVar7 = *unaff_R12;
    puVar18[-0x1a] = puVar18[-0x16];
    *(undefined8 *)(puVar18 + -0x1c) = *(undefined8 *)(puVar18 + -0x18);
    fVar2 = *(float *)(lVar4 + 0xb0);
    *(undefined8 *)(puVar16 + 0x60) = *(undefined8 *)(lVar7 + 0x2a0);
    *(undefined4 *)(puVar16 + 0x58) = *(undefined4 *)(lVar13 + 0x60);
    *(undefined8 *)(puVar16 + 0x50) = *(undefined8 *)(lVar13 + 0x98);
    *(undefined4 **)(puVar16 + 0x48) = puVar18 + -0x1c;
    puVar16[0x40] = puVar16[0x70];
    uVar20 = *(undefined8 *)(param_1 + 0x90);
    uVar3 = *(undefined4 *)(lVar13 + 0x88);
    uVar8 = *(undefined8 *)(puVar18 + -0x20);
    puVar16[0x38] = unaff_XMM11_Da < fVar2;
    *(undefined4 *)(puVar16 + 0x30) = uVar3;
    *(undefined8 *)(puVar16 + 0x28) = uVar20;
    *(longlong *)(puVar16 + 0x20) = param_3;
    pcVar9 = *(code **)(lVar7 + 0x298);
    *(undefined **)(puVar16 + -8) = &UNK_1814c80b2;
    (*pcVar9)(uVar3,pbVar19,uVar8);
    if (*(longlong *)(lVar11 + 0x18) != 0) {
      if (*(int *)(*(longlong *)(lVar11 + 0x18) + 0x50) == 1) {
        *(undefined **)(puVar16 + -8) = &UNK_1814c80e2;
        lVar13 = sub_8032f970(uRam0000000187c2f828);
        uVar20 = uRam0000000187b2fec0;
      }
      else {
        *(undefined **)(puVar16 + -8) = &UNK_1814c80d1;
        lVar13 = sub_8032f970(uRam0000000187c2f828);
        param_1 = lVar11;
        uVar20 = uRam0000000187bd1090;
      }
      *(undefined **)(puVar16 + -8) = &UNK_1814c80fa;
      sub_80afad00(lVar13,param_1,uVar20,0);
      unaff_R12[0x25] = lVar13;
      *(undefined **)(puVar16 + -8) = &UNK_1814c8112;
      sub_8032e8d0(unaff_R12 + 0x25,lVar13);
      *(undefined **)(puVar16 + -8) = &UNK_1814c811c;
      SkillEffectCtrl::RestartTween(param_4,0);
      return;
    }
  }
code_r0x0001814c817a:
  *(undefined **)(puVar17 + -8) = &UNK_1814c817f;
  sub_8032f9d0();
  pcVar9 = (code *)swi(3);
  (*pcVar9)();
  return;
}



/* ==== UnitActionController::createNormalPrefabWithDelay  @ 0x14c82c0  size=1 ==== */

longlong UnitActionController::createNormalPrefabWithDelay(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined1 param_4,
                      undefined1 param_5,undefined1 param_6,undefined1 param_7)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626b3 == '\0') {
    sub_8032f6d0(0x187c21930);
    cRam000000018ab626b3 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c21930);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x30) = param_1;
    sub_8032e8d0(lVar2 + 0x30,param_1);
    *(undefined8 *)(lVar2 + 0x38) = param_2;
    sub_8032e8d0(lVar2 + 0x38,param_2);
    *(undefined8 *)(lVar2 + 0x28) = param_3;
    sub_8032e8d0(lVar2 + 0x28,param_3);
    *(undefined1 *)(lVar2 + 0x40) = param_5;
    *(undefined1 *)(lVar2 + 0x41) = param_6;
    *(undefined1 *)(lVar2 + 0x42) = param_7;
    *(undefined1 *)(lVar2 + 0x20) = param_4;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::createNormalPrefabWithDelayAndTarget  @ 0x14c81e0  size=1 ==== */

longlong UnitActionController::createNormalPrefabWithDelayAndTarget(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined4 param_4,
                      undefined8 param_5,undefined1 param_6)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626b4 == '\0') {
    sub_8032f6d0(0x187c21ae8);
    cRam000000018ab626b4 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c21ae8);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x38) = param_1;
    sub_8032e8d0(lVar2 + 0x38,param_1);
    *(undefined8 *)(lVar2 + 0x40) = param_2;
    sub_8032e8d0(lVar2 + 0x40,param_2);
    *(undefined8 *)(lVar2 + 0x28) = param_3;
    sub_8032e8d0(lVar2 + 0x28,param_3);
    *(undefined8 *)(lVar2 + 0x48) = param_5;
    *(undefined4 *)(lVar2 + 0x30) = param_4;
    sub_8032e8d0(lVar2 + 0x48);
    *(undefined1 *)(lVar2 + 0x20) = param_6;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::createPrefab  @ 0x14c83a0  size=1 ==== */

longlong *
UnitActionController::createPrefab(longlong param_1,longlong param_2,undefined8 param_3,undefined8 param_4,
             longlong *param_5)

{
  uint uVar1;
  longlong lVar2;
  longlong lVar3;
  longlong *plVar4;
  code *pcVar5;
  char cVar6;
  longlong lVar7;
  undefined8 uVar8;
  longlong *plVar9;
  longlong lVar10;
  undefined8 uVar11;
  ulonglong uVar12;
  undefined8 *puVar13;
  ushort uVar14;
  ulonglong uVar15;
  
  if (cRam000000018ab626b6 == '\0') {
    sub_8032f6d0(0x187b2f280);
    sub_8032f6d0(0x187c0b200);
    sub_8032f6d0(0x187c4ad38);
    sub_8032f6d0(0x187b8ae18);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187c318b8);
    sub_8032f6d0(0x187b82c20);
    sub_8032f6d0(0x187bd1248);
    sub_8032f6d0(0x187bd1400);
    sub_8032f6d0(0x187bd15b8);
    sub_8032f6d0(0x187c1fdf0);
    cRam000000018ab626b6 = '\x01';
  }
  lVar7 = sub_8032f970(uRam0000000187c1fdf0);
  sub_804383d0(lVar7,0);
  if (lVar7 != 0) {
    *(undefined8 *)(lVar7 + 0x10) = param_3;
    sub_8032e8d0(lVar7 + 0x10,param_3);
    *(undefined8 *)(lVar7 + 0x18) = param_4;
    sub_8032e8d0(lVar7 + 0x18,param_4);
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (lVar10 != 0) {
      if ((*(char *)(lVar10 + 0x165) == '\0') && (cVar6 = UnitCtrl::get_IsForceLeftDirOrPartsBoss(lVar10,0), cVar6 == '\0')) {
        if (param_2 == 0) goto code_r0x0001814c8945;
        lVar10 = *(longlong *)(param_2 + 0x10);
      }
      else {
        if (param_2 == 0) goto code_r0x0001814c8945;
        lVar10 = *(longlong *)(param_2 + 0x18);
      }
      *param_5 = lVar10;
      sub_8032e8d0(param_5);
      if (*param_5 != 0) {
        uVar8 = Object::get_name(*param_5,0);
        if (cRam000000018ab626db == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626db = '\x01';
        }
        lVar10 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x18);
        if ((lVar10 != 0) &&
           (plVar9 = (longlong *)
                     sub_800121c0(0,uRam0000000187b8ae18,lVar10,*param_5,
                                   *(undefined8 *)(param_1 + 0x90)), plVar9 != (longlong *)0x0)) {
          lVar10 = Component::get_transform(plVar9,0);
          uVar11 = ExceptNGUIRoot::get_Transform(0);
          if (lVar10 != 0) {
            Transform::set_parent(lVar10,uVar11,0);
            lVar10 = lRam0000000187c318b8;
            if ((*(longlong *)(lVar7 + 0x10) != 0) &&
               (lVar2 = *(longlong *)(*(longlong *)(lVar7 + 0x10) + 0x98), lVar2 != 0)) {
              *(int *)(lVar2 + 0x1c) = *(int *)(lVar2 + 0x1c) + 1;
              lVar3 = *(longlong *)(lVar2 + 0x10);
              uVar1 = *(uint *)(lVar2 + 0x18);
              if (lVar3 != 0) {
                if (uVar1 < *(uint *)(lVar3 + 0x18)) {
                  *(uint *)(lVar2 + 0x18) = uVar1 + 1;
                  if (*(uint *)(lVar3 + 0x18) <= uVar1) {
                    sub_8032f9c0();
                    pcVar5 = (code *)swi(3);
                    plVar9 = (longlong *)(*pcVar5)();
                    return plVar9;
                  }
                  *(longlong **)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = plVar9;
                  sub_8032e8d0(lVar3 + ((longlong)(int)uVar1 + 4) * 8,plVar9);
                }
                else {
                  sub_84a97470(lVar2,plVar9,
                                *(undefined8 *)
                                 (*(longlong *)(*(longlong *)(lVar10 + 0x20) + 0xc0) + 0x70));
                }
                uVar15 = 0;
                if (*(int *)(param_2 + 0x4c) == 1) {
                  if ((*(longlong *)(lVar7 + 0x10) == 0) ||
                     (lVar10 = *(longlong *)(*(longlong *)(lVar7 + 0x10) + 0xa0), lVar10 == 0))
                  goto code_r0x0001814c8945;
                  sub_80002460(lVar10,plVar9,lRam0000000187c318b8);
                  lVar10 = plVar9[0x11];
                  uVar11 = sub_8032f970(uRam0000000187b2f280);
                  sub_80afad00(uVar11,lVar7,uRam0000000187bd1248,0);
                  lVar10 = Delegate::Combine(lVar10,uVar11,0);
                  uVar11 = uRam0000000187b2f280;
                  uVar12 = uVar15;
                  if ((lVar10 != 0) &&
                     (uVar12 = sub_8032e8f0(lVar10,uRam0000000187b2f280), uVar12 == 0)) {
                    sub_8032e920(lVar10,uVar11);
                    pcVar5 = (code *)swi(3);
                    plVar9 = (longlong *)(*pcVar5)();
                    return plVar9;
                  }
                  plVar9[0x11] = uVar12;
                  sub_8032e8d0(plVar9 + 0x11,uVar12);
                }
                if ((char)plVar9[10] != '\0') {
                  lVar10 = *plVar9;
                  if (((*(byte *)(lVar10 + 0x130) < *(byte *)(lRam0000000187c4ad38 + 0x130)) ||
                      (*(longlong *)
                        (*(longlong *)(lVar10 + 200) + -8 +
                        (ulonglong)*(byte *)(lRam0000000187c4ad38 + 0x130) * 8) !=
                       lRam0000000187c4ad38)) &&
                     ((*(byte *)(lVar10 + 0x130) < *(byte *)(lRam0000000187b82c20 + 0x130) ||
                      (*(longlong *)
                        (*(longlong *)(lVar10 + 200) + -8 +
                        (ulonglong)*(byte *)(lRam0000000187b82c20 + 0x130) * 8) !=
                       lRam0000000187b82c20)))) {
                    if (((*(longlong *)(lVar7 + 0x18) == 0) ||
                        (lVar10 = *(longlong *)(*(longlong *)(lVar7 + 0x18) + 0x68), lVar10 == 0))
                       || (lVar10 = *(longlong *)(lVar10 + 0x100), lVar10 == 0))
                    goto code_r0x0001814c8945;
                    sub_80002460(lVar10,plVar9,lRam0000000187c318b8);
                    lVar10 = plVar9[0x11];
                    uVar11 = sub_8032f970(uRam0000000187b2f280);
                    sub_80afad00(uVar11,lVar7,uRam0000000187bd1400,0);
                    lVar10 = Delegate::Combine(lVar10,uVar11,0);
                    uVar11 = uRam0000000187b2f280;
                    uVar12 = uVar15;
                    if ((lVar10 != 0) &&
                       (uVar12 = sub_8032e8f0(lVar10,uRam0000000187b2f280), uVar12 == 0)) {
                      sub_8032e920(lVar10,uVar11);
                      pcVar5 = (code *)swi(3);
                      plVar9 = (longlong *)(*pcVar5)();
                      return plVar9;
                    }
                    plVar9[0x11] = uVar12;
                    sub_8032e8d0(plVar9 + 0x11,uVar12);
                  }
                }
                lVar10 = plVar9[0x11];
                uVar11 = sub_8032f970(uRam0000000187b2f280);
                sub_80afad00(uVar11,lVar7,uRam0000000187bd15b8,0);
                lVar7 = Delegate::Combine(lVar10,uVar11,0);
                uVar11 = uRam0000000187b2f280;
                uVar12 = uVar15;
                if ((lVar7 != 0) &&
                   (uVar12 = sub_8032e8f0(lVar7,uRam0000000187b2f280), uVar12 == 0)) {
                  sub_8032e920(lVar7,uVar11);
                  pcVar5 = (code *)swi(3);
                  plVar9 = (longlong *)(*pcVar5)();
                  return plVar9;
                }
                plVar9[0x11] = uVar12;
                sub_8032e8d0(plVar9 + 0x11,uVar12);
                if (*(char *)(param_2 + 0x28) == '\0') {
                  return plVar9;
                }
                if (cRam000000018ab626da == '\0') {
                  sub_8032f6d0(0x187bfaba0);
                  cRam000000018ab626da = '\x01';
                }
                plVar4 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
                if (plVar4 != (longlong *)0x0) {
                  lVar7 = *plVar4;
                  if (*(ushort *)(lVar7 + 0x12e) != 0) {
                    do {
                      if (*(longlong *)(*(longlong *)(lVar7 + 0xb0) + uVar15 * 0x10) ==
                          lRam0000000187b93630) {
                        puVar13 = (undefined8 *)
                                  ((longlong)
                                   (*(int *)(*(longlong *)(lVar7 + 0xb0) + 8 + uVar15 * 0x10) + 0x10
                                   ) * 0x10 + 0x138 + lVar7);
                        goto code_r0x0001814c88d2;
                      }
                      uVar14 = (short)uVar15 + 1;
                      uVar15 = (ulonglong)uVar14;
                    } while (uVar14 < *(ushort *)(lVar7 + 0x12e));
                  }
                  puVar13 = (undefined8 *)sub_8032c930(plVar4,lRam0000000187b93630,0x10);
code_r0x0001814c88d2:
                  lVar7 = (*(code *)*puVar13)(plVar4,puVar13[1]);
                  uVar11 = Component::get_gameObject(plVar9,0);
                  if (lVar7 != 0) {
                    sub_85685e10(lVar7,uVar8,uVar11,uRam0000000187c0b200);
                    return plVar9;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
code_r0x0001814c8945:
  sub_8032f9d0();
  pcVar5 = (code *)swi(3);
  plVar9 = (longlong *)(*pcVar5)();
  return plVar9;
}



/* ==== UnitActionController::execActionConsiderTargetNum  @ 0x14c8980  size=1 ==== */

void UnitActionController::execActionConsiderTargetNum(undefined8 param_1,undefined8 param_2,longlong param_3)

{
  code *pcVar1;
  int iVar2;
  longlong lVar3;
  int iVar4;
  
  if (cRam000000018ab626d2 == '\0') {
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b812d0);
    cRam000000018ab626d2 = '\x01';
  }
  iVar4 = 0;
  if (param_3 != 0) {
    while (*(longlong *)(param_3 + 0x50) != 0) {
      if (*(int *)(*(longlong *)(param_3 + 0x50) + 0x18) <= iVar4) {
        return;
      }
      iVar2 = 1;
      if (*(int *)(param_3 + 0x1c) != 0) {
        iVar2 = *(int *)(param_3 + 0x1c);
      }
      if (iVar2 <= iVar4) {
        return;
      }
      lVar3 = sub_849ed2d0(*(undefined8 *)(param_3 + 0x50),iVar4,uRam0000000187b812d0);
      if (lVar3 == 0) break;
      FEFJGFLKNLK::MNLBJHHAJHG(param_3,*(undefined8 *)(lVar3 + 0x68),0,0);
      UnitActionController::ExecAction(param_1,param_3,param_2,lVar3,0,0,1,0,0,0);
      iVar4 = iVar4 + 1;
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitActionController::execActionOnWaveStart  @ 0x14c8ab0  size=1 ==== */

void UnitActionController::execActionOnWaveStart(longlong param_1,longlong param_2)

{
  longlong lVar1;
  code *pcVar2;
  longlong *plVar3;
  int iVar4;
  
  if (cRam000000018ab6269c == '\0') {
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c19e18);
    cRam000000018ab6269c = '\x01';
  }
  iVar4 = 0;
  if (param_2 != 0) {
    while (lVar1 = *(longlong *)(param_2 + 0x20), lVar1 != 0) {
      if (*(int *)(lVar1 + 0x18) <= iVar4) {
        return;
      }
      plVar3 = (longlong *)sub_849ed2d0(lVar1,iVar4,uRam0000000187c19e18);
      if (plVar3 == (longlong *)0x0) break;
      (**(code **)(*plVar3 + 0x228))
                (plVar3,param_2,*(undefined8 *)(param_1 + 0x90),param_1,
                 *(undefined8 *)(*plVar3 + 0x230));
      iVar4 = iVar4 + 1;
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::filterTargetByFlight  @ 0x14c8b60  size=1 ==== */

void UnitActionController::filterTargetByFlight(undefined8 param_1,longlong param_2)

{
  undefined8 uVar1;
  undefined8 uVar2;
  code *pcVar3;
  char cVar4;
  longlong lVar5;
  longlong lVar6;
  longlong lVar7;
  
  if (cRam000000018ab626bb == '\0') {
    sub_8032f6d0(0x187bd9b10);
    sub_8032f6d0(0x187bf0170);
    sub_8032f6d0(0x187b80c00);
    sub_8032f6d0(0x187b82670);
    sub_8032f6d0(0x187bcff60);
    sub_8032f6d0(0x187bd0118);
    sub_8032f6d0(0x187bd02d0);
    sub_8032f6d0(0x187bd1e50);
    sub_8032f6d0(0x187c20678);
    sub_8032f6d0(0x187c1f720);
    cRam000000018ab626bb = '\x01';
  }
  if (param_2 == 0) goto code_r0x0001814c8efd;
  if (*(int *)(param_2 + 0x14) == 0x2f) {
    return;
  }
  if (*(int *)(param_2 + 0x24) == 4) {
    return;
  }
  if (*(int *)(param_2 + 0x24) == 5) {
    return;
  }
  if (*(int *)(param_2 + 0x24) == 6) {
    return;
  }
  if (*(int *)(param_2 + 0x10) != 1) {
    return;
  }
  if (*(int *)(param_2 + 0x1c) == 1) {
    return;
  }
  if (*(char *)(param_2 + 0x86) == '\0') {
    uVar1 = *(undefined8 *)(param_2 + 0x50);
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar5 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x88);
    if (lVar5 == 0) {
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar2 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
      lVar5 = sub_8032f970(uRam0000000187bf0170);
      sub_84cacb40(lVar5,uVar2,uRam0000000187bd0118,0);
      *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x88) = lVar5;
      sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x88,lVar5);
    }
    cVar4 = sub_84789f00(uVar1,lVar5,uRam0000000187bd9b10);
    if (cVar4 == '\0') {
      return;
    }
    lVar5 = *(longlong *)(param_2 + 0x50);
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar6 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x90);
    if (lVar6 == 0) {
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar1 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
      lVar6 = sub_8032f970(uRam0000000187b82670);
      sub_84cacb40(lVar6,uVar1,uRam0000000187bd02d0,0);
      *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x90) = lVar6;
      lVar7 = *(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x90;
code_r0x0001814c8db7:
      sub_8032e8d0(lVar7,lVar6);
    }
  }
  else if (*(int *)(param_2 + 0x18) < 1) {
    lVar7 = sub_8032f970(uRam0000000187c20678);
    sub_804383d0(lVar7,0);
    if (lVar7 == 0) goto code_r0x0001814c8efd;
    *(undefined1 *)(lVar7 + 0x10) = *(undefined1 *)(param_2 + 0x98);
    lVar5 = *(longlong *)(param_2 + 0x50);
    lVar6 = sub_8032f970(uRam0000000187b82670);
    sub_84cacb40(lVar6,lVar7,uRam0000000187bd1e50,0);
  }
  else {
    lVar5 = *(longlong *)(param_2 + 0x50);
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar6 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x80);
    if (lVar6 == 0) {
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar1 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
      lVar6 = sub_8032f970(uRam0000000187b82670);
      sub_84cacb40(lVar6,uVar1,uRam0000000187bcff60,0);
      *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x80) = lVar6;
      lVar7 = *(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x80;
      goto code_r0x0001814c8db7;
    }
  }
  if (lVar5 != 0) {
    sub_84a9d8f0(lVar5,lVar6,uRam0000000187b80c00);
    return;
  }
code_r0x0001814c8efd:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitActionController::foregroundActiveWithDelay  @ 0x14c8f10  size=1 ==== */

longlong UnitActionController::foregroundActiveWithDelay(undefined8 param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626a7 == '\0') {
    sub_8032f6d0(0x187c21c98);
    cRam000000018ab626a7 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c21c98);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x28) = param_1;
    sub_8032e8d0(lVar2 + 0x28,param_1);
    *(undefined4 *)(lVar2 + 0x20) = param_2;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::get_ContinuousActionEndDone  @ 0x5e9a40  size=8 ==== */

undefined1 UnitActionController::get_ContinuousActionEndDone(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xb8);
}



/* ==== UnitActionController::get_DisableUBByModeChange  @ 0x14c8f90  size=8 ==== */

undefined1 UnitActionController::get_DisableUBByModeChange(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x9a);
}



/* ==== UnitActionController::get_IsCancelModeChangeByTrigger  @ 0x661010  size=8 ==== */

undefined1 UnitActionController::get_IsCancelModeChangeByTrigger(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xa8);
}



/* ==== UnitActionController::get_ModeChanging  @ 0x14c8fa0  size=8 ==== */

undefined1 UnitActionController::get_ModeChanging(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x9b);
}



/* ==== UnitActionController::get_MoveEnd  @ 0x14c8fb0  size=8 ==== */

undefined1 UnitActionController::get_MoveEnd(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xa9);
}



/* ==== UnitActionController::get_OnNormalAttackStart  @ 0x438c50  size=8 ==== */

undefined8 UnitActionController::get_OnNormalAttackStart(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xa0);
}



/* ==== UnitActionController::get_Owner  @ 0x438ab0  size=8 ==== */

undefined8 UnitActionController::get_Owner(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x90);
}



/* ==== UnitActionController::get_StopModeChangeEndEffectCalled  @ 0xa9fb60  size=8 ==== */

undefined1 UnitActionController::get_StopModeChangeEndEffectCalled(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x9c);
}



/* ==== UnitActionController::get_UnionBurstCharging  @ 0xf3f0f0  size=8 ==== */

undefined1 UnitActionController::get_UnionBurstCharging(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x99);
}



/* ==== UnitActionController::get_UnionBurstIsChargeTime  @ 0xe0b070  size=8 ==== */

undefined1 UnitActionController::get_UnionBurstIsChargeTime(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x98);
}



/* ==== UnitActionController::get_battleCameraEffect  @ 0x14c8fc0  size=1 ==== */

undefined8 UnitActionController::get_battleCameraEffect(void)

{
  if (cRam000000018ab62693 == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab62693 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x10);
}



/* ==== UnitActionController::get_battleEffectPool  @ 0x14c9000  size=1 ==== */

undefined8 UnitActionController::get_battleEffectPool(void)

{
  if (cRam000000018ab62694 == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab62694 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x18);
}



/* ==== UnitActionController::get_battleManager  @ 0x14c9040  size=1 ==== */

undefined8 UnitActionController::get_battleManager(void)

{
  if (cRam000000018ab62692 == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab62692 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
}



/* ==== UnitActionController::get_battleTimeScale  @ 0x14c9080  size=1 ==== */

undefined8 UnitActionController::get_battleTimeScale(void)

{
  if (cRam000000018ab62695 == '\0') {
    sub_8032f6d0(0x187bfaba0);
    cRam000000018ab62695 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 0x20);
}



/* ==== UnitActionController::get_isUnionBurstOnlyOwner  @ 0x14c90c0  size=8 ==== */

undefined1 UnitActionController::get_isUnionBurstOnlyOwner(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xb9);
}



/* ==== UnitActionController::get_skillDictionary  @ 0x44ad60  size=8 ==== */

undefined8 UnitActionController::get_skillDictionary(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xb0);
}



/* ==== UnitActionController::get_updateBranchMotionRunning  @ 0x8dba80  size=8 ==== */

undefined1 UnitActionController::get_updateBranchMotionRunning(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xba);
}



/* ==== UnitActionController::initWhenNoTarget  @ 0x14c90d0  size=1 ==== */

void UnitActionController::initWhenNoTarget(undefined8 param_1,longlong param_2,longlong param_3)

{
  undefined8 uVar1;
  longlong lVar2;
  code *pcVar3;
  undefined4 uVar4;
  longlong lVar5;
  longlong lVar6;
  int iVar7;
  
  if (cRam000000018ab626b2 == '\0') {
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187b8e140);
    sub_8032f6d0(0x187b8e2f8);
    sub_8032f6d0(0x187b8df90);
    sub_8032f6d0(0x187b8e660);
    sub_8032f6d0(0x187badb60);
    cRam000000018ab626b2 = '\x01';
  }
  if (param_2 != 0) {
    EventExtension::Call(*(undefined8 *)(param_2 + 0xe0),0);
    uVar1 = *(undefined8 *)(param_2 + 0x88);
    lVar5 = sub_8032f970(uRam0000000187badb60);
    sub_84cdd840(lVar5,uVar1,uRam0000000187b8df90);
    if (lVar5 != 0) {
      while( true ) {
        if (*(int *)(lVar5 + 0x20) < 1) {
          return;
        }
        if (param_3 == 0) break;
        lVar6 = *(longlong *)(param_3 + 0x20);
        uVar4 = sub_84cda8f0(lVar5,uRam0000000187b8e140);
        if (lVar6 == 0) break;
        lVar6 = sub_849ed2d0(lVar6,uVar4,uRam0000000187c19e18);
        if (lVar6 == 0) break;
        EventExtension::Call(*(undefined8 *)(lVar6 + 0xe0),0);
        iVar7 = 0;
        while( true ) {
          lVar2 = *(longlong *)(lVar6 + 0x88);
          if (lVar2 == 0) goto code_r0x0001814c9249;
          if (*(int *)(lVar2 + 0x18) <= iVar7) break;
          uVar4 = sub_84a610f0(lVar2,iVar7,uRam0000000187b446b8);
          sub_84cdb860(lVar5,uVar4,uRam0000000187b8e2f8);
          iVar7 = iVar7 + 1;
        }
      }
    }
  }
code_r0x0001814c9249:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitActionController::isInActionTargetArea  @ 0x14c9250  size=1 ==== */

void UnitActionController::isInActionTargetArea(longlong param_1,longlong param_2,longlong param_3,undefined8 *param_4,
                  undefined1 param_5,undefined8 param_6)

{
  code *pcVar1;
  int iVar2;
  longlong lVar3;
  undefined4 *puVar4;
  float fVar5;
  float fVar6;
  undefined8 uStack_38;
  undefined4 uStack_30;
  
  if (param_3 != 0) {
    lVar3 = *(longlong *)(param_1 + 0x90);
    switch(*(undefined4 *)(param_3 + 0x24)) {
    case 1:
    case 4:
    case 7:
      if (lVar3 == 0) goto code_r0x0001814c93d8;
      iVar2 = 1;
      if (*(char *)(lVar3 + 0x165) != '\0') {
        iVar2 = -1;
      }
      break;
    default:
      iVar2 = 0;
    }
    fVar6 = *(float *)(param_3 + 0x20);
    if (fVar6 <= 0.0) {
      if (param_2 == 0) goto code_r0x0001814c93d8;
      if (*(int *)(param_2 + 0x78) == 1) {
        if ((lVar3 == 0) || (*(longlong *)(lVar3 + 0xb8) == 0)) goto code_r0x0001814c93d8;
        fVar6 = *(float *)(*(longlong *)(lVar3 + 0xb8) + 0x18);
      }
      else {
        fVar6 = *(float *)(param_2 + 0x7c);
      }
    }
    if (iVar2 == 1) {
      fVar5 = 0.0;
    }
    else {
      fVar5 = -fVar6;
      if (iVar2 == -1) {
        fVar6 = 0.0;
      }
    }
    if (((*(longlong *)(param_1 + 0x20) != 0) &&
        (lVar3 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x18), lVar3 != 0)) &&
       (lVar3 = sub_86215a70(lVar3,0), lVar3 != 0)) {
      puVar4 = (undefined4 *)Transform::get_lossyScale(&uStack_38,lVar3,0);
      uStack_38 = *param_4;
      uStack_30 = *(undefined4 *)(param_4 + 1);
      UnitActionController::judgeIsInTargetArea(param_1,param_3,&uStack_38,param_5,*puVar4,fVar5,fVar6,param_6,0);
      return;
    }
  }
code_r0x0001814c93d8:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitActionController::judgeIsInTargetArea  @ 0x14c9400  size=1 ==== */

bool UnitActionController::judgeIsInTargetArea(longlong param_1,longlong param_2,float *param_3,char param_4,float param_5,
                  float param_6,float param_7,longlong *param_8)

{
  code *pcVar1;
  char cVar2;
  undefined1 uVar3;
  undefined8 *puVar4;
  float extraout_XMM0_Da;
  float fVar5;
  float fVar6;
  undefined1 auStack_38 [48];
  
  if (param_2 != 0) {
    if (((*(int *)(param_2 + 0x24) == 7) || (*(int *)(param_2 + 0x24) == 8)) ||
       (*(int *)(param_2 + 0x24) == 9)) {
      if ((param_8 == (longlong *)0x0) || (param_8[0xd] == 0)) goto code_r0x0001814c95a4;
      cVar2 = UnitCtrl::get_IsSummonOrPhantom(param_8[0xd],0);
      if (cVar2 != '\0') {
        return false;
      }
    }
    if (param_8 != (longlong *)0x0) {
      cVar2 = (**(code **)(*param_8 + 0x398))(param_8,*(undefined8 *)(*param_8 + 0x3a0));
      if (cVar2 == '\0') {
        return false;
      }
      puVar4 = (undefined8 *)
               (**(code **)(*param_8 + 0x178))(auStack_38,param_8,*(undefined8 *)(*param_8 + 0x180))
      ;
      fVar6 = (float)*puVar4 / param_5 - *param_3 / param_5;
      (**(code **)(*param_8 + 0x198))(param_8,*(undefined8 *)(*param_8 + 0x1a0));
      fVar5 = extraout_XMM0_Da;
      if (param_4 != '\0') {
        if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814c95a4;
        fVar5 = extraout_XMM0_Da + *(float *)(*(longlong *)(param_1 + 0x90) + 0x118);
      }
      param_6 = param_6 - fVar5 * 0.5;
      param_7 = fVar5 * 0.5 + param_7;
      if (((fVar6 < param_6) || (param_7 < fVar6)) &&
         ((cVar2 = BattleUtil::Approximately(fVar6,param_6,0), cVar2 == '\0' &&
          (((cVar2 = BattleUtil::Approximately(fVar6,param_7,0), cVar2 == '\0' &&
            (*(int *)(param_2 + 0x24) != 3)) && (*(int *)(param_2 + 0x24) != 6)))))) {
        return *(int *)(param_2 + 0x24) == 9;
      }
      return true;
    }
  }
code_r0x0001814c95a4:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar3 = (*pcVar1)();
  return (bool)uVar3;
}



/* ==== UnitActionController::judgeIsTarget  @ 0x14c95b0  size=1 ==== */

bool UnitActionController::judgeIsTarget(longlong param_1,longlong param_2,longlong param_3)

{
  undefined8 uVar1;
  longlong lVar2;
  longlong lVar3;
  code *pcVar4;
  char cVar5;
  bool bVar6;
  undefined1 uVar7;
  longlong lVar8;
  undefined4 uStack_38;
  undefined4 uStack_34;
  undefined4 uStack_30;
  undefined4 uStack_2c;
  undefined8 uStack_28;
  
  if (cRam000000018ab626b9 == '\0') {
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b61160);
    cRam000000018ab626b9 = '\x01';
  }
  if (param_2 == 0) goto code_r0x0001814c9750;
  lVar2 = *(longlong *)(param_2 + 0x38);
  lVar3 = *(longlong *)(param_2 + 0xd0);
  if (lVar2 == 0) goto code_r0x0001814c9750;
  cVar5 = CLKMBEGHOLA::OLICNDIBOOK(lVar2,*(undefined8 *)(param_1 + 0x90),param_3,0);
  if ((cVar5 == '\0') && (*(char *)(lVar2 + 0x260) == '\0')) {
    cVar5 = UnitCtrl::get_IsPhantom(param_2,0);
    if (cVar5 != '\0') {
      if (param_3 == 0) goto code_r0x0001814c9750;
      if (*(int *)(param_3 + 0x14) != 0x17) goto code_r0x0001814c974c;
    }
    if (lVar3 == 0) goto code_r0x0001814c9750;
    if (*(char *)(lVar3 + 0xb9) == '\0') {
      lVar8 = *(longlong *)(param_2 + 0x88);
      if (lVar8 == 0) goto code_r0x0001814c9750;
      uStack_38 = *(undefined4 *)(lVar8 + 0x430);
      uStack_34 = *(undefined4 *)(lVar8 + 0x434);
      uStack_30 = *(undefined4 *)(lVar8 + 0x438);
      uStack_2c = *(undefined4 *)(lVar8 + 0x43c);
      uVar1 = *(undefined8 *)(lVar8 + 0x440);
      if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_28 = uVar1;
      lVar8 = ObscuredLong::FHOALPFLEEC(&uStack_38,0);
      if (lVar8 < 1) goto code_r0x0001814c96d3;
    }
    else {
code_r0x0001814c96d3:
      if (*(char *)(lVar3 + 0xbd) == '\0') goto code_r0x0001814c974c;
    }
    if (param_3 == 0) {
code_r0x0001814c9750:
      sub_8032f9d0();
      pcVar4 = (code *)swi(3);
      uVar7 = (*pcVar4)();
      return (bool)uVar7;
    }
    if (*(int *)(param_3 + 0x10) == 1) {
      uVar1 = *(undefined8 *)(param_1 + 0x90);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar5 = Object::op_Inequality(uVar1,param_2,0);
      if (cVar5 == '\0') goto code_r0x0001814c974c;
      if ((*(int *)(param_3 + 0x10) == 1) && (*(int *)(param_3 + 0x1c) == 1)) {
        cVar5 = CLKMBEGHOLA::BHNFPDMBOAM(lVar2,0x43);
        return cVar5 == '\0';
      }
    }
    bVar6 = true;
  }
  else {
code_r0x0001814c974c:
    bVar6 = false;
  }
  return bVar6;
}



/* ==== UnitActionController::onFirearmHit  @ 0x14c9760  size=1 ==== */

void UnitActionController::onFirearmHit(longlong param_1,longlong param_2)

{
  int iVar1;
  longlong *plVar2;
  longlong lVar3;
  code *pcVar4;
  char cVar5;
  longlong lVar6;
  undefined8 uVar7;
  undefined8 *puVar8;
  ushort uVar9;
  ulonglong uVar10;
  uint uVar11;
  ulonglong uVar12;
  ulonglong uVar13;
  ulonglong in_stack_ffffffffffffffb8;
  ulonglong in_stack_ffffffffffffffc8;
  
  if (cRam000000018ab626b7 == '\0') {
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187c313a0);
    sub_8032f6d0(0x187c19c60);
    sub_8032f6d0(0x187c24e18);
    sub_8032f6d0(0x187c31550);
    sub_8032f6d0(0x187c19e18);
    sub_8032f6d0(0x187c24fc8);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab626b7 = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar5 = Object::op_Equality(param_2,0,0);
  if (cVar5 != '\0') {
    return;
  }
  if (param_2 != 0) {
    if (*(longlong *)(param_2 + 0x108) == 0) {
      return;
    }
    if (*(longlong *)(param_2 + 0x120) == 0) {
      return;
    }
    uVar13 = 0;
    uVar10 = uVar13;
    while (lVar6 = *(longlong *)(param_2 + 0x140), lVar6 != 0) {
      if (*(int *)(lVar6 + 0x18) <= (int)uVar10) {
        if (*(longlong *)(param_2 + 0x118) != 0) {
          iVar1 = *(int *)(*(longlong *)(param_2 + 0x118) + 0x18);
          uVar10 = uVar13;
          if (0 < iVar1) goto code_r0x0001814c9940;
          goto code_r0x0001814c9988;
        }
        break;
      }
      lVar6 = sub_849ed2d0(lVar6,uVar10,uRam0000000187c24fc8);
      if (lVar6 == 0) break;
      if (*(int *)(lVar6 + 0x1c) == 0) {
        if (*(longlong *)(param_2 + 0x140) == 0) break;
        uVar7 = sub_849ed2d0(*(longlong *)(param_2 + 0x140),uVar10,uRam0000000187c24fc8);
        uVar7 = UnitActionController::StartShakeWithDelay(param_1,uVar7,*(undefined8 *)(param_2 + 0x120),0,0);
        if (*(longlong *)(param_2 + 0x120) == 0) break;
        uVar12 = uVar13;
        if (0.0 < *(float *)(*(longlong *)(param_2 + 0x120) + 0xb0)) {
          uVar12 = *(ulonglong *)(param_1 + 0x90);
        }
        if (param_1 == 0) break;
        in_stack_ffffffffffffffb8 = 0;
        UnitActionController::AppendCoroutine(param_1,uVar7,0,uVar12,0);
      }
      uVar10 = (ulonglong)((int)uVar10 + 1);
    }
  }
  goto DAT_1814c9b4d;
  while( true ) {
    uVar7 = sub_849ed2d0(*(longlong *)(param_2 + 0x118),uVar10,uRam0000000187c19e18);
    in_stack_ffffffffffffffc8 = 0;
    in_stack_ffffffffffffffb8 = CONCAT71((int7)(in_stack_ffffffffffffffb8 >> 8),1);
    UnitActionController::ExecUnitActionWithDelay(param_1,uVar7,*(undefined8 *)(param_2 + 0x120),0,in_stack_ffffffffffffffb8,0,0);
    uVar11 = (int)uVar10 + 1;
    uVar10 = (ulonglong)uVar11;
    if (iVar1 <= (int)uVar11) break;
code_r0x0001814c9940:
    if (*(longlong *)(param_2 + 0x118) == 0) goto DAT_1814c9b4d;
  }
code_r0x0001814c9988:
  if (*(longlong *)(param_2 + 0x110) != 0) {
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x110) + 0x18);
    uVar10 = uVar13;
    if (0 < iVar1) {
      do {
        if (*(longlong *)(param_2 + 0x110) == 0) goto DAT_1814c9b4d;
        lVar6 = sub_849ed2d0(*(longlong *)(param_2 + 0x110),uVar10,uRam0000000187c31550);
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        plVar2 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
        if (plVar2 == (longlong *)0x0) goto DAT_1814c9b4d;
        lVar3 = *plVar2;
        uVar9 = 0;
        if (*(ushort *)(lVar3 + 0x12e) != 0) {
          do {
            if (*(longlong *)(*(longlong *)(lVar3 + 0xb0) + (ulonglong)uVar9 * 0x10) ==
                lRam0000000187b93630) {
              puVar8 = (undefined8 *)
                       ((longlong)
                        (*(int *)(*(longlong *)(lVar3 + 0xb0) + 8 + (ulonglong)uVar9 * 0x10) + 0x18)
                        * 0x10 + 0x138 + lVar3);
              goto code_r0x0001814c9a59;
            }
            uVar9 = uVar9 + 1;
          } while (uVar9 < *(ushort *)(lVar3 + 0x12e));
        }
        puVar8 = (undefined8 *)sub_8032c930(plVar2,lRam0000000187b93630,0x18);
code_r0x0001814c9a59:
        cVar5 = (*(code *)*puVar8)(plVar2,puVar8[1]);
        if (cVar5 == '\0') {
code_r0x0001814c9a8b:
          in_stack_ffffffffffffffc8 = in_stack_ffffffffffffffc8 & 0xffffffffffffff00;
          uVar7 = UnitActionController::createNormalPrefabWithDelay(param_1,lVar6,*(undefined8 *)(param_2 + 0x120),0,
                                in_stack_ffffffffffffffb8 & 0xffffffffffffff00,1,
                                in_stack_ffffffffffffffc8,0);
          if (*(longlong *)(param_2 + 0x120) == 0) goto DAT_1814c9b4d;
          uVar12 = uVar13;
          if (0.0 < *(float *)(*(longlong *)(param_2 + 0x120) + 0xb0)) {
            uVar12 = *(ulonglong *)(param_1 + 0x90);
          }
          if (param_1 == 0) goto DAT_1814c9b4d;
          in_stack_ffffffffffffffb8 = 0;
          UnitActionController::AppendCoroutine(param_1,uVar7,0,uVar12,0);
        }
        else {
          if (lVar6 == 0) goto DAT_1814c9b4d;
          if ((*(int *)(lVar6 + 0x50) != 0) && (2 < *(int *)(lVar6 + 0x50) - 2U))
          goto code_r0x0001814c9a8b;
        }
        uVar11 = (int)uVar10 + 1;
        uVar10 = (ulonglong)uVar11;
      } while ((int)uVar11 < iVar1);
    }
    return;
  }
DAT_1814c9b4d:
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitActionController::searchAndSortTarget  @ 0x14c9b60  size=1 ==== */

void UnitActionController::searchAndSortTarget(longlong param_1,longlong param_2,longlong param_3,undefined8 *param_4,
                  undefined1 param_5,undefined1 param_6)

{
  uint uVar1;
  longlong lVar2;
  longlong lVar3;
  code *pcVar4;
  int iVar5;
  bool bVar6;
  char cVar7;
  undefined8 uVar8;
  undefined8 uVar9;
  longlong lVar10;
  undefined4 *puVar11;
  longlong lVar12;
  int iVar13;
  float fVar14;
  float fVar15;
  undefined8 uVar16;
  undefined4 uVar17;
  undefined8 uVar18;
  undefined4 uVar19;
  undefined8 uStack_58;
  undefined4 uStack_50;
  
  if (cRam000000018ab626a3 == '\0') {
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187b7fc80);
    sub_8032f6d0(0x187b7fff8);
    sub_8032f6d0(0x187b80db0);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b81480);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab626a3 = '\x01';
  }
  uStack_58 = *param_4;
  uStack_50 = *(undefined4 *)(param_4 + 1);
  iVar13 = 0;
  uVar18 = 0;
  UnitActionController::searchTargetUnit(param_1,param_3,&uStack_58,param_2,param_6,0);
  if (*(longlong *)(param_1 + 0x90) != 0) {
    uVar8 = UnitCtrl::get_BottomTransform(*(longlong *)(param_1 + 0x90),0);
    uVar16 = 0;
    UnitActionController::sortTargetListByTargetPattern(param_1,param_3,uVar8,param_5,0);
    bVar6 = false;
    if (param_3 != 0) {
      while( true ) {
        uVar17 = (undefined4)((ulonglong)uVar16 >> 0x20);
        uVar19 = (undefined4)((ulonglong)uVar18 >> 0x20);
        if (*(longlong *)(param_3 + 0x50) == 0) break;
        if (*(int *)(*(longlong *)(param_3 + 0x50) + 0x18) - *(int *)(param_3 + 0x18) <= iVar13) {
          if (bVar6) {
            if (*(longlong *)(param_3 + 0x50) != 0) {
              iVar13 = *(int *)(*(longlong *)(param_3 + 0x50) + 0x18);
              iVar5 = iVar13;
              goto code_r0x0001814c9d00;
            }
          }
          else {
            if (*(int *)(param_3 + 0x18) == 0) goto code_r0x0001814c9dff;
            lVar12 = *(longlong *)(param_3 + 0x50);
            if (lVar12 != 0) {
              if (*(int *)(lVar12 + 0x18) == 0) goto code_r0x0001814c9dff;
              uVar18 = sub_849ed2d0(lVar12,*(int *)(lVar12 + 0x18) + -1,uRam0000000187b812d0);
              lVar12 = *(longlong *)(param_3 + 0x50);
              if (lVar12 != 0) {
                iVar13 = *(int *)(lVar12 + 0x18);
                *(int *)(lVar12 + 0x1c) = *(int *)(lVar12 + 0x1c) + 1;
                *(undefined4 *)(lVar12 + 0x18) = 0;
                if (0 < iVar13) {
                  Array::Clear(*(undefined8 *)(lVar12 + 0x10),0,iVar13,0);
                }
                lVar10 = lRam0000000187b7fc80;
                lVar12 = *(longlong *)(param_3 + 0x50);
                if (lVar12 != 0) {
                  *(int *)(lVar12 + 0x1c) = *(int *)(lVar12 + 0x1c) + 1;
                  lVar2 = *(longlong *)(lVar12 + 0x10);
                  uVar1 = *(uint *)(lVar12 + 0x18);
                  if (lVar2 != 0) {
                    if (uVar1 < *(uint *)(lVar2 + 0x18)) {
                      *(uint *)(lVar12 + 0x18) = uVar1 + 1;
                      if (*(uint *)(lVar2 + 0x18) <= uVar1) goto code_r0x0001814ca466;
                      *(undefined8 *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = uVar18;
                      sub_8032e8d0(lVar2 + 0x20 + (longlong)(int)uVar1 * 8,uVar18);
                    }
                    else {
                      sub_84a97470(lVar12,uVar18,
                                    *(undefined8 *)
                                     (*(longlong *)(*(longlong *)(lVar10 + 0x20) + 0xc0) + 0x70));
                    }
                    goto code_r0x0001814c9dff;
                  }
                }
              }
            }
          }
          break;
        }
        uVar8 = *(undefined8 *)(param_3 + 0x50);
        bVar6 = true;
        uVar9 = sub_849ed2d0(uVar8,*(int *)(param_3 + 0x18) + iVar13,uRam0000000187b812d0);
        sub_84aa4620(uVar8,iVar13,uVar9);
        iVar13 = iVar13 + 1;
      }
    }
  }
  goto code_r0x0001814ca460;
code_r0x0001814c9d00:
  uVar17 = (undefined4)((ulonglong)uVar16 >> 0x20);
  uVar19 = (undefined4)((ulonglong)uVar18 >> 0x20);
  iVar5 = iVar5 + -1;
  if (iVar5 < iVar13 - *(int *)(param_3 + 0x18)) goto code_r0x0001814c9dff;
  if (*(longlong *)(param_3 + 0x50) == 0) goto code_r0x0001814ca460;
  sub_84a9dd20(*(longlong *)(param_3 + 0x50),iVar5,uRam0000000187b80db0);
  goto code_r0x0001814c9d00;
code_r0x0001814c9dff:
  lVar12 = 0;
  if (((*(int *)(param_3 + 0x10) != 1) || (*(int *)(param_3 + 0x1c) != 1)) ||
     (*(char *)(param_3 + 0x1a5) != '\0')) goto code_r0x0001814ca426;
  lVar10 = *(longlong *)(param_1 + 0x90);
  if (lVar10 == 0) goto code_r0x0001814ca460;
  lVar2 = *(longlong *)(lVar10 + 0x38);
  if (*(char *)(lVar10 + 0x178) == '\0') {
    if (lVar2 == 0) goto code_r0x0001814ca460;
    cVar7 = CLKMBEGHOLA::GMKFGFKMFCE(lVar2,0);
    lVar12 = 0;
    if (cVar7 == '\0') {
      if (cRam000000018ab626da == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
      goto code_r0x0001814ca460;
      uVar18 = sub_80002270(4,uRam0000000187b93630);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar7 = Object::op_Inequality(uVar18,0,0);
      if (cVar7 != '\0') {
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
code_r0x0001814ca11c:
        if (lVar12 == 0) goto code_r0x0001814ca460;
        uVar18 = 4;
code_r0x0001814ca12a:
        lVar12 = sub_80002270(uVar18,uRam0000000187b93630);
        if (lVar12 == 0) goto code_r0x0001814ca460;
        lVar12 = *(longlong *)(lVar12 + 0x80);
        if (((*(longlong *)(param_1 + 0x90) == 0) ||
            (lVar10 = FixedTransformMonoBehavior::get_transform(*(longlong *)(param_1 + 0x90),0), lVar10 == 0)) ||
           (puVar11 = (undefined4 *)FixedTransform::get_position(&uStack_58,lVar10,0), lVar12 == 0))
        goto code_r0x0001814ca460;
        lVar12 = AIGOPEMKDKL::MCMPDLOCDCI(lVar12,0,*puVar11,0);
      }
    }
    else {
      if (cRam000000018ab626da == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
      goto code_r0x0001814ca460;
      uVar18 = sub_80002270(3,uRam0000000187b93630);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar7 = Object::op_Inequality(uVar18,0,0);
      if (cVar7 != '\0') {
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
        goto code_r0x0001814ca460;
        uVar18 = 3;
        goto code_r0x0001814ca12a;
      }
    }
  }
  else {
    if (lVar2 == 0) goto code_r0x0001814ca460;
    cVar7 = CLKMBEGHOLA::GMKFGFKMFCE(lVar2,0);
    if (cVar7 == '\0') {
      if (cRam000000018ab626da == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
      goto code_r0x0001814ca460;
      uVar18 = sub_80002270(3,uRam0000000187b93630);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar7 = Object::op_Inequality(uVar18,0,0);
      if (cVar7 != '\0') {
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
        goto code_r0x0001814ca460;
        uVar18 = 3;
        goto code_r0x0001814ca12a;
      }
    }
    else {
      if (cRam000000018ab626da == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
      goto code_r0x0001814ca460;
      uVar18 = sub_80002270(4,uRam0000000187b93630);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar7 = Object::op_Inequality(uVar18,0,0);
      if (cVar7 != '\0') {
        if (cRam000000018ab626da == '\0') {
          sub_8032f6d0(0x187bfaba0);
          cRam000000018ab626da = '\x01';
        }
        lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
        goto code_r0x0001814ca11c;
      }
    }
  }
  if ((*(longlong *)(param_1 + 0x90) != 0) &&
     (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar10 != 0)) {
    cVar7 = CLKMBEGHOLA::IEEAGJAAHJP(lVar10,0);
    if (cVar7 != '\0') {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar10 == 0))
      goto code_r0x0001814ca460;
      cVar7 = CLKMBEGHOLA::GMKFGFKMFCE(lVar10,0);
      if (cVar7 == '\0') {
        if ((*(longlong *)(param_1 + 0x90) == 0) ||
           (lVar12 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar12 == 0))
        goto code_r0x0001814ca460;
        lVar12 = *(longlong *)(lVar12 + 0x180);
      }
    }
    if (lVar12 == 0) {
code_r0x0001814ca426:
      *(undefined1 *)(param_3 + 0x1a4) = 1;
      return;
    }
    lVar10 = *(longlong *)(lVar12 + 0x68);
    if ((lVar10 != 0) && (*(longlong *)(lVar10 + 0xd0) != 0)) {
      if (*(char *)(*(longlong *)(lVar10 + 0xd0) + 0xb9) != '\0') goto code_r0x0001814ca426;
      if (*(longlong *)(lVar10 + 0x38) != 0) {
        cVar7 = CLKMBEGHOLA::BGPIDFLHLGC(*(longlong *)(lVar10 + 0x38),*(undefined8 *)(param_1 + 0x90),0);
        if (cVar7 != '\0') goto code_r0x0001814ca426;
        lVar10 = *(longlong *)(param_1 + 0x90);
        switch(*(undefined4 *)(param_3 + 0x24)) {
        case 1:
        case 4:
        case 7:
          if (lVar10 == 0) goto code_r0x0001814ca460;
          iVar13 = 1;
          if (*(char *)(lVar10 + 0x165) != '\0') {
            iVar13 = -1;
          }
          break;
        default:
          iVar13 = 0;
        }
        fVar15 = *(float *)(param_3 + 0x20);
        if (fVar15 <= 0.0) {
          if (param_2 == 0) goto code_r0x0001814ca460;
          if (*(int *)(param_2 + 0x78) == 1) {
            if ((lVar10 == 0) || (*(longlong *)(lVar10 + 0xb8) == 0)) goto code_r0x0001814ca460;
            fVar15 = *(float *)(*(longlong *)(lVar10 + 0xb8) + 0x18);
          }
          else {
            fVar15 = *(float *)(param_2 + 0x7c);
          }
        }
        if (iVar13 == 1) {
          fVar14 = 0.0;
        }
        else {
          fVar14 = -fVar15;
          if (iVar13 == -1) {
            fVar15 = 0.0;
          }
        }
        if (((*(longlong *)(param_1 + 0x20) != 0) &&
            (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x18), lVar10 != 0)) &&
           (lVar10 = sub_86215a70(lVar10,0), lVar10 != 0)) {
          puVar11 = (undefined4 *)Transform::get_lossyScale(&uStack_58,lVar10,0);
          uStack_58 = *param_4;
          uStack_50 = *(undefined4 *)(param_4 + 1);
          cVar7 = UnitActionController::judgeIsInTargetArea(param_1,param_3,&uStack_58,param_6,CONCAT44(uVar17,*puVar11),
                                CONCAT44(uVar19,fVar14),fVar15,lVar12,0);
          if (cVar7 == '\0') goto code_r0x0001814ca426;
          lVar10 = *(longlong *)(param_3 + 0x50);
          if (lVar10 != 0) {
            iVar13 = *(int *)(lVar10 + 0x18);
            *(int *)(lVar10 + 0x1c) = *(int *)(lVar10 + 0x1c) + 1;
            *(undefined4 *)(lVar10 + 0x18) = 0;
            if (0 < iVar13) {
              Array::Clear(*(undefined8 *)(lVar10 + 0x10),0,iVar13,0);
            }
            lVar2 = lRam0000000187b7fc80;
            lVar10 = *(longlong *)(param_3 + 0x50);
            if (lVar10 != 0) {
              *(int *)(lVar10 + 0x1c) = *(int *)(lVar10 + 0x1c) + 1;
              lVar3 = *(longlong *)(lVar10 + 0x10);
              uVar1 = *(uint *)(lVar10 + 0x18);
              if (lVar3 != 0) {
                if (uVar1 < *(uint *)(lVar3 + 0x18)) {
                  *(uint *)(lVar10 + 0x18) = uVar1 + 1;
                  if (*(uint *)(lVar3 + 0x18) <= uVar1) {
code_r0x0001814ca466:
                    sub_8032f9c0();
                    pcVar4 = (code *)swi(3);
                    (*pcVar4)();
                    return;
                  }
                  *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar12;
                  sub_8032e8d0(lVar3 + 0x20 + (longlong)(int)uVar1 * 8,lVar12);
                }
                else {
                  sub_84a97470(lVar10,lVar12,
                                *(undefined8 *)
                                 (*(longlong *)(*(longlong *)(lVar2 + 0x20) + 0xc0) + 0x70));
                }
                goto code_r0x0001814ca426;
              }
            }
          }
        }
      }
    }
  }
code_r0x0001814ca460:
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitActionController::searchTargetUnit  @ 0x14ca490  size=1 ==== */

void UnitActionController::searchTargetUnit(longlong param_1,longlong param_2,undefined8 *param_3,longlong param_4,
                  undefined1 param_5)

{
  undefined4 uVar1;
  code *pcVar2;
  undefined8 uVar3;
  char cVar4;
  char cVar5;
  uint uVar6;
  longlong lVar7;
  undefined4 *puVar8;
  longlong lVar9;
  longlong lVar10;
  undefined8 uVar11;
  longlong *plVar12;
  undefined8 *puVar13;
  float *pfVar14;
  longlong *plVar15;
  undefined8 uVar16;
  int iVar17;
  int iVar18;
  int iVar19;
  longlong lVar20;
  bool bVar21;
  float fVar22;
  float fVar23;
  float fVar24;
  longlong lStack_168;
  int iStack_160;
  undefined8 uStack_158;
  undefined4 uStack_150;
  longlong lStack_148;
  undefined8 uStack_138;
  undefined4 uStack_130;
  undefined8 uStack_128;
  undefined8 uStack_118;
  undefined8 uStack_108;
  undefined8 uStack_100;
  undefined8 uStack_f8;
  undefined1 auStack_e8 [16];
  undefined1 auStack_d8 [16];
  undefined1 auStack_c8 [16];
  undefined1 auStack_b8 [128];
  
  if (cRam000000018ab626b8 == '\0') {
    sub_8032f6d0(0x187b33478);
    sub_8032f6d0(0x187bbdcb8);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187b71ad8);
    sub_8032f6d0(0x187b7fc80);
    sub_8032f6d0(0x187b7fff8);
    sub_8032f6d0(0x187b72388);
    sub_8032f6d0(0x187b71760);
    sub_8032f6d0(0x187b9ead8);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b73698);
    sub_8032f6d0(0x187b9ec90);
    sub_8032f6d0(0x187b73858);
    sub_8032f6d0(0x187c32990);
    sub_8032f6d0(0x187b61160);
    sub_8032f6d0(0x187b97c88);
    sub_8032f6d0(0x187bd0488);
    sub_8032f6d0(0x187bd0640);
    sub_8032f6d0(0x187bd07f8);
    sub_8032f6d0(0x187bd09b0);
    sub_8032f6d0(0x187bd0b68);
    sub_8032f6d0(0x187bd1770);
    sub_8032f6d0(0x187c1ffa8);
    sub_8032f6d0(0x187bd1928);
    sub_8032f6d0(0x187c20158);
    sub_8032f6d0(0x187bd1ae0);
    sub_8032f6d0(0x187c20310);
    sub_8032f6d0(0x187bd1c98);
    sub_8032f6d0(0x187c204c0);
    sub_8032f6d0(0x187c1f720);
    sub_8032f6d0(0x187b2fb58);
    sub_8032f6d0(0x187b2fd10);
    cRam000000018ab626b8 = '\x01';
  }
  if (((*(longlong *)(param_1 + 0x20) == 0) ||
      (lVar7 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x18), lVar7 == 0)) ||
     (lVar7 = sub_86215a70(lVar7,0), lVar7 == 0)) goto code_r0x0001814cb764;
  puVar8 = (undefined4 *)Transform::get_lossyScale(&uStack_158,lVar7,0);
  uVar1 = *puVar8;
  if ((param_2 == 0) || (lVar7 = *(longlong *)(param_2 + 0x50), lVar7 == 0))
  goto code_r0x0001814cb764;
  iVar17 = *(int *)(lVar7 + 0x18);
  *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
  *(undefined4 *)(lVar7 + 0x18) = 0;
  if (0 < iVar17) {
    Array::Clear(*(undefined8 *)(lVar7 + 0x10),0,iVar17,0);
  }
  lVar7 = *(longlong *)(param_1 + 0x90);
  if (lVar7 == 0) goto code_r0x0001814cb764;
  lVar20 = *(longlong *)(lVar7 + 0x38);
  cVar5 = *(char *)(lVar7 + 0x178);
  lStack_148 = lVar20;
  if (lVar20 == 0) goto code_r0x0001814cb764;
  cVar4 = CLKMBEGHOLA::GMKFGFKMFCE(lVar20,0);
  if (cVar4 == '\0') {
    if (cVar5 != '\0') {
      if (cRam000000018ab626da == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
      goto code_r0x0001814cb764;
      uVar16 = 5;
      goto code_r0x0001814ca8a3;
    }
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
    goto code_r0x0001814cb764;
    uVar16 = 6;
code_r0x0001814ca817:
    lVar7 = sub_80002270(uVar16,uRam0000000187b93630);
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
    goto code_r0x0001814cb764;
    uVar16 = 5;
  }
  else {
    if (cVar5 == '\0') {
      if (cRam000000018ab626da == '\0') {
        sub_8032f6d0(0x187bfaba0);
        cRam000000018ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
      goto code_r0x0001814cb764;
      uVar16 = 5;
      goto code_r0x0001814ca817;
    }
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
    goto code_r0x0001814cb764;
    uVar16 = 6;
code_r0x0001814ca8a3:
    lVar7 = sub_80002270(uVar16,uRam0000000187b93630);
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
    goto code_r0x0001814cb764;
    uVar16 = 6;
  }
  lVar9 = sub_80002270(uVar16,uRam0000000187b93630);
  iVar17 = *(int *)(param_2 + 0x10);
  lVar10 = lVar9;
  if (((iVar17 != 0) && (lVar10 = lVar7, iVar17 != 1)) &&
     ((lVar10 = lVar9, iVar17 != 2 && (lVar10 = 0, iVar17 == 3)))) {
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0)
    goto code_r0x0001814cb764;
    uVar16 = sub_80002270(5,uRam0000000187b93630);
    lVar10 = sub_8032f970(uRam0000000187c32990);
    sub_84aa4390(lVar10,uVar16,uRam0000000187b71760);
    if (cRam000000018ab626da == '\0') {
      sub_8032f6d0(0x187bfaba0);
      cRam000000018ab626da = '\x01';
    }
    if ((*(longlong *)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8) == 0) ||
       (uVar16 = sub_80002270(6,uRam0000000187b93630), lVar10 == 0)) goto code_r0x0001814cb764;
    sub_84a97390(lVar10,uVar16,uRam0000000187b71ad8);
  }
  iVar17 = *(int *)(param_2 + 0x14);
  if ((999 < iVar17) && (iVar17 = (iVar17 / 1000) * 1000, 11000 < iVar17)) {
    if (iVar17 < 0x32c9) {
      if (iVar17 == 12000) {
code_r0x0001814cad1e:
        lVar7 = sub_8032f970(uRam0000000187c1ffa8);
        sub_804383d0(lVar7,0);
        if (lVar7 == 0) goto code_r0x0001814cb764;
        *(int *)(lVar7 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        lVar9 = sub_8032f970(uRam0000000187b97c88);
        uVar16 = uRam0000000187bd1770;
        goto code_r0x0001814caaa8;
      }
      if (iVar17 == 13000) {
        lVar7 = sub_8032f970(uRam0000000187c20158);
        sub_804383d0(lVar7,0);
        if (lVar7 == 0) goto code_r0x0001814cb764;
        *(int *)(lVar7 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        uVar16 = sub_8032f970(uRam0000000187b97c88);
        sub_84cacb40(uVar16,lVar7,uRam0000000187bd1928,0);
        if (lVar10 == 0) goto code_r0x0001814cb764;
        lVar10 = sub_84a990e0(lVar10,uVar16,uRam0000000187b72388);
      }
    }
    else {
      if (iVar17 == 14000) {
        lVar7 = sub_8032f970(uRam0000000187c20310);
        sub_804383d0(lVar7,0);
        if (lVar7 == 0) goto code_r0x0001814cb764;
        *(int *)(lVar7 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        lVar9 = sub_8032f970(uRam0000000187b97c88);
        uVar16 = uRam0000000187bd1ae0;
code_r0x0001814caaa8:
        sub_84cacb40(lVar9,lVar7,uVar16,0);
        goto joined_r0x0001814cb061;
      }
      if (iVar17 == 15000) {
        lVar7 = sub_8032f970(uRam0000000187c204c0);
        sub_804383d0(lVar7,0);
        if (lVar7 == 0) goto code_r0x0001814cb764;
        *(longlong *)(lVar7 + 0x18) = param_1;
        sub_8032e8d0(lVar7 + 0x18,param_1);
        *(int *)(lVar7 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        uVar16 = sub_8032f970(uRam0000000187b97c88);
        sub_84cacb40(uVar16,lVar7,uRam0000000187bd1c98,0);
        if (lVar10 == 0) goto code_r0x0001814cb764;
        lVar10 = sub_84a990e0(lVar10,uVar16,uRam0000000187b72388);
      }
    }
    goto code_r0x0001814cb0bd;
  }
  if (0x22 < iVar17) {
    switch(iVar17) {
    case 0x29:
    case 0x2b:
    case 0x2c:
    case 0x32:
code_r0x0001814cb074:
      uVar11 = sub_8032f970(uRam0000000187b97c88);
      uVar16 = uRam0000000187b2fb58;
      goto code_r0x0001814cb087;
    case 0x2a:
    case 0x2d:
    case 0x2e:
    case 0x30:
    case 0x31:
      break;
    case 0x2f:
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70();
      }
      lVar9 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x78);
      if (lVar9 == 0) {
        if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uVar16 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
        lVar9 = sub_8032f970(uRam0000000187b97c88);
        sub_84cacb40(lVar9,uVar16,uRam0000000187bd0b68,0);
        *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x78) = lVar9;
        sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x78,lVar9);
      }
      goto joined_r0x0001814cb061;
    case 0x33:
      uVar11 = sub_8032f970(uRam0000000187b97c88);
      uVar16 = uRam0000000187b2fd10;
code_r0x0001814cb087:
      sub_84cacb40(uVar11,param_1,uVar16,0);
      if (lVar10 == 0) goto code_r0x0001814cb764;
      lVar10 = sub_84a990e0(lVar10,uVar11,uRam0000000187b72388);
      break;
    default:
      if (iVar17 == 11000) goto code_r0x0001814cad1e;
    }
    goto code_r0x0001814cb0bd;
  }
  switch(iVar17) {
  case 0x12:
  case 0x16:
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar9 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x58);
    if (lVar9 == 0) {
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar16 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
      lVar9 = sub_8032f970(uRam0000000187b97c88);
      sub_84cacb40(lVar9,uVar16,uRam0000000187bd0488,0);
      *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x58) = lVar9;
      sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x58,lVar9);
    }
    break;
  case 0x13:
    goto code_r0x0001814cb0bd;
  case 0x14:
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar9 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x70);
    if (lVar9 == 0) {
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar16 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
      lVar9 = sub_8032f970(uRam0000000187b97c88);
      sub_84cacb40(lVar9,uVar16,uRam0000000187bd09b0,0);
      *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x70) = lVar9;
      sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x70,lVar9);
    }
    break;
  case 0x15:
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lVar9 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x68);
    if (lVar9 == 0) {
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar16 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
      lVar9 = sub_8032f970(uRam0000000187b97c88);
      sub_84cacb40(lVar9,uVar16,uRam0000000187bd07f8,0);
      *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x68) = lVar9;
      sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x68,lVar9);
    }
    break;
  case 0x17:
    if ((*(longlong *)(param_1 + 0x90) == 0) ||
       (lVar7 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x1b0), lVar7 == 0))
    goto code_r0x0001814cb764;
    uVar16 = sub_855377d0(lVar7,uRam0000000187b33478);
    lVar10 = sub_847b2640(uVar16,uRam0000000187bbdcb8);
    if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
      sub_8032fa70(lRam0000000187c1f720);
    }
    lVar9 = *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x60);
    if (lVar9 == 0) {
      if (*(int *)(lRam0000000187c1f720 + 0xe4) == 0) {
        sub_8032fa70(lRam0000000187c1f720);
      }
      uVar16 = **(undefined8 **)(lRam0000000187c1f720 + 0xb8);
      lVar9 = sub_8032f970(uRam0000000187b97c88);
      sub_84cacb40(lVar9,uVar16,uRam0000000187bd0640,0);
      *(longlong *)(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x60) = lVar9;
      sub_8032e8d0(*(longlong *)(lRam0000000187c1f720 + 0xb8) + 0x60,lVar9);
    }
    break;
  default:
    if (iVar17 == 0x22) goto code_r0x0001814cb074;
    goto code_r0x0001814cb0bd;
  }
joined_r0x0001814cb061:
  if (lVar10 != 0) {
    lVar10 = sub_84a990e0(lVar10,lVar9,uRam0000000187b72388);
code_r0x0001814cb0bd:
    fVar24 = *(float *)(param_2 + 0x20);
    if ((*(int *)(param_2 + 0x14) == 10) || (*(int *)(param_2 + 0x14) == 9)) {
      iVar17 = 0;
      if (lVar10 != 0) {
        iVar18 = *(int *)(lVar10 + 0x18);
        if (0 < iVar18) {
          do {
            lVar7 = sub_849ed2d0(lVar10,iVar17,uRam0000000187b73858);
            cVar5 = UnitActionController::judgeIsTarget(param_1,lVar7,param_2,0);
            if (cVar5 != '\0') {
              if (lVar7 == 0) goto code_r0x0001814cb764;
              lVar20 = *(longlong *)(lVar7 + 0x80);
              cVar5 = UnitCtrl::get_IsPartsBoss(lVar7,0);
              if (cVar5 == '\0') {
                lVar7 = *(longlong *)(param_2 + 0x50);
                if ((lVar20 == 0) || (uVar16 = AIGOPEMKDKL::MCMPDLOCDCI(lVar20,0,0), lVar7 == 0))
                goto code_r0x0001814cb764;
                sub_80002460(lVar7,uVar16,uRam0000000187b7fc80);
              }
              else {
                if (lVar20 == 0) goto code_r0x0001814cb764;
                lVar7 = *(longlong *)(lVar20 + 0x18);
                iVar19 = 0;
                if (lVar7 == 0) goto code_r0x0001814cb764;
                for (; iVar19 < *(int *)(lVar7 + 0x18); iVar19 = iVar19 + 1) {
                  plVar15 = (longlong *)sub_849ed2d0(lVar7,iVar19,uRam0000000187b9ec90);
                  if (plVar15 == (longlong *)0x0) goto code_r0x0001814cb764;
                  cVar5 = (**(code **)(*plVar15 + 0x398))(plVar15,*(undefined8 *)(*plVar15 + 0x3a0))
                  ;
                  if (cVar5 != '\0') {
                    if (*(longlong *)(param_2 + 0x50) == 0) goto code_r0x0001814cb764;
                    sub_80002460(*(longlong *)(param_2 + 0x50),plVar15,uRam0000000187b7fc80);
                  }
                }
              }
            }
            iVar17 = iVar17 + 1;
          } while (iVar17 < iVar18);
        }
        UnitActionController::filterTargetByFlight(param_1,param_2,0);
        return;
      }
    }
    else {
      lVar7 = *(longlong *)(param_1 + 0x90);
      switch(*(undefined4 *)(param_2 + 0x24)) {
      case 1:
      case 4:
      case 7:
        if (lVar7 == 0) goto code_r0x0001814cb764;
        uVar6 = 1;
        if (*(char *)(lVar7 + 0x165) != '\0') {
          uVar6 = 0xffffffff;
        }
        break;
      default:
        uVar6 = 0;
      }
      if (fVar24 <= 0.0) {
        if (param_4 == 0) goto code_r0x0001814cb764;
        if (*(int *)(param_4 + 0x78) == 1) {
          if ((lVar7 == 0) || (*(longlong *)(lVar7 + 0xb8) == 0)) goto code_r0x0001814cb764;
          fVar24 = *(float *)(*(longlong *)(lVar7 + 0xb8) + 0x18);
        }
        else {
          fVar24 = *(float *)(param_4 + 0x7c);
        }
      }
      if ((int)uVar6 < 1) {
        fVar23 = -fVar24;
        if (0x7fffffff < uVar6) {
          fVar24 = 0.0;
        }
      }
      else {
        fVar23 = 0.0;
      }
      lStack_168 = 0;
      iStack_160 = 0;
      if (lVar10 != 0) {
        iVar17 = *(int *)(lVar10 + 0x18);
        lVar7 = 0;
        if (0 < iVar17) {
code_r0x0001814cb1b0:
          lVar7 = sub_849ed2d0(lVar10,iStack_160,uRam0000000187b73858);
          if (lVar7 != 0) {
            lVar20 = *(longlong *)(lVar7 + 0x80);
            cVar5 = UnitCtrl::get_IsPartsBoss(lVar7,0);
            if (cVar5 == '\0') {
              if (lVar20 != 0) {
                uVar16 = AIGOPEMKDKL::MCMPDLOCDCI(lVar20,0,0,0);
                uStack_150 = *(undefined4 *)(param_3 + 1);
                uStack_158 = *param_3;
                cVar5 = UnitActionController::judgeIsInTargetArea(param_1,param_2,&uStack_158,param_5,uVar1,fVar23,fVar24,uVar16
                                      ,0);
                if (cVar5 == '\0') goto code_r0x0001814cb56a;
                cVar5 = UnitActionController::judgeIsTarget(param_1,lVar7,param_2);
                if (cVar5 == '\0') {
                  lVar7 = *(longlong *)(lVar7 + 0x88);
                  if (lVar7 != 0) {
                    uVar11 = *(undefined8 *)(lVar7 + 0x430);
                    uVar3 = *(undefined8 *)(lVar7 + 0x438);
                    uVar16 = *(undefined8 *)(lVar7 + 0x440);
                    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
                      sub_8032fa70();
                    }
                    uStack_108 = uVar11;
                    uStack_100 = uVar3;
                    uStack_f8 = uVar16;
                    lVar7 = ObscuredLong::FHOALPFLEEC(&uStack_108,0);
                    if (lVar7 == 0) {
                      lStack_168 = AIGOPEMKDKL::MCMPDLOCDCI(lVar20,0,0,0);
                    }
                    goto code_r0x0001814cb56a;
                  }
                }
                else {
                  lVar7 = *(longlong *)(param_2 + 0x50);
                  plVar15 = (longlong *)AIGOPEMKDKL::MCMPDLOCDCI(lVar20,0,0,0);
                  if (lVar7 != 0) goto code_r0x0001814cb551;
                }
              }
            }
            else {
              if ((((*(int *)(param_2 + 0x14) == 5) || (*(int *)(param_2 + 0x14) == 6)) ||
                  (*(int *)(param_2 + 0x14) == 0xd)) || (*(int *)(param_2 + 0x14) == 0xc)) {
                bVar21 = true;
              }
              else {
                bVar21 = *(int *)(param_2 + 0x14) == 0x13;
              }
              if (lVar20 != 0) {
                lVar20 = *(longlong *)(lVar20 + 0x18);
                plVar15 = (longlong *)0x0;
                iVar18 = 0;
                if (lVar20 != 0) {
code_r0x0001814cb23d:
                  do {
                    if (*(int *)(lVar20 + 0x18) <= iVar18) goto code_r0x0001814cb417;
                    plVar12 = (longlong *)sub_849ed2d0(lVar20,iVar18,uRam0000000187b9ec90);
                    uStack_130 = *(undefined4 *)(param_3 + 1);
                    uStack_138 = *param_3;
                    cVar5 = UnitActionController::judgeIsInTargetArea(param_1,param_2,&uStack_138,param_5,uVar1,fVar23,fVar24,
                                          plVar12,0);
                    if ((cVar5 != '\0') && (cVar5 = UnitActionController::judgeIsTarget(param_1,lVar7), cVar5 != '\0')) {
                      if (!bVar21) {
                        if (*(longlong *)(param_2 + 0x50) == 0) break;
                        sub_80002460(*(longlong *)(param_2 + 0x50),plVar12);
                        iVar18 = iVar18 + 1;
                        goto code_r0x0001814cb23d;
                      }
                      if (plVar15 != (longlong *)0x0) {
                        puVar13 = (undefined8 *)
                                  (**(code **)(*plVar15 + 0x178))
                                            (auStack_e8,plVar15,*(undefined8 *)(*plVar15 + 0x180));
                        uStack_128 = *puVar13;
                        if ((*(longlong *)(param_1 + 0x90) == 0) ||
                           (lVar9 = FixedTransformMonoBehavior::get_transform(*(longlong *)(param_1 + 0x90),0), lVar9 == 0))
                        break;
                        pfVar14 = (float *)FixedTransform::get_position(auStack_d8,lVar9,0);
                        fVar22 = (float)uStack_128 - *pfVar14;
                        if (plVar12 == (longlong *)0x0) break;
                        puVar13 = (undefined8 *)(**(code **)(*plVar12 + 0x178))(auStack_c8,plVar12);
                        uStack_118 = *puVar13;
                        if ((*(longlong *)(param_1 + 0x90) == 0) ||
                           (lVar9 = FixedTransformMonoBehavior::get_transform(*(longlong *)(param_1 + 0x90),0), lVar9 == 0))
                        break;
                        pfVar14 = (float *)FixedTransform::get_position(auStack_b8,lVar9);
                        if ((ABS(fVar22) <= ABS((float)uStack_118 - *pfVar14)) &&
                           (cVar5 = BattleUtil::Approximately(ABS(fVar22)), cVar5 == '\0'))
                        goto code_r0x0001814cb410;
                      }
                      plVar15 = plVar12;
                    }
code_r0x0001814cb410:
                    iVar18 = iVar18 + 1;
                  } while( true );
                }
              }
            }
          }
          goto code_r0x0001814cb764;
        }
code_r0x0001814cb583:
        UnitActionController::filterTargetByFlight(param_1,param_2,0);
        if (*(int *)(param_2 + 0x14) == 3) {
          if (*(longlong *)(param_2 + 0x50) == 0) goto code_r0x0001814cb764;
          if (((*(int *)(*(longlong *)(param_2 + 0x50) + 0x18) == 0) && (lVar7 != 0)) &&
             (cVar5 = CLKMBEGHOLA::GMKFGFKMFCE(lVar20,0), cVar5 == '\0')) {
            if (*(longlong *)(param_2 + 0x50) == 0) goto code_r0x0001814cb764;
            sub_80002460(*(longlong *)(param_2 + 0x50),lVar7,uRam0000000187b7fc80);
          }
        }
        return;
      }
    }
  }
code_r0x0001814cb764:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
code_r0x0001814cb417:
  if (plVar15 != (longlong *)0x0) {
    lVar7 = *(longlong *)(param_2 + 0x50);
    if (lVar7 == 0) goto code_r0x0001814cb764;
code_r0x0001814cb551:
    sub_80002460(lVar7,plVar15,uRam0000000187b7fc80);
  }
code_r0x0001814cb56a:
  iStack_160 = iStack_160 + 1;
  lVar7 = lStack_168;
  lVar20 = lStack_148;
  if (iVar17 <= iStack_160) goto code_r0x0001814cb583;
  goto code_r0x0001814cb1b0;
}



/* ==== UnitActionController::setCutInSkipTimeForPrincessForm  @ 0x14cb7d0  size=1 ==== */

void UnitActionController::setCutInSkipTimeForPrincessForm(longlong param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626a4 == '\0') {
    sub_8032f6d0(0x187c421b8);
    cRam000000018ab626a4 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar2 = sub_855bdd60(*(longlong *)(param_1 + 0xb0),param_2,uRam0000000187c421b8);
    if (lVar2 != 0) {
      if (*(char *)(lVar2 + 0x10) != '\0') {
        *(undefined4 *)(lVar2 + 0xd0) = 0;
      }
      return;
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitActionController::setSkillParameter  @ 0x14cb840  size=1 ==== */

/* WARNING: Removing unreachable block (ram,0x0001814cc3e3) */

void UnitActionController::setSkillParameter(longlong param_1,longlong *param_2,longlong param_3,int param_4)

{
  longlong lVar1;
  code *pcVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  char cVar5;
  undefined2 uVar6;
  int iVar7;
  undefined4 uVar8;
  longlong lVar9;
  undefined8 uVar10;
  longlong lVar11;
  undefined8 *puVar12;
  longlong *plVar13;
  longlong lVar14;
  undefined8 uVar15;
  ushort uVar16;
  float fVar17;
  double dVar18;
  undefined4 auStack_d8 [4];
  longlong lStack_c8;
  undefined4 uStack_c0;
  undefined4 uStack_b8;
  undefined2 uStack_b4;
  longlong *plStack_b0;
  undefined8 uStack_a8;
  undefined8 *puStack_a0;
  longlong lStack_98;
  undefined8 uStack_90;
  longlong lStack_88;
  undefined8 uStack_80;
  undefined8 *puStack_78;
  longlong lStack_70;
  undefined8 uStack_68;
  
  plStack_b0 = param_2;
  if (cRam000000018ab62698 == '\0') {
    sub_8032f6d0(0x187c32f48);
    sub_8032f6d0(0x187b42130);
    sub_8032f6d0(0x187b44ea0);
    sub_8032f6d0(0x187b41390);
    sub_8032f6d0(0x187c1cf68);
    sub_8032f6d0(0x187ba82c8);
    sub_8032f6d0(0x187b4a7c8);
    sub_8032f6d0(0x187b4a978);
    sub_8032f6d0(0x187b4ab30);
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187bcb0c0);
    sub_8032f6d0(0x187b64318);
    sub_8032f6d0(0x187b8ae18);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187b861a8);
    sub_8032f6d0(0x187c19590);
    sub_8032f6d0(0x187ba38a0);
    sub_8032f6d0(0x187c30cd0);
    sub_8032f6d0(0x187c31950);
    sub_8032f6d0(0x187c30e80);
    sub_8032f6d0(0x187c31b08);
    sub_8032f6d0(0x187ba3a58);
    sub_8032f6d0(0x187be2298);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b60c28);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b61d80);
    sub_8032f6d0(0x187b77e20);
    sub_8032f6d0(0x187b778e8);
    sub_8032f6d0(0x187bd2a58);
    sub_8032f6d0(0x187bd2c10);
    sub_8032f6d0(0x187c20b90);
    cRam000000018ab62698 = '\x01';
  }
  auStack_d8[0] = 0;
  uStack_b8 = 0;
  uStack_b4 = 0;
  uStack_80 = 0;
  puStack_78 = (undefined8 *)0x0;
  lStack_70 = 0;
  lVar9 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar9 + 0x135) & 1) == 0) {
    lVar9 = sub_8032d010(lVar9);
  }
  lVar9 = *(longlong *)(*(longlong *)(lVar9 + 0xc0) + 0x10);
  if ((*(byte *)(lVar9 + 0x135) & 1) == 0) {
    lVar9 = sub_8032d010(lVar9);
  }
  lVar9 = **(longlong **)(lVar9 + 0xb8);
  lStack_88 = lVar9;
  uVar10 = sub_8032f970(uRam0000000187b64318);
  sub_804383d0(uVar10,0);
  uStack_68 = sub_8032f970(uRam0000000187ba82c8);
  sub_855de480(uStack_68,uVar10,uRam0000000187b44ea0);
  lVar11 = sub_8032f970(uRam0000000187be2298);
  sub_849de5b0(lVar11,uRam0000000187c19590);
  if (param_2 != (longlong *)0x0) {
    param_2[4] = lVar11;
    sub_8032e8d0(param_2 + 4,lVar11);
    if (param_3 != 0) {
      iVar7 = OLNDAJKNIAG::OMCBOPBAHMH(param_3,0);
      if (*(longlong *)(param_1 + 0x90) != 0) {
        if (iVar7 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
          auStack_d8[0] = 1;
          if (*(int *)(lRam0000000187b77e20 + 0xe4) == 0) {
            sub_8032fa70(lRam0000000187b77e20);
          }
          lVar14 = *(longlong *)(*(longlong *)(lRam0000000187b77e20 + 0xb8) + 0x38);
          lVar11 = *(longlong *)(param_3 + 0x2a0);
          uVar8 = *(undefined4 *)(param_3 + 0x2a8);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          lStack_c8 = lVar11;
          uStack_c0 = uVar8;
          uVar8 = ObscuredInt::FHOALPFLEEC(&lStack_c8,0);
          if (lVar14 == 0) goto code_r0x0001814cc67a;
          cVar5 = sub_855a6fd0(lVar14,uVar8,auStack_d8,uRam0000000187b42130);
          if (cVar5 == '\0') {
            auStack_d8[0] = 1;
          }
          if ((*(longlong *)(param_1 + 0x90) == 0) ||
             (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 200), lVar11 == 0))
          goto code_r0x0001814cc67a;
          sub_804bd5e0(lVar11,auStack_d8[0],0);
        }
        if (param_4 == -1) {
          param_4 = OLNDAJKNIAG::OMCBOPBAHMH(param_3,0);
        }
        *(int *)(param_2 + 0xf) = param_4;
        lVar11 = *(longlong *)(param_3 + 0x68);
        uVar8 = *(undefined4 *)(param_3 + 0x70);
        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
          sub_8032fa70();
        }
        lStack_c8 = lVar11;
        uStack_c0 = uVar8;
        iVar7 = ObscuredInt::FHOALPFLEEC(&lStack_c8,0);
        if (iVar7 == 0) {
          if ((*(longlong *)(param_1 + 0x90) == 0) ||
             (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar11 == 0))
          goto code_r0x0001814cc67a;
          fVar17 = *(float *)(lVar11 + 0x18);
        }
        else {
          lVar11 = *(longlong *)(param_3 + 0x68);
          uVar8 = *(undefined4 *)(param_3 + 0x70);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          lStack_c8 = lVar11;
          uStack_c0 = uVar8;
          iVar7 = ObscuredInt::FHOALPFLEEC(&lStack_c8,0);
          fVar17 = (float)iVar7;
        }
        *(float *)((longlong)param_2 + 0x7c) = fVar17;
        if (lVar9 != 0) {
          lStack_c8 = *(longlong *)(lVar9 + 0x1cb0);
          iVar7 = 0;
          while (lVar9 = *(longlong *)(param_3 + 0x28), lVar9 != 0) {
            if (*(int *)(lVar9 + 0x18) <= iVar7) {
              uVar10 = *(undefined8 *)(param_3 + 0x78);
              uVar15 = *(undefined8 *)(param_3 + 0x80);
              uVar3 = *(undefined8 *)(param_3 + 0x88);
              uVar4 = *(undefined8 *)(param_3 + 0x90);
              if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uStack_a8 = uVar10;
              puStack_a0 = (undefined8 *)uVar15;
              lStack_98 = uVar3;
              uStack_90 = uVar4;
              dVar18 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_a8,0);
              *(float *)(param_2 + 0xe) = (float)dVar18;
              uStack_a8 = *(undefined8 *)(param_3 + 0x98);
              puStack_a0 = *(undefined8 **)(param_3 + 0xa0);
              lStack_98 = *(longlong *)(param_3 + 0xa8);
              uStack_90 = *(undefined8 *)(param_3 + 0xb0);
              dVar18 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_a8,0);
              *(float *)((longlong)param_2 + 0x74) = (float)dVar18;
              lVar9 = OLNDAJKNIAG::HLGHEKAFNCA(param_3,0);
              param_2[0x1c] = lVar9;
              sub_8032e8d0(param_2 + 0x1c,lVar9);
              switch((int)param_2[0x1f]) {
              case 0:
                if ((*(longlong *)(param_1 + 0x90) == 0) ||
                   (lVar9 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x508), lVar9 == 0))
                goto code_r0x0001814cc67a;
                uVar8 = sub_855ac210(lVar9,(int)param_2[0xf],uRam0000000187b41390);
                *(undefined4 *)(param_2 + 0x10) = uVar8;
                break;
              case 1:
                *(undefined4 *)(param_2 + 0x10) = 0x3a;
                break;
              case 2:
                *(undefined4 *)(param_2 + 0x10) = 0x22;
                break;
              case 4:
                *(undefined4 *)(param_2 + 0x10) = 0x2c;
                break;
              case 5:
                *(undefined4 *)(param_2 + 0x10) = 0x2b;
              }
              UnitActionController::DependActionSolve(param_1,param_2,0);
              if (cRam000000018ab6269d == '\0') {
                sub_8032f6d0(0x187c19c60);
                sub_8032f6d0(0x187c19e18);
                cRam000000018ab6269d = '\x01';
              }
              iVar7 = 0;
              goto code_r0x0001814cc020;
            }
            lVar9 = sub_849ed2d0(lVar9,iVar7,uRam0000000187ba3a58);
            if (lVar9 == 0) break;
            uStack_b8 = *(undefined4 *)(lVar9 + 0x2c);
            uStack_b4 = *(undefined2 *)(lVar9 + 0x30);
            if (*(int *)(lRam0000000187b61d80 + 0xe4) == 0) {
              sub_8032fa70();
            }
            uVar6 = ObscuredUShort::EMLLIDNPKAK(&uStack_b8,0);
            if (cRam000000018ab626da == '\0') {
              sub_8032f6d0(0x187bfaba0);
              cRam000000018ab626da = '\x01';
            }
            plVar13 = *(longlong **)(*(longlong *)(lRam0000000187bfaba0 + 0xb8) + 8);
            if (plVar13 == (longlong *)0x0) break;
            lVar11 = *plVar13;
            uVar16 = 0;
            if (*(ushort *)(lVar11 + 0x12e) != 0) {
              do {
                if (*(longlong *)(*(longlong *)(lVar11 + 0xb0) + (ulonglong)uVar16 * 0x10) ==
                    lRam0000000187b93630) {
                  puVar12 = (undefined8 *)
                            ((longlong)
                             (*(int *)(*(longlong *)(lVar11 + 0xb0) + 8 + (ulonglong)uVar16 * 0x10)
                             + 0x1b) * 0x10 + 0x138 + lVar11);
                  goto code_r0x0001814cbdc9;
                }
                uVar16 = uVar16 + 1;
              } while (uVar16 < *(ushort *)(lVar11 + 0x12e));
            }
            puVar12 = (undefined8 *)sub_8032c930(plVar13,lRam0000000187b93630,0x1b);
code_r0x0001814cbdc9:
            cVar5 = (*(code *)*puVar12)(plVar13,puVar12[1]);
            if (cVar5 != '\0') {
              if (*(longlong *)(param_1 + 0x90) == 0) break;
              if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x178) == '\0') {
                uVar10 = *(undefined8 *)(lVar9 + 0x14);
                uVar8 = *(undefined4 *)(lVar9 + 0x1c);
                if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                  sub_8032fa70();
                }
                puStack_a0 = (undefined8 *)CONCAT44(puStack_a0._4_4_,uVar8);
                uStack_a8 = uVar10;
                uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
                if (lStack_c8 == 0) break;
                lVar11 = IKGBNKHMJIG::BKEJKNOJCEE(lStack_c8,uVar8,0);
                if (lVar11 != 0) {
                  lVar9 = GAMCMFJBHME::FHOALPFLEEC(lVar11,0);
                }
              }
            }
            UnitActionController::createActionValue(param_1,param_3,lVar9,uVar6,param_2,uStack_68,0);
            iVar7 = iVar7 + 1;
          }
        }
      }
    }
  }
  goto code_r0x0001814cc67a;
code_r0x0001814cc020:
  lVar9 = param_2[4];
  if (lVar9 == 0) goto code_r0x0001814cc67a;
  if (*(int *)(lVar9 + 0x18) <= iVar7) {
    if (param_2[6] != 0) {
      sub_849e2810(&uStack_a8,param_2[6],uRam0000000187b861a8);
      uStack_80 = uStack_a8;
      puStack_78 = puStack_a0;
      lStack_70 = lStack_98;
      uStack_a8 = 0;
      puStack_a0 = &uStack_80;
      goto code_r0x0001814cc0c0;
    }
    goto code_r0x0001814cc67a;
  }
  plVar13 = (longlong *)sub_849ed2d0(lVar9,iVar7,uRam0000000187c19e18);
  if (plVar13 == (longlong *)0x0) goto code_r0x0001814cc67a;
  (**(code **)(*plVar13 + 0x248))
            (plVar13,*(undefined8 *)(param_1 + 0x90),*(undefined8 *)(*plVar13 + 0x250));
  iVar7 = iVar7 + 1;
  goto code_r0x0001814cc020;
code_r0x0001814cc0c0:
  cVar5 = sub_8572bc70(&uStack_80,uRam0000000187b4a978);
  lVar9 = lStack_70;
  if (cVar5 == '\0') goto code_r0x0001814cc365;
  if (lStack_70 == 0) goto code_r0x0001814cc66e;
  uVar10 = *(undefined8 *)(lStack_70 + 0x18);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar5 = Object::op_Equality(uVar10,0);
  if (cVar5 == '\0') {
    if (lVar9 != 0) {
      uVar10 = *(undefined8 *)(lVar9 + 0x10);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar5 = Object::op_Equality(uVar10,0);
      if (cVar5 != '\0') goto code_r0x0001814cc0c0;
      if (*(longlong *)(lVar9 + 0x18) != 0) {
        uVar10 = sub_847c6e60(*(longlong *)(lVar9 + 0x18),uRam0000000187bcb0c0);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        cVar5 = Object::op_Implicit(uVar10,0);
        if (cVar5 == '\0') goto code_r0x0001814cc0c0;
        lVar11 = sub_8032f970(uRam0000000187c20b90);
        sub_804383d0(lVar11,0);
        lVar14 = sub_814a9040(param_1,0);
        if (lVar14 != 0) {
          uVar10 = sub_800121c0(0,uRam0000000187b8ae18,lVar14,*(undefined8 *)(lVar9 + 0x18),0);
          if (lVar11 != 0) {
            *(undefined8 *)(lVar11 + 0x10) = uVar10;
            sub_8032e8d0(lVar11 + 0x10,uVar10);
            if (*(longlong *)(lVar11 + 0x10) != 0) {
              *(undefined1 *)(*(longlong *)(lVar11 + 0x10) + 0xb4) = 1;
              if (*(longlong *)(lVar11 + 0x10) != 0) {
                *(undefined1 *)(*(longlong *)(lVar11 + 0x10) + 0xa0) = 0;
                if (*(longlong *)(lVar11 + 0x10) != 0) {
                  lVar14 = Component::get_transform(*(longlong *)(lVar11 + 0x10),0);
                  uVar10 = ExceptNGUIRoot::get_Transform(0);
                  if (lVar14 != 0) {
                    Transform::set_parent(lVar14,uVar10,0);
                    uVar10 = sub_8032f970(uRam0000000187c32f48);
                    sub_8044b290(uVar10,lVar11,uRam0000000187bd2a58,0);
                    if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                      sub_8032fa70();
                    }
                    ExtensionMethods::Timer(param_1,uVar10,0);
                    lVar14 = sub_814a9040(param_1,0);
                    if (lVar14 != 0) {
                      uVar10 = sub_800121c0(0,uRam0000000187b8ae18,lVar14,
                                             *(undefined8 *)(lVar9 + 0x10),0);
                      *(undefined8 *)(lVar11 + 0x18) = uVar10;
                      sub_8032e8d0(lVar11 + 0x18,uVar10);
                      if (*(longlong *)(lVar11 + 0x18) != 0) {
                        *(undefined1 *)(*(longlong *)(lVar11 + 0x18) + 0xb4) = 1;
                        if (*(longlong *)(lVar11 + 0x18) != 0) {
                          *(undefined1 *)(*(longlong *)(lVar11 + 0x18) + 0xa0) = 0;
                          if (*(longlong *)(lVar11 + 0x18) != 0) {
                            lVar9 = Component::get_transform(*(longlong *)(lVar11 + 0x18),0);
                            uVar10 = ExceptNGUIRoot::get_Transform(0);
                            if (lVar9 != 0) {
                              Transform::set_parent(lVar9,uVar10,0);
                              uVar10 = sub_8032f970(uRam0000000187c32f48);
                              sub_8044b290(uVar10,lVar11,uRam0000000187bd2c10,0);
                              ExtensionMethods::Timer(param_1,uVar10);
                              goto code_r0x0001814cc0c0;
                            }
                            sub_8032f9d0();
                          }
                          sub_8032f9d0();
                        }
                        sub_8032f9d0();
                      }
                      sub_8032f9d0();
                    }
                    sub_8032f9d0();
                  }
                  sub_8032f9d0();
                }
                sub_8032f9d0();
              }
              sub_8032f9d0();
            }
            sub_8032f9d0();
          }
          sub_8032f9d0();
        }
        sub_8032f9d0();
      }
      sub_8032f9d0();
    }
    sub_8032f9d0();
code_r0x0001814cc66e:
    sub_8032f9d0();
    sub_8032b6b0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  goto code_r0x0001814cc0c0;
code_r0x0001814cc365:
  sub_80443e50(&uStack_80,uRam0000000187b4a7c8);
  lVar11 = lStack_88;
  lVar9 = *(longlong *)(lStack_88 + 0x418);
  uVar8 = OLNDAJKNIAG::OMCBOPBAHMH(param_3,0);
  if (lVar9 != 0) {
    lVar14 = GAFPNJAKNOL::CAMFLOFGAAN(lVar9,uVar8,0);
    lVar9 = *(longlong *)(lVar11 + 0x3d8);
    if ((*(byte *)(*param_2 + 0x130) < *(byte *)(lRam0000000187b778e8 + 0x130)) ||
       (*(longlong *)
         (*(longlong *)(*param_2 + 200) + -8 +
         (ulonglong)*(byte *)(lRam0000000187b778e8 + 0x130) * 8) != lRam0000000187b778e8)) {
      return;
    }
    if (*(longlong *)
         (*(longlong *)(*plStack_b0 + 200) + -8 +
         (ulonglong)*(byte *)(lRam0000000187b778e8 + 0x130) * 8) == lRam0000000187b778e8) {
      iVar7 = 0;
      plVar13 = plStack_b0;
      while (lVar11 = plVar13[0x34], lVar11 != 0) {
        if (*(int *)(lVar11 + 0x18) <= iVar7) {
          return;
        }
        if (lVar14 == 0) break;
        if (iVar7 < *(int *)(lVar14 + 0x18)) {
          lVar11 = sub_849ed2d0(lVar11,iVar7,uRam0000000187c30e80);
          if (lVar11 == 0) break;
          *(undefined4 *)(lVar11 + 0x94) = *(undefined4 *)((longlong)param_2 + 0x94);
          if (plVar13[0x34] == 0) break;
          uVar10 = sub_849ed2d0(plVar13[0x34],iVar7,uRam0000000187c30e80);
          lVar11 = sub_849ed2d0(lVar14,iVar7,uRam0000000187c31b08);
          if (lVar11 == 0) break;
          uVar15 = *(undefined8 *)(lVar11 + 0x28);
          uVar8 = *(undefined4 *)(lVar11 + 0x30);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          puStack_a0 = (undefined8 *)CONCAT44(puStack_a0._4_4_,uVar8);
          uStack_a8 = uVar15;
          uVar8 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
          if (lVar9 == 0) break;
          uVar15 = HOEABFDCBIG::CBBMJADBDGF(lVar9,uVar8,0);
          uVar8 = OLNDAJKNIAG::OMCBOPBAHMH(param_3,0);
          UnitActionController::setSkillParameter(param_1,uVar10,uVar15,uVar8,0);
          if (plStack_b0[0x34] == 0) break;
          lVar11 = sub_849ed2d0(plStack_b0[0x34],iVar7,uRam0000000187c30e80);
          if (*(longlong *)(param_1 + 0x90) == 0) break;
          lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x500);
          uVar8 = OLNDAJKNIAG::OMCBOPBAHMH(param_3,0);
          if ((lVar1 == 0) || (uVar8 = sub_855ac210(lVar1,uVar8,uRam0000000187c1cf68), lVar11 == 0)
             ) break;
          SkillBase::SetLevel(lVar11,uVar8,0);
          plVar13 = plStack_b0;
        }
        iVar7 = iVar7 + 1;
      }
    }
  }
code_r0x0001814cc67a:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::set_ContinuousActionEndDone  @ 0x5e88b0  size=7 ==== */

void UnitActionController::set_ContinuousActionEndDone(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xb8) = param_2;
  return;
}



/* ==== UnitActionController::set_DisableUBByModeChange  @ 0x14cc6a0  size=7 ==== */

void UnitActionController::set_DisableUBByModeChange(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x9a) = param_2;
  return;
}



/* ==== UnitActionController::set_IsCancelModeChangeByTrigger  @ 0x1021e00  size=7 ==== */

void UnitActionController::set_IsCancelModeChangeByTrigger(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xa8) = param_2;
  return;
}



/* ==== UnitActionController::set_ModeChanging  @ 0x14cc6b0  size=7 ==== */

void UnitActionController::set_ModeChanging(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x9b) = param_2;
  return;
}



/* ==== UnitActionController::set_MoveEnd  @ 0x14cc6c0  size=7 ==== */

void UnitActionController::set_MoveEnd(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xa9) = param_2;
  return;
}



/* ==== UnitActionController::set_OnNormalAttackStart  @ 0x438e80  size=19 ==== */

void UnitActionController::set_OnNormalAttackStart(longlong param_1,undefined8 param_2)

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



/* ==== UnitActionController::set_Owner  @ 0x438c80  size=19 ==== */

void UnitActionController::set_Owner(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x90) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x90U >> 0xc);
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



/* ==== UnitActionController::set_StopModeChangeEndEffectCalled  @ 0x14cc6d0  size=7 ==== */

void UnitActionController::set_StopModeChangeEndEffectCalled(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x9c) = param_2;
  return;
}



/* ==== UnitActionController::set_UnionBurstCharging  @ 0xf410f0  size=7 ==== */

void UnitActionController::set_UnionBurstCharging(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x99) = param_2;
  return;
}



/* ==== UnitActionController::set_UnionBurstIsChargeTime  @ 0xf410e0  size=7 ==== */

void UnitActionController::set_UnionBurstIsChargeTime(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x98) = param_2;
  return;
}



/* ==== UnitActionController::set_isUnionBurstOnlyOwner  @ 0x14cc6e0  size=7 ==== */

void UnitActionController::set_isUnionBurstOnlyOwner(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xb9) = param_2;
  return;
}



/* ==== UnitActionController::set_skillDictionary  @ 0x44ad90  size=19 ==== */

void UnitActionController::set_skillDictionary(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xb0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xb0U >> 0xc);
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



/* ==== UnitActionController::set_updateBranchMotionRunning  @ 0x14cc6f0  size=7 ==== */

void UnitActionController::set_updateBranchMotionRunning(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xba) = param_2;
  return;
}



/* ==== UnitActionController::sortTargetListByTargetPattern  @ 0x14cc700  size=1 ==== */

void UnitActionController::sortTargetListByTargetPattern(longlong param_1,longlong param_2,longlong param_3)

{
  longlong lVar1;
  code *pcVar2;
  longlong *plVar3;
  char cVar4;
  undefined4 uVar5;
  undefined8 uVar6;
  longlong lVar7;
  longlong lVar8;
  longlong lVar9;
  undefined8 uVar10;
  longlong lVar11;
  undefined8 uVar12;
  undefined4 *puVar13;
  int iVar14;
  int iVar15;
  bool bVar16;
  uint uVar17;
  longlong alStack_d8 [2];
  undefined8 uStack_c8;
  undefined8 *puStack_c0;
  undefined8 uStack_b8;
  undefined8 *puStack_b0;
  longlong lStack_a8;
  undefined8 uStack_a0;
  undefined8 *puStack_98;
  longlong lStack_90;
  undefined8 uStack_88;
  undefined8 uStack_80;
  longlong *plStack_78;
  longlong lStack_70;
  longlong lStack_68;
  undefined4 uStack_50;
  undefined4 uStack_4c;
  undefined4 uStack_48;
  undefined4 uStack_44;
  longlong *plStack_40;
  
  if (cRam000000018ab626ba == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187b67a30);
    sub_8032f6d0(0x187b67da8);
    sub_8032f6d0(0x187b67f60);
    sub_8032f6d0(0x187b67be8);
    sub_8032f6d0(0x187b68120);
    sub_8032f6d0(0x187b682d8);
    sub_8032f6d0(0x187b68650);
    sub_8032f6d0(0x187b68810);
    sub_8032f6d0(0x187b68498);
    sub_8032f6d0(0x187b689c8);
    sub_8032f6d0(0x187b68b88);
    sub_8032f6d0(0x187b68d40);
    sub_8032f6d0(0x187b68f00);
    sub_8032f6d0(0x187b690b8);
    sub_8032f6d0(0x187b69278);
    sub_8032f6d0(0x187b69430);
    sub_8032f6d0(0x187b695f0);
    sub_8032f6d0(0x187b697a8);
    sub_8032f6d0(0x187b69968);
    sub_8032f6d0(0x187b69b20);
    sub_8032f6d0(0x187b69ce0);
    sub_8032f6d0(0x187b69e98);
    sub_8032f6d0(0x187b6a210);
    sub_8032f6d0(0x187b6a3d0);
    sub_8032f6d0(0x187b6a058);
    sub_8032f6d0(0x187b6a588);
    sub_8032f6d0(0x187b67870);
    sub_8032f6d0(0x187b807d8);
    sub_8032f6d0(0x187b4f980);
    sub_8032f6d0(0x187b7f620);
    sub_8032f6d0(0x187b4fb30);
    sub_8032f6d0(0x187b7f7e0);
    sub_8032f6d0(0x187b7f998);
    sub_8032f6d0(0x187b4fce8);
    sub_8032f6d0(0x187b43738);
    sub_8032f6d0(0x187ba1bc0);
    sub_8032f6d0(0x187ba1d70);
    sub_8032f6d0(0x187ba1f28);
    sub_8032f6d0(0x187ba20d8);
    sub_8032f6d0(0x187ba2290);
    sub_8032f6d0(0x187ba2440);
    sub_8032f6d0(0x187ba25f8);
    sub_8032f6d0(0x187ba27a8);
    sub_8032f6d0(0x187ba2960);
    sub_8032f6d0(0x187ba2b10);
    sub_8032f6d0(0x187ba2cc8);
    sub_8032f6d0(0x187ba2e78);
    sub_8032f6d0(0x187ba3030);
    sub_8032f6d0(0x187ba31e0);
    sub_8032f6d0(0x187ba3398);
    sub_8032f6d0(0x187b93630);
    sub_8032f6d0(0x187b7fe40);
    sub_8032f6d0(0x187b7fc80);
    sub_8032f6d0(0x187b7fff8);
    sub_8032f6d0(0x187b9e928);
    sub_8032f6d0(0x187b72a78);
    sub_8032f6d0(0x187b80f68);
    sub_8032f6d0(0x187b7f908);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b81480);
    sub_8032f6d0(0x187bc5830);
    cRam000000018ab626ba = '\x01';
  }
  uStack_a0 = 0;
  puStack_98 = (undefined8 *)0x0;
  lStack_90 = 0;
  uStack_88 = 0;
  uStack_80 = 0;
  plStack_78 = (longlong *)0x0;
  if (param_2 == 0) goto code_r0x0001814cdb8f;
  lVar1 = *(longlong *)(param_2 + 0x50);
  iVar14 = *(int *)(param_2 + 0x14);
  alStack_d8[0] = lVar1;
  lStack_70 = lVar1;
  lStack_68 = lVar1;
  if ((999 < iVar14) && (iVar14 = (iVar14 / 1000) * 1000, 12000 < iVar14)) {
    if ((iVar14 == 13000) || ((iVar14 == 14000 || (iVar14 == 15000)))) goto code_r0x0001814cd97a;
    goto code_r0x0001814ccad5;
  }
  switch(iVar14) {
  case 2:
  case 8:
  case 0x16:
  case 0x17:
    if (lVar1 == 0) goto code_r0x0001814cdb8f;
    iVar14 = *(int *)(lVar1 + 0x18);
    while (1 < iVar14) {
      uVar5 = BattleManager::Random(0,iVar14,0);
      iVar14 = iVar14 + -1;
      uVar10 = sub_849ed2d0(lVar1,uVar5,uRam0000000187b812d0);
      uVar12 = sub_849ed2d0(lVar1,iVar14,uRam0000000187b812d0);
      sub_84aa4620(lVar1,uVar5,uVar12,uRam0000000187b81480);
      sub_84aa4620(lVar1,iVar14,uVar10,uRam0000000187b81480);
    }
    goto code_r0x0001814ccb2b;
  case 3:
  case 0x22:
code_r0x0001814cd97a:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    goto code_r0x0001814ccad5;
  case 4:
code_r0x0001814cd9c0:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba1d70;
    break;
  case 5:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b690b8;
    goto code_r0x0001814ccc3a;
  case 6:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b69278;
    goto code_r0x0001814ccc3a;
  case 7:
    if (lVar1 == 0) goto code_r0x0001814cdb8f;
    *(int *)(lVar1 + 0x1c) = *(int *)(lVar1 + 0x1c) + 1;
    iVar14 = *(int *)(lVar1 + 0x18);
    *(undefined4 *)(lVar1 + 0x18) = 0;
    if (0 < iVar14) {
      Array::Clear(*(undefined8 *)(lVar1 + 0x10),0,iVar14,0);
    }
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x80);
    if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
      sub_8032fa70(lRam0000000187b41650);
    }
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    uVar17 = *(uint *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 0x38) ^ 0x80000000;
    uVar10 = 0;
    goto code_r0x0001814cccfb;
  case 9:
  case 10:
    iVar14 = *(int *)(param_2 + 0x14);
    uVar10 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar10,0,uRam0000000187b68f00,0);
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b6a588,0);
    UnitActionController::sortTargetPosition(param_1,lVar1,iVar14 == 9,uVar10,uVar12,0);
    goto code_r0x0001814ccb2b;
  case 0xb:
    if (lVar1 == 0) goto code_r0x0001814cdb8f;
    *(int *)(lVar1 + 0x1c) = *(int *)(lVar1 + 0x1c) + 1;
    iVar14 = *(int *)(lVar1 + 0x18);
    *(undefined4 *)(lVar1 + 0x18) = 0;
    if (0 < iVar14) {
      Array::Clear(*(undefined8 *)(lVar1 + 0x10),0,iVar14,0);
    }
    if ((*(longlong *)(param_1 + 0x90) == 0) ||
       (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x80), lVar11 == 0))
    goto code_r0x0001814cdb8f;
    uVar17 = 0;
    uVar10 = 1;
code_r0x0001814cccfb:
    uVar10 = AIGOPEMKDKL::MCMPDLOCDCI(lVar11,uVar10,uVar17,0);
    sub_80002460(lVar1,uVar10,uRam0000000187b7fc80);
    goto code_r0x0001814ccb2b;
  case 0xc:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b689c8;
    goto code_r0x0001814ccc3a;
  case 0xd:
  case 0x13:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b68498;
    goto code_r0x0001814ccc3a;
  case 0xe:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b67be8;
    goto code_r0x0001814ccc3a;
  case 0xf:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b67a30;
    goto code_r0x0001814ccc3a;
  case 0x10:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b6a058;
    goto code_r0x0001814ccc3a;
  case 0x11:
    uVar12 = sub_8032f970(uRam0000000187b43738);
    uVar10 = uRam0000000187b69e98;
code_r0x0001814ccc3a:
    sub_854e7780(uVar12,0,uVar10,0);
    sub_847535e0(lVar1,uVar12,uRam0000000187b67870);
    goto code_r0x0001814ccb2b;
  case 0x12:
  case 0x14:
  case 0x15:
  case 0x2a:
  case 0x30:
  case 0x31:
    goto code_r0x0001814ccad5;
  case 0x18:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba2cc8;
    break;
  case 0x19:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba3030;
    break;
  case 0x1a:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba31e0;
    break;
  case 0x1b:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba27a8;
    break;
  case 0x1c:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba2960;
    break;
  case 0x1d:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba2e78;
    break;
  case 0x1e:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba3398;
    break;
  case 0x1f:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba1bc0;
    break;
  case 0x20:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba2440;
    break;
  case 0x21:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba2b10;
    break;
  case 0x23:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b697a8,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b69968;
    goto code_r0x0001814cd2ae;
  case 0x24:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b69430,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b695f0;
code_r0x0001814cd2ae:
    sub_854e7780(uVar6,0,uVar10,0);
    bVar16 = true;
    goto code_r0x0001814cd2c7;
  case 0x25:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba2290;
    break;
  case 0x26:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba1f28;
    break;
  case 0x27:
    lVar11 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto code_r0x0001814cdb8f;
    puVar13 = (undefined4 *)Transform::get_position(alStack_d8,param_3,0);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    *(undefined4 *)(lVar11 + 0x260) = *puVar13;
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba20d8;
    break;
  case 0x28:
    if (lVar1 == 0) goto code_r0x0001814cdb8f;
    *(int *)(lVar1 + 0x1c) = *(int *)(lVar1 + 0x1c) + 1;
    iVar14 = *(int *)(lVar1 + 0x18);
    *(undefined4 *)(lVar1 + 0x18) = 0;
    if (0 < iVar14) {
      Array::Clear(*(undefined8 *)(lVar1 + 0x10),0,iVar14,0);
    }
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x178) == '\0') {
      lVar11 = sub_814a9080(param_1,0);
      if (lVar11 == 0) goto code_r0x0001814cdb8f;
      uVar10 = 6;
    }
    else {
      lVar11 = sub_814a9080(param_1,0);
      if (lVar11 == 0) goto code_r0x0001814cdb8f;
      uVar10 = 5;
    }
    lVar11 = sub_80002270(uVar10,uRam0000000187b93630,lVar11);
    if (lVar11 == 0) goto code_r0x0001814cdb8f;
    sub_849e2810(&uStack_b8,lVar11,uRam0000000187b72a78);
    uStack_a0 = uStack_b8;
    puStack_98 = puStack_b0;
    lStack_90 = lStack_a8;
    uStack_c8 = 0;
    puStack_c0 = &uStack_a0;
    while (cVar4 = sub_8572bc70(&uStack_a0,uRam0000000187b7f7e0), lVar11 = lStack_90, cVar4 != '\0'
          ) {
      if (lStack_90 == 0) goto code_r0x0001814cdbb9;
      cVar4 = UnitCtrl::get_IsPartsBoss(lStack_90,0);
      if (cVar4 == '\0') {
        if (lVar11 == 0) goto code_r0x0001814cdbb4;
        if (*(longlong *)(lVar11 + 0x80) == 0) goto code_r0x0001814cdbaf;
        uVar10 = AIGOPEMKDKL::MCMPDLOCDCI(*(longlong *)(lVar11 + 0x80),0,0,0);
        sub_80002460(lVar1,uVar10,uRam0000000187b7fc80);
      }
      else {
        if (lVar11 == 0) goto code_r0x0001814cdbaa;
        if (*(longlong *)(lVar11 + 0x80) == 0) goto code_r0x0001814cdba5;
        lVar11 = *(longlong *)(*(longlong *)(lVar11 + 0x80) + 0x18);
        if (lVar11 == 0) goto code_r0x0001814cdba0;
        sub_849e2810(&uStack_50,lVar11,uRam0000000187b9e928);
        uStack_88 = CONCAT44(uStack_4c,uStack_50);
        uStack_80 = CONCAT44(uStack_44,uStack_48);
        plStack_78 = plStack_40;
        uStack_b8 = 0;
        puStack_b0 = &uStack_88;
        while (cVar4 = sub_8572bc70(&uStack_88,uRam0000000187b4fb30), plVar3 = plStack_78,
              cVar4 != '\0') {
          if (plStack_78 == (longlong *)0x0) goto code_r0x0001814cdb95;
          cVar4 = (**(code **)(*plStack_78 + 0x398))
                            (plStack_78,*(undefined8 *)(*plStack_78 + 0x3a0));
          if (cVar4 != '\0') {
            sub_80002460(lVar1,plVar3,uRam0000000187b7fc80);
          }
        }
        sub_80443e50(&uStack_88,uRam0000000187b4f980);
      }
    }
    sub_80443e50(puStack_c0,uRam0000000187b7f620);
    goto code_r0x0001814ccb2b;
  case 0x29:
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b68650,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar6,0,uRam0000000187b68810,0);
    bVar16 = false;
    goto code_r0x0001814cd2c7;
  case 0x2b:
  case 0x33:
    iVar14 = *(int *)(param_2 + 0x10);
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b67da8,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b67f60;
    goto code_r0x0001814cd950;
  case 0x2c:
    iVar14 = *(int *)(param_2 + 0x10);
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b68b88,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b68d40;
    goto code_r0x0001814cd950;
  case 0x2d:
    iVar14 = *(int *)(param_2 + 0x10);
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b68120,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b682d8;
    goto code_r0x0001814cd950;
  case 0x2e:
    iVar14 = *(int *)(param_2 + 0x10);
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b69b20,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b69ce0;
    goto code_r0x0001814cd950;
  case 0x2f:
    iVar14 = *(int *)(param_2 + 0x10);
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b68f00,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b6a588;
    goto code_r0x0001814cd950;
  case 0x32:
    iVar14 = *(int *)(param_2 + 0x10);
    uVar12 = sub_8032f970(uRam0000000187b807d8);
    sub_854e7780(uVar12,0,uRam0000000187b6a210,0);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar10 = uRam0000000187b6a3d0;
code_r0x0001814cd950:
    sub_854e7780(uVar6,0,uVar10,0);
    bVar16 = iVar14 == 1;
code_r0x0001814cd2c7:
    UnitActionController::sortTargetPosition(param_1,lVar1,bVar16,uVar12,uVar6,0);
    goto code_r0x0001814ccb2b;
  default:
    if (iVar14 == 11000) goto code_r0x0001814cd97a;
    if (iVar14 == 12000) goto code_r0x0001814cd9c0;
code_r0x0001814ccad5:
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001814cdb8f;
    uVar10 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar6 = sub_8032f970(uRam0000000187b807d8);
    uVar12 = uRam0000000187ba25f8;
  }
  sub_854e7780(uVar6,uVar10,uVar12,0);
  if (lVar1 == 0) goto code_r0x0001814cdb8f;
  sub_84a9fb90(lVar1,uVar6,uRam0000000187b80f68);
code_r0x0001814ccb2b:
  iVar15 = 0;
  iVar14 = *(int *)(param_2 + 0x14);
  if (iVar14 == 0xc) {
code_r0x0001814cdaa0:
    lVar8 = sub_8032f970(uRam0000000187bc5830);
    sub_849de5b0(lVar8,uRam0000000187b7f908);
    lVar7 = sub_8032f970(uRam0000000187bc5830);
    sub_849de5b0(lVar7,uRam0000000187b7f908);
    lVar11 = lStack_68;
    if (lVar1 == 0) goto code_r0x0001814cdb8f;
    for (; iVar15 < *(int *)(lVar11 + 0x18); iVar15 = iVar15 + 1) {
      lVar9 = sub_849ed2d0(lVar1,iVar15,uRam0000000187b812d0);
      if (((lVar9 == 0) || (*(longlong *)(lVar9 + 0x68) == 0)) ||
         (lVar9 = *(longlong *)(*(longlong *)(lVar9 + 0x68) + 0x88), lVar9 == 0))
      goto code_r0x0001814cdb8f;
      cVar4 = BattleUtil::Approximately((int)*(undefined8 *)(lVar9 + 0x848),0,0);
      if (cVar4 == '\0') {
        uVar10 = sub_849ed2d0(lVar1,iVar15,uRam0000000187b812d0);
        lVar9 = lVar7;
      }
      else {
        uVar10 = sub_849ed2d0(lVar1,iVar15,uRam0000000187b812d0);
        lVar9 = lVar8;
      }
      if (lVar9 == 0) goto code_r0x0001814cdb8f;
      sub_80002460(lVar9,uVar10,uRam0000000187b7fc80);
    }
  }
  else {
    if (iVar14 != 0x13) {
      if (iVar14 != 0x1b) {
        return;
      }
      goto code_r0x0001814cdaa0;
    }
    lVar7 = sub_8032f970(uRam0000000187bc5830);
    sub_849de5b0(lVar7,uRam0000000187b7f908);
    lVar8 = sub_8032f970(uRam0000000187bc5830);
    sub_849de5b0(lVar8,uRam0000000187b7f908);
    lVar11 = lStack_70;
    if (lVar1 == 0) goto code_r0x0001814cdb8f;
    for (; iVar15 < *(int *)(lVar11 + 0x18); iVar15 = iVar15 + 1) {
      lVar9 = sub_849ed2d0(lVar1,iVar15,uRam0000000187b812d0);
      if (((lVar9 == 0) || (*(longlong *)(lVar9 + 0x68) == 0)) ||
         (lVar9 = *(longlong *)(*(longlong *)(lVar9 + 0x68) + 0xd8), lVar9 == 0))
      goto code_r0x0001814cdb8f;
      cVar4 = MBNAOPKFJMN::AIKLIOFIFGN(lVar9,0);
      if (cVar4 == '\0') {
        uVar10 = sub_849ed2d0(lVar1,iVar15,uRam0000000187b812d0);
        lVar9 = lVar8;
      }
      else {
        uVar10 = sub_849ed2d0(lVar1,iVar15,uRam0000000187b812d0);
        lVar9 = lVar7;
      }
      if (lVar9 == 0) goto code_r0x0001814cdb8f;
      sub_80002460(lVar9,uVar10,uRam0000000187b7fc80);
    }
  }
  if (lVar7 != 0) {
    sub_84a97390(lVar7,lVar8,uRam0000000187b7fe40);
    *(longlong *)(param_2 + 0x50) = lVar7;
    sub_8032e8d0(param_2 + 0x50,lVar7);
    return;
  }
code_r0x0001814cdb8f:
  sub_8032f9d0();
code_r0x0001814cdb95:
  sub_8032f9d0();
  sub_8032b6b0();
code_r0x0001814cdba0:
  sub_8032f9d0();
code_r0x0001814cdba5:
  sub_8032f9d0();
code_r0x0001814cdbaa:
  sub_8032f9d0();
code_r0x0001814cdbaf:
  sub_8032f9d0();
code_r0x0001814cdbb4:
  sub_8032f9d0();
code_r0x0001814cdbb9:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::sortTargetPosition  @ 0x14cdc90  size=1 ==== */

void UnitActionController::sortTargetPosition(longlong param_1,longlong param_2,byte param_3,longlong param_4,longlong param_5)

{
  undefined4 uVar1;
  longlong lVar2;
  undefined8 uVar3;
  code *pcVar4;
  char cVar5;
  longlong lVar6;
  
  if (cRam000000018ab626bc == '\0') {
    sub_8032f6d0(0x187b80f68);
    cRam000000018ab626bc = '\x01';
  }
  lVar2 = *(longlong *)(param_1 + 0x90);
  if (lVar2 != 0) {
    if (*(char *)(lVar2 + 0x178) != '\0') {
      param_3 = param_3 ^ 1;
    }
    if (*(longlong *)(lVar2 + 0x38) != 0) {
      cVar5 = CLKMBEGHOLA::GMKFGFKMFCE(*(longlong *)(lVar2 + 0x38),0);
      lVar2 = lRam0000000187b80f68;
      if (cVar5 != '\0') {
        param_3 = param_3 ^ 1;
      }
      if (param_2 != 0) {
        if (param_3 == 0) {
          param_4 = param_5;
        }
        if (param_4 == 0) {
          ThrowHelper::ThrowArgumentNullException(0x22);
        }
        if (1 < *(int *)(param_2 + 0x18)) {
          uVar3 = *(undefined8 *)(param_2 + 0x10);
          uVar1 = *(undefined4 *)(param_2 + 0x18);
          lVar6 = *(longlong *)(*(longlong *)(*(longlong *)(lVar2 + 0x20) + 0xc0) + 0x1c8);
          if ((*(byte *)(lVar6 + 0x135) & 1) == 0) {
            lVar6 = sub_8032d010(lVar6);
          }
          if (*(int *)(lVar6 + 0xe4) == 0) {
            sub_8032fa70(lVar6);
          }
          sub_84a17440(uVar3,0,uVar1,param_4,
                        *(undefined8 *)(*(longlong *)(*(longlong *)(lVar2 + 0x20) + 0xc0) + 0x1c0));
        }
        *(int *)(param_2 + 0x1c) = *(int *)(param_2 + 0x1c) + 1;
        return;
      }
    }
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitActionController::startBlurEffects  @ 0x14cdd40  size=1 ==== */

void UnitActionController::startBlurEffects(longlong param_1,longlong param_2,undefined1 param_3)

{
  int iVar1;
  code *pcVar2;
  undefined8 uVar3;
  longlong lVar4;
  uint uVar5;
  ulonglong uVar6;
  ulonglong uVar7;
  
  if (cRam000000018ab626bf == '\0') {
    sub_8032f6d0(0x187bd2190);
    sub_8032f6d0(0x187bd2348);
    cRam000000018ab626bf = '\x01';
  }
  if ((param_2 != 0) && (*(longlong *)(param_2 + 0x40) != 0)) {
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x40) + 0x18);
    uVar6 = 0;
    if (0 < iVar1) {
      do {
        if (*(longlong *)(param_2 + 0x40) == 0) goto code_r0x0001814cdeb3;
        uVar3 = sub_849ed2d0(*(longlong *)(param_2 + 0x40),uVar6,uRam0000000187bd2348);
        if (cRam000000018ab626c0 == '\0') {
          sub_8032f6d0(0x187c21e50);
          cRam000000018ab626c0 = '\x01';
        }
        lVar4 = sub_8032f970(uRam0000000187c21e50);
        sub_80445940(lVar4,0,0);
        if (lVar4 == 0) goto code_r0x0001814cdeb3;
        *(longlong *)(lVar4 + 0x38) = param_1;
        sub_8032e8d0(lVar4 + 0x38,param_1);
        *(undefined8 *)(lVar4 + 0x28) = uVar3;
        sub_8032e8d0(lVar4 + 0x28,uVar3);
        *(longlong *)(lVar4 + 0x30) = param_2;
        sub_8032e8d0(lVar4 + 0x30,param_2);
        *(undefined1 *)(lVar4 + 0x20) = param_3;
        uVar7 = 0;
        if (0.0 < *(float *)(param_2 + 0xb0)) {
          uVar7 = *(ulonglong *)(param_1 + 0x90);
        }
        if (param_1 == 0) goto code_r0x0001814cdeb3;
        UnitActionController::AppendCoroutine(param_1,lVar4,0,uVar7,0);
        uVar5 = (int)uVar6 + 1;
        uVar6 = (ulonglong)uVar5;
      } while ((int)uVar5 < iVar1);
    }
    return;
  }
code_r0x0001814cdeb3:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitActionController::startBlurWithDelay  @ 0x14cdec0  size=1 ==== */

longlong UnitActionController::startBlurWithDelay(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined1 param_4)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626c0 == '\0') {
    sub_8032f6d0(0x187c21e50);
    cRam000000018ab626c0 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c21e50);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x38) = param_1;
    sub_8032e8d0(lVar2 + 0x38,param_1);
    *(undefined8 *)(lVar2 + 0x28) = param_2;
    sub_8032e8d0(lVar2 + 0x28,param_2);
    *(undefined8 *)(lVar2 + 0x30) = param_3;
    sub_8032e8d0(lVar2 + 0x30,param_3);
    *(undefined1 *)(lVar2 + 0x20) = param_4;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitActionController::updateCoroutineWithOutCutIn  @ 0x14cdf80  size=1 ==== */

longlong UnitActionController::updateCoroutineWithOutCutIn(undefined8 param_1,undefined8 param_2)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab626ab == '\0') {
    sub_8032f6d0(0x187c22000);
    cRam000000018ab626ab = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c22000);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    *(undefined8 *)(lVar2 + 0x28) = param_2;
    sub_8032e8d0(lVar2 + 0x28,param_2);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



