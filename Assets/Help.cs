using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Help : MonoBehaviour
{
    [SerializeField] private TMP_Text _argon;
    [SerializeField] private TMP_Text _argon1;
    [SerializeField] private int intToSave;
    [SerializeField] private int intToSave1;
    private bool _isFreeze;
    public static event Action<bool> PauseClicked;

#if UNITY_EDITOR
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
#endif
    
    private void Start()
    {
        loadrecord();
        yourschet();
    }

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
    public void loadrecord()
    {
        if (PlayerPrefs.HasKey("Saved"))
        {
            intToSave = PlayerPrefs.GetInt("Saved");
        }
        string s1 = intToSave.ToString();
        _argon.text = s1;
    }

    public void yourschet()
    {
        if (PlayerPrefs.HasKey("Saveda"))
        {
            intToSave1 = PlayerPrefs.GetInt("Saveda");
        }
        string s2 = intToSave1.ToString();
        _argon1.text = s2;
    }
}
