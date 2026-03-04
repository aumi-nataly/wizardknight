using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class BootstrapLoader : MonoBehaviour
{


    void Start()
    {
        SceneManager.LoadScene("MainMenu");

    }


}
