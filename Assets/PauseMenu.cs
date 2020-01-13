using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] public bool GamePaused;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePaused = !GamePaused;
        }

        if (GamePaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        PauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        GamePaused = false;
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
    }

}
