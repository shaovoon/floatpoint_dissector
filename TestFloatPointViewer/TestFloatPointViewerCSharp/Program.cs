using System;

namespace TestFloatPointViewerCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //FloatDissecter d = new FloatDissecter(0.5f);
            DoubleDissecter d = new DoubleDissecter(0.5);

            d.Display();

            d.SetSigned(DoubleDissecter.Sign.Negative);
            //d.SetMantissa(1);

            d.Display();

            Console.WriteLine("Done!");
        }
    }
}
