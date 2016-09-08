using UnityEngine;
using System.Collections;

public class EnemyPathFind : MonoBehaviour {

    private int[] myPosition;
    private GameManager gameManager;
    private GameObject[,] grid;
    private int[] target;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>() as GameManager;
        grid = gameManager.allGrid;
        myPosition = new int[2];
        target = new int[2];
        target[0] = 2; target[1] = 2;
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
        
        float[] totalDist = new float[9];
        GameObject final = null;
        float use = 100;
        int x = -1, y = -1, k = 0;
        do
        {
            do
            {
                if (k < 9)
                {
                    if (x + y != 0 && Mathf.Abs(x + y) < 2)
                    {
                        totalDist[k] += Mathf.Abs(target[0] + target[1] - grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[0] -
                                        grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[1] +
                                        grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value);

                    }
                    else
                    {
                        totalDist[k] += Mathf.Abs(target[0] + target[1] - grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[0] -
                                        grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().pos[1] +
                                        grid[myPosition[0] + x, myPosition[1] + y].GetComponent<GridChangeType>().value) + 4;
                    }
                    if (use > totalDist[k])
                    {
                        use = totalDist[k];
                        final = grid[myPosition[0] + x, myPosition[1] + y];
                    }
                }
                y++;
                k++;
            }
            while (y < 2);
            y = -1;
            x++;
        }
        while(x <= 2);

        return final;

    }
    void ds()
    {
        print("alvo " + target[0] + "/" + target[1]);
        print("proximo move "+ VerifyNext().name);
        //while(myPosition[0] != target[0] || myPosition[1] != target[1])
        {
        }

    }

}
