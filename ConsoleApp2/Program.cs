using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

var fullInput =
@"cut 9374
deal with increment 48
cut -2354
deal with increment 12
cut -7039
deal with increment 14
cut -2325
deal with increment 40
deal into new stack
cut 4219
deal with increment 15
cut -3393
deal with increment 48
cut 1221
deal with increment 66
cut 1336
deal with increment 53
deal into new stack
cut -5008
deal into new stack
deal with increment 34
cut 8509
deal with increment 24
cut -1292
deal into new stack
cut 8404
deal with increment 17
cut -105
deal with increment 51
cut 2974
deal with increment 5
deal into new stack
deal with increment 53
cut 155
deal with increment 31
cut 2831
deal with increment 61
cut -4193
deal into new stack
cut 9942
deal with increment 13
cut -532
deal with increment 41
cut 2847
deal into new stack
cut -2609
deal with increment 72
cut 9098
deal with increment 64
deal into new stack
cut 4292
deal into new stack
cut -4427
deal with increment 24
cut -4713
deal into new stack
cut 5898
deal with increment 56
cut -2515
deal with increment 2
cut -5502
deal with increment 66
cut 8414
deal with increment 7
deal into new stack
deal with increment 35
deal into new stack
deal with increment 29
cut -2176
deal with increment 14
cut 7773
deal with increment 36
cut 2903
deal into new stack
deal with increment 75
cut 239
deal with increment 45
cut 5450
deal with increment 10
cut 6661
deal with increment 64
cut -6842
deal with increment 40
deal into new stack
deal with increment 31
deal into new stack
deal with increment 46
cut 6462
deal into new stack
cut -8752
deal with increment 28
deal into new stack
deal with increment 43
deal into new stack
deal with increment 54
cut 9645
deal with increment 44
cut 5342
deal with increment 66
cut 3785";

var smallInput =
@"deal with increment 7
deal into new stack
deal into new stack";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = BigInteger.Parse("2020");
var deckSize = BigInteger.Parse("119315717514047");
var repeats = BigInteger.Parse("101741582076661");


result = new BigInteger(2019);
deckSize = new BigInteger(10007);
repeats =  new BigInteger(3);

//result = new BigInteger(6526);
//deckSize = new BigInteger(10007);
//forward = false;

var input2 = input.Replace("deal with increment", "deal").Replace("deal into new stack", "newStack").Split(Environment.NewLine);


var abs = new List<(BigInteger a, BigInteger b)>();
foreach (var line in input2)
{
    var split = line.Split(' ');
    var op = split[0];
    var number = split.Count() == 2 ? BigInteger.Parse(split[1]) : new BigInteger(-1);

    if (op == "newStack")
    {
        abs.Add(NewStackBreakdown(result));
    }
    else if (op == "cut")
    {
        abs.Add(CutBreakdown(result, number));
    }
    else
    {
        abs.Add(DealBreakdown(result, number));
    }
}

var state = (abs.First().a, abs.First().b);
for (int i = 1; i < abs.Count; i++)
{
    var ab = abs.ElementAt(i);
    state = Merge(state, ab);
}

Console.WriteLine(GoodMod(state.a * result + state.b, deckSize));

var g = (BigInteger.One, BigInteger.Zero);
var f = state;
while (repeats > 0)
{
    if (!repeats.IsEven)
    {
        g = Merge(g, f);    
    }
    repeats /= 2;
    f = Merge(f, f);
}
state = g;

Console.WriteLine(GoodMod(state.a * result + state.b, deckSize));

(BigInteger, BigInteger) Merge((BigInteger, BigInteger)a, (BigInteger, BigInteger) b)
{
    return (GoodMod(a.Item1 * b.Item1, deckSize), GoodMod(a.Item2 * b.Item1 + b.Item2, deckSize));
}

timer.Stop();

Console.WriteLine();

//Console.WriteLine(result); // 82616125058986 too high
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();

BigInteger NewStackR(BigInteger pos) => (2 * deckSize - 1 - pos) % deckSize;
BigInteger CutR(BigInteger pos, BigInteger n) => (pos + n + deckSize) % deckSize;
BigInteger DealR(BigInteger pos, BigInteger increment)
{
    BigInteger k = 0;
    while (true)
    {
        if ((pos + k * deckSize) % increment == 0)
        {
            return (pos + k * deckSize) / increment;
        }
        k++;
    }
}


(BigInteger a, BigInteger b) NewStackBreakdown(BigInteger pos) => (BigInteger.MinusOne, BigInteger.MinusOne);
(BigInteger a, BigInteger b) CutBreakdown(BigInteger pos, BigInteger n) => (BigInteger.One, -1 * n);
(BigInteger a, BigInteger b) DealBreakdown(BigInteger pos, BigInteger n) => (n, BigInteger.Zero);

BigInteger NewStack(BigInteger pos) => GoodMod(-1 * pos - 1, deckSize);
BigInteger Cut(BigInteger pos, BigInteger n) => GoodMod(pos - n, deckSize);
BigInteger Deal(BigInteger pos, BigInteger n) => GoodMod(pos * n, deckSize);

BigInteger GoodMod(BigInteger i, BigInteger m) => ((i % m) + m) % m;

// https://codeforces.com/blog/entry/72593


// https://stackoverflow.com/questions/30224589/biginteger-powbiginteger-biginteger
static BigInteger Pow(BigInteger a, BigInteger b)
{
    BigInteger total = 1;
    while (b > int.MaxValue)
    {
        b -= int.MaxValue;
        total = total * BigInteger.Pow(a, int.MaxValue);
    }
    total = total * BigInteger.Pow(a, (int)b);
    return total;
}