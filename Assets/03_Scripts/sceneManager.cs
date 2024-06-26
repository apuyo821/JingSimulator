using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public static sceneManager scenemanager;

    public void GoToGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void GoToStart_option_menu()
    {
        SceneManager.LoadScene("Start_option_menu");
    }

    public void GoToauditionScene()
    {
        SceneManager.LoadScene("auditionScene");
    }

    public void GoToScheduleProcess()
    {
        SceneManager.LoadScene("ScheduleProcess");
    }
}
