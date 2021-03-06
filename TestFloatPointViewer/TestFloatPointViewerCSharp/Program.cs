﻿using System;

namespace TestFloatPointViewerCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DoubleDissector d = new DoubleDissector(double.NegativeInfinity);

            d.SetNaN(DoubleDissector.Sign.Positive, 2);

            Console.WriteLine("IsNaN:{0}", d.IsNaN());
            Console.WriteLine("IsZero:{0}", d.IsZero());
            Console.WriteLine("IsInfinity:{0}", d.IsInfinity());
            Console.WriteLine("IsPositiveInfinity:{0}", d.IsPositiveInfinity());
            Console.WriteLine("IsNegativeInfinity:{0}", d.IsNegativeInfinity());
            Console.WriteLine("IsNaN:{0}", double.IsNaN(d.GetFloatPoint()));

            Console.WriteLine("Done!");
        }
    }
}
