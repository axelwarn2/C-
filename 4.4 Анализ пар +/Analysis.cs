using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.PairsAnalysis
{
    public static class Analysis
    {
        public static int FindMaxPeriodIndex(params DateTime[] data)
        {
            var differences = data.Pairs().Select(pair => (pair.Item2 - pair.Item1).TotalSeconds);
            return differences.MaxIndex();
        }

        public static double FindAverageRelativeDifference(params double[] data)
        {
            var differences = data.Pairs().Select(pair => (pair.Item2 - pair.Item1) / pair.Item1);
            return differences.Average();
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> source)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    yield break;

                var previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    yield return Tuple.Create(previous, iterator.Current);
                    previous = iterator.Current;
                }
            }
        }

        public static int MaxIndex<T>(this IEnumerable<T> source) where T : IComparable<T>
        {
            var maxIndex = -1;
            var maxValue = default(T);
            var currentIndex = 0;

            foreach (var item in source)
            {
                if (maxIndex == -1 || item.CompareTo(maxValue) > 0)
                {
                    maxIndex = currentIndex;
                    maxValue = item;
                }
                currentIndex++;
            }

            if (maxIndex == -1)
                throw new InvalidOperationException();

            return maxIndex;
        }
    }
}
