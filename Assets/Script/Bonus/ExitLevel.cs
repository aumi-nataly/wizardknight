using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class ExitLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private string NameNextLvl;

    private bool playerIsNear = false;
    private PlayerInputAction inputActions;
    private bool InteractionPress = false;

    private AudioManager _audio;
    private LevelManager _levelManager;

    [Inject]
    public void Construct(AudioManager audio, LevelManager levelManager)
    {
        _audio = audio;
        _levelManager = levelManager;
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
            _audio.PlayChangeLevel();
            _levelManager.LoadNextLevel(NameNextLvl);
        }

        InteractionPress = false;
    }
}
