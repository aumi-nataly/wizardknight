using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EmptyFloor : MonoBehaviour
{

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

        _audioManager.PlayTreeLoader();
        _worldStateManager.ResetWorld();
    }

    }
