using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    [SerializeField]
    private BonusType type;
    private bool isReadyToDespawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        { return; }

        switch (type) 
        {
            case BonusType.BonusLife:
                AudioManager.instance.PlayHappyGetLife();
                WorldStateManager.Instance.AddLife(1);
                break;
            case BonusType.BonusMoney:
                WorldStateManager.Instance.AddMoney(1);
                break;
            default:
                break;
        }


        isReadyToDespawn = true;
    }

    public bool IsReadyToDespawn() => isReadyToDespawn;
}
