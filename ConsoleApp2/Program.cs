﻿using System;
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

var result = 0;
var deckSize = 10007;



result = 6526;

foreach (var line in input.Replace("deal with increment", "deal").Replace("deal into new stack", "newStack").Split(Environment.NewLine).Reverse())
{
    var split = line.Split(' ');
    var op = split[0];
    var number = split.Count() == 2 ? int.Parse(split[1]) : -1;

    if (op == "newStack")
    {
        result = NewStackR(result);
    }
    else if (op == "cut")
    {
        result = CutR(result, number);
    }
    else
    {
        result = DealR(result, number);
    }
}


timer.Stop();

Console.WriteLine();

Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();

int NewStackR(int pos) => (2 * deckSize - 1 - pos) % deckSize;
int CutR(int pos, int n) => (pos + n + deckSize) % deckSize;
int DealR(int pos, int increment)
{
    int k = 0;
    while (true)
    {
        if ((pos + k * deckSize) % increment == 0)
        {
            return (pos + k * deckSize) / increment;
        }
        k++;
    }
}

// https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Modular_integers
long InvertMod(int a, int n)
{
    long t = 0;
    long newT = 1;
    long r = n;
    long newR = a;
    while (newR != 0)
    {
        var quotient = r / a;
        (t, newT) = (newT, t - quotient * newT);
        (r, newR) = (a, r - quotient * newR);
    }
    if (t > 1) { throw new Exception(); }
    return t + n;
}

// https://stackoverflow.com/questions/7483706/c-sharp-modinverse-function
int modInverse(int a, int n)
{
    int i = n, v = 0, d = 1;
    while (a > 0)
    {
        int t = i / a, x = a;
        a = i % x;
        i = x;
        x = d;
        d = v - t * x;
        v = x;
    }
    v %= n;
    if (v < 0) v = (v + n) % n;
    return v;
}

int ModInverse2(int a, int n) => ((int)BigInteger.ModPow(new BigInteger(a), new BigInteger(n - 2), new BigInteger(n)));