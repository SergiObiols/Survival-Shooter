using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    public void MenuExit()
    {
        SceneManager.UnloadScene("miv1");
        SceneManager.LoadScene("Menu");
    }

}
