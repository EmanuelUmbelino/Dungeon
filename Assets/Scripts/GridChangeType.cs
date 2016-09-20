using UnityEngine;
using System.Collections;

public class GridChangeType : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private GameObject enemy;
    private int[] pos;
    int i = 0;
    void Start()
    {
        string[] test = this.name.Split('/'); pos = new int[2];
        pos[0] = int.Parse(test[0]); pos[1] = int.Parse(test[1]);
    }
    public int getX()
    {
        return pos[0];
    }
    public int getY()
    {
        return pos[1];
    }

    void OnMouseDown()
    {
        if(!GameManager.target && !GameManager.enemy)
        {
            i++;
            sprite.color = Color.white;
            if (i > 2)
            {
                i = 0;
                this.gameObject.tag = "Grid";
            }
            else if (i.Equals(1))
            {
                this.gameObject.tag = "Water";
            }
            else if (i.Equals(2))
            {
                this.gameObject.tag = "Wall";
            }
            sprite.sprite = sprites[i];
        }
        else if (!GameManager.target)
        {
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            GameManager.enemy = false;
        }
        else
        {
            i = 2;
            this.gameObject.tag = "Target";
            sprite.sprite = sprites[0];
            sprite.color = Color.red;
            GameManager.target = false;
        }
    }

}
