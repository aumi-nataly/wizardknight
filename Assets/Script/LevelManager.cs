using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private string CurrentLevel;

    private void Awake()
    {
        if (instance == null)
        {
            CurrentLevel = "Level_01";
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    private void SetCurrentLevel(string name)
    {
        CurrentLevel = name;
    }

    public void LoadNextLevel(string name)
    {
        SetCurrentLevel(name);
        SceneManager.LoadScene(name);
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }
}
