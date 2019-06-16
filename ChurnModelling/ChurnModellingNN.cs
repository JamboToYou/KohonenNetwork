using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clusterization;
using Clusterization.Entities;
using Common.Entities;
using Common.Utils;
using NeuroNet.Entities;
using Newtonsoft.Json;

namespace ChurnModelling
{
	public class ChurnModellingNN
	{
		private readonly static string _outputTitle = "Exited";
		private readonly static List<string> _notInputColumns = new List<string> { "RowNumber", "CustomerId", _outputTitle };
		private string[] _titles { get; set; }

		public List<Cluster> Clusters { get; private set; }

		private NeuroNetwork _nn { get; set; }

		public ChurnModellingNN()
		{
			_nn = new NeuroNetwork(625, 11);
		}

		public void Study()
		{
			var inputs = Normalize();
			_nn.Study(inputs);
			Save();
		}

		private void Save()
		{
			var js = new JsonSerializer();

			var path = Directory.GetCurrentDirectory() + @"/cluster.json";
			using (var sw = new StreamWriter(path))
			using (var jw = new JsonTextWriter(sw))
			{
				js.Serialize(jw, _nn);
			}
		}

		private void Load()
		{
			var path = Directory.GetCurrentDirectory() + @"/cluster.json";
			var tmp = "";
			using (var sr = new StreamReader(path))
			{
				tmp = sr.ReadToEnd();
			}

			Clusters = JsonConvert.DeserializeObject<List<Cluster>>(tmp);
		}

		public void Execute()
		{
			Dot input;
			var path = Directory.GetCurrentDirectory() + @"/input.csv";

			ParsingUtils.ParseCsvByCallback(
				path,
				title => _titles = title.Split(','),
				record =>
				{
					input = GetNormalizedRecord(record.Split(','));
				}
			);
		}

		protected Dot[] Normalize()
		{
			Dot input;
			var path = Directory.GetCurrentDirectory() + @"/Churn_Modelling.csv";

			var inputs = new List<Dot>();

			ParsingUtils.ParseCsvByCallback(
				path,
				title => _titles = title.Split(','),
				record =>
				{
					input = GetNormalizedRecord(record.Split(','));
					inputs.Add(input);
				}
			);

			return inputs.ToArray();
		}

		private Dot GetNormalizedRecord(string[] v)
		{
			var tmp = new List<double>();

			for (int i = 0; i < _titles.Length; i++)
			{
				if (_notInputColumns.Contains(v[i]))
					continue;

				tmp.Add(NormalizeValue(_titles[i], v[i]));
			}

			return new Dot(tmp.ToArray());
		}

		private double NormalizeRange(double value, double min, double max) => (value - min) / (max - min);
		private double NormalizeValue(string title, string value)
		{
			var countries = new List<string> { "France", "Spain", "Germany" };
			switch (title)
			{
				case "RowNumber":
					return NormalizeRange(int.Parse(value), 1, 10000);
				case "CustomerId":
					return NormalizeRange(int.Parse(value), 15565701, 15815690);
				case "Surname":
					return 1 / value.GetHashCode();
				case "CreditScore":
					return NormalizeRange(int.Parse(value), 350, 850);
				case "Geography":
					return (countries.IndexOf(value) + 1) / 3;
				case "Gender":
					return value == "Female" ? 0 : 1;
				case "Age":
					return NormalizeRange(int.Parse(value), 18, 92);
				case "Tenure":
					return NormalizeRange(int.Parse(value), 0, 10);
				case "Balance":
					return NormalizeRange(double.Parse(value.Replace('.', ',')), 0, 250898.09);
				case "NumOfProducts":
					return NormalizeRange(int.Parse(value), 1, 4);
				case "EstimatedSalary":
					return NormalizeRange(double.Parse(value.Replace('.', ',')), 11.58, 199992.48);
				case "HasCrCard":
				case "IsActiveMember":
				case "Exited":
					return int.Parse(value);
				default:
					throw new ArgumentException("[error]: There`s no column with given title");
			}
		}
	}
}
