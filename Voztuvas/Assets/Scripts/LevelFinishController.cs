using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Play some animation, sounds and load next level.
        int sceneCount = SceneManager.sceneCount;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
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
