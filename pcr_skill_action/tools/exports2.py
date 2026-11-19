import struct, re
path="_il2cpp/GameAssembly_rva.dll"
f=open(path,"rb")
head=f.read(0x2000)
pe=struct.unpack_from("<I", head, 0x3C)[0]
opt=pe+24
magic=struct.unpack_from("<H", head, opt)[0]
dd = opt + (112 if magic==0x20b else 96)
export_rva, export_size = struct.unpack_from("<II", head, dd)
f.seek(export_rva); ex=f.read(0x40)
def rva2off(rva):
    # 我们的 PE 里 RVA==文件偏移
    return rva
nfun=struct.unpack_from("<I", ex, 0x14)[0]
nnames=struct.unpack_from("<I", ex, 0x18)[0]
aon=struct.unpack_from("<I", ex, 0x20)[0]
f.seek(aon); nametab=f.read(4*nnames)
names=[]
for i in range(nnames):
    nr=struct.unpack_from("<I", nametab, 4*i)[0]
    f.seek(nr); buf=b""
    while True:
        b=f.read(1)
        if b in (b"\x00", b""): break
        buf+=b
    names.append(buf.decode("latin1"))
print("导出总数 %d，其中 il2cpp_* %d" % (len(names), sum(1 for n in names if n.startswith("il2cpp_"))))
pat=re.compile(r"(class|field|type|method|image|assembly|domain|object)", re.I)
sel=[n for n in names if n.startswith("il2cpp_") and pat.search(n)]
print("相关 API %d 个：" % len(sel))
for n in sorted(sel): print("   " + n)
