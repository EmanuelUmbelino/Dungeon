using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int lines, columns;
    [SerializeField]
    private GameObject Grid;
    [SerializeField]
    private Transform mCamera;
    private Vector3 nCamera;
    public GameObject[,] allGrid;
    public static bool target;
    public static bool enemy;

    void Start () 
	{
		lines = PlayerPrefs.GetInt ("lines");
        columns = PlayerPrefs.GetInt("columns");
        allGrid = new GameObject[lines, columns];
		Spawnfield ();
        nCamera = mCamera.position;
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
    void Update()
    {
        if (new Vector3(Mathf.Round(mCamera.position.x - 0.3f), Mathf.Round(mCamera.position.y - 0.3f), Mathf.Round(mCamera.position.z)) !=
                nCamera)
            mCamera.position = Vector3.Lerp(mCamera.position, nCamera, 0.05f);
    }


    public void Atack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject i in enemies)
        {
            i.GetComponent<Program>().enabled = true;
        }
    }
    public void ToTarget()
    {
        target = true;
        enemy = false;
    }
    public void ToEnemy()
    {
        enemy = true;
        target = false;
    }
    public void MoveX(int distance)
    {
        nCamera = mCamera.position + new Vector3(distance, 0);
    }
    public void MoveY(int distance)
    {
        nCamera = mCamera.position + new Vector3(0, distance);
    }
}
