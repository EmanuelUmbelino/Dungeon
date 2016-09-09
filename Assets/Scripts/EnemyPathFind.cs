using UnityEngine;
using System.Collections;

public class EnemyPathFind : MonoBehaviour {

    private int[] myPosition;
    private GameManager gameManager;
    private GameObject[,] grid;
    private bool[,] myRouteVerify;
    private int[,] myRoute;
    private int[,] tested;
    private int cost, previous;
    [SerializeField]
    private int[] target;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>() as GameManager;
        grid = gameManager.allGrid;
        myRouteVerify = new bool[PlayerPrefs.GetInt("lines"), PlayerPrefs.GetInt("columns")];
        myRoute = new int[PlayerPrefs.GetInt("lines"), PlayerPrefs.GetInt("columns")];
        tested = new int[PlayerPrefs.GetInt("lines"), PlayerPrefs.GetInt("columns")];
        cost = 0;
        myPosition = new int[2];
        InvokeRepeating("ds", 1,1);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (this.name != other.name)
        {
            this.name = other.name;
            string[] i = this.name.Split('/');
            myPosition[0] = int.Parse(i[0])-1;
            myPosition[1] = int.Parse(i[1])-1;
        }
    }
    private bool ValidCoordinates(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return false;
        }
        if (x >= PlayerPrefs.GetInt("lines") || y >= PlayerPrefs.GetInt("columns"))
        {
            return false;
        }
        return true;
    }
    GameObject VerifyNext()
    {
        /*[-1,1] [0,1] [1,1]
          [-1,0]       [1,0]
          [-1,-1][0,-1][1,-1]*/
        // print to test
        //print("minha posição é:" + (myPosition[0] + 1) + "/" + (myPosition[1] + 1) + ". olhando pro: " + grid[myPosition[0] + x, myPosition[1] + y].name + ". seu valor é: " + grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value + ". seu total é: " + totalDist[k]);

        int[] totalDist = new int[9];
        GameObject final = null;
        previous = 100;
        int x = -1, y = -1, k = 0;
        if (myPosition[0] != target[0]-1 || myPosition[1] != target[1]-1)
        {
            do
            {
                do
                {
                    if (ValidCoordinates(myPosition[0] + x, myPosition[1] + y))
                    {
                        if (x + y != 0 && Mathf.Abs(x + y) < 2)
                        {
                            totalDist[k] += Mathf.Abs(target[0] - grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[0]) + Mathf.Abs(target[1] -
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[1]) +
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value;

                        }
                        else
                        {
                            totalDist[k] += Mathf.Abs(target[0] - grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[0]) + Mathf.Abs(target[1] -
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[1]) +
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value + 4;
                        }
                        if (totalDist[k] < previous && myRouteVerify[myPosition[0] + x, myPosition[1] + y] != true)
                        {
                            previous = totalDist[k];
                            final = grid[myPosition[0] + x, myPosition[1] + y];
                            tested[myPosition[0], myPosition[1]] = totalDist[k];
                        }
                    }
                    y++;
                    k++;
                    if (k >= 9) break;
                }
                while (y < 2);
                y = -1;
                if (k >= 9) break;
                x++;
            }
            while (x < 2);
        }
        if (final == null)
        {
            previous = 0;
            final = grid[myPosition[0], myPosition[1]];
            myRouteVerify = new bool[PlayerPrefs.GetInt("lines"), PlayerPrefs.GetInt("columns")];
            print("why");
        }
        myRoute[myPosition[0], myPosition[1]] = tested[myPosition[0], myPosition[1]];
        cost += previous;
        return final;

    }
    void ds()
    {
        this.transform.position = VerifyNext().transform.position;

    }
    void LateUpdate()
    {
        myRouteVerify[myPosition[0], myPosition[1]] = true;
    }

}
