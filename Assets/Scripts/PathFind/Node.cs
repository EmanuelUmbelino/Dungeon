using System;
using UnityEngine;

/// <summary>
/// Represents a single node on a grid that is being searched for a path between two points
/// </summary>
public class Node
{
    private Node parentNode;

    public Point Location { get; private set; }

    public bool IsWalkable { get; set; }

    public bool IsWater { get; set; }

    public float G { get; private set; }

    public float H { get; private set; }

    public NodeState State { get; set; }

    public float F
    {
        get { return this.G + this.H; }
    }

    public Node ParentNode
    {
        get { return this.parentNode; }
        set
        {
            this.parentNode = value;
            this.G = this.parentNode.G + GetTraversalCost(this.Location, this.parentNode.Location, this.IsWater);
        }
    }

    public Node(int x, int y, bool isWalkable, Point endLocation, bool isWater)
    {
        this.Location = new Point(x, y);
        this.State = NodeState.Untested;
        this.IsWalkable = isWalkable;
        this.IsWater = IsWater;
        this.H = GetTraversalCost(this.Location, endLocation, isWater);
        this.G = 0;
    }

    public override string ToString()
    {
        return string.Format("{0}, {1}: {2}", this.Location.X, this.Location.Y, this.State);
    }
    internal static float GetTraversalCost(Point location, Point otherLocation, bool isWater)
    {
        float deltaX = otherLocation.X - location.X;
        float deltaY = otherLocation.Y - location.Y;
        float water = 0;
        if (isWater)
            water += 5;
        return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY) + water;
    }
}

public struct Point
{
    public int X, Y;
    public Point(int px, int py)
    {
        X = px;
        Y = py;
    }
}