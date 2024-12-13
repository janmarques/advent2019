﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

var fullInput =
@"3,62,1001,62,11,10,109,2229,105,1,0,1392,1198,1169,736,2060,1136,767,1041,928,1072,1996,1662,1567,994,800,1363,571,2091,1928,1493,2192,705,1695,1800,1596,1464,868,965,1328,1631,1965,835,1262,2029,1101,1433,1829,639,899,1726,2161,1227,672,2122,1899,1526,1763,1295,604,1868,0,0,0,0,0,0,0,0,0,0,0,0,3,64,1008,64,-1,62,1006,62,88,1006,61,170,1106,0,73,3,65,20101,0,64,1,20102,1,66,2,21102,1,105,0,1106,0,436,1201,1,-1,64,1007,64,0,62,1005,62,73,7,64,67,62,1006,62,73,1002,64,2,133,1,133,68,133,102,1,0,62,1001,133,1,140,8,0,65,63,2,63,62,62,1005,62,73,1002,64,2,161,1,161,68,161,1101,1,0,0,1001,161,1,169,102,1,65,0,1102,1,1,61,1102,1,0,63,7,63,67,62,1006,62,203,1002,63,2,194,1,68,194,194,1006,0,73,1001,63,1,63,1105,1,178,21101,0,210,0,105,1,69,2101,0,1,70,1101,0,0,63,7,63,71,62,1006,62,250,1002,63,2,234,1,72,234,234,4,0,101,1,234,240,4,0,4,70,1001,63,1,63,1105,1,218,1105,1,73,109,4,21102,0,1,-3,21101,0,0,-2,20207,-2,67,-1,1206,-1,293,1202,-2,2,283,101,1,283,283,1,68,283,283,22001,0,-3,-3,21201,-2,1,-2,1106,0,263,21202,-3,1,-3,109,-4,2105,1,0,109,4,21102,1,1,-3,21102,0,1,-2,20207,-2,67,-1,1206,-1,342,1202,-2,2,332,101,1,332,332,1,68,332,332,22002,0,-3,-3,21201,-2,1,-2,1106,0,312,22102,1,-3,-3,109,-4,2106,0,0,109,1,101,1,68,358,21002,0,1,1,101,3,68,367,20101,0,0,2,21101,376,0,0,1105,1,436,21201,1,0,0,109,-1,2106,0,0,1,2,4,8,16,32,64,128,256,512,1024,2048,4096,8192,16384,32768,65536,131072,262144,524288,1048576,2097152,4194304,8388608,16777216,33554432,67108864,134217728,268435456,536870912,1073741824,2147483648,4294967296,8589934592,17179869184,34359738368,68719476736,137438953472,274877906944,549755813888,1099511627776,2199023255552,4398046511104,8796093022208,17592186044416,35184372088832,70368744177664,140737488355328,281474976710656,562949953421312,1125899906842624,109,8,21202,-6,10,-5,22207,-7,-5,-5,1205,-5,521,21102,1,0,-4,21102,0,1,-3,21101,51,0,-2,21201,-2,-1,-2,1201,-2,385,470,21001,0,0,-1,21202,-3,2,-3,22207,-7,-1,-5,1205,-5,496,21201,-3,1,-3,22102,-1,-1,-5,22201,-7,-5,-7,22207,-3,-6,-5,1205,-5,515,22102,-1,-6,-5,22201,-3,-5,-3,22201,-1,-4,-4,1205,-2,461,1106,0,547,21102,-1,1,-4,21202,-6,-1,-6,21207,-7,0,-5,1205,-5,547,22201,-7,-6,-7,21201,-4,1,-4,1106,0,529,22102,1,-4,-7,109,-8,2106,0,0,109,1,101,1,68,563,21001,0,0,0,109,-1,2105,1,0,1101,100267,0,66,1101,0,2,67,1102,598,1,68,1101,0,302,69,1101,0,1,71,1102,602,1,72,1105,1,73,0,0,0,0,32,91138,1101,51977,0,66,1102,3,1,67,1101,0,631,68,1102,302,1,69,1101,0,1,71,1102,637,1,72,1105,1,73,0,0,0,0,0,0,39,141837,1102,1,20297,66,1101,2,0,67,1101,666,0,68,1102,302,1,69,1102,1,1,71,1102,1,670,72,1105,1,73,0,0,0,0,31,202718,1101,0,30181,66,1102,1,1,67,1102,1,699,68,1102,556,1,69,1101,2,0,71,1101,701,0,72,1105,1,73,1,10,46,194762,45,241828,1102,84659,1,66,1101,1,0,67,1102,1,732,68,1102,556,1,69,1101,0,1,71,1101,0,734,72,1106,0,73,1,-24,41,54983,1101,14563,0,66,1101,0,1,67,1102,763,1,68,1101,556,0,69,1101,0,1,71,1101,0,765,72,1106,0,73,1,43,18,305589,1102,1,27259,66,1101,1,0,67,1101,794,0,68,1102,1,556,69,1101,0,2,71,1101,0,796,72,1105,1,73,1,2053,24,189586,41,164949,1102,23279,1,66,1101,3,0,67,1101,827,0,68,1102,1,302,69,1101,1,0,71,1101,0,833,72,1106,0,73,0,0,0,0,0,0,8,35158,1101,0,101359,66,1102,2,1,67,1101,862,0,68,1101,302,0,69,1101,0,1,71,1102,866,1,72,1105,1,73,0,0,0,0,39,189116,1101,81869,0,66,1102,1,1,67,1102,1,895,68,1102,556,1,69,1101,0,1,71,1102,897,1,72,1106,0,73,1,4127,34,291513,1102,73681,1,66,1102,1,1,67,1101,926,0,68,1101,0,556,69,1102,0,1,71,1101,928,0,72,1105,1,73,1,1053,1101,17579,0,66,1101,4,0,67,1101,0,955,68,1102,253,1,69,1102,1,1,71,1101,0,963,72,1106,0,73,0,0,0,0,0,0,0,0,16,100267,1102,102677,1,66,1101,0,1,67,1102,1,992,68,1101,0,556,69,1101,0,0,71,1101,0,994,72,1105,1,73,1,1635,1102,21481,1,66,1102,1,1,67,1101,0,1021,68,1102,556,1,69,1101,9,0,71,1102,1,1023,72,1105,1,73,1,2,32,45569,24,94793,10,83786,19,92671,11,14891,5,8087,37,20297,45,60457,45,302285,1101,88853,0,66,1102,1,1,67,1102,1068,1,68,1102,556,1,69,1102,1,1,71,1102,1,1070,72,1106,0,73,1,59,20,143578,1101,57793,0,66,1102,1,1,67,1102,1,1099,68,1102,556,1,69,1101,0,0,71,1102,1101,1,72,1106,0,73,1,1975,1102,97171,1,66,1101,0,3,67,1101,0,1128,68,1101,0,302,69,1101,0,1,71,1101,0,1134,72,1106,0,73,0,0,0,0,0,0,8,70316,1102,8087,1,66,1101,0,2,67,1102,1163,1,68,1101,0,302,69,1101,1,0,71,1102,1,1167,72,1105,1,73,0,0,0,0,37,40594,1102,58271,1,66,1101,1,0,67,1102,1196,1,68,1101,0,556,69,1102,1,0,71,1102,1,1198,72,1105,1,73,1,1468,1102,1,87991,66,1102,1,1,67,1102,1225,1,68,1102,556,1,69,1102,0,1,71,1101,1227,0,72,1106,0,73,1,1066,1101,0,54983,66,1101,3,0,67,1102,1,1254,68,1102,302,1,69,1102,1,1,71,1102,1260,1,72,1106,0,73,0,0,0,0,0,0,48,51977,1101,0,45569,66,1102,1,2,67,1102,1289,1,68,1101,302,0,69,1102,1,1,71,1102,1,1293,72,1105,1,73,0,0,0,0,24,284379,1101,87359,0,66,1102,1,2,67,1102,1,1322,68,1101,351,0,69,1102,1,1,71,1102,1326,1,72,1106,0,73,0,0,0,0,255,17599,1102,1,20399,66,1102,1,3,67,1101,0,1355,68,1101,0,302,69,1102,1,1,71,1102,1361,1,72,1105,1,73,0,0,0,0,0,0,39,47279,1102,1,13177,66,1101,1,0,67,1101,1390,0,68,1101,556,0,69,1102,0,1,71,1101,1392,0,72,1105,1,73,1,1410,1102,17599,1,66,1101,0,1,67,1101,0,1419,68,1102,556,1,69,1102,1,6,71,1101,1421,0,72,1105,1,73,1,23694,31,101359,48,103954,48,155931,28,20399,28,40798,28,61197,1101,0,56369,66,1101,0,1,67,1102,1460,1,68,1102,1,556,69,1101,0,1,71,1102,1462,1,72,1106,0,73,1,47163854,10,41893,1101,0,36263,66,1101,0,1,67,1102,1,1491,68,1102,556,1,69,1102,0,1,71,1101,1493,0,72,1105,1,73,1,1357,1102,1,92671,66,1102,2,1,67,1101,1520,0,68,1102,1,302,69,1102,1,1,71,1102,1524,1,72,1106,0,73,0,0,0,0,11,29782,1102,60457,1,66,1101,0,6,67,1102,1,1553,68,1102,302,1,69,1102,1,1,71,1101,1565,0,72,1105,1,73,0,0,0,0,0,0,0,0,0,0,0,0,47,174718,1102,4957,1,66,1102,1,1,67,1101,0,1594,68,1102,556,1,69,1102,0,1,71,1101,0,1596,72,1105,1,73,1,1623,1101,94793,0,66,1101,0,3,67,1102,1,1623,68,1101,0,302,69,1102,1,1,71,1101,1629,0,72,1106,0,73,0,0,0,0,0,0,39,94558,1101,31387,0,66,1102,1,1,67,1102,1,1658,68,1101,0,556,69,1101,0,1,71,1101,0,1660,72,1105,1,73,1,192,14,46558,1101,0,14891,66,1101,0,2,67,1101,0,1689,68,1102,1,302,69,1102,1,1,71,1102,1,1693,72,1106,0,73,0,0,0,0,5,16174,1102,1,46457,66,1102,1,1,67,1101,1722,0,68,1102,556,1,69,1102,1,1,71,1102,1,1724,72,1105,1,73,1,2341,14,69837,1102,47279,1,66,1101,0,4,67,1101,1753,0,68,1101,253,0,69,1102,1,1,71,1102,1761,1,72,1105,1,73,0,0,0,0,0,0,0,0,47,87359,1101,0,97381,66,1101,4,0,67,1101,0,1790,68,1101,0,302,69,1102,1,1,71,1101,0,1798,72,1106,0,73,0,0,0,0,0,0,0,0,45,120914,1102,68597,1,66,1102,1,1,67,1102,1827,1,68,1101,556,0,69,1101,0,0,71,1102,1,1829,72,1105,1,73,1,1465,1101,0,48857,66,1102,1,1,67,1102,1,1856,68,1102,556,1,69,1101,0,5,71,1101,1858,0,72,1106,0,73,1,1,20,215367,18,203726,34,97171,14,23279,41,109966,1101,3463,0,66,1102,1,1,67,1102,1,1895,68,1101,0,556,69,1101,1,0,71,1102,1,1897,72,1106,0,73,1,160,45,362742,1102,1,52181,66,1102,1,1,67,1101,0,1926,68,1101,556,0,69,1102,1,0,71,1101,1928,0,72,1105,1,73,1,1521,1101,0,101863,66,1102,4,1,67,1102,1,1955,68,1102,302,1,69,1102,1,1,71,1101,0,1963,72,1106,0,73,0,0,0,0,0,0,0,0,8,17579,1101,14173,0,66,1101,0,1,67,1102,1992,1,68,1102,1,556,69,1102,1,1,71,1101,0,1994,72,1105,1,73,1,-3,34,194342,1101,0,41893,66,1102,1,2,67,1102,2023,1,68,1102,302,1,69,1101,0,1,71,1102,2027,1,72,1106,0,73,0,0,0,0,19,185342,1101,0,57787,66,1102,1,1,67,1101,2056,0,68,1101,0,556,69,1102,1,1,71,1102,1,2058,72,1106,0,73,1,-209,18,101863,1102,1,66359,66,1102,1,1,67,1101,2087,0,68,1101,556,0,69,1101,1,0,71,1101,2089,0,72,1106,0,73,1,273,20,71789,1101,0,44293,66,1102,1,1,67,1102,2118,1,68,1102,1,556,69,1101,0,1,71,1102,1,2120,72,1106,0,73,1,125,46,292143,1101,0,95561,66,1101,1,0,67,1102,1,2149,68,1102,1,556,69,1102,1,5,71,1101,0,2151,72,1105,1,73,1,5,20,287156,18,407452,46,97381,46,389524,45,181371,1102,32869,1,66,1101,0,1,67,1102,2188,1,68,1102,1,556,69,1102,1,1,71,1101,0,2190,72,1105,1,73,1,128,16,200534,1101,0,71789,66,1101,4,0,67,1101,0,2219,68,1101,302,0,69,1101,0,1,71,1101,2227,0,72,1105,1,73,0,0,0,0,0,0,0,0,8,52737";

var smallInput =
@"3,60,1005,60,18,1101,0,1,61,4,61,104,1011,104,1,1105,1,22,1101,0,0,61,3,62,1007,62,0,64,1005,64,22,3,63,1002,63,2,63,1007,63,256,65,1005,65,48,1101,0,255,61,4,61,4,62,4,63,1105,1,22,99 ";

var smallest = "";

var input = smallInput;
//input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var pcCount = 2;
//var operations = input.Split(",").Select(long.Parse).ToList().ToDictionary((x,i) => i, x=> x).ToArray();

var operations = new Dictionary<long, long>();
var array = input.Split(",").Select(long.Parse).ToArray();
for (long i = 0; i < array.Length; i++)
{
    operations[i] = array[i];
}

var pcs = Enumerable.Range(0, pcCount).ToDictionary(x => (long)x, x => new StateMachine { inputBuffer = x, operations = operations });
var cache = Enumerable.Range(0, pcCount).ToDictionary(x => (long)x, x => new Queue<(long X, long Y, bool state)>(1000000));

foreach (var pc in pcs)
{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

    Task.Run(() =>
    {
        pc.Value.Execute();
    });

    Task.Run(async () =>
    {
        while (true)
        {
            if (!pc.Value.outputBuffer.Any()) { await Task.Delay(10); continue; }
            var id = pc.Value.outputBuffer.Dequeue();
            cache[id].Enqueue((X: pc.Value.outputBuffer.Dequeue(), Y: pc.Value.outputBuffer.Dequeue(), false));
        }
    });

    Task.Run(async () =>
    {
        while (true)
        {
            if (pc.Value.inputBuffer == -1 || !cache[pc.Key].Any()) { await Task.Delay(10); continue; }
            var packet = cache[pc.Key].Peek();
            if (packet == default)
            {
                pc.Value.inputBuffer = -1;
            }
            else
            {
                if (!packet.state)
                {
                    pc.Value.inputBuffer = packet.X;
                    packet.state = true;
                }
                else
                {
                    pc.Value.inputBuffer = packet.Y;
                    cache[pc.Key].Dequeue();
                }
            }
        }
    });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

}

timer.Stop();
Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms"); // 1102 too low
Console.ReadLine();




enum OpCode { Unknown = 0, Add = 1, Multiply = 2, Input = 3, Output = 4, JumpIfTrue = 5, JumpIfFalse = 6, LessThan = 7, Equals = 8, AdjustRelativeBase = 9, Stop = 99 }
enum ParameterMode { Position = 0, Value = 1, Relative = 2 }
class StateMachine
{
    private ParameterMode GetParameterMode(char v) => (ParameterMode)int.Parse(v.ToString());
    (ParameterMode parameterMode1, ParameterMode parameterMode2, ParameterMode parameterMode3, OpCode opCode) GetOpcode(long input)
    {
        var inputString = input.ToString();
        var opCode = (OpCode)int.Parse(new string(inputString.TakeLast(2).ToArray()));
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
    public Queue<long> outputBuffer { get; set; } = new Queue<long>(10000000);
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
                        outputBuffer.Enqueue(param1);
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