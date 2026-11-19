using System;
using System.Collections.Generic;
using System.Linq;

namespace PCRCaculator
{
    internal sealed class PrincessKnightShareCode
    {
        internal int[] Spheres = new int[5];
        internal int Master;
        internal readonly Dictionary<int, int> Nodes = new Dictionary<int, int>();
        internal readonly int[] Mastery = new int[32];

        private static void Check(bool valid)
        {
            if (!valid) throw new FormatException("分享码数据无效或超出可编码范围。");
        }

        // Fields occupy consecutive bits, low bit first. Blocks and the mastery tail start on byte boundaries.
        private sealed class Writer
        {
            internal readonly List<byte> Bytes = new List<byte>();
            private int bits;
            internal void Put(int value, int width)
            {
                Check(value >= 0 && value < (1 << width));
                for (int i = 0; i < width; i++, bits++)
                {
                    if (bits % 8 == 0) Bytes.Add(0);
                    if ((value & (1 << i)) != 0) Bytes[bits / 8] |= (byte)(1 << (bits % 8));
                }
            }
            internal void Align() => bits = Bytes.Count * 8;
        }

        private sealed class Reader
        {
            private readonly byte[] data;
            private int bits;
            internal Reader(byte[] data) { this.data = data; }
            internal int Remaining => data.Length * 8 - bits;
            internal int Get(int width)
            {
                Check(Remaining >= width);
                int value = 0;
                for (int i = 0; i < width; i++, bits++)
                    value |= ((data[bits / 8] >> (bits % 8)) & 1) << i;
                return value;
            }
            internal void Align() => Get((8 - bits % 8) % 8);
        }

        private static int BlockType(int[] values)
        {
            int max = values.Max();
            int range = max - values.Min();
            if (range == 0) return 1;
            if (range <= 3 && (values.Length % 4 != 1 || max <= 16383)) return 2;
            if (range <= 15 && (values.Length % 2 != 1 || max <= 4095)) return 3;
            return 0;
        }
        private static int DeltaWidth(int type) => type == 0 ? 8 : type == 1 ? 0 : type == 2 ? 2 : 4;
        private static int BaseWidth(int width, int count) =>
            (width == 2 && count % 4 == 1) || (width == 4 && count % 2 == 1) ? 16 - width : 16;

        private static void WriteBlock(Writer w, int[] values, int type)
        {
            int width = DeltaWidth(type);
            int min = values.Min();
            w.Put(min, BaseWidth(width, values.Length));
            foreach (int value in values) w.Put(value - min, width);
            w.Align();
        }
        private static int[] ReadBlock(Reader r, int count, int type)
        {
            int width = DeltaWidth(type);
            int min = r.Get(BaseWidth(width, count));
            var result = new int[count];
            for (int i = 0; i < count; i++) result[i] = min + r.Get(width);
            r.Align();
            return result;
        }

        private static void WriteLevels(Writer w, int[] levels)
        {
            for (int i = 0; i < levels.Length; i += 3)
            {
                int packed = 0;
                for (int j = Math.Min(i + 3, levels.Length) - 1; j >= i; j--)
                    packed = 6 * packed + levels[j];
                w.Put(packed, 8);
            }
        }

        internal string Encode()
        {
            var ids = Nodes.Keys.ToArray();
            var levels = ids.Select(id => Nodes[id]).ToArray();
            Check(Spheres.Length == 5 && Spheres.All(v => v > 0 && v <= 65535) &&
                ids.Length <= 255 && ids.All(id => id >= 0 && id <= 65535) &&
                levels.All(v => v >= 1 && v <= 5) && Master >= 0 && Master <= 65535);
            int count = ids.Length;
            Writer dense = null;
            if (count <= 3 && ids.All(id => id <= 255) && Spheres.Max() <= 511 && Master <= 255)
            {
                dense = new Writer();
                dense.Put(0, 8);
                foreach (int sphere in Spheres) dense.Put(sphere & 255, 8);
                foreach (int sphere in Spheres) dense.Put(sphere >> 8, 1);
                dense.Put(0, 1);
                dense.Put(count, 2);
                foreach (int id in ids) dense.Put(id, 8);
                WriteLevels(dense, levels);
                dense.Put(Master, 8);
            }
            Writer packed = null;
            if (count > 0 && count <= 15 && ids.Max() - ids.Min() <= 255 &&
                Spheres.Max() - Spheres.Min() <= 255 && Master <= 255)
            {
                packed = new Writer();
                packed.Put(0, 8);
                int spheresType = BlockType(Spheres), idsType = BlockType(ids);
                packed.Put(count | spheresType << 4 | idsType << 6, 8);
                WriteBlock(packed, Spheres, spheresType);
                WriteBlock(packed, ids, idsType);
                WriteLevels(packed, levels);
                packed.Put(Master, 8);
            }
            int mode = 0;
            Writer w = dense;
            if (packed != null && (w == null || packed.Bytes.Count < w.Bytes.Count))
            {
                w = packed;
                mode = 1;
            }
            if (w == null)
            {
                mode = 3;
                w = new Writer();
                w.Put(0, 8);
                foreach (int sphere in Spheres) w.Put(sphere, 16);
                w.Put(count, 8);
                foreach (int id in ids) { w.Put(id, 16); w.Put(Nodes[id], 8); }
                w.Put(Master, 16);
            }

            if (Mastery.Any(v => v != 0)) WriteMastery(w);
            w.Bytes[0] = (byte)((mode << 6) | Checksum(w.Bytes));
            return Convert.ToBase64String(w.Bytes.ToArray()).TrimEnd('=');
        }

        private static int Checksum(IEnumerable<byte> bytes) => (bytes.Skip(1).Sum(v => (int)v) & 63) ^ 0x17;

        private void WriteMastery(Writer w)
        {
            int width = 5, max = Mastery.Max();
            Check(Mastery.Min() >= 0 && max <= 255);
            while (width < 8 && max >= (1 << width) - 1) width++;
            if (width > 5) { w.Put(31, 5); w.Put(width, 5); w.Put(31, 5); }
            int marker = (1 << width) - 1;
            for (int i = 0; i < Mastery.Length;)
            {
                int start = i++;
                while (i < Mastery.Length && Mastery[i] == Mastery[start]) i++;
                int run = i - start;
                if (run >= 3 || Mastery[start] == marker)
                {
                    w.Put(marker, width);
                    w.Put(run - 1, 5);
                    w.Put(Mastery[start], width);
                }
                else for (int j = start; j < i; j++) w.Put(Mastery[start], width);
            }
        }

        internal static PrincessKnightShareCode Decode(string text)
        {
            string code = string.Concat((text ?? "").Where(c => !char.IsWhiteSpace(c)));
            Check(code.Length > 0 && code.Length <= 1200);
            var bytes = Convert.FromBase64String(code.PadRight((code.Length + 3) / 4 * 4, '='));
            var r = new Reader(bytes);
            int header = r.Get(8), mode = header >> 6;
            Check(mode != 2 && (header & 63) == Checksum(bytes));
            var data = new PrincessKnightShareCode();
            int[] ids;
            if (mode == 3)
            {
                for (int i = 0; i < 5; i++) data.Spheres[i] = r.Get(16);
                int count = r.Get(8);
                for (int i = 0; i < count; i++) data.AddNode(r.Get(16), r.Get(8));
                data.Master = r.Get(16);
            }
            else
            {
                if (mode == 0)
                {
                    for (int i = 0; i < 5; i++) data.Spheres[i] = r.Get(8);
                    for (int i = 0; i < 5; i++) data.Spheres[i] |= r.Get(1) << 8;
                    r.Get(1);
                    ids = new int[r.Get(2)];
                    for (int i = 0; i < ids.Length; i++) ids[i] = r.Get(8);
                }
                else
                {
                    int count = r.Get(4), spheresType = r.Get(2), idsType = r.Get(2);
                    Check(count > 0);
                    data.Spheres = ReadBlock(r, 5, spheresType);
                    ids = ReadBlock(r, count, idsType);
                }
                for (int i = 0; i < ids.Length;)
                {
                    int packed = r.Get(8);
                    for (int j = 0; j < 3 && i < ids.Length; j++, i++) {
                        data.AddNode(ids[i], packed % 6);
                        packed /= 6;
                    }
                    Check(packed == 0);
                }
                data.Master = r.Get(8);
            }
            Check(data.Spheres.All(v => v > 0 && v <= 65535));
            if (r.Remaining > 0) data.ReadMastery(r);
            return data;
        }
        private void AddNode(int id, int level)
        {
            Check(id <= 65535 && level >= 1 && level <= 5 && !Nodes.ContainsKey(id));
            Nodes.Add(id, level);
        }
        private void ReadMastery(Reader r)
        {
            int width = 5;
            for (int i = 0; i < Mastery.Length;)
            {
                int value = r.Get(width), run = 1;
                if (value == (1 << width) - 1)
                {
                    int count = r.Get(5);
                    value = r.Get(width);
                    if (width == 5 && value == 31)
                    {
                        Check(i == 0 && count >= 6 && count <= 8);
                        width = count;
                        continue;
                    }
                    run = count + 1;
                }
                Check(i + run <= Mastery.Length);
                while (run-- > 0) Mastery[i++] = value;
            }
            Check(r.Remaining < 8 && r.Get(r.Remaining) == 0);
        }
    }
}
