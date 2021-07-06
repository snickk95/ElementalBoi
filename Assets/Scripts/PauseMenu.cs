using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject Pause; // use serialize field to make privbate but alsao visable in editor
  
    //function to set time scale to 0 so nothing moves well in pause menu
    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true; //stops the audio of the game as well
        Debug.Log("game paused");
        Pause.SetActive(true); // activate pause menu
    }

    // function to restart the timescale so we can resume play
    public void Resume()
    {
        Time.timeScale = 1;
        AudioListener.pause = false; //starts the game audio again
        Debug.Log("game not paused");
        Pause.SetActive(false); //disable pause menu
    }

    //function to quit the game
    public void CloseGame()
    {
        Application.Quit();
    }
}
