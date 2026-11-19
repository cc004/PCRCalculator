/* ==== UnitActionController::.ctor  @ 0xfa9350  size=11 ==== */

void FUN_180fa9350(longlong param_1)

{
  *(undefined1 *)(param_1 + 0x30) = 1;
  FUN_186201610(param_1,0);
  return;
}



/* ==== UnitActionController::<searchTargetUnit>b__121_4  @ 0x14c3d40  size=96 ==== */

void FUN_1814c3d40(longlong param_1,undefined8 param_2)

{
  undefined8 uVar1;
  
  if (DAT_18ab626d7 == '\0') {
    FUN_18032f6d0(&DAT_187b5e978);
    DAT_18ab626d7 = '\x01';
  }
  uVar1 = *(undefined8 *)(param_1 + 0x90);
  if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
    il2cpp_runtime_class_init();
  }
  FUN_18620ca00(param_2,uVar1,0);
  return;
}



/* ==== UnitActionController::<searchTargetUnit>b__121_5  @ 0x14c3da0  size=132 ==== */

bool FUN_1814c3da0(longlong param_1,longlong param_2)

{
  undefined8 uVar1;
  char cVar2;
  
  if (DAT_18ab626d8 == '\0') {
    FUN_18032f6d0(&DAT_187b5e978);
    DAT_18ab626d8 = '\x01';
  }
  uVar1 = *(undefined8 *)(param_1 + 0x90);
  if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
    il2cpp_runtime_class_init();
  }
  cVar2 = FUN_18620ca00(param_2,uVar1,0);
  if (cVar2 == '\0') {
    return false;
  }
  if (param_2 != 0) {
    return *(int *)(param_2 + 0x4b0) == 1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::AddBlackoutTarget  @ 0x14bb540  size=1825 ==== */

void FUN_1814bb540(longlong param_1,undefined4 param_2)

{
  char cVar1;
  int iVar2;
  int iVar3;
  undefined8 uVar4;
  undefined4 uVar5;
  longlong lVar6;
  longlong lVar7;
  undefined8 *puVar8;
  longlong lVar9;
  undefined8 uVar10;
  longlong *plVar11;
  longlong lVar12;
  int iVar13;
  int iVar14;
  int iVar15;
  double dVar16;
  float fVar17;
  undefined1 local_res8;
  undefined1 local_res10;
  ulonglong in_stack_fffffffffffffec8;
  int local_118;
  float local_108;
  float fStack_104;
  undefined8 local_d8;
  undefined4 local_d0;
  undefined8 local_c8;
  float local_c0;
  undefined1 local_b8 [16];
  undefined1 local_a8 [16];
  undefined1 local_98 [16];
  undefined1 local_88 [16];
  undefined1 local_78 [64];
  
  if (DAT_18ab626a9 == '\0') {
    FUN_18032f6d0(&DAT_187b40048);
    FUN_18032f6d0(&DAT_187c421b8);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b812d0);
    FUN_18032f6d0(&DAT_187b446b8);
    DAT_18ab626a9 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar6 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    local_118 = 0;
    if ((lVar6 != 0) && (*(longlong *)(lVar6 + 0x20) != 0)) {
      iVar2 = *(int *)(*(longlong *)(lVar6 + 0x20) + 0x18);
      if (0 < iVar2) {
        do {
          if ((*(longlong *)(lVar6 + 0x20) == 0) ||
             (lVar7 = FUN_1849ed2d0(*(longlong *)(lVar6 + 0x20),local_118,DAT_187c19e18), lVar7 == 0
             )) goto LAB_1814bbc5c;
          if ((*(char *)(lVar7 + 0x86) == '\0') && (*(char *)(lVar7 + 0x84) == '\0')) {
            if (*(longlong *)(param_1 + 0x20) == 0) goto LAB_1814bbc5c;
            puVar8 = (undefined8 *)FUN_1814afa20(local_b8,*(longlong *)(param_1 + 0x20),0);
            in_stack_fffffffffffffec8 = CONCAT71((int7)(in_stack_fffffffffffffec8 >> 8),1);
            local_d8 = *puVar8;
            local_d0 = *(undefined4 *)(puVar8 + 1);
            FUN_1814c9b60(param_1,lVar6,lVar7,&local_d8,in_stack_fffffffffffffec8,1,0);
            iVar14 = 0;
            while( true ) {
              if (*(int *)(lVar7 + 0x1c) <= iVar14) break;
              lVar9 = *(longlong *)(lVar7 + 0x50);
              if (lVar9 == 0) goto LAB_1814bbc5c;
              if (iVar14 < *(int *)(lVar9 + 0x18)) {
                lVar9 = FUN_1849ed2d0(lVar9,iVar14,DAT_187b812d0);
                if (lVar9 == 0) goto LAB_1814bbc5c;
                in_stack_fffffffffffffec8 = *(ulonglong *)(lVar9 + 0x68);
                if (*(int *)(lVar7 + 0x10) == 1) {
                  if ((in_stack_fffffffffffffec8 == 0) ||
                     (*(longlong *)(in_stack_fffffffffffffec8 + 0xb0) == 0)) goto LAB_1814bbc5c;
                  FUN_1804f1ed0(*(longlong *)(in_stack_fffffffffffffec8 + 0xb0),1,0);
                }
                lVar9 = FUN_1814a9080(param_1,0);
                uVar4 = *(undefined8 *)(param_1 + 0x90);
                if ((*(longlong *)(lVar7 + 0x50) == 0) ||
                   (uVar10 = FUN_1849ed2d0(*(longlong *)(lVar7 + 0x50),iVar14,DAT_187b812d0),
                   lVar9 == 0)) goto LAB_1814bbc5c;
                FUN_180012050(2,DAT_187b93630,lVar9,uVar4,in_stack_fffffffffffffec8,uVar10);
                if (*(int *)(lVar7 + 0x60) == 7) {
                  iVar15 = *(int *)(lVar7 + 0x2c);
                  fVar17 = 0.0;
                  local_res8 = 1;
                  local_res10 = 0;
                  if (iVar15 == 1) {
                    if ((*(longlong *)(lVar7 + 0x50) == 0) ||
                       (plVar11 = (longlong *)
                                  FUN_1849ed2d0(*(longlong *)(lVar7 + 0x50),0,DAT_187b812d0),
                       plVar11 == (longlong *)0x0)) goto LAB_1814bbc5c;
                    puVar8 = (undefined8 *)(**(code **)(*plVar11 + 0x178))(local_78,plVar11);
LAB_1814bb9ce:
                    fVar17 = *(float *)(puVar8 + 1);
                    local_108 = (float)*puVar8;
                    fStack_104 = (float)((ulonglong)*puVar8 >> 0x20);
                    if ((plVar11[0xd] == 0) || (*(longlong *)(plVar11[0xd] + 0x38) == 0))
                    goto LAB_1814bbc5c;
                    local_res10 = FUN_18394da00();
                  }
                  else if (iVar15 == 2) {
                    if (*(longlong *)(param_1 + 0x20) == 0) goto LAB_1814bbc5c;
                    puVar8 = (undefined8 *)FUN_1814afa20(local_88,*(longlong *)(param_1 + 0x20),0);
                    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814bbc5c;
                    cVar1 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x165);
                    uVar4 = *puVar8;
                    fVar17 = *(float *)(puVar8 + 1);
                    if (*(longlong *)(lVar7 + 0x58) == 0) goto LAB_1814bbc5c;
                    dVar16 = (double)FUN_1855f06a0(*(longlong *)(lVar7 + 0x58),0,DAT_187b40048);
                    local_108 = (float)uVar4;
                    iVar15 = -1;
                    if (cVar1 == '\0') {
                      iVar15 = 1;
                    }
                    fVar17 = fVar17 + 0.0;
                    local_108 = local_108 + ((float)dVar16 * (float)iVar15) / 540.0;
                    fStack_104 = (float)((ulonglong)uVar4 >> 0x20);
                    fStack_104 = fStack_104 + 0.0;
                    local_res10 = 0;
                  }
                  else if (iVar15 == 3) {
                    if (*(longlong *)(param_1 + 0x20) == 0) goto LAB_1814bbc5c;
                    puVar8 = (undefined8 *)FUN_1814afa20(local_98,*(longlong *)(param_1 + 0x20),0);
                    local_res10 = 0;
                    fVar17 = *(float *)(puVar8 + 1);
                    local_108 = (float)*puVar8;
                    fStack_104 = (float)((ulonglong)*puVar8 >> 0x20);
                  }
                  else {
                    if (iVar15 == 4) {
                      if ((*(longlong *)(lVar7 + 0x50) != 0) &&
                         (plVar11 = (longlong *)
                                    FUN_1849ed2d0(*(longlong *)(lVar7 + 0x50),0,DAT_187b812d0),
                         plVar11 != (longlong *)0x0)) {
                        puVar8 = (undefined8 *)(**(code **)(*plVar11 + 0x178))(local_a8,plVar11);
                        local_res8 = 0;
                        goto LAB_1814bb9ce;
                      }
                      goto LAB_1814bbc5c;
                    }
                    local_108 = 0.0;
                    fStack_104 = 0.0;
                  }
                  iVar15 = 0;
                  if (*(longlong *)(lVar7 + 0x88) == 0) goto LAB_1814bbc5c;
                  iVar3 = *(int *)(*(longlong *)(lVar7 + 0x88) + 0x18);
                  if (0 < iVar3) {
                    do {
                      lVar9 = *(longlong *)(lVar6 + 0x20);
                      if (((*(longlong *)(lVar7 + 0x88) == 0) ||
                          (uVar5 = FUN_184a610f0(*(longlong *)(lVar7 + 0x88),iVar15,DAT_187b446b8),
                          lVar9 == 0)) ||
                         (lVar9 = FUN_1849ed2d0(lVar9,uVar5,DAT_187c19e18), lVar9 == 0))
                      goto LAB_1814bbc5c;
                      *(undefined1 *)(lVar9 + 0x98) = local_res10;
                      in_stack_fffffffffffffec8 = in_stack_fffffffffffffec8 & 0xffffffffffffff00;
                      local_c8 = CONCAT44(fStack_104,local_108);
                      local_c0 = fVar17;
                      FUN_1814c9b60(param_1,lVar6,lVar9,&local_c8,in_stack_fffffffffffffec8,
                                    local_res8,0);
                      for (iVar13 = 0; iVar13 < *(int *)(lVar9 + 0x1c); iVar13 = iVar13 + 1) {
                        lVar12 = *(longlong *)(lVar9 + 0x50);
                        if (lVar12 == 0) goto LAB_1814bbc5c;
                        if (iVar13 == *(int *)(lVar12 + 0x18)) break;
                        lVar12 = FUN_1849ed2d0(lVar12,iVar13,DAT_187b812d0);
                        if (lVar12 == 0) goto LAB_1814bbc5c;
                        in_stack_fffffffffffffec8 = *(ulonglong *)(lVar12 + 0x68);
                        if (*(int *)(lVar7 + 0x10) == 1) {
                          if ((in_stack_fffffffffffffec8 == 0) ||
                             (*(longlong *)(in_stack_fffffffffffffec8 + 0xb0) == 0))
                          goto LAB_1814bbc5c;
                          FUN_1804f1ed0(*(longlong *)(in_stack_fffffffffffffec8 + 0xb0),1,0);
                        }
                        lVar12 = FUN_1814a9080(param_1,0);
                        uVar4 = *(undefined8 *)(param_1 + 0x90);
                        if ((*(longlong *)(lVar9 + 0x50) == 0) ||
                           (uVar10 = FUN_1849ed2d0(*(longlong *)(lVar9 + 0x50),iVar13,DAT_187b812d0)
                           , lVar12 == 0)) goto LAB_1814bbc5c;
                        FUN_180012050(2,DAT_187b93630,lVar12,uVar4,in_stack_fffffffffffffec8,uVar10)
                        ;
                      }
                      iVar15 = iVar15 + 1;
                    } while (iVar15 < iVar3);
                  }
                }
              }
              iVar14 = iVar14 + 1;
            }
            lVar7 = FUN_1814a9080(param_1,0);
            if (lVar7 == 0) goto LAB_1814bbc5c;
            in_stack_fffffffffffffec8 = in_stack_fffffffffffffec8 & 0xffffffff00000000;
            FUN_1800158b0(0x15,DAT_187b93630,lVar7,lVar6,in_stack_fffffffffffffec8);
          }
          local_118 = local_118 + 1;
        } while (local_118 < iVar2);
      }
      return;
    }
  }
LAB_1814bbc5c:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::AppendCoroutine  @ 0x14bbc70  size=153 ==== */

void FUN_1814bbc70(undefined8 param_1,undefined8 param_2,undefined4 param_3,undefined8 param_4)

{
  longlong lVar1;
  
  if (DAT_18ab626c1 == '\0') {
    FUN_18032f6d0(&DAT_187b93630);
    DAT_18ab626c1 = '\x01';
  }
  if (DAT_18ab626da == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab626da = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
  if (lVar1 != 0) {
    FUN_1800110c0(0x11,DAT_187b93630,lVar1,param_2,param_3,param_4);
    return;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::CancelAction  @ 0x14bbd10  size=540 ==== */

void FUN_1814bbd10(longlong param_1,undefined4 param_2)

{
  int iVar1;
  char cVar2;
  longlong lVar3;
  undefined8 uVar4;
  longlong *plVar5;
  longlong lVar6;
  
  if (DAT_18ab626bd == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    FUN_18032f6d0(&DAT_187c4ad38);
    FUN_18032f6d0(&DAT_187c324a8);
    FUN_18032f6d0(&DAT_187c32658);
    FUN_18032f6d0(&DAT_187b5e978);
    DAT_18ab626bd = '\x01';
  }
  if (((*(longlong *)(param_1 + 0xb0) == 0) ||
      (lVar3 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8), lVar3 == 0)) ||
     (*(longlong *)(lVar3 + 0x98) == 0)) {
LAB_1814bbf27:
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
  iVar1 = *(int *)(*(longlong *)(lVar3 + 0x98) + 0x18);
joined_r0x0001814bbdb0:
  do {
    iVar1 = iVar1 + -1;
    if (iVar1 < 0) {
      *(undefined1 *)(lVar3 + 0x84) = 1;
      return;
    }
    if (*(longlong *)(lVar3 + 0x98) == 0) goto LAB_1814bbf27;
    uVar4 = FUN_1849ed2d0(*(longlong *)(lVar3 + 0x98),iVar1,DAT_187c32658);
    if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    cVar2 = FUN_18620ca00(uVar4,0,0);
  } while (cVar2 == '\0');
  if (*(longlong *)(lVar3 + 0x98) == 0) goto LAB_1814bbf27;
  plVar5 = (longlong *)FUN_1849ed2d0(*(longlong *)(lVar3 + 0x98),iVar1,DAT_187c32658);
  if (plVar5 != (longlong *)0x0) {
    if ((*(byte *)(DAT_187c4ad38 + 0x130) <= *(byte *)(*plVar5 + 0x130)) &&
       (*(longlong *)
         (*(longlong *)(*plVar5 + 200) + -8 + (ulonglong)*(byte *)(DAT_187c4ad38 + 0x130) * 8) ==
        DAT_187c4ad38)) goto joined_r0x0001814bbdb0;
  }
  if ((*(longlong *)(lVar3 + 0x98) == 0) ||
     (lVar6 = FUN_1849ed2d0(*(longlong *)(lVar3 + 0x98),iVar1,DAT_187c32658), lVar6 == 0))
  goto LAB_1814bbf27;
  if (*(char *)(lVar6 + 0x50) == '\0') {
    if ((*(longlong *)(lVar3 + 0x98) == 0) ||
       (lVar6 = FUN_1849ed2d0(*(longlong *)(lVar3 + 0x98),iVar1,DAT_187c32658), lVar6 == 0))
    goto LAB_1814bbf27;
    *(undefined1 *)(lVar6 + 0xb4) = 1;
    if ((*(longlong *)(lVar3 + 0x98) == 0) ||
       (lVar6 = FUN_1849ed2d0(*(longlong *)(lVar3 + 0x98),iVar1,DAT_187c32658), lVar6 == 0))
    goto LAB_1814bbf27;
    lVar6 = *(longlong *)(lVar6 + 0x88);
    if ((*(longlong *)(lVar3 + 0x98) == 0) ||
       (uVar4 = FUN_1849ed2d0(*(longlong *)(lVar3 + 0x98),iVar1,DAT_187c32658), lVar6 == 0))
    goto LAB_1814bbf27;
    (**(code **)(lVar6 + 0x18))(*(undefined8 *)(lVar6 + 0x40),uVar4,*(undefined8 *)(lVar6 + 0x28));
  }
  goto joined_r0x0001814bbdb0;
}



/* ==== UnitActionController::CreateExSkill  @ 0x14bbf30  size=3205 ==== */

longlong FUN_1814bbf30(longlong param_1,undefined4 param_2,undefined8 param_3,undefined8 param_4)

{
  undefined4 uVar1;
  char cVar2;
  undefined4 uVar3;
  longlong lVar4;
  longlong lVar5;
  undefined8 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 *puVar9;
  longlong lVar10;
  undefined8 uVar11;
  longlong *plVar12;
  longlong *plVar13;
  ushort uVar14;
  longlong lVar15;
  longlong *plVar16;
  undefined8 local_c8;
  undefined8 *local_c0;
  longlong local_b8;
  longlong local_b0;
  longlong local_a8;
  longlong local_a0;
  undefined8 local_98;
  undefined8 uStack_90;
  undefined8 local_88;
  undefined8 uStack_80;
  undefined8 local_78;
  undefined8 local_68;
  undefined8 uStack_60;
  undefined8 local_58;
  undefined8 uStack_50;
  undefined8 local_48;
  
  if (DAT_18ab626cf == '\0') {
    FUN_18032f6d0(&DAT_187b2e848);
    FUN_18032f6d0(&DAT_187c32f48);
    FUN_18032f6d0(&DAT_187b3cee0);
    FUN_18032f6d0(&DAT_187c37648);
    FUN_18032f6d0(&DAT_187be6780);
    FUN_18032f6d0(&DAT_187be6938);
    FUN_18032f6d0(&DAT_187be6af0);
    FUN_18032f6d0(&DAT_187b45ab0);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187bc7428);
    FUN_18032f6d0(&DAT_187bc75e0);
    FUN_18032f6d0(&DAT_187c19748);
    FUN_18032f6d0(&DAT_187c19590);
    FUN_18032f6d0(&DAT_187ba38a0);
    FUN_18032f6d0(&DAT_187ba3a58);
    FUN_18032f6d0(&DAT_187be2298);
    FUN_18032f6d0(&DAT_187b3c708);
    FUN_18032f6d0(&DAT_187c440d8);
    FUN_18032f6d0(&DAT_187b60fa0);
    FUN_18032f6d0(&DAT_187b778e8);
    FUN_18032f6d0(&DAT_187bd2008);
    FUN_18032f6d0(&DAT_187bd21c0);
    FUN_18032f6d0(&DAT_187bd2378);
    FUN_18032f6d0(&DAT_187bd2530);
    FUN_18032f6d0(&DAT_187c20828);
    FUN_18032f6d0(&DAT_187bd26e8);
    FUN_18032f6d0(&DAT_187bd28a0);
    FUN_18032f6d0(&DAT_187c209e0);
    DAT_18ab626cf = '\x01';
  }
  local_98 = 0;
  uStack_90 = 0;
  local_88 = 0;
  uStack_80 = 0;
  local_78 = 0;
  lVar4 = thunk_FUN_180383da0(DAT_187c20828);
  FUN_1804383d0(lVar4,0);
  local_a8 = lVar4;
  if (lVar4 != 0) {
    *(undefined8 *)(lVar4 + 0x10) = param_4;
    thunk_FUN_1803b4a20(lVar4 + 0x10,param_4);
    *(longlong *)(lVar4 + 0x18) = param_1;
    thunk_FUN_1803b4a20(lVar4 + 0x18,param_1);
    lVar5 = thunk_FUN_180383da0(DAT_187b778e8);
    local_b0 = lVar5;
    if (DAT_18ab62687 == '\0') {
      FUN_18032f6d0(&DAT_187c30b18);
      FUN_18032f6d0(&DAT_187c209c8);
      DAT_18ab62687 = '\x01';
    }
    uVar6 = thunk_FUN_180383da0(DAT_187c209c8);
    FUN_1849de5b0(uVar6,DAT_187c30b18);
    *(undefined8 *)(lVar5 + 0x1a0) = uVar6;
    thunk_FUN_1803b4a20(lVar5 + 0x1a0,uVar6);
    FUN_1814b5fd0(lVar5,0);
    local_a0 = lVar5;
    uVar6 = thunk_FUN_180383da0(DAT_187be2298);
    FUN_1849de5b0(uVar6,DAT_187c19590);
    *(undefined8 *)(lVar5 + 0x20) = uVar6;
    thunk_FUN_1803b4a20(lVar5 + 0x20,uVar6);
    lVar7 = *(longlong *)(DAT_187b3c708 + 0x20);
    if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
      lVar7 = FUN_18032d010(lVar7);
    }
    lVar7 = *(longlong *)(*(longlong *)(lVar7 + 0xc0) + 0x10);
    if ((*(byte *)(lVar7 + 0x135) & 1) == 0) {
      lVar7 = FUN_18032d010(lVar7);
    }
    lVar7 = **(longlong **)(lVar7 + 0xb8);
    if ((lVar7 != 0) && (*(longlong *)(lVar7 + 0x3d8) != 0)) {
      lVar8 = FUN_182b00540(*(longlong *)(lVar7 + 0x3d8),param_2,0);
      local_b8 = *(longlong *)(lVar7 + 0x1cb0);
      plVar16 = (longlong *)0x0;
      plVar13 = plVar16;
      if (lVar8 != 0) {
        while (lVar7 = *(longlong *)(lVar8 + 0x28), lVar7 != 0) {
          if (*(int *)(lVar7 + 0x18) <= (int)plVar13) {
            if (*(longlong *)(param_1 + 0x90) != 0) {
              lVar5 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
              if ((*(longlong *)(lVar4 + 0x10) != 0) &&
                 (lVar7 = *(longlong *)(*(longlong *)(lVar4 + 0x10) + 0x28), lVar7 != 0)) {
                FUN_1855337a0(&local_68,lVar7,DAT_187b3cee0);
                local_98 = local_68;
                uStack_90 = uStack_60;
                local_88 = local_58;
                uStack_80 = uStack_50;
                local_78 = local_48;
                local_c8 = 0;
                local_c0 = &local_98;
                goto switchD_1814bc5f9_caseD_4;
              }
            }
            break;
          }
          lVar7 = FUN_1849ed2d0(lVar7,plVar13,DAT_187ba3a58);
          if (DAT_18ab626da == '\0') {
            FUN_18032f6d0(&DAT_187bfaba0);
            DAT_18ab626da = '\x01';
          }
          plVar12 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
          if (plVar12 == (longlong *)0x0) break;
          lVar10 = *plVar12;
          uVar14 = 0;
          if (*(ushort *)(lVar10 + 0x12e) != 0) {
            do {
              if (*(longlong *)(*(longlong *)(lVar10 + 0xb0) + (ulonglong)uVar14 * 0x10) ==
                  DAT_187b93630) {
                puVar9 = (undefined8 *)
                         ((longlong)
                          (*(int *)(*(longlong *)(lVar10 + 0xb0) + 8 + (ulonglong)uVar14 * 0x10) +
                          0x1b) * 0x10 + 0x138 + lVar10);
                goto LAB_1814bc319;
              }
              uVar14 = uVar14 + 1;
            } while (uVar14 < *(ushort *)(lVar10 + 0x12e));
          }
          puVar9 = (undefined8 *)FUN_18032c930(plVar12,DAT_187b93630,0x1b);
LAB_1814bc319:
          cVar2 = (*(code *)*puVar9)(plVar12,puVar9[1]);
          if (cVar2 == '\0') {
LAB_1814bc3e5:
            lVar15 = *(longlong *)(lVar5 + 0x20);
          }
          else {
            if (*(longlong *)(param_1 + 0x90) == 0) break;
            if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x178) != '\0') goto LAB_1814bc3e5;
            if (lVar7 == 0) break;
            uVar6 = *(undefined8 *)(lVar7 + 0x14);
            uVar3 = *(undefined4 *)(lVar7 + 0x1c);
            if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
              il2cpp_runtime_class_init();
            }
            local_c0 = (undefined8 *)CONCAT44(local_c0._4_4_,uVar3);
            local_c8 = uVar6;
            uVar3 = FUN_183aa36e0(&local_c8,0);
            if (local_b8 == 0) break;
            lVar10 = FUN_1826a75b0(local_b8,uVar3,0);
            if (lVar10 == 0) goto LAB_1814bc3e5;
            lVar15 = *(longlong *)(lVar5 + 0x20);
            lVar7 = FUN_1826bc100(lVar10,0);
          }
          uVar6 = FUN_1814c5fa0(param_1,lVar7,param_3,*(undefined8 *)(lVar4 + 0x10),0);
          if (lVar15 == 0) break;
          FUN_180002460(lVar15,uVar6,DAT_187c19748);
          plVar13 = (longlong *)(ulonglong)((int)plVar13 + 1);
        }
      }
    }
  }
  goto LAB_1814bcb49;
switchD_1814bc5f9_caseD_4:
  cVar2 = FUN_185760f30(&local_98,DAT_187be6938);
  if (cVar2 == '\0') goto LAB_1814bca45;
  lVar7 = thunk_FUN_180383da0(DAT_187c209e0);
  FUN_1804383d0(lVar7,0);
  if (lVar7 == 0) {
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
  *(longlong *)(lVar7 + 0x20) = lVar4;
  thunk_FUN_1803b4a20(lVar7 + 0x20,lVar4);
  *(undefined4 *)(lVar7 + 0x10) = (undefined4)local_88;
  *(undefined4 *)(lVar7 + 0x14) = local_88._4_4_;
  *(undefined4 *)(lVar7 + 0x18) = (undefined4)uStack_80;
  *(undefined4 *)(lVar7 + 0x1c) = uStack_80._4_4_;
  thunk_FUN_1803b4a20(lVar7 + 0x18,0);
  if (*(longlong *)(lVar7 + 0x20) == 0) {
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
  lVar8 = *(longlong *)(*(longlong *)(lVar7 + 0x20) + 0x10);
  if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
  if (*(int *)(lVar8 + 0x4c) != -1) {
    if (*(longlong *)(lVar7 + 0x18) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    if (*(int *)(*(longlong *)(lVar7 + 0x18) + 0x14) == -1) {
      uVar3 = 0xffffffff;
    }
    else {
      uVar3 = *(undefined4 *)(*(longlong *)(*(longlong *)(lVar7 + 0x20) + 0x10) + 0x4c);
      uVar1 = *(undefined4 *)(*(longlong *)(lVar7 + 0x18) + 0x14);
      if (*(int *)(DAT_187c440d8 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar3 = FUN_185bd76c0(uVar3,uVar1,0);
    }
    *(undefined4 *)(lVar8 + 0x4c) = uVar3;
  }
  if (lVar5 == 0) {
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
  if (*(longlong *)(lVar7 + 0x18) == 0) {
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
  FUN_1847b7a70(*(undefined8 *)(lVar5 + 0x2b0),*(undefined4 *)(*(longlong *)(lVar7 + 0x18) + 0x2c),1
                ,DAT_187b45ab0);
  plVar13 = plVar16;
  switch(*(undefined4 *)(lVar7 + 0x10)) {
  case 1:
    if (*(longlong *)(param_1 + 0x90) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x70);
    if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    uVar6 = *(undefined8 *)(lVar8 + 0xe0);
    if (*(longlong *)(lVar7 + 0x20) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar10 = *(longlong *)(*(longlong *)(lVar7 + 0x20) + 0x28);
    if (lVar10 == 0) {
      lVar7 = *(longlong *)(lVar7 + 0x20);
      lVar10 = thunk_FUN_180383da0(DAT_187c32f48);
      FUN_18044b290(lVar10,lVar7,DAT_187bd21c0,0);
      *(longlong *)(lVar7 + 0x28) = lVar10;
      thunk_FUN_1803b4a20(lVar7 + 0x28,lVar10);
    }
    plVar12 = (longlong *)FUN_185c29720(uVar6,lVar10,0);
    if (plVar12 != (longlong *)0x0) {
      if (*plVar12 == DAT_187c32f48) {
        plVar13 = plVar12;
      }
      if (plVar13 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032e920(plVar12);
      }
    }
    *(longlong **)(lVar8 + 0xe0) = plVar13;
    lVar8 = lVar8 + 0xe0;
    break;
  case 2:
    if (*(longlong *)(param_1 + 0x90) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x70);
    if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    uVar6 = *(undefined8 *)(lVar8 + 0x88);
    if (*(longlong *)(lVar7 + 0x20) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar10 = *(longlong *)(*(longlong *)(lVar7 + 0x20) + 0x30);
    if (lVar10 == 0) {
      lVar7 = *(longlong *)(lVar7 + 0x20);
      lVar10 = thunk_FUN_180383da0(DAT_187c37648);
      FUN_183997990(lVar10,lVar7,DAT_187bd2378,0);
      *(longlong *)(lVar7 + 0x30) = lVar10;
      thunk_FUN_1803b4a20(lVar7 + 0x30,lVar10);
    }
    plVar12 = (longlong *)FUN_185c29720(uVar6,lVar10,0);
    plVar13 = (longlong *)0x0;
    if (plVar12 != (longlong *)0x0) {
      plVar13 = plVar16;
      if (*plVar12 == DAT_187c37648) {
        plVar13 = plVar12;
      }
      if (plVar13 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032e920(plVar12);
      }
    }
LAB_1814bc77e:
    *(longlong **)(lVar8 + 0x88) = plVar13;
    lVar8 = lVar8 + 0x88;
    break;
  case 3:
    if (*(longlong *)(param_1 + 0x90) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x70);
    if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    uVar6 = *(undefined8 *)(lVar8 + 0x90);
    uVar11 = thunk_FUN_180383da0(DAT_187c37648);
    FUN_183997990(uVar11,lVar7,DAT_187bd26e8,0);
    plVar12 = (longlong *)FUN_185c29720(uVar6,uVar11,0);
    if (plVar12 != (longlong *)0x0) {
      if (*plVar12 == DAT_187c37648) {
        plVar13 = plVar12;
      }
      if (plVar13 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032e920(plVar12);
      }
    }
    *(longlong **)(lVar8 + 0x90) = plVar13;
    thunk_FUN_1803b4a20(lVar8 + 0x90,plVar13);
  default:
    goto switchD_1814bc5f9_caseD_4;
  case 5:
    if (*(longlong *)(param_1 + 0x90) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x70);
    if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    uVar6 = *(undefined8 *)(lVar8 + 0x88);
    if (*(longlong *)(lVar7 + 0x20) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar10 = *(longlong *)(*(longlong *)(lVar7 + 0x20) + 0x38);
    if (lVar10 == 0) {
      lVar7 = *(longlong *)(lVar7 + 0x20);
      lVar10 = thunk_FUN_180383da0(DAT_187c37648);
      FUN_183997990(lVar10,lVar7,DAT_187bd2530,0);
      *(longlong *)(lVar7 + 0x38) = lVar10;
      thunk_FUN_1803b4a20(lVar7 + 0x38,lVar10);
    }
    plVar12 = (longlong *)FUN_185c29720(uVar6,lVar10,0);
    if (plVar12 == (longlong *)0x0) goto LAB_1814bc77e;
    if (*plVar12 == DAT_187c37648) {
      plVar13 = plVar12;
    }
    if (plVar13 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032e920(plVar12);
    }
    *(longlong **)(lVar8 + 0x88) = plVar13;
    lVar8 = lVar8 + 0x88;
    break;
  case 7:
    if (*(longlong *)(param_1 + 0x90) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd0);
    if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    uVar6 = *(undefined8 *)(lVar8 + 0x100);
    uVar11 = thunk_FUN_180383da0(DAT_187b2e848);
    FUN_184f4e300(uVar11,lVar7,DAT_187bd28a0,0);
    lVar7 = FUN_185c29720(uVar6,uVar11);
    uVar6 = DAT_187b2e848;
    if ((lVar7 != 0) &&
       (plVar13 = (longlong *)thunk_FUN_180382aa0(lVar7,DAT_187b2e848), plVar13 == (longlong *)0x0))
    {
                    /* WARNING: Subroutine does not return */
      FUN_18032e920(lVar7,uVar6);
    }
    *(longlong **)(lVar8 + 0x100) = plVar13;
    thunk_FUN_1803b4a20(lVar8 + 0x100,plVar13);
    goto switchD_1814bc5f9_caseD_4;
  case 8:
    uVar6 = *(undefined8 *)(param_1 + 0xa0);
    if (*(longlong *)(lVar7 + 0x20) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar8 = *(longlong *)(*(longlong *)(lVar7 + 0x20) + 0x20);
    if (lVar8 == 0) {
      lVar7 = *(longlong *)(lVar7 + 0x20);
      lVar8 = thunk_FUN_180383da0(DAT_187c32f48);
      FUN_18044b290(lVar8,lVar7,DAT_187bd2008,0);
      *(longlong *)(lVar7 + 0x20) = lVar8;
      thunk_FUN_1803b4a20(lVar7 + 0x20,lVar8);
    }
    plVar12 = (longlong *)FUN_185c29720(uVar6,lVar8);
    if (plVar12 != (longlong *)0x0) {
      if (*plVar12 == DAT_187c32f48) {
        plVar13 = plVar12;
      }
      if (plVar13 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032e920(plVar12,DAT_187c32f48);
      }
    }
    *(longlong **)(param_1 + 0xa0) = plVar13;
    thunk_FUN_1803b4a20(param_1 + 0xa0);
    goto switchD_1814bc5f9_caseD_4;
  }
  thunk_FUN_1803b4a20(lVar8,plVar13);
  goto switchD_1814bc5f9_caseD_4;
LAB_1814bca45:
  FUN_180443e50(&local_98,DAT_187be6780);
  lVar5 = local_b0;
  lVar4 = *(longlong *)(lVar4 + 0x10);
  if (lVar4 != 0) {
    *(undefined4 *)(lVar4 + 0x50) = *(undefined4 *)(lVar4 + 0x4c);
    FUN_1814bce50(param_1,local_b0,0);
    if (DAT_18ab6269d == '\0') {
      FUN_18032f6d0(&DAT_187c19c60);
      FUN_18032f6d0(&DAT_187c19e18);
      DAT_18ab6269d = '\x01';
    }
    if (lVar5 != 0) {
      while (lVar4 = *(longlong *)(lVar5 + 0x20), lVar4 != 0) {
        if (*(int *)(lVar4 + 0x18) <= (int)plVar16) {
          FUN_1814bd520(param_1,lVar5,0);
          return lVar5;
        }
        plVar13 = (longlong *)FUN_1849ed2d0(lVar4,plVar16,DAT_187c19e18);
        if (plVar13 == (longlong *)0x0) break;
        (**(code **)(*plVar13 + 0x248))(plVar13,*(undefined8 *)(param_1 + 0x90));
        plVar16 = (longlong *)(ulonglong)((int)plVar16 + 1);
      }
    }
  }
LAB_1814bcb49:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::CreateNormalPrefabWithTargetMotion  @ 0x14bcc20  size=548 ==== */

void FUN_1814bcc20(longlong param_1,longlong param_2,int param_3,undefined1 param_4,char param_5,
                  undefined1 param_6)

{
  int iVar1;
  byte bVar2;
  longlong lVar3;
  longlong lVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  byte bVar7;
  int iVar8;
  ulonglong in_stack_ffffffffffffffa8;
  
  if (DAT_18ab626ac == '\0') {
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187b86358);
    FUN_18032f6d0(&DAT_187b86510);
    DAT_18ab626ac = '\x01';
  }
  iVar8 = 0;
  if ((param_2 != 0) && (*(longlong *)(param_2 + 0x30) != 0)) {
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x30) + 0x18);
    if (0 < iVar1) {
      do {
        if ((*(longlong *)(param_2 + 0x30) == 0) ||
           (lVar3 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x30),iVar8,DAT_187b86510), lVar3 == 0))
        goto LAB_1814bce3f;
        if (*(int *)(lVar3 + 0x4c) == param_3) {
          if ((*(int *)(lVar3 + 0x50) == 0) || (*(int *)(lVar3 + 0x50) - 2U < 3)) {
            bVar7 = 1;
          }
          else {
            bVar7 = 0;
          }
          lVar4 = FUN_1814a9080(param_1,0);
          if (lVar4 == 0) goto LAB_1814bce3f;
          bVar2 = FUN_180002270(0x18,DAT_187b93630,lVar4);
          if ((bVar7 & bVar2) == 0) {
            if (param_5 == '\0') {
              if (*(float *)(param_2 + 0xb0) <= 0.0) {
                uVar6 = 0;
              }
              else {
                uVar6 = *(undefined8 *)(param_1 + 0x90);
              }
              uVar5 = FUN_1814c82c0(param_1,lVar3,param_2,param_4,
                                    in_stack_ffffffffffffffa8 & 0xffffffffffffff00,param_6,param_6,0
                                   );
              in_stack_ffffffffffffffa8 = 0;
              FUN_1814bbc70(param_1,uVar5,bVar7 ^ 1,uVar6,0);
            }
            else {
              lVar4 = FUN_1814a9080(param_1,0);
              in_stack_ffffffffffffffa8 = in_stack_ffffffffffffffa8 & 0xffffffffffffff00;
              uVar6 = FUN_1814c82c0(param_1,lVar3,param_2,param_4,in_stack_ffffffffffffffa8,0,0,0);
              if (lVar4 == 0) goto LAB_1814bce3f;
              FUN_18000d7b0(0x12,DAT_187b93630,lVar4,uVar6);
            }
          }
        }
        iVar8 = iVar8 + 1;
      } while (iVar8 < iVar1);
    }
    return;
  }
LAB_1814bce3f:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::DependActionSolve  @ 0x14bce50  size=1394 ==== */

void FUN_1814bce50(undefined8 param_1,longlong param_2)

{
  int iVar1;
  undefined8 uVar2;
  char cVar3;
  undefined1 uVar4;
  undefined4 uVar5;
  longlong lVar6;
  longlong lVar7;
  undefined8 uVar8;
  int iVar9;
  int iVar10;
  undefined8 *puVar11;
  int iVar12;
  undefined8 local_60;
  undefined8 *puStack_58;
  longlong local_50;
  undefined8 local_48;
  undefined8 uStack_40;
  longlong local_38;
  
  if (DAT_18ab62699 == '\0') {
    FUN_18032f6d0(&DAT_187b7ae48);
    FUN_18032f6d0(&DAT_187bb79f8);
    FUN_18032f6d0(&DAT_187bbb740);
    FUN_18032f6d0(&DAT_187bd0620);
    FUN_18032f6d0(&DAT_187b694b0);
    FUN_18032f6d0(&DAT_187b69670);
    FUN_18032f6d0(&DAT_187b69828);
    FUN_18032f6d0(&DAT_187c0c550);
    FUN_18032f6d0(&DAT_187bb7a88);
    FUN_18032f6d0(&DAT_187b418c0);
    FUN_18032f6d0(&DAT_187c311e8);
    FUN_18032f6d0(&DAT_187b413a0);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b446b8);
    FUN_18032f6d0(&DAT_187bf1860);
    FUN_18032f6d0(&DAT_187b5e978);
    FUN_18032f6d0(&DAT_187bcee30);
    FUN_18032f6d0();
    DAT_18ab62699 = '\x01';
  }
  local_48 = 0;
  uStack_40 = 0;
  local_38 = 0;
  iVar12 = 0;
  if (param_2 != 0) {
    while (lVar6 = *(longlong *)(param_2 + 0x20), lVar6 != 0) {
      if (*(int *)(lVar6 + 0x18) <= iVar12) {
        uVar8 = thunk_FUN_180383da0(DAT_187bf1860);
        FUN_1849de5b0(uVar8,DAT_187b413a0);
        *(undefined8 *)(param_2 + 0xa8) = uVar8;
        thunk_FUN_1803b4a20(param_2 + 0xa8,uVar8);
        iVar12 = 0;
        if (*(longlong *)(param_2 + 0x20) != 0) {
          iVar9 = *(int *)(*(longlong *)(param_2 + 0x20) + 0x18);
          if (0 < iVar9) goto LAB_1814bd0d0;
          goto LAB_1814bd1b6;
        }
        break;
      }
      lVar6 = FUN_1849ed2d0(lVar6,iVar12,DAT_187c19e18);
      if (lVar6 == 0) break;
      if (*(int *)(lVar6 + 0x80) != 0) {
        iVar9 = 0;
        if (*(longlong *)(param_2 + 0x20) == 0) break;
        iVar10 = *(int *)(*(longlong *)(param_2 + 0x20) + 0x18);
        if (0 < iVar10) {
          do {
            if (*(longlong *)(param_2 + 0x20) == 0) goto LAB_1814bd3e0;
            lVar6 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x20),iVar9,DAT_187c19e18);
            if (((*(longlong *)(param_2 + 0x20) == 0) ||
                (lVar7 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x20),iVar12,DAT_187c19e18),
                lVar7 == 0)) || (lVar6 == 0)) goto LAB_1814bd3e0;
            if (*(int *)(lVar7 + 0x80) == *(int *)(lVar6 + 0x38)) {
              if (*(longlong *)(lVar6 + 0x88) == 0) goto LAB_1814bd3e0;
              FUN_180002bb0(*(longlong *)(lVar6 + 0x88),iVar12,DAT_187b418c0);
            }
            iVar9 = iVar9 + 1;
          } while (iVar9 < iVar10);
        }
      }
      iVar12 = iVar12 + 1;
    }
  }
  goto LAB_1814bd3e0;
  while (iVar12 = iVar12 + 1, iVar12 < iVar9) {
LAB_1814bd0d0:
    puVar11 = (undefined8 *)(param_2 + 0xa8);
    if ((*(longlong *)(param_2 + 0x20) == 0) ||
       (lVar6 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x20),iVar12,DAT_187c19e18), lVar6 == 0))
    goto LAB_1814bd3e0;
    uVar8 = FUN_1847b36a0(*puVar11,*(undefined8 *)(lVar6 + 0x88),DAT_187bd0620);
    uVar8 = FUN_1847b1ce0(uVar8,DAT_187bb79f8);
    *puVar11 = uVar8;
    thunk_FUN_1803b4a20(puVar11,uVar8);
    if (*(int *)(lVar6 + 0x60) == 7) {
      iVar10 = 0;
      if (*(longlong *)(lVar6 + 0x88) == 0) goto LAB_1814bd3e0;
      iVar1 = *(int *)(*(longlong *)(lVar6 + 0x88) + 0x18);
      if (0 < iVar1) {
        do {
          lVar7 = *(longlong *)(param_2 + 0x20);
          if (((*(longlong *)(lVar6 + 0x88) == 0) ||
              (uVar5 = FUN_184a610f0(*(longlong *)(lVar6 + 0x88),iVar10,DAT_187b446b8), lVar7 == 0))
             || (lVar7 = FUN_1849ed2d0(lVar7,uVar5,DAT_187c19e18), lVar7 == 0)) goto LAB_1814bd3e0;
          *(undefined1 *)(lVar7 + 0x86) = 1;
          iVar10 = iVar10 + 1;
        } while (iVar10 < iVar1);
      }
    }
  }
LAB_1814bd1b6:
  uVar8 = *(undefined8 *)(param_2 + 0x30);
  if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
    il2cpp_runtime_class_init();
  }
  lVar6 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x18);
  if (lVar6 == 0) {
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    uVar2 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
    lVar6 = thunk_FUN_180383da0(DAT_187c0c550);
    FUN_18594c2e0(lVar6,uVar2,DAT_187bcee30,0);
    *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x18) = lVar6;
    thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x18,lVar6);
  }
  uVar8 = FUN_1847a5fd0(uVar8,lVar6,DAT_187b7ae48);
  lVar6 = FUN_1847b2640(uVar8,DAT_187bbb740);
  FUN_1814c3ec0(param_1,param_2,lVar6,1,0);
  if (lVar6 != 0) {
    FUN_1849e2810(&local_60,lVar6,DAT_187c311e8);
    local_38 = local_50;
    local_60 = 0;
    puStack_58 = &local_48;
    while( true ) {
      do {
        cVar3 = FUN_18572bc70(&local_48,DAT_187b69670);
        lVar6 = local_38;
        if (cVar3 == '\0') {
          FUN_180443e50(&local_48,DAT_187b694b0);
          return;
        }
        if (local_38 == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        uVar8 = *(undefined8 *)(local_38 + 0x10);
        if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
          il2cpp_runtime_class_init();
        }
        cVar3 = FUN_18620c870(uVar8,0,0);
      } while (cVar3 != '\0');
      if (lVar6 == 0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032f9d0();
      }
      if (*(longlong *)(lVar6 + 0x10) == 0) break;
      uVar8 = FUN_1847c74a0(*(longlong *)(lVar6 + 0x10),DAT_187bb7a88);
      if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar4 = FUN_18620ca00(uVar8,0);
      *(undefined1 *)(lVar6 + 0xb8) = uVar4;
    }
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
LAB_1814bd3e0:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecAction  @ 0x14bda70  size=2489 ==== */

void FUN_1814bda70(longlong param_1,longlong *param_2,longlong param_3,ulonglong param_4,int param_5
                  ,undefined4 param_6,undefined1 param_7,ulonglong param_8,char param_9)

{
  undefined8 uVar1;
  bool bVar2;
  undefined1 uVar3;
  char cVar4;
  char cVar5;
  longlong lVar6;
  undefined8 uVar7;
  longlong lVar8;
  undefined8 uVar9;
  undefined8 uVar10;
  undefined8 uVar11;
  ulonglong uVar12;
  ulonglong uVar13;
  ulonglong uVar14;
  bool bVar15;
  undefined4 uVar16;
  undefined8 in_stack_ffffffffffffff58;
  undefined4 uVar17;
  undefined8 in_stack_ffffffffffffff60;
  undefined8 uVar18;
  longlong lVar19;
  undefined4 uVar20;
  ulonglong uVar21;
  ulonglong local_70;
  undefined4 uVar22;
  
  if (DAT_18ab626b0 == '\0') {
    FUN_18032f6d0(&DAT_187b3fb28);
    FUN_18032f6d0(&DAT_187b2c8a0);
    FUN_18032f6d0(&DAT_187c4fe78);
    FUN_18032f6d0(&DAT_187b57018);
    FUN_18032f6d0(&DAT_187bfda18);
    FUN_18032f6d0(&DAT_187b3f978);
    FUN_18032f6d0(&DAT_187b2ca58);
    FUN_18032f6d0(&DAT_187bf7300);
    FUN_18032f6d0(&DAT_187b57380);
    FUN_18032f6d0(&DAT_187b2c1d0);
    FUN_18032f6d0(&DAT_187b44ce8);
    FUN_18032f6d0(&DAT_187b2c388);
    FUN_18032f6d0(&DAT_187bdde68);
    FUN_18032f6d0(&DAT_187ba6e58);
    FUN_18032f6d0(&DAT_187b43c50);
    FUN_18032f6d0(&DAT_187bf8138);
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b446b8);
    FUN_18032f6d0(&DAT_187b77e20);
    FUN_18032f6d0(&DAT_187bd0ed8);
    FUN_18032f6d0(&DAT_187c1fa88);
    DAT_18ab626b0 = '\x01';
  }
  lVar6 = thunk_FUN_180383da0(DAT_187c1fa88);
  FUN_1804383d0(lVar6,0);
  if (lVar6 == 0) goto LAB_1814be424;
  *(longlong *)(lVar6 + 0x28) = param_1;
  thunk_FUN_1803b4a20(lVar6 + 0x28,param_1);
  if (param_2 == (longlong *)0x0) goto LAB_1814be424;
  *(longlong *)(lVar6 + 0x10) = param_2[0x2e];
  thunk_FUN_1803b4a20(lVar6 + 0x10);
  *(longlong *)(lVar6 + 0x18) = param_2[0x2f];
  thunk_FUN_1803b4a20(lVar6 + 0x18);
  *(longlong *)(lVar6 + 0x20) = param_2[0x32];
  thunk_FUN_1803b4a20(lVar6 + 0x20);
  uVar7 = thunk_FUN_180383da0(DAT_187bdde68);
  FUN_185575ea0(uVar7,DAT_187bfda18);
  lVar8 = thunk_FUN_180383da0(DAT_187b43c50);
  FUN_18595b5f0(lVar8,lVar6,DAT_187bd0ed8,0);
  uVar9 = thunk_FUN_180383da0(DAT_187bf8138);
  FUN_1804383d0(uVar9,0);
  lVar6 = thunk_FUN_180383da0(DAT_187ba6e58);
  FUN_1855de480(lVar6,uVar9,DAT_187b3f978);
  if ((lVar8 == 0) ||
     (uVar16 = (**(code **)(lVar8 + 0x18))
                         (*(undefined8 *)(lVar8 + 0x40),param_2,0,*(undefined8 *)(lVar8 + 0x28)),
     lVar6 == 0)) goto LAB_1814be424;
  FUN_1855e0980(lVar6,0,uVar16,DAT_187b3fb28);
  uVar16 = (**(code **)(lVar8 + 0x18))
                     (*(undefined8 *)(lVar8 + 0x40),param_2,1,*(undefined8 *)(lVar8 + 0x28));
  FUN_1855e0980(lVar6,1,uVar16,DAT_187b3fb28);
  uVar16 = (**(code **)(lVar8 + 0x18))
                     (*(undefined8 *)(lVar8 + 0x40),param_2,2,*(undefined8 *)(lVar8 + 0x28));
  FUN_1855e0980(lVar6,2,uVar16,DAT_187b3fb28);
  uVar16 = (**(code **)(lVar8 + 0x18))
                     (*(undefined8 *)(lVar8 + 0x40),param_2,3,*(undefined8 *)(lVar8 + 0x28));
  FUN_1855e0980(lVar6,3,uVar16,DAT_187b3fb28);
  uVar16 = (**(code **)(lVar8 + 0x18))
                     (*(undefined8 *)(lVar8 + 0x40),param_2,4,*(undefined8 *)(lVar8 + 0x28));
  FUN_1855e0980(lVar6,4,uVar16,DAT_187b3fb28);
  uVar16 = (**(code **)(lVar8 + 0x18))
                     (*(undefined8 *)(lVar8 + 0x40),param_2,5,*(undefined8 *)(lVar8 + 0x28));
  FUN_1855e0980(lVar6,5,uVar16,DAT_187b3fb28);
  uVar16 = (**(code **)(lVar8 + 0x18))
                     (*(undefined8 *)(lVar8 + 0x40),param_2,6,*(undefined8 *)(lVar8 + 0x28));
  FUN_1855e0980(lVar6,6,uVar16,DAT_187b3fb28);
  if (param_4 == 0) goto LAB_1814be424;
  uVar3 = FUN_1837b2930(param_2,*(undefined8 *)(param_4 + 0x68),param_5,0);
  if (((param_2[0x25] == 0) ||
      (lVar8 = FUN_185685bc0(param_2[0x25],*(undefined8 *)(param_4 + 0x68),DAT_187b57380),
      lVar8 == 0)) || (lVar8 = FUN_1855bdd60(lVar8,param_5,DAT_187bf7300), lVar8 == 0))
  goto LAB_1814be424;
  if ((int)param_2[0xc] == 0xc) {
    uVar16 = (undefined4)param_2[6];
  }
  else {
    uVar16 = *(undefined4 *)((longlong)param_2 + 0x2c);
  }
  uVar13 = 0;
  uVar18 = CONCAT71((int7)((ulonglong)in_stack_ffffffffffffff60 >> 8),*(int *)(lVar8 + 0x14) == 1);
  uVar9 = CONCAT71((int7)((ulonglong)in_stack_ffffffffffffff58 >> 8),uVar3);
  local_70 = 0;
  uVar12 = param_4;
  cVar4 = FUN_1814aac20(param_4,(int)param_2[0xc],uVar16,*(undefined8 *)(param_1 + 0x90),uVar9,
                        uVar18,param_4,0);
  uVar17 = (undefined4)((ulonglong)uVar9 >> 0x20);
  uVar20 = (undefined4)((ulonglong)uVar18 >> 0x20);
  uVar22 = (undefined4)(uVar12 >> 0x20);
  uVar12 = param_4;
  uVar14 = uVar13;
  if (cVar4 == '\0') {
    cVar4 = FUN_1837b3470(param_2,*(undefined8 *)(param_4 + 0x68),param_5,0);
    uVar17 = (undefined4)((ulonglong)uVar9 >> 0x20);
    uVar20 = (undefined4)((ulonglong)uVar18 >> 0x20);
    if ((cVar4 == '\0') && (param_8 == 0)) {
      uVar14 = 0;
    }
    else {
      bVar15 = param_8 != 0;
      if (bVar15) {
        *(undefined1 *)(param_2 + 0x26) = 1;
      }
      if (*(longlong *)(param_4 + 0x68) == 0) goto LAB_1814be424;
      lVar8 = *(longlong *)(*(longlong *)(param_4 + 0x68) + 0x38);
      cVar4 = *(char *)((longlong)param_2 + 0x1a6);
      bVar2 = false;
      if (*(int *)(DAT_187b77e20 + 0xe4) == 0) {
        il2cpp_runtime_class_init(DAT_187b77e20);
      }
      lVar19 = *(longlong *)(*(longlong *)(DAT_187b77e20 + 0xb8) + 0x30);
      if (lVar19 == 0) goto LAB_1814be424;
      cVar5 = FUN_1855f08a0(lVar19,(int)param_2[0xc],DAT_187b44ce8);
      if (((cVar5 != '\0') && ((int)param_2[2] == 1)) && (param_9 == '\0' && cVar4 == '\0')) {
        if (((lVar8 == 0) || (lVar19 = *(longlong *)(lVar8 + 0x2a0), param_3 == 0)) ||
           (*(longlong *)(param_3 + 0x88) == 0)) goto LAB_1814be424;
        cVar4 = FUN_185653020(*(longlong *)(param_3 + 0x88),param_4,DAT_187c4fe78);
        uVar17 = (undefined4)((ulonglong)uVar9 >> 0x20);
        if (cVar4 == '\0') {
          if (lVar19 != 0) {
            cVar4 = FUN_183932570(lVar19,0);
            uVar17 = (undefined4)((ulonglong)uVar9 >> 0x20);
            if (cVar4 != '\0') {
              uVar1 = *(undefined8 *)(param_4 + 0x68);
              uVar10 = FUN_1814a9040(param_1,0);
              uVar11 = FUN_1814a9080(param_1,0);
              uVar18 = 0;
              uVar9 = CONCAT44(uVar17,(int)param_2[0xc]);
              FUN_183932a10(lVar19,uVar1,uVar10,uVar11,uVar9,0);
              uVar12 = *(ulonglong *)(lVar19 + 0x30);
              if (*(longlong *)(param_3 + 0x88) == 0) goto LAB_1814be424;
              FUN_185685e10(*(longlong *)(param_3 + 0x88),param_4,uVar12,DAT_187b2c388);
              goto LAB_1814be0fa;
            }
          }
        }
        else {
          uVar1 = *(undefined8 *)(param_4 + 0x68);
          uVar10 = FUN_1814a9040(param_1,0);
          uVar11 = FUN_1814a9080(param_1,0);
          if (lVar19 == 0) goto LAB_1814be424;
          uVar18 = 0;
          uVar9 = CONCAT44(uVar17,(int)param_2[0xc]);
          FUN_183932a10(lVar19,uVar1,uVar10,uVar11,uVar9,0);
          if (*(longlong *)(param_3 + 0x88) == 0) goto LAB_1814be424;
          uVar12 = FUN_185685bc0(*(longlong *)(param_3 + 0x88),param_4,DAT_187b2c1d0);
LAB_1814be0fa:
          bVar2 = true;
          bVar15 = false;
          param_2[0x37] = param_4;
          thunk_FUN_1803b4a20(param_2 + 0x37,param_4);
          *(undefined1 *)(param_2 + 0x26) = 1;
          local_70 = param_4;
          if (uVar12 == 0) goto LAB_1814be424;
        }
        if (param_2[0x25] == 0) goto LAB_1814be424;
        cVar4 = FUN_185653020(param_2[0x25],*(undefined8 *)(uVar12 + 0x68),DAT_187b57018);
        if (cVar4 == '\0') {
          FUN_1837b2d90(param_2,*(undefined8 *)(uVar12 + 0x68),param_5,0);
        }
      }
      uVar17 = (undefined4)((ulonglong)uVar9 >> 0x20);
      if (bVar15) {
        local_70 = param_8;
        if ((lVar8 == 0) || (lVar8 = *(longlong *)(lVar8 + 0x2a8), param_8 == 0))
        goto LAB_1814be424;
        uVar1 = *(undefined8 *)(param_8 + 0x68);
        if (DAT_18ab626db == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626db = '\x01';
        }
        uVar10 = *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x18);
        if (DAT_18ab626da == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626da = '\x01';
        }
        if (lVar8 == 0) goto LAB_1814be424;
        uVar18 = 0;
        uVar9 = CONCAT44(uVar17,(int)param_2[0xc]);
        FUN_183932a10(lVar8,uVar1,uVar10,*(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8),
                      uVar9,0);
      }
      uVar14 = local_70;
      if (bVar2) {
        if (uVar12 == 0) goto LAB_1814be424;
        uVar18 = CONCAT71((int7)((ulonglong)uVar18 >> 8),1);
        uVar9 = CONCAT71((int7)((ulonglong)uVar9 >> 8),uVar3);
        uVar21 = uVar12;
        cVar4 = FUN_1814aac20(uVar12,(int)param_2[0xc],uVar16,*(undefined8 *)(uVar12 + 0x68),uVar9,
                              uVar18,uVar12,0);
        uVar17 = (undefined4)((ulonglong)uVar9 >> 0x20);
        uVar20 = (undefined4)((ulonglong)uVar18 >> 0x20);
        uVar22 = (undefined4)(uVar21 >> 0x20);
        if (cVar4 != '\0') goto LAB_1814be2c0;
      }
      lVar8 = param_1;
      lVar19 = param_3;
      (**(code **)(*param_2 + 600))
                (param_2,*(undefined8 *)(param_1 + 0x90),uVar12,param_5,param_1,param_3,
                 CONCAT44(uVar22,param_6),uVar7,lVar6,*(undefined8 *)(*param_2 + 0x260));
      uVar17 = (undefined4)((ulonglong)lVar8 >> 0x20);
      uVar20 = (undefined4)((ulonglong)lVar19 >> 0x20);
      *(undefined1 *)(param_2 + 0x26) = 0;
    }
  }
LAB_1814be2c0:
  if ((int)param_2[0xc] != 7) {
    if (param_2[0x24] == 0) goto LAB_1814be424;
    cVar4 = FUN_185653020(param_2[0x24],uVar12,DAT_187b2c8a0);
    if (cVar4 != '\0') {
      if (param_2[0xd] == 0) {
LAB_1814be424:
                    /* WARNING: Subroutine does not return */
        FUN_18032f9d0();
      }
      if (param_5 != *(int *)(param_2[0xd] + 0x18) + -1) {
        return;
      }
      if (param_2[0x24] == 0) goto LAB_1814be424;
      cVar4 = FUN_185675470(param_2[0x24],uVar12,DAT_187b2ca58);
      if (cVar4 == '\0') {
        while( true ) {
          lVar6 = param_2[0x11];
          if (lVar6 == 0) break;
          if (*(int *)(lVar6 + 0x18) <= (int)uVar13) {
            return;
          }
          if (param_3 == 0) break;
          lVar8 = *(longlong *)(param_3 + 0x20);
          uVar16 = FUN_184a610f0(lVar6,uVar13,DAT_187b446b8);
          if ((lVar8 == 0) || (lVar6 = FUN_1849ed2d0(lVar8,uVar16,DAT_187c19e18), lVar6 == 0))
          break;
          if (*(longlong *)(lVar6 + 0xd0) != 0) {
            lVar6 = *(longlong *)(lVar6 + 0xd0);
            (**(code **)(lVar6 + 0x18))(*(undefined8 *)(lVar6 + 0x40),*(undefined8 *)(lVar6 + 0x28))
            ;
          }
          uVar13 = (ulonglong)((int)uVar13 + 1);
        }
        goto LAB_1814be424;
      }
    }
    FUN_1814be430(param_1,param_2,param_3,uVar12,CONCAT44(uVar17,param_5),CONCAT44(uVar20,param_6),
                  uVar7,param_7,uVar14,0);
  }
  return;
}



/* ==== UnitActionController::ExecActionOnStart  @ 0x14bd520  size=610 ==== */

void FUN_1814bd520(longlong param_1,longlong param_2)

{
  longlong *plVar1;
  longlong lVar2;
  undefined8 uVar3;
  int iVar4;
  int iVar5;
  
  if (DAT_18ab6269b == '\0') {
    FUN_18032f6d0(&DAT_187b695b8);
    FUN_18032f6d0(&DAT_187b58b58);
    FUN_18032f6d0(&DAT_187b59568);
    FUN_18032f6d0(&DAT_187b715a8);
    FUN_18032f6d0(&DAT_187b7f908);
    FUN_18032f6d0(&DAT_187b86358);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b86510);
    FUN_18032f6d0(&DAT_187c32990);
    FUN_18032f6d0(&DAT_187bc5830);
    DAT_18ab6269b = '\x01';
  }
  iVar4 = 0;
  iVar5 = 0;
  if (param_2 != 0) {
    while (lVar2 = *(longlong *)(param_2 + 0x20), lVar2 != 0) {
      if (*(int *)(lVar2 + 0x18) <= iVar5) goto LAB_1814bd665;
      plVar1 = (longlong *)FUN_1849ed2d0(lVar2,iVar5,DAT_187c19e18);
      if (plVar1 == (longlong *)0x0) break;
      if ((*(byte *)(DAT_187b695b8 + 0x130) <= *(byte *)(*plVar1 + 0x130)) &&
         (*(longlong *)
           (*(longlong *)(*plVar1 + 200) + -8 + (ulonglong)*(byte *)(DAT_187b695b8 + 0x130) * 8) ==
          DAT_187b695b8)) {
        *(undefined1 *)(param_2 + 0x140) = 1;
      }
      FUN_180015bc0(8,plVar1,param_2,*(undefined8 *)(param_1 + 0x90),param_1);
      iVar5 = iVar5 + 1;
    }
  }
LAB_1814bd77d:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
LAB_1814bd665:
  lVar2 = *(longlong *)(param_2 + 0x30);
  if (lVar2 == 0) goto LAB_1814bd77d;
  if (*(int *)(lVar2 + 0x18) <= iVar4) {
    uVar3 = thunk_FUN_180383da0(DAT_187bc5830);
    FUN_1849de5b0(uVar3,DAT_187b7f908);
    *(undefined8 *)(param_2 + 0x148) = uVar3;
    thunk_FUN_1803b4a20(param_2 + 0x148,uVar3);
    return;
  }
  lVar2 = FUN_1849ed2d0(lVar2,iVar4,DAT_187b86510);
  uVar3 = thunk_FUN_180383da0(DAT_187b59568);
  FUN_185660990(uVar3,DAT_187b58b58);
  if (lVar2 == 0) goto LAB_1814bd77d;
  *(undefined8 *)(lVar2 + 0xa8) = uVar3;
  thunk_FUN_1803b4a20(lVar2 + 0xa8,uVar3);
  if (*(longlong *)(param_2 + 0x30) == 0) goto LAB_1814bd77d;
  lVar2 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x30),iVar4,DAT_187b86510);
  uVar3 = thunk_FUN_180383da0(DAT_187c32990);
  FUN_1849de5b0(uVar3,DAT_187b715a8);
  if (lVar2 == 0) goto LAB_1814bd77d;
  *(undefined8 *)(lVar2 + 0xb0) = uVar3;
  thunk_FUN_1803b4a20(lVar2 + 0xb0,uVar3);
  iVar4 = iVar4 + 1;
  goto LAB_1814bd665;
}



/* ==== UnitActionController::ExecActionOnStart  @ 0x14bd3f0  size=257 ==== */

void FUN_1814bd3f0(longlong param_1)

{
  char cVar1;
  undefined8 local_60;
  undefined8 uStack_58;
  undefined8 local_50;
  undefined8 uStack_48;
  undefined8 local_40;
  undefined8 local_38;
  undefined8 uStack_30;
  undefined8 local_28;
  undefined8 uStack_20;
  undefined8 local_18;
  
  if (DAT_18ab626c9 == '\0') {
    FUN_18032f6d0(&DAT_187c42000);
    FUN_18032f6d0(&DAT_187c0ed88);
    FUN_18032f6d0(&DAT_187c0ef40);
    FUN_18032f6d0(&DAT_187c0f0f0);
    FUN_18032f6d0(&DAT_187bd6490);
    DAT_18ab626c9 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    FUN_1855337a0(&local_38,*(longlong *)(param_1 + 0xb0),DAT_187c42000);
    local_60 = local_38;
    uStack_58 = uStack_30;
    local_50 = local_28;
    uStack_48 = uStack_20;
    local_40 = local_18;
    while( true ) {
      cVar1 = FUN_185760f30(&local_60,DAT_187c0ef40);
      if (cVar1 == '\0') break;
      FUN_1814bd520(param_1,uStack_48,0);
    }
    FUN_180443e50(&local_60,DAT_187c0ed88);
    return;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecActionOnWaveStart  @ 0x14bd790  size=431 ==== */

void FUN_1814bd790(longlong param_1)

{
  longlong lVar1;
  longlong lVar2;
  char cVar3;
  longlong *plVar4;
  int iVar5;
  undefined8 local_60;
  undefined8 uStack_58;
  undefined8 local_50;
  longlong lStack_48;
  undefined8 local_40;
  undefined8 local_38;
  undefined8 uStack_30;
  undefined8 local_28;
  longlong lStack_20;
  undefined8 local_18;
  
  if (DAT_18ab626ca == '\0') {
    FUN_18032f6d0(&DAT_187c42000);
    FUN_18032f6d0(&DAT_187c0ed88);
    FUN_18032f6d0(&DAT_187c0ef40);
    FUN_18032f6d0(&DAT_187c0f0f0);
    FUN_18032f6d0(&DAT_187bd6490);
    DAT_18ab626ca = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    FUN_1855337a0(&local_38,*(longlong *)(param_1 + 0xb0),DAT_187c42000);
    local_60 = local_38;
    uStack_58 = uStack_30;
    local_50 = local_28;
    lStack_48 = lStack_20;
    local_40 = local_18;
    while( true ) {
      cVar3 = FUN_185760f30(&local_60,DAT_187c0ef40);
      lVar2 = lStack_48;
      if (cVar3 == '\0') {
        FUN_180443e50(&local_60,DAT_187c0ed88);
        return;
      }
      if (DAT_18ab6269c == '\0') {
        FUN_18032f6d0(&DAT_187c19c60);
        FUN_18032f6d0(&DAT_187c19e18);
        DAT_18ab6269c = '\x01';
      }
      iVar5 = 0;
      if (lVar2 == 0) break;
      while( true ) {
        lVar1 = *(longlong *)(lVar2 + 0x20);
        if (lVar1 == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        if (*(int *)(lVar1 + 0x18) <= iVar5) break;
        plVar4 = (longlong *)FUN_1849ed2d0(lVar1,iVar5,DAT_187c19e18);
        if (plVar4 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0(0);
        }
        (**(code **)(*plVar4 + 0x228))
                  (plVar4,lVar2,*(undefined8 *)(param_1 + 0x90),param_1,
                   *(undefined8 *)(*plVar4 + 0x230));
        iVar5 = iVar5 + 1;
      }
    }
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecActionWithDelayAndTarget  @ 0x14bd970  size=255 ==== */

longlong FUN_1814bd970(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4,
                      undefined4 param_5,undefined1 param_6,undefined1 param_7,undefined1 param_8,
                      undefined8 param_9)

{
  longlong lVar1;
  
  if (DAT_18ab626af == '\0') {
    FUN_18032f6d0(&DAT_187c210b0);
    DAT_18ab626af = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c210b0);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x40) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x40,param_1);
    *(undefined8 *)(lVar1 + 0x20) = param_2;
    thunk_FUN_1803b4a20(lVar1 + 0x20,param_2);
    *(undefined8 *)(lVar1 + 0x38) = param_3;
    thunk_FUN_1803b4a20(lVar1 + 0x38,param_3);
    *(undefined8 *)(lVar1 + 0x28) = param_4;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_4);
    *(undefined1 *)(lVar1 + 0x31) = param_6;
    *(undefined1 *)(lVar1 + 0x30) = param_7;
    *(undefined1 *)(lVar1 + 0x48) = param_8;
    *(undefined4 *)(lVar1 + 0x34) = param_5;
    *(undefined8 *)(lVar1 + 0x50) = param_9;
    thunk_FUN_1803b4a20(lVar1 + 0x50);
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecChildrenAction  @ 0x14be430  size=1563 ==== */

void FUN_1814be430(longlong param_1,longlong param_2,longlong param_3,ulonglong param_4,int param_5,
                  uint param_6,longlong param_7,char param_8,ulonglong param_9)

{
  int iVar1;
  longlong lVar2;
  longlong *plVar3;
  char cVar4;
  char cVar5;
  undefined4 uVar6;
  longlong lVar7;
  undefined8 uVar8;
  ulonglong uVar9;
  ulonglong uVar10;
  ulonglong uVar11;
  ulonglong in_stack_ffffffffffffff08;
  uint in_stack_ffffffffffffff10;
  ulonglong in_stack_ffffffffffffff20;
  ulonglong in_stack_ffffffffffffff28;
  uint local_c8;
  ulonglong local_c0;
  undefined8 local_a0;
  undefined8 uStack_98;
  longlong *local_90;
  longlong local_88;
  undefined4 local_78;
  undefined4 uStack_74;
  undefined4 uStack_70;
  undefined4 uStack_6c;
  longlong *local_68;
  
  if (DAT_18ab626b1 == '\0') {
    FUN_18032f6d0(&DAT_187bfe0f8);
    FUN_18032f6d0(&DAT_187b44ce8);
    FUN_18032f6d0(&DAT_187bfe990);
    FUN_18032f6d0(&DAT_187b4f980);
    FUN_18032f6d0(&DAT_187b4fb30);
    FUN_18032f6d0(&DAT_187b4fce8);
    FUN_18032f6d0(&DAT_187b71920);
    FUN_18032f6d0(&DAT_187b71e50);
    FUN_18032f6d0(&DAT_187b9e928);
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b446b8);
    FUN_18032f6d0(&DAT_187b77e20);
    DAT_18ab626b1 = '\x01';
  }
  local_a0 = 0;
  uStack_98 = 0;
  local_90 = (longlong *)0x0;
  uVar11 = 0;
  local_c8 = 0;
  if ((param_2 != 0) && (*(longlong *)(param_2 + 0x88) != 0)) {
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x88) + 0x18);
    uVar9 = uVar11;
    while( true ) {
      if (iVar1 <= (int)uVar9) {
        return;
      }
      local_c0 = 0;
      if (param_3 == 0) break;
      lVar7 = *(longlong *)(param_3 + 0x20);
      if ((*(longlong *)(param_2 + 0x88) == 0) ||
         (uVar6 = FUN_184a610f0(*(longlong *)(param_2 + 0x88),uVar9,DAT_187b446b8), lVar7 == 0))
      break;
      lVar7 = FUN_1849ed2d0(lVar7,uVar6,DAT_187c19e18);
      local_88 = lVar7;
      if (param_9 == 0) {
        local_c0 = 0;
        uVar9 = param_4;
        if (lVar7 == 0) break;
      }
      else {
        if (lVar7 == 0) break;
        cVar5 = *(char *)(lVar7 + 0x1a6);
        if (*(int *)(DAT_187b77e20 + 0xe4) == 0) {
          il2cpp_runtime_class_init(DAT_187b77e20);
        }
        lVar2 = *(longlong *)(*(longlong *)(DAT_187b77e20 + 0xb8) + 0x30);
        if (lVar2 == 0) break;
        cVar4 = FUN_1855f08a0(lVar2,*(undefined4 *)(lVar7 + 0x60),DAT_187b44ce8);
        uVar9 = param_9;
        if (cVar4 != '\0' && cVar5 == '\0') {
          local_c0 = param_9;
          uVar9 = param_4;
        }
      }
      param_4 = uVar9;
      if ((*(int *)(lVar7 + 0x60) == 0xe) && (*(int *)(lVar7 + 0x2c) == 3)) {
LAB_1814bea4f:
        local_c8 = local_c8 + 1;
        uVar9 = (ulonglong)local_c8;
      }
      else {
        if (param_7 == 0) break;
        cVar5 = FUN_185568500(param_7,*(undefined4 *)(lVar7 + 0x38),DAT_187bfe0f8);
        if ((cVar5 != '\0') &&
           (cVar5 = FUN_185579340(param_7,*(undefined4 *)(lVar7 + 0x38),DAT_187bfe990),
           cVar5 == '\0')) goto LAB_1814bea4f;
        if (*(int *)(param_2 + 0x60) == 1) {
          if (*(longlong *)(param_2 + 0x68) == 0) break;
          if (param_5 != *(int *)(*(longlong *)(param_2 + 0x68) + 0x18) + -1) goto LAB_1814bea4f;
        }
        if (*(int *)(lVar7 + 0x14) == 0x2a) {
          if ((uVar9 == 0) || (*(longlong *)(uVar9 + 0x68) == 0)) break;
          cVar5 = FUN_181605dc0(*(longlong *)(uVar9 + 0x68),0);
          if (cVar5 == '\0') goto LAB_1814bea4f;
          if (*(longlong *)(lVar7 + 0x1a8) == 0) break;
          cVar5 = FUN_184a98ab0(*(longlong *)(lVar7 + 0x1a8),*(undefined8 *)(uVar9 + 0x68),
                                DAT_187b71e50);
          if (cVar5 != '\0') goto LAB_1814bea4f;
          if (((*(longlong *)(uVar9 + 0x68) == 0) ||
              (lVar2 = *(longlong *)(*(longlong *)(uVar9 + 0x68) + 0x80), lVar2 == 0)) ||
             (lVar2 = *(longlong *)(lVar2 + 0x18), lVar2 == 0)) break;
          FUN_1849e2810(&local_78,lVar2,DAT_187b9e928);
          local_a0 = CONCAT44(uStack_74,local_78);
          uStack_98 = CONCAT44(uStack_6c,uStack_70);
          local_90 = local_68;
          while (cVar5 = FUN_18572bc70(&local_a0,DAT_187b4fb30), plVar3 = local_90, cVar5 != '\0') {
            if (local_90 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
              FUN_18032f9d0();
            }
            cVar5 = (**(code **)(*local_90 + 0x398))(local_90,*(undefined8 *)(*local_90 + 0x3a0));
            if (cVar5 != '\0') {
              if (param_8 == '\0') {
                in_stack_ffffffffffffff20 = in_stack_ffffffffffffff20 & 0xffffffffffffff00;
                in_stack_ffffffffffffff10 = in_stack_ffffffffffffff10 & 0xffffff00;
                in_stack_ffffffffffffff28 = local_c0;
                uVar8 = FUN_1814bd970(param_1,lVar7,param_3,plVar3,param_6,in_stack_ffffffffffffff10
                                      ,1,in_stack_ffffffffffffff20,local_c0,0);
                uVar10 = uVar11;
                if (0.0 < *(float *)(param_3 + 0xb0)) {
                  uVar10 = *(ulonglong *)(param_1 + 0x90);
                }
                if (param_1 == 0) {
                    /* WARNING: Subroutine does not return */
                  FUN_18032f9d0();
                }
                in_stack_ffffffffffffff08 = 0;
                FUN_1814bbc70(param_1,uVar8,1,uVar10,0);
              }
              else {
                if (lVar7 == 0) {
                    /* WARNING: Subroutine does not return */
                  FUN_18032f9d0();
                }
                FUN_1837b2d90(lVar7,*(undefined8 *)(uVar9 + 0x68),0,0);
                in_stack_ffffffffffffff28 = in_stack_ffffffffffffff28 & 0xffffffffffffff00;
                in_stack_ffffffffffffff08 = in_stack_ffffffffffffff08 & 0xffffffff00000000;
                in_stack_ffffffffffffff10 = param_6;
                in_stack_ffffffffffffff20 = local_c0;
                FUN_1814bda70(param_1,lVar7,param_3,uVar9,in_stack_ffffffffffffff08,param_6,1,
                              local_c0,in_stack_ffffffffffffff28,0);
              }
            }
          }
          FUN_180443e50(&local_a0,DAT_187b4f980);
          if ((uVar9 == 0) || (*(longlong *)(lVar7 + 0x1a8) == 0)) break;
          FUN_180002460(*(longlong *)(lVar7 + 0x1a8),*(undefined8 *)(uVar9 + 0x68),DAT_187b71920);
          local_c8 = local_c8 + 1;
          uVar9 = (ulonglong)local_c8;
        }
        else {
          if (param_8 == '\0') {
            in_stack_ffffffffffffff20 = in_stack_ffffffffffffff20 & 0xffffffffffffff00;
            in_stack_ffffffffffffff10 = in_stack_ffffffffffffff10 & 0xffffff00;
            uVar8 = FUN_1814bd970(param_1,lVar7,param_3,uVar9,param_6,in_stack_ffffffffffffff10,1,
                                  in_stack_ffffffffffffff20,local_c0,0);
            uVar9 = uVar11;
            if (0.0 < *(float *)(param_3 + 0xb0)) {
              uVar9 = *(ulonglong *)(param_1 + 0x90);
            }
            if (param_1 != 0) {
              in_stack_ffffffffffffff08 = 0;
              FUN_1814bbc70(param_1,uVar8,1,uVar9,0);
              in_stack_ffffffffffffff28 = local_c0;
              goto LAB_1814bea4f;
            }
            break;
          }
          if (uVar9 == 0) break;
          FUN_1837b2d90(lVar7,*(undefined8 *)(uVar9 + 0x68),0,0);
          in_stack_ffffffffffffff28 = in_stack_ffffffffffffff28 & 0xffffffffffffff00;
          in_stack_ffffffffffffff08 = in_stack_ffffffffffffff08 & 0xffffffff00000000;
          in_stack_ffffffffffffff10 = param_6;
          FUN_1814bda70(param_1,lVar7,param_3,uVar9,in_stack_ffffffffffffff08,param_6,1,local_c0,
                        in_stack_ffffffffffffff28,0);
          local_c8 = local_c8 + 1;
          uVar9 = (ulonglong)local_c8;
          in_stack_ffffffffffffff20 = local_c0;
        }
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecExSkill  @ 0x14beab0  size=747 ==== */

void FUN_1814beab0(longlong param_1,longlong param_2)

{
  undefined8 uVar1;
  char cVar2;
  longlong lVar3;
  int iVar4;
  undefined1 local_res20 [8];
  undefined8 local_60;
  undefined8 uStack_58;
  longlong *local_50;
  undefined4 local_48;
  undefined4 uStack_44;
  undefined4 uStack_40;
  undefined4 uStack_3c;
  longlong *local_38;
  
  if (DAT_18ab626d0 == '\0') {
    FUN_18032f6d0(&DAT_187c4c378);
    FUN_18032f6d0(&DAT_187c4c530);
    FUN_18032f6d0(&DAT_187c4c6e0);
    FUN_18032f6d0(&DAT_187b42140);
    FUN_18032f6d0(&DAT_187c19ab0);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c19e18);
    DAT_18ab626d0 = '\x01';
  }
  local_res20[0] = 0;
  iVar4 = 0;
  FUN_1814c4340(param_1,param_2,local_res20,0,1,0);
  if (param_2 != 0) {
    FUN_1814b5d40(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar3 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar3 != 0)) {
      uVar1 = FUN_18394cde0(lVar3,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar1;
      if (*(longlong *)(param_2 + 0x20) != 0) {
        FUN_1849e2810(&local_60,*(longlong *)(param_2 + 0x20),DAT_187c19ab0);
        local_48 = (undefined4)local_60;
        uStack_44 = local_60._4_4_;
        uStack_40 = (undefined4)uStack_58;
        uStack_3c = uStack_58._4_4_;
        local_38 = local_50;
        local_60 = 0;
        uStack_58 = &local_48;
        while( true ) {
          cVar2 = FUN_18572bc70(&local_48,DAT_187c4c530);
          if (cVar2 == '\0') break;
          if (local_38 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          (**(code **)(*local_38 + 0x218))
                    (local_38,*(undefined8 *)(param_1 + 0x90),param_1,param_2,
                     *(undefined8 *)(*local_38 + 0x220));
        }
        FUN_180443e50(&local_48,DAT_187c4c378);
        if (*(longlong *)(param_2 + 0x20) != 0) {
          FUN_1849e2810(&local_60,*(longlong *)(param_2 + 0x20),DAT_187c19ab0);
          local_48 = (undefined4)local_60;
          uStack_44 = local_60._4_4_;
          uStack_40 = (undefined4)uStack_58;
          uStack_3c = uStack_58._4_4_;
          local_38 = local_50;
          local_60 = 0;
          uStack_58 = &local_48;
          while( true ) {
            cVar2 = FUN_18572bc70(&local_48,DAT_187c4c530);
            if (cVar2 == '\0') break;
            if (local_38 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
              FUN_18032f9d0();
            }
            if ((int)local_38[0xc] == 0x1c) {
              FUN_1814c8980(param_1,param_2,local_38,0);
            }
          }
          FUN_180443e50(&local_48,DAT_187c4c378);
          while (*(longlong *)(param_2 + 0x20) != 0) {
            if (*(int *)(*(longlong *)(param_2 + 0x20) + 0x18) <= iVar4) {
              return;
            }
            lVar3 = FUN_1849ed2d0(*(undefined8 *)(param_2 + 0x20),iVar4,DAT_187c19e18);
            if (lVar3 == 0) break;
            if ((*(int *)(lVar3 + 0x60) != 0x1c) && (*(char *)(lVar3 + 0xe8) == '\0')) {
              if (*(longlong *)(param_2 + 0xa8) == 0) break;
              cVar2 = FUN_184a7f390(*(longlong *)(param_2 + 0xa8),iVar4,DAT_187b42140);
              if (((cVar2 == '\0') || (*(char *)(lVar3 + 0x86) != '\0')) &&
                 (*(char *)(lVar3 + 0x85) == '\0')) {
                FUN_1814c8980(param_1,param_2,lVar3,0);
              }
            }
            iVar4 = iVar4 + 1;
          }
        }
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecSkillWithTargetId  @ 0x14bee20  size=383 ==== */

void FUN_1814bee20(longlong param_1,longlong param_2,int param_3)

{
  longlong lVar1;
  char cVar2;
  longlong *plVar3;
  int iVar4;
  undefined8 uVar5;
  undefined1 local_res10 [8];
  ulonglong in_stack_ffffffffffffffd8;
  
  if (DAT_18ab626d1 == '\0') {
    FUN_18032f6d0(&DAT_187b42140);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c19e18);
    DAT_18ab626d1 = '\x01';
  }
  iVar4 = 0;
  local_res10[0] = 0;
  FUN_1814c4340(param_1,param_2,local_res10,1,in_stack_ffffffffffffffd8 & 0xffffffffffffff00,0);
  if (param_2 != 0) {
    FUN_1814b5d40(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar1 != 0)) {
      uVar5 = FUN_18394cde0(lVar1,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar5;
      while( true ) {
        lVar1 = *(longlong *)(param_2 + 0x20);
        if (lVar1 == 0) break;
        if (*(int *)(lVar1 + 0x18) <= iVar4) {
          return;
        }
        plVar3 = (longlong *)FUN_1849ed2d0(lVar1,iVar4,DAT_187c19e18);
        if (plVar3 == (longlong *)0x0) break;
        (**(code **)(*plVar3 + 0x218))
                  (plVar3,*(undefined8 *)(param_1 + 0x90),param_1,param_2,
                   *(undefined8 *)(*plVar3 + 0x220));
        if (*(longlong *)(param_2 + 0xa8) == 0) break;
        cVar2 = FUN_184a7f390(*(longlong *)(param_2 + 0xa8),iVar4,DAT_187b42140);
        if ((((cVar2 == '\0') || (*(char *)((longlong)plVar3 + 0x86) != '\0')) &&
            (*(char *)((longlong)plVar3 + 0x85) == '\0')) && (param_3 == (int)plVar3[7])) {
          FUN_1814c8980(param_1,param_2,plVar3,0);
        }
        iVar4 = iVar4 + 1;
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecSkillWithTargetParts  @ 0x14bf390  size=1419 ==== */

void FUN_1814bf390(longlong param_1,longlong param_2,longlong param_3)

{
  longlong lVar1;
  undefined8 uVar2;
  longlong *plVar3;
  char cVar4;
  longlong lVar5;
  int iVar6;
  undefined8 in_stack_fffffffffffffef8;
  ulonglong uVar7;
  ulonglong uVar8;
  undefined1 local_d8 [4];
  int local_d4;
  undefined8 local_d0;
  undefined8 uStack_c8;
  longlong *local_c0;
  undefined8 local_b8;
  undefined8 uStack_b0;
  longlong local_a8;
  undefined8 local_a0;
  undefined4 *local_98;
  undefined4 local_90;
  undefined4 uStack_8c;
  undefined4 uStack_88;
  undefined4 uStack_84;
  longlong local_80;
  undefined4 local_78;
  undefined4 uStack_74;
  undefined4 uStack_70;
  undefined4 uStack_6c;
  longlong *local_68;
  
  if (DAT_18ab626d4 == '\0') {
    FUN_18032f6d0(&DAT_187c4c378);
    FUN_18032f6d0(&DAT_187c2fce0);
    FUN_18032f6d0(&DAT_187c2fe90);
    FUN_18032f6d0(&DAT_187c4c530);
    FUN_18032f6d0(&DAT_187c30048);
    FUN_18032f6d0(&DAT_187c4c6e0);
    FUN_18032f6d0(&DAT_187b42140);
    FUN_18032f6d0(&DAT_187b806e0);
    FUN_18032f6d0(&DAT_187c19ab0);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c19e18);
    DAT_18ab626d4 = '\x01';
  }
  local_d8[0] = 0;
  local_b8 = 0;
  uStack_b0 = 0;
  local_a8 = 0;
  iVar6 = 0;
  uVar8 = 0;
  uVar7 = CONCAT71((int7)((ulonglong)in_stack_fffffffffffffef8 >> 8),1);
  FUN_1814c4340(param_1,param_2,local_d8,0,uVar7,0);
  if (param_2 != 0) {
    FUN_1814b5d40(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar5 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar5 != 0)) {
      uVar2 = FUN_18394cde0(lVar5,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar2;
      if (*(longlong *)(param_2 + 0x20) != 0) {
        FUN_1849e2810(&local_d0,*(longlong *)(param_2 + 0x20),DAT_187c19ab0);
        local_78 = (undefined4)local_d0;
        uStack_74 = local_d0._4_4_;
        uStack_70 = (undefined4)uStack_c8;
        uStack_6c = uStack_c8._4_4_;
        local_68 = local_c0;
        local_a0 = 0;
        local_98 = &local_78;
        while( true ) {
          cVar4 = FUN_18572bc70(&local_78,DAT_187c4c530);
          plVar3 = local_68;
          if (cVar4 == '\0') break;
          if (local_68 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          uVar7 = *(ulonglong *)(*local_68 + 0x220);
          (**(code **)(*local_68 + 0x218))
                    (local_68,*(undefined8 *)(param_1 + 0x90),param_1,param_2,uVar7);
          if (plVar3 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          if ((int)plVar3[0xc] == 0x1c) {
            if ((int)plVar3[2] == 1) {
              if (param_3 == 0) {
                    /* WARNING: Subroutine does not return */
                FUN_18032f9d0();
              }
              FUN_1837b2d90(plVar3,*(undefined8 *)(param_3 + 0x68),0,0);
              uVar8 = uVar8 & 0xffffffff00000000;
              uVar7 = uVar7 & 0xffffffff00000000;
              FUN_1814bda70(param_1,plVar3,param_2,param_3,uVar7,uVar8,1,0,0,0);
            }
            else {
              if (plVar3[10] == 0) {
                    /* WARNING: Subroutine does not return */
                FUN_18032f9d0();
              }
              FUN_1849e2810(&local_90,plVar3[10],DAT_187b806e0);
              local_b8 = CONCAT44(uStack_8c,local_90);
              uStack_b0 = CONCAT44(uStack_84,uStack_88);
              local_a8 = local_80;
              local_d0 = 0;
              uStack_c8 = &local_b8;
              while( true ) {
                cVar4 = FUN_18572bc70(&local_b8,DAT_187c2fe90);
                lVar5 = local_a8;
                if (cVar4 == '\0') break;
                if (local_a8 == 0) {
                    /* WARNING: Subroutine does not return */
                  FUN_18032f9d0();
                }
                FUN_1837b2d90(plVar3,*(undefined8 *)(local_a8 + 0x68),0,0);
                uVar8 = uVar8 & 0xffffffff00000000;
                uVar7 = uVar7 & 0xffffffff00000000;
                FUN_1814bda70(param_1,plVar3,param_2,lVar5,uVar7,uVar8,1,0,0,0);
              }
              FUN_180443e50(&local_b8,DAT_187c2fce0);
            }
          }
        }
        FUN_180443e50(local_98,DAT_187c4c378);
        while( true ) {
          lVar5 = *(longlong *)(param_2 + 0x20);
          local_d4 = iVar6;
          if (lVar5 == 0) break;
          if (*(int *)(lVar5 + 0x18) <= iVar6) {
            return;
          }
          lVar5 = FUN_1849ed2d0(lVar5,iVar6,DAT_187c19e18);
          if (lVar5 == 0) break;
          if ((*(int *)(lVar5 + 0x60) != 0x1c) && (*(char *)(lVar5 + 0xe8) == '\0')) {
            if (*(longlong *)(param_2 + 0xa8) == 0) break;
            cVar4 = FUN_184a7f390(*(longlong *)(param_2 + 0xa8),iVar6,DAT_187b42140);
            if (((cVar4 == '\0') || (*(char *)(lVar5 + 0x86) != '\0')) &&
               (*(char *)(lVar5 + 0x85) == '\0')) {
              if (*(int *)(lVar5 + 0x10) == 1) {
                if (((param_3 == 0) || (*(longlong *)(param_3 + 0x68) == 0)) ||
                   (lVar1 = *(longlong *)(*(longlong *)(param_3 + 0x68) + 0x38), lVar1 == 0)) break;
                if (*(char *)(lVar1 + 0x260) == '\0') {
                  cVar4 = FUN_18394d880(lVar1,*(undefined8 *)(param_1 + 0x90),0);
                  if (cVar4 == '\0') {
                    FUN_1837b2d90(lVar5,*(undefined8 *)(param_3 + 0x68),0,0);
                    uVar8 = uVar8 & 0xffffffff00000000;
                    uVar7 = uVar7 & 0xffffffff00000000;
                    FUN_1814bda70(param_1,lVar5,param_2,param_3,uVar7,uVar8,1,0,0,0);
                  }
                }
              }
              else {
                if (*(longlong *)(lVar5 + 0x50) == 0) break;
                FUN_1849e2810(&local_90,*(longlong *)(lVar5 + 0x50),DAT_187b806e0);
                local_b8 = CONCAT44(uStack_8c,local_90);
                uStack_b0 = CONCAT44(uStack_84,uStack_88);
                local_a8 = local_80;
                local_d0 = 0;
                uStack_c8 = &local_b8;
                while( true ) {
                  cVar4 = FUN_18572bc70(&local_b8,DAT_187c2fe90);
                  lVar1 = local_a8;
                  if (cVar4 == '\0') break;
                  if (local_a8 == 0) {
                    /* WARNING: Subroutine does not return */
                    FUN_18032f9d0();
                  }
                  FUN_1837b2d90(lVar5,*(undefined8 *)(local_a8 + 0x68),0,0);
                  uVar8 = uVar8 & 0xffffffff00000000;
                  uVar7 = uVar7 & 0xffffffff00000000;
                  FUN_1814bda70(param_1,lVar5,param_2,lVar1,uVar7,uVar8,1,0,0,0);
                }
                FUN_180443e50(&local_b8,DAT_187c2fce0);
              }
            }
          }
          iVar6 = iVar6 + 1;
        }
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecSkillWithTargetPartsAndActionList  @ 0x14befa0  size=909 ==== */

void FUN_1814befa0(longlong param_1,longlong param_2,longlong param_3,longlong param_4,char param_5)

{
  longlong lVar1;
  undefined8 uVar2;
  int iVar3;
  char cVar4;
  longlong *plVar5;
  ulonglong uVar6;
  ulonglong uVar7;
  undefined1 local_98 [4];
  int local_94;
  undefined8 local_88;
  undefined8 *local_80;
  undefined8 local_78;
  undefined8 uStack_70;
  longlong local_68;
  undefined4 local_60;
  undefined4 uStack_5c;
  undefined4 uStack_58;
  undefined4 uStack_54;
  longlong local_50;
  
  if (DAT_18ab626d3 == '\0') {
    FUN_18032f6d0(&DAT_187c2fce0);
    FUN_18032f6d0(&DAT_187c2fe90);
    FUN_18032f6d0(&DAT_187c30048);
    FUN_18032f6d0(&DAT_187b42140);
    FUN_18032f6d0(&DAT_187b806e0);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c19e18);
    DAT_18ab626d3 = '\x01';
  }
  local_98[0] = 0;
  local_78 = 0;
  uStack_70 = 0;
  local_68 = 0;
  uVar7 = 0;
  FUN_1814c4340(param_1,param_2,local_98,1,1,0);
  if (param_2 != 0) {
    FUN_1814b5d40(param_2,0);
    if ((*(longlong *)(param_1 + 0x90) != 0) &&
       (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar1 != 0)) {
      uVar2 = FUN_18394cde0(lVar1,0,0,0);
      *(undefined8 *)(param_2 + 0x160) = uVar2;
      local_94 = 0;
      if (param_4 != 0) {
        do {
          iVar3 = local_94;
          if (*(int *)(param_4 + 0x18) <= local_94) {
            return;
          }
          plVar5 = (longlong *)FUN_1849ed2d0(param_4,local_94,DAT_187c19e18);
          if (plVar5 == (longlong *)0x0) break;
          uVar6 = *(ulonglong *)(*plVar5 + 0x220);
          (**(code **)(*plVar5 + 0x218))
                    (plVar5,*(undefined8 *)(param_1 + 0x90),param_1,param_2,uVar6);
          if (*(longlong *)(param_2 + 0xa8) == 0) break;
          cVar4 = FUN_184a7f390(*(longlong *)(param_2 + 0xa8),iVar3,DAT_187b42140);
          if (((cVar4 == '\0') || (*(char *)((longlong)plVar5 + 0x86) != '\0')) &&
             (*(char *)((longlong)plVar5 + 0x85) == '\0')) {
            if ((int)plVar5[2] == 1) {
LAB_1814bf2bd:
              if (((param_3 == 0) || (*(longlong *)(param_3 + 0x68) == 0)) ||
                 (lVar1 = *(longlong *)(*(longlong *)(param_3 + 0x68) + 0x38), lVar1 == 0)) break;
              cVar4 = FUN_18394d880(lVar1,*(undefined8 *)(param_1 + 0x90),0);
            }
            else {
              if (param_5 != '\0') {
                if (plVar5[10] != 0) {
                  FUN_1849e2810(&local_60,plVar5[10],DAT_187b806e0);
                  local_78 = CONCAT44(uStack_5c,local_60);
                  uStack_70 = CONCAT44(uStack_54,uStack_58);
                  local_68 = local_50;
                  local_88 = 0;
                  local_80 = &local_78;
                  while (cVar4 = FUN_18572bc70(&local_78,DAT_187c2fe90), lVar1 = local_68,
                        cVar4 != '\0') {
                    if (local_68 == 0) {
                    /* WARNING: Subroutine does not return */
                      FUN_18032f9d0();
                    }
                    FUN_1837b2d90(plVar5,*(undefined8 *)(local_68 + 0x68),0,0);
                    uVar7 = uVar7 & 0xffffffff00000000;
                    uVar6 = uVar6 & 0xffffffff00000000;
                    FUN_1814bda70(param_1,plVar5,param_2,lVar1,uVar6,uVar7,1,0,0,0);
                  }
                  FUN_180443e50(&local_78,DAT_187c2fce0);
                  goto LAB_1814bf352;
                }
                break;
              }
              if ((int)plVar5[2] == 1) goto LAB_1814bf2bd;
              cVar4 = '\0';
              if (param_3 == 0) break;
            }
            lVar1 = *(longlong *)(param_3 + 0x68);
            if ((lVar1 == 0) || (*(longlong *)(lVar1 + 0x38) == 0)) break;
            if (cVar4 == '\0' && *(char *)(*(longlong *)(lVar1 + 0x38) + 0x260) == '\0') {
              FUN_1837b2d90(plVar5,lVar1,0,0);
              uVar7 = uVar7 & 0xffffffff00000000;
              FUN_1814bda70(param_1,plVar5,param_2,param_3,uVar6 & 0xffffffff00000000,uVar7,1,0,0,0)
              ;
            }
          }
LAB_1814bf352:
          local_94 = iVar3 + 1;
        } while( true );
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecUnitActionNoDelay  @ 0x14bfa00  size=384 ==== */

void FUN_1814bfa00(undefined8 param_1,longlong param_2,undefined8 param_3)

{
  char cVar1;
  longlong lVar2;
  int iVar3;
  
  if (DAT_18ab626ae == '\0') {
    FUN_18032f6d0(&DAT_187b57018);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b812d0);
    DAT_18ab626ae = '\x01';
  }
  if ((param_2 != 0) && (*(longlong *)(param_2 + 0x50) != 0)) {
    if (*(int *)(*(longlong *)(param_2 + 0x50) + 0x18) == 0) {
      FUN_1814c90d0(param_1,param_2,param_3,0);
    }
    iVar3 = 0;
    while (lVar2 = *(longlong *)(param_2 + 0x50), lVar2 != 0) {
      if ((*(int *)(lVar2 + 0x18) <= iVar3) ||
         (((*(int *)(param_2 + 0x1c) <= iVar3 && (*(int *)(param_2 + 0x1c) != 0)) && (iVar3 != 0))))
      {
        return;
      }
      lVar2 = FUN_1849ed2d0(lVar2,iVar3,DAT_187b812d0);
      if ((lVar2 == 0) || (*(longlong *)(param_2 + 0x128) == 0)) break;
      cVar1 = FUN_185653020(*(longlong *)(param_2 + 0x128),*(undefined8 *)(lVar2 + 0x68),
                            DAT_187b57018);
      if (cVar1 == '\0') {
        FUN_1837b2d90(param_2,*(undefined8 *)(lVar2 + 0x68),0,0);
      }
      FUN_1814bda70(param_1,param_2,param_3,lVar2,0,0,0,0,0,0);
      iVar3 = iVar3 + 1;
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::ExecUnitActionWithDelay  @ 0x14bfb90  size=654 ==== */

void FUN_1814bfb90(longlong param_1,longlong *param_2,longlong param_3,undefined1 param_4,
                  undefined1 param_5,undefined1 param_6)

{
  char cVar1;
  longlong lVar2;
  undefined8 uVar3;
  int iVar4;
  longlong *plVar5;
  longlong *plVar6;
  longlong *plVar7;
  ulonglong in_stack_ffffffffffffff98;
  undefined4 uVar8;
  
  if (DAT_18ab626ad == '\0') {
    FUN_18032f6d0(&DAT_187b644c8);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b812d0);
    DAT_18ab626ad = '\x01';
  }
  if ((param_2 != (longlong *)0x0) && (param_2[10] != 0)) {
    if (*(int *)(param_2[10] + 0x18) == 0) {
      FUN_1814c90d0(param_1,param_2,param_3,0);
    }
    uVar8 = (undefined4)(in_stack_ffffffffffffff98 >> 0x20);
    plVar7 = (longlong *)0x0;
    plVar6 = plVar7;
    if ((int)param_2[0xc] == 0x19) {
      if (((*(longlong *)(param_1 + 0x90) == 0) ||
          (lVar2 = FUN_1815f9970(*(longlong *)(param_1 + 0x90),0), param_3 == 0)) || (lVar2 == 0))
      goto LAB_1814bfe19;
      in_stack_ffffffffffffff98 = CONCAT44(uVar8,0xffffffff);
      cVar1 = FUN_183dd2e00(lVar2,*(undefined4 *)(param_3 + 0x80),*(undefined4 *)(param_3 + 0x94),1,
                            in_stack_ffffffffffffff98,0);
      if (cVar1 != '\0') {
        if ((*(byte *)(*param_2 + 0x130) < *(byte *)(DAT_187b644c8 + 0x130)) ||
           (lVar2 = *(longlong *)(*param_2 + 200),
           *(longlong *)(lVar2 + -8 + (ulonglong)*(byte *)(DAT_187b644c8 + 0x130) * 8) !=
           DAT_187b644c8)) goto LAB_1814bfe19;
        plVar5 = plVar7;
        if (*(longlong *)(lVar2 + -8 + (ulonglong)*(byte *)(DAT_187b644c8 + 0x130) * 8) ==
            DAT_187b644c8) {
          plVar5 = param_2;
        }
        uVar3 = FUN_18386e4f0(plVar5,param_3,*(undefined8 *)(param_1 + 0x90),param_1,0);
        plVar5 = plVar7;
        if (0.0 < *(float *)(param_3 + 0xb0)) {
          plVar5 = *(longlong **)(param_1 + 0x90);
        }
        in_stack_ffffffffffffff98 = 0;
        FUN_1814bbc70(param_1,uVar3,1,plVar5,0);
      }
      *(undefined4 *)(param_2 + 0x1f) = 0;
    }
    while (lVar2 = param_2[10], lVar2 != 0) {
      iVar4 = (int)plVar6;
      if ((*(int *)(lVar2 + 0x18) <= iVar4) ||
         (((*(int *)((longlong)param_2 + 0x1c) <= iVar4 && (*(int *)((longlong)param_2 + 0x1c) != 0)
           ) && (iVar4 != 0)))) {
        return;
      }
      uVar3 = FUN_1849ed2d0(lVar2,plVar6,DAT_187b812d0);
      uVar3 = FUN_1814bd970(param_1,param_2,param_3,uVar3,
                            in_stack_ffffffffffffff98 & 0xffffffff00000000,param_4,param_5,param_6,0
                            ,0);
      if (param_3 == 0) break;
      plVar6 = plVar7;
      if (0.0 < *(float *)(param_3 + 0xb0)) {
        plVar6 = *(longlong **)(param_1 + 0x90);
      }
      if (param_1 == 0) break;
      in_stack_ffffffffffffff98 = 0;
      FUN_1814bbc70(param_1,uVar3,1,plVar6,0);
      plVar6 = (longlong *)(ulonglong)(iVar4 + 1);
    }
  }
LAB_1814bfe19:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::GetAnimeId  @ 0x14bfe20  size=96 ==== */

undefined4 FUN_1814bfe20(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626c6 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626c6 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      return *(undefined4 *)(lVar1 + 0x80);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::GetAttackSide  @ 0x14bfe90  size=76 ==== */

undefined8 FUN_1814bfe90(undefined4 param_1,longlong param_2)

{
  undefined8 uVar1;
  
  switch(param_1) {
  case 1:
  case 4:
  case 7:
    break;
  default:
    return 0;
  }
  if (param_2 != 0) {
    uVar1 = 1;
    if (*(char *)(param_2 + 0x165) != '\0') {
      uVar1 = 0xffffffff;
    }
    return uVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::GetForceCutinOff  @ 0x14bff00  size=97 ==== */

undefined1 FUN_1814bff00(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626cc == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626cc = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      return *(undefined1 *)(lVar1 + 0xd4);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::GetIsSkillPrincessForm  @ 0x14bff70  size=94 ==== */

undefined1 FUN_1814bff70(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626cd == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626cd = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      return *(undefined1 *)(lVar1 + 0x10);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::GetPrincessFormMovieData  @ 0x14bffd0  size=94 ==== */

undefined8 FUN_1814bffd0(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626ce == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626ce = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      return *(undefined8 *)(lVar1 + 0x18);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::GetSkillMotionType  @ 0x14c0030  size=96 ==== */

undefined4 FUN_1814c0030(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626c4 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626c4 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      return *(undefined4 *)(lVar1 + 0xf8);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::GetSkillNum  @ 0x14c00a0  size=96 ==== */

undefined4 FUN_1814c00a0(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626c5 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626c5 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      return *(undefined4 *)(lVar1 + 0x94);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::HasNextAnime  @ 0x14c0110  size=242 ==== */

undefined8 FUN_1814c0110(longlong param_1,undefined4 param_2)

{
  int iVar1;
  undefined4 uVar2;
  longlong lVar3;
  undefined8 uVar4;
  
  if (DAT_18ab626c2 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626c2 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar3 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar3 != 0) {
      if (*(char *)(lVar3 + 0x10) != '\0') {
        return 1;
      }
      iVar1 = *(int *)(lVar3 + 0x80);
      uVar2 = *(undefined4 *)(lVar3 + 0x94);
      if ((((iVar1 - 0x29U & 0xfffffffc) != 0) || (iVar1 == 0x2a)) && (iVar1 != 0x2f)) {
        return 0;
      }
      if (*(longlong *)(param_1 + 0x90) != 0) {
        lVar3 = FUN_1815f9970(*(longlong *)(param_1 + 0x90),0);
        if (lVar3 != 0) {
          uVar4 = FUN_183dd2e00(lVar3,iVar1,uVar2,1,0xffffffff,0);
          return uVar4;
        }
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::Initialize  @ 0x14c02c0  size=6899 ==== */

void FUN_1814c02c0(longlong param_1,longlong param_2,longlong param_3,char param_4,longlong param_5)

{
  float fVar1;
  double dVar2;
  ulonglong uVar3;
  char cVar4;
  int iVar5;
  undefined4 uVar6;
  int iVar7;
  int iVar8;
  undefined8 uVar9;
  longlong *plVar10;
  longlong lVar11;
  longlong lVar12;
  undefined8 uVar13;
  longlong lVar14;
  ulonglong uVar15;
  longlong lVar16;
  uint uVar17;
  float fVar18;
  undefined4 local_f8 [2];
  undefined8 local_f0;
  undefined8 local_e8;
  undefined8 *puStack_e0;
  ulonglong local_d8;
  longlong lStack_d0;
  undefined8 local_c8;
  undefined8 local_b8;
  undefined4 local_b0;
  undefined8 local_a8;
  undefined8 *puStack_a0;
  ulonglong local_98;
  longlong lStack_90;
  undefined8 local_88;
  undefined8 local_80;
  undefined8 *puStack_78;
  ulonglong local_70;
  longlong lStack_68;
  
  if (DAT_18ab62697 == '\0') {
    FUN_18032f6d0(&DAT_187c21b68);
    FUN_18032f6d0(&DAT_187c332b0);
    FUN_18032f6d0(&DAT_187b41650);
    FUN_18032f6d0(&DAT_187b802a8);
    FUN_18032f6d0(&DAT_187b3fb28);
    FUN_18032f6d0(&DAT_187c1c1c8);
    FUN_18032f6d0(&DAT_187c41e50);
    FUN_18032f6d0(&DAT_187c42000);
    FUN_18032f6d0(&DAT_187c1c898);
    FUN_18032f6d0(&DAT_187bfc580);
    FUN_18032f6d0(&DAT_187c41c98);
    FUN_18032f6d0(&DAT_187b3f978);
    FUN_18032f6d0(&DAT_187c1baf8);
    FUN_18032f6d0(&DAT_187c1cf68);
    FUN_18032f6d0(&DAT_187c421b8);
    FUN_18032f6d0(&DAT_187c42368);
    FUN_18032f6d0(&DAT_187ba6e58);
    FUN_18032f6d0(&DAT_187bea258);
    FUN_18032f6d0(&DAT_187be3458);
    FUN_18032f6d0(&DAT_187b58ff8);
    FUN_18032f6d0(&DAT_187bc4128);
    FUN_18032f6d0(&DAT_187c0ed88);
    FUN_18032f6d0(&DAT_187c00080);
    FUN_18032f6d0(&DAT_187c0ef40);
    FUN_18032f6d0(&DAT_187c00238);
    FUN_18032f6d0(&DAT_187c0f0f0);
    FUN_18032f6d0(&DAT_187c003f0);
    FUN_18032f6d0(&DAT_187c3c750);
    FUN_18032f6d0(&DAT_187bfcff0);
    FUN_18032f6d0(&DAT_187bf8138);
    FUN_18032f6d0(&DAT_187bd2340);
    FUN_18032f6d0(&DAT_187bd62d8);
    FUN_18032f6d0(&DAT_187bd6490);
    FUN_18032f6d0(&DAT_187bd24f8);
    FUN_18032f6d0(&DAT_187c19748);
    FUN_18032f6d0(&DAT_187b69958);
    FUN_18032f6d0(&DAT_187b69b10);
    FUN_18032f6d0(&DAT_187b69798);
    FUN_18032f6d0(&DAT_187b413a0);
    FUN_18032f6d0(&DAT_187b695e0);
    FUN_18032f6d0(&DAT_187b7f908);
    FUN_18032f6d0(&DAT_187c19590);
    FUN_18032f6d0(&DAT_187b6a200);
    FUN_18032f6d0(&DAT_187c307b0);
    FUN_18032f6d0(&DAT_187c31950);
    FUN_18032f6d0(&DAT_187b69cd0);
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c30e80);
    FUN_18032f6d0(&DAT_187b6a3c0);
    FUN_18032f6d0(&DAT_187c31b08);
    FUN_18032f6d0(&DAT_187c30968);
    FUN_18032f6d0(&DAT_187b69e88);
    FUN_18032f6d0(&DAT_187b446b8);
    FUN_18032f6d0(&DAT_187bf1860);
    FUN_18032f6d0(&DAT_187bc09d8);
    FUN_18032f6d0(&DAT_187be2298);
    FUN_18032f6d0(&DAT_187bc5830);
    FUN_18032f6d0(&DAT_187b3c708);
    FUN_18032f6d0(&DAT_187b60fa0);
    FUN_18032f6d0(&DAT_187c013d8);
    FUN_18032f6d0(&DAT_187bf48b8);
    FUN_18032f6d0(&DAT_187bf4a70);
    FUN_18032f6d0(&DAT_187bf4c28);
    FUN_18032f6d0(&DAT_187bf4de0);
    FUN_18032f6d0(&DAT_187bcefe8);
    FUN_18032f6d0(&DAT_187bcf1a0);
    FUN_18032f6d0(&DAT_187c1f720);
    FUN_18032f6d0(&DAT_187bfaba0);
    FUN_18032f6d0(&DAT_187c05928);
    DAT_18ab62697 = '\x01';
  }
  local_a8 = 0;
  puStack_a0 = (undefined8 *)0x0;
  local_98 = 0;
  lStack_90 = 0;
  local_88 = 0;
  local_80 = 0;
  puStack_78 = (undefined8 *)0x0;
  local_70 = 0;
  lStack_68 = 0;
  if (**(longlong **)(DAT_187bfaba0 + 0xb8) == 0) {
    uVar9 = FUN_1847c7d00(param_1,DAT_187bfcff0);
    **(undefined8 **)(DAT_187bfaba0 + 0xb8) = uVar9;
    thunk_FUN_1803b4a20(*(undefined8 *)(DAT_187bfaba0 + 0xb8),uVar9);
    if (**(longlong **)(DAT_187bfaba0 + 0xb8) == 0) goto LAB_1814c1db1;
    uVar9 = FUN_184670860(**(longlong **)(DAT_187bfaba0 + 0xb8),DAT_187bf4c28);
    *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) = uVar9;
    thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8,uVar9);
    if (**(longlong **)(DAT_187bfaba0 + 0xb8) == 0) goto LAB_1814c1db1;
    uVar9 = FUN_184670860(**(longlong **)(DAT_187bfaba0 + 0xb8),DAT_187bf48b8);
    *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x10) = uVar9;
    thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x10,uVar9);
    if (**(longlong **)(DAT_187bfaba0 + 0xb8) == 0) goto LAB_1814c1db1;
    uVar9 = FUN_184670860(**(longlong **)(DAT_187bfaba0 + 0xb8),DAT_187bf4a70);
    *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x18) = uVar9;
    thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x18,uVar9);
    if (**(longlong **)(DAT_187bfaba0 + 0xb8) == 0) goto LAB_1814c1db1;
    uVar9 = FUN_184670860(**(longlong **)(DAT_187bfaba0 + 0xb8),DAT_187bf4de0);
    *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20) = uVar9;
    thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20,uVar9);
  }
  *(longlong *)(param_1 + 0x90) = param_2;
  thunk_FUN_1803b4a20(param_1 + 0x90,param_2);
  if (param_2 != 0) {
    uVar9 = FUN_1814af720(param_2,0);
    *(undefined8 *)(param_1 + 0x20) = uVar9;
    thunk_FUN_1803b4a20(param_1 + 0x20,uVar9);
    uVar9 = thunk_FUN_180383da0(DAT_187bea258);
    FUN_185575ea0(uVar9,DAT_187c41c98);
    *(undefined8 *)(param_1 + 0xb0) = uVar9;
    thunk_FUN_1803b4a20(param_1 + 0xb0,uVar9);
    if (*(longlong *)(param_1 + 0x38) != 0) {
      *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x80) = 0x22;
      if (param_4 == '\0') {
        if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c1db1;
        uVar6 = *(undefined4 *)(*(longlong *)(param_1 + 0x90) + 0x11c);
      }
      else {
        if (param_5 == 0) goto LAB_1814c1db1;
        uVar6 = *(undefined4 *)(param_5 + 0x11c);
      }
      if (*(longlong *)(param_1 + 0x38) != 0) {
        *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x100) = uVar6;
        if (((*(longlong *)(param_1 + 0x90) != 0) &&
            (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar11 != 0)) &&
           (*(longlong *)(param_1 + 0x38) != 0)) {
          *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x7c) = *(undefined4 *)(lVar11 + 0x18);
          if (*(longlong *)(param_1 + 0x38) != 0) {
            *(undefined4 *)(*(longlong *)(param_1 + 0x38) + 0x78) = 1;
            plVar10 = (longlong *)thunk_FUN_180383da0(DAT_187c21b68);
            FUN_183830c40(plVar10,0);
            if (plVar10 != (longlong *)0x0) {
              (**(code **)(*plVar10 + 0x1d8))(plVar10,*(undefined8 *)(*plVar10 + 0x1e0));
              *(undefined4 *)(plVar10 + 2) = 1;
              *(undefined8 *)((longlong)plVar10 + 0x14) = 3;
              *(undefined4 *)((longlong)plVar10 + 0x1c) = 1;
              if ((*(longlong *)(param_1 + 0x90) != 0) &&
                 (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar11 != 0)) {
                *(undefined4 *)(plVar10 + 4) = *(undefined4 *)(lVar11 + 0x18);
                *(undefined4 *)((longlong)plVar10 + 0x24) = 1;
                *(undefined4 *)(plVar10 + 0xc) = 1;
                uVar9 = thunk_FUN_180383da0(DAT_187bf8138);
                FUN_1804383d0(uVar9,0);
                lVar11 = thunk_FUN_180383da0(DAT_187ba6e58);
                FUN_1855de480(lVar11,uVar9,DAT_187b3f978);
                if (lVar11 != 0) {
                  FUN_1855e0980(lVar11,0,0,DAT_187b3fb28);
                  FUN_1855e0980(lVar11,2,0x3ff0000000000000,DAT_187b3fb28);
                  plVar10[0xb] = lVar11;
                  thunk_FUN_1803b4a20(plVar10 + 0xb,lVar11);
                  *(undefined4 *)((longlong)plVar10 + 0x2c) = *(undefined4 *)(param_2 + 0x4b0);
                  local_f8[0] = 0;
                  if (*(char *)(param_1 + 0x30) != '\0') {
                    if (*(int *)(DAT_187b41650 + 0xe4) == 0) {
                      il2cpp_runtime_class_init(DAT_187b41650);
                    }
                    lVar11 = *(longlong *)(*(longlong *)(DAT_187b41650 + 0xb8) + 0x58);
                    if (param_4 == '\0') {
                      if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c1db1;
                      uVar6 = *(undefined4 *)(*(longlong *)(param_1 + 0x90) + 0x120);
                    }
                    else {
                      if (param_5 == 0) goto LAB_1814c1db1;
                      uVar6 = *(undefined4 *)(param_5 + 0x120);
                    }
                    if (lVar11 == 0) goto LAB_1814c1db1;
                    cVar4 = FUN_1855eb450(lVar11,uVar6,local_f8,DAT_187bfc580);
                    if (cVar4 != '\0') {
                      lVar11 = thunk_FUN_180383da0(DAT_187bc09d8);
                      FUN_1849de5b0(lVar11,DAT_187b695e0);
                      lVar12 = thunk_FUN_180383da0(DAT_187c332b0);
                      *(undefined8 *)(lVar12 + 0x18) = 0x3ff0000000000000;
                      *(undefined4 *)(lVar12 + 0x20) = 0x3f800000;
                      FUN_180443e50(lVar12,0);
                      *(undefined8 *)(lVar12 + 0x18) = 0x3ff0000000000000;
                      *(undefined4 *)(lVar12 + 0x14) = 0;
                      if (lVar11 != 0) {
                        FUN_180002460(lVar11,lVar12,DAT_187b69958);
                        plVar10[0xe] = lVar11;
                        thunk_FUN_1803b4a20(plVar10 + 0xe,lVar11);
                        lVar11 = FUN_18032e960(DAT_187c013d8,1);
                        if (lVar11 != 0) {
                          if (*(int *)(lVar11 + 0x18) == 0) {
LAB_1814c1db7:
                    /* WARNING: Subroutine does not return */
                            FUN_18032f9c0();
                          }
                          *(undefined4 *)(lVar11 + 0x20) = local_f8[0];
                          plVar10[0xd] = lVar11;
                          thunk_FUN_1803b4a20(plVar10 + 0xd,lVar11);
                          plVar10[0xf] = 0x3ff0000000000000;
LAB_1814c0eeb:
                          lVar11 = thunk_FUN_180383da0(DAT_187bf1860);
                          FUN_1849de5b0(lVar11,DAT_187b413a0);
                          plVar10[0x11] = lVar11;
                          thunk_FUN_1803b4a20(plVar10 + 0x11,lVar11);
                          uVar6 = *(undefined4 *)(param_2 + 0x488);
                          if (*(int *)(DAT_187c05928 + 0xe4) == 0) {
                            il2cpp_runtime_class_init();
                          }
                          iVar5 = FUN_18403db00(uVar6,0);
                          *(int *)(plVar10 + 7) = iVar5 * 10000;
                          lVar11 = thunk_FUN_180383da0(DAT_187bc5830);
                          FUN_1849de5b0(lVar11,DAT_187b7f908);
                          plVar10[10] = lVar11;
                          thunk_FUN_1803b4a20(plVar10 + 10,lVar11);
                          lVar11 = *(longlong *)(param_1 + 0x38);
                          uVar9 = thunk_FUN_180383da0(DAT_187be2298);
                          FUN_1849de5b0(uVar9,DAT_187c19590);
                          if (lVar11 != 0) {
                            *(undefined8 *)(lVar11 + 0x20) = uVar9;
                            thunk_FUN_1803b4a20(lVar11 + 0x20,uVar9);
                            lVar11 = DAT_187c19748;
                            if ((*(longlong *)(param_1 + 0x38) != 0) &&
                               (lVar12 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x20),
                               lVar12 != 0)) {
                              *(int *)(lVar12 + 0x1c) = *(int *)(lVar12 + 0x1c) + 1;
                              lVar14 = *(longlong *)(lVar12 + 0x10);
                              uVar17 = *(uint *)(lVar12 + 0x18);
                              if (lVar14 != 0) {
                                if (uVar17 < *(uint *)(lVar14 + 0x18)) {
                                  *(uint *)(lVar12 + 0x18) = uVar17 + 1;
                                  if (*(uint *)(lVar14 + 0x18) <= uVar17) goto LAB_1814c1db7;
                                  *(longlong **)(lVar14 + 0x20 + (longlong)(int)uVar17 * 8) =
                                       plVar10;
                                  thunk_FUN_1803b4a20(lVar14 + 0x20 + (longlong)(int)uVar17 * 8,
                                                      plVar10);
                                }
                                else {
                                  FUN_184a97470(lVar12,plVar10,
                                                *(undefined8 *)
                                                 (*(longlong *)(*(longlong *)(lVar11 + 0x20) + 0xc0)
                                                 + 0x70));
                                }
                                if (*(longlong *)(param_1 + 0xb0) != 0) {
                                  FUN_1855ac5e0(*(longlong *)(param_1 + 0xb0),1,
                                                *(undefined8 *)(param_1 + 0x38),DAT_187c41e50);
                                  FUN_1814bce50(param_1,*(undefined8 *)(param_1 + 0x38),0);
                                  FUN_1814bd520(param_1,*(undefined8 *)(param_1 + 0x38),0);
                                  if (param_4 != '\0') {
                                    return;
                                  }
                                  if (param_3 != 0) {
                                    lVar11 = *(longlong *)(param_3 + 0x38);
                                    if (*(longlong *)(param_1 + 0x40) != 0) {
                                      if (*(int *)(*(longlong *)(param_1 + 0x40) + 0x18) != 0) {
                                        if (lVar11 == 0) goto LAB_1814c1db1;
                                        lVar12 = *(longlong *)(lVar11 + 0x10);
                                        lVar14 = *(longlong *)(param_1 + 0xb0);
                                        if (lVar12 == 0) goto LAB_1814c1db1;
                                        uVar6 = FUN_184a610f0(lVar12,0,DAT_187b446b8);
                                        if ((*(longlong *)(param_1 + 0x40) == 0) ||
                                           (uVar9 = FUN_1849ed2d0(*(longlong *)(param_1 + 0x40),0,
                                                                  DAT_187c30968), lVar14 == 0))
                                        goto LAB_1814c1db1;
                                        FUN_1855ac5e0(lVar14,uVar6,uVar9,DAT_187c41e50);
                                        iVar5 = FUN_184a610f0(lVar12,0,DAT_187b446b8);
                                        *(bool *)(param_1 + 0xb9) = iVar5 != 0;
                                        if ((*(longlong *)(param_1 + 0x40) == 0) ||
                                           (lVar12 = FUN_1849ed2d0(*(longlong *)(param_1 + 0x40),0,
                                                                   DAT_187c30968), lVar12 == 0))
                                        goto LAB_1814c1db1;
                                        *(undefined4 *)(lVar12 + 0x94) = 0;
                                      }
                                      if ((*(longlong *)(param_1 + 0x60) != 0) && (lVar11 != 0)) {
                                        if (*(int *)(*(longlong *)(param_1 + 0x60) + 0x18) != 0) {
                                          if (*(longlong *)(lVar11 + 0x48) == 0) goto LAB_1814c1db1;
                                          uVar6 = FUN_184a610f0(*(longlong *)(lVar11 + 0x48),0,
                                                                DAT_187b446b8);
                                          lVar12 = *(longlong *)(param_1 + 0xb0);
                                          if ((*(longlong *)(param_1 + 0x60) == 0) ||
                                             (uVar9 = FUN_1849ed2d0(*(longlong *)(param_1 + 0x60),0,
                                                                    DAT_187c30968), lVar12 == 0))
                                          goto LAB_1814c1db1;
                                          FUN_1855ac5e0(lVar12,uVar6,uVar9,DAT_187c41e50);
                                          if ((*(longlong *)(param_1 + 0x60) == 0) ||
                                             (lVar12 = FUN_1849ed2d0(*(longlong *)(param_1 + 0x60),0
                                                                     ,DAT_187c30968), lVar12 == 0))
                                          goto LAB_1814c1db1;
                                          *(undefined4 *)(lVar12 + 0x94) = 0;
                                        }
                                        lVar12 = *(longlong *)(lVar11 + 0x28);
                                        iVar5 = 0;
                                        if (lVar12 != 0) {
                                          iVar8 = *(int *)(lVar12 + 0x18);
                                          if (0 < iVar8) {
                                            do {
                                              iVar7 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8);
                                              if (iVar7 != 0) {
                                                if ((*(longlong *)(param_1 + 0x48) == 0) ||
                                                   (lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                            (param_1 + 0x48),iVar5,
                                                                           DAT_187c30968),
                                                   lVar14 == 0)) goto LAB_1814c1db1;
                                                *(int *)(lVar14 + 0x94) = iVar5 + 1;
                                                lVar14 = *(longlong *)(param_1 + 0xb0);
                                                uVar6 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8);
                                                if ((*(longlong *)(param_1 + 0x48) == 0) ||
                                                   (uVar9 = FUN_1849ed2d0(*(longlong *)
                                                                           (param_1 + 0x48),iVar5,
                                                                          DAT_187c30968),
                                                   lVar14 == 0)) goto LAB_1814c1db1;
                                                FUN_1855ac5e0(lVar14,uVar6,uVar9,DAT_187c41e50);
                                              }
                                              iVar5 = iVar5 + 1;
                                            } while (iVar5 < iVar8);
                                          }
                                          lVar12 = *(longlong *)(lVar11 + 0x50);
                                          iVar5 = 0;
                                          if (lVar12 != 0) {
                                            for (; iVar5 < *(int *)(lVar12 + 0x18);
                                                iVar5 = iVar5 + 1) {
                                              iVar8 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8);
                                              if (iVar8 != 0) {
                                                if ((*(longlong *)(param_1 + 0x68) == 0) ||
                                                   (lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                            (param_1 + 0x68),iVar5,
                                                                           DAT_187c30968),
                                                   lVar14 == 0)) goto LAB_1814c1db1;
                                                *(int *)(lVar14 + 0x94) = iVar5 + 1;
                                                lVar14 = *(longlong *)(param_1 + 0xb0);
                                                uVar6 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8);
                                                if ((*(longlong *)(param_1 + 0x68) == 0) ||
                                                   (uVar9 = FUN_1849ed2d0(*(longlong *)
                                                                           (param_1 + 0x68),iVar5,
                                                                          DAT_187c30968),
                                                   lVar14 == 0)) goto LAB_1814c1db1;
                                                FUN_1855ac5e0(lVar14,uVar6,uVar9,DAT_187c41e50);
                                              }
                                            }
                                            lVar12 = *(longlong *)(lVar11 + 0x58);
                                            iVar5 = 0;
                                            if (lVar12 != 0) {
                                              for (; iVar5 < *(int *)(lVar12 + 0x18);
                                                  iVar5 = iVar5 + 1) {
                                                iVar8 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8);
                                                if (iVar8 != 0) {
                                                  if ((*(longlong *)(param_1 + 0x78) == 0) ||
                                                     (lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                              (param_1 + 0x78),iVar5
                                                                             ,DAT_187c30968),
                                                     lVar14 == 0)) goto LAB_1814c1db1;
                                                  *(int *)(lVar14 + 0x94) = iVar5 + 1;
                                                  lVar14 = *(longlong *)(param_1 + 0xb0);
                                                  uVar6 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8);
                                                  if ((*(longlong *)(param_1 + 0x78) == 0) ||
                                                     (uVar9 = FUN_1849ed2d0(*(longlong *)
                                                                             (param_1 + 0x78),iVar5,
                                                                            DAT_187c30968),
                                                     lVar14 == 0)) goto LAB_1814c1db1;
                                                  FUN_1855ac5e0(lVar14,uVar6,uVar9,DAT_187c41e50);
                                                }
                                              }
                                              lVar12 = *(longlong *)(lVar11 + 0x30);
                                              iVar5 = 0;
                                              if (lVar12 != 0) {
                                                iVar8 = *(int *)(lVar12 + 0x18);
                                                if (0 < iVar8) {
                                                  do {
                                                    iVar7 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8
                                                                         );
                                                    if (iVar7 == 0) break;
                                                    if ((*(longlong *)(param_1 + 0x50) == 0) ||
                                                       (lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                                (param_1 + 0x50),
                                                                               iVar5,DAT_187c30968),
                                                       lVar14 == 0)) goto LAB_1814c1db1;
                                                    *(int *)(lVar14 + 0x94) = iVar5 + 1;
                                                    lVar14 = *(longlong *)(param_1 + 0xb0);
                                                    uVar6 = FUN_184a610f0(lVar12,iVar5,DAT_187b446b8
                                                                         );
                                                    if ((*(longlong *)(param_1 + 0x50) == 0) ||
                                                       (uVar9 = FUN_1849ed2d0(*(longlong *)
                                                                               (param_1 + 0x50),
                                                                              iVar5,DAT_187c30968),
                                                       lVar14 == 0)) goto LAB_1814c1db1;
                                                    FUN_1855ac5e0(lVar14,uVar6,uVar9,DAT_187c41e50);
                                                    iVar5 = iVar5 + 1;
                                                  } while (iVar5 < iVar8);
                                                }
                                                lVar12 = *(longlong *)(lVar11 + 0x38);
                                                iVar5 = 0;
                                                if (lVar12 != 0) {
                                                  while ((iVar5 < *(int *)(lVar12 + 0x18) &&
                                                         (iVar8 = FUN_184a610f0(lVar12,iVar5,
                                                                                DAT_187b446b8),
                                                         iVar8 != 0))) {
                                                    if ((*(longlong *)(param_1 + 0x58) == 0) ||
                                                       (lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                                (param_1 + 0x58),
                                                                               iVar5,DAT_187c30968),
                                                       lVar14 == 0)) goto LAB_1814c1db1;
                                                    *(int *)(lVar14 + 0x94) = iVar5 + 1;
                                                    lVar14 = *(longlong *)(param_1 + 0xb0);
                                                    if ((*(longlong *)(param_1 + 0x58) == 0) ||
                                                       (uVar9 = FUN_1849ed2d0(*(longlong *)
                                                                               (param_1 + 0x58),
                                                                              iVar5,DAT_187c30968),
                                                       lVar14 == 0)) goto LAB_1814c1db1;
                                                    FUN_1855ac5e0(lVar14,iVar8,uVar9,DAT_187c41e50);
                                                    iVar5 = iVar5 + 1;
                                                  }
                                                  lVar12 = *(longlong *)(lVar11 + 0x40);
                                                  iVar5 = 0;
                                                  if (lVar12 != 0) {
                                                    while ((iVar5 < *(int *)(lVar12 + 0x18) &&
                                                           (iVar8 = FUN_184a610f0(lVar12,iVar5,
                                                                                  DAT_187b446b8),
                                                           iVar8 != 0))) {
                                                      if ((*(longlong *)(param_1 + 0x80) == 0) ||
                                                         (lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                                  (param_1 + 0x80),
                                                                                 iVar5,DAT_187c30968
                                                                                ), lVar14 == 0))
                                                      goto LAB_1814c1db1;
                                                      *(int *)(lVar14 + 0x94) = iVar5 + 1;
                                                      lVar14 = *(longlong *)(param_1 + 0xb0);
                                                      if ((*(longlong *)(param_1 + 0x80) == 0) ||
                                                         (uVar9 = FUN_1849ed2d0(*(longlong *)
                                                                                 (param_1 + 0x80),
                                                                                iVar5,DAT_187c30968)
                                                         , lVar14 == 0)) goto LAB_1814c1db1;
                                                      FUN_1855ac5e0(lVar14,iVar8,uVar9,DAT_187c41e50
                                                                   );
                                                      iVar5 = iVar5 + 1;
                                                    }
                                                    lVar12 = *(longlong *)(lVar11 + 0x60);
                                                    iVar5 = 0;
                                                    if (lVar12 != 0) {
                                                      while ((iVar5 < *(int *)(lVar12 + 0x18) &&
                                                             (iVar8 = FUN_184a610f0(lVar12,iVar5,
                                                                                    DAT_187b446b8),
                                                             iVar8 != 0))) {
                                                        if ((*(longlong *)(param_1 + 0x70) == 0) ||
                                                           (lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                                    (param_1 + 0x70)
                                                                                   ,iVar5,
                                                  DAT_187c30968), lVar14 == 0)) goto LAB_1814c1db1;
                                                  *(int *)(lVar14 + 0x94) = iVar5;
                                                  lVar14 = *(longlong *)(param_1 + 0xb0);
                                                  if ((*(longlong *)(param_1 + 0x70) == 0) ||
                                                     (uVar9 = FUN_1849ed2d0(*(longlong *)
                                                                             (param_1 + 0x70),iVar5,
                                                                            DAT_187c30968),
                                                     lVar14 == 0)) goto LAB_1814c1db1;
                                                  FUN_1855ac5e0(lVar14,iVar8,uVar9,DAT_187c41e50);
                                                  iVar5 = iVar5 + 1;
                                                  }
                                                  uVar15 = thunk_FUN_180383da0(DAT_187be3458);
                                                  FUN_185575ea0(uVar15,DAT_187c1baf8);
                                                  local_f0 = uVar15;
                                                  if (*(longlong *)(param_1 + 0xb0) != 0) {
                                                    FUN_1855337a0(&local_e8,
                                                                  *(longlong *)(param_1 + 0xb0),
                                                                  DAT_187c42000);
                                                    local_a8 = local_e8;
                                                    puStack_a0 = puStack_e0;
                                                    local_98 = local_d8;
                                                    lStack_90 = lStack_d0;
                                                    local_88 = local_c8;
                                                    local_e8 = 0;
                                                    puStack_e0 = &local_a8;
LAB_1814c17a0:
                                                    cVar4 = FUN_185760f30(&local_a8,DAT_187c0ef40);
                                                    lVar12 = lStack_90;
                                                    uVar3 = local_98;
                                                    if (cVar4 != '\0') {
                                                      if (((int)local_98 != 1) &&
                                                         ((int)local_98 != 0)) {
                                                        if (*(longlong *)(param_1 + 0x90) == 0) {
                    /* WARNING: Subroutine does not return */
                                                          FUN_18032f9d0();
                                                        }
                                                        lVar14 = *(longlong *)
                                                                  (*(longlong *)(param_1 + 0x90) +
                                                                  0x500);
                                                        if (lVar14 == 0) {
                    /* WARNING: Subroutine does not return */
                                                          FUN_18032f9d0();
                                                        }
                                                        iVar5 = FUN_1855ac210(lVar14,local_98 &
                                                                                     0xffffffff);
                                                        if (iVar5 != 0) {
                                                          lVar14 = FUN_180003d20(DAT_187b3c708);
                                                          if (lVar14 == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0();
                                                          }
                                                          if (*(longlong *)(lVar14 + 0x3d8) == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0();
                                                          }
                                                          uVar9 = FUN_182b00540(*(longlong *)
                                                                                 (lVar14 + 0x3d8),
                                                                                uVar3 & 0xffffffff,0
                                                                               );
                                                          FUN_1814cb840(param_1,lVar12,uVar9,
                                                                        0xffffffff,0);
                                                          if (*(longlong *)(param_1 + 0x90) == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0();
                                                          }
                                                          lVar14 = *(longlong *)
                                                                    (*(longlong *)(param_1 + 0x90) +
                                                                    0x500);
                                                          if (lVar14 == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0();
                                                          }
                                                          uVar6 = FUN_1855ac210(lVar14,uVar3 & 
                                                  0xffffffff,DAT_187c1cf68);
                                                  if (lVar12 == 0) {
                    /* WARNING: Subroutine does not return */
                                                    FUN_18032f9d0();
                                                  }
                                                  FUN_1814b5f20(lVar12,uVar6,0);
                                                  lVar14 = FUN_180003d20(DAT_187b3c708);
                                                  if (lVar14 == 0) {
                    /* WARNING: Subroutine does not return */
                                                    FUN_18032f9d0();
                                                  }
                                                  if (*(longlong *)(lVar14 + 0x418) == 0) {
                    /* WARNING: Subroutine does not return */
                                                    FUN_18032f9d0();
                                                  }
                                                  uVar9 = FUN_1833947a0(*(longlong *)
                                                                         (lVar14 + 0x418),
                                                                        uVar3 & 0xffffffff,0);
                                                  if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
                                                    il2cpp_runtime_class_init();
                                                  }
                                                  lVar14 = *(longlong *)
                                                            (*(longlong *)(DAT_187c1f720 + 0xb8) +
                                                            0x10);
                                                  if (lVar14 == 0) {
                                                    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
                                                      il2cpp_runtime_class_init();
                                                    }
                                                    uVar13 = **(undefined8 **)(DAT_187c1f720 + 0xb8)
                                                    ;
                                                    lVar14 = thunk_FUN_180383da0(DAT_187c3c750);
                                                    FUN_18594e530(lVar14,uVar13,DAT_187bcf1a0,0);
                                                    *(longlong *)
                                                     (*(longlong *)(DAT_187c1f720 + 0xb8) + 0x10) =
                                                         lVar14;
                                                    thunk_FUN_1803b4a20(*(longlong *)
                                                                         (DAT_187c1f720 + 0xb8) +
                                                                        0x10,lVar14);
                                                  }
                                                  uVar9 = FUN_1847a0cb0(uVar9,lVar14);
                                                  lVar14 = FUN_1847b2640(uVar9,DAT_187bc4128);
                                                  iVar8 = 0;
                                                  if (lVar14 == 0) {
                    /* WARNING: Subroutine does not return */
                                                    FUN_18032f9d0();
                                                  }
                                                  for (; iVar8 < *(int *)(lVar14 + 0x18);
                                                      iVar8 = iVar8 + 1) {
                                                    lVar16 = FUN_1849ed2d0(lVar14,iVar8);
                                                    if (lVar16 == 0) {
                    /* WARNING: Subroutine does not return */
                                                      FUN_18032f9d0();
                                                    }
                                                    uVar9 = *(undefined8 *)(lVar16 + 0x34);
                                                    uVar6 = *(undefined4 *)(lVar16 + 0x3c);
                                                    if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
                                                      il2cpp_runtime_class_init();
                                                    }
                                                    local_b8 = uVar9;
                                                    local_b0 = uVar6;
                                                    iVar7 = FUN_183aa36e0(&local_b8,0);
                                                    if (iVar5 < iVar7) {
                                                      if (iVar8 != 0) {
                                                        if (uVar15 == 0) {
                    /* WARNING: Subroutine does not return */
                                                          FUN_18032f9d0();
                                                        }
                                                        FUN_18559d660(uVar15,*(undefined4 *)
                                                                              (lVar12 + 0x78),
                                                                      iVar8 + -1,DAT_187c1c1c8);
                                                      }
                                                      break;
                                                    }
                                                    if (*(int *)(lVar14 + 0x18) == iVar8 + 1) {
                                                      if (uVar15 == 0) {
                    /* WARNING: Subroutine does not return */
                                                        FUN_18032f9d0();
                                                      }
                                                      FUN_18559d660(uVar15,*(undefined4 *)
                                                                            (lVar12 + 0x78),iVar8,
                                                                    DAT_187c1c1c8);
                                                    }
                                                  }
                                                  }
                                                  }
                                                  goto LAB_1814c17a0;
                                                  }
                                                  FUN_180443e50(&local_a8,DAT_187c0ed88);
                                                  lVar12 = *(longlong *)(DAT_187b3c708 + 0x20);
                                                  if ((*(byte *)(lVar12 + 0x135) & 1) == 0) {
                                                    lVar12 = FUN_18032d010(lVar12);
                                                  }
                                                  lVar12 = *(longlong *)
                                                            (*(longlong *)(lVar12 + 0xc0) + 0x10);
                                                  if ((*(byte *)(lVar12 + 0x135) & 1) == 0) {
                                                    lVar12 = FUN_18032d010(lVar12);
                                                  }
                                                  if (**(longlong **)(lVar12 + 0xb8) != 0) {
                                                    lVar12 = *(longlong *)
                                                              (**(longlong **)(lVar12 + 0xb8) +
                                                              0x100);
                                                    if ((*(longlong *)(param_1 + 0x90) != 0) &&
                                                       (lVar12 != 0)) {
                                                      lVar12 = FUN_181e4fb70(lVar12,*(undefined4 *)
                                                                                     (*(longlong *)
                                                                                       (param_1 +
                                                                                       0x90) + 0x488
                                                                                     ),0);
                                                      if (lVar12 == 0) {
                                                        if (uVar15 == 0) goto LAB_1814c1db1;
                                                        FUN_185557680(&local_e8,uVar15,DAT_187c1c898
                                                                     );
                                                        local_80 = local_e8;
                                                        puStack_78 = puStack_e0;
                                                        local_70 = local_d8;
                                                        lStack_68 = lStack_d0;
                                                        local_e8 = 0;
                                                        puStack_e0 = &local_80;
                                                        while (cVar4 = FUN_185747e60(&local_80,
                                                                                     DAT_187c00238),
                                                              uVar15 = local_70, cVar4 != '\0') {
                                                          local_f0 = local_70;
                                                          lVar12 = *(longlong *)(param_1 + 0xb0);
                                                          if (lVar12 == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0(0);
                                                          }
                                                          lVar14 = FUN_1855bdd60(lVar12,local_70 &
                                                                                        0xffffffff,
                                                                                 DAT_187c421b8);
                                                          if (lVar14 == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0();
                                                          }
                                                          if (*(longlong *)(lVar14 + 0x1a0) == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0();
                                                          }
                                                          lVar14 = FUN_1849ed2d0(*(longlong *)
                                                                                  (lVar14 + 0x1a0),
                                                                                 local_f0._4_4_,
                                                                                 DAT_187c30e80);
                                                          if (lVar14 == 0) {
                    /* WARNING: Subroutine does not return */
                                                            FUN_18032f9d0();
                                                          }
                                                          if (DAT_18ab62689 == '\0') {
                                                            FUN_18032f6d0(&DAT_187b778e8);
                                                            DAT_18ab62689 = '\x01';
                                                          }
                                                          uVar9 = thunk_FUN_180383da0(DAT_187b778e8)
                                                          ;
                                                          FUN_1814b6840(uVar9,lVar14,0);
                                                          FUN_1855bde00(lVar12,uVar15 & 0xffffffff,
                                                                        uVar9,DAT_187c42368);
                                                        }
                                                        FUN_180443e50(&local_80,DAT_187c00080);
                                                      }
                                                      if (*(longlong *)(param_1 + 0x40) != 0) {
                                                        if (*(int *)(*(longlong *)(param_1 + 0x40) +
                                                                    0x18) != 0) {
                                                          if ((lVar11 == 0) ||
                                                             (*(longlong *)(lVar11 + 0x10) == 0))
                                                          goto LAB_1814c1db1;
                                                          uVar6 = FUN_184a610f0(*(longlong *)
                                                                                 (lVar11 + 0x10),0,
                                                                                DAT_187b446b8);
                                                          if (DAT_18ab626a4 == '\0') {
                                                            FUN_18032f6d0(&DAT_187c421b8);
                                                            DAT_18ab626a4 = '\x01';
                                                          }
                                                          if ((*(longlong *)(param_1 + 0xb0) == 0)
                                                             || (lVar12 = FUN_1855bdd60(*(longlong *
                                                                                         )(param_1 +
                                                                                          0xb0),
                                                  uVar6,DAT_187c421b8), lVar12 == 0))
                                                  goto LAB_1814c1db1;
                                                  if (*(char *)(lVar12 + 0x10) != '\0') {
                                                    *(undefined4 *)(lVar12 + 0xd0) = 0;
                                                  }
                                                  }
                                                  if (*(longlong *)(param_1 + 0x60) != 0) {
                                                    if (*(int *)(*(longlong *)(param_1 + 0x60) +
                                                                0x18) == 0) {
                                                      return;
                                                    }
                                                    if ((lVar11 != 0) &&
                                                       (*(longlong *)(lVar11 + 0x48) != 0)) {
                                                      uVar6 = FUN_184a610f0(*(longlong *)
                                                                             (lVar11 + 0x48),0,
                                                                            DAT_187b446b8);
                                                      if (DAT_18ab626a4 == '\0') {
                                                        FUN_18032f6d0(&DAT_187c421b8);
                                                        DAT_18ab626a4 = '\x01';
                                                      }
                                                      if ((*(longlong *)(param_1 + 0xb0) != 0) &&
                                                         (lVar11 = FUN_1855bdd60(*(longlong *)
                                                                                  (param_1 + 0xb0),
                                                                                 uVar6,DAT_187c421b8
                                                                                ), lVar11 != 0)) {
                                                        if (*(char *)(lVar11 + 0x10) == '\0') {
                                                          return;
                                                        }
                                                        *(undefined4 *)(lVar11 + 0xd0) = 0;
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
                      goto LAB_1814c1db1;
                    }
                  }
                  lVar11 = *(longlong *)(param_1 + 0x28);
                  if (lVar11 != 0) {
                    uVar9 = *(undefined8 *)(lVar11 + 0x20);
                    uVar13 = thunk_FUN_180383da0(DAT_187bc09d8);
                    FUN_184aa4390(uVar13,uVar9,DAT_187b69798);
                    *(undefined8 *)(lVar11 + 0x18) = uVar13;
                    thunk_FUN_1803b4a20(lVar11 + 0x18,uVar13);
                    iVar5 = 0;
                    if (*(longlong *)(lVar11 + 0x28) != 0) {
                      iVar8 = *(int *)(*(longlong *)(lVar11 + 0x28) + 0x18);
                      if (0 < iVar8) {
                        do {
                          if (*(longlong *)(lVar11 + 0x28) == 0) goto LAB_1814c1db1;
                          lVar12 = FUN_1849ed2d0(*(longlong *)(lVar11 + 0x28),iVar5,DAT_187b6a3c0);
                          iVar7 = 0;
                          if (lVar12 == 0) goto LAB_1814c1db1;
                          for (; iVar7 < *(int *)(lVar12 + 0x1c); iVar7 = iVar7 + 1) {
                            if (*(int *)(lVar12 + 0x20) == 0) {
                              lVar14 = *(longlong *)(lVar11 + 0x18);
                              lVar16 = thunk_FUN_180383da0(DAT_187c332b0);
                              *(undefined8 *)(lVar16 + 0x18) = 0x3ff0000000000000;
                              *(undefined4 *)(lVar16 + 0x20) = 0x3f800000;
                              FUN_180443e50(lVar16,0);
                              *(float *)(lVar16 + 0x10) =
                                   (float)iVar7 * *(float *)(lVar12 + 0x14) +
                                   *(float *)(lVar12 + 0x10);
LAB_1814c0d0e:
                              *(undefined4 *)(lVar16 + 0x14) = 1;
                              if (lVar14 == 0) goto LAB_1814c1db1;
                              FUN_180002460(lVar14);
                            }
                            else if (*(int *)(lVar12 + 0x20) == 1) {
                              lVar14 = *(longlong *)(lVar11 + 0x18);
                              lVar16 = thunk_FUN_180383da0(DAT_187c332b0);
                              *(undefined8 *)(lVar16 + 0x18) = 0x3ff0000000000000;
                              *(undefined4 *)(lVar16 + 0x20) = 0x3f800000;
                              FUN_180443e50(lVar16,0);
                              fVar1 = *(float *)(lVar12 + 0x10);
                              if (*(longlong *)(lVar12 + 0x28) != 0) {
                                fVar18 = (float)FUN_1861c11f0(*(longlong *)(lVar12 + 0x28),
                                                              (float)iVar7 /
                                                              (float)*(int *)(lVar12 + 0x1c),0);
                                *(float *)(lVar16 + 0x10) =
                                     (float)*(int *)(lVar12 + 0x1c) * fVar18 *
                                     *(float *)(lVar12 + 0x14) + fVar1;
                                goto LAB_1814c0d0e;
                              }
                              goto LAB_1814c1db1;
                            }
                          }
                          iVar5 = iVar5 + 1;
                        } while (iVar5 < iVar8);
                      }
                      lVar12 = *(longlong *)(lVar11 + 0x18);
                      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
                        il2cpp_runtime_class_init();
                      }
                      lVar14 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 8);
                      if (lVar14 == 0) {
                        if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
                          il2cpp_runtime_class_init();
                        }
                        uVar9 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
                        lVar14 = thunk_FUN_180383da0(DAT_187b802a8);
                        FUN_1854e7780(lVar14,uVar9,DAT_187bcefe8,0);
                        *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 8) = lVar14;
                        thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 8,lVar14);
                      }
                      if (lVar12 != 0) {
                        FUN_184a9fb90(lVar12,lVar14,DAT_187b69b10);
                        plVar10[0xe] = *(longlong *)(lVar11 + 0x18);
                        thunk_FUN_1803b4a20(plVar10 + 0xe);
                        if (*(longlong *)(lVar11 + 0x18) != 0) {
                          lVar12 = FUN_18032e960(DAT_187c013d8,
                                                 *(undefined4 *)
                                                  (*(longlong *)(lVar11 + 0x18) + 0x18));
                          plVar10[0xd] = lVar12;
                          thunk_FUN_1803b4a20(plVar10 + 0xd,lVar12);
                          uVar17 = 0;
                          while (lVar12 = *(longlong *)(lVar11 + 0x18), lVar12 != 0) {
                            if (*(int *)(lVar12 + 0x18) <= (int)uVar17) goto LAB_1814c0eeb;
                            dVar2 = (double)plVar10[0xf];
                            lVar12 = FUN_1849ed2d0(lVar12,uVar17,DAT_187b69e88);
                            if (lVar12 == 0) break;
                            plVar10[0xf] = (longlong)(dVar2 + *(double *)(lVar12 + 0x18));
                            lVar12 = plVar10[0xd];
                            if (((*(longlong *)(lVar11 + 0x18) == 0) ||
                                (lVar14 = FUN_1849ed2d0(*(longlong *)(lVar11 + 0x18),uVar17,
                                                        DAT_187b69e88), lVar14 == 0)) ||
                               (lVar12 == 0)) break;
                            if (*(uint *)(lVar12 + 0x18) <= uVar17) goto LAB_1814c1db7;
                            *(undefined4 *)(lVar12 + 0x20 + (longlong)(int)uVar17 * 4) =
                                 *(undefined4 *)(lVar14 + 0x10);
                            uVar17 = uVar17 + 1;
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
LAB_1814c1db1:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::InitializeAction  @ 0x14c0210  size=160 ==== */

void FUN_1814c0210(longlong param_1,longlong param_2)

{
  longlong lVar1;
  longlong *plVar2;
  int iVar3;
  
  if (DAT_18ab6269d == '\0') {
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c19e18);
    DAT_18ab6269d = '\x01';
  }
  iVar3 = 0;
  if (param_2 != 0) {
    while (lVar1 = *(longlong *)(param_2 + 0x20), lVar1 != 0) {
      if (*(int *)(lVar1 + 0x18) <= iVar3) {
        return;
      }
      plVar2 = (longlong *)FUN_1849ed2d0(lVar1,iVar3,DAT_187c19e18);
      if (plVar2 == (longlong *)0x0) break;
      (**(code **)(*plVar2 + 0x248))
                (plVar2,*(undefined8 *)(param_1 + 0x90),*(undefined8 *)(*plVar2 + 0x250));
      iVar3 = iVar3 + 1;
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::IsLoopMotionPlaying  @ 0x14c1e30  size=240 ==== */

void FUN_1814c1e30(longlong param_1,undefined4 param_2)

{
  undefined4 uVar1;
  undefined4 uVar2;
  longlong lVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  
  if (DAT_18ab626c3 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626c3 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar3 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar3 != 0) {
      uVar1 = *(undefined4 *)(lVar3 + 0x80);
      uVar2 = *(undefined4 *)(lVar3 + 0x94);
      if (*(longlong *)(param_1 + 0x90) != 0) {
        lVar3 = FUN_1815f9970(*(longlong *)(param_1 + 0x90),0);
        if (lVar3 != 0) {
          uVar4 = FUN_1804815e0(lVar3,0);
          if (*(longlong *)(param_1 + 0x90) != 0) {
            lVar3 = FUN_1815f9970(*(longlong *)(param_1 + 0x90),0);
            if (lVar3 != 0) {
              uVar5 = FUN_183dd29a0(lVar3,uVar1,uVar2,1,0xffffffff,0);
              FUN_185a26dd0(uVar4,uVar5,0);
              return;
            }
          }
        }
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::IsModeChange  @ 0x14c1f30  size=97 ==== */

undefined1 FUN_1814c1f30(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626c7 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626c7 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      return *(undefined1 *)(lVar1 + 0xf4);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::OnDestroy  @ 0x14c1fa0  size=259 ==== */

void FUN_1814c1fa0(longlong param_1)

{
  *(undefined8 *)(param_1 + 0x28) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x28,0);
  *(undefined8 *)(param_1 + 0x38) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x38,0);
  *(undefined8 *)(param_1 + 0x40) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x40,0);
  *(undefined8 *)(param_1 + 0x48) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x48,0);
  *(undefined8 *)(param_1 + 0x50) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x50,0);
  *(undefined8 *)(param_1 + 0x58) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x58,0);
  *(undefined8 *)(param_1 + 0x60) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x60,0);
  *(undefined8 *)(param_1 + 0x68) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x68,0);
  *(undefined8 *)(param_1 + 0x70) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x70,0);
  *(undefined8 *)(param_1 + 0x78) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x78,0);
  *(undefined8 *)(param_1 + 0x80) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x80,0);
  *(undefined8 *)(param_1 + 0x90) = 0;
  thunk_FUN_1803b4a20(param_1 + 0x90,0);
  *(undefined8 *)(param_1 + 0xb0) = 0;
  thunk_FUN_1803b4a20(param_1 + 0xb0,0);
  *(undefined8 *)(param_1 + 0xa0) = 0;
  thunk_FUN_1803b4a20(param_1 + 0xa0,0);
  return;
}



/* ==== UnitActionController::SearchTargetByAction  @ 0x14c20b0  size=1322 ==== */

void FUN_1814c20b0(longlong param_1,undefined4 param_2)

{
  undefined8 uVar1;
  char cVar2;
  int iVar3;
  int iVar4;
  undefined1 uVar5;
  undefined4 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 *puVar9;
  longlong *plVar10;
  longlong lVar11;
  int iVar12;
  int iVar13;
  double dVar14;
  float fVar15;
  undefined1 local_res10;
  int local_res20;
  float local_108;
  float fStack_104;
  undefined8 local_d8;
  undefined4 local_d0;
  undefined8 local_c8;
  float local_c0;
  undefined1 local_b8 [16];
  undefined1 local_a8 [16];
  undefined1 local_98 [16];
  undefined1 local_88 [16];
  undefined1 local_78 [64];
  
  if (DAT_18ab626aa == '\0') {
    FUN_18032f6d0(&DAT_187b40048);
    FUN_18032f6d0(&DAT_187c421b8);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b812d0);
    FUN_18032f6d0(&DAT_187b446b8);
    DAT_18ab626aa = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar7 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    local_res20 = 0;
    if ((lVar7 != 0) && (*(longlong *)(lVar7 + 0x20) != 0)) {
      iVar3 = *(int *)(*(longlong *)(lVar7 + 0x20) + 0x18);
      if (0 < iVar3) {
        do {
          if ((*(longlong *)(lVar7 + 0x20) == 0) ||
             (lVar8 = FUN_1849ed2d0(*(longlong *)(lVar7 + 0x20),local_res20,DAT_187c19e18),
             lVar8 == 0)) goto LAB_1814c25d5;
          if (*(char *)(lVar8 + 0x86) == '\0') {
            if (*(longlong *)(param_1 + 0x20) == 0) goto LAB_1814c25d5;
            puVar9 = (undefined8 *)FUN_1814afa20(local_b8,*(longlong *)(param_1 + 0x20),0);
            local_d8 = *puVar9;
            local_d0 = *(undefined4 *)(puVar9 + 1);
            FUN_1814c9b60(param_1,lVar7,lVar8,&local_d8,1,1,0);
            *(undefined1 *)(lVar8 + 0x1a4) = 0;
            for (iVar12 = 0; iVar12 < *(int *)(lVar8 + 0x1c); iVar12 = iVar12 + 1) {
              lVar11 = *(longlong *)(lVar8 + 0x50);
              if (lVar11 == 0) goto LAB_1814c25d5;
              if ((iVar12 < *(int *)(lVar11 + 0x18)) && (*(int *)(lVar8 + 0x60) == 7)) {
                iVar13 = *(int *)(lVar8 + 0x2c);
                fVar15 = 0.0;
                uVar5 = 0;
                local_res10 = 1;
                if (iVar13 == 1) {
                  if ((lVar11 == 0) ||
                     (plVar10 = (longlong *)FUN_1849ed2d0(lVar11,0,DAT_187b812d0),
                     plVar10 == (longlong *)0x0)) goto LAB_1814c25d5;
                  puVar9 = (undefined8 *)
                           (**(code **)(*plVar10 + 0x178))
                                     (local_78,plVar10,*(undefined8 *)(*plVar10 + 0x180));
LAB_1814c2451:
                  fVar15 = *(float *)(puVar9 + 1);
                  local_108 = (float)*puVar9;
                  fStack_104 = (float)((ulonglong)*puVar9 >> 0x20);
                  if ((plVar10[0xd] == 0) ||
                     (lVar11 = *(longlong *)(plVar10[0xd] + 0x38), lVar11 == 0)) goto LAB_1814c25d5;
                  uVar5 = FUN_18394da00(lVar11,0x47);
                }
                else if (iVar13 == 2) {
                  if (*(longlong *)(param_1 + 0x20) == 0) goto LAB_1814c25d5;
                  puVar9 = (undefined8 *)FUN_1814afa20(local_88,*(longlong *)(param_1 + 0x20),0);
                  if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c25d5;
                  cVar2 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x165);
                  uVar1 = *puVar9;
                  fVar15 = *(float *)(puVar9 + 1);
                  if (*(longlong *)(lVar8 + 0x58) == 0) goto LAB_1814c25d5;
                  dVar14 = (double)FUN_1855f06a0(*(longlong *)(lVar8 + 0x58),0);
                  local_108 = (float)uVar1;
                  iVar13 = -1;
                  if (cVar2 == '\0') {
                    iVar13 = 1;
                  }
                  fVar15 = fVar15 + 0.0;
                  local_108 = local_108 + ((float)dVar14 * (float)iVar13) / 540.0;
                  fStack_104 = (float)((ulonglong)uVar1 >> 0x20);
                  fStack_104 = fStack_104 + 0.0;
                }
                else if (iVar13 == 3) {
                  if (*(longlong *)(param_1 + 0x20) == 0) goto LAB_1814c25d5;
                  puVar9 = (undefined8 *)FUN_1814afa20(local_98,*(longlong *)(param_1 + 0x20),0);
                  local_108 = (float)*puVar9;
                  fStack_104 = (float)((ulonglong)*puVar9 >> 0x20);
                  fVar15 = *(float *)(puVar9 + 1);
                }
                else {
                  if (iVar13 == 4) {
                    if ((lVar11 != 0) &&
                       (plVar10 = (longlong *)FUN_1849ed2d0(lVar11,0,DAT_187b812d0),
                       plVar10 != (longlong *)0x0)) {
                      puVar9 = (undefined8 *)(**(code **)(*plVar10 + 0x178))(local_a8,plVar10);
                      local_res10 = 0;
                      goto LAB_1814c2451;
                    }
                    goto LAB_1814c25d5;
                  }
                  local_108 = 0.0;
                  fStack_104 = 0.0;
                }
                iVar13 = 0;
                if (*(longlong *)(lVar8 + 0x88) == 0) goto LAB_1814c25d5;
                iVar4 = *(int *)(*(longlong *)(lVar8 + 0x88) + 0x18);
                if (0 < iVar4) {
                  do {
                    lVar11 = *(longlong *)(lVar7 + 0x20);
                    if (((*(longlong *)(lVar8 + 0x88) == 0) ||
                        (uVar6 = FUN_184a610f0(*(longlong *)(lVar8 + 0x88),iVar13,DAT_187b446b8),
                        lVar11 == 0)) ||
                       (lVar11 = FUN_1849ed2d0(lVar11,uVar6,DAT_187c19e18), lVar11 == 0))
                    goto LAB_1814c25d5;
                    *(undefined1 *)(lVar11 + 0x98) = uVar5;
                    local_c8 = CONCAT44(fStack_104,local_108);
                    local_c0 = fVar15;
                    FUN_1814c9b60(param_1,lVar7,lVar11,&local_c8,0,local_res10,0);
                    iVar13 = iVar13 + 1;
                    *(undefined1 *)(lVar11 + 0x1a4) = 0;
                  } while (iVar13 < iVar4);
                }
              }
            }
          }
          local_res20 = local_res20 + 1;
        } while (local_res20 < iVar3);
      }
      return;
    }
  }
LAB_1814c25d5:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::SetAttackValue  @ 0x14c25e0  size=342 ==== */

void FUN_1814c25e0(longlong param_1,longlong param_2)

{
  longlong lVar1;
  char cVar2;
  undefined8 local_60;
  undefined8 uStack_58;
  longlong local_50;
  undefined4 local_48;
  undefined4 uStack_44;
  undefined4 uStack_40;
  undefined4 uStack_3c;
  longlong local_38;
  
  if (DAT_18ab626d6 == '\0') {
    FUN_18032f6d0(&DAT_187b401f8);
    FUN_18032f6d0(&DAT_187c4c378);
    FUN_18032f6d0(&DAT_187c4c530);
    FUN_18032f6d0(&DAT_187c4c6e0);
    FUN_18032f6d0(&DAT_187c19ab0);
    DAT_18ab626d6 = '\x01';
  }
  if ((*(longlong *)(param_1 + 0x38) != 0) &&
     (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x20), lVar1 != 0)) {
    FUN_1849e2810(&local_60,lVar1,DAT_187c19ab0);
    local_48 = (undefined4)local_60;
    uStack_44 = local_60._4_4_;
    uStack_40 = (undefined4)uStack_58;
    uStack_3c = uStack_58._4_4_;
    local_38 = local_50;
    local_60 = 0;
    uStack_58 = &local_48;
    while( true ) {
      cVar2 = FUN_18572bc70(&local_48,DAT_187c4c530);
      if (cVar2 == '\0') {
        FUN_180443e50(&local_48,DAT_187c4c378);
        return;
      }
      if (local_38 == 0) break;
      if (*(longlong *)(local_38 + 0x58) == 0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032f9d0();
      }
      FUN_1855f0a20(*(longlong *)(local_38 + 0x58),2,(double)param_2 / 10000.0 + 1.0,DAT_187b401f8);
    }
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::SetBonusId  @ 0x14c2760  size=109 ==== */

void FUN_1814c2760(longlong param_1,undefined4 param_2,undefined4 param_3)

{
  longlong lVar1;
  
  if (DAT_18ab626c8 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626c8 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      *(undefined4 *)(lVar1 + 0x198) = param_3;
      return;
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::StartAction  @ 0x14c3a20  size=100 ==== */

void FUN_1814c3a20(longlong param_1,undefined4 param_2)

{
  undefined8 uVar1;
  
  if (DAT_18ab6269f == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab6269f = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    uVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    FUN_1814c2890(param_1,param_2,uVar1,0);
    return;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::StartAction  @ 0x14c2890  size=4481 ==== */

undefined8 FUN_1814c2890(longlong param_1,int param_2,longlong param_3)

{
  int iVar1;
  undefined4 uVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  char cVar5;
  char cVar6;
  undefined4 uVar7;
  undefined4 uVar8;
  undefined4 uVar9;
  longlong lVar10;
  longlong lVar11;
  undefined8 *puVar12;
  longlong lVar13;
  undefined8 uVar14;
  longlong *plVar15;
  longlong lVar16;
  ushort uVar17;
  ushort uVar18;
  int iVar19;
  ulonglong uVar20;
  int iVar21;
  uint uVar22;
  ulonglong uVar24;
  bool bVar25;
  char local_res18 [16];
  undefined8 in_stack_ffffffffffffff38;
  undefined4 uVar28;
  undefined8 uVar26;
  ulonglong uVar27;
  ulonglong uVar29;
  undefined8 uVar30;
  ulonglong in_stack_ffffffffffffff50;
  undefined8 local_78;
  undefined8 uStack_70;
  ulonglong uVar23;
  
  if (DAT_18ab626a0 == '\0') {
    FUN_18032f6d0(&DAT_187b8aab0);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187b94060);
    FUN_18032f6d0(&DAT_187b42140);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b73698);
    FUN_18032f6d0(&DAT_187c24e18);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187c24fc8);
    FUN_18032f6d0(&DAT_187b812d0);
    FUN_18032f6d0(&DAT_187b73858);
    FUN_18032f6d0(&DAT_187b5e978);
    FUN_18032f6d0(&DAT_187bb0fd0);
    DAT_18ab626a0 = '\x01';
  }
  iVar19 = 0;
  uVar29 = 0;
  local_res18[0] = '\0';
  uVar27 = CONCAT71((int7)((ulonglong)in_stack_ffffffffffffff38 >> 8),1);
  FUN_1814c4340(param_1,param_3,local_res18,0,uVar27,0);
  if (param_3 == 0) goto LAB_1814c3a0c;
  lVar16 = *(longlong *)(param_3 + 0xa8);
  if (0.0 < *(float *)(param_3 + 0xb0)) {
    while( true ) {
      uVar28 = (undefined4)(uVar27 >> 0x20);
      iVar21 = 0;
      lVar10 = *(longlong *)(param_3 + 0x20);
      if (lVar10 == 0) break;
      if (*(int *)(lVar10 + 0x18) <= iVar19) {
        if (DAT_18ab626da == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626da = '\x01';
        }
        lVar10 = *(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
        if (lVar10 != 0) {
          uVar27 = CONCAT44(uVar28,*(undefined4 *)(param_3 + 0xd0));
          FUN_1800158b0(0x15,DAT_187b93630,lVar10,param_3,uVar27);
          goto LAB_1814c2c96;
        }
        break;
      }
      lVar10 = FUN_1849ed2d0(lVar10,iVar19,DAT_187c19e18);
      if (lVar16 == 0) break;
      cVar5 = FUN_184a7f390(lVar16,iVar19,DAT_187b42140);
      if (cVar5 == '\0') {
LAB_1814c2a5e:
        if (lVar10 == 0) break;
        if (*(char *)(lVar10 + 0x84) == '\0') {
          if (*(longlong *)(lVar10 + 0x50) == 0) break;
          iVar1 = *(int *)(*(longlong *)(lVar10 + 0x50) + 0x18);
          if (0 < iVar1) {
            do {
              if (((*(int *)(lVar10 + 0x1c) <= iVar21) && (*(int *)(lVar10 + 0x1c) != 0)) &&
                 (iVar21 != 0)) break;
              if ((*(longlong *)(lVar10 + 0x50) == 0) ||
                 (lVar11 = FUN_1849ed2d0(*(longlong *)(lVar10 + 0x50),iVar21,DAT_187b812d0),
                 lVar11 == 0)) goto LAB_1814c3a0c;
              uVar23 = *(ulonglong *)(lVar11 + 0x68);
              if (*(int *)(lVar10 + 0x10) == 1) {
                if ((uVar23 == 0) || (*(longlong *)(uVar23 + 0xb0) == 0)) goto LAB_1814c3a0c;
                FUN_1804f1ed0(*(longlong *)(uVar23 + 0xb0),1,0);
              }
              lVar11 = FUN_1814a9080(param_1,0);
              uVar14 = *(undefined8 *)(param_1 + 0x90);
              if ((*(longlong *)(lVar10 + 0x50) == 0) ||
                 (uVar29 = FUN_1849ed2d0(*(longlong *)(lVar10 + 0x50),iVar21,DAT_187b812d0),
                 lVar11 == 0)) goto LAB_1814c3a0c;
              uVar27 = uVar23;
              FUN_180012050(2,DAT_187b93630,lVar11,uVar14,uVar23,uVar29);
              uVar14 = *(undefined8 *)(param_1 + 0x90);
              if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
                il2cpp_runtime_class_init();
              }
              cVar5 = FUN_18620ca00(uVar14,uVar23,0);
              if ((cVar5 != '\0') && (*(char *)(param_3 + 0x90) != '\0')) {
                if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c3a0c;
                if (param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
                  lVar11 = FUN_1814a9080(param_1,0);
                  if (lVar11 == 0) goto LAB_1814c3a0c;
                  cVar5 = FUN_180002270(0xb,DAT_187b93630,lVar11);
                  if (cVar5 != '\0') goto LAB_1814c2c10;
                }
                if ((uVar23 == 0) || (lVar11 = FUN_1815f9970(uVar23,0), lVar11 == 0))
                goto LAB_1814c3a0c;
                local_78 = 0x3f8000003f800000;
                uStack_70 = 0x3f800000;
                FUN_183dc1780(lVar11,&local_78,0);
              }
LAB_1814c2c10:
              iVar21 = iVar21 + 1;
            } while (iVar21 < iVar1);
          }
        }
      }
      else {
        if (lVar10 == 0) break;
        if (*(char *)(lVar10 + 0x86) != '\0') goto LAB_1814c2a5e;
      }
      iVar19 = iVar19 + 1;
    }
    goto LAB_1814c3a0c;
  }
LAB_1814c2c96:
  bVar25 = false;
  if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c3a0c;
  cVar5 = '\x01';
  if (param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    cVar6 = '\x01';
    if (*(char *)(param_3 + 0xb4) == '\0') {
      cVar6 = *(char *)(param_3 + 0x10);
    }
    uVar28 = *(undefined4 *)(param_3 + 0xb0);
    uVar14 = *(undefined8 *)(param_1 + 0x90);
    plVar15 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
    if (plVar15 == (longlong *)0x0) goto LAB_1814c3a0c;
    lVar10 = *plVar15;
    uVar17 = 0;
    uVar3 = *(undefined8 *)(param_3 + 0xb8);
    uVar4 = *(undefined8 *)(param_3 + 0xc0);
    if (*(ushort *)(lVar10 + 0x12e) != 0) {
      do {
        if (*(longlong *)(*(longlong *)(lVar10 + 0xb0) + (ulonglong)uVar17 * 0x10) == DAT_187b93630)
        {
          puVar12 = (undefined8 *)
                    ((longlong)*(int *)(*(longlong *)(lVar10 + 0xb0) + 8 + (ulonglong)uVar17 * 0x10)
                     * 0x10 + 0x138 + lVar10);
          goto LAB_1814c2d76;
        }
        uVar17 = uVar17 + 1;
      } while (uVar17 < *(ushort *)(lVar10 + 0x12e));
    }
    puVar12 = (undefined8 *)FUN_18032c930(plVar15,DAT_187b93630,0);
LAB_1814c2d76:
    uVar30 = puVar12[1];
    uVar26 = CONCAT71((int7)(uVar27 >> 8),cVar6 != '\0');
    local_78 = uVar3;
    uStack_70 = uVar4;
    (*(code *)*puVar12)(plVar15,uVar14,uVar28,&local_78,uVar26,uVar30);
    uVar28 = (undefined4)((ulonglong)uVar30 >> 0x20);
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    lVar10 = *(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar10 == 0)) goto LAB_1814c3a0c;
    uVar29 = CONCAT44(uVar28,*(undefined4 *)(param_3 + 0xd0));
    uVar27 = CONCAT71((int7)((ulonglong)uVar26 >> 8),1);
    FUN_180015960(1,DAT_187b93630,lVar10,*(undefined4 *)(param_3 + 0x78),uVar27,uVar29,
                  *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0xd0));
    if (DAT_18ab626dc == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626dc = '\x01';
    }
    if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20) == 0) goto LAB_1814c3a0c;
    FUN_180002360(1,DAT_187b94060);
    if (*(longlong *)(param_3 + 0x50) == 0) goto LAB_1814c3a0c;
    if (*(char *)(*(longlong *)(param_3 + 0x50) + 0x10) != '\0') {
      if (DAT_18ab626dc == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626dc = '\x01';
      }
      uVar14 = *(undefined8 *)(param_3 + 0x50);
      uVar3 = *(undefined8 *)(param_1 + 0x90);
      uVar28 = *(undefined4 *)(param_3 + 0xd0);
      plVar15 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20);
      if (plVar15 == (longlong *)0x0) goto LAB_1814c3a0c;
      lVar10 = *plVar15;
      uVar29 = 0;
      if (*(ushort *)(lVar10 + 0x12e) != 0) {
        do {
          if (*(longlong *)(*(longlong *)(lVar10 + 0xb0) + uVar29 * 0x10) == DAT_187b94060) {
            puVar12 = (undefined8 *)
                      ((longlong)*(int *)(*(longlong *)(lVar10 + 0xb0) + 8 + uVar29 * 0x10) * 0x10 +
                       0x138 + lVar10);
            goto LAB_1814c2f16;
          }
          uVar17 = (short)uVar29 + 1;
          uVar29 = (ulonglong)uVar17;
        } while (uVar17 < *(ushort *)(lVar10 + 0x12e));
      }
      puVar12 = (undefined8 *)FUN_18032c930(plVar15,DAT_187b94060,0);
LAB_1814c2f16:
      uVar29 = puVar12[1];
      uVar27 = uVar27 & 0xffffffffffffff00;
      (*(code *)*puVar12)(plVar15,uVar14,uVar3,uVar28,uVar27,uVar29);
    }
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c3a0c;
    cVar6 = FUN_181605e20(*(longlong *)(param_1 + 0x90),0);
    bVar25 = cVar6 != '\0';
  }
  uVar23 = 0;
  lVar10 = *(longlong *)(param_1 + 0x90);
  if (lVar10 == 0) goto LAB_1814c3a0c;
  if ((*(char *)(lVar10 + 0x1a8) != '\0') && (param_2 != *(int *)(lVar10 + 0x4b8))) {
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    lVar10 = *(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar10 == 0)) goto LAB_1814c3a0c;
    uVar29 = uVar29 & 0xffffffff00000000;
    uVar27 = uVar27 & 0xffffffffffffff00;
    FUN_180015960(1,DAT_187b93630,lVar10,*(undefined4 *)(param_3 + 0x78),uVar27,uVar29,
                  *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0xd0));
  }
  if (bVar25) {
LAB_1814c3012:
    lVar10 = *(longlong *)(DAT_187bb0fd0 + 0x20);
    if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
      lVar10 = FUN_18032d010(lVar10);
    }
    lVar10 = *(longlong *)(*(longlong *)(lVar10 + 0xc0) + 0x10);
    if ((*(byte *)(lVar10 + 0x135) & 1) == 0) {
      lVar10 = FUN_18032d010(lVar10);
    }
    if (**(longlong **)(lVar10 + 0xb8) == 0) goto LAB_1814c3a0c;
    lVar10 = *(longlong *)(**(longlong **)(lVar10 + 0xb8) + 200);
    if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar10 == 0)) goto LAB_1814c3a0c;
    FUN_181395840(lVar10,*(undefined8 *)(param_3 + 0xe0),
                  *(undefined1 *)(*(longlong *)(param_1 + 0x90) + 0x1a8),0);
  }
  else {
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c3a0c;
    cVar5 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x1a8);
    if (cVar5 != '\0') goto LAB_1814c3012;
  }
  if (*(longlong *)(param_1 + 0x90) != 0) {
    lVar10 = FUN_1815f9970(*(longlong *)(param_1 + 0x90),0);
    cVar6 = local_res18[0];
    if (*(char *)(param_3 + 0x58) != '\0') {
      cVar6 = '\0';
    }
    if (*(longlong *)(param_1 + 0x90) != 0) {
      lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
      if (cVar6 == '\0') {
        if (lVar11 == 0) goto LAB_1814c3a0c;
        cVar6 = FUN_18394dd40(lVar11,0);
        if ((cVar6 == '\0') || (*(char *)(lVar11 + 0x1f8) != '\0')) {
          uVar20 = uVar23;
          if (cVar5 == '\0') {
            if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c3a0c;
            FUN_1815f9f80(*(longlong *)(param_1 + 0x90),*(undefined8 *)(param_3 + 0xe0),0);
          }
          while (lVar13 = *(longlong *)(param_3 + 0x38), lVar13 != 0) {
            if (*(int *)(lVar13 + 0x18) <= (int)uVar20) {
              if (DAT_18ab626bf == '\0') {
                FUN_18032f6d0(&DAT_187bd2190);
                FUN_18032f6d0(&DAT_187bd2348);
                DAT_18ab626bf = '\x01';
              }
              if (*(longlong *)(param_3 + 0x40) != 0) {
                iVar19 = *(int *)(*(longlong *)(param_3 + 0x40) + 0x18);
                if (iVar19 < 1) goto LAB_1814c3321;
                goto LAB_1814c3251;
              }
              break;
            }
            lVar13 = FUN_1849ed2d0(lVar13,uVar20,DAT_187c24fc8);
            if (lVar13 == 0) break;
            if (*(int *)(lVar13 + 0x1c) == 0) {
              if (*(longlong *)(param_3 + 0x38) == 0) break;
              uVar14 = FUN_1849ed2d0(*(longlong *)(param_3 + 0x38),uVar20,DAT_187c24fc8);
              uVar14 = FUN_1814c3b10(param_1,uVar14,param_3,1,0);
              uVar24 = uVar23;
              if (0.0 < *(float *)(param_3 + 0xb0)) {
                uVar24 = *(ulonglong *)(param_1 + 0x90);
              }
              uVar27 = 0;
              FUN_1814bbc70(param_1,uVar14,0,uVar24,0);
            }
            uVar20 = (ulonglong)((int)uVar20 + 1);
          }
          goto LAB_1814c3a0c;
        }
      }
      uVar28 = (undefined4)(uVar27 >> 0x20);
      if (*(longlong *)(param_1 + 0x90) != 0) {
        if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x1a8) == '\0') {
          return 0;
        }
        if (lVar11 != 0) {
          cVar5 = FUN_1839540a0(lVar11,0);
          if (cVar5 != '\0') {
            return 0;
          }
          if (*(char *)(param_3 + 0x10) != '\0') {
            return 0;
          }
          uVar2 = *(undefined4 *)(param_3 + 0x80);
          uVar7 = *(undefined4 *)(param_3 + 0x94);
          if ((*(longlong *)(param_1 + 0x90) != 0) &&
             (uVar9 = FUN_181605f10(*(longlong *)(param_1 + 0x90),0), lVar10 != 0)) {
            uVar29 = 0;
            uVar14 = CONCAT44(uVar28,uVar9);
            cVar5 = FUN_183dd2e00(lVar10,uVar2,uVar7,0xffffffff,uVar14,0);
            uVar28 = (undefined4)((ulonglong)uVar14 >> 0x20);
            if (cVar5 == '\0') {
              return 0;
            }
            lVar16 = *(longlong *)(param_1 + 0x90);
            if (lVar16 != 0) {
              lVar10 = *(longlong *)(lVar16 + 0x48);
              uVar2 = *(undefined4 *)(param_3 + 0x80);
              uVar7 = *(undefined4 *)(param_3 + 0x94);
              uVar9 = FUN_181605f10(lVar16,0);
              if (lVar10 != 0) {
                uVar29 = uVar29 & 0xffffffffffffff00;
                uVar27 = CONCAT44(uVar28,uVar9);
                FUN_18398f3e0(lVar10,uVar2,uVar7,0xffffffff,uVar27,uVar29,0,
                              in_stack_ffffffffffffff50 & 0xffffffffffffff00,0,0,0);
                FUN_1814bcc20(param_1,param_3,0,1,uVar27 & 0xffffffffffffff00,
                              uVar29 & 0xffffffffffffff00,0);
                return 0;
              }
            }
          }
        }
      }
    }
  }
  goto LAB_1814c3a0c;
  while( true ) {
    uVar14 = FUN_1849ed2d0(*(longlong *)(param_3 + 0x40),uVar23,DAT_187bd2348);
    if (DAT_18ab626c0 == '\0') {
      FUN_18032f6d0(&DAT_187c21e50);
      DAT_18ab626c0 = '\x01';
    }
    lVar13 = thunk_FUN_180383da0(DAT_187c21e50);
    FUN_180445940(lVar13,0,0);
    if (lVar13 == 0) goto LAB_1814c3a0c;
    *(longlong *)(lVar13 + 0x38) = param_1;
    thunk_FUN_1803b4a20(lVar13 + 0x38,param_1);
    *(undefined8 *)(lVar13 + 0x28) = uVar14;
    thunk_FUN_1803b4a20(lVar13 + 0x28,uVar14);
    *(longlong *)(lVar13 + 0x30) = param_3;
    thunk_FUN_1803b4a20(lVar13 + 0x30,param_3);
    *(undefined1 *)(lVar13 + 0x20) = 1;
    if (*(float *)(param_3 + 0xb0) <= 0.0) {
      uVar14 = 0;
    }
    else {
      uVar14 = *(undefined8 *)(param_1 + 0x90);
    }
    uVar27 = 0;
    FUN_1814bbc70(param_1,lVar13,0,uVar14,0);
    uVar22 = (int)uVar23 + 1;
    uVar23 = (ulonglong)uVar22;
    if (iVar19 <= (int)uVar22) break;
LAB_1814c3251:
    if (*(longlong *)(param_3 + 0x40) == 0) goto LAB_1814c3a0c;
  }
LAB_1814c3321:
  if ((*(int *)(param_3 + 0x80) != 0) && (*(char *)(param_3 + 0x10) == '\0')) {
    uVar28 = *(undefined4 *)(param_3 + 0x80);
    uVar2 = *(undefined4 *)(param_3 + 0x94);
    if ((*(longlong *)(param_1 + 0x90) == 0) ||
       (uVar7 = FUN_181605f10(*(longlong *)(param_1 + 0x90),0), lVar10 == 0)) goto LAB_1814c3a0c;
    uVar29 = 0;
    uVar27 = CONCAT44((int)(uVar27 >> 0x20),uVar7);
    cVar5 = FUN_183dd2e00(lVar10,uVar28,uVar2,0xffffffff,uVar27,0);
    if (cVar5 != '\0') {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd8), lVar10 == 0))
      goto LAB_1814c3a0c;
      if (*(char *)(lVar10 + 0x28) == '\0') {
        uVar28 = *(undefined4 *)(param_3 + 0x80);
        uVar2 = *(undefined4 *)(param_3 + 0x94);
        lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x48);
        uVar7 = FUN_181605f10(*(longlong *)(param_1 + 0x90),0);
        if (lVar10 == 0) goto LAB_1814c3a0c;
        uVar29 = uVar29 & 0xffffffffffffff00;
        uVar27 = CONCAT44((int)(uVar27 >> 0x20),uVar7);
        FUN_18398f3e0(lVar10,uVar28,uVar2,0xffffffff,uVar27,uVar29,0,
                      in_stack_ffffffffffffff50 & 0xffffffffffffff00,*(undefined4 *)(param_3 + 0xd0)
                      ,0,0);
      }
      else {
        FUN_18043b700(lVar10,0,0);
        uVar9 = (undefined4)(uVar27 >> 0x20);
        lVar10 = *(longlong *)(param_1 + 0x90);
        uVar28 = *(undefined4 *)(param_3 + 0xd0);
        uVar2 = *(undefined4 *)(param_3 + 0x80);
        uVar7 = *(undefined4 *)(param_3 + 0x94);
        if (lVar10 == 0) goto LAB_1814c3a0c;
        uVar8 = FUN_181605f10(lVar10,0);
        uVar29 = 0;
        uVar27 = CONCAT44(uVar9,uVar8);
        FUN_181601d00(lVar10,uVar28,uVar2,uVar7,uVar27,0);
      }
    }
  }
  uVar23 = 0;
  FUN_1814b5d40(param_3,0);
  if (*(longlong *)(param_1 + 0x90) != 0) {
    uVar14 = FUN_18394cde0(lVar11,param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8),
                           param_2 == 1,0);
    *(undefined8 *)(param_3 + 0x160) = uVar14;
    uVar20 = uVar23;
    while (lVar10 = *(longlong *)(param_3 + 0x20), lVar10 != 0) {
      uVar24 = uVar23;
      if (*(int *)(lVar10 + 0x18) <= (int)uVar20) goto LAB_1814c3595;
      plVar15 = (longlong *)FUN_1849ed2d0(lVar10,uVar20,DAT_187c19e18);
      if (plVar15 == (longlong *)0x0) break;
      uVar27 = *(ulonglong *)(*plVar15 + 0x220);
      (**(code **)(*plVar15 + 0x218))
                (plVar15,*(undefined8 *)(param_1 + 0x90),param_1,param_3,uVar27);
      if (lVar16 == 0) break;
      cVar5 = FUN_184a7f390(lVar16,uVar20,DAT_187b42140);
      if (((cVar5 == '\0') || (*(char *)((longlong)plVar15 + 0x86) != '\0')) &&
         (*(char *)((longlong)plVar15 + 0x85) == '\0')) {
        uVar29 = uVar29 & 0xffffffffffffff00;
        uVar27 = CONCAT71((int7)(uVar27 >> 8),1);
        FUN_1814bfb90(param_1,plVar15,param_3,1,uVar27,uVar29,0);
      }
      uVar20 = (ulonglong)((int)uVar20 + 1);
    }
  }
  goto LAB_1814c3a0c;
LAB_1814c3595:
  if (DAT_18ab626da == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab626da = '\x01';
  }
  plVar15 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
  if (plVar15 == (longlong *)0x0) goto LAB_1814c3a0c;
  lVar16 = *plVar15;
  uVar17 = 0;
  if (*(ushort *)(lVar16 + 0x12e) != 0) {
    uVar18 = uVar17;
    do {
      if (*(longlong *)(*(longlong *)(lVar16 + 0xb0) + (ulonglong)uVar18 * 0x10) == DAT_187b93630) {
        puVar12 = (undefined8 *)
                  ((longlong)
                   (*(int *)(*(longlong *)(lVar16 + 0xb0) + 8 + (ulonglong)uVar18 * 0x10) + 9) *
                   0x10 + 0x138 + lVar16);
        goto LAB_1814c3619;
      }
      uVar18 = uVar18 + 1;
    } while (uVar18 < *(ushort *)(lVar16 + 0x12e));
  }
  puVar12 = (undefined8 *)FUN_18032c930(plVar15,DAT_187b93630,9);
LAB_1814c3619:
  lVar16 = (*(code *)*puVar12)(plVar15,puVar12[1]);
  if (lVar16 == 0) goto LAB_1814c3a0c;
  if (*(int *)(lVar16 + 0x18) <= (int)uVar24) {
    uVar29 = uVar29 & 0xffffffffffffff00;
    uVar27 = uVar27 & 0xffffffffffffff00;
    FUN_1814bcc20(param_1,param_3,0,1,uVar27,uVar29,0);
    if (*(longlong *)(param_3 + 0x48) != 0) {
      if (*(char *)(*(longlong *)(param_3 + 0x48) + 0x10) == '\0') goto LAB_1814c384b;
      if (DAT_18ab626dd == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626dd = '\x01';
      }
      lVar16 = *(longlong *)(param_1 + 0x90);
      uVar14 = *(undefined8 *)(param_3 + 0x48);
      uVar28 = *(undefined4 *)(param_3 + 0xd0);
      iVar19 = *(int *)(param_3 + 0x78);
      plVar15 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x10);
      if ((lVar16 != 0) && (iVar21 = *(int *)(lVar16 + 0x4b8), plVar15 != (longlong *)0x0)) {
        lVar10 = *plVar15;
        if (*(ushort *)(lVar10 + 0x12e) == 0) goto LAB_1814c380b;
        goto LAB_1814c37f0;
      }
    }
    goto LAB_1814c3a0c;
  }
  if (DAT_18ab626da == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab626da = '\x01';
  }
  plVar15 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
  if (plVar15 == (longlong *)0x0) goto LAB_1814c3a0c;
  lVar16 = *plVar15;
  if (*(ushort *)(lVar16 + 0x12e) != 0) {
    do {
      if (*(longlong *)(*(longlong *)(lVar16 + 0xb0) + (ulonglong)uVar17 * 0x10) == DAT_187b93630) {
        puVar12 = (undefined8 *)
                  ((longlong)
                   (*(int *)(*(longlong *)(lVar16 + 0xb0) + 8 + (ulonglong)uVar17 * 0x10) + 9) *
                   0x10 + 0x138 + lVar16);
        goto LAB_1814c36b7;
      }
      uVar17 = uVar17 + 1;
    } while (uVar17 < *(ushort *)(lVar16 + 0x12e));
  }
  puVar12 = (undefined8 *)FUN_18032c930(plVar15,DAT_187b93630,9);
LAB_1814c36b7:
  lVar16 = (*(code *)*puVar12)(plVar15,puVar12[1]);
  if ((lVar16 == 0) || (lVar16 = FUN_1849ed2d0(lVar16,uVar24,DAT_187b73858), lVar16 == 0))
  goto LAB_1814c3a0c;
  *(undefined1 *)(lVar16 + 0x250) = 0;
  uVar24 = (ulonglong)((int)uVar24 + 1);
  goto LAB_1814c3595;
  while (uVar17 = (short)uVar23 + 1, uVar23 = (ulonglong)uVar17,
        uVar17 < *(ushort *)(lVar10 + 0x12e)) {
LAB_1814c37f0:
    if (*(longlong *)(*(longlong *)(lVar10 + 0xb0) + uVar23 * 0x10) == DAT_187b8aab0) {
      puVar12 = (undefined8 *)
                ((longlong)*(int *)(*(longlong *)(lVar10 + 0xb0) + 8 + uVar23 * 0x10) * 0x10 + 0x138
                + lVar10);
      goto LAB_1814c3816;
    }
  }
LAB_1814c380b:
  puVar12 = (undefined8 *)FUN_18032c930(plVar15,DAT_187b8aab0,0);
LAB_1814c3816:
  (*(code *)*puVar12)(plVar15,uVar14,lVar16,uVar28,uVar27 & 0xffffffffffffff00,
                      uVar29 & 0xffffffffffffff00,iVar19 != iVar21,puVar12[1]);
LAB_1814c384b:
  if ((*(longlong *)(param_1 + 0x90) != 0) &&
     (lVar16 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xc0), lVar16 != 0)) {
    FUN_1839b8ce0(lVar16,*(undefined8 *)(param_3 + 0x60),*(undefined4 *)(param_3 + 0xd0),0);
    if (param_2 == 1) {
      FUN_183a23ad0(*(undefined8 *)(param_1 + 0xa0),0);
    }
    return 1;
  }
LAB_1814c3a0c:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::StartActionWithOutCutIn  @ 0x14c27d0  size=186 ==== */

longlong FUN_1814c27d0(undefined8 param_1,undefined8 param_2,undefined4 param_3,undefined8 param_4)

{
  longlong lVar1;
  
  if (DAT_18ab626a5 == '\0') {
    FUN_18032f6d0(&DAT_187c21260);
    DAT_18ab626a5 = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c21260);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x20) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x20,param_1);
    *(undefined8 *)(lVar1 + 0x28) = param_2;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_2);
    *(undefined4 *)(lVar1 + 0x38) = param_3;
    *(undefined8 *)(lVar1 + 0x30) = param_4;
    thunk_FUN_1803b4a20(lVar1 + 0x30,param_4);
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::StartAnnihilationSkillAnimation  @ 0x14c3a90  size=124 ==== */

longlong FUN_1814c3a90(undefined8 param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626a6 == '\0') {
    FUN_18032f6d0(&DAT_187c21418);
    DAT_18ab626a6 = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c21418);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x20) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x20,param_1);
    *(undefined4 *)(lVar1 + 0x28) = param_2;
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::StartShakeWithDelay  @ 0x14c3b10  size=187 ==== */

longlong FUN_1814c3b10(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined1 param_4)

{
  longlong lVar1;
  
  if (DAT_18ab626be == '\0') {
    FUN_18032f6d0(&DAT_187c215c8);
    DAT_18ab626be = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c215c8);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x38) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x38,param_1);
    *(undefined8 *)(lVar1 + 0x28) = param_2;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_2);
    *(undefined8 *)(lVar1 + 0x30) = param_3;
    thunk_FUN_1803b4a20(lVar1 + 0x30,param_3);
    *(undefined1 *)(lVar1 + 0x20) = param_4;
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::StaticRelease  @ 0x14c3bd0  size=251 ==== */

void FUN_1814c3bd0(void)

{
  if (DAT_18ab62696 == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab62696 = '\x01';
  }
  **(undefined8 **)(DAT_187bfaba0 + 0xb8) = 0;
  thunk_FUN_1803b4a20(*(undefined8 *)(DAT_187bfaba0 + 0xb8),0);
  *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) = 0;
  thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8,0);
  *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x10) = 0;
  thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x10,0);
  *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x18) = 0;
  thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x18,0);
  *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20) = 0;
  thunk_FUN_1803b4a20(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20,0);
  return;
}



/* ==== UnitActionController::StopSlowEffect  @ 0x14c3cd0  size=109 ==== */

void FUN_1814c3cd0(void)

{
  if (DAT_18ab626a8 == '\0') {
    FUN_18032f6d0(&DAT_187b94060);
    DAT_18ab626a8 = '\x01';
  }
  if (DAT_18ab626dc == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab626dc = '\x01';
  }
  if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20) != 0) {
    FUN_180002360(1,DAT_187b94060);
    return;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::UpdateBranchMotion  @ 0x14c3e30  size=138 ==== */

longlong FUN_1814c3e30(undefined8 param_1,undefined8 param_2,undefined8 param_3)

{
  longlong lVar1;
  
  if (DAT_18ab626cb == '\0') {
    FUN_18032f6d0(&DAT_187c21780);
    DAT_18ab626cb = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c21780);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x28) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_1);
    *(undefined8 *)(lVar1 + 0x20) = param_3;
    thunk_FUN_1803b4a20(lVar1 + 0x20,param_3);
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::UpdateEffectRunTimeData  @ 0x14c3ec0  size=950 ==== */

void FUN_1814c3ec0(undefined8 param_1,longlong param_2,longlong param_3,char param_4)

{
  int iVar1;
  int iVar2;
  longlong lVar3;
  longlong lVar4;
  longlong *plVar5;
  longlong lVar6;
  undefined8 uVar7;
  int iVar8;
  longlong *plVar9;
  
  if (DAT_18ab6269a == '\0') {
    FUN_18032f6d0(&DAT_187c198f8);
    FUN_18032f6d0(&DAT_187c313a0);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c31550);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b53068);
    FUN_18032f6d0(&DAT_187b891b0);
    FUN_18032f6d0(&DAT_187bd2dc8);
    FUN_18032f6d0(&DAT_187bd2f80);
    FUN_18032f6d0(&DAT_187c20d48);
    DAT_18ab6269a = '\x01';
  }
  if (param_4 != '\0') {
    iVar8 = 0;
    if ((param_2 == 0) || (*(longlong *)(param_2 + 0x20) == 0)) goto LAB_1814c4271;
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x20) + 0x18);
    if (0 < iVar1) {
      do {
        if ((*(longlong *)(param_2 + 0x20) == 0) ||
           (lVar6 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x20),iVar8,DAT_187c19e18), lVar6 == 0))
        goto LAB_1814c4271;
        iVar8 = iVar8 + 1;
        *(undefined1 *)(lVar6 + 0x85) = 0;
      } while (iVar8 < iVar1);
    }
  }
  iVar8 = 0;
  if (param_3 != 0) {
    iVar1 = *(int *)(param_3 + 0x18);
    if (0 < iVar1) {
      do {
        lVar6 = thunk_FUN_180383da0(DAT_187c20d48);
        FUN_1804383d0(lVar6,0);
        uVar7 = FUN_1849ed2d0(param_3,iVar8,DAT_187c31550);
        if (lVar6 == 0) goto LAB_1814c4271;
        *(undefined8 *)(lVar6 + 0x10) = uVar7;
        thunk_FUN_1803b4a20(lVar6 + 0x10,uVar7);
        lVar3 = *(longlong *)(lVar6 + 0x10);
        if (param_2 == 0) goto LAB_1814c4271;
        lVar4 = *(longlong *)(param_2 + 0x20);
        uVar7 = thunk_FUN_180383da0(DAT_187b891b0);
        FUN_184cacb40(uVar7,lVar6,DAT_187bd2dc8,0);
        if ((lVar4 == 0) || (uVar7 = FUN_184a8c920(lVar4,uVar7,DAT_187c198f8), lVar3 == 0))
        goto LAB_1814c4271;
        *(undefined8 *)(lVar3 + 0x38) = uVar7;
        thunk_FUN_1803b4a20(lVar3 + 0x38);
        lVar3 = *(longlong *)(lVar6 + 0x10);
        if (lVar3 == 0) goto LAB_1814c4271;
        if (*(longlong *)(lVar3 + 0x38) == 0) {
          if (*(longlong *)(param_2 + 0x20) == 0) goto LAB_1814c4271;
          uVar7 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x20),0,DAT_187c19e18);
          *(undefined8 *)(lVar3 + 0x38) = uVar7;
          thunk_FUN_1803b4a20(lVar3 + 0x38);
        }
        if (*(longlong **)(lVar6 + 0x10) != (longlong *)0x0) {
          lVar3 = **(longlong **)(lVar6 + 0x10);
          if ((*(byte *)(DAT_187b53068 + 0x130) <= *(byte *)(lVar3 + 0x130)) &&
             (*(longlong *)
               (*(longlong *)(lVar3 + 200) + -8 + (ulonglong)*(byte *)(DAT_187b53068 + 0x130) * 8)
              == DAT_187b53068)) {
            plVar5 = *(longlong **)(lVar6 + 0x10);
            if (*(longlong *)
                 (*(longlong *)(*plVar5 + 200) + -8 +
                 (ulonglong)*(byte *)(DAT_187b53068 + 0x130) * 8) != DAT_187b53068)
            goto LAB_1814c4271;
            plVar9 = (longlong *)0x0;
            if (*(longlong *)
                 (*(longlong *)(*plVar5 + 200) + -8 +
                 (ulonglong)*(byte *)(DAT_187b53068 + 0x130) * 8) == DAT_187b53068) {
              plVar9 = plVar5;
            }
            FUN_1814c3ec0(param_1,param_2,plVar9[0x18],0,0);
          }
        }
        if (*(longlong *)(lVar6 + 0x10) == 0) goto LAB_1814c4271;
        iVar2 = *(int *)(*(longlong *)(lVar6 + 0x10) + 0x50);
        if ((iVar2 == 1) || (iVar2 - 5U < 2)) {
          lVar3 = *(longlong *)(lVar6 + 0x10);
          if (*(int *)(param_2 + 0x78) == 1) {
            if (*(longlong *)(param_2 + 0x20) == 0) goto LAB_1814c4271;
            uVar7 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x20),0,DAT_187c19e18);
            *(undefined8 *)(lVar3 + 0x40) = uVar7;
          }
          else {
            if (*(int *)(lVar3 + 0x48) == -1) goto LAB_1814c4243;
            lVar4 = *(longlong *)(param_2 + 0x20);
            uVar7 = thunk_FUN_180383da0(DAT_187b891b0);
            FUN_184cacb40(uVar7,lVar6,DAT_187bd2f80,0);
            if (lVar4 == 0) goto LAB_1814c4271;
            uVar7 = FUN_184a8c920(lVar4,uVar7,DAT_187c198f8);
            *(undefined8 *)(lVar3 + 0x40) = uVar7;
          }
          thunk_FUN_1803b4a20(lVar3 + 0x40);
          if ((*(longlong *)(lVar6 + 0x10) == 0) ||
             (lVar6 = *(longlong *)(*(longlong *)(lVar6 + 0x10) + 0x40), lVar6 == 0))
          goto LAB_1814c4271;
          *(undefined1 *)(lVar6 + 0x85) = 1;
        }
LAB_1814c4243:
        iVar8 = iVar8 + 1;
      } while (iVar8 < iVar1);
    }
    return;
  }
LAB_1814c4271:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::actionCancelAndGetCancel  @ 0x14c4280  size=186 ==== */

undefined1 FUN_1814c4280(longlong param_1,int param_2)

{
  longlong lVar1;
  longlong lVar2;
  char cVar3;
  
  if (*(longlong *)(param_1 + 0x90) != 0) {
    lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd0);
    lVar2 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
    if (lVar2 != 0) {
      cVar3 = FUN_18394ead0(lVar2,0);
      if (cVar3 == '\0') {
        if ((*(longlong *)(param_1 + 0x90) == 0) || (lVar1 == 0)) goto LAB_1814c4335;
        cVar3 = FUN_1839ce830(lVar1,param_2 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8),
                              0xffffffff);
        if (cVar3 == '\0') {
          return 0;
        }
      }
      else if (lVar1 == 0) goto LAB_1814c4335;
      *(undefined1 *)(lVar1 + 0xce) = 0;
      *(undefined1 *)(lVar1 + 200) = 0;
      FUN_1814bbd10(param_1,param_2,0);
      return 1;
    }
  }
LAB_1814c4335:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::actionStartFirstProcess  @ 0x14c4340  size=2151 ==== */

void FUN_1814c4340(longlong param_1,longlong param_2,undefined1 *param_3,char param_4,char param_5)

{
  char cVar1;
  undefined1 uVar2;
  int iVar3;
  undefined4 uVar4;
  undefined8 *puVar5;
  undefined8 uVar6;
  longlong lVar7;
  longlong *plVar8;
  longlong lVar9;
  ushort uVar10;
  int iVar11;
  int iVar12;
  double dVar13;
  float fVar14;
  float fVar15;
  undefined1 local_res10;
  float local_140;
  float fStack_13c;
  undefined8 local_118;
  undefined4 local_110;
  undefined8 local_108;
  float local_100;
  undefined1 local_f8 [16];
  undefined1 local_e8 [16];
  undefined1 local_d8 [16];
  undefined1 local_c8 [16];
  undefined1 local_b8 [128];
  
  if (DAT_18ab626a1 == '\0') {
    FUN_18032f6d0(&DAT_187b97150);
    FUN_18032f6d0(&DAT_187b2d490);
    FUN_18032f6d0(&DAT_187b40048);
    FUN_18032f6d0(&DAT_187ba2da0);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b812d0);
    FUN_18032f6d0(&DAT_187b446b8);
    DAT_18ab626a1 = '\x01';
  }
  if (param_2 != 0) {
    if (0.0 < *(float *)(param_2 + 0x74)) {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar7 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xd8), lVar7 == 0))
      goto LAB_1814c4ba2;
      *(undefined4 *)(lVar7 + 0x1c) = *(undefined4 *)(param_2 + 0x74);
    }
    iVar11 = 0;
    *(undefined4 *)(param_2 + 0x11c) = 0;
    *(undefined8 *)(param_2 + 0x120) = 0;
    if (param_5 != '\0') {
      *(undefined1 *)(param_2 + 0x84) = 0;
    }
    *(undefined4 *)(param_2 + 0x130) = 0;
    *(undefined1 *)(param_2 + 0x141) = 0;
    lVar7 = *(longlong *)(param_1 + 0x20);
    *(undefined1 *)(param_1 + 0xb8) = 0;
    if (lVar7 != 0) {
      iVar12 = *(int *)(lVar7 + 0x10);
      iVar3 = *(int *)(lVar7 + 0x14);
      *(undefined1 *)(param_2 + 0x128) = 0;
      *(ulonglong *)(param_2 + 0xe8) = CONCAT44((float)iVar3 / 100.0,(float)iVar12 / 100.0);
      *(undefined4 *)(param_2 + 0xf0) = 0;
      if (DAT_18ab626da == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626da = '\x01';
      }
      plVar8 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
      if (plVar8 != (longlong *)0x0) {
        lVar7 = *plVar8;
        uVar10 = 0;
        if (*(ushort *)(lVar7 + 0x12e) != 0) {
          do {
            if (*(longlong *)(*(longlong *)(lVar7 + 0xb0) + (ulonglong)uVar10 * 0x10) ==
                DAT_187b93630) {
              puVar5 = (undefined8 *)
                       ((longlong)
                        (*(int *)(*(longlong *)(lVar7 + 0xb0) + 8 + (ulonglong)uVar10 * 0x10) + 0x17
                        ) * 0x10 + 0x138 + lVar7);
              goto LAB_1814c45a9;
            }
            uVar10 = uVar10 + 1;
          } while (uVar10 < *(ushort *)(lVar7 + 0x12e));
        }
        puVar5 = (undefined8 *)FUN_18032c930(plVar8,DAT_187b93630,0x17);
LAB_1814c45a9:
        uVar6 = (*(code *)*puVar5)(plVar8,puVar5[1]);
        *(undefined8 *)(param_2 + 0x170) = uVar6;
        if (*(longlong *)(param_1 + 0x90) != 0) {
          if ((*(char *)(param_2 + 0x140) != '\0') && (*(char *)(param_2 + 0x168) != '\0')) {
            lVar7 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38);
            if (lVar7 == 0) goto LAB_1814c4ba2;
            FUN_18394d690(lVar7,param_2,0);
          }
          *param_3 = 1;
          while( true ) {
            iVar12 = 0;
            lVar7 = *(longlong *)(param_2 + 0x20);
            if (lVar7 == 0) break;
            if (*(int *)(lVar7 + 0x18) <= iVar11) {
              return;
            }
            lVar7 = FUN_1849ed2d0(lVar7,iVar11,DAT_187c19e18);
            if (param_4 == '\0') {
LAB_1814c4675:
              uVar6 = thunk_FUN_180383da0(DAT_187ba2da0);
              FUN_185660990(uVar6,DAT_187b2d490);
              if (lVar7 == 0) break;
              *(undefined8 *)(lVar7 + 0xf0) = uVar6;
              thunk_FUN_1803b4a20(lVar7 + 0xf0,uVar6);
              *(undefined1 *)(lVar7 + 0xe8) = 0;
              *(undefined8 *)(lVar7 + 0x170) = 0;
              thunk_FUN_1803b4a20(lVar7 + 0x170,0);
              *(undefined8 *)(lVar7 + 0x178) = 0;
              thunk_FUN_1803b4a20(lVar7 + 0x178,0);
              *(undefined8 *)(lVar7 + 400) = 0;
              thunk_FUN_1803b4a20(lVar7 + 400,0);
              if (*(longlong *)(lVar7 + 0x150) == 0) break;
              FUN_185531bb0(*(longlong *)(lVar7 + 0x150),DAT_187b97150);
              if (*(char *)(lVar7 + 0x86) != '\0') goto LAB_1814c4b26;
              if (*(char *)(lVar7 + 0x1a4) == '\0') {
                if (*(longlong *)(param_1 + 0x20) == 0) break;
                puVar5 = (undefined8 *)FUN_1814afa20(local_f8,*(longlong *)(param_1 + 0x20),0);
                local_118 = *puVar5;
                local_110 = *(undefined4 *)(puVar5 + 1);
                FUN_1814c9b60(param_1,param_2,lVar7,&local_118,0,1,0);
              }
              *(undefined1 *)(lVar7 + 0x1a4) = 0;
              if (*(int *)(lVar7 + 0x60) == 7) {
                iVar3 = *(int *)(lVar7 + 0x2c);
                fVar14 = 0.0;
                uVar2 = 0;
                local_res10 = 1;
                fVar15 = 0.0;
                if (iVar3 == 1) {
                  lVar9 = *(longlong *)(lVar7 + 0x50);
                  if (lVar9 == 0) break;
                  if (*(int *)(lVar9 + 0x18) == 0) {
LAB_1814c4b0d:
                    FUN_1814c4bb0(param_1,lVar7,param_2,0);
                    goto LAB_1814c4b26;
                  }
                  plVar8 = (longlong *)FUN_1849ed2d0(lVar9,0,DAT_187b812d0);
                  if (plVar8 == (longlong *)0x0) break;
                  puVar5 = (undefined8 *)
                           (**(code **)(*plVar8 + 0x178))
                                     (local_b8,plVar8,*(undefined8 *)(*plVar8 + 0x180));
LAB_1814c49d1:
                  fVar14 = *(float *)(puVar5 + 1);
                  local_140 = (float)*puVar5;
                  fStack_13c = (float)((ulonglong)*puVar5 >> 0x20);
                  if ((plVar8[0xd] == 0) || (lVar9 = *(longlong *)(plVar8[0xd] + 0x38), lVar9 == 0))
                  break;
                  uVar2 = FUN_18394da00(lVar9,0x47);
                }
                else if (iVar3 == 2) {
                  if (*(longlong *)(param_1 + 0x20) == 0) break;
                  puVar5 = (undefined8 *)FUN_1814afa20(local_c8,*(longlong *)(param_1 + 0x20),0);
                  if (*(longlong *)(param_1 + 0x90) == 0) break;
                  cVar1 = *(char *)(*(longlong *)(param_1 + 0x90) + 0x165);
                  uVar6 = *puVar5;
                  fVar14 = *(float *)(puVar5 + 1);
                  if (*(longlong *)(lVar7 + 0x58) == 0) break;
                  dVar13 = (double)FUN_1855f06a0(*(longlong *)(lVar7 + 0x58),0,DAT_187b40048);
                  local_140 = (float)uVar6;
                  fVar14 = fVar14 + 0.0;
                  iVar3 = -1;
                  if (cVar1 == '\0') {
                    iVar3 = 1;
                  }
                  fStack_13c = (float)((ulonglong)uVar6 >> 0x20);
                  fStack_13c = fStack_13c + 0.0;
                  local_140 = local_140 + (float)((dVar13 * (double)iVar3) / 540.0);
                }
                else if (iVar3 == 3) {
                  if (*(longlong *)(param_1 + 0x20) == 0) break;
                  puVar5 = (undefined8 *)FUN_1814afa20(local_d8,*(longlong *)(param_1 + 0x20),0);
                  local_140 = (float)*puVar5;
                  fStack_13c = (float)((ulonglong)*puVar5 >> 0x20);
                  fVar14 = *(float *)(puVar5 + 1);
                  if (*(longlong *)(param_1 + 0x90) == 0) break;
                  if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x165) == '\0') {
                    fVar15 = 1.0;
                  }
                  else {
                    fVar15 = -1.0;
                  }
                  if (*(longlong *)(lVar7 + 0x58) == 0) break;
                  dVar13 = (double)FUN_1855f06a0(*(longlong *)(lVar7 + 0x58),0,DAT_187b40048);
                  fVar15 = (float)dVar13 * fVar15;
                }
                else {
                  if (iVar3 == 4) {
                    lVar9 = *(longlong *)(lVar7 + 0x50);
                    if (lVar9 != 0) {
                      if (*(int *)(lVar9 + 0x18) == 0) goto LAB_1814c4b0d;
                      plVar8 = (longlong *)FUN_1849ed2d0(lVar9,0,DAT_187b812d0);
                      if (plVar8 != (longlong *)0x0) {
                        puVar5 = (undefined8 *)
                                 (**(code **)(*plVar8 + 0x178))
                                           (local_e8,plVar8,*(undefined8 *)(*plVar8 + 0x180));
                        local_res10 = 0;
                        goto LAB_1814c49d1;
                      }
                    }
                    break;
                  }
                  local_140 = 0.0;
                  fStack_13c = 0.0;
                }
                if (*(longlong *)(lVar7 + 0x88) == 0) break;
                iVar3 = *(int *)(*(longlong *)(lVar7 + 0x88) + 0x18);
                if (0 < iVar3) {
                  do {
                    lVar9 = *(longlong *)(param_2 + 0x20);
                    if (((*(longlong *)(lVar7 + 0x88) == 0) ||
                        (uVar4 = FUN_184a610f0(*(longlong *)(lVar7 + 0x88),iVar12,DAT_187b446b8),
                        lVar9 == 0)) ||
                       (lVar9 = FUN_1849ed2d0(lVar9,uVar4,DAT_187c19e18), lVar9 == 0))
                    goto LAB_1814c4ba2;
                    *(float *)(lVar9 + 0x28) = fVar15;
                    *(undefined1 *)(lVar9 + 0x98) = uVar2;
                    local_108 = CONCAT44(fStack_13c,local_140);
                    local_100 = fVar14;
                    FUN_1814c9b60(param_1,param_2,lVar9,&local_108,0,local_res10,0);
                    iVar12 = iVar12 + 1;
                  } while (iVar12 < iVar3);
                }
              }
              if (*(longlong *)(lVar7 + 0x50) == 0) break;
              if (*(int *)(*(longlong *)(lVar7 + 0x50) + 0x18) == 0) goto LAB_1814c4b26;
              iVar11 = iVar11 + 1;
              *param_3 = 0;
            }
            else {
              if (lVar7 == 0) break;
              if (*(char *)(lVar7 + 0xe9) != '\0') goto LAB_1814c4675;
LAB_1814c4b26:
              iVar11 = iVar11 + 1;
            }
          }
        }
      }
    }
  }
LAB_1814c4ba2:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::clearChildrenTarget  @ 0x14c4bb0  size=354 ==== */

void FUN_1814c4bb0(undefined8 param_1,longlong param_2,longlong param_3)

{
  int iVar1;
  char cVar2;
  longlong lVar3;
  undefined8 local_40;
  undefined8 uStack_38;
  ulonglong local_30;
  undefined4 local_28;
  undefined4 uStack_24;
  undefined4 uStack_20;
  undefined4 uStack_1c;
  ulonglong local_18;
  
  if (DAT_18ab626a2 == '\0') {
    FUN_18032f6d0(&DAT_187b3b288);
    FUN_18032f6d0(&DAT_187b3b438);
    FUN_18032f6d0(&DAT_187b3b5f0);
    FUN_18032f6d0(&DAT_187b7fff8);
    FUN_18032f6d0(&DAT_187b42d30);
    FUN_18032f6d0(&DAT_187c19e18);
    DAT_18ab626a2 = '\x01';
  }
  if ((param_2 == 0) || (*(longlong *)(param_2 + 0x88) == 0)) {
                    /* WARNING: Subroutine does not return */
    FUN_18032f9d0();
  }
  FUN_184a55540(&local_40,*(longlong *)(param_2 + 0x88),DAT_187b42d30);
  local_28 = (undefined4)local_40;
  uStack_24 = local_40._4_4_;
  uStack_20 = (undefined4)uStack_38;
  uStack_1c = uStack_38._4_4_;
  local_18 = local_30;
  local_40 = 0;
  uStack_38 = &local_28;
  while( true ) {
    cVar2 = FUN_185723320(&local_28,DAT_187b3b438);
    if (cVar2 == '\0') {
      FUN_180443e50(&local_28,DAT_187b3b288);
      return;
    }
    if (param_3 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    if (*(longlong *)(param_3 + 0x20) == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    lVar3 = FUN_1849ed2d0(*(longlong *)(param_3 + 0x20),local_18 & 0xffffffff,DAT_187c19e18);
    if (lVar3 == 0) break;
    lVar3 = *(longlong *)(lVar3 + 0x50);
    if (lVar3 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    *(int *)(lVar3 + 0x1c) = *(int *)(lVar3 + 0x1c) + 1;
    iVar1 = *(int *)(lVar3 + 0x18);
    *(undefined4 *)(lVar3 + 0x18) = 0;
    if (0 < iVar1) {
      FUN_185be98b0(*(undefined8 *)(lVar3 + 0x10),0,iVar1,0);
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::convertPriorityPattern  @ 0x14c4d40  size=28 ==== */

uint FUN_1814c4d40(undefined8 param_1,uint param_2)

{
  if ((int)param_2 < 1000) {
    return param_2;
  }
  return (param_2 / 1000) * 1000;
}



/* ==== UnitActionController::createActionValue  @ 0x14c4d60  size=4661 ==== */

void FUN_1814c4d60(longlong param_1,undefined8 param_2,longlong param_3,undefined4 param_4,
                  longlong param_5,longlong param_6)

{
  double dVar1;
  longlong lVar2;
  bool bVar3;
  char cVar4;
  undefined2 uVar5;
  ushort uVar6;
  undefined4 uVar7;
  int iVar8;
  int iVar9;
  longlong lVar10;
  undefined8 uVar11;
  longlong *plVar12;
  longlong lVar13;
  undefined8 uVar14;
  longlong lVar15;
  longlong lVar16;
  longlong *plVar17;
  uint uVar18;
  longlong *plVar19;
  float fVar20;
  float fVar21;
  int local_res20;
  int local_88;
  undefined2 local_84;
  longlong local_78;
  undefined4 local_70;
  undefined8 local_68;
  undefined8 uStack_60;
  undefined8 local_58;
  undefined8 uStack_50;
  
  if (DAT_18ab6269e == '\0') {
    FUN_18032f6d0(&DAT_187c332b0);
    FUN_18032f6d0(&DAT_187c337c8);
    FUN_18032f6d0(&DAT_187c33618);
    FUN_18032f6d0(&DAT_187c33980);
    FUN_18032f6d0(&DAT_187b802a8);
    FUN_18032f6d0(&DAT_187b3fb28);
    FUN_18032f6d0(&DAT_187b45050);
    FUN_18032f6d0(&DAT_187b45208);
    FUN_18032f6d0(&DAT_187b3f978);
    FUN_18032f6d0(&DAT_187b45a88);
    FUN_18032f6d0(&DAT_187b453b8);
    FUN_18032f6d0(&DAT_187b45570);
    FUN_18032f6d0(&DAT_187ba6e58);
    FUN_18032f6d0(&DAT_187c3dbc8);
    FUN_18032f6d0(&DAT_187bf8138);
    FUN_18032f6d0(&DAT_187b6ac68);
    FUN_18032f6d0(&DAT_187c19748);
    FUN_18032f6d0(&DAT_187b6a738);
    FUN_18032f6d0(&DAT_187b69958);
    FUN_18032f6d0(&DAT_187b6a8f0);
    FUN_18032f6d0(&DAT_187b69b10);
    FUN_18032f6d0(&DAT_187b69798);
    FUN_18032f6d0(&DAT_187b413a0);
    FUN_18032f6d0(&DAT_187b695e0);
    FUN_18032f6d0(&DAT_187b7f908);
    FUN_18032f6d0(&DAT_187b6a200);
    FUN_18032f6d0(&DAT_187b6ae28);
    FUN_18032f6d0(&DAT_187b69cd0);
    FUN_18032f6d0(&DAT_187b6a3c0);
    FUN_18032f6d0(&DAT_187b6afe0);
    FUN_18032f6d0(&DAT_187b69e88);
    FUN_18032f6d0(&DAT_187bf1860);
    FUN_18032f6d0(&DAT_187bc09d8);
    FUN_18032f6d0(&DAT_187bc5830);
    FUN_18032f6d0(&DAT_187b3d4a8);
    FUN_18032f6d0(&DAT_187b60c28);
    FUN_18032f6d0(&DAT_187b60fa0);
    FUN_18032f6d0(&DAT_187b61d80);
    FUN_18032f6d0(&DAT_187b81c38);
    FUN_18032f6d0(&DAT_187c013d8);
    FUN_18032f6d0(&DAT_187b77e20);
    FUN_18032f6d0(&DAT_187bcfa38);
    FUN_18032f6d0(&DAT_187bd3138);
    FUN_18032f6d0(&DAT_187c20ef8);
    FUN_18032f6d0(&DAT_187c1f720);
    DAT_18ab6269e = '\x01';
  }
  lVar10 = thunk_FUN_180383da0(DAT_187c20ef8);
  FUN_1804383d0(lVar10,0);
  if (lVar10 != 0) {
    *(undefined4 *)(lVar10 + 0x10) = param_4;
    if (*(int *)(DAT_187b77e20 + 0xe4) == 0) {
      il2cpp_runtime_class_init(DAT_187b77e20);
    }
    lVar13 = *(longlong *)(*(longlong *)(DAT_187b77e20 + 0xb8) + 8);
    if (param_3 == 0) goto LAB_1814c5f8b;
    iVar8 = *(int *)(param_3 + 0x2c);
    uVar5 = *(undefined2 *)(param_3 + 0x30);
    if (*(int *)(DAT_187b61d80 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    local_88 = iVar8;
    local_84 = uVar5;
    uVar5 = FUN_183ae8180(&local_88,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    uVar11 = FUN_1855ffe50(lVar13,uVar5,DAT_187b45a88);
    plVar12 = (longlong *)FUN_185c06400(uVar11,0);
    if (plVar12 == (longlong *)0x0) {
      plVar19 = (longlong *)0x0;
    }
    else {
      if ((*(byte *)(*plVar12 + 0x130) < *(byte *)(DAT_187c3dbc8 + 0x130)) ||
         (*(longlong *)
           (*(longlong *)(*plVar12 + 200) + -8 + (ulonglong)*(byte *)(DAT_187c3dbc8 + 0x130) * 8) !=
          DAT_187c3dbc8)) {
        bVar3 = false;
      }
      else {
        bVar3 = true;
      }
      plVar19 = (longlong *)0x0;
      if (bVar3) {
        plVar19 = plVar12;
      }
    }
    lVar13 = *(longlong *)(param_3 + 0x15c);
    uVar7 = *(undefined4 *)(param_3 + 0x164);
    if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    local_78 = lVar13;
    local_70 = uVar7;
    uVar7 = FUN_183aa36e0(&local_78,0);
    if (plVar19 == (longlong *)0x0) goto LAB_1814c5f8b;
    *(undefined4 *)((longlong)plVar19 + 0x14) = uVar7;
    local_78 = *(longlong *)(param_3 + 0x168);
    local_70 = *(undefined4 *)(param_3 + 0x170);
    uVar7 = FUN_183aa36e0(&local_78,0);
    *(undefined4 *)(plVar19 + 3) = uVar7;
    local_78 = *(longlong *)(param_3 + 0x174);
    local_70 = *(undefined4 *)(param_3 + 0x17c);
    uVar7 = FUN_183aa36e0(&local_78,0);
    *(undefined4 *)((longlong)plVar19 + 0x1c) = uVar7;
    local_78 = *(longlong *)(param_3 + 0x150);
    local_70 = *(undefined4 *)(param_3 + 0x158);
    iVar8 = FUN_183aa36e0(&local_78,0);
    if (iVar8 < 1) {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar13 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar13 == 0))
      goto LAB_1814c5f8b;
      fVar20 = *(float *)(lVar13 + 0x18);
    }
    else {
      lVar13 = *(longlong *)(param_3 + 0x150);
      uVar7 = *(undefined4 *)(param_3 + 0x158);
      if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      local_78 = lVar13;
      local_70 = uVar7;
      iVar8 = FUN_183aa36e0(&local_78,0);
      fVar20 = (float)iVar8;
    }
    *(float *)(plVar19 + 4) = fVar20;
    iVar8 = *(int *)(param_3 + 0x2c);
    uVar5 = *(undefined2 *)(param_3 + 0x30);
    if (*(int *)(DAT_187b61d80 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    local_88 = iVar8;
    local_84 = uVar5;
    uVar6 = FUN_183ae8180(&local_88,0);
    *(uint *)(plVar19 + 0xc) = (uint)uVar6;
    lVar13 = *(longlong *)(param_3 + 0x34);
    uVar7 = *(undefined4 *)(param_3 + 0x3c);
    if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    local_78 = lVar13;
    local_70 = uVar7;
    uVar7 = FUN_183aa36e0(&local_78,0);
    *(undefined4 *)((longlong)plVar19 + 0x2c) = uVar7;
    local_78 = *(longlong *)(param_3 + 0x40);
    local_70 = *(undefined4 *)(param_3 + 0x48);
    uVar7 = FUN_183aa36e0(&local_78,0);
    *(undefined4 *)(plVar19 + 6) = uVar7;
    local_78 = *(longlong *)(param_3 + 0x4c);
    local_70 = *(undefined4 *)(param_3 + 0x54);
    uVar7 = FUN_183aa36e0(&local_78,0);
    *(undefined4 *)((longlong)plVar19 + 0x34) = uVar7;
    uVar11 = thunk_FUN_180383da0(DAT_187bf8138);
    FUN_1804383d0(uVar11,0);
    lVar13 = thunk_FUN_180383da0(DAT_187ba6e58);
    FUN_1855de480(lVar13,uVar11,DAT_187b3f978);
    plVar19[0xb] = lVar13;
    thunk_FUN_1803b4a20(plVar19 + 0xb,lVar13);
    lVar13 = plVar19[0xb];
    local_68 = *(undefined8 *)(param_3 + 0x58);
    uStack_60 = *(undefined8 *)(param_3 + 0x60);
    local_58 = *(undefined8 *)(param_3 + 0x68);
    uStack_50 = *(undefined8 *)(param_3 + 0x70);
    if (*(int *)(DAT_187b60c28 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    uVar7 = FUN_183a98560(&local_68,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    FUN_1855e0980(lVar13,0,uVar7,DAT_187b3fb28);
    local_68 = *(undefined8 *)(param_3 + 0x78);
    uStack_60 = *(undefined8 *)(param_3 + 0x80);
    lVar13 = plVar19[0xb];
    local_58 = *(undefined8 *)(param_3 + 0x88);
    uStack_50 = *(undefined8 *)(param_3 + 0x90);
    uVar7 = FUN_183a98560(&local_68,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    FUN_1855e0980(lVar13,1,uVar7,DAT_187b3fb28);
    local_68 = *(undefined8 *)(param_3 + 0x98);
    uStack_60 = *(undefined8 *)(param_3 + 0xa0);
    lVar13 = plVar19[0xb];
    local_58 = *(undefined8 *)(param_3 + 0xa8);
    uStack_50 = *(undefined8 *)(param_3 + 0xb0);
    uVar7 = FUN_183a98560(&local_68,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    FUN_1855e0980(lVar13,2,uVar7,DAT_187b3fb28);
    local_68 = *(undefined8 *)(param_3 + 0xb8);
    uStack_60 = *(undefined8 *)(param_3 + 0xc0);
    lVar13 = plVar19[0xb];
    local_58 = *(undefined8 *)(param_3 + 200);
    uStack_50 = *(undefined8 *)(param_3 + 0xd0);
    uVar7 = FUN_183a98560(&local_68,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    FUN_1855e0980(lVar13,3,uVar7,DAT_187b3fb28);
    local_68 = *(undefined8 *)(param_3 + 0xd8);
    uStack_60 = *(undefined8 *)(param_3 + 0xe0);
    lVar13 = plVar19[0xb];
    local_58 = *(undefined8 *)(param_3 + 0xe8);
    uStack_50 = *(undefined8 *)(param_3 + 0xf0);
    uVar7 = FUN_183a98560(&local_68,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    FUN_1855e0980(lVar13,4,uVar7,DAT_187b3fb28);
    local_68 = *(undefined8 *)(param_3 + 0xf8);
    uStack_60 = *(undefined8 *)(param_3 + 0x100);
    lVar13 = plVar19[0xb];
    local_58 = *(undefined8 *)(param_3 + 0x108);
    uStack_50 = *(undefined8 *)(param_3 + 0x110);
    uVar7 = FUN_183a98560(&local_68,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    FUN_1855e0980(lVar13,5,uVar7,DAT_187b3fb28);
    local_68 = *(undefined8 *)(param_3 + 0x118);
    uStack_60 = *(undefined8 *)(param_3 + 0x120);
    lVar13 = plVar19[0xb];
    local_58 = *(undefined8 *)(param_3 + 0x128);
    uStack_50 = *(undefined8 *)(param_3 + 0x130);
    uVar7 = FUN_183a98560(&local_68,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    FUN_1855e0980(lVar13,6,uVar7,DAT_187b3fb28);
    plVar19[0x33] = param_3;
    thunk_FUN_1803b4a20(plVar19 + 0x33,param_3);
    iVar8 = 0;
    if (param_6 == 0) goto LAB_1814c5f8b;
    cVar4 = FUN_1855e0af0(param_6,*(undefined4 *)(lVar10 + 0x10),DAT_187b45208);
    if (cVar4 == '\0') {
      FUN_1855e0a60(param_6,*(undefined4 *)(lVar10 + 0x10),1,DAT_187b45050);
    }
    else {
      iVar8 = FUN_1855f0800(param_6,*(undefined4 *)(lVar10 + 0x10),DAT_187b453b8);
      uVar7 = *(undefined4 *)(lVar10 + 0x10);
      iVar9 = FUN_1855f0800(param_6,uVar7,DAT_187b453b8);
      FUN_1855f0a50(param_6,uVar7,iVar9 + 1,DAT_187b45570);
    }
    if (param_5 == 0) goto LAB_1814c5f8b;
    lVar13 = *(longlong *)(param_5 + 0x28);
    uVar11 = thunk_FUN_180383da0(DAT_187b81c38);
    FUN_184cacb40(uVar11,lVar10,DAT_187bd3138,0);
    if (lVar13 == 0) goto LAB_1814c5f8b;
    lVar10 = FUN_184a8c920(lVar13,uVar11,DAT_187b6a8f0);
    local_78 = lVar10;
    if (lVar10 == 0) {
      lVar10 = thunk_FUN_180383da0(DAT_187c33618);
      local_78 = lVar10;
      if (DAT_18ab6260a == '\0') {
        FUN_18032f6d0(&DAT_187c3e3c0);
        FUN_18032f6d0(&DAT_187b6aab0);
        FUN_18032f6d0(&DAT_187bc0ef0);
        DAT_18ab6260a = '\x01';
      }
      *(undefined4 *)(lVar10 + 0x14) = 1;
      uVar11 = thunk_FUN_180383da0(DAT_187bc0ef0);
      FUN_1849de5b0(uVar11,DAT_187b6aab0);
      *(undefined8 *)(lVar10 + 0x18) = uVar11;
      thunk_FUN_1803b4a20(lVar10 + 0x18,uVar11);
      uVar11 = thunk_FUN_180383da0(DAT_187c3e3c0);
      FUN_1861c19b0(uVar11,0);
      *(undefined8 *)(lVar10 + 0x20) = uVar11;
      thunk_FUN_1803b4a20(lVar10 + 0x20,uVar11);
      uVar11 = thunk_FUN_180383da0(DAT_187c3e3c0);
      FUN_1861c19b0(uVar11,0);
      *(undefined8 *)(lVar10 + 0x28) = uVar11;
      thunk_FUN_1803b4a20(lVar10 + 0x28,uVar11);
      FUN_180443e50(lVar10,0);
      *(int *)(lVar10 + 0x14) = (int)plVar19[0xc];
      lVar16 = DAT_187b6a738;
      lVar13 = *(longlong *)(param_5 + 0x28);
      if (lVar13 == 0) goto LAB_1814c5f8b;
      *(int *)(lVar13 + 0x1c) = *(int *)(lVar13 + 0x1c) + 1;
      lVar15 = *(longlong *)(lVar13 + 0x10);
      uVar18 = *(uint *)(lVar13 + 0x18);
      if (lVar15 == 0) goto LAB_1814c5f8b;
      if (uVar18 < *(uint *)(lVar15 + 0x18)) {
        *(uint *)(lVar13 + 0x18) = uVar18 + 1;
        if (*(uint *)(lVar15 + 0x18) <= uVar18) goto LAB_1814c5f91;
        *(longlong *)(lVar15 + 0x20 + (longlong)(int)uVar18 * 8) = lVar10;
        thunk_FUN_1803b4a20(lVar15 + 0x20 + (longlong)(int)uVar18 * 8,lVar10);
      }
      else {
        FUN_184a97470(lVar13,lVar10,
                      *(undefined8 *)(*(longlong *)(*(longlong *)(lVar16 + 0x20) + 0xc0) + 0x70));
      }
    }
    if ((lVar10 != 0) && (lVar13 = *(longlong *)(lVar10 + 0x18), lVar13 != 0)) {
      if (iVar8 < *(int *)(lVar13 + 0x18)) {
        lVar13 = FUN_1849ed2d0(lVar13,iVar8,DAT_187b6afe0);
        if (lVar13 == 0) goto LAB_1814c5f8b;
        uVar11 = *(undefined8 *)(lVar13 + 0x20);
        uVar14 = thunk_FUN_180383da0(DAT_187bc09d8);
        FUN_184aa4390(uVar14,uVar11,DAT_187b69798);
        *(undefined8 *)(lVar13 + 0x18) = uVar14;
        thunk_FUN_1803b4a20(lVar13 + 0x18,uVar14);
        local_res20 = 0;
        if (*(longlong *)(lVar13 + 0x28) == 0) goto LAB_1814c5f8b;
        local_88 = *(int *)(*(longlong *)(lVar13 + 0x28) + 0x18);
        if (0 < local_88) {
          do {
            iVar8 = 0;
            if ((*(longlong *)(lVar13 + 0x28) == 0) ||
               (lVar10 = FUN_1849ed2d0(*(longlong *)(lVar13 + 0x28),local_res20,DAT_187b6a3c0),
               lVar10 == 0)) goto LAB_1814c5f8b;
            for (; iVar8 < *(int *)(lVar10 + 0x1c); iVar8 = iVar8 + 1) {
              if (*(int *)(lVar10 + 0x20) == 0) {
                lVar16 = *(longlong *)(lVar13 + 0x18);
                lVar15 = thunk_FUN_180383da0(DAT_187c332b0);
                *(undefined8 *)(lVar15 + 0x18) = 0x3ff0000000000000;
                *(undefined4 *)(lVar15 + 0x20) = 0x3f800000;
                FUN_180443e50(lVar15,0);
                *(float *)(lVar15 + 0x10) =
                     (float)iVar8 * *(float *)(lVar10 + 0x14) + *(float *)(lVar10 + 0x10);
LAB_1814c58cb:
                *(undefined4 *)(lVar15 + 0x14) = 1;
                if (lVar16 == 0) goto LAB_1814c5f8b;
                FUN_180002460(lVar16);
              }
              else if (*(int *)(lVar10 + 0x20) == 1) {
                lVar16 = *(longlong *)(lVar13 + 0x18);
                lVar15 = thunk_FUN_180383da0(DAT_187c332b0);
                *(undefined8 *)(lVar15 + 0x18) = 0x3ff0000000000000;
                *(undefined4 *)(lVar15 + 0x20) = 0x3f800000;
                FUN_180443e50(lVar15,0);
                fVar20 = *(float *)(lVar10 + 0x10);
                if (*(longlong *)(lVar10 + 0x28) != 0) {
                  fVar21 = (float)FUN_1861c11f0(*(longlong *)(lVar10 + 0x28),
                                                (float)iVar8 / (float)*(int *)(lVar10 + 0x1c),0);
                  *(float *)(lVar15 + 0x10) =
                       (float)*(int *)(lVar10 + 0x1c) * fVar21 * *(float *)(lVar10 + 0x14) + fVar20;
                  goto LAB_1814c58cb;
                }
                goto LAB_1814c5f8b;
              }
            }
            local_res20 = local_res20 + 1;
          } while (local_res20 < local_88);
        }
        lVar10 = *(longlong *)(lVar13 + 0x18);
        if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
          il2cpp_runtime_class_init();
        }
        lVar16 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x20);
        if (lVar16 == 0) {
          if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          uVar11 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
          lVar16 = thunk_FUN_180383da0(DAT_187b802a8);
          FUN_1854e7780(lVar16,uVar11,DAT_187bcfa38,0);
          *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x20) = lVar16;
          thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x20,lVar16);
        }
        if (lVar10 == 0) goto LAB_1814c5f8b;
        FUN_184a9fb90(lVar10,lVar16,DAT_187b69b10);
        lVar10 = local_78;
      }
      else {
        lVar13 = thunk_FUN_180383da0(DAT_187c337c8);
        if (DAT_18ab6260b == '\0') {
          FUN_18032f6d0(&DAT_187b695e0);
          FUN_18032f6d0(&DAT_187b6a048);
          FUN_18032f6d0(&DAT_187b85ff0);
          FUN_18032f6d0(&DAT_187bc0b88);
          FUN_18032f6d0(&DAT_187bfde00);
          FUN_18032f6d0(&DAT_187bc09d8);
          DAT_18ab6260b = '\x01';
        }
        uVar11 = thunk_FUN_180383da0(DAT_187bc09d8);
        FUN_1849de5b0(uVar11,DAT_187b695e0);
        *(undefined8 *)(lVar13 + 0x20) = uVar11;
        thunk_FUN_1803b4a20(lVar13 + 0x20,uVar11);
        uVar11 = thunk_FUN_180383da0(DAT_187bc0b88);
        FUN_1849de5b0(uVar11,DAT_187b6a048);
        *(undefined8 *)(lVar13 + 0x28) = uVar11;
        thunk_FUN_1803b4a20(lVar13 + 0x28,uVar11);
        uVar11 = thunk_FUN_180383da0(DAT_187bfde00);
        FUN_1849de5b0(uVar11,DAT_187b85ff0);
        *(undefined8 *)(lVar13 + 0x38) = uVar11;
        thunk_FUN_1803b4a20(lVar13 + 0x38,uVar11);
        uVar11 = thunk_FUN_180383da0(DAT_187bfde00);
        FUN_1849de5b0(uVar11,DAT_187b85ff0);
        *(undefined8 *)(lVar13 + 0x40) = uVar11;
        thunk_FUN_1803b4a20(lVar13 + 0x40,uVar11);
        FUN_180443e50(lVar13,0);
        uVar11 = thunk_FUN_180383da0(DAT_187bc09d8);
        FUN_1849de5b0(uVar11,DAT_187b695e0);
        *(undefined8 *)(lVar13 + 0x18) = uVar11;
        thunk_FUN_1803b4a20(lVar13 + 0x18,uVar11);
        lVar15 = DAT_187b6ac68;
        lVar16 = *(longlong *)(lVar10 + 0x18);
        if (lVar16 == 0) goto LAB_1814c5f8b;
        *(int *)(lVar16 + 0x1c) = *(int *)(lVar16 + 0x1c) + 1;
        lVar2 = *(longlong *)(lVar16 + 0x10);
        uVar18 = *(uint *)(lVar16 + 0x18);
        if (lVar2 == 0) goto LAB_1814c5f8b;
        if (uVar18 < *(uint *)(lVar2 + 0x18)) {
          *(uint *)(lVar16 + 0x18) = uVar18 + 1;
          if (*(uint *)(lVar2 + 0x18) <= uVar18) {
LAB_1814c5f91:
                    /* WARNING: Subroutine does not return */
            FUN_18032f9c0();
          }
          *(longlong *)(lVar2 + 0x20 + (longlong)(int)uVar18 * 8) = lVar13;
          thunk_FUN_1803b4a20(lVar2 + ((longlong)(int)uVar18 + 4) * 8,lVar13);
        }
        else {
          FUN_184a97470(lVar16,lVar13,
                        *(undefined8 *)(*(longlong *)(*(longlong *)(lVar15 + 0x20) + 0xc0) + 0x70));
        }
        if (lVar13 == 0) goto LAB_1814c5f8b;
      }
      plVar12 = (longlong *)0x0;
      plVar19[0xe] = *(longlong *)(lVar13 + 0x18);
      thunk_FUN_1803b4a20(plVar19 + 0xe);
      if (*(longlong *)(lVar13 + 0x18) != 0) {
        lVar16 = FUN_18032e960(DAT_187c013d8,*(undefined4 *)(*(longlong *)(lVar13 + 0x18) + 0x18));
        plVar19[0xd] = lVar16;
        thunk_FUN_1803b4a20(plVar19 + 0xd,lVar16);
        plVar19[0x22] = *(longlong *)(lVar10 + 0x20);
        thunk_FUN_1803b4a20(plVar19 + 0x22);
        plVar19[0x23] = *(longlong *)(lVar10 + 0x28);
        thunk_FUN_1803b4a20(plVar19 + 0x23);
        *(undefined4 *)(plVar19 + 0x34) = *(undefined4 *)(lVar10 + 0x30);
        plVar19[0x14] = *(longlong *)(lVar13 + 0x38);
        thunk_FUN_1803b4a20(plVar19 + 0x14);
        plVar19[0x15] = *(longlong *)(lVar13 + 0x40);
        thunk_FUN_1803b4a20(plVar19 + 0x15);
        if (*(longlong *)(lVar13 + 0x18) != 0) {
          iVar8 = *(int *)(*(longlong *)(lVar13 + 0x18) + 0x18);
          plVar17 = plVar12;
          if (0 < iVar8) {
            do {
              dVar1 = (double)plVar19[0xf];
              if (*(longlong *)(lVar13 + 0x18) == 0) goto LAB_1814c5f8b;
              uVar18 = (uint)plVar17;
              lVar10 = FUN_1849ed2d0(*(longlong *)(lVar13 + 0x18),plVar17,DAT_187b69e88);
              if (lVar10 == 0) goto LAB_1814c5f8b;
              plVar19[0xf] = (longlong)(dVar1 + *(double *)(lVar10 + 0x18));
              lVar10 = plVar19[0xd];
              if (((*(longlong *)(lVar13 + 0x18) == 0) ||
                  (lVar16 = FUN_1849ed2d0(*(longlong *)(lVar13 + 0x18),plVar17,DAT_187b69e88),
                  lVar16 == 0)) || (lVar10 == 0)) goto LAB_1814c5f8b;
              if (*(uint *)(lVar10 + 0x18) <= uVar18) goto LAB_1814c5f91;
              *(undefined4 *)(lVar10 + 0x20 + (longlong)(int)uVar18 * 4) =
                   *(undefined4 *)(lVar16 + 0x10);
              plVar17 = (longlong *)(ulonglong)(uVar18 + 1);
              lVar10 = local_78;
            } while ((int)(uVar18 + 1) < iVar8);
          }
          *(undefined4 *)(plVar19 + 0x10) = *(undefined4 *)(param_3 + 0x10);
          lVar13 = *(longlong *)(param_3 + 0x14);
          uVar7 = *(undefined4 *)(param_3 + 0x1c);
          if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          local_78 = lVar13;
          local_70 = uVar7;
          uVar7 = FUN_183aa36e0(&local_78,0);
          *(undefined4 *)(plVar19 + 7) = uVar7;
          plVar17 = plVar12;
          while (lVar13 = plVar19[0xe], lVar13 != 0) {
            if (*(int *)(lVar13 + 0x18) <= (int)plVar17) {
LAB_1814c5e23:
              lVar13 = thunk_FUN_180383da0(DAT_187bc5830);
              FUN_1849de5b0(lVar13,DAT_187b7f908);
              plVar19[10] = lVar13;
              thunk_FUN_1803b4a20(plVar19 + 10,lVar13);
              lVar13 = thunk_FUN_180383da0(DAT_187bf1860);
              FUN_1849de5b0(lVar13,DAT_187b413a0);
              plVar19[0x11] = lVar13;
              thunk_FUN_1803b4a20(plVar19 + 0x11,lVar13);
              lVar13 = *(longlong *)(param_3 + 0x138);
              uVar7 = *(undefined4 *)(param_3 + 0x140);
              if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
                il2cpp_runtime_class_init();
              }
              local_78 = lVar13;
              local_70 = uVar7;
              uVar7 = FUN_183aa36e0(&local_78,0);
              *(undefined4 *)(plVar19 + 2) = uVar7;
              local_78 = *(longlong *)(param_3 + 0x144);
              local_70 = *(undefined4 *)(param_3 + 0x14c);
              uVar7 = FUN_183aa36e0(&local_78,0);
              *(undefined4 *)((longlong)plVar19 + 0x24) = uVar7;
              *(undefined1 *)((longlong)plVar19 + 0x84) = *(undefined1 *)(lVar10 + 0x34);
              lVar13 = DAT_187c19748;
              lVar10 = *(longlong *)(param_5 + 0x20);
              if (lVar10 != 0) {
                *(int *)(lVar10 + 0x1c) = *(int *)(lVar10 + 0x1c) + 1;
                lVar16 = *(longlong *)(lVar10 + 0x10);
                uVar18 = *(uint *)(lVar10 + 0x18);
                if (lVar16 != 0) {
                  if (uVar18 < *(uint *)(lVar16 + 0x18)) {
                    *(uint *)(lVar10 + 0x18) = uVar18 + 1;
                    if (*(uint *)(lVar16 + 0x18) <= uVar18) goto LAB_1814c5f91;
                    *(longlong **)(lVar16 + 0x20 + (longlong)(int)uVar18 * 8) = plVar19;
                    thunk_FUN_1803b4a20(lVar16 + 0x20 + (longlong)(int)uVar18 * 8,plVar19);
                  }
                  else {
                    FUN_184a97470(lVar10,plVar19,
                                  *(undefined8 *)
                                   (*(longlong *)(*(longlong *)(lVar13 + 0x20) + 0xc0) + 0x70));
                  }
                  return;
                }
              }
              break;
            }
            lVar13 = FUN_1849ed2d0(lVar13,plVar17,DAT_187b69e88);
            if (lVar13 == 0) break;
            if ((*(int *)(lVar13 + 0x14) == 3) || (*(int *)(lVar13 + 0x14) == 4)) {
              lVar13 = *(longlong *)(DAT_187b3d4a8 + 0x20);
              if ((*(byte *)(lVar13 + 0x135) & 1) == 0) {
                lVar13 = FUN_18032d010(lVar13);
              }
              lVar13 = *(longlong *)(*(longlong *)(lVar13 + 0xc0) + 0x10);
              if ((*(byte *)(lVar13 + 0x135) & 1) == 0) {
                lVar13 = FUN_18032d010(lVar13);
              }
              if (**(longlong **)(lVar13 + 0xb8) != 0) {
                plVar17 = (longlong *)
                          FUN_183907fb0(**(longlong **)(lVar13 + 0xb8),0x507dbe19,
                                        (longlong)(int)plVar19[7],0,0);
                if (plVar17 != (longlong *)0x0) {
                  if ((*(byte *)(*plVar17 + 0x130) < *(byte *)(DAT_187c33980 + 0x130)) ||
                     (*(longlong *)
                       (*(longlong *)(*plVar17 + 200) + -8 +
                       (ulonglong)*(byte *)(DAT_187c33980 + 0x130) * 8) != DAT_187c33980)) {
                    bVar3 = false;
                  }
                  else {
                    bVar3 = true;
                  }
                  if (bVar3) {
                    plVar12 = plVar17;
                  }
                }
                plVar19[8] = (longlong)plVar12;
                thunk_FUN_1803b4a20(plVar19 + 8,plVar12);
                goto LAB_1814c5e23;
              }
              break;
            }
            plVar17 = (longlong *)(ulonglong)((int)plVar17 + 1);
          }
        }
      }
    }
  }
LAB_1814c5f8b:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::createExEquipActionParameter  @ 0x14c5fa0  size=2861 ==== */

longlong * FUN_1814c5fa0(longlong param_1,longlong param_2,longlong param_3,longlong param_4)

{
  uint uVar1;
  longlong lVar2;
  bool bVar3;
  undefined6 uVar4;
  undefined2 uVar5;
  ushort uVar6;
  undefined4 uVar7;
  int iVar8;
  undefined4 uVar9;
  undefined8 uVar10;
  longlong *plVar11;
  longlong lVar12;
  longlong lVar13;
  longlong lVar14;
  longlong *plVar15;
  double dVar16;
  double dVar17;
  double dVar18;
  float fVar19;
  undefined8 local_a8;
  undefined4 local_a0;
  undefined8 local_98;
  undefined8 uStack_90;
  undefined8 local_88;
  undefined8 uStack_80;
  
  if (DAT_18ab626d5 == '\0') {
    FUN_18032f6d0(&DAT_187c332b0);
    FUN_18032f6d0(&DAT_187b3fb28);
    FUN_18032f6d0(&DAT_187b3cb78);
    FUN_18032f6d0(&DAT_187b3f978);
    FUN_18032f6d0(&DAT_187b40048);
    FUN_18032f6d0(&DAT_187b45a88);
    FUN_18032f6d0(&DAT_187ba6e58);
    FUN_18032f6d0(&DAT_187c3dbc8);
    FUN_18032f6d0(&DAT_187bf8138);
    FUN_18032f6d0(&DAT_187c162f0);
    FUN_18032f6d0(&DAT_187b69958);
    FUN_18032f6d0(&DAT_187b413a0);
    FUN_18032f6d0(&DAT_187b695e0);
    FUN_18032f6d0(&DAT_187b85ff0);
    FUN_18032f6d0(&DAT_187b7f908);
    FUN_18032f6d0(&DAT_187bf1860);
    FUN_18032f6d0(&DAT_187bfde00);
    FUN_18032f6d0(&DAT_187bc09d8);
    FUN_18032f6d0(&DAT_187bc5830);
    FUN_18032f6d0(&DAT_187b60c28);
    FUN_18032f6d0(&DAT_187b60fa0);
    FUN_18032f6d0(&DAT_187b61d80);
    FUN_18032f6d0(&DAT_187c013d8);
    FUN_18032f6d0(&DAT_187b77e20);
    DAT_18ab626d5 = '\x01';
  }
  if (*(int *)(DAT_187b77e20 + 0xe4) == 0) {
    il2cpp_runtime_class_init(DAT_187b77e20);
  }
  lVar12 = *(longlong *)(*(longlong *)(DAT_187b77e20 + 0xb8) + 8);
  if (param_2 != 0) {
    uVar4 = *(undefined6 *)(param_2 + 0x2c);
    if (*(int *)(DAT_187b61d80 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    local_a8 = CONCAT26(local_a8._6_2_,uVar4);
    uVar5 = FUN_183ae8180(&local_a8,0);
    if (lVar12 != 0) {
      uVar10 = FUN_1855ffe50(lVar12,uVar5,DAT_187b45a88);
      plVar11 = (longlong *)FUN_185c06400(uVar10,0);
      if (plVar11 != (longlong *)0x0) {
        if ((*(byte *)(*plVar11 + 0x130) < *(byte *)(DAT_187c3dbc8 + 0x130)) ||
           (*(longlong *)
             (*(longlong *)(*plVar11 + 200) + -8 + (ulonglong)*(byte *)(DAT_187c3dbc8 + 0x130) * 8)
            != DAT_187c3dbc8)) {
          bVar3 = false;
        }
        else {
          bVar3 = true;
        }
        plVar15 = (longlong *)0x0;
        if (bVar3) {
          plVar15 = plVar11;
        }
        if (plVar15 != (longlong *)0x0) {
          *(undefined1 *)(plVar15 + 9) = 1;
          uVar10 = *(undefined8 *)(param_2 + 0x15c);
          uVar7 = *(undefined4 *)(param_2 + 0x164);
          if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          local_a8 = uVar10;
          local_a0 = uVar7;
          uVar7 = FUN_183aa36e0(&local_a8,0);
          *(undefined4 *)((longlong)plVar15 + 0x14) = uVar7;
          local_a8 = *(undefined8 *)(param_2 + 0x168);
          local_a0 = *(undefined4 *)(param_2 + 0x170);
          uVar7 = FUN_183aa36e0(&local_a8,0);
          *(undefined4 *)(plVar15 + 3) = uVar7;
          local_a8 = *(undefined8 *)(param_2 + 0x174);
          local_a0 = *(undefined4 *)(param_2 + 0x17c);
          uVar7 = FUN_183aa36e0(&local_a8,0);
          *(undefined4 *)((longlong)plVar15 + 0x1c) = uVar7;
          local_a8 = *(undefined8 *)(param_2 + 0x150);
          local_a0 = *(undefined4 *)(param_2 + 0x158);
          iVar8 = FUN_183aa36e0(&local_a8,0);
          if (iVar8 < 1) {
            if ((*(longlong *)(param_1 + 0x90) == 0) ||
               (lVar12 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar12 == 0))
            goto LAB_1814c6ac3;
            fVar19 = *(float *)(lVar12 + 0x18);
          }
          else {
            uVar10 = *(undefined8 *)(param_2 + 0x150);
            uVar7 = *(undefined4 *)(param_2 + 0x158);
            if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
              il2cpp_runtime_class_init();
            }
            local_a8 = uVar10;
            local_a0 = uVar7;
            iVar8 = FUN_183aa36e0(&local_a8,0);
            fVar19 = (float)iVar8;
          }
          *(float *)(plVar15 + 4) = fVar19;
          uVar4 = *(undefined6 *)(param_2 + 0x2c);
          if (*(int *)(DAT_187b61d80 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          local_a8 = CONCAT26(local_a8._6_2_,uVar4);
          uVar6 = FUN_183ae8180(&local_a8,0);
          *(uint *)(plVar15 + 0xc) = (uint)uVar6;
          uVar10 = *(undefined8 *)(param_2 + 0x34);
          uVar7 = *(undefined4 *)(param_2 + 0x3c);
          if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          local_a8 = uVar10;
          local_a0 = uVar7;
          uVar7 = FUN_183aa36e0(&local_a8,0);
          *(undefined4 *)((longlong)plVar15 + 0x2c) = uVar7;
          local_a8 = *(undefined8 *)(param_2 + 0x40);
          local_a0 = *(undefined4 *)(param_2 + 0x48);
          uVar7 = FUN_183aa36e0(&local_a8,0);
          *(undefined4 *)(plVar15 + 6) = uVar7;
          local_a8 = *(undefined8 *)(param_2 + 0x4c);
          local_a0 = *(undefined4 *)(param_2 + 0x54);
          uVar7 = FUN_183aa36e0(&local_a8,0);
          *(undefined4 *)((longlong)plVar15 + 0x34) = uVar7;
          uVar10 = thunk_FUN_180383da0(DAT_187bf8138);
          FUN_1804383d0(uVar10,0);
          lVar12 = thunk_FUN_180383da0(DAT_187ba6e58);
          FUN_1855de480(lVar12,uVar10,DAT_187b3f978);
          plVar15[0xb] = lVar12;
          thunk_FUN_1803b4a20(plVar15 + 0xb,lVar12);
          lVar12 = plVar15[0xb];
          local_98 = *(undefined8 *)(param_2 + 0x58);
          uStack_90 = *(undefined8 *)(param_2 + 0x60);
          local_88 = *(undefined8 *)(param_2 + 0x68);
          uStack_80 = *(undefined8 *)(param_2 + 0x70);
          if (*(int *)(DAT_187b60c28 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          uVar7 = FUN_183a98560(&local_98,0);
          if (lVar12 != 0) {
            FUN_1855e0980(lVar12,0,uVar7,DAT_187b3fb28);
            local_98 = *(undefined8 *)(param_2 + 0x78);
            uStack_90 = *(undefined8 *)(param_2 + 0x80);
            lVar12 = plVar15[0xb];
            local_88 = *(undefined8 *)(param_2 + 0x88);
            uStack_80 = *(undefined8 *)(param_2 + 0x90);
            uVar7 = FUN_183a98560(&local_98,0);
            if (lVar12 != 0) {
              FUN_1855e0980(lVar12,1,uVar7,DAT_187b3fb28);
              local_98 = *(undefined8 *)(param_2 + 0x98);
              uStack_90 = *(undefined8 *)(param_2 + 0xa0);
              lVar12 = plVar15[0xb];
              local_88 = *(undefined8 *)(param_2 + 0xa8);
              uStack_80 = *(undefined8 *)(param_2 + 0xb0);
              uVar7 = FUN_183a98560(&local_98,0);
              if (lVar12 != 0) {
                FUN_1855e0980(lVar12,2,uVar7,DAT_187b3fb28);
                local_98 = *(undefined8 *)(param_2 + 0xb8);
                uStack_90 = *(undefined8 *)(param_2 + 0xc0);
                lVar12 = plVar15[0xb];
                local_88 = *(undefined8 *)(param_2 + 200);
                uStack_80 = *(undefined8 *)(param_2 + 0xd0);
                uVar7 = FUN_183a98560(&local_98,0);
                if (lVar12 != 0) {
                  FUN_1855e0980(lVar12,3,uVar7,DAT_187b3fb28);
                  local_98 = *(undefined8 *)(param_2 + 0xd8);
                  uStack_90 = *(undefined8 *)(param_2 + 0xe0);
                  lVar12 = plVar15[0xb];
                  local_88 = *(undefined8 *)(param_2 + 0xe8);
                  uStack_80 = *(undefined8 *)(param_2 + 0xf0);
                  uVar7 = FUN_183a98560(&local_98,0);
                  if (lVar12 != 0) {
                    FUN_1855e0980(lVar12,4,uVar7,DAT_187b3fb28);
                    local_98 = *(undefined8 *)(param_2 + 0xf8);
                    uStack_90 = *(undefined8 *)(param_2 + 0x100);
                    lVar12 = plVar15[0xb];
                    local_88 = *(undefined8 *)(param_2 + 0x108);
                    uStack_80 = *(undefined8 *)(param_2 + 0x110);
                    uVar7 = FUN_183a98560(&local_98,0);
                    if (lVar12 != 0) {
                      FUN_1855e0980(lVar12,5,uVar7,DAT_187b3fb28);
                      local_98 = *(undefined8 *)(param_2 + 0x118);
                      uStack_90 = *(undefined8 *)(param_2 + 0x120);
                      lVar12 = plVar15[0xb];
                      local_88 = *(undefined8 *)(param_2 + 0x128);
                      uStack_80 = *(undefined8 *)(param_2 + 0x130);
                      uVar7 = FUN_183a98560(&local_98,0);
                      if (lVar12 != 0) {
                        FUN_1855e0980(lVar12,6,uVar7,DAT_187b3fb28);
                        plVar15[0x33] = param_2;
                        thunk_FUN_1803b4a20(plVar15 + 0x33,param_2);
                        lVar12 = thunk_FUN_180383da0(DAT_187bfde00);
                        FUN_1849de5b0(lVar12,DAT_187b85ff0);
                        plVar15[0x14] = lVar12;
                        thunk_FUN_1803b4a20(plVar15 + 0x14,lVar12);
                        lVar13 = thunk_FUN_180383da0(DAT_187bc09d8);
                        FUN_1849de5b0(lVar13,DAT_187b695e0);
                        lVar14 = thunk_FUN_180383da0(DAT_187c332b0);
                        *(undefined8 *)(lVar14 + 0x18) = 0x3ff0000000000000;
                        *(undefined4 *)(lVar14 + 0x20) = 0x3f800000;
                        FUN_180443e50(lVar14,0);
                        *(undefined4 *)(lVar14 + 0x14) = 0;
                        *(undefined8 *)(lVar14 + 0x18) = 0x3ff0000000000000;
                        lVar12 = DAT_187b69958;
                        if (lVar13 != 0) {
                          *(int *)(lVar13 + 0x1c) = *(int *)(lVar13 + 0x1c) + 1;
                          lVar2 = *(longlong *)(lVar13 + 0x10);
                          uVar1 = *(uint *)(lVar13 + 0x18);
                          if (lVar2 != 0) {
                            if (uVar1 < *(uint *)(lVar2 + 0x18)) {
                              *(uint *)(lVar13 + 0x18) = uVar1 + 1;
                              if (*(uint *)(lVar2 + 0x18) <= uVar1) {
                    /* WARNING: Subroutine does not return */
                                FUN_18032f9c0();
                              }
                              *(longlong *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = lVar14;
                              thunk_FUN_1803b4a20(lVar2 + (longlong)(int)uVar1 * 8 + 0x20,lVar14);
                            }
                            else {
                              FUN_184a97470(lVar13,lVar14,
                                            *(undefined8 *)
                                             (*(longlong *)(*(longlong *)(lVar12 + 0x20) + 0xc0) +
                                             0x70));
                            }
                            plVar15[0xe] = lVar13;
                            thunk_FUN_1803b4a20(plVar15 + 0xe,lVar13);
                            plVar15[0xf] = 0x3ff0000000000000;
                            lVar12 = FUN_18032e960(DAT_187c013d8,1);
                            plVar15[0xd] = lVar12;
                            thunk_FUN_1803b4a20(plVar15 + 0xd,lVar12);
                            if (param_3 != 0) {
                              plVar15[0x22] = *(longlong *)(param_3 + 0x18);
                              thunk_FUN_1803b4a20(plVar15 + 0x22);
                              plVar15[0x23] = *(longlong *)(param_3 + 0x20);
                              thunk_FUN_1803b4a20(plVar15 + 0x23);
                              *(undefined4 *)(plVar15 + 0x34) = *(undefined4 *)(param_3 + 0x28);
                              *(undefined4 *)(plVar15 + 0x10) = *(undefined4 *)(param_2 + 0x10);
                              local_a8 = *(undefined8 *)(param_2 + 0x14);
                              local_a0 = *(undefined4 *)(param_2 + 0x1c);
                              uVar7 = FUN_183aa36e0(&local_a8,0);
                              *(undefined4 *)(plVar15 + 7) = uVar7;
                              lVar12 = thunk_FUN_180383da0(DAT_187bc5830);
                              FUN_1849de5b0(lVar12,DAT_187b7f908);
                              plVar15[10] = lVar12;
                              thunk_FUN_1803b4a20(plVar15 + 10,lVar12);
                              lVar12 = thunk_FUN_180383da0(DAT_187bf1860);
                              FUN_1849de5b0(lVar12,DAT_187b413a0);
                              plVar15[0x11] = lVar12;
                              thunk_FUN_1803b4a20(plVar15 + 0x11,lVar12);
                              local_a8 = *(undefined8 *)(param_2 + 0x138);
                              local_a0 = *(undefined4 *)(param_2 + 0x140);
                              uVar7 = FUN_183aa36e0(&local_a8,0);
                              *(undefined4 *)(plVar15 + 2) = uVar7;
                              local_a8 = *(undefined8 *)(param_2 + 0x144);
                              local_a0 = *(undefined4 *)(param_2 + 0x14c);
                              uVar7 = FUN_183aa36e0(&local_a8,0);
                              *(undefined4 *)((longlong)plVar15 + 0x24) = uVar7;
                              if ((int)plVar15[0xc] == 0x385) {
                                if (param_4 != 0) {
                                  lVar12 = *(longlong *)(param_4 + 0x28);
                                  lVar13 = thunk_FUN_180383da0(DAT_187c162f0);
                                  FUN_1804383d0(lVar13,0);
                                  if (lVar12 != 0) {
                                    uVar7 = 0;
LAB_1814c6a6f:
                                    FUN_1855f0b70(lVar12,uVar7,lVar13,DAT_187b3cb78);
                                    return plVar15;
                                  }
                                }
                              }
                              else {
                                if ((int)plVar15[0xc] != 0x386) {
                                  return plVar15;
                                }
                                if (plVar15[0xb] != 0) {
                                  dVar16 = (double)FUN_1855f06a0(plVar15[0xb],3,DAT_187b40048);
                                  if (param_4 != 0) {
                                    if (0.0 < (float)dVar16) {
                                      *(undefined1 *)(param_4 + 0x54) = 1;
                                    }
                                    lVar12 = *(longlong *)(param_4 + 0x28);
                                    uVar7 = *(undefined4 *)((longlong)plVar15 + 0x2c);
                                    lVar13 = thunk_FUN_180383da0(DAT_187c162f0);
                                    FUN_1804383d0(lVar13,0);
                                    if (lVar13 != 0) {
                                      *(int *)(lVar13 + 0x10) = (int)plVar15[6];
                                      *(undefined4 *)(lVar13 + 0x14) =
                                           *(undefined4 *)((longlong)plVar15 + 0x34);
                                      if (plVar15[0xb] != 0) {
                                        dVar17 = (double)FUN_1855f06a0(plVar15[0xb],0,DAT_187b40048)
                                        ;
                                        if (plVar15[0xb] != 0) {
                                          dVar18 = (double)FUN_1855f06a0(plVar15[0xb],1,
                                                                         DAT_187b40048);
                                          if ((*(longlong *)(param_1 + 0x90) != 0) &&
                                             (lVar14 = *(longlong *)
                                                        (*(longlong *)(param_1 + 0x90) + 0x88),
                                             lVar14 != 0)) {
                                            uVar9 = *(undefined4 *)(lVar14 + 0x428);
                                            uVar10 = *(undefined8 *)(lVar14 + 0x420);
                                            if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
                                              il2cpp_runtime_class_init();
                                            }
                                            local_a8 = uVar10;
                                            local_a0 = uVar9;
                                            iVar8 = FUN_183aa36e0(&local_a8,0);
                                            fVar19 = (float)((double)iVar8 * dVar18 + dVar17);
                                            if (fVar19 < 0.0) {
                                              fVar19 = 0.0;
                                            }
                                            else if (1.0 < fVar19) {
                                              fVar19 = 1.0;
                                            }
                                            *(float *)(lVar13 + 0x18) = fVar19;
                                            if (plVar15[0xb] != 0) {
                                              uVar10 = FUN_1855f06a0(plVar15[0xb],2,DAT_187b40048);
                                              *(undefined8 *)(lVar13 + 0x20) = uVar10;
                                              *(float *)(lVar13 + 0x28) = (float)dVar16;
                                              if (plVar15[0xb] != 0) {
                                                uVar10 = FUN_1855f06a0(plVar15[0xb],4,DAT_187b40048)
                                                ;
                                                uVar9 = FUN_183dd3dd0(uVar10,0);
                                                *(undefined4 *)(lVar13 + 0x2c) = uVar9;
                                                if (lVar12 != 0) goto LAB_1814c6a6f;
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
LAB_1814c6ac3:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::createNormalEffectPrefab  @ 0x14c6ad0  size=1461 ==== */

/* WARNING: Instruction at (ram,0x0001814c700d) overlaps instruction at (ram,0x0001814c700c)
    */
/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_1814c6ad0(longlong param_1,byte *param_2,longlong param_3,undefined8 param_4,
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
  undefined4 local_48;
  undefined4 uStack_44;
  undefined1 auStack_40 [24];
  
  puVar18 = &local_48;
  puVar16 = auStack_148;
  lVar13 = param_3;
  uVar20 = param_4;
  if (DAT_18ab626b5 == '\0') {
    FUN_18032f6d0(&DAT_187c21b68);
    FUN_18032f6d0(&DAT_187c2f828);
    FUN_18032f6d0(&DAT_187b33e08);
    FUN_18032f6d0(&DAT_187b355e0);
    FUN_18032f6d0(&DAT_187b41650);
    FUN_18032f6d0(&DAT_187b65e38);
    FUN_18032f6d0(&DAT_187bcd128);
    FUN_18032f6d0(&DAT_187c38198);
    FUN_18032f6d0(&DAT_187c4ad38);
    FUN_18032f6d0(&DAT_187b42b50);
    FUN_18032f6d0(&DAT_187b5f350);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187c318b8);
    FUN_18032f6d0(&DAT_187c19748);
    FUN_18032f6d0(&DAT_187c31038);
    FUN_18032f6d0(&DAT_187c19590);
    FUN_18032f6d0(&DAT_187b9aea8);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b9b058);
    FUN_18032f6d0(&DAT_187b812d0);
    FUN_18032f6d0(&DAT_187c20b78);
    FUN_18032f6d0(&DAT_187be2298);
    FUN_18032f6d0(&DAT_187b53068);
    FUN_18032f6d0(&DAT_187bd1090);
    FUN_18032f6d0(&DAT_187c1fc40);
    FUN_18032f6d0(&DAT_187b2fec0);
    DAT_18ab626b5 = '\x01';
  }
  bVar21 = (byte)uVar20;
  local_48 = unaff_XMM7_Da;
  uStack_44 = unaff_XMM7_Db;
  lVar11 = thunk_FUN_180383da0(DAT_187c1fc40);
  FUN_1804383d0(lVar11,0);
  puVar17 = auStack_148;
  if (lVar11 == 0) goto LAB_1814c817a;
  *(longlong *)(lVar11 + 0x10) = param_1;
  thunk_FUN_1803b4a20(lVar11 + 0x10,param_1);
  *(byte **)(lVar11 + 0x18) = param_2;
  thunk_FUN_1803b4a20(lVar11 + 0x18,param_2);
  *(longlong *)(lVar11 + 0x20) = param_3;
  thunk_FUN_1803b4a20(lVar11 + 0x20,param_3);
  uVar15 = lVar11 + 0x28;
  *(longlong *)(lVar11 + 0x28) = param_5;
  thunk_FUN_1803b4a20();
  lVar4 = *(longlong *)(lVar11 + 0x18);
  puVar17 = auStack_148;
  if (lVar4 == 0) goto LAB_1814c817a;
  if (*(int *)(lVar4 + 0x50) == 6) {
    if (*(longlong **)(lVar4 + 0x40) == (longlong *)0x0) {
LAB_1814c6e49:
      param_2 = *(byte **)(lVar11 + 0x18);
      puVar17 = auStack_148;
      if (*(longlong *)(lVar11 + 0x28) == 0) goto LAB_1814c817a;
      param_3 = *(longlong *)(*(longlong *)(lVar11 + 0x28) + 0x68);
      if (DAT_18ab62646 == '\0') {
        FUN_18032f6d0(&DAT_187b58d08);
        FUN_18032f6d0(&DAT_187b58ec0);
        FUN_18032f6d0(&DAT_187b59228);
        FUN_18032f6d0(&DAT_187b59590);
        FUN_18032f6d0(&DAT_187b71920);
        DAT_18ab62646 = '\x01';
      }
      puVar17 = auStack_148;
      if (*(longlong *)(param_2 + 0xa8) == 0) goto LAB_1814c817a;
      cVar10 = FUN_185653020(*(longlong *)(param_2 + 0xa8),param_3,DAT_187b58ec0);
      lVar13 = *(longlong *)(param_2 + 0xa8);
      param_5 = param_3;
      if (cVar10 == '\0') {
        puVar17 = auStack_148;
        if (lVar13 == 0) goto LAB_1814c817a;
        uVar20 = DAT_187b58d08;
        FUN_1856640d0(lVar13,param_3,1);
        bVar21 = (byte)uVar20;
        uVar15 = *(ulonglong *)(param_2 + 0xb0);
        puVar17 = auStack_148;
        if (uVar15 == 0) goto LAB_1814c817a;
        lVar13 = DAT_187b71920;
        FUN_180002460();
      }
      else {
        puVar17 = auStack_148;
        if (lVar13 == 0) goto LAB_1814c817a;
        uVar20 = DAT_187b59228;
        cVar10 = FUN_185675470(lVar13,param_3,DAT_187b59228);
        if (cVar10 != '\0') {
          return;
        }
        uVar15 = *(ulonglong *)(param_2 + 0xa8);
        puVar17 = auStack_148;
        if (uVar15 == 0) goto LAB_1814c817a;
        lVar13 = CONCAT71((int7)((ulonglong)uVar20 >> 8),1);
        uVar20 = DAT_187b59590;
        FUN_1856756f0();
        bVar21 = (byte)uVar20;
      }
    }
    else {
      lVar13 = **(longlong **)(lVar4 + 0x40);
      if ((*(byte *)(lVar13 + 0x130) < *(byte *)(DAT_187c21b68 + 0x130)) ||
         (uVar15 = (ulonglong)*(byte *)(DAT_187c21b68 + 0x130), param_5 = DAT_187c21b68,
         *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != DAT_187c21b68)) {
        lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
        if ((*(byte *)(lVar13 + 0x130) < *(byte *)(DAT_187b355e0 + 0x130)) ||
           (uVar15 = (ulonglong)*(byte *)(DAT_187b355e0 + 0x130), param_5 = DAT_187b355e0,
           *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != DAT_187b355e0)) {
          lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
          if ((*(byte *)(lVar13 + 0x130) < *(byte *)(DAT_187b5f350 + 0x130)) ||
             (uVar15 = (ulonglong)*(byte *)(DAT_187b5f350 + 0x130), param_5 = DAT_187b5f350,
             *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != DAT_187b5f350)) {
            lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
            if ((*(byte *)(lVar13 + 0x130) < *(byte *)(DAT_187b65e38 + 0x130)) ||
               (uVar15 = (ulonglong)*(byte *)(DAT_187b65e38 + 0x130), param_5 = DAT_187b65e38,
               *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != DAT_187b65e38)) {
              lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
              if ((*(byte *)(lVar13 + 0x130) < *(byte *)(DAT_187b42b50 + 0x130)) ||
                 (uVar15 = (ulonglong)*(byte *)(DAT_187b42b50 + 0x130), param_5 = DAT_187b42b50,
                 *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != DAT_187b42b50)) {
                lVar13 = **(longlong **)(*(longlong *)(lVar11 + 0x18) + 0x40);
                if ((*(byte *)(lVar13 + 0x130) < *(byte *)(DAT_187bcd128 + 0x130)) ||
                   (uVar15 = (ulonglong)*(byte *)(DAT_187bcd128 + 0x130), param_5 = DAT_187bcd128,
                   *(longlong *)(*(longlong *)(lVar13 + 200) + -8 + uVar15 * 8) != DAT_187bcd128))
                goto LAB_1814c6e49;
              }
            }
          }
        }
      }
    }
  }
  uVar5 = *(ulonglong *)(lVar11 + 0x18);
  puVar17 = auStack_148;
  if (uVar5 == 0) goto LAB_1814c817a;
  if (*(int *)(uVar5 + 0xa0) != 0) {
    puVar17 = auStack_148;
    if (*(longlong *)(lVar11 + 0x20) == 0) goto LAB_1814c817a;
    uVar15 = uVar5;
    if (*(int *)(uVar5 + 0xa0) != *(int *)(*(longlong *)(lVar11 + 0x20) + 0x130)) {
      return;
    }
  }
  if (*(int *)(*(longlong *)(lVar11 + 0x18) + 0x4c) == 1) {
    puVar17 = auStack_148;
    if (*(longlong *)(lVar11 + 0x20) == 0) goto LAB_1814c817a;
    if (*(char *)(*(longlong *)(lVar11 + 0x20) + 0x141) != '\0') {
      return;
    }
  }
  if ((in_stack_00000050 != '\0') && (*(char *)(param_1 + 0x9c) != '\0')) {
    return;
  }
  piVar6 = *(int **)(lVar11 + 0x18);
  if ((((char)piVar6[0x2e] == '\0') && (piVar6[0x14] != 1)) && (piVar6[0x14] != 6)) {
    pcVar12 = (char *)FUN_1814a9080(param_1,0);
    puVar17 = auStack_148;
    if (pcVar12 == (char *)0x0) goto LAB_1814c817a;
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
    if (lVar13 == 0) goto LAB_1814c817a;
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
    puVar18 = (undefined4 *)CONCAT44(uStack_44,local_48);
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
        lVar13 = thunk_FUN_180383da0(_DAT_187c2f828);
        uVar20 = _DAT_187b2fec0;
      }
      else {
        *(undefined **)(puVar16 + -8) = &UNK_1814c80d1;
        lVar13 = thunk_FUN_180383da0(_DAT_187c2f828);
        param_1 = lVar11;
        uVar20 = _DAT_187bd1090;
      }
      *(undefined **)(puVar16 + -8) = &UNK_1814c80fa;
      FUN_180afad00(lVar13,param_1,uVar20,0);
      unaff_R12[0x25] = lVar13;
      *(undefined **)(puVar16 + -8) = &UNK_1814c8112;
      thunk_FUN_1803b4a20(unaff_R12 + 0x25,lVar13);
      *(undefined8 *)(puVar16 + -8) = 0x1814c811c;
      FUN_1810580b0(param_4,0);
      return;
    }
  }
LAB_1814c817a:
                    /* WARNING: Subroutine does not return */
  *(undefined **)(puVar17 + -8) = &UNK_1814c817f;
  FUN_18032f9d0();
}



/* ==== UnitActionController::createNormalPrefabWithDelay  @ 0x14c82c0  size=211 ==== */

longlong FUN_1814c82c0(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined1 param_4,
                      undefined1 param_5,undefined1 param_6,undefined1 param_7)

{
  longlong lVar1;
  
  if (DAT_18ab626b3 == '\0') {
    FUN_18032f6d0(&DAT_187c21930);
    DAT_18ab626b3 = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c21930);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x30) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x30,param_1);
    *(undefined8 *)(lVar1 + 0x38) = param_2;
    thunk_FUN_1803b4a20(lVar1 + 0x38,param_2);
    *(undefined8 *)(lVar1 + 0x28) = param_3;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_3);
    *(undefined1 *)(lVar1 + 0x40) = param_5;
    *(undefined1 *)(lVar1 + 0x41) = param_6;
    *(undefined1 *)(lVar1 + 0x42) = param_7;
    *(undefined1 *)(lVar1 + 0x20) = param_4;
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::createNormalPrefabWithDelayAndTarget  @ 0x14c81e0  size=211 ==== */

longlong FUN_1814c81e0(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined4 param_4,
                      undefined8 param_5,undefined1 param_6)

{
  longlong lVar1;
  
  if (DAT_18ab626b4 == '\0') {
    FUN_18032f6d0(&DAT_187c21ae8);
    DAT_18ab626b4 = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c21ae8);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x38) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x38,param_1);
    *(undefined8 *)(lVar1 + 0x40) = param_2;
    thunk_FUN_1803b4a20(lVar1 + 0x40,param_2);
    *(undefined8 *)(lVar1 + 0x28) = param_3;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_3);
    *(undefined8 *)(lVar1 + 0x48) = param_5;
    *(undefined4 *)(lVar1 + 0x30) = param_4;
    thunk_FUN_1803b4a20(lVar1 + 0x48);
    *(undefined1 *)(lVar1 + 0x20) = param_6;
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::createPrefab  @ 0x14c83a0  size=1488 ==== */

longlong *
FUN_1814c83a0(longlong param_1,longlong param_2,undefined8 param_3,undefined8 param_4,
             longlong *param_5)

{
  uint uVar1;
  longlong lVar2;
  longlong lVar3;
  longlong *plVar4;
  char cVar5;
  longlong lVar6;
  undefined8 uVar7;
  longlong *plVar8;
  longlong lVar9;
  undefined8 uVar10;
  ulonglong uVar11;
  undefined8 *puVar12;
  ushort uVar13;
  ulonglong uVar14;
  
  if (DAT_18ab626b6 == '\0') {
    FUN_18032f6d0(&DAT_187b2f280);
    FUN_18032f6d0(&DAT_187c0b200);
    FUN_18032f6d0(&DAT_187c4ad38);
    FUN_18032f6d0(&DAT_187b8ae18);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187c318b8);
    FUN_18032f6d0(&DAT_187b82c20);
    FUN_18032f6d0(&DAT_187bd1248);
    FUN_18032f6d0(&DAT_187bd1400);
    FUN_18032f6d0(&DAT_187bd15b8);
    FUN_18032f6d0(&DAT_187c1fdf0);
    DAT_18ab626b6 = '\x01';
  }
  lVar6 = thunk_FUN_180383da0(DAT_187c1fdf0);
  FUN_1804383d0(lVar6,0);
  if (lVar6 != 0) {
    *(undefined8 *)(lVar6 + 0x10) = param_3;
    thunk_FUN_1803b4a20(lVar6 + 0x10,param_3);
    *(undefined8 *)(lVar6 + 0x18) = param_4;
    thunk_FUN_1803b4a20(lVar6 + 0x18,param_4);
    lVar9 = *(longlong *)(param_1 + 0x90);
    if (lVar9 != 0) {
      if ((*(char *)(lVar9 + 0x165) == '\0') && (cVar5 = FUN_181605d30(lVar9,0), cVar5 == '\0')) {
        if (param_2 == 0) goto LAB_1814c8945;
        lVar9 = *(longlong *)(param_2 + 0x10);
      }
      else {
        if (param_2 == 0) goto LAB_1814c8945;
        lVar9 = *(longlong *)(param_2 + 0x18);
      }
      *param_5 = lVar9;
      thunk_FUN_1803b4a20(param_5);
      if (*param_5 != 0) {
        uVar7 = FUN_18620c860(*param_5,0);
        if (DAT_18ab626db == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626db = '\x01';
        }
        lVar9 = *(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x18);
        if ((lVar9 != 0) &&
           (plVar8 = (longlong *)
                     FUN_1800121c0(0,DAT_187b8ae18,lVar9,*param_5,*(undefined8 *)(param_1 + 0x90)),
           plVar8 != (longlong *)0x0)) {
          lVar9 = FUN_186202760(plVar8,0);
          uVar10 = FUN_1838e90b0(0);
          if (lVar9 != 0) {
            FUN_186218890(lVar9,uVar10,0);
            lVar9 = DAT_187c318b8;
            if ((*(longlong *)(lVar6 + 0x10) != 0) &&
               (lVar2 = *(longlong *)(*(longlong *)(lVar6 + 0x10) + 0x98), lVar2 != 0)) {
              *(int *)(lVar2 + 0x1c) = *(int *)(lVar2 + 0x1c) + 1;
              lVar3 = *(longlong *)(lVar2 + 0x10);
              uVar1 = *(uint *)(lVar2 + 0x18);
              if (lVar3 != 0) {
                if (uVar1 < *(uint *)(lVar3 + 0x18)) {
                  *(uint *)(lVar2 + 0x18) = uVar1 + 1;
                  if (*(uint *)(lVar3 + 0x18) <= uVar1) {
                    /* WARNING: Subroutine does not return */
                    FUN_18032f9c0();
                  }
                  *(longlong **)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = plVar8;
                  thunk_FUN_1803b4a20(lVar3 + ((longlong)(int)uVar1 + 4) * 8,plVar8);
                }
                else {
                  FUN_184a97470(lVar2,plVar8,
                                *(undefined8 *)
                                 (*(longlong *)(*(longlong *)(lVar9 + 0x20) + 0xc0) + 0x70));
                }
                uVar14 = 0;
                if (*(int *)(param_2 + 0x4c) == 1) {
                  if ((*(longlong *)(lVar6 + 0x10) == 0) ||
                     (lVar9 = *(longlong *)(*(longlong *)(lVar6 + 0x10) + 0xa0), lVar9 == 0))
                  goto LAB_1814c8945;
                  FUN_180002460(lVar9,plVar8,DAT_187c318b8);
                  lVar9 = plVar8[0x11];
                  uVar10 = thunk_FUN_180383da0(DAT_187b2f280);
                  FUN_180afad00(uVar10,lVar6,DAT_187bd1248,0);
                  lVar9 = FUN_185c29720(lVar9,uVar10,0);
                  uVar10 = DAT_187b2f280;
                  uVar11 = uVar14;
                  if ((lVar9 != 0) &&
                     (uVar11 = thunk_FUN_180382aa0(lVar9,DAT_187b2f280), uVar11 == 0)) {
                    /* WARNING: Subroutine does not return */
                    FUN_18032e920(lVar9,uVar10);
                  }
                  plVar8[0x11] = uVar11;
                  thunk_FUN_1803b4a20(plVar8 + 0x11,uVar11);
                }
                if ((char)plVar8[10] != '\0') {
                  lVar9 = *plVar8;
                  if (((*(byte *)(lVar9 + 0x130) < *(byte *)(DAT_187c4ad38 + 0x130)) ||
                      (*(longlong *)
                        (*(longlong *)(lVar9 + 200) + -8 +
                        (ulonglong)*(byte *)(DAT_187c4ad38 + 0x130) * 8) != DAT_187c4ad38)) &&
                     ((*(byte *)(lVar9 + 0x130) < *(byte *)(DAT_187b82c20 + 0x130) ||
                      (*(longlong *)
                        (*(longlong *)(lVar9 + 200) + -8 +
                        (ulonglong)*(byte *)(DAT_187b82c20 + 0x130) * 8) != DAT_187b82c20)))) {
                    if (((*(longlong *)(lVar6 + 0x18) == 0) ||
                        (lVar9 = *(longlong *)(*(longlong *)(lVar6 + 0x18) + 0x68), lVar9 == 0)) ||
                       (lVar9 = *(longlong *)(lVar9 + 0x100), lVar9 == 0)) goto LAB_1814c8945;
                    FUN_180002460(lVar9,plVar8,DAT_187c318b8);
                    lVar9 = plVar8[0x11];
                    uVar10 = thunk_FUN_180383da0(DAT_187b2f280);
                    FUN_180afad00(uVar10,lVar6,DAT_187bd1400,0);
                    lVar9 = FUN_185c29720(lVar9,uVar10,0);
                    uVar10 = DAT_187b2f280;
                    uVar11 = uVar14;
                    if ((lVar9 != 0) &&
                       (uVar11 = thunk_FUN_180382aa0(lVar9,DAT_187b2f280), uVar11 == 0)) {
                    /* WARNING: Subroutine does not return */
                      FUN_18032e920(lVar9,uVar10);
                    }
                    plVar8[0x11] = uVar11;
                    thunk_FUN_1803b4a20(plVar8 + 0x11,uVar11);
                  }
                }
                lVar9 = plVar8[0x11];
                uVar10 = thunk_FUN_180383da0(DAT_187b2f280);
                FUN_180afad00(uVar10,lVar6,DAT_187bd15b8,0);
                lVar6 = FUN_185c29720(lVar9,uVar10,0);
                uVar10 = DAT_187b2f280;
                uVar11 = uVar14;
                if ((lVar6 != 0) && (uVar11 = thunk_FUN_180382aa0(lVar6,DAT_187b2f280), uVar11 == 0)
                   ) {
                    /* WARNING: Subroutine does not return */
                  FUN_18032e920(lVar6,uVar10);
                }
                plVar8[0x11] = uVar11;
                thunk_FUN_1803b4a20(plVar8 + 0x11,uVar11);
                if (*(char *)(param_2 + 0x28) == '\0') {
                  return plVar8;
                }
                if (DAT_18ab626da == '\0') {
                  FUN_18032f6d0(&DAT_187bfaba0);
                  DAT_18ab626da = '\x01';
                }
                plVar4 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
                if (plVar4 != (longlong *)0x0) {
                  lVar6 = *plVar4;
                  if (*(ushort *)(lVar6 + 0x12e) != 0) {
                    do {
                      if (*(longlong *)(*(longlong *)(lVar6 + 0xb0) + uVar14 * 0x10) ==
                          DAT_187b93630) {
                        puVar12 = (undefined8 *)
                                  ((longlong)
                                   (*(int *)(*(longlong *)(lVar6 + 0xb0) + 8 + uVar14 * 0x10) + 0x10
                                   ) * 0x10 + 0x138 + lVar6);
                        goto LAB_1814c88d2;
                      }
                      uVar13 = (short)uVar14 + 1;
                      uVar14 = (ulonglong)uVar13;
                    } while (uVar13 < *(ushort *)(lVar6 + 0x12e));
                  }
                  puVar12 = (undefined8 *)FUN_18032c930(plVar4,DAT_187b93630,0x10);
LAB_1814c88d2:
                  lVar6 = (*(code *)*puVar12)(plVar4,puVar12[1]);
                  uVar10 = FUN_1862025e0(plVar8,0);
                  if (lVar6 != 0) {
                    FUN_185685e10(lVar6,uVar7,uVar10,DAT_187c0b200);
                    return plVar8;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
LAB_1814c8945:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::execActionConsiderTargetNum  @ 0x14c8980  size=289 ==== */

void FUN_1814c8980(undefined8 param_1,undefined8 param_2,longlong param_3)

{
  int iVar1;
  longlong lVar2;
  int iVar3;
  
  if (DAT_18ab626d2 == '\0') {
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b812d0);
    DAT_18ab626d2 = '\x01';
  }
  iVar3 = 0;
  if (param_3 != 0) {
    while (*(longlong *)(param_3 + 0x50) != 0) {
      if (*(int *)(*(longlong *)(param_3 + 0x50) + 0x18) <= iVar3) {
        return;
      }
      iVar1 = 1;
      if (*(int *)(param_3 + 0x1c) != 0) {
        iVar1 = *(int *)(param_3 + 0x1c);
      }
      if (iVar1 <= iVar3) {
        return;
      }
      lVar2 = FUN_1849ed2d0(*(undefined8 *)(param_3 + 0x50),iVar3,DAT_187b812d0);
      if (lVar2 == 0) break;
      FUN_1837b2d90(param_3,*(undefined8 *)(lVar2 + 0x68),0,0);
      FUN_1814bda70(param_1,param_3,param_2,lVar2,0,0,1,0,0,0);
      iVar3 = iVar3 + 1;
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::execActionOnWaveStart  @ 0x14c8ab0  size=171 ==== */

void FUN_1814c8ab0(longlong param_1,longlong param_2)

{
  longlong lVar1;
  longlong *plVar2;
  int iVar3;
  
  if (DAT_18ab6269c == '\0') {
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c19e18);
    DAT_18ab6269c = '\x01';
  }
  iVar3 = 0;
  if (param_2 != 0) {
    while (lVar1 = *(longlong *)(param_2 + 0x20), lVar1 != 0) {
      if (*(int *)(lVar1 + 0x18) <= iVar3) {
        return;
      }
      plVar2 = (longlong *)FUN_1849ed2d0(lVar1,iVar3,DAT_187c19e18);
      if (plVar2 == (longlong *)0x0) break;
      (**(code **)(*plVar2 + 0x228))
                (plVar2,param_2,*(undefined8 *)(param_1 + 0x90),param_1,
                 *(undefined8 *)(*plVar2 + 0x230));
      iVar3 = iVar3 + 1;
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::filterTargetByFlight  @ 0x14c8b60  size=930 ==== */

void FUN_1814c8b60(undefined8 param_1,longlong param_2)

{
  undefined8 uVar1;
  undefined8 uVar2;
  char cVar3;
  longlong lVar4;
  longlong lVar5;
  longlong lVar6;
  
  if (DAT_18ab626bb == '\0') {
    FUN_18032f6d0(&DAT_187bd9b10);
    FUN_18032f6d0(&DAT_187bf0170);
    FUN_18032f6d0(&DAT_187b80c00);
    FUN_18032f6d0(&DAT_187b82670);
    FUN_18032f6d0(&DAT_187bcff60);
    FUN_18032f6d0(&DAT_187bd0118);
    FUN_18032f6d0(&DAT_187bd02d0);
    FUN_18032f6d0(&DAT_187bd1e50);
    FUN_18032f6d0(&DAT_187c20678);
    FUN_18032f6d0(&DAT_187c1f720);
    DAT_18ab626bb = '\x01';
  }
  if (param_2 == 0) goto LAB_1814c8efd;
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
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    lVar4 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x88);
    if (lVar4 == 0) {
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar2 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
      lVar4 = thunk_FUN_180383da0(DAT_187bf0170);
      FUN_184cacb40(lVar4,uVar2,DAT_187bd0118,0);
      *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x88) = lVar4;
      thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x88,lVar4);
    }
    cVar3 = FUN_184789f00(uVar1,lVar4,DAT_187bd9b10);
    if (cVar3 == '\0') {
      return;
    }
    lVar4 = *(longlong *)(param_2 + 0x50);
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    lVar5 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x90);
    if (lVar5 == 0) {
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar1 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
      lVar5 = thunk_FUN_180383da0(DAT_187b82670);
      FUN_184cacb40(lVar5,uVar1,DAT_187bd02d0,0);
      *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x90) = lVar5;
      lVar6 = *(longlong *)(DAT_187c1f720 + 0xb8) + 0x90;
LAB_1814c8db7:
      thunk_FUN_1803b4a20(lVar6,lVar5);
    }
  }
  else if (*(int *)(param_2 + 0x18) < 1) {
    lVar6 = thunk_FUN_180383da0(DAT_187c20678);
    FUN_1804383d0(lVar6,0);
    if (lVar6 == 0) goto LAB_1814c8efd;
    *(undefined1 *)(lVar6 + 0x10) = *(undefined1 *)(param_2 + 0x98);
    lVar4 = *(longlong *)(param_2 + 0x50);
    lVar5 = thunk_FUN_180383da0(DAT_187b82670);
    FUN_184cacb40(lVar5,lVar6,DAT_187bd1e50,0);
  }
  else {
    lVar4 = *(longlong *)(param_2 + 0x50);
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    lVar5 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x80);
    if (lVar5 == 0) {
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar1 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
      lVar5 = thunk_FUN_180383da0(DAT_187b82670);
      FUN_184cacb40(lVar5,uVar1,DAT_187bcff60,0);
      *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x80) = lVar5;
      lVar6 = *(longlong *)(DAT_187c1f720 + 0xb8) + 0x80;
      goto LAB_1814c8db7;
    }
  }
  if (lVar4 != 0) {
    FUN_184a9d8f0(lVar4,lVar5,DAT_187b80c00);
    return;
  }
LAB_1814c8efd:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::foregroundActiveWithDelay  @ 0x14c8f10  size=127 ==== */

longlong FUN_1814c8f10(undefined8 param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626a7 == '\0') {
    FUN_18032f6d0(&DAT_187c21c98);
    DAT_18ab626a7 = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c21c98);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x28) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_1);
    *(undefined4 *)(lVar1 + 0x20) = param_2;
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::get_ContinuousActionEndDone  @ 0x5e9a40  size=8 ==== */

undefined1 FUN_1805e9a40(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xb8);
}



/* ==== UnitActionController::get_DisableUBByModeChange  @ 0x14c8f90  size=8 ==== */

undefined1 FUN_1814c8f90(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x9a);
}



/* ==== UnitActionController::get_IsCancelModeChangeByTrigger  @ 0x661010  size=8 ==== */

undefined1 FUN_180661010(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xa8);
}



/* ==== UnitActionController::get_ModeChanging  @ 0x14c8fa0  size=8 ==== */

undefined1 FUN_1814c8fa0(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x9b);
}



/* ==== UnitActionController::get_MoveEnd  @ 0x14c8fb0  size=8 ==== */

undefined1 FUN_1814c8fb0(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xa9);
}



/* ==== UnitActionController::get_OnNormalAttackStart  @ 0x438c50  size=8 ==== */

undefined8 FUN_180438c50(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xa0);
}



/* ==== UnitActionController::get_Owner  @ 0x438ab0  size=8 ==== */

undefined8 FUN_180438ab0(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x90);
}



/* ==== UnitActionController::get_StopModeChangeEndEffectCalled  @ 0xa9fb60  size=8 ==== */

undefined1 FUN_180a9fb60(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x9c);
}



/* ==== UnitActionController::get_UnionBurstCharging  @ 0xf3f0f0  size=8 ==== */

undefined1 FUN_180f3f0f0(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x99);
}



/* ==== UnitActionController::get_UnionBurstIsChargeTime  @ 0xe0b070  size=8 ==== */

undefined1 FUN_180e0b070(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x98);
}



/* ==== UnitActionController::get_battleCameraEffect  @ 0x14c8fc0  size=55 ==== */

undefined8 FUN_1814c8fc0(void)

{
  if (DAT_18ab62693 == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab62693 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x10);
}



/* ==== UnitActionController::get_battleEffectPool  @ 0x14c9000  size=55 ==== */

undefined8 FUN_1814c9000(void)

{
  if (DAT_18ab62694 == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab62694 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x18);
}



/* ==== UnitActionController::get_battleManager  @ 0x14c9040  size=55 ==== */

undefined8 FUN_1814c9040(void)

{
  if (DAT_18ab62692 == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab62692 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
}



/* ==== UnitActionController::get_battleTimeScale  @ 0x14c9080  size=55 ==== */

undefined8 FUN_1814c9080(void)

{
  if (DAT_18ab62695 == '\0') {
    FUN_18032f6d0(&DAT_187bfaba0);
    DAT_18ab62695 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 0x20);
}



/* ==== UnitActionController::get_isUnionBurstOnlyOwner  @ 0x14c90c0  size=8 ==== */

undefined1 FUN_1814c90c0(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xb9);
}



/* ==== UnitActionController::get_skillDictionary  @ 0x44ad60  size=8 ==== */

undefined8 FUN_18044ad60(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xb0);
}



/* ==== UnitActionController::get_updateBranchMotionRunning  @ 0x8dba80  size=8 ==== */

undefined1 FUN_1808dba80(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xba);
}



/* ==== UnitActionController::initWhenNoTarget  @ 0x14c90d0  size=382 ==== */

void FUN_1814c90d0(undefined8 param_1,longlong param_2,longlong param_3)

{
  undefined8 uVar1;
  longlong lVar2;
  undefined4 uVar3;
  longlong lVar4;
  longlong lVar5;
  int iVar6;
  
  if (DAT_18ab626b2 == '\0') {
    FUN_18032f6d0(&DAT_187b44508);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187b446b8);
    FUN_18032f6d0(&DAT_187b8e140);
    FUN_18032f6d0(&DAT_187b8e2f8);
    FUN_18032f6d0(&DAT_187b8df90);
    FUN_18032f6d0(&DAT_187b8e660);
    FUN_18032f6d0(&DAT_187badb60);
    DAT_18ab626b2 = '\x01';
  }
  if (param_2 != 0) {
    FUN_183a23ad0(*(undefined8 *)(param_2 + 0xe0),0);
    uVar1 = *(undefined8 *)(param_2 + 0x88);
    lVar4 = thunk_FUN_180383da0(DAT_187badb60);
    FUN_184cdd840(lVar4,uVar1,DAT_187b8df90);
    if (lVar4 != 0) {
      while( true ) {
        if (*(int *)(lVar4 + 0x20) < 1) {
          return;
        }
        if (param_3 == 0) break;
        lVar5 = *(longlong *)(param_3 + 0x20);
        uVar3 = FUN_184cda8f0(lVar4,DAT_187b8e140);
        if (lVar5 == 0) break;
        lVar5 = FUN_1849ed2d0(lVar5,uVar3,DAT_187c19e18);
        if (lVar5 == 0) break;
        FUN_183a23ad0(*(undefined8 *)(lVar5 + 0xe0),0);
        iVar6 = 0;
        while( true ) {
          lVar2 = *(longlong *)(lVar5 + 0x88);
          if (lVar2 == 0) goto LAB_1814c9249;
          if (*(int *)(lVar2 + 0x18) <= iVar6) break;
          uVar3 = FUN_184a610f0(lVar2,iVar6,DAT_187b446b8);
          FUN_184cdb860(lVar4,uVar3,DAT_187b8e2f8);
          iVar6 = iVar6 + 1;
        }
      }
    }
  }
LAB_1814c9249:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::isInActionTargetArea  @ 0x14c9250  size=397 ==== */

void FUN_1814c9250(longlong param_1,longlong param_2,longlong param_3,undefined8 *param_4,
                  undefined1 param_5,undefined8 param_6)

{
  int iVar1;
  longlong lVar2;
  undefined4 *puVar3;
  float fVar4;
  float fVar5;
  undefined8 local_38;
  undefined4 local_30;
  
  if (param_3 != 0) {
    lVar2 = *(longlong *)(param_1 + 0x90);
    switch(*(undefined4 *)(param_3 + 0x24)) {
    case 1:
    case 4:
    case 7:
      if (lVar2 == 0) goto LAB_1814c93d8;
      iVar1 = 1;
      if (*(char *)(lVar2 + 0x165) != '\0') {
        iVar1 = -1;
      }
      break;
    default:
      iVar1 = 0;
    }
    fVar5 = *(float *)(param_3 + 0x20);
    if (fVar5 <= 0.0) {
      if (param_2 == 0) goto LAB_1814c93d8;
      if (*(int *)(param_2 + 0x78) == 1) {
        if ((lVar2 == 0) || (*(longlong *)(lVar2 + 0xb8) == 0)) goto LAB_1814c93d8;
        fVar5 = *(float *)(*(longlong *)(lVar2 + 0xb8) + 0x18);
      }
      else {
        fVar5 = *(float *)(param_2 + 0x7c);
      }
    }
    if (iVar1 == 1) {
      fVar4 = 0.0;
    }
    else {
      fVar4 = -fVar5;
      if (iVar1 == -1) {
        fVar5 = 0.0;
      }
    }
    if (((*(longlong *)(param_1 + 0x20) != 0) &&
        (lVar2 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x18), lVar2 != 0)) &&
       (lVar2 = FUN_186215a70(lVar2,0), lVar2 != 0)) {
      puVar3 = (undefined4 *)FUN_186217dd0(&local_38,lVar2,0);
      local_38 = *param_4;
      local_30 = *(undefined4 *)(param_4 + 1);
      FUN_1814c9400(param_1,param_3,&local_38,param_5,*puVar3,fVar4,fVar5,param_6,0);
      return;
    }
  }
LAB_1814c93d8:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::judgeIsInTargetArea  @ 0x14c9400  size=425 ==== */

bool FUN_1814c9400(longlong param_1,longlong param_2,float *param_3,char param_4,float param_5,
                  float param_6,float param_7,longlong *param_8)

{
  char cVar1;
  undefined8 *puVar2;
  float extraout_XMM0_Da;
  float fVar3;
  float fVar4;
  undefined1 local_38 [48];
  
  if (param_2 != 0) {
    if (((*(int *)(param_2 + 0x24) == 7) || (*(int *)(param_2 + 0x24) == 8)) ||
       (*(int *)(param_2 + 0x24) == 9)) {
      if ((param_8 == (longlong *)0x0) || (param_8[0xd] == 0)) goto LAB_1814c95a4;
      cVar1 = FUN_181605eb0(param_8[0xd],0);
      if (cVar1 != '\0') {
        return false;
      }
    }
    if (param_8 != (longlong *)0x0) {
      cVar1 = (**(code **)(*param_8 + 0x398))(param_8,*(undefined8 *)(*param_8 + 0x3a0));
      if (cVar1 == '\0') {
        return false;
      }
      puVar2 = (undefined8 *)
               (**(code **)(*param_8 + 0x178))(local_38,param_8,*(undefined8 *)(*param_8 + 0x180));
      fVar4 = (float)*puVar2 / param_5 - *param_3 / param_5;
      (**(code **)(*param_8 + 0x198))(param_8,*(undefined8 *)(*param_8 + 0x1a0));
      fVar3 = extraout_XMM0_Da;
      if (param_4 != '\0') {
        if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814c95a4;
        fVar3 = extraout_XMM0_Da + *(float *)(*(longlong *)(param_1 + 0x90) + 0x118);
      }
      param_6 = param_6 - fVar3 * 0.5;
      param_7 = fVar3 * 0.5 + param_7;
      if (((fVar4 < param_6) || (param_7 < fVar4)) &&
         ((cVar1 = FUN_183dd3110(fVar4,param_6,0), cVar1 == '\0' &&
          (((cVar1 = FUN_183dd3110(fVar4,param_7,0), cVar1 == '\0' &&
            (*(int *)(param_2 + 0x24) != 3)) && (*(int *)(param_2 + 0x24) != 6)))))) {
        return *(int *)(param_2 + 0x24) == 9;
      }
      return true;
    }
  }
LAB_1814c95a4:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::judgeIsTarget  @ 0x14c95b0  size=421 ==== */

bool FUN_1814c95b0(longlong param_1,longlong param_2,longlong param_3)

{
  undefined8 uVar1;
  longlong lVar2;
  longlong lVar3;
  char cVar4;
  bool bVar5;
  longlong lVar6;
  undefined4 local_38;
  undefined4 uStack_34;
  undefined4 uStack_30;
  undefined4 uStack_2c;
  undefined8 local_28;
  
  if (DAT_18ab626b9 == '\0') {
    FUN_18032f6d0(&DAT_187b5e978);
    FUN_18032f6d0(&DAT_187b61160);
    DAT_18ab626b9 = '\x01';
  }
  if (param_2 == 0) goto LAB_1814c9750;
  lVar2 = *(longlong *)(param_2 + 0x38);
  lVar3 = *(longlong *)(param_2 + 0xd0);
  if (lVar2 == 0) goto LAB_1814c9750;
  cVar4 = FUN_18395fce0(lVar2,*(undefined8 *)(param_1 + 0x90),param_3,0);
  if ((cVar4 == '\0') && (*(char *)(lVar2 + 0x260) == '\0')) {
    cVar4 = FUN_181605e10(param_2,0);
    if (cVar4 != '\0') {
      if (param_3 == 0) goto LAB_1814c9750;
      if (*(int *)(param_3 + 0x14) != 0x17) goto LAB_1814c974c;
    }
    if (lVar3 == 0) goto LAB_1814c9750;
    if (*(char *)(lVar3 + 0xb9) == '\0') {
      lVar6 = *(longlong *)(param_2 + 0x88);
      if (lVar6 == 0) goto LAB_1814c9750;
      local_38 = *(undefined4 *)(lVar6 + 0x430);
      uStack_34 = *(undefined4 *)(lVar6 + 0x434);
      uStack_30 = *(undefined4 *)(lVar6 + 0x438);
      uStack_2c = *(undefined4 *)(lVar6 + 0x43c);
      uVar1 = *(undefined8 *)(lVar6 + 0x440);
      if (*(int *)(DAT_187b61160 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      local_28 = uVar1;
      lVar6 = FUN_183aa7a70(&local_38,0);
      if (lVar6 < 1) goto LAB_1814c96d3;
    }
    else {
LAB_1814c96d3:
      if (*(char *)(lVar3 + 0xbd) == '\0') goto LAB_1814c974c;
    }
    if (param_3 == 0) {
LAB_1814c9750:
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    if (*(int *)(param_3 + 0x10) == 1) {
      uVar1 = *(undefined8 *)(param_1 + 0x90);
      if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      cVar4 = FUN_18620ca00(uVar1,param_2,0);
      if (cVar4 == '\0') goto LAB_1814c974c;
      if ((*(int *)(param_3 + 0x10) == 1) && (*(int *)(param_3 + 0x1c) == 1)) {
        cVar4 = FUN_18394da00(lVar2,0x43);
        return cVar4 == '\0';
      }
    }
    bVar5 = true;
  }
  else {
LAB_1814c974c:
    bVar5 = false;
  }
  return bVar5;
}



/* ==== UnitActionController::onFirearmHit  @ 0x14c9760  size=1010 ==== */

void FUN_1814c9760(longlong param_1,longlong param_2)

{
  int iVar1;
  longlong *plVar2;
  longlong lVar3;
  char cVar4;
  longlong lVar5;
  undefined8 uVar6;
  undefined8 *puVar7;
  ushort uVar8;
  ulonglong uVar9;
  uint uVar10;
  ulonglong uVar11;
  ulonglong uVar12;
  ulonglong in_stack_ffffffffffffffb8;
  ulonglong in_stack_ffffffffffffffc8;
  
  if (DAT_18ab626b7 == '\0') {
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187c313a0);
    FUN_18032f6d0(&DAT_187c19c60);
    FUN_18032f6d0(&DAT_187c24e18);
    FUN_18032f6d0(&DAT_187c31550);
    FUN_18032f6d0(&DAT_187c19e18);
    FUN_18032f6d0(&DAT_187c24fc8);
    FUN_18032f6d0(&DAT_187b5e978);
    DAT_18ab626b7 = '\x01';
  }
  if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
    il2cpp_runtime_class_init();
  }
  cVar4 = FUN_18620c870(param_2,0,0);
  if (cVar4 != '\0') {
    return;
  }
  if (param_2 != 0) {
    if (*(longlong *)(param_2 + 0x108) == 0) {
      return;
    }
    if (*(longlong *)(param_2 + 0x120) == 0) {
      return;
    }
    uVar12 = 0;
    uVar9 = uVar12;
    while (lVar5 = *(longlong *)(param_2 + 0x140), lVar5 != 0) {
      if (*(int *)(lVar5 + 0x18) <= (int)uVar9) {
        if (*(longlong *)(param_2 + 0x118) != 0) {
          iVar1 = *(int *)(*(longlong *)(param_2 + 0x118) + 0x18);
          uVar9 = uVar12;
          if (0 < iVar1) goto LAB_1814c9940;
          goto LAB_1814c9988;
        }
        break;
      }
      lVar5 = FUN_1849ed2d0(lVar5,uVar9,DAT_187c24fc8);
      if (lVar5 == 0) break;
      if (*(int *)(lVar5 + 0x1c) == 0) {
        if (*(longlong *)(param_2 + 0x140) == 0) break;
        uVar6 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x140),uVar9,DAT_187c24fc8);
        uVar6 = FUN_1814c3b10(param_1,uVar6,*(undefined8 *)(param_2 + 0x120),0,0);
        if (*(longlong *)(param_2 + 0x120) == 0) break;
        uVar11 = uVar12;
        if (0.0 < *(float *)(*(longlong *)(param_2 + 0x120) + 0xb0)) {
          uVar11 = *(ulonglong *)(param_1 + 0x90);
        }
        if (param_1 == 0) break;
        in_stack_ffffffffffffffb8 = 0;
        FUN_1814bbc70(param_1,uVar6,0,uVar11,0);
      }
      uVar9 = (ulonglong)((int)uVar9 + 1);
    }
  }
  goto LAB_1814c9b4d;
  while( true ) {
    uVar6 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x118),uVar9,DAT_187c19e18);
    in_stack_ffffffffffffffc8 = 0;
    in_stack_ffffffffffffffb8 = CONCAT71((int7)(in_stack_ffffffffffffffb8 >> 8),1);
    FUN_1814bfb90(param_1,uVar6,*(undefined8 *)(param_2 + 0x120),0,in_stack_ffffffffffffffb8,0,0);
    uVar10 = (int)uVar9 + 1;
    uVar9 = (ulonglong)uVar10;
    if (iVar1 <= (int)uVar10) break;
LAB_1814c9940:
    if (*(longlong *)(param_2 + 0x118) == 0) goto LAB_1814c9b4d;
  }
LAB_1814c9988:
  if (*(longlong *)(param_2 + 0x110) != 0) {
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x110) + 0x18);
    uVar9 = uVar12;
    if (0 < iVar1) {
      do {
        if (*(longlong *)(param_2 + 0x110) == 0) goto LAB_1814c9b4d;
        lVar5 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x110),uVar9,DAT_187c31550);
        if (DAT_18ab626da == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626da = '\x01';
        }
        plVar2 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
        if (plVar2 == (longlong *)0x0) goto LAB_1814c9b4d;
        lVar3 = *plVar2;
        uVar8 = 0;
        if (*(ushort *)(lVar3 + 0x12e) != 0) {
          do {
            if (*(longlong *)(*(longlong *)(lVar3 + 0xb0) + (ulonglong)uVar8 * 0x10) ==
                DAT_187b93630) {
              puVar7 = (undefined8 *)
                       ((longlong)
                        (*(int *)(*(longlong *)(lVar3 + 0xb0) + 8 + (ulonglong)uVar8 * 0x10) + 0x18)
                        * 0x10 + 0x138 + lVar3);
              goto LAB_1814c9a59;
            }
            uVar8 = uVar8 + 1;
          } while (uVar8 < *(ushort *)(lVar3 + 0x12e));
        }
        puVar7 = (undefined8 *)FUN_18032c930(plVar2,DAT_187b93630,0x18);
LAB_1814c9a59:
        cVar4 = (*(code *)*puVar7)(plVar2,puVar7[1]);
        if (cVar4 == '\0') {
LAB_1814c9a8b:
          in_stack_ffffffffffffffc8 = in_stack_ffffffffffffffc8 & 0xffffffffffffff00;
          uVar6 = FUN_1814c82c0(param_1,lVar5,*(undefined8 *)(param_2 + 0x120),0,
                                in_stack_ffffffffffffffb8 & 0xffffffffffffff00,1,
                                in_stack_ffffffffffffffc8,0);
          if (*(longlong *)(param_2 + 0x120) == 0) goto LAB_1814c9b4d;
          uVar11 = uVar12;
          if (0.0 < *(float *)(*(longlong *)(param_2 + 0x120) + 0xb0)) {
            uVar11 = *(ulonglong *)(param_1 + 0x90);
          }
          if (param_1 == 0) goto LAB_1814c9b4d;
          in_stack_ffffffffffffffb8 = 0;
          FUN_1814bbc70(param_1,uVar6,0,uVar11,0);
        }
        else {
          if (lVar5 == 0) goto LAB_1814c9b4d;
          if ((*(int *)(lVar5 + 0x50) != 0) && (2 < *(int *)(lVar5 + 0x50) - 2U))
          goto LAB_1814c9a8b;
        }
        uVar10 = (int)uVar9 + 1;
        uVar9 = (ulonglong)uVar10;
      } while ((int)uVar10 < iVar1);
    }
    return;
  }
LAB_1814c9b4d:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::searchAndSortTarget  @ 0x14c9b60  size=2314 ==== */

void FUN_1814c9b60(longlong param_1,longlong param_2,longlong param_3,undefined8 *param_4,
                  undefined1 param_5,undefined1 param_6)

{
  uint uVar1;
  longlong lVar2;
  longlong lVar3;
  int iVar4;
  bool bVar5;
  char cVar6;
  undefined8 uVar7;
  undefined8 uVar8;
  longlong lVar9;
  undefined4 *puVar10;
  longlong lVar11;
  int iVar12;
  float fVar13;
  float fVar14;
  undefined8 uVar15;
  undefined4 uVar16;
  undefined8 uVar17;
  undefined4 uVar18;
  undefined8 local_58;
  undefined4 local_50;
  
  if (DAT_18ab626a3 == '\0') {
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187b7fc80);
    FUN_18032f6d0(&DAT_187b7fff8);
    FUN_18032f6d0(&DAT_187b80db0);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b812d0);
    FUN_18032f6d0(&DAT_187b81480);
    FUN_18032f6d0(&DAT_187b5e978);
    DAT_18ab626a3 = '\x01';
  }
  local_58 = *param_4;
  local_50 = *(undefined4 *)(param_4 + 1);
  iVar12 = 0;
  uVar17 = 0;
  FUN_1814ca490(param_1,param_3,&local_58,param_2,param_6,0);
  if (*(longlong *)(param_1 + 0x90) != 0) {
    uVar7 = FUN_1816056d0(*(longlong *)(param_1 + 0x90),0);
    uVar15 = 0;
    FUN_1814cc700(param_1,param_3,uVar7,param_5,0);
    bVar5 = false;
    if (param_3 != 0) {
      while( true ) {
        uVar16 = (undefined4)((ulonglong)uVar15 >> 0x20);
        uVar18 = (undefined4)((ulonglong)uVar17 >> 0x20);
        if (*(longlong *)(param_3 + 0x50) == 0) break;
        if (*(int *)(*(longlong *)(param_3 + 0x50) + 0x18) - *(int *)(param_3 + 0x18) <= iVar12) {
          if (bVar5) {
            if (*(longlong *)(param_3 + 0x50) != 0) {
              iVar12 = *(int *)(*(longlong *)(param_3 + 0x50) + 0x18);
              iVar4 = iVar12;
              goto LAB_1814c9d00;
            }
          }
          else {
            if (*(int *)(param_3 + 0x18) == 0) goto LAB_1814c9dff;
            lVar11 = *(longlong *)(param_3 + 0x50);
            if (lVar11 != 0) {
              if (*(int *)(lVar11 + 0x18) == 0) goto LAB_1814c9dff;
              uVar17 = FUN_1849ed2d0(lVar11,*(int *)(lVar11 + 0x18) + -1,DAT_187b812d0);
              lVar11 = *(longlong *)(param_3 + 0x50);
              if (lVar11 != 0) {
                iVar12 = *(int *)(lVar11 + 0x18);
                *(int *)(lVar11 + 0x1c) = *(int *)(lVar11 + 0x1c) + 1;
                *(undefined4 *)(lVar11 + 0x18) = 0;
                if (0 < iVar12) {
                  FUN_185be98b0(*(undefined8 *)(lVar11 + 0x10),0,iVar12,0);
                }
                lVar9 = DAT_187b7fc80;
                lVar11 = *(longlong *)(param_3 + 0x50);
                if (lVar11 != 0) {
                  *(int *)(lVar11 + 0x1c) = *(int *)(lVar11 + 0x1c) + 1;
                  lVar2 = *(longlong *)(lVar11 + 0x10);
                  uVar1 = *(uint *)(lVar11 + 0x18);
                  if (lVar2 != 0) {
                    if (uVar1 < *(uint *)(lVar2 + 0x18)) {
                      *(uint *)(lVar11 + 0x18) = uVar1 + 1;
                      if (*(uint *)(lVar2 + 0x18) <= uVar1) goto LAB_1814ca466;
                      *(undefined8 *)(lVar2 + 0x20 + (longlong)(int)uVar1 * 8) = uVar17;
                      thunk_FUN_1803b4a20(lVar2 + 0x20 + (longlong)(int)uVar1 * 8,uVar17);
                    }
                    else {
                      FUN_184a97470(lVar11,uVar17,
                                    *(undefined8 *)
                                     (*(longlong *)(*(longlong *)(lVar9 + 0x20) + 0xc0) + 0x70));
                    }
                    goto LAB_1814c9dff;
                  }
                }
              }
            }
          }
          break;
        }
        uVar7 = *(undefined8 *)(param_3 + 0x50);
        bVar5 = true;
        uVar8 = FUN_1849ed2d0(uVar7,*(int *)(param_3 + 0x18) + iVar12,DAT_187b812d0);
        FUN_184aa4620(uVar7,iVar12,uVar8);
        iVar12 = iVar12 + 1;
      }
    }
  }
  goto LAB_1814ca460;
LAB_1814c9d00:
  uVar16 = (undefined4)((ulonglong)uVar15 >> 0x20);
  uVar18 = (undefined4)((ulonglong)uVar17 >> 0x20);
  iVar4 = iVar4 + -1;
  if (iVar4 < iVar12 - *(int *)(param_3 + 0x18)) goto LAB_1814c9dff;
  if (*(longlong *)(param_3 + 0x50) == 0) goto LAB_1814ca460;
  FUN_184a9dd20(*(longlong *)(param_3 + 0x50),iVar4,DAT_187b80db0);
  goto LAB_1814c9d00;
LAB_1814c9dff:
  lVar11 = 0;
  if (((*(int *)(param_3 + 0x10) != 1) || (*(int *)(param_3 + 0x1c) != 1)) ||
     (*(char *)(param_3 + 0x1a5) != '\0')) goto LAB_1814ca426;
  lVar9 = *(longlong *)(param_1 + 0x90);
  if (lVar9 == 0) goto LAB_1814ca460;
  lVar2 = *(longlong *)(lVar9 + 0x38);
  if (*(char *)(lVar9 + 0x178) == '\0') {
    if (lVar2 == 0) goto LAB_1814ca460;
    cVar6 = FUN_1839540a0(lVar2,0);
    lVar11 = 0;
    if (cVar6 == '\0') {
      if (DAT_18ab626da == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814ca460;
      uVar17 = FUN_180002270(4,DAT_187b93630);
      if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      cVar6 = FUN_18620ca00(uVar17,0,0);
      if (cVar6 != '\0') {
        if (DAT_18ab626da == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626da = '\x01';
        }
        lVar11 = *(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
LAB_1814ca11c:
        if (lVar11 == 0) goto LAB_1814ca460;
        uVar17 = 4;
LAB_1814ca12a:
        lVar11 = FUN_180002270(uVar17,DAT_187b93630);
        if (lVar11 == 0) goto LAB_1814ca460;
        lVar11 = *(longlong *)(lVar11 + 0x80);
        if (((*(longlong *)(param_1 + 0x90) == 0) ||
            (lVar9 = FUN_1814af720(*(longlong *)(param_1 + 0x90),0), lVar9 == 0)) ||
           (puVar10 = (undefined4 *)FUN_1814afa20(&local_58,lVar9,0), lVar11 == 0))
        goto LAB_1814ca460;
        lVar11 = FUN_183991830(lVar11,0,*puVar10,0);
      }
    }
    else {
      if (DAT_18ab626da == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814ca460;
      uVar17 = FUN_180002270(3,DAT_187b93630);
      if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      cVar6 = FUN_18620ca00(uVar17,0,0);
      if (cVar6 != '\0') {
        if (DAT_18ab626da == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626da = '\x01';
        }
        if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814ca460;
        uVar17 = 3;
        goto LAB_1814ca12a;
      }
    }
  }
  else {
    if (lVar2 == 0) goto LAB_1814ca460;
    cVar6 = FUN_1839540a0(lVar2,0);
    if (cVar6 == '\0') {
      if (DAT_18ab626da == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814ca460;
      uVar17 = FUN_180002270(3,DAT_187b93630);
      if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      cVar6 = FUN_18620ca00(uVar17,0,0);
      if (cVar6 != '\0') {
        if (DAT_18ab626da == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626da = '\x01';
        }
        if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814ca460;
        uVar17 = 3;
        goto LAB_1814ca12a;
      }
    }
    else {
      if (DAT_18ab626da == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814ca460;
      uVar17 = FUN_180002270(4,DAT_187b93630);
      if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      cVar6 = FUN_18620ca00(uVar17,0,0);
      if (cVar6 != '\0') {
        if (DAT_18ab626da == '\0') {
          FUN_18032f6d0(&DAT_187bfaba0);
          DAT_18ab626da = '\x01';
        }
        lVar11 = *(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
        goto LAB_1814ca11c;
      }
    }
  }
  if ((*(longlong *)(param_1 + 0x90) != 0) &&
     (lVar9 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar9 != 0)) {
    cVar6 = FUN_183954f50(lVar9,0);
    if (cVar6 != '\0') {
      if ((*(longlong *)(param_1 + 0x90) == 0) ||
         (lVar9 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar9 == 0))
      goto LAB_1814ca460;
      cVar6 = FUN_1839540a0(lVar9,0);
      if (cVar6 == '\0') {
        if ((*(longlong *)(param_1 + 0x90) == 0) ||
           (lVar11 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x38), lVar11 == 0))
        goto LAB_1814ca460;
        lVar11 = *(longlong *)(lVar11 + 0x180);
      }
    }
    if (lVar11 == 0) {
LAB_1814ca426:
      *(undefined1 *)(param_3 + 0x1a4) = 1;
      return;
    }
    lVar9 = *(longlong *)(lVar11 + 0x68);
    if ((lVar9 != 0) && (*(longlong *)(lVar9 + 0xd0) != 0)) {
      if (*(char *)(*(longlong *)(lVar9 + 0xd0) + 0xb9) != '\0') goto LAB_1814ca426;
      if (*(longlong *)(lVar9 + 0x38) != 0) {
        cVar6 = FUN_18394d880(*(longlong *)(lVar9 + 0x38),*(undefined8 *)(param_1 + 0x90),0);
        if (cVar6 != '\0') goto LAB_1814ca426;
        lVar9 = *(longlong *)(param_1 + 0x90);
        switch(*(undefined4 *)(param_3 + 0x24)) {
        case 1:
        case 4:
        case 7:
          if (lVar9 == 0) goto LAB_1814ca460;
          iVar12 = 1;
          if (*(char *)(lVar9 + 0x165) != '\0') {
            iVar12 = -1;
          }
          break;
        default:
          iVar12 = 0;
        }
        fVar14 = *(float *)(param_3 + 0x20);
        if (fVar14 <= 0.0) {
          if (param_2 == 0) goto LAB_1814ca460;
          if (*(int *)(param_2 + 0x78) == 1) {
            if ((lVar9 == 0) || (*(longlong *)(lVar9 + 0xb8) == 0)) goto LAB_1814ca460;
            fVar14 = *(float *)(*(longlong *)(lVar9 + 0xb8) + 0x18);
          }
          else {
            fVar14 = *(float *)(param_2 + 0x7c);
          }
        }
        if (iVar12 == 1) {
          fVar13 = 0.0;
        }
        else {
          fVar13 = -fVar14;
          if (iVar12 == -1) {
            fVar14 = 0.0;
          }
        }
        if (((*(longlong *)(param_1 + 0x20) != 0) &&
            (lVar9 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x18), lVar9 != 0)) &&
           (lVar9 = FUN_186215a70(lVar9,0), lVar9 != 0)) {
          puVar10 = (undefined4 *)FUN_186217dd0(&local_58,lVar9,0);
          local_58 = *param_4;
          local_50 = *(undefined4 *)(param_4 + 1);
          cVar6 = FUN_1814c9400(param_1,param_3,&local_58,param_6,CONCAT44(uVar16,*puVar10),
                                CONCAT44(uVar18,fVar13),fVar14,lVar11,0);
          if (cVar6 == '\0') goto LAB_1814ca426;
          lVar9 = *(longlong *)(param_3 + 0x50);
          if (lVar9 != 0) {
            iVar12 = *(int *)(lVar9 + 0x18);
            *(int *)(lVar9 + 0x1c) = *(int *)(lVar9 + 0x1c) + 1;
            *(undefined4 *)(lVar9 + 0x18) = 0;
            if (0 < iVar12) {
              FUN_185be98b0(*(undefined8 *)(lVar9 + 0x10),0,iVar12,0);
            }
            lVar2 = DAT_187b7fc80;
            lVar9 = *(longlong *)(param_3 + 0x50);
            if (lVar9 != 0) {
              *(int *)(lVar9 + 0x1c) = *(int *)(lVar9 + 0x1c) + 1;
              lVar3 = *(longlong *)(lVar9 + 0x10);
              uVar1 = *(uint *)(lVar9 + 0x18);
              if (lVar3 != 0) {
                if (uVar1 < *(uint *)(lVar3 + 0x18)) {
                  *(uint *)(lVar9 + 0x18) = uVar1 + 1;
                  if (*(uint *)(lVar3 + 0x18) <= uVar1) {
LAB_1814ca466:
                    /* WARNING: Subroutine does not return */
                    FUN_18032f9c0();
                  }
                  *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar11;
                  thunk_FUN_1803b4a20(lVar3 + 0x20 + (longlong)(int)uVar1 * 8,lVar11);
                }
                else {
                  FUN_184a97470(lVar9,lVar11,
                                *(undefined8 *)
                                 (*(longlong *)(*(longlong *)(lVar2 + 0x20) + 0xc0) + 0x70));
                }
                goto LAB_1814ca426;
              }
            }
          }
        }
      }
    }
  }
LAB_1814ca460:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::searchTargetUnit  @ 0x14ca490  size=4825 ==== */

void FUN_1814ca490(longlong param_1,longlong param_2,undefined8 *param_3,longlong param_4,
                  undefined1 param_5)

{
  undefined4 uVar1;
  undefined8 uVar2;
  char cVar3;
  char cVar4;
  uint uVar5;
  longlong lVar6;
  undefined4 *puVar7;
  longlong lVar8;
  longlong lVar9;
  undefined8 uVar10;
  longlong *plVar11;
  undefined8 *puVar12;
  float *pfVar13;
  longlong *plVar14;
  undefined8 uVar15;
  int iVar16;
  int iVar17;
  int iVar18;
  longlong lVar19;
  bool bVar20;
  float fVar21;
  float fVar22;
  float fVar23;
  longlong local_168;
  int local_160;
  undefined8 local_158;
  undefined4 local_150;
  longlong local_148;
  undefined8 local_138;
  undefined4 local_130;
  undefined8 local_128;
  undefined8 local_118;
  undefined8 local_108;
  undefined8 uStack_100;
  undefined8 local_f8;
  undefined1 local_e8 [16];
  undefined1 local_d8 [16];
  undefined1 local_c8 [16];
  undefined1 local_b8 [128];
  
  if (DAT_18ab626b8 == '\0') {
    FUN_18032f6d0(&DAT_187b33478);
    FUN_18032f6d0(&DAT_187bbdcb8);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187b71ad8);
    FUN_18032f6d0(&DAT_187b7fc80);
    FUN_18032f6d0(&DAT_187b7fff8);
    FUN_18032f6d0(&DAT_187b72388);
    FUN_18032f6d0(&DAT_187b71760);
    FUN_18032f6d0(&DAT_187b9ead8);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b73698);
    FUN_18032f6d0(&DAT_187b9ec90);
    FUN_18032f6d0(&DAT_187b73858);
    FUN_18032f6d0(&DAT_187c32990);
    FUN_18032f6d0(&DAT_187b61160);
    FUN_18032f6d0(&DAT_187b97c88);
    FUN_18032f6d0(&DAT_187bd0488);
    FUN_18032f6d0(&DAT_187bd0640);
    FUN_18032f6d0(&DAT_187bd07f8);
    FUN_18032f6d0(&DAT_187bd09b0);
    FUN_18032f6d0(&DAT_187bd0b68);
    FUN_18032f6d0(&DAT_187bd1770);
    FUN_18032f6d0(&DAT_187c1ffa8);
    FUN_18032f6d0(&DAT_187bd1928);
    FUN_18032f6d0(&DAT_187c20158);
    FUN_18032f6d0(&DAT_187bd1ae0);
    FUN_18032f6d0(&DAT_187c20310);
    FUN_18032f6d0(&DAT_187bd1c98);
    FUN_18032f6d0(&DAT_187c204c0);
    FUN_18032f6d0(&DAT_187c1f720);
    FUN_18032f6d0(&DAT_187b2fb58);
    FUN_18032f6d0(&DAT_187b2fd10);
    DAT_18ab626b8 = '\x01';
  }
  if (((*(longlong *)(param_1 + 0x20) == 0) ||
      (lVar6 = *(longlong *)(*(longlong *)(param_1 + 0x20) + 0x18), lVar6 == 0)) ||
     (lVar6 = FUN_186215a70(lVar6,0), lVar6 == 0)) goto LAB_1814cb764;
  puVar7 = (undefined4 *)FUN_186217dd0(&local_158,lVar6,0);
  uVar1 = *puVar7;
  if ((param_2 == 0) || (lVar6 = *(longlong *)(param_2 + 0x50), lVar6 == 0)) goto LAB_1814cb764;
  iVar16 = *(int *)(lVar6 + 0x18);
  *(int *)(lVar6 + 0x1c) = *(int *)(lVar6 + 0x1c) + 1;
  *(undefined4 *)(lVar6 + 0x18) = 0;
  if (0 < iVar16) {
    FUN_185be98b0(*(undefined8 *)(lVar6 + 0x10),0,iVar16,0);
  }
  lVar6 = *(longlong *)(param_1 + 0x90);
  if (lVar6 == 0) goto LAB_1814cb764;
  lVar19 = *(longlong *)(lVar6 + 0x38);
  cVar4 = *(char *)(lVar6 + 0x178);
  local_148 = lVar19;
  if (lVar19 == 0) goto LAB_1814cb764;
  cVar3 = FUN_1839540a0(lVar19,0);
  if (cVar3 == '\0') {
    if (cVar4 != '\0') {
      if (DAT_18ab626da == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814cb764;
      uVar15 = 5;
      goto LAB_1814ca8a3;
    }
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814cb764;
    uVar15 = 6;
LAB_1814ca817:
    lVar6 = FUN_180002270(uVar15,DAT_187b93630);
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814cb764;
    uVar15 = 5;
  }
  else {
    if (cVar4 == '\0') {
      if (DAT_18ab626da == '\0') {
        FUN_18032f6d0(&DAT_187bfaba0);
        DAT_18ab626da = '\x01';
      }
      if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814cb764;
      uVar15 = 5;
      goto LAB_1814ca817;
    }
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814cb764;
    uVar15 = 6;
LAB_1814ca8a3:
    lVar6 = FUN_180002270(uVar15,DAT_187b93630);
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814cb764;
    uVar15 = 6;
  }
  lVar8 = FUN_180002270(uVar15,DAT_187b93630);
  iVar16 = *(int *)(param_2 + 0x10);
  lVar9 = lVar8;
  if (((iVar16 != 0) && (lVar9 = lVar6, iVar16 != 1)) &&
     ((lVar9 = lVar8, iVar16 != 2 && (lVar9 = 0, iVar16 == 3)))) {
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    if (*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) goto LAB_1814cb764;
    uVar15 = FUN_180002270(5,DAT_187b93630);
    lVar9 = thunk_FUN_180383da0(DAT_187c32990);
    FUN_184aa4390(lVar9,uVar15,DAT_187b71760);
    if (DAT_18ab626da == '\0') {
      FUN_18032f6d0(&DAT_187bfaba0);
      DAT_18ab626da = '\x01';
    }
    if ((*(longlong *)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8) == 0) ||
       (uVar15 = FUN_180002270(6,DAT_187b93630), lVar9 == 0)) goto LAB_1814cb764;
    FUN_184a97390(lVar9,uVar15,DAT_187b71ad8);
  }
  iVar16 = *(int *)(param_2 + 0x14);
  if ((999 < iVar16) && (iVar16 = (iVar16 / 1000) * 1000, 11000 < iVar16)) {
    if (iVar16 < 0x32c9) {
      if (iVar16 == 12000) {
LAB_1814cad1e:
        lVar6 = thunk_FUN_180383da0(DAT_187c1ffa8);
        FUN_1804383d0(lVar6,0);
        if (lVar6 == 0) goto LAB_1814cb764;
        *(int *)(lVar6 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        lVar8 = thunk_FUN_180383da0(DAT_187b97c88);
        uVar15 = DAT_187bd1770;
        goto LAB_1814caaa8;
      }
      if (iVar16 == 13000) {
        lVar6 = thunk_FUN_180383da0(DAT_187c20158);
        FUN_1804383d0(lVar6,0);
        if (lVar6 == 0) goto LAB_1814cb764;
        *(int *)(lVar6 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        uVar15 = thunk_FUN_180383da0(DAT_187b97c88);
        FUN_184cacb40(uVar15,lVar6,DAT_187bd1928,0);
        if (lVar9 == 0) goto LAB_1814cb764;
        lVar9 = FUN_184a990e0(lVar9,uVar15,DAT_187b72388);
      }
    }
    else {
      if (iVar16 == 14000) {
        lVar6 = thunk_FUN_180383da0(DAT_187c20310);
        FUN_1804383d0(lVar6,0);
        if (lVar6 == 0) goto LAB_1814cb764;
        *(int *)(lVar6 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        lVar8 = thunk_FUN_180383da0(DAT_187b97c88);
        uVar15 = DAT_187bd1ae0;
LAB_1814caaa8:
        FUN_184cacb40(lVar8,lVar6,uVar15,0);
        goto joined_r0x0001814cb061;
      }
      if (iVar16 == 15000) {
        lVar6 = thunk_FUN_180383da0(DAT_187c204c0);
        FUN_1804383d0(lVar6,0);
        if (lVar6 == 0) goto LAB_1814cb764;
        *(longlong *)(lVar6 + 0x18) = param_1;
        thunk_FUN_1803b4a20(lVar6 + 0x18,param_1);
        *(int *)(lVar6 + 0x10) = *(int *)(param_2 + 0x14) % 1000;
        uVar15 = thunk_FUN_180383da0(DAT_187b97c88);
        FUN_184cacb40(uVar15,lVar6,DAT_187bd1c98,0);
        if (lVar9 == 0) goto LAB_1814cb764;
        lVar9 = FUN_184a990e0(lVar9,uVar15,DAT_187b72388);
      }
    }
    goto switchD_1814cac55_caseD_2a;
  }
  if (0x22 < iVar16) {
    switch(iVar16) {
    case 0x29:
    case 0x2b:
    case 0x2c:
    case 0x32:
switchD_1814cac55_caseD_29:
      uVar10 = thunk_FUN_180383da0(DAT_187b97c88);
      uVar15 = DAT_187b2fb58;
      goto LAB_1814cb087;
    case 0x2a:
    case 0x2d:
    case 0x2e:
    case 0x30:
    case 0x31:
      break;
    case 0x2f:
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      lVar8 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x78);
      if (lVar8 == 0) {
        if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
          il2cpp_runtime_class_init();
        }
        uVar15 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
        lVar8 = thunk_FUN_180383da0(DAT_187b97c88);
        FUN_184cacb40(lVar8,uVar15,DAT_187bd0b68,0);
        *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x78) = lVar8;
        thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x78,lVar8);
      }
      goto joined_r0x0001814cb061;
    case 0x33:
      uVar10 = thunk_FUN_180383da0(DAT_187b97c88);
      uVar15 = DAT_187b2fd10;
LAB_1814cb087:
      FUN_184cacb40(uVar10,param_1,uVar15,0);
      if (lVar9 == 0) goto LAB_1814cb764;
      lVar9 = FUN_184a990e0(lVar9,uVar10,DAT_187b72388);
      break;
    default:
      if (iVar16 == 11000) goto LAB_1814cad1e;
    }
    goto switchD_1814cac55_caseD_2a;
  }
  switch(iVar16) {
  case 0x12:
  case 0x16:
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    lVar8 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x58);
    if (lVar8 == 0) {
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar15 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
      lVar8 = thunk_FUN_180383da0(DAT_187b97c88);
      FUN_184cacb40(lVar8,uVar15,DAT_187bd0488,0);
      *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x58) = lVar8;
      thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x58,lVar8);
    }
    break;
  case 0x13:
    goto switchD_1814cac55_caseD_2a;
  case 0x14:
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    lVar8 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x70);
    if (lVar8 == 0) {
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar15 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
      lVar8 = thunk_FUN_180383da0(DAT_187b97c88);
      FUN_184cacb40(lVar8,uVar15,DAT_187bd09b0,0);
      *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x70) = lVar8;
      thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x70,lVar8);
    }
    break;
  case 0x15:
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    lVar8 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x68);
    if (lVar8 == 0) {
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      uVar15 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
      lVar8 = thunk_FUN_180383da0(DAT_187b97c88);
      FUN_184cacb40(lVar8,uVar15,DAT_187bd07f8,0);
      *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x68) = lVar8;
      thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x68,lVar8);
    }
    break;
  case 0x17:
    if ((*(longlong *)(param_1 + 0x90) == 0) ||
       (lVar6 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x1b0), lVar6 == 0))
    goto LAB_1814cb764;
    uVar15 = FUN_1855377d0(lVar6,DAT_187b33478);
    lVar9 = FUN_1847b2640(uVar15,DAT_187bbdcb8);
    if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
      il2cpp_runtime_class_init(DAT_187c1f720);
    }
    lVar8 = *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x60);
    if (lVar8 == 0) {
      if (*(int *)(DAT_187c1f720 + 0xe4) == 0) {
        il2cpp_runtime_class_init(DAT_187c1f720);
      }
      uVar15 = **(undefined8 **)(DAT_187c1f720 + 0xb8);
      lVar8 = thunk_FUN_180383da0(DAT_187b97c88);
      FUN_184cacb40(lVar8,uVar15,DAT_187bd0640,0);
      *(longlong *)(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x60) = lVar8;
      thunk_FUN_1803b4a20(*(longlong *)(DAT_187c1f720 + 0xb8) + 0x60,lVar8);
    }
    break;
  default:
    if (iVar16 == 0x22) goto switchD_1814cac55_caseD_29;
    goto switchD_1814cac55_caseD_2a;
  }
joined_r0x0001814cb061:
  if (lVar9 != 0) {
    lVar9 = FUN_184a990e0(lVar9,lVar8,DAT_187b72388);
switchD_1814cac55_caseD_2a:
    fVar23 = *(float *)(param_2 + 0x20);
    if ((*(int *)(param_2 + 0x14) == 10) || (*(int *)(param_2 + 0x14) == 9)) {
      iVar16 = 0;
      if (lVar9 != 0) {
        iVar17 = *(int *)(lVar9 + 0x18);
        if (0 < iVar17) {
          do {
            lVar6 = FUN_1849ed2d0(lVar9,iVar16,DAT_187b73858);
            cVar4 = FUN_1814c95b0(param_1,lVar6,param_2,0);
            if (cVar4 != '\0') {
              if (lVar6 == 0) goto LAB_1814cb764;
              lVar19 = *(longlong *)(lVar6 + 0x80);
              cVar4 = FUN_181605dc0(lVar6,0);
              if (cVar4 == '\0') {
                lVar6 = *(longlong *)(param_2 + 0x50);
                if ((lVar19 == 0) || (uVar15 = FUN_183991830(lVar19,0,0), lVar6 == 0))
                goto LAB_1814cb764;
                FUN_180002460(lVar6,uVar15,DAT_187b7fc80);
              }
              else {
                if (lVar19 == 0) goto LAB_1814cb764;
                lVar6 = *(longlong *)(lVar19 + 0x18);
                iVar18 = 0;
                if (lVar6 == 0) goto LAB_1814cb764;
                for (; iVar18 < *(int *)(lVar6 + 0x18); iVar18 = iVar18 + 1) {
                  plVar14 = (longlong *)FUN_1849ed2d0(lVar6,iVar18,DAT_187b9ec90);
                  if (plVar14 == (longlong *)0x0) goto LAB_1814cb764;
                  cVar4 = (**(code **)(*plVar14 + 0x398))(plVar14,*(undefined8 *)(*plVar14 + 0x3a0))
                  ;
                  if (cVar4 != '\0') {
                    if (*(longlong *)(param_2 + 0x50) == 0) goto LAB_1814cb764;
                    FUN_180002460(*(longlong *)(param_2 + 0x50),plVar14,DAT_187b7fc80);
                  }
                }
              }
            }
            iVar16 = iVar16 + 1;
          } while (iVar16 < iVar17);
        }
        FUN_1814c8b60(param_1,param_2,0);
        return;
      }
    }
    else {
      lVar6 = *(longlong *)(param_1 + 0x90);
      switch(*(undefined4 *)(param_2 + 0x24)) {
      case 1:
      case 4:
      case 7:
        if (lVar6 == 0) goto LAB_1814cb764;
        uVar5 = 1;
        if (*(char *)(lVar6 + 0x165) != '\0') {
          uVar5 = 0xffffffff;
        }
        break;
      default:
        uVar5 = 0;
      }
      if (fVar23 <= 0.0) {
        if (param_4 == 0) goto LAB_1814cb764;
        if (*(int *)(param_4 + 0x78) == 1) {
          if ((lVar6 == 0) || (*(longlong *)(lVar6 + 0xb8) == 0)) goto LAB_1814cb764;
          fVar23 = *(float *)(*(longlong *)(lVar6 + 0xb8) + 0x18);
        }
        else {
          fVar23 = *(float *)(param_4 + 0x7c);
        }
      }
      if ((int)uVar5 < 1) {
        fVar22 = -fVar23;
        if (0x7fffffff < uVar5) {
          fVar23 = 0.0;
        }
      }
      else {
        fVar22 = 0.0;
      }
      local_168 = 0;
      local_160 = 0;
      if (lVar9 != 0) {
        iVar16 = *(int *)(lVar9 + 0x18);
        lVar6 = 0;
        if (0 < iVar16) {
LAB_1814cb1b0:
          lVar6 = FUN_1849ed2d0(lVar9,local_160,DAT_187b73858);
          if (lVar6 != 0) {
            lVar19 = *(longlong *)(lVar6 + 0x80);
            cVar4 = FUN_181605dc0(lVar6,0);
            if (cVar4 == '\0') {
              if (lVar19 != 0) {
                uVar15 = FUN_183991830(lVar19,0,0,0);
                local_150 = *(undefined4 *)(param_3 + 1);
                local_158 = *param_3;
                cVar4 = FUN_1814c9400(param_1,param_2,&local_158,param_5,uVar1,fVar22,fVar23,uVar15,
                                      0);
                if (cVar4 == '\0') goto LAB_1814cb56a;
                cVar4 = FUN_1814c95b0(param_1,lVar6,param_2);
                if (cVar4 == '\0') {
                  lVar6 = *(longlong *)(lVar6 + 0x88);
                  if (lVar6 != 0) {
                    uVar10 = *(undefined8 *)(lVar6 + 0x430);
                    uVar2 = *(undefined8 *)(lVar6 + 0x438);
                    uVar15 = *(undefined8 *)(lVar6 + 0x440);
                    if (*(int *)(DAT_187b61160 + 0xe4) == 0) {
                      il2cpp_runtime_class_init();
                    }
                    local_108 = uVar10;
                    uStack_100 = uVar2;
                    local_f8 = uVar15;
                    lVar6 = FUN_183aa7a70(&local_108,0);
                    if (lVar6 == 0) {
                      local_168 = FUN_183991830(lVar19,0,0,0);
                    }
                    goto LAB_1814cb56a;
                  }
                }
                else {
                  lVar6 = *(longlong *)(param_2 + 0x50);
                  plVar14 = (longlong *)FUN_183991830(lVar19,0,0,0);
                  if (lVar6 != 0) goto LAB_1814cb551;
                }
              }
            }
            else {
              if ((((*(int *)(param_2 + 0x14) == 5) || (*(int *)(param_2 + 0x14) == 6)) ||
                  (*(int *)(param_2 + 0x14) == 0xd)) || (*(int *)(param_2 + 0x14) == 0xc)) {
                bVar20 = true;
              }
              else {
                bVar20 = *(int *)(param_2 + 0x14) == 0x13;
              }
              if (lVar19 != 0) {
                lVar19 = *(longlong *)(lVar19 + 0x18);
                plVar14 = (longlong *)0x0;
                iVar17 = 0;
                if (lVar19 != 0) {
LAB_1814cb23d:
                  do {
                    if (*(int *)(lVar19 + 0x18) <= iVar17) goto LAB_1814cb417;
                    plVar11 = (longlong *)FUN_1849ed2d0(lVar19,iVar17,DAT_187b9ec90);
                    local_130 = *(undefined4 *)(param_3 + 1);
                    local_138 = *param_3;
                    cVar4 = FUN_1814c9400(param_1,param_2,&local_138,param_5,uVar1,fVar22,fVar23,
                                          plVar11,0);
                    if ((cVar4 != '\0') && (cVar4 = FUN_1814c95b0(param_1,lVar6), cVar4 != '\0')) {
                      if (!bVar20) {
                        if (*(longlong *)(param_2 + 0x50) == 0) break;
                        FUN_180002460(*(longlong *)(param_2 + 0x50),plVar11);
                        iVar17 = iVar17 + 1;
                        goto LAB_1814cb23d;
                      }
                      if (plVar14 != (longlong *)0x0) {
                        puVar12 = (undefined8 *)
                                  (**(code **)(*plVar14 + 0x178))
                                            (local_e8,plVar14,*(undefined8 *)(*plVar14 + 0x180));
                        local_128 = *puVar12;
                        if ((*(longlong *)(param_1 + 0x90) == 0) ||
                           (lVar8 = FUN_1814af720(*(longlong *)(param_1 + 0x90),0), lVar8 == 0))
                        break;
                        pfVar13 = (float *)FUN_1814afa20(local_d8,lVar8,0);
                        fVar21 = (float)local_128 - *pfVar13;
                        if (plVar11 == (longlong *)0x0) break;
                        puVar12 = (undefined8 *)(**(code **)(*plVar11 + 0x178))(local_c8,plVar11);
                        local_118 = *puVar12;
                        if ((*(longlong *)(param_1 + 0x90) == 0) ||
                           (lVar8 = FUN_1814af720(*(longlong *)(param_1 + 0x90),0), lVar8 == 0))
                        break;
                        pfVar13 = (float *)FUN_1814afa20(local_b8,lVar8);
                        if ((ABS(fVar21) <= ABS((float)local_118 - *pfVar13)) &&
                           (cVar4 = FUN_183dd3110(ABS(fVar21)), cVar4 == '\0')) goto LAB_1814cb410;
                      }
                      plVar14 = plVar11;
                    }
LAB_1814cb410:
                    iVar17 = iVar17 + 1;
                  } while( true );
                }
              }
            }
          }
          goto LAB_1814cb764;
        }
LAB_1814cb583:
        FUN_1814c8b60(param_1,param_2,0);
        if (*(int *)(param_2 + 0x14) == 3) {
          if (*(longlong *)(param_2 + 0x50) == 0) goto LAB_1814cb764;
          if (((*(int *)(*(longlong *)(param_2 + 0x50) + 0x18) == 0) && (lVar6 != 0)) &&
             (cVar4 = FUN_1839540a0(lVar19,0), cVar4 == '\0')) {
            if (*(longlong *)(param_2 + 0x50) == 0) goto LAB_1814cb764;
            FUN_180002460(*(longlong *)(param_2 + 0x50),lVar6,DAT_187b7fc80);
          }
        }
        return;
      }
    }
  }
LAB_1814cb764:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
LAB_1814cb417:
  if (plVar14 != (longlong *)0x0) {
    lVar6 = *(longlong *)(param_2 + 0x50);
    if (lVar6 == 0) goto LAB_1814cb764;
LAB_1814cb551:
    FUN_180002460(lVar6,plVar14,DAT_187b7fc80);
  }
LAB_1814cb56a:
  local_160 = local_160 + 1;
  lVar6 = local_168;
  lVar19 = local_148;
  if (iVar16 <= local_160) goto LAB_1814cb583;
  goto LAB_1814cb1b0;
}



/* ==== UnitActionController::setCutInSkipTimeForPrincessForm  @ 0x14cb7d0  size=106 ==== */

void FUN_1814cb7d0(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626a4 == '\0') {
    FUN_18032f6d0(&DAT_187c421b8);
    DAT_18ab626a4 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xb0) != 0) {
    lVar1 = FUN_1855bdd60(*(longlong *)(param_1 + 0xb0),param_2,DAT_187c421b8);
    if (lVar1 != 0) {
      if (*(char *)(lVar1 + 0x10) != '\0') {
        *(undefined4 *)(lVar1 + 0xd0) = 0;
      }
      return;
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::setSkillParameter  @ 0x14cb840  size=3572 ==== */

/* WARNING: Removing unreachable block (ram,0x0001814cc3e3) */

void FUN_1814cb840(longlong param_1,longlong *param_2,longlong param_3,int param_4)

{
  longlong lVar1;
  undefined8 uVar2;
  undefined8 uVar3;
  char cVar4;
  undefined2 uVar5;
  int iVar6;
  undefined4 uVar7;
  longlong lVar8;
  undefined8 uVar9;
  longlong lVar10;
  undefined8 *puVar11;
  longlong *plVar12;
  longlong lVar13;
  undefined8 uVar14;
  ushort uVar15;
  float fVar16;
  double dVar17;
  undefined4 local_d8 [4];
  longlong local_c8;
  undefined4 local_c0;
  undefined4 local_b8;
  undefined2 local_b4;
  longlong *local_b0;
  undefined8 local_a8;
  undefined8 *puStack_a0;
  longlong local_98;
  undefined8 uStack_90;
  longlong local_88;
  undefined8 local_80;
  undefined8 *puStack_78;
  longlong local_70;
  undefined8 local_68;
  
  local_b0 = param_2;
  if (DAT_18ab62698 == '\0') {
    FUN_18032f6d0(&DAT_187c32f48);
    FUN_18032f6d0(&DAT_187b42130);
    FUN_18032f6d0(&DAT_187b44ea0);
    FUN_18032f6d0(&DAT_187b41390);
    FUN_18032f6d0(&DAT_187c1cf68);
    FUN_18032f6d0(&DAT_187ba82c8);
    FUN_18032f6d0(&DAT_187b4a7c8);
    FUN_18032f6d0(&DAT_187b4a978);
    FUN_18032f6d0(&DAT_187b4ab30);
    FUN_18032f6d0(&DAT_187c38198);
    FUN_18032f6d0(&DAT_187bcb0c0);
    FUN_18032f6d0(&DAT_187b64318);
    FUN_18032f6d0(&DAT_187b8ae18);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187b861a8);
    FUN_18032f6d0(&DAT_187c19590);
    FUN_18032f6d0(&DAT_187ba38a0);
    FUN_18032f6d0(&DAT_187c30cd0);
    FUN_18032f6d0(&DAT_187c31950);
    FUN_18032f6d0(&DAT_187c30e80);
    FUN_18032f6d0(&DAT_187c31b08);
    FUN_18032f6d0(&DAT_187ba3a58);
    FUN_18032f6d0(&DAT_187be2298);
    FUN_18032f6d0(&DAT_187b3c708);
    FUN_18032f6d0(&DAT_187b5e978);
    FUN_18032f6d0(&DAT_187b60c28);
    FUN_18032f6d0(&DAT_187b60fa0);
    FUN_18032f6d0(&DAT_187b61d80);
    FUN_18032f6d0(&DAT_187b77e20);
    FUN_18032f6d0(&DAT_187b778e8);
    FUN_18032f6d0(&DAT_187bd2a58);
    FUN_18032f6d0(&DAT_187bd2c10);
    FUN_18032f6d0(&DAT_187c20b90);
    DAT_18ab62698 = '\x01';
  }
  local_d8[0] = 0;
  local_b8 = 0;
  local_b4 = 0;
  local_80 = 0;
  puStack_78 = (undefined8 *)0x0;
  local_70 = 0;
  lVar8 = *(longlong *)(DAT_187b3c708 + 0x20);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = FUN_18032d010(lVar8);
  }
  lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = FUN_18032d010(lVar8);
  }
  lVar8 = **(longlong **)(lVar8 + 0xb8);
  local_88 = lVar8;
  uVar9 = thunk_FUN_180383da0(DAT_187b64318);
  FUN_1804383d0(uVar9,0);
  local_68 = thunk_FUN_180383da0(DAT_187ba82c8);
  FUN_1855de480(local_68,uVar9,DAT_187b44ea0);
  lVar10 = thunk_FUN_180383da0(DAT_187be2298);
  FUN_1849de5b0(lVar10,DAT_187c19590);
  if (param_2 != (longlong *)0x0) {
    param_2[4] = lVar10;
    thunk_FUN_1803b4a20(param_2 + 4,lVar10);
    if (param_3 != 0) {
      iVar6 = FUN_182b137b0(param_3,0);
      if (*(longlong *)(param_1 + 0x90) != 0) {
        if (iVar6 == *(int *)(*(longlong *)(param_1 + 0x90) + 0x4b8)) {
          local_d8[0] = 1;
          if (*(int *)(DAT_187b77e20 + 0xe4) == 0) {
            il2cpp_runtime_class_init(DAT_187b77e20);
          }
          lVar13 = *(longlong *)(*(longlong *)(DAT_187b77e20 + 0xb8) + 0x38);
          lVar10 = *(longlong *)(param_3 + 0x2a0);
          uVar7 = *(undefined4 *)(param_3 + 0x2a8);
          if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          local_c8 = lVar10;
          local_c0 = uVar7;
          uVar7 = FUN_183aa36e0(&local_c8,0);
          if (lVar13 == 0) goto LAB_1814cc67a;
          cVar4 = FUN_1855a6fd0(lVar13,uVar7,local_d8,DAT_187b42130);
          if (cVar4 == '\0') {
            local_d8[0] = 1;
          }
          if ((*(longlong *)(param_1 + 0x90) == 0) ||
             (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 200), lVar10 == 0))
          goto LAB_1814cc67a;
          FUN_1804bd5e0(lVar10,local_d8[0],0);
        }
        if (param_4 == -1) {
          param_4 = FUN_182b137b0(param_3,0);
        }
        *(int *)(param_2 + 0xf) = param_4;
        lVar10 = *(longlong *)(param_3 + 0x68);
        uVar7 = *(undefined4 *)(param_3 + 0x70);
        if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
          il2cpp_runtime_class_init();
        }
        local_c8 = lVar10;
        local_c0 = uVar7;
        iVar6 = FUN_183aa36e0(&local_c8,0);
        if (iVar6 == 0) {
          if ((*(longlong *)(param_1 + 0x90) == 0) ||
             (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0xb8), lVar10 == 0))
          goto LAB_1814cc67a;
          fVar16 = *(float *)(lVar10 + 0x18);
        }
        else {
          lVar10 = *(longlong *)(param_3 + 0x68);
          uVar7 = *(undefined4 *)(param_3 + 0x70);
          if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          local_c8 = lVar10;
          local_c0 = uVar7;
          iVar6 = FUN_183aa36e0(&local_c8,0);
          fVar16 = (float)iVar6;
        }
        *(float *)((longlong)param_2 + 0x7c) = fVar16;
        if (lVar8 != 0) {
          local_c8 = *(longlong *)(lVar8 + 0x1cb0);
          iVar6 = 0;
          while (lVar8 = *(longlong *)(param_3 + 0x28), lVar8 != 0) {
            if (*(int *)(lVar8 + 0x18) <= iVar6) {
              uVar9 = *(undefined8 *)(param_3 + 0x78);
              uVar14 = *(undefined8 *)(param_3 + 0x80);
              uVar2 = *(undefined8 *)(param_3 + 0x88);
              uVar3 = *(undefined8 *)(param_3 + 0x90);
              if (*(int *)(DAT_187b60c28 + 0xe4) == 0) {
                il2cpp_runtime_class_init();
              }
              local_a8 = uVar9;
              puStack_a0 = (undefined8 *)uVar14;
              local_98 = uVar2;
              uStack_90 = uVar3;
              dVar17 = (double)FUN_183a98560(&local_a8,0);
              *(float *)(param_2 + 0xe) = (float)dVar17;
              local_a8 = *(undefined8 *)(param_3 + 0x98);
              puStack_a0 = *(undefined8 **)(param_3 + 0xa0);
              local_98 = *(longlong *)(param_3 + 0xa8);
              uStack_90 = *(undefined8 *)(param_3 + 0xb0);
              dVar17 = (double)FUN_183a98560(&local_a8,0);
              *(float *)((longlong)param_2 + 0x74) = (float)dVar17;
              lVar8 = FUN_182b11f60(param_3,0);
              param_2[0x1c] = lVar8;
              thunk_FUN_1803b4a20(param_2 + 0x1c,lVar8);
              switch((int)param_2[0x1f]) {
              case 0:
                if ((*(longlong *)(param_1 + 0x90) == 0) ||
                   (lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x508), lVar8 == 0))
                goto LAB_1814cc67a;
                uVar7 = FUN_1855ac210(lVar8,(int)param_2[0xf],DAT_187b41390);
                *(undefined4 *)(param_2 + 0x10) = uVar7;
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
              FUN_1814bce50(param_1,param_2,0);
              if (DAT_18ab6269d == '\0') {
                FUN_18032f6d0(&DAT_187c19c60);
                FUN_18032f6d0(&DAT_187c19e18);
                DAT_18ab6269d = '\x01';
              }
              iVar6 = 0;
              goto LAB_1814cc020;
            }
            lVar8 = FUN_1849ed2d0(lVar8,iVar6,DAT_187ba3a58);
            if (lVar8 == 0) break;
            local_b8 = *(undefined4 *)(lVar8 + 0x2c);
            local_b4 = *(undefined2 *)(lVar8 + 0x30);
            if (*(int *)(DAT_187b61d80 + 0xe4) == 0) {
              il2cpp_runtime_class_init();
            }
            uVar5 = FUN_183ae7dd0(&local_b8,0);
            if (DAT_18ab626da == '\0') {
              FUN_18032f6d0(&DAT_187bfaba0);
              DAT_18ab626da = '\x01';
            }
            plVar12 = *(longlong **)(*(longlong *)(DAT_187bfaba0 + 0xb8) + 8);
            if (plVar12 == (longlong *)0x0) break;
            lVar10 = *plVar12;
            uVar15 = 0;
            if (*(ushort *)(lVar10 + 0x12e) != 0) {
              do {
                if (*(longlong *)(*(longlong *)(lVar10 + 0xb0) + (ulonglong)uVar15 * 0x10) ==
                    DAT_187b93630) {
                  puVar11 = (undefined8 *)
                            ((longlong)
                             (*(int *)(*(longlong *)(lVar10 + 0xb0) + 8 + (ulonglong)uVar15 * 0x10)
                             + 0x1b) * 0x10 + 0x138 + lVar10);
                  goto LAB_1814cbdc9;
                }
                uVar15 = uVar15 + 1;
              } while (uVar15 < *(ushort *)(lVar10 + 0x12e));
            }
            puVar11 = (undefined8 *)FUN_18032c930(plVar12,DAT_187b93630,0x1b);
LAB_1814cbdc9:
            cVar4 = (*(code *)*puVar11)(plVar12,puVar11[1]);
            if (cVar4 != '\0') {
              if (*(longlong *)(param_1 + 0x90) == 0) break;
              if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x178) == '\0') {
                uVar9 = *(undefined8 *)(lVar8 + 0x14);
                uVar7 = *(undefined4 *)(lVar8 + 0x1c);
                if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
                  il2cpp_runtime_class_init();
                }
                puStack_a0 = (undefined8 *)CONCAT44(puStack_a0._4_4_,uVar7);
                local_a8 = uVar9;
                uVar7 = FUN_183aa36e0(&local_a8,0);
                if (local_c8 == 0) break;
                lVar10 = FUN_1826a75b0(local_c8,uVar7,0);
                if (lVar10 != 0) {
                  lVar8 = FUN_1826bc100(lVar10,0);
                }
              }
            }
            FUN_1814c4d60(param_1,param_3,lVar8,uVar5,param_2,local_68,0);
            iVar6 = iVar6 + 1;
          }
        }
      }
    }
  }
  goto LAB_1814cc67a;
LAB_1814cc020:
  lVar8 = param_2[4];
  if (lVar8 == 0) goto LAB_1814cc67a;
  if (iVar6 < *(int *)(lVar8 + 0x18)) {
    plVar12 = (longlong *)FUN_1849ed2d0(lVar8,iVar6,DAT_187c19e18);
    if (plVar12 == (longlong *)0x0) goto LAB_1814cc67a;
    (**(code **)(*plVar12 + 0x248))
              (plVar12,*(undefined8 *)(param_1 + 0x90),*(undefined8 *)(*plVar12 + 0x250));
    iVar6 = iVar6 + 1;
    goto LAB_1814cc020;
  }
  if (param_2[6] == 0) goto LAB_1814cc67a;
  FUN_1849e2810(&local_a8,param_2[6],DAT_187b861a8);
  local_80 = local_a8;
  puStack_78 = puStack_a0;
  local_70 = local_98;
  local_a8 = 0;
  puStack_a0 = &local_80;
  while (cVar4 = FUN_18572bc70(&local_80,DAT_187b4a978), lVar8 = local_70, cVar4 != '\0') {
    if (local_70 == 0) {
                    /* WARNING: Subroutine does not return */
      FUN_18032f9d0();
    }
    uVar9 = *(undefined8 *)(local_70 + 0x18);
    if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
      il2cpp_runtime_class_init();
    }
    cVar4 = FUN_18620c870(uVar9,0);
    if (cVar4 == '\0') {
      if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032f9d0();
      }
      uVar9 = *(undefined8 *)(lVar8 + 0x10);
      if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
        il2cpp_runtime_class_init();
      }
      cVar4 = FUN_18620c870(uVar9,0);
      if (cVar4 == '\0') {
        if (*(longlong *)(lVar8 + 0x18) == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        uVar9 = FUN_1847c6e60(*(longlong *)(lVar8 + 0x18),DAT_187bcb0c0);
        if (*(int *)(DAT_187b5e978 + 0xe4) == 0) {
          il2cpp_runtime_class_init();
        }
        cVar4 = FUN_18620c970(uVar9,0);
        if (cVar4 != '\0') {
          lVar10 = thunk_FUN_180383da0(DAT_187c20b90);
          FUN_1804383d0(lVar10,0);
          lVar13 = FUN_1814a9040(param_1,0);
          if (lVar13 == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          uVar9 = FUN_1800121c0(0,DAT_187b8ae18,lVar13,*(undefined8 *)(lVar8 + 0x18),0);
          if (lVar10 == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          *(undefined8 *)(lVar10 + 0x10) = uVar9;
          thunk_FUN_1803b4a20(lVar10 + 0x10,uVar9);
          if (*(longlong *)(lVar10 + 0x10) == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          *(undefined1 *)(*(longlong *)(lVar10 + 0x10) + 0xb4) = 1;
          if (*(longlong *)(lVar10 + 0x10) == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          *(undefined1 *)(*(longlong *)(lVar10 + 0x10) + 0xa0) = 0;
          if (*(longlong *)(lVar10 + 0x10) == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          lVar13 = FUN_186202760(*(longlong *)(lVar10 + 0x10),0);
          uVar9 = FUN_1838e90b0(0);
          if (lVar13 == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          FUN_186218890(lVar13,uVar9,0);
          uVar9 = thunk_FUN_180383da0(DAT_187c32f48);
          FUN_18044b290(uVar9,lVar10,DAT_187bd2a58,0);
          if (*(int *)(DAT_187c38198 + 0xe4) == 0) {
            il2cpp_runtime_class_init();
          }
          FUN_183d9c7b0(param_1,uVar9,0);
          lVar13 = FUN_1814a9040(param_1,0);
          if (lVar13 == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          uVar9 = FUN_1800121c0(0,DAT_187b8ae18,lVar13,*(undefined8 *)(lVar8 + 0x10),0);
          *(undefined8 *)(lVar10 + 0x18) = uVar9;
          thunk_FUN_1803b4a20(lVar10 + 0x18,uVar9);
          if (*(longlong *)(lVar10 + 0x18) == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          *(undefined1 *)(*(longlong *)(lVar10 + 0x18) + 0xb4) = 1;
          if (*(longlong *)(lVar10 + 0x18) == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          *(undefined1 *)(*(longlong *)(lVar10 + 0x18) + 0xa0) = 0;
          if (*(longlong *)(lVar10 + 0x18) == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          lVar8 = FUN_186202760(*(longlong *)(lVar10 + 0x18),0);
          uVar9 = FUN_1838e90b0(0);
          if (lVar8 == 0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          FUN_186218890(lVar8,uVar9,0);
          uVar9 = thunk_FUN_180383da0(DAT_187c32f48);
          FUN_18044b290(uVar9,lVar10,DAT_187bd2c10,0);
          FUN_183d9c7b0(param_1,uVar9);
        }
      }
    }
  }
  FUN_180443e50(&local_80,DAT_187b4a7c8);
  lVar10 = local_88;
  lVar8 = *(longlong *)(local_88 + 0x418);
  uVar7 = FUN_182b137b0(param_3,0);
  if (lVar8 != 0) {
    lVar13 = FUN_1833947a0(lVar8,uVar7,0);
    lVar8 = *(longlong *)(lVar10 + 0x3d8);
    if ((*(byte *)(*param_2 + 0x130) < *(byte *)(DAT_187b778e8 + 0x130)) ||
       (*(longlong *)
         (*(longlong *)(*param_2 + 200) + -8 + (ulonglong)*(byte *)(DAT_187b778e8 + 0x130) * 8) !=
        DAT_187b778e8)) {
      return;
    }
    if (*(longlong *)
         (*(longlong *)(*local_b0 + 200) + -8 + (ulonglong)*(byte *)(DAT_187b778e8 + 0x130) * 8) !=
        DAT_187b778e8) goto LAB_1814cc67a;
    iVar6 = 0;
    plVar12 = local_b0;
    while (lVar10 = plVar12[0x34], lVar10 != 0) {
      if (*(int *)(lVar10 + 0x18) <= iVar6) {
        return;
      }
      if (lVar13 == 0) break;
      if (iVar6 < *(int *)(lVar13 + 0x18)) {
        lVar10 = FUN_1849ed2d0(lVar10,iVar6,DAT_187c30e80);
        if (lVar10 == 0) break;
        *(undefined4 *)(lVar10 + 0x94) = *(undefined4 *)((longlong)param_2 + 0x94);
        if (plVar12[0x34] == 0) break;
        uVar9 = FUN_1849ed2d0(plVar12[0x34],iVar6,DAT_187c30e80);
        lVar10 = FUN_1849ed2d0(lVar13,iVar6,DAT_187c31b08);
        if (lVar10 == 0) break;
        uVar14 = *(undefined8 *)(lVar10 + 0x28);
        uVar7 = *(undefined4 *)(lVar10 + 0x30);
        if (*(int *)(DAT_187b60fa0 + 0xe4) == 0) {
          il2cpp_runtime_class_init();
        }
        puStack_a0 = (undefined8 *)CONCAT44(puStack_a0._4_4_,uVar7);
        local_a8 = uVar14;
        uVar7 = FUN_183aa36e0(&local_a8,0);
        if (lVar8 == 0) break;
        uVar14 = FUN_182b00540(lVar8,uVar7,0);
        uVar7 = FUN_182b137b0(param_3,0);
        FUN_1814cb840(param_1,uVar9,uVar14,uVar7,0);
        if (local_b0[0x34] == 0) break;
        lVar10 = FUN_1849ed2d0(local_b0[0x34],iVar6,DAT_187c30e80);
        if (*(longlong *)(param_1 + 0x90) == 0) break;
        lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x500);
        uVar7 = FUN_182b137b0(param_3,0);
        if ((lVar1 == 0) || (uVar7 = FUN_1855ac210(lVar1,uVar7,DAT_187c1cf68), lVar10 == 0)) break;
        FUN_1814b5f20(lVar10,uVar7,0);
        plVar12 = local_b0;
      }
      iVar6 = iVar6 + 1;
    }
  }
LAB_1814cc67a:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::set_ContinuousActionEndDone  @ 0x5e88b0  size=7 ==== */

void FUN_1805e88b0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xb8) = param_2;
  return;
}



/* ==== UnitActionController::set_DisableUBByModeChange  @ 0x14cc6a0  size=7 ==== */

void FUN_1814cc6a0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x9a) = param_2;
  return;
}



/* ==== UnitActionController::set_IsCancelModeChangeByTrigger  @ 0x1021e00  size=7 ==== */

void FUN_181021e00(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xa8) = param_2;
  return;
}



/* ==== UnitActionController::set_ModeChanging  @ 0x14cc6b0  size=7 ==== */

void FUN_1814cc6b0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x9b) = param_2;
  return;
}



/* ==== UnitActionController::set_MoveEnd  @ 0x14cc6c0  size=7 ==== */

void FUN_1814cc6c0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xa9) = param_2;
  return;
}



/* ==== UnitActionController::set_OnNormalAttackStart  @ 0x438e80  size=19 ==== */

void FUN_180438e80(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0xa0) = param_2;
  thunk_FUN_1803b4a20(param_1 + 0xa0);
  return;
}



/* ==== UnitActionController::set_Owner  @ 0x438c80  size=19 ==== */

void FUN_180438c80(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0x90) = param_2;
  thunk_FUN_1803b4a20(param_1 + 0x90);
  return;
}



/* ==== UnitActionController::set_StopModeChangeEndEffectCalled  @ 0x14cc6d0  size=7 ==== */

void FUN_1814cc6d0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x9c) = param_2;
  return;
}



/* ==== UnitActionController::set_UnionBurstCharging  @ 0xf410f0  size=7 ==== */

void FUN_180f410f0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x99) = param_2;
  return;
}



/* ==== UnitActionController::set_UnionBurstIsChargeTime  @ 0xf410e0  size=7 ==== */

void FUN_180f410e0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x98) = param_2;
  return;
}



/* ==== UnitActionController::set_isUnionBurstOnlyOwner  @ 0x14cc6e0  size=7 ==== */

void FUN_1814cc6e0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xb9) = param_2;
  return;
}



/* ==== UnitActionController::set_skillDictionary  @ 0x44ad90  size=19 ==== */

void FUN_18044ad90(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0xb0) = param_2;
  thunk_FUN_1803b4a20(param_1 + 0xb0);
  return;
}



/* ==== UnitActionController::set_updateBranchMotionRunning  @ 0x14cc6f0  size=7 ==== */

void FUN_1814cc6f0(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xba) = param_2;
  return;
}



/* ==== UnitActionController::sortTargetListByTargetPattern  @ 0x14cc700  size=5199 ==== */

void FUN_1814cc700(longlong param_1,longlong param_2,longlong param_3)

{
  longlong lVar1;
  longlong *plVar2;
  char cVar3;
  undefined4 uVar4;
  undefined8 uVar5;
  longlong lVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 uVar9;
  longlong lVar10;
  undefined8 uVar11;
  undefined4 *puVar12;
  int iVar13;
  int iVar14;
  bool bVar15;
  uint uVar16;
  longlong local_d8 [2];
  undefined8 local_c8;
  undefined8 *local_c0;
  undefined8 local_b8;
  undefined8 *puStack_b0;
  longlong local_a8;
  undefined8 local_a0;
  undefined8 *puStack_98;
  longlong local_90;
  undefined8 local_88;
  undefined8 uStack_80;
  longlong *local_78;
  longlong local_70;
  longlong local_68;
  undefined4 local_50;
  undefined4 uStack_4c;
  undefined4 uStack_48;
  undefined4 uStack_44;
  longlong *local_40;
  
  if (DAT_18ab626ba == '\0') {
    FUN_18032f6d0(&DAT_187b41650);
    FUN_18032f6d0(&DAT_187b67a30);
    FUN_18032f6d0(&DAT_187b67da8);
    FUN_18032f6d0(&DAT_187b67f60);
    FUN_18032f6d0(&DAT_187b67be8);
    FUN_18032f6d0(&DAT_187b68120);
    FUN_18032f6d0(&DAT_187b682d8);
    FUN_18032f6d0(&DAT_187b68650);
    FUN_18032f6d0(&DAT_187b68810);
    FUN_18032f6d0(&DAT_187b68498);
    FUN_18032f6d0(&DAT_187b689c8);
    FUN_18032f6d0(&DAT_187b68b88);
    FUN_18032f6d0(&DAT_187b68d40);
    FUN_18032f6d0(&DAT_187b68f00);
    FUN_18032f6d0(&DAT_187b690b8);
    FUN_18032f6d0(&DAT_187b69278);
    FUN_18032f6d0(&DAT_187b69430);
    FUN_18032f6d0(&DAT_187b695f0);
    FUN_18032f6d0(&DAT_187b697a8);
    FUN_18032f6d0(&DAT_187b69968);
    FUN_18032f6d0(&DAT_187b69b20);
    FUN_18032f6d0(&DAT_187b69ce0);
    FUN_18032f6d0(&DAT_187b69e98);
    FUN_18032f6d0(&DAT_187b6a210);
    FUN_18032f6d0(&DAT_187b6a3d0);
    FUN_18032f6d0(&DAT_187b6a058);
    FUN_18032f6d0(&DAT_187b6a588);
    FUN_18032f6d0(&DAT_187b67870);
    FUN_18032f6d0(&DAT_187b807d8);
    FUN_18032f6d0(&DAT_187b4f980);
    FUN_18032f6d0(&DAT_187b7f620);
    FUN_18032f6d0(&DAT_187b4fb30);
    FUN_18032f6d0(&DAT_187b7f7e0);
    FUN_18032f6d0(&DAT_187b7f998);
    FUN_18032f6d0(&DAT_187b4fce8);
    FUN_18032f6d0(&DAT_187b43738);
    FUN_18032f6d0(&DAT_187ba1bc0);
    FUN_18032f6d0(&DAT_187ba1d70);
    FUN_18032f6d0(&DAT_187ba1f28);
    FUN_18032f6d0(&DAT_187ba20d8);
    FUN_18032f6d0(&DAT_187ba2290);
    FUN_18032f6d0(&DAT_187ba2440);
    FUN_18032f6d0(&DAT_187ba25f8);
    FUN_18032f6d0(&DAT_187ba27a8);
    FUN_18032f6d0(&DAT_187ba2960);
    FUN_18032f6d0(&DAT_187ba2b10);
    FUN_18032f6d0(&DAT_187ba2cc8);
    FUN_18032f6d0(&DAT_187ba2e78);
    FUN_18032f6d0(&DAT_187ba3030);
    FUN_18032f6d0(&DAT_187ba31e0);
    FUN_18032f6d0(&DAT_187ba3398);
    FUN_18032f6d0(&DAT_187b93630);
    FUN_18032f6d0(&DAT_187b7fe40);
    FUN_18032f6d0(&DAT_187b7fc80);
    FUN_18032f6d0(&DAT_187b7fff8);
    FUN_18032f6d0(&DAT_187b9e928);
    FUN_18032f6d0(&DAT_187b72a78);
    FUN_18032f6d0(&DAT_187b80f68);
    FUN_18032f6d0(&DAT_187b7f908);
    FUN_18032f6d0(&DAT_187b81118);
    FUN_18032f6d0(&DAT_187b812d0);
    FUN_18032f6d0(&DAT_187b81480);
    FUN_18032f6d0(&DAT_187bc5830);
    DAT_18ab626ba = '\x01';
  }
  local_a0 = 0;
  puStack_98 = (undefined8 *)0x0;
  local_90 = 0;
  local_88 = 0;
  uStack_80 = 0;
  local_78 = (longlong *)0x0;
  if (param_2 == 0) goto LAB_1814cdb8f;
  lVar1 = *(longlong *)(param_2 + 0x50);
  iVar13 = *(int *)(param_2 + 0x14);
  local_d8[0] = lVar1;
  local_70 = lVar1;
  local_68 = lVar1;
  if ((999 < iVar13) && (iVar13 = (iVar13 / 1000) * 1000, 12000 < iVar13)) {
    if ((iVar13 == 13000) || ((iVar13 == 14000 || (iVar13 == 15000))))
    goto switchD_1814ccc25_caseD_3;
    goto switchD_1814ccc25_caseD_12;
  }
  switch(iVar13) {
  case 2:
  case 8:
  case 0x16:
  case 0x17:
    if (lVar1 == 0) goto LAB_1814cdb8f;
    iVar13 = *(int *)(lVar1 + 0x18);
    while (1 < iVar13) {
      uVar4 = FUN_1837167c0(0,iVar13,0);
      iVar13 = iVar13 + -1;
      uVar9 = FUN_1849ed2d0(lVar1,uVar4,DAT_187b812d0);
      uVar11 = FUN_1849ed2d0(lVar1,iVar13,DAT_187b812d0);
      FUN_184aa4620(lVar1,uVar4,uVar11,DAT_187b81480);
      FUN_184aa4620(lVar1,iVar13,uVar9,DAT_187b81480);
    }
    goto LAB_1814ccb2b;
  case 3:
  case 0x22:
switchD_1814ccc25_caseD_3:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    goto switchD_1814ccc25_caseD_12;
  case 4:
switchD_1814ccc25_caseD_4:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba1d70;
    break;
  case 5:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b690b8;
    goto LAB_1814ccc3a;
  case 6:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b69278;
    goto LAB_1814ccc3a;
  case 7:
    if (lVar1 == 0) goto LAB_1814cdb8f;
    *(int *)(lVar1 + 0x1c) = *(int *)(lVar1 + 0x1c) + 1;
    iVar13 = *(int *)(lVar1 + 0x18);
    *(undefined4 *)(lVar1 + 0x18) = 0;
    if (0 < iVar13) {
      FUN_185be98b0(*(undefined8 *)(lVar1 + 0x10),0,iVar13,0);
    }
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x80);
    if (*(int *)(DAT_187b41650 + 0xe4) == 0) {
      il2cpp_runtime_class_init(DAT_187b41650);
    }
    if (lVar10 == 0) goto LAB_1814cdb8f;
    uVar16 = *(uint *)(*(longlong *)(DAT_187b41650 + 0xb8) + 0x38) ^ 0x80000000;
    uVar9 = 0;
    goto LAB_1814cccfb;
  case 9:
  case 10:
    iVar13 = *(int *)(param_2 + 0x14);
    uVar9 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar9,0,DAT_187b68f00,0);
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b6a588,0);
    FUN_1814cdc90(param_1,lVar1,iVar13 == 9,uVar9,uVar11,0);
    goto LAB_1814ccb2b;
  case 0xb:
    if (lVar1 == 0) goto LAB_1814cdb8f;
    *(int *)(lVar1 + 0x1c) = *(int *)(lVar1 + 0x1c) + 1;
    iVar13 = *(int *)(lVar1 + 0x18);
    *(undefined4 *)(lVar1 + 0x18) = 0;
    if (0 < iVar13) {
      FUN_185be98b0(*(undefined8 *)(lVar1 + 0x10),0,iVar13,0);
    }
    if ((*(longlong *)(param_1 + 0x90) == 0) ||
       (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x90) + 0x80), lVar10 == 0))
    goto LAB_1814cdb8f;
    uVar16 = 0;
    uVar9 = 1;
LAB_1814cccfb:
    uVar9 = FUN_183991830(lVar10,uVar9,uVar16,0);
    FUN_180002460(lVar1,uVar9,DAT_187b7fc80);
    goto LAB_1814ccb2b;
  case 0xc:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b689c8;
    goto LAB_1814ccc3a;
  case 0xd:
  case 0x13:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b68498;
    goto LAB_1814ccc3a;
  case 0xe:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b67be8;
    goto LAB_1814ccc3a;
  case 0xf:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b67a30;
    goto LAB_1814ccc3a;
  case 0x10:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b6a058;
    goto LAB_1814ccc3a;
  case 0x11:
    uVar11 = thunk_FUN_180383da0(DAT_187b43738);
    uVar9 = DAT_187b69e98;
LAB_1814ccc3a:
    FUN_1854e7780(uVar11,0,uVar9,0);
    FUN_1847535e0(lVar1,uVar11,DAT_187b67870);
    goto LAB_1814ccb2b;
  case 0x12:
  case 0x14:
  case 0x15:
  case 0x2a:
  case 0x30:
  case 0x31:
    goto switchD_1814ccc25_caseD_12;
  case 0x18:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba2cc8;
    break;
  case 0x19:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba3030;
    break;
  case 0x1a:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba31e0;
    break;
  case 0x1b:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba27a8;
    break;
  case 0x1c:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba2960;
    break;
  case 0x1d:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba2e78;
    break;
  case 0x1e:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba3398;
    break;
  case 0x1f:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba1bc0;
    break;
  case 0x20:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba2440;
    break;
  case 0x21:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba2b10;
    break;
  case 0x23:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b697a8,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b69968;
    goto LAB_1814cd2ae;
  case 0x24:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b69430,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b695f0;
LAB_1814cd2ae:
    FUN_1854e7780(uVar5,0,uVar9,0);
    bVar15 = true;
    goto LAB_1814cd2c7;
  case 0x25:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba2290;
    break;
  case 0x26:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba1f28;
    break;
  case 0x27:
    lVar10 = *(longlong *)(param_1 + 0x90);
    if (param_3 == 0) goto LAB_1814cdb8f;
    puVar12 = (undefined4 *)FUN_186217ec0(local_d8,param_3,0);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    *(undefined4 *)(lVar10 + 0x260) = *puVar12;
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba20d8;
    break;
  case 0x28:
    if (lVar1 == 0) goto LAB_1814cdb8f;
    *(int *)(lVar1 + 0x1c) = *(int *)(lVar1 + 0x1c) + 1;
    iVar13 = *(int *)(lVar1 + 0x18);
    *(undefined4 *)(lVar1 + 0x18) = 0;
    if (0 < iVar13) {
      FUN_185be98b0(*(undefined8 *)(lVar1 + 0x10),0,iVar13,0);
    }
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    if (*(char *)(*(longlong *)(param_1 + 0x90) + 0x178) == '\0') {
      lVar10 = FUN_1814a9080(param_1,0);
      if (lVar10 == 0) goto LAB_1814cdb8f;
      uVar9 = 6;
    }
    else {
      lVar10 = FUN_1814a9080(param_1,0);
      if (lVar10 == 0) goto LAB_1814cdb8f;
      uVar9 = 5;
    }
    lVar10 = FUN_180002270(uVar9,DAT_187b93630,lVar10);
    if (lVar10 == 0) goto LAB_1814cdb8f;
    FUN_1849e2810(&local_b8,lVar10,DAT_187b72a78);
    local_a0 = local_b8;
    puStack_98 = puStack_b0;
    local_90 = local_a8;
    local_c8 = 0;
    local_c0 = &local_a0;
    while (cVar3 = FUN_18572bc70(&local_a0,DAT_187b7f7e0), lVar10 = local_90, cVar3 != '\0') {
      if (local_90 == 0) {
                    /* WARNING: Subroutine does not return */
        FUN_18032f9d0();
      }
      cVar3 = FUN_181605dc0(local_90,0);
      if (cVar3 == '\0') {
        if (lVar10 == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        if (*(longlong *)(lVar10 + 0x80) == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        uVar9 = FUN_183991830(*(longlong *)(lVar10 + 0x80),0,0,0);
        FUN_180002460(lVar1,uVar9,DAT_187b7fc80);
      }
      else {
        if (lVar10 == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        if (*(longlong *)(lVar10 + 0x80) == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        lVar10 = *(longlong *)(*(longlong *)(lVar10 + 0x80) + 0x18);
        if (lVar10 == 0) {
                    /* WARNING: Subroutine does not return */
          FUN_18032f9d0();
        }
        FUN_1849e2810(&local_50,lVar10,DAT_187b9e928);
        local_88 = CONCAT44(uStack_4c,local_50);
        uStack_80 = CONCAT44(uStack_44,uStack_48);
        local_78 = local_40;
        local_b8 = 0;
        puStack_b0 = &local_88;
        while (cVar3 = FUN_18572bc70(&local_88,DAT_187b4fb30), plVar2 = local_78, cVar3 != '\0') {
          if (local_78 == (longlong *)0x0) {
                    /* WARNING: Subroutine does not return */
            FUN_18032f9d0();
          }
          cVar3 = (**(code **)(*local_78 + 0x398))(local_78,*(undefined8 *)(*local_78 + 0x3a0));
          if (cVar3 != '\0') {
            FUN_180002460(lVar1,plVar2,DAT_187b7fc80);
          }
        }
        FUN_180443e50(&local_88,DAT_187b4f980);
      }
    }
    FUN_180443e50(local_c0,DAT_187b7f620);
    goto LAB_1814ccb2b;
  case 0x29:
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b68650,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar5,0,DAT_187b68810,0);
    bVar15 = false;
    goto LAB_1814cd2c7;
  case 0x2b:
  case 0x33:
    iVar13 = *(int *)(param_2 + 0x10);
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b67da8,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b67f60;
    goto LAB_1814cd950;
  case 0x2c:
    iVar13 = *(int *)(param_2 + 0x10);
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b68b88,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b68d40;
    goto LAB_1814cd950;
  case 0x2d:
    iVar13 = *(int *)(param_2 + 0x10);
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b68120,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b682d8;
    goto LAB_1814cd950;
  case 0x2e:
    iVar13 = *(int *)(param_2 + 0x10);
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b69b20,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b69ce0;
    goto LAB_1814cd950;
  case 0x2f:
    iVar13 = *(int *)(param_2 + 0x10);
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b68f00,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b6a588;
    goto LAB_1814cd950;
  case 0x32:
    iVar13 = *(int *)(param_2 + 0x10);
    uVar11 = thunk_FUN_180383da0(DAT_187b807d8);
    FUN_1854e7780(uVar11,0,DAT_187b6a210,0);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar9 = DAT_187b6a3d0;
LAB_1814cd950:
    FUN_1854e7780(uVar5,0,uVar9,0);
    bVar15 = iVar13 == 1;
LAB_1814cd2c7:
    FUN_1814cdc90(param_1,lVar1,bVar15,uVar11,uVar5,0);
    goto LAB_1814ccb2b;
  default:
    if (iVar13 == 11000) goto switchD_1814ccc25_caseD_3;
    if (iVar13 == 12000) goto switchD_1814ccc25_caseD_4;
switchD_1814ccc25_caseD_12:
    if (*(longlong *)(param_1 + 0x90) == 0) goto LAB_1814cdb8f;
    uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x68);
    uVar5 = thunk_FUN_180383da0(DAT_187b807d8);
    uVar11 = DAT_187ba25f8;
  }
  FUN_1854e7780(uVar5,uVar9,uVar11,0);
  if (lVar1 == 0) goto LAB_1814cdb8f;
  FUN_184a9fb90(lVar1,uVar5,DAT_187b80f68);
LAB_1814ccb2b:
  iVar14 = 0;
  iVar13 = *(int *)(param_2 + 0x14);
  if (iVar13 == 0xc) {
LAB_1814cdaa0:
    lVar7 = thunk_FUN_180383da0(DAT_187bc5830);
    FUN_1849de5b0(lVar7,DAT_187b7f908);
    lVar6 = thunk_FUN_180383da0(DAT_187bc5830);
    FUN_1849de5b0(lVar6,DAT_187b7f908);
    lVar10 = local_68;
    if (lVar1 == 0) goto LAB_1814cdb8f;
    for (; iVar14 < *(int *)(lVar10 + 0x18); iVar14 = iVar14 + 1) {
      lVar8 = FUN_1849ed2d0(lVar1,iVar14,DAT_187b812d0);
      if (((lVar8 == 0) || (*(longlong *)(lVar8 + 0x68) == 0)) ||
         (lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0x68) + 0x88), lVar8 == 0))
      goto LAB_1814cdb8f;
      cVar3 = FUN_183dd31a0((int)*(undefined8 *)(lVar8 + 0x848),0,0);
      if (cVar3 == '\0') {
        uVar9 = FUN_1849ed2d0(lVar1,iVar14,DAT_187b812d0);
        lVar8 = lVar6;
      }
      else {
        uVar9 = FUN_1849ed2d0(lVar1,iVar14,DAT_187b812d0);
        lVar8 = lVar7;
      }
      if (lVar8 == 0) goto LAB_1814cdb8f;
      FUN_180002460(lVar8,uVar9,DAT_187b7fc80);
    }
  }
  else {
    if (iVar13 != 0x13) {
      if (iVar13 != 0x1b) {
        return;
      }
      goto LAB_1814cdaa0;
    }
    lVar6 = thunk_FUN_180383da0(DAT_187bc5830);
    FUN_1849de5b0(lVar6,DAT_187b7f908);
    lVar7 = thunk_FUN_180383da0(DAT_187bc5830);
    FUN_1849de5b0(lVar7,DAT_187b7f908);
    lVar10 = local_70;
    if (lVar1 == 0) goto LAB_1814cdb8f;
    for (; iVar14 < *(int *)(lVar10 + 0x18); iVar14 = iVar14 + 1) {
      lVar8 = FUN_1849ed2d0(lVar1,iVar14,DAT_187b812d0);
      if (((lVar8 == 0) || (*(longlong *)(lVar8 + 0x68) == 0)) ||
         (lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0x68) + 0xd8), lVar8 == 0))
      goto LAB_1814cdb8f;
      cVar3 = FUN_1839d6c50(lVar8,0);
      if (cVar3 == '\0') {
        uVar9 = FUN_1849ed2d0(lVar1,iVar14,DAT_187b812d0);
        lVar8 = lVar7;
      }
      else {
        uVar9 = FUN_1849ed2d0(lVar1,iVar14,DAT_187b812d0);
        lVar8 = lVar6;
      }
      if (lVar8 == 0) goto LAB_1814cdb8f;
      FUN_180002460(lVar8,uVar9,DAT_187b7fc80);
    }
  }
  if (lVar6 != 0) {
    FUN_184a97390(lVar6,lVar7,DAT_187b7fe40);
    *(longlong *)(param_2 + 0x50) = lVar6;
    thunk_FUN_1803b4a20(param_2 + 0x50,lVar6);
    return;
  }
LAB_1814cdb8f:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::sortTargetPosition  @ 0x14cdc90  size=167 ==== */

void FUN_1814cdc90(longlong param_1,longlong param_2,byte param_3,undefined8 param_4,
                  undefined8 param_5)

{
  longlong lVar1;
  char cVar2;
  
  if (DAT_18ab626bc == '\0') {
    FUN_18032f6d0(&DAT_187b80f68);
    DAT_18ab626bc = '\x01';
  }
  lVar1 = *(longlong *)(param_1 + 0x90);
  if (lVar1 != 0) {
    if (*(char *)(lVar1 + 0x178) != '\0') {
      param_3 = param_3 ^ 1;
    }
    if (*(longlong *)(lVar1 + 0x38) != 0) {
      cVar2 = FUN_1839540a0(*(longlong *)(lVar1 + 0x38),0);
      if (cVar2 != '\0') {
        param_3 = param_3 ^ 1;
      }
      if (param_2 != 0) {
        if (param_3 == 0) {
          param_4 = param_5;
        }
        FUN_184a9fb90(param_2,param_4,DAT_187b80f68);
        return;
      }
    }
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::startBlurEffects  @ 0x14cdd40  size=376 ==== */

void FUN_1814cdd40(longlong param_1,longlong param_2,undefined1 param_3)

{
  int iVar1;
  undefined8 uVar2;
  longlong lVar3;
  uint uVar4;
  ulonglong uVar5;
  ulonglong uVar6;
  
  if (DAT_18ab626bf == '\0') {
    FUN_18032f6d0(&DAT_187bd2190);
    FUN_18032f6d0(&DAT_187bd2348);
    DAT_18ab626bf = '\x01';
  }
  if ((param_2 != 0) && (*(longlong *)(param_2 + 0x40) != 0)) {
    iVar1 = *(int *)(*(longlong *)(param_2 + 0x40) + 0x18);
    uVar5 = 0;
    if (0 < iVar1) {
      do {
        if (*(longlong *)(param_2 + 0x40) == 0) goto LAB_1814cdeb3;
        uVar2 = FUN_1849ed2d0(*(longlong *)(param_2 + 0x40),uVar5,DAT_187bd2348);
        if (DAT_18ab626c0 == '\0') {
          FUN_18032f6d0(&DAT_187c21e50);
          DAT_18ab626c0 = '\x01';
        }
        lVar3 = thunk_FUN_180383da0(DAT_187c21e50);
        FUN_180445940(lVar3,0,0);
        if (lVar3 == 0) goto LAB_1814cdeb3;
        *(longlong *)(lVar3 + 0x38) = param_1;
        thunk_FUN_1803b4a20(lVar3 + 0x38,param_1);
        *(undefined8 *)(lVar3 + 0x28) = uVar2;
        thunk_FUN_1803b4a20(lVar3 + 0x28,uVar2);
        *(longlong *)(lVar3 + 0x30) = param_2;
        thunk_FUN_1803b4a20(lVar3 + 0x30,param_2);
        *(undefined1 *)(lVar3 + 0x20) = param_3;
        uVar6 = 0;
        if (0.0 < *(float *)(param_2 + 0xb0)) {
          uVar6 = *(ulonglong *)(param_1 + 0x90);
        }
        if (param_1 == 0) goto LAB_1814cdeb3;
        FUN_1814bbc70(param_1,lVar3,0,uVar6,0);
        uVar4 = (int)uVar5 + 1;
        uVar5 = (ulonglong)uVar4;
      } while ((int)uVar4 < iVar1);
    }
    return;
  }
LAB_1814cdeb3:
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::startBlurWithDelay  @ 0x14cdec0  size=187 ==== */

longlong FUN_1814cdec0(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined1 param_4)

{
  longlong lVar1;
  
  if (DAT_18ab626c0 == '\0') {
    FUN_18032f6d0(&DAT_187c21e50);
    DAT_18ab626c0 = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c21e50);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x38) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x38,param_1);
    *(undefined8 *)(lVar1 + 0x28) = param_2;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_2);
    *(undefined8 *)(lVar1 + 0x30) = param_3;
    thunk_FUN_1803b4a20(lVar1 + 0x30,param_3);
    *(undefined1 *)(lVar1 + 0x20) = param_4;
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



/* ==== UnitActionController::updateCoroutineWithOutCutIn  @ 0x14cdf80  size=138 ==== */

longlong FUN_1814cdf80(undefined8 param_1,undefined8 param_2)

{
  longlong lVar1;
  
  if (DAT_18ab626ab == '\0') {
    FUN_18032f6d0(&DAT_187c22000);
    DAT_18ab626ab = '\x01';
  }
  lVar1 = thunk_FUN_180383da0(DAT_187c22000);
  FUN_180445940(lVar1,0,0);
  if (lVar1 != 0) {
    *(undefined8 *)(lVar1 + 0x20) = param_1;
    thunk_FUN_1803b4a20(lVar1 + 0x20,param_1);
    *(undefined8 *)(lVar1 + 0x28) = param_2;
    thunk_FUN_1803b4a20(lVar1 + 0x28,param_2);
    return lVar1;
  }
                    /* WARNING: Subroutine does not return */
  FUN_18032f9d0();
}



