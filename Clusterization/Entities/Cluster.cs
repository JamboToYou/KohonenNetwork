using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;

namespace Clusterization.Entities
{
	public class Cluster
	{
		public List<Dot> Dots { get; }
		public Dot[] prevCenters { get; }
		public Dot center { get; set; }
		public float hue { get; set; }

		public Cluster(float hue)
		{
			Dots = new List<Dot>();
			this.hue = hue;
			prevCenters = new Dot[2];
		}

		public Cluster(float hue, Dot center) : this(hue)
		{
			this.center = center;
		}

		public void CorrectCenter()
		{
			prevCenters[0] = prevCenters[1];
			prevCenters[1] = center;
			center = Dots.Count != 0 ? Dots.Aggregate((acc, dot) => acc + dot) / Dots.Count : center;
		}

		public double To1(Cluster cluster)
		{
			var sum = 0.0;
			for (int i = 0; i < Dots.Count; i++)
				for (int j = 0; j < cluster.Dots.Count; j++)
					sum += Dots[i].To(cluster.Dots[j]);

			return sum / (Dots.Count + cluster.Dots.Count);
		}

		public double To(Cluster cluster) =>
			Dots.Select(dot => cluster.Dots.Select(d => dot.To(d)).Min()).Min();

		public double To(Dot dot) => center.To(dot);
	}
}