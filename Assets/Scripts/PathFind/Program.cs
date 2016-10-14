using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class Program : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private bool[,] map;
    private bool[,] water;
    private bool live;
    private int inRoute;
    private List<Point> path;
    private int[] target;
    private Point myPosition;
    private GameManager gameManager;
    private SearchParameters searchParameters;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>() as GameManager;
        this.tag = "Enemy";
        InvokeRepeating("Go", 2, 0.5f);
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
    public void Live()
    {
        inRoute = 0;
        Think();
        live = true;
    }

    void Go()
    {
        this.transform.position = gameManager.allGrid[path[inRoute].X, path[inRoute].Y].transform.position;
    }

    void Update()
    {
        if (live && path.Capacity > 0)
        {
            //print(path.Capacity + " / " + inRoute + " / " + path[inRoute].X);
            if (new Vector3(Mathf.Round(this.transform.position.x - 0.01f), Mathf.Round(this.transform.position.y - 0.01f), Mathf.Round(this.transform.position.z)).Equals
                (gameManager.allGrid[path[inRoute].X, path[inRoute].Y].transform.position))
            {
                path.RemoveAt(0);
                animator.SetInteger("local", 0);
                if (myPosition.X < path[inRoute].X)
                    animator.SetInteger("local", 4);
                else
                    animator.SetInteger("local", 2);
                if (myPosition.Y < path[inRoute].Y)
                    animator.SetInteger("local", 3);
                else
                    animator.SetInteger("local", 1);
            }
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
