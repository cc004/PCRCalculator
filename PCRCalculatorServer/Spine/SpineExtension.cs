using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Spine.Unity
{
    internal static class SpineExtension
    {
        public static void GetWorldToLocalMatrix(this Bone bone, out float ia, out float ib, out float ic, out float id)
        {
            float a = bone.a;
            float b = bone.b;
            float c = bone.c;
            float d = bone.d;
            float num = 1f / (a * d - b * c);
            ia = num * d;
            ib = num * (0f - b);
            ic = num * (0f - c);
            id = num * a;
        }

        public static Vector2[] GetLocalVertices(this VertexAttachment va, Slot slot, Vector2[] buffer)
        {
            int worldVerticesLength = va.worldVerticesLength;
            int num = worldVerticesLength >> 1;
            buffer = buffer ?? new Vector2[num];
            if (buffer.Length < num)
            {
                throw new ArgumentException($"Vector2 buffer too small. {va.Name} requires an array of size {worldVerticesLength}. Use the attachment's .WorldVerticesLength to get the correct size.", "buffer");
            }
            if (va.bones == null)
            {
                float[] vertices = va.vertices;
                for (int i = 0; i < num; i++)
                {
                    int num2 = i * 2;
                    buffer[i] = new Vector2(vertices[num2], vertices[num2 + 1]);
                }
            }
            else
            {
                float[] array = new float[worldVerticesLength];
                va.ComputeWorldVertices(slot, array);
                Bone bone = slot.bone;
                float worldX = bone.worldX;
                float worldY = bone.worldY;
                bone.GetWorldToLocalMatrix(out var ia, out var ib, out var ic, out var id);
                for (int j = 0; j < num; j++)
                {
                    int num3 = j * 2;
                    float num4 = array[num3] - worldX;
                    float num5 = array[num3 + 1] - worldY;
                    buffer[j] = new Vector2(num4 * ia + num5 * ib, num4 * ic + num5 * id);
                }
            }
            return buffer;
        }

    }
}
