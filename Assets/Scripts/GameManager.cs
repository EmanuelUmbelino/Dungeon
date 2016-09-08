using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int lines, columns;
    [SerializeField]
    private GameObject Grid;
    public GameObject[,] allGrid;

	void Start () 
	{
		lines = PlayerPrefs.GetInt ("lines");
        columns = PlayerPrefs.GetInt("columns");
        allGrid = new GameObject[lines, columns];
		Spawnfield ();
	}

	void Spawnfield()
	{
		for (int i = 1; i < lines+1; i++) 
		{
			for(int j = 1; j < columns+1; j++)
			{
                GameObject x = (GameObject)Instantiate(Grid, new Vector3(i - (lines + 1) / 2, j - (columns + 1) / 2, 0), transform.rotation);
                x.name = i + "/" + (columns - j + 1);
                allGrid[i-1, columns - j] = x;

			}
		}
	}

	void Update () 
	{
		//print (lines + "||" + columns);
	}
}
