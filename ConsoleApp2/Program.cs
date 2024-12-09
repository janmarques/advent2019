﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
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
        static int ToInt(char c) => ToInt(c.ToString());
        static int ToInt(string c) => int.Parse(c);
        public static long XDay01_Pt1_GetResult(string[] data)
        {
            return 0;
        }
        public static long XDay02_Pt1_GetResult(string[] data)
        {
            return 0;
        }
        /*
        public static long Day01_Pt1_GetResult(string[] data)
        {

            int CalculateFuel(int mass)
            {
                return (mass / 3) - 2;
            }

            return data.Select(int.Parse).Select(CalculateFuel).Sum();
        }

        public static long Day01_Pt2_GetResult(string[] data)
        {

            int CalculateFuel(int mass)
            {
                var fuel = (mass / 3) - 2;
                if (fuel <= 0) { return 0; }
                else { return fuel + CalculateFuel(fuel); }
            }

            return data.Select(int.Parse).Select(CalculateFuel).Sum();
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
            return -1;
        }

        public static long Day03_Pt1_GetResult(string[] data)
        {
            IEnumerable<(int, int)> GetIntersections(int[,] grid)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] > 1) { yield return (i, j); }
                    }
                }
            }

            int CalculateManhattenDistance((int, int) input, int offset) => Math.Abs(input.Item1 - offset) + Math.Abs(input.Item2 - offset);
            var lines = data.Select(x => x.Split(",")).ToArray();
            var size = 20000;
            var offset = 10000;
            var grid = new int[size, size];
            foreach (var line in lines)
            {
                var previousX = offset;
                var previousY = offset;
                foreach (var point in line)
                {
                    var direction = point[0];
                    var distance = int.Parse(new string(point.Skip(1).ToArray()));
                    var upDown = direction == 'U' ? 1 : direction == 'D' ? -1 : 0;
                    var rightLeft = direction == 'R' ? 1 : direction == 'L' ? -1 : 0;
                    if (upDown != 0)
                    {
                        Enumerable.Range(1, distance).ToList().ForEach(x => grid[previousX, previousY + x * upDown]++);
                        previousY += upDown * distance;
                    }
                    else
                    {
                        Enumerable.Range(1, distance).ToList().ForEach(x => grid[previousX + x * rightLeft, previousY]++);
                        previousX += rightLeft * distance;
                    }
                }
            }

            var intersections = GetIntersections(grid).Select(x => CalculateManhattenDistance(x, offset)).ToList();

            return intersections.Min();
        }
       
        public static long Day03_Pt2_GetResult(string[] data)
        {
            IEnumerable<(int, int)> GetIntersections(int[,] grid)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] > 1) { yield return (i, j); }
                    }
                }
            }



            var lines = data.Select(x => x.Split(",")).ToArray();
            var size = 20000;
            var offset = 10000;
            var grid = new int[size, size];


            long CalculateDistanceToIntersection(string[] line, (int, int) intersection)
            {
                var i = 0;
                var previousX = offset;
                var previousY = offset;
                foreach (var point in line)
                {
                    var direction = point[0];
                    var distance = int.Parse(new string(point.Skip(1).ToArray()));
                    var upDown = direction == 'U' ? 1 : direction == 'D' ? -1 : 0;
                    var rightLeft = direction == 'R' ? 1 : direction == 'L' ? -1 : 0;
                    if (upDown != 0)
                    {
                        foreach (var x in Enumerable.Range(1, distance))
                        {
                            var thisY = previousY + x * upDown;
                            grid[previousX, thisY]++;
                            if (intersection == (previousX, thisY))
                            {
                                return i + x;
                            }
                        };
                        previousY += upDown * distance;
                        i += distance;
                    }
                    else
                    {
                        foreach (var x in Enumerable.Range(1, distance))
                        {

                            var thisX = previousX + x * rightLeft;
                            grid[thisX, previousY]++;
                            
                            if (intersection == (thisX, previousY))
                            {
                                return i + x;
                            }
                        }
                        previousX += rightLeft * distance;
                        i += distance;
                    }
                }

                return int.MaxValue;
            }

            foreach (var line in lines)
            {
                var previousX = offset;
                var previousY = offset;
                foreach (var point in line)
                {
                    var direction = point[0];
                    var distance = int.Parse(new string(point.Skip(1).ToArray()));
                    var upDown = direction == 'U' ? 1 : direction == 'D' ? -1 : 0;
                    var rightLeft = direction == 'R' ? 1 : direction == 'L' ? -1 : 0;
                    if (upDown != 0)
                    {
                        Enumerable.Range(1, distance).ToList().ForEach(x => grid[previousX, previousY + x * upDown]++);
                        previousY += upDown * distance;
                    }
                    else
                    {
                        Enumerable.Range(1, distance).ToList().ForEach(x => grid[previousX + x * rightLeft, previousY]++);
                        previousX += rightLeft * distance;
                    }
                }
            }

            var intersections = GetIntersections(grid).ToList();

            var minSteps = long.MaxValue;
            foreach (var intersection in intersections)
            {
                var sum = CalculateDistanceToIntersection(lines[0], intersection) + CalculateDistanceToIntersection(lines[1], intersection);
                if(sum == 12934)
                {
                    Console.WriteLine(intersection);
                }
                minSteps = Math.Min(minSteps, sum);
            }

            return minSteps;

            // 20718 too high
        }

        

        public static long Day04_Pt1_GetResult(string[] data)
        {
            var range = data.Single().Split("-").Select(int.Parse).ToArray();
            var min = range[0];
            var max = range[1];

            var count = 0;
            for (int i = min; i <= max; i++)
            {
                var str = i.ToString();
                var adjecentOk = false;
                var increasingOk = true;
                for (int j = 0; j < 5; j++)
                {
                    if (str[j] == str[j + 1])
                    {
                        adjecentOk = true;
                    }
                    if (int.Parse(str[j].ToString()) > int.Parse(str[j + 1].ToString()))
                    {
                        increasingOk = false; break;
                    }
                }

                if (increasingOk && adjecentOk)
                {
                    count++;
                }
            }

            return count; // 909 too low
        }

        public static long Day04_Pt2_GetResult(string[] data)
        {
            var range = data.Single().Split("-").Select(int.Parse).ToArray();
            var min = range[0];
            var max = range[1];

            var count = 0;
            for (int i = min; i <= max; i++)
            {
                var str = i.ToString();
                var adjecentOk = Enumerable.Range(0,10).Any(x => str.Contains($"{x}{x}") && !str.Contains($"{x}{x}{x}"));
                var increasingOk = true;
                for (int j = 0; j < 5; j++)
                {
                    if (int.Parse(str[j].ToString()) > int.Parse(str[j + 1].ToString()))
                    {
                        increasingOk = false; break;
                    }
                }

                if (increasingOk && adjecentOk)
                {
                    count++;
                }
            }

            return count; // 1054 too low
        }

        

        public static long Day05_Pt1_GetResult(string[] data)
        {
            (bool? parameterMode1, bool? parameterMode2, bool? parameterMode3, int opCode) GetOpcode(int input)
            {
                var inputString = input.ToString();
                var opCode = int.Parse(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = false;
                var parameterMode2 = false;
                var parameterMode3 = false;
                if (inputString.Length == 3)
                {
                    parameterMode1 = inputString[0] == '1';
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = inputString[0] == '1';
                    parameterMode1 = inputString[1] == '1';
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = inputString[0] == '1';
                    parameterMode2 = inputString[1] == '1';
                    parameterMode1 = inputString[2] == '1';
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }
            var operations = data.Single().Split(",").Select(int.Parse).ToArray();

            const int stop = 99;
            const int add = 1;
            const int multiply = 2;
            const int input = 3;
            const int output = 4;

            var tmpInput = 1;
            var tmpOutput = int.MinValue;
            for (int i = 0; ;)
            {
                var opCode = GetOpcode(operations[i]);
                switch (opCode.opCode)
                {
                    case stop:
                        {
                            //return operations[0];
                            return tmpOutput;
                        }
                    case add:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            var value = param1 + param2;
                            if (opCode.parameterMode3.Value)
                            {
                                operations[i + 3] = value;
                            }
                            else
                            {
                                operations[operations[i + 3]] = value;
                            }
                            i += 4;
                        }
                        break;
                    case multiply:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            var value = param1 * param2;
                            if (opCode.parameterMode3.Value)
                            {
                                operations[i + 3] = value;
                            }
                            else
                            {
                                operations[operations[i + 3]] = value;
                            }
                            i += 4;
                        }
                        break;
                    case input:
                        {
                            if (opCode.parameterMode1.Value)
                            {
                                operations[i + 1] = tmpInput;
                            }
                            else
                            {
                                operations[operations[i + 1]] = tmpInput;
                            }
                            i += 2;
                        }
                        break;
                    case output:
                        {
                            if (opCode.parameterMode1.Value)
                            {
                                tmpOutput = operations[i + 1];
                            }
                            else
                            {
                                tmpOutput = operations[operations[i + 1]];
                            }
                            i += 2;
                        }
                        break;
                    default:
                        {
                            throw new Exception();
                        }
                }
            }

            throw new Exception();
        }


        public static long Day05_Pt2_GetResult(string[] data)
        {
            (bool? parameterMode1, bool? parameterMode2, bool? parameterMode3, int opCode) GetOpcode(int input)
            {
                var inputString = input.ToString();
                var opCode = int.Parse(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = false;
                var parameterMode2 = false;
                var parameterMode3 = false;
                if (inputString.Length == 3)
                {
                    parameterMode1 = inputString[0] == '1';
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = inputString[0] == '1';
                    parameterMode1 = inputString[1] == '1';
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = inputString[0] == '1';
                    parameterMode2 = inputString[1] == '1';
                    parameterMode1 = inputString[2] == '1';
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }
            var operations = data.Single().Split(",").Select(int.Parse).ToArray();

            const int stop = 99;
            const int add = 1;
            const int multiply = 2;
            const int input = 3;
            const int output = 4;
            const int jumpIfTrue = 5;
            const int jumpIfFalse = 6;
            const int lessThan = 7;
            const int equals = 8;

            var tmpInput = 5;
            var tmpOutput = int.MinValue;
            for (int i = 0; ;)
            {
                var opCode = GetOpcode(operations[i]);
                switch (opCode.opCode)
                {
                    case stop:
                        {
                            //return operations[0];
                            return tmpOutput;
                        }
                    case add:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            var value = param1 + param2;
                            if (opCode.parameterMode3.Value)
                            {
                                operations[i + 3] = value;
                            }
                            else
                            {
                                operations[operations[i + 3]] = value;
                            }
                            i += 4;
                        }
                        break;
                    case multiply:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            var value = param1 * param2;
                            if (opCode.parameterMode3.Value)
                            {
                                operations[i + 3] = value;
                            }
                            else
                            {
                                operations[operations[i + 3]] = value;
                            }
                            i += 4;
                        }
                        break;
                    case input:
                        {
                            if (opCode.parameterMode1.Value)
                            {
                                operations[i + 1] = tmpInput;
                            }
                            else
                            {
                                operations[operations[i + 1]] = tmpInput;
                            }
                            i += 2;
                        }
                        break;
                    case output:
                        {
                            if (opCode.parameterMode1.Value)
                            {
                                tmpOutput = operations[i + 1];
                            }
                            else
                            {
                                tmpOutput = operations[operations[i + 1]];
                            }
                            i += 2;
                        }
                        break;
                    case jumpIfTrue:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            if (param1 != 0)
                            {
                                i = param2;
                            }
                            else
                            {
                                i += 3;
                            }
                        }
                        break;
                    case jumpIfFalse:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            if (param1 == 0)
                            {
                                i = param2;
                            }
                            else
                            {
                                i += 3;
                            }
                        }
                        break;
                    case lessThan:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            var value = param1 < param2 ? 1 : 0;
                            if (opCode.parameterMode3.Value)
                            {
                                operations[i + 3] = value;
                            }
                            else
                            {
                                operations[operations[i + 3]] = value;
                            }
                            i += 4;
                        }
                        break;
                    case equals:
                        {
                            var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                            var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                            var value = param1 == param2 ? 1 : 0;
                            if (opCode.parameterMode3.Value)
                            {
                                operations[i + 3] = value;
                            }
                            else
                            {
                                operations[operations[i + 3]] = value;
                            }
                            i += 4;
                        }
                        break;
                    default:
                        {
                            throw new Exception();
                        }
                }
            }

            throw new Exception();
        }
        

        class Planet
        {
            public string Name { get; set; }
            public List<Planet> OrbitedBy { get; set; } = new List<Planet>();
            public Planet Orbits { get; set; }
        }
        public static long Day06_Pt1_GetResult(string[] data)
        {
            var dct = new Dictionary<string, Planet>();
            Planet GetOrCreate(string name)
            {
                if (!dct.ContainsKey(name))
                {
                    dct.Add(name, new Planet { Name = name });
                }
                return dct[name];
            }

            int CountUntilSource(Planet planet)
            {
                if (planet.Orbits == null)
                {
                    return 1;
                }
                else
                {
                    return 1 + CountUntilSource(planet.Orbits);
                }
            }

            foreach (var item in data)
            {
                var split = item.Split(')');
                var planet1 = GetOrCreate(split[0]);
                var planet2 = GetOrCreate(split[1]);
                planet1.OrbitedBy.Add(planet2);
                planet2.Orbits = planet1;
            }

            var sum = dct.Values.Select(x => CountUntilSource(x) - 1).Sum();
            return sum;
        }

        public static long Day06_Pt2_GetResult(string[] data)
        {
            var dct = new Dictionary<string, Planet>();
            Planet GetOrCreate(string name)
            {
                if (!dct.ContainsKey(name))
                {
                    dct.Add(name, new Planet { Name = name });
                }
                return dct[name];
            }

            var visited = new List<Planet>();
            (int, bool) CountUntil(Planet you, Planet san)
            {
                visited.Add(you);
                if (you == san)
                {
                    return (0, true);
                }

                foreach (var other in you.OrbitedBy.Union(new[] { you.Orbits }).Except(visited))
                {
                    var otherResult = CountUntil(other, san);
                    if (otherResult.Item2)
                    {
                        return (1 + otherResult.Item1, true);
                    }
                }
                return (0, false);
            }

            foreach (var item in data)
            {
                var split = item.Split(')');
                var planet1 = GetOrCreate(split[0]);
                var planet2 = GetOrCreate(split[1]);
                planet1.OrbitedBy.Add(planet2);
                planet2.Orbits = planet1;
            }

            var you = dct["YOU"];
            var santa = dct["SAN"];

            var result = CountUntil(you, santa);
            return result.Item1 -2 ;
        }

        public static long Day07_Pt1_GetResult(string[] data)
        {
            (bool? parameterMode1, bool? parameterMode2, bool? parameterMode3, int opCode) GetOpcode(int input)
            {
                var inputString = input.ToString();
                var opCode = int.Parse(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = false;
                var parameterMode2 = false;
                var parameterMode3 = false;
                if (inputString.Length == 3)
                {
                    parameterMode1 = inputString[0] == '1';
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = inputString[0] == '1';
                    parameterMode1 = inputString[1] == '1';
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = inputString[0] == '1';
                    parameterMode2 = inputString[1] == '1';
                    parameterMode1 = inputString[2] == '1';
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            const int stop = 99;
            const int add = 1;
            const int multiply = 2;
            const int input = 3;
            const int output = 4;
            const int jumpIfTrue = 5;
            const int jumpIfFalse = 6;
            const int lessThan = 7;
            const int equals = 8;

            int Execute(int[] operations, int phase, int inputValue)
            {
                var first = true;
                var tmpOutput = int.MinValue;
                for (int i = 0; ;)
                {
                    var opCode = GetOpcode(operations[i]);
                    switch (opCode.opCode)
                    {
                        case stop:
                            {
                                //return operations[0];
                                return tmpOutput;
                            }
                        case add:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 + param2;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        case multiply:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 * param2;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        case input:
                            {
                                var inputtt = first ? phase : inputValue;
                                if (first) { first = false; }
                                if (opCode.parameterMode1.Value)
                                {
                                    operations[i + 1] = inputtt;
                                }
                                else
                                {
                                    operations[operations[i + 1]] = inputtt;
                                }
                                i += 2;
                            }
                            break;
                        case output:
                            {
                                if (opCode.parameterMode1.Value)
                                {
                                    tmpOutput = operations[i + 1];
                                }
                                else
                                {
                                    tmpOutput = operations[operations[i + 1]];
                                }
                                i += 2;
                            }
                            break;
                        case jumpIfTrue:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case jumpIfFalse:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case lessThan:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 < param2 ? 1 : 0;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        case equals:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 == param2 ? 1 : 0;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }

            IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> list, int length)
            {
                if (length == 1) return list.Select(t => new int[] { t });

                return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new int[] { t2 }));
            }

            var operations = data.Single().Split(",").Select(int.Parse).ToArray();

            var permutations = GetPermutations(Enumerable.Range(0, 5), 5);
            var outputs = new List<int>();
            foreach (var permutation in permutations)
            {
                var outputValue = 0;
                foreach (var i in permutation)
                {
                    outputValue = Execute(operations, i, outputValue);
                }
                outputs.Add(outputValue);
            }

            return outputs.Max();
        }

        class StateMachine
        {
            (bool? parameterMode1, bool? parameterMode2, bool? parameterMode3, int opCode) GetOpcode(int input)
            {
                var inputString = input.ToString();
                var opCode = int.Parse(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = false;
                var parameterMode2 = false;
                var parameterMode3 = false;
                if (inputString.Length == 3)
                {
                    parameterMode1 = inputString[0] == '1';
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = inputString[0] == '1';
                    parameterMode1 = inputString[1] == '1';
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = inputString[0] == '1';
                    parameterMode2 = inputString[1] == '1';
                    parameterMode1 = inputString[2] == '1';
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            const int stop = 99;
            const int add = 1;
            const int multiply = 2;
            const int input = 3;
            const int output = 4;
            const int jumpIfTrue = 5;
            const int jumpIfFalse = 6;
            const int lessThan = 7;
            const int equals = 8;

            public int inputBuffer { get; set; }
            public int outputBuffer { get; set; }
            public int phase { get; set; }
            public int i { get; set; }
            public int[] operations { get; set; }
            public bool first { get; set; } = true;
            public int GetNext()
            {
                while (true)
                {
                    var opCode = GetOpcode(operations[i]);
                    switch (opCode.opCode)
                    {
                        case stop:
                            {
                                //return operations[0];
                                //throw new InvalidOperationException();
                                return int.MinValue;
                            }
                        case add:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 + param2;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        case multiply:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 * param2;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        case input:
                            {
                                var inputtt = first ? phase : inputBuffer;
                                if (first) { first = false; }
                                if (opCode.parameterMode1.Value)
                                {
                                    operations[i + 1] = inputtt;
                                }
                                else
                                {
                                    operations[operations[i + 1]] = inputtt;
                                }
                                i += 2;
                            }
                            break;
                        case output:
                            {
                                if (opCode.parameterMode1.Value)
                                {
                                    outputBuffer = operations[i + 1];
                                }
                                else
                                {
                                    outputBuffer = operations[operations[i + 1]];
                                }
                                i += 2;
                                return outputBuffer;
                            }
                            break;
                        case jumpIfTrue:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case jumpIfFalse:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case lessThan:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 < param2 ? 1 : 0;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        case equals:
                            {
                                var param1 = opCode.parameterMode1.Value ? operations[i + 1] : operations[operations[i + 1]];
                                var param2 = opCode.parameterMode2.Value ? operations[i + 2] : operations[operations[i + 2]];
                                var value = param1 == param2 ? 1 : 0;
                                if (opCode.parameterMode3.Value)
                                {
                                    operations[i + 3] = value;
                                }
                                else
                                {
                                    operations[operations[i + 3]] = value;
                                }
                                i += 4;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }
        }

        public static long Day07_Pt2_GetResult(string[] data)
        {
            IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> list, int length)
            {
                if (length == 1) return list.Select(t => new int[] { t });

                return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new int[] { t2 }));
            }

            var operations = data.Single().Split(",").Select(int.Parse).ToArray();

            var permutations = GetPermutations(Enumerable.Range(5, 5), 5);
            //var permutations = new int[][] { new int[] { 9, 8, 7, 6, 5 } };
            var outputs = new List<int>();
            var ampCount = 5;
            foreach (var permutation in permutations)
            {
                var amps = permutation.Select(x => new StateMachine { inputBuffer = 0, outputBuffer = 0, phase = x, operations = operations.ToArray() }).ToArray();
                var outputValue = 0;
                while (true)
                {
                    for (int i = 0; ; i++)
                    {
                        var amp = amps[i % ampCount];
                        amp.inputBuffer = outputValue;
                        var returnValue = amp.GetNext();
                        if (returnValue == int.MinValue)
                        {
                            outputs.Add(amp.inputBuffer);
                            goto label;
                            //return amp.inputBuffer;
                        }
                        else
                        {
                            outputValue = returnValue;
                        }
                    }

                    if (outputValue == 139629729)
                    {

                    }
                }
            label:
                var bla = 0;
            }

            return outputs.Max();
        }
        public static long Day08_Pt1_GetResult(string[] data)
        {
            var width = int.Parse(data[0].Split("x")[0].ToString());
            var height = int.Parse(data[0].Split("x")[1].ToString());
            var numbers = data[1].Select(x => int.Parse(x.ToString())).ToArray();
            var layer = new List<List<int>>();
            var layers = new List<List<List<int>>>();
            for (int i = 0; i < numbers.Count(); i += width)
            {
                if (i != 0 && i % (height * width) == 0)
                {
                    layers.Add(layer);
                    layer = new List<List<int>>();
                }
                layer.Add(numbers.Skip(i).Take(width).ToList());

            }
            layers.Add(layer);

            var leastZeroes = layers.OrderBy(x => x.SelectMany(y => y).Count(y => y == 0)).First();
            var values = leastZeroes.SelectMany(x => x);
            return values.Count(x => x == 1) * values.Count(x => x == 2);
        }

        public static long Day08_Pt2_GetResult(string[] data)
        {
            void PrintGrid(int[][] grid)
            {
                string Value(int x) => x == 1 ? "#" : " ";

                for (int i = 0; i < grid.Length; i++)
                {
                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        Console.Write($"{Value(grid[i][j])}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

            }

            var width = int.Parse(data[0].Split("x")[0].ToString());
            var height = int.Parse(data[0].Split("x")[1].ToString());
            var numbers = data[1].Select(x => int.Parse(x.ToString())).ToArray();
            var layerX = new List<List<int>>();
            var layers = new List<List<List<int>>>();
            for (int i = 0; i < numbers.Count(); i += width)
            {
                if (i != 0 && i % (height * width) == 0)
                {
                    layers.Add(layerX);
                    layerX = new List<List<int>>();
                }
                layerX.Add(numbers.Skip(i).Take(width).ToList());

            }
            layers.Add(layerX);
            layers.Reverse();
            var resultingLayer = Enumerable.Range(0, height).Select(x => Enumerable.Range(0, width).Select(x => 2).ToArray()).ToArray();
            foreach (var layer in layers)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        var me = layer[i][j];
                        //var other = resultingLayer[i][j];
                        if (me != 2)
                        {
                            resultingLayer[i][j] = me;
                        }
                    }
                }
            }

            PrintGrid(resultingLayer);

            return -1;
        }


        enum OpCode { Unknown = 0, Add = 1, Multiply = 2, Input = 3, Output = 4, JumpIfTrue = 5, JumpIfFalse = 6, LessThan = 7, Equals = 8, AdjustRelativeBase = 9, Stop = 99 }
        enum ParameterMode { Position = 0, Value = 1, Relative = 2 }
        class StateMachine
        {
            private ParameterMode GetParameterMode(char v) => (ParameterMode)ToInt(v);
            (ParameterMode parameterMode1, ParameterMode parameterMode2, ParameterMode parameterMode3, OpCode opCode) GetOpcode(long input)
            {
                var inputString = input.ToString();
                var opCode = (OpCode)ToInt(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = ParameterMode.Position;
                var parameterMode2 = ParameterMode.Position;
                var parameterMode3 = ParameterMode.Position;
                if (inputString.Length == 3)
                {
                    parameterMode1 = GetParameterMode(inputString[0]);
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = GetParameterMode(inputString[0]);
                    parameterMode1 = GetParameterMode(inputString[1]);
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = GetParameterMode(inputString[0]);
                    parameterMode2 = GetParameterMode(inputString[1]);
                    parameterMode1 = GetParameterMode(inputString[2]);
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            public long inputBuffer { get; set; }
            public List<long> outputBuffer { get; set; } = new List<long>();
            public long i { get; set; }
            public long relativeBase { get; set; }
            public Dictionary<long, long> operations { get; set; }

            long GetOrDefault(long address)
            {
                if (operations.TryGetValue(address, out var result))
                {
                    return result;
                }
                return 0;
            }
            long Get(ParameterMode mode, long address)
            {
                switch (mode)
                {
                    case ParameterMode.Position: return GetOrDefault(GetOrDefault(address));
                    case ParameterMode.Value: return GetOrDefault(address);
                    case ParameterMode.Relative: return GetOrDefault(relativeBase + GetOrDefault(address));
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            void Set(ParameterMode mode, long address, long value)
            {
                switch (mode)
                {
                    case ParameterMode.Position: operations[GetOrDefault(address)] = value; break;
                    case ParameterMode.Value: operations[address] = value; break;
                    case ParameterMode.Relative: operations[relativeBase + GetOrDefault(address)] = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            public int Execute()
            {
                while (true)
                {
                    var opCode = GetOpcode(operations[i]);
                    switch (opCode.opCode)
                    {
                        case OpCode.Stop:
                            {
                                //return operations[0];
                                //throw new InvalidOperationException();
                                return int.MinValue;
                            }
                        case OpCode.Add:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 + param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Multiply:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 * param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Input:
                            {
                                Set(opCode.parameterMode1, i + 1, inputBuffer);
                                i += 2;
                            }
                            break;
                        case OpCode.Output:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                outputBuffer.Add(param1);
                                i += 2;
                            }
                            break;
                        case OpCode.JumpIfTrue:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.JumpIfFalse:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.LessThan:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 < param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Equals:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 == param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.AdjustRelativeBase:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                relativeBase += param1;
                                i += 2;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }
        }

        public static long Day09_Pt1_GetResult(string[] data)
        {
            IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> list, int length)
            {
                if (length == 1) return list.Select(t => new int[] { t });

                return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new int[] { t2 }));
            }

            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }

            var machine = new StateMachine { inputBuffer = operations.Count > 500 ? 1 : 0, operations = operations };
            var output = machine.Execute();

            return output;
        }

        public static long Day09_Pt2_GetResult(string[] data)
        {
            IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> list, int length)
            {
                if (length == 1) return list.Select(t => new int[] { t });

                return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new int[] { t2 }));
            }

            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }

            var machine = new StateMachine { inputBuffer = 2 , operations = operations };
            var output = machine.Execute();

            return output;
        }

                public static long Day10_Pt1_GetResult(string[] data)
        {
            var cells = new List<Cell>();
            for (int j = 0; j < data.Length; j++)
            {
                for (int i = 0; i < data[0].Length; i++)
                {
                    var isAstroid = data[j][i] == '#';
                    if (isAstroid)
                    {
                        var cell = new Cell() { X = i, Y = j, IsAstroid = true };
                        cells.Add(cell);
                    }
                }
            }

            cells.ForEach(x => x.SetCounts(cells));

            var max = cells.Max(x => x.VisibleOthers);
            return max;
        }

        class Cell
        {
            private Dictionary<double, List<Cell>> VisibleAngles;

            public int X { get; set; }
            public int Y { get; set; }
            public int VisibleOthers { get; set; }
            public bool IsAstroid { get; set; }

            double CalculateAngle(Cell one, Cell two)
            {
                return ((Math.Atan2(one.X - two.X, one.Y - two.Y) * (180 / Math.PI)) + 360) % 360;
                //return (double)(one.X - two.X) / (double)(one.Y - two.Y);
            }

            int CalculateManhattenDistance(Cell one, Cell two) => Math.Abs(one.X - two.X) + Math.Abs(one.Y - two.Y);
            public void SetCounts(IEnumerable<Cell> cells)
            {
                VisibleAngles = cells.Where(x => x != this)
                    .Select(x => (cell: x, angle: CalculateAngle(this, x)))
                    .Select(x => (cell: x.cell, angle: x.angle == 0 ? 360 : x.angle))
                    .GroupBy(x => x.angle).OrderByDescending(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Select(y => y.cell).OrderBy(y => CalculateManhattenDistance(this, y)).ToList());
            }

            public Cell GetCell200() => VisibleAngles.ElementAt(200 - 1).Value.First();
        }


        public static long Day10_Pt2_GetResult(string[] data)
        {
            var cells = new List<Cell>();
            for (int j = 0; j < data.Length; j++)
            {
                for (int i = 0; i < data[0].Length; i++)
                {
                    var isAstroid = data[j][i] == '#';
                    if (isAstroid)
                    {
                        var cell = new Cell() { X = i, Y = j, IsAstroid = true };
                        cells.Add(cell);
                    }
                }
            }

            //sample: 210 visible: 11,13. Real: 314 visible: 27,19
            var laserCell = cells.Count > 300 ? cells.Single(x => x.X == 27 && x.Y == 19) : cells.Single(x => x.X == 11 && x.Y == 13);
            laserCell.SetCounts(cells);
            var target = laserCell.GetCell200();
            return target.X * 100+target.Y;
        }

        enum OpCode { Unknown = 0, Add = 1, Multiply = 2, Input = 3, Output = 4, JumpIfTrue = 5, JumpIfFalse = 6, LessThan = 7, Equals = 8, AdjustRelativeBase = 9, Stop = 99 }
        enum ParameterMode { Position = 0, Value = 1, Relative = 2 }
        class StateMachine
        {
            private ParameterMode GetParameterMode(char v) => (ParameterMode)ToInt(v);
            (ParameterMode parameterMode1, ParameterMode parameterMode2, ParameterMode parameterMode3, OpCode opCode) GetOpcode(long input)
            {
                var inputString = input.ToString();
                var opCode = (OpCode)ToInt(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = ParameterMode.Position;
                var parameterMode2 = ParameterMode.Position;
                var parameterMode3 = ParameterMode.Position;
                if (inputString.Length == 3)
                {
                    parameterMode1 = GetParameterMode(inputString[0]);
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = GetParameterMode(inputString[0]);
                    parameterMode1 = GetParameterMode(inputString[1]);
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = GetParameterMode(inputString[0]);
                    parameterMode2 = GetParameterMode(inputString[1]);
                    parameterMode1 = GetParameterMode(inputString[2]);
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            public long inputBuffer { get; set; }
            public List<long> outputBuffer { get; set; } = new List<long>();
            public long i { get; set; }
            public long relativeBase { get; set; }
            public Dictionary<long, long> operations { get; set; }

            long GetOrDefault(long address)
            {
                if (operations.TryGetValue(address, out var result))
                {
                    return result;
                }
                return 0;
            }
            long Get(ParameterMode mode, long address)
            {
                switch (mode)
                {
                    case ParameterMode.Position: return GetOrDefault(GetOrDefault(address));
                    case ParameterMode.Value: return GetOrDefault(address);
                    case ParameterMode.Relative: return GetOrDefault(relativeBase + GetOrDefault(address));
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            void Set(ParameterMode mode, long address, long value)
            {
                switch (mode)
                {
                    case ParameterMode.Position: operations[GetOrDefault(address)] = value; break;
                    case ParameterMode.Value: operations[address] = value; break;
                    case ParameterMode.Relative: operations[relativeBase + GetOrDefault(address)] = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            public long Execute()
            {
                while (true)
                {
                    var opCode = GetOpcode(operations[i]);
                    switch (opCode.opCode)
                    {
                        case OpCode.Stop:
                            {
                                //return operations[0];
                                //throw new InvalidOperationException();
                                return long.MinValue;
                            }
                        case OpCode.Add:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 + param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Multiply:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 * param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Input:
                            {
                                Set(opCode.parameterMode1, i + 1, inputBuffer);
                                i += 2;
                            }
                            break;
                        case OpCode.Output:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                outputBuffer.Add(param1);
                                i += 2;
                                return param1;
                            }
                            break;
                        case OpCode.JumpIfTrue:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.JumpIfFalse:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.LessThan:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 < param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Equals:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 == param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.AdjustRelativeBase:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                relativeBase += param1;
                                i += 2;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }
        }

        class Cell
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool? Black { get; set; }
            public void Paint(bool black)
            {
                Black = black;
            }
        }

        enum Direction { N, E, S, W }
        public static long Day11_Pt1_GetResult(string[] data)
        {
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }

            var cells = new Dictionary<(int x, int y), Cell>();
            Cell GetOrCreate(int x, int y)
            {
                var key = (x, y);
                if (!cells.ContainsKey(key))
                {
                    cells[key] = new Cell() { X = x, Y = y };
                }
                return cells[key];
            }

            (int x, int y) MoveInDirection(int x, int y, Direction direction)
            {
                switch (direction)
                {
                    case Direction.N: return (x, y - 1);
                    case Direction.E: return (x + 1, y);
                    case Direction.S: return (x, y + 1);
                    case Direction.W: return (x - 1, y);
                    default: throw new Exception();
                }
            }

            Direction Rotate(Direction oldDirection, bool left)
            {
                if (left)
                {
                    switch (oldDirection)
                    {
                        case Direction.N: return Direction.W;
                        case Direction.E: return Direction.N;
                        case Direction.S: return Direction.E;
                        case Direction.W: return Direction.S;
                    }
                }
                else
                {
                    switch (oldDirection)
                    {
                        case Direction.N: return Direction.E;
                        case Direction.E: return Direction.S;
                        case Direction.S: return Direction.W;
                        case Direction.W: return Direction.N;
                    }
                }
                throw new Exception();
            }

            void PrintGrid(List<Cell> whiteCells)
            {
                var minX = whiteCells.Min(x => x.X);
                var maxX = whiteCells.Max(x => x.X);
                var minY = whiteCells.Min(x => x.Y);
                var maxY = whiteCells.Max(x => x.Y);

                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minY; x <= maxX; x++)
                    {
                        if (whiteCells.Any(z => z.X == x && z.Y == y))
                        {
                            Console.Write("X");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
            }

            var currentCell = GetOrCreate(0, 0);
            currentCell.Black = false;

            var machine = new StateMachine { inputBuffer = 0, operations = operations };
            var stopOutput = long.MinValue;
            var direction = Direction.N;
            while (true)
            {
                machine.inputBuffer = currentCell.Black.HasValue ? (currentCell.Black.Value ? 0 : 1) : 0;
                var outputColor = machine.Execute();
                if (outputColor == stopOutput) { break; }
                var outputDirectionChange = machine.Execute();
                var isMoveLeft = outputDirectionChange == 0;

                currentCell.Black = outputColor == 0;
                direction = Rotate(direction, isMoveLeft);
                //direction = newDirection;
                //Console.WriteLine(newDirection);
                var newDestination = MoveInDirection(currentCell.X, currentCell.Y, direction);
                currentCell = GetOrCreate(newDestination.x, newDestination.y);
            }

            var whiteCells = cells.Select(x => x.Value).Where(x => x.Black.HasValue && !x.Black.Value).ToList();
            PrintGrid(whiteCells);
            var count = cells.Count(x => x.Value.Black.HasValue);
            return count; // 2721 too high


        }


    

        public static long Day12_Pt1_GetResult(string[] data)
        {
            var moons = data
                .Select(x => x.Replace("<", "").Replace(">", ""))
                .Select(x => x.Split(", ")).Select(x => (X: x[0].Split("=")[1], Y: x[1].Split("=")[1], Z: x[2].Split("=")[1]))
                .Select(x => (X: ToInt(x.X), Y: ToInt(x.Y), Z: ToInt(x.Z)))
                .Select(x => new Moon { Position = new Coordinate { X = x.X, Y = x.Y, Z = x.Z }, Velocity = new Coordinate() })
                .ToList();

            Console.WriteLine($"Step 0");
            moons.ForEach(x => Console.WriteLine(x));
            //moons.ForEach(x =>
            //{
            //    x.NewVelocity = x.Velocity.Clone();
            //    x.NewPosition = x.Position.Clone();
            //});
            for (int i = 0; i < 1000; i++)
            {

                foreach (var moon in moons)
                {
                    foreach (var otherMoon in moons.Where(x => x != moon))
                    {
                        var xIncrease = moon.Position.X == otherMoon.Position.X ? (bool?)null : moon.Position.X < otherMoon.Position.X;
                        var yIncrease = moon.Position.Y == otherMoon.Position.Y ? (bool?)null : moon.Position.Y < otherMoon.Position.Y;
                        var zIncrease = moon.Position.Z == otherMoon.Position.Z ? (bool?)null : moon.Position.Z < otherMoon.Position.Z;
                        if (xIncrease.HasValue)
                        {
                            if (xIncrease.Value)
                            {
                                moon.Velocity.X++;
                            }
                            else
                            {
                                moon.Velocity.X--;
                            }
                        }
                        if (yIncrease.HasValue)
                        {
                            if (yIncrease.Value)
                            {
                                moon.Velocity.Y++;
                            }
                            else
                            {
                                moon.Velocity.Y--;
                            }
                        }
                        if (zIncrease.HasValue)
                        {
                            if (zIncrease.Value)
                            {
                                moon.Velocity.Z++;
                            }
                            else
                            {
                                moon.Velocity.Z--;
                            }
                        }
                    }
                }

                foreach (var moon in moons)
                {
                    moon.Position.X += moon.Velocity.X;
                    moon.Position.Y += moon.Velocity.Y;
                    moon.Position.Z += moon.Velocity.Z;
                }

                //Console.WriteLine($"Step {i + 1}");
            }
                moons.ForEach(x => Console.WriteLine(x));

            var energy = moons.Sum(x => x.Product());

            return energy; // 14645 too high
        }

        class Coordinate
        {
            public long X { get; set; }
            public long Y { get; set; }
            public long Z { get; set; }
            public override string ToString() => $"{X,3},{Y,3},{Z,3}";

            public Coordinate Clone() => new Coordinate { X = X, Y = Y, Z = Z };
            long Abs(long x) => Math.Abs(x);
            public long Sum() => Abs(X) + Abs(Y) + Abs(Z);
            public long Hash() => ToString().GetHashCode();
        }
        class Moon
        {
            public Coordinate Position { get; set; }
            public Coordinate Velocity { get; set; }
            public long Product() => Position.Sum() * Velocity.Sum();

            //public Coordinate NewPosition { get; set; }
            //public Coordinate NewVelocity { get; set; }
            public override string ToString() => $"P:{Position} V:{Velocity}";

            public string Hash() => (Position.ToString() + Velocity.ToString());
            public string XHash() => (Position.X.ToString() + Velocity.X.ToString());
            public string YHash() => (Position.Y.ToString() + Velocity.Y.ToString());
            public string ZHash() => (Position.Z.ToString() + Velocity.Z.ToString());
        }
        public static long Day12_Pt1_GetResult(string[] data)
        {
            var moons = data
                .Select(x => x.Replace("<", "").Replace(">", ""))
                .Select(x => x.Split(", ")).Select(x => (X: x[0].Split("=")[1], Y: x[1].Split("=")[1], Z: x[2].Split("=")[1]))
                .Select(x => (X: ToInt(x.X), Y: ToInt(x.Y), Z: ToInt(x.Z)))
                .Select(x => new Moon { Position = new Coordinate { X = x.X, Y = x.Y, Z = x.Z }, Velocity = new Coordinate() })
                .ToList();

            Console.WriteLine($"Step 0");
            moons.ForEach(x => Console.WriteLine(x));
            //moons.ForEach(x =>
            //{
            //    x.NewVelocity = x.Velocity.Clone();
            //    x.NewPosition = x.Position.Clone();
            //});
            var visitedX = new HashSet<string>();
            var visitedY = new HashSet<string>();
            var visitedZ = new HashSet<string>();

            long xRepeat = 0;
            long yRepeat = 0;
            long zRepeat = 0;
            var stopwatch = Stopwatch.StartNew();
            for (long i = 0; ; i++)
            {
                if (i % 100_000 == 0)
                {
                    Console.WriteLine($"Step {i}: {stopwatch.Elapsed}");
                    moons.ForEach(x => Console.WriteLine(x));
                }
                var xHash = moons[0].XHash() + moons[1].XHash() + moons[2].XHash() + moons[3].XHash();
                var yHash = moons[0].YHash() + moons[1].YHash() + moons[2].YHash() + moons[3].YHash();
                var zHash = moons[0].ZHash() + moons[1].ZHash() + moons[2].ZHash() + moons[3].ZHash();
                if (!visitedX.Add(xHash))
                {
                    if (xRepeat == 0) { xRepeat = i; }
                    if (xRepeat != 0 && yRepeat != 0 && zRepeat != 0) { break; }
                }
                if (!visitedY.Add(yHash))
                {
                    if (yRepeat == 0) { yRepeat = i; }
                    if (xRepeat != 0 && yRepeat != 0 && zRepeat != 0) { break; }
                }
                if (!visitedZ.Add(zHash))
                {
                    if (zRepeat == 0) { zRepeat = i; }
                    if (xRepeat != 0 && yRepeat != 0 && zRepeat != 0) { break; }
                }
                foreach (var moon in moons)
                {
                    foreach (var otherMoon in moons.Where(x => x != moon))
                    {
                        var xIncrease = moon.Position.X == otherMoon.Position.X ? (bool?)null : moon.Position.X < otherMoon.Position.X;
                        var yIncrease = moon.Position.Y == otherMoon.Position.Y ? (bool?)null : moon.Position.Y < otherMoon.Position.Y;
                        var zIncrease = moon.Position.Z == otherMoon.Position.Z ? (bool?)null : moon.Position.Z < otherMoon.Position.Z;
                        if (xIncrease.HasValue)
                        {
                            if (xIncrease.Value)
                            {
                                moon.Velocity.X++;
                            }
                            else
                            {
                                moon.Velocity.X--;
                            }
                        }
                        if (yIncrease.HasValue)
                        {
                            if (yIncrease.Value)
                            {
                                moon.Velocity.Y++;
                            }
                            else
                            {
                                moon.Velocity.Y--;
                            }
                        }
                        if (zIncrease.HasValue)
                        {
                            if (zIncrease.Value)
                            {
                                moon.Velocity.Z++;
                            }
                            else
                            {
                                moon.Velocity.Z--;
                            }
                        }
                    }
                }

                foreach (var moon in moons)
                {
                    moon.Position.X += moon.Velocity.X;
                    moon.Position.Y += moon.Velocity.Y;
                    moon.Position.Z += moon.Velocity.Z;
                }

                //Console.WriteLine($"Step {i + 1}");
            }

            long gcf(long  a, long b)
            {
                while (b != 0)
                {
                    var temp = b;
                    b = a % b;
                    a = temp;
                }
                return a;
            }
            long lcm(long a, long b)
            {
                return (a / gcf(a, b)) * b;
            }

            var result = lcm(xRepeat, lcm(yRepeat, zRepeat)); // lcm code cheated!

            return result; // 803881 too low
        }


        enum OpCode { Unknown = 0, Add = 1, Multiply = 2, Input = 3, Output = 4, JumpIfTrue = 5, JumpIfFalse = 6, LessThan = 7, Equals = 8, AdjustRelativeBase = 9, Stop = 99 }
        enum ParameterMode { Position = 0, Value = 1, Relative = 2 }
        class StateMachine
        {
            private ParameterMode GetParameterMode(char v) => (ParameterMode)ToInt(v);
            (ParameterMode parameterMode1, ParameterMode parameterMode2, ParameterMode parameterMode3, OpCode opCode) GetOpcode(long input)
            {
                var inputString = input.ToString();
                var opCode = (OpCode)ToInt(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = ParameterMode.Position;
                var parameterMode2 = ParameterMode.Position;
                var parameterMode3 = ParameterMode.Position;
                if (inputString.Length == 3)
                {
                    parameterMode1 = GetParameterMode(inputString[0]);
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = GetParameterMode(inputString[0]);
                    parameterMode1 = GetParameterMode(inputString[1]);
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = GetParameterMode(inputString[0]);
                    parameterMode2 = GetParameterMode(inputString[1]);
                    parameterMode1 = GetParameterMode(inputString[2]);
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            public long inputBuffer { get; set; }
            public List<long> outputBuffer { get; set; } = new List<long>();
            public long i { get; set; }
            public long relativeBase { get; set; }
            public Dictionary<long, long> operations { get; set; }

            long GetOrDefault(long address)
            {
                if (operations.TryGetValue(address, out var result))
                {
                    return result;
                }
                return 0;
            }
            long Get(ParameterMode mode, long address)
            {
                switch (mode)
                {
                    case ParameterMode.Position: return GetOrDefault(GetOrDefault(address));
                    case ParameterMode.Value: return GetOrDefault(address);
                    case ParameterMode.Relative: return GetOrDefault(relativeBase + GetOrDefault(address));
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            void Set(ParameterMode mode, long address, long value)
            {
                switch (mode)
                {
                    case ParameterMode.Position: operations[GetOrDefault(address)] = value; break;
                    case ParameterMode.Value: operations[address] = value; break;
                    case ParameterMode.Relative: operations[relativeBase + GetOrDefault(address)] = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            private bool first = true;
            public long Execute()
            {
                while (true)
                {
                    var opCode = GetOpcode(operations[i]);
                    switch (opCode.opCode)
                    {
                        case OpCode.Stop:
                            {
                                //return operations[0];
                                //throw new InvalidOperationException();
                                return long.MinValue;
                            }
                        case OpCode.Add:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 + param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Multiply:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 * param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Input:
                            {
                                //Set(opCode.parameterMode1, i + 1, first ? 2 : inputBuffer);
                                Set(opCode.parameterMode1, i + 1, inputBuffer);
                                first = false;
                                i += 2;
                            }
                            break;
                        case OpCode.Output:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                outputBuffer.Add(param1);
                                i += 2;
                                return param1;
                            }
                            break;
                        case OpCode.JumpIfTrue:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.JumpIfFalse:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.LessThan:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 < param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Equals:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 == param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.AdjustRelativeBase:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                relativeBase += param1;
                                i += 2;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }
        }

        public static long Day13_Pt1_GetResult(string[] data)
        {
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }

            var machine = new StateMachine { inputBuffer = 0, operations = operations };
            var blocks = new HashSet<(long, long)>();
            var stopOutput = long.MinValue;
            while (true)
            {
                var output1 = machine.Execute();
                if (output1 == stopOutput) { break; }
                var output2 = machine.Execute();
                var output3 = machine.Execute();

                if (output3 == 2)
                {
                    blocks.Add((output1, output2));
                }
                else
                {
                    blocks.Remove((output1, output2));
                }
            }

            return blocks.Count;
        }

        class Cell
        {
            public long X { get; set; }
            public long Y { get; set; }
            public long Value { get; set; }
            public override string ToString()
            {
                switch (Value)
                {
                    default: throw new Exception();
                    case 0: return " ";
                    case 1: return "|";
                    case 2: return "#";
                    case 3: return "Ü";
                    case 4: return "O";
                }
            }
        }
        //        0 is an empty tile.No game object appears in this tile.
        //1 is a wall tile.Walls are indestructible barriers.
        //2 is a block tile.Blocks can be broken by the ball.
        //3 is a horizontal paddle tile. The paddle is indestructible.
        //4 is a ball tile.The ball moves diagonally and bounces off objects.
        public static string Day13_Pt2_GetResult(string[] data)
        {
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }


            var cells = new Dictionary<(long x, long y), Cell>();
            Cell GetOrCreate(long x, long y)
            {
                var key = (x, y);
                if (!cells.ContainsKey(key))
                {
                    cells[key] = new Cell() { X = x, Y = y };
                }
                return cells[key];
            }

            void PrintGrid(List<Cell> cells, long score)
            {
                var minX = cells.Min(x => x.X);
                var maxX = cells.Max(x => x.X);
                var minY = cells.Min(x => x.Y);
                var maxY = cells.Max(x => x.Y);

                Console.Clear();
                Console.WriteLine("Score" + score);
                for (long y = minY; y <= maxY; y++)
                {
                    for (long x = minX; x <= maxX; x++)
                    {
                        var cell = cells.SingleOrDefault(z => z.X == x && z.Y == y);
                        if (cell != null)
                        {
                            Console.Write(cell.ToString());
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }

            }

            var ballHits = new List<(long, long)>();
            string BallHitToString((long, long) x) => $",{{{x.Item1}, {x.Item2}}}";

            int? originalBlocksCount = null;
            var goTo = new Dictionary<int, int> { { 1, 22 }, { 18, 21 }, { 41, 15 }, { 68, 15 }, { 82, 15 }, { 106, 17 },
                { 152, 25 }, { 166, 25 }, { 300, 17 }, { 318, 17 }, { 352, 17 }, { 378, 17 }, { 418, 15 }, { 444, 15 }, { 484, 15 },
                { 510, 27 },{532, 27},{554, 27},{584, 13},{610, 13},{642, 13},{676, 13},{710, 13},{776, 29},{808, 29},{828, 29},{956, 9}
                ,{996, 9},{1010, 9},{1186, 19},{1206, 19},{1220, 19},{1326, 27},{1342, 27},{1380, 27},{1406, 27},{1458, 25},{1462, 25},{1480, 33}
                ,{1494, 33},{1510, 39},{1520, 39},{1536, 35},{1544, 35},{1566, 25},{1584, 25},{1632, 3}, {1674, 3},{1690, 3},{1740, 3},{1758, 3},{1836, 25}
                ,{1880, 25},{1936, 25},{1986, 25},{2578, 29},{2586, 29},{2644, 29},{2824, 27},{2828, 27},{2892, 29},{2896, 29},{2994, 29},{3134, 23}
                ,{3232, 23},{3290, 23},{3316, 23},{3378, 23},{3460, 39},{3492, 39},{3506, 39},{3590, 5},{3676, 5},{3756, 35},{3816, 35},{3882, 35},{3962, 5}
                ,{4022, 5},{4044, 7},{4048, 7},{4114, 7},{4156, 7},{4238, 7},{4284, 17},{4304, 17},{4334, 17},{4380, 17},{4460, 23},{4472, 23},{4564, 31}
                ,{4580, 31},{4660, 9},{4704, 9},{4726, 9},{4806, 31},{4850, 31},{4958, 7},{5380, 7},{5460, 33},{5512, 33},{5638, 29},{5652, 29},{5776, 25}
                ,{5972, 25},{6052, 15},{6120, 15},{6200, 25},{6220, 25},{6350, 23},{6428, 17},{6490, 17},{6570, 23},{6680, 23},{6760, 17},{6772, 17},{6852, 25}
                ,{6868, 25},{6948, 15},{7034, 15},{7114, 25},{7134, 25},{7214, 17},{7230, 17},{7310, 23},{7424, 23},{7504, 17},{7516, 17},{7596, 25},{7612, 25},{7692, 15}
                ,{7712, 15},{7792, 27},{7816, 27},{7898, 39},{7922, 39},{8004, 21},{8128, 21},{8208, 19},{8262, 19},{8342, 21},{8408, 21},{8488, 19},{8492, 19},{8572, 23}
                ,{8580, 23},{8660, 17},{8672, 17},{8752, 25},{8768, 25},{8848, 15},{8868, 15},{8948, 27},{9082, 27},{9162, 15},{9186, 15},{9266, 25},{9286, 25},{9366, 17}
                ,{9382, 17},{9462, 23},{9474, 23},{9554, 19},{9562, 19},{9642, 21},{9726, 21},{9806, 19},{9810, 19},{9890, 23}

            };

            while (true)
            {


                var machine = new StateMachine { inputBuffer = 1, operations = operations };
                var scores = new List<long>();
                var stopOutput = long.MinValue;
                var first = true;
                var shoot = false;
                var paddleStep = 0;
                var padelHorizontal = 0L;
                var padelVertical = 0L;
                var ballHorizontal = 0L;
                var ballVertical = 0L;
                int ball = 0;
                int goToIndex = 0;
                var previousBallVertical = long.MaxValue;


                int z = 0;
                int GetTarget()
                {
                    var next = goTo.Where(x => x.Key >= z).OrderBy(x => x.Key).FirstOrDefault();
                    if (next.Key == default)
                    {
                        return goTo.Last().Value;
                    }
                    else
                    {
                        return next.Value;
                    }
                }
                while (true)
                {
                    if (ball > 1)
                    {
                        if (originalBlocksCount == null)
                        {
                            originalBlocksCount = cells.Values.Count(x => x.Value == 2);
                        }

                        //PrintGrid(cells.Values.ToList(), scores.LastOrDefault());
                        //Console.WriteLine(z);
                        z++;

                        //var x = Console.ReadKey();
                        //if (x.Key == ConsoleKey.Enter) { machine.inputBuffer = 2; }
                        //if (x.Key == ConsoleKey.LeftArrow) { machine.inputBuffer = -1; }
                        //if (x.Key == ConsoleKey.Spacebar) { machine.inputBuffer = 0; }
                        //if (x.Key == ConsoleKey.RightArrow) { machine.inputBuffer = 1; }
                    }

                    var output1 = machine.Execute();
                    if (output1 == stopOutput) { break; }
                    var output2 = machine.Execute();
                    var output3 = machine.Execute();

                    if (output1 == -1 && output2 == 0)
                    {
                        scores.Add(output3);
                    }
                    else
                    {
                        var cell = GetOrCreate(output1, output2);
                        cell.Value = output3;
                        if (output3 == 3)
                        {
                            paddleStep++;
                            padelHorizontal = output1;
                            padelVertical = output2;
                        }
                        if (output3 == 4)
                        {
                            ball++;
                            ballHorizontal = output1;
                            ballVertical = output2;
                            if (ballVertical == padelVertical - 1)
                            {
                                ballHits.Add((z, ballHorizontal));
                                Console.WriteLine(BallHitToString(ballHits.Last()));
                                //Console.Read();
                            }
                            previousBallVertical = ballVertical;
                        }
                    }

                    var target = GetTarget();
                    machine.inputBuffer = target == padelHorizontal ? 0 : target > padelHorizontal ? 1 : -1;
                    //var goLeft = padelHorizontal > ballHorizontal;
                    //var goRight = padelHorizontal < ballHorizontal;
                    //machine.inputBuffer = goLeft ? -1 : 1;

                }

                if(cells.Values.Count(x => x.Value == 2) == 0)
                {
                    PrintGrid(cells.Values.ToList(), scores.Max());
                    Console.WriteLine($"Blocks left: {cells.Values.Count(x => x.Value == 2)} / {originalBlocksCount}");
                    Console.WriteLine(string.Join("", ballHits.Select(BallHitToString)));
                    Console.Read();
                }

                var lastGoTo = goTo.Last();
                var lastHitAtSamePosition = ballHits.Last(x => x.Item2 == lastGoTo.Value);
                if(lastHitAtSamePosition.Item1 > lastGoTo.Key)
                {
                    goTo[(int)lastHitAtSamePosition.Item1] = (int)lastHitAtSamePosition.Item2;
                }
                var lastHit = ballHits.Last();
                goTo[(int)lastHit.Item1] = (int)lastHit.Item2;

               
            }

            return "";
            //return scores.Max().ToString();
            //return string.Join("", ballHits.Select(BallHitToString)); // 33 * 273 = 9009
            // 9009 too low
        }


        public static long Day14_Pt1_GetResult(string[] data)
        {
            var materials = data.Select(x => x.Split(" => ")[1]).Select(x => (count: x.Split(" ")[0], name: x.Split(" ")[1])).Select(x => new Material { Count = ToInt(x.count), Name = x.name }).ToDictionary(x => x.Name, x => x);
            var ore = new Material { Name = "ORE" };
            materials.Add("ORE", ore);
            foreach (var item in data)
            {
                var split = item.Split(" => ");
                var destinationName = split[1].Split(" ")[1];
                var destination = materials[destinationName];
                var sources = split[0].Split(", ");
                foreach (var source in sources)
                {
                    var splitSource = source.Split(" ");
                    var count = ToInt(splitSource[0]);
                    var name = splitSource[1];
                    var sourceMaterial = materials[name];
                    destination.SourceMaterials.Add(sourceMaterial, count);
                }
            }
            var fuel = materials["FUEL"];
            


            var oreUsed = 0;
            var inventory = materials.ToDictionary(x => x.Value, x => 0);
            void GetOrCreate(Material material, int count)
            {
                if (material == ore)
                {
                    oreUsed += count;
                }
                else
                {

                    var neededToCreate = count - inventory[material];
                    if (neededToCreate > 0)
                    {
                        var unitsToCreate = (int)Math.Ceiling((double)neededToCreate / material.Count);
                        foreach (var sourceMaterial in material.SourceMaterials)
                        {
                            GetOrCreate(sourceMaterial.Key, sourceMaterial.Value * unitsToCreate);
                        }
                        inventory[material] += unitsToCreate * material.Count;
                    };
                    inventory[material] -= count;
                }
            }

            GetOrCreate(fuel, 1);
            return oreUsed;
        }


        class Material
        {
            public int Count { get; set; }
            public string Name { get; set; }
            public Dictionary<Material, int> SourceMaterials { get; set; } = new Dictionary<Material, int>();
            public long Index { get; set; }
            public override string ToString()
            {
                return Name;
            }
        }
        public static double Day14_Pt2_GetResult(string[] data)
        {
            var materials = data.Select(x => x.Split(" => ")[1]).Select(x => (count: x.Split(" ")[0], name: x.Split(" ")[1])).Select(x => new Material { Count = ToInt(x.count), Name = x.name }).ToDictionary(x => x.Name, x => x);
            var ore = new Material { Name = "ORE" };
            materials.Add("ORE", ore);
            foreach (var item in data)
            {
                var split = item.Split(" => ");
                var destinationName = split[1].Split(" ")[1];
                var destination = materials[destinationName];
                var sources = split[0].Split(", ");
                foreach (var source in sources)
                {
                    var splitSource = source.Split(" ");
                    var count = ToInt(splitSource[0]);
                    var name = splitSource[1];
                    var sourceMaterial = materials[name];
                    destination.SourceMaterials.Add(sourceMaterial, count);
                }
            }
            var fuel = materials["FUEL"];



            //var oreUsed = 0;
            var inventory = materials.ToDictionary(x => x.Value, x => 0L);
            long GetOrCreate(Material material, long count, long oreUsed)
            {
                if (material == ore)
                {
                    oreUsed += count;
                }
                else
                {

                    var neededToCreate = count - inventory[material];
                    if (neededToCreate > 0)
                    {
                        var unitsToCreate = (long)Math.Ceiling((double)neededToCreate / material.Count);
                        foreach (var sourceMaterial in material.SourceMaterials)
                        {
                            oreUsed = GetOrCreate(sourceMaterial.Key, sourceMaterial.Value * unitsToCreate, oreUsed);
                        }
                        inventory[material] += unitsToCreate * material.Count;
                    };
                    inventory[material] -= count;
                }
                return oreUsed;
            }

            Console.WriteLine(GetOrCreate(fuel, 998536, 0));

            var aTrilli = 1000000000000; // 1000000000000
            var oreUsedForOneFuel = GetOrCreate(fuel, 1, 0);

            var fuelCreated = aTrilli / oreUsedForOneFuel;

            foreach (var material in inventory)
            {
                inventory[material.Key] *= fuelCreated;
            }

            var remainingOre = aTrilli - fuelCreated * oreUsedForOneFuel;

            while (true)
            {
                var usedOre = GetOrCreate(fuel, 1, 0);
                if (usedOre > remainingOre)
                {
                    break;
                }
                remainingOre -= usedOre;
                fuelCreated++;
            }

            return fuelCreated;
        }// 998537 too high

        public static string Day15_Pt1_GetResult(string[] data)
        {
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }


            var cells = new Dictionary<(long x, long y), Cell>();
            Cell GetOrCreate(long x, long y)
            {
                var key = (x, y);
                if (!cells.ContainsKey(key))
                {
                    cells[key] = new Cell() { X = x, Y = y };
                }
                return cells[key];
            }

            void PrintGrid(IEnumerable<Cell> cells)
            {
                var minX = cells.Min(x => x.X);
                var maxX = cells.Max(x => x.X);
                var minY = cells.Min(x => x.Y);
                var maxY = cells.Max(x => x.Y);

                Console.Clear();
                for (long y = minY; y <= maxY; y++)
                {
                    for (long x = minX; x <= maxX; x++)
                    {
                        var cell = cells.SingleOrDefault(z => z.X == x && z.Y == y);
                        if (cell != null)
                        {
                            Console.Write(cell.ToString());
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }

            }

            var machine = new StateMachine { inputBuffer = 1, operations = operations };
            var x = 0;
            var y = 0;
            var stopOutput = long.MinValue;
            var lastDroidLocation = GetOrCreate(0, 0);
            lastDroidLocation.HasDroid = true;
            lastDroidLocation.IsStart = true;
            lastDroidLocation.Value = 1;
            PrintGrid(cells.Values);
            var random = new Random();
            int j = 0;
            while (true)
            {
                j++;
                if (j % 10000 == 0)
                {
                    PrintGrid(cells.Values);
                }
                var newX = x;
                var newY = y;
                var input = random.Next(1, 5);
                //if (input.Key == ConsoleKey.UpArrow) { machine.inputBuffer = 1; newY += -1; }
                //if (input.Key == ConsoleKey.DownArrow) { machine.inputBuffer = 2; newY += 1; }
                //if (input.Key == ConsoleKey.RightArrow) { machine.inputBuffer = 3; newX += 1; }
                //if (input.Key == ConsoleKey.LeftArrow) { machine.inputBuffer = 4; newX += -1; }
                if (input == 1) { machine.inputBuffer = 1; newY += -1; }
                if (input == 2) { machine.inputBuffer = 2; newY += 1; }
                if (input == 3) { machine.inputBuffer = 3; newX += 1; }
                if (input == 4) { machine.inputBuffer = 4; newX += -1; }


                var output = machine.Execute();
                if (output == stopOutput) { throw new Exception(); }
                var cell = GetOrCreate(newX, newY);
                cell.Value = output;
                if (output != 0)
                {
                    lastDroidLocation.HasDroid = false;
                    cell.HasDroid = true;
                    x = newX;
                    y = newY;
                    lastDroidLocation = cell;
                }
                if (output == 2)
                {
                    PrintGrid(cells.Values);
                    //return "";
                }


                if (y == 46545645654)
                {
                    PrintGrid(cells.Values);
                }
            }

            return "";
        }

        enum OpCode { Unknown = 0, Add = 1, Multiply = 2, Input = 3, Output = 4, JumpIfTrue = 5, JumpIfFalse = 6, LessThan = 7, Equals = 8, AdjustRelativeBase = 9, Stop = 99 }
        enum ParameterMode { Position = 0, Value = 1, Relative = 2 }
        class StateMachine
        {
            private ParameterMode GetParameterMode(char v) => (ParameterMode)ToInt(v);
            (ParameterMode parameterMode1, ParameterMode parameterMode2, ParameterMode parameterMode3, OpCode opCode) GetOpcode(long input)
            {
                var inputString = input.ToString();
                var opCode = (OpCode)ToInt(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = ParameterMode.Position;
                var parameterMode2 = ParameterMode.Position;
                var parameterMode3 = ParameterMode.Position;
                if (inputString.Length == 3)
                {
                    parameterMode1 = GetParameterMode(inputString[0]);
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = GetParameterMode(inputString[0]);
                    parameterMode1 = GetParameterMode(inputString[1]);
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = GetParameterMode(inputString[0]);
                    parameterMode2 = GetParameterMode(inputString[1]);
                    parameterMode1 = GetParameterMode(inputString[2]);
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            public long inputBuffer { get; set; }
            public List<long> outputBuffer { get; set; } = new List<long>();
            public long i { get; set; }
            public long relativeBase { get; set; }
            public Dictionary<long, long> operations { get; set; }

            long GetOrDefault(long address)
            {
                if (operations.TryGetValue(address, out var result))
                {
                    return result;
                }
                return 0;
            }
            long Get(ParameterMode mode, long address)
            {
                switch (mode)
                {
                    case ParameterMode.Position: return GetOrDefault(GetOrDefault(address));
                    case ParameterMode.Value: return GetOrDefault(address);
                    case ParameterMode.Relative: return GetOrDefault(relativeBase + GetOrDefault(address));
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            void Set(ParameterMode mode, long address, long value)
            {
                switch (mode)
                {
                    case ParameterMode.Position: operations[GetOrDefault(address)] = value; break;
                    case ParameterMode.Value: operations[address] = value; break;
                    case ParameterMode.Relative: operations[relativeBase + GetOrDefault(address)] = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            private bool first = true;
            public long Execute()
            {
                while (true)
                {
                    var opCode = GetOpcode(operations[i]);
                    switch (opCode.opCode)
                    {
                        case OpCode.Stop:
                            {
                                //return operations[0];
                                //throw new InvalidOperationException();
                                return long.MinValue;
                            }
                        case OpCode.Add:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 + param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Multiply:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 * param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Input:
                            {
                                //Set(opCode.parameterMode1, i + 1, first ? 2 : inputBuffer);
                                Set(opCode.parameterMode1, i + 1, inputBuffer);
                                first = false;
                                i += 2;
                            }
                            break;
                        case OpCode.Output:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                outputBuffer.Add(param1);
                                i += 2;
                                return param1;
                            }
                            break;
                        case OpCode.JumpIfTrue:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.JumpIfFalse:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.LessThan:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 < param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Equals:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 == param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.AdjustRelativeBase:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                relativeBase += param1;
                                i += 2;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }
        }

        class Cell
        {
            public long X { get; set; }
            public long Y { get; set; }
            public long Value { get; set; }
            public bool HasDroid { get; set; }
            public bool IsStart { get; set; }
            public bool IsTank { get; set; }
            public bool IsSpace { get; set; }
            public bool WasReached { get; set; }

            public override string ToString()
            {
                if (WasReached) { return "x"; }
                switch (Value)
                {
                    default: throw new Exception();
                    case 0: return "#";
                    case 1: return IsStart ? "0" : (HasDroid ? "D" : ".");
                    case 2: return "!";
                }
            }
        }


        public static string Day15_Pt2_GetResult(string[] data)
        {
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }


            var cells = new Dictionary<(long x, long y), Cell>();
            Cell GetOrCreate(long x, long y)
            {
                var key = (x, y);
                if (!cells.ContainsKey(key))
                {
                    cells[key] = new Cell() { X = x, Y = y };
                }
                return cells[key];
            }

            void PrintGrid(IEnumerable<Cell> cells)
            {
                var minX = cells.Min(x => x.X);
                var maxX = cells.Max(x => x.X);
                var minY = cells.Min(x => x.Y);
                var maxY = cells.Max(x => x.Y);

                Console.Clear();
                for (long y = minY; y <= maxY; y++)
                {
                    for (long x = minX; x <= maxX; x++)
                    {
                        var cell = cells.SingleOrDefault(z => z.X == x && z.Y == y);
                        if (cell != null)
                        {
                            Console.Write(cell.ToString());
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }

            }

            var machine = new StateMachine { inputBuffer = 1, operations = operations };
            var x = 0;
            var y = 0;
            var stopOutput = long.MinValue;
            var lastDroidLocation = GetOrCreate(0, 0);
            lastDroidLocation.HasDroid = true;
            lastDroidLocation.IsStart = true;
            lastDroidLocation.Value = 1;
            PrintGrid(cells.Values);
            var random = new Random();
            int j = 0;
            while (j < 2_000_000)
            {
                j++;
                if (j % 100000 == 0)
                {
                }
                var newX = x;
                var newY = y;
                var input = random.Next(1, 5);
                //if (input.Key == ConsoleKey.UpArrow) { machine.inputBuffer = 1; newY += -1; }
                //if (input.Key == ConsoleKey.DownArrow) { machine.inputBuffer = 2; newY += 1; }
                //if (input.Key == ConsoleKey.RightArrow) { machine.inputBuffer = 3; newX += 1; }
                //if (input.Key == ConsoleKey.LeftArrow) { machine.inputBuffer = 4; newX += -1; }
                if (input == 1) { machine.inputBuffer = 1; newY += -1; }
                if (input == 2) { machine.inputBuffer = 2; newY += 1; }
                if (input == 3) { machine.inputBuffer = 3; newX += 1; }
                if (input == 4) { machine.inputBuffer = 4; newX += -1; }


                var output = machine.Execute();
                if (output == stopOutput) { throw new Exception(); }
                var cell = GetOrCreate(newX, newY);
                cell.Value = output;
                if (output != 0)
                {
                    cell.IsSpace = output == 1;
                    cell.IsTank = output == 2;
                    lastDroidLocation.HasDroid = false;
                    cell.HasDroid = true;
                    x = newX;
                    y = newY;
                    lastDroidLocation = cell;
                }
                if (output == 2)
                {
                    //PrintGrid(cells.Values);
                    //return "";
                }


                if (y == 46545645654)
                {
                    PrintGrid(cells.Values);
                }
            }

            PrintGrid(cells.Values);
            int k = 0;
            while (true)
            {
                k++;
                foreach (var item in cells.Values.Where(x => x.WasReached || x.IsTank).ToList())
                {
                    cells.TryGetValue((item.X - 1, item.Y), out var left);
                    cells.TryGetValue((item.X + 1, item.Y), out var right);
                    cells.TryGetValue((item.X, item.Y - 1), out var up);
                    cells.TryGetValue((item.X, item.Y + 1), out var down);
                    if (left.IsSpace) { left.WasReached = true; }
                    if (right.IsSpace) { right.WasReached = true; }
                    if (up.IsSpace) { up.WasReached = true; }
                    if (down.IsSpace) { down.WasReached = true; }
                }
                //PrintGrid(cells.Values);

                if (cells.Values.Where(x => x.IsSpace).All(x => x.WasReached))
                {
                    return k.ToString();
                }
            }

            return "";
        }


        public static string Day16_Pt1_GetResult(string[] data)
        {
            var pattern = new[] { 0, 1, 0, -1 };
            IEnumerable<int> GetPattern(int repeat)
            {
                var lst = new List<int>();
                foreach (var item in pattern)
                {
                    for (int i = 0; i < repeat; i++)
                    {
                        lst.Add(item);
                    }
                }

                return Enumerable.Repeat(lst, 200).SelectMany(x => x).Skip(1);
            }
            var input = data.Single().Select(ToInt).ToList();

            var stepSum = "";
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                stepSum = "";
                for (int j = 1; j <= input.Count; j++)
                {
                    var localPattern = GetPattern(j).ToArray();
                    var localSum = 0;
                    for (int k = 0; k < input.Count; k++)
                    {
                        var number = input[k];
                        var patternMultiplier = localPattern[k];
                        localSum += number * patternMultiplier;
                    }
                    localSum = Math.Abs(localSum) % 10;
                    stepSum += localSum;
                }
                input = stepSum.Select(ToInt).ToList();
            }

            return new string(stepSum.Take(8).ToArray());
        }

        public static string Day16_Pt2_GetResult(string[] data)
        {
            var sb = new List<int>();
            {
                int listRepeat = 10_000;
                var inputData = data.Single();
                var totalLength = inputData.Length * listRepeat;
                var location = ToInt(new string(inputData.Take(7).ToArray()));
                var previous = 0;
                for (int j = totalLength - 1; j >= location; j--)
                {
                    if (j % 100_000 == 0) { Console.WriteLine(j); }
                    var number = ToInt(inputData[j % inputData.Length]);
                    var current = (number + previous) % 10;
                    sb.Add(current);
                    previous = current;
                }

                //Console.WriteLine(sb.ToString());
            }

            {

                for (int i = 0; i < 99; i++)
                {
                    Console.WriteLine(i);
                    sb.Reverse();

                    var str = sb.ToList();
                    sb = new List<int>();
                    var previous = 0;
                    for (int j = str.Count - 1; j >= 0; j--)
                    {
                        if (j % 100_000 == 0) { Console.WriteLine(j); }
                        var number = str.ElementAt(j);
                        var current = (number + previous) % 10;
                        sb.Add(current);
                        previous = current;
                    }
                }
            }
            sb.Reverse();

            return new string(String.Join("", sb.Take(8)).ToArray());
        }

        public static string Day17_Pt1_GetResult(string[] data)
        {
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }


            var cells = new Dictionary<(long x, long y), Cell>();
            Cell GetOrCreate(long x, long y)
            {
                var key = (x, y);
                if (!cells.ContainsKey(key))
                {
                    cells[key] = new Cell() { X = x, Y = y };
                }
                return cells[key];
            }

            void PrintGrid(IEnumerable<Cell> cells)
            {
                var minX = cells.Min(x => x.X);
                var maxX = cells.Max(x => x.X);
                var minY = cells.Min(x => x.Y);
                var maxY = cells.Max(x => x.Y);

                Console.Clear();
                for (long y = minY; y <= maxY; y++)
                {
                    for (long x = minX; x <= maxX; x++)
                    {
                        var cell = cells.SingleOrDefault(z => z.X == x && z.Y == y);
                        if (cell != null)
                        {
                            Console.Write(cell.ToString());
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }

            }

            var machine = new StateMachine { inputBuffer = 1, operations = operations };
            var stopOutput = long.MinValue;
            var str = "";
            while (true)
            {
                var output = machine.Execute();
                if (output == stopOutput) { break; }
                str += (char)output;

            }

            var split = str.Split((char)10).Select(x => x.ToCharArray()).TakeWhile(x => x.Any()).ToArray();
            var found = new List<(int x, int y)>();
            for (int x = 1; x < split.Length - 1; x++)
            {
                for (int y = 1; y < split[x].Length - 1; y++)
                {
                    var up = split[x + 1][y];
                    var down = split[x - 1][y];
                    var left = split[x][y + 1];
                    var right = split[x][y - 1];
                    var me = split[x][y];

                    if (up == '#' && down == '#' && left == '#' && right == '#' && me == '#')
                    {
                        found.Add((x, y));
                    }
                }
            }

            Console.Write(str);

            var sum = found.Sum(o => o.x*o.y);

            return sum.ToString();
        }



        enum OpCode { Unknown = 0, Add = 1, Multiply = 2, Input = 3, Output = 4, JumpIfTrue = 5, JumpIfFalse = 6, LessThan = 7, Equals = 8, AdjustRelativeBase = 9, Stop = 99 }
        enum ParameterMode { Position = 0, Value = 1, Relative = 2 }
        class StateMachine
        {
            private ParameterMode GetParameterMode(char v) => (ParameterMode)ToInt(v);
            (ParameterMode parameterMode1, ParameterMode parameterMode2, ParameterMode parameterMode3, OpCode opCode) GetOpcode(long input)
            {
                var inputString = input.ToString();
                var opCode = (OpCode)ToInt(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = ParameterMode.Position;
                var parameterMode2 = ParameterMode.Position;
                var parameterMode3 = ParameterMode.Position;
                if (inputString.Length == 3)
                {
                    parameterMode1 = GetParameterMode(inputString[0]);
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = GetParameterMode(inputString[0]);
                    parameterMode1 = GetParameterMode(inputString[1]);
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = GetParameterMode(inputString[0]);
                    parameterMode2 = GetParameterMode(inputString[1]);
                    parameterMode1 = GetParameterMode(inputString[2]);
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            public Queue<long> inputBuffer { get; set; } =  new Queue<long>();
            public List<long> outputBuffer { get; set; } = new List<long>();
            public long i { get; set; }
            public long relativeBase { get; set; }
            public Dictionary<long, long> operations { get; set; }

            long GetOrDefault(long address)
            {
                if (operations.TryGetValue(address, out var result))
                {
                    return result;
                }
                return 0;
            }
            long Get(ParameterMode mode, long address)
            {
                switch (mode)
                {
                    case ParameterMode.Position: return GetOrDefault(GetOrDefault(address));
                    case ParameterMode.Value: return GetOrDefault(address);
                    case ParameterMode.Relative: return GetOrDefault(relativeBase + GetOrDefault(address));
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            void Set(ParameterMode mode, long address, long value)
            {
                switch (mode)
                {
                    case ParameterMode.Position: operations[GetOrDefault(address)] = value; break;
                    case ParameterMode.Value: operations[address] = value; break;
                    case ParameterMode.Relative: operations[relativeBase + GetOrDefault(address)] = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            private bool first = true;
            public long Execute()
            {
                while (true)
                {
                    var opCode = GetOpcode(operations[i]);
                    switch (opCode.opCode)
                    {
                        case OpCode.Stop:
                            {
                                //return operations[0];
                                //throw new InvalidOperationException();
                                return long.MinValue;
                            }
                        case OpCode.Add:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 + param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Multiply:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 * param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Input:
                            {
                                //Set(opCode.parameterMode1, i + 1, first ? 2 : inputBuffer);
                                Set(opCode.parameterMode1, i + 1, inputBuffer.Dequeue());
                                first = false;
                                i += 2;
                            }
                            break;
                        case OpCode.Output:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                outputBuffer.Add(param1);
                                i += 2;
                                return param1;
                            }
                            break;
                        case OpCode.JumpIfTrue:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.JumpIfFalse:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.LessThan:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 < param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Equals:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 == param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.AdjustRelativeBase:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                relativeBase += param1;
                                i += 2;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }
        }
        public static string Day17_Pt2_GetResult(string[] data)
        {
            //var ten = '[';
            //var twelve = ']';
            //var main = "C,A,C,B,C,A,B,A,B,A";
            //var a = "R,8,L,],R,4,R,4";
            //var b = "R,8,L,[,R,8";
            //var c = "R,8,L,[,L,],R,4";

            var newLine = (char)10;
            var main = "C,A,C,B,C,A,B,A,B,A";
            var a = "R,8,L,12,R,4,R,4";
            var b = "R,8,L,10,R,8";
            var c = "R,8,L,10,L,12,R,4";
            var video = "n";
            var inputs = new[] { main, a, b, c, video };

            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            array[0] = 2;
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }

            var machine = new StateMachine { operations = operations };
            var stopOutput = long.MinValue;
            var str = "";

            foreach (var input in inputs)
            {
                foreach (var item in input)
                {
                    machine.inputBuffer.Enqueue(item);
                }
                machine.inputBuffer.Enqueue(newLine);
            }

            while (true)
            {
                var output = machine.Execute();
                if (output == stopOutput) { break; }
                str += (char)output;
            }

            var split = str.Split((char)10).Select(x => x.ToCharArray()).TakeWhile(x => x.Any()).ToArray();
            var found = new List<(int x, int y)>();
            for (int x = 1; x < split.Length - 1; x++)
            {
                for (int y = 1; y < split[x].Length - 1; y++)
                {
                    var up = split[x + 1][y];
                    var down = split[x - 1][y];
                    var left = split[x][y + 1];
                    var right = split[x][y - 1];
                    var me = split[x][y];

                    if (up == '#' && down == '#' && left == '#' && right == '#' && me == '#')
                    {
                        found.Add((x, y));
                    }
                }
            }

            Console.Write(str);

            return machine.outputBuffer.Last().ToString();
        }
        public static long Day18_Pt1_GetResult(string[] data)
        {
            void PrintGrid(Dictionary<(int x, int y), Cell> cells)
            {
                for (int y = 0; y < data.Length; y++)
                {
                    for (int x = 0; x < data.First().Length; x++)
                    {
                        if (cells.TryGetValue((x, y), out var result))
                        {
                            Console.Write(result.Value);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            var dct = new Dictionary<(int x, int y), Cell>();
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data.First().Length; x++)
                {
                    var value = data[y][x];
                    if (value != '#')
                    {
                        var cell = new Cell { X = x, Y = y, Value = value };
                        dct.Add((x, y), cell);
                    }
                };
            }

            PrintGrid(dct);
            foreach (var cell in dct.Values)
            {
                dct.TryGetValue((cell.X, cell.Y - 1), out var up);
                dct.TryGetValue((cell.X, cell.Y + 1), out var down);
                dct.TryGetValue((cell.X - 1, cell.Y), out var right);
                dct.TryGetValue((cell.X + 1, cell.Y), out var left);
                cell.Up = up;
                cell.Down = down;
                cell.Left = left;
                cell.Right = right;
            }

            foreach (var item in dct.Values.Where(x => x.Value != '.'))
            {
                SetDistancesToInterestingPoints(item, dct.Values);
            }

            string GetHash(Cell location, string keys) => $"{location.X},{location.Y}:{keys}";
            string GetAlphabeticHash(Cell location, string keys) => $"{location.X},{location.Y}:{new string(keys.OrderBy(x => x).ToArray())}";

            var alphabeticKeysVisited = new Dictionary<string, int>();
            var visited = new HashSet<string>();
            var keysCount = dct.Values.Select(x => x.Value).Where(x => char.IsLower(x)).Count();
            var origin = dct.Single(x => x.Value.Value == '@').Value;

            var tentativeDistance = new Dictionary<(Cell cell, string keys), int>();
            var nonInfiniteTentativeDistance = new Dictionary<(Cell cell, string keys), int>();
            var startState = (cell: origin, keys: "");
            tentativeDistance[startState] = 0;
            var current = startState;
            while (true)
            {
                //Console.WriteLine($"{dct.Values.Count(x => x.Visited)} / {dct.Values.Count}");
                var currentTentativeDistance = tentativeDistance[current];
                foreach (var neighbour in current.cell.Neighbourgs)
                {
                    var keys = current.keys;

                    var neighbourHash = GetHash(neighbour.otherCell, keys);
                    if (visited.Contains(neighbourHash)) { continue; }

                    if (char.IsLower(neighbour.otherCell.Value))
                    {
                        if (keys.Contains(neighbour.otherCell.Value))
                        {
                            continue; // dumb to go to key again
                        }
                        else
                        {
                            keys += neighbour.otherCell.Value;

                        }
                    }
                    else if (char.IsUpper(neighbour.otherCell.Value))
                    {
                        if (!keys.Contains(neighbour.otherCell.Value.ToString().ToLower()))
                        {
                            continue;
                        }
                    }
                    var distance = currentTentativeDistance + neighbour.distance;
                    var newDctKey = (neighbour.otherCell, keys);
                    var alphabeticHash = GetAlphabeticHash(neighbour.otherCell, keys);
                    if (alphabeticKeysVisited.TryGetValue(alphabeticHash, out var result))
                    {
                        if (result <= distance) { continue; }
                    }
                    alphabeticKeysVisited[alphabeticHash] = distance;

                    if (tentativeDistance.ContainsKey(newDctKey))
                    {
                        tentativeDistance[newDctKey] = Math.Min(tentativeDistance[newDctKey], distance);
                    }
                    else
                    {
                        tentativeDistance[newDctKey] = distance;
                    }
                    nonInfiniteTentativeDistance[newDctKey] = tentativeDistance[newDctKey];
                }
                var currentHash = GetHash(current.cell, current.keys);

                visited.Add(currentHash);
                tentativeDistance.Remove((current.cell, current.keys));
                nonInfiniteTentativeDistance.Remove((current.cell, current.keys));
                var newCurrent = nonInfiniteTentativeDistance.OrderBy(x => x.Value).ThenByDescending(x => x.Key.keys.Count()).FirstOrDefault();
                current = newCurrent.Key;
                if (newCurrent.Key.cell == null) { break; }
                if (newCurrent.Value == int.MaxValue) { break; }
                if (newCurrent.Key.keys.Count() == keysCount) { break; }

            }

            return tentativeDistance[current];
        }


        static void SetDistancesToInterestingPoints(Cell source, IEnumerable<Cell> others)
        {
            var visited = new HashSet<string>();

            var tentativeDistance = new Dictionary<Cell, int>();
            tentativeDistance[source] = 0;
            var current = source;
            while (true)
            {
                var currentTentativeDistance = tentativeDistance[current];
                if (!char.IsUpper(current.Value) || current.Value == source.Value)
                {
                    foreach (var neighbour in current.GetAllDirections())
                    {
                        var distance = currentTentativeDistance + 1;
                        if (tentativeDistance.ContainsKey(neighbour))
                        {

                            tentativeDistance[neighbour] = Math.Min(tentativeDistance[neighbour], distance);
                        }
                        else
                        {
                            tentativeDistance[neighbour] = distance;
                        }
                    }
                }
                visited.Add(current.ToString());
                var newCurrent = tentativeDistance.Where(x => !visited.Contains(x.Key.ToString())).OrderBy(x => x.Value).FirstOrDefault();
                current = newCurrent.Key;
                if (current == null) { break; }
                if (newCurrent.Value == int.MaxValue) { break; }
            }

            source.Neighbourgs = tentativeDistance.Where(x => x.Key != source && x.Key.Value != '.' && x.Key.Value != '@').Select(x => (x.Key, x.Value)).ToList();
        }



        public static long Day18_Pt2_GetResult(string[] data)
        {
            void PrintGrid(Dictionary<(int x, int y), Cell> cells)
            {
                for (int y = 0; y < data.Length; y++)
                {
                    for (int x = 0; x < data.First().Length; x++)
                    {
                        if (cells.TryGetValue((x, y), out var result))
                        {
                            Console.Write(result.Value);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            var dct = new Dictionary<(int x, int y), Cell>();
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data.First().Length; x++)
                {
                    var value = data[y][x];
                    if (value != '#')
                    {
                        var cell = new Cell { X = x, Y = y, Value = value };
                        dct.Add((x, y), cell);
                    }
                };
            }

            PrintGrid(dct);
            foreach (var cell in dct.Values)
            {
                dct.TryGetValue((cell.X, cell.Y - 1), out var up);
                dct.TryGetValue((cell.X, cell.Y + 1), out var down);
                dct.TryGetValue((cell.X - 1, cell.Y), out var right);
                dct.TryGetValue((cell.X + 1, cell.Y), out var left);
                cell.Up = up;
                cell.Down = down;
                cell.Left = left;
                cell.Right = right;
            }

            foreach (var item in dct.Values.Where(x => x.Value != '.'))
            {
                SetDistancesToInterestingPoints(item, dct.Values);
            }

            string GetHash((Cell r1, Cell r2, Cell r3, Cell r4, string keys) x) => $"{x.r1.X},{x.r1.Y}:{x.r2.X},{x.r2.Y}:{x.r3.X},{x.r3.Y}:{x.r4.X},{x.r4.Y}:{x.keys}";
            string GetAlphabeticHash((Cell r1, Cell r2, Cell r3, Cell r4, string keys) x) => $"{x.r1.X},{x.r1.Y}:{x.r2.X},{x.r2.Y}:{x.r3.X},{x.r3.Y}:{x.r4.X},{x.r4.Y}:{new string(x.keys.OrderBy(x => x).ToArray())}";
            string GetAlphabeticHash2((Cell r1, string keys) x) => $"{x.r1.X},{x.r1.Y}:{new string(x.keys.OrderBy(x => x).ToArray())}";

            var alphabeticKeysVisited = new Dictionary<string, int>();
            var visited = new HashSet<string>();
            var keysCount = dct.Values.Select(x => x.Value).Where(x => char.IsLower(x)).Count();
            var robot1 = dct.Single(x => x.Value.Value == '1').Value;
            var robot2 = dct.Single(x => x.Value.Value == '2').Value;
            var robot3 = dct.Single(x => x.Value.Value == '3').Value;
            var robot4 = dct.Single(x => x.Value.Value == '4').Value;
            var robots = new[] { robot1, robot2, robot3, robot4 };

            var tentativeDistance = new Dictionary<(Cell r1, Cell r2, Cell r3, Cell r4, string keys), int>();
            var startState = (currentR1: robot1, currentR2: robot2, currentR3: robot3, currentR4: robot4, keys: "");
            tentativeDistance[startState] = 0;
            var current = startState;
            int i = 0;
            while (true)
            {
                i++;
                if (i % 1_000 == 0)
                {
                    Console.WriteLine($"{i} Keys {current.keys.Length}/{keysCount}");
                }
                var currentTentativeDistance = tentativeDistance[current];
                foreach (var robot in new[] { current.currentR1, current.currentR2, current.currentR3, current.currentR4 })
                {
                    foreach (var neighbour in robot.Neighbourgs)
                    {
                        var keys = current.keys;

                        var tmpR1 = robot != current.currentR1 ? current.currentR1 : neighbour.otherCell;
                        var tmpR2 = robot != current.currentR2 ? current.currentR2 : neighbour.otherCell;
                        var tmpR3 = robot != current.currentR3 ? current.currentR3 : neighbour.otherCell;
                        var tmpR4 = robot != current.currentR4 ? current.currentR4 : neighbour.otherCell;

                        var neighbourHash = GetHash((tmpR1, tmpR2, tmpR3, tmpR4, keys));
                        if (visited.Contains(neighbourHash)) { continue; }

                        if (char.IsLower(neighbour.otherCell.Value))
                        {
                            if (keys.Contains(neighbour.otherCell.Value))
                            {
                                continue; // dumb to go to key again
                            }
                            else
                            {
                                keys += neighbour.otherCell.Value;

                            }
                        }
                        else if (char.IsUpper(neighbour.otherCell.Value))
                        {
                            if (!keys.Contains(neighbour.otherCell.Value.ToString().ToLower()))
                            {
                                continue;
                            }
                        }
                        else if (char.IsDigit(neighbour.otherCell.Value))
                        {
                            continue; // dumb to go to starting position
                        }
                        var distance = currentTentativeDistance + neighbour.distance;
                        if(distance > 2996) { continue; }
                        var newDctKey = (tmpR1, tmpR2, tmpR3, tmpR4, keys);

                        var alphabeticHash = GetAlphabeticHash((tmpR1, tmpR2, tmpR3, tmpR4, keys));
                        if (alphabeticKeysVisited.TryGetValue(alphabeticHash, out var result))
                        {
                            if (result <= distance) { continue; }
                        }
                        alphabeticKeysVisited[alphabeticHash] = distance;

                        if (tentativeDistance.ContainsKey(newDctKey))
                        {
                            tentativeDistance[newDctKey] = Math.Min(tentativeDistance[newDctKey], distance);
                        }
                        else
                        {
                            tentativeDistance[newDctKey] = distance;
                        }
                    }
                }

                var currentHash = GetHash(current);

                visited.Add(currentHash);
                tentativeDistance.Remove(current);
                var newCurrent = tentativeDistance.OrderBy(x => x.Value).ThenByDescending(x => x.Key.keys.Count()).FirstOrDefault();
                //var newCurrent = tentativeDistance.OrderByDescending(x => x.Key.keys.Count()).ThenBy(x => x.Value).FirstOrDefault();
                current = newCurrent.Key;
                if (newCurrent.Key.r1 == null || newCurrent.Key.r2 == null || newCurrent.Key.r3 == null || newCurrent.Key.r4 == null) { break; }
                if (newCurrent.Value == int.MaxValue) { break; }
                if (newCurrent.Key.keys.Count() == keysCount) { break; }

            }

            return tentativeDistance[current];
        }

        class Cell
        {
            public char Value { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public bool Visited { get; set; }

            public override string ToString()
            {
                return $"{X},{Y} ({Value})";
            }
            public Cell Up { get; set; }
            public Cell Down { get; set; }
            public Cell Left { get; set; }
            public Cell Right { get; set; }
            public IEnumerable<Cell> GetAllDirections()
            {
                if (Down != null) { yield return Down; }
                if (Right != null) { yield return Right; }
                if (Left != null) { yield return Left; }
                if (Up != null) { yield return Up; }
            }

            public List<(Cell otherCell, int distance)> Neighbourgs { get; set; }
        }

              public static string Day19_Pt1_GetResult(string[] data)
        {
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }

            var results = new List<long>();
            for (int j = 0; j <= 49; j++)
            {
                for (int k = 0; k <= 49; k++)
                {
                    var machine = new StateMachine { operations = operations.ToDictionary(x => x.Key, x => x.Value) };
                    machine.inputBuffer.Enqueue(j);
                    machine.inputBuffer.Enqueue(k);
                    var result = machine.Execute();
                    results.Add(result);
                    //Console.Clear();
                    Console.WriteLine($"{k}/{j}");
                }
            }
            return results.Count(x => x == 1).ToString(); // 1 fout
        }



        enum OpCode { Unknown = 0, Add = 1, Multiply = 2, Input = 3, Output = 4, JumpIfTrue = 5, JumpIfFalse = 6, LessThan = 7, Equals = 8, AdjustRelativeBase = 9, Stop = 99 }
        enum ParameterMode { Position = 0, Value = 1, Relative = 2 }
        class StateMachine
        {
            private ParameterMode GetParameterMode(char v) => (ParameterMode)ToInt(v);
            (ParameterMode parameterMode1, ParameterMode parameterMode2, ParameterMode parameterMode3, OpCode opCode) GetOpcode(long input)
            {
                var inputString = input.ToString();
                var opCode = (OpCode)ToInt(new string(inputString.TakeLast(2).ToArray()));
                var parameterMode1 = ParameterMode.Position;
                var parameterMode2 = ParameterMode.Position;
                var parameterMode3 = ParameterMode.Position;
                if (inputString.Length == 3)
                {
                    parameterMode1 = GetParameterMode(inputString[0]);
                }
                else if (inputString.Length == 4)
                {
                    parameterMode2 = GetParameterMode(inputString[0]);
                    parameterMode1 = GetParameterMode(inputString[1]);
                }
                else if (inputString.Length == 5)
                {
                    parameterMode3 = GetParameterMode(inputString[0]);
                    parameterMode2 = GetParameterMode(inputString[1]);
                    parameterMode1 = GetParameterMode(inputString[2]);
                }
                return (parameterMode1, parameterMode2, parameterMode3, opCode);
            }

            public Queue<long> inputBuffer { get; set; } = new Queue<long>();
            public List<long> outputBuffer { get; set; } = new List<long>();
            public long i { get; set; }
            public long relativeBase { get; set; }
            public Dictionary<long, long> operations { get; set; }

            long GetOrDefault(long address)
            {
                if (operations.TryGetValue(address, out var result))
                {
                    return result;
                }
                return 0;
            }
            long Get(ParameterMode mode, long address)
            {
                switch (mode)
                {
                    case ParameterMode.Position: return GetOrDefault(GetOrDefault(address));
                    case ParameterMode.Value: return GetOrDefault(address);
                    case ParameterMode.Relative: return GetOrDefault(relativeBase + GetOrDefault(address));
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            void Set(ParameterMode mode, long address, long value)
            {
                switch (mode)
                {
                    case ParameterMode.Position: operations[GetOrDefault(address)] = value; break;
                    case ParameterMode.Value: operations[address] = value; break;
                    case ParameterMode.Relative: operations[relativeBase + GetOrDefault(address)] = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            private bool first = true;
            public long Execute()
            {
                while (true)
                {
                    var opCode = GetOpcode(operations[i]);

                    //Console.WriteLine($"{i}: " + opCode);
                    switch (opCode.opCode)
                    {
                        case OpCode.Stop:
                            {
                                //return operations[0];
                                //throw new InvalidOperationException();
                                return long.MinValue;
                            }
                        case OpCode.Add:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 + param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Multiply:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 * param2;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Input:
                            {
                                //Set(opCode.parameterMode1, i + 1, first ? 2 : inputBuffer);
                                Set(opCode.parameterMode1, i + 1, inputBuffer.Dequeue());
                                first = false;
                                i += 2;
                            }
                            break;
                        case OpCode.Output:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                outputBuffer.Add(param1);
                                i += 2;
                                return param1;
                            }
                        //Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        case OpCode.JumpIfTrue:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 != 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        //Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        case OpCode.JumpIfFalse:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                if (param1 == 0)
                                {
                                    i = param2;
                                }
                                else
                                {
                                    i += 3;
                                }
                            }
                            break;
                        case OpCode.LessThan:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 < param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.Equals:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                var param2 = Get(opCode.parameterMode2, i + 2);
                                var value = param1 == param2 ? 1 : 0;
                                Set(opCode.parameterMode3, i + 3, value);
                                i += 4;
                            }
                            break;
                        case OpCode.AdjustRelativeBase:
                            {
                                var param1 = Get(opCode.parameterMode1, i + 1);
                                relativeBase += param1;
                                i += 2;
                            }
                            break;
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
            }
        }

        public static string Day19_Pt2_GetResult(string[] data)
        {
            return "";
            var operations = new Dictionary<long, long>();
            var array = data.Single().Split(",").Select(long.Parse).ToArray();
            for (long i = 0; i < array.Length; i++)
            {
                operations[i] = array[i];
            }

            var results = new List<(int x, int y, char value)>();
            var sb = new StringBuilder(); ;
            var size = 3000;
            for (int j = 0; j <= size; j++)
            {
                var wasLit = false;
                for (int k = 0; k <= size; k++)
                {
                    var machine = new StateMachine { operations = operations.ToDictionary(x => x.Key, x => x.Value) };
                    machine.inputBuffer.Enqueue(j);
                    machine.inputBuffer.Enqueue(k);
                    var result = machine.Execute();
                    //results.Add((j, k, result == 1 ? '#' : ' '));
                    var lit = result == 1;
                    sb.Append(lit ? '#' : ' ');
                    if (lit) { wasLit = true; }
                    if (wasLit && !lit) { break; }
                    //Console.Clear();
                    //Console.WriteLine($"{k}/{j}");
                }
                sb.AppendLine();
                Console.WriteLine($"{j}/{size}");
            }
            using (StreamWriter w = File.AppendText(@"C:\Users\pc2020\Desktop\tst3.txt"))
            {
                w.Write(sb.ToString());
            }

            return "";
        }



        public static long Day19_Pt2Bis_GetResult(string[] data)
        {
            var lines = File.ReadAllLines(@"C:\Users\pc2020\Desktop\tst3.txt");

            var list = new List<(int y, int x1, int x2)>();
            int i = 0;
            foreach (var line in lines)
            {
                list.Add((i, line.IndexOf("#"), line.LastIndexOf("#")));
                i++;
            }

            var size = 100;
            //list = list.Where(x => x.x2 - x.x1 >= size).ToList();
            foreach (var top in list)
            {
                var bottom = list.Single(x => x.y == top.y + size - 1);
                if (top.x1 <= bottom.x1 && bottom.x1 <= top.x2)
                {
                    if (bottom.x1 <= top.x2 && top.x2 <= bottom.x2)
                    {
                        if (top.x2 - bottom.x1 == size - 1)
                        {
                            return bottom.x1 * 10000 + top.y;
                        }
                    }
                }
            }
            return -1; // 9051045 too low.... 9121054 too low
            // y/x omgewisseld in wegschrijven -> 10450905
        }

                public static long Day20_Pt1_GetResult(string[] data)
        {
            void PrintGrid(Dictionary<(int x, int y), Cell> cells)
            {
                for (int y = 0; y < data.Length; y++)
                {
                    for (int x = 0; x < data.First().Length; x++)
                    {
                        if (cells.TryGetValue((x, y), out var result))
                        {
                            Console.Write(result.Value);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            var dct = new Dictionary<(int x, int y), Cell>();
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data.First().Length; x++)
                {
                    var value = data[y][x];
                    if (value != '#')
                    {
                        var cell = new Cell { X = x, Y = y, Value = value };
                        dct.Add((x, y), cell);
                    }
                };
            }

            PrintGrid(dct);
            foreach (var cell in dct.Values)
            {
                dct.TryGetValue((cell.X, cell.Y - 1), out var up);
                dct.TryGetValue((cell.X, cell.Y + 1), out var down);
                dct.TryGetValue((cell.X + 1, cell.Y), out var right);
                dct.TryGetValue((cell.X - 1, cell.Y), out var left);
                cell.Up = up;
                cell.Down = down;
                cell.Left = left;
                cell.Right = right;
            }
            var portals = new Dictionary<string, List<Cell>>();
            foreach (var cell in dct.Values.Where(x => x.Value == '.'))
            {
                string portalKey = null;
                if (cell.Up != null && char.IsLetter(cell.Up.Value))
                {
                    portalKey = $"{cell.Up.Up.Value}{cell.Up.Value}";
                }
                else if (cell.Left != null && char.IsLetter(cell.Left.Value))
                {
                    portalKey = $"{cell.Left.Left.Value}{cell.Left.Value}";
                }
                else if (cell.Right != null && char.IsLetter(cell.Right.Value))
                {
                    portalKey = $"{cell.Right.Value}{cell.Right.Right.Value}";
                }
                else if (cell.Down != null && char.IsLetter(cell.Down.Value))
                {
                    portalKey = $"{cell.Down.Value}{cell.Down.Down.Value}";
                }

                if (portalKey != null)
                {
                    if (!portals.ContainsKey(portalKey))
                    {
                        portals[portalKey] = new List<Cell>();
                    }
                    portals[portalKey].Add(cell);
                }
            }

            var origin = portals["AA"].Single();
            var destination = portals["ZZ"].Single();
            origin.SetPortal(null);
            destination.SetPortal(null);
            foreach (var item in portals.Where(x => x.Key != "AA" && x.Key != "ZZ"))
            {
                var one = item.Value[0];
                var two = item.Value[1];
                one.SetPortal(two);
                two.SetPortal(one);
            }


            var unvisited = dct.Values.ToList();
            var tentativeDistance = unvisited.ToDictionary(x => x, x => int.MaxValue);
            tentativeDistance[origin] = 0;
            var current = origin;
            while (true)
            {
                //Console.WriteLine($"{dct.Values.Count(x => x.Visited)} / {dct.Values.Count}");
                var currentTentativeDistance = tentativeDistance[current];
                foreach (var neighbour in current.GetAllDirections().Where(x => !x.Visited))
                {
                    var distance = currentTentativeDistance + 1;
                    tentativeDistance[neighbour] = Math.Min(tentativeDistance[neighbour], distance);
                }
                current.Visited = true;
                var newCurrent = tentativeDistance.Where(x => !x.Key.Visited).OrderBy(x => x.Value).FirstOrDefault();
                if (newCurrent.Key == null) { break; }
                if (newCurrent.Value == int.MaxValue) { break; }
                if (newCurrent.Key == destination) { break; }
                current = newCurrent.Key;

            }

            return tentativeDistance[destination];
        }

        */


        class Cell
        {
            public char Value { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return $"{X},{Y} ({Value})";
            }
            public (Cell cell, int depthModifier) Up { get; set; }
            public (Cell cell, int depthModifier) Down { get; set; }
            public (Cell cell, int depthModifier) Left { get; set; }
            public (Cell cell, int depthModifier) Right { get; set; }
            public IEnumerable<(Cell cell, int depthModifier)> GetAllDirections()
            {
                if (Down.cell != null) { yield return Down; }
                if (Right.cell != null) { yield return Right; }
                if (Left.cell != null) { yield return Left; }
                if (Up.cell != null) { yield return Up; }
            }

            public List<(Cell otherCell, int distance)> Neighbourgs { get; set; }

            internal void SetPortal(Cell other)
            {
                if (Down.cell != null && char.IsLetter(Down.cell.Value)) { Down = (other, Down.depthModifier); }
                else if (Right.cell != null && char.IsLetter(Right.cell.Value)) { Right = (other, Right.depthModifier); }
                else if (Left.cell != null && char.IsLetter(Left.cell.Value)) { Left = (other, Left.depthModifier); }
                else if (Up.cell != null && char.IsLetter(Up.cell.Value)) { Up = (other, Up.depthModifier); }
                else { throw new NotImplementedException(); }
            }
        }

        public static long Day20_Pt2_GetResult(string[] data)
        {
            void PrintGrid(Dictionary<(int x, int y), Cell> cells)
            {
                for (int y = 0; y < data.Length; y++)
                {
                    for (int x = 0; x < data.First().Length; x++)
                    {
                        if (cells.TryGetValue((x, y), out var result))
                        {
                            Console.Write(result.Value);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            var dct = new Dictionary<(int x, int y), Cell>();
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data.First().Length; x++)
                {
                    var value = data[y][x];
                    if (value != '#')
                    {
                        var cell = new Cell { X = x, Y = y, Value = value };
                        dct.Add((x, y), cell);
                    }
                };
            }

            PrintGrid(dct);
            foreach (var cell in dct.Values)
            {
                dct.TryGetValue((cell.X, cell.Y - 1), out var up);
                dct.TryGetValue((cell.X, cell.Y + 1), out var down);
                dct.TryGetValue((cell.X + 1, cell.Y), out var right);
                dct.TryGetValue((cell.X - 1, cell.Y), out var left);
                cell.Up = (up, 0);
                cell.Down = (down, 0);
                cell.Left = (left, 0);
                cell.Right = (right, 0);
            }
            var maxX = dct.Max(z => z.Key.x);
            var maxY = dct.Max(z => z.Key.y);
            var portals = new Dictionary<string, List<Cell>>();
            foreach (var cell in dct.Values.Where(x => x.Value == '.'))
            {
                string portalKey = null;
                var outside = false;
                if (cell.Up.cell != null && char.IsLetter(cell.Up.cell.Value))
                {
                    portalKey = $"{cell.Up.cell.Up.cell.Value}{cell.Up.cell.Value}";
                    outside = cell.Up.cell.Up.cell.Y == 0;
                    cell.Up = (cell.Up.cell, outside ? -1 : 1);
                }
                else if (cell.Left.cell != null && char.IsLetter(cell.Left.cell.Value))
                {
                    portalKey = $"{cell.Left.cell.Left.cell.Value}{cell.Left.cell.Value}";
                    outside = cell.Left.cell.Left.cell.X == 0;
                    cell.Left = (cell.Left.cell, outside ? -1 : 1);
                }
                else if (cell.Right.cell != null && char.IsLetter(cell.Right.cell.Value))
                {
                    portalKey = $"{cell.Right.cell.Value}{cell.Right.cell.Right.cell.Value}";
                    outside = cell.Right.cell.Right.cell.X == maxX;
                    cell.Right = (cell.Right.cell, outside ? -1 : 1);
                }
                else if (cell.Down.cell != null && char.IsLetter(cell.Down.cell.Value))
                {
                    portalKey = $"{cell.Down.cell.Value}{cell.Down.cell.Down.cell.Value}";
                    outside = cell.Down.cell.Down.cell.Y == maxY;
                    cell.Down = (cell.Down.cell, outside ? -1 : 1);
                }

                if (portalKey != null)
                {
                    if (!portals.ContainsKey(portalKey))
                    {
                        portals[portalKey] = new List<Cell>();
                    }
                    portals[portalKey].Add(cell);
                }
            }

            var origin = portals["AA"].Single();
            var destination = portals["ZZ"].Single();
            origin.SetPortal(null);
            destination.SetPortal(null);
            foreach (var item in portals.Where(x => x.Key != "AA" && x.Key != "ZZ"))
            {
                var one = item.Value[0];
                var two = item.Value[1];
                one.SetPortal(two);
                two.SetPortal(one);
            }

            var visited = new HashSet<string>();
            string Hash((Cell cell, int depth) z) => $"{z.depth}:{z.cell.X}:{z.cell.Y}";
            //var tentativeDistance = dct.Values.ToList().ToDictionary(x => (cell: x, depth: 0), x => int.MaxValue);
            var tentativeDistance = new Dictionary<(Cell cell, int depth), int>();
            var current = (cell: origin, depth: 0);
            tentativeDistance[(origin, 0)] = 0;
            var target = (cell: destination, depth: 0);
            var targetHash = Hash(target);
            while (true)
            {
                var visitCount = visited.Count;
                if (visitCount % 1_000 == 0)
                {
                    Console.WriteLine($"{tentativeDistance.Last().Key} {tentativeDistance.Last().Value}");
                }
                //Console.WriteLine($"{dct.Values.Count(x => x.Visited)} / {dct.Values.Count}");
                var currentTentativeDistance = tentativeDistance[current];
                foreach (var neighbour in current.cell.GetAllDirections())
                {
                    var newDepth = current.depth + neighbour.depthModifier;
                    //if (newDepth < 0 || newDepth > 100) { continue; }
                    if (newDepth < 0 || newDepth > 30) { continue; }
                    var localNeighbour = (cell: neighbour.cell, depth: newDepth);
                    var neighbourHash = Hash(localNeighbour);
                    if (visited.Contains(neighbourHash)) { continue; }
                    var distance = currentTentativeDistance + 1;
                    if (tentativeDistance.ContainsKey(localNeighbour))
                    {
                        tentativeDistance[localNeighbour] = Math.Min(tentativeDistance[localNeighbour], distance);
                    }
                    else
                    {
                        tentativeDistance[localNeighbour] = distance;
                    }
                }
                visited.Add(Hash(current));
                var newCurrent = tentativeDistance.Where(x => !visited.Contains(Hash(x.Key))).OrderBy(x => x.Value).FirstOrDefault();
                if (newCurrent.Key.cell == null) { break; }
                if (newCurrent.Value == int.MaxValue) { break; }
                if (Hash(newCurrent.Key) == targetHash) { break; }
                current = newCurrent.Key;

            }

            return tentativeDistance[target];
        }
    }
}