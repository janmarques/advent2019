﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Security.Cryptography;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//var fullInput =
//@"cut 9374
//deal with increment 48
//cut -2354
//deal with increment 12
//cut -7039
//deal with increment 14
//cut -2325
//deal with increment 40
//deal into new stack
//cut 4219
//deal with increment 15
//cut -3393
//deal with increment 48
//cut 1221
//deal with increment 66
//cut 1336
//deal with increment 53
//deal into new stack
//cut -5008
//deal into new stack
//deal with increment 34
//cut 8509
//deal with increment 24
//cut -1292
//deal into new stack
//cut 8404
//deal with increment 17
//cut -105
//deal with increment 51
//cut 2974
//deal with increment 5
//deal into new stack
//deal with increment 53
//cut 155
//deal with increment 31
//cut 2831
//deal with increment 61
//cut -4193
//deal into new stack
//cut 9942
//deal with increment 13
//cut -532
//deal with increment 41
//cut 2847
//deal into new stack
//cut -2609
//deal with increment 72
//cut 9098
//deal with increment 64
//deal into new stack
//cut 4292
//deal into new stack
//cut -4427
//deal with increment 24
//cut -4713
//deal into new stack
//cut 5898
//deal with increment 56
//cut -2515
//deal with increment 2
//cut -5502
//deal with increment 66
//cut 8414
//deal with increment 7
//deal into new stack
//deal with increment 35
//deal into new stack
//deal with increment 29
//cut -2176
//deal with increment 14
//cut 7773
//deal with increment 36
//cut 2903
//deal into new stack
//deal with increment 75
//cut 239
//deal with increment 45
//cut 5450
//deal with increment 10
//cut 6661
//deal with increment 64
//cut -6842
//deal with increment 40
//deal into new stack
//deal with increment 31
//deal into new stack
//deal with increment 46
//cut 6462
//deal into new stack
//cut -8752
//deal with increment 28
//deal into new stack
//deal with increment 43
//deal into new stack
//deal with increment 54
//cut 9645
//deal with increment 44
//cut 5342
//deal with increment 66
//cut 3785";

//var smallInput =
//@"deal with increment 7
//deal into new stack
//deal into new stack";

//var smallest = "";

//var input = smallInput;
//input = fullInput;
////input = smallest;
//var timer = System.Diagnostics.Stopwatch.StartNew();

//var parsed = input.Replace("deal with increment", "deal").Replace("deal into new stack", "newStack").Split(Environment.NewLine).Select(x => x.Split(' '))
//    .Select(x => (op: x[0], number: BigInteger.Parse(x.ElementAtOrDefault(1) ?? "-1")));

//var result = BigInteger.Parse("2020");
//var deckSize = BigInteger.Parse("119315717514047");
//var repeats = BigInteger.Parse("101741582076661");

//(BigInteger a, BigInteger b) bigFunction = default;
//foreach (var (op, number) in parsed)
//{
//    var extraState = op switch
//    {
//        "newStack" => (BigInteger.MinusOne, BigInteger.MinusOne),
//        "cut" => (BigInteger.One, -1 * number),
//        _ => (number, BigInteger.Zero),
//    };

//    if (bigFunction == default)
//    {
//        bigFunction = extraState;
//        continue;
//    }
//    bigFunction = Merge(bigFunction, extraState);
//}

//var repeatedFunction = (BigInteger.One, BigInteger.Zero);
//while (repeats > 0)
//{
//    if (!repeats.IsEven)
//    {
//        repeatedFunction = Merge(repeatedFunction, bigFunction);
//    }
//    repeats /= 2;
//    bigFunction = Merge(bigFunction, bigFunction);
//}
//bigFunction = repeatedFunction;

//result = GoodMod(GoodMod(result - bigFunction.b, deckSize) * InverseMod(bigFunction.a, deckSize), deckSize);

//timer.Stop();

//Console.WriteLine();
//Console.WriteLine(result); // 79855812422607 correct
//Console.WriteLine(timer.ElapsedMilliseconds + "ms");
//Console.ReadLine();

//(BigInteger, BigInteger) Merge((BigInteger, BigInteger) a, (BigInteger, BigInteger) b)
//{
//    return (GoodMod(a.Item1 * b.Item1, deckSize), GoodMod(a.Item2 * b.Item1 + b.Item2, deckSize));
//}

//BigInteger GoodMod(BigInteger i, BigInteger m) => ((i % m) + m) % m;

//// https://codeforces.com/blog/entry/72593

//// https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
//BigInteger InverseMod(BigInteger a, BigInteger n)
//{
//    var t = BigInteger.Zero;
//    var newT = BigInteger.One;
//    var r = n;
//    var newR = a;
//    while (newR != 0)
//    {
//        var q = r / newR;
//        (t, newT) = (newT, t - q * newT);
//        (r, newR) = (newR, r - q * newR);
//    }
//    if (r > 1) { throw new Exception(); }
//    if (t < 0) { t += n; }
//    return t;
//}