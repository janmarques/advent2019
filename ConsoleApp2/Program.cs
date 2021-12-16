using System;
using System.Collections.Generic;
using System.Diagnostics;
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
         */


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
                    if(originalBlocksCount == null)
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

            PrintGrid(cells.Values.ToList(), scores.Max());
            Console.WriteLine($"Blocks left: {cells.Values.Count(x => x.Value == 2)} / {originalBlocksCount}");
            Console.WriteLine(string.Join("", ballHits.TakeLast(5).Select(BallHitToString)));
            Console.Read();

            return scores.Max().ToString();
            //return string.Join("", ballHits.Select(BallHitToString)); // 33 * 273 = 9009
            // 9009 too low
        }
    }
}