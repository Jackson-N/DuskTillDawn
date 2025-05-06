using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStateController : MonoBehaviour
{
    public BearScript bear;
    public HeatController heat;
    //public SceneMagager sceneManager;

    // Update is called once per frame
    void Update()
    {
        if(bear == null) return;
        if(heat == null) return;

        if (bear.gameOver == true)
        {
            //go to game over scene
            SceneManager.LoadScene("GameLose");
        }

        if (heat.gameOver == true)
        {
            SceneManager.LoadScene("GameLoseL2");
        }
    }
}
