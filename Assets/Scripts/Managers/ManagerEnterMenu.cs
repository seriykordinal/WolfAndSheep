using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerEnterMenu : MonoBehaviour
{
    public void buttonStart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void buttonExit()
    {
        Application.Quit();
    }
}
