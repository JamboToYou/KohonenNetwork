using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Entities
{
	public class Dot
	{
		public double[] Coords { get; }

		public Dot(double[] coords)
		{
			Coords = coords;
		}

		public double To(Dot dot) => Math.Sqrt(Coords.Zip(dot.Coords, (x1, x2) => Math.Pow(x1 - x2, 2)).Sum());
		public override string ToString() => $"[{string.Join(",", Coords.Select(coord => coord.ToString("0.000")))}]";

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			return this == (Dot)obj;
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator == (Dot dot1, Dot dot2) => dot1.Coords.SequenceEqual(dot2.Coords);
		public static bool operator != (Dot dot1, Dot dot2) => !dot1.Coords.SequenceEqual(dot2.Coords);
		public static Dot operator + (Dot dot1, Dot dot2) => new Dot(dot1.Coords.Zip(dot2.Coords, (x1, x2) => x1 + x2).ToArray());
		public static Dot operator - (Dot dot1, Dot dot2) => new Dot(dot1.Coords.Zip(dot2.Coords, (x1, x2) => x1 - x2).ToArray());
		public static Dot operator * (Dot dot1, double num) => new Dot(dot1.Coords.Select(x => x * num).ToArray());
		public static Dot operator / (Dot dot1, double num) => new Dot(dot1.Coords.Select(x => x / num).ToArray());
	}
}