using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void Update()
    {
         if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Juego");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Menú");
        }

    }
}


