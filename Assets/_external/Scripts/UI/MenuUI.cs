using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("Menu UI Properties")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(GoToGameplayScene);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void GoToGameplayScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
       UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif
    }

}
