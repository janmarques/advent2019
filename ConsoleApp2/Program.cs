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

            int CalculateFuel(int mass)
            {
                return (mass / 3) - 2;
            }

            return data.Select(int.Parse).Select(CalculateFuel).Sum();
            //foreach (var item in data)
            //{
            //    var mass = int.Parse(item);
            //}
        }

        public static long Day01_Pt2_GetResult(string[] data)
        {

            int CalculateFuel(int mass)
            {
                var fuel = (mass / 3) - 2;
                if (fuel <= 0) { return 0; }
                else { return fuel + CalculateFuel(fuel); }
            }

            //CalculateFuel(100756);

            return data.Select(int.Parse).Select(CalculateFuel).Sum();
            //foreach (var item in data)
            //{
            //    var mass = int.Parse(item);
            //}
        }

        public static long Day02_Pt1_GetResult(string[] data)
        {
            var operations = data.Single().Split(",").Select(int.Parse).ToArray();
            operations[1] = 12;
            operations[2] = 2;

            var stop = 99;
            var add = 1;
            var multiply = 2;
            for (int i = 0; ; i = i + 4)
            {
                var opCode = operations[i];
                if (opCode == stop) { return operations[0]; }
                var input1 = operations[operations[i + 1]];
                var input2 = operations[operations[i + 2]];
                var target = operations[i + 3];
                var value = opCode == add ? input1 + input2 : input1 * input2;
                operations[target] = value;
            }

            throw new Exception();
        }

        public static long Day02_Pt2_GetResult(string[] data)
        {
            var operations = data.Single().Split(",").Select(int.Parse).ToArray();


            var stop = 99;
            var add = 1;
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var clone = operations.ToArray();
                    clone[1] = noun;
                    clone[2] = verb;

                    for (int i = 0; ; i = i + 4)
                    {
                        var opCode = clone[i];
                        if (opCode == stop)
                        {
                            if (clone[0] == 19690720)
                            {
                                return 100 * noun + verb;
                            }
                            else
                            {
                                break;
                            }
                        }
                        var input1 = clone[clone[i + 1]];
                        var input2 = clone[clone[i + 2]];
                        var target = clone[i + 3];
                        var value = opCode == add ? input1 + input2 : input1 * input2;
                        clone[target] = value;
                    }
                }
            }
            throw new Exception();
            // 682644 too low
        }
    }
}