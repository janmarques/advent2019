using System;
using System.Collections.Generic;
using System.Linq;

var fullInput =
@"##.#.
##.#.
##.##
.####
.#...";

var smallInput =
@"....#
#..#.
#..##
..#..
#....";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();
var directions = new[] { new[] { 0, 1 }, new[] { 0, -1 }, new[] { 1, 0 }, new[] { -1, 0 } };

var result = 0l;

var grid = input.Split(Environment.NewLine).Select(x => x.Select(y => y == '#').ToArray()).ToArray();
var height = grid.Length;
var width = grid[0].Length;

var visited = new HashSet<long>();
while (true)
{
    var biodiversity = GetBiodiversity(grid);
    if (visited.Contains(biodiversity))
    {
        result = biodiversity;
        break;
    }
    visited.Add(biodiversity);
    grid = Transform(grid);
}

long GetBiodiversity(bool[][] grid) => grid.SelectMany(x => x).Select((x, i) => x ? (long)Math.Pow(2, i) : 0).Sum();
bool[][] Transform(bool[][] grid)
{
    var copy = grid.Select(x => x.ToArray()).ToArray();
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            var isBug = grid[x][y];
            var count = 0;
            foreach (var direction in directions)
            {
                var otherX = direction[0] + x;
                var otherY = direction[1] + y;
                if (otherX < 0 || otherY < 0 || otherX >= width || otherY >= height) { continue; }
                if (grid[otherX][otherY]) { count++; }
            }
            if (isBug && count != 1)
            {
                copy[x][y] = false;
            }
            if (!isBug && (count == 2 || count == 1))
            {
                copy[x][y] = true;
            }
        }
    }
    return copy;
}

foreach (var line in input.Split(Environment.NewLine))
{

}

timer.Stop();
Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();
