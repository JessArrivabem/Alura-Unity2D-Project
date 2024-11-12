using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour
{
    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
