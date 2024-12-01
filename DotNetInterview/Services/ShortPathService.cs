using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public static class ShortPathService
    {
        public static ShortestPathData GetShortestPathFromNodes(string fromNodeName, string toNodeName, List<Node> nodes)
        {
            var distances = new Dictionary<Node, int>();
            var previous = new Dictionary<Node, Node>();
            var priorityQueue = new SortedSet<(int distance, Node node)>(Comparer<(int, Node)>.Create((a, b) =>
            {
                int result = a.Item1.CompareTo(b.Item1);
                if (result == 0)
                    result = a.Item2.CompareTo(b.Item2);
                return result;

            }));
            var nodeDict = new Dictionary<string, Node>();

            foreach (var node in nodes)
            {
                distances[node] = int.MaxValue;
                nodeDict[node.Name] = node;
            }

            var startNode = nodeDict[fromNodeName];
            var endNode = nodeDict[toNodeName];

            distances[startNode] = 0;
            priorityQueue.Add((0, startNode));

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentNode) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                if (currentNode == endNode)
                {
                    break;
                }

                foreach (var (neighbor, weight) in currentNode.Neighbors)
                {
                    var distance = currentDistance + weight;
                    if (distance < distances[neighbor])
                    {
                        priorityQueue.Remove((distances[neighbor], neighbor));
                        distances[neighbor] = distance;
                        previous[neighbor] = currentNode;
                        priorityQueue.Add((distance, neighbor));
                    }
                }
            }

            var shortestPath = new List<string>();
            var current = endNode;

            while (current != null)
            {
                shortestPath.Insert(0, current.Name);
                previous.TryGetValue(current, out current);
            }

            return new ShortestPathData
            {
                NodeNames = shortestPath,
                Distance = distances[endNode]
            };
        }
    }
}
