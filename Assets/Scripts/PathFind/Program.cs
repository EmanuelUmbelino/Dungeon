using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class Program : MonoBehaviour
{
    private bool[,] map;
    private bool[,] water;
    private int inRoute;
    private List<Point> path;
    [SerializeField]
    private int[] target;
    private Point myPosition;
    private GameManager gameManager;
    private SearchParameters searchParameters;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>() as GameManager;
        inRoute = 0;
        InvokeRepeating("WalkInRoute", 1, 1);
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

    void WalkInRoute()
    {
        if(path.Capacity > 0)
            if (myPosition.X != target[0] || myPosition.Y != target[1])
            {
                this.transform.position = gameManager.allGrid[path[inRoute].X, path[inRoute].Y].transform.position;
                inRoute++;
            }

    }
    
    void Think()
    {
        InitializeMap();
        PathFinder pathFinder = new PathFinder(searchParameters);
        path = pathFinder.FindPath();
        foreach(Point p in path)
        {
            print(p.X + "  /  " + p.Y);
        }
    }    

    private void InitializeMap()
    {
        int lines = PlayerPrefs.GetInt("lines");
        int columns = PlayerPrefs.GetInt("columns");
        this.map = new bool[lines, columns];
        this.water = new bool[lines, columns];

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
                else
                {
                    map[x, y] = true;
                    water[x, y] = false;
                }
            }

        var startLocation = myPosition;
        var endLocation = new Point(target[0],target[1]);
        this.searchParameters = new SearchParameters(startLocation, endLocation, map, water);
    }
    
}
