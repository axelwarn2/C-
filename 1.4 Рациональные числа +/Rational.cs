using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public int Numerator { get; }
        public int Denominator { get; }
        public bool IsNan { get; }

        public Rational(int numerator, int denominator = 1)
        {
            if (denominator == 0)
            {
                IsNan = true;
                return;
            }

            int gcd = GetGCD(Math.Abs(numerator), Math.Abs(denominator));
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;

            if (denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }

            IsNan = false;
        }

        public static Rational operator +(Rational a, Rational b)
        {
            if (a.IsNan || b.IsNan)
                return new Rational(0, 0);

            int num = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            int den = a.Denominator * b.Denominator;

            return new Rational(num, den);
        }

        public static Rational operator -(Rational a, Rational b)
        {
            if (a.IsNan || b.IsNan)
                return new Rational(0, 0);

            int num = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            int den = a.Denominator * b.Denominator;

            return new Rational(num, den);
        }

        public static Rational operator *(Rational a, Rational b)
        {
            if (a.IsNan || b.IsNan)
                return new Rational(0, 0);

            int num = a.Numerator * b.Numerator;
            int den = a.Denominator * b.Denominator;

            return new Rational(num, den);
        }

        public static Rational operator /(Rational a, Rational b)
        {
            if (a.IsNan || b.IsNan || b.Numerator == 0)
                return new Rational(0, 0); 

            int num = a.Numerator * b.Denominator;
            int den = a.Denominator * b.Numerator;

            return new Rational(num, den);
        }

        public static implicit operator Rational(int value)
        {
            return new Rational(value, 1);
        }

        public static explicit operator int(Rational value)
        {
            if (value.IsNan)
                throw new Exception();

            if (value.Denominator != 1)
                throw new Exception();

            return value.Numerator;
        }

        public static implicit operator double(Rational value)
        {
            if (value.IsNan)
                return double.NaN;

            return (double)value.Numerator / value.Denominator;
        }

        private int GetGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
