using System;

namespace MyPhotoshop
{
    public class Photo
    {
        public readonly int width;
        public readonly int height;
        private readonly Pixel[,] data;

        public Pixel this[int x, int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value;}
        }

        public Photo(int width, int height)
        {
            this.width = width;
            this.height = height;
            data = new Pixel[width, height];
        }
    }

	public struct Pixel
	{
        public Pixel(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public double Check(double value)
		{
            if (value < 0 || value > 1) throw new ArgumentException();
			return value;
        }

        public static double Trim(double value)
        {
            if (value < 0) return 0;
            if (value > 1) return 1;
            return value;
        }

        double r;
        public double R 
		{  get { return r; } 
			set
			{
				r = Check(value);
			} 
		}
		double g;
        public double G
        {
            get { return g; }
            set
            {
                g = Check(value);
            }
        }
        double b;
        public double B
        {
            get { return b; }
            set
            {
                b = Check(value);
            }
        }

        public static Pixel operator*(Pixel p, double c)
        {
            return new Pixel(
                   Pixel.Trim(p.R * c),
                   Pixel.Trim(p.G * c),
                   Pixel.Trim(p.B * c));
        }

        public static Pixel operator *(double c, Pixel p)
        {
            return p * c;
        }
    }
}

