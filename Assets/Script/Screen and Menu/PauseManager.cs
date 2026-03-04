using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using VContainer;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject firstDelectedButton;

    private PlayerInputAction input;

    private AudioManager _audio;
    private LevelManager _levelManager;

    [Inject]
    public void Construct(AudioManager audio, LevelManager levelManager)
    {
        _audio = audio;
        _levelManager = levelManager;
    }
    private void Awake()
    {
        input = new PlayerInputAction();
        input.Player.Pause.performed += ctx => Pause();
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    public void Pause()
    {
        IsPaused = !IsPaused;
        pauseMenu.SetActive(IsPaused);
        Time.timeScale =IsPaused ? 0f : 1f;

        if (IsPaused)
        {
            EventSystem.current.SetSelectedGameObject(firstDelectedButton);
        }
        else 
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void Resume()
    {
        Pause();
    }

    public void OnResumePressed()
    {
        _audio.PlayMenuClick();
        Resume();
    }


    public async void OnMainMenuPressed()
    {
        string levelName = _levelManager.GetCurrentLevel();
        int countMoney = WorldStateManager.Instance.GetCurrentMoney();
        int countLife = WorldStateManager.Instance.GetCurrentMaxHealth();
        var collected = WorldStateManager.Instance.GetDictionaryCollectedBonus();

        await SaveManager.SaveAsync(levelName, countMoney, countLife, collected);

         _audio.PlayMenuClick();
        SceneManager.LoadScene("MainMenu");
    }

}
