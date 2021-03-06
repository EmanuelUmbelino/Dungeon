﻿using System;
using UnityEngine;


/// <summary>
/// Defines the parameters which will be used to find a path across a section of the map
/// </summary>
public class SearchParameters
{
    public Point StartLocation { get; set; }

    public Point EndLocation { get; set; }
        
    public bool[,] Map { get; set; }

    public bool[,] Water { get; set; }

    public SpriteRenderer[,] Sprites { get; set; }

    public SearchParameters(Point startLocation, Point endLocation, bool[,] map, bool[,] water, SpriteRenderer[,] sprites)
    {
        this.StartLocation = startLocation;
        this.EndLocation = endLocation;
        this.Map = map;
        this.Water = water;
        this.Sprites = sprites;
    }
}
