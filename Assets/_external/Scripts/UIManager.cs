using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject PausePanel;

    private void Awake()
    {
        OptionsPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    public void OpenClosePauseMenu()
    {
        if (PausePanel.activeSelf == false)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }
    }

    public void OpenOptionsPanel()
    {
        print("Set options to be opened");
        OptionsPanel.SetActive(true);
    }
}
