using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(nextIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextIndex = 0;
            }

            SceneManager.LoadScene(nextIndex);
        }
    }
}
