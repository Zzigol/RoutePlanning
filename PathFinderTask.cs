using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var bestOrder = MakeTrivialPermutation(new int[checkpoints.Length], checkpoints);
			return bestOrder;
		}

        private static int[] MakeTrivialPermutation(int[] order, Point[] checkpoints)
        {
            var result = new List<int[]>();
            MakePermutations(order, result);
            var min = double.MaxValue;
            var bestOrder = new int[order.Length];
            foreach (var variant in result)
            { 
                double len = PointExtensions.GetPathLength(checkpoints, variant); 
                if (len < min)
                {
                    min = len;
                    bestOrder = variant;
                }
            }
                
            return bestOrder;
        }

        private static List<int[]> MakePermutations(int[] order, List<int[]> result, int position = 1)
        {            
            if (position == order.Length)
            {
                result.Add((int[])order.Clone());
                return result;
            }

            for (int i = 1; i < order.Length; i++)
            {
                var index = Array.IndexOf(order, i, 1, position-1);
                if (index != -1)
                    continue;
                order[position] = i;
                MakePermutations(order, result, position + 1);
            }
            return result;
        }
    }
}