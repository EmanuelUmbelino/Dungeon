using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class ButtomClicked : MonoBehaviour {

    int lines, columns;
    [SerializeField]
    private Text _text;

	public void Buttomclicked(string ButtomName)
	{
        try
        {
            if (ButtomName == "Play" && int.Parse(GameObject.FindGameObjectWithTag("Lines").GetComponent<InputField>().text) > 0 &&
                int.Parse(GameObject.FindGameObjectWithTag("Columns").GetComponent<InputField>().text) > 0)
            {
                PlayerPrefs.SetInt("lines", int.Parse(GameObject.FindGameObjectWithTag("Lines").GetComponent<InputField>().text));
                PlayerPrefs.SetInt("columns", int.Parse(GameObject.FindGameObjectWithTag("Columns").GetComponent<InputField>().text));
                Application.LoadLevel("Game");
            }
            else
            {
                _text.text = "Please, just put numbers greater than zero";
            }
        }
        catch 
        {
            _text.text = "Please, just put numbers greater than zero";
        }
	}
}