using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")]
    public UIManager UIManager;

    public AudioManager AudioManager;
    public InputManager InputManager { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        InputManager = new InputManager();
    }

}
