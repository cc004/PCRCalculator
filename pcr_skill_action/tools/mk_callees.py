import csv, re, collections

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
        names[va].add((r["class"], r["method"]))

txt = open("_il2cpp/uac_analyzed.c", encoding="utf-8", errors="replace").read()
toks = sorted(set(int(t, 16) for t in re.findall(r"FUN_([0-9a-fA-F]{8,16})", txt)))
known = [(va, sorted(names[va])) for va in toks if va in names]
unknown = [va for va in toks if va not in names]
print("引用 FUN_ 去重 %d 个：已具名 %d，未具名 %d" % (len(toks), len(known), len(unknown)))

rows = []
for va in unknown:
    rows.append(("sub_%x" % (va - BASE), "sub_%x" % (va - BASE), "0x%x" % (va - BASE)))
with open("_il2cpp/callees.csv", "w", encoding="utf-8", newline="") as f:
    w = csv.writer(f)
    w.writerow(["class", "method", "rva"])
    for r in rows:
        w.writerow(r)
print("写出 _il2cpp/callees.csv：%d 行" % len(rows))
for va in unknown[:12]:
    print("   0x%x" % va)
