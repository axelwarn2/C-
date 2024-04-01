using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
    public class Indexer
    {
        private readonly double[] data;
        private readonly int start;
        private readonly int length;

        public int Length => length;

        public Indexer(double[] data, int start, int length)
        {
            if (start < 0 || length < 0 || start + length > data.Length)
                throw new ArgumentException("Invalid range.");

            this.data = data;
            this.start = start;
            this.length = length;
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= length)
                    throw new IndexOutOfRangeException("Index is out of range.");

                return data[start + index];
            }
            set
            {
                if (index < 0 || index >= length)
                    throw new IndexOutOfRangeException("Index is out of range.");

                data[start + index] = value;
            }
        }
    }   
}