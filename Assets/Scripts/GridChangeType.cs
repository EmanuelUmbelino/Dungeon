using UnityEngine;
using System.Collections;

public class GridChangeType : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite[] sprites;
    public int value;
    public int[] pos;
    int i = 0;
    void Start()
    {
        string[] test = this.name.Split('/'); pos = new int[2];
        pos[0] = int.Parse(test[0]); pos[1] = int.Parse(test[1]);
        value = 10;
    }
    void OnMouseDown()
    {
        i++;
        if (i > 2)
        {
            i = 0;
            this.gameObject.tag = "Grid";
            value = 10;
        }
        else if (i.Equals(1))
        {
            this.gameObject.tag = "Water";
            value = 20;
        }
        else if (i.Equals(2))
        {
            this.gameObject.tag = "Wall";
            value = 1000;
        }
        sprite.sprite = sprites[i];
    }

}
