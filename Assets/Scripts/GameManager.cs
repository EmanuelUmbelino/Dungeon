using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int lines, columns;
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
		for (int i = 0; i < lines; i++) 
		{
			for(int j = columns-1; j >= 0; j--)
			{
                GameObject x = (GameObject)Instantiate(Grid, new Vector3(i - lines / 2, j - columns / 2, j), transform.rotation);
                x.name = i + "/" + (columns-1-j);
                allGrid[i, columns - 1 - j] = x;

			}
		}
	}

	void Update () 
	{
		//print (lines + "||" + columns);
	}
}
