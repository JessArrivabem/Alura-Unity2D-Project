using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitToMenuButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(ClosePauseMenu);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        quitToMenuButton.onClick.AddListener(QuitToMenuButton);
    }

    private void ClosePauseMenu()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        this.gameObject.SetActive(false);
    }
    private void OpenOptionsMenu()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        GameManager.Instance.UIManager.OpenOptionsPanel();
    }

    private void QuitToMenuButton()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        SceneManager.LoadScene("Menu");
    }
}
