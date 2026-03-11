using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Bonus : MonoBehaviour
{

    [SerializeField]
    private BonusType type;
    private bool isReadyToDespawn;

    private WorldStateManager _worldStateManager;
    private AudioManager _audioManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager, AudioManager audioManager)
    {
        _worldStateManager = worldStateManager;
        _audioManager = audioManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        { return; }

        switch (type) 
        {
            case BonusType.BonusLife:
                _audioManager.PlayHappyGetLife();
                _worldStateManager.AddLife(1);
                break;
            case BonusType.BonusMoney:
                _audioManager.PlayGetFlower();
                _worldStateManager.AddMoney(1);
                break;
            default:
                break;
        }


        isReadyToDespawn = true;
    }

    public bool IsReadyToDespawn() => isReadyToDespawn;
}
