using System;
using System.Linq;
using Common.Entities;

namespace NeuroNet.Entities
{
    public class NeuroNetwork
    {
        public Dot[][] NeuronWeights { get; }
        public int SquareSide { get; set; }

        public NeuroNetwork(int neuronsCount, int inputSize)
        {
            var rnd = new Random();
            SquareSide = (int)Math.Sqrt(neuronsCount);
            NeuronWeights = new Dot[SquareSide][];
            for (int i = 0; i < SquareSide; i++)
            {
                NeuronWeights[i] = new Dot[SquareSide];
                for (int j = 0; j < SquareSide; j++)
                {
                    NeuronWeights[i][j] = new Dot(new double[inputSize].Select(el => rnd.NextDouble()).ToArray());
                }
            }
        }

        public void Study(Dot[] snaps)
        {
            int x, y;

            for (int i = 0; i < snaps.Length; i++)
            {
                (x, y) = GetWinner(snaps[i]);
                CorrectNeurons(x, y, snaps[i]);
            }
        }

        public (int, int) GetWinner(Dot snap)
        {
            var x = -1;
			var y = -1;
            var tmp = 0.0;
            var min = double.MaxValue;

            for (int i = 0; i < NeuronWeights.Length; i++)
            {
                for (int j = 0; j < NeuronWeights[i].Length; j++)
                {
                    tmp = NeuronWeights[i][j].To(snap);
                    if (tmp < min)
                    {
                        min = tmp;
                        x = i;
						y = j;
                    }
                }
            }

            return (x, y);
        }

        private void CorrectNeurons(int wy, int wx, Dot snap)
        {
            var len = NeuronWeights.Length;
            for (int i = 0; i < NeuronWeights.Length; i++)
            {
                for (int j = 0; j < NeuronWeights[i].Length; j++)
                {
                    NeuronWeights[i][j] +=
                        (snap - NeuronWeights[i][j]) *
                        GetNeighbourhoodIndicator(wy, wx, j, i);
                }
            }
        }

        private double GetNeighbourhoodIndicator(int wy, int wx, int ty, int tx) =>
            Math.Exp(-(Math.Abs(wy - ty) + Math.Abs(wx + tx)));
    }
}
