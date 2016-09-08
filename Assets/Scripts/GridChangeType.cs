using UnityEngine;
using System.Collections;

public class GridChangeType : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite[] sprites;
    public int value;
    int i = 0;
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
            value = 500;
        }
        sprite.sprite = sprites[i];
    }

}
