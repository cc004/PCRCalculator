import io, collections
from capstone import Cs, CS_ARCH_X86, CS_MODE_64

TARGETS = {"UnitActionController","SkillBase","SkillEffectCtrl","UnitSkill","SkillDefine",
           "SkillUtility","SkillActionValue","ActionData","ActionExecedData"}
rows = []
with io.open("methods.csv", encoding="utf-8") as f:
    next(f)
    for line in f:
        p = line.rstrip("\n").split(",")
        if len(p) < 5: continue
        if p[2] in TARGETS and p[4].startswith("0x"):
            rows.append((p[2], p[3], int(p[4], 16)))
print("目标类方法数:", len(rows))
cnt = collections.Counter(c for c,_,_ in rows)
print(dict(cnt))

img = open("_il2cpp/GameAssembly_rva.dll","rb").read()
md = Cs(CS_ARCH_X86, CS_MODE_64)
def dis(rva, n=8):
    return list(md.disasm(img[rva:rva+n*8], rva))[:n]

print("\n=== 抽样 6 个方法的前 6 条指令 ===")
for cls, meth, rva in rows[:6]:
    ins = dis(rva, 6)
    print("%s::%s  rva=0x%X" % (cls, meth, rva))
    for i in ins:
        print("   0x%X  %-8s %s" % (i.address, i.mnemonic, i.op_str))
    if not ins: print("   <反汇编为空>")
