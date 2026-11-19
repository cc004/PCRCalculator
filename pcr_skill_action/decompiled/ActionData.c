/* ==== ActionData::.ctor  @ 0x15d3cc0  size=1 ==== */

ulonglong ActionData::_ctor(longlong param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  sub_80443e50(param_1,0);
  uVar3 = 0;
  *(undefined4 *)(param_1 + 0x10) = 0;
  *(undefined8 *)(param_1 + 0x18) = 0;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x18U >> 0xc);
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



/* ==== ActionData::SetActionId  @ 0x43abe0  size=4 ==== */

void ActionData::SetActionId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x10) = param_2;
  return;
}



/* ==== ActionData::SetActionValue  @ 0x437900  size=13 ==== */

void ActionData::SetActionValue(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x18) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x18U >> 0xc);
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



/* ==== ActionData::get_ActionId  @ 0x437320  size=4 ==== */

undefined4 ActionData::get_ActionId(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x10);
}



/* ==== ActionData::get_ActionValue  @ 0x4378e0  size=5 ==== */

undefined8 ActionData::get_ActionValue(longlong param_1)

{
  return *(undefined8 *)(param_1 + 0x18);
}



/* ==== ActionData::initializeActionData  @ 0x15d3cf0  size=20 ==== */

ulonglong ActionData::initializeActionData(longlong param_1)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  bool bVar5;
  
  uVar3 = 0;
  *(undefined4 *)(param_1 + 0x10) = 0;
  *(undefined8 *)(param_1 + 0x18) = 0;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x18U >> 0xc);
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



/* ==== ActionData::set_ActionId  @ 0x43abe0  size=4 ==== */

void ActionData::set_ActionId(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x10) = param_2;
  return;
}



/* ==== ActionData::set_ActionValue  @ 0x437900  size=13 ==== */

void ActionData::set_ActionValue(longlong param_1,undefined8 param_2)

{
  ulonglong *puVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  uint uVar4;
  
  *(undefined8 *)(param_1 + 0x18) = param_2;
  if (iRam000000018ab8fbd8 != 0) {
    uVar4 = (uint)(param_1 + 0x18U >> 0xc);
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



