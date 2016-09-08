using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class ButtomClicked : MonoBehaviour {

	int lines, columns;

	public void Buttomclicked(string ButtomName)
	{
		if (ButtomName == "Play")
		{
			PlayerPrefs.SetInt("lines", int.Parse(GameObject.FindGameObjectWithTag("Lines").GetComponent<InputField>().text));
			PlayerPrefs.SetInt("columns", int.Parse(GameObject.FindGameObjectWithTag("Columns").GetComponent<InputField>().text));
			Application.LoadLevel("Game");
		}
	}
}