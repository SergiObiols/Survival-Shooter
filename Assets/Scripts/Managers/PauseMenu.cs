using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuUI;
    
    [SerializeField] public  bool GamePaused;


    void Update()
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        PauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
    }


}
