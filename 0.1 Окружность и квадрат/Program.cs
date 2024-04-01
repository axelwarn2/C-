Console.WriteLine("Введите сторону квадрата: ");
int a = int.Parse(Console.ReadLine());
Console.WriteLine("Введите радиус окружности: ");
int r = int.Parse(Console.ReadLine());

double S1 = Math.Pow(a, 2);
double S2 = Math.PI * (Math.Pow(r,2));
double d = Math.Sqrt((Math.Pow(a, 2)) + (Math.Pow(a, 2)));

if(d <= r + r)
{
    Console.WriteLine("Площадь квадрата = " + S1);
}
else if(r <= a / 2)
{
    Console.WriteLine("Площадь окружности = " + S2);
}
else
{
    double xorda = Math.Sqrt(r ^ 2 + (a / 2) ^ 2) * 2;
    double alpha = 2 * Math.Asin(xorda / 2 / r);
    double seg = r * r / 2 * (alpha - Math.Sin(alpha));
    double res = S2 - 4 * seg;
    Console.WriteLine(res);
}