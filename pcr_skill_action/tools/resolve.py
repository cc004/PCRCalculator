import csv, re, collections, sys

BASE = 0x180000000
names = collections.defaultdict(set)
with open("methods.csv", encoding="utf-8", newline="") as f:
    for r in csv.DictReader(f):
        rva = (r.get("rva") or "").strip()
        if not rva.lower().startswith("0x"):
            continue
        try:
            va = BASE + int(rva, 16)
        except ValueError:
            continue
        names[va].add("%s::%s" % (r["class"], r["method"]))

src = sys.argv[1] if len(sys.argv) > 1 else "_il2cpp/uac_analyzed.c"
anchor = sys.argv[2] if len(sys.argv) > 2 else "UnitActionController::ExecAction  @"
span = int(sys.argv[3]) if len(sys.argv) > 3 else 14000

txt = open(src, encoding="utf-8", errors="replace").read()
i = txt.find(anchor)
seg = txt[i:i + span]
toks = sorted(set(int(t, 16) for t in re.findall(r"FUN_([0-9a-fA-F]{8,16})", seg)))
print("%s -> %d FUN_ refs" % (anchor.split("  @")[0], len(toks)))
for va in toks:
    hit = sorted(names.get(va, []))
    print("  0x%x  ->  %s" % (va, "; ".join(hit[:3]) if hit else "?"))
