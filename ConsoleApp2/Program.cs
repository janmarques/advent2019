﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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
@"deal into new stack
cut -2
deal with increment 7
cut 8
cut -4
deal with increment 7
cut 3
deal with increment 9
deal with increment 3
cut -1";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var deck = Enumerable.Range(0, 10007).ToArray();
var deckHistory = new HashSet<int[]>();


foreach (var line in input.Replace("deal with increment", "deal").Replace("deal into new stack", "newStack").Split(Environment.NewLine))
{
    var split = line.Split(' ');
    var op = split[0];
    var number = split.Count() == 2 ? int.Parse(split[1]) : -1;

    if (op == "newStack")
    {
        deck = deck.Reverse().ToArray();
    }
    else if (op == "cut")
    {
        if (number > 0)
        {
            deck = deck.Skip(number).Concat(deck.Take(number)).ToArray();
        }
        else
        {
            deck = deck.TakeLast(number * -1).Concat(deck.Take(deck.Length + number)).ToArray();
        }
    }
    else
    {
        var newDeck = new int[deck.Length];
        int i = 0;
        foreach (var item in deck)
        {
            newDeck[(i * number) % deck.Length] = item;
            i++;
        }
        deck = newDeck;
    }

    if (deckHistory.Contains(deck))
    {
        goto end;
    }
    deckHistory.Add(deck);
}
end:


timer.Stop();

foreach (var item in deck)
{
    Console.Write(item.ToString() + " ");
}
Console.WriteLine();

result = deck.ToList().IndexOf(2019);
//result = deck[2019];

Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();