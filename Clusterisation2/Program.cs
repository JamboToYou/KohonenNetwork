using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clusterisation.Entities;
using Newtonsoft.Json;

namespace Clusterisation
{
	class Program
	{
		static void Main1(string[] args)
		{
			var b = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			Console.WriteLine(b.Aggregate((acc, i) => acc + i));
		}
		static void Main(string[] args)
		{
			var dots = new List<Dot>();
			var rnd = new Random();

			for (int i = 0; i < 500; i++)
			{
				dots.Add(new Dot(rnd.Next(800), rnd.Next(500)));
			}

			for (int i = 2; i < 7; i++)
			{
				ClusterisationEngine.SerializeClusters(ClusterisationEngine.Clusterise(dots, i));
			}
		}
	}
}
