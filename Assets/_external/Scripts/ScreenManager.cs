using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void sceneToMoveTo()
    {
        SceneManager.LoadScene("SecondLevel");
    }

    public void sceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SceneRestart")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
