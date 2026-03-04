using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string CurrentLevel;

    private void Awake()
    {

        CurrentLevel = "Level_01";
        DontDestroyOnLoad(gameObject);

    }


    private void SetCurrentLevel(string name)
    {
        CurrentLevel = name;
    }

    public string GetCurrentLevel()
    {
        return CurrentLevel;
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
