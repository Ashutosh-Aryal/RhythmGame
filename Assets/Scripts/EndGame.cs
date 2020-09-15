using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private const KeyCode RESTART_KEY = KeyCode.R;
    private const KeyCode QUIT_KEY = KeyCode.Escape;

    [SerializeField] GameObject endingText;
    // Start is called before the first frame update
    void Start()
    {

        float percentage = ((float)(CollisionBehavior.currentScore) / (float)(CenterThing.MAX_POINTS)) * 100.0f; 
        endingText.GetComponent<Text>().text = "Your final percentage was: " + percentage + "%!";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(QUIT_KEY))
        {
            Application.Quit();
        } else if(Input.GetKeyDown(RESTART_KEY))
        {
            CollisionBehavior.currentScore = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
