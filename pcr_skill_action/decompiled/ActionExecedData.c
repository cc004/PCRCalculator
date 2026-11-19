/* ==== ActionExecedData::.ctor  @ 0x4383d0  size=7 ==== */

void ActionExecedData::_ctor(void)

{
  return;
}



/* ==== ActionExecedData::get_AlreadyExeced  @ 0x4bd500  size=5 ==== */

undefined1 ActionExecedData::get_AlreadyExeced(longlong param_1)

{
  return *(undefined1 *)(param_1 + 0x10);
}



/* ==== ActionExecedData::get_ExecedPartsNumber  @ 0x465600  size=4 ==== */

undefined4 ActionExecedData::get_ExecedPartsNumber(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x18);
}



/* ==== ActionExecedData::get_TargetPartsNumber  @ 0x465520  size=4 ==== */

undefined4 ActionExecedData::get_TargetPartsNumber(longlong param_1)

{
  return *(undefined4 *)(param_1 + 0x14);
}



/* ==== ActionExecedData::set_AlreadyExeced  @ 0x4bd510  size=4 ==== */

void ActionExecedData::set_AlreadyExeced(longlong param_1,undefined1 param_2)

{
  *(undefined1 *)(param_1 + 0x10) = param_2;
  return;
}



/* ==== ActionExecedData::set_ExecedPartsNumber  @ 0x465630  size=4 ==== */

void ActionExecedData::set_ExecedPartsNumber(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x18) = param_2;
  return;
}



/* ==== ActionExecedData::set_TargetPartsNumber  @ 0x465610  size=4 ==== */

void ActionExecedData::set_TargetPartsNumber(longlong param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0x14) = param_2;
  return;
}



