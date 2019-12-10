using System;

namespace TestFloatPointViewerCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DoubleDissecter d = new DoubleDissecter(double.NegativeInfinity);

            d.SetNaN(DoubleDissecter.Sign.Positive, 2);

            Console.WriteLine("IsNaN:{0}\n", d.IsNaN());
            Console.WriteLine("IsZero:{0}\n", d.IsZero());
            Console.WriteLine("IsInfinity:{0}\n", d.IsInfinity());
            Console.WriteLine("IsPositiveInfinity:{0}\n", d.IsPositiveInfinity());
            Console.WriteLine("IsNegativeInfinity:{0}\n", d.IsNegativeInfinity());
            Console.WriteLine("IsNaN:{0}\n", double.IsNaN(d.GetFloatPoint()));

            Console.WriteLine("Done!");
        }
    }
}
