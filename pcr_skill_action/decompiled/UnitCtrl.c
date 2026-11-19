/* ==== UnitCtrl::.ctor  @ 0x1604140  size=1 ==== */

void UnitCtrl::.ctor(longlong param_1)

{
  uint uVar1;
  undefined4 uVar2;
  longlong lVar3;
  code *pcVar4;
  longlong lVar5;
  undefined8 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 *puVar9;
  undefined1 auStack_18 [16];
  
  if (cRam000000018ab63075 == '\0') {
    sub_8032f6d0(0x187b41028);
    sub_8032f6d0(0x187c1baf8);
    sub_8032f6d0(0x187b5c088);
    sub_8032f6d0(0x187bad9a8);
    sub_8032f6d0(0x187bf0450);
    sub_8032f6d0(0x187be3458);
    sub_8032f6d0(0x187b98c10);
    sub_8032f6d0(0x187b9e0a0);
    sub_8032f6d0(0x187b413a0);
    sub_8032f6d0(0x187b98a58);
    sub_8032f6d0(0x187b839e0);
    sub_8032f6d0(0x187b79f30);
    sub_8032f6d0(0x187bae5c8);
    sub_8032f6d0(0x187c31708);
    sub_8032f6d0(0x187bf1860);
    sub_8032f6d0(0x187bc60b0);
    sub_8032f6d0(0x187c38220);
    sub_8032f6d0(0x187c20d30);
    sub_8032f6d0(0x187bc4570);
    sub_8032f6d0(0x187c3d588);
    sub_8032f6d0(0x187c027e0);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187c20790);
    sub_8032f6d0(0x187bef820);
    cRam000000018ab63075 = '\x01';
  }
  uVar6 = sub_8032f970(uRam0000000187bad9a8);
  sub_85575ea0(uVar6,uRam0000000187b5c088);
  *(undefined8 *)(param_1 + 0x110) = uVar6;
  sub_8032e8d0(param_1 + 0x110,uVar6);
  uVar6 = sub_8032f970(uRam0000000187be3458);
  sub_85575ea0(uVar6,uRam0000000187c1baf8);
  *(undefined8 *)(param_1 + 0x1d0) = uVar6;
  sub_8032e8d0(param_1 + 0x1d0,uVar6);
  uVar6 = sub_8032f970(uRam0000000187bc60b0);
  sub_849de5b0(uVar6,uRam0000000187b839e0);
  *(undefined8 *)(param_1 + 600) = uVar6;
  sub_8032e8d0(param_1 + 600,uVar6);
  *(undefined4 *)(param_1 + 0x338) = 0x3f000000;
  *(undefined4 *)(param_1 + 0x33c) = 0x40400000;
  *(undefined4 *)(param_1 + 0x340) = 0x3f4ccccd;
  *(undefined4 *)(param_1 + 0x344) = 0x3f800000;
  *(undefined4 *)(param_1 + 0x348) = 0x3faccccd;
  *(undefined8 *)(param_1 + 0x360) = uRam0000000187bef820;
  sub_8032e8d0(param_1 + 0x360,uRam0000000187bef820);
  *(undefined8 *)(param_1 + 0x368) = uRam0000000187bef820;
  sub_8032e8d0(param_1 + 0x368,uRam0000000187bef820);
  uVar6 = sub_8032f970(uRam0000000187bc4570);
  sub_849de5b0(uVar6,uRam0000000187b79f30);
  *(undefined8 *)(param_1 + 0x378) = uVar6;
  sub_8032e8d0(param_1 + 0x378,uVar6);
  uVar6 = sub_8032f970(uRam0000000187c027e0);
  sub_849de5b0(uVar6,uRam0000000187b9e0a0);
  *(undefined8 *)(param_1 + 0x388) = uVar6;
  sub_8032e8d0(param_1 + 0x388,uVar6);
  *(undefined4 *)(param_1 + 0x394) = 0x3f800000;
  uVar6 = sub_8032f970(uRam0000000187c20d30);
  sub_849de5b0(uVar6,uRam0000000187c31708);
  *(undefined8 *)(param_1 + 0x3a0) = uVar6;
  sub_8032e8d0(param_1 + 0x3a0,uVar6);
  lVar7 = sub_8032f970(uRam0000000187c38220);
  sub_849de5b0(lVar7,uRam0000000187b98a58);
  lVar8 = sub_8032f970(uRam0000000187c20790);
  sub_81712400(lVar8,0);
  if (lVar8 != 0) {
    *(undefined1 *)(lVar8 + 0x14) = 1;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3b8) = lVar7;
    sub_8032e8d0(param_1 + 0x3b8,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0x40000000;
    *(undefined1 *)(lVar8 + 0x14) = 1;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3c0) = lVar7;
    sub_8032e8d0(param_1 + 0x3c0,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0x40000000;
    *(undefined1 *)(lVar8 + 0x14) = 1;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3c8) = lVar7;
    sub_8032e8d0(param_1 + 0x3c8,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 1;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3d0) = lVar7;
    sub_8032e8d0(param_1 + 0x3d0,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 1;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3d8) = lVar7;
    sub_8032e8d0(param_1 + 0x3d8,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 1;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3e0) = lVar7;
    sub_8032e8d0(param_1 + 0x3e0,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 1000) = lVar7;
    sub_8032e8d0(param_1 + 1000,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3f0) = lVar7;
    sub_8032e8d0(param_1 + 0x3f0,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x3f8) = lVar7;
    sub_8032e8d0(param_1 + 0x3f8,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x400) = lVar7;
    sub_8032e8d0(param_1 + 0x400,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x408) = lVar7;
    sub_8032e8d0(param_1 + 0x408,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x410) = lVar7;
    sub_8032e8d0(param_1 + 0x410,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x418) = lVar7;
    sub_8032e8d0(param_1 + 0x418,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x420) = lVar7;
    sub_8032e8d0(param_1 + 0x420,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x428) = lVar7;
    sub_8032e8d0(param_1 + 0x428,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x430) = lVar7;
    sub_8032e8d0(param_1 + 0x430,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x438) = lVar7;
    sub_8032e8d0(param_1 + 0x438,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 0;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x440) = lVar7;
    sub_8032e8d0(param_1 + 0x440,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 == 0) goto code_r0x000181605679;
    *(undefined4 *)(lVar8 + 0x10) = 0;
    *(undefined1 *)(lVar8 + 0x14) = 1;
    lVar5 = lRam0000000187b98c10;
    if (lVar7 == 0) goto code_r0x000181605679;
    *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
    lVar3 = *(longlong *)(lVar7 + 0x10);
    uVar1 = *(uint *)(lVar7 + 0x18);
    if (lVar3 == 0) goto code_r0x000181605679;
    if (uVar1 < *(uint *)(lVar3 + 0x18)) {
      *(uint *)(lVar7 + 0x18) = uVar1 + 1;
      if (*(uint *)(lVar3 + 0x18) <= uVar1) goto code_r0x00018160567f;
      *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
      sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
    }
    else {
      sub_84a97470(lVar7,lVar8,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70));
    }
    *(longlong *)(param_1 + 0x448) = lVar7;
    sub_8032e8d0(param_1 + 0x448,lVar7);
    lVar7 = sub_8032f970(uRam0000000187c38220);
    sub_849de5b0(lVar7,uRam0000000187b98a58);
    lVar8 = sub_8032f970(uRam0000000187c20790);
    sub_81712400(lVar8,0);
    if (lVar8 != 0) {
      *(undefined4 *)(lVar8 + 0x10) = 0;
      *(undefined1 *)(lVar8 + 0x14) = 1;
      lVar5 = lRam0000000187b98c10;
      if (lVar7 != 0) {
        *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
        lVar3 = *(longlong *)(lVar7 + 0x10);
        uVar1 = *(uint *)(lVar7 + 0x18);
        if (lVar3 != 0) {
          if (uVar1 < *(uint *)(lVar3 + 0x18)) {
            *(uint *)(lVar7 + 0x18) = uVar1 + 1;
            if (*(uint *)(lVar3 + 0x18) <= uVar1) {
code_r0x00018160567f:
              sub_8032f9c0();
              pcVar4 = (code *)swi(3);
              (*pcVar4)();
              return;
            }
            *(longlong *)(lVar3 + 0x20 + (longlong)(int)uVar1 * 8) = lVar8;
            sub_8032e8d0(lVar3 + (longlong)(int)uVar1 * 8 + 0x20,lVar8);
          }
          else {
            sub_84a97470(lVar7,lVar8,
                          *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70))
            ;
          }
          *(longlong *)(param_1 + 0x450) = lVar7;
          sub_8032e8d0(param_1 + 0x450,lVar7);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x4c0) = uVar6;
          sub_8032e8d0(param_1 + 0x4c0,uVar6);
          if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
            sub_8032fa70();
          }
          puVar9 = (undefined8 *)ObscuredInt::FHOALPFLEEC(auStack_18,0,0);
          uVar2 = *(undefined4 *)(puVar9 + 1);
          *(undefined8 *)(param_1 + 0x4c8) = *puVar9;
          *(undefined4 *)(param_1 + 0x4d0) = uVar2;
          puVar9 = (undefined8 *)ObscuredInt::FHOALPFLEEC(auStack_18,0,0);
          uVar2 = *(undefined4 *)(puVar9 + 1);
          *(undefined8 *)(param_1 + 0x4d4) = *puVar9;
          *(undefined4 *)(param_1 + 0x4dc) = uVar2;
          uVar6 = sub_8032f970(uRam0000000187be3458);
          sub_85575ea0(uVar6,uRam0000000187c1baf8);
          *(undefined8 *)(param_1 + 0x500) = uVar6;
          sub_8032e8d0(param_1 + 0x500,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf0450);
          sub_85575ea0(uVar6,uRam0000000187b41028);
          *(undefined8 *)(param_1 + 0x508) = uVar6;
          sub_8032e8d0(param_1 + 0x508,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x510) = uVar6;
          sub_8032e8d0(param_1 + 0x510,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x518) = uVar6;
          sub_8032e8d0(param_1 + 0x518,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x520) = uVar6;
          sub_8032e8d0(param_1 + 0x520,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x528) = uVar6;
          sub_8032e8d0(param_1 + 0x528,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x530) = uVar6;
          sub_8032e8d0(param_1 + 0x530,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x538) = uVar6;
          sub_8032e8d0(param_1 + 0x538,uVar6);
          uVar6 = sub_8032f970(uRam0000000187bf1860);
          sub_849de5b0(uVar6,uRam0000000187b413a0);
          *(undefined8 *)(param_1 + 0x540) = uVar6;
          sub_8032e8d0(param_1 + 0x540,uVar6);
          uVar6 = sub_8032f970(uRam0000000187c3d588);
          sub_849de5b0(uVar6,uRam0000000187bae5c8);
          *(undefined8 *)(param_1 + 0x548) = uVar6;
          sub_8032e8d0(param_1 + 0x548,uVar6);
          if (cRam000000018ab8a5bc == '\0') {
            sub_8032f6d0(0x187b5e978,0);
            cRam000000018ab8a5bc = '\x01';
          }
          if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
            sub_8032fa70();
          }
          return;
        }
      }
    }
  }
code_r0x000181605679:
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitCtrl::<PlayUbChainVoice>b__716_0  @ 0x1603690  size=1 ==== */

bool UnitCtrl::<PlayUbChainVoice>b__716_0(undefined8 param_1,longlong param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  char cVar3;
  undefined1 uVar4;
  longlong lVar5;
  undefined4 uStack_28;
  undefined4 uStack_24;
  undefined4 uStack_20;
  undefined4 uStack_1c;
  undefined8 uStack_18;
  
  if (cRam000000018ab63076 == '\0') {
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b61160);
    cRam000000018ab63076 = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = Object::op_Inequality(param_2,param_1,0);
  if (cVar3 != '\0') {
    if ((param_2 == 0) || (lVar5 = *(longlong *)(param_2 + 0x88), lVar5 == 0)) {
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      uVar4 = (*pcVar2)();
      return (bool)uVar4;
    }
    uStack_28 = *(undefined4 *)(lVar5 + 0x430);
    uStack_24 = *(undefined4 *)(lVar5 + 0x434);
    uStack_20 = *(undefined4 *)(lVar5 + 0x438);
    uStack_1c = *(undefined4 *)(lVar5 + 0x43c);
    uVar1 = *(undefined8 *)(lVar5 + 0x440);
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_18 = uVar1;
    lVar5 = ObscuredLong::FHOALPFLEEC(&uStack_28,0);
    if ((0 < lVar5) && (*(int *)(param_2 + 0x180) != 1)) {
      return *(int *)(param_2 + 0x180) != 2;
    }
  }
  return false;
}



/* ==== UnitCtrl::<SearchContinuousUnit>b__718_0  @ 0x16037a0  size=1 ==== */

ulonglong UnitCtrl::<SearchContinuousUnit>b__718_0(longlong param_1,longlong param_2)

{
  code *pcVar1;
  ulonglong uVar2;
  
  if (param_2 != 0) {
    return (ulonglong)
           CONCAT31((int3)((uint)*(int *)(param_1 + 0x490) >> 8),
                    *(int *)(param_2 + 0x10) == *(int *)(param_1 + 0x490));
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar2 = (*pcVar1)();
  return uVar2;
}



/* ==== UnitCtrl::<instantiateResources>b__659_0  @ 0x16037c0  size=1 ==== */

void UnitCtrl::<instantiateResources>b__659_0(longlong param_1,int param_2,char param_3)

{
  longlong lVar1;
  code *pcVar2;
  longlong lVar3;
  
  lVar1 = *(longlong *)(param_1 + 0x268);
  if (param_3 != '\0') {
    if (lVar1 != 0) {
      if (param_2 == 0) {
        return;
      }
      lVar3 = Component::get_gameObject(lVar1,0,0);
      if (lVar3 != 0) {
        GameObject::SetActive(lVar3,1,0);
        *(int *)(lVar1 + 0x80) = *(int *)(lVar1 + 0x80) + 1;
        if (*(longlong *)(lVar1 + 0x78) != 0) {
          AbromalStateIconController::AddIcon(*(longlong *)(lVar1 + 0x78),param_2,0);
          return;
        }
      }
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
code_r0x0001816037f2:
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  if (lVar1 == 0) goto code_r0x0001816037f2;
  if (param_2 != 0) {
    if (((*(char *)(lVar1 + 0x6c) == '\0') && (*(char *)(lVar1 + 0x6d) == '\0')) &&
       (*(int *)(lVar1 + 0x80) == 0)) {
      lVar3 = Component::get_gameObject(lVar1,0);
      if (lVar3 == 0) goto code_r0x0001814afdc5;
      GameObject::SetActive(lVar3,0,0);
    }
    *(int *)(lVar1 + 0x80) = *(int *)(lVar1 + 0x80) + -1;
    if (*(longlong *)(lVar1 + 0x78) == 0) {
code_r0x0001814afdc5:
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
    AbromalStateIconController::DeleteIcon(*(longlong *)(lVar1 + 0x78),param_2,0);
  }
  return;
}



/* ==== UnitCtrl::<instantiateResources>b__659_1  @ 0x1603800  size=1 ==== */

void UnitCtrl::<instantiateResources>b__659_1(longlong param_1,ulonglong param_2,undefined8 param_3)

{
  undefined8 uVar1;
  longlong lVar2;
  code *pcVar3;
  longlong lVar4;
  undefined8 uVar5;
  longlong lVar6;
  undefined4 auStack_28 [2];
  undefined4 uStack_20;
  undefined4 uStack_18;
  undefined4 uStack_14;
  undefined4 uStack_10;
  
  if (*(longlong *)(param_1 + 0x268) == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  if ((int)param_2 != 0) {
    lVar2 = *(longlong *)(*(longlong *)(param_1 + 0x268) + 0x78);
    if (lVar2 == 0) {
      sub_8032f9d0();
      pcVar3 = (code *)swi(3);
      (*pcVar3)();
      return;
    }
    if (cRam000000018ab60ee1 == '\0') {
      sub_8032f6d0(0x187c2cb00,param_2,param_3,0);
      sub_8032f6d0(0x187bc0060);
      sub_8032f6d0(0x187bc0218);
      sub_8032f6d0(0x187b9ac38);
      sub_8032f6d0(0x187b93cb8);
      sub_8032f6d0(0x187b93e70);
      sub_8032f6d0(0x187b3e3d0);
      cRam000000018ab60ee1 = '\x01';
    }
    lVar4 = sub_8032f970(uRam0000000187b3e3d0);
    sub_804383d0(lVar4,0);
    if (lVar4 != 0) {
      *(int *)(lVar4 + 0x10) = (int)param_2;
      if (*(longlong *)(lVar2 + 0x60) != 0) {
        sub_849deb70(auStack_28,*(longlong *)(lVar2 + 0x60),param_2 & 0xffffffff,
                      uRam0000000187bc0060);
        _uStack_18 = CONCAT44((int)param_3,auStack_28[0]);
        if (*(longlong *)(lVar2 + 0x60) != 0) {
          uStack_10 = uStack_20;
          sub_849ded00(*(longlong *)(lVar2 + 0x60),*(undefined4 *)(lVar4 + 0x10),&uStack_18,
                        uRam0000000187bc0218);
          uVar1 = *(undefined8 *)(lVar2 + 0x40);
          uVar5 = sub_8032f970(uRam0000000187b9ac38);
          sub_84cacb40(uVar5,lVar4,uRam0000000187b93cb8,0);
          lVar6 = sub_8484ca50(uVar1,uVar5,uRam0000000187c2cb00);
          if (lVar6 != 0) {
            SpriteAndLabel::SetIconType(lVar6,*(undefined4 *)(lVar4 + 0x10),*(undefined8 *)(lVar2 + 0x60),0);
          }
          uVar1 = *(undefined8 *)(lVar2 + 0x48);
          uVar5 = sub_8032f970(uRam0000000187b9ac38);
          sub_84cacb40(uVar5,lVar4,uRam0000000187b93e70,0);
          lVar6 = sub_8484ca50(uVar1,uVar5,uRam0000000187c2cb00);
          if (lVar6 != 0) {
            SpriteAndLabel::SetIconType(lVar6,*(undefined4 *)(lVar4 + 0x10),*(undefined8 *)(lVar2 + 0x60),0);
          }
          return;
        }
      }
    }
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  return;
}



/* ==== UnitCtrl::<instantiateResources>b__659_2  @ 0x1603830  size=1 ==== */

void UnitCtrl::<instantiateResources>b__659_2(longlong param_1,ulonglong param_2,undefined8 param_3)

{
  undefined8 uVar1;
  longlong lVar2;
  code *pcVar3;
  longlong lVar4;
  undefined8 uVar5;
  longlong lVar6;
  undefined8 uStack_28;
  undefined4 uStack_20;
  undefined8 uStack_18;
  undefined4 uStack_10;
  
  if (*(longlong *)(param_1 + 0x268) == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  if ((int)param_2 != 0) {
    lVar2 = *(longlong *)(*(longlong *)(param_1 + 0x268) + 0x78);
    if (lVar2 == 0) {
      sub_8032f9d0();
      pcVar3 = (code *)swi(3);
      (*pcVar3)();
      return;
    }
    if (cRam000000018ab60ee3 == '\0') {
      sub_8032f6d0(0x187c2cb00,param_2,param_3,0);
      sub_8032f6d0(0x187bc0060);
      sub_8032f6d0(0x187bc0218);
      sub_8032f6d0(0x187b9ac38);
      sub_8032f6d0(0x187b94018);
      sub_8032f6d0(0x187b941d0);
      sub_8032f6d0(0x187b3e588);
      cRam000000018ab60ee3 = '\x01';
    }
    lVar4 = sub_8032f970(uRam0000000187b3e588);
    sub_804383d0(lVar4,0);
    if (lVar4 != 0) {
      *(int *)(lVar4 + 0x10) = (int)param_2;
      if (*(longlong *)(lVar2 + 0x60) != 0) {
        sub_849deb70(&uStack_28,*(longlong *)(lVar2 + 0x60),param_2 & 0xffffffff,
                      uRam0000000187bc0060);
        uStack_20 = CONCAT31(uStack_20._1_3_,(char)param_3);
        if (*(longlong *)(lVar2 + 0x60) != 0) {
          uStack_10 = uStack_20;
          uStack_18 = uStack_28;
          sub_849ded00(*(longlong *)(lVar2 + 0x60),*(undefined4 *)(lVar4 + 0x10),&uStack_18,
                        uRam0000000187bc0218);
          uVar1 = *(undefined8 *)(lVar2 + 0x40);
          uVar5 = sub_8032f970(uRam0000000187b9ac38);
          sub_84cacb40(uVar5,lVar4,uRam0000000187b94018,0);
          lVar6 = sub_8484ca50(uVar1,uVar5,uRam0000000187c2cb00);
          if (lVar6 != 0) {
            SpriteAndLabel::SetIconType(lVar6,*(undefined4 *)(lVar4 + 0x10),*(undefined8 *)(lVar2 + 0x60),0);
          }
          uVar1 = *(undefined8 *)(lVar2 + 0x48);
          uVar5 = sub_8032f970(uRam0000000187b9ac38);
          sub_84cacb40(uVar5,lVar4,uRam0000000187b941d0,0);
          lVar6 = sub_8484ca50(uVar1,uVar5,uRam0000000187c2cb00);
          if (lVar6 != 0) {
            SpriteAndLabel::SetIconType(lVar6,*(undefined4 *)(lVar4 + 0x10),*(undefined8 *)(lVar2 + 0x60),0);
          }
          return;
        }
      }
    }
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  return;
}



/* ==== UnitCtrl::AppendBreakLog  @ 0x15f8160  size=1 ==== */

void UnitCtrl::AppendBreakLog(undefined8 param_1,undefined8 param_2)

{
  longlong lVar1;
  code *pcVar2;
  
  if (cRam000000018ab6306b == '\0') {
    sub_8032f6d0(0x187b8afd0);
    cRam000000018ab6306b = '\x01';
  }
  if (cRam000000018ab63079 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63079 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8);
  if (lVar1 != 0) {
    sub_800157a0(0,uRam0000000187b8afd0,lVar1,0xe,0,0,0,0,0,1,1,param_2,param_1);
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::AppendCoroutine  @ 0x15f8220  size=1 ==== */

ulonglong UnitCtrl::AppendCoroutine(undefined8 param_1,undefined8 param_2,undefined4 param_3,undefined8 param_4)

{
  ulonglong *puVar1;
  longlong lVar2;
  code *pcVar3;
  char cVar4;
  longlong lVar5;
  undefined8 uVar6;
  longlong lVar7;
  ulonglong uVar8;
  ulonglong uVar9;
  uint uVar10;
  uint uVar11;
  undefined8 unaff_RDI;
  uint uVar12;
  bool bVar13;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar7 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar7 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar9 = (*pcVar3)();
    return uVar9;
  }
  lVar7 = *(longlong *)(lVar7 + 0x2c8);
  if (lVar7 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar9 = (*pcVar3)();
    return uVar9;
  }
  if (cRam000000018ab60eaf == '\0') {
    sub_8032f6d0(0x187bb7388);
    sub_8032f6d0(0x187b598f8);
    sub_8032f6d0(0x187b59c60);
    sub_8032f6d0(0x187b5a330);
    sub_8032f6d0(0x187b83308);
    sub_8032f6d0(0x187b309f8);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab60eaf = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar4 = Object::op_Equality(param_4,0,0);
  lVar5 = *(longlong *)(lVar7 + 0x20);
  if (cVar4 == '\0') {
    if (((lVar5 == 0) || (lVar5 = sub_855ffe50(lVar5,param_3,uRam0000000187b83308), lVar5 == 0)) ||
       (*(longlong *)(lVar5 + 0x18) == 0)) goto code_r0x000181165ee1;
    cVar4 = sub_85653020(*(longlong *)(lVar5 + 0x18),param_4,uRam0000000187b59c60);
    if (cVar4 == '\0') {
      if ((*(longlong *)(lVar7 + 0x20) == 0) ||
         (lVar5 = sub_855ffe50(*(longlong *)(lVar7 + 0x20),param_3,uRam0000000187b83308),
         lVar5 == 0)) goto code_r0x000181165ee1;
      lVar5 = *(longlong *)(lVar5 + 0x18);
      uVar6 = sub_8032f970(uRam0000000187bb7388);
      CoroutineDataBase::.ctor(uVar6,0);
      if (lVar5 == 0) goto code_r0x000181165ee1;
      sub_856757b0(lVar5,param_4,uVar6,uRam0000000187b598f8);
    }
    if (((*(longlong *)(lVar7 + 0x20) == 0) ||
        (lVar7 = sub_855ffe50(*(longlong *)(lVar7 + 0x20),param_3,uRam0000000187b83308), lVar7 == 0
        )) || (*(longlong *)(lVar7 + 0x18) == 0)) goto code_r0x000181165ee1;
    lVar7 = sub_85685bc0(*(longlong *)(lVar7 + 0x18),param_4,uRam0000000187b5a330);
  }
  else {
    if (lVar5 == 0) goto code_r0x000181165ee1;
    lVar7 = sub_855ffe50(lVar5,param_3,uRam0000000187b83308);
  }
  lVar5 = lRam0000000187b309f8;
  if ((lVar7 == 0) || (lVar7 = *(longlong *)(lVar7 + 0x10), lVar7 == 0)) {
code_r0x000181165ee1:
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar9 = (*pcVar3)();
    return uVar9;
  }
  *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
  lVar2 = *(longlong *)(lVar7 + 0x10);
  uVar12 = *(uint *)(lVar7 + 0x18);
  if (lVar2 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar9 = (*pcVar3)();
    return uVar9;
  }
  if (uVar12 < *(uint *)(lVar2 + 0x18)) {
    uVar8 = (ulonglong)(uVar12 + 1);
    *(uint *)(lVar7 + 0x18) = uVar12 + 1;
    if (*(uint *)(lVar2 + 0x18) <= uVar12) {
      sub_8032f9c0();
      pcVar3 = (code *)swi(3);
      uVar9 = (*pcVar3)();
      return uVar9;
    }
    *(undefined8 *)(lVar2 + 0x20 + (longlong)(int)uVar12 * 8) = param_2;
    uVar9 = lVar2 + 0x20 + (longlong)(int)uVar12 * 8;
LAB_18032e8d0:
    if (iRam000000018ab8fbd8 != 0) {
      uVar12 = (uint)(uVar9 >> 0xc);
      puVar1 = (ulonglong *)((ulonglong)((uVar12 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
      do {
        uVar9 = *puVar1;
        LOCK();
        uVar8 = *puVar1;
        bVar13 = uVar9 == uVar8;
        if (bVar13) {
          *puVar1 = uVar9 | 1L << (uVar12 & 0x3f);
          uVar8 = uVar9;
        }
        UNLOCK();
      } while (!bVar13);
    }
    return uVar8;
  }
  uVar12 = *(uint *)(lVar7 + 0x18);
  lVar2 = *(longlong *)(lVar7 + 0x10);
  uVar11 = uVar12 + 1;
  lVar5 = *(longlong *)
           (*(longlong *)
             (*(longlong *)
               (*(longlong *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0x70) + 0x20) +
             0xc0) + 0x78);
  if (lVar2 != 0) {
    if (*(int *)(lVar2 + 0x18) < (int)uVar11) {
      if (*(longlong *)(lVar2 + 0x18) == 0) {
        uVar10 = 4;
      }
      else {
        uVar10 = *(int *)(lVar2 + 0x18) * 2;
        if (0x7fefffff < uVar10) {
          uVar10 = 0x7fefffff;
        }
      }
      if ((int)uVar11 <= (int)uVar10) {
        uVar11 = uVar10;
      }
      sub_80003170(lVar7,uVar11,
                          *(undefined8 *)(*(longlong *)(*(longlong *)(lVar5 + 0x20) + 0xc0) + 0xf0),
                          lVar5,unaff_RDI);
    }
    *(uint *)(lVar7 + 0x18) = uVar12 + 1;
    lVar7 = *(longlong *)(lVar7 + 0x10);
    if (lVar7 != 0) {
      if (*(uint *)(lVar7 + 0x18) <= uVar12) {
        sub_8032f9c0();
        pcVar3 = (code *)swi(3);
        uVar9 = (*pcVar3)();
        return uVar9;
      }
      *(undefined8 *)(lVar7 + 0x20 + (longlong)(int)uVar12 * 8) = param_2;
      uVar8 = lVar7 + 0x20;
      uVar9 = uVar8 + (longlong)(int)uVar12 * 8;
      goto LAB_18032e8d0;
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar9 = (*pcVar3)();
  return uVar9;
}



/* ==== UnitCtrl::Awake  @ 0x15f82a0  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001815f8375: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f83a9: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f83dd: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f8411: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x0001815f83e2) */
/* WARNING: Removing unreachable block (ram,0x0001815f83ae) */
/* WARNING: Removing unreachable block (ram,0x0001815f837a) */
/* WARNING: Removing unreachable block (ram,0x0001815f8416) */

ulonglong UnitCtrl::Awake(longlong param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  undefined8 uVar3;
  ulonglong uVar4;
  uint uVar5;
  bool bVar6;
  
  if (cRam000000018ab63048 == '\0') {
    sub_8032f6d0(0x187b32888);
    sub_8032f6d0(0x187bedcc8);
    sub_8032f6d0(0x187b44d88);
    sub_8032f6d0(0x187b84028);
    sub_8032f6d0(0x187bb89c0);
    sub_8032f6d0(0x187c1c028);
    sub_8032f6d0(0x187bd0020);
    sub_8032f6d0(0x187be2b30);
    sub_8032f6d0(0x187c34ba0);
    sub_8032f6d0(0x187bf1a18);
    cRam000000018ab63048 = '\x01';
  }
  FixedTransformMonoBehavior::OnAwake(param_1,0);
  *(undefined1 *)(param_1 + 0x164) = 0;
  uVar3 = sub_8032f970(uRam0000000187bf1a18);
  uVar4 = sub_849de5b0(uVar3,uRam0000000187b44d88);
  *(undefined8 *)(param_1 + 400) = uVar3;
  if (iRam000000018ab8fbd8 != 0) {
    uVar5 = (uint)(param_1 + 400U >> 0xc);
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



/* ==== UnitCtrl::BattleRecovery  @ 0x15f8460  size=1 ==== */

void UnitCtrl::BattleRecovery(longlong param_1,int param_2)

{
  bool bVar1;
  code *pcVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  int iVar5;
  undefined4 uVar6;
  longlong lVar7;
  longlong lVar8;
  undefined8 *puVar9;
  undefined8 uVar10;
  double dVar11;
  double dVar12;
  ulonglong in_stack_ffffffffffffff60;
  undefined8 uStack_78;
  undefined8 uStack_70;
  undefined8 uStack_68;
  
  if (cRam000000018ab63059 == '\0') {
    sub_8032f6d0(0x187bfd360);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b61160);
    sub_8032f6d0(0x187bf5d58);
    sub_8032f6d0(0x187c0e350);
    sub_8032f6d0(0x187bfc3b0);
    cRam000000018ab63059 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x0001815f8adb;
  if (*(char *)(*(longlong *)(param_1 + 0xd0) + 0xb9) != '\0') {
    return;
  }
  dVar12 = 1.0;
  dVar11 = 1.0;
  if (param_2 == 2) {
code_r0x0001815f8621:
    lVar7 = sub_80003d20(uRam0000000187b3c708);
    if (lVar7 == 0) goto code_r0x0001815f8adb;
    lVar7 = *(longlong *)(lVar7 + 0x838);
    lVar8 = *(longlong *)(lRam0000000187c0e350 + 0x20);
    if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
      lVar8 = sub_8032d010(lVar8);
    }
    lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
    if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
      lVar8 = sub_8032d010(lVar8);
    }
    if (((**(longlong **)(lVar8 + 0xb8) == 0) ||
        (uVar6 = DungeonTempData::get_CurrentEnterAreaId(**(longlong **)(lVar8 + 0xb8),0), lVar7 == 0)) ||
       (lVar7 = LGBFNOPFKJD::BKEJKNOJCEE(lVar7,uVar6,0), lVar7 == 0)) goto code_r0x0001815f8adb;
    uVar10 = *(undefined8 *)(lVar7 + 0xa4);
    uVar6 = *(undefined4 *)(lVar7 + 0xac);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_70 = CONCAT44(uStack_70._4_4_,uVar6);
    uStack_78 = uVar10;
    iVar5 = ObscuredInt::FHOALPFLEEC(&uStack_78,0);
    uStack_78 = *(undefined8 *)(lVar7 + 0xb0);
    uVar6 = *(undefined4 *)(lVar7 + 0xb8);
code_r0x0001815f86fa:
    uStack_70 = CONCAT44(uStack_70._4_4_,uVar6);
    dVar12 = (double)iVar5 / 100.0;
    iVar5 = ObscuredInt::FHOALPFLEEC(&uStack_78,0);
    dVar11 = (double)iVar5 / 100.0;
  }
  else {
    if (param_2 == 0xe) {
      lVar7 = sub_847c7d00(param_1,uRam0000000187bfd360);
      if (lVar7 == 0) goto code_r0x0001815f8adb;
      lVar7 = sub_84670860(lVar7,uRam0000000187bf5d58);
      lVar8 = sub_80003d20(uRam0000000187b3c708);
      if ((((lVar8 == 0) || (lVar7 == 0)) || (*(longlong *)(lVar8 + 0xef8) == 0)) ||
         (lVar7 = IIMIMLHCGLB::BKEJKNOJCEE(*(longlong *)(lVar8 + 0xef8),*(undefined4 *)(lVar7 + 0x10),0),
         lVar7 == 0)) goto code_r0x0001815f8adb;
      uVar10 = *(undefined8 *)(lVar7 + 0xa8);
      uVar6 = *(undefined4 *)(lVar7 + 0xb0);
      if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_70 = CONCAT44(uStack_70._4_4_,uVar6);
      uStack_78 = uVar10;
      iVar5 = ObscuredInt::FHOALPFLEEC(&uStack_78,0);
      uStack_78 = *(undefined8 *)(lVar7 + 0xb4);
      uVar6 = *(undefined4 *)(lVar7 + 0xbc);
      goto code_r0x0001815f86fa;
    }
    if (param_2 == 0x1e) goto code_r0x0001815f8621;
  }
  lVar7 = *(longlong *)(param_1 + 0x88);
  if (lVar7 != 0) {
    uVar3 = *(undefined8 *)(lVar7 + 0x430);
    uVar4 = *(undefined8 *)(lVar7 + 0x438);
    uVar10 = *(undefined8 *)(lVar7 + 0x440);
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_78 = uVar3;
    uStack_70 = uVar4;
    uStack_68 = uVar10;
    lVar8 = ObscuredLong::FHOALPFLEEC(&uStack_78,0);
    lVar7 = *(longlong *)(param_1 + 0x88);
    if (lVar7 != 0) {
      uStack_78 = *(undefined8 *)(lVar7 + 0x448);
      uStack_70 = *(undefined8 *)(lVar7 + 0x450);
      uStack_68 = *(undefined8 *)(lVar7 + 0x458);
      lVar7 = ObscuredLong::FHOALPFLEEC(&uStack_78,0);
      if (lVar8 < lVar7) {
        lVar7 = *(longlong *)(param_1 + 0x88);
        if (lVar7 == 0) goto code_r0x0001815f8adb;
        uVar3 = *(undefined8 *)(lVar7 + 0x520);
        uVar4 = *(undefined8 *)(lVar7 + 0x528);
        uVar10 = *(undefined8 *)(lVar7 + 0x530);
        if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_78 = uVar3;
        uStack_70 = uVar4;
        uStack_68 = uVar10;
        lVar7 = ObscuredLong::FHOALPFLEEC(&uStack_78,0);
        bVar1 = 0 < lVar7;
      }
      else {
        bVar1 = false;
      }
      if (bVar1) {
        lVar7 = *(longlong *)(param_1 + 0x70);
        if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x0001815f8adb;
        puVar9 = (undefined8 *)MBJPLNBCBDB::MLLHLJPKBLK(&uStack_78,*(longlong *)(param_1 + 0x90),0);
        uVar3 = *puVar9;
        uVar4 = puVar9[1];
        uVar10 = puVar9[2];
        if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_78 = uVar3;
        uStack_70 = uVar4;
        uStack_68 = uVar10;
        lVar8 = ObscuredLong::FHOALPFLEEC(&uStack_78,0);
        uVar10 = BattleUtil::DoubleToLong((double)lVar8 * dVar12,0);
        if (lVar7 == 0) goto code_r0x0001815f8adb;
        DBGNMELIABG::JFCBCLOLFOH(lVar7,uVar10,0,2,param_1,0,0,0x3ff0000000000000,1,0,0,
                      in_stack_ffffffffffffff60 & 0xffffffffffffff00,1,0,0,0);
      }
      if (*(longlong *)(param_1 + 0x88) != 0) {
        dVar12 = *(double *)(*(longlong *)(param_1 + 0x88) + 0x848);
        if (*(int *)(lRam0000000187bfc3b0 + 0xe4) == 0) {
          sub_8032fa70(lRam0000000187bfc3b0);
        }
        if ((double)*(int *)(*(longlong *)(lRam0000000187bfc3b0 + 0xb8) + 0x68) <= dVar12) {
          return;
        }
        lVar7 = *(longlong *)(param_1 + 0x88);
        if (lVar7 != 0) {
          uVar3 = *(undefined8 *)(lVar7 + 0x538);
          uVar4 = *(undefined8 *)(lVar7 + 0x540);
          uVar10 = *(undefined8 *)(lVar7 + 0x548);
          if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
            sub_8032fa70();
          }
          uStack_78 = uVar3;
          uStack_70 = uVar4;
          uStack_68 = uVar10;
          lVar7 = ObscuredLong::FHOALPFLEEC(&uStack_78,0);
          if (lVar7 < 1) {
            return;
          }
          if (bVar1) {
            if (cRam000000018ab6305b == '\0') {
              sub_8032f6d0(0x187c23470);
              cRam000000018ab6305b = '\x01';
            }
            lVar7 = sub_8032f970(uRam0000000187c23470);
            sub_80445940(lVar7,0,0);
            if (lVar7 != 0) {
              *(longlong *)(lVar7 + 0x20) = param_1;
              sub_8032e8d0(lVar7 + 0x20,param_1);
              *(double *)(lVar7 + 0x28) = dVar11;
              sub_86209420(param_1,lVar7,0);
              return;
            }
          }
          else {
            lVar7 = *(longlong *)(param_1 + 0x70);
            if (*(longlong *)(param_1 + 0x90) != 0) {
              puVar9 = (undefined8 *)MBJPLNBCBDB::FIKEHNCJHEO(&uStack_78,*(longlong *)(param_1 + 0x90),0);
              uVar3 = *puVar9;
              uVar4 = puVar9[1];
              uVar10 = puVar9[2];
              if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uStack_78 = uVar3;
              uStack_70 = uVar4;
              uStack_68 = uVar10;
              lVar8 = ObscuredLong::FHOALPFLEEC(&uStack_78,0);
              if (lVar7 != 0) {
                DBGNMELIABG::IHEEAOJPFLL(lVar7,7,(double)lVar8 * dVar11,0,0,1,0,1,0,1,0,0x3ff0000000000000,0);
                return;
              }
            }
          }
        }
      }
    }
  }
code_r0x0001815f8adb:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::BattleStartProcess  @ 0x15f8af0  size=1 ==== */

void UnitCtrl::BattleStartProcess(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  longlong *plStack_50;
  longlong *plStack_48;
  longlong *plStack_40;
  longlong *plStack_38;
  longlong *plStack_30;
  longlong *plStack_28;
  longlong *plStack_20;
  longlong *plStack_18;
  
  if (cRam000000018ab6304f == '\0') {
    sub_8032f6d0(0x187c268b8);
    sub_8032f6d0(0x187c26a70);
    sub_8032f6d0(0x187c26c20);
    sub_8032f6d0(0x187b71e50);
    sub_8032f6d0(0x187b4e120);
    cRam000000018ab6304f = '\x01';
  }
  *(undefined4 *)(param_1 + 0x168) = param_2;
  if (cRam000000018ab63050 == '\0') {
    sub_8032f6d0(0x187c22f58);
    cRam000000018ab63050 = '\x01';
  }
  lVar4 = sub_8032f970(uRam0000000187c22f58);
  sub_80445940(lVar4,0,0);
  if (lVar4 != 0) {
    *(longlong *)(lVar4 + 0x20) = param_1;
    sub_8032e8d0(lVar4 + 0x20,param_1);
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar1 != 0) {
      BattleManager::AppendCoroutine(lVar1,lVar4,1,param_1,0);
      *(undefined1 *)(param_1 + 0x270) = *(undefined1 *)(param_1 + 0x370);
      if ((*(longlong *)(param_1 + 0x38) != 0) &&
         (lVar4 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x1b8), lVar4 != 0)) {
        sub_849d8f60(&plStack_50,lVar4,uRam0000000187b4e120);
        plStack_30 = plStack_50;
        plStack_28 = plStack_48;
        plStack_20 = plStack_40;
        plStack_18 = plStack_38;
        while( true ) {
          cVar3 = sub_8571f880(&plStack_30,uRam0000000187c26a70);
          if (cVar3 == '\0') break;
          plStack_50 = plStack_20;
          plStack_48 = plStack_18;
          if (plStack_18 == (longlong *)0x0) goto code_r0x0001815f8dac;
          (**(code **)(*plStack_18 + 0x218))(plStack_18,*(undefined8 *)(*plStack_18 + 0x220));
          if (plStack_50 == (longlong *)0x0) goto code_r0x0001815f8da6;
          (**(code **)(*plStack_50 + 0x218))(plStack_50,*(undefined8 *)(*plStack_50 + 0x220));
        }
        sub_80443e50(&plStack_30,uRam0000000187c268b8);
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if ((lVar4 != 0) && (lVar4 = *(longlong *)(lVar4 + 0x400), lVar4 != 0)) {
          cVar3 = sub_84a98ab0(lVar4,param_1,uRam0000000187b71e50);
          lVar4 = *(longlong *)(param_1 + 0xc0);
          if (cVar3 == '\0') {
            if (lVar4 == 0) goto code_r0x0001815f8da0;
            IDFKMEFJKOD::CAGFFKLFPHJ(lVar4,0);
          }
          else {
            if (lVar4 == 0) goto code_r0x0001815f8da0;
            IDFKMEFJKOD::KBFBMOPGKHJ(lVar4,0);
          }
          return;
        }
      }
    }
  }
code_r0x0001815f8da0:
  sub_8032f9d0();
code_r0x0001815f8da6:
  sub_8032f9d0();
code_r0x0001815f8dac:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::CanBattleManagerPlayCutIn  @ 0x15f8dc0  size=1 ==== */

undefined8 UnitCtrl::CanBattleManagerPlayCutIn(void)

{
  longlong lVar1;
  longlong lVar2;
  code *pcVar3;
  char cVar4;
  undefined8 uVar5;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 == 0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    uVar5 = (*pcVar3)();
    return uVar5;
  }
  if (*(longlong *)(lVar1 + 0x540) != 0) {
    if (*(char *)(*(longlong *)(lVar1 + 0x540) + 0x30) != '\0') {
      return 0;
    }
    if (*(longlong *)(lVar1 + 1000) != 0) {
      lVar2 = *(longlong *)(*(longlong *)(lVar1 + 1000) + 0x4d0);
      cVar4 = QuestAutoProgressUtility::IsEnableBattleCategory(*(undefined4 *)(lVar1 + 0x2c0),0);
      if (cVar4 != '\0') {
        if ((*(longlong *)(lVar1 + 1000) == 0) ||
           (lVar1 = *(longlong *)(*(longlong *)(lVar1 + 1000) + 0x4d0), lVar1 == 0))
        goto code_r0x00018370c676;
        if (*(char *)(lVar1 + 0x18) != '\0') {
          if (lVar2 == 0) goto code_r0x00018370c676;
          if (*(char *)(lVar2 + 0x19) == '\0') {
            return 0;
          }
        }
      }
      return 1;
    }
  }
code_r0x00018370c676:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar5 = (*pcVar3)();
  return uVar5;
}



/* ==== UnitCtrl::ChangeChargeSkill  @ 0x15f8e10  size=1 ==== */

void UnitCtrl::ChangeChargeSkill(longlong param_1,undefined4 param_2,float param_3)

{
  undefined4 uVar1;
  longlong lVar2;
  code *pcVar3;
  longlong lVar4;
  
  uVar1 = *(undefined4 *)(param_1 + 0x4b8);
  *(undefined4 *)(param_1 + 0x4b8) = param_2;
  if (param_3 <= 0.0) {
    return;
  }
  if (cRam000000018ab63060 == '\0') {
    sub_8032f6d0(0x187c23108);
    cRam000000018ab63060 = '\x01';
  }
  lVar4 = sub_8032f970(uRam0000000187c23108);
  sub_80445940(lVar4,0,0);
  if (lVar4 != 0) {
    *(longlong *)(lVar4 + 0x20) = param_1;
    sub_8032e8d0(lVar4 + 0x20,param_1);
    *(float *)(lVar4 + 0x28) = param_3;
    *(undefined4 *)(lVar4 + 0x2c) = uVar1;
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar2 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar2 != 0) {
      BattleManager::AppendCoroutine(lVar2,lVar4,1,param_1,0);
      return;
    }
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitCtrl::ChangeChildObjectLayer  @ 0x15f8f00  size=1 ==== */

void UnitCtrl::ChangeChildObjectLayer(undefined8 param_1,undefined4 param_2,undefined8 param_3,undefined8 param_4)

{
  code *pcVar1;
  int iVar2;
  longlong lVar3;
  longlong lVar4;
  longlong lVar5;
  int iVar6;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab63066 == '\0') {
    sub_8032f6d0(0x187c38198);
    cRam000000018ab63066 = '\x01';
  }
  lVar3 = Component::get_gameObject(param_1,0);
  if (lVar3 == 0) {
    sub_8032f9d0();
    pcVar1 = (code *)swi(3);
    (*pcVar1)();
    return;
  }
  lVar3 = GameObject::get_transform(lVar3,0);
  if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (cRam000000018ab7b020 == '\0') {
    sub_8032f6d0(0x187c38198,param_2,0,param_4,unaff_RDI);
    cRam000000018ab7b020 = '\x01';
  }
  if (lVar3 != 0) {
    lVar4 = Component::get_gameObject(lVar3,0);
    if (lVar4 != 0) {
      GameObject::set_layer(lVar4,param_2,0);
      if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
        sub_8032fa70();
      }
      if (cRam000000018ab7b021 == '\0') {
        sub_8032f6d0(0x187c38198,param_2,0,param_4,unaff_RDI);
        cRam000000018ab7b021 = '\x01';
      }
      iVar6 = 0;
      if (lVar3 != 0) {
        while( true ) {
          iVar2 = Transform::get_childCount(lVar3,0);
          if (iVar2 <= iVar6) {
            return;
          }
          lVar4 = Transform::GetChild(lVar3,iVar6,0);
          if (lVar4 == 0) break;
          lVar5 = Component::get_gameObject(lVar4,0);
          if (lVar5 == 0) break;
          GameObject::set_layer(lVar5,param_2);
          if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
            sub_8032fa70();
          }
          ExtensionMethods::recursivChildObjectLayer(lVar4);
          iVar6 = iVar6 + 1;
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



/* ==== UnitCtrl::DestroyAndCoroutineRemove  @ 0x15f8f80  size=1 ==== */

void UnitCtrl::DestroyAndCoroutineRemove(undefined8 param_1)

{
  code *pcVar1;
  longlong lVar2;
  undefined8 uVar3;
  
  if (cRam000000018ab63049 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab63049 = '\x01';
  }
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar2 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar2 != 0) {
    BattleManager::RemoveCoroutine(lVar2,param_1,0);
    lVar2 = Component::get_gameObject(param_1,0);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    if (cRam000000018ab8a7d5 == '\0') {
      sub_8032f6d0(0x187b5e978,0);
      cRam000000018ab8a7d5 = '\x01';
    }
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    if (cRam000000018ab8a7d4 == '\0') {
      sub_8032f6d0(0x187b6d7f8);
      sub_8032f6d0(0x187b5e978);
      cRam000000018ab8a7d4 = '\x01';
    }
    if (*(longlong *)(lRam0000000187b6d7f8 + 0x38) == 0) {
      sub_8032d090();
    }
    uVar3 = 0;
    if (lVar2 != 0) {
      uVar3 = *(undefined8 *)(lVar2 + 0x10);
    }
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    if (pcRam000000018ab8a810 == (code *)0x0) {
      pcRam000000018ab8a810 = (code *)sub_8032fa40(&UNK_1867abf00);
    }
                    /* WARNING: Could not recover jumptable at 0x00018620ad2d. Too many branches */
                    /* WARNING: Treating indirect jump as call */
    (*pcRam000000018ab8a810)(uVar3,0);
    return;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitCtrl::DestructByOnDestroy  @ 0x15f9020  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001815f9046: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9070: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f909a: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f90c4: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f90ee: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9118: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9142: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f916c: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9196: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f91c0: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f91ea: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9214: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f923e: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9262: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f928c: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f92b6: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f92e0: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f930a: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9334: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9358: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f937c: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f939a: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f93b8: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f93d6: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f93fa: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f9424: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f944e: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f948c: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f94b6: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001815f94e0: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x0001815f94bb) */
/* WARNING: Removing unreachable block (ram,0x0001815f9491) */
/* WARNING: Removing unreachable block (ram,0x0001815f9453) */
/* WARNING: Removing unreachable block (ram,0x0001815f9504) */
/* WARNING: Removing unreachable block (ram,0x0001815f9478) */
/* WARNING: Removing unreachable block (ram,0x0001815f9429) */
/* WARNING: Removing unreachable block (ram,0x0001815f93ff) */
/* WARNING: Removing unreachable block (ram,0x0001815f93db) */
/* WARNING: Removing unreachable block (ram,0x0001815f93bd) */
/* WARNING: Removing unreachable block (ram,0x0001815f939f) */
/* WARNING: Removing unreachable block (ram,0x0001815f9381) */
/* WARNING: Removing unreachable block (ram,0x0001815f935d) */
/* WARNING: Removing unreachable block (ram,0x0001815f9339) */
/* WARNING: Removing unreachable block (ram,0x0001815f930f) */
/* WARNING: Removing unreachable block (ram,0x0001815f92e5) */
/* WARNING: Removing unreachable block (ram,0x0001815f92bb) */
/* WARNING: Removing unreachable block (ram,0x0001815f9291) */
/* WARNING: Removing unreachable block (ram,0x0001815f9267) */
/* WARNING: Removing unreachable block (ram,0x0001815f9243) */
/* WARNING: Removing unreachable block (ram,0x0001815f9219) */
/* WARNING: Removing unreachable block (ram,0x0001815f91ef) */
/* WARNING: Removing unreachable block (ram,0x0001815f91c5) */
/* WARNING: Removing unreachable block (ram,0x0001815f919b) */
/* WARNING: Removing unreachable block (ram,0x0001815f9171) */
/* WARNING: Removing unreachable block (ram,0x0001815f9147) */
/* WARNING: Removing unreachable block (ram,0x0001815f911d) */
/* WARNING: Removing unreachable block (ram,0x0001815f90f3) */
/* WARNING: Removing unreachable block (ram,0x0001815f90c9) */
/* WARNING: Removing unreachable block (ram,0x0001815f909f) */
/* WARNING: Removing unreachable block (ram,0x0001815f9075) */
/* WARNING: Removing unreachable block (ram,0x0001815f904b) */
/* WARNING: Removing unreachable block (ram,0x0001815f94e5) */

ulonglong UnitCtrl::DestructByOnDestroy(longlong param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  uVar3 = sub_8054c300(param_1,0);
  *(undefined8 *)(param_1 + 0xe0) = 0;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xe0U >> 0xc);
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



/* ==== UnitCtrl::ExecActionOnStartAndDetermineInstanceID  @ 0x15f9510  size=1 ==== */

void UnitCtrl::ExecActionOnStartAndDetermineInstanceID(longlong param_1)

{
  int iVar1;
  longlong lVar2;
  code *pcVar3;
  char cVar4;
  undefined8 uStack_60;
  undefined *puStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined *puStack_30;
  undefined8 uStack_28;
  undefined8 uStack_20;
  undefined8 uStack_18;
  
  if (cRam000000018ab63077 == '\0') {
    puStack_30 = &UNK_1815f952e;
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar2 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar2 != 0) {
    iVar1 = *(int *)(lVar2 + 0x4f0);
    *(int *)(lVar2 + 0x4f0) = iVar1 + 1;
    *(int *)(param_1 + 0x160) = iVar1;
    lVar2 = *(longlong *)(param_1 + 0x28);
    if (lVar2 != 0) {
      if (cRam000000018ab626c9 == '\0') {
        sub_8032f6d0(0x187c42000,0);
        sub_8032f6d0(0x187c0ed88);
        sub_8032f6d0(0x187c0ef40);
        sub_8032f6d0(0x187c0f0f0);
        sub_8032f6d0(0x187bd6490);
        cRam000000018ab626c9 = '\x01';
      }
      if (*(longlong *)(lVar2 + 0xb0) != 0) {
        sub_855337a0(&uStack_38,*(longlong *)(lVar2 + 0xb0),uRam0000000187c42000);
        uStack_60 = uStack_38;
        puStack_58 = puStack_30;
        uStack_50 = uStack_28;
        uStack_48 = uStack_20;
        uStack_40 = uStack_18;
        while( true ) {
          cVar4 = sub_85760f30(&uStack_60,uRam0000000187c0ef40);
          if (cVar4 == '\0') break;
          UnitActionController::ExecActionOnStart(lVar2,uStack_48,0);
        }
        sub_80443e50(&uStack_60,uRam0000000187c0ed88);
        return;
      }
      sub_8032f9d0();
      pcVar3 = (code *)swi(3);
      (*pcVar3)();
      return;
    }
  }
  puStack_30 = &UNK_1815f957d;
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitCtrl::ExecSkillBySkillId  @ 0x15f9580  size=1 ==== */

void UnitCtrl::ExecSkillBySkillId(longlong param_1,ulonglong param_2,undefined4 param_3)

{
  code *pcVar1;
  longlong lVar2;
  undefined8 unaff_RDI;
  
  if (*(longlong *)(param_1 + 0x28) != 0) {
    UnitActionController::StartAction(*(longlong *)(param_1 + 0x28),param_2,0);
    lVar2 = *(longlong *)(param_1 + 0x28);
    if (lVar2 != 0) {
      if (cRam000000018ab626c8 == '\0') {
        sub_8032f6d0(0x187c421b8,param_2 & 0xffffffff,param_3,0,unaff_RDI);
        cRam000000018ab626c8 = '\x01';
      }
      lVar2 = *(longlong *)(lVar2 + 0xb0);
      if (lVar2 != 0) {
        lVar2 = sub_855bdd60(lVar2,param_2 & 0xffffffff,uRam0000000187c421b8);
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
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitCtrl::FadeoutSe  @ 0x15f95e0  size=1 ==== */

void UnitCtrl::FadeoutSe(longlong param_1,undefined4 param_2)

{
  longlong lVar1;
  code *pcVar2;
  
  lVar1 = *(longlong *)(param_1 + 200);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  if (lVar1 != 0) {
    sub_839b5800(lVar1,*(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28),param_2,0
                 );
    if (*(longlong *)(param_1 + 0x30) != 0) {
      JCFNLBAPNAF::OCKBNPGDCIH(*(undefined8 *)(param_1 + 0x30),param_2,0);
    }
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::GetActionWaitProgressRate  @ 0x15f9670  size=1 ==== */

ulonglong UnitCtrl::GetActionWaitProgressRate(longlong param_1)

{
  undefined8 uVar1;
  code *pcVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  float fVar5;
  ulonglong uVar6;
  undefined8 uStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  
  if (cRam000000018ab63053 == '\0') {
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab63053 = '\x01';
  }
  if (*(longlong *)(param_1 + 0xd0) != 0) {
    if ((*(char *)(*(longlong *)(param_1 + 0xd0) + 0xb9) == '\0') &&
       (0.0 < *(float *)(param_1 + 0x290))) {
      uVar3 = *(undefined8 *)(param_1 + 0x278);
      uVar4 = *(undefined8 *)(param_1 + 0x280);
      uVar1 = *(undefined8 *)(param_1 + 0x288);
      if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_58 = uVar3;
      uStack_50 = uVar4;
      uStack_48 = uVar1;
      fVar5 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_58,0);
      fVar5 = 1.0 - fVar5 / *(float *)(param_1 + 0x290);
      if (fVar5 < 0.0) {
        fVar5 = 0.0;
      }
      else if (1.0 < fVar5) {
        return 0x3f800000;
      }
      return (ulonglong)(uint)fVar5;
    }
    return 0;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar6 = (*pcVar2)();
  return uVar6;
}



/* ==== UnitCtrl::GetAuraEffects  @ 0x8dbb60  size=8 ==== */

undefined8 UnitCtrl::GetAuraEffects(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x330);
}



/* ==== UnitCtrl::GetBattleCategory  @ 0x15f9780  size=1 ==== */

ulonglong UnitCtrl::GetBattleCategory(void)

{
  longlong lVar1;
  code *pcVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 != 0) {
    return (ulonglong)*(uint *)(lVar1 + 0x2c0);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetBattleDeltaTime  @ 0x15f97d0  size=1 ==== */

ulonglong UnitCtrl::GetBattleDeltaTime(void)

{
  longlong lVar1;
  code *pcVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 != 0) {
    return (ulonglong)*(uint *)(lVar1 + 0x2f0);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetBattleFrameCount  @ 0x15f9820  size=1 ==== */

void UnitCtrl::GetBattleFrameCount(void)

{
  undefined8 uVar1;
  undefined4 uVar2;
  longlong lVar3;
  code *pcVar4;
  undefined8 uStack_18;
  undefined4 uStack_10;
  
  if (cRam000000018ab63065 == '\0') {
    sub_8032f6d0(0x187b60fa0);
    cRam000000018ab63065 = '\x01';
  }
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar3 != 0) {
    uVar1 = *(undefined8 *)(lVar3 + 0x210);
    uVar2 = *(undefined4 *)(lVar3 + 0x218);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_18 = uVar1;
    uStack_10 = uVar2;
    ObscuredInt::FHOALPFLEEC(&uStack_18,0);
    return;
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitCtrl::GetBattleResult  @ 0x15f9920  size=1 ==== */

ulonglong UnitCtrl::GetBattleResult(void)

{
  longlong lVar1;
  code *pcVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 != 0) {
    return (ulonglong)*(uint *)(lVar1 + 0x300);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetBattleResultCameraScale  @ 0x15f98d0  size=1 ==== */

void UnitCtrl::GetBattleResultCameraScale(void)

{
  longlong lVar1;
  longlong *plVar2;
  code *pcVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 != 0) {
    plVar2 = *(longlong **)(lVar1 + 0x520);
    if (plVar2 != (longlong *)0x0) {
                    /* WARNING: Could not recover jumptable at 0x000183712d65. Too many branches */
                    /* WARNING: Treating indirect jump as call */
      (**(code **)(*plVar2 + 0x1e8))(plVar2,*(undefined8 *)(*plVar2 + 0x1f0));
      return;
    }
    sub_8032f9d0(0,0);
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitCtrl::GetCurrentSpineCtrl  @ 0x15f9970  size=1 ==== */

undefined8 UnitCtrl::GetCurrentSpineCtrl(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  longlong lVar3;
  undefined8 uVar4;
  
  if (cRam000000018ab63047 == '\0') {
    sub_8032f6d0(0x187b9aea8);
    sub_8032f6d0(0x187b9b058);
    cRam000000018ab63047 = '\x01';
  }
  lVar3 = *(longlong *)(param_1 + 0x38);
  if ((lVar3 != 0) && (lVar1 = *(longlong *)(lVar3 + 0x298), lVar1 != 0)) {
    if (*(int *)(lVar1 + 0x18) < 1) {
      if (cRam000000018ab6303d == '\0') {
        sub_8032f6d0(0x187b9aea8);
        cRam000000018ab6303d = '\x01';
      }
      if ((*(longlong *)(param_1 + 0x38) != 0) &&
         (lVar3 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar3 != 0)) {
        if ((*(int *)(lVar3 + 0x18) < 1) && (*(int *)(param_1 + 0x1a0) != -1)) {
          return *(undefined8 *)(param_1 + 0x138);
        }
        return *(undefined8 *)(param_1 + 0x128);
      }
    }
    else if ((lVar3 != 0) && (lVar1 != 0)) {
      lVar3 = sub_849ed2d0(lVar1,0,uRam0000000187b9b058);
      if (lVar3 != 0) {
        return *(undefined8 *)(lVar3 + 0x18);
      }
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar4 = (*pcVar2)();
  return uVar4;
}



/* ==== UnitCtrl::GetCurrentZoom  @ 0x15f9a50  size=1 ==== */

void UnitCtrl::GetCurrentZoom(void)

{
  longlong lVar1;
  longlong *plVar2;
  code *pcVar3;
  undefined8 *puVar4;
  ushort uVar5;
  ulonglong uVar6;
  
  if (cRam000000018ab63064 == '\0') {
    sub_8032f6d0(0x187b8ac68);
    cRam000000018ab63064 = '\x01';
  }
  if (cRam000000018ab6307c == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307c = '\x01';
  }
  plVar2 = *(longlong **)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x10);
  if (plVar2 == (longlong *)0x0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  lVar1 = *plVar2;
  uVar6 = 0;
  if (*(ushort *)(lVar1 + 0x12e) != 0) {
    do {
      if (*(longlong *)(*(longlong *)(lVar1 + 0xb0) + uVar6 * 0x10) == lRam0000000187b8ac68) {
        puVar4 = (undefined8 *)
                 ((longlong)*(int *)(*(longlong *)(lVar1 + 0xb0) + 8 + uVar6 * 0x10) * 0x10 + 0x138
                 + lVar1);
        goto code_r0x000180003ba3;
      }
      uVar5 = (short)uVar6 + 1;
      uVar6 = (ulonglong)uVar5;
    } while (uVar5 < *(ushort *)(lVar1 + 0x12e));
  }
  puVar4 = (undefined8 *)sub_8032c930(plVar2,lRam0000000187b8ac68,0);
code_r0x000180003ba3:
                    /* WARNING: Could not recover jumptable at 0x000180003bb7. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  (*(code *)*puVar4)(plVar2,puVar4[1],(code *)*puVar4);
  return;
}



/* ==== UnitCtrl::GetDieEffects  @ 0x6c3c70  size=8 ==== */

undefined8 UnitCtrl::GetDieEffects(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x300);
}



/* ==== UnitCtrl::GetEffect  @ 0x15f9ac0  size=1 ==== */

void UnitCtrl::GetEffect(undefined8 param_1,undefined8 param_2,undefined8 param_3)

{
  longlong lVar1;
  code *pcVar2;
  
  if (cRam000000018ab6306d == '\0') {
    sub_8032f6d0(0x187b8ae18);
    cRam000000018ab6306d = '\x01';
  }
  if (cRam000000018ab6307a == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307a = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x18);
  if (lVar1 != 0) {
    sub_800121c0(0,uRam0000000187b8ae18,lVar1,param_2,param_3);
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::GetEnemyList  @ 0x15f9b50  size=1 ==== */

undefined8 UnitCtrl::GetEnemyList(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  undefined8 uVar3;
  
  if (*(char *)(param_1 + 0x178) == '\0') {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar1 != 0) {
      return *(undefined8 *)(lVar1 + 0x248);
    }
  }
  else {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar1 != 0) {
      return *(undefined8 *)(lVar1 + 0x240);
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetFriendList  @ 0x15f9bf0  size=1 ==== */

undefined8 UnitCtrl::GetFriendList(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  undefined8 uVar3;
  
  if (*(char *)(param_1 + 0x178) == '\0') {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar1 != 0) {
      return *(undefined8 *)(lVar1 + 0x240);
    }
  }
  else {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar1 != 0) {
      return *(undefined8 *)(lVar1 + 0x248);
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetGameStartEffects  @ 0x8dbae0  size=8 ==== */

undefined8 UnitCtrl::GetGameStartEffects(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x2f8);
}



/* ==== UnitCtrl::GetIsMuteVoice  @ 0x15f9c90  size=1 ==== */

ulonglong UnitCtrl::GetIsMuteVoice(void)

{
  longlong lVar1;
  code *pcVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 != 0) {
    return (ulonglong)*(byte *)(lVar1 + 0x5e0);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetIsPrincessFormUnionBurstRunning  @ 0x15f9ce0  size=1 ==== */

ulonglong UnitCtrl::GetIsPrincessFormUnionBurstRunning(void)

{
  longlong lVar1;
  code *pcVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 != 0) {
    if (*(char *)(lVar1 + 0x382) != '\0') {
      return CONCAT71((int7)((ulonglong)*(longlong *)(lRam0000000187bfb7a8 + 0xb8) >> 8),1);
    }
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar1 != 0) {
      return (ulonglong)*(byte *)(lVar1 + 0x31b);
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetPassiveDictionaryByEnemy  @ 0x15f9d70  size=1 ==== */

undefined8 UnitCtrl::GetPassiveDictionaryByEnemy(longlong param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4)

{
  char cVar1;
  uint uVar2;
  longlong lVar3;
  code *pcVar4;
  undefined8 uVar5;
  longlong lVar6;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar3 == 0) {
    sub_8032f9d0();
    pcVar4 = (code *)swi(3);
    uVar5 = (*pcVar4)();
    return uVar5;
  }
  cVar1 = *(char *)(param_1 + 0x178);
  if (cRam000000018ab76239 == '\0') {
    sub_8032f6d0(0x187c4ed80,cVar1,0);
    cRam000000018ab76239 = '\x01';
  }
  if (cVar1 == '\0') {
    lVar6 = *(longlong *)(lVar3 + 0x4d0);
  }
  else {
    lVar6 = *(longlong *)(lVar3 + 0x4c8);
  }
  if (lVar6 != 0) {
    uVar2 = *(uint *)(lVar3 + 0x22c);
    if (*(uint *)(lVar6 + 0x18) <= uVar2) {
      ThrowHelper::ThrowArgumentOutOfRange_IndexException(0,uVar2,uRam0000000187c4ed80,param_4,unaff_RDI);
    }
    lVar3 = *(longlong *)(lVar6 + 0x10);
    if (lVar3 != 0) {
      if (uVar2 < *(uint *)(lVar3 + 0x18)) {
        return *(undefined8 *)(lVar3 + 0x20 + (longlong)(int)uVar2 * 8);
      }
      sub_8032f9c0();
      pcVar4 = (code *)swi(3);
      uVar5 = (*pcVar4)();
      return uVar5;
    }
    sub_8032f9d0();
    pcVar4 = (code *)swi(3);
    uVar5 = (*pcVar4)();
    return uVar5;
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  uVar5 = (*pcVar4)();
  return uVar5;
}



/* ==== UnitCtrl::GetPassiveDictionaryByUnit  @ 0x15f9dd0  size=1 ==== */

undefined8 UnitCtrl::GetPassiveDictionaryByUnit(longlong param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4)

{
  char cVar1;
  code *pcVar2;
  undefined8 uVar3;
  longlong lVar4;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar4 == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    uVar3 = (*pcVar2)();
    return uVar3;
  }
  cVar1 = *(char *)(param_1 + 0x178);
  if (cRam000000018ab7623a == '\0') {
    sub_8032f6d0(0x187c4ed80,cVar1,0);
    cRam000000018ab7623a = '\x01';
  }
  if (cVar1 == '\0') {
    lVar4 = *(longlong *)(lVar4 + 0x4b8);
  }
  else {
    lVar4 = *(longlong *)(lVar4 + 0x4c0);
  }
  if (lVar4 != 0) {
    if (*(int *)(lVar4 + 0x18) == 0) {
      ThrowHelper::ThrowArgumentOutOfRange_IndexException(0,0,uRam0000000187c4ed80,param_4,unaff_RDI);
    }
    lVar4 = *(longlong *)(lVar4 + 0x10);
    if (lVar4 != 0) {
      if (*(int *)(lVar4 + 0x18) != 0) {
        return *(undefined8 *)(lVar4 + 0x20);
      }
      sub_8032f9c0();
      pcVar2 = (code *)swi(3);
      uVar3 = (*pcVar2)();
      return uVar3;
    }
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    uVar3 = (*pcVar2)();
    return uVar3;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::GetSortOrderConsiderBlackout  @ 0x15f9e30  size=1 ==== */

ulonglong UnitCtrl::GetSortOrderConsiderBlackout(longlong param_1)

{
  uint uVar1;
  longlong lVar2;
  longlong lVar3;
  code *pcVar4;
  uint uVar5;
  char cVar6;
  ulonglong uVar7;
  
  lVar2 = *(longlong *)(param_1 + 0xc0);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar3 != 0) {
    cVar6 = BattleManager::IsSkillExeUnit(lVar3,param_1,0);
    if (lVar2 != 0) {
      uVar1 = *(uint *)(lVar2 + 0x20);
      uVar5 = uVar1 - 0x2cec;
      if (cVar6 == '\0') {
        uVar5 = uVar1;
      }
      return (ulonglong)uVar5;
    }
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  uVar7 = (*pcVar4)();
  return uVar7;
}



/* ==== UnitCtrl::GetSpeedUpFlag  @ 0x15f9eb0  size=1 ==== */

void UnitCtrl::GetSpeedUpFlag(void)

{
  longlong lVar1;
  code *UNRECOVERED_JUMPTABLE;
  longlong *plVar2;
  ushort uVar3;
  undefined8 *puVar4;
  
  if (cRam000000018ab63070 == '\0') {
    sub_8032f6d0(0x187b94218);
    cRam000000018ab63070 = '\x01';
  }
  if (cRam000000018ab6307b == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307b = '\x01';
  }
  plVar2 = *(longlong **)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x20);
  if (plVar2 != (longlong *)0x0) {
    uVar3 = 0;
    lVar1 = *plVar2;
    if (*(ushort *)(lVar1 + 0x12e) != 0) {
      do {
        if (*(longlong *)(*(longlong *)(lVar1 + 0xb0) + (ulonglong)uVar3 * 0x10) ==
            lRam0000000187b94218) {
          puVar4 = (undefined8 *)
                   ((longlong)*(int *)(*(longlong *)(lVar1 + 0xb0) + 8 + (ulonglong)uVar3 * 0x10) *
                    0x10 + 0x138 + lVar1);
          UNRECOVERED_JUMPTABLE = (code *)*puVar4;
                    /* WARNING: Could not recover jumptable at 0x0001800022fb. Too many branches */
                    /* WARNING: Treating indirect jump as call */
          (*UNRECOVERED_JUMPTABLE)(plVar2,puVar4[1],UNRECOVERED_JUMPTABLE);
          return;
        }
        uVar3 = uVar3 + 1;
      } while (uVar3 < *(ushort *)(lVar1 + 0x12e));
    }
    puVar4 = (undefined8 *)sub_8032c930(plVar2);
                    /* WARNING: Could not recover jumptable at 0x0001800022cc. Too many branches */
                    /* WARNING: Treating indirect jump as call */
    (*(code *)*puVar4)(plVar2,puVar4[1],(code *)*puVar4);
    return;
  }
  sub_8032f9d0();
  UNRECOVERED_JUMPTABLE = (code *)swi(3);
  (*UNRECOVERED_JUMPTABLE)();
  return;
}



/* ==== UnitCtrl::HideLifeGauge  @ 0x15f9f20  size=1 ==== */

void UnitCtrl::HideLifeGauge(longlong param_1)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  
  if (cRam000000018ab6304e == '\0') {
    sub_8032f6d0(0x187c38198);
    cRam000000018ab6304e = '\x01';
  }
  lVar3 = *(longlong *)(param_1 + 0x268);
  if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (cRam000000018ab7b011 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab7b011 = '\x01';
  }
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar2 = Object::op_Equality(lVar3,0,0);
  if (cVar2 != '\0') {
    return;
  }
  if ((lVar3 != 0) && (lVar3 = Component::get_gameObject(lVar3,0), lVar3 != 0)) {
    GameObject::SetActive(lVar3,0,0);
    return;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitCtrl::IndicateSkillName  @ 0x15f9f80  size=1 ==== */

void UnitCtrl::IndicateSkillName(longlong param_1,longlong param_2)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  longlong *plVar5;
  longlong lVar6;
  undefined8 uVar7;
  
  lVar1 = *(longlong *)(param_1 + 0x268);
  if (lVar1 == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  if (cRam000000018ab62656 == '\0') {
    sub_8032f6d0(0x187b90838,param_2,0);
    sub_8032f6d0(0x187b73698);
    sub_8032f6d0(0x187bf7ae8);
    cRam000000018ab62656 = '\x01';
  }
  cVar3 = String::IsNullOrEmpty(param_2,0);
  if (cVar3 != '\0') {
    return;
  }
  if ((*(longlong *)(lVar1 + 0x60) != 0) &&
     (lVar4 = *(longlong *)(*(longlong *)(lVar1 + 0x60) + 0xc0), lVar4 != 0)) {
    if (*(char *)(lVar4 + 0x18) != '\0') {
      if (cRam000000018ab626d9 == '\0') {
        sub_8032f6d0(0x187c29df8);
        cRam000000018ab626d9 = '\x01';
      }
      if (*(int *)(lRam0000000187c29df8 + 0xe4) == 0) {
        sub_8032fa70(lRam0000000187c29df8);
      }
      if ((*(longlong *)(*(longlong *)(lRam0000000187c29df8 + 0xb8) + 8) == 0) ||
         (lVar4 = sub_80002270(4,uRam0000000187b90838), lVar4 == 0)) goto DAT_1814b00b4;
      if (*(int *)(lVar4 + 0x18) != 0) {
        return;
      }
    }
    lVar4 = Component::get_gameObject(lVar1,0);
    if (lVar4 != 0) {
      cVar3 = GameObject::get_activeSelf(lVar4,0);
      if (cVar3 == '\0') {
        lVar4 = Component::get_gameObject(lVar1,0);
        if (lVar4 == 0) goto DAT_1814b00b4;
        GameObject::SetActive(lVar4,1,0);
      }
      lVar4 = *(longlong *)(lVar1 + 0x50);
      plVar5 = (longlong *)sub_8032e960(uRam0000000187bf7ae8,1);
      if (plVar5 != (longlong *)0x0) {
        if ((param_2 != 0) &&
           (lVar6 = sub_8032e8f0(param_2,*(undefined8 *)(*plVar5 + 0x40)), lVar6 == 0)) {
          uVar7 = sub_8032f220();
          sub_8032f990(uVar7,0);
          pcVar2 = (code *)swi(3);
          (*pcVar2)();
          return;
        }
        if ((int)plVar5[3] == 0) {
          sub_8032f9c0();
          pcVar2 = (code *)swi(3);
          (*pcVar2)();
          return;
        }
        plVar5[4] = param_2;
        sub_8032e8d0(plVar5 + 4,param_2);
        if (lVar4 != 0) {
          CustomUILabel::SetText(lVar4,0x41756681,plVar5,0);
          *(undefined1 *)(lVar1 + 0x6d) = 1;
          if ((((*(longlong *)(lVar1 + 0x48) != 0) &&
               (lVar4 = Component::get_gameObject(*(longlong *)(lVar1 + 0x48),0), lVar4 != 0)) &&
              (GameObject::SetActive(lVar4,1,0), param_2 != 0)) && (*(longlong *)(lVar1 + 0x48) != 0)) {
            UIWidget::set_width(*(longlong *)(lVar1 + 0x48),*(int *)(param_2 + 0x10) * 0x12 + 0x26,0);
            if (cRam000000018ab626d9 == '\0') {
              sub_8032f6d0(0x187c29df8);
              cRam000000018ab626d9 = '\x01';
            }
            if (*(int *)(lRam0000000187c29df8 + 0xe4) == 0) {
              sub_8032fa70(lRam0000000187c29df8);
            }
            lVar4 = *(longlong *)(*(longlong *)(lRam0000000187c29df8 + 0xb8) + 8);
            if (cRam000000018ab62657 == '\0') {
              sub_8032f6d0(0x187c3c890);
              cRam000000018ab62657 = '\x01';
            }
            lVar6 = sub_8032f970(uRam0000000187c3c890);
            sub_80443e50(lVar6,0);
            *(undefined4 *)(lVar6 + 0x10) = 0;
            *(longlong *)(lVar6 + 0x28) = lVar1;
            sub_8032e8d0(lVar6 + 0x28,lVar1);
            *(longlong *)(lVar6 + 0x20) = param_2;
            sub_8032e8d0(lVar6 + 0x20,param_2);
            if (lVar4 != 0) {
              sub_800110c0(2,uRam0000000187b90838,lVar4,lVar6,2,0);
              return;
            }
          }
        }
      }
    }
  }
DAT_1814b00b4:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::Initialize  @ 0x15f9fb0  size=1 ==== */

void UnitCtrl::Initialize(longlong param_1,undefined8 param_2,char param_3,undefined8 param_4,
                  undefined1 param_5,longlong param_6,undefined8 param_7,undefined4 param_8,
                  int param_9)

{
  int *piVar1;
  longlong lVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  code *pcVar8;
  undefined8 uVar9;
  undefined8 uVar10;
  undefined8 uVar11;
  undefined8 uVar12;
  undefined8 uVar13;
  undefined8 uVar14;
  longlong lVar15;
  undefined1 uVar16;
  char cVar17;
  char cVar18;
  undefined4 uVar19;
  int iVar20;
  undefined4 uVar21;
  undefined4 uVar22;
  int iVar23;
  undefined4 uVar24;
  longlong lVar25;
  undefined8 uVar26;
  undefined8 uVar27;
  undefined8 uVar28;
  longlong lVar29;
  undefined8 *puVar30;
  longlong lVar31;
  longlong lVar32;
  ulonglong uVar33;
  longlong *plVar34;
  float *pfVar35;
  undefined1 *puVar36;
  uint uVar37;
  longlong *plVar38;
  longlong *plVar39;
  bool bVar40;
  bool bVar41;
  float fVar42;
  float fVar43;
  double dVar44;
  float fVar45;
  undefined8 *puVar46;
  ulonglong uVar47;
  undefined8 *puVar48;
  ulonglong uVar49;
  undefined8 *puVar50;
  undefined8 *puVar51;
  longlong lStack_e68;
  undefined4 uStack_e60;
  longlong lStack_e58;
  byte bStack_e50;
  longlong lStack_e48;
  ulonglong uStack_e40;
  ulonglong uStack_e38;
  longlong lStack_e28;
  undefined4 uStack_e20;
  undefined8 uStack_e18;
  undefined8 *puStack_e10;
  undefined8 uStack_e08;
  longlong lStack_df8;
  longlong lStack_df0;
  longlong lStack_de8;
  undefined8 uStack_dd8;
  undefined8 *puStack_dd0;
  undefined8 uStack_dc8;
  undefined8 uStack_dc0;
  undefined8 uStack_db8;
  undefined8 uStack_db0;
  undefined8 uStack_da8;
  undefined4 uStack_da0;
  undefined8 uStack_d98;
  undefined4 uStack_d90;
  undefined8 uStack_d88;
  undefined4 uStack_d80;
  undefined8 uStack_d78;
  undefined8 uStack_d70;
  longlong lStack_d68;
  int aiStack_d58 [2];
  longlong lStack_d50;
  longlong lStack_d48;
  longlong lStack_d40;
  undefined8 uStack_d38;
  undefined8 uStack_d30;
  undefined8 uStack_d28;
  undefined8 uStack_d20;
  undefined8 uStack_d18;
  undefined4 uStack_d10;
  undefined8 uStack_d08;
  undefined4 uStack_d00;
  undefined8 uStack_cf8;
  undefined4 uStack_cf0;
  undefined8 uStack_ce8;
  undefined4 uStack_ce0;
  undefined8 uStack_cd8;
  undefined4 uStack_cd0;
  undefined8 uStack_cc8;
  undefined8 uStack_cc0;
  longlong lStack_cb8;
  undefined8 uStack_cb0;
  undefined8 uStack_ca8;
  longlong lStack_ca0;
  undefined8 uStack_c98;
  undefined8 uStack_c90;
  ulonglong uStack_c88;
  undefined8 uStack_c80;
  undefined8 uStack_c78;
  undefined8 uStack_c70;
  undefined8 uStack_c68;
  undefined8 uStack_c60;
  undefined8 uStack_c58;
  undefined8 uStack_c50;
  undefined8 uStack_c48;
  undefined8 uStack_c40;
  undefined8 uStack_c38;
  undefined8 uStack_c30;
  undefined8 uStack_c28;
  undefined8 uStack_c20;
  undefined8 uStack_c18;
  undefined8 uStack_c10;
  undefined8 uStack_c08;
  undefined8 uStack_c00;
  undefined8 uStack_bf8;
  undefined8 uStack_bf0;
  undefined8 uStack_be8;
  undefined8 uStack_be0;
  undefined8 uStack_bd8;
  undefined8 uStack_bd0;
  undefined8 uStack_bc8;
  undefined8 uStack_bc0;
  undefined8 uStack_bb8;
  undefined8 uStack_ba8;
  undefined8 uStack_ba0;
  undefined8 uStack_b98;
  undefined8 uStack_b88;
  undefined8 uStack_b80;
  undefined8 uStack_b78;
  undefined8 uStack_b68;
  undefined8 uStack_b60;
  undefined8 uStack_b58;
  undefined8 uStack_b48;
  undefined8 uStack_b40;
  longlong lStack_b38;
  undefined8 uStack_b28;
  undefined8 uStack_b20;
  longlong lStack_b18;
  undefined8 uStack_b08;
  undefined8 uStack_b00;
  longlong lStack_af8;
  undefined8 uStack_ae8;
  undefined8 uStack_ae0;
  longlong lStack_ad8;
  undefined8 uStack_ac8;
  undefined8 uStack_ac0;
  longlong lStack_ab8;
  undefined8 uStack_aa8;
  undefined8 uStack_aa0;
  ulonglong uStack_a98;
  undefined8 uStack_a88;
  undefined8 uStack_a80;
  ulonglong uStack_a78;
  undefined8 uStack_a68;
  undefined8 uStack_a60;
  undefined8 uStack_a58;
  undefined8 uStack_a48;
  undefined8 uStack_a40;
  undefined8 uStack_a38;
  undefined8 uStack_a28;
  undefined8 uStack_a20;
  undefined8 uStack_a18;
  undefined8 uStack_a08;
  undefined8 uStack_a00;
  undefined8 uStack_9f8;
  undefined8 uStack_9e8;
  undefined8 uStack_9e0;
  undefined8 uStack_9d8;
  undefined8 uStack_9c8;
  undefined8 uStack_9c0;
  undefined8 uStack_9b8;
  undefined8 uStack_9a8;
  undefined8 uStack_9a0;
  undefined8 uStack_998;
  undefined8 uStack_988;
  undefined8 uStack_980;
  undefined8 uStack_978;
  undefined8 uStack_968;
  undefined8 uStack_960;
  undefined8 uStack_958;
  undefined8 uStack_948;
  undefined8 uStack_940;
  undefined8 uStack_938;
  undefined8 uStack_928;
  undefined8 uStack_920;
  undefined8 uStack_918;
  undefined8 uStack_908;
  undefined8 uStack_900;
  undefined8 uStack_8f8;
  undefined8 uStack_8e8;
  undefined8 uStack_8e0;
  undefined8 uStack_8d8;
  undefined8 uStack_8c8;
  undefined8 uStack_8c0;
  undefined8 uStack_8b8;
  undefined8 uStack_8a8;
  undefined8 uStack_8a0;
  undefined8 uStack_898;
  undefined8 uStack_888;
  undefined8 uStack_880;
  undefined8 uStack_878;
  undefined8 uStack_868;
  undefined8 uStack_860;
  undefined8 uStack_858;
  undefined8 uStack_848;
  undefined8 uStack_840;
  undefined8 uStack_838;
  undefined8 uStack_828;
  undefined8 uStack_820;
  undefined8 uStack_818;
  undefined8 uStack_808;
  undefined8 uStack_800;
  undefined8 uStack_7f8;
  undefined8 uStack_7e8;
  undefined8 uStack_7e0;
  undefined8 uStack_7d8;
  undefined8 uStack_7c8;
  undefined8 uStack_7c0;
  undefined8 uStack_7b8;
  undefined8 uStack_7a8;
  undefined8 uStack_7a0;
  undefined8 uStack_798;
  undefined8 uStack_788;
  undefined8 uStack_780;
  undefined8 uStack_778;
  undefined8 uStack_768;
  undefined8 uStack_760;
  undefined8 uStack_758;
  undefined8 uStack_748;
  undefined8 uStack_740;
  undefined8 uStack_718;
  undefined8 uStack_710;
  longlong lStack_708;
  undefined8 uStack_6f8;
  undefined8 uStack_6f0;
  undefined8 uStack_6e8;
  undefined8 uStack_6d8;
  undefined8 uStack_6d0;
  undefined8 uStack_6c8;
  undefined8 uStack_6b8;
  undefined8 uStack_6b0;
  undefined8 uStack_6a8;
  undefined8 uStack_6a0;
  undefined8 uStack_698;
  longlong lStack_690;
  undefined8 uStack_688;
  undefined8 uStack_680;
  ulonglong uStack_678;
  undefined1 auStack_670 [16];
  undefined1 auStack_660 [16];
  undefined1 auStack_650 [16];
  undefined1 auStack_640 [16];
  undefined1 auStack_630 [16];
  undefined1 auStack_620 [16];
  undefined1 auStack_610 [24];
  undefined1 auStack_5f8 [24];
  undefined1 auStack_5e0 [24];
  undefined1 auStack_5c8 [24];
  undefined1 auStack_5b0 [24];
  undefined1 auStack_598 [24];
  undefined1 auStack_580 [24];
  undefined1 auStack_568 [24];
  undefined1 auStack_550 [24];
  undefined1 auStack_538 [24];
  undefined1 auStack_520 [24];
  undefined1 auStack_508 [24];
  undefined1 auStack_4f0 [24];
  undefined1 auStack_4d8 [24];
  undefined1 auStack_4c0 [24];
  undefined1 auStack_4a8 [24];
  undefined1 auStack_490 [24];
  undefined1 auStack_478 [24];
  undefined1 auStack_460 [24];
  undefined1 auStack_448 [24];
  undefined1 auStack_430 [24];
  undefined1 auStack_418 [24];
  undefined1 auStack_400 [24];
  undefined1 auStack_3e8 [24];
  undefined1 auStack_3d0 [24];
  undefined1 auStack_3b8 [24];
  undefined1 auStack_3a0 [24];
  undefined1 auStack_388 [24];
  undefined1 auStack_370 [24];
  undefined1 auStack_358 [24];
  undefined1 auStack_340 [24];
  undefined1 auStack_328 [24];
  undefined1 auStack_310 [24];
  undefined1 auStack_2f8 [24];
  undefined1 auStack_2e0 [24];
  undefined1 auStack_2c8 [24];
  undefined1 auStack_2b0 [24];
  undefined1 auStack_298 [24];
  undefined1 auStack_280 [24];
  undefined1 auStack_268 [24];
  undefined1 auStack_250 [24];
  undefined1 auStack_238 [24];
  undefined1 auStack_220 [24];
  undefined1 auStack_208 [24];
  undefined1 auStack_1f0 [32];
  undefined1 auStack_1d0 [32];
  undefined1 auStack_1b0 [32];
  undefined1 auStack_190 [32];
  undefined1 auStack_170 [32];
  undefined1 auStack_150 [32];
  undefined1 auStack_130 [32];
  undefined1 auStack_110 [32];
  undefined1 auStack_f0 [200];
  
  lStack_df8 = param_1;
  lStack_de8 = param_1;
  lStack_d50 = param_1;
  if (cRam000000018ab6304b == '\0') {
    sub_8032f6d0(0x187c25398);
    sub_8032f6d0(0x187c25dd0);
    sub_8032f6d0(0x187c35268);
    sub_8032f6d0(0x187b6ed88);
    sub_8032f6d0(0x187b35790);
    sub_8032f6d0(0x187b3c490);
    sub_8032f6d0(0x187b3e4f0);
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187b634d8);
    sub_8032f6d0(0x187b440e8);
    sub_8032f6d0(0x187b541f8);
    sub_8032f6d0(0x187b6b178);
    sub_8032f6d0(0x187b6bd98);
    sub_8032f6d0(0x187b6c2d0);
    sub_8032f6d0(0x187b93750);
    sub_8032f6d0(0x187c044b8);
    sub_8032f6d0(0x187bc6f78);
    sub_8032f6d0(0x187bc83e8);
    sub_8032f6d0(0x187c1c530);
    sub_8032f6d0(0x187c409e0);
    sub_8032f6d0(0x187c1cf68);
    sub_8032f6d0(0x187b74b50);
    sub_8032f6d0(0x187bea0a0);
    sub_8032f6d0(0x187c12358);
    sub_8032f6d0(0x187b5c128);
    sub_8032f6d0(0x187b33d98);
    sub_8032f6d0(0x187b3b288);
    sub_8032f6d0(0x187b70728);
    sub_8032f6d0(0x187b3b438);
    sub_8032f6d0(0x187b5c2e8);
    sub_8032f6d0(0x187b33f48);
    sub_8032f6d0(0x187b708e8);
    sub_8032f6d0(0x187b70aa0);
    sub_8032f6d0(0x187b5c4a0);
    sub_8032f6d0(0x187b34100);
    sub_8032f6d0(0x187b3b5f0);
    sub_8032f6d0(0x187b31568);
    sub_8032f6d0(0x187b8d958);
    sub_8032f6d0(0x187b8db08);
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187b9f600);
    sub_8032f6d0(0x187b9f298);
    sub_8032f6d0(0x187c010f0);
    sub_8032f6d0(0x187b9bf20);
    sub_8032f6d0(0x187b472d8);
    sub_8032f6d0(0x187bc3418);
    sub_8032f6d0(0x187bc4f58);
    sub_8032f6d0(0x187bfd360);
    sub_8032f6d0(0x187b74fd8);
    sub_8032f6d0(0x187b8afd0);
    sub_8032f6d0(0x187b9d7f8);
    sub_8032f6d0(0x187ba6038);
    sub_8032f6d0(0x187bf3230);
    sub_8032f6d0(0x187b418c0);
    sub_8032f6d0(0x187b4ddb8);
    sub_8032f6d0(0x187b9e258);
    sub_8032f6d0(0x187c318b8);
    sub_8032f6d0(0x187c37668);
    sub_8032f6d0(0x187c32d28);
    sub_8032f6d0(0x187beb698);
    sub_8032f6d0(0x187c379d0);
    sub_8032f6d0(0x187beb850);
    sub_8032f6d0(0x187c4c5c8);
    sub_8032f6d0(0x187b42d30);
    sub_8032f6d0(0x187bebbc0);
    sub_8032f6d0(0x187b413a0);
    sub_8032f6d0(0x187c15698);
    sub_8032f6d0(0x187c4afa8);
    sub_8032f6d0(0x187c31708);
    sub_8032f6d0(0x187c329c0);
    sub_8032f6d0(0x187c33918);
    sub_8032f6d0(0x187b9ead8);
    sub_8032f6d0(0x187c160d0);
    sub_8032f6d0(0x187b7a0f0);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187b7a2a8);
    sub_8032f6d0(0x187c16280);
    sub_8032f6d0(0x187b9ec90);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187c33ac8);
    sub_8032f6d0(0x187bf1860);
    sub_8032f6d0(0x187be1690);
    sub_8032f6d0(0x187c20d30);
    sub_8032f6d0(0x187c20ee0);
    sub_8032f6d0(0x187c25b88);
    sub_8032f6d0(0x187c361d0);
    sub_8032f6d0(0x187c366e8);
    sub_8032f6d0(0x187c36f70);
    sub_8032f6d0(0x187c38fc8);
    sub_8032f6d0(0x187c3d070);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b3d4a8);
    sub_8032f6d0(0x187c440d8);
    sub_8032f6d0(0x187b45b98);
    sub_8032f6d0(0x187b572d0);
    sub_8032f6d0(0x187b57638);
    sub_8032f6d0(0x187b5ca48);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b60538);
    sub_8032f6d0(0x187b60c28);
    sub_8032f6d0(0x187b60de8);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187b61160);
    sub_8032f6d0(0x187b6cae8);
    sub_8032f6d0(0x187bd11f0);
    sub_8032f6d0(0x187b93508);
    sub_8032f6d0(0x187b90710);
    sub_8032f6d0(0x187bf5308);
    sub_8032f6d0(0x187bf54c0);
    sub_8032f6d0(0x187bf5678);
    sub_8032f6d0(0x187bf5830);
    sub_8032f6d0(0x187bf59e8);
    sub_8032f6d0(0x187bf5ba0);
    sub_8032f6d0(0x187bf5f10);
    sub_8032f6d0(0x187c16f00);
    sub_8032f6d0(0x187c0f7d0);
    sub_8032f6d0(0x187c0c990);
    sub_8032f6d0(0x187b78350);
    sub_8032f6d0(0x187bd45d8);
    sub_8032f6d0(0x187bd4790);
    sub_8032f6d0(0x187bd4948);
    sub_8032f6d0(0x187c22520);
    sub_8032f6d0(0x187bd4e70);
    sub_8032f6d0(0x187bd5028);
    sub_8032f6d0(0x187c22a38);
    sub_8032f6d0(0x187c22368);
    sub_8032f6d0(0x187bfb280);
    sub_8032f6d0(0x187b308f8);
    sub_8032f6d0(0x187b30aa8);
    sub_8032f6d0(0x187bfb7a8);
    sub_8032f6d0(0x187c05928);
    sub_8032f6d0(0x187b8ef70);
    sub_8032f6d0(0x187b9e658);
    sub_8032f6d0(0x187c16868);
    sub_8032f6d0(0x187b46750);
    sub_8032f6d0(0x187c166b8);
    cRam000000018ab6304b = '\x01';
  }
  uStack_cb0 = 0;
  uStack_ca8 = 0;
  lStack_ca0 = 0;
  uStack_c80 = 0;
  uStack_c78 = 0;
  uStack_c70 = 0;
  uStack_cc8 = 0;
  uStack_cc0 = 0;
  lStack_cb8 = 0;
  uStack_c98 = 0;
  uStack_c90 = 0;
  uStack_c88 = 0;
  lVar25 = sub_8032f970(uRam0000000187c22520);
  lStack_df0 = lVar25;
  sub_804383d0(lVar25,0);
  lStack_d48 = lVar25;
  if (lVar25 == 0) goto code_r0x000181600f6f;
  *(longlong *)(lVar25 + 0x10) = param_1;
  sub_8032e8d0(lVar25 + 0x10,param_1);
  *(undefined8 *)(lVar25 + 0x20) = param_2;
  sub_8032e8d0(lVar25 + 0x20,param_2);
  uVar26 = sub_80003d20(uRam0000000187b3c708);
  *(undefined8 *)(lVar25 + 0x28) = uVar26;
  sub_8032e8d0(lVar25 + 0x28,uVar26);
  if (**(longlong **)(lRam0000000187bfb7a8 + 0xb8) == 0) {
    uVar26 = sub_847c7d00(param_1,uRam0000000187bfd360);
    **(undefined8 **)(lRam0000000187bfb7a8 + 0xb8) = uVar26;
    sub_8032e8d0(*(undefined8 *)(lRam0000000187bfb7a8 + 0xb8),uVar26);
    if (cRam000000018ab63078 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63078 = '\x01';
    }
    if (**(longlong **)(lRam0000000187bfb7a8 + 0xb8) == 0) goto code_r0x000181600f6f;
    uVar26 = sub_84670860(**(longlong **)(lRam0000000187bfb7a8 + 0xb8),uRam0000000187bf5830);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28) = uVar26;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28,uVar26);
    if (cRam000000018ab63078 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63078 = '\x01';
    }
    if (**(longlong **)(lRam0000000187bfb7a8 + 0xb8) == 0) goto code_r0x000181600f6f;
    uVar26 = sub_84670860(**(longlong **)(lRam0000000187bfb7a8 + 0xb8),uRam0000000187bf5678);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8) = uVar26;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8,uVar26);
    if (cRam000000018ab63078 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63078 = '\x01';
    }
    if (**(longlong **)(lRam0000000187bfb7a8 + 0xb8) == 0) goto code_r0x000181600f6f;
    uVar26 = sub_84670860(**(longlong **)(lRam0000000187bfb7a8 + 0xb8),uRam0000000187bf5308);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x10) = uVar26;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x10,uVar26);
    if (cRam000000018ab63078 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63078 = '\x01';
    }
    if (**(longlong **)(lRam0000000187bfb7a8 + 0xb8) == 0) goto code_r0x000181600f6f;
    uVar26 = sub_84670860(**(longlong **)(lRam0000000187bfb7a8 + 0xb8),uRam0000000187bf54c0);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x18) = uVar26;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x18,uVar26);
    if (cRam000000018ab63078 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63078 = '\x01';
    }
    if (**(longlong **)(lRam0000000187bfb7a8 + 0xb8) == 0) goto code_r0x000181600f6f;
    uVar26 = sub_84670860(**(longlong **)(lRam0000000187bfb7a8 + 0xb8),uRam0000000187bf59e8);
    *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x20) = uVar26;
    sub_8032e8d0(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x20,uVar26);
    sub_8595e430(uRam0000000187b9bf20);
  }
  lStack_e58 = *(longlong *)(param_1 + 0x2f0);
  lStack_e48 = *(longlong *)(param_1 + 0x2f8);
  uVar26 = *(undefined8 *)(param_1 + 0x300);
  uVar28 = *(undefined8 *)(param_1 + 0x308);
  uVar27 = *(undefined8 *)(param_1 + 0x310);
  uVar4 = *(undefined8 *)(param_1 + 0x318);
  uVar5 = *(undefined8 *)(param_1 + 800);
  uVar6 = *(undefined8 *)(param_1 + 0x328);
  uVar7 = *(undefined8 *)(param_1 + 0x330);
  uStack_e38 = sub_8032f970(uRam0000000187c36f70);
  sub_81774e00(uStack_e38,lStack_e58,lStack_e48,uVar26,uVar28,uVar27,uVar4,uVar5,uVar6,uVar7,0);
  uVar26 = sub_8032f970(uRam0000000187c20d30);
  sub_849de5b0(uVar26,uRam0000000187c31708);
  *(undefined8 *)(param_1 + 0x100) = uVar26;
  sub_8032e8d0(param_1 + 0x100,uVar26);
  uVar26 = sub_8032f970(uRam0000000187c20d30);
  sub_849de5b0(uVar26,uRam0000000187c31708);
  *(undefined8 *)(param_1 + 0x108) = uVar26;
  sub_8032e8d0(param_1 + 0x108,uVar26);
  uVar26 = sub_8032f970(uRam0000000187b6bd98);
  sub_80e0ef60(uVar26,0);
  *(undefined8 *)(param_1 + 0x1c0) = uVar26;
  sub_8032e8d0(param_1 + 0x1c0,uVar26);
  fVar45 = ABS(*(float *)(param_1 + 0x348));
  fVar42 = -*(float *)(param_1 + 0x348);
  *(float *)(param_1 + 0x464) = fVar42;
  *(float *)(param_1 + 0x468) = fVar45;
  *(float *)(param_1 + 0x46c) = fVar42 * -1.0;
  *(float *)(param_1 + 0x470) = fVar45;
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar15 = lStack_df0;
  lVar25 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar25 == 0) goto code_r0x000181600f6f;
  uStack_e40 = CONCAT44(uStack_e40._4_4_,*(undefined4 *)(lVar25 + 0x2c0));
  *(undefined8 *)(param_1 + 0x170) = *(undefined8 *)(lStack_df0 + 0x20);
  sub_8032e8d0(param_1 + 0x170);
  *(char *)(param_1 + 0x178) = param_3;
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar25 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar25 == 0))
  goto code_r0x000181600f6f;
  lVar29 = *(longlong *)(lVar25 + 0x78);
  uVar19 = *(undefined4 *)(lVar25 + 0x80);
  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
    sub_8032fa70();
  }
  lStack_e68 = lVar29;
  uStack_e60 = uVar19;
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x11c) = uVar19;
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar25 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar25 == 0))
  goto code_r0x000181600f6f;
  lStack_e68 = *(longlong *)(lVar25 + 0x6c);
  uStack_e60 = *(undefined4 *)(lVar25 + 0x74);
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x120) = uVar19;
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar25 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x10), lStack_df0 = lVar25, lVar25 == 0)
     ) goto code_r0x000181600f6f;
  lStack_e68 = *(longlong *)(lVar25 + 0x10);
  uStack_e60 = *(undefined4 *)(lVar25 + 0x18);
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x488) = uVar19;
  if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uVar16 = UnitUtility::JudgeIsBoss(uVar19,0);
  *(undefined1 *)(param_1 + 0x1a8) = uVar16;
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar29 == 0))
  goto code_r0x000181600f6f;
  lStack_e68 = *(longlong *)(lVar29 + 0x14);
  uStack_e60 = *(undefined4 *)(lVar29 + 0x1c);
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x48c) = uVar19;
  uVar19 = UnitUtility::GetOriginalUnitId(*(undefined4 *)(param_1 + 0x488),0);
  *(undefined4 *)(param_1 + 0x490) = uVar19;
  *(undefined8 *)(param_1 + 0x128) = param_7;
  sub_8032e8d0(param_1 + 0x128);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  uVar26 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (cRam000000018ab63079 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63079 = '\x01';
  }
  uVar28 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8);
  uVar27 = sub_8032f970(uRam0000000187b6c2d0);
  plVar39 = (longlong *)0x0;
  CLKMBEGHOLA::.ctor(uVar27,param_1,uVar26,uVar28,0);
  *(undefined8 *)(param_1 + 0x38) = uVar27;
  sub_8032e8d0(param_1 + 0x38,uVar27);
  uVar19 = *(undefined4 *)(param_1 + 0x488);
  uVar26 = *(undefined8 *)(param_1 + 0x500);
  uVar28 = sub_8032f970(uRam0000000187c38fc8);
  MGAEMKEEHND::.ctor(uVar28,param_1,uVar19,uVar26,0);
  *(undefined8 *)(param_1 + 0x40) = uVar28;
  sub_8032e8d0(param_1 + 0x40,uVar28);
  lStack_e48 = sub_8032f970(uRam0000000187b9f298);
  sub_83a711a0(lStack_e48,param_1,uRam0000000187b308f8,0);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  plVar34 = *(longlong **)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  uVar26 = sub_8032f970(uRam0000000187b9f600);
  if (plVar34 == (longlong *)0x0) goto code_r0x000181600f6f;
  sub_80f4e630(uVar26,plVar34,*(undefined8 *)(*plVar34 + 0x1d20),0);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  plVar34 = *(longlong **)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  uVar28 = sub_8032f970(uRam0000000187b9f600);
  if (plVar34 == (longlong *)0x0) goto code_r0x000181600f6f;
  sub_80f4e630(uVar28,plVar34,*(undefined8 *)(*plVar34 + 0xea0),0);
  uVar27 = sub_8032f970(uRam0000000187b572d0);
  sub_808ebc70(uVar27,lStack_e48,uVar26,uVar28,0);
  *(undefined8 *)(param_1 + 0x48) = uVar27;
  sub_8032e8d0(param_1 + 0x48,uVar27);
  uVar26 = sub_8032f970(uRam0000000187b5ca48);
  sub_804383d0(uVar26,0);
  *(undefined8 *)(param_1 + 0x50) = uVar26;
  sub_8032e8d0(param_1 + 0x50,uVar26);
  uVar26 = FixedTransformMonoBehavior::get_transform(param_1,0);
  uVar28 = sub_8032f970(uRam0000000187b3c490);
  BOOMKCIDJHD::.ctor(uVar28,param_1,uVar26,0);
  *(undefined8 *)(param_1 + 0x58) = uVar28;
  sub_8032e8d0(param_1 + 0x58,uVar28);
  uVar26 = sub_8032f970(uRam0000000187c12358);
  ECOCNEKAIMD::.ctor(uVar26,param_1,0);
  *(undefined8 *)(param_1 + 0x60) = uVar26;
  sub_8032e8d0(param_1 + 0x60,uVar26);
  uVar26 = sub_8032f970(uRam0000000187b472d8);
  sub_80438ea0(uVar26,param_1,0);
  *(undefined8 *)(param_1 + 0x68) = uVar26;
  sub_8032e8d0(param_1 + 0x68,uVar26);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lStack_e48 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (cRam000000018ab63079 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63079 = '\x01';
  }
  uVar26 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8);
  if (cRam000000018ab6307a == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307a = '\x01';
  }
  uVar28 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x18);
  uVar27 = sub_8032f970(uRam0000000187bc6f78);
  DBGNMELIABG::.ctor(uVar27,param_1,lStack_e48,uVar26,uVar28,0);
  *(undefined8 *)(param_1 + 0x70) = uVar27;
  sub_8032e8d0(param_1 + 0x70,uVar27);
  if (cRam000000018ab63079 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63079 = '\x01';
  }
  uVar26 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8);
  uVar28 = sub_8032f970(uRam0000000187ba6038);
  sub_80437890(uVar28,param_1,uVar26,0);
  *(undefined8 *)(param_1 + 0x78) = uVar28;
  sub_8032e8d0(param_1 + 0x78,uVar28);
  uVar26 = sub_8032f970(uRam0000000187c25398);
  AIGOPEMKDKL::.ctor(uVar26,param_1,0);
  *(undefined8 *)(param_1 + 0x80) = uVar26;
  sub_8032e8d0(param_1 + 0x80,uVar26);
  uVar26 = sub_8032f970(uRam0000000187c35268);
  sub_837b2060(uVar26,param_1,uRam0000000187b30aa8,0);
  uVar28 = sub_8032f970(uRam0000000187b6b178);
  CIFIMDJAGGF::.ctor(uVar28,uVar26,0);
  *(undefined8 *)(param_1 + 0x88) = uVar28;
  sub_8032e8d0(param_1 + 0x88,uVar28);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar29 == 0) goto code_r0x000181600f6f;
  uVar19 = *(undefined4 *)(lVar29 + 0x588);
  if (*(char *)(param_1 + 0x1a8) == '\0') {
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar29 == 0))
    goto code_r0x000181600f6f;
    lVar32 = *(longlong *)(lVar29 + 0x84);
    uVar24 = *(undefined4 *)(lVar29 + 0x8c);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lStack_e68 = lVar32;
    uStack_e60 = uVar24;
    iVar20 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
    bVar41 = iVar20 == 0;
  }
  else {
    bVar41 = true;
  }
  uVar26 = sub_8032f970(uRam0000000187c361d0);
  MBJPLNBCBDB::.ctor(uVar26,uVar19,bVar41,0);
  *(undefined8 *)(param_1 + 0x90) = uVar26;
  sub_8032e8d0(param_1 + 0x90,uVar26);
  if (cRam000000018ab63078 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63078 = '\x01';
  }
  if (**(longlong **)(lRam0000000187bfb7a8 + 0xb8) == 0) goto code_r0x000181600f6f;
  uVar26 = sub_84670860(**(longlong **)(lRam0000000187bfb7a8 + 0xb8),uRam0000000187bf5ba0);
  uVar28 = sub_8032f970(uRam0000000187b6cae8);
  PBLMDEFAGBK::.ctor(uVar28,param_1,uVar26,0);
  *(undefined8 *)(param_1 + 0x98) = uVar28;
  sub_8032e8d0(param_1 + 0x98,uVar28);
  lVar29 = sub_8032f970(uRam0000000187bfb280);
  if (cRam000000018ab63033 == '\0') {
    sub_8032f6d0(0x187c432d0);
    sub_8032f6d0(0x187bea1c8);
    cRam000000018ab63033 = '\x01';
  }
  uVar26 = sub_8032f970(uRam0000000187bea1c8);
  sub_849de5b0(uVar26,uRam0000000187c432d0);
  *(undefined8 *)(lVar29 + 0x18) = uVar26;
  sub_8032e8d0(lVar29 + 0x18,uVar26);
  uVar26 = sub_8032f970(uRam0000000187bea1c8);
  sub_849de5b0(uVar26,uRam0000000187c432d0);
  *(undefined8 *)(lVar29 + 0x20) = uVar26;
  sub_8032e8d0(lVar29 + 0x20,uVar26);
  sub_80438ea0(lVar29,param_1,0);
  *(longlong *)(param_1 + 0xa0) = lVar29;
  sub_8032e8d0(param_1 + 0xa0,lVar29);
  uVar26 = sub_8032f970(uRam0000000187b57638);
  sub_839bce60(uVar26,param_1,0);
  *(undefined8 *)(param_1 + 0xa8) = uVar26;
  sub_8032e8d0(param_1 + 0xa8,uVar26);
  uVar26 = sub_8032f970(uRam0000000187b45b98);
  NLHACDMHDNP::.ctor(uVar26,param_1,0);
  *(undefined8 *)(param_1 + 0xb8) = uVar26;
  sub_8032e8d0(param_1 + 0xb8,uVar26);
  uVar26 = sub_8032f970(uRam0000000187c3d070);
  MNANDEPAJJK::.ctor(uVar26,param_1,0);
  *(undefined8 *)(param_1 + 0xb0) = uVar26;
  sub_8032e8d0(param_1 + 0xb0,uVar26);
  uVar26 = sub_8032f970(uRam0000000187b9d7f8);
  sub_839bce60(uVar26,param_1,0);
  *(undefined8 *)(param_1 + 0xc0) = uVar26;
  sub_8032e8d0(param_1 + 0xc0,uVar26);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lStack_e48 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar29 == 0))
  goto code_r0x000181600f6f;
  lVar32 = *(longlong *)(lVar29 + 0x30);
  uVar19 = *(undefined4 *)(lVar29 + 0x38);
  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
    sub_8032fa70();
  }
  lStack_e68 = lVar32;
  uStack_e60 = uVar19;
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  uVar26 = Component::get_gameObject(param_1,0);
  uVar28 = sub_8032f970(uRam0000000187b35790);
  BCKKJALAJGO::.ctor(uVar28,param_1,lStack_e48,uVar19,uVar26,0);
  *(undefined8 *)(param_1 + 200) = uVar28;
  sub_8032e8d0(param_1 + 200,uVar28);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  uVar26 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (cRam000000018ab63079 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63079 = '\x01';
  }
  uVar28 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8);
  uVar27 = sub_8032f970(uRam0000000187bc83e8);
  uVar33 = uStack_e38;
  DEBLMMJMGID::.ctor(uVar27,param_1,uVar26,uVar28,uStack_e38,0);
  uVar19 = (undefined4)(uVar33 >> 0x20);
  *(undefined8 *)(param_1 + 0xd0) = uVar27;
  sub_8032e8d0(param_1 + 0xd0,uVar27);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  uVar26 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (cRam000000018ab6307b == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307b = '\x01';
  }
  uVar28 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x20);
  uVar27 = sub_8032f970(uRam0000000187c366e8);
  MBNAOPKFJMN::.ctor(uVar27,param_1,uVar26,uVar28,CONCAT44(uVar19,param_8),0);
  *(undefined8 *)(param_1 + 0xd8) = uVar27;
  sub_8032e8d0(param_1 + 0xd8,uVar27);
  UnitCtrl::registerAllComponentSet(param_1,0);
  sub_847b8200(*(undefined8 *)(param_1 + 0x2e8),param_1,uRam0000000187b31568);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar29 == 0) goto code_r0x000181600f6f;
  BattleManager::InitializeExAndFreeSkill(lVar29,lVar25,2,0,param_1,0);
  if (*(longlong *)(param_1 + 200) == 0) goto code_r0x000181600f6f;
  BCKKJALAJGO::BNCKPNIOJHD(*(longlong *)(param_1 + 200),0);
  if ((*(longlong *)(lVar15 + 0x28) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x28) + 0xf0), lVar29 == 0))
  goto code_r0x000181600f6f;
  lVar29 = CJKJNDOEAPA::BKEJKNOJCEE(lVar29,*(undefined4 *)(param_1 + 0x48c),0);
  if (lVar29 != 0) {
    lVar31 = *(longlong *)(param_1 + 200);
    lVar32 = *(longlong *)(lVar29 + 0x1c);
    uVar19 = *(undefined4 *)(lVar29 + 0x24);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    lStack_e68 = lVar32;
    uStack_e60 = uVar19;
    uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
    if (lVar31 == 0) goto code_r0x000181600f6f;
    *(undefined4 *)(lVar31 + 0x40) = uVar19;
  }
  cVar17 = UnitCtrl::get_IsPlayerUnit(param_1,0);
  if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uVar19 = UnitUtility::GetSetSkinNo(lVar25,1,cVar17 == '\0',0,0);
  *(undefined4 *)(param_1 + 0x494) = uVar19;
  lVar29 = *(longlong *)(lVar25 + 0x34);
  uVar19 = *(undefined4 *)(lVar25 + 0x3c);
  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
    sub_8032fa70();
  }
  lStack_e68 = lVar29;
  uStack_e60 = uVar19;
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x49c) = uVar19;
  lStack_e68 = *(longlong *)(lVar25 + 0x40);
  uStack_e60 = *(undefined4 *)(lVar25 + 0x48);
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x4a0) = uVar19;
  uVar19 = UnitUtility::GetSetSkinNo(lVar25,0,0,0,0);
  *(undefined4 *)(param_1 + 0x498) = uVar19;
  *(undefined4 *)(param_1 + 0x4a4) = *(undefined4 *)(lVar25 + 100);
  lStack_e68 = *(longlong *)(lVar25 + 0x118);
  uStack_e60 = *(undefined4 *)(lVar25 + 0x120);
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x4a8) = uVar19;
  lStack_e68 = *(longlong *)(lVar25 + 0x124);
  uStack_e60 = *(undefined4 *)(lVar25 + 300);
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x4ac) = uVar19;
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar29 == 0))
  goto code_r0x000181600f6f;
  lStack_e68 = *(longlong *)(lVar29 + 0x30);
  uStack_e60 = *(undefined4 *)(lVar29 + 0x38);
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x15c) = uVar19;
  iVar20 = (int)uStack_e40;
  bStack_e50 = BattleUtil::IsClanBattleCategory(uStack_e40 & 0xffffffff,0);
  bVar41 = false;
  if (param_3 == '\0') {
    *(undefined1 *)(param_1 + 0x1a9) = 0;
    if (bStack_e50 != 0) {
code_r0x0001815fb9c6:
      if (*(char *)(param_1 + 0x1a8) == '\0') {
        *(undefined1 *)(param_1 + 0x1a9) = 0;
      }
      if (param_3 != '\0') goto code_r0x0001815fb9f3;
    }
    *(undefined1 *)(param_1 + 0x1aa) = 0;
    bVar40 = false;
  }
  else {
    if (bStack_e50 != 0) {
      *(undefined1 *)(param_1 + 0x1a9) = 1;
      goto code_r0x0001815fb9c6;
    }
    *(bool *)(param_1 + 0x1a9) = iVar20 == 0x14;
code_r0x0001815fb9f3:
    *(bool *)(param_1 + 0x1aa) = iVar20 == 0x1f || iVar20 == 0x20;
    bVar40 = bVar41;
    if (*(char *)(param_1 + 0x1a8) != '\0') {
      if (iVar20 == 0x2f) {
        bVar40 = true;
      }
      else {
        bVar40 = iVar20 == 0x30;
      }
    }
  }
  *(bool *)(param_1 + 0x1ab) = bVar40;
  lVar29 = *(longlong *)(param_1 + 0x88);
  if (lVar29 == 0) goto code_r0x000181600f6f;
  uVar19 = *(undefined4 *)(lVar25 + 0x54);
  *(undefined8 *)(lVar29 + 0x420) = *(undefined8 *)(lVar25 + 0x4c);
  *(undefined4 *)(lVar29 + 0x428) = uVar19;
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar29 == 0))
  goto code_r0x000181600f6f;
  lVar32 = *(longlong *)(lVar29 + 0x9c);
  uVar19 = *(undefined4 *)(lVar29 + 0xa4);
  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
    sub_8032fa70();
  }
  lStack_e68 = lVar32;
  uStack_e60 = uVar19;
  uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  *(undefined4 *)(param_1 + 0x4b0) = uVar19;
  *(undefined4 *)(param_1 + 0x1a0) = 0xffffffff;
  if (*(longlong *)(param_1 + 0x70) == 0) goto code_r0x000181600f6f;
  sub_8397cbe0(*(longlong *)(param_1 + 0x70),0);
  lVar29 = *(longlong *)(param_1 + 0xb8);
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar32 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar32 == 0))
  goto code_r0x000181600f6f;
  lStack_e68 = *(longlong *)(lVar32 + 0x90);
  uStack_e60 = *(undefined4 *)(lVar32 + 0x98);
  ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
  if (lVar29 == 0) goto code_r0x000181600f6f;
  sub_839c02a0(lVar29);
  if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181600f6f;
  *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0xb9) = 0;
  if ((*(longlong *)(lVar15 + 0x20) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar29 == 0))
  goto code_r0x000181600f6f;
  uVar26 = *(undefined8 *)(lVar29 + 0xa8);
  uVar28 = *(undefined8 *)(lVar29 + 0xb0);
  uVar27 = *(undefined8 *)(lVar29 + 0xb8);
  uVar4 = *(undefined8 *)(lVar29 + 0xc0);
  if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uStack_dc8 = uVar26;
  uStack_dc0 = uVar28;
  uStack_db8 = uVar27;
  uStack_db0 = uVar4;
  dVar44 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_dc8,0);
  *(float *)(param_1 + 0x4b4) = (float)dVar44;
  uVar19 = *(undefined4 *)(param_1 + 0x490);
  if (*(int *)(lRam0000000187b440e8 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uVar26 = BattleVoiceUtility::GetVoiceTimingData(uVar19,0,0);
  *(undefined8 *)(param_1 + 0x248) = uVar26;
  sub_8032e8d0(param_1 + 0x248,uVar26);
  if ((*(longlong *)(lVar15 + 0x28) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x28) + 0x1720), lVar29 == 0))
  goto code_r0x000181600f6f;
  uVar26 = HMDABKLHIDA::GMEEGGMGOPO(lVar29,*(undefined4 *)(param_1 + 0x490),0);
  *(undefined8 *)(param_1 + 0x4e0) = uVar26;
  sub_8032e8d0(param_1 + 0x4e0,uVar26);
  if ((*(longlong *)(lVar15 + 0x28) == 0) ||
     (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x28) + 0x1b48), lVar29 == 0))
  goto code_r0x000181600f6f;
  uVar19 = NKJOEJEGAFE::NJLCFNMPIOB(lVar29,*(undefined4 *)(param_1 + 0x490),0);
  *(undefined4 *)(param_1 + 0x4e8) = uVar19;
  if ((param_6 == 0) || (*(longlong *)(param_1 + 0x98) == 0)) goto code_r0x000181600f6f;
  PBLMDEFAGBK::NPHLOLNCNPP(*(longlong *)(param_1 + 0x98),*(undefined8 *)(param_6 + 0x10),0);
  lStack_e48 = *(longlong *)(param_1 + 0x98);
  lStack_e58 = *(longlong *)(lVar15 + 0x20);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar29 == 0) goto code_r0x000181600f6f;
  uVar19 = BattleManager::GetSystemId(lVar29,0);
  uStack_e40 = CONCAT44(uStack_e40._4_4_,uVar19);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  uVar26 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  uVar28 = sub_8032f970(uRam0000000187c010f0);
  sub_80747d70(uVar28,uVar26,uRam0000000187b634d8,0);
  if (lStack_e48 == 0) goto code_r0x000181600f6f;
  uVar19 = 0;
  PBLMDEFAGBK::KHENDOBMAOP(lStack_e48,lStack_e58,uStack_e40 & 0xffffffff,uVar28,0);
  uVar26 = *(undefined8 *)(param_6 + 0x18);
  if (*(longlong *)(param_1 + 0x98) == 0) goto code_r0x000181600f6f;
  sub_839a0000(*(longlong *)(param_1 + 0x98),uVar26,0);
  lVar29 = *(longlong *)(param_1 + 0xa8);
  if (lVar29 == 0) goto code_r0x000181600f6f;
  *(undefined8 *)(lVar29 + 0x18) = uVar26;
  sub_8032e8d0(lVar29 + 0x18,uVar26);
  if (param_9 != 0) {
    *(int *)(param_1 + 0x180) = param_9;
    if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181600f6f;
    *(undefined4 *)(*(longlong *)(param_1 + 0xd0) + 0xb4) = 6;
  }
  UnitCtrl::instantiateResources(param_1,0);
  if ((*(int *)(param_1 + 0x180) == 1) || (*(int *)(param_1 + 0x180) == 2)) {
    if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181600f6f;
    *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0xbb) = 1;
  }
  else {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    lVar32 = *(longlong *)(param_1 + 0xd0);
    if (*(int *)(lVar29 + 0x2c0) == 6) {
      if (lVar32 == 0) goto code_r0x000181600f6f;
      *(undefined1 *)(lVar32 + 0xbb) = 1;
    }
    else {
      lVar29 = sub_815f21c0(param_1,0);
      if (lVar29 == 0) goto code_r0x000181600f6f;
      if (*(int *)(lVar29 + 0x22c) == 0) {
        lVar29 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
        if (lVar29 == 0) goto code_r0x000181600f6f;
        cVar17 = BattleUnitBaseSpineController::IsAnimation(lVar29,0xb,0xffffffff,0xffffffff,CONCAT44(uVar19,0xffffffff),0);
        bVar40 = cVar17 == '\0';
      }
      else {
        bVar40 = true;
      }
      if (lVar32 == 0) goto code_r0x000181600f6f;
      *(bool *)(lVar32 + 0xbb) = bVar40;
    }
  }
  if (*(longlong *)(param_1 + 0x88) == 0) goto code_r0x000181600f6f;
  uVar26 = 0;
  CIFIMDJAGGF::IGIIMJMGHKH();
  lVar29 = *(longlong *)(param_1 + 0x268);
  if (*(longlong *)(param_1 + 0xe0) == 0) goto code_r0x000181600f6f;
  fVar42 = *(float *)(*(longlong *)(param_1 + 0xe0) + 0x80);
  fVar45 = *(float *)(param_1 + 0x348);
  if (*(int *)(lRam0000000187c440d8 + 0xe4) == 0) {
    sub_8032fa70();
  }
  if (*(longlong *)(param_1 + 200) == 0) goto code_r0x000181600f6f;
  uVar19 = *(undefined4 *)(*(longlong *)(param_1 + 200) + 0x38);
  if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar17 = UnitUtility::IsPersonUnit(uVar19,0);
  if (cVar17 == '\0') {
    fVar43 = 0.0;
  }
  else {
    if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
      sub_8032fa70(lRam0000000187b41650);
    }
    fVar43 = *(float *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 0x90);
  }
  if (lVar29 == 0) goto code_r0x000181600f6f;
  uVar26 = CONCAT71((int7)((ulonglong)uVar26 >> 8),param_5);
  LifeGaugeController::Initialize(lVar29,param_3,ABS(fVar45) * fVar42 + fVar43,param_1,uVar26,0);
  uVar19 = (undefined4)((ulonglong)uVar26 >> 0x20);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar29 == 0) goto code_r0x000181600f6f;
  cVar17 = BattleManager::get_IsHatsuneBoss(lVar29,0);
  if ((cVar17 == '\0') || (*(char *)(param_1 + 0x1a8) == '\0')) {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    cVar17 = BattleManager::get_IsClanBattle(lVar29,0);
    if ((cVar17 == '\0') || (*(char *)(param_1 + 0x1a8) == '\0')) {
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar29 == 0) goto code_r0x000181600f6f;
      cVar17 = BattleManager::get_IsTrialClanBattle(lVar29,0);
      if ((cVar17 == '\0') || (*(char *)(param_1 + 0x1a8) == '\0')) {
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar29 == 0) goto code_r0x000181600f6f;
        if (*(int *)(lVar29 + 0x2c0) != 0x19) {
          lVar29 = sub_815f21c0(param_1,0);
          if (lVar29 == 0) goto code_r0x000181600f6f;
          if (*(int *)(lVar29 + 0x2c0) != 0x1d) {
            lVar29 = sub_815f21c0(param_1,0);
            if (lVar29 == 0) goto code_r0x000181600f6f;
            if (*(int *)(lVar29 + 0x2c0) != 0x23) {
              lVar29 = sub_815f21c0(param_1,0);
              if (lVar29 == 0) goto code_r0x000181600f6f;
              if (*(int *)(lVar29 + 0x2c0) != 0x36) {
                lVar29 = sub_815f21c0(param_1,0);
                if (lVar29 == 0) goto code_r0x000181600f6f;
                if (*(int *)(lVar29 + 0x2c0) != 0x37) {
                  lVar29 = sub_815f21c0(param_1,0);
                  if (lVar29 == 0) goto code_r0x000181600f6f;
                  if (*(int *)(lVar29 + 0x2c0) != 0x51) {
                    lVar29 = sub_815f21c0(param_1,0);
                    if (lVar29 == 0) goto code_r0x000181600f6f;
                    if (*(int *)(lVar29 + 0x2c0) != 0x50) {
                      lVar29 = sub_815f21c0(param_1,0);
                      if (lVar29 == 0) goto code_r0x000181600f6f;
                      cVar17 = BattleManager::get_IsSubStoryRaidBattle(lVar29,0);
                      if (cVar17 == '\0') {
                        lVar29 = *(longlong *)(param_1 + 0x330);
                        if (*(int *)(lRam0000000187c22368 + 0xe4) == 0) {
                          sub_8032fa70();
                        }
                        lVar32 = *(longlong *)(*(longlong *)(lRam0000000187c22368 + 0xb8) + 8);
                        if (lVar32 == 0) {
                          if (*(int *)(lRam0000000187c22368 + 0xe4) == 0) {
                            sub_8032fa70();
                          }
                          uVar26 = **(undefined8 **)(lRam0000000187c22368 + 0xb8);
                          lVar32 = sub_8032f970(uRam0000000187b90710);
                          sub_84cacb40(lVar32,uVar26,uRam0000000187bd45d8,0);
                          *(longlong *)(*(longlong *)(lRam0000000187c22368 + 0xb8) + 8) = lVar32;
                          sub_8032e8d0(*(longlong *)(lRam0000000187c22368 + 0xb8) + 8,lVar32)
                          ;
                        }
                        if (lVar29 == 0) goto code_r0x000181600f6f;
                        lVar29 = sub_84a8c920(lVar29,lVar32,uRam0000000187beb850);
                        if (lVar29 == 0) {
                          lVar29 = *(longlong *)(param_1 + 0x330);
                          if (lVar29 == 0) goto code_r0x000181600f6f;
                          *(int *)(lVar29 + 0x1c) = *(int *)(lVar29 + 0x1c) + 1;
                          iVar20 = *(int *)(lVar29 + 0x18);
                          *(undefined4 *)(lVar29 + 0x18) = 0;
                          if (0 < iVar20) {
                            Array::Clear(*(undefined8 *)(lVar29 + 0x10),0,iVar20,0);
                          }
                        }
                        goto code_r0x0001815fc753;
                      }
                    }
                  }
                }
              }
            }
          }
        }
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar29 == 0) goto code_r0x000181600f6f;
        if (*(int *)(lVar29 + 0x2c0) == 0x51) {
          lVar29 = sub_815f21c0(param_1,0);
          if (lVar29 == 0) goto code_r0x000181600f6f;
          cVar17 = BattleManager::IsPrincessSelected(lVar29,0);
          if (cVar17 != '\0') {
            *(undefined4 *)(param_1 + 0x2e0) = 5;
          }
        }
      }
      else {
        lVar29 = sub_847c7d00(param_1,uRam0000000187bfd360);
        if (lVar29 == 0) goto code_r0x000181600f6f;
        lVar29 = sub_84670860(lVar29,uRam0000000187bf5f10);
        lStack_e58 = 0;
        if ((((*(longlong *)(lVar15 + 0x28) == 0) ||
             (lVar32 = *(longlong *)(*(longlong *)(lVar15 + 0x28) + 0x8b8), lVar32 == 0)) ||
            (lVar32 = MKJKJMLMOEL::GBHHHNJOAIM(lVar32,0), lVar32 == 0)) ||
           (lVar32 = sub_855377d0(lVar32,uRam0000000187b74b50), lVar32 == 0))
        goto code_r0x000181600f6f;
        sub_849962c0(&uStack_718,lVar32,uRam0000000187b8ef70);
        uStack_cb0 = uStack_718;
        uStack_ca8 = uStack_710;
        lStack_ca0 = lStack_708;
        uStack_e18 = 0;
        puStack_e10 = &uStack_cb0;
        do {
          do {
            cVar17 = sub_85744470(&uStack_cb0,uRam0000000187b33f48);
            lVar32 = lStack_ca0;
            if (cVar17 == '\0') {
              sub_80443e50(&uStack_cb0,uRam0000000187b33d98);
              goto code_r0x000181600f6f;
            }
            if (lStack_ca0 == 0) goto code_r0x000181600f63;
            lVar31 = *(longlong *)(lStack_ca0 + 0x1c);
            uStack_e40 = CONCAT44(uStack_e40._4_4_,*(undefined4 *)(lStack_ca0 + 0x24));
            uVar19 = *(undefined4 *)(lStack_ca0 + 0x24);
            if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
              sub_8032fa70();
              uVar19 = (int)uStack_e40;
            }
            lStack_e68 = lVar31;
            uStack_e60 = uVar19;
            iVar20 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
            if (lVar29 == 0) {
code_r0x000181600f5e:
              sub_8032f9d0();
code_r0x000181600f63:
              sub_8032f9d0();
              sub_8032b6b0();
              pcVar8 = (code *)swi(3);
              (*pcVar8)();
              return;
            }
            if (*(longlong *)(lVar29 + 0x60) == 0) {
code_r0x000181600f59:
              sub_8032f9d0();
              goto code_r0x000181600f5e;
            }
          } while (iVar20 != *(int *)(*(longlong *)(lVar29 + 0x60) + 0x10));
          if (lVar32 == 0) {
code_r0x000181600f54:
            sub_8032f9d0();
            goto code_r0x000181600f59;
          }
          iVar20 = PELJGKACDDO::get_Phase(lVar32,0);
          if (*(longlong *)(lVar29 + 0x68) == 0) {
            sub_8032f9d0();
            goto code_r0x000181600f54;
          }
        } while (iVar20 != *(int *)(*(longlong *)(lVar29 + 0x68) + 0x18));
        lStack_e58 = lVar32;
        sub_80443e50(&uStack_cb0,uRam0000000187b33d98);
        lVar29 = *(longlong *)(lVar32 + 0x1c);
        uVar19 = *(undefined4 *)(lVar32 + 0x24);
        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
          sub_8032fa70();
        }
        lStack_e68 = lVar29;
        uStack_e60 = uVar19;
        uVar24 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
        uVar21 = PELJGKACDDO::get_LapNumFrom(lVar32,0);
        uVar22 = PELJGKACDDO::get_Phase(lVar32,0);
        if (*(int *)(lRam0000000187b93750 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uVar19 = 0;
        uVar24 = ClanBattleUtil::GetCurrentBossEffectDifficulty(uVar24,uVar21,uVar22,0,0);
        *(undefined4 *)(param_1 + 0x2e0) = uVar24;
      }
    }
    else {
      lVar29 = sub_8048c110(uRam0000000187c0c990);
      if ((lVar29 == 0) || (*(longlong *)(lVar29 + 0x30) == 0)) goto code_r0x000181600f6f;
      cVar17 = ClanBattleTopInfo::IsSingleBattleMode(*(longlong *)(lVar29 + 0x30),0);
      bVar40 = bVar41;
      if (cVar17 != '\0') {
        bVar40 = *(char *)(lVar29 + 0x91) == '\0';
      }
      uVar24 = *(undefined4 *)(lVar29 + 0x38);
      uStack_e40 = CONCAT44(uStack_e40._4_4_,*(undefined4 *)(lVar29 + 0x40));
      uVar21 = *(undefined4 *)(lVar29 + 0x44);
      uVar22 = *(undefined4 *)(lVar29 + 0x40);
      if (*(int *)(lRam0000000187b93750 + 0xe4) == 0) {
        sub_8032fa70();
        uVar22 = (int)uStack_e40;
      }
      uVar19 = 0;
      uVar24 = ClanBattleUtil::GetCurrentBossEffectDifficulty(uVar24,uVar22,uVar21,bVar40,0);
      *(undefined4 *)(param_1 + 0x2e0) = uVar24;
    }
  }
  else {
    lVar29 = sub_80003d20(uRam0000000187c0f7d0);
    if ((lVar29 == 0) || (*(longlong *)(lVar29 + 0x28) == 0)) goto code_r0x000181600f6f;
    uVar24 = *(undefined4 *)(*(longlong *)(lVar29 + 0x28) + 0x28);
    if (*(int *)(lRam0000000187b74fd8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iVar20 = HatsuneUtility::GetCurrentEventBossDifficulty(uVar24,0);
    if (iVar20 == 1) {
      *(undefined4 *)(param_1 + 0x2e0) = 1;
    }
    else if (iVar20 == 2) {
      *(undefined4 *)(param_1 + 0x2e0) = 2;
    }
    else if (iVar20 == 3) {
      *(undefined4 *)(param_1 + 0x2e0) = 3;
    }
  }
code_r0x0001815fc753:
  if (*(char *)(param_1 + 0x1a8) != '\0') {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    if (*(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28) == 0)
    goto code_r0x000181600f6f;
    BattleManager::SetBossCameraScale();
  }
  lVar29 = *(longlong *)(param_1 + 0x128);
  if (lVar29 != 0) {
    uVar33 = CONCAT44(uVar19,0xffffffff);
    uVar26 = BattleUnitBaseSpineController::ConvertAnimeIdToAnimeName(lVar29,1,0xffffffff,0xffffffff,uVar33,0);
    SkeletonAnimation::set_AnimationName(lVar29,uVar26,0);
    if (*(longlong *)(param_1 + 0x128) != 0) {
      lVar29 = Component::get_transform(*(longlong *)(param_1 + 0x128),0);
      lStack_e68 = *(longlong *)(param_1 + 0x46c);
      uStack_e60 = 0;
      if (lVar29 != 0) {
        uStack_e20 = 0;
        lStack_e28 = lStack_e68;
        Transform::set_localScale(lVar29,&lStack_e28,0);
        lVar29 = *(longlong *)(param_1 + 0xf0);
        if (lVar29 != 0) {
          fVar42 = *(float *)(lVar29 + 0x74);
          fVar45 = *(float *)(lVar29 + 0x80);
          if ((*(longlong *)(param_1 + 0x128) != 0) &&
             (lVar29 = Component::get_transform(*(longlong *)(param_1 + 0x128),0), lVar29 != 0)) {
            puVar30 = (undefined8 *)Transform::get_lossyScale(auStack_620,lVar29,0);
            uStack_e18 = *puVar30;
            fVar43 = *(float *)(puVar30 + 1);
            *(ulonglong *)(param_1 + 0x1e8) =
                 CONCAT44(fVar45 * (float)((ulonglong)uStack_e18 >> 0x20),fVar42 * (float)uStack_e18
                         );
            *(float *)(param_1 + 0x1f0) = fVar43 * 0.0;
            if (*(char *)(param_1 + 500) != '\0') {
              lVar29 = *(longlong *)(param_1 + 0x1f8);
              if (lVar29 == 0) goto code_r0x000181600f6f;
              fVar42 = *(float *)(lVar29 + 0x74);
              fVar45 = *(float *)(lVar29 + 0x80);
              if ((*(longlong *)(param_1 + 0x128) == 0) ||
                 (lVar29 = Component::get_transform(*(longlong *)(param_1 + 0x128),0), lVar29 == 0))
              goto code_r0x000181600f6f;
              puVar30 = (undefined8 *)Transform::get_lossyScale(auStack_630,lVar29,0);
              uStack_e18 = *puVar30;
              fVar43 = *(float *)(puVar30 + 1);
              *(ulonglong *)(param_1 + 0x200) =
                   CONCAT44(fVar45 * (float)((ulonglong)uStack_e18 >> 0x20),
                            fVar42 * (float)uStack_e18);
              *(float *)(param_1 + 0x208) = fVar43 * 0.0;
            }
            lVar29 = *(longlong *)(param_1 + 0xe0);
            if (lVar29 != 0) {
              fVar42 = *(float *)(lVar29 + 0x74);
              fVar45 = *(float *)(lVar29 + 0x80);
              if ((*(longlong *)(param_1 + 0x128) != 0) &&
                 (lVar29 = Component::get_transform(*(longlong *)(param_1 + 0x128),0), lVar29 != 0)) {
                puVar30 = (undefined8 *)Transform::get_lossyScale(auStack_640,lVar29,0);
                uStack_e18 = *puVar30;
                fVar43 = *(float *)(puVar30 + 1);
                *(ulonglong *)(param_1 + 0x20c) =
                     CONCAT44(fVar45 * (float)((ulonglong)uStack_e18 >> 0x20),
                              fVar42 * (float)uStack_e18);
                *(float *)(param_1 + 0x214) = fVar43 * 0.0;
                lVar29 = *(longlong *)(param_1 + 0x128);
                if ((lVar29 != 0) && (*(longlong *)(lVar29 + 0xa8) != 0)) {
                  lVar32 = *(longlong *)(*(longlong *)(lVar29 + 0xa8) + 0x20);
                  lStack_df8 = lVar32;
                  if (*(int *)(lRam0000000187c22368 + 0xe4) == 0) {
                    sub_8032fa70();
                  }
                  lVar31 = *(longlong *)(*(longlong *)(lRam0000000187c22368 + 0xb8) + 0x10);
                  if (lVar31 == 0) {
                    if (*(int *)(lRam0000000187c22368 + 0xe4) == 0) {
                      sub_8032fa70();
                    }
                    uVar26 = **(undefined8 **)(lRam0000000187c22368 + 0xb8);
                    lVar31 = sub_8032f970(uRam0000000187b93508);
                    sub_84cacb40(lVar31,uVar26,uRam0000000187bd4790,0);
                    *(longlong *)(*(longlong *)(lRam0000000187c22368 + 0xb8) + 0x10) = lVar31;
                    sub_8032e8d0(*(longlong *)(lRam0000000187c22368 + 0xb8) + 0x10,lVar31);
                    lVar32 = lStack_df8;
                  }
                  if ((lVar32 != 0) &&
                     (lVar32 = sub_85923310(lVar32,lVar31,uRam0000000187b8d958), lVar32 != 0)) {
                    iVar20 = *(int *)(lVar32 + 0x18);
                    uStack_e40 = CONCAT44(uStack_e40._4_4_,iVar20);
                    if (*(longlong *)(lVar29 + 0xa8) != 0) {
                      lVar32 = Skeleton::FindSlot(*(longlong *)(lVar29 + 0xa8),uRam0000000187c166b8,0);
                      if (lVar32 != 0) {
                        lVar32 = *(longlong *)(lVar29 + 0x50);
                        if (lVar32 == 0) goto code_r0x000181600f6f;
                        *(int *)(lVar32 + 0x1c) = *(int *)(lVar32 + 0x1c) + 1;
                        iVar23 = *(int *)(lVar32 + 0x18);
                        *(undefined4 *)(lVar32 + 0x18) = 0;
                        if (0 < iVar23) {
                          Array::Clear(*(undefined8 *)(lVar32 + 0x10),0,iVar23,0);
                        }
                        plVar34 = plVar39;
                        if (0 < iVar20) {
                          do {
                            lVar32 = *(longlong *)(lVar29 + 0xa8);
                            aiStack_d58[0] = (int)plVar34 + 1;
                            uVar26 = sub_8032e8a0(uRam000000018ab8e158,aiStack_d58);
                            uVar26 = String::Format(uRam0000000187c16868,uVar26,0);
                            if (lVar32 == 0) goto code_r0x000181600f6f;
                            lVar32 = Skeleton::FindSlot(lVar32,uVar26);
                            if (lVar32 != 0) {
                              if (*(longlong *)(lVar29 + 0x50) == 0) goto code_r0x000181600f6f;
                              sub_80002460(*(longlong *)(lVar29 + 0x50),lVar32);
                            }
                            uVar37 = (int)plVar34 + 1;
                            plVar34 = (longlong *)(ulonglong)uVar37;
                          } while ((int)uVar37 < (int)uStack_e40);
                        }
                        uVar33 = uVar33 & 0xffffffff00000000;
                        uVar26 = SkeletonRenderSeparator::AddToSkeletonRenderer(lVar29,0,0,5,uVar33,1,0);
                        *(undefined8 *)(param_1 + 0x130) = uVar26;
                        sub_8032e8d0(param_1 + 0x130,uVar26);
                        if (*(longlong *)(param_1 + 0x130) == 0) goto code_r0x000181600f6f;
                        Behaviour::set_enabled(*(longlong *)(param_1 + 0x130),0,0);
                        if (*(longlong *)(param_1 + 0x130) == 0) goto code_r0x000181600f6f;
                        Behaviour::set_enabled(*(longlong *)(param_1 + 0x130),1,0);
                      }
                      lVar29 = sub_8032f970(uRam0000000187b3e4f0);
                      BasePartsData::.ctor(lVar29,0);
                      if (lVar29 != 0) {
                        *(undefined4 *)(lVar29 + 0x20) = 0;
                        BasePartsData::set_Owner(lVar29,param_1,0);
                        if (cRam000000018ab63077 == '\0') {
                          sub_8032f6d0(0x187bfb7a8);
                          cRam000000018ab63077 = '\x01';
                        }
                        sub_80438e60(lVar29,*(undefined8 *)
                                                    (*(longlong *)(lRam0000000187bfb7a8 + 0xb8) +
                                                    0x28),0,lVar29);
                        lVar32 = *(longlong *)(lVar25 + 0xb8);
                        uVar19 = *(undefined4 *)(lVar25 + 0xc0);
                        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                          sub_8032fa70();
                        }
                        lStack_e68 = lVar32;
                        uStack_e60 = uVar19;
                        uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
                        BasePartsData::SetAbnormalResistId(lVar29,uVar19,1,0);
                        lStack_e68 = *(longlong *)(lVar25 + 0xc4);
                        uStack_e60 = *(undefined4 *)(lVar25 + 0xcc);
                        uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e68,0);
                        BasePartsData::SetDebuffResistId(lVar29,uVar19,1,0);
                        if (*(longlong *)(param_1 + 0x80) != 0) {
                          sub_80437880(*(longlong *)(param_1 + 0x80),lVar29,0);
                          if (cRam000000018ab63077 == '\0') {
                            sub_8032f6d0(0x187bfb7a8);
                            cRam000000018ab63077 = '\x01';
                          }
                          lVar32 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
                          if (lVar32 != 0) {
                            BattleManager::SetPartsTalentWeakness(lVar32,*(undefined4 *)(param_1 + 0x490),lVar29,0);
                            lVar29 = sub_80003d20(uRam0000000187b3c708);
                            if ((lVar29 != 0) && (*(longlong *)(lVar29 + 0xc0) != 0)) {
                              lVar29 = ILBPBODGJHD::GAINNJHMLLE(*(longlong *)(lVar29 + 0xc0),
                                                     *(undefined4 *)(param_1 + 0x488),0);
                              lStack_e68 = lVar29;
                              if ((*(char *)(param_1 + 0x1a8) != '\0') && (lVar29 != 0)) {
                                lVar32 = *(longlong *)(lVar29 + 0x214);
                                uVar19 = *(undefined4 *)(lVar29 + 0x21c);
                                if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                  sub_8032fa70();
                                }
                                lStack_e28 = lVar32;
                                uStack_e20 = uVar19;
                                iVar20 = ObscuredInt::FHOALPFLEEC(&lStack_e28,0);
                                if (0 < iVar20) {
                                  lVar32 = sub_8032f970(uRam0000000187bd11f0);
                                  PartsData::.ctor(lVar32,0);
                                  if (lVar32 == 0) goto code_r0x000181600f6f;
                                  BasePartsData::set_Owner(lVar32,param_1,0);
                                  *(undefined4 *)(lVar32 + 0x20) = 0;
                                  lVar31 = *(longlong *)(lVar29 + 0x214);
                                  uVar19 = *(undefined4 *)(lVar29 + 0x21c);
                                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                    sub_8032fa70();
                                  }
                                  lStack_e28 = lVar31;
                                  uStack_e20 = uVar19;
                                  iVar20 = ObscuredInt::FHOALPFLEEC(&lStack_e28,0);
                                  *(longlong *)(lVar32 + 0x2e8) = (longlong)iVar20;
                                  uVar26 = sub_815f21c0(param_1,0);
                                  sub_80438e60(lVar32,uVar26,0);
                                  if (*(longlong *)(param_1 + 0x80) == 0) goto code_r0x000181600f6f;
                                  sub_80437d70(*(longlong *)(param_1 + 0x80),lVar32,0);
                                  lVar29 = sub_815f21c0(param_1,0);
                                  if (lVar29 == 0) goto code_r0x000181600f6f;
                                  *(int *)(lVar29 + 0x378) = *(int *)(lVar29 + 0x378) + 1;
                                  if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181600f6f;
                                  *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0x189) = 1;
                                }
                              }
                              if (cRam000000018ab6306a == '\0') {
                                sub_8032f6d0(0x187b9ead8);
                                cRam000000018ab6306a = '\x01';
                              }
                              uVar19 = (undefined4)(uVar33 >> 0x20);
                              if (*(longlong *)(param_1 + 0x388) != 0) {
                                if (*(int *)(*(longlong *)(param_1 + 0x388) + 0x18) < 1) {
code_r0x0001815fd1d9:
                                  uVar26 = sub_8032f970(uRam0000000187bea0a0);
                                  sub_85575ea0(uVar26,uRam0000000187c409e0);
                                  *(undefined8 *)(param_1 + 0x478) = uVar26;
                                  sub_8032e8d0(param_1 + 0x478,uVar26);
                                  lVar29 = sub_80003d20(uRam0000000187b3c708);
                                  if (lVar29 != 0) {
                                    *(undefined8 *)(lVar15 + 0x18) = *(undefined8 *)(lVar29 + 0x3d8)
                                    ;
                                    sub_8032e8d0(lVar15 + 0x18);
                                    lVar29 = sub_8032f970(uRam0000000187b6ed88);
                                    lStack_d40 = lVar29;
                                    sub_856f2ce0(lVar29,lVar15,uRam0000000187bd4948,0);
                                    if (((*(longlong *)(lVar15 + 0x20) != 0) &&
                                        (lVar32 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38)
                                        , lVar32 != 0)) && (lVar29 != 0)) {
                                      (**(code **)(lVar29 + 0x18))
                                                (*(undefined8 *)(lVar29 + 0x40),
                                                 *(undefined8 *)(lVar32 + 0x10),
                                                 *(undefined8 *)(lVar25 + 0x68),0x29,
                                                 CONCAT44(uVar19,0xd),*(undefined8 *)(lVar29 + 0x28)
                                                );
                                      if ((*(longlong *)(lVar15 + 0x20) != 0) &&
                                         (lVar32 = *(longlong *)
                                                    (*(longlong *)(lVar15 + 0x20) + 0x38),
                                         lVar32 != 0)) {
                                        (**(code **)(lVar29 + 0x18))
                                                  (*(undefined8 *)(lVar29 + 0x40),
                                                   *(undefined8 *)(lVar32 + 0x48),
                                                   *(undefined8 *)(lVar25 + 0x68),0x29,0xd,
                                                   *(undefined8 *)(lVar29 + 0x28));
                                        if ((*(longlong *)(lVar15 + 0x20) != 0) &&
                                           (lVar32 = *(longlong *)
                                                      (*(longlong *)(lVar15 + 0x20) + 0x38),
                                           lVar32 != 0)) {
                                          (**(code **)(lVar29 + 0x18))
                                                    (*(undefined8 *)(lVar29 + 0x40),
                                                     *(undefined8 *)(lVar32 + 0x28),
                                                     *(undefined8 *)(lVar25 + 0x70),0x29,0xd,
                                                     *(undefined8 *)(lVar29 + 0x28));
                                          if ((*(longlong *)(lVar15 + 0x20) != 0) &&
                                             (lVar32 = *(longlong *)
                                                        (*(longlong *)(lVar15 + 0x20) + 0x38),
                                             lVar32 != 0)) {
                                            (**(code **)(lVar29 + 0x18))
                                                      (*(undefined8 *)(lVar29 + 0x40),
                                                       *(undefined8 *)(lVar32 + 0x50),
                                                       *(undefined8 *)(lVar25 + 0x70),0x29,0xd,
                                                       *(undefined8 *)(lVar29 + 0x28));
                                            if ((*(longlong *)(lVar15 + 0x20) != 0) &&
                                               (lVar32 = *(longlong *)
                                                          (*(longlong *)(lVar15 + 0x20) + 0x38),
                                               lVar32 != 0)) {
                                              (**(code **)(lVar29 + 0x18))
                                                        (*(undefined8 *)(lVar29 + 0x40),
                                                         *(undefined8 *)(lVar32 + 0x58),
                                                         *(undefined8 *)(lVar25 + 0x70),0x29,0xd,
                                                         *(undefined8 *)(lVar29 + 0x28));
                                              lVar29 = sub_8032f970(uRam0000000187c20ee0);
                                              sub_849de5b0(lVar29,uRam0000000187c329c0);
                                              lVar32 = sub_80003d20(uRam0000000187b3c708);
                                              if (lVar32 != 0) {
                                                lStack_df8 = *(longlong *)(lVar32 + 0x408);
                                                plVar34 = plVar39;
                                                while( true ) {
                                                  lVar32 = lStack_d40;
                                                  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                      (lVar31 = *(longlong *)
                                                                 (*(longlong *)(lVar15 + 0x20) +
                                                                 0x38), lVar31 == 0)) ||
                                                     (lVar31 = *(longlong *)(lVar31 + 0x30),
                                                     lVar31 == 0)) goto code_r0x000181600f6f;
                                                  iVar20 = (int)plVar34;
                                                  if (*(int *)(lVar31 + 0x18) <= iVar20) break;
                                                  lVar32 = sub_8032f970(uRam0000000187b78350)
                                                  ;
                                                  lStack_e48 = lVar32;
                                                  SkillLevelInfo::.ctor(lVar32,0);
                                                  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                      (lVar31 = *(longlong *)
                                                                 (*(longlong *)(lVar15 + 0x20) +
                                                                 0x38), lVar31 == 0)) ||
                                                     (lVar31 = *(longlong *)(lVar31 + 0x30),
                                                     lVar31 == 0)) goto code_r0x000181600f6f;
                                                  uVar19 = sub_84a610f0(lVar31,plVar34,
                                                                         uRam0000000187b446b8);
                                                  uStack_e40 = CONCAT44(uStack_e40._4_4_,uVar19);
                                                  if ((lVar32 == 0) ||
                                                     (SkillLevelInfo::SetSkillId(lVar32,uVar19,0),
                                                     lStack_df8 == 0)) goto code_r0x000181600f6f;
                                                  lVar32 = BBEJHAHPDCK::BKEJKNOJCEE(lStack_df8,
                                                                         uStack_e40 & 0xffffffff,0);
                                                  if (lVar32 == 0) {
code_r0x0001815fd900:
                                                    lVar32 = *(longlong *)(param_1 + 0x88);
                                                    if (lVar32 == 0) goto code_r0x000181600f6f;
                                                    uVar26 = *(undefined8 *)(lVar32 + 0x420);
                                                    uVar19 = *(undefined4 *)(lVar32 + 0x428);
                                                    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0)
                                                    {
                                                      sub_8032fa70();
                                                    }
                                                    uStack_d98 = uVar26;
                                                    uStack_d90 = uVar19;
                                                    uVar19 = ObscuredInt::FHOALPFLEEC(&uStack_d98,0);
                                                    lVar32 = lStack_e48;
                                                    uStack_e40 = CONCAT44(uStack_e40._4_4_,uVar19);
                                                    SkillLevelInfo::SetSkillLevel(lStack_e48,uVar19,0);
                                                    if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                        (lVar31 = *(longlong *)
                                                                   (*(longlong *)(lVar15 + 0x20) +
                                                                   0x38), lVar31 == 0)) ||
                                                       (lVar31 = *(longlong *)(lVar31 + 0x40),
                                                       lVar31 == 0)) goto code_r0x000181600f6f;
                                                    if (iVar20 < *(int *)(lVar31 + 0x18)) {
                                                      lStack_e58 = sub_8032f970(
                                                  uRam0000000187b78350);
                                                  SkillLevelInfo::.ctor(lStack_e58,0);
                                                  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                      (lVar31 = *(longlong *)
                                                                 (*(longlong *)(lVar15 + 0x20) +
                                                                 0x38), lVar31 == 0)) ||
                                                     ((lVar31 = *(longlong *)(lVar31 + 0x40),
                                                      lVar31 == 0 ||
                                                      (uVar19 = sub_84a610f0(lVar31,plVar34,
                                                                              uRam0000000187b446b8),
                                                      lStack_e58 == 0)))) goto code_r0x000181600f6f;
                                                  SkillLevelInfo::SetSkillId(lStack_e58,uVar19,0);
                                                  SkillLevelInfo::SetSkillLevel(lStack_e58,uStack_e40 & 0xffffffff,0
                                                               );
                                                  if (lVar29 == 0) goto code_r0x000181600f6f;
                                                  sub_80002460(lVar29,lStack_e58,
                                                                uRam0000000187c32d28);
                                                  }
                                                  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                      (lVar31 = *(longlong *)
                                                                 (*(longlong *)(lVar15 + 0x20) +
                                                                 0x38), lVar31 == 0)) ||
                                                     (lVar31 = *(longlong *)(lVar31 + 0x38),
                                                     lVar31 == 0)) goto code_r0x000181600f6f;
                                                  if (*(int *)(lVar31 + 0x18) <= iVar20)
                                                  goto code_r0x0001815fd8dc;
                                                  lStack_e58 = sub_8032f970(
                                                  uRam0000000187b78350);
                                                  SkillLevelInfo::.ctor(lStack_e58,0);
                                                  if ((((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                       (lVar31 = *(longlong *)
                                                                  (*(longlong *)(lVar15 + 0x20) +
                                                                  0x38), lVar31 == 0)) ||
                                                      (lVar31 = *(longlong *)(lVar31 + 0x38),
                                                      lVar31 == 0)) ||
                                                     (uVar19 = sub_84a610f0(lVar31,plVar34,
                                                                             uRam0000000187b446b8),
                                                     lStack_e58 == 0)) goto code_r0x000181600f6f;
                                                  SkillLevelInfo::SetSkillId(lStack_e58,uVar19,0);
                                                  SkillLevelInfo::SetSkillLevel(lStack_e58,uStack_e40 & 0xffffffff,0
                                                               );
                                                  if (lVar29 == 0) goto code_r0x000181600f6f;
                                                  sub_80002460(lVar29,lStack_e58,
                                                                uRam0000000187c32d28);
                                                  }
                                                  else {
                                                    lStack_e58 = *(longlong *)(param_1 + 0x500);
                                                    lVar31 = *(longlong *)(lVar32 + 0x1c);
                                                    uStack_e38 = CONCAT44(uStack_e38._4_4_,
                                                                          *(undefined4 *)
                                                                           (lVar32 + 0x24));
                                                    uVar19 = *(undefined4 *)(lVar32 + 0x24);
                                                    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0)
                                                    {
                                                      sub_8032fa70();
                                                      uVar19 = (undefined4)uStack_e38;
                                                    }
                                                    lStack_e28 = lVar31;
                                                    uStack_e20 = uVar19;
                                                    uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e28,0);
                                                    if (lStack_e58 == 0) goto code_r0x000181600f6f;
                                                    cVar17 = sub_85568500(lStack_e58,uVar19,
                                                                           uRam0000000187c1c530);
                                                    if (cVar17 == '\0') goto code_r0x0001815fd900;
                                                    if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                        (lVar31 = *(longlong *)
                                                                   (*(longlong *)(lVar15 + 0x20) +
                                                                   0x38), lVar31 == 0)) ||
                                                       (lVar31 = *(longlong *)(lVar31 + 0x40),
                                                       lVar31 == 0)) goto code_r0x000181600f6f;
                                                    if (iVar20 < *(int *)(lVar31 + 0x18)) {
                                                      lStack_e58 = *(longlong *)(param_1 + 0x4c0);
                                                      uVar19 = sub_84a610f0(*(undefined8 *)
                                                                              (*(longlong *)
                                                                                (*(longlong *)
                                                                                  (lVar15 + 0x20) +
                                                                                0x38) + 0x40),
                                                                             plVar34,
                                                  uRam0000000187b446b8);
                                                  if (lStack_e58 == 0) goto code_r0x000181600f6f;
                                                  sub_80002bb0(lStack_e58,uVar19,
                                                                uRam0000000187b418c0);
                                                  lStack_e58 = sub_8032f970(
                                                  uRam0000000187b78350);
                                                  SkillLevelInfo::.ctor(lStack_e58,0);
                                                  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                      (lVar31 = *(longlong *)
                                                                 (*(longlong *)(lVar15 + 0x20) +
                                                                 0x38), lVar31 == 0)) ||
                                                     ((lVar31 = *(longlong *)(lVar31 + 0x40),
                                                      lVar31 == 0 ||
                                                      (uVar19 = sub_84a610f0(lVar31,plVar34,
                                                                              uRam0000000187b446b8),
                                                      lStack_e58 == 0)))) goto code_r0x000181600f6f;
                                                  SkillLevelInfo::SetSkillId(lStack_e58,uVar19,0);
                                                  uVar26 = *(undefined8 *)(lVar32 + 0x1c);
                                                  uStack_e38 = CONCAT44(uStack_e38._4_4_,
                                                                        *(undefined4 *)
                                                                         (lVar32 + 0x24));
                                                  uVar19 = *(undefined4 *)(lVar32 + 0x24);
                                                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                                    sub_8032fa70();
                                                    uVar19 = (undefined4)uStack_e38;
                                                  }
                                                  puStack_dd0 = (undefined8 *)
                                                                CONCAT44(puStack_dd0._4_4_,uVar19);
                                                  uStack_dd8 = uVar26;
                                                  uVar19 = ObscuredInt::FHOALPFLEEC(&uStack_dd8,0);
                                                  uVar19 = UnitCtrl::searchSpSkillLevel(param_1,uVar19,
                                                                         *(undefined8 *)
                                                                          (lVar15 + 0x20),0);
                                                  SkillLevelInfo::SetSkillLevel(lStack_e58,uVar19,0);
                                                  if (lVar29 == 0) goto code_r0x000181600f6f;
                                                  sub_80002460(lVar29,lStack_e58,
                                                                uRam0000000187c32d28);
                                                  }
                                                  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                      (lVar31 = *(longlong *)
                                                                 (*(longlong *)(lVar15 + 0x20) +
                                                                 0x38), lVar31 == 0)) ||
                                                     (lVar31 = *(longlong *)(lVar31 + 0x38),
                                                     lVar31 == 0)) goto code_r0x000181600f6f;
                                                  if (iVar20 < *(int *)(lVar31 + 0x18)) {
                                                    lStack_e58 = *(longlong *)(param_1 + 0x4c0);
                                                    uVar19 = sub_84a610f0(*(undefined8 *)
                                                                            (*(longlong *)
                                                                              (*(longlong *)
                                                                                (lVar15 + 0x20) +
                                                                              0x38) + 0x38),plVar34,
                                                                           uRam0000000187b446b8);
                                                    if (lStack_e58 == 0) goto code_r0x000181600f6f;
                                                    sub_80002bb0(lStack_e58,uVar19,
                                                                  uRam0000000187b418c0);
                                                    lStack_e58 = sub_8032f970(
                                                  uRam0000000187b78350);
                                                  SkillLevelInfo::.ctor(lStack_e58,0);
                                                  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
                                                      (lVar31 = *(longlong *)
                                                                 (*(longlong *)(lVar15 + 0x20) +
                                                                 0x38), lVar31 == 0)) ||
                                                     ((lVar31 = *(longlong *)(lVar31 + 0x38),
                                                      lVar31 == 0 ||
                                                      (uVar19 = sub_84a610f0(lVar31,plVar34,
                                                                              uRam0000000187b446b8),
                                                      lStack_e58 == 0)))) goto code_r0x000181600f6f;
                                                  SkillLevelInfo::SetSkillId(lStack_e58,uVar19,0);
                                                  uVar26 = *(undefined8 *)(lVar32 + 0x1c);
                                                  uStack_e38 = CONCAT44(uStack_e38._4_4_,
                                                                        *(undefined4 *)
                                                                         (lVar32 + 0x24));
                                                  uVar19 = *(undefined4 *)(lVar32 + 0x24);
                                                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                                    sub_8032fa70();
                                                    uVar19 = (undefined4)uStack_e38;
                                                  }
                                                  uStack_da8 = uVar26;
                                                  uStack_da0 = uVar19;
                                                  uVar19 = ObscuredInt::FHOALPFLEEC(&uStack_da8,0);
                                                  uVar19 = UnitCtrl::searchSpSkillLevel(param_1,uVar19,
                                                                         *(undefined8 *)
                                                                          (lVar15 + 0x20),0);
                                                  SkillLevelInfo::SetSkillLevel(lStack_e58,uVar19,0);
                                                  if (lVar29 == 0) goto code_r0x000181600f6f;
                                                  sub_80002460(lVar29,lStack_e58,
                                                                uRam0000000187c32d28);
                                                  }
                                                  if (*(longlong *)(param_1 + 0x4c0) == 0)
                                                  goto code_r0x000181600f6f;
                                                  sub_80002bb0(*(longlong *)(param_1 + 0x4c0),
                                                                uStack_e40 & 0xffffffff,
                                                                uRam0000000187b418c0);
                                                  uVar26 = *(undefined8 *)(lVar32 + 0x1c);
                                                  uVar19 = *(undefined4 *)(lVar32 + 0x24);
                                                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                                                    sub_8032fa70();
                                                  }
                                                  uStack_d88 = uVar26;
                                                  uStack_d80 = uVar19;
                                                  uVar19 = ObscuredInt::FHOALPFLEEC(&uStack_d88,0);
                                                  uVar19 = UnitCtrl::searchSpSkillLevel(param_1,uVar19,
                                                                         *(undefined8 *)
                                                                          (lVar15 + 0x20),0);
                                                  lVar32 = lStack_e48;
                                                  SkillLevelInfo::SetSkillLevel(lStack_e48,uVar19,0);
code_r0x0001815fd8dc:
                                                  if (lVar29 == 0) goto code_r0x000181600f6f;
                                                  }
                                                  sub_80002460(lVar29,lVar32,uRam0000000187c32d28);
                                                  plVar34 = (longlong *)(ulonglong)(iVar20 + 1);
                                                }
                                                if ((*(longlong *)(lVar15 + 0x20) != 0) &&
                                                   (lVar31 = *(longlong *)
                                                              (*(longlong *)(lVar15 + 0x20) + 0x38),
                                                   lVar31 != 0)) {
                                                  (**(code **)(lStack_d40 + 0x18))
                                                            (*(undefined8 *)(lStack_d40 + 0x40),
                                                             *(undefined8 *)(lVar31 + 0x30),lVar29,
                                                             0x2a,0xe,*(undefined8 *)
                                                                       (lStack_d40 + 0x28));
                                                  if ((*(longlong *)(lVar15 + 0x20) != 0) &&
                                                     (lVar31 = *(longlong *)
                                                                (*(longlong *)(lVar15 + 0x20) + 0x38
                                                                ), lVar31 != 0)) {
                                                    (**(code **)(lVar32 + 0x18))
                                                              (*(undefined8 *)(lVar32 + 0x40),
                                                               *(undefined8 *)(lVar31 + 0x38),lVar29
                                                               ,0x2a,0xe,
                                                               *(undefined8 *)(lVar32 + 0x28));
                                                    if ((*(longlong *)(lVar15 + 0x20) != 0) &&
                                                       (lVar31 = *(longlong *)
                                                                  (*(longlong *)(lVar15 + 0x20) +
                                                                  0x38), lVar31 != 0)) {
                                                      (**(code **)(lVar32 + 0x18))
                                                                (*(undefined8 *)(lVar32 + 0x40),
                                                                 *(undefined8 *)(lVar31 + 0x40),
                                                                 lVar29,0x2a,0xe,
                                                                 *(undefined8 *)(lVar32 + 0x28));
                                                      lStack_e48 = sub_8032f970(
                                                  uRam0000000187c20ee0);
                                                  sub_849de5b0(lStack_e48,uRam0000000187c329c0);
                                                  plVar34 = plVar39;
                                                  goto code_r0x0001815fdc20;
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
                                else {
                                  lVar29 = sub_80003d20(uRam0000000187b3c708);
                                  if ((lVar29 != 0) && (*(longlong *)(lVar29 + 0xe0) != 0)) {
                                    lStack_df8 = NPHPFHLFDIA::BKEJKNOJCEE(*(longlong *)(lVar29 + 0xe0),
                                                               *(undefined4 *)(param_1 + 0x488),0);
                                    lVar29 = *(longlong *)(param_1 + 0x388);
                                    plVar34 = plVar39;
                                    if (lVar29 != 0) {
                                      while( true ) {
                                        uVar19 = (undefined4)(uVar33 >> 0x20);
                                        if (*(int *)(lVar29 + 0x18) <= (int)plVar34) break;
                                        if (*(longlong *)(param_1 + 0x388) == 0)
                                        goto code_r0x000181600f6f;
                                        lVar29 = sub_849ed2d0(*(longlong *)(param_1 + 0x388),
                                                               plVar34,uRam0000000187b9ec90);
                                        lVar32 = sub_8032f970(uRam0000000187bd11f0);
                                        PartsData::.ctor(lVar32,0);
                                        if ((lVar29 == 0) || (lVar32 == 0))
                                        goto code_r0x000181600f6f;
                                        *(undefined4 *)(lVar32 + 0x4c) =
                                             *(undefined4 *)(lVar29 + 0x4c);
                                        *(undefined4 *)(lVar32 + 0x20) =
                                             *(undefined4 *)(lVar29 + 0x20);
                                        *(undefined4 *)(lVar32 + 0x48) =
                                             *(undefined4 *)(lVar29 + 0x20);
                                        *(undefined4 *)(lVar32 + 0x54) =
                                             *(undefined4 *)(lVar29 + 0x54);
                                        *(undefined4 *)(lVar32 + 0x50) =
                                             *(undefined4 *)(lVar29 + 0x50);
                                        BasePartsData::set_Owner(lVar32,param_1,0);
                                        *(undefined4 *)(lVar32 + 0x24) =
                                             *(undefined4 *)(lVar29 + 0x24);
                                        uVar19 = *(undefined4 *)(lVar29 + 0xfc);
                                        *(undefined4 *)(lVar32 + 0xf8) =
                                             *(undefined4 *)(lVar29 + 0xf8);
                                        *(undefined4 *)(lVar32 + 0xfc) = uVar19;
                                        *(undefined4 *)(lVar32 + 0x100) =
                                             *(undefined4 *)(lVar29 + 0x100);
                                        *(undefined1 *)(lVar32 + 0x104) =
                                             *(undefined1 *)(lVar29 + 0x104);
                                        *(undefined8 *)(lVar32 + 0x28) =
                                             *(undefined8 *)(lVar29 + 0x28);
                                        sub_8032e8d0(lVar32 + 0x28);
                                        *(undefined4 *)(lVar32 + 0x34) =
                                             *(undefined4 *)(lVar29 + 0x34);
                                        *(undefined4 *)(lVar32 + 0x38) =
                                             *(undefined4 *)(lVar29 + 0x38);
                                        *(undefined1 *)(lVar32 + 0x30) =
                                             *(undefined1 *)(lVar29 + 0x30);
                                        *(undefined4 *)(lVar32 + 0x58) =
                                             *(undefined4 *)(lVar29 + 0x58);
                                        *(undefined4 *)(lVar32 + 0x60) =
                                             *(undefined4 *)(lVar29 + 0x60);
                                        *(undefined4 *)(lVar32 + 0x5c) =
                                             *(undefined4 *)(lVar29 + 0x5c);
                                        if (cRam000000018ab63077 == '\0') {
                                          sub_8032f6d0(0x187bfb7a8);
                                          cRam000000018ab63077 = '\x01';
                                        }
                                        PartsData::Initialize(lVar32,lStack_df8,
                                                      *(undefined8 *)
                                                       (*(longlong *)(lRam0000000187bfb7a8 + 0xb8) +
                                                       0x28));
                                        if (cRam000000018ab63077 == '\0') {
                                          sub_8032f6d0(0x187bfb7a8);
                                          cRam000000018ab63077 = '\x01';
                                        }
                                        sub_80438e60(lVar32,*(undefined8 *)
                                                                    (*(longlong *)
                                                                      (lRam0000000187bfb7a8 + 0xb8)
                                                                    + 0x28),0);
                                        if ((*(longlong *)(param_1 + 0x80) == 0) ||
                                           (lVar29 = *(longlong *)
                                                      (*(longlong *)(param_1 + 0x80) + 0x18),
                                           lVar29 == 0)) goto code_r0x000181600f6f;
                                        sub_804372b0(lVar29,lVar32,uRam0000000187b9e258);
                                        plVar34 = (longlong *)(ulonglong)((int)plVar34 + 1);
                                        lVar29 = *(longlong *)(param_1 + 0x388);
                                        if (lVar29 == 0) goto code_r0x000181600f6f;
                                      }
                                      if (cRam000000018ab63077 == '\0') {
                                        sub_8032f6d0(0x187bfb7a8);
                                        cRam000000018ab63077 = '\x01';
                                      }
                                      lVar29 = *(longlong *)
                                                (*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
                                      if (lVar29 != 0) {
                                        piVar1 = (int *)(lVar29 + 0x378);
                                        *piVar1 = *piVar1 + 1;
                                        if (*(longlong *)(param_1 + 0xd0) != 0) {
                                          *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0x189) = 1
                                          ;
                                          goto code_r0x0001815fd1d9;
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
  goto code_r0x000181600f6f;
code_r0x0001815fdc20:
  if (((*(longlong *)(lVar15 + 0x20) == 0) ||
      (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0)) ||
     (lVar29 = *(longlong *)(lVar29 + 0x60), lVar29 == 0)) goto code_r0x000181600f6f;
  if (*(int *)(lVar29 + 0x18) <= (int)plVar34) {
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    (**(code **)(lVar32 + 0x18))
              (*(undefined8 *)(lVar32 + 0x40),*(undefined8 *)(lVar29 + 0x60),lStack_e48,0x2f,0xf,
               *(undefined8 *)(lVar32 + 0x28));
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    *(undefined8 *)(param_1 + 0x510) = *(undefined8 *)(lVar29 + 0x28);
    sub_8032e8d0(param_1 + 0x510);
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    *(undefined8 *)(param_1 + 0x518) = *(undefined8 *)(lVar29 + 0x30);
    sub_8032e8d0(param_1 + 0x518);
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    *(undefined8 *)(param_1 + 0x528) = *(undefined8 *)(lVar29 + 0x38);
    sub_8032e8d0(param_1 + 0x528);
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    *(undefined8 *)(param_1 + 0x538) = *(undefined8 *)(lVar29 + 0x40);
    sub_8032e8d0(param_1 + 0x538);
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    *(undefined8 *)(param_1 + 0x520) = *(undefined8 *)(lVar29 + 0x50);
    sub_8032e8d0(param_1 + 0x520);
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    *(undefined8 *)(param_1 + 0x530) = *(undefined8 *)(lVar29 + 0x58);
    sub_8032e8d0(param_1 + 0x530);
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0))
    goto code_r0x000181600f6f;
    *(undefined8 *)(param_1 + 0x540) = *(undefined8 *)(lVar29 + 0x60);
    sub_8032e8d0(param_1 + 0x540);
    if (((*(longlong *)(lVar15 + 0x20) == 0) ||
        (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0)) ||
       (lVar29 = *(longlong *)(lVar29 + 0x48), lVar29 == 0)) goto code_r0x000181600f6f;
    iVar20 = sub_84a610f0(lVar29,0,uRam0000000187b446b8);
    if (iVar20 == 0) {
code_r0x0001815fdf60:
      if (((*(longlong *)(lVar15 + 0x20) == 0) ||
          (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar29 == 0)) ||
         (lVar29 = *(longlong *)(lVar29 + 0x10), lVar29 == 0)) goto code_r0x000181600f6f;
      iVar20 = sub_84a610f0(lVar29,0,uRam0000000187b446b8);
    }
    else {
      if (*(longlong *)(param_1 + 0x500) == 0) goto code_r0x000181600f6f;
      iVar23 = sub_855ac210(*(longlong *)(param_1 + 0x500),iVar20,uRam0000000187c1cf68);
      if (iVar23 == 0) goto code_r0x0001815fdf60;
    }
    *(int *)(lStack_de8 + 0x4b8) = iVar20;
    if (*(int *)(param_1 + 0x4b8) != 0) {
      if ((*(longlong *)(lVar15 + 0x18) == 0) ||
         (lVar29 = HOEABFDCBIG::CBBMJADBDGF(*(longlong *)(lVar15 + 0x18),*(undefined4 *)(param_1 + 0x4b8)
                                       ,0), lVar29 == 0)) goto code_r0x000181600f6f;
      lVar32 = *(longlong *)(lVar29 + 0x68);
      uVar19 = *(undefined4 *)(lVar29 + 0x70);
      if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
        sub_8032fa70();
      }
      lStack_e28 = lVar32;
      uStack_e20 = uVar19;
      iVar20 = ObscuredInt::FHOALPFLEEC(&lStack_e28,0);
      *(float *)(param_1 + 0x474) = (float)iVar20;
    }
    if ((*(longlong *)(lVar15 + 0x20) == 0) || (*(longlong *)(param_1 + 0x40) == 0))
    goto code_r0x000181600f6f;
    MGAEMKEEHND::GOCPKPNIGGE(*(longlong *)(param_1 + 0x40),*(undefined8 *)(*(longlong *)(lVar15 + 0x20) + 0x38)
                  ,0);
    uVar19 = *(undefined4 *)(param_1 + 0x490);
    if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar17 = UnitUtility::IsPlayableUnit(uVar19,0);
    if (((cVar17 != '\0') && (*(int *)(param_1 + 0x180) != 1)) && (*(int *)(param_1 + 0x180) != 2))
    {
      bVar41 = *(char *)(param_1 + 0x184) == '\0';
    }
    if (((bStack_e50 & bVar41) != 0) &&
       ((*(longlong *)(lVar15 + 0x20) == 0 ||
        (lVar25 = UnitParameter::get_LimitedUnitData(*(longlong *)(lVar15 + 0x20),0), lStack_df0 = lVar25, lVar25 == 0)))
       ) goto code_r0x000181600f6f;
    uVar26 = UnitData::get_TotalHp(lVar25,0);
    lVar29 = *(longlong *)(param_1 + 0x88);
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_208,uVar26,0);
    uStack_d78 = *puVar30;
    uStack_d70 = puVar30[1];
    lStack_e28 = puVar30[2];
    iVar20 = UnitData::get_TotalAtk(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_610,(longlong)iVar20,0);
    uStack_b68 = *puVar30;
    uStack_b60 = puVar30[1];
    uStack_dd8 = puVar30[2];
    iVar20 = UnitData::get_TotalDef(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_5f8,(longlong)iVar20,0);
    uStack_b88 = *puVar30;
    uStack_b80 = puVar30[1];
    uStack_da8 = puVar30[2];
    iVar20 = UnitData::get_TotalMagicAtk(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_5e0,(longlong)iVar20,0);
    uStack_ba8 = *puVar30;
    uStack_ba0 = puVar30[1];
    uStack_d88 = puVar30[2];
    iVar20 = UnitData::get_TotalMagicDef(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_5c8,(longlong)iVar20,0);
    uStack_bc8 = *puVar30;
    uStack_bc0 = puVar30[1];
    uStack_d98 = puVar30[2];
    iVar20 = UnitData::get_TotalWaveHpRecovery(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_5b0,(longlong)iVar20,0);
    uStack_768 = *puVar30;
    uStack_760 = puVar30[1];
    uStack_d18 = puVar30[2];
    iVar20 = UnitData::get_TotalWaveEnergyRecovery(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_598,(longlong)iVar20,0);
    uStack_788 = *puVar30;
    uStack_780 = puVar30[1];
    uStack_d08 = puVar30[2];
    iVar20 = UnitData::get_TotalCritical(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_580,(longlong)iVar20,0);
    uStack_7a8 = *puVar30;
    uStack_7a0 = puVar30[1];
    uStack_cf8 = puVar30[2];
    iVar20 = UnitData::get_TotalMagicCritical(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_568,(longlong)iVar20,0);
    uStack_7c8 = *puVar30;
    uStack_7c0 = puVar30[1];
    uStack_ce8 = puVar30[2];
    iVar20 = UnitData::get_TotalDodge(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_550,(longlong)iVar20,0);
    uStack_7e8 = *puVar30;
    uStack_7e0 = puVar30[1];
    uStack_cd8 = puVar30[2];
    iVar20 = UnitData::get_TotalAccuracy(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_538,(longlong)iVar20,0);
    uStack_808 = *puVar30;
    uStack_800 = puVar30[1];
    uStack_c68 = puVar30[2];
    iVar20 = UnitData::get_TotalPhysicalPenetrate(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_520,(longlong)iVar20,0);
    uStack_848 = *puVar30;
    uStack_840 = puVar30[1];
    uStack_be0 = puVar30[2];
    iVar20 = UnitData::get_TotalMagicPenetrate(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_508,(longlong)iVar20,0);
    uStack_868 = *puVar30;
    uStack_860 = puVar30[1];
    uStack_bf8 = puVar30[2];
    iVar20 = UnitData::get_TotalLifeSteal(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_4f0,(longlong)iVar20,0);
    uStack_828 = *puVar30;
    uStack_820 = puVar30[1];
    uStack_bd8 = puVar30[2];
    iVar20 = UnitData::get_TotalHpRecoveryRate(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_4d8,(longlong)iVar20,0);
    uStack_888 = *puVar30;
    uStack_880 = puVar30[1];
    uStack_c08 = puVar30[2];
    iVar20 = UnitData::get_TotalEnergyRecoveryRate(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_4c0,(longlong)iVar20,0);
    uStack_8a8 = *puVar30;
    uStack_8a0 = puVar30[1];
    uStack_c30 = puVar30[2];
    iVar20 = UnitData::get_TotalEnergyReduceRate(lVar25,0);
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_4a8,(longlong)iVar20,0);
    uStack_8c8 = *puVar30;
    uStack_8c0 = puVar30[1];
    uStack_c00 = puVar30[2];
    uVar19 = *(undefined4 *)(param_1 + 0x490);
    if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar17 = UnitUtility::IsPlayableUnit(uVar19,0);
    if (cVar17 == '\0') {
      uVar26 = UnitData::get_TotalHp(lVar25,0);
    }
    else {
      if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uVar26 = UnitUtility::CalcUnitParameterForDamagedEnergy(1,param_1);
    }
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_490,uVar26,0);
    uVar28 = *puVar30;
    uVar27 = puVar30[1];
    uVar26 = puVar30[2];
    uVar19 = *(undefined4 *)(param_1 + 0x490);
    if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar17 = UnitUtility::IsPlayableUnit(uVar19,0);
    if (cVar17 == '\0') {
      iVar20 = UnitData::get_TotalDef(lVar25,0);
      lVar32 = (longlong)iVar20;
    }
    else {
      if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
        sub_8032fa70();
      }
      lVar32 = UnitUtility::CalcUnitParameterForDamagedEnergy(3,param_1);
    }
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_478,lVar32,0);
    uVar5 = *puVar30;
    uVar6 = puVar30[1];
    uVar4 = puVar30[2];
    uVar19 = *(undefined4 *)(param_1 + 0x490);
    if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar17 = UnitUtility::IsPlayableUnit(uVar19,0);
    if (cVar17 == '\0') {
      iVar20 = UnitData::get_TotalMagicDef(lVar25,0);
      lVar32 = (longlong)iVar20;
    }
    else {
      if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
        sub_8032fa70();
      }
      lVar32 = UnitUtility::CalcUnitParameterForDamagedEnergy(5,param_1);
    }
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_460,lVar32,0);
    uVar9 = *puVar30;
    uVar10 = puVar30[1];
    uVar7 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_448,100);
    uStack_8e8 = *puVar30;
    uStack_8e0 = puVar30[1];
    uStack_c60 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_430,100);
    uStack_908 = *puVar30;
    uStack_900 = puVar30[1];
    uStack_c58 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_418,0,0);
    uStack_928 = *puVar30;
    uStack_920 = puVar30[1];
    uStack_c50 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_400,0,0);
    uStack_948 = *puVar30;
    uStack_940 = puVar30[1];
    uStack_c48 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_3e8,100);
    uStack_968 = *puVar30;
    uStack_960 = puVar30[1];
    uStack_c40 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_3d0,0,0);
    uStack_988 = *puVar30;
    uStack_980 = puVar30[1];
    uStack_bd0 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_3b8,0,0);
    uStack_9a8 = *puVar30;
    uStack_9a0 = puVar30[1];
    uStack_c38 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_3a0,0,0);
    uStack_9c8 = *puVar30;
    uStack_9c0 = puVar30[1];
    uStack_c28 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_388,0,0);
    uStack_9e8 = *puVar30;
    uStack_9e0 = puVar30[1];
    uStack_c20 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_370,0,0);
    uStack_a08 = *puVar30;
    uStack_a00 = puVar30[1];
    uStack_c18 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_358,0,0);
    uStack_a28 = *puVar30;
    uStack_a20 = puVar30[1];
    uStack_c10 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_340,0,0);
    uStack_a48 = *puVar30;
    uStack_a40 = puVar30[1];
    uStack_bf0 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_328,0,0);
    uStack_a68 = *puVar30;
    uStack_a60 = puVar30[1];
    uStack_be8 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_310,0,0);
    uStack_a88 = *puVar30;
    uStack_a80 = puVar30[1];
    uStack_e40 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_2f8,0,0);
    uStack_aa8 = *puVar30;
    uStack_aa0 = puVar30[1];
    uStack_e38 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_2e0,0,0);
    uStack_ac8 = *puVar30;
    uStack_ac0 = puVar30[1];
    lStack_de8 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_2c8,0,0);
    uStack_ae8 = *puVar30;
    uStack_ae0 = puVar30[1];
    lStack_e58 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_2b0,0,0);
    uStack_b08 = *puVar30;
    uStack_b00 = puVar30[1];
    lStack_e48 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_298,0,0);
    uStack_b28 = *puVar30;
    uStack_b20 = puVar30[1];
    lStack_df8 = puVar30[2];
    puVar30 = (undefined8 *)ObscuredLong::FHOALPFLEEC(auStack_280,0,0);
    uStack_b48 = *puVar30;
    uStack_b40 = puVar30[1];
    lStack_d40 = puVar30[2];
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puVar30 = (undefined8 *)ObscuredDouble::FHOALPFLEEC(auStack_1f0);
    uVar11 = *puVar30;
    uVar12 = puVar30[1];
    uVar13 = puVar30[2];
    uVar14 = puVar30[3];
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar32 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar32 == 0))
    goto code_r0x000181600f6f;
    uVar3 = *(undefined8 *)(lVar32 + 0x84);
    uVar19 = *(undefined4 *)(lVar32 + 0x8c);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puStack_e10 = (undefined8 *)CONCAT44(puStack_e10._4_4_,uVar19);
    uStack_e18 = uVar3;
    puVar30 = (undefined8 *)ObscuredInt::FHOALPFLEEC(auStack_268,&uStack_e18,0);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    uStack_e18 = *puVar30;
    puStack_e10 = (undefined8 *)puVar30[1];
    uStack_e08 = puVar30[2];
    lStack_b38 = lStack_d40;
    lStack_b18 = lStack_df8;
    lStack_af8 = lStack_e48;
    lStack_ad8 = lStack_e58;
    lStack_ab8 = lStack_de8;
    uStack_a98 = uStack_e38;
    uStack_a78 = uStack_e40;
    uStack_a58 = uStack_be8;
    uStack_a38 = uStack_bf0;
    uStack_a18 = uStack_c10;
    uStack_9f8 = uStack_c18;
    uStack_9d8 = uStack_c20;
    uStack_9b8 = uStack_c28;
    uStack_998 = uStack_c38;
    uStack_978 = uStack_bd0;
    uStack_958 = uStack_c40;
    uStack_938 = uStack_c48;
    uStack_918 = uStack_c50;
    uStack_8f8 = uStack_c58;
    uStack_8d8 = uStack_c60;
    uStack_8b8 = uStack_c00;
    uStack_898 = uStack_c30;
    uStack_878 = uStack_c08;
    uStack_858 = uStack_bf8;
    uStack_838 = uStack_be0;
    uStack_818 = uStack_bd8;
    uStack_7f8 = uStack_c68;
    uStack_7d8 = uStack_cd8;
    uStack_7b8 = uStack_ce8;
    uStack_798 = uStack_cf8;
    uStack_778 = uStack_d08;
    uStack_758 = uStack_d18;
    uStack_bb8 = uStack_d98;
    uStack_b98 = uStack_d88;
    uStack_b78 = uStack_da8;
    uStack_b58 = uStack_dd8;
    lStack_d68 = lStack_e28;
    puVar30 = &uStack_7e8;
    puVar51 = &uStack_7c8;
    puVar50 = &uStack_7a8;
    puVar48 = &uStack_788;
    puVar46 = &uStack_ba8;
    uStack_dc8 = uVar28;
    uStack_dc0 = uVar27;
    uStack_db8 = uVar26;
    uStack_d38 = uVar11;
    uStack_d30 = uVar12;
    uStack_d28 = uVar13;
    uStack_d20 = uVar14;
    uStack_6f8 = uVar9;
    uStack_6f0 = uVar10;
    uStack_6e8 = uVar7;
    uStack_6d8 = uVar5;
    uStack_6d0 = uVar6;
    uStack_6c8 = uVar4;
    CIFIMDJAGGF::LPIAADFAHOM(lVar29,&uStack_d78,&uStack_b68,&uStack_b88,puVar46,&uStack_bc8,&uStack_768,puVar48
                  ,puVar50,puVar51,puVar30,&uStack_808,&uStack_828,&uStack_848,&uStack_868,
                  &uStack_888,&uStack_8a8,&uStack_8c8,&uStack_dc8,&uStack_6d8,&uStack_6f8,
                  &uStack_8e8,&uStack_908,&uStack_928,&uStack_948,&uStack_968,&uStack_988,
                  &uStack_9a8,&uStack_9c8,&uStack_9e8,&uStack_a08,&uStack_a28,&uStack_a48,
                  &uStack_a68,&uStack_a88,&uStack_aa8,&uStack_ac8,&uStack_ae8,&uStack_b08,
                  &uStack_b28,&uStack_b48,&uStack_d38,&uStack_e18,0);
    uVar24 = (undefined4)((ulonglong)puVar46 >> 0x20);
    uVar21 = (undefined4)((ulonglong)puVar51 >> 0x20);
    uVar22 = (undefined4)((ulonglong)puVar30 >> 0x20);
    uVar26 = *(undefined8 *)(param_1 + 0x4f0);
    uVar19 = *(undefined4 *)(param_1 + 0x4f8);
    if (*(int *)(lRam0000000187b60538 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puStack_e10 = (undefined8 *)CONCAT44(puStack_e10._4_4_,uVar19);
    uStack_e18 = uVar26;
    cVar17 = ObscuredBool::FHOALPFLEEC(&uStack_e18,0);
    if (cVar17 == '\0') {
      if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x000181600f6f;
      puVar30 = (undefined8 *)MBJPLNBCBDB::ODOPDBNIJNO(auStack_250,*(longlong *)(param_1 + 0x90),0);
      uVar28 = *puVar30;
      uVar27 = puVar30[1];
      uVar26 = puVar30[2];
      if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_dc8 = uVar28;
      uStack_dc0 = uVar27;
      uStack_db8 = uVar26;
      fVar42 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_dc8,0);
      if (param_3 == '\0') {
        fVar45 = 1.0;
      }
      else {
        fVar45 = -1.0;
      }
      fVar45 = fVar45 * fVar42;
    }
    else {
      fVar45 = 0.0;
    }
    *(float *)(param_1 + 0x2d8) = fVar45;
    if (cRam000000018ab63079 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63079 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8);
    lVar32 = *(longlong *)(param_1 + 0x88);
    if (lVar32 == 0) goto code_r0x000181600f6f;
    uVar28 = *(undefined8 *)(lVar32 + 0x430);
    uVar27 = *(undefined8 *)(lVar32 + 0x438);
    uVar26 = *(undefined8 *)(lVar32 + 0x440);
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_dc8 = uVar28;
    uStack_dc0 = uVar27;
    uStack_db8 = uVar26;
    uVar33 = ObscuredLong::FHOALPFLEEC(&uStack_dc8,0);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    uVar49 = (ulonglong)puVar48 & 0xffffffff00000000;
    sub_800157a0(0,uRam0000000187b8afd0,lVar29,2,CONCAT44(uVar24,3),0,uVar33,uVar49,
                  (ulonglong)puVar50 & 0xffffffff00000000,CONCAT44(uVar21,1),CONCAT44(uVar22,1),0,
                  param_1);
    lVar29 = *(longlong *)(param_1 + 0x88);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    uStack_dc8 = *(undefined8 *)(lVar29 + 0x3d8);
    uStack_dc0 = *(undefined8 *)(lVar29 + 0x3e0);
    uStack_db8 = *(undefined8 *)(lVar29 + 1000);
    ObscuredLong::FHOALPFLEEC(&uStack_dc8,0);
    lVar29 = lStack_e68;
    if (lStack_e68 != 0) {
      uVar28 = *(undefined8 *)(lStack_e68 + 0x220);
      uVar27 = *(undefined8 *)(lStack_e68 + 0x228);
      uVar26 = *(undefined8 *)(lStack_e68 + 0x230);
      if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_dc8 = uVar28;
      uStack_dc0 = uVar27;
      uStack_db8 = uVar26;
      lVar32 = ObscuredLong::FHOALPFLEEC(&uStack_dc8,0);
      if (lVar32 != 0) {
        uVar28 = *(undefined8 *)(lVar29 + 0x220);
        uVar27 = *(undefined8 *)(lVar29 + 0x228);
        uVar26 = *(undefined8 *)(lVar29 + 0x230);
        if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_dc8 = uVar28;
        uStack_dc0 = uVar27;
        uStack_db8 = uVar26;
        ObscuredLong::FHOALPFLEEC(&uStack_dc8,0);
      }
    }
    lVar29 = lStack_d50;
    if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puVar30 = (undefined8 *)ObscuredDouble::FHOALPFLEEC(auStack_1d0);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    uVar26 = puVar30[1];
    uVar28 = puVar30[2];
    uVar27 = puVar30[3];
    *(undefined8 *)(lVar29 + 0x298) = *puVar30;
    *(undefined8 *)(lVar29 + 0x2a0) = uVar26;
    *(undefined8 *)(lVar29 + 0x2a8) = uVar28;
    *(undefined8 *)(lVar29 + 0x2b0) = uVar27;
    sub_8032e8d0(lVar29 + 0x2a8,0);
    uVar19 = *(undefined4 *)(param_1 + 0x490);
    if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar17 = UnitUtility::IsPlayableUnit(uVar19,0);
    if (cVar17 != '\0') {
      uVar19 = *(undefined4 *)(lVar25 + 100);
      cVar17 = *(char *)(param_1 + 0x178);
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar29 == 0) goto code_r0x000181600f6f;
      if (cVar17 == *(char *)(lVar29 + 0x298)) {
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar29 == 0) goto code_r0x000181600f6f;
        BattleManager::CalcPlayerDamageTpReduceRate(lVar29,uVar19,0);
      }
      else {
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar29 == 0) goto code_r0x000181600f6f;
        BattleManager::CalcEnemyDamageTpReduceRate(lVar29,uVar19,0);
      }
      uVar26 = *(undefined8 *)(param_1 + 0x298);
      uVar28 = *(undefined8 *)(param_1 + 0x2a0);
      uVar27 = *(undefined8 *)(param_1 + 0x2a8);
      uVar4 = *(undefined8 *)(param_1 + 0x2b0);
      if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_d38 = uVar26;
      uStack_d30 = uVar28;
      uStack_d28 = uVar27;
      uStack_d20 = uVar4;
      ObscuredDouble::FHOALPFLEEC(&uStack_d38,0);
      puVar30 = (undefined8 *)ObscuredDouble::FHOALPFLEEC(auStack_1b0);
      uVar26 = puVar30[1];
      uVar28 = puVar30[2];
      uVar27 = puVar30[3];
      *(undefined8 *)(param_1 + 0x298) = *puVar30;
      *(undefined8 *)(param_1 + 0x2a0) = uVar26;
      *(undefined8 *)(param_1 + 0x2a8) = uVar28;
      *(undefined8 *)(param_1 + 0x2b0) = uVar27;
      sub_8032e8d0(param_1 + 0x2a8,0);
    }
    if ((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar29 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x30), lVar29 == 0))
    goto code_r0x000181600f6f;
    uVar26 = *(undefined8 *)(lVar29 + 0x90);
    uVar19 = *(undefined4 *)(lVar29 + 0x98);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puStack_e10 = (undefined8 *)CONCAT44(puStack_e10._4_4_,uVar19);
    uStack_e18 = uVar26;
    ObscuredInt::FHOALPFLEEC(&uStack_e18,0);
    if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
      sub_8032fa70();
    }
    iVar20 = UnitUtility::GetUnitPosType();
    if (iVar20 == 1) {
      uVar26 = *(undefined8 *)(param_1 + 0x298);
      uVar28 = *(undefined8 *)(param_1 + 0x2a0);
      uVar27 = *(undefined8 *)(param_1 + 0x2a8);
      uVar4 = *(undefined8 *)(param_1 + 0x2b0);
      if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_d38 = uVar26;
      uStack_d30 = uVar28;
      uStack_d28 = uVar27;
      uStack_d20 = uVar4;
      dVar44 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d38,0);
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar29 == 0) goto code_r0x000181600f6f;
      puVar30 = (undefined8 *)ObscuredDouble::FHOALPFLEEC(auStack_110,*(double *)(lVar29 + 0x70) * dVar44,0);
      uVar26 = puVar30[1];
      uVar28 = puVar30[2];
      uVar27 = puVar30[3];
      *(undefined8 *)(param_1 + 0x298) = *puVar30;
      *(undefined8 *)(param_1 + 0x2a0) = uVar26;
      *(undefined8 *)(param_1 + 0x2a8) = uVar28;
      *(undefined8 *)(param_1 + 0x2b0) = uVar27;
      sub_8032e8d0(param_1 + 0x2a8,0);
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar29 == 0) goto code_r0x000181600f6f;
      uVar26 = *(undefined8 *)(lVar29 + 0x50);
      puVar36 = auStack_f0;
code_r0x0001815ffa2e:
      puVar30 = (undefined8 *)ObscuredDouble::FHOALPFLEEC(puVar36,uVar26,0);
      uVar26 = puVar30[2];
      uVar28 = puVar30[3];
      uVar27 = puVar30[1];
      *(undefined8 *)(param_1 + 0x2b8) = *puVar30;
      *(undefined8 *)(param_1 + 0x2c0) = uVar27;
      *(undefined8 *)(param_1 + 0x2c8) = uVar26;
      *(undefined8 *)(param_1 + 0x2d0) = uVar28;
      sub_8032e8d0(param_1 + 0x2c8,0);
    }
    else {
      if (iVar20 == 2) {
        uVar26 = *(undefined8 *)(param_1 + 0x298);
        uVar28 = *(undefined8 *)(param_1 + 0x2a0);
        uVar27 = *(undefined8 *)(param_1 + 0x2a8);
        uVar4 = *(undefined8 *)(param_1 + 0x2b0);
        if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_d38 = uVar26;
        uStack_d30 = uVar28;
        uStack_d28 = uVar27;
        uStack_d20 = uVar4;
        dVar44 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d38,0);
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar29 != 0) {
          puVar30 = (undefined8 *)ObscuredDouble::FHOALPFLEEC(auStack_150,*(double *)(lVar29 + 0x78) * dVar44,0);
          uVar26 = puVar30[1];
          uVar28 = puVar30[2];
          uVar27 = puVar30[3];
          *(undefined8 *)(param_1 + 0x298) = *puVar30;
          *(undefined8 *)(param_1 + 0x2a0) = uVar26;
          *(undefined8 *)(param_1 + 0x2a8) = uVar28;
          *(undefined8 *)(param_1 + 0x2b0) = uVar27;
          sub_8032e8d0(param_1 + 0x2a8,0);
          if (cRam000000018ab63077 == '\0') {
            sub_8032f6d0(0x187bfb7a8);
            cRam000000018ab63077 = '\x01';
          }
          lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
          if (lVar29 != 0) {
            uVar26 = *(undefined8 *)(lVar29 + 0x58);
            puVar36 = auStack_130;
            goto code_r0x0001815ffa2e;
          }
        }
        goto code_r0x000181600f6f;
      }
      if (iVar20 == 3) {
        uVar26 = *(undefined8 *)(param_1 + 0x298);
        uVar28 = *(undefined8 *)(param_1 + 0x2a0);
        uVar27 = *(undefined8 *)(param_1 + 0x2a8);
        uVar4 = *(undefined8 *)(param_1 + 0x2b0);
        if (*(int *)(lRam0000000187b60c28 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_d38 = uVar26;
        uStack_d30 = uVar28;
        uStack_d28 = uVar27;
        uStack_d20 = uVar4;
        dVar44 = (double)ObscuredDouble::FHOALPFLEEC(&uStack_d38,0);
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar29 != 0) {
          puVar30 = (undefined8 *)ObscuredDouble::FHOALPFLEEC(auStack_190,*(double *)(lVar29 + 0x80) * dVar44,0);
          uVar26 = puVar30[1];
          uVar28 = puVar30[2];
          uVar27 = puVar30[3];
          *(undefined8 *)(param_1 + 0x298) = *puVar30;
          *(undefined8 *)(param_1 + 0x2a0) = uVar26;
          *(undefined8 *)(param_1 + 0x2a8) = uVar28;
          *(undefined8 *)(param_1 + 0x2b0) = uVar27;
          sub_8032e8d0(param_1 + 0x2a8,0);
          if (cRam000000018ab63077 == '\0') {
            sub_8032f6d0(0x187bfb7a8);
            cRam000000018ab63077 = '\x01';
          }
          lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
          if (lVar29 != 0) {
            uVar26 = *(undefined8 *)(lVar29 + 0x60);
            puVar36 = auStack_170;
            goto code_r0x0001815ffa2e;
          }
        }
        goto code_r0x000181600f6f;
      }
    }
    uVar26 = sub_8475aaf0(param_1,uRam0000000187c044b8);
    *(undefined8 *)(param_1 + 0x28) = uVar26;
    sub_8032e8d0(param_1 + 0x28,uVar26);
    if (*(longlong *)(param_1 + 0x28) != 0) {
      uVar47 = 0;
      UnitActionController::Initialize(*(longlong *)(param_1 + 0x28),param_1,*(undefined8 *)(lVar15 + 0x20),0,0,0);
      lVar29 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
      if ((lVar29 != 0) && (*(longlong *)(lVar29 + 0xa8) != 0)) {
        lStack_e68 = Skeleton::FindSlot(*(longlong *)(lVar29 + 0xa8),uRam0000000187b46750,0);
        lVar29 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
        if ((lVar29 != 0) &&
           ((((*(longlong *)(lVar29 + 0xa8) != 0 &&
              (lVar29 = *(longlong *)(*(longlong *)(lVar29 + 0xa8) + 0x10), lVar29 != 0)) &&
             (lVar29 = *(longlong *)(lVar29 + 0x28), lVar29 != 0)) &&
            (lVar29 = *(longlong *)(lVar29 + 0x10), lVar29 != 0)))) {
          if (*(int *)(lVar29 + 0x18) == 0) {
            sub_8032f9c0();
            goto code_r0x000181600f7b;
          }
          lVar29 = *(longlong *)(lVar29 + 0x20);
          lVar32 = sub_8032f970(uRam0000000187c25b88);
          sub_849de5b0(lVar32,uRam0000000187c4afa8);
          lVar31 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
          if ((lVar31 != 0) && (*(longlong *)(lVar31 + 0xa8) != 0)) {
            uVar19 = Skeleton::FindSlotIndex(*(longlong *)(lVar31 + 0xa8),uRam0000000187b46750,0);
            uStack_e38 = CONCAT44(uStack_e38._4_4_,uVar19);
            if ((lVar29 != 0) && (Skin::FindNamesForSlot(lVar29,uVar19,lVar32,0), lVar32 != 0)) {
              sub_849e2810(&uStack_6b8,lVar32,uRam0000000187c4c5c8);
              uStack_c80 = uStack_6b8;
              uStack_c78 = uStack_6b0;
              uStack_c70 = uStack_6a8;
              uStack_dd8 = 0;
              puStack_dd0 = &uStack_c80;
              goto code_r0x0001815ffc00;
            }
          }
        }
      }
    }
    goto code_r0x000181600f6f;
  }
  lVar29 = sub_8032f970(uRam0000000187b78350);
  SkillLevelInfo::.ctor(lVar29,0);
  if ((((*(longlong *)(lVar15 + 0x20) == 0) ||
       (lVar31 = *(longlong *)(*(longlong *)(lVar15 + 0x20) + 0x38), lVar31 == 0)) ||
      (lVar31 = *(longlong *)(lVar31 + 0x60), lVar31 == 0)) ||
     (uVar19 = sub_84a610f0(lVar31,plVar34,uRam0000000187b446b8), lVar29 == 0))
  goto code_r0x000181600f6f;
  SkillLevelInfo::SetSkillId(lVar29,uVar19,0);
  lVar31 = *(longlong *)(lVar25 + 0x68);
  if (lVar31 == 0) goto code_r0x000181600f6f;
  if (0 < *(int *)(lVar31 + 0x18)) {
    lVar31 = sub_849ed2d0(lVar31,0,uRam0000000187c33ac8);
    if (lVar31 == 0) goto code_r0x000181600f6f;
    lVar2 = *(longlong *)(lVar31 + 0x1c);
    uStack_e38 = CONCAT44(uStack_e38._4_4_,*(undefined4 *)(lVar31 + 0x24));
    uVar19 = *(undefined4 *)(lVar31 + 0x24);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
      uVar19 = (undefined4)uStack_e38;
    }
    lStack_e28 = lVar2;
    uStack_e20 = uVar19;
    uVar19 = ObscuredInt::FHOALPFLEEC(&lStack_e28,0);
    SkillLevelInfo::SetSkillLevel(lVar29,uVar19,0);
  }
  if (lStack_e48 == 0) goto code_r0x000181600f6f;
  sub_80002460(lStack_e48,lVar29,uRam0000000187c32d28);
  plVar34 = (longlong *)(ulonglong)((int)plVar34 + 1);
  goto code_r0x0001815fdc20;
code_r0x000181600f7b:
  sub_8032f9d0();
code_r0x000181600f80:
  sub_8032f9d0();
code_r0x000181600f85:
  sub_8032f9d0();
code_r0x000181600f8a:
  sub_8032f9d0();
  sub_8032b6b0();
code_r0x000181600f96:
  sub_8032f9d0();
code_r0x000181600f9b:
  sub_8032f9d0();
code_r0x000181600fa0:
  sub_8032f9d0();
code_r0x000181600fa5:
  sub_8032f9d0();
  sub_8032b6b0();
code_r0x000181600fb1:
  sub_8032f9d0();
code_r0x000181600fb6:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar8 = (code *)swi(3);
  (*pcVar8)();
  return;
code_r0x000181600700:
  cVar17 = sub_8572bc70(&uStack_cc8,uRam0000000187b5c2e8);
  lVar29 = lStack_cb8;
  if (cVar17 != '\0') {
    if (lStack_cb8 == 0) goto code_r0x000181600fa5;
    if (*(int *)(lStack_cb8 + 0x30) == 4) {
      lStack_e48 = UnitCtrl::initializeCharaAuraEffect(param_1,*(undefined8 *)(lStack_cb8 + 0x10),
                                 *(undefined8 *)(lStack_cb8 + 0x28),
                                 *(undefined1 *)(lStack_cb8 + 0x34),0);
      if (lVar29 == 0) goto code_r0x000181600fa0;
      uVar26 = UnitCtrl::initializeCharaAuraEffect(param_1,*(undefined8 *)(lVar29 + 0x18),*(undefined8 *)(lVar29 + 0x28),
                             *(undefined1 *)(lVar29 + 0x34),0);
      if (*(char *)(param_1 + 0x165) == '\0') {
        if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
          sub_8032fa70();
        }
        ExtensionMethods::SetActive(uVar26,0,0);
      }
      else {
        if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
          sub_8032fa70();
        }
        ExtensionMethods::SetActive(lStack_e48,0,0);
      }
      if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x000181600f9b;
      lVar29 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x1b8);
      uStack_748 = 0;
      uStack_740 = 0;
      sub_81813980(&uStack_748,lStack_e48,uVar26,uRam0000000187b9e658);
      if (lVar29 == 0) goto code_r0x000181600f96;
      uStack_d78 = uStack_748;
      uStack_d70 = uStack_740;
      sub_80005d30(lVar29,&uStack_d78,uRam0000000187b4ddb8);
    }
    goto code_r0x000181600700;
  }
  sub_80443e50(&uStack_cc8,uRam0000000187b5c128);
  uVar26 = sub_8032f970(uRam0000000187bf3230);
  JCFNLBAPNAF::.ctor(uVar26,0);
  *(undefined8 *)(param_1 + 0x30) = uVar26;
  sub_8032e8d0(param_1 + 0x30,uVar26);
  lVar29 = *(longlong *)(param_1 + 0x30);
  uVar26 = *(undefined8 *)(param_1 + 0x28);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  uVar28 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (cRam000000018ab6307b == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307b = '\x01';
  }
  if (lVar29 != 0) {
    JCFNLBAPNAF::EJAEPHJPOGC(lVar29,param_1,uVar26,uVar28,
                  *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x20),0);
    lVar29 = sub_80003d20(uRam0000000187b3c708);
    if (lVar29 != 0) {
      lStack_e28 = *(longlong *)(lVar29 + 0x240);
      lVar29 = sub_8032f970(uRam0000000187bf1860);
      sub_849de5b0(lVar29,uRam0000000187b413a0);
      if (lVar25 != 0) {
        lVar25 = *(longlong *)(lVar25 + 0x108);
        lStack_e48 = lVar25;
        if (lVar25 == 0) {
          lVar25 = sub_8032f970(uRam0000000187be1690);
          lStack_e48 = lVar25;
          sub_849de5b0(lVar25,uRam0000000187c15698);
        }
        lStack_e68 = sub_8048c110(uRam0000000187c16f00);
        if (lVar25 != 0) {
          for (; (int)plVar39 < *(int *)(lVar25 + 0x18);
              plVar39 = (longlong *)(ulonglong)((int)plVar39 + 1)) {
            lVar25 = sub_849ed2d0(lVar25,plVar39,uRam0000000187c16280);
            if (lVar25 == 0) goto code_r0x000181600fc2;
            uVar26 = *(undefined8 *)(lVar25 + 0x10);
            uVar19 = *(undefined4 *)(lVar25 + 0x18);
            if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
              sub_8032fa70();
            }
            puStack_e10 = (undefined8 *)CONCAT44(puStack_e10._4_4_,uVar19);
            uStack_e18 = uVar26;
            iVar20 = ObscuredInt::FHOALPFLEEC(&uStack_e18,0);
            if (iVar20 == 0) {
              uVar26 = *(undefined8 *)(lVar25 + 0x1c);
              uVar19 = *(undefined4 *)(lVar25 + 0x24);
              if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                sub_8032fa70();
              }
              puStack_dd0 = (undefined8 *)CONCAT44(puStack_dd0._4_4_,uVar19);
              uStack_dd8 = uVar26;
              iVar20 = ObscuredInt::FHOALPFLEEC(&uStack_dd8,0);
              if (iVar20 != 0) goto code_r0x000181600a91;
            }
            else {
code_r0x000181600a91:
              uVar26 = *(undefined8 *)(lVar25 + 0x1c);
              uVar19 = *(undefined4 *)(lVar25 + 0x24);
              if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uStack_da8 = uVar26;
              uStack_da0 = uVar19;
              uVar19 = ObscuredInt::FHOALPFLEEC(&uStack_da8,0);
              uStack_d88 = *(undefined8 *)(lVar25 + 0x34);
              uStack_d80 = *(undefined4 *)(lVar25 + 0x3c);
              uVar24 = ObscuredInt::FHOALPFLEEC(&uStack_d88,0);
              uStack_e40 = CONCAT44(uStack_e40._4_4_,uVar24);
              uStack_d98 = *(undefined8 *)(lVar25 + 0x10);
              uStack_d90 = *(undefined4 *)(lVar25 + 0x18);
              iVar20 = ObscuredInt::FHOALPFLEEC(&uStack_d98,0);
              if (iVar20 != 0) {
                uVar26 = *(undefined8 *)(lVar25 + 0x10);
                uVar24 = *(undefined4 *)(lVar25 + 0x18);
                if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                  sub_8032fa70();
                }
                uStack_d18 = uVar26;
                uStack_d10 = uVar24;
                uVar24 = ObscuredInt::FHOALPFLEEC(&uStack_d18,0);
                if (lStack_e68 == 0) goto code_r0x000181600fc2;
                lVar25 = UserData::SearchExtraEquip(lStack_e68,uVar24,0);
                if (lVar25 != 0) {
                  uVar19 = ExtraEquipPair::get_ExEquipmentId(lVar25,0);
                  uVar24 = ExtraEquipPair::get_Rank(lVar25,0);
                  uStack_e40 = CONCAT44(uStack_e40._4_4_,uVar24);
                }
              }
              if ((*(longlong *)(lVar15 + 0x28) == 0) ||
                 (lVar25 = *(longlong *)(*(longlong *)(lVar15 + 0x28) + 0x670), lVar25 == 0))
              goto code_r0x000181600fc2;
              cVar17 = EDMHHBPPJJI::BBDLEHBFPOO(lVar25,*(undefined4 *)(param_1 + 0x490),uVar19,0);
              if (cVar17 != '\0') {
                if ((lStack_e28 == 0) || (lVar25 = BPECCALDBCO::BKEJKNOJCEE(lStack_e28,uVar19,0), lVar25 == 0))
                goto code_r0x000181600fc2;
                uVar26 = *(undefined8 *)(lVar25 + 0x21c);
                uStack_e38 = CONCAT44(uStack_e38._4_4_,*(undefined4 *)(lVar25 + 0x224));
                uVar24 = *(undefined4 *)(lVar25 + 0x224);
                if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                  sub_8032fa70();
                  uVar24 = (undefined4)uStack_e38;
                }
                uStack_d08 = uVar26;
                uStack_d00 = uVar24;
                iVar20 = ObscuredInt::FHOALPFLEEC(&uStack_d08,0);
                if (iVar20 != 0) {
                  uVar26 = *(undefined8 *)(lVar25 + 0x21c);
                  uStack_e38 = CONCAT44(uStack_e38._4_4_,*(undefined4 *)(lVar25 + 0x224));
                  uVar24 = *(undefined4 *)(lVar25 + 0x224);
                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                    sub_8032fa70();
                    uVar24 = (undefined4)uStack_e38;
                  }
                  uStack_cf8 = uVar26;
                  uStack_cf0 = uVar24;
                  uVar24 = ObscuredInt::FHOALPFLEEC(&uStack_cf8,0);
                  if (lVar29 == 0) goto code_r0x000181600fc2;
                  sub_80002bb0(lVar29,uVar24,uRam0000000187b418c0);
                }
                uVar26 = *(undefined8 *)(lVar25 + 0x228);
                uStack_e38 = CONCAT44(uStack_e38._4_4_,*(undefined4 *)(lVar25 + 0x230));
                uVar24 = *(undefined4 *)(lVar25 + 0x230);
                if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                  sub_8032fa70();
                  uVar24 = (undefined4)uStack_e38;
                }
                uStack_ce8 = uVar26;
                uStack_ce0 = uVar24;
                iVar20 = ObscuredInt::FHOALPFLEEC(&uStack_ce8,0);
                if (iVar20 != 0) {
                  uVar26 = *(undefined8 *)(lVar25 + 0x228);
                  uVar24 = *(undefined4 *)(lVar25 + 0x230);
                  if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
                    sub_8032fa70();
                  }
                  uStack_cd8 = uVar26;
                  uStack_cd0 = uVar24;
                  uVar24 = ObscuredInt::FHOALPFLEEC(&uStack_cd8,0);
                  if (lVar29 == 0) goto code_r0x000181600fc2;
                  sub_80002bb0(lVar29,uVar24,uRam0000000187b418c0);
                }
                iVar20 = ExEquip5ExceedLimitUtility::GetExceedLimitSkillId(uVar19,uStack_e40 & 0xffffffff,0);
                if (iVar20 != 0) {
                  if (lVar29 == 0) goto code_r0x000181600fc2;
                  sub_80002bb0(lVar29,iVar20,uRam0000000187b418c0);
                }
              }
            }
            lVar25 = lStack_e48;
          }
          if (lVar29 != 0) {
            sub_84a55540(&uStack_688,lVar29,uRam0000000187b42d30);
            uStack_c98 = uStack_688;
            uStack_c90 = uStack_680;
            uStack_c88 = uStack_678;
            uStack_e18 = 0;
            puStack_e10 = &uStack_c98;
            while( true ) {
              cVar17 = sub_85723320(&uStack_c98,uRam0000000187b3b438);
              uVar33 = uStack_c88;
              if (cVar17 == '\0') {
                sub_80443e50(&uStack_c98,uRam0000000187b3b288);
                return;
              }
              lVar25 = sub_8032f970(uRam0000000187c25dd0);
              AJDJDPHBJPE::.ctor(lVar25,0);
              uVar26 = *(undefined8 *)(param_1 + 0x28);
              if (cRam000000018ab63077 == '\0') {
                sub_8032f6d0(0x187bfb7a8);
                cRam000000018ab63077 = '\x01';
              }
              if (lVar25 == 0) break;
              AJDJDPHBJPE::EJAEPHJPOGC(lVar25,uVar33 & 0xffffffff,param_1,uVar26,
                            *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28),0);
              if (*(longlong *)(param_1 + 0x98) == 0) goto code_r0x000181600fb1;
              PBLMDEFAGBK::JOAPCJGKFMJ(*(longlong *)(param_1 + 0x98),lVar25,0);
            }
            goto code_r0x000181600fb6;
          }
        }
      }
    }
  }
code_r0x000181600fc2:
  sub_8032f9d0();
  pcVar8 = (code *)swi(3);
  (*pcVar8)();
  return;
code_r0x0001815ffc00:
  lStack_e28 = lVar25;
  cVar17 = sub_8572bc70(&uStack_c80,uRam0000000187b708e8);
  if (cVar17 != '\0') {
    plVar34 = (longlong *)Skin::GetAttachment(lVar29,uStack_e38 & 0xffffffff);
    if (plVar34 != (longlong *)0x0) {
      if ((*(byte *)(*plVar34 + 0x130) < *(byte *)(lRam0000000187b541f8 + 0x130)) ||
         (*(longlong *)
           (*(longlong *)(*plVar34 + 200) + -8 +
           (ulonglong)*(byte *)(lRam0000000187b541f8 + 0x130) * 8) != lRam0000000187b541f8)) {
        bVar41 = false;
      }
      else {
        bVar41 = true;
      }
      plVar38 = plVar39;
      if (bVar41) {
        plVar38 = plVar34;
      }
      if (plVar38 != (longlong *)0x0) {
        uVar26 = Component::get_gameObject(param_1,0);
        uVar33 = 0;
        uVar47 = uVar47 & 0xffffffff00000000;
        lVar32 = SkeletonUtility::AddBoundingBoxAsComponent(plVar38,lStack_e68,uVar26,0,1,uVar47,0);
        if (lVar32 == 0) goto code_r0x000181600f8a;
        puVar30 = (undefined8 *)Collider2D::get_bounds(auStack_238,lVar32);
        uStack_d78 = *puVar30;
        uStack_d70 = puVar30[1];
        lStack_d68 = puVar30[2];
        lVar31 = FixedTransformMonoBehavior::get_transform(param_1,0);
        if (lVar31 == 0) goto code_r0x000181600f85;
        puVar30 = (undefined8 *)FixedTransform::get_position(auStack_650,lVar31);
        uStack_e18 = *puVar30;
        fVar42 = *(float *)(puVar30 + 1);
        *(ulonglong *)(param_1 + 0x218) =
             CONCAT44(uStack_d78._4_4_ - (float)((ulonglong)uStack_e18 >> 0x20),
                      (float)uStack_d78 - (float)uStack_e18);
        *(float *)(param_1 + 0x220) = (float)uStack_d70 - fVar42;
        lVar31 = Collider2D::get_bounds(auStack_220,lVar32);
        lStack_d68 = *(longlong *)(lVar31 + 0x10);
        fVar42 = (float)((ulonglong)lStack_d68 >> 0x20);
        fVar45 = *(float *)(lVar31 + 0xc) + *(float *)(lVar31 + 0xc);
        *(ulonglong *)(param_1 + 0x224) = CONCAT44((float)lStack_d68 + (float)lStack_d68,fVar45);
        *(float *)(param_1 + 0x22c) = fVar42 + fVar42;
        lVar31 = FixedTransformMonoBehavior::get_transform(param_1,0);
        if (lVar31 == 0) goto code_r0x000181600f80;
        pfVar35 = (float *)FixedTransform::get_lossyScale(auStack_660,lVar31);
        *(float *)(param_1 + 0x118) = fVar45 / *pfVar35 + *(float *)(param_1 + 0x358);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        Object::Destroy(lVar32,0);
        lVar32 = Component::get_gameObject(param_1,0);
        if (lVar32 == 0) goto code_r0x000181600f7b;
        uVar26 = sub_847c74a0(lVar32,uRam0000000187bc3418);
        Object::Destroy(uVar26,0);
      }
    }
    goto code_r0x0001815ffc00;
  }
  sub_80443e50(&uStack_c80,uRam0000000187b70728);
  if (*(char *)(param_1 + 0x178) != '\0') {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    iVar20 = BattleManager::GetPurpose(lVar29,0);
    if (iVar20 == 3) {
      lVar29 = *(longlong *)(param_1 + 0xd0);
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar32 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if ((lVar32 == 0) ||
         (uVar19 = BattleManager::GetEnemyPoint(lVar32,*(undefined4 *)(param_1 + 0x488),0), lVar29 == 0))
      goto code_r0x000181600f6f;
      *(undefined4 *)(lVar29 + 300) = uVar19;
    }
  }
  if (*(char *)(param_1 + 0x1a8) != '\0') {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    cVar17 = BattleManager::get_IsSpecialBattle(lVar29,0);
    if (cVar17 == '\0') goto code_r0x000181600144;
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    cVar17 = BattleManager::get_IsDungeonSpecialBattle(lVar29,0);
    if (cVar17 == '\0') {
      if (*(int *)(lRam0000000187b74fd8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      cVar17 = HatsuneUtility::IsNowEventMultiBoss(0);
      if (cVar17 == '\0') {
        cVar17 = '\x01';
      }
      else {
        uVar19 = *(undefined4 *)(param_1 + 0x488);
        if (*(int *)(lRam0000000187b74fd8 + 0xe4) == 0) {
          sub_8032fa70();
        }
        cVar17 = HatsuneUtility::IsMultiBossDestructionTarget(uVar19,0);
      }
      cVar18 = '\0';
code_r0x00018160006a:
      lVar29 = lStack_df0;
      if (cVar18 == '\0' && cVar17 == '\0') goto code_r0x000181600144;
    }
    else {
      cVar17 = '\0';
      cVar18 = DungeonUtility::IsNowSpecialMultiBoss(0);
      lVar29 = lVar25;
      if (cVar18 != '\0') {
        cVar18 = DungeonUtility::IsMustKillSpecialMultiBoss(*(undefined4 *)(param_1 + 0x488),0);
        lStack_df0 = lVar25;
        goto code_r0x00018160006a;
      }
    }
    lStack_df0 = lVar29;
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar29 == 0) goto code_r0x000181600f6f;
    iVar20 = BattleManager::GetTriggerHpPercent(lVar29,0);
    if (iVar20 != 0) {
      lVar29 = *(longlong *)(param_1 + 0xd0);
      lVar32 = *(longlong *)(param_1 + 0x88);
      if (lVar32 == 0) goto code_r0x000181600f6f;
      uVar28 = *(undefined8 *)(lVar32 + 0x448);
      uVar27 = *(undefined8 *)(lVar32 + 0x450);
      uVar26 = *(undefined8 *)(lVar32 + 0x458);
      if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_dc8 = uVar28;
      uStack_dc0 = uVar27;
      uStack_db8 = uVar26;
      lVar32 = ObscuredLong::FHOALPFLEEC(&uStack_dc8,0);
      uVar26 = BattleUtil::DoubleToLong((double)(iVar20 * lVar32) / 100.0,0);
      if (lVar29 == 0) goto code_r0x000181600f6f;
      *(undefined8 *)(lVar29 + 0x130) = uVar26;
    }
  }
code_r0x000181600144:
  lVar29 = *(longlong *)(param_1 + 0x378);
  plVar34 = plVar39;
  if (lVar29 != 0) {
    while ((int)plVar34 < *(int *)(lVar29 + 0x18)) {
      lVar29 = sub_8032f970(uRam0000000187c22a38);
      sub_804383d0(lVar29,0);
      if ((*(longlong *)(param_1 + 0x378) == 0) ||
         (uVar26 = sub_849ed2d0(*(longlong *)(param_1 + 0x378),plVar34,uRam0000000187b7a2a8),
         lVar29 == 0)) goto code_r0x000181600f6f;
      *(undefined8 *)(lVar29 + 0x10) = uVar26;
      sub_8032e8d0(lVar29 + 0x10,uVar26);
      lVar32 = *(longlong *)(lVar29 + 0x10);
      if ((*(longlong *)(param_1 + 0x128) == 0) ||
         (lVar31 = *(longlong *)(*(longlong *)(param_1 + 0x128) + 0xa8), lVar31 == 0))
      goto code_r0x000181600f6f;
      lStack_e68 = *(longlong *)(lVar31 + 0x20);
      lStack_e28 = sub_8032f970(uRam0000000187b93508);
      sub_84cacb40(lStack_e28,lVar29,uRam0000000187bd4e70);
      if ((lStack_e68 == 0) ||
         (uVar19 = sub_859253f0(lStack_e68,lStack_e28,uRam0000000187b8db08), lVar32 == 0))
      goto code_r0x000181600f6f;
      *(undefined4 *)(lVar32 + 0x18) = uVar19;
      lVar32 = *(longlong *)(lVar29 + 0x10);
      if ((*(longlong *)(param_1 + 0x128) == 0) ||
         ((lVar31 = *(longlong *)(*(longlong *)(param_1 + 0x128) + 0xa8), lVar32 == 0 ||
          (lVar31 == 0)))) goto code_r0x000181600f6f;
      uVar26 = Skeleton::GetAttachment(lVar31,*(undefined4 *)(lVar32 + 0x18),*(undefined8 *)(lVar32 + 0x10));
      *(undefined8 *)(lVar32 + 0x28) = uVar26;
      sub_8032e8d0(lVar32 + 0x28,uVar26);
      if ((*(longlong *)(param_1 + 0x128) == 0) ||
         (lVar32 = *(longlong *)(*(longlong *)(param_1 + 0x128) + 0xa8), lVar32 == 0))
      goto code_r0x000181600f6f;
      lVar32 = *(longlong *)(lVar32 + 0x20);
      lStack_e68 = sub_8032f970(uRam0000000187b93508);
      sub_84cacb40(lStack_e68,lVar29,uRam0000000187bd5028);
      if (lVar32 == 0) goto code_r0x000181600f6f;
      uVar19 = sub_859253f0(lVar32,lStack_e68,uRam0000000187b8db08);
      if (*(longlong *)(param_1 + 0x128) == 0) goto code_r0x000181600f6f;
      lVar32 = *(longlong *)(*(longlong *)(param_1 + 0x128) + 0xa8);
      if ((*(longlong *)(lVar29 + 0x10) == 0) || (lVar32 == 0)) goto code_r0x000181600f6f;
      uVar26 = Skeleton::GetAttachment(lVar32,uVar19,*(undefined8 *)(*(longlong *)(lVar29 + 0x10) + 0x20));
      lVar29 = *(longlong *)(lVar29 + 0x10);
      if (lVar29 == 0) goto code_r0x000181600f6f;
      *(undefined8 *)(lVar29 + 0x30) = uVar26;
      sub_8032e8d0(lVar29 + 0x30);
      lVar29 = *(longlong *)(param_1 + 0x378);
      plVar34 = (longlong *)(ulonglong)((int)plVar34 + 1);
      if (lVar29 == 0) goto code_r0x000181600f6f;
    }
    if ((*(char *)(param_1 + 0x188) == '\0') || (*(int *)(param_1 + 0x49c) != 6)) {
code_r0x00018160069e:
      if (*(longlong *)(param_1 + 0x330) == 0) goto code_r0x000181600fc2;
      sub_849e2810(&uStack_6a0,*(longlong *)(param_1 + 0x330),uRam0000000187bebbc0);
      uStack_cc8 = uStack_6a0;
      uStack_cc0 = uStack_698;
      lStack_cb8 = lStack_690;
      uStack_e18 = 0;
      puStack_e10 = &uStack_cc8;
      goto code_r0x000181600700;
    }
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar29 != 0) {
      cVar17 = BattleManager::GetEnableShadowEffect(lVar29,0);
      if (cVar17 == '\0') goto code_r0x00018160069e;
      lVar29 = sub_80003d20(uRam0000000187b3d4a8);
      uVar26 = ExceptNGUIRoot::get_Transform(0);
      if (((lVar29 != 0) && (lVar29 = ResourceManager::LoadImmediately(lVar29,0x6386e488,uVar26,0,0), lVar29 != 0)) &&
         (plVar34 = (longlong *)sub_847c74a0(lVar29,uRam0000000187bc4f58),
         plVar34 != (longlong *)0x0)) {
        (**(code **)(*plVar34 + 0x188))(plVar34,*(undefined8 *)(*plVar34 + 400));
        if (*(longlong *)(param_1 + 200) != 0) {
          SkillEffectCtrl::PlaySe(plVar34,*(undefined4 *)(*(longlong *)(param_1 + 200) + 0x38),
                        *(undefined1 *)(param_1 + 0x165),0);
          lVar29 = Component::get_transform(plVar34,0);
          lVar32 = FixedTransformMonoBehavior::get_transform(param_1,0);
          if (lVar32 != 0) {
            puVar30 = (undefined8 *)FixedTransform::get_position(auStack_670,lVar32,0);
            if (lVar29 == 0) goto code_r0x000181600fc2;
            uStack_e18 = *puVar30;
            puStack_e10 = (undefined8 *)CONCAT44(puStack_e10._4_4_,*(undefined4 *)(puVar30 + 1));
            Transform::set_position(lVar29,&uStack_e18,0);
            lVar29 = Component::get_transform(plVar34,0);
            uVar26 = ExceptNGUIRoot::get_Transform(0);
            if (lVar29 == 0) goto code_r0x000181600fc2;
            Transform::set_parent(lVar29,uVar26,0);
            plVar34[0x12] = param_1;
            sub_8032e8d0(plVar34 + 0x12,param_1);
            (**(code **)(*plVar34 + 0x198))(plVar34,param_1,1,*(undefined8 *)(*plVar34 + 0x1a0));
            if (cRam000000018ab63077 == '\0') {
              sub_8032f6d0(0x187bfb7a8);
              cRam000000018ab63077 = '\x01';
            }
            lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
            if (*(longlong *)(param_1 + 0x80) == 0) goto code_r0x000181600fc2;
            uVar26 = AIGOPEMKDKL::MCMPDLOCDCI(*(longlong *)(param_1 + 0x80),1,0,0);
            if (cRam000000018ab5adcc == '\0') {
              sub_8032f6d0(0x187c16e48);
              cRam000000018ab5adcc = '\x01';
            }
            uStack_e18 = **(undefined8 **)(lRam0000000187c16e48 + 0xb8);
            puStack_e10 = (undefined8 *)
                          CONCAT44(puStack_e10._4_4_,
                                   *(undefined4 *)
                                    (*(undefined8 **)(lRam0000000187c16e48 + 0xb8) + 1));
            uVar26 = SkillEffectCtrl::TrackTarget(plVar34,uVar26,&uStack_e18,1,1,*(undefined8 *)(param_1 + 0xf0),
                                   uVar33 & 0xffffffffffffff00,uVar49 & 0xffffffff00000000,0);
            if (lVar29 == 0) goto code_r0x000181600fc2;
            sub_86209420(lVar29,uVar26,0);
            if (cRam000000018ab63077 == '\0') {
              sub_8032f6d0(0x187bfb7a8);
              cRam000000018ab63077 = '\x01';
            }
            lVar29 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
            uVar26 = SkillEffectCtrl::TrackTargetSort(plVar34,param_1,1,0);
            if (lVar29 == 0) goto code_r0x000181600fc2;
            sub_86209420(lVar29,uVar26,0);
            if (*(longlong *)(param_1 + 0x100) == 0) goto code_r0x000181600fc2;
            sub_80002460(*(longlong *)(param_1 + 0x100),plVar34,uRam0000000187c318b8);
            if (*(longlong *)(param_1 + 0x108) == 0) goto code_r0x000181600fc2;
            sub_80002460(*(longlong *)(param_1 + 0x108),plVar34,uRam0000000187c318b8);
            goto code_r0x00018160069e;
          }
        }
      }
    }
  }
code_r0x000181600f6f:
  sub_8032f9d0();
  pcVar8 = (code *)swi(3);
  (*pcVar8)();
  return;
}



/* ==== UnitCtrl::IsAuraRemainBattle  @ 0x1600fd0  size=1 ==== */

ulonglong UnitCtrl::IsAuraRemainBattle(void)

{
  uint uVar1;
  int iVar2;
  longlong lVar3;
  code *pcVar4;
  uint3 uVar5;
  ulonglong uVar6;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar3 != 0) {
    iVar2 = *(int *)(lVar3 + 0x2c0);
    uVar1 = iVar2 - 0x19;
    uVar5 = (uint3)(uVar1 >> 8);
    if (((0x37 < uVar1) || ((0x80000000000411U >> ((ulonglong)uVar1 & 0x3f) & 1) == 0)) &&
       (iVar2 != 0x51)) {
      return (ulonglong)CONCAT31(uVar5,iVar2 == 0x1a);
    }
    return CONCAT71((uint7)uVar5,1);
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  uVar6 = (*pcVar4)();
  return uVar6;
}



/* ==== UnitCtrl::IsInitialMode  @ 0x1601020  size=1 ==== */

void UnitCtrl::IsInitialMode(void)

{
  longlong lVar1;
  longlong *plVar2;
  code *pcVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 != 0) {
    plVar2 = *(longlong **)(lVar1 + 0x520);
    if (plVar2 != (longlong *)0x0) {
                    /* WARNING: Could not recover jumptable at 0x000183713a55. Too many branches */
                    /* WARNING: Treating indirect jump as call */
      (**(code **)(*plVar2 + 0x778))(plVar2,*(undefined8 *)(*plVar2 + 0x780));
      return;
    }
    sub_8032f9d0(0,0);
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitCtrl::JudgeCanPlayVoice  @ 0x1601070  size=1 ==== */

undefined8 UnitCtrl::JudgeCanPlayVoice(longlong param_1,undefined4 param_2,int param_3)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  undefined8 uVar4;
  char cStackX_8;
  char cStackX_9;
  
  if (cRam000000018ab63067 == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187bf5cb0);
    sub_8032f6d0(0x187b9aea8);
    cRam000000018ab63067 = '\x01';
  }
  if ((*(longlong *)(param_1 + 0x38) != 0) &&
     (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar1 != 0)) {
    if (0 < *(int *)(lVar1 + 0x18)) {
      return 0;
    }
    if (*(char *)(param_1 + 0x188) != '\0') {
      if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
        sub_8032fa70(lRam0000000187b41650);
      }
      lVar1 = *(longlong *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 0xb0);
      if (lVar1 == 0) goto code_r0x000181601289;
      sub_855ffd90(&cStackX_8,lVar1,param_2,uRam0000000187bf5cb0);
      if (cStackX_9 == '\0') {
        return 0;
      }
    }
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar1 != 0) {
      cVar3 = BattleManager::CanPlayVoice(lVar1,0);
      if (cVar3 == '\0') {
        return 0;
      }
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar1 != 0) {
        if (((*(char *)(lVar1 + 0x298) != '\0') || (*(char *)(param_1 + 0x178) == '\0')) ||
           (param_3 == 0)) {
          cVar3 = *(char *)(param_1 + 0x178);
          if (cRam000000018ab63077 == '\0') {
            sub_8032f6d0(0x187bfb7a8);
            cRam000000018ab63077 = '\x01';
          }
          lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
          if (lVar1 == 0) goto code_r0x000181601289;
          if (cVar3 != *(char *)(lVar1 + 0x298)) {
            if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
              sub_8032fa70(lRam0000000187b41650);
            }
            lVar1 = *(longlong *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 0xb0);
            if (lVar1 == 0) goto code_r0x000181601289;
            sub_855ffd90(&cStackX_8,lVar1,param_2,uRam0000000187bf5cb0);
            if (cStackX_8 == '\0') {
              return 0;
            }
          }
        }
        return 1;
      }
    }
  }
code_r0x000181601289:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar4 = (*pcVar2)();
  return uVar4;
}



/* ==== UnitCtrl::JudgeIsRarity6  @ 0x1601290  size=11 ==== */

bool UnitCtrl::JudgeIsRarity6(longlong param_1)

{
  return *(int *)(param_1 + 0x49c) == 6;
}



/* ==== UnitCtrl::MoveToNext  @ 0x16012a0  size=1 ==== */

void UnitCtrl::MoveToNext(longlong param_1)

{
  undefined4 uVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  undefined4 uStack_18;
  undefined4 uStack_14;
  undefined4 uStack_10;
  undefined4 uStack_c;
  
  if (cRam000000018ab6305a == '\0') {
    sub_8032f6d0(0x187c05928);
    cRam000000018ab6305a = '\x01';
  }
  if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x0001816013d7;
  if (*(char *)(*(longlong *)(param_1 + 0xd0) + 0xb9) != '\0') {
    return;
  }
  uVar1 = *(undefined4 *)(param_1 + 0x488);
  if (*(int *)(lRam0000000187c05928 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = UnitUtility::JudgeIsSummon(uVar1,0);
  if (cVar3 == '\0') {
    if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x0001816013d7;
    *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0xb8) = 0;
    lVar4 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
    if (lVar4 == 0) goto code_r0x0001816013d7;
    uStack_18 = 0x3f800000;
    uStack_14 = 0x3f800000;
    uStack_10 = 0x3f800000;
    uStack_c = 0x3f800000;
    SpineController::set_CurColor(lVar4,&uStack_18,0);
  }
  if (*(longlong *)(param_1 + 0xb8) != 0) {
    NLHACDMHDNP::AFKFLHEAGDA(*(longlong *)(param_1 + 0xb8),0);
    if (*(longlong *)(param_1 + 0xd0) != 0) {
      *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 199) = 0;
      if (*(longlong *)(param_1 + 0xd0) != 0) {
        DEBLMMJMGID::IDENJLFHMEJ(*(longlong *)(param_1 + 0xd0),4,0,0,0,0);
        UnitCtrl::SetLeftDirection(param_1,0,0);
        if (*(longlong *)(param_1 + 0xd0) != 0) {
          DEBLMMJMGID::GOJOLLDNLFD(*(longlong *)(param_1 + 0xd0),0);
          *(undefined1 *)(param_1 + 0x2de) = 0;
          return;
        }
      }
    }
  }
code_r0x0001816013d7:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::PlayUbChainVoice  @ 0x16013e0  size=1 ==== */

void UnitCtrl::PlayUbChainVoice(longlong param_1)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  undefined8 uVar4;
  undefined4 uStack_28;
  undefined4 uStack_24;
  undefined4 uStack_20;
  undefined4 uStack_1c;
  undefined8 uStack_18;
  
  if (cRam000000018ab63068 == '\0') {
    sub_8032f6d0(0x187b94218);
    sub_8032f6d0(0x187b72388);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b97c88);
    sub_8032f6d0(0x187b30078);
    cRam000000018ab63068 = '\x01';
  }
  if (cRam000000018ab6307b == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307b = '\x01';
  }
  if (*(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x20) != 0) {
    cVar2 = sub_80002270(0,uRam0000000187b94218);
    if (cVar2 != '\0') {
      return;
    }
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar3 != 0) {
      cVar2 = BattleManager::CanPlayVoice(lVar3,0);
      if (cVar2 == '\0') {
        return;
      }
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar3 != 0) {
        uVar4 = *(undefined8 *)(lVar3 + 0x438);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        cVar2 = Object::op_Equality(uVar4,param_1,0);
        if (cVar2 == '\0') {
          return;
        }
        if (*(char *)(param_1 + 0x178) == '\0') {
          lVar3 = sub_815f21c0(param_1);
          if (lVar3 == 0) goto code_r0x00018160166d;
          lVar3 = *(longlong *)(lVar3 + 0x240);
        }
        else {
          lVar3 = sub_815f21c0(param_1);
          if (lVar3 == 0) goto code_r0x00018160166d;
          lVar3 = *(longlong *)(lVar3 + 0x248);
        }
        uVar4 = sub_8032f970(uRam0000000187b97c88);
        sub_84cacb40(uVar4,param_1,uRam0000000187b30078,0);
        if (lVar3 != 0) {
          uVar4 = sub_84a990e0(lVar3,uVar4,uRam0000000187b72388);
          if ((*(longlong *)(param_1 + 200) != 0) &&
             (lVar3 = *(longlong *)(param_1 + 0xd0), lVar3 != 0)) {
            uStack_28 = *(undefined4 *)(lVar3 + 0x170);
            uStack_24 = *(undefined4 *)(lVar3 + 0x174);
            uStack_20 = *(undefined4 *)(lVar3 + 0x178);
            uStack_1c = *(undefined4 *)(lVar3 + 0x17c);
            uStack_18 = *(undefined8 *)(lVar3 + 0x180);
            BCKKJALAJGO::MLECBNAPHFC(*(undefined8 *)(param_1 + 200),
                          *(undefined4 *)(*(longlong *)(param_1 + 200) + 0x50),
                          *(undefined4 *)(param_1 + 0x488),&uStack_28,uVar4,0);
            if (cRam000000018ab63077 == '\0') {
              sub_8032f6d0(0x187bfb7a8);
              cRam000000018ab63077 = '\x01';
            }
            lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
            if (lVar3 != 0) {
              *(undefined8 *)(lVar3 + 0x438) = 0;
              sub_8032e8d0(lVar3 + 0x438,0);
              return;
            }
          }
        }
      }
    }
  }
code_r0x00018160166d:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitCtrl::PreparePrincessFormProcessor  @ 0x1601680  size=1 ==== */

void UnitCtrl::PreparePrincessFormProcessor(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  char cVar4;
  int iVar5;
  longlong lVar6;
  undefined8 uVar7;
  undefined8 uStack_98;
  undefined8 uStack_90;
  undefined8 uStack_88;
  longlong lStack_80;
  undefined8 uStack_78;
  undefined8 uStack_70;
  undefined8 uStack_68;
  undefined8 uStack_60;
  longlong lStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  undefined *puStack_30;
  undefined8 uStack_28;
  
  lVar1 = *(longlong *)(param_1 + 0x30);
  if (lVar1 == 0) {
    puStack_30 = &UNK_18160169d;
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  if (cRam000000018ab76869 == '\0') {
    sub_8032f6d0(0x187b53630);
    sub_8032f6d0(0x187c3a2a0);
    sub_8032f6d0(0x187c469d8);
    sub_8032f6d0(0x187beaa88);
    sub_8032f6d0(0x187beac40);
    sub_8032f6d0(0x187c46b90);
    sub_8032f6d0(0x187beadf8);
    sub_8032f6d0(0x187c46d40);
    sub_8032f6d0(0x187b94218);
    sub_8032f6d0(0x187bf83a8);
    sub_8032f6d0(0x187bc9650);
    sub_8032f6d0(0x187b3dd28);
    cRam000000018ab76869 = '\x01';
  }
  uStack_98 = 0;
  uStack_90 = 0;
  uStack_88 = 0;
  lStack_80 = 0;
  uStack_78 = 0;
  if (*(longlong *)(lVar1 + 0x28) != 0) {
    *(undefined1 *)(*(longlong *)(lVar1 + 0x28) + 900) = 1;
    if (*(longlong *)(lVar1 + 0x28) != 0) {
      cVar3 = BattleManager::CanCreateBattleEffect(*(longlong *)(lVar1 + 0x28),0);
      if (cVar3 == '\0') {
        return;
      }
      if (*(longlong *)(lVar1 + 0x28) != 0) {
        cVar3 = BattleManager::get_IsUbSkipModeTrueAndIsNotPlayingEnemyUb(*(longlong *)(lVar1 + 0x28),0);
        if (cVar3 != '\0') {
          return;
        }
        if (*(char *)(lVar1 + 0x90) == '\0') {
          if (*(longlong *)(lVar1 + 0x10) == 0) goto code_r0x0001837cee68;
          cVar3 = *(char *)(*(longlong *)(lVar1 + 0x10) + 0x188);
        }
        else {
          cVar3 = '\0';
        }
        lVar6 = sub_80003d20(uRam0000000187b3dd28);
        if (lVar6 != 0) {
          cVar4 = SaveDataManager::get_IsPrincessUpMovieSpeedUp(lVar6,0);
          if (cVar4 == '\0') {
            if (*(longlong *)(lVar1 + 0x38) == 0) goto code_r0x0001837cee68;
            uVar7 = 2;
          }
          else {
            if (*(longlong *)(lVar1 + 0x38) == 0) goto code_r0x0001837cee68;
            uVar7 = 0;
          }
          cVar4 = sub_80002270(uVar7,uRam0000000187b94218);
          if (cVar4 == '\0') {
            iVar5 = (cVar3 != '\0') + 0x1f;
          }
          else {
            iVar5 = (cVar3 != '\0') + 0x21;
          }
          *(int *)(lVar1 + 0x40) = iVar5;
          if ((*(longlong *)(lVar1 + 0x10) != 0) && (*(longlong *)(lVar1 + 0x20) != 0)) {
            MovieManager::Load(*(longlong *)(lVar1 + 0x20),*(undefined4 *)(lVar1 + 0x40),
                          (longlong)*(int *)(*(longlong *)(lVar1 + 0x10) + 0x4b8),0,0,1,1,0,0,0,0,
                          0x3f800000,0,0);
            if (*(longlong *)(lVar1 + 0x58) != 0) {
              sub_855337a0(&uStack_70,*(longlong *)(lVar1 + 0x58),uRam0000000187b53630);
              uStack_48 = uStack_70;
              uStack_40 = uStack_68;
              uStack_38 = uStack_60;
              puStack_30 = (undefined *)lStack_58;
              uStack_28 = uStack_50;
              while( true ) {
                cVar3 = sub_85760f30(&uStack_48,uRam0000000187beac40);
                if (cVar3 == '\0') {
                  sub_80443e50(&uStack_48,uRam0000000187beaa88);
                  return;
                }
                if (puStack_30 == (undefined *)0x0) break;
                sub_855337a0(&uStack_70,puStack_30,uRam0000000187c3a2a0);
                uStack_98 = uStack_70;
                uStack_90 = uStack_68;
                uStack_88 = uStack_60;
                lStack_80 = lStack_58;
                uStack_78 = uStack_50;
                while (cVar3 = sub_85760f30(&uStack_98,uRam0000000187c46b90), cVar3 != '\0') {
                  if (lStack_80 == 0) {
code_r0x0001837cee51:
                    sub_8032f9d0();
                    sub_8032b6b0();
                    goto code_r0x0001837cee5c;
                  }
                  if (*(longlong *)(lStack_80 + 0x10) == 0) {
                    sub_8032f9d0();
                    goto code_r0x0001837cee51;
                  }
                  *(undefined1 *)(*(longlong *)(lStack_80 + 0x10) + 0x14) = 0;
                }
                sub_80443e50(&uStack_98,uRam0000000187c469d8);
              }
code_r0x0001837cee5c:
              sub_8032f9d0();
              sub_8032b6b0();
              pcVar2 = (code *)swi(3);
              (*pcVar2)();
              return;
            }
          }
        }
      }
    }
  }
code_r0x0001837cee68:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::ResetBattleCameraShakeEffect  @ 0x16016a0  size=1 ==== */

void UnitCtrl::ResetBattleCameraShakeEffect(void)

{
  undefined8 uVar1;
  undefined4 uVar2;
  longlong *plVar3;
  longlong lVar4;
  code *pcVar5;
  undefined8 *puVar6;
  ushort uVar7;
  undefined8 uStack_18;
  undefined4 uStack_10;
  
  if (cRam000000018ab6306c == '\0') {
    sub_8032f6d0(0x187b8ac68);
    cRam000000018ab6306c = '\x01';
  }
  if (cRam000000018ab6307c == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307c = '\x01';
  }
  plVar3 = *(longlong **)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x10);
  if (cRam000000018ab5adcc == '\0') {
    sub_8032f6d0(0x187c16e48);
    cRam000000018ab5adcc = '\x01';
  }
  if (plVar3 == (longlong *)0x0) {
    sub_8032f9d0();
    pcVar5 = (code *)swi(3);
    (*pcVar5)();
    return;
  }
  uVar1 = **(undefined8 **)(lRam0000000187c16e48 + 0xb8);
  lVar4 = *plVar3;
  uVar2 = *(undefined4 *)(*(undefined8 **)(lRam0000000187c16e48 + 0xb8) + 1);
  uVar7 = 0;
  if (*(ushort *)(lVar4 + 0x12e) != 0) {
    do {
      if (*(longlong *)(*(longlong *)(lVar4 + 0xb0) + (ulonglong)uVar7 * 0x10) ==
          lRam0000000187b8ac68) {
        puVar6 = (undefined8 *)
                 ((longlong)
                  (*(int *)(*(longlong *)(lVar4 + 0xb0) + 8 + (ulonglong)uVar7 * 0x10) + 3) * 0x10 +
                  0x138 + lVar4);
        goto code_r0x000181601789;
      }
      uVar7 = uVar7 + 1;
    } while (uVar7 < *(ushort *)(lVar4 + 0x12e));
  }
  puVar6 = (undefined8 *)sub_8032c930(plVar3,lRam0000000187b8ac68,3);
code_r0x000181601789:
  uStack_18 = uVar1;
  uStack_10 = uVar2;
  (*(code *)*puVar6)(plVar3,&uStack_18,puVar6[1]);
  return;
}



/* ==== UnitCtrl::ResetCastTime  @ 0x16017e0  size=1 ==== */

void UnitCtrl::ResetCastTime(longlong param_1)

{
  undefined8 uVar1;
  int iVar2;
  longlong lVar3;
  code *pcVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  undefined8 uVar7;
  undefined8 uVar8;
  undefined4 *puVar9;
  float fVar10;
  float fVar11;
  undefined4 uVar12;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 auStack_38 [6];
  
  if (cRam000000018ab63052 == '\0') {
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab63052 = '\x01';
  }
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar3 == 0) goto code_r0x000181601958;
  iVar2 = *(int *)(lVar3 + 0x2c0);
  if (iVar2 == 5) {
    if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uVar12 = 0x42b40000;
  }
  else {
    if (iVar2 != 6) {
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar3 == 0) {
code_r0x000181601958:
        sub_8032f9d0();
        pcVar4 = (code *)swi(3);
        (*pcVar4)();
        return;
      }
      if (*(int *)(lVar3 + 0x22c) == 0) {
        uVar12 = 0x40200000;
      }
      else {
        uVar12 = 0x3e99999a;
      }
      if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      puVar9 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(auStack_38,uVar12,0);
      if (param_1 == 0) goto code_r0x000181601958;
      goto code_r0x000181601924;
    }
    if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uVar12 = 0x3e99999a;
  }
  puVar9 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(auStack_38,uVar12,0);
code_r0x000181601924:
  uVar12 = puVar9[1];
  uVar5 = puVar9[2];
  uVar6 = puVar9[3];
  uVar1 = *(undefined8 *)(puVar9 + 4);
  *(undefined4 *)(param_1 + 0x278) = *puVar9;
  *(undefined4 *)(param_1 + 0x27c) = uVar12;
  *(undefined4 *)(param_1 + 0x280) = uVar5;
  *(undefined4 *)(param_1 + 0x284) = uVar6;
  *(undefined8 *)(param_1 + 0x288) = uVar1;
  sub_8032e8d0(param_1 + 0x280,0);
  if (cRam000000018ab63073 == '\0') {
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab63073 = '\x01';
  }
  uVar7 = *(undefined8 *)(param_1 + 0x278);
  uVar8 = *(undefined8 *)(param_1 + 0x280);
  uVar1 = *(undefined8 *)(param_1 + 0x288);
  if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uStack_48 = uVar7;
  uStack_40 = uVar8;
  auStack_38[0] = uVar1;
  fVar10 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_48,0);
  fVar11 = 0.0;
  if (0.0 <= fVar10) {
    fVar11 = fVar10;
  }
  *(float *)(param_1 + 0x290) = fVar11;
  return;
}



/* ==== UnitCtrl::ResetPosForUserUnit  @ 0x1601960  size=1 ==== */

void UnitCtrl::ResetPosForUserUnit(longlong param_1,int param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  undefined8 uVar3;
  ulonglong uVar4;
  longlong lVar5;
  undefined8 *puVar6;
  float fVar7;
  float fVar8;
  undefined8 uStack_68;
  ulonglong uStack_60;
  undefined8 uStack_58;
  undefined8 uStack_48;
  undefined4 uStack_40;
  
  if (cRam000000018ab63054 == '\0') {
    sub_8032f6d0(0x187c41780);
    sub_8032f6d0(0x187b60de8);
    sub_8032f6d0(0x187be9010);
    cRam000000018ab63054 = '\x01';
  }
  lVar5 = FixedTransformMonoBehavior::get_transform(param_1,0);
  if (lVar5 == 0) goto code_r0x000181601c9c;
  puVar6 = (undefined8 *)FixedTransform::get_localPosition(&uStack_68,lVar5,0);
  uStack_48 = *puVar6;
  uStack_40 = *(undefined4 *)(puVar6 + 1);
  fVar8 = -560.0;
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar5 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar5 == 0) goto code_r0x000181601c9c;
  if (*(int *)(lVar5 + 0x2c0) == 6) {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar5 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar5 == 0) goto code_r0x000181601c9c;
    if (*(int *)(lVar5 + 0x22c) != 0) goto code_r0x000181601b2b;
    if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181601c9c;
    *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0xb8) = 1;
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar5 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar5 == 0) goto code_r0x000181601c9c;
    if (*(int *)(lVar5 + 0x2c0) == 6) {
      if (*(int *)(lRam0000000187be9010 + 0xe4) == 0) {
        sub_8032fa70(lRam0000000187be9010);
      }
      lVar5 = *(longlong *)(*(longlong *)(lRam0000000187be9010 + 0xb8) + 0x30);
      if ((*(longlong *)(param_1 + 200) == 0) || (lVar5 == 0)) goto code_r0x000181601c9c;
      fVar8 = (float)sub_855cef80(lVar5,*(undefined4 *)(*(longlong *)(param_1 + 200) + 0x38),
                                   uRam0000000187c41780);
    }
  }
  else {
code_r0x000181601b2b:
    fVar8 = -560.0 - (float)(param_2 + 1) * 200.0;
  }
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar5 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar5 == 0) goto code_r0x000181601c9c;
  if (*(char *)(lVar5 + 0x344) != '\0') {
    lVar5 = *(longlong *)(param_1 + 0x88);
    if (lVar5 == 0) goto code_r0x000181601c9c;
    uVar3 = *(undefined8 *)(lVar5 + 0x7e0);
    uVar4 = *(ulonglong *)(lVar5 + 0x7e8);
    uVar1 = *(undefined8 *)(lVar5 + 0x7f0);
    if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_68 = uVar3;
    uStack_60 = uVar4;
    uStack_58 = uVar1;
    fVar7 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_68,0);
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar5 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if ((lVar5 == 0) || (lVar5 = *(longlong *)(lVar5 + 0x110), lVar5 == 0))
    goto code_r0x000181601c9c;
    fVar8 = fVar8 - fVar7 * *(float *)(lVar5 + 0x33c) * 1.6;
  }
  lVar5 = FixedTransformMonoBehavior::get_transform(param_1,0);
  uStack_60 = uStack_60 & 0xffffffff00000000;
  uStack_68 = CONCAT44((int)((ulonglong)uStack_48 >> 0x20),fVar8);
  if (lVar5 != 0) {
    uStack_40 = 0;
    uStack_48 = uStack_68;
    FixedTransform::set_localPosition(lVar5,&uStack_48,0);
    return;
  }
code_r0x000181601c9c:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::ResetThanksTargetUnit  @ 0x1601cb0  size=1 ==== */

void UnitCtrl::ResetThanksTargetUnit(void)

{
  longlong lVar1;
  code *pcVar2;
  longlong lVar3;
  int iVar4;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar1 == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  if (cRam000000018ab761e3 == '\0') {
    sub_8032f6d0(0x187b73698,0);
    sub_8032f6d0(0x187b73858);
    cRam000000018ab761e3 = '\x01';
  }
  iVar4 = 0;
  lVar3 = *(longlong *)(lVar1 + 0x240);
  while (lVar3 != 0) {
    if (*(int *)(lVar3 + 0x18) <= iVar4) {
      return;
    }
    if (((*(longlong *)(lVar1 + 0x240) == 0) ||
        (lVar3 = sub_849ed2d0(*(longlong *)(lVar1 + 0x240),iVar4,uRam0000000187b73858), lVar3 == 0)
        ) || (*(longlong *)(lVar3 + 0xd8) == 0)) break;
    iVar4 = iVar4 + 1;
    *(undefined4 *)(*(longlong *)(lVar3 + 0xd8) + 0x18) = 0;
    lVar3 = *(longlong *)(lVar1 + 0x240);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::RestartPlayAnimeCoroutine  @ 0x1601d00  size=1 ==== */

/* WARNING: Removing unreachable block (ram,0x000183dd217a) */
/* WARNING: Removing unreachable block (ram,0x000183dd2183) */
/* WARNING: Removing unreachable block (ram,0x000183dd2196) */
/* WARNING: Removing unreachable block (ram,0x000183dd21d3) */

void UnitCtrl::RestartPlayAnimeCoroutine(undefined8 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                  undefined4 param_5)

{
  undefined4 uVar1;
  code *pcVar2;
  longlong lVar3;
  undefined8 uVar4;
  longlong lVar5;
  undefined8 unaff_RDI;
  int iVar6;
  undefined4 unaff_XMM6_Da;
  undefined4 unaff_XMM6_Db;
  undefined4 unaff_XMM6_Dc;
  undefined4 unaff_XMM6_Dd;
  undefined4 uVar7;
  undefined8 uVar8;
  
  uVar4 = CONCAT44(unaff_XMM6_Db,unaff_XMM6_Da);
  uVar8 = CONCAT44(unaff_XMM6_Dd,unaff_XMM6_Dc);
  lVar3 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
  if (lVar3 == 0) {
    sub_8032f9d0();
    pcVar2 = (code *)swi(3);
    (*pcVar2)();
    return;
  }
  if ((*(longlong *)(lVar3 + 200) != 0) &&
     (lVar5 = *(longlong *)(*(longlong *)(lVar3 + 200) + 0x10), lVar5 != 0)) {
    lVar5 = *(longlong *)(lVar5 + 0x10);
    uVar4 = BattleUnitBaseSpineController::ConvertAnimeIdToAnimeName(lVar3,param_3,param_4,0xffffffff,param_5,0,uVar4,uVar8,unaff_RDI);
    if ((lVar5 != 0) && (lVar5 = SkeletonData::FindAnimation(lVar5,uVar4,0), lVar5 != 0)) {
      uVar1 = *(undefined4 *)(lVar5 + 0x18);
      *(undefined1 *)(lVar3 + 0x1a8) = 1;
      if (cRam000000018ab7b1ab == '\0') {
        sub_8032f6d0(0x187b8c7a8);
        cRam000000018ab7b1ab = '\x01';
      }
      if (cRam000000018ab7b21f == '\0') {
        sub_8032f6d0(0x187b42c78);
        cRam000000018ab7b21f = '\x01';
      }
      iVar6 = *(int *)(lVar3 + 0x1a4) + 1;
      uVar7 = 0;
      lVar5 = *(longlong *)(*(longlong *)(lRam0000000187b42c78 + 0xb8) + 8);
      *(int *)(lVar3 + 0x1a4) = iVar6;
      uVar4 = BattleSpineController::updateAnime(lVar3,param_2,uVar1,iVar6,0);
      if (lVar5 != 0) {
        sub_800110c0(3,uRam0000000187b8c7a8,lVar5,uVar4,CONCAT44(uVar7,1),
                      *(undefined8 *)(lVar3 + 0x178));
        return;
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



/* ==== UnitCtrl::SearchContinuousUnit  @ 0x1601d60  size=1 ==== */

undefined8 UnitCtrl::SearchContinuousUnit(undefined8 param_1)

{
  longlong lVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  longlong lVar5;
  undefined8 uVar6;
  uint uVar7;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab63069 == '\0') {
    sub_8032f6d0(0x187bd2e20);
    sub_8032f6d0(0x187b85620);
    sub_8032f6d0(0x187b30228);
    cRam000000018ab63069 = '\x01';
  }
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar4 != 0) {
    lVar4 = BattleManager::GetContinuousUnits(lVar4,0);
    lVar5 = sub_8032f970(uRam0000000187b85620);
    sub_84cacb40(lVar5,param_1,uRam0000000187b30228,0);
    if (lVar4 != 0) {
      if (lVar5 == 0) {
        ThrowHelper::ThrowArgumentNullException(8,0,uRam0000000187bd2e20);
      }
      uVar7 = 0;
      if (0 < *(int *)(lVar4 + 0x18)) {
        do {
          lVar1 = *(longlong *)(lVar4 + 0x10);
          if (lVar1 == 0) goto code_r0x000184a8c9bd;
          if (*(uint *)(lVar1 + 0x18) <= uVar7) {
code_r0x000184a8c9c3:
            sub_8032f9c0();
            pcVar2 = (code *)swi(3);
            uVar6 = (*pcVar2)();
            return uVar6;
          }
          if (lVar5 == 0) {
code_r0x000184a8c9bd:
            sub_8032f9d0();
            pcVar2 = (code *)swi(3);
            uVar6 = (*pcVar2)();
            return uVar6;
          }
          cVar3 = (**(code **)(lVar5 + 0x18))
                            (*(undefined8 *)(lVar5 + 0x40),
                             *(undefined8 *)(lVar1 + 0x20 + (longlong)(int)uVar7 * 8),
                             *(undefined8 *)(lVar5 + 0x28),*(code **)(lVar5 + 0x18),unaff_RDI);
          if (cVar3 != '\0') {
            lVar4 = *(longlong *)(lVar4 + 0x10);
            if (lVar4 != 0) {
              if (uVar7 < *(uint *)(lVar4 + 0x18)) {
                return *(undefined8 *)(lVar4 + 0x20 + (longlong)(int)uVar7 * 8);
              }
              goto code_r0x000184a8c9c3;
            }
            goto code_r0x000184a8c9bd;
          }
          uVar7 = uVar7 + 1;
        } while ((int)uVar7 < *(int *)(lVar4 + 0x18));
      }
      return 0;
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar6 = (*pcVar2)();
  return uVar6;
}



/* ==== UnitCtrl::SetCurrentHpForTowerTimeUp  @ 0x1601e40  size=1 ==== */

void UnitCtrl::SetCurrentHpForTowerTimeUp(longlong param_1,undefined8 param_2)

{
  undefined8 uVar1;
  longlong lVar2;
  code *pcVar3;
  undefined4 uVar4;
  char cVar5;
  undefined4 *puVar6;
  undefined4 uVar7;
  undefined4 uVar8;
  undefined1 auStack_38 [48];
  
  if (cRam000000018ab63063 == '\0') {
    sub_8032f6d0(0x187c411f8);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b61160);
    cRam000000018ab63063 = '\x01';
  }
  lVar2 = *(longlong *)(param_1 + 0x88);
  if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
    sub_8032fa70();
  }
  puVar6 = (undefined4 *)ObscuredLong::FHOALPFLEEC(auStack_38,param_2,0);
  if (lVar2 != 0) {
    uVar7 = puVar6[1];
    uVar8 = puVar6[2];
    uVar4 = puVar6[3];
    uVar1 = *(undefined8 *)(puVar6 + 4);
    *(undefined4 *)(lVar2 + 0x430) = *puVar6;
    *(undefined4 *)(lVar2 + 0x434) = uVar7;
    *(undefined4 *)(lVar2 + 0x438) = uVar8;
    *(undefined4 *)(lVar2 + 0x43c) = uVar4;
    *(undefined8 *)(lVar2 + 0x440) = uVar1;
    uVar1 = *(undefined8 *)(param_1 + 0x268);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar5 = Object::op_Inequality(uVar1,0,0);
    if (cVar5 == '\0') {
      return;
    }
    if (*(longlong *)(param_1 + 0x88) != 0) {
      uVar7 = CIFIMDJAGGF::EMGAPCLDPEC(*(longlong *)(param_1 + 0x88),0);
      lVar2 = *(longlong *)(param_1 + 0x268);
      if ((*(longlong *)(param_1 + 0x88) != 0) &&
         (uVar8 = CIFIMDJAGGF::EKBENPKDHNC(*(longlong *)(param_1 + 0x88),0), lVar2 != 0)) {
        LifeGaugeController::SetNormalizedLifeAmount(lVar2,uVar7,uVar8,0,1,0);
        if (*(longlong *)(param_1 + 0x70) != 0) {
          sub_847b6ca0(*(undefined8 *)(*(longlong *)(param_1 + 0x70) + 0x130),uVar7,
                        uRam0000000187c411f8);
          if (*(longlong *)(param_1 + 0x70) != 0) {
            EventExtension::Call(*(undefined8 *)(*(longlong *)(param_1 + 0x70) + 0xd8),0);
            if (*(longlong *)(param_1 + 0x70) != 0) {
              uVar1 = DBGNMELIABG::IFAEPOJPNFB(*(longlong *)(param_1 + 0x70),0);
              sub_86209420(param_1,uVar1,0);
              return;
            }
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



/* ==== UnitCtrl::SetDirectionAuto  @ 0x1601fe0  size=1 ==== */

void UnitCtrl::SetDirectionAuto(longlong param_1)

{
  undefined8 uVar1;
  byte bVar2;
  undefined4 uVar3;
  code *pcVar4;
  undefined8 *puVar5;
  char cVar6;
  char cVar7;
  longlong lVar8;
  undefined8 *puVar9;
  longlong lVar10;
  int iVar11;
  bool bVar12;
  uint uVar13;
  bool bVar14;
  float fVar15;
  float fVar16;
  undefined8 uStack_110;
  undefined8 uStack_f8;
  undefined8 *puStack_f0;
  undefined8 uStack_e8;
  longlong lStack_e0;
  undefined8 uStack_d8;
  uint uStack_c8;
  uint uStack_c4;
  longlong lStack_c0;
  undefined8 uStack_b8;
  undefined8 *puStack_b0;
  undefined8 uStack_a8;
  longlong lStack_a0;
  undefined8 uStack_98;
  undefined8 *puStack_90;
  undefined8 uStack_88;
  longlong lStack_80;
  undefined8 uStack_78;
  
  cVar6 = UnitCtrl::isNearestEnemyLeft(param_1,0);
  lStack_c0 = param_1;
  if (cRam000000018ab6305c == '\0') {
    sub_8032f6d0(0x187bf15a8,cVar6,0);
    sub_8032f6d0(0x187c268b8);
    sub_8032f6d0(0x187b47a60);
    sub_8032f6d0(0x187c26a70);
    sub_8032f6d0(0x187b47c10);
    sub_8032f6d0(0x187b47dc8);
    sub_8032f6d0(0x187c26c20);
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187c122f0);
    sub_8032f6d0(0x187b4e120);
    sub_8032f6d0(0x187b9aea8);
    sub_8032f6d0(0x187b37388);
    sub_8032f6d0(0x187b37540);
    sub_8032f6d0(0x187b9b058);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b60538);
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab6305c = '\x01';
  }
  uStack_b8 = 0;
  puStack_b0 = (undefined8 *)0x0;
  uStack_a8 = 0;
  lStack_a0 = 0;
  *(char *)(param_1 + 0x165) = cVar6;
  if ((*(longlong *)(param_1 + 0x38) == 0) ||
     (lVar8 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar8 == 0))
  goto code_r0x000181602d14;
  if (*(int *)(lVar8 + 0x18) < 1) {
    lVar8 = UnitCtrl::GetCurrentSpineCtrl(param_1);
    if (lVar8 == 0) goto code_r0x000181602d14;
    lVar8 = Component::get_transform(lVar8,0);
    if ((*(char *)(param_1 + 0x165) == '\0') && (*(char *)(param_1 + 0x380) == '\0')) {
      if (cRam000000018ab6306a == '\0') {
        sub_8032f6d0(0x187b9ead8);
        cRam000000018ab6306a = '\x01';
      }
      if (*(longlong *)(param_1 + 0x388) == 0) goto code_r0x000181602d14;
      if (0 < *(int *)(*(longlong *)(param_1 + 0x388) + 0x18)) goto code_r0x0001816025b6;
      fVar15 = *(float *)(param_1 + 0x46c);
      fVar16 = *(float *)(param_1 + 0x470);
    }
    else {
code_r0x0001816025b6:
      fVar15 = *(float *)(param_1 + 0x464);
      fVar16 = *(float *)(param_1 + 0x468);
    }
    lVar10 = *(longlong *)(param_1 + 0xb0);
    if ((lVar10 == 0) ||
       (uStack_110 = CONCAT44(fVar16 * *(float *)(lVar10 + 0x1c),fVar15 * *(float *)(lVar10 + 0x1c))
       , lVar8 == 0)) goto code_r0x000181602d14;
    uStack_f8 = uStack_110;
    puStack_f0 = (undefined8 *)((ulonglong)puStack_f0 & 0xffffffff00000000);
    Transform::set_localScale(lVar8,&uStack_f8,0);
  }
  else {
    lVar8 = UnitCtrl::GetCurrentSpineCtrl(param_1);
    if (lVar8 == 0) goto code_r0x000181602d14;
    lVar8 = Component::get_transform(lVar8,0);
    if ((*(char *)(param_1 + 0x165) == '\0') && (*(char *)(param_1 + 0x380) == '\0')) {
      if (((*(longlong *)(param_1 + 0x38) == 0) ||
          (lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar10 == 0)) ||
         (lVar10 = sub_849ed2d0(lVar10,0,uRam0000000187b9b058), lVar10 == 0))
      goto code_r0x000181602d14;
      uVar1 = *(undefined8 *)(lVar10 + 0x2c);
      fVar15 = *(float *)(lVar10 + 0x34);
    }
    else {
      if ((*(longlong *)(param_1 + 0x38) == 0) ||
         ((lVar10 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar10 == 0 ||
          (lVar10 = sub_849ed2d0(lVar10,0,uRam0000000187b9b058), lVar10 == 0))))
      goto code_r0x000181602d14;
      uVar1 = *(undefined8 *)(lVar10 + 0x20);
      fVar15 = *(float *)(lVar10 + 0x28);
    }
    if (*(longlong *)(param_1 + 0xb0) == 0) goto code_r0x000181602d14;
    fVar16 = *(float *)(*(longlong *)(param_1 + 0xb0) + 0x1c);
    uStack_110._4_4_ = (float)((ulonglong)uVar1 >> 0x20);
    uStack_110._0_4_ = (float)uVar1;
    uStack_110 = CONCAT44(uStack_110._4_4_ * fVar16,(float)uStack_110 * fVar16);
    if (lVar8 == 0) goto code_r0x000181602d14;
    uStack_f8 = uStack_110;
    puStack_f0 = (undefined8 *)CONCAT44(puStack_f0._4_4_,fVar15 * fVar16);
    Transform::set_localScale(lVar8,&uStack_f8,0);
  }
  fVar15 = 0.0;
  uVar1 = *(undefined8 *)(param_1 + 0x4f0);
  uVar3 = *(undefined4 *)(param_1 + 0x4f8);
  if (*(int *)(lRam0000000187b60538 + 0xe4) == 0) {
    sub_8032fa70();
  }
  puStack_f0 = (undefined8 *)CONCAT44(puStack_f0._4_4_,uVar3);
  uStack_f8 = uVar1;
  cVar7 = ObscuredBool::FHOALPFLEEC(&uStack_f8,0);
  if (cVar7 == '\0') {
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x000181602d14;
    puVar9 = (undefined8 *)MBJPLNBCBDB::ODOPDBNIJNO(&uStack_f8,*(longlong *)(param_1 + 0x90),0);
    uVar1 = *puVar9;
    puVar5 = (undefined8 *)puVar9[1];
    uStack_e8 = puVar9[2];
    if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_f8 = uVar1;
    puStack_f0 = puVar5;
    fVar15 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_f8,0);
    if (*(char *)(param_1 + 0x165) == '\0') {
      fVar16 = 1.0;
    }
    else {
      fVar16 = -1.0;
    }
    fVar15 = fVar15 * fVar16;
  }
  *(float *)(param_1 + 0x2d8) = fVar15;
  if (cVar6 == '\0') {
    if (*(char *)(param_1 + 0x380) == '\0') {
      if (cRam000000018ab6306a == '\0') {
        sub_8032f6d0(0x187b9ead8);
        cRam000000018ab6306a = '\x01';
      }
      if (*(longlong *)(param_1 + 0x388) == 0) goto code_r0x000181602d14;
      iVar11 = *(int *)(*(longlong *)(param_1 + 0x388) + 0x18);
      bVar12 = 0 < iVar11;
      uStack_c4 = (uint)(iVar11 < 1);
      uVar13 = (uint)(iVar11 < 1);
      bVar14 = iVar11 < 1;
    }
    else {
      bVar12 = true;
      uStack_c4 = 0;
      uVar13 = 0;
      bVar14 = false;
    }
  }
  else {
    bVar12 = true;
    uVar13 = 0;
    uStack_c4 = 0;
    bVar14 = false;
  }
  lVar8 = *(longlong *)(param_1 + 0x38);
  uStack_c8 = uVar13;
  if (lVar8 != 0) {
    bVar2 = *(byte *)(lVar8 + 0xe6);
    if (*(longlong *)(lVar8 + 0x90) != 0) {
      sub_855337a0(&uStack_f8,*(longlong *)(lVar8 + 0x90),uRam0000000187bf15a8);
      uStack_98 = uStack_f8;
      puStack_90 = puStack_f0;
      uStack_88 = uStack_e8;
      lStack_80 = lStack_e0;
      uStack_78 = uStack_d8;
      uStack_f8 = 0;
      puStack_f0 = &uStack_98;
code_r0x000181602930:
      cVar6 = sub_85760f30(&uStack_98,uRam0000000187b47c10);
      lVar8 = lStack_80;
      if (cVar6 != '\0') {
        iVar11 = 0;
        do {
          if (lVar8 == 0) goto code_r0x000181602d08;
          lVar10 = *(longlong *)(lVar8 + 0x48);
          if (lVar10 == 0) {
code_r0x000181602d03:
            sub_8032f9d0();
code_r0x000181602d08:
            sub_8032f9d0();
            sub_8032b6b0();
            pcVar4 = (code *)swi(3);
            (*pcVar4)();
            return;
          }
          if (*(int *)(lVar10 + 0x18) <= iVar11) goto code_r0x000181602930;
          lVar10 = sub_849ed2d0(lVar10,iVar11,uRam0000000187b37540);
          if (lVar10 == 0) {
            sub_8032f9d0();
            goto code_r0x000181602d03;
          }
          uVar1 = *(undefined8 *)(lVar10 + 0x18);
          if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
            sub_8032fa70();
          }
          cVar6 = Object::op_Inequality(uVar1,0,0);
          if (cVar6 != '\0') {
            uVar1 = *(undefined8 *)(lVar10 + 0x18);
            if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
              sub_8032fa70();
            }
            ExtensionMethods::SetActive(uVar1,bVar2 & bVar12,0);
            uVar1 = *(undefined8 *)(lVar10 + 0x10);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar6 = Object::op_Inequality(uVar1,0,0);
            if (cVar6 != '\0') {
              uVar1 = *(undefined8 *)(lVar10 + 0x10);
              if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                sub_8032fa70();
              }
              ExtensionMethods::SetActive(uVar1,bVar14 & bVar2,0);
            }
          }
          iVar11 = iVar11 + 1;
        } while( true );
      }
      sub_80443e50(&uStack_98,uRam0000000187b47a60);
      lVar8 = *(longlong *)(param_1 + 0x38);
      if (bVar2 == 0) {
        bVar14 = false;
        if (lVar8 == 0) goto code_r0x000181602d14;
      }
      else {
        if (lVar8 == 0) goto code_r0x000181602d14;
        bVar14 = *(char *)(lVar8 + 0x208) != '\0';
        lVar8 = *(longlong *)(lStack_c0 + 0x38);
      }
      if (*(longlong *)(lVar8 + 0x1b8) != 0) {
        sub_849d8f60(&uStack_f8,*(longlong *)(lVar8 + 0x1b8),uRam0000000187b4e120);
        uStack_b8 = uStack_f8;
        puStack_b0 = puStack_f0;
        uStack_a8 = uStack_e8;
        lStack_a0 = lStack_e0;
        uStack_f8 = 0;
        puStack_f0 = &uStack_b8;
        while (cVar6 = sub_8571f880(&uStack_b8,uRam0000000187c26a70), lVar8 = lStack_a0,
              uVar1 = uStack_a8, cVar6 != '\0') {
          if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
            sub_8032fa70();
          }
          ExtensionMethods::SetActive(lVar8,bVar14 & bVar12);
          ExtensionMethods::SetActive(uVar1,(byte)uVar13 & bVar14);
        }
        sub_80443e50(&uStack_b8,uRam0000000187c268b8);
        lVar8 = *(longlong *)(param_1 + 0x38);
        if (lVar8 != 0) {
          if (*(int *)(lVar8 + 0x350) != 0) {
            uVar1 = *(undefined8 *)(lVar8 + 0x360);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar6 = Object::op_Inequality(uVar1,0,0);
            if (cVar6 != '\0') {
              if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x000181602d14;
              uVar1 = *(undefined8 *)(*(longlong *)(param_1 + 0x38) + 0x360);
              if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                sub_8032fa70();
              }
              ExtensionMethods::SetActive(uVar1,bVar2 & bVar12,0);
            }
            if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x000181602d14;
            uVar1 = *(undefined8 *)(*(longlong *)(param_1 + 0x38) + 0x358);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar6 = Object::op_Inequality(uVar1,0,0);
            if (cVar6 != '\0') {
              if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x000181602d14;
              uVar1 = *(undefined8 *)(*(longlong *)(param_1 + 0x38) + 0x358);
              if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                sub_8032fa70();
              }
              ExtensionMethods::SetActive(uVar1,(byte)uStack_c4 & bVar2,0);
            }
          }
          return;
        }
      }
    }
  }
code_r0x000181602d14:
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitCtrl::SetInitialOrder  @ 0x1602010  size=1 ==== */

void UnitCtrl::SetInitialOrder(undefined8 param_1,int param_2)

{
  undefined8 uVar1;
  undefined4 uVar2;
  undefined4 uVar3;
  code *pcVar4;
  longlong lVar5;
  char cVar6;
  int iVar7;
  longlong lVar8;
  undefined8 *puVar9;
  undefined8 uStack_d8;
  undefined4 uStack_d0;
  undefined8 uStack_c8;
  undefined4 uStack_c0;
  undefined8 uStack_b8;
  undefined8 *puStack_b0;
  undefined8 uStack_a8;
  undefined4 uStack_a0;
  undefined8 uStack_98;
  undefined8 uStack_90;
  undefined8 uStack_88;
  longlong lStack_80;
  undefined8 uStack_78;
  undefined8 uStack_68;
  longlong lStack_60;
  undefined8 uStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  longlong lStack_40;
  undefined8 uStack_38;
  
  if (cRam000000018ab63057 == '\0') {
    sub_8032f6d0(0x187c3d910);
    sub_8032f6d0(0x187c47410);
    sub_8032f6d0(0x187c475c0);
    sub_8032f6d0(0x187c47770);
    sub_8032f6d0(0x187bf8df8);
    sub_8032f6d0(0x187b3c708);
    sub_8032f6d0(0x187b60fa0);
    sub_8032f6d0(0x187c0f7d0);
    cRam000000018ab63057 = '\x01';
  }
  lVar8 = *(longlong *)(lRam0000000187b3c708 + 0x20);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = sub_8032d010(lVar8);
  }
  lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
  if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
    lVar8 = sub_8032d010(lVar8);
  }
  if (((**(longlong **)(lVar8 + 0xb8) == 0) ||
      (lVar8 = *(longlong *)(**(longlong **)(lVar8 + 0xb8) + 0xb30), lVar8 == 0)) ||
     (lVar8 = JDHDCDAJAJA::GBHHHNJOAIM(lVar8,0), lVar8 == 0)) {
    sub_8032f9d0();
    pcVar4 = (code *)swi(3);
    (*pcVar4)();
    return;
  }
  sub_855337a0(&uStack_58,lVar8,uRam0000000187c3d910);
  uStack_98 = uStack_58;
  uStack_90 = uStack_50;
  uStack_88 = uStack_48;
  lStack_80 = lStack_40;
  uStack_78 = uStack_38;
  uStack_b8 = 0;
  puStack_b0 = &uStack_98;
  do {
    do {
      do {
        cVar6 = sub_85760f30(&uStack_98,uRam0000000187c475c0);
        if (cVar6 == '\0') {
          sub_80443e50(&uStack_98,uRam0000000187c47410);
          return;
        }
        uStack_68 = uStack_88;
        lStack_60 = lStack_80;
        lVar8 = *(longlong *)(lRam0000000187c0f7d0 + 0x20);
        if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
          lVar8 = sub_8032d010(lVar8);
        }
        lVar8 = *(longlong *)(*(longlong *)(lVar8 + 0xc0) + 0x10);
        if ((*(byte *)(lVar8 + 0x135) & 1) == 0) {
          lVar8 = sub_8032d010(lVar8);
        }
        lVar5 = lStack_60;
        if (**(longlong **)(lVar8 + 0xb8) == 0) goto code_r0x0001816023d0;
        lVar8 = *(longlong *)(**(longlong **)(lVar8 + 0xb8) + 0x28);
        if (lStack_60 == 0) goto code_r0x0001816023cb;
        uVar1 = *(undefined8 *)(lStack_60 + 0x1c);
        uVar2 = *(undefined4 *)(lStack_60 + 0x24);
        if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_a8 = uVar1;
        uStack_a0 = uVar2;
        iVar7 = ObscuredInt::FHOALPFLEEC(&uStack_a8,0);
        if (lVar8 == 0) goto code_r0x0001816023c6;
      } while (iVar7 != *(int *)(lVar8 + 0x14));
      uVar1 = *(undefined8 *)(lVar5 + 0x28);
      uVar2 = *(undefined4 *)(lVar5 + 0x30);
      if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_c8 = uVar1;
      uStack_c0 = uVar2;
      iVar7 = ObscuredInt::FHOALPFLEEC(&uStack_c8,0);
    } while (iVar7 != *(int *)(lVar8 + 0x1c));
    uVar1 = *(undefined8 *)(lVar5 + 0x4c);
    uVar2 = *(undefined4 *)(lVar5 + 0x54);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_d8 = uVar1;
    uStack_d0 = uVar2;
    iVar7 = ObscuredInt::FHOALPFLEEC(&uStack_d8,0);
  } while (iVar7 != param_2);
  lVar8 = FixedTransformMonoBehavior::get_transform(param_1,0);
  if (lVar8 != 0) {
    puVar9 = (undefined8 *)FixedTransform::get_localPosition(&uStack_d8,lVar8,0);
    uStack_c8 = *puVar9;
    uVar2 = *(undefined4 *)(puVar9 + 1);
    uVar1 = *(undefined8 *)(lVar5 + 0x40);
    uVar3 = *(undefined4 *)(lVar5 + 0x48);
    if (*(int *)(lRam0000000187b60fa0 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_d8 = uVar1;
    uStack_d0 = uVar3;
    iVar7 = ObscuredInt::FHOALPFLEEC(&uStack_d8,0);
    uStack_c8 = CONCAT44(uStack_c8._4_4_,(float)iVar7);
    lVar8 = FixedTransformMonoBehavior::get_transform(param_1,0);
    if (lVar8 != 0) {
      uStack_d8 = uStack_c8;
      uStack_d0 = uVar2;
      FixedTransform::set_localPosition(lVar8,&uStack_d8,0);
      sub_80443e50(&uStack_98,uRam0000000187c47410);
      return;
    }
    sub_8032f9d0();
  }
  sub_8032f9d0();
code_r0x0001816023c6:
  sub_8032f9d0();
code_r0x0001816023cb:
  sub_8032f9d0();
code_r0x0001816023d0:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitCtrl::SetLeftDirection  @ 0x16023f0  size=1 ==== */

void UnitCtrl::SetLeftDirection(longlong param_1,char param_2)

{
  undefined8 uVar1;
  byte bVar2;
  undefined4 uVar3;
  code *pcVar4;
  undefined8 *puVar5;
  char cVar6;
  longlong lVar7;
  undefined8 *puVar8;
  longlong lVar9;
  int iVar10;
  bool bVar11;
  uint uVar12;
  bool bVar13;
  float fVar14;
  float fVar15;
  undefined8 uStack_110;
  undefined8 uStack_f8;
  undefined8 *puStack_f0;
  undefined8 uStack_e8;
  longlong lStack_e0;
  undefined8 uStack_d8;
  uint uStack_c8;
  uint uStack_c4;
  longlong lStack_c0;
  undefined8 uStack_b8;
  undefined8 *puStack_b0;
  undefined8 uStack_a8;
  longlong lStack_a0;
  undefined8 uStack_98;
  undefined8 *puStack_90;
  undefined8 uStack_88;
  longlong lStack_80;
  undefined8 uStack_78;
  
  lStack_c0 = param_1;
  if (cRam000000018ab6305c == '\0') {
    sub_8032f6d0(0x187bf15a8);
    sub_8032f6d0(0x187c268b8);
    sub_8032f6d0(0x187b47a60);
    sub_8032f6d0(0x187c26a70);
    sub_8032f6d0(0x187b47c10);
    sub_8032f6d0(0x187b47dc8);
    sub_8032f6d0(0x187c26c20);
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187c122f0);
    sub_8032f6d0(0x187b4e120);
    sub_8032f6d0(0x187b9aea8);
    sub_8032f6d0(0x187b37388);
    sub_8032f6d0(0x187b37540);
    sub_8032f6d0(0x187b9b058);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b60538);
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab6305c = '\x01';
  }
  uStack_b8 = 0;
  puStack_b0 = (undefined8 *)0x0;
  uStack_a8 = 0;
  lStack_a0 = 0;
  *(char *)(param_1 + 0x165) = param_2;
  if ((*(longlong *)(param_1 + 0x38) == 0) ||
     (lVar7 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar7 == 0))
  goto code_r0x000181602d14;
  if (*(int *)(lVar7 + 0x18) < 1) {
    lVar7 = UnitCtrl::GetCurrentSpineCtrl(param_1);
    if (lVar7 == 0) goto code_r0x000181602d14;
    lVar7 = Component::get_transform(lVar7,0);
    if ((*(char *)(param_1 + 0x165) == '\0') && (*(char *)(param_1 + 0x380) == '\0')) {
      if (cRam000000018ab6306a == '\0') {
        sub_8032f6d0(0x187b9ead8);
        cRam000000018ab6306a = '\x01';
      }
      if (*(longlong *)(param_1 + 0x388) == 0) goto code_r0x000181602d14;
      if (0 < *(int *)(*(longlong *)(param_1 + 0x388) + 0x18)) goto code_r0x0001816025b6;
      fVar14 = *(float *)(param_1 + 0x46c);
      fVar15 = *(float *)(param_1 + 0x470);
    }
    else {
code_r0x0001816025b6:
      fVar14 = *(float *)(param_1 + 0x464);
      fVar15 = *(float *)(param_1 + 0x468);
    }
    lVar9 = *(longlong *)(param_1 + 0xb0);
    if ((lVar9 == 0) ||
       (uStack_110 = CONCAT44(fVar15 * *(float *)(lVar9 + 0x1c),fVar14 * *(float *)(lVar9 + 0x1c)),
       lVar7 == 0)) goto code_r0x000181602d14;
    uStack_f8 = uStack_110;
    puStack_f0 = (undefined8 *)((ulonglong)puStack_f0 & 0xffffffff00000000);
    Transform::set_localScale(lVar7,&uStack_f8,0);
  }
  else {
    lVar7 = UnitCtrl::GetCurrentSpineCtrl(param_1);
    if (lVar7 == 0) goto code_r0x000181602d14;
    lVar7 = Component::get_transform(lVar7,0);
    if ((*(char *)(param_1 + 0x165) == '\0') && (*(char *)(param_1 + 0x380) == '\0')) {
      if (((*(longlong *)(param_1 + 0x38) == 0) ||
          (lVar9 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar9 == 0)) ||
         (lVar9 = sub_849ed2d0(lVar9,0,uRam0000000187b9b058), lVar9 == 0))
      goto code_r0x000181602d14;
      uVar1 = *(undefined8 *)(lVar9 + 0x2c);
      fVar14 = *(float *)(lVar9 + 0x34);
    }
    else {
      if ((*(longlong *)(param_1 + 0x38) == 0) ||
         ((lVar9 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar9 == 0 ||
          (lVar9 = sub_849ed2d0(lVar9,0,uRam0000000187b9b058), lVar9 == 0))))
      goto code_r0x000181602d14;
      uVar1 = *(undefined8 *)(lVar9 + 0x20);
      fVar14 = *(float *)(lVar9 + 0x28);
    }
    if (*(longlong *)(param_1 + 0xb0) == 0) goto code_r0x000181602d14;
    fVar15 = *(float *)(*(longlong *)(param_1 + 0xb0) + 0x1c);
    uStack_110._4_4_ = (float)((ulonglong)uVar1 >> 0x20);
    uStack_110._0_4_ = (float)uVar1;
    uStack_110 = CONCAT44(uStack_110._4_4_ * fVar15,(float)uStack_110 * fVar15);
    if (lVar7 == 0) goto code_r0x000181602d14;
    uStack_f8 = uStack_110;
    puStack_f0 = (undefined8 *)CONCAT44(puStack_f0._4_4_,fVar14 * fVar15);
    Transform::set_localScale(lVar7,&uStack_f8,0);
  }
  fVar14 = 0.0;
  uVar1 = *(undefined8 *)(param_1 + 0x4f0);
  uVar3 = *(undefined4 *)(param_1 + 0x4f8);
  if (*(int *)(lRam0000000187b60538 + 0xe4) == 0) {
    sub_8032fa70();
  }
  puStack_f0 = (undefined8 *)CONCAT44(puStack_f0._4_4_,uVar3);
  uStack_f8 = uVar1;
  cVar6 = ObscuredBool::FHOALPFLEEC(&uStack_f8,0);
  if (cVar6 == '\0') {
    if (*(longlong *)(param_1 + 0x90) == 0) goto code_r0x000181602d14;
    puVar8 = (undefined8 *)MBJPLNBCBDB::ODOPDBNIJNO(&uStack_f8,*(longlong *)(param_1 + 0x90),0);
    uVar1 = *puVar8;
    puVar5 = (undefined8 *)puVar8[1];
    uStack_e8 = puVar8[2];
    if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_f8 = uVar1;
    puStack_f0 = puVar5;
    fVar14 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_f8,0);
    if (*(char *)(param_1 + 0x165) == '\0') {
      fVar15 = 1.0;
    }
    else {
      fVar15 = -1.0;
    }
    fVar14 = fVar14 * fVar15;
  }
  *(float *)(param_1 + 0x2d8) = fVar14;
  if (param_2 == '\0') {
    if (*(char *)(param_1 + 0x380) == '\0') {
      if (cRam000000018ab6306a == '\0') {
        sub_8032f6d0(0x187b9ead8);
        cRam000000018ab6306a = '\x01';
      }
      if (*(longlong *)(param_1 + 0x388) == 0) goto code_r0x000181602d14;
      iVar10 = *(int *)(*(longlong *)(param_1 + 0x388) + 0x18);
      bVar11 = 0 < iVar10;
      uStack_c4 = (uint)(iVar10 < 1);
      uVar12 = (uint)(iVar10 < 1);
      bVar13 = iVar10 < 1;
    }
    else {
      bVar11 = true;
      uStack_c4 = 0;
      uVar12 = 0;
      bVar13 = false;
    }
  }
  else {
    bVar11 = true;
    uVar12 = 0;
    uStack_c4 = 0;
    bVar13 = false;
  }
  lVar7 = *(longlong *)(param_1 + 0x38);
  uStack_c8 = uVar12;
  if (lVar7 != 0) {
    bVar2 = *(byte *)(lVar7 + 0xe6);
    if (*(longlong *)(lVar7 + 0x90) != 0) {
      sub_855337a0(&uStack_f8,*(longlong *)(lVar7 + 0x90),uRam0000000187bf15a8);
      uStack_98 = uStack_f8;
      puStack_90 = puStack_f0;
      uStack_88 = uStack_e8;
      lStack_80 = lStack_e0;
      uStack_78 = uStack_d8;
      uStack_f8 = 0;
      puStack_f0 = &uStack_98;
code_r0x000181602930:
      cVar6 = sub_85760f30(&uStack_98,uRam0000000187b47c10);
      lVar7 = lStack_80;
      if (cVar6 != '\0') {
        iVar10 = 0;
        do {
          if (lVar7 == 0) goto code_r0x000181602d08;
          lVar9 = *(longlong *)(lVar7 + 0x48);
          if (lVar9 == 0) {
code_r0x000181602d03:
            sub_8032f9d0();
code_r0x000181602d08:
            sub_8032f9d0();
            sub_8032b6b0();
            pcVar4 = (code *)swi(3);
            (*pcVar4)();
            return;
          }
          if (*(int *)(lVar9 + 0x18) <= iVar10) goto code_r0x000181602930;
          lVar9 = sub_849ed2d0(lVar9,iVar10,uRam0000000187b37540);
          if (lVar9 == 0) {
            sub_8032f9d0();
            goto code_r0x000181602d03;
          }
          uVar1 = *(undefined8 *)(lVar9 + 0x18);
          if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
            sub_8032fa70();
          }
          cVar6 = Object::op_Inequality(uVar1,0,0);
          if (cVar6 != '\0') {
            uVar1 = *(undefined8 *)(lVar9 + 0x18);
            if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
              sub_8032fa70();
            }
            ExtensionMethods::SetActive(uVar1,bVar2 & bVar11,0);
            uVar1 = *(undefined8 *)(lVar9 + 0x10);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar6 = Object::op_Inequality(uVar1,0,0);
            if (cVar6 != '\0') {
              uVar1 = *(undefined8 *)(lVar9 + 0x10);
              if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                sub_8032fa70();
              }
              ExtensionMethods::SetActive(uVar1,bVar13 & bVar2,0);
            }
          }
          iVar10 = iVar10 + 1;
        } while( true );
      }
      sub_80443e50(&uStack_98,uRam0000000187b47a60);
      lVar7 = *(longlong *)(param_1 + 0x38);
      if (bVar2 == 0) {
        bVar13 = false;
        if (lVar7 == 0) goto code_r0x000181602d14;
      }
      else {
        if (lVar7 == 0) goto code_r0x000181602d14;
        bVar13 = *(char *)(lVar7 + 0x208) != '\0';
        lVar7 = *(longlong *)(lStack_c0 + 0x38);
      }
      if (*(longlong *)(lVar7 + 0x1b8) != 0) {
        sub_849d8f60(&uStack_f8,*(longlong *)(lVar7 + 0x1b8),uRam0000000187b4e120);
        uStack_b8 = uStack_f8;
        puStack_b0 = puStack_f0;
        uStack_a8 = uStack_e8;
        lStack_a0 = lStack_e0;
        uStack_f8 = 0;
        puStack_f0 = &uStack_b8;
        while (cVar6 = sub_8571f880(&uStack_b8,uRam0000000187c26a70), lVar7 = lStack_a0,
              uVar1 = uStack_a8, cVar6 != '\0') {
          if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
            sub_8032fa70();
          }
          ExtensionMethods::SetActive(lVar7,bVar13 & bVar11);
          ExtensionMethods::SetActive(uVar1,(byte)uVar12 & bVar13);
        }
        sub_80443e50(&uStack_b8,uRam0000000187c268b8);
        lVar7 = *(longlong *)(param_1 + 0x38);
        if (lVar7 != 0) {
          if (*(int *)(lVar7 + 0x350) != 0) {
            uVar1 = *(undefined8 *)(lVar7 + 0x360);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar6 = Object::op_Inequality(uVar1,0,0);
            if (cVar6 != '\0') {
              if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x000181602d14;
              uVar1 = *(undefined8 *)(*(longlong *)(param_1 + 0x38) + 0x360);
              if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                sub_8032fa70();
              }
              ExtensionMethods::SetActive(uVar1,bVar2 & bVar11,0);
            }
            if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x000181602d14;
            uVar1 = *(undefined8 *)(*(longlong *)(param_1 + 0x38) + 0x358);
            if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
              sub_8032fa70();
            }
            cVar6 = Object::op_Inequality(uVar1,0,0);
            if (cVar6 != '\0') {
              if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x000181602d14;
              uVar1 = *(undefined8 *)(*(longlong *)(param_1 + 0x38) + 0x358);
              if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
                sub_8032fa70();
              }
              ExtensionMethods::SetActive(uVar1,(byte)uStack_c4 & bVar2,0);
            }
          }
          return;
        }
      }
    }
  }
code_r0x000181602d14:
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitCtrl::SetMotionPause  @ 0x1602d20  size=1 ==== */

void UnitCtrl::SetMotionPause(undefined8 param_1)

{
  code *pcVar1;
  char cVar2;
  undefined8 uVar3;
  longlong lVar4;
  
  if (cRam000000018ab6305f == '\0') {
    sub_8032f6d0(0x187b71e50);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab6305f = '\x01';
  }
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  uVar3 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar2 = Object::op_Equality(uVar3,0,0);
  if (cVar2 == '\0') {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar4 == 0) {
code_r0x000181602e99:
      sub_8032f9d0();
      pcVar1 = (code *)swi(3);
      (*pcVar1)();
      return;
    }
    if (*(longlong *)(lVar4 + 800) != 0) {
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if ((lVar4 == 0) || (lVar4 = *(longlong *)(lVar4 + 800), lVar4 == 0))
      goto code_r0x000181602e99;
      cVar2 = sub_84a98ab0(lVar4,param_1,uRam0000000187b71e50);
      if (cVar2 == '\0') {
        uVar3 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
        if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
          sub_8032fa70();
        }
        cVar2 = Object::op_Inequality(uVar3,0,0);
        if (cVar2 != '\0') {
          lVar4 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
          if (lVar4 == 0) goto code_r0x000181602e99;
          BattleUnitBaseSpineController::Pause(lVar4,0);
        }
      }
    }
  }
  return;
}



/* ==== UnitCtrl::SetMotionResume  @ 0x1602ea0  size=1 ==== */

void UnitCtrl::SetMotionResume(undefined8 param_1)

{
  code *pcVar1;
  undefined8 uVar2;
  char cVar3;
  longlong lVar4;
  
  if (cRam000000018ab6305e == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab6305e = '\x01';
  }
  uVar2 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = Object::op_Inequality(uVar2,0,0);
  if (cVar3 != '\0') {
    lVar4 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
    if (lVar4 == 0) {
      sub_8032f9d0();
      pcVar1 = (code *)swi(3);
      (*pcVar1)();
      return;
    }
    BattleUnitBaseSpineController::Resume(lVar4,0);
  }
  return;
}



/* ==== UnitCtrl::SetOverlapPos  @ 0x1602f30  size=1 ==== */

void UnitCtrl::SetOverlapPos(longlong param_1,float param_2)

{
  code *pcVar1;
  ulonglong uVar2;
  longlong lVar3;
  undefined8 *puVar4;
  float fVar5;
  undefined8 uStack_48;
  undefined4 uStack_40;
  
  lVar3 = UnitCtrl::get_BottomTransform(param_1,0);
  if (lVar3 != 0) {
    lVar3 = Component::get_transform(lVar3,0);
    if (lVar3 != 0) {
      puVar4 = (undefined8 *)Transform::get_localPosition(&uStack_48,lVar3,0);
      uStack_48 = *puVar4;
      fVar5 = (float)uStack_48;
      uVar2 = (ulonglong)uStack_48 >> 0x20;
      uStack_40 = *(undefined4 *)(puVar4 + 1);
      lVar3 = UnitCtrl::get_BottomTransform(param_1,0);
      if (lVar3 != 0) {
        lVar3 = Component::get_transform(lVar3,0);
        if (lVar3 != 0) {
          uStack_48 = CONCAT44((int)uVar2,fVar5 + param_2);
          uStack_40 = 0;
          Transform::set_localPosition(lVar3,&uStack_48,0);
          *(float *)(param_1 + 0x18c) = param_2;
          if (*(longlong *)(param_1 + 0x268) != 0) {
            lVar3 = Component::get_transform(*(longlong *)(param_1 + 0x268),0);
            if (lVar3 != 0) {
              puVar4 = (undefined8 *)Transform::get_localPosition(&uStack_48,lVar3,0);
              uStack_48 = *puVar4;
              fVar5 = (float)uStack_48;
              uStack_40 = *(undefined4 *)(puVar4 + 1);
              if (*(longlong *)(param_1 + 0x268) != 0) {
                lVar3 = Component::get_transform(*(longlong *)(param_1 + 0x268),0);
                if (lVar3 != 0) {
                  uStack_48 = CONCAT44(uStack_48._4_4_,fVar5 + param_2);
                  uStack_40 = 0;
                  Transform::set_localPosition(lVar3,&uStack_48,0);
                  return;
                }
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



/* ==== UnitCtrl::SetRecastTime  @ 0x16030c0  size=1 ==== */

void UnitCtrl::SetRecastTime(longlong param_1,int param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  undefined4 uVar3;
  undefined4 uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  char cVar7;
  undefined4 *puVar8;
  undefined4 uVar9;
  float fVar10;
  undefined8 uVar11;
  undefined8 uStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  
  if (cRam000000018ab63071 == '\0') {
    sub_8032f6d0(0x187c41780);
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab63071 = '\x01';
  }
  if (param_2 != 0) {
    if (param_2 == 1) {
      uVar9 = *(undefined4 *)(param_1 + 0x4b4);
    }
    else {
      if (*(longlong *)(param_1 + 0x478) == 0) goto DAT_18160328e;
      uVar9 = sub_855cef80(*(longlong *)(param_1 + 0x478),param_2,uRam0000000187c41780);
    }
    if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    puVar8 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(&uStack_58,uVar9,0);
    uVar9 = puVar8[1];
    uVar3 = puVar8[2];
    uVar4 = puVar8[3];
    uVar11 = *(undefined8 *)(puVar8 + 4);
    *(undefined4 *)(param_1 + 0x278) = *puVar8;
    *(undefined4 *)(param_1 + 0x27c) = uVar9;
    *(undefined4 *)(param_1 + 0x280) = uVar3;
    *(undefined4 *)(param_1 + 0x284) = uVar4;
    *(undefined8 *)(param_1 + 0x288) = uVar11;
    sub_8032e8d0(param_1 + 0x280,0);
    if (cRam000000018ab63072 == '\0') {
      sub_8032f6d0(0x187b60de8);
      cRam000000018ab63072 = '\x01';
    }
    if (*(longlong *)(param_1 + 0x38) == 0) {
DAT_18160328e:
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
    uVar11 = CLKMBEGHOLA::CDICFGMBBIM(*(longlong *)(param_1 + 0x38),0);
    cVar7 = BattleUtil::Approximately(uVar11,0,0);
    if (cVar7 == '\0') {
      uVar5 = *(undefined8 *)(param_1 + 0x278);
      uVar6 = *(undefined8 *)(param_1 + 0x280);
      uVar1 = *(undefined8 *)(param_1 + 0x288);
      if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_58 = uVar5;
      uStack_50 = uVar6;
      uStack_48 = uVar1;
      fVar10 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_58,0);
      puVar8 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(&uStack_58,fVar10 / (float)uVar11,0);
    }
    else {
      if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      puVar8 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(&uStack_58,0x42b40000,0);
    }
    uVar9 = puVar8[1];
    uVar3 = puVar8[2];
    uVar4 = puVar8[3];
    uVar11 = *(undefined8 *)(puVar8 + 4);
    *(undefined4 *)(param_1 + 0x278) = *puVar8;
    *(undefined4 *)(param_1 + 0x27c) = uVar9;
    *(undefined4 *)(param_1 + 0x280) = uVar3;
    *(undefined4 *)(param_1 + 0x284) = uVar4;
    *(undefined8 *)(param_1 + 0x288) = uVar11;
    sub_8032e8d0(param_1 + 0x280,0);
    UnitCtrl::updateActionWaitTimerMax(param_1,0);
  }
  return;
}



/* ==== UnitCtrl::SetSpineSortOrder  @ 0x16032a0  size=1 ==== */

void UnitCtrl::SetSpineSortOrder(longlong param_1,int param_2)

{
  code *pcVar1;
  char cVar2;
  longlong lVar3;
  longlong lVar4;
  int iVar5;
  
  if (cRam000000018ab63074 == '\0') {
    sub_8032f6d0(0x187c2e908);
    sub_8032f6d0(0x187c2eac0);
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab63074 = '\x01';
  }
  lVar3 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar2 = Object::op_Inequality(lVar3,0,0);
  if (cVar2 != '\0') {
    if (lVar3 == 0) goto code_r0x0001816033c0;
    SpineController::set_Depth(lVar3,param_2,0);
  }
  lVar3 = *(longlong *)(param_1 + 0x130);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar2 = Object::op_Inequality(lVar3,0,0);
  if (cVar2 == '\0') {
    return;
  }
  if (lVar3 != 0) {
    lVar3 = *(longlong *)(lVar3 + 0x38);
    iVar5 = 0;
    if (lVar3 != 0) {
      while( true ) {
        if (*(int *)(lVar3 + 0x18) <= iVar5) {
          return;
        }
        lVar4 = sub_849ed2d0(lVar3,iVar5,uRam0000000187c2eac0);
        if ((lVar4 == 0) || (lVar4 = SkeletonPartsRenderer::get_MeshRenderer(lVar4,0), lVar4 == 0)) break;
        Renderer::set_sortingOrder(lVar4,param_2,0);
        iVar5 = iVar5 + 1;
        param_2 = param_2 + 2;
      }
    }
  }
code_r0x0001816033c0:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitCtrl::StartShake  @ 0x16033d0  size=1 ==== */

void UnitCtrl::StartShake(undefined8 param_1,undefined8 param_2,undefined8 param_3,undefined8 param_4)

{
  longlong lVar1;
  code *pcVar2;
  
  if (cRam000000018ab6306f == '\0') {
    sub_8032f6d0(0x187b8ac68);
    cRam000000018ab6306f = '\x01';
  }
  if (cRam000000018ab6307c == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307c = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x10);
  if (lVar1 != 0) {
    sub_80012050(1,uRam0000000187b8ac68,lVar1,param_2,param_3,param_4);
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::StaticRelease  @ 0x1603470  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001816034b5: Changing call to branch */
/* WARNING: Possible PIC construction at 0x00018160350b: Changing call to branch */
/* WARNING: Possible PIC construction at 0x000181603561: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x000181603510) */
/* WARNING: Removing unreachable block (ram,0x0001816034ba) */
/* WARNING: Removing unreachable block (ram,0x000181603566) */

ulonglong UnitCtrl::StaticRelease(void)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  if (cRam000000018ab63046 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63046 = '\x01';
  }
  uVar3 = uRam0000000187bfb7a8;
  **(undefined8 **)(uRam0000000187bfb7a8 + 0xb8) = 0;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(*(ulonglong *)(uRam0000000187bfb7a8 + 0xb8) >> 0xc);
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



/* ==== UnitCtrl::StopZoomEffect  @ 0x16035a0  size=1 ==== */

void UnitCtrl::StopZoomEffect(undefined8 param_1)

{
  longlong *plVar1;
  longlong lVar2;
  code *pcVar3;
  undefined8 *puVar4;
  ushort uVar5;
  
  if (cRam000000018ab6306e == '\0') {
    sub_8032f6d0(0x187b8ac68);
    cRam000000018ab6306e = '\x01';
  }
  if (cRam000000018ab6307c == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307c = '\x01';
  }
  plVar1 = *(longlong **)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x10);
  if (plVar1 == (longlong *)0x0) {
    sub_8032f9d0();
    pcVar3 = (code *)swi(3);
    (*pcVar3)();
    return;
  }
  lVar2 = *plVar1;
  uVar5 = 0;
  if (*(ushort *)(lVar2 + 0x12e) != 0) {
    do {
      if (*(longlong *)(*(longlong *)(lVar2 + 0xb0) + (ulonglong)uVar5 * 0x10) ==
          lRam0000000187b8ac68) {
        puVar4 = (undefined8 *)
                 ((longlong)
                  (*(int *)(*(longlong *)(lVar2 + 0xb0) + 8 + (ulonglong)uVar5 * 0x10) + 4) * 0x10 +
                  0x138 + lVar2);
        goto code_r0x000181603645;
      }
      uVar5 = uVar5 + 1;
    } while (uVar5 < *(ushort *)(lVar2 + 0x12e));
  }
  puVar4 = (undefined8 *)sub_8032c930(plVar1,lRam0000000187b8ac68,4);
code_r0x000181603645:
                    /* WARNING: Could not recover jumptable at 0x000181603659. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  (*(code *)*puVar4)(plVar1,param_1,puVar4[1]);
  return;
}



/* ==== UnitCtrl::UpdateLastDamageVoiceTime  @ 0x1603860  size=1 ==== */

void UnitCtrl::UpdateLastDamageVoiceTime(void)

{
  longlong lVar1;
  code *pcVar2;
  undefined4 uVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  uVar3 = Time::get_time(0);
  if (lVar1 != 0) {
    *(undefined4 *)(lVar1 + 0x360) = uVar3;
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::UpdateLastRetireVoiceTime  @ 0x16038c0  size=1 ==== */

void UnitCtrl::UpdateLastRetireVoiceTime(void)

{
  longlong lVar1;
  code *pcVar2;
  undefined4 uVar3;
  
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar1 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  uVar3 = Time::get_time(0);
  if (lVar1 != 0) {
    *(undefined4 *)(lVar1 + 0x35c) = uVar3;
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::UpdateSummon  @ 0x1603920  size=1 ==== */

longlong UnitCtrl::UpdateSummon(undefined8 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                      undefined8 *param_5,undefined4 param_6)

{
  undefined4 uVar1;
  code *pcVar2;
  longlong lVar3;
  
  if (cRam000000018ab63061 == '\0') {
    sub_8032f6d0(0x187c22da0);
    cRam000000018ab63061 = '\x01';
  }
  lVar3 = sub_8032f970(uRam0000000187c22da0);
  sub_80445940(lVar3,0,0);
  if (lVar3 != 0) {
    *(undefined8 *)(lVar3 + 0x20) = param_1;
    sub_8032e8d0(lVar3 + 0x20,param_1);
    *(undefined4 *)(lVar3 + 0x28) = param_3;
    uVar1 = *(undefined4 *)(param_5 + 1);
    *(undefined8 *)(lVar3 + 0x34) = *param_5;
    *(undefined4 *)(lVar3 + 0x3c) = uVar1;
    *(undefined4 *)(lVar3 + 0x2c) = param_4;
    *(undefined4 *)(lVar3 + 0x40) = param_6;
    *(undefined4 *)(lVar3 + 0x30) = param_2;
    return lVar3;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  lVar3 = (*pcVar2)();
  return lVar3;
}



/* ==== UnitCtrl::WaveStartProcess  @ 0x16039e0  size=1 ==== */

void UnitCtrl::WaveStartProcess(longlong param_1,undefined1 param_2)

{
  undefined8 uVar1;
  int iVar2;
  longlong lVar3;
  code *pcVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  char cVar7;
  undefined4 *puVar8;
  undefined4 uVar9;
  undefined8 uStack_98;
  undefined8 uStack_90;
  longlong lStack_88;
  undefined4 uStack_80;
  undefined4 uStack_7c;
  undefined4 uStack_78;
  undefined4 uStack_74;
  longlong lStack_70;
  longlong lStack_68;
  undefined8 uStack_60;
  undefined1 auStack_58 [16];
  longlong lStack_48;
  longlong lStack_40;
  undefined8 uStack_38;
  
  if (cRam000000018ab63051 == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187b3c660);
    sub_8032f6d0(0x187be6258);
    sub_8032f6d0(0x187c2ab28);
    sub_8032f6d0(0x187c2acd8);
    sub_8032f6d0(0x187be6410);
    sub_8032f6d0(0x187c2ae90);
    sub_8032f6d0(0x187be65c8);
    sub_8032f6d0(0x187bc7278);
    sub_8032f6d0(0x187b5f2e0);
    sub_8032f6d0(0x187bb2838);
    cRam000000018ab63051 = '\x01';
  }
  uStack_98 = 0;
  uStack_90 = 0;
  lStack_88 = 0;
  if (*(longlong *)(param_1 + 0x88) != 0) {
    CIFIMDJAGGF::JHDLODFKJAG(*(longlong *)(param_1 + 0x88),param_2,0);
    if (*(longlong *)(param_1 + 0x98) != 0) {
      PBLMDEFAGBK::KFFNNIPJJFA(*(longlong *)(param_1 + 0x98),param_2,1,0);
      lVar3 = *(longlong *)(param_1 + 0x88);
      if (lVar3 != 0) {
        *(undefined4 *)(lVar3 + 0x830) = *(undefined4 *)(lVar3 + 0x448);
        *(undefined4 *)(lVar3 + 0x834) = *(undefined4 *)(lVar3 + 0x44c);
        *(undefined4 *)(lVar3 + 0x838) = *(undefined4 *)(lVar3 + 0x450);
        *(undefined4 *)(lVar3 + 0x83c) = *(undefined4 *)(lVar3 + 0x454);
        *(undefined8 *)(lVar3 + 0x840) = *(undefined8 *)(lVar3 + 0x458);
        if (*(longlong *)(param_1 + 0x40) != 0) {
          MGAEMKEEHND::GGOENIJIPHC(*(longlong *)(param_1 + 0x40),0);
          if (cRam000000018ab63052 == '\0') {
            sub_8032f6d0(0x187b60de8);
            cRam000000018ab63052 = '\x01';
          }
          if (cRam000000018ab63077 == '\0') {
            sub_8032f6d0(0x187bfb7a8);
            cRam000000018ab63077 = '\x01';
          }
          lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
          if (lVar3 != 0) {
            iVar2 = *(int *)(lVar3 + 0x2c0);
            if (iVar2 == 5) {
              if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uVar9 = 0x42b40000;
            }
            else if (iVar2 == 6) {
              if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
                sub_8032fa70();
              }
              uVar9 = 0x3e99999a;
            }
            else {
              if (cRam000000018ab63077 == '\0') {
                sub_8032f6d0(0x187bfb7a8);
                cRam000000018ab63077 = '\x01';
              }
              lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
              if (lVar3 == 0) goto code_r0x000181603eb1;
              if (*(int *)(lVar3 + 0x22c) == 0) {
                uVar9 = 0x40200000;
              }
              else {
                uVar9 = 0x3e99999a;
              }
              if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
                sub_8032fa70();
              }
            }
            puVar8 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(&uStack_80,uVar9,0);
            uVar1 = *(undefined8 *)(puVar8 + 4);
            uVar9 = puVar8[1];
            uVar5 = puVar8[2];
            uVar6 = puVar8[3];
            *(undefined4 *)(param_1 + 0x278) = *puVar8;
            *(undefined4 *)(param_1 + 0x27c) = uVar9;
            *(undefined4 *)(param_1 + 0x280) = uVar5;
            *(undefined4 *)(param_1 + 0x284) = uVar6;
            *(undefined8 *)(param_1 + 0x288) = uVar1;
            sub_8032e8d0(param_1 + 0x280,0);
            UnitCtrl::updateActionWaitTimerMax(param_1,0);
            if (*(char *)(param_1 + 0x178) == '\0') {
              if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
                sub_8032fa70(lRam0000000187b41650);
              }
              lVar3 = *(longlong *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 8);
              if (lVar3 == 0) goto code_r0x000181603eb1;
              uVar9 = sub_84a7fb80(lVar3,*(undefined4 *)(param_1 + 0x168),uRam0000000187bb2838);
              UnitCtrl::ResetPosForUserUnit(param_1,uVar9,0);
            }
            else {
              UnitCtrl::resetPosForEnemyUnit(param_1,*(undefined4 *)(param_1 + 0x168),0);
            }
            if (*(longlong *)(param_1 + 0xd0) != 0) {
              DEBLMMJMGID::GOJOLLDNLFD(*(longlong *)(param_1 + 0xd0),0);
              if (*(longlong *)(param_1 + 0xa0) != 0) {
                UnitComponentRelicSkill::ResetExecedFlag(*(longlong *)(param_1 + 0xa0),0);
                if ((*(longlong *)(param_1 + 0x98) != 0) &&
                   (lVar3 = *(longlong *)(*(longlong *)(param_1 + 0x98) + 0x30), lVar3 != 0)) {
                  sub_855337a0(&uStack_80,lVar3,uRam0000000187b3c660);
                  lStack_48 = lStack_70;
                  lStack_40 = lStack_68;
                  uStack_38 = uStack_60;
                  while (cVar7 = sub_85760f30(auStack_58,uRam0000000187be6410), cVar7 != '\0') {
                    if (lStack_40 == 0) goto code_r0x000181603ec2;
                    sub_849e2810(&uStack_80,lStack_40,uRam0000000187b5f2e0);
                    uStack_98 = CONCAT44(uStack_7c,uStack_80);
                    uStack_90 = CONCAT44(uStack_74,uStack_78);
                    lStack_88 = lStack_70;
                    while (cVar7 = sub_8572bc70(&uStack_98,uRam0000000187c2acd8), cVar7 != '\0') {
                      if (lStack_88 == 0) goto code_r0x000181603eb7;
                      AJDJDPHBJPE::KCJGOCEOMOE(lStack_88,param_1,0);
                    }
                    sub_80443e50(&uStack_98,uRam0000000187c2ab28);
                  }
                  sub_80443e50(auStack_58,uRam0000000187be6258);
                  if (*(longlong *)(param_1 + 0x28) != 0) {
                    UnitActionController::ExecActionOnWaveStart(*(longlong *)(param_1 + 0x28),0);
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
code_r0x000181603eb1:
  sub_8032f9d0();
code_r0x000181603eb7:
  sub_8032f9d0();
  sub_8032b6b0();
code_r0x000181603ec2:
  sub_8032f9d0();
  sub_8032b6b0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitCtrl::_Update  @ 0x1603ed0  size=1 ==== */

void UnitCtrl::_Update(longlong param_1)

{
  undefined4 uVar1;
  longlong lVar2;
  longlong lVar3;
  code *pcVar4;
  int iVar5;
  undefined8 uVar6;
  bool bVar7;
  
  if (*(char *)(param_1 + 0x164) == '\0') {
    lVar2 = *(longlong *)(param_1 + 0xd0);
    if (lVar2 != 0) {
      if ((*(int *)(lVar2 + 0xb4) == 0) && (*(char *)(lVar2 + 0xb8) == '\0')) {
        bVar7 = *(char *)(lVar2 + 0xbc) == '\0';
      }
      else {
        bVar7 = false;
      }
      lVar2 = *(longlong *)(param_1 + 0x98);
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (((lVar3 != 0) && (*(longlong *)(param_1 + 0xd0) != 0)) && (lVar2 != 0)) {
        PBLMDEFAGBK::NOLPDDNEAFF(lVar2,*(undefined4 *)(lVar3 + 400),bVar7,
                      *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0xb9),0);
        lVar2 = *(longlong *)(param_1 + 0xd8);
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar3 != 0) {
          uVar1 = *(undefined4 *)(lVar3 + 0x2f0);
          if (cRam000000018ab63077 == '\0') {
            sub_8032f6d0(0x187bfb7a8);
            cRam000000018ab63077 = '\x01';
          }
          lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
          if ((lVar3 != 0) && (iVar5 = BattleManager::GetBlackOutUnitLength(lVar3,0), lVar2 != 0)) {
            MBNAOPKFJMN::ANFADOOIHFI(lVar2,uVar1,iVar5 != 0,0);
            lVar2 = *(longlong *)(param_1 + 0x70);
            if (cRam000000018ab63077 == '\0') {
              sub_8032f6d0(0x187bfb7a8);
              cRam000000018ab63077 = '\x01';
            }
            lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
            if ((lVar3 != 0) && (lVar2 != 0)) {
              DBGNMELIABG::OPAEHEDJBBA(lVar2,*(undefined4 *)(lVar3 + 0x2f0),0);
              return;
            }
          }
        }
      }
    }
  }
  else {
    if (cRam000000018ab63049 == '\0') {
      sub_8032f6d0(0x187b5e978);
      cRam000000018ab63049 = '\x01';
    }
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar2 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar2 != 0) {
      BattleManager::RemoveCoroutine(lVar2,param_1,0);
      uVar6 = Component::get_gameObject(param_1,0);
      if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
        sub_8032fa70();
      }
      Object::Destroy(uVar6,0);
      return;
    }
  }
  sub_8032f9d0();
  pcVar4 = (code *)swi(3);
  (*pcVar4)();
  return;
}



/* ==== UnitCtrl::get_ActionsTargetOnMe  @ 0x4b1a80  size=8 ==== */

undefined8 UnitCtrl::get_ActionsTargetOnMe(longlong param_1)

{
  return *(undefined8 *)(param_1 + 400);
}



/* ==== UnitCtrl::get_AnimeIdDictionary  @ 0x8cb7c0  size=8 ==== */

undefined8 UnitCtrl::get_AnimeIdDictionary(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x508);
}



/* ==== UnitCtrl::get_AtkRecastTime  @ 0x1605690  size=9 ==== */

undefined4 UnitCtrl::get_AtkRecastTime(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4b4);
}



/* ==== UnitCtrl::get_AtkType  @ 0x16056a0  size=7 ==== */

undefined4 UnitCtrl::get_AtkType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4b0);
}



/* ==== UnitCtrl::get_AuraEffectList  @ 0x572f60  size=8 ==== */

undefined8 UnitCtrl::get_AuraEffectList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x108);
}



/* ==== UnitCtrl::get_BaseX  @ 0x16056b0  size=9 ==== */

undefined4 UnitCtrl::get_BaseX(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x260);
}



/* ==== UnitCtrl::get_BattleRarity  @ 0x16056c0  size=7 ==== */

undefined4 UnitCtrl::get_BattleRarity(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4a0);
}



/* ==== UnitCtrl::get_BodyWidth  @ 0x7ceec0  size=9 ==== */

undefined4 UnitCtrl::get_BodyWidth(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x118);
}



/* ==== UnitCtrl::get_BonusEnemyType  @ 0x564e80  size=7 ==== */

undefined4 UnitCtrl::get_BonusEnemyType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x17c);
}



/* ==== UnitCtrl::get_BottomTransform  @ 0x16056d0  size=1 ==== */

undefined8 UnitCtrl::get_BottomTransform(longlong param_1)

{
  code *pcVar1;
  char cVar2;
  undefined8 uVar3;
  longlong lVar4;
  
  if (cRam000000018ab63045 == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab63045 = '\x01';
  }
  uVar3 = *(undefined8 *)(param_1 + 0x458);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar2 = Object::op_Equality(uVar3,0,0);
  if (cVar2 != '\0') {
    uVar3 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar2 = Object::op_Equality(uVar3,0,0);
    if (cVar2 == '\0') {
      lVar4 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
      if (lVar4 == 0) {
        sub_8032f9d0();
        pcVar1 = (code *)swi(3);
        uVar3 = (*pcVar1)();
        return uVar3;
      }
      uVar3 = Component::get_transform(lVar4,0);
    }
    else {
      uVar3 = 0;
    }
    *(undefined8 *)(param_1 + 0x458) = uVar3;
    sub_8032e8d0(param_1 + 0x458,uVar3);
  }
  return *(undefined8 *)(param_1 + 0x458);
}



/* ==== UnitCtrl::get_CastTimer  @ 0x16057b0  size=27 ==== */

undefined8 * UnitCtrl::get_CastTimer(undefined8 *param_1,longlong param_2)

{
  undefined8 uVar1;
  undefined8 uVar2;
  
  uVar2 = *(undefined8 *)(param_2 + 0x280);
  uVar1 = *(undefined8 *)(param_2 + 0x288);
  *param_1 = *(undefined8 *)(param_2 + 0x278);
  param_1[1] = uVar2;
  param_1[2] = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_CenterBone  @ 0x56fe20  size=8 ==== */

undefined8 UnitCtrl::get_CenterBone(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xf0);
}



/* ==== UnitCtrl::get_CenterBoneModeChange  @ 0x56fe10  size=8 ==== */

undefined8 UnitCtrl::get_CenterBoneModeChange(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xf8);
}



/* ==== UnitCtrl::get_CenterForDamageNumBone  @ 0x8db760  size=8 ==== */

undefined8 UnitCtrl::get_CenterForDamageNumBone(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x1f8);
}



/* ==== UnitCtrl::get_CharacterUnitId  @ 0x16057d0  size=7 ==== */

undefined4 UnitCtrl::get_CharacterUnitId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x48c);
}



/* ==== UnitCtrl::get_CircleEffectList  @ 0x56fde0  size=8 ==== */

undefined8 UnitCtrl::get_CircleEffectList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x148);
}



/* ==== UnitCtrl::get_ColliderCenter  @ 0x16057e0  size=25 ==== */

undefined8 * UnitCtrl::get_ColliderCenter(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x220);
  *param_1 = *(undefined8 *)(param_2 + 0x218);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_ColliderSize  @ 0x1605800  size=25 ==== */

undefined8 * UnitCtrl::get_ColliderSize(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x22c);
  *param_1 = *(undefined8 *)(param_2 + 0x224);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_ComponentAbnormal  @ 0x4377a0  size=5 ==== */

undefined8 UnitCtrl::get_ComponentAbnormal(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x38);
}



/* ==== UnitCtrl::get_ComponentActionPattern  @ 0x437710  size=5 ==== */

undefined8 UnitCtrl::get_ComponentActionPattern(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x40);
}



/* ==== UnitCtrl::get_ComponentAnimation  @ 0x437720  size=5 ==== */

undefined8 UnitCtrl::get_ComponentAnimation(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x48);
}



/* ==== UnitCtrl::get_ComponentAnnihilation  @ 0x437740  size=5 ==== */

undefined8 UnitCtrl::get_ComponentAnnihilation(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x50);
}



/* ==== UnitCtrl::get_ComponentBattleResult  @ 0x437730  size=5 ==== */

undefined8 UnitCtrl::get_ComponentBattleResult(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x58);
}



/* ==== UnitCtrl::get_ComponentColor  @ 0x438890  size=5 ==== */

undefined8 UnitCtrl::get_ComponentColor(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x60);
}



/* ==== UnitCtrl::get_ComponentCompare  @ 0x4388b0  size=5 ==== */

undefined8 UnitCtrl::get_ComponentCompare(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x68);
}



/* ==== UnitCtrl::get_ComponentDamageControl  @ 0x4388a0  size=5 ==== */

undefined8 UnitCtrl::get_ComponentDamageControl(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x70);
}



/* ==== UnitCtrl::get_ComponentLog  @ 0x438ae0  size=5 ==== */

undefined8 UnitCtrl::get_ComponentLog(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x78);
}



/* ==== UnitCtrl::get_ComponentMultiPartsBoss  @ 0x438c30  size=8 ==== */

undefined8 UnitCtrl::get_ComponentMultiPartsBoss(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x80);
}



/* ==== UnitCtrl::get_ComponentParameter  @ 0x438c20  size=8 ==== */

undefined8 UnitCtrl::get_ComponentParameter(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x88);
}



/* ==== UnitCtrl::get_ComponentParameterClamped  @ 0x438ab0  size=8 ==== */

undefined8 UnitCtrl::get_ComponentParameterClamped(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x90);
}



/* ==== UnitCtrl::get_ComponentPassiveSkill  @ 0x438c10  size=8 ==== */

undefined8 UnitCtrl::get_ComponentPassiveSkill(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x98);
}



/* ==== UnitCtrl::get_ComponentRelicPassiveSkill  @ 0x44ad50  size=8 ==== */

undefined8 UnitCtrl::get_ComponentRelicPassiveSkill(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xa8);
}



/* ==== UnitCtrl::get_ComponentRelicSkill  @ 0x438c50  size=8 ==== */

undefined8 UnitCtrl::get_ComponentRelicSkill(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xa0);
}



/* ==== UnitCtrl::get_ComponentScaleChange  @ 0x44ad60  size=8 ==== */

undefined8 UnitCtrl::get_ComponentScaleChange(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xb0);
}



/* ==== UnitCtrl::get_ComponentSearchArea  @ 0x69d930  size=8 ==== */

undefined8 UnitCtrl::get_ComponentSearchArea(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xb8);
}



/* ==== UnitCtrl::get_ComponentSortOrder  @ 0x438c00  size=8 ==== */

undefined8 UnitCtrl::get_ComponentSortOrder(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xc0);
}



/* ==== UnitCtrl::get_ComponentSound  @ 0x438af0  size=8 ==== */

undefined8 UnitCtrl::get_ComponentSound(longlong param_1)

{
  return *(undefined8 *)(param_1 + 200);
}



/* ==== UnitCtrl::get_ComponentState  @ 0x597ca0  size=8 ==== */

undefined8 UnitCtrl::get_ComponentState(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xd0);
}



/* ==== UnitCtrl::get_ComponentUnionBurst  @ 0x5d3fb0  size=8 ==== */

undefined8 UnitCtrl::get_ComponentUnionBurst(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xd8);
}



/* ==== UnitCtrl::get_ConnectRank  @ 0x1605830  size=7 ==== */

undefined4 UnitCtrl::get_ConnectRank(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4a8);
}



/* ==== UnitCtrl::get_ConnectRankLimit  @ 0x1605820  size=7 ==== */

undefined4 UnitCtrl::get_ConnectRankLimit(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4ac);
}



/* ==== UnitCtrl::get_CurrentSkillId  @ 0x1605840  size=7 ==== */

undefined4 UnitCtrl::get_CurrentSkillId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x230);
}



/* ==== UnitCtrl::get_CurrentSpineSpeedCoroutineId  @ 0x1605850  size=7 ==== */

undefined4 UnitCtrl::get_CurrentSpineSpeedCoroutineId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x1e4);
}



/* ==== UnitCtrl::get_CutInFrameSet  @ 0x579d80  size=8 ==== */

undefined8 UnitCtrl::get_CutInFrameSet(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x1c0);
}



/* ==== UnitCtrl::get_DamagedHpEffectDictionary  @ 0x483ce0  size=8 ==== */

undefined8 UnitCtrl::get_DamagedHpEffectDictionary(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x110);
}



/* ==== UnitCtrl::get_DeltaTimeForPause  @ 0x1605860  size=1 ==== */

ulonglong UnitCtrl::get_DeltaTimeForPause(longlong param_1)

{
  undefined8 uVar1;
  code *pcVar2;
  char cVar3;
  longlong lVar4;
  ulonglong uVar5;
  
  if (cRam000000018ab6303c == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab6303c = '\x01';
  }
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  uVar1 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = Object::op_Equality(uVar1,0,0);
  if (cVar3 != '\0') {
    return 0;
  }
  if ((*(longlong *)(param_1 + 0xd8) == 0) ||
     (*(char *)(*(longlong *)(param_1 + 0xd8) + 0x2a) == '\0')) {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar4 == 0) goto code_r0x000181605998;
    if (*(char *)(lVar4 + 0x381) != '\0') {
      return 0;
    }
    if (*(char *)(param_1 + 0x460) != '\0') {
      return 0;
    }
    lVar4 = sub_815f21c0(param_1,0);
  }
  else {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  }
  if (lVar4 != 0) {
    return (ulonglong)*(uint *)(lVar4 + 0x2f0);
  }
code_r0x000181605998:
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar5 = (*pcVar2)();
  return uVar5;
}



/* ==== UnitCtrl::get_DisableSortOrderFrontOnBlackoutTarget  @ 0x16059a0  size=8 ==== */

undefined1 UnitCtrl::get_DisableSortOrderFrontOnBlackoutTarget(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x250);
}



/* ==== UnitCtrl::get_EffectDifficulty  @ 0x16059b0  size=7 ==== */

undefined4 UnitCtrl::get_EffectDifficulty(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x2e0);
}



/* ==== UnitCtrl::get_EffectSpineControllerList  @ 0x8db7d0  size=8 ==== */

undefined8 UnitCtrl::get_EffectSpineControllerList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 600);
}



/* ==== UnitCtrl::get_FirearmCtrlsOnMe  @ 0x565300  size=8 ==== */

undefined8 UnitCtrl::get_FirearmCtrlsOnMe(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x198);
}



/* ==== UnitCtrl::get_FixedCenterForDamageNumPos  @ 0x16059c0  size=1 ==== */

uint * UnitCtrl::get_FixedCenterForDamageNumPos(uint *param_1,longlong param_2)

{
  uint uVar1;
  code *pcVar2;
  uint *puVar3;
  uint uVar4;
  
  if (*(char *)(param_2 + 500) == '\0') {
    if ((*(char *)(param_2 + 0x165) == '\0') && (*(char *)(param_2 + 0x380) == '\0')) {
      if (cRam000000018ab6306a == '\0') {
        sub_8032f6d0(0x187b9ead8);
        cRam000000018ab6306a = '\x01';
      }
      if (*(longlong *)(param_2 + 0x388) == 0) goto code_r0x000181605aeb;
      if (*(int *)(*(longlong *)(param_2 + 0x388) + 0x18) < 1) {
        uVar4 = (uint)*(undefined8 *)(param_2 + 0x1e8);
        goto code_r0x000181605abe;
      }
    }
    uVar4 = (uint)*(undefined8 *)(param_2 + 0x1e8) ^ 0x80000000;
code_r0x000181605abe:
    uVar1 = *(uint *)(param_2 + 0x1f0);
    *(ulonglong *)param_1 = CONCAT44(*(undefined4 *)(param_2 + 0x1ec),uVar4);
    param_1[2] = uVar1;
    return param_1;
  }
  if ((*(char *)(param_2 + 0x165) == '\0') && (*(char *)(param_2 + 0x380) == '\0')) {
    if (cRam000000018ab6306a == '\0') {
      sub_8032f6d0(0x187b9ead8);
      cRam000000018ab6306a = '\x01';
    }
    if (*(longlong *)(param_2 + 0x388) == 0) {
code_r0x000181605aeb:
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      puVar3 = (uint *)(*pcVar2)();
      return puVar3;
    }
    if (*(int *)(*(longlong *)(param_2 + 0x388) + 0x18) < 1) {
      uVar4 = (uint)*(undefined8 *)(param_2 + 0x200);
      goto code_r0x000181605a3d;
    }
  }
  uVar4 = (uint)*(undefined8 *)(param_2 + 0x200) ^ 0x80000000;
code_r0x000181605a3d:
  param_1[1] = *(uint *)(param_2 + 0x204);
  param_1[2] = *(uint *)(param_2 + 0x208);
  *param_1 = uVar4;
  return param_1;
}



/* ==== UnitCtrl::get_FixedCenterPos  @ 0x1605b00  size=1 ==== */

uint * UnitCtrl::get_FixedCenterPos(uint *param_1,longlong param_2)

{
  code *pcVar1;
  uint *puVar2;
  uint uVar3;
  
  if ((*(char *)(param_2 + 0x165) == '\0') && (*(char *)(param_2 + 0x380) == '\0')) {
    if (cRam000000018ab6306a == '\0') {
      sub_8032f6d0(0x187b9ead8);
      cRam000000018ab6306a = '\x01';
    }
    if (*(longlong *)(param_2 + 0x388) == 0) {
      sub_8032f9d0();
      pcVar1 = (code *)swi(3);
      puVar2 = (uint *)(*pcVar1)();
      return puVar2;
    }
    if (*(int *)(*(longlong *)(param_2 + 0x388) + 0x18) < 1) {
      uVar3 = (uint)*(undefined8 *)(param_2 + 0x1e8);
      goto code_r0x000181605b6c;
    }
  }
  uVar3 = (uint)*(undefined8 *)(param_2 + 0x1e8) ^ 0x80000000;
code_r0x000181605b6c:
  param_1[1] = *(uint *)(param_2 + 0x1ec);
  param_1[2] = *(uint *)(param_2 + 0x1f0);
  *param_1 = uVar3;
  return param_1;
}



/* ==== UnitCtrl::get_FixedStatePos  @ 0x1605ba0  size=25 ==== */

undefined8 * UnitCtrl::get_FixedStatePos(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x214);
  *param_1 = *(undefined8 *)(param_2 + 0x20c);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_HasCenterForDamageNumBone  @ 0x1605bc0  size=8 ==== */

undefined1 UnitCtrl::get_HasCenterForDamageNumBone(longlong param_1)

{
  return *(undefined1 *)(param_1 + 500);
}



/* ==== UnitCtrl::get_HasDieLoop  @ 0x1605bd0  size=1 ==== */

undefined1 UnitCtrl::get_HasDieLoop(undefined8 param_1)

{
  code *pcVar1;
  undefined8 uVar2;
  char cVar3;
  undefined1 uVar4;
  longlong lVar5;
  
  if (cRam000000018ab6303e == '\0') {
    sub_8032f6d0(0x187b5e978);
    cRam000000018ab6303e = '\x01';
  }
  uVar2 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
  if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
    sub_8032fa70();
  }
  cVar3 = Object::op_Equality(uVar2,0,0);
  if (cVar3 != '\0') {
    return 0;
  }
  lVar5 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
  if (lVar5 != 0) {
    uVar4 = BattleUnitBaseSpineController::IsAnimation(lVar5,0x28,0xffffffff,0xffffffff,0xffffffff,0);
    return uVar4;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar4 = (*pcVar1)();
  return uVar4;
}



/* ==== UnitCtrl::get_HasSpecialDie  @ 0xe257b0  size=8 ==== */

undefined1 UnitCtrl::get_HasSpecialDie(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x240);
}



/* ==== UnitCtrl::get_IconSkin  @ 0x1605c80  size=7 ==== */

undefined4 UnitCtrl::get_IconSkin(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x498);
}



/* ==== UnitCtrl::get_IdentifyNum  @ 0x1605c90  size=7 ==== */

undefined4 UnitCtrl::get_IdentifyNum(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x484);
}



/* ==== UnitCtrl::get_Index  @ 0x1605ca0  size=7 ==== */

undefined4 UnitCtrl::get_Index(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x480);
}



/* ==== UnitCtrl::get_IsAbyssBossEnemy  @ 0x1605cb0  size=8 ==== */

undefined1 UnitCtrl::get_IsAbyssBossEnemy(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x1ab);
}



/* ==== UnitCtrl::get_IsAwakeMotion  @ 0x1605cc0  size=8 ==== */

undefined1 UnitCtrl::get_IsAwakeMotion(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x2dc);
}



/* ==== UnitCtrl::get_IsBonusEnemy  @ 0x1605cd0  size=11 ==== */

bool UnitCtrl::get_IsBonusEnemy(longlong param_1)

{
  return *(int *)(param_1 + 0x17c) != 0;
}



/* ==== UnitCtrl::get_IsBoss  @ 0x777960  size=8 ==== */

undefined1 UnitCtrl::get_IsBoss(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x1a8);
}



/* ==== UnitCtrl::get_IsClanBattleOrSekaiEnemy  @ 0x1605ce0  size=8 ==== */

undefined1 UnitCtrl::get_IsClanBattleOrSekaiEnemy(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x1a9);
}



/* ==== UnitCtrl::get_IsDamageNumCenterBoneEnable  @ 0x1605cf0  size=8 ==== */

undefined1 UnitCtrl::get_IsDamageNumCenterBoneEnable(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x398);
}



/* ==== UnitCtrl::get_IsDeadBySetCurrentHp  @ 0x1605d00  size=8 ==== */

undefined1 UnitCtrl::get_IsDeadBySetCurrentHp(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x550);
}



/* ==== UnitCtrl::get_IsDepthBack  @ 0x1605d10  size=8 ==== */

undefined1 UnitCtrl::get_IsDepthBack(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x270);
}



/* ==== UnitCtrl::get_IsDivision  @ 0x1605d20  size=14 ==== */

bool UnitCtrl::get_IsDivision(longlong param_1)

{
  return *(int *)(param_1 + 0x180) == 0x3e9;
}



/* ==== UnitCtrl::get_IsDivisionSourceForDamage  @ 0x5fed40  size=8 ==== */

undefined1 UnitCtrl::get_IsDivisionSourceForDamage(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x186);
}



/* ==== UnitCtrl::get_IsDivisionSourceForDie  @ 0x84af50  size=8 ==== */

undefined1 UnitCtrl::get_IsDivisionSourceForDie(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x187);
}



/* ==== UnitCtrl::get_IsForceLeftDirOrPartsBoss  @ 0x1605d30  size=1 ==== */

bool UnitCtrl::get_IsForceLeftDirOrPartsBoss(longlong param_1)

{
  code *pcVar1;
  undefined1 uVar2;
  
  if (*(char *)(param_1 + 0x380) != '\0') {
    return true;
  }
  if (cRam000000018ab6306a == '\0') {
    sub_8032f6d0(0x187b9ead8);
    cRam000000018ab6306a = '\x01';
  }
  if (*(longlong *)(param_1 + 0x388) != 0) {
    return 0 < *(int *)(*(longlong *)(param_1 + 0x388) + 0x18);
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  uVar2 = (*pcVar1)();
  return (bool)uVar2;
}



/* ==== UnitCtrl::get_IsGuest  @ 0x5fed80  size=8 ==== */

undefined1 UnitCtrl::get_IsGuest(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x184);
}



/* ==== UnitCtrl::get_IsGuestFadeout  @ 0x5fed00  size=8 ==== */

undefined1 UnitCtrl::get_IsGuestFadeout(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x185);
}



/* ==== UnitCtrl::get_IsLeftDir  @ 0x1605d90  size=8 ==== */

undefined1 UnitCtrl::get_IsLeftDir(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x165);
}



/* ==== UnitCtrl::get_IsMoveSpeedForceZero  @ 0x1605da0  size=25 ==== */

undefined8 * UnitCtrl::get_IsMoveSpeedForceZero(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x4f8);
  *param_1 = *(undefined8 *)(param_2 + 0x4f0);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_IsOther  @ 0x5b2140  size=8 ==== */

undefined1 UnitCtrl::get_IsOther(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x178);
}



/* ==== UnitCtrl::get_IsPartsBoss  @ 0x1605dc0  size=1 ==== */

undefined8 UnitCtrl::get_IsPartsBoss(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  undefined8 uVar3;
  
  if (cRam000000018ab6306a == '\0') {
    sub_8032f6d0(0x187b9ead8);
    cRam000000018ab6306a = '\x01';
  }
  lVar1 = *(longlong *)(param_1 + 0x388);
  if (lVar1 != 0) {
    return CONCAT71((int7)((ulonglong)lVar1 >> 8),0 < *(int *)(lVar1 + 0x18));
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::get_IsPhantom  @ 0x1605e10  size=11 ==== */

bool UnitCtrl::get_IsPhantom(longlong param_1)

{
  return *(int *)(param_1 + 0x180) == 2;
}



/* ==== UnitCtrl::get_IsPlayerUnit  @ 0x1605e20  size=1 ==== */

undefined8 UnitCtrl::get_IsPlayerUnit(longlong param_1)

{
  char cVar1;
  longlong lVar2;
  code *pcVar3;
  undefined8 uVar4;
  
  cVar1 = *(char *)(param_1 + 0x178);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar2 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar2 != 0) {
    return CONCAT71((int7)((ulonglong)lVar2 >> 8),cVar1 == *(char *)(lVar2 + 0x298));
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar4 = (*pcVar3)();
  return uVar4;
}



/* ==== UnitCtrl::get_IsRecreated  @ 0x1605e80  size=8 ==== */

undefined1 UnitCtrl::get_IsRecreated(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x150);
}



/* ==== UnitCtrl::get_IsRelicEnemy  @ 0x1605e90  size=11 ==== */

bool UnitCtrl::get_IsRelicEnemy(longlong param_1)

{
  return *(int *)(param_1 + 0x17c) == 2;
}



/* ==== UnitCtrl::get_IsShadow  @ 0x69c280  size=8 ==== */

undefined1 UnitCtrl::get_IsShadow(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x188);
}



/* ==== UnitCtrl::get_IsStartSkillExeced  @ 0x1605ea0  size=8 ==== */

undefined1 UnitCtrl::get_IsStartSkillExeced(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x2de);
}



/* ==== UnitCtrl::get_IsSummonOrPhantom  @ 0x1605eb0  size=23 ==== */

bool UnitCtrl::get_IsSummonOrPhantom(longlong param_1)

{
  if (*(int *)(param_1 + 0x180) == 1) {
    return true;
  }
  return *(int *)(param_1 + 0x180) == 2;
}



/* ==== UnitCtrl::get_IsTrialBattleEnemy  @ 0x1605ed0  size=8 ==== */

undefined1 UnitCtrl::get_IsTrialBattleEnemy(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x1aa);
}



/* ==== UnitCtrl::get_LevelFixedSpSkillIdList  @ 0x8cb690  size=8 ==== */

undefined8 UnitCtrl::get_LevelFixedSpSkillIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x4c0);
}



/* ==== UnitCtrl::get_LifeGauge  @ 0x8db7e0  size=8 ==== */

undefined8 UnitCtrl::get_LifeGauge(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x268);
}



/* ==== UnitCtrl::get_MainSkill1EvolutionType  @ 0x1605ee0  size=7 ==== */

undefined4 UnitCtrl::get_MainSkill1EvolutionType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x154);
}



/* ==== UnitCtrl::get_MainSkill2EvolutionType  @ 0x1605ef0  size=7 ==== */

undefined4 UnitCtrl::get_MainSkill2EvolutionType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x158);
}



/* ==== UnitCtrl::get_MainSkillEvolutionIdList  @ 0x9dc7e0  size=8 ==== */

undefined8 UnitCtrl::get_MainSkillEvolutionIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x520);
}



/* ==== UnitCtrl::get_MainSkillIdList  @ 0x8cb780  size=8 ==== */

undefined8 UnitCtrl::get_MainSkillIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x510);
}



/* ==== UnitCtrl::get_MainSkillRevolutionIdList  @ 0x1605f00  size=8 ==== */

undefined8 UnitCtrl::get_MainSkillRevolutionIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x530);
}



/* ==== UnitCtrl::get_MotionPrefix  @ 0x1605f10  size=1 ==== */

ulonglong UnitCtrl::get_MotionPrefix(longlong param_1)

{
  longlong lVar1;
  code *pcVar2;
  ulonglong uVar3;
  
  if (cRam000000018ab6303d == '\0') {
    sub_8032f6d0(0x187b9aea8);
    cRam000000018ab6303d = '\x01';
  }
  if ((*(longlong *)(param_1 + 0x38) != 0) &&
     (lVar1 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x298), lVar1 != 0)) {
    if (0 < *(int *)(lVar1 + 0x18)) {
      return 0xffffffff;
    }
    return (ulonglong)*(uint *)(param_1 + 0x1a0);
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar3 = (*pcVar2)();
  return uVar3;
}



/* ==== UnitCtrl::get_MoveRate  @ 0x1605f70  size=9 ==== */

undefined4 UnitCtrl::get_MoveRate(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x2d8);
}



/* ==== UnitCtrl::get_OnInitCallbackForUi  @ 0x8dbb00  size=8 ==== */

undefined8 UnitCtrl::get_OnInitCallbackForUi(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x2e8);
}



/* ==== UnitCtrl::get_OnIsFrontFalse  @ 0x62e3e0  size=8 ==== */

undefined8 UnitCtrl::get_OnIsFrontFalse(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x238);
}



/* ==== UnitCtrl::get_OriginalUnitId  @ 0x1605f80  size=7 ==== */

undefined4 UnitCtrl::get_OriginalUnitId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x490);
}



/* ==== UnitCtrl::get_OverlapPosX  @ 0x1605f90  size=9 ==== */

undefined4 UnitCtrl::get_OverlapPosX(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x18c);
}



/* ==== UnitCtrl::get_PartsMotionPrefix  @ 0x5fed90  size=7 ==== */

undefined4 UnitCtrl::get_PartsMotionPrefix(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x1a4);
}



/* ==== UnitCtrl::get_Pause  @ 0x1605fa0  size=8 ==== */

undefined1 UnitCtrl::get_Pause(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x460);
}



/* ==== UnitCtrl::get_PrefabId  @ 0x1605fb0  size=7 ==== */

undefined4 UnitCtrl::get_PrefabId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x15c);
}



/* ==== UnitCtrl::get_PrincessFormProcessor  @ 0x437d60  size=5 ==== */

undefined8 UnitCtrl::get_PrincessFormProcessor(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x30);
}



/* ==== UnitCtrl::get_PromotionLevel  @ 0x1605fc0  size=7 ==== */

undefined4 UnitCtrl::get_PromotionLevel(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4a4);
}



/* ==== UnitCtrl::get_Rarity  @ 0x1605fd0  size=7 ==== */

undefined4 UnitCtrl::get_Rarity(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x49c);
}



/* ==== UnitCtrl::get_RepeatEffectList  @ 0x5233f0  size=8 ==== */

undefined8 UnitCtrl::get_RepeatEffectList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x100);
}



/* ==== UnitCtrl::get_RespawnPos  @ 0x5389b0  size=7 ==== */

undefined4 UnitCtrl::get_RespawnPos(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x168);
}



/* ==== UnitCtrl::get_RewardCount  @ 0x1605fe0  size=25 ==== */

undefined8 * UnitCtrl::get_RewardCount(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x4dc);
  *param_1 = *(undefined8 *)(param_2 + 0x4d4);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_RoleId  @ 0x1606000  size=7 ==== */

undefined4 UnitCtrl::get_RoleId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4e8);
}



/* ==== UnitCtrl::get_RotateCenter  @ 0x66a670  size=8 ==== */

undefined8 UnitCtrl::get_RotateCenter(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x1d8);
}



/* ==== UnitCtrl::get_Rupee  @ 0x1606010  size=25 ==== */

undefined8 * UnitCtrl::get_Rupee(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x4d0);
  *param_1 = *(undefined8 *)(param_2 + 0x4c8);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_SDSkin  @ 0x1606030  size=7 ==== */

undefined4 UnitCtrl::get_SDSkin(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x494);
}



/* ==== UnitCtrl::get_SkeletonRenderSeparator  @ 0x509fa0  size=8 ==== */

undefined8 UnitCtrl::get_SkeletonRenderSeparator(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x130);
}



/* ==== UnitCtrl::get_SkillEnableFrame  @ 0x6847c0  size=7 ==== */

undefined4 UnitCtrl::get_SkillEnableFrame(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x1c8);
}



/* ==== UnitCtrl::get_SkillLevels  @ 0x8cb7a0  size=8 ==== */

undefined8 UnitCtrl::get_SkillLevels(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x500);
}



/* ==== UnitCtrl::get_SkillStackVal  @ 0x1606060  size=25 ==== */

undefined8 * UnitCtrl::get_SkillStackVal(undefined8 *param_1,longlong param_2)

{
  undefined8 uVar1;
  undefined8 uVar2;
  undefined8 uVar3;
  
  uVar1 = *(undefined8 *)(param_2 + 0x2c0);
  uVar2 = *(undefined8 *)(param_2 + 0x2c8);
  uVar3 = *(undefined8 *)(param_2 + 0x2d0);
  *param_1 = *(undefined8 *)(param_2 + 0x2b8);
  param_1[1] = uVar1;
  param_1[2] = uVar2;
  param_1[3] = uVar3;
  return param_1;
}



/* ==== UnitCtrl::get_SkillStackValDmg  @ 0x1606040  size=25 ==== */

undefined8 * UnitCtrl::get_SkillStackValDmg(undefined8 *param_1,longlong param_2)

{
  undefined8 uVar1;
  undefined8 uVar2;
  undefined8 uVar3;
  
  uVar1 = *(undefined8 *)(param_2 + 0x2a0);
  uVar2 = *(undefined8 *)(param_2 + 0x2a8);
  uVar3 = *(undefined8 *)(param_2 + 0x2b0);
  *param_1 = *(undefined8 *)(param_2 + 0x298);
  param_1[1] = uVar1;
  param_1[2] = uVar2;
  param_1[3] = uVar3;
  return param_1;
}



/* ==== UnitCtrl::get_SkinRarity  @ 0x1606080  size=7 ==== */

undefined4 UnitCtrl::get_SkinRarity(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x254);
}



/* ==== UnitCtrl::get_SpecialSkillEvolutionIdList  @ 0x9dc7d0  size=8 ==== */

undefined8 UnitCtrl::get_SpecialSkillEvolutionIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x528);
}



/* ==== UnitCtrl::get_SpecialSkillIdList  @ 0x8cb7d0  size=8 ==== */

undefined8 UnitCtrl::get_SpecialSkillIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x518);
}



/* ==== UnitCtrl::get_SpecialSkillRevolutionIdList  @ 0x1606090  size=8 ==== */

undefined8 UnitCtrl::get_SpecialSkillRevolutionIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x538);
}



/* ==== UnitCtrl::get_SpecialVoiceTimingData  @ 0x8db770  size=8 ==== */

undefined8 UnitCtrl::get_SpecialVoiceTimingData(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x248);
}



/* ==== UnitCtrl::get_StartDashDone  @ 0x16060a0  size=8 ==== */

undefined1 UnitCtrl::get_StartDashDone(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x2dd);
}



/* ==== UnitCtrl::get_StartHpPercent  @ 0x16060b0  size=9 ==== */

undefined4 UnitCtrl::get_StartHpPercent(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4ec);
}



/* ==== UnitCtrl::get_StateBone  @ 0x483cf0  size=8 ==== */

undefined8 UnitCtrl::get_StateBone(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xe0);
}



/* ==== UnitCtrl::get_StateBoneModeChange  @ 0x574c40  size=8 ==== */

undefined8 UnitCtrl::get_StateBoneModeChange(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0xe8);
}



/* ==== UnitCtrl::get_SubUnionBurstIdList  @ 0x16060c0  size=8 ==== */

undefined8 UnitCtrl::get_SubUnionBurstIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x540);
}



/* ==== UnitCtrl::get_SummonRespawnPos  @ 0x5389e0  size=7 ==== */

undefined4 UnitCtrl::get_SummonRespawnPos(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x16c);
}



/* ==== UnitCtrl::get_SummonSource  @ 0x579d90  size=8 ==== */

undefined8 UnitCtrl::get_SummonSource(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x1b8);
}



/* ==== UnitCtrl::get_SummonType  @ 0x5b2090  size=7 ==== */

undefined4 UnitCtrl::get_SummonType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x180);
}



/* ==== UnitCtrl::get_SummonUnitDictionary  @ 0x579dd0  size=8 ==== */

undefined8 UnitCtrl::get_SummonUnitDictionary(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x1b0);
}



/* ==== UnitCtrl::get_TalentIds  @ 0x8cb680  size=8 ==== */

undefined8 UnitCtrl::get_TalentIds(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x4e0);
}



/* ==== UnitCtrl::get_TimeToDie  @ 0x1197580  size=8 ==== */

undefined1 UnitCtrl::get_TimeToDie(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x164);
}



/* ==== UnitCtrl::get_TreasureAnimeIdList  @ 0x16060d0  size=8 ==== */

undefined8 UnitCtrl::get_TreasureAnimeIdList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x548);
}



/* ==== UnitCtrl::get_UnionBurstSkillId  @ 0x16060e0  size=7 ==== */

undefined4 UnitCtrl::get_UnionBurstSkillId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x4b8);
}



/* ==== UnitCtrl::get_UnitActionController  @ 0x437d00  size=5 ==== */

undefined8 UnitCtrl::get_UnitActionController(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x28);
}



/* ==== UnitCtrl::get_UnitId  @ 0x16060f0  size=7 ==== */

undefined4 UnitCtrl::get_UnitId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x488);
}



/* ==== UnitCtrl::get_UnitInstanceId  @ 0x6bd060  size=7 ==== */

undefined4 UnitCtrl::get_UnitInstanceId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x160);
}



/* ==== UnitCtrl::get_UnitParameter  @ 0x4b4890  size=8 ==== */

undefined8 UnitCtrl::get_UnitParameter(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x170);
}



/* ==== UnitCtrl::get_UnitSpineCtrl  @ 0x483cd0  size=8 ==== */

undefined8 UnitCtrl::get_UnitSpineCtrl(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x128);
}



/* ==== UnitCtrl::get_UnitSpineCtrlModeChange  @ 0x509f80  size=8 ==== */

undefined8 UnitCtrl::get_UnitSpineCtrlModeChange(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x138);
}



/* ==== UnitCtrl::get_UnitUnionBurstTimelineList  @ 0x5c3580  size=8 ==== */

undefined8 UnitCtrl::get_UnitUnionBurstTimelineList(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x140);
}



/* ==== UnitCtrl::get_UsesEnemyUbOrAwakeSetting  @ 0x1606100  size=1 ==== */

ulonglong UnitCtrl::get_UsesEnemyUbOrAwakeSetting(longlong param_1)

{
  char cVar1;
  longlong lVar2;
  code *pcVar3;
  ulonglong uVar4;
  
  cVar1 = *(char *)(param_1 + 0x178);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar2 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar2 != 0) {
    if (cVar1 != *(char *)(lVar2 + 0x298)) {
      return CONCAT71((int7)((ulonglong)lVar2 >> 8),1);
    }
    return (ulonglong)*(byte *)(param_1 + 0x184);
  }
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar4 = (*pcVar3)();
  return uVar4;
}



/* ==== UnitCtrl::get_WeaponMotionType  @ 0x5c3570  size=7 ==== */

undefined4 UnitCtrl::get_WeaponMotionType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x120);
}



/* ==== UnitCtrl::get_WeaponSeType  @ 0x5d8330  size=7 ==== */

undefined4 UnitCtrl::get_WeaponSeType(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x11c);
}



/* ==== UnitCtrl::get_battleCameraEffect  @ 0x1606180  size=1 ==== */

undefined8 UnitCtrl::get_battleCameraEffect(void)

{
  if (cRam000000018ab63041 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63041 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x10);
}



/* ==== UnitCtrl::get_battleEffectPool  @ 0x16061c0  size=1 ==== */

undefined8 UnitCtrl::get_battleEffectPool(void)

{
  if (cRam000000018ab63042 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63042 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x18);
}



/* ==== UnitCtrl::get_battleLog  @ 0x1606200  size=1 ==== */

undefined8 UnitCtrl::get_battleLog(void)

{
  if (cRam000000018ab63040 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63040 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 8);
}



/* ==== UnitCtrl::get_battleManager  @ 0x1606240  size=1 ==== */

undefined8 UnitCtrl::get_battleManager(void)

{
  if (cRam000000018ab63044 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63044 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
}



/* ==== UnitCtrl::get_battleTimeScale  @ 0x1606280  size=1 ==== */

undefined8 UnitCtrl::get_battleTimeScale(void)

{
  if (cRam000000018ab63043 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63043 = '\x01';
  }
  return *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x20);
}



/* ==== UnitCtrl::get_bottomTransform  @ 0x9e31c0  size=8 ==== */

undefined8 UnitCtrl::get_bottomTransform(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x458);
}



/* ==== UnitCtrl::get_castTimeDictionary  @ 0x9d73e0  size=8 ==== */

undefined8 UnitCtrl::get_castTimeDictionary(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x478);
}



/* ==== UnitCtrl::get_fixedCenterForDamageNumPos  @ 0x16062c0  size=25 ==== */

undefined8 * UnitCtrl::get_fixedCenterForDamageNumPos(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x208);
  *param_1 = *(undefined8 *)(param_2 + 0x200);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_fixedCenterPos  @ 0x16062e0  size=25 ==== */

undefined8 * UnitCtrl::get_fixedCenterPos(undefined8 *param_1,longlong param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 0x1f0);
  *param_1 = *(undefined8 *)(param_2 + 0x1e8);
  *(undefined4 *)(param_1 + 1) = uVar1;
  return param_1;
}



/* ==== UnitCtrl::get_isPaused  @ 0x1605fa0  size=8 ==== */

undefined1 UnitCtrl::get_isPaused(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x460);
}



/* ==== UnitCtrl::get_leftDirScale  @ 0x1606300  size=25 ==== */

undefined8 UnitCtrl::get_leftDirScale(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x464);
}



/* ==== UnitCtrl::get_motionPrefix  @ 0x5fed10  size=7 ==== */

undefined4 UnitCtrl::get_motionPrefix(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x1a0);
}



/* ==== UnitCtrl::get_rightDirScale  @ 0x1606320  size=25 ==== */

undefined8 UnitCtrl::get_rightDirScale(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x46c);
}



/* ==== UnitCtrl::get_singletonTree  @ 0x1606340  size=1 ==== */

undefined8 UnitCtrl::get_singletonTree(void)

{
  if (cRam000000018ab6303f == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6303f = '\x01';
  }
  return **(undefined8 **)(lRam0000000187bfb7a8 + 0xb8);
}



/* ==== UnitCtrl::get_unionBurstSkillAreaWidth  @ 0x1606380  size=9 ==== */

undefined4 UnitCtrl::get_unionBurstSkillAreaWidth(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x474);
}



/* ==== UnitCtrl::initializeCharaAuraEffect  @ 0x1606390  size=1 ==== */

longlong * UnitCtrl::initializeCharaAuraEffect(longlong param_1,undefined8 param_2,undefined8 param_3,undefined1 param_4)

{
  undefined8 uVar1;
  undefined4 uVar2;
  uint uVar3;
  longlong lVar4;
  code *pcVar5;
  char cVar6;
  longlong *plVar7;
  longlong lVar8;
  longlong lVar9;
  undefined8 *puVar10;
  undefined8 uVar11;
  undefined8 uVar12;
  undefined8 uStack_48;
  undefined4 uStack_40;
  undefined1 auStack_38 [32];
  
  if (cRam000000018ab6304d == '\0') {
    sub_8032f6d0(0x187c38198);
    sub_8032f6d0(0x187b8ae18);
    sub_8032f6d0(0x187c318b8);
    cRam000000018ab6304d = '\x01';
  }
  if (cRam000000018ab6307a == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab6307a = '\x01';
  }
  lVar8 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x18);
  if (lVar8 != 0) {
    plVar7 = (longlong *)sub_800121c0(0,uRam0000000187b8ae18,lVar8,param_2,param_1);
    if (plVar7 == (longlong *)0x0) goto code_r0x0001816068ab;
    (**(code **)(*plVar7 + 0x188))(plVar7,*(undefined8 *)(*plVar7 + 400));
    if (*(longlong *)(param_1 + 200) == 0) goto code_r0x0001816068ab;
    SkillEffectCtrl::PlaySe(plVar7,*(undefined4 *)(*(longlong *)(param_1 + 200) + 0x38),
                  *(undefined1 *)(param_1 + 0x165),0);
    lVar8 = Component::get_transform(plVar7,0);
    lVar9 = FixedTransformMonoBehavior::get_transform(param_1,0);
    if (lVar9 == 0) goto code_r0x0001816068ab;
    puVar10 = (undefined8 *)FixedTransform::get_position(auStack_38,lVar9,0);
    if (lVar8 == 0) goto code_r0x0001816068ab;
    uStack_48 = *puVar10;
    uStack_40 = *(undefined4 *)(puVar10 + 1);
    Transform::set_position(lVar8,&uStack_48,0);
    lVar8 = Component::get_transform(plVar7,0);
    uVar11 = ExceptNGUIRoot::get_Transform(0);
    if (lVar8 == 0) goto code_r0x0001816068ab;
    Transform::set_parent(lVar8,uVar11,0);
    plVar7[0x12] = param_1;
    sub_8032e8d0(plVar7 + 0x12,param_1);
    (**(code **)(*plVar7 + 0x198))(plVar7,param_1,1,*(undefined8 *)(*plVar7 + 0x1a0));
    if (*(longlong *)(param_1 + 0x80) == 0) goto code_r0x0001816068ab;
    (**(code **)(*plVar7 + 0x1b8))
              (plVar7,0,*(undefined8 *)(*(longlong *)(param_1 + 0x80) + 0x20),
               *(undefined8 *)(*plVar7 + 0x1c0));
    (**(code **)(*plVar7 + 0x218))(plVar7,*(undefined8 *)(*plVar7 + 0x220));
    if (*(int *)(lRam0000000187c38198 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar6 = ExtensionMethods::IsNullOrEmpty(param_3,0);
    if (cVar6 == '\0') {
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar8 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (*(longlong *)(param_1 + 0x80) == 0) goto code_r0x0001816068ab;
      uVar11 = AIGOPEMKDKL::MCMPDLOCDCI(*(longlong *)(param_1 + 0x80),1,0,0);
      if (cRam000000018ab5adcc == '\0') {
        sub_8032f6d0(0x187c16e48);
        cRam000000018ab5adcc = '\x01';
      }
      uVar1 = **(undefined8 **)(lRam0000000187c16e48 + 0xb8);
      uVar2 = *(undefined4 *)(*(undefined8 **)(lRam0000000187c16e48 + 0xb8) + 1);
      lVar9 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
      if ((lVar9 == 0) || (*(longlong *)(lVar9 + 0xa8) == 0)) goto code_r0x0001816068ab;
      uVar12 = Skeleton::FindBone(*(longlong *)(lVar9 + 0xa8),param_3,0);
      uStack_48 = uVar1;
      uStack_40 = uVar2;
    }
    else {
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar8 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (*(longlong *)(param_1 + 0x80) == 0) goto code_r0x0001816068ab;
      uVar11 = AIGOPEMKDKL::MCMPDLOCDCI(*(longlong *)(param_1 + 0x80),1,0,0);
      if (cRam000000018ab5adcc == '\0') {
        sub_8032f6d0(0x187c16e48);
        cRam000000018ab5adcc = '\x01';
      }
      param_4 = 0;
      uStack_48 = **(undefined8 **)(lRam0000000187c16e48 + 0xb8);
      uStack_40 = *(undefined4 *)(*(undefined8 **)(lRam0000000187c16e48 + 0xb8) + 1);
      uVar12 = 0;
    }
    uVar11 = SkillEffectCtrl::TrackTarget(plVar7,uVar11,&uStack_48,1,1,uVar12,param_4,0,0);
    if (lVar8 == 0) goto code_r0x0001816068ab;
    sub_86209420(lVar8,uVar11,0);
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar8 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    uVar11 = SkillEffectCtrl::TrackTargetSort(plVar7,param_1,1,0);
    if (lVar8 == 0) goto code_r0x0001816068ab;
    sub_86209420(lVar8,uVar11,0);
    lVar9 = lRam0000000187c318b8;
    lVar8 = *(longlong *)(param_1 + 0x100);
    if (lVar8 == 0) goto code_r0x0001816068ab;
    *(int *)(lVar8 + 0x1c) = *(int *)(lVar8 + 0x1c) + 1;
    lVar4 = *(longlong *)(lVar8 + 0x10);
    uVar3 = *(uint *)(lVar8 + 0x18);
    if (lVar4 == 0) goto code_r0x0001816068ab;
    if (uVar3 < *(uint *)(lVar4 + 0x18)) {
      *(uint *)(lVar8 + 0x18) = uVar3 + 1;
      if (*(uint *)(lVar4 + 0x18) <= uVar3) goto code_r0x0001816068b1;
      *(longlong **)(lVar4 + 0x20 + (longlong)(int)uVar3 * 8) = plVar7;
      sub_8032e8d0(lVar4 + 0x20 + (longlong)(int)uVar3 * 8,plVar7);
    }
    else {
      sub_84a97470(lVar8,plVar7,
                    *(undefined8 *)(*(longlong *)(*(longlong *)(lVar9 + 0x20) + 0xc0) + 0x70));
    }
    lVar9 = lRam0000000187c318b8;
    lVar8 = *(longlong *)(param_1 + 0x108);
    if (lVar8 != 0) {
      *(int *)(lVar8 + 0x1c) = *(int *)(lVar8 + 0x1c) + 1;
      lVar4 = *(longlong *)(lVar8 + 0x10);
      uVar3 = *(uint *)(lVar8 + 0x18);
      if (lVar4 != 0) {
        if (uVar3 < *(uint *)(lVar4 + 0x18)) {
          *(uint *)(lVar8 + 0x18) = uVar3 + 1;
          if (*(uint *)(lVar4 + 0x18) <= uVar3) {
code_r0x0001816068b1:
            sub_8032f9c0();
            pcVar5 = (code *)swi(3);
            plVar7 = (longlong *)(*pcVar5)();
            return plVar7;
          }
          *(longlong **)(lVar4 + 0x20 + (longlong)(int)uVar3 * 8) = plVar7;
          sub_8032e8d0(lVar4 + 0x20 + (longlong)(int)uVar3 * 8,plVar7);
        }
        else {
          sub_84a97470(lVar8,plVar7,
                        *(undefined8 *)(*(longlong *)(*(longlong *)(lVar9 + 0x20) + 0xc0) + 0x70));
        }
        return plVar7;
      }
    }
  }
code_r0x0001816068ab:
  sub_8032f9d0();
  pcVar5 = (code *)swi(3);
  plVar7 = (longlong *)(*pcVar5)();
  return plVar7;
}



/* ==== UnitCtrl::instantiateResources  @ 0x16068c0  size=1 ==== */

void UnitCtrl::instantiateResources(longlong param_1)

{
  char cVar1;
  code *pcVar2;
  longlong lVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  longlong lVar6;
  longlong lVar7;
  longlong lVar8;
  
  if (cRam000000018ab6304a == '\0') {
    sub_8032f6d0(0x187b64708);
    sub_8032f6d0(0x187b64550);
    sub_8032f6d0(0x187bb8158);
    sub_8032f6d0(0x187b3d4a8);
    sub_8032f6d0(0x187b303e0);
    sub_8032f6d0(0x187b30590);
    sub_8032f6d0(0x187b30748);
    cRam000000018ab6304a = '\x01';
  }
  lVar3 = *(longlong *)(lRam0000000187b3d4a8 + 0x20);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  lVar3 = *(longlong *)(*(longlong *)(lVar3 + 0xc0) + 0x10);
  if ((*(byte *)(lVar3 + 0x135) & 1) == 0) {
    lVar3 = sub_8032d010(lVar3);
  }
  if (**(longlong **)(lVar3 + 0xb8) != 0) {
    lVar8 = 0;
    lVar3 = ResourceManager::LoadImmediately(**(longlong **)(lVar3 + 0xb8),0x4c9333ce,0,0,0);
    if (lVar3 != 0) {
      uVar4 = sub_847c74a0(lVar3,uRam0000000187bb8158);
      *(undefined8 *)(param_1 + 0x268) = uVar4;
      sub_8032e8d0(param_1 + 0x268,uVar4);
      cVar1 = *(char *)(param_1 + 0x178);
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar3 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar3 != 0) {
        if ((cVar1 == *(char *)(lVar3 + 0x298)) || (*(char *)(param_1 + 0x1a8) != '\0')) {
          return;
        }
        lVar3 = *(longlong *)(param_1 + 0x38);
        if (lVar3 != 0) {
          uVar4 = *(undefined8 *)(lVar3 + 0x2b0);
          uVar5 = sub_8032f970(uRam0000000187b64550);
          sub_856f0480(uVar5,param_1,uRam0000000187b303e0,0);
          lVar6 = Delegate::Combine(uVar4,uVar5,0);
          uVar4 = uRam0000000187b64550;
          lVar7 = lVar8;
          if ((lVar6 != 0) && (lVar7 = sub_8032e8f0(lVar6,uRam0000000187b64550), lVar7 == 0))
          {
            sub_8032e920(lVar6,uVar4);
            pcVar2 = (code *)swi(3);
            (*pcVar2)();
            return;
          }
          *(longlong *)(lVar3 + 0x2b0) = lVar7;
          sub_8032e8d0(lVar3 + 0x2b0,lVar7);
          lVar3 = *(longlong *)(param_1 + 0x38);
          if (lVar3 != 0) {
            uVar4 = *(undefined8 *)(lVar3 + 0x2b8);
            uVar5 = sub_8032f970(uRam0000000187b64708);
            OnDelete::.ctor(uVar5,param_1,uRam0000000187b30590,0);
            lVar6 = Delegate::Combine(uVar4,uVar5,0);
            uVar4 = uRam0000000187b64708;
            lVar7 = lVar8;
            if ((lVar6 != 0) &&
               (lVar7 = sub_8032e8f0(lVar6,uRam0000000187b64708), lVar7 == 0)) {
              sub_8032e920(lVar6,uVar4);
              pcVar2 = (code *)swi(3);
              (*pcVar2)();
              return;
            }
            *(longlong *)(lVar3 + 0x2b8) = lVar7;
            sub_8032e8d0(lVar3 + 0x2b8,lVar7);
            lVar3 = *(longlong *)(param_1 + 0x38);
            if (lVar3 != 0) {
              uVar4 = *(undefined8 *)(lVar3 + 0x2c0);
              uVar5 = sub_8032f970(uRam0000000187b64550);
              sub_856f0480(uVar5,param_1,uRam0000000187b30748,0);
              lVar7 = Delegate::Combine(uVar4,uVar5,0);
              uVar4 = uRam0000000187b64550;
              if ((lVar7 != 0) &&
                 (lVar8 = sub_8032e8f0(lVar7,uRam0000000187b64550), lVar8 == 0)) {
                sub_8032e920(lVar7,uVar4);
                pcVar2 = (code *)swi(3);
                (*pcVar2)();
                return;
              }
              *(longlong *)(lVar3 + 0x2c0) = lVar8;
              sub_8032e8d0(lVar3 + 0x2c0,lVar8);
              return;
            }
          }
        }
      }
    }
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::isNearestEnemyLeft  @ 0x1606c00  size=1 ==== */

undefined8 UnitCtrl::isNearestEnemyLeft(longlong param_1)

{
  float fVar1;
  uint uVar2;
  code *pcVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  char cVar6;
  longlong lVar7;
  longlong lVar8;
  longlong lVar9;
  longlong lVar10;
  float *pfVar11;
  undefined8 uVar12;
  int iVar13;
  ulonglong uVar14;
  float fVar15;
  undefined8 uStack_138;
  undefined8 uStack_130;
  undefined8 uStack_128;
  undefined1 auStack_118 [16];
  undefined1 auStack_108 [16];
  undefined1 auStack_f8 [16];
  undefined1 auStack_e8 [16];
  undefined1 auStack_d8 [16];
  undefined1 auStack_c8 [16];
  undefined1 auStack_b8 [16];
  undefined1 auStack_a8 [16];
  undefined1 auStack_98 [16];
  undefined1 auStack_88 [16];
  undefined1 auStack_78 [16];
  undefined1 auStack_68 [80];
  
  if (cRam000000018ab6305d == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187b71920);
    sub_8032f6d0(0x187b715a8);
    sub_8032f6d0(0x187b9ead8);
    sub_8032f6d0(0x187b73698);
    sub_8032f6d0(0x187b9ec90);
    sub_8032f6d0(0x187b73858);
    sub_8032f6d0(0x187c32990);
    sub_8032f6d0(0x187b5e978);
    sub_8032f6d0(0x187b61160);
    cRam000000018ab6305d = '\x01';
  }
  if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x0001816072f9;
  cVar6 = CLKMBEGHOLA::GMKFGFKMFCE(*(longlong *)(param_1 + 0x38),0);
  if (cVar6 == '\0') {
    if (*(char *)(param_1 + 0x178) == '\0') goto code_r0x000181606d09;
code_r0x000181606e5d:
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar7 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar7 == 0) goto code_r0x0001816072f9;
    lVar7 = *(longlong *)(lVar7 + 0x240);
  }
  else {
    if (*(char *)(param_1 + 0x178) == '\0') goto code_r0x000181606e5d;
code_r0x000181606d09:
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar7 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar7 == 0) goto code_r0x0001816072f9;
    lVar7 = *(longlong *)(lVar7 + 0x248);
  }
  if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
    sub_8032fa70();
  }
  fVar15 = *(float *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 0x38);
  iVar13 = -1;
  if (*(char *)(param_1 + 0x178) != '\0') {
    iVar13 = 1;
  }
  fVar15 = (fVar15 + fVar15) * (float)iVar13;
  if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x0001816072f9;
  cVar6 = CLKMBEGHOLA::GMKFGFKMFCE(*(longlong *)(param_1 + 0x38),0);
  if (cVar6 == '\0') {
    if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x0001816072f9;
    cVar6 = CLKMBEGHOLA::IEEAGJAAHJP(*(longlong *)(param_1 + 0x38),0);
    if (cVar6 != '\0') {
      lVar7 = sub_8032f970(uRam0000000187c32990);
      sub_849de5b0(lVar7,uRam0000000187b715a8);
      lVar8 = lRam0000000187b71920;
      if (((*(longlong *)(param_1 + 0x38) != 0) &&
          (lVar9 = *(longlong *)(*(longlong *)(param_1 + 0x38) + 0x180), lVar9 != 0)) &&
         (uVar12 = *(undefined8 *)(lVar9 + 0x68), lVar7 != 0)) {
        *(int *)(lVar7 + 0x1c) = *(int *)(lVar7 + 0x1c) + 1;
        lVar9 = *(longlong *)(lVar7 + 0x10);
        uVar2 = *(uint *)(lVar7 + 0x18);
        if (lVar9 == 0) goto code_r0x0001816072f9;
        if (uVar2 < *(uint *)(lVar9 + 0x18)) {
          *(uint *)(lVar7 + 0x18) = uVar2 + 1;
          if (*(uint *)(lVar9 + 0x18) <= uVar2) {
            sub_8032f9c0();
            pcVar3 = (code *)swi(3);
            uVar12 = (*pcVar3)();
            return uVar12;
          }
          *(undefined8 *)(lVar9 + 0x20 + (longlong)(int)uVar2 * 8) = uVar12;
          sub_8032e8d0(lVar9 + (longlong)(int)uVar2 * 8 + 0x20);
        }
        else {
          sub_84a97470(lVar7,uVar12,
                        *(undefined8 *)(*(longlong *)(*(longlong *)(lVar8 + 0x20) + 0xc0) + 0x70));
        }
        goto code_r0x000181606ed9;
      }
      goto code_r0x0001816072f9;
    }
  }
  if (lVar7 != 0) {
code_r0x000181606ed9:
    uVar14 = 0;
    do {
      if (*(int *)(lVar7 + 0x18) <= (int)uVar14) {
        return CONCAT71((int7)(uVar14 >> 8),0.0 < fVar15);
      }
      lVar8 = sub_849ed2d0(lVar7,uVar14,uRam0000000187b73858);
      if (lVar8 == 0) break;
      if (*(int *)(lVar8 + 0x180) != 2) {
        if (*(longlong *)(lVar8 + 0xd0) == 0) break;
        if (*(char *)(*(longlong *)(lVar8 + 0xd0) + 0xb9) == '\0') {
          if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
            sub_8032fa70();
          }
          cVar6 = Object::op_Equality(lVar8,param_1,0);
          if (cVar6 == '\0') {
            lVar9 = *(longlong *)(lVar8 + 0x88);
            if (lVar9 == 0) break;
            uVar4 = *(undefined8 *)(lVar9 + 0x430);
            uVar5 = *(undefined8 *)(lVar9 + 0x438);
            uVar12 = *(undefined8 *)(lVar9 + 0x440);
            if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
              sub_8032fa70();
            }
            uStack_138 = uVar4;
            uStack_130 = uVar5;
            uStack_128 = uVar12;
            lVar9 = ObscuredLong::FHOALPFLEEC(&uStack_138,0);
            if (lVar9 != 0) {
              lVar9 = *(longlong *)(lVar8 + 0x38);
              if (lVar9 == 0) break;
              if (*(char *)(lVar9 + 0x260) == '\0') {
                cVar6 = CLKMBEGHOLA::BGPIDFLHLGC(lVar9,param_1,0);
                if (cVar6 == '\0') {
                  cVar6 = UnitCtrl::get_IsPartsBoss(lVar8,0);
                  if (cVar6 == '\0') {
                    lVar9 = FixedTransformMonoBehavior::get_transform(param_1,0);
                    if (lVar9 == 0) break;
                    pfVar11 = (float *)FixedTransform::get_localPosition(auStack_b8,lVar9,0);
                    fVar1 = *pfVar11;
                    lVar9 = FixedTransformMonoBehavior::get_transform(lVar8,0);
                    if (lVar9 == 0) break;
                    pfVar11 = (float *)FixedTransform::get_localPosition(auStack_a8,lVar9,0);
                    if (ABS(fVar15) <= ABS(fVar1 - *pfVar11)) {
                      lVar9 = FixedTransformMonoBehavior::get_transform(param_1,0);
                      if (lVar9 == 0) break;
                      FixedTransform::get_localPosition(auStack_98,lVar9,0);
                      lVar9 = FixedTransformMonoBehavior::get_transform(lVar8,0);
                      if (lVar9 == 0) break;
                      FixedTransform::get_localPosition(auStack_88,lVar9,0);
                      cVar6 = BattleUtil::Approximately();
                      if (cVar6 == '\0') goto code_r0x000181607292;
                    }
                    lVar9 = FixedTransformMonoBehavior::get_transform(param_1,0);
                    if (lVar9 == 0) break;
                    pfVar11 = (float *)FixedTransform::get_localPosition(auStack_78,lVar9,0);
                    fVar15 = *pfVar11;
                    lVar8 = FixedTransformMonoBehavior::get_transform(lVar8,0);
                    if (lVar8 == 0) break;
                    pfVar11 = (float *)FixedTransform::get_localPosition(auStack_68,lVar8,0);
                    fVar15 = fVar15 - *pfVar11;
                  }
                  else {
                    iVar13 = 0;
                    while( true ) {
                      if ((*(longlong *)(lVar8 + 0x80) == 0) ||
                         (lVar9 = *(longlong *)(*(longlong *)(lVar8 + 0x80) + 0x18), lVar9 == 0))
                      goto code_r0x0001816072f9;
                      if (*(int *)(lVar9 + 0x18) <= iVar13) break;
                      lVar9 = sub_849ed2d0(lVar9,iVar13,uRam0000000187b9ec90);
                      lVar10 = FixedTransformMonoBehavior::get_transform(param_1,0);
                      if (lVar10 == 0) goto code_r0x0001816072f9;
                      pfVar11 = (float *)FixedTransform::get_localPosition(auStack_118,lVar10,0);
                      fVar1 = *pfVar11;
                      lVar10 = FixedTransformMonoBehavior::get_transform(lVar8,0);
                      if (lVar10 == 0) goto code_r0x0001816072f9;
                      pfVar11 = (float *)FixedTransform::get_localPosition(auStack_108,lVar10,0);
                      if (lVar9 == 0) goto code_r0x0001816072f9;
                      if (ABS((fVar1 - *pfVar11) - *(float *)(lVar9 + 0x20)) < ABS(fVar15)) {
code_r0x000181607110:
                        lVar10 = FixedTransformMonoBehavior::get_transform(param_1,0);
                        if (lVar10 == 0) goto code_r0x0001816072f9;
                        pfVar11 = (float *)FixedTransform::get_localPosition(auStack_d8,lVar10,0);
                        fVar15 = *pfVar11;
                        lVar10 = FixedTransformMonoBehavior::get_transform(lVar8,0);
                        if (lVar10 == 0) goto code_r0x0001816072f9;
                        pfVar11 = (float *)FixedTransform::get_localPosition(auStack_c8,lVar10,0);
                        fVar15 = (fVar15 - *pfVar11) - *(float *)(lVar9 + 0x20);
                      }
                      else {
                        lVar10 = FixedTransformMonoBehavior::get_transform(param_1,0);
                        if (lVar10 == 0) goto code_r0x0001816072f9;
                        FixedTransform::get_localPosition(auStack_f8,lVar10,0);
                        lVar10 = FixedTransformMonoBehavior::get_transform(lVar8,0);
                        if (lVar10 == 0) goto code_r0x0001816072f9;
                        FixedTransform::get_localPosition(auStack_e8,lVar10,0);
                        cVar6 = BattleUtil::Approximately();
                        if (cVar6 != '\0') goto code_r0x000181607110;
                      }
                      iVar13 = iVar13 + 1;
                    }
                  }
                }
              }
            }
          }
        }
      }
code_r0x000181607292:
      uVar14 = (ulonglong)((int)uVar14 + 1);
    } while( true );
  }
code_r0x0001816072f9:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  uVar12 = (*pcVar3)();
  return uVar12;
}



/* ==== UnitCtrl::onHpChanged  @ 0x1607310  size=1 ==== */

void UnitCtrl::onHpChanged(longlong param_1,longlong param_2)

{
  undefined8 uVar1;
  code *pcVar2;
  undefined8 uVar3;
  undefined8 uVar4;
  longlong lVar5;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  
  if (cRam000000018ab63062 == '\0') {
    sub_8032f6d0(0x187b61160);
    cRam000000018ab63062 = '\x01';
  }
  if (param_2 < 1) {
    lVar5 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
    if (lVar5 == 0) goto code_r0x00018160747f;
    uStack_48 = 0x3f8000003f800000;
    uStack_40 = 0x3f800000;
    SpineController::set_CurColor(lVar5,&uStack_48,0);
    lVar5 = *(longlong *)(param_1 + 0xd0);
    if (lVar5 == 0) goto code_r0x00018160747f;
    *(undefined8 *)(lVar5 + 0xd8) = 0;
    sub_8032e8d0(lVar5 + 0xd8,0);
    if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x00018160747f;
    DEBLMMJMGID::IDENJLFHMEJ(*(longlong *)(param_1 + 0xd0),7,0,0,0,0);
    if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x00018160747f;
    *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0xbb) = 1;
    *(undefined1 *)(param_1 + 0x550) = 1;
  }
  if (*(float *)(param_1 + 0x4ec) == 0.0) {
    lVar5 = *(longlong *)(param_1 + 0x88);
    if (lVar5 == 0) {
code_r0x00018160747f:
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      (*pcVar2)();
      return;
    }
    uVar3 = *(undefined8 *)(lVar5 + 0x448);
    uVar4 = *(undefined8 *)(lVar5 + 0x450);
    uVar1 = *(undefined8 *)(lVar5 + 0x458);
    if (*(int *)(lRam0000000187b61160 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_48 = uVar3;
    uStack_40 = uVar4;
    uStack_38 = uVar1;
    lVar5 = ObscuredLong::FHOALPFLEEC(&uStack_48,0);
    *(float *)(param_1 + 0x4ec) = (float)param_2 / (float)lVar5;
  }
  return;
}



/* ==== UnitCtrl::registerAllComponentSet  @ 0x1607490  size=1 ==== */

/* WARNING: Possible PIC construction at 0x0001839dadbe: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001839dae2f: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001839daea5: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001839daecf: Changing call to branch */
/* WARNING: Possible PIC construction at 0x0001839daf45: Changing call to branch */
/* WARNING: Removing unreachable block (ram,0x0001839daed4) */
/* WARNING: Removing unreachable block (ram,0x0001839daeef) */
/* WARNING: Removing unreachable block (ram,0x0001839daf00) */
/* WARNING: Removing unreachable block (ram,0x0001839db029) */
/* WARNING: Removing unreachable block (ram,0x0001839daf10) */
/* WARNING: Removing unreachable block (ram,0x0001839daf19) */
/* WARNING: Removing unreachable block (ram,0x0001839daf27) */
/* WARNING: Removing unreachable block (ram,0x0001839daeaa) */
/* WARNING: Removing unreachable block (ram,0x0001839dae34) */
/* WARNING: Removing unreachable block (ram,0x0001839dae4f) */
/* WARNING: Removing unreachable block (ram,0x0001839dae60) */
/* WARNING: Removing unreachable block (ram,0x0001839db007) */
/* WARNING: Removing unreachable block (ram,0x0001839dae70) */
/* WARNING: Removing unreachable block (ram,0x0001839dae79) */
/* WARNING: Removing unreachable block (ram,0x0001839dae87) */
/* WARNING: Removing unreachable block (ram,0x0001839dadc3) */
/* WARNING: Removing unreachable block (ram,0x0001839dadde) */
/* WARNING: Removing unreachable block (ram,0x0001839dadf0) */
/* WARNING: Removing unreachable block (ram,0x0001839dafe5) */
/* WARNING: Removing unreachable block (ram,0x0001839dae00) */
/* WARNING: Removing unreachable block (ram,0x0001839dae09) */
/* WARNING: Removing unreachable block (ram,0x0001839dae17) */
/* WARNING: Removing unreachable block (ram,0x0001839daf4a) */
/* WARNING: Removing unreachable block (ram,0x0001839daf61) */
/* WARNING: Removing unreachable block (ram,0x0001839daf70) */
/* WARNING: Removing unreachable block (ram,0x0001839db04b) */
/* WARNING: Removing unreachable block (ram,0x0001839daf80) */
/* WARNING: Removing unreachable block (ram,0x0001839daf88) */
/* WARNING: Removing unreachable block (ram,0x0001839daf96) */

ulonglong UnitCtrl::registerAllComponentSet(longlong *param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  longlong lVar3;
  longlong lVar4;
  code *pcVar5;
  undefined8 *puVar6;
  ulonglong uVar7;
  ushort uVar8;
  uint uVar9;
  bool bVar10;
  
  if (param_1[7] != 0) {
    CLKMBEGHOLA::KMANPHBJLGK(param_1[7],param_1,0);
    if (param_1[8] != 0) {
      MGAEMKEEHND::KMANPHBJLGK(param_1[8],param_1,0);
      if (param_1[9] != 0) {
        OBAJFDJFDJG::KMANPHBJLGK(param_1[9],param_1,0);
        if (param_1[10] != 0) {
          sub_80443e50(param_1[10],param_1,0);
          if (param_1[0xb] != 0) {
            BOOMKCIDJHD::KMANPHBJLGK(param_1[0xb],param_1,0);
            if (param_1[0xc] != 0) {
              ECOCNEKAIMD::KMANPHBJLGK(param_1[0xc],param_1,0);
              if (param_1[0xd] != 0) {
                GNBAMECMKCM::KMANPHBJLGK(param_1[0xd],param_1,0);
                if (param_1[0xe] != 0) {
                  DBGNMELIABG::KMANPHBJLGK(param_1[0xe],param_1,0);
                  if (param_1[0xf] != 0) {
                    sub_80443e50(param_1[0xf],param_1,0);
                    if (param_1[0x10] != 0) {
                      sub_80443e50(param_1[0x10],param_1,0);
                      if (param_1[0x11] != 0) {
                        CIFIMDJAGGF::KMANPHBJLGK(param_1[0x11],param_1,0);
                        if (param_1[0x12] != 0) {
                          MBJPLNBCBDB::KMANPHBJLGK(param_1[0x12],param_1,0);
                          if (param_1[0x13] != 0) {
                            PBLMDEFAGBK::KMANPHBJLGK(param_1[0x13],param_1,0);
                            if (param_1[0x15] != 0) {
                              OBEOPLBHEPJ::KMANPHBJLGK(param_1[0x15],param_1,0);
                              if (param_1[0x16] != 0) {
                                sub_80443e50(param_1[0x16],param_1,0);
                                if (param_1[0x17] != 0) {
                                  NLHACDMHDNP::KMANPHBJLGK(param_1[0x17],param_1,0);
                                  if (param_1[0x18] != 0) {
                                    IDFKMEFJKOD::KMANPHBJLGK(param_1[0x18],param_1,0);
                                    if (param_1[0x19] != 0) {
                                      BCKKJALAJGO::KMANPHBJLGK(param_1[0x19],param_1,0);
                                      if (param_1[0x1a] != 0) {
                                        DEBLMMJMGID::KMANPHBJLGK(param_1[0x1a],param_1,0);
                                        lVar3 = param_1[0x1b];
                                        if (lVar3 != 0) {
                                          if (cRam000000018ab77870 == '\0') {
                                            sub_8032f6d0(0x187b5f508);
                                            cRam000000018ab77870 = '\x01';
                                          }
                                          if (param_1 == (longlong *)0x0) {
                                            sub_8032f9d0();
                                            pcVar5 = (code *)swi(3);
                                            uVar7 = (*pcVar5)();
                                            return uVar7;
                                          }
                                          lVar4 = *param_1;
                                          uVar8 = 0;
                                          if (*(ushort *)(lVar4 + 0x12e) != 0) {
                                            do {
                                              if (*(longlong *)
                                                   (*(longlong *)(lVar4 + 0xb0) +
                                                   (ulonglong)uVar8 * 0x10) == lRam0000000187b5f508)
                                              {
                                                puVar6 = (undefined8 *)
                                                         ((longlong)
                                                          *(int *)(*(longlong *)(lVar4 + 0xb0) + 8 +
                                                                  (ulonglong)uVar8 * 0x10) * 0x10 +
                                                          0x138 + lVar4);
                                                goto code_r0x00018032e8d0;
                                              }
                                              uVar8 = uVar8 + 1;
                                            } while (uVar8 < *(ushort *)(lVar4 + 0x12e));
                                          }
                                          puVar6 = (undefined8 *)
                                                   sub_8032c930(param_1,lRam0000000187b5f508,0);
code_r0x00018032e8d0:
                                          uVar7 = (*(code *)*puVar6)(param_1,puVar6[1]);
                                          *(ulonglong *)(lVar3 + 0x70) = uVar7;
                                          if (iRam000000018ab8fbd8 != 0) {
                                            uVar9 = (uint)(lVar3 + 0x70U >> 0xc);
                                            puVar1 = (ulonglong *)
                                                     ((ulonglong)((uVar9 & 0x1fffff) >> 6) * 8 +
                                                     0x18abd84e0);
                                            do {
                                              uVar2 = *puVar1;
                                              LOCK();
                                              uVar7 = *puVar1;
                                              bVar10 = uVar2 == uVar7;
                                              if (bVar10) {
                                                *puVar1 = uVar2 | 1L << (uVar9 & 0x3f);
                                                uVar7 = uVar2;
                                              }
                                              UNLOCK();
                                            } while (!bVar10);
                                          }
                                          return uVar7;
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
  sub_8032f9d0();
  pcVar5 = (code *)swi(3);
  uVar7 = (*pcVar5)();
  return uVar7;
}



/* ==== UnitCtrl::resetPosForEnemyUnit  @ 0x16076a0  size=1 ==== */

void UnitCtrl::resetPosForEnemyUnit(longlong param_1,undefined4 param_2)

{
  undefined8 uVar1;
  int iVar2;
  code *pcVar3;
  undefined4 uVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  undefined8 uVar7;
  ulonglong uVar8;
  int iVar9;
  undefined4 *puVar10;
  undefined8 *puVar11;
  longlong lVar12;
  byte bVar13;
  float fVar14;
  undefined4 uVar15;
  ulonglong in_stack_ffffffffffffff88;
  float fStack_68;
  float fStack_64;
  undefined8 uStack_58;
  ulonglong uStack_50;
  undefined8 uStack_48;
  
  if (cRam000000018ab63055 == '\0') {
    sub_8032f6d0(0x187b41650);
    sub_8032f6d0(0x187bb2838);
    sub_8032f6d0(0x187b60538);
    sub_8032f6d0(0x187b60de8);
    sub_8032f6d0(0x187be9010);
    cRam000000018ab63055 = '\x01';
  }
  if (*(int *)(lRam0000000187b41650 + 0xe4) == 0) {
    sub_8032fa70(lRam0000000187b41650);
  }
  lVar12 = *(longlong *)(*(longlong *)(lRam0000000187b41650 + 0xb8) + 8);
  if (lVar12 == 0) goto code_r0x000181607dc6;
  iVar9 = sub_84a7fb80(lVar12,param_2,uRam0000000187bb2838);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar12 == 0) goto code_r0x000181607dc6;
  iVar2 = *(int *)(lVar12 + 0x2c0);
  if (cRam000000018ab63077 == '\0') {
    sub_8032f6d0(0x187bfb7a8);
    cRam000000018ab63077 = '\x01';
  }
  lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
  if (lVar12 == 0) goto code_r0x000181607dc6;
  fStack_64 = (float)BattleManager::GetRespawnPos(lVar12,param_2,0);
  if (*(char *)(param_1 + 0x1a8) == '\0') {
    if (*(int *)(param_1 + 0x17c) == 0) {
      if (iVar2 == 6) {
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar12 == 0) goto code_r0x000181607dc6;
        if (*(int *)(lVar12 + 0x22c) == 0) {
          if (*(int *)(lRam0000000187be9010 + 0xe4) == 0) {
            sub_8032fa70(lRam0000000187be9010);
          }
          fStack_68 = *(float *)(*(longlong *)(lRam0000000187be9010 + 0xb8) + 0x38);
          goto code_r0x000181607d60;
        }
      }
      fStack_68 = (float)iVar9 * 200.0 + 560.0;
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar12 == 0) goto code_r0x000181607dc6;
      if (*(char *)(lVar12 + 0x344) != '\0') {
        lVar12 = *(longlong *)(param_1 + 0x88);
        if (lVar12 == 0) goto code_r0x000181607dc6;
        uVar7 = *(undefined8 *)(lVar12 + 0x7e0);
        uVar8 = *(ulonglong *)(lVar12 + 0x7e8);
        uVar1 = *(undefined8 *)(lVar12 + 0x7f0);
        if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
          sub_8032fa70();
        }
        uStack_58 = uVar7;
        uStack_50 = uVar8;
        uStack_48 = uVar1;
        fVar14 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_58,0);
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if ((lVar12 == 0) || (lVar12 = *(longlong *)(lVar12 + 0x110), lVar12 == 0))
        goto code_r0x000181607dc6;
        bVar13 = 1;
        fStack_68 = fVar14 * *(float *)(lVar12 + 0x33c) * 1.6 + fStack_68;
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if (lVar12 == 0) goto code_r0x000181607dc6;
        iVar9 = BattleManager::GetPurpose(lVar12,0);
        if ((iVar9 == 2) || (iVar9 == 3)) {
          lVar12 = sub_815f21c0(param_1,0);
          if (lVar12 == 0) goto code_r0x000181607dc6;
          iVar9 = BattleManager::GetInitialPosition(lVar12,*(undefined4 *)(param_1 + 0x488),0);
          fStack_68 = (float)iVar9 + 100.0;
          lVar12 = sub_815f21c0(param_1,0);
          if ((lVar12 == 0) || (lVar12 = BattleManager::GetSpecialBattleData(lVar12,0), lVar12 == 0))
          goto code_r0x000181607dc6;
          if (*(double *)(lVar12 + 0x28) == 0.0) {
            if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181607dc6;
            DEBLMMJMGID::IDENJLFHMEJ(*(longlong *)(param_1 + 0xd0),8,0,0,
                          in_stack_ffffffffffffff88 & 0xffffffffffffff00,0);
          }
          else {
            lVar12 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
            if (lVar12 == 0) goto code_r0x000181607dc6;
            uStack_58 = 0x3f8000003f800000;
            uStack_50 = 0x3f800000;
            SpineController::set_CurColor(lVar12,&uStack_58,0);
            lVar12 = *(longlong *)(param_1 + 0x88);
            if (lVar12 == 0) goto code_r0x000181607dc6;
            uVar7 = *(undefined8 *)(lVar12 + 0x7e0);
            uVar8 = *(ulonglong *)(lVar12 + 0x7e8);
            uVar1 = *(undefined8 *)(lVar12 + 0x7f0);
            if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
              sub_8032fa70();
            }
            uStack_58 = uVar7;
            uStack_50 = uVar8;
            uStack_48 = uVar1;
            uVar15 = ObscuredFloat::FHOALPFLEEC(&uStack_58,0);
            lVar12 = *(longlong *)(param_1 + 0x88);
            puVar10 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(&uStack_58,0,0);
            if (lVar12 == 0) goto code_r0x000181607dc6;
            uVar4 = puVar10[1];
            uVar5 = puVar10[2];
            uVar6 = puVar10[3];
            uVar1 = *(undefined8 *)(puVar10 + 4);
            *(undefined4 *)(lVar12 + 0x7e0) = *puVar10;
            *(undefined4 *)(lVar12 + 0x7e4) = uVar4;
            *(undefined4 *)(lVar12 + 0x7e8) = uVar5;
            *(undefined4 *)(lVar12 + 0x7ec) = uVar6;
            *(undefined8 *)(lVar12 + 0x7f0) = uVar1;
            sub_8032e8d0(lVar12 + 0x7e8,0);
            if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181607dc6;
            *(undefined1 *)(*(longlong *)(param_1 + 0xd0) + 0x169) = 1;
            if (*(longlong *)(param_1 + 0xd0) == 0) goto code_r0x000181607dc6;
            DEBLMMJMGID::IDENJLFHMEJ(*(longlong *)(param_1 + 0xd0),0,0,0,
                          in_stack_ffffffffffffff88 & 0xffffffffffffff00,0);
            if (cRam000000018ab63056 == '\0') {
              sub_8032f6d0(0x187c23628);
              cRam000000018ab63056 = '\x01';
            }
            lVar12 = sub_8032f970(uRam0000000187c23628);
            sub_80445940(lVar12,0,0);
            if (lVar12 == 0) goto code_r0x000181607dc6;
            *(longlong *)(lVar12 + 0x20) = param_1;
            sub_8032e8d0(lVar12 + 0x20,param_1);
            *(undefined4 *)(lVar12 + 0x28) = uVar15;
            UnitCtrl::AppendCoroutine(param_1,lVar12,1,0,0);
          }
          bVar13 = 0;
        }
        if ((*(byte *)(param_1 + 0x188) & bVar13) != 0) {
          if (*(longlong *)(param_1 + 0xb8) == 0) goto code_r0x000181607dc6;
          fStack_68 = *(float *)(*(longlong *)(param_1 + 0xb8) + 0x18) + 100.0;
          if (cRam000000018ab63058 == '\0') {
            sub_8032f6d0(0x187c232c0);
            cRam000000018ab63058 = '\x01';
          }
          lVar12 = sub_8032f970(uRam0000000187c232c0);
          sub_80445940(lVar12,0,0);
          if (lVar12 == 0) goto code_r0x000181607dc6;
          *(longlong *)(lVar12 + 0x20) = param_1;
          sub_8032e8d0(lVar12 + 0x20,param_1);
          UnitCtrl::AppendCoroutine(param_1,lVar12,1,0,0);
          if (*(int *)(lRam0000000187b60538 + 0xe4) == 0) {
            sub_8032fa70();
          }
          puVar11 = (undefined8 *)ObscuredBool::FHOALPFLEEC(&uStack_58,1,0);
          uVar15 = *(undefined4 *)(puVar11 + 1);
          *(undefined8 *)(param_1 + 0x4f0) = *puVar11;
          *(undefined4 *)(param_1 + 0x4f8) = uVar15;
        }
      }
    }
    else {
      fStack_68 = 1500.0;
    }
  }
  else {
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    lVar12 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (lVar12 == 0) goto code_r0x000181607dc6;
    fStack_64 = (float)BattleManager::GetRespawnPos(lVar12,8);
    fStack_64 = *(float *)(param_1 + 0x350) * 540.0 + fStack_64;
    fStack_68 = (*(float *)(param_1 + 0x34c) * 540.0 + 560.0) - -34.0;
  }
code_r0x000181607d60:
  lVar12 = FixedTransformMonoBehavior::get_transform(param_1,0);
  if (lVar12 != 0) {
    uStack_58 = CONCAT44(fStack_64,fStack_68);
    uStack_50 = uStack_50 & 0xffffffff00000000;
    FixedTransform::set_localPosition(lVar12,&uStack_58,0);
    return;
  }
code_r0x000181607dc6:
  sub_8032f9d0();
  pcVar3 = (code *)swi(3);
  (*pcVar3)();
  return;
}



/* ==== UnitCtrl::searchSpSkillLevel  @ 0x1607dd0  size=1 ==== */

ulonglong UnitCtrl::searchSpSkillLevel(longlong param_1,undefined4 param_2,longlong param_3)

{
  longlong lVar1;
  code *pcVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  uint uVar6;
  longlong lVar7;
  undefined8 uVar8;
  ulonglong uVar9;
  longlong lVar10;
  undefined8 unaff_RDI;
  
  if (cRam000000018ab6304c == '\0') {
    sub_8032f6d0(0x187c1cf68);
    sub_8032f6d0(0x187b429c8);
    sub_8032f6d0(0x187b44508);
    sub_8032f6d0(0x187b446b8);
    sub_8032f6d0(0x187b8ab40);
    sub_8032f6d0(0x187bd51e0);
    sub_8032f6d0(0x187bd5398);
    sub_8032f6d0(0x187bd5550);
    sub_8032f6d0(0x187c22bf0);
    cRam000000018ab6304c = '\x01';
  }
  lVar7 = sub_8032f970(uRam0000000187c22bf0);
  sub_804383d0(lVar7,0);
  if (((lVar7 == 0) || (*(undefined4 *)(lVar7 + 0x10) = param_2, param_3 == 0)) ||
     (lVar10 = *(longlong *)(param_3 + 0x38), lVar10 == 0)) goto code_r0x000181608127;
  lVar1 = *(longlong *)(lVar10 + 0x10);
  uVar8 = sub_8032f970(uRam0000000187b8ab40);
  sub_80747d70(uVar8,lVar7,uRam0000000187bd51e0,0);
  if (lVar1 == 0) goto code_r0x000181608127;
  iVar3 = sub_84a53b20(lVar1,uVar8,uRam0000000187b429c8);
  if (iVar3 < 0) {
code_r0x000181607f69:
    lVar1 = *(longlong *)(lVar10 + 0x28);
    uVar8 = sub_8032f970(uRam0000000187b8ab40);
    sub_80747d70(uVar8,lVar7,uRam0000000187bd5398,0);
    if (lVar1 == 0) goto code_r0x000181608127;
    iVar4 = sub_84a53b20(lVar1,uVar8,uRam0000000187b429c8);
    if (-1 < iVar4) {
      lVar1 = *(longlong *)(lVar10 + 0x58);
      if (lVar1 == 0) goto code_r0x000181608127;
      if ((iVar4 < *(int *)(lVar1 + 0x18)) &&
         (iVar3 = sub_84a610f0(lVar1,iVar4,uRam0000000187b446b8), iVar3 != 0)) {
        if (*(longlong *)(param_1 + 0x500) == 0) goto code_r0x000181608127;
        iVar5 = sub_855ac210(*(longlong *)(param_1 + 0x500),iVar3,lRam0000000187c1cf68);
        if (iVar5 == 0) goto code_r0x000181608021;
        lVar10 = *(longlong *)(param_1 + 0x500);
        goto joined_r0x00018160800c;
      }
code_r0x000181608021:
      lVar1 = *(longlong *)(lVar10 + 0x50);
      if (lVar1 == 0) goto code_r0x000181608127;
      if ((iVar4 < *(int *)(lVar1 + 0x18)) &&
         (iVar3 = sub_84a610f0(lVar1,iVar4,uRam0000000187b446b8), iVar3 != 0)) {
        if (*(longlong *)(param_1 + 0x500) == 0) goto code_r0x000181608127;
        iVar4 = sub_855ac210(*(longlong *)(param_1 + 0x500),iVar3,lRam0000000187c1cf68);
        if (iVar4 != 0) goto code_r0x000181607f32;
      }
    }
    lVar1 = *(longlong *)(lVar10 + 0x18);
    uVar8 = sub_8032f970(uRam0000000187b8ab40);
    sub_80747d70(uVar8,lVar7,uRam0000000187bd5550,0);
    if (lVar1 == 0) goto code_r0x000181608127;
    iVar3 = sub_84a53b20(lVar1,uVar8,uRam0000000187b429c8);
    if (-1 < iVar3) {
      if (*(longlong *)(lVar10 + 0x20) == 0) goto code_r0x000181608127;
      iVar3 = sub_84a610f0(*(longlong *)(lVar10 + 0x20),iVar3,uRam0000000187b446b8);
      if (iVar3 != 0) {
        if (*(longlong *)(param_1 + 0x500) == 0) goto code_r0x000181608127;
        iVar4 = sub_855ac210(*(longlong *)(param_1 + 0x500),iVar3,lRam0000000187c1cf68);
        if (iVar4 != 0) {
          lVar10 = *(longlong *)(param_1 + 0x500);
          if (lVar10 != 0) goto code_r0x000181607f4b;
          goto code_r0x000181608127;
        }
      }
    }
    lVar10 = *(longlong *)(param_1 + 0x500);
    if (lVar10 == 0) {
code_r0x000181608127:
      sub_8032f9d0();
      pcVar2 = (code *)swi(3);
      uVar9 = (*pcVar2)();
      return uVar9;
    }
    iVar3 = *(int *)(lVar7 + 0x10);
  }
  else {
    if (*(longlong *)(lVar10 + 0x48) == 0) goto code_r0x000181608127;
    iVar3 = sub_84a610f0(*(longlong *)(lVar10 + 0x48),iVar3,uRam0000000187b446b8);
    if (iVar3 == 0) goto code_r0x000181607f69;
    if (*(longlong *)(param_1 + 0x500) == 0) goto code_r0x000181608127;
    iVar4 = sub_855ac210(*(longlong *)(param_1 + 0x500),iVar3,lRam0000000187c1cf68);
    if (iVar4 == 0) goto code_r0x000181607f69;
code_r0x000181607f32:
    lVar10 = *(longlong *)(param_1 + 0x500);
joined_r0x00018160800c:
    if (lVar10 == 0) goto code_r0x000181608127;
  }
code_r0x000181607f4b:
  lVar1 = lRam0000000187c1cf68;
  lVar7 = *(longlong *)(*(longlong *)(lRam0000000187c1cf68 + 0x20) + 0xc0);
  uVar6 = sub_8556a0c0(lVar10,iVar3,*(undefined8 *)(lVar7 + 0x110),lVar7,unaff_RDI);
  if ((int)uVar6 < 0) {
    uVar8 = sub_8032e8a0(*(undefined8 *)
                                 (*(longlong *)(*(longlong *)(lVar1 + 0x20) + 0xc0) + 0x70),
                                &stack0x00000010);
    ThrowHelper::ThrowKeyNotFoundException(uVar8,0);
    return 0;
  }
  lVar7 = *(longlong *)(lVar10 + 0x18);
  if (lVar7 != 0) {
    if (uVar6 < *(uint *)(lVar7 + 0x18)) {
      return (ulonglong)*(uint *)(lVar7 + 0x2c + (longlong)(int)uVar6 * 0x10);
    }
    sub_8032f9c0();
    pcVar2 = (code *)swi(3);
    uVar9 = (*pcVar2)();
    return uVar9;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  uVar9 = (*pcVar2)();
  return uVar9;
}



/* ==== UnitCtrl::setIdleCastTime  @ 0x1608130  size=1 ==== */

void UnitCtrl::setIdleCastTime(longlong param_1)

{
  undefined8 uVar1;
  code *pcVar2;
  undefined4 uVar3;
  undefined4 uVar4;
  undefined4 uVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  char cVar8;
  undefined4 *puVar9;
  float fVar10;
  float fVar11;
  undefined8 uVar12;
  undefined8 uStack_58;
  undefined8 uStack_50;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  
  if (cRam000000018ab63072 == '\0') {
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab63072 = '\x01';
  }
  if (*(longlong *)(param_1 + 0x38) != 0) {
    uVar12 = CLKMBEGHOLA::CDICFGMBBIM(*(longlong *)(param_1 + 0x38),0);
    cVar8 = BattleUtil::Approximately(uVar12,0,0);
    if (cVar8 == '\0') {
      uVar6 = *(undefined8 *)(param_1 + 0x278);
      uVar7 = *(undefined8 *)(param_1 + 0x280);
      uVar1 = *(undefined8 *)(param_1 + 0x288);
      if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      uStack_58 = uVar6;
      uStack_50 = uVar7;
      uStack_48 = uVar1;
      fVar10 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_58,0);
      puVar9 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(&uStack_58,fVar10 / (float)uVar12,0);
    }
    else {
      if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
        sub_8032fa70();
      }
      puVar9 = (undefined4 *)ObscuredFloat::FHOALPFLEEC(&uStack_58,0x42b40000,0);
    }
    uVar3 = puVar9[1];
    uVar4 = puVar9[2];
    uVar5 = puVar9[3];
    uVar12 = *(undefined8 *)(puVar9 + 4);
    *(undefined4 *)(param_1 + 0x278) = *puVar9;
    *(undefined4 *)(param_1 + 0x27c) = uVar3;
    *(undefined4 *)(param_1 + 0x280) = uVar4;
    *(undefined4 *)(param_1 + 0x284) = uVar5;
    *(undefined8 *)(param_1 + 0x288) = uVar12;
    sub_8032e8d0(param_1 + 0x280,0);
    if (cRam000000018ab63073 == '\0') {
      sub_8032f6d0(0x187b60de8);
      cRam000000018ab63073 = '\x01';
    }
    uVar1 = *(undefined8 *)(param_1 + 0x278);
    uVar6 = *(undefined8 *)(param_1 + 0x280);
    uVar12 = *(undefined8 *)(param_1 + 0x288);
    if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
      sub_8032fa70();
    }
    uStack_48 = uVar1;
    uStack_40 = uVar6;
    uStack_38 = uVar12;
    fVar11 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_48,0);
    fVar10 = 0.0;
    if (0.0 <= fVar11) {
      fVar10 = fVar11;
    }
    *(float *)(param_1 + 0x290) = fVar10;
    return;
  }
  sub_8032f9d0();
  pcVar2 = (code *)swi(3);
  (*pcVar2)();
  return;
}



/* ==== UnitCtrl::set_ActionsTargetOnMe  @ 0x4b2120  size=19 ==== */

void UnitCtrl::set_ActionsTargetOnMe(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 400) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 400U >> 0xc);
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



/* ==== UnitCtrl::set_AnimeIdDictionary  @ 0x8cb920  size=19 ==== */

void UnitCtrl::set_AnimeIdDictionary(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x508) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x508U >> 0xc);
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



/* ==== UnitCtrl::set_AtkRecastTime  @ 0x1608250  size=9 ==== */

void UnitCtrl::set_AtkRecastTime(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4b4) = param_2;
  return;
}



/* ==== UnitCtrl::set_AtkType  @ 0x1608260  size=7 ==== */

void UnitCtrl::set_AtkType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4b0) = param_2;
  return;
}



/* ==== UnitCtrl::set_AuraEffectList  @ 0x573850  size=19 ==== */

void UnitCtrl::set_AuraEffectList(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_BaseX  @ 0x1608270  size=9 ==== */

void UnitCtrl::set_BaseX(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x260) = param_2;
  return;
}



/* ==== UnitCtrl::set_BattleRarity  @ 0x1608280  size=7 ==== */

void UnitCtrl::set_BattleRarity(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4a0) = param_2;
  return;
}



/* ==== UnitCtrl::set_BodyWidth  @ 0x1608290  size=9 ==== */

void UnitCtrl::set_BodyWidth(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x118) = param_2;
  return;
}



/* ==== UnitCtrl::set_BonusEnemyType  @ 0x564ea0  size=7 ==== */

void UnitCtrl::set_BonusEnemyType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x17c) = param_2;
  return;
}



/* ==== UnitCtrl::set_CastTimer  @ 0x16082a0  size=37 ==== */

void UnitCtrl::set_CastTimer(longlong param_1,undefined8 *param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  undefined8 uVar3;
  ulonglong uVar4;
  undefined8 uVar5;
  uint uVar6;
  
  uVar5 = param_2[1];
  uVar3 = param_2[2];
  *(undefined8 *)(param_1 + 0x278) = *param_2;
  *(undefined8 *)(param_1 + 0x280) = uVar5;
  *(undefined8 *)(param_1 + 0x288) = uVar3;
  if (iRam000000018ab8fbd8 != 0) {
    uVar6 = (uint)(param_1 + 0x280U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar6 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar4 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar4 == uVar2) {
        *puVar1 = uVar4 | 1L << (uVar6 & 0x3f);
      }
      UNLOCK();
    } while (uVar4 != uVar2);
  }
  return;
}



/* ==== UnitCtrl::set_CenterBone  @ 0x571bf0  size=19 ==== */

void UnitCtrl::set_CenterBone(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xf0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xf0U >> 0xc);
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



/* ==== UnitCtrl::set_CenterBoneModeChange  @ 0x571bb0  size=19 ==== */

void UnitCtrl::set_CenterBoneModeChange(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xf8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xf8U >> 0xc);
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



/* ==== UnitCtrl::set_CenterForDamageNumBone  @ 0x8dbce0  size=19 ==== */

void UnitCtrl::set_CenterForDamageNumBone(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x1f8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x1f8U >> 0xc);
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



/* ==== UnitCtrl::set_CharacterUnitId  @ 0x16082d0  size=7 ==== */

void UnitCtrl::set_CharacterUnitId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x48c) = param_2;
  return;
}



/* ==== UnitCtrl::set_CircleEffectList  @ 0x571b20  size=19 ==== */

void UnitCtrl::set_CircleEffectList(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ColliderCenter  @ 0x16082e0  size=22 ==== */

void UnitCtrl::set_ColliderCenter(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x218) = *param_2;
  *(undefined4 *)(param_1 + 0x220) = uVar1;
  return;
}



/* ==== UnitCtrl::set_ColliderSize  @ 0x1608300  size=22 ==== */

void UnitCtrl::set_ColliderSize(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x224) = *param_2;
  *(undefined4 *)(param_1 + 0x22c) = uVar1;
  return;
}



/* ==== UnitCtrl::set_ComponentAbnormal  @ 0x437860  size=13 ==== */

void UnitCtrl::set_ComponentAbnormal(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x38) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x38U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentActionPattern  @ 0x437d80  size=13 ==== */

void UnitCtrl::set_ComponentActionPattern(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x40) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x40U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentAnimation  @ 0x437830  size=13 ==== */

void UnitCtrl::set_ComponentAnimation(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentAnnihilation  @ 0x437850  size=13 ==== */

void UnitCtrl::set_ComponentAnnihilation(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x50) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x50U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentBattleResult  @ 0x437840  size=13 ==== */

void UnitCtrl::set_ComponentBattleResult(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentColor  @ 0x4388d0  size=13 ==== */

void UnitCtrl::set_ComponentColor(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x60) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x60U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentCompare  @ 0x4388f0  size=13 ==== */

void UnitCtrl::set_ComponentCompare(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x68) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x68U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentDamageControl  @ 0x4388e0  size=13 ==== */

void UnitCtrl::set_ComponentDamageControl(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x70) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x70U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentLog  @ 0x438cc0  size=13 ==== */

void UnitCtrl::set_ComponentLog(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x78) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x78U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentMultiPartsBoss  @ 0x438e60  size=16 ==== */

void UnitCtrl::set_ComponentMultiPartsBoss(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x80) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x80U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentParameter  @ 0x438e40  size=19 ==== */

void UnitCtrl::set_ComponentParameter(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentParameterClamped  @ 0x438c80  size=19 ==== */

void UnitCtrl::set_ComponentParameterClamped(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentPassiveSkill  @ 0x438e20  size=19 ==== */

void UnitCtrl::set_ComponentPassiveSkill(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentRelicPassiveSkill  @ 0x44ad70  size=19 ==== */

void UnitCtrl::set_ComponentRelicPassiveSkill(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentRelicSkill  @ 0x438e80  size=19 ==== */

void UnitCtrl::set_ComponentRelicSkill(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentScaleChange  @ 0x44ad90  size=19 ==== */

void UnitCtrl::set_ComponentScaleChange(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentSearchArea  @ 0x69dc70  size=19 ==== */

void UnitCtrl::set_ComponentSearchArea(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xb8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xb8U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentSortOrder  @ 0x438e00  size=19 ==== */

void UnitCtrl::set_ComponentSortOrder(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xc0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xc0U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentSound  @ 0x438cd0  size=19 ==== */

void UnitCtrl::set_ComponentSound(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 200) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 200U >> 0xc);
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



/* ==== UnitCtrl::set_ComponentState  @ 0x5e8be0  size=19 ==== */

void UnitCtrl::set_ComponentState(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ComponentUnionBurst  @ 0x5d47f0  size=19 ==== */

void UnitCtrl::set_ComponentUnionBurst(longlong param_1,undefined8 param_2)

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



/* ==== UnitCtrl::set_ConnectRank  @ 0x1608330  size=7 ==== */

void UnitCtrl::set_ConnectRank(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4a8) = param_2;
  return;
}



/* ==== UnitCtrl::set_ConnectRankLimit  @ 0x1608320  size=7 ==== */

void UnitCtrl::set_ConnectRankLimit(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4ac) = param_2;
  return;
}



/* ==== UnitCtrl::set_CurrentSkillId  @ 0x1608340  size=7 ==== */

void UnitCtrl::set_CurrentSkillId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x230) = param_2;
  return;
}



/* ==== UnitCtrl::set_CurrentSpineSpeedCoroutineId  @ 0x1608350  size=7 ==== */

void UnitCtrl::set_CurrentSpineSpeedCoroutineId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x1e4) = param_2;
  return;
}



/* ==== UnitCtrl::set_CutInFrameSet  @ 0x579e30  size=19 ==== */

void UnitCtrl::set_CutInFrameSet(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x1c0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x1c0U >> 0xc);
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



/* ==== UnitCtrl::set_DamagedHpEffectDictionary  @ 0x575ac0  size=19 ==== */

void UnitCtrl::set_DamagedHpEffectDictionary(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x110) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x110U >> 0xc);
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



/* ==== UnitCtrl::set_DisableSortOrderFrontOnBlackoutTarget  @ 0x1608360  size=7 ==== */

void UnitCtrl::set_DisableSortOrderFrontOnBlackoutTarget(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x250) = param_2;
  return;
}



/* ==== UnitCtrl::set_EffectDifficulty  @ 0x13497f0  size=7 ==== */

void UnitCtrl::set_EffectDifficulty(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x2e0) = param_2;
  return;
}



/* ==== UnitCtrl::set_EffectSpineControllerList  @ 0x8dbe20  size=19 ==== */

void UnitCtrl::set_EffectSpineControllerList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 600) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 600U >> 0xc);
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



/* ==== UnitCtrl::set_FirearmCtrlsOnMe  @ 0x565310  size=19 ==== */

void UnitCtrl::set_FirearmCtrlsOnMe(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x198) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x198U >> 0xc);
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



/* ==== UnitCtrl::set_FixedStatePos  @ 0x1608370  size=22 ==== */

void UnitCtrl::set_FixedStatePos(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x20c) = *param_2;
  *(undefined4 *)(param_1 + 0x214) = uVar1;
  return;
}



/* ==== UnitCtrl::set_HasCenterForDamageNumBone  @ 0x1608390  size=7 ==== */

void UnitCtrl::set_HasCenterForDamageNumBone(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 500) = param_2;
  return;
}



/* ==== UnitCtrl::set_HasSpecialDie  @ 0xe26340  size=7 ==== */

void UnitCtrl::set_HasSpecialDie(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x240) = param_2;
  return;
}



/* ==== UnitCtrl::set_IconSkin  @ 0x16083a0  size=7 ==== */

void UnitCtrl::set_IconSkin(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x498) = param_2;
  return;
}



/* ==== UnitCtrl::set_IdentifyNum  @ 0x16083b0  size=7 ==== */

void UnitCtrl::set_IdentifyNum(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x484) = param_2;
  return;
}



/* ==== UnitCtrl::set_Index  @ 0x16083c0  size=7 ==== */

void UnitCtrl::set_Index(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x480) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsAbyssBossEnemy  @ 0x16083d0  size=7 ==== */

void UnitCtrl::set_IsAbyssBossEnemy(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x1ab) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsAwakeMotion  @ 0x16083e0  size=7 ==== */

void UnitCtrl::set_IsAwakeMotion(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x2dc) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsBoss  @ 0x777970  size=7 ==== */

void UnitCtrl::set_IsBoss(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x1a8) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsClanBattleOrSekaiEnemy  @ 0x16083f0  size=7 ==== */

void UnitCtrl::set_IsClanBattleOrSekaiEnemy(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x1a9) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsDeadBySetCurrentHp  @ 0x1608400  size=7 ==== */

void UnitCtrl::set_IsDeadBySetCurrentHp(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x550) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsDepthBack  @ 0x1608410  size=7 ==== */

void UnitCtrl::set_IsDepthBack(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x270) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsDivisionSourceForDamage  @ 0x5fee00  size=7 ==== */

void UnitCtrl::set_IsDivisionSourceForDamage(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x186) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsDivisionSourceForDie  @ 0x1608420  size=7 ==== */

void UnitCtrl::set_IsDivisionSourceForDie(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x187) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsGuest  @ 0x5fee40  size=7 ==== */

void UnitCtrl::set_IsGuest(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x184) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsGuestFadeout  @ 0x5feda0  size=7 ==== */

void UnitCtrl::set_IsGuestFadeout(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x185) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsLeftDir  @ 0x1608430  size=7 ==== */

void UnitCtrl::set_IsLeftDir(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x165) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsMoveSpeedForceZero  @ 0x1608440  size=22 ==== */

void UnitCtrl::set_IsMoveSpeedForceZero(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x4f0) = *param_2;
  *(undefined4 *)(param_1 + 0x4f8) = uVar1;
  return;
}



/* ==== UnitCtrl::set_IsOther  @ 0x5b2170  size=7 ==== */

void UnitCtrl::set_IsOther(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x178) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsRecreated  @ 0x48f4c0  size=7 ==== */

void UnitCtrl::set_IsRecreated(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x150) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsShadow  @ 0x69c250  size=7 ==== */

void UnitCtrl::set_IsShadow(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x188) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsStartSkillExeced  @ 0x1608460  size=7 ==== */

void UnitCtrl::set_IsStartSkillExeced(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x2de) = param_2;
  return;
}



/* ==== UnitCtrl::set_IsTrialBattleEnemy  @ 0x1608470  size=7 ==== */

void UnitCtrl::set_IsTrialBattleEnemy(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x1aa) = param_2;
  return;
}



/* ==== UnitCtrl::set_LevelFixedSpSkillIdList  @ 0x8cb800  size=19 ==== */

void UnitCtrl::set_LevelFixedSpSkillIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x4c0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x4c0U >> 0xc);
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



/* ==== UnitCtrl::set_LifeGauge  @ 0x8dbe40  size=19 ==== */

void UnitCtrl::set_LifeGauge(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x268) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x268U >> 0xc);
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



/* ==== UnitCtrl::set_MainSkill1EvolutionType  @ 0x135a750  size=7 ==== */

void UnitCtrl::set_MainSkill1EvolutionType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x154) = param_2;
  return;
}



/* ==== UnitCtrl::set_MainSkill2EvolutionType  @ 0x135a740  size=7 ==== */

void UnitCtrl::set_MainSkill2EvolutionType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x158) = param_2;
  return;
}



/* ==== UnitCtrl::set_MainSkillEvolutionIdList  @ 0x1608480  size=19 ==== */

void UnitCtrl::set_MainSkillEvolutionIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x520) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x520U >> 0xc);
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



/* ==== UnitCtrl::set_MainSkillIdList  @ 0x8cb8a0  size=19 ==== */

void UnitCtrl::set_MainSkillIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x510) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x510U >> 0xc);
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



/* ==== UnitCtrl::set_MainSkillRevolutionIdList  @ 0x16084a0  size=19 ==== */

void UnitCtrl::set_MainSkillRevolutionIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x530) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x530U >> 0xc);
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



/* ==== UnitCtrl::set_MotionPrefix  @ 0x5fedb0  size=7 ==== */

void UnitCtrl::set_MotionPrefix(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x1a0) = param_2;
  return;
}



/* ==== UnitCtrl::set_MoveRate  @ 0x16084c0  size=9 ==== */

void UnitCtrl::set_MoveRate(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x2d8) = param_2;
  return;
}



/* ==== UnitCtrl::set_OnInitCallbackForUi  @ 0x8dbf80  size=19 ==== */

void UnitCtrl::set_OnInitCallbackForUi(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x2e8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x2e8U >> 0xc);
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



/* ==== UnitCtrl::set_OnIsFrontFalse  @ 0x635810  size=19 ==== */

void UnitCtrl::set_OnIsFrontFalse(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x238) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x238U >> 0xc);
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



/* ==== UnitCtrl::set_OriginalUnitId  @ 0x16084d0  size=7 ==== */

void UnitCtrl::set_OriginalUnitId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x490) = param_2;
  return;
}



/* ==== UnitCtrl::set_OverlapPosX  @ 0x16084e0  size=9 ==== */

void UnitCtrl::set_OverlapPosX(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x18c) = param_2;
  return;
}



/* ==== UnitCtrl::set_PartsMotionPrefix  @ 0x5fee50  size=7 ==== */

void UnitCtrl::set_PartsMotionPrefix(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x1a4) = param_2;
  return;
}



/* ==== UnitCtrl::set_Pause  @ 0x16084f0  size=1 ==== */

void UnitCtrl::set_Pause(longlong param_1,char param_2)

{
  code *pcVar1;
  char cVar2;
  undefined8 uVar3;
  longlong lVar4;
  
  *(char *)(param_1 + 0x460) = param_2;
  if (param_2 == '\0') {
    if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x00018160875a;
    cVar2 = CLKMBEGHOLA::DIPBIJJPKCO(*(longlong *)(param_1 + 0x38),0);
    if (cVar2 != '\0') {
      if (*(longlong *)(param_1 + 0x38) == 0) goto code_r0x00018160875a;
      cVar2 = CLKMBEGHOLA::HHEBMDIBDHH(*(longlong *)(param_1 + 0x38),0);
      if (cVar2 == '\0') goto code_r0x00018160871d;
    }
    if (cRam000000018ab6305e == '\0') {
      sub_8032f6d0(0x187b5e978);
      cRam000000018ab6305e = '\x01';
    }
    uVar3 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar2 = Object::op_Inequality(uVar3,0,0);
    if (cVar2 != '\0') {
      lVar4 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
      if (lVar4 == 0) goto code_r0x00018160875a;
      BattleUnitBaseSpineController::Resume(lVar4,0);
    }
  }
  else {
    if (cRam000000018ab6305f == '\0') {
      sub_8032f6d0(0x187b71e50);
      sub_8032f6d0(0x187b5e978);
      cRam000000018ab6305f = '\x01';
    }
    if (cRam000000018ab63077 == '\0') {
      sub_8032f6d0(0x187bfb7a8);
      cRam000000018ab63077 = '\x01';
    }
    uVar3 = *(undefined8 *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
    if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
      sub_8032fa70();
    }
    cVar2 = Object::op_Equality(uVar3,0,0);
    if (cVar2 == '\0') {
      if (cRam000000018ab63077 == '\0') {
        sub_8032f6d0(0x187bfb7a8);
        cRam000000018ab63077 = '\x01';
      }
      lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
      if (lVar4 == 0) goto code_r0x00018160875a;
      if (*(longlong *)(lVar4 + 800) != 0) {
        if (cRam000000018ab63077 == '\0') {
          sub_8032f6d0(0x187bfb7a8);
          cRam000000018ab63077 = '\x01';
        }
        lVar4 = *(longlong *)(*(longlong *)(lRam0000000187bfb7a8 + 0xb8) + 0x28);
        if ((lVar4 == 0) || (lVar4 = *(longlong *)(lVar4 + 800), lVar4 == 0))
        goto code_r0x00018160875a;
        cVar2 = sub_84a98ab0(lVar4,param_1,uRam0000000187b71e50);
        if (cVar2 == '\0') {
          uVar3 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
          if (*(int *)(lRam0000000187b5e978 + 0xe4) == 0) {
            sub_8032fa70();
          }
          cVar2 = Object::op_Inequality(uVar3,0,0);
          if (cVar2 != '\0') {
            lVar4 = UnitCtrl::GetCurrentSpineCtrl(param_1,0);
            if (lVar4 == 0) goto code_r0x00018160875a;
            BattleUnitBaseSpineController::Pause(lVar4,0);
          }
        }
      }
    }
  }
code_r0x00018160871d:
  if (*(longlong *)(param_1 + 200) != 0) {
    BCKKJALAJGO::BBMECDPHGKJ(*(longlong *)(param_1 + 200),*(undefined1 *)(param_1 + 0x460),0);
    lVar4 = *(longlong *)(param_1 + 0x30);
    if (lVar4 != 0) {
      if (*(longlong *)(lVar4 + 0x28) != 0) {
        if (*(char *)(*(longlong *)(lVar4 + 0x28) + 0x5c9) != '\0') {
          if (*(longlong *)(lVar4 + 0x20) == 0) goto code_r0x0001837cc0ee;
          MovieManager::Pause(*(longlong *)(lVar4 + 0x20),*(undefined4 *)(lVar4 + 0x40),
                        *(undefined8 *)(lVar4 + 0x48),*(undefined1 *)(param_1 + 0x460),
                        *(undefined8 *)(lVar4 + 0x50),1,0);
        }
        return;
      }
code_r0x0001837cc0ee:
      sub_8032f9d0(lVar4,*(undefined1 *)(param_1 + 0x460),0);
      pcVar1 = (code *)swi(3);
      (*pcVar1)();
      return;
    }
  }
code_r0x00018160875a:
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  (*pcVar1)();
  return;
}



/* ==== UnitCtrl::set_PrefabId  @ 0x135a760  size=7 ==== */

void UnitCtrl::set_PrefabId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x15c) = param_2;
  return;
}



/* ==== UnitCtrl::set_PrincessFormProcessor  @ 0x437d90  size=13 ==== */

void UnitCtrl::set_PrincessFormProcessor(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x30) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x30U >> 0xc);
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



/* ==== UnitCtrl::set_PromotionLevel  @ 0x1608760  size=7 ==== */

void UnitCtrl::set_PromotionLevel(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4a4) = param_2;
  return;
}



/* ==== UnitCtrl::set_Rarity  @ 0x1608770  size=7 ==== */

void UnitCtrl::set_Rarity(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x49c) = param_2;
  return;
}



/* ==== UnitCtrl::set_RepeatEffectList  @ 0x571bd0  size=19 ==== */

void UnitCtrl::set_RepeatEffectList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x100) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x100U >> 0xc);
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



/* ==== UnitCtrl::set_RespawnPos  @ 0x5389f0  size=7 ==== */

void UnitCtrl::set_RespawnPos(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x168) = param_2;
  return;
}



/* ==== UnitCtrl::set_RewardCount  @ 0x1608780  size=22 ==== */

void UnitCtrl::set_RewardCount(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x4d4) = *param_2;
  *(undefined4 *)(param_1 + 0x4dc) = uVar1;
  return;
}



/* ==== UnitCtrl::set_RoleId  @ 0x16087a0  size=7 ==== */

void UnitCtrl::set_RoleId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4e8) = param_2;
  return;
}



/* ==== UnitCtrl::set_RotateCenter  @ 0x66a6b0  size=19 ==== */

void UnitCtrl::set_RotateCenter(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x1d8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x1d8U >> 0xc);
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



/* ==== UnitCtrl::set_Rupee  @ 0x16087b0  size=22 ==== */

void UnitCtrl::set_Rupee(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x4c8) = *param_2;
  *(undefined4 *)(param_1 + 0x4d0) = uVar1;
  return;
}



/* ==== UnitCtrl::set_SDSkin  @ 0x16087d0  size=7 ==== */

void UnitCtrl::set_SDSkin(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x494) = param_2;
  return;
}



/* ==== UnitCtrl::set_SkeletonRenderSeparator  @ 0x573830  size=19 ==== */

void UnitCtrl::set_SkeletonRenderSeparator(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x130) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x130U >> 0xc);
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



/* ==== UnitCtrl::set_SkillEnableFrame  @ 0x684730  size=7 ==== */

void UnitCtrl::set_SkillEnableFrame(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x1c8) = param_2;
  return;
}



/* ==== UnitCtrl::set_SkillLevels  @ 0x8cb8e0  size=19 ==== */

void UnitCtrl::set_SkillLevels(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x500) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x500U >> 0xc);
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



/* ==== UnitCtrl::set_SkillStackVal  @ 0x1608810  size=35 ==== */

void UnitCtrl::set_SkillStackVal(longlong param_1,undefined8 *param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  uint uVar7;
  
  uVar4 = param_2[1];
  uVar5 = param_2[2];
  uVar6 = param_2[3];
  *(undefined8 *)(param_1 + 0x2b8) = *param_2;
  *(undefined8 *)(param_1 + 0x2c0) = uVar4;
  *(undefined8 *)(param_1 + 0x2c8) = uVar5;
  *(undefined8 *)(param_1 + 0x2d0) = uVar6;
  if (iRam000000018ab8fbd8 != 0) {
    uVar7 = (uint)(param_1 + 0x2c8U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar7 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar7 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== UnitCtrl::set_SkillStackValDmg  @ 0x16087e0  size=35 ==== */

void UnitCtrl::set_SkillStackValDmg(longlong param_1,undefined8 *param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  undefined8 uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  uint uVar7;
  
  uVar4 = param_2[1];
  uVar5 = param_2[2];
  uVar6 = param_2[3];
  *(undefined8 *)(param_1 + 0x298) = *param_2;
  *(undefined8 *)(param_1 + 0x2a0) = uVar4;
  *(undefined8 *)(param_1 + 0x2a8) = uVar5;
  *(undefined8 *)(param_1 + 0x2b0) = uVar6;
  if (iRam000000018ab8fbd8 != 0) {
    uVar7 = (uint)(param_1 + 0x2a8U >> 0xc);
    puVar1 = (ulonglong *)((ulonglong)((uVar7 & 0x1fffff) >> 6) * 8 + 0x18abd84e0);
    do {
      uVar3 = *puVar1;
      LOCK();
      uVar2 = *puVar1;
      if (uVar3 == uVar2) {
        *puVar1 = uVar3 | 1L << (uVar7 & 0x3f);
      }
      UNLOCK();
    } while (uVar3 != uVar2);
  }
  return;
}



/* ==== UnitCtrl::set_SkinRarity  @ 0x1364760  size=7 ==== */

void UnitCtrl::set_SkinRarity(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x254) = param_2;
  return;
}



/* ==== UnitCtrl::set_SpecialSkillEvolutionIdList  @ 0x1608840  size=19 ==== */

void UnitCtrl::set_SpecialSkillEvolutionIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x528) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x528U >> 0xc);
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



/* ==== UnitCtrl::set_SpecialSkillIdList  @ 0x8cb940  size=19 ==== */

void UnitCtrl::set_SpecialSkillIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x518) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x518U >> 0xc);
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



/* ==== UnitCtrl::set_SpecialSkillRevolutionIdList  @ 0x1608860  size=19 ==== */

void UnitCtrl::set_SpecialSkillRevolutionIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x538) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x538U >> 0xc);
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



/* ==== UnitCtrl::set_SpecialVoiceTimingData  @ 0x8dbd00  size=19 ==== */

void UnitCtrl::set_SpecialVoiceTimingData(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x248) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x248U >> 0xc);
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



/* ==== UnitCtrl::set_StartDashDone  @ 0x1608880  size=7 ==== */

void UnitCtrl::set_StartDashDone(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x2dd) = param_2;
  return;
}



/* ==== UnitCtrl::set_StartHpPercent  @ 0x1608890  size=9 ==== */

void UnitCtrl::set_StartHpPercent(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4ec) = param_2;
  return;
}



/* ==== UnitCtrl::set_StateBone  @ 0x5d47b0  size=19 ==== */

void UnitCtrl::set_StateBone(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xe0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xe0U >> 0xc);
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



/* ==== UnitCtrl::set_StateBoneModeChange  @ 0x575a70  size=19 ==== */

void UnitCtrl::set_StateBoneModeChange(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0xe8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0xe8U >> 0xc);
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



/* ==== UnitCtrl::set_SubUnionBurstIdList  @ 0x16088a0  size=19 ==== */

void UnitCtrl::set_SubUnionBurstIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x540) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x540U >> 0xc);
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



/* ==== UnitCtrl::set_SummonRespawnPos  @ 0x538a20  size=7 ==== */

void UnitCtrl::set_SummonRespawnPos(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x16c) = param_2;
  return;
}



/* ==== UnitCtrl::set_SummonSource  @ 0x579e50  size=19 ==== */

void UnitCtrl::set_SummonSource(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x1b8) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x1b8U >> 0xc);
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



/* ==== UnitCtrl::set_SummonType  @ 0x5b20b0  size=7 ==== */

void UnitCtrl::set_SummonType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x180) = param_2;
  return;
}



/* ==== UnitCtrl::set_SummonUnitDictionary  @ 0x579eb0  size=19 ==== */

void UnitCtrl::set_SummonUnitDictionary(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x1b0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x1b0U >> 0xc);
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



/* ==== UnitCtrl::set_TalentIds  @ 0x8cb7e0  size=19 ==== */

void UnitCtrl::set_TalentIds(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x4e0) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x4e0U >> 0xc);
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



/* ==== UnitCtrl::set_TimeToDie  @ 0x119ea40  size=7 ==== */

void UnitCtrl::set_TimeToDie(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x164) = param_2;
  return;
}



/* ==== UnitCtrl::set_TreasureAnimeIdList  @ 0x16088c0  size=19 ==== */

void UnitCtrl::set_TreasureAnimeIdList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x548) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x548U >> 0xc);
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



/* ==== UnitCtrl::set_UnionBurstSkillId  @ 0x16088e0  size=7 ==== */

void UnitCtrl::set_UnionBurstSkillId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x4b8) = param_2;
  return;
}



/* ==== UnitCtrl::set_UnitActionController  @ 0x437d70  size=13 ==== */

void UnitCtrl::set_UnitActionController(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x28) = param_2;
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



/* ==== UnitCtrl::set_UnitId  @ 0x16088f0  size=7 ==== */

void UnitCtrl::set_UnitId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x488) = param_2;
  return;
}



/* ==== UnitCtrl::set_UnitInstanceId  @ 0x7cfd30  size=7 ==== */

void UnitCtrl::set_UnitInstanceId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x160) = param_2;
  return;
}



/* ==== UnitCtrl::set_UnitParameter  @ 0x4b51a0  size=19 ==== */

void UnitCtrl::set_UnitParameter(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x170) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x170U >> 0xc);
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



/* ==== UnitCtrl::set_UnitSpineCtrl  @ 0x573810  size=19 ==== */

void UnitCtrl::set_UnitSpineCtrl(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x128) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x128U >> 0xc);
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



/* ==== UnitCtrl::set_UnitSpineCtrlModeChange  @ 0x571b60  size=19 ==== */

void UnitCtrl::set_UnitSpineCtrlModeChange(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x138) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x138U >> 0xc);
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



/* ==== UnitCtrl::set_UnitUnionBurstTimelineList  @ 0x5c5230  size=19 ==== */

void UnitCtrl::set_UnitUnionBurstTimelineList(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x140) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x140U >> 0xc);
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



/* ==== UnitCtrl::set_WeaponMotionType  @ 0x5c5220  size=7 ==== */

void UnitCtrl::set_WeaponMotionType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x120) = param_2;
  return;
}



/* ==== UnitCtrl::set_WeaponSeType  @ 0x5d8db0  size=7 ==== */

void UnitCtrl::set_WeaponSeType(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x11c) = param_2;
  return;
}



/* ==== UnitCtrl::set_bottomTransform  @ 0x9e32d0  size=19 ==== */

void UnitCtrl::set_bottomTransform(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x458) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x458U >> 0xc);
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



/* ==== UnitCtrl::set_castTimeDictionary  @ 0x9d74f0  size=19 ==== */

void UnitCtrl::set_castTimeDictionary(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x478) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x478U >> 0xc);
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



/* ==== UnitCtrl::set_fixedCenterForDamageNumPos  @ 0x1608900  size=22 ==== */

void UnitCtrl::set_fixedCenterForDamageNumPos(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x200) = *param_2;
  *(undefined4 *)(param_1 + 0x208) = uVar1;
  return;
}



/* ==== UnitCtrl::set_fixedCenterPos  @ 0x1608920  size=22 ==== */

void UnitCtrl::set_fixedCenterPos(longlong param_1,undefined8 *param_2)

{
  undefined4 uVar1;
  
  uVar1 = *(undefined4 *)(param_2 + 1);
  *(undefined8 *)(param_1 + 0x1e8) = *param_2;
  *(undefined4 *)(param_1 + 0x1f0) = uVar1;
  return;
}



/* ==== UnitCtrl::set_isPaused  @ 0x1608940  size=7 ==== */

void UnitCtrl::set_isPaused(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x460) = param_2;
  return;
}



/* ==== UnitCtrl::set_leftDirScale  @ 0x1608950  size=8 ==== */

void UnitCtrl::set_leftDirScale(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0x464) = param_2;
  return;
}



/* ==== UnitCtrl::set_motionPrefix  @ 0x5fedb0  size=7 ==== */

void UnitCtrl::set_motionPrefix(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x1a0) = param_2;
  return;
}



/* ==== UnitCtrl::set_rightDirScale  @ 0x1608960  size=8 ==== */

void UnitCtrl::set_rightDirScale(longlong param_1,undefined8 param_2)

{
  *(undefined8 *)(param_1 + 0x46c) = param_2;
  return;
}



/* ==== UnitCtrl::set_unionBurstSkillAreaWidth  @ 0x1608970  size=9 ==== */

void UnitCtrl::set_unionBurstSkillAreaWidth(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x474) = param_2;
  return;
}



/* ==== UnitCtrl::updateActionWaitTimerMax  @ 0x1608980  size=1 ==== */

void UnitCtrl::updateActionWaitTimerMax(longlong param_1)

{
  undefined8 uVar1;
  undefined8 uVar2;
  undefined8 uVar3;
  float fVar4;
  float fVar5;
  undefined8 uStack_48;
  undefined8 uStack_40;
  undefined8 uStack_38;
  
  if (cRam000000018ab63073 == '\0') {
    sub_8032f6d0(0x187b60de8);
    cRam000000018ab63073 = '\x01';
  }
  uVar2 = *(undefined8 *)(param_1 + 0x278);
  uVar3 = *(undefined8 *)(param_1 + 0x280);
  uVar1 = *(undefined8 *)(param_1 + 0x288);
  if (*(int *)(lRam0000000187b60de8 + 0xe4) == 0) {
    sub_8032fa70();
  }
  uStack_48 = uVar2;
  uStack_40 = uVar3;
  uStack_38 = uVar1;
  fVar4 = (float)ObscuredFloat::FHOALPFLEEC(&uStack_48,0);
  fVar5 = 0.0;
  if (0.0 <= fVar4) {
    fVar5 = fVar4;
  }
  *(float *)(param_1 + 0x290) = fVar5;
  return;
}



/* ==== UnitCtrl::updateAttackTarget  @ 0x1608a10  size=1 ==== */

longlong UnitCtrl::updateAttackTarget(undefined8 param_1)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab63050 == '\0') {
    sub_8032f6d0(0x187c22f58);
    cRam000000018ab63050 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c22f58);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitCtrl::updateChangeSkillNum  @ 0x1608a80  size=1 ==== */

longlong UnitCtrl::updateChangeSkillNum(undefined8 param_1,undefined4 param_2,undefined4 param_3)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab63060 == '\0') {
    sub_8032f6d0(0x187c23108);
    cRam000000018ab63060 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c23108);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    *(undefined4 *)(lVar2 + 0x28) = param_3;
    *(undefined4 *)(lVar2 + 0x2c) = param_2;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitCtrl::waitBossMotionEnd  @ 0x1608b10  size=1 ==== */

longlong UnitCtrl::waitBossMotionEnd(undefined8 param_1)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab63058 == '\0') {
    sub_8032f6d0(0x187c232c0);
    cRam000000018ab63058 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c232c0);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitCtrl::waitCargeEnergy  @ 0x1608b80  size=1 ==== */

longlong UnitCtrl::waitCargeEnergy(undefined8 param_1,undefined8 param_2)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab6305b == '\0') {
    sub_8032f6d0(0x187c23470);
    cRam000000018ab6305b = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c23470);
  sub_80445940(lVar2,0,0);
  if (lVar2 != 0) {
    *(undefined8 *)(lVar2 + 0x20) = param_1;
    sub_8032e8d0(lVar2 + 0x20,param_1);
    *(undefined8 *)(lVar2 + 0x28) = param_2;
    return lVar2;
  }
  sub_8032f9d0();
  pcVar1 = (code *)swi(3);
  lVar2 = (*pcVar1)();
  return lVar2;
}



/* ==== UnitCtrl::waitShadowAppear  @ 0x1608c00  size=1 ==== */

longlong UnitCtrl::waitShadowAppear(undefined8 param_1,undefined4 param_2)

{
  code *pcVar1;
  longlong lVar2;
  
  if (cRam000000018ab63056 == '\0') {
    sub_8032f6d0(0x187c23628);
    cRam000000018ab63056 = '\x01';
  }
  lVar2 = sub_8032f970(uRam0000000187c23628);
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



