using System;
using UnityEngine;

namespace Spine
{
	public static class MathUtils
	{
		public const float PI = (float)Math.PI;

		public const float PI2 = (float)Math.PI * 2f;

		public const float RadDeg = 180f / (float)Math.PI;

		public const float DegRad = (float)Math.PI / 180f;

		public static float Sin(float radians)
		{
			return Mathf.Sin(radians);
		}

		public static float Cos(float radians)
		{
			return Mathf.Cos(radians);
		}

		public static float SinDeg(float degrees)
		{
			return Mathf.Sin(degrees * ((float)Math.PI / 180f));
		}

		public static float CosDeg(float degrees)
		{
			return Mathf.Cos(degrees * ((float)Math.PI / 180f));
		}

		public static float Atan2(float y, float x)
		{
			if (x == 0f)
			{
				if (y > 0f)
				{
					return (float)Math.PI / 2f;
				}
				if (y == 0f)
				{
					return 0f;
				}
				return -(float)Math.PI / 2f;
			}
			return Mathf.Atan2(y, x);
		}

		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}
	}
}
