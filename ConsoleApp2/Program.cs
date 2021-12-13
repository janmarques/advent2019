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
         */

        public static long XDay01_Pt1_GetResult(string[] data)
        {
            return 0;
        }
    }
}