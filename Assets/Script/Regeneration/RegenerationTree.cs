using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class RegenerationTree : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private bool playerIsNear = false;
    private PlayerInputAction inputActions;
    private bool InteractionPress = false;

    private WorldStateManager _worldStateManager;
    private AudioManager _audioManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager, AudioManager audioManager)
    {
        _worldStateManager = worldStateManager;
        _audioManager = audioManager;
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();
    
    private void Awake()
    {
        inputActions = new PlayerInputAction();
        inputActions.Player.Interaction.performed += ctx => InteractionPress = true;
    }

    private void Start()
    {     
        GameObject girl = player.transform.GetChild(0).gameObject; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsNear = false;
    }

    private void Update()
    {
        InteractionPlayer();
    }

    private void InteractionPlayer()
    {
        if (InteractionPress && playerIsNear)
        {
            _audioManager.PlayTreeLoader();
            _worldStateManager.ResetWorld();
        }

        InteractionPress = false;
    }
}
