using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject Menu;

    [SerializeField]
    private GameObject firstDelectedButton;

    [SerializeField]
    private GameObject InstructionMenu;

    [SerializeField]
    private GameObject instructionBackButton;


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

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstDelectedButton);
    }

    public async void OnNewGamePressed()
    {
        await SaveManager.ResetSaveToDefaultsAsync();
        await _worldStateManager.RestartNewGame();
        _audio.PlayMenuClick();
        _levelManager.LoadNextLevel("Level_01");
    }

    public async void OnLoadPressed()
    {
          _audio.PlayMenuClick();

        try
        {
            var data = await SaveManager.LoadAsync();

            if (data == null) 
            {
                return;
            }
            _levelManager.LoadNextLevel(data.LevelName);

        }
        catch (Exception ex) 
        {
            Debug.LogError(ex);
        }

    }

    public void OnInstructionPressed()
    {
        _audio.PlayMenuClick();
        Menu.SetActive(false);
        InstructionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(instructionBackButton);
    }

    public void OnExitPressed()
    {
        _audio.PlayMenuClick();
        Application.Quit();
    }

    public void OnInstructionBackPressed()
    {
        _audio.PlayMenuClick();
        Menu.SetActive(true);
        InstructionMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstDelectedButton);
    }

}
