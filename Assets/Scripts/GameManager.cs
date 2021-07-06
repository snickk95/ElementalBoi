using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    
    //function to set time scale to 0 so nothing moves well in pause menu
    void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true; //stops the audio of the game as well
        Debug.Log("game paused");
    }

    // function to restart the timescale so we can resume play
    void Resume()
    {
        Time.timeScale = 1;
        AudioListener.pause = false; //starts the game audio again
        Debug.Log("game not paused");
    }
}
