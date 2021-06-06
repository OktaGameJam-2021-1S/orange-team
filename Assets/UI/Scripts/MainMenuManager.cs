using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Text volumeText;

    private void Start()
    {
        PlayerPrefs.SetFloat("Volume", 5f);
        string raw = PlayerPrefs.GetFloat("Volume").ToString();
        volumeText.text = raw.Trim(new char[] { '.', ',' });
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetVolume(bool plus)
    {
        if (plus)
        {
            PlayerPrefs.SetFloat("Volume", PlayerPrefs.GetFloat("Volume") + 1f);
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", PlayerPrefs.GetFloat("Volume") - 1f); 
        }
        string raw = PlayerPrefs.GetFloat("Volume").ToString();

        volumeText.text = raw.Trim(new char[] { '.', ',' });
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
