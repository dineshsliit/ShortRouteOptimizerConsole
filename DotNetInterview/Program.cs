using System;
using System.Collections.Generic;

namespace DotNetInterview
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var nodes = Graph.CreateGraph();

                Console.WriteLine("Enter the source node");
                string source = Console.ReadLine();

                Console.WriteLine("Enter the destination node");
                string destination = Console.ReadLine();

                var result = ShortPathService.GetShortestPathFromNodes(source, destination, nodes);

                Console.WriteLine($"Path: {string.Join(", ", result.NodeNames)}");
                Console.WriteLine($"Total Distance: {result.Distance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // Keep the console window open
            Console.ReadLine();
        }
    }
}
