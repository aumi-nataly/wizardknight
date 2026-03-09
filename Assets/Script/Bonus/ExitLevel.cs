using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    private WorldStateManager _worldStateManager;

    [Inject]
    public void Construct(AudioManager audio, LevelManager levelManager, WorldStateManager worldStateManager)
    {
        _audio = audio;
        _levelManager = levelManager;
        _worldStateManager = worldStateManager;
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

    private async void Update()
    {
        await InteractionPlayer();
    }

    private async Task InteractionPlayer()
    {
        if (InteractionPress && playerIsNear)
        {
            _audio.PlayChangeLevel();


            if (NameNextLvl == "MainMenu")
            {
                string levelName = _levelManager.GetCurrentLevel();
                int countMoney = _worldStateManager.GetCurrentMoney();
                int countLife = _worldStateManager.GetCurrentMaxHealth();
                var collected = _worldStateManager.GetDictionaryCollectedBonus();

                await SaveManager.SaveAsync(levelName, countMoney, countLife, collected);
            }  
            
            _levelManager.LoadNextLevel(NameNextLvl);
        }

        InteractionPress = false;
    }
}
