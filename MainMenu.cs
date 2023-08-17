using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// links for main menu (play and quit)
public class MainMenu : MonoBehaviour {
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame() {
        SceneManager.LoadScene("scene1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}

