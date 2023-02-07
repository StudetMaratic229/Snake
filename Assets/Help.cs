using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    private bool _isFreeze;
    
    public static event Action<bool> PauseClicked;

    public void FreezeTime()
    {
        _isFreeze = !_isFreeze;
        Time.timeScale = _isFreeze ? 0 : 1;
        PauseClicked?.Invoke(_isFreeze);
    }
    
    public void Button()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void Run()
    {
        Application.Quit();
    }
    public void Run1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/SampleScene");
    }
}
