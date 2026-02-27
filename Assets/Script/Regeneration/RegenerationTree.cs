using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationTree : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private bool playerIsNear = false;
    private PlayerInputAction inputActions;
    private bool InteractionPress = false;

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
            WorldStateManager.Instance.ResetWorld();
        }

        InteractionPress = false;
    }
}
