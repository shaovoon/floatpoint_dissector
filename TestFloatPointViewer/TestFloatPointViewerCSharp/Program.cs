using System;

namespace TestFloatPointViewerCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //FloatDissecter d = new FloatDissecter(0.5f);
            DoubleDissecter d = new DoubleDissecter(double.NegativeInfinity);

            //d.Display();

            //d.SetSign(DoubleDissecter.Sign.Negative);
            //d.SetMantissa(1);

            //d.Display();

            Console.WriteLine("IsNaN:{0}\n", d.IsNaN());
            Console.WriteLine("IsZero:{0}\n", d.IsZero());
            Console.WriteLine("IsInfinity:{0}\n", d.IsInfinity());
            Console.WriteLine("IsPositiveInfinity:{0}\n", d.IsPositiveInfinity());
            Console.WriteLine("IsNegativeInfinity:{0}\n", d.IsNegativeInfinity());

            Console.WriteLine("Done!");
        }
    }
}
