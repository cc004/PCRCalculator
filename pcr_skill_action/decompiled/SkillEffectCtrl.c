/* ==== SkillEffectCtrl::.cctor  @ 0x105a2f0  size=1 ==== */

/* WARNING: Possible PIC construction at 0x00018105a386: Changing call to branch */
/* WARNING: Possible PIC construction at 0x00018105a3ec: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x00018105a38b) */
/* WARNING: Removing unreachable block (ram,0x00018105a3f1) */

ulonglong SkillEffectCtrl::.cctor(void)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  code *pcVar3;
  ulonglong uVar4;
  uint uVar5;
  bool bVar6;
  
  if (cRam000000018ab606bb == '\0') {
    sub_8032f6d0(0x187b78198);
    sub_8032f6d0(0x187b92070);
    sub_8032f6d0(0x187c0f830);
    cRam000000018ab606bb = '\x01';
  }
  uVar4 = sub_8032e960(uRam0000000187c0f830,2);
  if (uVar4 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar4 = (*pcVar3)();
    return uVar4;
  }
  if ((*(int *)(uVar4 + 0x18) != 0) &&
     (*(undefined4 *)(uVar4 + 0x20) = 0xe3, 1 < *(uint *)(uVar4 + 0x18))) {
    *(undefined4 *)(uVar4 + 0x24) = 0xf;
    **(ulonglong **)(lRam0000000187b78198 + 0xb8) = uVar4;
    if (iRam000000018ab8fbd8 != 0) {
      uVar5 = (uint)(*(ulonglong *)(lRam0000000187b78198 + 0xb8) >> 0xc);
      puVar1 = (ulonglong *)((ulonglong)((uVar5 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
      do {
        uVar2 = *puVar1;
        LOCK();
        uVar4 = *puVar1;
        bVar6 = uVar2 == uVar4;
        if (bVar6) {
          *puVar1 = uVar2 | 1L << (uVar5 & 0x3f);
          uVar4 = uVar2;
        }
        UNLOCK();
      } while (!bVar6);
    }
    return uVar4;
  }
  sub_8032f9c0();
  pcVar3 = (code *)swi(3);
  uVar4 = (*pcVar3)();
  return uVar4;
}



/* ==== SkillEffectCtrl::.ctor  @ 0x105a430  size=1 ==== */

void SkillEffectCtrl::.ctor(longlong param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4)

{
  undefined4 uVar1;
  undefined8 uVar2;
  longlong lVar3;
  undefined8 unaff_RBX;
  
  if (cRam000000018ab606ba == '\0') {
    sub_8032f6d0(0x187bdfba8);
    sub_8032f6d0(0x187b3e760);
    sub_8032f6d0(0x187c07938);
    sub_8032f6d0(0x187c07af0);
    cRam000000018ab606ba = '\x01';
  }
  uVar2 = sub_8032e960(uRam0000000187c07938,0);
  *(undefined8 *)(param_1 + 0x28) = uVar2;
  sub_8032e8d0(param_1 + 0x28,uVar2);
  uVar2 = sub_8032e960(uRam0000000187c07af0,0);
  *(undefined8 *)(param_1 + 0x30) = uVar2;
  sub_8032e8d0(param_1 + 0x30,uVar2);
  uVar2 = sub_8032e960(uRam0000000187bdfba8,0);
  *(undefined8 *)(param_1 + 0x38) = uVar2;
  sub_8032e8d0(param_1 + 0x38,uVar2);
  if (cRam000000018ab5adcd == '\0') {
    sub_8032f6d0(0x187c16e48);
    cRam000000018ab5adcd = '\x01';
  }
  uVar1 = *(undefined4 *)(*(longlong *)(lRam0000000187c16e48 + 0xb8) + 0x14);
  *(undefined8 *)(param_1 + 0xa8) =
       *(undefined8 *)(*(longlong *)(lRam0000000187c16e48 + 0xb8) + 0xc);
  *(undefined4 *)(param_1 + 0xb0) = uVar1;
  lVar3 = *(longlong *)(lRam0000000187b3e760 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  *(undefined8 *)(param_1 + 0xb8) = **(undefined8 **)(lVar3 + 0xb8);
  sub_8032e8d0(param_1 + 0xb8);
  if (cRam000000018ab8a5bc == '\0') {
    sub_8032f6d0(0x187b5e978,0,param_3,param_4,unaff_RBX);
    cRam000000018ab8a5bc = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  return;
}



/* ==== SkillEffectCtrl::AppendCoroutine  @ 0x1056a90  size=1 ==== */

void SkillEffectCtrl::AppendCoroutine(undefined8 param_1,undefined8 param_2,undefined4 param_3,undefined8 param_4)

{
  longlong lVar1;
  code *pcVar2;
  
  if (cRam000000018ab606b1 == '\0') {
    sub_8032f6d0(0x187b91ca8);
    cRam000000018ab606b1 = '\x01';
  }
  if (cRam000000018ab606c5 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab606c5 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18);
  if (lVar1 != 0) {
    sub_800110c0(6,uRam0000000187b91ca8,lVar1,param_2,param_3,param_4);
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::Awake  @ 0x1056b50  size=1 ==== */

/* WARNING: Possible PIC construction at 0x000181056c41: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x000181056c46) */
/* WARNING: Removing unreachable block (ram,0x000181056c5d) */

ulonglong SkillEffectCtrl::Awake(longlong param_1)

{
  ulonglong *puVar1;
  undefined4 uVar2;
  code *pcVar3;
  longlong lVar4;
  undefined8 *puVar5;
  ulonglong uVar6;
  ulonglong uVar7;
  uint uVar8;
  bool bVar9;
  undefined1 auStack_18 [16];
  
  lVar4 = Component::get_transform(param_1,0);
  if (lVar4 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar7 = (*pcVar3)();
    return uVar7;
  }
  puVar5 = (undefined8 *)Transform::get_localScale(auStack_18,lVar4,0);
  bVar9 = cRam000000018ab60699 == '\0';
  uVar2 = *(undefined4 *)(puVar5 + 1);
  *(undefined8 *)(param_1 + 0xa8) = *puVar5;
  *(undefined4 *)(param_1 + 0xb0) = uVar2;
  if (bVar9) {
    sub_8032f6d0(0x187bf9e18);
    sub_8032f6d0(0x187bf05b0);
    sub_8032f6d0(0x187b78198);
    cRam000000018ab60699 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  if (*(longlong *)(*(ulonglong *)(lRam0000000187b78198 + 0xb8) + 0x10) != 0) {
    return *(ulonglong *)(lRam0000000187b78198 + 0xb8);
  }
  uVar6 = sub_847c7d00(param_1,uRam0000000187bf9e18);
  uVar7 = uVar6;
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    uVar7 = sub_8032fa70();
  }
  *(ulonglong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x10) = uVar6;
  if (iRam000000018ab8fbd8 != 0) {
    uVar8 = (uint)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x10U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar8 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar6 = *puVar1;
      LOCK();
      uVar7 = *puVar1;
      bVar9 = uVar6 == uVar7;
      if (bVar9) {
        *puVar1 = uVar6 | 1L << (uVar8 & 0x3f);
        uVar7 = uVar6;
      }
      UNLOCK();
    } while (!bVar9);
  }
  return uVar7;
}



/* ==== SkillEffectCtrl::ExecAppendCoroutine  @ 0x1056cb0  size=1 ==== */

void SkillEffectCtrl::ExecAppendCoroutine(longlong param_1,undefined8 param_2,undefined1 param_3)

{
  code *pcVar1;
  longlong lVar2;
  int iVar3;
  undefined8 uVar4;
  bool bVar5;
  
  if (cRam000000018ab6069f == '\0') {
    sub_8032f6d0(0x187b91ca8);
    cRam000000018ab6069f = '\x01';
  }
  if (*(char *)(param_1 + 0x50) == '\0') {
    iVar3 = *(int *)(param_1 + 0xe4) + 1;
    bVar5 = cRam000000018ab606a6 == '\0';
    *(int *)(param_1 + 0xe4) = iVar3;
    if (bVar5) {
      sub_8032f6d0(0x187bc39a0);
      cRam000000018ab606a6 = '\x01';
    }
    lVar2 = sub_8032f970(uRam0000000187bc39a0);
    sub_80445940(lVar2,0,0);
    if (lVar2 == 0) goto code_r0x000181056e44;
    *(longlong *)(lVar2 + 0x28) = param_1;
    sub_8032e8d0(lVar2 + 0x28,param_1);
    *(int *)(lVar2 + 0x20) = iVar3;
    uVar4 = param_2;
  }
  else {
    if (cRam000000018ab606a7 == '\0') {
      sub_8032f6d0(0x187bc3638);
      cRam000000018ab606a7 = '\x01';
    }
    lVar2 = sub_8032f970(uRam0000000187bc3638);
    sub_80443e50(lVar2,0);
    *(undefined4 *)(lVar2 + 0x10) = 0;
    *(longlong *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    uVar4 = 0;
  }
  SkillEffectCtrl::AppendCoroutine(param_1,lVar2,0,uVar4,0);
  if (cRam000000018ab606c5 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab606c5 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  lVar2 = *(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18);
  if (lVar2 != 0) {
    sub_80011160(0,uRam0000000187b91ca8,lVar2,param_1,param_2,param_3);
    return;
  }
code_r0x000181056e44:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillEffectCtrl::FadeoutSe  @ 0x1056e50  size=1 ==== */

longlong SkillEffectCtrl::FadeoutSe(undefined8 param_1,undefined8 param_2,undefined4 param_3)

{
  longlong lVar1;
  
  if (cRam000000018ab606b5 == '\0') {
    sub_8032f6d0(0x187bc2db0);
    cRam000000018ab606b5 = '\x01';
  }
  lVar1 = sub_8032f970(uRam0000000187bc2db0);
  sub_80443e50(lVar1,0);
  *(undefined4 *)(lVar1 + 0x10) = 0;
  *(undefined8 *)(lVar1 + 0x28) = param_1;
  sub_8032e8d0(lVar1 + 0x28,param_1);
  *(undefined8 *)(lVar1 + 0x30) = param_2;
  sub_8032e8d0(lVar1 + 0x30,param_2);
  *(undefined4 *)(lVar1 + 0x20) = param_3;
  return lVar1;
}



/* ==== SkillEffectCtrl::InitializeSort  @ 0x1056f60  size=1 ==== */

void SkillEffectCtrl::InitializeSort(longlong param_1)

{
  code *pcVar1;
  char cVar2;
  int iVar3;
  uint uVar4;
  undefined8 uVar5;
  longlong lVar6;
  longlong lVar7;
  uint uVar8;
  undefined4 uVar9;
  undefined8 uStackX_8;
  undefined8 auStackX_18 [2];
  
  if (cRam000000018ab6069d == '\0') {
    sub_8032f6d0(0x187b91ca8);
    sub_8032f6d0(0x187b78198);
    cRam000000018ab6069d = '\x01';
  }
  if (cRam000000018ab606c5 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab606c5 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  lVar6 = *(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18);
  if (lVar6 == 0) {
    sub_8032f9d0();
    pcVar1 = (code *)swi(3);
    (*pcVar1)();
    return;
  }
  sub_80002fb0(1,uRam0000000187b91ca8,lVar6,param_1);
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  SkillEffectCtrl::setLayerName(param_1,*(undefined8 *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 8),0);
  if (cRam000000018ab606a0 == '\0') {
    sub_8032f6d0(0x187bfabd0,0);
    sub_8032f6d0(0x187bfad88);
    sub_8032f6d0(0x187bff3f8);
    sub_8032f6d0(0x187bf5c20);
    sub_8032f6d0(0x187bf2368);
    sub_8032f6d0(0x187bf21b0);
    sub_8032f6d0(0x187bf5a68);
    sub_8032f6d0(0x187b35940);
    sub_8032f6d0(0x187b36528);
    sub_8032f6d0(0x187bce608);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606a0 = '\x01';
  }
  uVar8 = 0;
  auStackX_18[0] = 0;
  if (*(longlong *)(param_1 + 0xc0) != 0) {
    return;
  }
  uVar5 = sub_8032f970(uRam0000000187b36528);
  sub_85660990(uVar5,uRam0000000187bf5a68);
  *(undefined8 *)(param_1 + 0xc0) = uVar5;
  sub_8032e8d0(param_1 + 0xc0,uVar5);
  lVar6 = Component::get_gameObject(param_1,0);
  if (lVar6 != 0) {
    uVar5 = sub_847c75c0(lVar6,1,uRam0000000187bce608);
    *(undefined8 *)(param_1 + 0xd0) = uVar5;
    sub_8032e8d0(param_1 + 0xd0,uVar5);
    uVar5 = sub_8032f970(uRam0000000187b35940);
    sub_85660990(uVar5,uRam0000000187bf21b0);
    *(undefined8 *)(param_1 + 0xd8) = uVar5;
    sub_8032e8d0(param_1 + 0xd8,uVar5);
    lVar6 = sub_8475aaf0(param_1,uRam0000000187bfabd0);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar2 = Object::op_Inequality(lVar6,0,0);
    if (cVar2 != '\0') {
      if (lVar6 == 0) goto DAT_18105ab0f;
      *(undefined8 *)(param_1 + 0x78) = *(undefined8 *)(lVar6 + 0x20);
      sub_8032e8d0(param_1 + 0x78);
    }
    lVar6 = *(longlong *)(param_1 + 0xd0);
    if (lVar6 != 0) {
      while( true ) {
        if (*(int *)(lVar6 + 0x18) <= (int)uVar8) {
          return;
        }
        lVar6 = *(longlong *)(param_1 + 0xd0);
        if (lVar6 == 0) break;
        if (*(uint *)(lVar6 + 0x18) <= uVar8) {
code_r0x00018105ab15:
          sub_8032f9c0();
          pcVar1 = (code *)swi(3);
          (*pcVar1)();
          return;
        }
        lVar6 = *(longlong *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        if ((lVar6 == 0) || (lVar6 = sub_8475aaf0(lVar6,uRam0000000187bff3f8), lVar6 == 0)) break;
        iVar3 = Renderer::get_sortingOrder(lVar6,0);
        if (5000 < iVar3) {
          iVar3 = 5000;
        }
        Renderer::set_sortingOrder(lVar6,iVar3,0);
        uVar4 = Renderer::get_sortingOrder(lVar6,0);
        lVar7 = *(longlong *)(param_1 + 0xd0);
        uStackX_8 = (ulonglong)uVar4;
        if (lVar7 == 0) break;
        if (*(uint *)(lVar7 + 0x18) <= uVar8) goto code_r0x00018105ab15;
        lVar7 = *(longlong *)(lVar7 + 0x20 + (longlong)(int)uVar8 * 8);
        if (lVar7 == 0) break;
        lVar7 = sub_8475aaf0(lVar7,uRam0000000187bfad88);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        cVar2 = Object::op_Inequality(lVar7,0,0);
        if (cVar2 != '\0') {
          if (lVar7 == 0) break;
          uStackX_8 = CONCAT44(*(undefined4 *)(lVar7 + 0x20),uVar4);
        }
        if (*(longlong *)(param_1 + 0xc0) == 0) break;
        sub_856a40c0(*(longlong *)(param_1 + 0xc0),lVar6,uStackX_8,uRam0000000187bf5c20);
        lVar6 = *(longlong *)(param_1 + 0xd0);
        lVar7 = *(longlong *)(param_1 + 0xd8);
        if (lVar6 == 0) break;
        if (*(uint *)(lVar6 + 0x18) <= uVar8) goto code_r0x00018105ab15;
        uVar5 = *(undefined8 *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        lVar6 = *(longlong *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        if (lVar6 == 0) break;
        auStackX_18[0] = sub_85c66220(lVar6,0);
        uVar9 = MainModule::get_startDelayMultiplier(auStackX_18,0);
        if (lVar7 == 0) break;
        sub_85686120(lVar7,uVar5,uVar9,uRam0000000187bf2368);
        lVar6 = *(longlong *)(param_1 + 0xd0);
        uVar8 = uVar8 + 1;
        if (lVar6 == 0) break;
      }
    }
  }
DAT_18105ab0f:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillEffectCtrl::InitializeSortForWithOutBattle  @ 0x1056ef0  size=1 ==== */

void SkillEffectCtrl::InitializeSortForWithOutBattle(longlong param_1)

{
  code *pcVar1;
  char cVar2;
  int iVar3;
  uint uVar4;
  undefined8 uVar5;
  longlong lVar6;
  longlong lVar7;
  uint uVar8;
  undefined4 uVar9;
  undefined8 uStackX_8;
  undefined8 auStackX_18 [2];
  
  if (cRam000000018ab6069e == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab6069e = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  SkillEffectCtrl::setLayerName(param_1,*(undefined8 *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 8),0);
  if (cRam000000018ab606a0 == '\0') {
    sub_8032f6d0(0x187bfabd0,0);
    sub_8032f6d0(0x187bfad88);
    sub_8032f6d0(0x187bff3f8);
    sub_8032f6d0(0x187bf5c20);
    sub_8032f6d0(0x187bf2368);
    sub_8032f6d0(0x187bf21b0);
    sub_8032f6d0(0x187bf5a68);
    sub_8032f6d0(0x187b35940);
    sub_8032f6d0(0x187b36528);
    sub_8032f6d0(0x187bce608);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606a0 = '\x01';
  }
  uVar8 = 0;
  auStackX_18[0] = 0;
  if (*(longlong *)(param_1 + 0xc0) != 0) {
    return;
  }
  uVar5 = sub_8032f970(uRam0000000187b36528);
  sub_85660990(uVar5,uRam0000000187bf5a68);
  *(undefined8 *)(param_1 + 0xc0) = uVar5;
  sub_8032e8d0(param_1 + 0xc0,uVar5);
  lVar6 = Component::get_gameObject(param_1,0);
  if (lVar6 != 0) {
    uVar5 = sub_847c75c0(lVar6,1,uRam0000000187bce608);
    *(undefined8 *)(param_1 + 0xd0) = uVar5;
    sub_8032e8d0(param_1 + 0xd0,uVar5);
    uVar5 = sub_8032f970(uRam0000000187b35940);
    sub_85660990(uVar5,uRam0000000187bf21b0);
    *(undefined8 *)(param_1 + 0xd8) = uVar5;
    sub_8032e8d0(param_1 + 0xd8,uVar5);
    lVar6 = sub_8475aaf0(param_1,uRam0000000187bfabd0);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar2 = Object::op_Inequality(lVar6,0,0);
    if (cVar2 != '\0') {
      if (lVar6 == 0) goto DAT_18105ab0f;
      *(undefined8 *)(param_1 + 0x78) = *(undefined8 *)(lVar6 + 0x20);
      sub_8032e8d0(param_1 + 0x78);
    }
    lVar6 = *(longlong *)(param_1 + 0xd0);
    if (lVar6 != 0) {
      while( true ) {
        if (*(int *)(lVar6 + 0x18) <= (int)uVar8) {
          return;
        }
        lVar6 = *(longlong *)(param_1 + 0xd0);
        if (lVar6 == 0) break;
        if (*(uint *)(lVar6 + 0x18) <= uVar8) {
code_r0x00018105ab15:
          sub_8032f9c0();
          pcVar1 = (code *)swi(3);
          (*pcVar1)();
          return;
        }
        lVar6 = *(longlong *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        if ((lVar6 == 0) || (lVar6 = sub_8475aaf0(lVar6,uRam0000000187bff3f8), lVar6 == 0)) break;
        iVar3 = Renderer::get_sortingOrder(lVar6,0);
        if (5000 < iVar3) {
          iVar3 = 5000;
        }
        Renderer::set_sortingOrder(lVar6,iVar3,0);
        uVar4 = Renderer::get_sortingOrder(lVar6,0);
        lVar7 = *(longlong *)(param_1 + 0xd0);
        uStackX_8 = (ulonglong)uVar4;
        if (lVar7 == 0) break;
        if (*(uint *)(lVar7 + 0x18) <= uVar8) goto code_r0x00018105ab15;
        lVar7 = *(longlong *)(lVar7 + 0x20 + (longlong)(int)uVar8 * 8);
        if (lVar7 == 0) break;
        lVar7 = sub_8475aaf0(lVar7,uRam0000000187bfad88);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        cVar2 = Object::op_Inequality(lVar7,0,0);
        if (cVar2 != '\0') {
          if (lVar7 == 0) break;
          uStackX_8 = CONCAT44(*(undefined4 *)(lVar7 + 0x20),uVar4);
        }
        if (*(longlong *)(param_1 + 0xc0) == 0) break;
        sub_856a40c0(*(longlong *)(param_1 + 0xc0),lVar6,uStackX_8,uRam0000000187bf5c20);
        lVar6 = *(longlong *)(param_1 + 0xd0);
        lVar7 = *(longlong *)(param_1 + 0xd8);
        if (lVar6 == 0) break;
        if (*(uint *)(lVar6 + 0x18) <= uVar8) goto code_r0x00018105ab15;
        uVar5 = *(undefined8 *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        lVar6 = *(longlong *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        if (lVar6 == 0) break;
        auStackX_18[0] = sub_85c66220(lVar6,0);
        uVar9 = MainModule::get_startDelayMultiplier(auStackX_18,0);
        if (lVar7 == 0) break;
        sub_85686120(lVar7,uVar5,uVar9,uRam0000000187bf2368);
        lVar6 = *(longlong *)(param_1 + 0xd0);
        uVar8 = uVar8 + 1;
        if (lVar6 == 0) break;
      }
    }
  }
DAT_18105ab0f:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillEffectCtrl::OnAwakeWhenSkipCutIn  @ 0x443e50  size=3 ==== */

void SkillEffectCtrl::OnAwakeWhenSkipCutIn(void)

{
  return;
}



/* ==== SkillEffectCtrl::OnDestroy  @ 0x1057040  size=1 ==== */

void SkillEffectCtrl::OnDestroy(longlong *param_1)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  
  if (cRam000000018ab6069a == '\0') {
    sub_8032f6d0(0x187b91ca8);
    cRam000000018ab6069a = '\x01';
  }
  if (cRam000000018ab606c5 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab606c5 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (*(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18) != 0) {
    if (cRam000000018ab606c5 == '\0') {
      sub_8032f6d0(0x187b78198);
      cRam000000018ab606c5 = '\x01';
    }
    if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
      sub_8032fa70();
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18) == 0) {
DAT_181057260:
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
    cVar3 = sub_80002270(3,uRam0000000187b91ca8);
    if (cVar3 != '\0') {
      if (cRam000000018ab606c5 == '\0') {
        sub_8032f6d0(0x187b78198);
        cRam000000018ab606c5 = '\x01';
      }
      if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
        sub_8032fa70(lRam0000000187b78198);
      }
      lVar1 = *(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18);
      if (lVar1 == 0) goto DAT_181057260;
      sub_80002fb0(2,uRam0000000187b91ca8,lVar1,param_1);
    }
  }
  param_1[4] = 0;
  sub_8032e8d0(param_1 + 4,0);
  param_1[5] = 0;
  sub_8032e8d0(param_1 + 5,0);
  param_1[6] = 0;
  sub_8032e8d0(param_1 + 6,0);
  param_1[9] = 0;
  sub_8032e8d0(param_1 + 9,0);
  param_1[0xb] = 0;
  sub_8032e8d0(param_1 + 0xb,0);
  param_1[0xd] = 0;
  sub_8032e8d0(param_1 + 0xd,0);
  param_1[0x11] = 0;
  sub_8032e8d0(param_1 + 0x11,0);
  param_1[0x12] = 0;
  sub_8032e8d0(param_1 + 0x12,0);
  param_1[0x13] = 0;
  sub_8032e8d0(param_1 + 0x13,0);
  param_1[0x17] = 0;
  sub_8032e8d0(param_1 + 0x17,0);
  param_1[0x18] = 0;
  sub_8032e8d0(param_1 + 0x18,0);
                    /* WARNING: Could not recover jumptable at 0x000181057259. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  (**(code **)(*param_1 + 0x178))(param_1,*(undefined8 *)(*param_1 + 0x180));
  return;
}



/* ==== SkillEffectCtrl::Pause  @ 0x1057270  size=1 ==== */

void SkillEffectCtrl::Pause(longlong param_1)

{
  undefined8 uVar1;
  code *pcVar2;
  char cVar3;
  undefined4 uVar4;
  
  if (cRam000000018ab606a8 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606a8 = '\x01';
  }
  uVar1 = *(undefined8 *)(param_1 + 0x20);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = Object::op_Inequality(uVar1,0,0);
  if (cVar3 != '\0') {
    if (*(longlong *)(param_1 + 0x20) != 0) {
      ParticleSystem::Pause(*(longlong *)(param_1 + 0x20),1,0);
      if (*(longlong *)(param_1 + 0x20) != 0) {
        uVar4 = ParticleSystem::get_time(*(longlong *)(param_1 + 0x20),0);
        *(undefined4 *)(param_1 + 0xe0) = uVar4;
        SkillEffectCtrl::pauseSe(param_1,1,0);
        goto code_r0x0001810572fb;
      }
    }
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
code_r0x0001810572fb:
  SkillEffectCtrl::setEnableAnimations(param_1,0,0);
  *(undefined1 *)(param_1 + 0xb5) = 1;
  return;
}



/* ==== SkillEffectCtrl::PlaySe  @ 0x1057430  size=1 ==== */

void SkillEffectCtrl::PlaySe(longlong param_1,undefined4 param_2,undefined1 param_3)

{
  undefined4 uVar1;
  undefined8 uVar2;
  code *pcVar3;
  char cVar4;
  int iVar5;
  undefined8 uVar6;
  longlong lVar7;
  int iVar8;
  
  if (cRam000000018ab606b2 == '\0') {
    sub_8032f6d0(0x187b440e8);
    sub_8032f6d0(0x187b91ca8);
    sub_8032f6d0(0x187b78198);
    sub_8032f6d0(0x187b7e5d8);
    sub_8032f6d0(0x187c37f38);
    sub_8032f6d0(0x187c44a68);
    sub_8032f6d0(0x187c3b230);
    sub_8032f6d0(0x187c42d70);
    sub_8032f6d0(0x187c37d88);
    cRam000000018ab606b2 = '\x01';
  }
  if (cRam000000018ab606c5 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab606c5 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  if (*(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18) != 0) {
    cVar4 = sub_80002270(0x18,uRam0000000187b91ca8);
    if (cVar4 == '\0') {
      return;
    }
    if (*(int *)(param_1 + 100) == 0) {
      if (*(char *)(param_1 + 0x60) == '\0') {
        if (*(int *)(lRam0000000187b440e8 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uVar6 = BattleVoiceUtility::ConvertToSkillCueSheetName(param_2,0);
      }
      else {
        if (*(int *)(lRam0000000187b7e5d8 + 0xe4) == 0) {
          sub_8032fa70(lRam0000000187b7e5d8);
        }
        uVar6 = *(undefined8 *)(*(longlong *)(lRam0000000187b7e5d8 + 0xb8) + 0x20);
      }
      lVar7 = Component::get_gameObject(param_1,0);
      if ((((lVar7 != 0) && (lVar7 = Object::get_name(lVar7,0), lVar7 != 0)) &&
          ((cVar4 = String::Contains(lVar7,uRam0000000187c37d88,0), cVar4 == '\0' ||
           ((lRam0000000187c37f38 != 0 &&
            (lVar7 = String::Substring(lVar7,*(undefined4 *)(lRam0000000187c37f38 + 0x10),0),
            lVar7 != 0)))))) &&
         ((iVar5 = String::LastIndexOf(lVar7,uRam0000000187c3b230,0), iVar5 < 0 ||
          (lVar7 = String::Substring(lVar7,0,iVar5,0), lVar7 != 0)))) {
        iVar8 = *(int *)(lVar7 + 0x10) + -2;
        iVar5 = String::LastIndexOf(lVar7,uRam0000000187c42d70,0);
        if ((iVar5 == iVar8) ||
           (iVar5 = String::LastIndexOf(lVar7,uRam0000000187c44a68,0), iVar5 == iVar8)) {
          lVar7 = String::Substring(lVar7,0,iVar8,0);
        }
        uVar2 = *(undefined8 *)(param_1 + 0x48);
        if (*(int *)(lRam0000000187b440e8 + 0xe4) == 0) {
          sub_8032fa70();
        }
        BattleVoiceUtility::EnchantSeDirectionToSeSource(uVar2,param_3,0);
        if (*(longlong *)(param_1 + 0xb8) != 0) {
          SoundManager::PlaySeByOuterSource(*(longlong *)(param_1 + 0xb8),*(undefined8 *)(param_1 + 0x48),0,uVar6,lVar7,
                        0);
          return;
        }
      }
    }
    else {
      if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
        sub_8032fa70();
      }
      lVar7 = **(longlong **)(lRam0000000187b78198 + 0xb8);
      if (lVar7 != 0) {
        if (*(uint *)(lVar7 + 0x18) <= *(uint *)(param_1 + 100)) {
          sub_8032f9c0();
          pcVar3 = (code *)swi(3);
          (*pcVar3)();
          return;
        }
        uVar1 = *(undefined4 *)(lVar7 + 0x20 + (longlong)(int)*(uint *)(param_1 + 100) * 4);
        if (cRam000000018ab606b3 == '\0') {
          sub_8032f6d0(0x187b440e8);
          sub_8032f6d0(0x187b91ca8);
          cRam000000018ab606b3 = '\x01';
        }
        if (cRam000000018ab606c5 == '\0') {
          sub_8032f6d0(0x187b78198);
          cRam000000018ab606c5 = '\x01';
        }
        if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
          sub_8032fa70();
        }
        if (*(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18) != 0) {
          cVar4 = sub_80002270(0x18,uRam0000000187b91ca8);
          if (cVar4 == '\0') {
            return;
          }
          uVar6 = *(undefined8 *)(param_1 + 0x48);
          if (*(int *)(lRam0000000187b440e8 + 0xe4) == 0) {
            sub_8032fa70();
          }
          BattleVoiceUtility::EnchantSeDirectionToSeSource(uVar6,param_3,0);
          if (*(longlong *)(param_1 + 0xb8) != 0) {
            SoundManager::PlaySeByOuterSource(*(longlong *)(param_1 + 0xb8),*(undefined8 *)(param_1 + 0x48),uVar1,0,0);
            return;
          }
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== SkillEffectCtrl::PlaySe  @ 0x1057320  size=1 ==== */

void SkillEffectCtrl::PlaySe(longlong param_1,undefined4 param_2,undefined1 param_3)

{
  undefined8 uVar1;
  code *pcVar2;
  char cVar3;
  
  if (cRam000000018ab606b3 == '\0') {
    sub_8032f6d0(0x187b440e8);
    sub_8032f6d0(0x187b91ca8);
    cRam000000018ab606b3 = '\x01';
  }
  if (cRam000000018ab606c5 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab606c5 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  if (*(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18) != 0) {
    cVar3 = sub_80002270(0x18,uRam0000000187b91ca8);
    if (cVar3 != '\0') {
      uVar1 = *(undefined8 *)(param_1 + 0x48);
      if (*(int *)(lRam0000000187b440e8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      BattleVoiceUtility::EnchantSeDirectionToSeSource(uVar1,param_3,0);
      if (*(longlong *)(param_1 + 0xb8) == 0) goto code_r0x000181057426;
      SoundManager::PlaySeByOuterSource(*(longlong *)(param_1 + 0xb8),*(undefined8 *)(param_1 + 0x48),param_2,0,0);
    }
    return;
  }
code_r0x000181057426:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::ResetParameter  @ 0x1057810  size=1 ==== */

void SkillEffectCtrl::ResetParameter(longlong param_1,longlong param_2,int param_3)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  longlong lVar4;
  longlong lVar5;
  longlong lVar6;
  undefined8 uVar7;
  longlong *plVar8;
  int iVar9;
  longlong lStack_a8;
  undefined8 uStack_a0;
  undefined8 uStack_98;
  undefined8 *puStack_90;
  undefined8 uStack_88;
  undefined8 uStack_80;
  undefined8 uStack_78;
  longlong lStack_70;
  undefined8 uStack_68;
  undefined8 uStack_60;
  undefined8 uStack_50;
  undefined8 uStack_48;
  longlong lStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  
  if (cRam000000018ab606b7 == '\0') {
    sub_8032f6d0(0x187bfaa18);
    sub_8032f6d0(0x187bf2520);
    sub_8032f6d0(0x187b47f78);
    sub_8032f6d0(0x187b48130);
    sub_8032f6d0(0x187b482e0);
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187b91ca8);
    sub_8032f6d0(0x187c124a0);
    sub_8032f6d0(0x187c12658);
    sub_8032f6d0(0x187b32a58);
    sub_8032f6d0(0x187c24bd8);
    sub_8032f6d0(0x187c24870);
    sub_8032f6d0(0x187c24d88);
    sub_8032f6d0(0x187c24a20);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187babe70);
    sub_8032f6d0(0x187b574d8);
    sub_8032f6d0(0x187bc2c00);
    cRam000000018ab606b7 = '\x01';
  }
  uStack_80 = 0;
  uStack_78 = 0;
  lStack_70 = 0;
  uStack_68 = 0;
  uStack_60 = 0;
  uStack_88 = 0;
  lVar3 = sub_8032f970(uRam0000000187bc2c00);
  sub_80443e50(lVar3,0);
  if (lVar3 != 0) {
    *(int *)(lVar3 + 0x10) = param_3;
    if (param_3 == 0) {
code_r0x000181057a7c:
      if (cRam000000018ab606c5 == '\0') {
        sub_8032f6d0(0x187b78198);
        cRam000000018ab606c5 = '\x01';
      }
      if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
        sub_8032fa70(lRam0000000187b78198);
      }
      if (*(longlong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18) != 0) {
        cVar2 = sub_80002270(0xd,uRam0000000187b91ca8);
        if (cVar2 == '\0') {
code_r0x000181057d48:
          *(undefined1 *)(param_1 + 0xb4) = 0;
          *(undefined1 *)(param_1 + 0xa0) = 1;
          *(undefined8 *)(param_1 + 0x88) = 0;
          sub_8032e8d0(param_1 + 0x88,0);
          if (cRam000000018ab606b6 == '\0') {
            sub_8032f6d0(0x187b5e978);
            cRam000000018ab606b6 = '\x01';
          }
          uVar7 = *(undefined8 *)(param_1 + 0x48);
          if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
            sub_8032fa70();
          }
          cVar2 = Object::op_Equality(uVar7,0,0);
          if (cVar2 == '\0') {
            if (*(longlong *)(param_1 + 0x48) == 0) goto code_r0x00018105809d;
            CriAtomSourceBase::Pause(*(longlong *)(param_1 + 0x48),0,0);
          }
          lVar3 = Component::get_transform(param_1,0);
          if (((param_2 != 0) && (lVar4 = GameObject::get_transform(param_2,0), lVar4 != 0)) &&
             (plVar8 = (longlong *)Transform::get_localPosition(&uStack_98,lVar4,0), lVar3 != 0)) {
            lStack_a8 = *plVar8;
            uStack_a0 = CONCAT44(uStack_a0._4_4_,(int)plVar8[1]);
            Transform::set_localPosition(lVar3,&lStack_a8,0);
            uVar7 = *(undefined8 *)(param_1 + 0x20);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar2 = Object::op_Inequality(uVar7,0,0);
            if (cVar2 == '\0') {
code_r0x000181057f92:
              if (*(longlong *)(param_1 + 0xd8) != 0) {
                sub_855337a0(&uStack_50,*(undefined8 *)(param_1 + 0xd8),uRam0000000187bf2520);
                uStack_80 = uStack_50;
                uStack_78 = uStack_48;
                lStack_70 = lStack_40;
                uStack_68 = uStack_38;
                uStack_60 = uStack_30;
                uStack_98 = 0;
                puStack_90 = &uStack_80;
                while (cVar2 = sub_85772d60(&uStack_80,uRam0000000187b48130), cVar2 != '\0') {
                  lStack_a8 = lStack_70;
                  uStack_a0 = uStack_68;
                  if (lStack_70 == 0) goto code_r0x0001810580a3;
                  uStack_88 = sub_85c66220(lStack_70,0);
                  MainModule::set_startDelayMultiplier(&uStack_88,(undefined4)uStack_a0,0);
                }
                sub_80443e50(&uStack_80,uRam0000000187b47f78);
              }
              return;
            }
            if (*(longlong *)(param_1 + 0x20) != 0) {
              lVar3 = Component::get_transform(*(longlong *)(param_1 + 0x20),0);
              lVar4 = GameObject::get_transform(param_2,0);
              if (((lVar4 != 0) && (lVar4 = Transform::GetChild(lVar4,0,0), lVar4 != 0)) &&
                 (plVar8 = (longlong *)Transform::get_localPosition(&uStack_98,lVar4,0), lVar3 != 0)) {
                lStack_a8 = *plVar8;
                uStack_a0 = CONCAT44(uStack_a0._4_4_,(int)plVar8[1]);
                Transform::set_localPosition(lVar3,&lStack_a8,0);
                if (*(longlong *)(param_1 + 0x20) != 0) {
                  lVar3 = Component::get_transform(*(longlong *)(param_1 + 0x20),0);
                  lVar4 = GameObject::get_transform(param_2,0);
                  if (((lVar4 != 0) && (lVar4 = Transform::GetChild(lVar4,0,0), lVar4 != 0)) &&
                     (plVar8 = (longlong *)Transform::get_localEulerAngles(&uStack_98,lVar4,0), lVar3 != 0)) {
                    lStack_a8 = *plVar8;
                    uStack_a0 = CONCAT44(uStack_a0._4_4_,(int)plVar8[1]);
                    Transform::set_localEulerAngles(lVar3,&lStack_a8,0);
                    if (*(longlong *)(param_1 + 0x20) != 0) {
                      ParticleSystem::Simulate(*(longlong *)(param_1 + 0x20),0,1,1,1,0);
                      if (*(longlong *)(param_1 + 0x20) != 0) {
                        ParticleSystem::Play(*(longlong *)(param_1 + 0x20),1,0);
                        goto code_r0x000181057f92;
                      }
                    }
                  }
                }
              }
            }
          }
        }
        else {
          iVar9 = 0;
          lVar3 = *(longlong *)(param_1 + 0x70);
          if (lVar3 != 0) {
            while (iVar9 < *(int *)(lVar3 + 0x18)) {
              if ((((*(longlong *)(param_1 + 0x70) == 0) ||
                   (lVar3 = sub_849ed2d0(*(longlong *)(param_1 + 0x70),iVar9,uRam0000000187c24a20),
                   lVar3 == 0)) || (*(longlong *)(lVar3 + 0x18) == 0)) ||
                 (lVar4 = sub_861de240(*(longlong *)(lVar3 + 0x18),0), lVar4 == 0))
              goto code_r0x00018105809d;
              Material::SetTexture(lVar4,*(undefined8 *)(lVar3 + 0x20),*(undefined8 *)(lVar3 + 0x10),0);
              if (*(longlong *)(lVar3 + 0x18) == 0) goto code_r0x00018105809d;
              uVar7 = ParticleSystemRenderer::get_trailMaterial(*(longlong *)(lVar3 + 0x18),0);
              if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
                sub_8032fa70();
              }
              cVar2 = Object::op_Inequality(uVar7,0);
              if (cVar2 != '\0') {
                if ((*(longlong *)(lVar3 + 0x18) == 0) ||
                   (lVar4 = ParticleSystemRenderer::get_trailMaterial(*(longlong *)(lVar3 + 0x18),0), lVar4 == 0))
                goto code_r0x00018105809d;
                Material::SetTexture(lVar4,*(undefined8 *)(lVar3 + 0x20),*(undefined8 *)(lVar3 + 0x10),0);
              }
              if (*(char *)(lVar3 + 0x28) != '\0') {
                if (*(longlong *)(lVar3 + 0x18) == 0) goto code_r0x00018105809d;
                uVar7 = Component::get_transform(*(longlong *)(lVar3 + 0x18),0);
                if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                  sub_8032fa70();
                }
                ExtensionMethods::SetLocalPosX(uVar7);
              }
              iVar9 = iVar9 + 1;
              lVar3 = *(longlong *)(param_1 + 0x70);
              if (lVar3 == 0) goto code_r0x00018105809d;
            }
            lVar3 = sub_8475aaf0(param_1,uRam0000000187bfaa18);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar2 = Object::op_Inequality(lVar3,0,0);
            if (cVar2 == '\0') goto code_r0x000181057d48;
            if (lVar3 != 0) {
              if (cRam000000018ab6067a == '\0') {
                sub_8032f6d0(0x187c3ac48);
                sub_8032f6d0();
                cRam000000018ab6067a = '\x01';
              }
              iVar9 = 0;
              lVar4 = *(longlong *)(lVar3 + 0x20);
              if (lVar4 != 0) goto code_r0x000181057ca0;
            }
          }
        }
      }
    }
    else {
      iVar9 = 0;
      lVar4 = *(longlong *)(param_1 + 0x68);
      if (lVar4 != 0) {
        while( true ) {
          if (*(int *)(lVar4 + 0x18) <= iVar9) goto code_r0x000181057a7c;
          if (((*(longlong *)(param_1 + 0x68) == 0) ||
              (lVar4 = sub_849ed2d0(*(longlong *)(param_1 + 0x68),iVar9,uRam0000000187c24d88),
              lVar4 == 0)) || (*(longlong *)(lVar4 + 0x10) == 0)) break;
          lVar4 = sub_861de240(*(longlong *)(lVar4 + 0x10),0);
          if ((*(longlong *)(param_1 + 0x68) == 0) ||
             (lVar5 = sub_849ed2d0(*(longlong *)(param_1 + 0x68),iVar9,uRam0000000187c24d88),
             lVar5 == 0)) break;
          lVar5 = *(longlong *)(lVar5 + 0x18);
          lVar6 = *(longlong *)(lVar3 + 0x18);
          if (lVar6 == 0) {
            lVar6 = sub_8032f970(uRam0000000187babe70);
            sub_84cacb40(lVar6,lVar3,uRam0000000187b574d8);
            *(longlong *)(lVar3 + 0x18) = lVar6;
            sub_8032e8d0(lVar3 + 0x18,lVar6);
          }
          if (((lVar5 == 0) || (lVar5 = sub_84a8c920(lVar5,lVar6,uRam0000000187b32a58), lVar5 == 0)
              ) || (lVar4 == 0)) break;
          Material::set_mainTexture(lVar4,*(undefined8 *)(lVar5 + 0x10),0);
          iVar9 = iVar9 + 1;
          lVar4 = *(longlong *)(param_1 + 0x68);
          if (lVar4 == 0) break;
        }
      }
    }
  }
  goto code_r0x00018105809d;
code_r0x000181057ca0:
  if (*(int *)(lVar4 + 0x18) <= iVar9) {
    iVar9 = 0;
    lVar4 = *(longlong *)(lVar3 + 0x28);
    if (lVar4 != 0) goto code_r0x000181057d00;
    goto code_r0x00018105809d;
  }
  if ((*(longlong *)(lVar3 + 0x20) == 0) ||
     (lVar4 = sub_849ed2d0(*(longlong *)(lVar3 + 0x20),iVar9), lVar4 == 0))
  goto code_r0x00018105809d;
  GameObject::SetActive(lVar4);
  iVar9 = iVar9 + 1;
  lVar4 = *(longlong *)(lVar3 + 0x20);
  if (lVar4 == 0) goto code_r0x00018105809d;
  goto code_r0x000181057ca0;
  while( true ) {
    GameObject::SetActive(lVar4);
    iVar9 = iVar9 + 1;
    lVar4 = *(longlong *)(lVar3 + 0x28);
    if (lVar4 == 0) break;
code_r0x000181057d00:
    if (*(int *)(lVar4 + 0x18) <= iVar9) goto code_r0x000181057d48;
    if ((*(longlong *)(lVar3 + 0x28) == 0) || (lVar4 = sub_849ed2d0(), lVar4 == 0)) break;
  }
code_r0x00018105809d:
  sub_8032f9d0();
code_r0x0001810580a3:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillEffectCtrl::RestartTween  @ 0x10580b0  size=1 ==== */

void SkillEffectCtrl::RestartTween(longlong *param_1)

{
  int iVar1;
  longlong lVar2;
  code *pcVar3;
  uint uVar4;
  uint uVar5;
  
  uVar4 = 0;
  uVar5 = 0;
  if (param_1[5] != 0) {
    iVar1 = *(int *)(param_1[5] + 0x18);
    if (0 < iVar1) {
      do {
        lVar2 = param_1[5];
        if (lVar2 == 0) goto code_r0x000181058186;
        if (*(uint *)(lVar2 + 0x18) <= uVar5) goto code_r0x00018105818c;
        (**(code **)(*param_1 + 0x208))
                  (param_1,*(undefined8 *)(lVar2 + 0x20 + (longlong)(int)uVar5 * 8),
                   *(undefined8 *)(*param_1 + 0x210));
        uVar5 = uVar5 + 1;
      } while ((int)uVar5 < iVar1);
    }
    if (param_1[6] != 0) {
      iVar1 = *(int *)(param_1[6] + 0x18);
      if (0 < iVar1) {
        do {
          lVar2 = param_1[6];
          if (lVar2 == 0) goto code_r0x000181058186;
          if (*(uint *)(lVar2 + 0x18) <= uVar4) {
code_r0x00018105818c:
            sub_8032f9c0();
            pcVar3 = (code *)swi(3);
            (*pcVar3)();
            return;
          }
          lVar2 = *(longlong *)(lVar2 + 0x20 + (longlong)(int)uVar4 * 8);
          if (lVar2 == 0) goto code_r0x000181058186;
          UITweener::ResetToBeginning(lVar2,0);
          lVar2 = param_1[6];
          if (lVar2 == 0) goto code_r0x000181058186;
          if (*(uint *)(lVar2 + 0x18) <= uVar4) goto code_r0x00018105818c;
          if (*(longlong *)(lVar2 + 0x20 + (longlong)(int)uVar4 * 8) == 0)
          goto code_r0x000181058186;
          sub_82682120();
          uVar4 = uVar4 + 1;
        } while ((int)uVar4 < iVar1);
      }
      return;
    }
  }
code_r0x000181058186:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== SkillEffectCtrl::Resume  @ 0x10581a0  size=1 ==== */

void SkillEffectCtrl::Resume(longlong *param_1)

{
  code *pcVar1;
  char cVar2;
  undefined8 uVar3;
  longlong lVar4;
  
  if (cRam000000018ab606ab == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606ab = '\x01';
  }
  lVar4 = param_1[4];
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar2 = Object::op_Inequality(lVar4,0,0);
  if (cVar2 != '\0') {
    if (param_1[4] == 0) goto code_r0x0001810582b2;
    cVar2 = ParticleSystem::get_isPaused(param_1[4],0);
    if (cVar2 != '\0') {
      uVar3 = Component::get_gameObject(param_1,0);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar2 = Object::op_Inequality(uVar3,0,0);
      if (cVar2 != '\0') {
        lVar4 = Component::get_gameObject(param_1,0);
        if (lVar4 == 0) {
code_r0x0001810582b2:
          sub_8032f9d0();
          pcVar1 = (code *)swi(3);
          (*pcVar1)();
          return;
        }
        cVar2 = GameObject::get_activeSelf(lVar4,0);
        if (cVar2 != '\0') {
          if (param_1[4] == 0) goto code_r0x0001810582b2;
          ParticleSystem::Play(param_1[4],0);
          (**(code **)(*param_1 + 0x1e8))(param_1,*(undefined8 *)(*param_1 + 0x1f0));
          SkillEffectCtrl::pauseSe(param_1,0,0);
        }
      }
    }
  }
  SkillEffectCtrl::setEnableAnimations(param_1,1,0);
  *(undefined1 *)((longlong)param_1 + 0xb5) = 0;
  return;
}



/* ==== SkillEffectCtrl::SetLayerUI  @ 0x10582c0  size=1 ==== */

void SkillEffectCtrl::SetLayerUI(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4)

{
  code *pcVar1;
  longlong lVar2;
  undefined4 uVar3;
  int iVar4;
  longlong lVar5;
  longlong lVar6;
  int iVar7;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab6069c == '\0') {
    sub_8032f6d0(0x187c38198);
    cRam000000018ab6069c = '\x01';
  }
  lVar2 = Component::get_transform(param_1,0);
  uVar3 = LayerDefine::get_LAYER_UI(0);
  if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (cRam000000018ab7b020 == '\0') {
    sub_8032f6d0(0x187c38198,uVar3,0,param_4,unaff_RDI);
    cRam000000018ab7b020 = '\x01';
  }
  if (lVar2 != 0) {
    lVar5 = Component::get_gameObject(lVar2,0);
    if (lVar5 != 0) {
      GameObject::set_layer(lVar5,uVar3,0);
      if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
        sub_8032fa70();
      }
      if (cRam000000018ab7b021 == '\0') {
        sub_8032f6d0(0x187c38198,uVar3,0,param_4,unaff_RDI);
        cRam000000018ab7b021 = '\x01';
      }
      iVar7 = 0;
      if (lVar2 != 0) {
        while( true ) {
          iVar4 = Transform::get_childCount(lVar2,0);
          if (iVar4 <= iVar7) {
            return;
          }
          lVar5 = Transform::GetChild(lVar2,iVar7,0);
          if (lVar5 == 0) break;
          lVar6 = Component::get_gameObject(lVar5,0);
          if (lVar6 == 0) break;
          GameObject::set_layer(lVar6,uVar3);
          if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
            sub_8032fa70();
          }
          ExtensionMethods::recursivChildObjectLayer(lVar5);
          iVar7 = iVar7 + 1;
        }
      }
      sub_8032f9d0();
      pcVar1 = (code *)swi(3);
      (*pcVar1)();
      return;
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillEffectCtrl::SetPossitionAppearanceType  @ 0x1058330  size=1 ==== */

void SkillEffectCtrl::SetPossitionAppearanceType(longlong param_1,longlong param_2,longlong *param_3,longlong param_4,
                  longlong param_5,undefined4 param_6)

{
  float fVar1;
  int iVar2;
  ulonglong uVar3;
  char cVar4;
  longlong lVar5;
  undefined8 uVar6;
  undefined8 *puVar7;
  code *pcVar8;
  undefined8 uVar9;
  longlong *plVar10;
  float *pfVar11;
  int iVar12;
  longlong lVar13;
  longlong lVar14;
  undefined8 uVar15;
  float fVar16;
  float fVar17;
  float fVar18;
  float fVar19;
  ulonglong in_stack_ffffffffffffff38;
  undefined1 uVar20;
  undefined8 uStack_98;
  float fStack_90;
  undefined8 uStack_88;
  float fStack_80;
  undefined8 auStack_78 [10];
  
  if (cRam000000018ab606a1 == '\0') {
    sub_8032f6d0(0x187b91ca8);
    sub_8032f6d0(0x187b81118);
    sub_8032f6d0(0x187b812d0);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606a1 = '\x01';
  }
  *(longlong *)(param_1 + 0x58) = param_4;
  sub_8032e8d0(param_1 + 0x58,param_4);
  fVar19 = 9.259259;
  uVar6 = 0;
  fVar18 = 0.0;
  fVar17 = 0.0;
  if (param_2 == 0) goto code_r0x000181058d49;
  if (6 < *(uint *)(param_2 + 0x54)) goto code_r0x000181058688;
  if (param_3 == (longlong *)0x0) goto code_r0x000181058d49;
  *(longlong *)(param_1 + 0x90) = param_3[0xd];
  sub_8032e8d0(param_1 + 0x90);
  iVar12 = *(int *)(param_2 + 0x58);
  if (iVar12 == 0) {
    puVar7 = (undefined8 *)
             (**(code **)(*param_3 + 0x1a8))(&uStack_88,param_3,*(undefined8 *)(*param_3 + 0x1b0));
    uStack_98 = *puVar7;
    fVar18 = (float)uStack_98;
    fVar19 = (float)((ulonglong)uStack_98 >> 0x20);
code_r0x000181058683:
    fVar17 = *(float *)(puVar7 + 1);
  }
  else {
    if (iVar12 == 1) {
      uVar6 = (**(code **)(*param_3 + 0x1c8))(param_3,*(undefined8 *)(*param_3 + 0x1d0));
      pcVar8 = *(code **)(*param_3 + 0x1c8);
      uVar9 = *(undefined8 *)(*param_3 + 0x1d0);
code_r0x0001810585af:
      uVar9 = (*pcVar8)(param_3,uVar9);
      if (param_3[0xd] == 0) goto code_r0x000181058d49;
      uVar15 = *(undefined8 *)(param_3[0xd] + 0x1d8);
      puVar7 = (undefined8 *)BasePartsData::GetBottomLossyScale(&uStack_98,param_3,0);
      fStack_80 = *(float *)(puVar7 + 1);
      uStack_88 = *puVar7;
      puVar7 = (undefined8 *)
               BattleUnitBaseSpineController::BoneWorldToGlobalPosConsiderRotate(&uStack_98,uVar9,uVar15,&uStack_88,
                             in_stack_ffffffffffffff38 & 0xffffffffffffff00,0);
      uStack_88 = *puVar7;
      fVar18 = (float)uStack_88;
      fVar19 = (float)((ulonglong)uStack_88 >> 0x20);
      goto code_r0x000181058683;
    }
    if (iVar12 == 2) {
      uVar6 = (**(code **)(*param_3 + 0x1d8))(param_3,*(undefined8 *)(*param_3 + 0x1e0));
      pcVar8 = *(code **)(*param_3 + 0x1d8);
      uVar9 = *(undefined8 *)(*param_3 + 0x1e0);
      goto code_r0x0001810585af;
    }
    uVar6 = 0;
    if (iVar12 == 3) {
      puVar7 = (undefined8 *)
               (**(code **)(*param_3 + 0x1a8))(&uStack_88,param_3,*(undefined8 *)(*param_3 + 0x1b0))
      ;
      uVar9 = *puVar7;
      fVar17 = *(float *)(puVar7 + 1);
      puVar7 = (undefined8 *)
               (**(code **)(*param_3 + 0x1e8))
                         (&uStack_88,param_3,0,*(undefined8 *)(*param_3 + 0x1f0));
      uStack_98 = *puVar7;
      fVar17 = fVar17 + *(float *)(puVar7 + 1);
      fVar18 = (float)uVar9 + (float)uStack_98;
      fVar19 = (float)((ulonglong)uVar9 >> 0x20) + (float)((ulonglong)uStack_98 >> 0x20);
    }
    else if (iVar12 == 4) {
      if (((*(longlong *)(param_1 + 0x90) == 0) ||
          (lVar5 = UnitCtrl::GetCurrentSpineCtrl(*(longlong *)(param_1 + 0x90),0), lVar5 == 0)) ||
         (*(longlong *)(lVar5 + 0xa8) == 0)) goto code_r0x000181058d49;
      uVar6 = Skeleton::FindBone(*(longlong *)(lVar5 + 0xa8),*(undefined8 *)(param_2 + 0x70),0);
      if (*(int *)(param_2 + 100) != 3) {
        *(undefined4 *)(param_2 + 100) = 0;
      }
      *(undefined4 *)(param_2 + 0x68) = 0;
      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x000181058d49;
      uVar9 = *(undefined8 *)(*(longlong *)(param_1 + 0x90) + 0x1d8);
      lVar5 = Component::get_transform(lVar5,0);
      if (lVar5 == 0) goto code_r0x000181058d49;
      puVar7 = (undefined8 *)Transform::get_lossyScale(&uStack_98,lVar5,0);
      uStack_88 = *puVar7;
      fStack_80 = *(float *)(puVar7 + 1);
      puVar7 = (undefined8 *)
               BattleUnitBaseSpineController::BoneWorldToGlobalPosConsiderRotate(&uStack_98,uVar6,uVar9,&uStack_88,
                             in_stack_ffffffffffffff38 & 0xffffffffffffff00,0);
      uStack_88 = *puVar7;
      fVar18 = (float)uStack_88;
      fVar19 = (float)((ulonglong)uStack_88 >> 0x20);
      goto code_r0x000181058683;
    }
  }
code_r0x000181058688:
  iVar12 = 0;
  if (*(int *)(param_2 + 0x50) == 6) {
code_r0x0001810586d0:
    if ((*(longlong *)(param_1 + 0x20) == 0) ||
       (lVar5 = Component::get_transform(*(longlong *)(param_1 + 0x20),0), lVar5 == 0))
    goto code_r0x000181058d49;
    puVar7 = (undefined8 *)Transform::get_position(&uStack_88,lVar5,0);
    uStack_98 = *puVar7;
    fStack_90 = *(float *)(puVar7 + 1);
    if (param_3 == (longlong *)0x0) goto code_r0x000181058d49;
    puVar7 = (undefined8 *)
             (**(code **)(*param_3 + 0x1a8))(&uStack_88,param_3,*(undefined8 *)(*param_3 + 0x1b0));
    auStack_78[0] = *puVar7;
    fVar1 = *(float *)(puVar7 + 1);
    fVar16 = (float)auStack_78[0];
    uVar3 = (ulonglong)auStack_78[0] >> 0x20;
    puVar7 = (undefined8 *)
             (**(code **)(*param_3 + 0x1e8))(auStack_78,param_3,0,*(undefined8 *)(*param_3 + 0x1f0))
    ;
    fStack_80 = fStack_90 + ((fVar17 - fVar1) - *(float *)(puVar7 + 1));
    uStack_88 = CONCAT44(uStack_98._4_4_ +
                         ((fVar19 - (float)uVar3) - (float)((ulonglong)*puVar7 >> 0x20)),
                         (float)uStack_98 + ((fVar18 - fVar16) - (float)*puVar7));
    Transform::set_position(lVar5,&uStack_88,0);
    puVar7 = (undefined8 *)
             (**(code **)(*param_3 + 0x1a8))(auStack_78,param_3,*(undefined8 *)(*param_3 + 0x1b0));
    uVar9 = *puVar7;
    fVar17 = *(float *)(puVar7 + 1);
    puVar7 = (undefined8 *)
             (**(code **)(*param_3 + 0x1e8))(auStack_78,param_3,0,*(undefined8 *)(*param_3 + 0x1f0))
    ;
    fVar18 = (float)uVar9 + (float)*puVar7;
    fVar19 = (float)((ulonglong)*puVar7 >> 0x20) + (float)((ulonglong)uVar9 >> 0x20);
    fVar17 = *(float *)(puVar7 + 1) + fVar17;
    uStack_88 = uVar9;
  }
  else if ((*(int *)(param_2 + 0x50) == 1) && (*(int *)(param_2 + 0x58) != 0)) {
    uVar9 = *(undefined8 *)(param_1 + 0x20);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar4 = Object::op_Inequality(uVar9,0,0);
    if (cVar4 != '\0') goto code_r0x0001810586d0;
  }
  if ((*(int *)(param_2 + 0x50) == 0) && (*(int *)(param_2 + 0x54) == 6)) {
    fVar17 = -3.4028235e+38;
    fVar19 = 3.4028235e+38;
    while( true ) {
      lVar5 = *(longlong *)(param_2 + 0x38);
      if ((lVar5 == 0) || (lVar14 = *(longlong *)(lVar5 + 0x50), lVar14 == 0))
      goto code_r0x000181058d49;
      if ((*(int *)(lVar14 + 0x18) <= iVar12) ||
         (((*(int *)(lVar5 + 0x1c) <= iVar12 && (*(int *)(lVar5 + 0x1c) != 0)) && (iVar12 != 0))))
      break;
      plVar10 = (longlong *)sub_849ed2d0(lVar14,iVar12,uRam0000000187b812d0);
      if (plVar10 == (longlong *)0x0) goto code_r0x000181058d49;
      puVar7 = (undefined8 *)
               (**(code **)(*plVar10 + 0x178))(auStack_78,plVar10,*(undefined8 *)(*plVar10 + 0x180))
      ;
      uStack_88 = *puVar7;
      fStack_80 = *(float *)(puVar7 + 1);
      if (fVar17 <= (float)uStack_88) {
        fVar17 = (float)uStack_88;
      }
      pfVar11 = (float *)(**(code **)(*plVar10 + 0x178))
                                   (&uStack_98,plVar10,*(undefined8 *)(*plVar10 + 0x180));
      uStack_88 = *(undefined8 *)pfVar11;
      fStack_80 = pfVar11[2];
      if (*pfVar11 <= fVar19) {
        fVar19 = *pfVar11;
      }
      iVar12 = iVar12 + 1;
    }
    lVar5 = Component::get_transform(param_1,0);
    if (param_3 != (longlong *)0x0) {
      puVar7 = (undefined8 *)
               (**(code **)(*param_3 + 0x1a8))(auStack_78,param_3,*(undefined8 *)(*param_3 + 0x1b0))
      ;
      fStack_90 = 0.0;
      uStack_88 = *puVar7;
      fStack_80 = *(float *)(puVar7 + 1);
      uStack_98 = CONCAT44((int)((ulonglong)uStack_88 >> 0x20),(fVar19 + fVar17) * 0.5);
      if (lVar5 != 0) {
        fStack_80 = 0.0;
        uStack_88 = uStack_98;
        Transform::set_position(lVar5,&uStack_88,0);
        *(undefined4 *)(param_2 + 100) = 2;
        goto code_r0x000181058a2d;
      }
    }
    goto code_r0x000181058d49;
  }
  lVar5 = Component::get_transform(param_1,0);
  if (lVar5 == 0) goto code_r0x000181058d49;
  puVar7 = (undefined8 *)Transform::get_position(auStack_78,lVar5,0);
  fStack_80 = *(float *)(puVar7 + 1) + fVar17;
  uStack_88 = CONCAT44((float)((ulonglong)*puVar7 >> 0x20) + fVar19,(float)*puVar7 + fVar18);
  Transform::set_position(lVar5,&uStack_88,0);
code_r0x000181058a2d:
  if ((*(int *)(param_2 + 0x50) == 1) || (*(int *)(param_2 + 0x50) == 6)) {
    *(undefined4 *)(param_2 + 100) = 2;
  }
  lVar5 = Component::get_transform(param_1,0);
  if (lVar5 == 0) goto code_r0x000181058d49;
  puVar7 = (undefined8 *)Transform::get_position(auStack_78,lVar5,0);
  uStack_88 = *puVar7;
  fStack_80 = *(float *)(puVar7 + 1);
  if (param_3 == (longlong *)0x0) goto code_r0x000181058d49;
  puVar7 = (undefined8 *)
           (**(code **)(*param_3 + 0x1a8))(auStack_78,param_3,*(undefined8 *)(*param_3 + 0x1b0));
  fStack_90 = fStack_80 - *(float *)(puVar7 + 1);
  uStack_98 = CONCAT44(uStack_88._4_4_ - (float)((ulonglong)*puVar7 >> 0x20),
                       (float)uStack_88 - (float)*puVar7);
  iVar12 = *(int *)(param_2 + 100);
  if (iVar12 == 0) {
    iVar12 = *(int *)(param_2 + 0x68);
    if (iVar12 == 0) {
      lVar5 = sub_8104e4b0(param_1,0);
      fStack_80 = fStack_90;
      uVar20 = *(undefined1 *)(param_2 + 0x78);
      uVar9 = 1;
    }
    else {
      if (iVar12 != 1) {
        if (iVar12 != 2) {
          return;
        }
        lVar5 = sub_8104e4b0(param_1,0);
        uVar15 = 0;
        fStack_80 = fStack_90;
        uVar20 = 0;
        uVar9 = 1;
        goto code_r0x000181058cdc;
      }
      lVar5 = sub_8104e4b0(param_1,0);
code_r0x000181058cd4:
      uVar20 = 0;
      uVar9 = 0;
      fStack_80 = fStack_90;
    }
code_r0x000181058cd9:
    uVar15 = 1;
  }
  else {
    if (iVar12 != 1) {
      if (iVar12 == 2) {
        return;
      }
      if (iVar12 != 3) {
        return;
      }
      if ((param_5 != 0) && (param_4 != 0)) {
        iVar12 = *(int *)(param_5 + 0x78);
        iVar2 = *(int *)(param_4 + 0x4b8);
        uVar20 = *(undefined1 *)(param_2 + 0x78);
        if (cRam000000018ab606a3 == '\0') {
          sub_8032f6d0(0x187bc37e8);
          cRam000000018ab606a3 = '\x01';
        }
        lVar5 = sub_8032f970(uRam0000000187bc37e8);
        sub_80443e50(lVar5,0);
        *(longlong *)(lVar5 + 0x20) = param_1;
        *(undefined4 *)(lVar5 + 0x10) = 0;
        sub_8032e8d0(lVar5 + 0x20,param_1);
        *(longlong **)(lVar5 + 0x28) = param_3;
        sub_8032e8d0(lVar5 + 0x28,param_3);
        *(undefined1 *)(lVar5 + 0x30) = uVar20;
        if (iVar12 == iVar2) {
          lVar13 = 3;
          lVar14 = 0;
        }
        else {
          lVar14 = param_3[0xd];
          lVar13 = 0;
        }
        SkillEffectCtrl::AppendCoroutine(param_1,lVar5,lVar13,lVar14,0);
        return;
      }
      goto code_r0x000181058d49;
    }
    iVar12 = *(int *)(param_2 + 0x68);
    if (iVar12 == 0) {
      lVar5 = sub_8104e4b0(param_1,0);
      param_6 = 0;
      uVar20 = 0;
      uVar6 = 0;
      uVar9 = 1;
      fStack_80 = fStack_90;
      goto code_r0x000181058cd9;
    }
    if (iVar12 == 1) {
      lVar5 = sub_8104e4b0(param_1,0);
      param_6 = 0;
      uVar6 = 0;
      goto code_r0x000181058cd4;
    }
    if (iVar12 != 2) {
      return;
    }
    lVar5 = sub_8104e4b0(param_1,0);
    uVar15 = 0;
    param_6 = 0;
    uVar20 = 0;
    uVar6 = 0;
    uVar9 = 1;
    fStack_80 = fStack_90;
  }
code_r0x000181058cdc:
  uStack_88 = uStack_98;
  fStack_90 = fStack_80;
  uVar6 = SkillEffectCtrl::TrackTarget(param_1,param_3,&uStack_88,uVar15,uVar9,uVar6,uVar20,param_6,0);
  if (lVar5 != 0) {
    sub_8000d7b0(5,uRam0000000187b91ca8,lVar5,uVar6);
    return;
  }
code_r0x000181058d49:
  sub_8032f9d0();
  pcVar8 = (code *)swi(3);
  (*pcVar8)();
  return;
}



/* ==== SkillEffectCtrl::SetSortOrderBack  @ 0x1058d50  size=1 ==== */

void SkillEffectCtrl::SetSortOrderBack(longlong *param_1)

{
  longlong lVar1;
  code *pcVar2;
  longlong lVar3;
  char cVar4;
  int iVar5;
  undefined8 uStack_88;
  undefined8 uStack_80;
  undefined8 uStack_78;
  undefined8 uStack_70;
  undefined8 uStack_68;
  undefined8 uStack_60;
  undefined8 *puStack_58;
  undefined4 uStack_48;
  undefined4 uStack_44;
  int iStack_40;
  int iStack_3c;
  undefined8 uStack_38;
  undefined8 uStack_30;
  longlong lStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab606ac == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187bf5dd8);
    sub_8032f6d0(0x187b49e20);
    sub_8032f6d0(0x187b49fd8);
    sub_8032f6d0(0x187b4a188);
    sub_8032f6d0(0x187c13090);
    sub_8032f6d0(0x187c13240);
    cRam000000018ab606ac = '\x01';
  }
  *(undefined1 *)(param_1 + 0x19) = 0;
  SkillEffectCtrl::particleRendererDictionaryInitialize(param_1,0);
  if (param_1[0x18] == 0) {
    sub_8032f9d0();
  }
  else {
    sub_855337a0(&uStack_38,param_1[0x18],uRam0000000187bf5dd8);
    uStack_88 = uStack_38;
    uStack_80 = uStack_30;
    uStack_78 = lStack_28;
    uStack_70 = uStack_20;
    uStack_68 = uStack_18;
    uStack_60 = 0;
    puStack_58 = &uStack_88;
    while( true ) {
      while( true ) {
        while( true ) {
          while( true ) {
            while( true ) {
              cVar4 = sub_8576ca10(&uStack_88,uRam0000000187b49fd8);
              lVar3 = uStack_78;
              if (cVar4 == '\0') {
                sub_80443e50(&uStack_88,uRam0000000187b49e20);
                (**(code **)(*param_1 + 0x228))(param_1,*(undefined8 *)(*param_1 + 0x230));
                return;
              }
              uStack_48 = (undefined4)uStack_78;
              uStack_44 = uStack_78._4_4_;
              iStack_40 = (int)uStack_70;
              iStack_3c = uStack_70._4_4_;
              if (uStack_70._4_4_ == 0) break;
              lVar1 = param_1[0x12];
              if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
                sub_8032fa70();
              }
              iVar5 = BattleDefine::GetUnitSortOrder(lVar1,0);
              if (lVar3 == 0) goto code_r0x00018105902a;
              Renderer::set_sortingOrder(lVar3,iStack_3c + iVar5,0);
            }
            if (-0x3e9 < (int)uStack_70) break;
            if (uStack_78 == 0) goto code_r0x000181059025;
            Renderer::set_sortingOrder(uStack_78,0x2b2,0);
          }
          if (0 < (int)uStack_70) break;
          lVar1 = param_1[0x12];
          if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
            sub_8032fa70();
          }
          iVar5 = BattleDefine::GetUnitSortOrder(lVar1,0);
          if (lVar3 == 0) goto code_r0x000181059020;
          Renderer::set_sortingOrder(lVar3,iVar5 + -300,0);
        }
        if (999 < (int)uStack_70) break;
        lVar1 = param_1[0x12];
        if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
          sub_8032fa70();
        }
        iVar5 = BattleDefine::GetUnitSortOrder(lVar1,0);
        if (lVar3 == 0) goto code_r0x00018105901b;
        Renderer::set_sortingOrder(lVar3,iVar5 + 300,0);
      }
      if (uStack_78 == 0) break;
      Renderer::set_sortingOrder(uStack_78,11000,0);
    }
  }
  sub_8032f9d0();
code_r0x00018105901b:
  sub_8032f9d0();
code_r0x000181059020:
  sub_8032f9d0();
code_r0x000181059025:
  sub_8032f9d0();
code_r0x00018105902a:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::SetSortOrderForSummon  @ 0x1059040  size=1 ==== */

void SkillEffectCtrl::SetSortOrderForSummon(longlong param_1,int param_2)

{
  code *pcVar1;
  char cVar2;
  undefined8 uStack_60;
  undefined8 uStack_58;
  longlong lStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  longlong lStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab606b0 == '\0') {
    sub_8032f6d0(0x187bf5dd8);
    sub_8032f6d0(0x187b49e20);
    sub_8032f6d0(0x187b49fd8);
    sub_8032f6d0(0x187b4a188);
    sub_8032f6d0(0x187c13090);
    sub_8032f6d0(0x187c13240);
    cRam000000018ab606b0 = '\x01';
  }
  SkillEffectCtrl::particleRendererDictionaryInitialize(param_1,0);
  if (*(longlong *)(param_1 + 0xc0) == 0) {
    sub_8032f9d0();
    pcVar1 = (code *)swi(3);
    (*pcVar1)();
    return;
  }
  sub_855337a0(&uStack_38,*(longlong *)(param_1 + 0xc0),uRam0000000187bf5dd8);
  uStack_60 = uStack_38;
  uStack_58 = uStack_30;
  lStack_50 = lStack_28;
  uStack_48 = uStack_20;
  uStack_40 = uStack_18;
code_r0x000181059110:
  while( true ) {
    cVar2 = sub_8576ca10(&uStack_60,uRam0000000187b49fd8);
    if (cVar2 == '\0') {
      sub_80443e50(&uStack_60,uRam0000000187b49e20);
      return;
    }
    if (-0x3e9 < (int)uStack_48) break;
    if (lStack_50 == 0) goto code_r0x0001810591e6;
    Renderer::set_sortingOrder(lStack_50,0x2b2,0);
  }
  if ((int)uStack_48 < 1) {
    if (lStack_50 != 0) {
      Renderer::set_sortingOrder(lStack_50,param_2 + -300,0);
      goto code_r0x000181059110;
    }
  }
  else {
    if ((int)uStack_48 < 1000) {
      if (lStack_50 != 0) {
        Renderer::set_sortingOrder(lStack_50,param_2 + 300,0);
        goto code_r0x000181059110;
      }
    }
    else {
      if (lStack_50 != 0) {
        Renderer::set_sortingOrder(lStack_50,11000,0);
        goto code_r0x000181059110;
      }
      sub_8032f9d0();
    }
    sub_8032f9d0();
  }
  sub_8032f9d0();
code_r0x0001810591e6:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillEffectCtrl::SetSortOrderFront  @ 0x1059200  size=1 ==== */

void SkillEffectCtrl::SetSortOrderFront(longlong param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4)

{
  undefined8 uVar1;
  code *pcVar2;
  longlong lVar3;
  char cVar4;
  int iVar5;
  undefined8 uVar6;
  undefined8 *puVar7;
  undefined8 uStack_78;
  undefined8 uStack_70;
  longlong lStack_68;
  undefined8 uStack_60;
  undefined8 uStack_58;
  longlong lStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  longlong lStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab606ad == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187bf5dd8);
    sub_8032f6d0(0x187b49e20);
    sub_8032f6d0(0x187b49fd8);
    sub_8032f6d0(0x187b4a188);
    sub_8032f6d0(0x187c13090);
    sub_8032f6d0(0x187c13240);
    cRam000000018ab606ad = '\x01';
  }
  *(undefined1 *)(param_1 + 200) = 1;
  SkillEffectCtrl::particleRendererDictionaryInitialize(param_1,0);
  if (*(longlong *)(param_1 + 0xc0) == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  sub_855337a0(&uStack_38,*(longlong *)(param_1 + 0xc0),uRam0000000187bf5dd8);
  uStack_78 = uStack_38;
  uStack_70 = uStack_30;
  lStack_68 = lStack_28;
  uStack_60 = uStack_20;
  uStack_58 = uStack_18;
  uVar6 = 0;
  puVar7 = &uStack_78;
  while( true ) {
    cVar4 = sub_8576ca10(&uStack_78,uRam0000000187b49fd8);
    lVar3 = lStack_68;
    if (cVar4 == '\0') {
      sub_80443e50(&uStack_78,uRam0000000187b49e20);
      return;
    }
    lStack_48 = lStack_68;
    uStack_40 = uStack_60;
    if ((int)((ulonglong)uStack_60 >> 0x20) == 0) break;
    uVar1 = *(undefined8 *)(param_1 + 0x90);
    if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iVar5 = BattleDefine::GetUnitSortOrder(uVar1,0);
    if (lVar3 == 0) goto code_r0x0001810594cd;
    Renderer::set_sortingOrder(lVar3,(int)((ulonglong)uStack_40 >> 0x20) + iVar5,0,param_4,uVar6,puVar7);
code_r0x000181059448:
    if (lVar3 == 0) goto code_r0x0001810594c8;
    iVar5 = Renderer::get_sortingOrder(lVar3,0);
    Renderer::set_sortingOrder(lVar3,iVar5 + 0x2cec,0);
  }
  if ((int)uStack_60 < -1000) {
    if (lStack_68 == 0) goto code_r0x0001810594c3;
    Renderer::set_sortingOrder(lStack_68,0x2b2,0,param_4,uVar6,puVar7);
    goto code_r0x000181059448;
  }
  if (0 < (int)uStack_60) {
    if ((int)uStack_60 < 1000) {
      uVar1 = *(undefined8 *)(param_1 + 0x90);
      if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
        sub_8032fa70();
      }
      iVar5 = BattleDefine::GetUnitSortOrder(uVar1,0);
      if (lVar3 == 0) goto code_r0x0001810594b9;
      Renderer::set_sortingOrder(lVar3,iVar5 + 300,0,param_4,uVar6,puVar7);
    }
    else {
      if (lStack_68 == 0) {
        sub_8032f9d0();
code_r0x0001810594b9:
        sub_8032f9d0();
        goto code_r0x0001810594be;
      }
      Renderer::set_sortingOrder(lStack_68,11000,0,param_4,uVar6,puVar7);
    }
    goto code_r0x000181059448;
  }
  uVar1 = *(undefined8 *)(param_1 + 0x90);
  if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
    sub_8032fa70();
  }
  iVar5 = BattleDefine::GetUnitSortOrder(uVar1,0);
  if (lVar3 != 0) {
    Renderer::set_sortingOrder(lVar3,iVar5 + -300,0,param_4,uVar6,puVar7);
    goto code_r0x000181059448;
  }
code_r0x0001810594be:
  sub_8032f9d0();
code_r0x0001810594c3:
  sub_8032f9d0();
code_r0x0001810594c8:
  sub_8032f9d0();
code_r0x0001810594cd:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::SetSortOrderHit  @ 0x10594e0  size=1 ==== */

void SkillEffectCtrl::SetSortOrderHit(longlong param_1,int param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  longlong lVar3;
  char cVar4;
  int iVar5;
  undefined8 uStack_60;
  undefined8 uStack_58;
  longlong lStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  longlong lStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab606af == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187bf5dd8);
    sub_8032f6d0(0x187b49e20);
    sub_8032f6d0(0x187b49fd8);
    sub_8032f6d0(0x187b4a188);
    sub_8032f6d0(0x187c13090);
    sub_8032f6d0(0x187c13240);
    cRam000000018ab606af = '\x01';
  }
  SkillEffectCtrl::particleRendererDictionaryInitialize(param_1,0);
  if (*(longlong *)(param_1 + 0xc0) == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  sub_855337a0(&uStack_38,*(longlong *)(param_1 + 0xc0),uRam0000000187bf5dd8);
  uStack_60 = uStack_38;
  uStack_58 = uStack_30;
  lStack_50 = lStack_28;
  uStack_48 = uStack_20;
  uStack_40 = uStack_18;
code_r0x0001810595d0:
  while( true ) {
    cVar4 = sub_8576ca10(&uStack_60,uRam0000000187b49fd8);
    lVar3 = lStack_50;
    if (cVar4 == '\0') {
      sub_80443e50(&uStack_60,uRam0000000187b49e20);
      return;
    }
    if (-0x3e9 < (int)uStack_48) break;
    if (lStack_50 == 0) goto code_r0x0001810596fa;
    Renderer::set_sortingOrder(lStack_50,0x2b2,0);
  }
  if ((int)uStack_48 < 0) {
    uVar1 = *(undefined8 *)(param_1 + 0x90);
    if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iVar5 = BattleDefine::GetUnitSortOrder(uVar1,0);
    if (lVar3 != 0) {
      Renderer::set_sortingOrder(lVar3,iVar5 + -300,0);
      goto code_r0x0001810595d0;
    }
  }
  else {
    if ((int)uStack_48 < 1000) {
      if (lStack_50 != 0) {
        Renderer::set_sortingOrder(lStack_50,param_2 + 11000,0);
        goto code_r0x0001810595d0;
      }
    }
    else {
      if (lStack_50 != 0) {
        Renderer::set_sortingOrder(lStack_50,11000,0);
        goto code_r0x0001810595d0;
      }
      sub_8032f9d0();
    }
    sub_8032f9d0();
  }
  sub_8032f9d0();
code_r0x0001810596fa:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::SetSortOrderStatus  @ 0x1059710  size=1 ==== */

void SkillEffectCtrl::SetSortOrderStatus(longlong param_1,int param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  longlong lVar3;
  char cVar4;
  int iVar5;
  undefined8 uStack_60;
  undefined8 uStack_58;
  longlong lStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  longlong lStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab606ae == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187bf5dd8);
    sub_8032f6d0(0x187b49e20);
    sub_8032f6d0(0x187b49fd8);
    sub_8032f6d0(0x187b4a188);
    sub_8032f6d0(0x187c13090);
    sub_8032f6d0(0x187c13240);
    cRam000000018ab606ae = '\x01';
  }
  SkillEffectCtrl::particleRendererDictionaryInitialize(param_1,0);
  if (*(longlong *)(param_1 + 0xc0) == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  sub_855337a0(&uStack_38,*(longlong *)(param_1 + 0xc0),uRam0000000187bf5dd8);
  uStack_60 = uStack_38;
  uStack_58 = uStack_30;
  lStack_50 = lStack_28;
  uStack_48 = uStack_20;
  uStack_40 = uStack_18;
code_r0x000181059800:
  while( true ) {
    cVar4 = sub_8576ca10(&uStack_60,uRam0000000187b49fd8);
    lVar3 = lStack_50;
    if (cVar4 == '\0') {
      sub_80443e50(&uStack_60,uRam0000000187b49e20);
      return;
    }
    if (-0x3e9 < (int)uStack_48) break;
    if (lStack_50 == 0) goto code_r0x00018105995d;
    Renderer::set_sortingOrder(lStack_50,0x2b2,0);
  }
  if ((int)uStack_48 < 0) {
    uVar1 = *(undefined8 *)(param_1 + 0x90);
    if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iVar5 = BattleDefine::GetUnitSortOrder(uVar1,0);
    if (lVar3 != 0) {
      Renderer::set_sortingOrder(lVar3,iVar5 + -300,0);
      goto code_r0x000181059800;
    }
  }
  else {
    if ((int)uStack_48 < 1000) {
      uVar1 = *(undefined8 *)(param_1 + 0x90);
      if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
        sub_8032fa70();
      }
      iVar5 = BattleDefine::GetUnitSortOrder(uVar1,0);
      if (lVar3 != 0) {
        Renderer::set_sortingOrder(lVar3,param_2 + 0x15e + iVar5,0);
        goto code_r0x000181059800;
      }
    }
    else {
      if (lStack_50 != 0) {
        Renderer::set_sortingOrder(lStack_50,11000,0);
        goto code_r0x000181059800;
      }
      sub_8032f9d0();
    }
    sub_8032f9d0();
  }
  sub_8032f9d0();
code_r0x00018105995d:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::SetSource  @ 0x437840  size=13 ==== */

void SkillEffectCtrl::SetSource(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x58) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x58U >> 0xc);
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



/* ==== SkillEffectCtrl::SetStartTime  @ 0x1059970  size=1 ==== */

void SkillEffectCtrl::SetStartTime(longlong param_1,float param_2)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  undefined4 uVar4;
  longlong lVar5;
  undefined8 *puVar6;
  uint uVar7;
  float fVar8;
  undefined8 uStackX_20;
  undefined8 uVar9;
  undefined8 uStack_d8;
  undefined8 uStack_d0;
  longlong lStack_c8;
  undefined8 uStack_c0;
  undefined8 uStack_b8;
  undefined8 uStack_b0;
  undefined8 uStack_a8;
  undefined8 uStack_a0;
  undefined8 uStack_98;
  undefined4 uStack_90;
  longlong lStack_80;
  undefined8 uStack_78;
  undefined8 uStack_70;
  undefined8 uStack_68;
  longlong lStack_60;
  undefined8 uStack_58;
  undefined8 uStack_50;
  
  if (cRam000000018ab606b9 == '\0') {
    sub_8032f6d0(0x187bf2520);
    sub_8032f6d0(0x187b47f78);
    sub_8032f6d0(0x187b48130);
    sub_8032f6d0(0x187b482e0);
    sub_8032f6d0(0x187c124a0);
    sub_8032f6d0(0x187c12658);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606b9 = '\x01';
  }
  uStack_b0 = 0;
  uStack_a8 = 0;
  uStack_a0 = 0;
  uStack_98 = 0;
  uStack_90 = 0;
  uStack_d8 = 0;
  uStack_d0 = 0;
  lStack_c8 = 0;
  uStack_c0 = 0;
  uStack_b8 = 0;
  uVar7 = 0;
  uStackX_20 = 0;
  lVar1 = *(longlong *)(param_1 + 0x38);
  while (lVar1 != 0) {
    if (*(int *)(lVar1 + 0x18) <= (int)uVar7) {
      uVar9 = *(undefined8 *)(param_1 + 0x20);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar3 = Object::op_Inequality(uVar9,0,0);
      if (cVar3 == '\0') goto code_r0x000181059cfb;
      if (*(longlong *)(param_1 + 0xd8) != 0) {
        sub_855337a0(&uStack_70,*(longlong *)(param_1 + 0xd8),uRam0000000187bf2520);
        uStack_d8 = uStack_70;
        uStack_d0 = uStack_68;
        lStack_c8 = lStack_60;
        uStack_c0 = uStack_58;
        uStack_b8 = uStack_50;
        uVar9 = 0;
        puVar6 = &uStack_d8;
        goto code_r0x000181059be0;
      }
      break;
    }
    lVar1 = *(longlong *)(param_1 + 0x38);
    if (lVar1 == 0) break;
    if (*(uint *)(lVar1 + 0x18) <= uVar7) {
code_r0x000181059d2b:
      sub_8032f9c0();
      goto code_r0x000181059d31;
    }
    lVar1 = *(longlong *)(lVar1 + 0x20 + (longlong)(int)uVar7 * 8);
    if ((lVar1 == 0) || (lVar5 = Animator::GetCurrentAnimatorClipInfo(lVar1), lVar5 == 0)) break;
    if (*(longlong *)(lVar5 + 0x18) != 0) {
      if (*(int *)(lVar5 + 0x18) == 0) goto code_r0x000181059d2b;
      lVar5 = AnimatorClipInfo::get_clip(lVar5 + 0x20,0);
      puVar6 = (undefined8 *)Animator::GetCurrentAnimatorStateInfo(&uStack_70,lVar1,0,0);
      uStack_b0 = *puVar6;
      uStack_a8 = puVar6[1];
      uStack_a0 = puVar6[2];
      uStack_98 = puVar6[3];
      uStack_90 = *(undefined4 *)(puVar6 + 4);
      Behaviour::set_enabled(lVar1,1,0);
      uVar4 = sub_84c32ca0(&uStack_b0,0);
      if (lVar5 == 0) break;
      fVar8 = (float)AnimationClip::get_length(lVar5,0);
      fVar8 = param_2 / fVar8;
      if (1.0 < fVar8) {
        fVar8 = 1.0;
      }
      Animator::Play(lVar1,uVar4,0xffffffff,fVar8,0);
    }
    uVar7 = uVar7 + 1;
    lVar1 = *(longlong *)(param_1 + 0x38);
  }
  goto code_r0x000181059d25;
code_r0x000181059d31:
  sub_8032f9d0();
code_r0x000181059d37:
  sub_8032f9d0();
code_r0x000181059d3c:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
code_r0x000181059be0:
  cVar3 = sub_85772d60(&uStack_d8,uRam0000000187b48130);
  lVar1 = lStack_c8;
  if (cVar3 != '\0') {
    lStack_80 = lStack_c8;
    uStack_78 = uStack_c0;
    if (lStack_c8 == 0) goto code_r0x000181059d3c;
    uStackX_20 = sub_85c66220(lStack_c8,0);
    if (param_2 < (float)uStack_78) {
      MainModule::set_startDelayMultiplier(&uStackX_20,(float)uStack_78 - param_2,0);
      if (lVar1 == 0) goto code_r0x000181059d37;
      ParticleSystem::Simulate(lVar1,0,1,1,1,0,uVar9,puVar6);
    }
    else {
      MainModule::set_startDelayMultiplier(&uStackX_20,0,0);
      if (lVar1 == 0) goto code_r0x000181059d31;
      ParticleSystem::Simulate(lVar1);
    }
    goto code_r0x000181059be0;
  }
  sub_80443e50(&uStack_d8,uRam0000000187b47f78);
  if (*(longlong *)(param_1 + 0x20) != 0) {
    ParticleSystem::Play(*(longlong *)(param_1 + 0x20),1,0);
code_r0x000181059cfb:
    *(float *)(param_1 + 0x80) = param_2;
    return;
  }
code_r0x000181059d25:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::SetTimeToDie  @ 0x1059d50  size=7 ==== */

void SkillEffectCtrl::SetTimeToDie(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xb4) = param_2;
  return;
}



/* ==== SkillEffectCtrl::SetTimeToDieByStartHp  @ 0x443e50  size=3 ==== */

void SkillEffectCtrl::SetTimeToDieByStartHp(void)

{
  return;
}



/* ==== SkillEffectCtrl::StartUpdateDisappear  @ 0x443e50  size=3 ==== */

void SkillEffectCtrl::StartUpdateDisappear(void)

{
  return;
}



/* ==== SkillEffectCtrl::StaticInitSingletonTree  @ 0x1059d60  size=1 ==== */

ulonglong SkillEffectCtrl::StaticInitSingletonTree(void)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  if (cRam000000018ab60697 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab60697 = '\x01';
  }
  if (*(int *)(uRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(uRam0000000187b78198);
  }
  *(undefined8 *)(*(longlong *)(uRam0000000187b78198 + 0xb8) + 0x10) = 0;
  uVar3 = uRam0000000187b78198;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(*(longlong *)(uRam0000000187b78198 + 0xb8) + 0x10U >> 0xc);
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



/* ==== SkillEffectCtrl::StaticRelease  @ 0x1059dd0  size=1 ==== */

/* WARNING: Possible PIC construction at 0x000181059e32: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x000181059e37) */

ulonglong SkillEffectCtrl::StaticRelease(void)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  if (cRam000000018ab60698 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab60698 = '\x01';
  }
  if (*(int *)(uRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(uRam0000000187b78198);
  }
  *(undefined8 *)(*(longlong *)(uRam0000000187b78198 + 0xb8) + 0x10) = 0;
  uVar3 = uRam0000000187b78198;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(*(longlong *)(uRam0000000187b78198 + 0xb8) + 0x10U >> 0xc);
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



/* ==== SkillEffectCtrl::StopSeImmediately  @ 0x1059e70  size=1 ==== */

void SkillEffectCtrl::StopSeImmediately(longlong param_1)

{
  undefined8 uVar1;
  longlong lVar2;
  code *pcVar3;
  char cVar4;
  
  if (cRam000000018ab606b4 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606b4 = '\x01';
  }
  uVar1 = *(undefined8 *)(param_1 + 0x48);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar4 = Object::op_Equality(uVar1,0,0);
  if (cVar4 != '\0') {
    return;
  }
  lVar2 = *(longlong *)(param_1 + 0x48);
  if (lVar2 != 0) {
    if (*(longlong *)(lVar2 + 0x30) == 0) {
      CriAtomSourceBase::Stop(lVar2,0);
      return;
    }
    lVar2 = *(longlong *)(lVar2 + 0x30);
    if (lVar2 != 0) {
      if (cRam000000018ab7958f == '\0') {
        sub_8032f6d0(0x187bb9e08);
        cRam000000018ab7958f = '\x01';
      }
      if (*(longlong *)(lVar2 + 0x48) != 0) {
        uVar1 = *(undefined8 *)(lVar2 + 0x48);
        if (*(int *)(lRam0000000187bb9e08 + 0xe4) == 0) {
          sub_8032fa70();
        }
        CriAtomExPlayer::criAtomExPlayer_StopWithoutReleaseTime(uVar1,0);
        CriAtomExPlayer::CRIWARE87C8F275(*(undefined8 *)(lVar2 + 0x38),0);
      }
      return;
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== SkillEffectCtrl::TrackTarget  @ 0x105a040  size=1 ==== */

longlong SkillEffectCtrl::TrackTarget(undefined8 param_1,undefined8 param_2,undefined8 *param_3,undefined1 param_4,
                      undefined1 param_5,undefined8 param_6,undefined1 param_7,undefined4 param_8)

{
  undefined4 uVar1;
  longlong lVar2;
  
  if (cRam000000018ab606a2 == '\0') {
    sub_8032f6d0(0x187bc2f68);
    cRam000000018ab606a2 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187bc2f68);
  sub_80443e50(lVar2,0);
  *(undefined4 *)(lVar2 + 0x10) = 0;
  *(undefined8 *)(lVar2 + 0x20) = param_1;
  sub_8032e8d0(lVar2 + 0x20,param_1);
  *(undefined8 *)(lVar2 + 0x28) = param_2;
  sub_8032e8d0(lVar2 + 0x28,param_2);
  uVar1 = *(undefined4 *)(param_3 + 1);
  *(undefined8 *)(lVar2 + 0x40) = *param_3;
  *(undefined4 *)(lVar2 + 0x48) = uVar1;
  *(undefined1 *)(lVar2 + 0x3d) = param_5;
  *(undefined1 *)(lVar2 + 0x3c) = param_4;
  *(undefined8 *)(lVar2 + 0x30) = param_6;
  sub_8032e8d0(lVar2 + 0x30);
  *(undefined1 *)(lVar2 + 0x4c) = param_7;
  *(undefined4 *)(lVar2 + 0x38) = param_8;
  return lVar2;
}



/* ==== SkillEffectCtrl::TrackTarget  @ 0x105a120  size=1 ==== */

longlong SkillEffectCtrl::TrackTarget(undefined8 param_1,undefined8 param_2,undefined8 *param_3,undefined1 param_4,
                      undefined1 param_5,undefined8 param_6,undefined1 param_7,undefined8 param_8,
                      undefined4 param_9,undefined1 param_10)

{
  undefined4 uVar1;
  longlong lVar2;
  
  if (cRam000000018ab606a4 == '\0') {
    sub_8032f6d0(0x187bc3118);
    cRam000000018ab606a4 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187bc3118);
  sub_80443e50(lVar2,0);
  *(undefined4 *)(lVar2 + 0x10) = 0;
  *(undefined8 *)(lVar2 + 0x20) = param_1;
  sub_8032e8d0(lVar2 + 0x20,param_1);
  *(undefined8 *)(lVar2 + 0x28) = param_2;
  sub_8032e8d0(lVar2 + 0x28,param_2);
  uVar1 = *(undefined4 *)(param_3 + 1);
  *(undefined8 *)(lVar2 + 0x40) = *param_3;
  *(undefined4 *)(lVar2 + 0x48) = uVar1;
  *(undefined1 *)(lVar2 + 0x31) = param_5;
  *(undefined1 *)(lVar2 + 0x30) = param_4;
  *(undefined8 *)(lVar2 + 0x38) = param_6;
  sub_8032e8d0(lVar2 + 0x38);
  *(undefined1 *)(lVar2 + 0x4d) = param_7;
  *(undefined8 *)(lVar2 + 0x50) = param_8;
  sub_8032e8d0(lVar2 + 0x50);
  *(undefined1 *)(lVar2 + 0x4c) = param_10;
  *(undefined4 *)(lVar2 + 0x58) = param_9;
  return lVar2;
}



/* ==== SkillEffectCtrl::TrackTargetSort  @ 0x1059fa0  size=1 ==== */

longlong SkillEffectCtrl::TrackTargetSort(undefined8 param_1,undefined8 param_2,undefined1 param_3)

{
  longlong lVar1;
  
  if (cRam000000018ab606a5 == '\0') {
    sub_8032f6d0(0x187bc32c8);
    cRam000000018ab606a5 = '\x01';
  }
  lVar1 = sub_8032f970(uRam0000000187bc32c8);
  sub_80443e50(lVar1,0);
  *(undefined4 *)(lVar1 + 0x10) = 0;
  *(undefined8 *)(lVar1 + 0x28) = param_1;
  sub_8032e8d0(lVar1 + 0x28,param_1);
  *(undefined8 *)(lVar1 + 0x20) = param_2;
  sub_8032e8d0(lVar1 + 0x20,param_2);
  *(undefined1 *)(lVar1 + 0x30) = param_3;
  return lVar1;
}



/* ==== SkillEffectCtrl::TrackTargetSortForSummon  @ 0x1059f10  size=1 ==== */

longlong SkillEffectCtrl::TrackTargetSortForSummon(undefined8 param_1,undefined8 param_2)

{
  longlong lVar1;
  
  if (cRam000000018ab606b8 == '\0') {
    sub_8032f6d0(0x187bc3480);
    cRam000000018ab606b8 = '\x01';
  }
  lVar1 = sub_8032f970(uRam0000000187bc3480);
  sub_80443e50(lVar1,0);
  *(undefined4 *)(lVar1 + 0x10) = 0;
  *(undefined8 *)(lVar1 + 0x28) = param_1;
  sub_8032e8d0(lVar1 + 0x28,param_1);
  *(undefined8 *)(lVar1 + 0x20) = param_2;
  sub_8032e8d0(lVar1 + 0x20,param_2);
  return lVar1;
}



/* ==== SkillEffectCtrl::UpdateTimerRepeat  @ 0x105a220  size=1 ==== */

longlong SkillEffectCtrl::UpdateTimerRepeat(undefined8 param_1)

{
  longlong lVar1;
  
  if (cRam000000018ab606a7 == '\0') {
    sub_8032f6d0(0x187bc3638);
    cRam000000018ab606a7 = '\x01';
  }
  lVar1 = sub_8032f970(uRam0000000187bc3638);
  sub_80443e50(lVar1,0);
  *(undefined4 *)(lVar1 + 0x10) = 0;
  *(undefined8 *)(lVar1 + 0x20) = param_1;
  sub_8032e8d0(lVar1 + 0x20,param_1);
  return lVar1;
}



/* ==== SkillEffectCtrl::_Update  @ 0x105a290  size=1 ==== */

bool SkillEffectCtrl::_Update(longlong param_1)

{
  code *pcVar1;
  undefined1 uVar2;
  longlong lVar3;
  
  if (*(char *)(param_1 + 0xb4) == '\0') {
code_r0x00018105a2d5:
    return *(char *)(param_1 + 0xb4) == '\0';
  }
  if (*(longlong *)(param_1 + 0x48) != 0) {
    CriAtomSourceBase::Stop(*(longlong *)(param_1 + 0x48),0);
    *(undefined1 *)(param_1 + 0xa0) = 0;
    lVar3 = Component::get_gameObject(param_1,0);
    if (lVar3 != 0) {
      GameObject::SetActive(lVar3,0,0);
      goto code_r0x00018105a2d5;
    }
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar2 = (*pcVar1)();
  return (bool)uVar2;
}



/* ==== SkillEffectCtrl::awakeImpl  @ 0x105a580  size=1 ==== */

/* WARNING: Possible PIC construction at 0x00018105a63c: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x00018105a641) */
/* WARNING: Removing unreachable block (ram,0x00018105a69b) */
/* WARNING: Removing unreachable block (ram,0x00018105a658) */

ulonglong SkillEffectCtrl::awakeImpl(undefined8 param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  if (cRam000000018ab60699 == '\0') {
    sub_8032f6d0(0x187bf9e18);
    sub_8032f6d0(0x187bf05b0);
    sub_8032f6d0(0x187b78198);
    cRam000000018ab60699 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  if (*(longlong *)(*(ulonglong *)(lRam0000000187b78198 + 0xb8) + 0x10) != 0) {
    return *(ulonglong *)(lRam0000000187b78198 + 0xb8);
  }
  uVar2 = sub_847c7d00(param_1,uRam0000000187bf9e18);
  uVar3 = uVar2;
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    uVar3 = sub_8032fa70();
  }
  *(ulonglong *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x10) = uVar2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x10U >> 0xc);
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



/* ==== SkillEffectCtrl::get_BaseScale  @ 0x97b790  size=25 ==== */

undefined8 * SkillEffectCtrl::get_BaseScale(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0xb0);
  *param_1 = *(undefined8 *)(param_2 + 0xa8);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== SkillEffectCtrl::get_IsAura  @ 0x5e9a30  size=5 ==== */

undefined1 SkillEffectCtrl::get_IsAura(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x40);
}



/* ==== SkillEffectCtrl::get_IsCommon  @ 0x8ebdf0  size=5 ==== */

undefined1 SkillEffectCtrl::get_IsCommon(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x60);
}



/* ==== SkillEffectCtrl::get_IsFront  @ 0xb152a0  size=8 ==== */

undefined1 SkillEffectCtrl::get_IsFront(longlong param_1)

{
  return *(undefined1 *)(param_1 + 200);
}



/* ==== SkillEffectCtrl::get_IsPlaying  @ 0xe69180  size=8 ==== */

undefined1 SkillEffectCtrl::get_IsPlaying(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0xa0);
}



/* ==== SkillEffectCtrl::get_IsRepeat  @ 0x52c760  size=5 ==== */

undefined1 SkillEffectCtrl::get_IsRepeat(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x50);
}



/* ==== SkillEffectCtrl::get_OnEffectEnd  @ 0x438c20  size=8 ==== */

undefined8 SkillEffectCtrl::get_OnEffectEnd(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x88);
}



/* ==== SkillEffectCtrl::get_SeSource  @ 0x437720  size=5 ==== */

undefined8 SkillEffectCtrl::get_SeSource(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x48);
}



/* ==== SkillEffectCtrl::get_SortTarget  @ 0x438ab0  size=8 ==== */

undefined8 SkillEffectCtrl::get_SortTarget(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x90);
}



/* ==== SkillEffectCtrl::get_Target  @ 0x438c10  size=8 ==== */

undefined8 SkillEffectCtrl::get_Target(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x98);
}



/* ==== SkillEffectCtrl::get_battleManager  @ 0x105a6b0  size=1 ==== */

undefined8 SkillEffectCtrl::get_battleManager(void)

{
  if (cRam000000018ab60696 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab60696 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x18);
}



/* ==== SkillEffectCtrl::get_particleStartDelayDictionary  @ 0x5d3fb0  size=8 ==== */

undefined8 SkillEffectCtrl::get_particleStartDelayDictionary(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xd8);
}



/* ==== SkillEffectCtrl::get_particles  @ 0x597ca0  size=8 ==== */

undefined8 SkillEffectCtrl::get_particles(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xd0);
}



/* ==== SkillEffectCtrl::get_resumeTime  @ 0x105a700  size=9 ==== */

undefined4 SkillEffectCtrl::get_resumeTime(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0xe0);
}



/* ==== SkillEffectCtrl::get_singletonTree  @ 0x105a710  size=1 ==== */

undefined8 SkillEffectCtrl::get_singletonTree(void)

{
  if (cRam000000018ab60695 == '\0') {
    sub_8032f6d0(0x187b78198);
    cRam000000018ab60695 = '\x01';
  }
  if (*(int *)(lRam0000000187b78198 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b78198);
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187b78198 + 0xb8) + 0x10);
}



/* ==== SkillEffectCtrl::get_trackTargetCoroutinueId  @ 0x5e9b10  size=7 ==== */

undefined4 SkillEffectCtrl::get_trackTargetCoroutinueId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0xa4);
}



/* ==== SkillEffectCtrl::initTweener  @ 0x105a760  size=1 ==== */

void SkillEffectCtrl::initTweener(undefined8 param_1,longlong *param_2)

{
  code *pcVar1;
  float fVar2;
  float fVar3;
  
  if (param_2 == (longlong *)0x0) {
    sub_8032f9d0();
    pcVar1 = (code *)swi(3);
    (*pcVar1)();
    return;
  }
  UITweener::ResetToBeginning(param_2,0);
  if (*(float *)(param_2 + 0xe) != *(float *)(param_2 + 8)) {
    *(int *)(param_2 + 0xe) = (int)param_2[8];
    fVar3 = 1.0;
    if (*(float *)(param_2 + 8) <= 0.0) {
      fVar2 = 1000.0;
    }
    else {
      fVar2 = 1.0 / *(float *)(param_2 + 8);
    }
    if (*(float *)((longlong)param_2 + 0x74) < 0.0) {
      fVar3 = -1.0;
    }
    *(float *)((longlong)param_2 + 0x74) = fVar3 * ABS(fVar2);
  }
  *(uint *)((longlong)param_2 + 0x74) = *(uint *)((longlong)param_2 + 0x74) & 0x7fffffff;
  Behaviour::set_enabled(param_2,1,0);
                    /* WARNING: Could not recover jumptable at 0x0001826821b1. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  (**(code **)(*param_2 + 0x188))(param_2,*(undefined8 *)(*param_2 + 400));
  return;
}



/* ==== SkillEffectCtrl::onDestroy  @ 0x443e50  size=3 ==== */

void SkillEffectCtrl::onDestroy(void)

{
  return;
}



/* ==== SkillEffectCtrl::particleRendererDictionaryInitialize  @ 0x105a790  size=1 ==== */

void SkillEffectCtrl::particleRendererDictionaryInitialize(longlong param_1)

{
  code *pcVar1;
  char cVar2;
  int iVar3;
  uint uVar4;
  undefined8 uVar5;
  longlong lVar6;
  longlong lVar7;
  uint uVar8;
  undefined4 uVar9;
  undefined8 uStackX_8;
  undefined8 auStackX_18 [2];
  
  if (cRam000000018ab606a0 == '\0') {
    sub_8032f6d0(0x187bfabd0);
    sub_8032f6d0(0x187bfad88);
    sub_8032f6d0(0x187bff3f8);
    sub_8032f6d0(0x187bf5c20);
    sub_8032f6d0(0x187bf2368);
    sub_8032f6d0(0x187bf21b0);
    sub_8032f6d0(0x187bf5a68);
    sub_8032f6d0(0x187b35940);
    sub_8032f6d0(0x187b36528);
    sub_8032f6d0(0x187bce608);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606a0 = '\x01';
  }
  uVar8 = 0;
  auStackX_18[0] = 0;
  if (*(longlong *)(param_1 + 0xc0) != 0) {
    return;
  }
  uVar5 = sub_8032f970(uRam0000000187b36528);
  sub_85660990(uVar5,uRam0000000187bf5a68);
  *(undefined8 *)(param_1 + 0xc0) = uVar5;
  sub_8032e8d0(param_1 + 0xc0,uVar5);
  lVar6 = Component::get_gameObject(param_1,0);
  if (lVar6 != 0) {
    uVar5 = sub_847c75c0(lVar6,1,uRam0000000187bce608);
    *(undefined8 *)(param_1 + 0xd0) = uVar5;
    sub_8032e8d0(param_1 + 0xd0,uVar5);
    uVar5 = sub_8032f970(uRam0000000187b35940);
    sub_85660990(uVar5,uRam0000000187bf21b0);
    *(undefined8 *)(param_1 + 0xd8) = uVar5;
    sub_8032e8d0(param_1 + 0xd8,uVar5);
    lVar6 = sub_8475aaf0(param_1,uRam0000000187bfabd0);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar2 = Object::op_Inequality(lVar6,0,0);
    if (cVar2 != '\0') {
      if (lVar6 == 0) goto DAT_18105ab0f;
      *(undefined8 *)(param_1 + 0x78) = *(undefined8 *)(lVar6 + 0x20);
      sub_8032e8d0(param_1 + 0x78);
    }
    lVar6 = *(longlong *)(param_1 + 0xd0);
    if (lVar6 != 0) {
      while( true ) {
        if (*(int *)(lVar6 + 0x18) <= (int)uVar8) {
          return;
        }
        lVar6 = *(longlong *)(param_1 + 0xd0);
        if (lVar6 == 0) break;
        if (*(uint *)(lVar6 + 0x18) <= uVar8) {
code_r0x00018105ab15:
          sub_8032f9c0();
          pcVar1 = (code *)swi(3);
          (*pcVar1)();
          return;
        }
        lVar6 = *(longlong *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        if ((lVar6 == 0) || (lVar6 = sub_8475aaf0(lVar6,uRam0000000187bff3f8), lVar6 == 0)) break;
        iVar3 = Renderer::get_sortingOrder(lVar6,0);
        if (5000 < iVar3) {
          iVar3 = 5000;
        }
        Renderer::set_sortingOrder(lVar6,iVar3,0);
        uVar4 = Renderer::get_sortingOrder(lVar6,0);
        lVar7 = *(longlong *)(param_1 + 0xd0);
        uStackX_8 = (ulonglong)uVar4;
        if (lVar7 == 0) break;
        if (*(uint *)(lVar7 + 0x18) <= uVar8) goto code_r0x00018105ab15;
        lVar7 = *(longlong *)(lVar7 + 0x20 + (longlong)(int)uVar8 * 8);
        if (lVar7 == 0) break;
        lVar7 = sub_8475aaf0(lVar7,uRam0000000187bfad88);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        cVar2 = Object::op_Inequality(lVar7,0,0);
        if (cVar2 != '\0') {
          if (lVar7 == 0) break;
          uStackX_8 = CONCAT44(*(undefined4 *)(lVar7 + 0x20),uVar4);
        }
        if (*(longlong *)(param_1 + 0xc0) == 0) break;
        sub_856a40c0(*(longlong *)(param_1 + 0xc0),lVar6,uStackX_8,uRam0000000187bf5c20);
        lVar6 = *(longlong *)(param_1 + 0xd0);
        lVar7 = *(longlong *)(param_1 + 0xd8);
        if (lVar6 == 0) break;
        if (*(uint *)(lVar6 + 0x18) <= uVar8) goto code_r0x00018105ab15;
        uVar5 = *(undefined8 *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        lVar6 = *(longlong *)(lVar6 + 0x20 + (longlong)(int)uVar8 * 8);
        if (lVar6 == 0) break;
        auStackX_18[0] = sub_85c66220(lVar6,0);
        uVar9 = MainModule::get_startDelayMultiplier(auStackX_18,0);
        if (lVar7 == 0) break;
        sub_85686120(lVar7,uVar5,uVar9,uRam0000000187bf2368);
        lVar6 = *(longlong *)(param_1 + 0xd0);
        uVar8 = uVar8 + 1;
        if (lVar6 == 0) break;
      }
    }
  }
DAT_18105ab0f:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== SkillEffectCtrl::pauseSe  @ 0x105ab20  size=1 ==== */

void SkillEffectCtrl::pauseSe(longlong param_1,undefined1 param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  char cVar3;
  
  if (cRam000000018ab606b6 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606b6 = '\x01';
  }
  uVar1 = *(undefined8 *)(param_1 + 0x48);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = Object::op_Equality(uVar1,0,0);
  if (cVar3 == '\0') {
    if (*(longlong *)(param_1 + 0x48) == 0) {
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
    CriAtomSourceBase::Pause(*(longlong *)(param_1 + 0x48),param_2,0);
  }
  return;
}



/* ==== SkillEffectCtrl::resetStartDelay  @ 0x105abb0  size=1 ==== */

void SkillEffectCtrl::resetStartDelay(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  uint uVar3;
  float fVar4;
  undefined8 uStackX_8;
  
  if (cRam000000018ab606aa == '\0') {
    sub_8032f6d0(0x187bf26d8);
    cRam000000018ab606aa = '\x01';
  }
  lVar1 = *(longlong *)(param_1 + 0xd0);
  uVar3 = 0;
  uStackX_8 = 0;
  while (lVar1 != 0) {
    if (*(int *)(lVar1 + 0x18) <= (int)uVar3) {
      return;
    }
    lVar1 = *(longlong *)(param_1 + 0xd0);
    if (lVar1 == 0) break;
    if (*(uint *)(lVar1 + 0x18) <= uVar3) {
code_r0x00018105acbc:
      sub_8032f9c0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
    lVar1 = *(longlong *)(lVar1 + 0x20 + (longlong)(int)uVar3 * 8);
    if (lVar1 == 0) break;
    uStackX_8 = sub_85c66220(lVar1,0);
    if (*(longlong *)(param_1 + 0xd8) == 0) {
      fVar4 = (float)MainModule::get_startDelayMultiplier(&uStackX_8);
    }
    else {
      lVar1 = *(longlong *)(param_1 + 0xd0);
      if (lVar1 == 0) break;
      if (*(uint *)(lVar1 + 0x18) <= uVar3) goto code_r0x00018105acbc;
      fVar4 = (float)sub_85694c10(*(undefined8 *)(param_1 + 0xd8),
                                   *(undefined8 *)(lVar1 + 0x20 + (longlong)(int)uVar3 * 8),
                                   uRam0000000187bf26d8);
    }
    if (*(float *)(param_1 + 0xe0) <= fVar4 && fVar4 != *(float *)(param_1 + 0xe0)) {
      MainModule::set_startDelayMultiplier(&uStackX_8,fVar4 - *(float *)(param_1 + 0xe0),0);
    }
    uVar3 = uVar3 + 1;
    lVar1 = *(longlong *)(param_1 + 0xd0);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::setBaseScale  @ 0x105acd0  size=1 ==== */

void SkillEffectCtrl::setBaseScale(longlong param_1)

{
  undefined4 uVar1;
  code *pcVar2;
  longlong lVar3;
  undefined8 *puVar4;
  undefined1 auStack_18 [16];
  
  lVar3 = Component::get_transform(param_1,0);
  if (lVar3 != 0) {
    puVar4 = (undefined8 *)Transform::get_localScale(auStack_18,lVar3,0);
    uVar1 = *(undefined4 *)(puVar4 + 1);
    *(undefined8 *)(param_1 + 0xa8) = *puVar4;
    *(undefined4 *)(param_1 + 0xb0) = uVar1;
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::setEnableAnimations  @ 0x105ad20  size=1 ==== */

void SkillEffectCtrl::setEnableAnimations(longlong param_1)

{
  undefined8 uVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  uint uVar5;
  uint uVar6;
  
  if (cRam000000018ab606a9 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab606a9 = '\x01';
  }
  uVar6 = 0;
  uVar5 = 0;
  lVar4 = *(longlong *)(param_1 + 0x28);
  while (lVar4 != 0) {
    if (*(int *)(lVar4 + 0x18) <= (int)uVar5) {
      lVar4 = *(longlong *)(param_1 + 0x30);
      uVar5 = 0;
      if (lVar4 != 0) goto code_r0x00018105ae16;
      break;
    }
    lVar4 = *(longlong *)(param_1 + 0x28);
    if (lVar4 == 0) break;
    if (*(uint *)(lVar4 + 0x18) <= uVar5) goto code_r0x00018105af60;
    uVar1 = *(undefined8 *)(lVar4 + 0x20 + (longlong)(int)uVar5 * 8);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar3 = Object::op_Inequality(uVar1,0,0);
    if (cVar3 != '\0') {
      lVar4 = *(longlong *)(param_1 + 0x28);
      if (lVar4 == 0) break;
      if (*(uint *)(lVar4 + 0x18) <= uVar5) goto code_r0x00018105af60;
      if (*(longlong *)(lVar4 + 0x20 + (longlong)(int)uVar5 * 8) == 0) break;
      Behaviour::set_enabled();
    }
    uVar5 = uVar5 + 1;
    lVar4 = *(longlong *)(param_1 + 0x28);
  }
  goto code_r0x00018105af5a;
  while( true ) {
    lVar4 = *(longlong *)(param_1 + 0x30);
    if (lVar4 == 0) break;
    if (*(uint *)(lVar4 + 0x18) <= uVar5) goto code_r0x00018105af60;
    uVar1 = *(undefined8 *)(lVar4 + 0x20 + (longlong)(int)uVar5 * 8);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar3 = Object::op_Inequality(uVar1,0,0);
    if (cVar3 != '\0') {
      lVar4 = *(longlong *)(param_1 + 0x30);
      if (lVar4 == 0) break;
      if (*(uint *)(lVar4 + 0x18) <= uVar5) goto code_r0x00018105af60;
      if (*(longlong *)(lVar4 + 0x20 + (longlong)(int)uVar5 * 8) == 0) break;
      Behaviour::set_enabled();
    }
    lVar4 = *(longlong *)(param_1 + 0x30);
    uVar5 = uVar5 + 1;
    if (lVar4 == 0) break;
code_r0x00018105ae16:
    if (*(int *)(lVar4 + 0x18) <= (int)uVar5) {
      lVar4 = *(longlong *)(param_1 + 0x38);
      if (lVar4 != 0) goto code_r0x00018105aec0;
      break;
    }
  }
  goto code_r0x00018105af5a;
  while( true ) {
    lVar4 = *(longlong *)(param_1 + 0x38);
    uVar6 = uVar6 + 1;
    if (lVar4 == 0) break;
code_r0x00018105aec0:
    if (*(int *)(lVar4 + 0x18) <= (int)uVar6) {
      return;
    }
    lVar4 = *(longlong *)(param_1 + 0x38);
    if (lVar4 == 0) break;
    if (*(uint *)(lVar4 + 0x18) <= uVar6) goto code_r0x00018105af60;
    uVar1 = *(undefined8 *)(lVar4 + 0x20 + (longlong)(int)uVar6 * 8);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar3 = Object::op_Inequality(uVar1,0,0);
    if (cVar3 != '\0') {
      lVar4 = *(longlong *)(param_1 + 0x38);
      if (lVar4 == 0) break;
      if (*(uint *)(lVar4 + 0x18) <= uVar6) {
code_r0x00018105af60:
        sub_8032f9c0();
        pcVar2 = (code *)swi(3);
        (*pcVar2)();
        return;
      }
      if (*(longlong *)(lVar4 + 0x20 + (longlong)(int)uVar6 * 8) == 0) break;
      Behaviour::set_enabled();
    }
  }
code_r0x00018105af5a:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== SkillEffectCtrl::setLayerName  @ 0x105af70  size=1 ==== */

void SkillEffectCtrl::setLayerName(longlong param_1,undefined8 param_2)

{
  code *pcVar1;
  char cVar2;
  undefined8 uStack_60;
  undefined8 uStack_58;
  longlong lStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined8 uStack_30;
  longlong lStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab6069b == '\0') {
    sub_8032f6d0(0x187bf5dd8);
    sub_8032f6d0(0x187b49e20);
    sub_8032f6d0(0x187b49fd8);
    sub_8032f6d0(0x187b4a188);
    sub_8032f6d0(0x187c13090);
    cRam000000018ab6069b = '\x01';
  }
  if (*(longlong *)(param_1 + 0xc0) != 0) {
    sub_855337a0(&uStack_38,*(undefined8 *)(param_1 + 0xc0),uRam0000000187bf5dd8);
    uStack_60 = uStack_38;
    uStack_58 = uStack_30;
    lStack_50 = lStack_28;
    uStack_48 = uStack_20;
    uStack_40 = uStack_18;
    while( true ) {
      cVar2 = sub_8576ca10(&uStack_60,uRam0000000187b49fd8);
      if (cVar2 == '\0') break;
      if (lStack_50 == 0) {
        sub_8032f9d0();
        sub_8032b6b0();
        pcVar1 = (code *)swi(3);
        (*pcVar1)();
        return;
      }
      Renderer::set_sortingLayerName(lStack_50,param_2,0);
    }
    sub_80443e50(&uStack_60,uRam0000000187b49e20);
  }
  return;
}



/* ==== SkillEffectCtrl::setSortOrderBackPostProcess  @ 0x443e50  size=3 ==== */

void SkillEffectCtrl::setSortOrderBackPostProcess(void)

{
  return;
}



/* ==== SkillEffectCtrl::set_BaseScale  @ 0x105b0b0  size=22 ==== */

void SkillEffectCtrl::set_BaseScale(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0xa8) = *param_2;
  *(undefined4 *)(param_1 + 0xb0) = uVar1;
  return;
}



/* ==== SkillEffectCtrl::set_IsAura  @ 0x5e9ba0  size=4 ==== */

void SkillEffectCtrl::set_IsAura(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x40) = param_2;
  return;
}



/* ==== SkillEffectCtrl::set_IsCommon  @ 0x7cc290  size=4 ==== */

void SkillEffectCtrl::set_IsCommon(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x60) = param_2;
  return;
}



/* ==== SkillEffectCtrl::set_IsFront  @ 0xb15610  size=7 ==== */

void SkillEffectCtrl::set_IsFront(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 200) = param_2;
  return;
}



/* ==== SkillEffectCtrl::set_IsPlaying  @ 0xe692a0  size=7 ==== */

void SkillEffectCtrl::set_IsPlaying(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0xa0) = param_2;
  return;
}



/* ==== SkillEffectCtrl::set_IsRepeat  @ 0x52d030  size=4 ==== */

void SkillEffectCtrl::set_IsRepeat(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x50) = param_2;
  return;
}



/* ==== SkillEffectCtrl::set_OnEffectEnd  @ 0x438e40  size=19 ==== */

void SkillEffectCtrl::set_OnEffectEnd(longlong param_1,undefined8 param_2)

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



/* ==== SkillEffectCtrl::set_SeSource  @ 0x437830  size=13 ==== */

void SkillEffectCtrl::set_SeSource(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x48) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x48U >> 0xc);
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



/* ==== SkillEffectCtrl::set_SortTarget  @ 0x438c80  size=19 ==== */

void SkillEffectCtrl::set_SortTarget(longlong param_1,undefined8 param_2)

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



/* ==== SkillEffectCtrl::set_Target  @ 0x438e20  size=19 ==== */

void SkillEffectCtrl::set_Target(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x98) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x98U >> 0xc);
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



/* ==== SkillEffectCtrl::set_particleStartDelayDictionary  @ 0x5d47f0  size=19 ==== */

void SkillEffectCtrl::set_particleStartDelayDictionary(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xd8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xd8U >> 0xc);
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



/* ==== SkillEffectCtrl::set_particles  @ 0x5e8be0  size=19 ==== */

void SkillEffectCtrl::set_particles(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xd0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xd0U >> 0xc);
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



/* ==== SkillEffectCtrl::set_resumeTime  @ 0x105b0d0  size=9 ==== */

void SkillEffectCtrl::set_resumeTime(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0xe0) = param_2;
  return;
}



/* ==== SkillEffectCtrl::set_trackTargetCoroutinueId  @ 0x5e9c30  size=7 ==== */

void SkillEffectCtrl::set_trackTargetCoroutinueId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0xa4) = param_2;
  return;
}



/* ==== SkillEffectCtrl::updateBoneTimeLine  @ 0x105b0e0  size=1 ==== */

longlong SkillEffectCtrl::updateBoneTimeLine(undefined8 param_1,undefined8 param_2,undefined1 param_3)

{
  longlong lVar1;
  
  if (cRam000000018ab606a3 == '\0') {
    sub_8032f6d0(0x187bc37e8);
    cRam000000018ab606a3 = '\x01';
  }
  lVar1 = sub_8032f970(uRam0000000187bc37e8);
  sub_80443e50(lVar1,0);
  *(undefined4 *)(lVar1 + 0x10) = 0;
  *(undefined8 *)(lVar1 + 0x20) = param_1;
  sub_8032e8d0(lVar1 + 0x20,param_1);
  *(undefined8 *)(lVar1 + 0x28) = param_2;
  sub_8032e8d0(lVar1 + 0x28,param_2);
  *(undefined1 *)(lVar1 + 0x30) = param_3;
  return lVar1;
}



/* ==== SkillEffectCtrl::updateTimer  @ 0x105b180  size=1 ==== */

longlong SkillEffectCtrl::updateTimer(undefined8 param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab606a6 == '\0') {
    sub_8032f6d0(0x187bc39a0);
    cRam000000018ab606a6 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187bc39a0);
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



