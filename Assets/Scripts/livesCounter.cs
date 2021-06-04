using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class livesCounter : MonoBehaviour
{
    public int lives = 1; // this will be replaced with a get component from player script in future
    Text livesNum;
    // Start is called before the first frame update
    void Start()
    {
        livesNum = GetComponent<Text>(); // getting the text component to refrence in the update
    }

    // Update is called once per frame
    void Update()
    {
        livesNum.text = "lives: " + lives; // display text
        
    }
}
