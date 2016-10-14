using System;
using UnityEngine;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour
{
    private int width;
    private int height;
    private Node[,] nodes;
    private Node startNode;
    private Node endNode;
    private SearchParameters searchParameters;

    public PathFinder(SearchParameters searchParameters)
    {
        this.searchParameters = searchParameters;
        InitializeNodes(searchParameters.Map, searchParameters.Water, searchParameters.Sprites);
        this.startNode = this.nodes[searchParameters.StartLocation.X, searchParameters.StartLocation.Y];
        this.startNode.State = NodeState.Open;
        this.endNode = this.nodes[searchParameters.EndLocation.X, searchParameters.EndLocation.Y];
    }

    public List<Point> FindPath()
    {
        List<Point> path = new List<Point>();
        bool success = Search(startNode);
        if (success)
        {    
            Node node = this.endNode;
            while (node.ParentNode != null)
            {
                path.Add(node.Location);
                node = node.ParentNode;
            }

            path.Reverse();
        }

        return path;
    }

    private void InitializeNodes(bool[,] map, bool[,] water, SpriteRenderer[,] sprite)
    {
        this.width = map.GetLength(0);
        this.height = map.GetLength(1);
        this.nodes = new Node[this.width, this.height];
        for (int y = 0; y < this.height; y++)
        {
            for (int x = 0; x < this.width; x++)
            {
                this.nodes[x, y] = new Node(x, y, map[x, y], this.searchParameters.EndLocation, water[x,y], sprite[x,y]);
            }
        }
    }

    private bool Search(Node currentNode)
    {
        currentNode.State = NodeState.Closed;
        currentNode.Sprite.color = new Color(0.6f,1f,0.6f,1);
        List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

        nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
        foreach (var nextNode in nextNodes)
        {
            if (nextNode.Location.Equals(this.endNode.Location))
            {
                return true;
            }
            else
            {
                if (Search(nextNode))
                    return true;
            }
        }

        return false;
    }
    
    private List<Node> GetAdjacentWalkableNodes(Node fromNode)
    {
        List<Node> walkableNodes = new List<Node>();
        IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

        foreach (var location in nextLocations)
        {
            int x = location.X;
            int y = location.Y;
            
            if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                continue;

            Node node = this.nodes[x, y];

            if (!node.IsWalkable)
                continue;

            if (node.State == NodeState.Closed)
                continue;

            if (node.State == NodeState.Open)
            {
                float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location, node.IsWater);
                float gTemp = fromNode.G;
                //print(node.Location.X + "/" + node.Location.Y + "  " + node.G + "  " + gTemp);
                if (gTemp < node.G)
                {
                    node.ParentNode = fromNode;
                    walkableNodes.Add(node);
                    node.Sprite.color = new Color(1f, 0.6f, 0.6f, 1);
                }
            }
            else
            {
                node.ParentNode = fromNode;
                node.State = NodeState.Open;
                walkableNodes.Add(node);
                node.Sprite.color = new Color(0.6f,0.6f,1f,1);
            }
        }

        return walkableNodes;
    }

    private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
    {
        return new Point[]
        {
            new Point(fromLocation.X-1, fromLocation.Y-1),
            new Point(fromLocation.X-1, fromLocation.Y  ),
            new Point(fromLocation.X-1, fromLocation.Y+1),
            new Point(fromLocation.X,   fromLocation.Y+1),
            new Point(fromLocation.X+1, fromLocation.Y+1),
            new Point(fromLocation.X+1, fromLocation.Y  ),
            new Point(fromLocation.X+1, fromLocation.Y-1),
            new Point(fromLocation.X,   fromLocation.Y-1)
        };
    }
}
