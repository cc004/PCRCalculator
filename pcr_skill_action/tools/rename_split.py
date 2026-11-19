"""把 Ghidra 伪 C 里的 FUN_xxx / func_0x... 还原成 类::方法，并按类拆分。

规则（v2）：
  1. 方法表里「一个 RVA 只对应一个方法」→ 直接用那个名字；
  2. 「一个 RVA 对应多个方法」（il2cpp 代码折叠 / 共享桩）→ 只有当这个 RVA 正好是
     当前段落自己那个方法时才用段落头的名字，其余保留 sub_xxxxxxxx；
  3. 仍然查不到的 → sub_xxxxxxxx。
相比 v1（只改唯一 VA），v2 能把「自己就是共享桩」的那 500+ 个方法也正名。
"""
import csv, os, re, collections

BASE  = 0x180000000
SRC   = r"_il2cpp\skill_all.c"
DST   = r"_il2cpp\skill_all_named.c"
OUT   = r"_il2cpp\out"
TABLE = "methods.csv"

names = collections.defaultdict(set)
with open(TABLE, encoding="utf-8", newline="") as f:
    for r in csv.DictReader(f):
        rva = (r.get("rva") or "").strip()
        if not rva.lower().startswith("0x"):
            continue
        try:
            va = BASE + int(rva, 16)
        except ValueError:
            continue
        names[va].add("%s::%s" % (r["class"], r["method"]))

uniq = {va: next(iter(s)) for va, s in names.items() if len(s) == 1}
amb  = sum(1 for s in names.values() if len(s) > 1)
print("总 VA %d，唯一可命名 %d，多主 %d" % (len(names), len(uniq), amb))

def clean(n):
    return re.sub(r"[^0-9A-Za-z_:<>$]", "_", n)

TOKEN = re.compile(r"\b(?:FUN_|func_0x)([0-9a-fA-F]{8,16})\b")

src = open(SRC, encoding="utf-8", errors="replace").read()
blocks = re.split(r"(?=/\* ==== )", src)

renamed = kept = 0
out_blocks = []
for blk in blocks:
    mo = re.match(r"/\* ==== ([^:]+)::(\S+)\s+@ (0x[0-9a-fA-F]+)", blk)
    own_va = None
    own_name = None
    if mo:
        own_name = clean("%s::%s" % (mo.group(1), mo.group(2)))
        try:
            own_va = BASE + int(mo.group(3), 16)
        except ValueError:
            own_va = None

    def repl(m):
        global renamed, kept
        va = int(m.group(1), 16)
        if va in uniq:
            renamed += 1
            return uniq[va]
        if own_va is not None and va == own_va:
            renamed += 1
            return own_name
        kept += 1
        return "sub_%08x" % (va & 0xFFFFFFFF)

    out_blocks.append(TOKEN.sub(repl, blk))

out = "".join(out_blocks)
open(DST, "w", encoding="utf-8").write(out)
print("改名 %d 处，保留 sub_ %d 处，输出 %.2f MB" % (renamed, kept, len(out) / 1048576))

os.makedirs(OUT, exist_ok=True)
for fn in os.listdir(OUT):
    os.remove(os.path.join(OUT, fn))
buckets = {}
for p in re.split(r"(?=/\* ==== )", out):
    m = re.match(r"/\* ==== ([^:]+)::", p)
    if m:
        buckets.setdefault(m.group(1), []).append(p)
for cls, segs in buckets.items():
    with open(os.path.join(OUT, cls + ".c"), "w", encoding="utf-8") as f:
        f.write("".join(segs))
print("拆分 %d 个类文件" % len(buckets))
