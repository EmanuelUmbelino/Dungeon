﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class Program : MonoBehaviour
{
    private bool[,] map;
    private bool[,] water;
    private int inRoute;
    private List<Point> path;
    private int[] target;
    private Point myPosition;
    private GameManager gameManager;
    private SearchParameters searchParameters;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>() as GameManager;
        inRoute = 0;
        //InvokeRepeating("WalkInRoute", 1, 0.1f);
        Think();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (this.name != other.name)
        {
            this.name = other.name;
            string[] i = this.name.Split('/');
            myPosition.X = int.Parse(i[0]);
            myPosition.Y = int.Parse(i[1]);
        }
    }
    void ChangeRoute(Point newTarget)
    {
        inRoute = 0;
        target[0] = newTarget.X; target[1] = newTarget.Y;
        Think();
    }

    void Update()
    {
        if (path.Capacity > 0)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, gameManager.allGrid[path[inRoute].X, path[inRoute].Y].transform.position, 0.03f);
            //print(path.Capacity + " / " + inRoute + " / " + path[inRoute].X);
            if (new Vector3(Mathf.Round(this.transform.position.x-0.01f), Mathf.Round(this.transform.position.y - 0.01f), Mathf.Round(this.transform.position.z)).Equals
                (gameManager.allGrid[path[inRoute].X, path[inRoute].Y].transform.position) && inRoute < path.Capacity-1)
                inRoute++;
        }

    }
    
    void Think()
    {
        InitializeMap();
        PathFinder pathFinder = new PathFinder(searchParameters);
        path = pathFinder.FindPath();
        inRoute++;
    }    

    private void InitializeMap()
    {
        int lines = PlayerPrefs.GetInt("lines");
        int columns = PlayerPrefs.GetInt("columns");
        this.map = new bool[lines, columns];
        this.water = new bool[lines, columns];
        Point startLocation = myPosition;
        Point endLocation = myPosition;

        for (int y = 0; y < columns; y++)
            for (int x = 0; x < lines; x++)
            {
                if(gameManager.allGrid[x,y].tag.Equals("Wall"))
                {
                    map[x, y] = false;
                    water[x, y] = false;
                }
                else if (gameManager.allGrid[x, y].tag.Equals("Water"))
                {
                    map[x, y] = true;
                    water[x, y] = true;
                }
                else if (gameManager.allGrid[x, y].tag.Equals("Target"))
                {
                    map[x, y] = true;
                    water[x, y] = false;
                    endLocation = new Point(x, y);
                }
                else
                {
                    map[x, y] = true;
                    water[x, y] = false;
                }
            }

        this.searchParameters = new SearchParameters(startLocation, endLocation, map, water);
    }
    
}
