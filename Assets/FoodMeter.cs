using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMeter : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    public int Count;

    [SerializeField] int FoodLossCondition;
    [SerializeField] GameObject LossIndicator;

    private void OnTriggerEnter(Collider other)
    {
        Count++;
        Check();
    }

    private void OnTriggerExit(Collider other)
    {
        Count--;
        Check();
    }

    void Check()
    {
        if (Count >= FoodLossCondition)
        {
            turnManager.Disable();
            LossIndicator.SetActive(true);
            LossIndicator.transform.position = transform.position;
        }
    }
}

