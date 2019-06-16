using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clusterization.Entities;
using Common.Entities;
using Newtonsoft.Json;

namespace Clusterization
{
	public static class ClusterizationEngine
	{
		public static List<Cluster> Clusterize(List<Dot> dots, int ccnt)
		{
			var clusters = new List<Cluster>(ccnt);
			var rnd = new Random();


			Cluster tmpCluster;
			var tmpDots = new double[dots[0].Coords.Length];
			float clr = 0;
			float dlt = 315f / ccnt;

			for (int i = 0; i < ccnt; i++, clr += dlt)
				clusters.Add(new Cluster(clr, new Dot(tmpDots.Select(el => rnd.NextDouble()).ToArray())));

			do
			{
				foreach (var cluster in clusters)
					cluster.Dots.Clear();

				foreach (var dot in dots)
				{
					tmpCluster = clusters
						.OrderBy(cluster => cluster
							.center
							.To(dot))
						.FirstOrDefault();

					tmpCluster.Dots
						.Add(dot);
				}

				foreach (var cluster in clusters)
					cluster.CorrectCenter();

				//tmp
				foreach (var cluster in clusters)
				{
					Console.Write(cluster.Dots.Count + " ");
				}
				Console.WriteLine();
				foreach (var cluster in clusters)
				{
					Console.Write(cluster.center + " ");
				}
				Console.WriteLine();
				//~tmp

			} while (clusters.Any(cluster => !cluster.prevCenters.Contains(cluster.center)));

			return clusters;
		}

		public static List<Cluster> Clusterize(List<Dot> dots) => new int[] { 2, 3, 4, 5, 6, 7 }
			.Select(cnt => Clusterize(dots, cnt))
			.OrderByDescending(clusters =>
				ClusterizationRanking(clusters))
			.FirstOrDefault();

		public static void SerializeClusters(List<Cluster> clusters)
		{
			var serializer = new JsonSerializer();

			using (var sw = new StreamWriter(Directory.GetCurrentDirectory() + $@"\cluster{clusters.Count}.json"))
			using (var jw = new JsonTextWriter(sw))
			{
				serializer.Serialize(jw, clusters);
			}
		}

		private static double ClusterizationRanking(List<Cluster> clusters)
		{
			var cnt = clusters.Count;
			double sum = 0;

			for (int i = 0; i < cnt - 1; i++)
			{
				for (int j = i + 1; j < cnt; j++)
				{
					sum += clusters[i].To(clusters[j]);
				}
			}

			return sum / (cnt * (cnt - 1));
		}
	}
}