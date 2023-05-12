using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botton : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindAnyObjectByType<Audio>().play("HorrorBg", false);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            FindAnyObjectByType<Audio>().play("Piano", false);
        }
    }

    public void quits()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
