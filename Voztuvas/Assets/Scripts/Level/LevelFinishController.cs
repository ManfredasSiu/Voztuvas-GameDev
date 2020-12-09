using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Play some animation, sounds and load next level.
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Trying to load scene: " + (currentScene + 1) + ", scenes in total: " + sceneCount);
        if (currentScene < sceneCount)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            Debug.LogWarning("Trying to load a non-existing scene. (LevelFinishController)");
        }
        
    }
}
