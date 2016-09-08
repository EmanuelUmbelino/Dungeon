using UnityEngine;
using System.Collections;

public class EnemyPathFind : MonoBehaviour {

    private int[] myPosition;
    private GameManager gameManager;
    private int[] target;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>() as GameManager;
        myPosition = new int[2];
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (this.name != other.name)
        {
            this.name = other.name;
            string[] i = this.name.Split('/');
            myPosition[0] = int.Parse(i[0]);
            myPosition[1] = int.Parse(i[1]);
        }
    }
    GameObject VerifyNext()
    {
        int[] distance = new int[2]; distance[0] = target[0] - myPosition[0];distance[1] = target[1] - myPosition[1];
        
        float[] totalDist = new float[8];

        totalDist[0] = gameManager.allGrid[myPosition[0]+1, myPosition[1]].transform.position.x +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].transform.position.y - target[0] - target[1] + 
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value;
        totalDist[1] = gameManager.allGrid[myPosition[0] - 1, myPosition[1]].transform.position.x +
            gameManager.allGrid[myPosition[0] - 1, myPosition[1]].transform.position.y - target[0] - target[1] +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value;
        totalDist[2] = gameManager.allGrid[myPosition[0], myPosition[1]+1].transform.position.x +
            gameManager.allGrid[myPosition[0], myPosition[1] + 1].transform.position.y - target[0] - target[1] +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value;
        totalDist[3] = gameManager.allGrid[myPosition[0], myPosition[1]-1].transform.position.x +
            gameManager.allGrid[myPosition[0], myPosition[1] - 1].transform.position.y - target[0] - target[1] +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value;
        totalDist[4] = gameManager.allGrid[myPosition[0] + 1, myPosition[1] + 1].transform.position.x +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1] + 1].transform.position.y - target[0] - target[1] +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value + 4;
        totalDist[5] = gameManager.allGrid[myPosition[0] - 1, myPosition[1] -1].transform.position.x +
            gameManager.allGrid[myPosition[0] - 1, myPosition[1] - 1].transform.position.y - target[0] - target[1] +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value + 4;
        totalDist[6] = gameManager.allGrid[myPosition[0]-1, myPosition[1] + 1].transform.position.x +
            gameManager.allGrid[myPosition[0] - 1, myPosition[1] + 1].transform.position.y - target[0] - target[1] +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value + 4;
        totalDist[7] = gameManager.allGrid[myPosition[0]+1, myPosition[1] - 1].transform.position.x +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1] - 1].transform.position.y - target[0] - target[1] +
            gameManager.allGrid[myPosition[0] + 1, myPosition[1]].GetComponent<GridChangeType>().value + 4;

        float value = Mathf.Min(totalDist[0], totalDist[1], totalDist[2], totalDist[3], totalDist[4], totalDist[5], totalDist[6], totalDist[7]);
        int myName = -1;
        for(int i = 0; i < totalDist.Length; i++)
        {
            if(value.Equals(totalDist[i]))
                myName = i;
        }        
        if (myName.Equals(0))
            return gameManager.allGrid[myPosition[0] + 1, myPosition[1]];
        else if (myName.Equals(1))
            return gameManager.allGrid[myPosition[0] - 1, myPosition[1]];
        else if (myName.Equals(2))
            return gameManager.allGrid[myPosition[0], myPosition[1] + 1];
        else if (myName.Equals(3))
            return gameManager.allGrid[myPosition[0], myPosition[1] - 1];
        else if (myName.Equals(4))
            return gameManager.allGrid[myPosition[0] + 1, myPosition[1] + 1];
        else if (myName.Equals(5))
            return gameManager.allGrid[myPosition[0] - 1, myPosition[1] - 1];
        else if (myName.Equals(6))
            return gameManager.allGrid[myPosition[0] - 1, myPosition[1] + 1];
        else if (myName.Equals(7))
            return gameManager.allGrid[myPosition[0] + 1, myPosition[1] - 1];

        return this.gameObject;

    }
    void MakeRoute()
    {
        while(myPosition[0] != target[0] || myPosition[1] != target[1])
        {

        }
    }

}
