using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        public static void Main(string[] args)
        {
            var methods = typeof(Program).GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

            foreach (var method in methods.Where(x => x.Name.StartsWith("Day")))
            {
                var day = method.Name.Split("_")[0];
                foreach (var dataSet in new string[] { "sample", "real" })
                {
                    var data = File.ReadAllLines($"..\\..\\..\\content\\{day}\\{dataSet}.txt");
                    var result = method.Invoke(null, new object[] { data });
                    Console.WriteLine($"{method.Name} {dataSet} => {result}");
                }
                Console.WriteLine();
            }

            Console.Read();
        }

        public static long XDay01_Pt1_GetResult(string[] data)
        {
            return 0;
        }
        public static long XDay02_Pt1_GetResult(string[] data)
        {
            return 0;
        }

        public static long Day01_Pt1_GetResult(string[] data)
        {
            return 0;
        }
        public static long Day02_Pt1_GetResult(string[] data)
        {
            return 0;
        }
    }
}