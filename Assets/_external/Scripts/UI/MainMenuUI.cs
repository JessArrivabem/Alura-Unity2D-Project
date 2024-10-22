using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;

    [Header("Menu UI Properties")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void GoToGameplayScene()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        SceneManager.LoadScene("FirstLevel");
    }

    private void OpenOptionsMenu()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        optionsPanel.SetActive(true);
    }
    private void ExitGame()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif
    }

}
