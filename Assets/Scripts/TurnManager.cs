using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public bool BlueTurn;

    [SerializeField] ProjectileSelector BluePicker;
    [SerializeField] ProjectileSelector RedPicker;

    // Start is called before the first frame update
    void Start()
    {
        BlueTurn = Random.value < 0.5f;
        TurnInit();
    }

    public void SwapTurn()
    {
        BlueTurn = !BlueTurn;
        TurnInit();
    }

    public void Disable()
    {
        BluePicker.SetWindowState(false);
        RedPicker.SetWindowState(false);
    }

    void TurnInit()
    {
        if (BlueTurn)
        {
            BluePicker.SetWindowState(true);
            RedPicker.SetWindowState(false);
        }else
        {
            BluePicker.SetWindowState(false);
            RedPicker.SetWindowState(true);
        }
    }
}
