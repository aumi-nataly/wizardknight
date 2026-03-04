using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
      //  AudioManager.instance.PlayMenuClick();
        LevelManager.instance.LoadNextLevel("Level_01");
    }

    public async void OnLoadPressed()
    {
      //  AudioManager.instance.PlayMenuClick();
        try
        {
            var data = await SaveManager.LoadAsync();

            if (data == null) 
            {
                return;
            }
            LevelManager.instance.LoadNextLevel(data.LevelName);

        }
        catch (Exception ex) 
        {
            Debug.LogError(ex);
        }

    }

    public void OnInstructionPressed()
    {
      //  AudioManager.instance.PlayMenuClick();
        Menu.SetActive(false);
        InstructionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(instructionBackButton);
    }

    public void OnExitPressed()
    {
      //  AudioManager.instance.PlayMenuClick();
        Application.Quit();
    }

    public void OnInstructionBackPressed()
    {
      //  AudioManager.instance.PlayMenuClick();
        Menu.SetActive(true);
        InstructionMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstDelectedButton);
    }

}
