//using System;
//using System.Collections.Generic;
//using System.Linq;

//var fullInput =
//@"##.#.
//##.#.
//##.##
//.####
//.#...";

//var smallInput =
//@"....#
//#..#.
//#..##
//..#..
//#....";

//var smallest = "";

//var input = smallInput;
//input = fullInput;
////input = smallest;
//var timer = System.Diagnostics.Stopwatch.StartNew();
//var directions = new[] { (x: 0, y: 1, d: 'v'), (x: 0, y: -1, d: '^'), (x: 1, y: 0, d: '>'), (x: -1, y: 0, d: '<') };

//var result = 0l;

//var grid = input.Split(Environment.NewLine).Select(x => x.Select(y => y == '#').ToArray()).ToArray();
//var height = grid.Length;
//var width = grid[0].Length;

//var init = new GridContainer { Grid = grid };
//init.EnsureParent();
//init.EnsureChild();
//var containers = new HashSet<GridContainer> { init, init.Parent, init.Child };

//for (int i = 0; i < 200; i++)
//{
//    Console.WriteLine(i);
//    Console.WriteLine();
//    var list = containers.OrderBy(x => x.Level).ToList(); // copy because we add to it during the run
//    foreach (var container in list)
//    {
//        Transform(container);
//    }

//    foreach (var container in list)
//    {
//        container.Grid = container.NewGrid;
//        container.NewGrid = null;
//        //container.Print();
//        //Console.WriteLine();
//        //Console.WriteLine();
//    }
//}

//result = containers.SelectMany(x => x.Grid.SelectMany(y => y)).Count(x => x);

//void Transform(GridContainer gridContainer)
//{
//    var grid = gridContainer.Grid;
//    var copy = grid.Select(x => x.ToArray()).ToArray();
//    for (int y = 0; y < height; y++)
//    {
//        for (int x = 0; x < width; x++)
//        {
//            if (x == 2 && y == 2) { continue; }
//            var isBug = grid[x][y];
//            var count = 0;
//            foreach (var direction in directions)
//            {
//                var otherX = direction.x + x;
//                var otherY = direction.y + y;
//                if (otherX < 0 || otherY < 0 || otherX >= width || otherY >= height)
//                {
//                    gridContainer.EnsureParent();
//                    bool relevant;
//                    switch (direction.d)
//                    {
//                        case '>': // from e,j to 14
//                            relevant = gridContainer.Parent.Grid[2][3];
//                            break;
//                        case '<': // from a,f to 12
//                            relevant = gridContainer.Parent.Grid[2][1];
//                            break;
//                        case 'v': // from u,v to 18
//                            relevant = gridContainer.Parent.Grid[3][2];
//                            break;
//                        case '^': // from a,b,.. 8 to 
//                            relevant = gridContainer.Parent.Grid[1][2];
//                            break;
//                        default: throw new NotImplementedException();
//                    }
//                    if (relevant)
//                    {
//                        count++;
//                    }
//                }
//                else if (otherX == 2 && otherY == 2)
//                {
//                    gridContainer.EnsureChild();
//                    IEnumerable<bool> relevant;
//                    switch (direction.d)
//                    {
//                        case '>': // from 12 to a,f,k,p,u
//                            relevant = gridContainer.Child.Grid.Select(x => x.First());
//                            break;
//                        case '<': // from 14 to e,j..
//                            relevant = gridContainer.Child.Grid.Select(x => x.Last());
//                            break;
//                        case 'v': // from 8 to a,b,..
//                            relevant = gridContainer.Child.Grid.First();
//                            break;
//                        case '^': // from 18 to u,v,..
//                            relevant = gridContainer.Child.Grid.Last();
//                            break;
//                        default: throw new NotImplementedException();
//                    }
//                    count += relevant.Count(x => x);
//                }
//                else if (grid[otherX][otherY])
//                {
//                    count++;
//                }

//            }
//            if (isBug && count != 1)
//            {
//                copy[x][y] = false;
//            }
//            if (!isBug && (count == 2 || count == 1))
//            {
//                copy[x][y] = true;
//            }
//        }
//    }
//    gridContainer.NewGrid = copy;
//    if (gridContainer.Parent != null) { containers.Add(gridContainer.Parent); }
//    if (gridContainer.Child != null) { containers.Add(gridContainer.Child); }
//}

//foreach (var line in input.Split(Environment.NewLine))
//{

//}

//timer.Stop();
//Console.WriteLine(result);
//Console.WriteLine(timer.ElapsedMilliseconds + "ms");
//Console.ReadLine();

//class GridContainer
//{
//    public int Level { get; set; }
//    public bool[][] Grid { get; set; } = Enumerable.Range(0, 5).Select(x => Enumerable.Range(0, 5).Select(y => false).ToArray()).ToArray();
//    public bool[][] NewGrid { get; set; }
//    public GridContainer Parent { get; set; }
//    public GridContainer Child { get; set; }

//    public void EnsureChild()
//    {
//        if (Child != null) { return; }
//        Child = new GridContainer() { Parent = this, Level = Level + 1 };
//    }

//    public void EnsureParent()
//    {
//        if (Parent != null) { return; }
//        Parent = new GridContainer() { Child = this, Level = Level - 1 };
//    }

//    public void Print()
//    {
//        Console.WriteLine($"Level {Level}");
//        for (int i = 0; i < Grid.Length; i++)
//        {
//            for (int j = 0; j < Grid[i].Length; j++)
//            {
//                Console.Write(Grid[i][j] ? '#' : '.');
//            }
//            Console.WriteLine();
//        }
//    }
//}

