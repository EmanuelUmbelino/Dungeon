using UnityEngine;
using System.Collections;

public class EnemyPathFind : MonoBehaviour {

    private int[] myPosition;
    private GameManager gameManager;
    private GameObject[,] grid;
    private int[,] used;
    [SerializeField]
    private int[] target;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>() as GameManager;
        grid = gameManager.allGrid;
        used = new int[PlayerPrefs.GetInt("lines"), PlayerPrefs.GetInt("columns")];
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
        // print("olhando pro: " + grid[myPosition[0] + x, myPosition[1] + y].name + ". seu valor é: " + (grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value) + ". seu total é: " + totalDist[k]);

        float[] totalDist = new float[9];
        GameObject final = null;
        float use = 100;
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
                            totalDist[k] += Mathf.Abs((target[0] - grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[0]) + (target[1] -
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[1])) +
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value;
                            print("minha posição é:" + myPosition[0]+"/"+myPosition[1] + ". olhando pro: " + grid[myPosition[0] + x, myPosition[1] + y].name + ". seu valor é: " +
                                grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value + ". seu total é: " + totalDist[k]);

                        }
                        else
                        {
                            totalDist[k] += Mathf.Abs((target[0] - grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[0]) + (target[1] -
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[1])) +
                                            grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value + 4;
                            print("minha posição é:" + myPosition[0] + "/" + myPosition[1] + ". olhando pro: " + grid[myPosition[0] + x, myPosition[1] + y].name + ". seu valor é: " +
                                (grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value + 4) + ". seu total é: " + totalDist[k]);
                        }
                        if (totalDist[k] < use && grid[myPosition[0] + x, myPosition[1] + y].name != this.gameObject.name &&
                            used[myPosition[0] + x, myPosition[1] + y] != 1)
                        {
                            use = totalDist[k];
                            final = grid[myPosition[0] + x, myPosition[1] + y];
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
        if(final == null)final = grid[myPosition[0], myPosition[1]];
        used[final.GetComponent<GridChangeType>().pos[0], final.GetComponent<GridChangeType>().pos[1]] = 1;
        return final;

    }
    void ds()
    {
        this.transform.position = VerifyNext().transform.position;
        print(VerifyNext().name);

    }

}
