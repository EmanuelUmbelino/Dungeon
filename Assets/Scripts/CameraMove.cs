using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private GameManager game;
    [SerializeField]
    private bool lateral;
    [SerializeField]
    private int value;
    private bool active;


    void Update()
    {
        if (active)
        {
            if (lateral)
                game.MoveX(value);
            else
                game.MoveY(value);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        active = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        active = false;
    }

}
