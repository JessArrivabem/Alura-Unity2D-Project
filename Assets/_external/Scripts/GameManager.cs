using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputManager InputManager { get; private set; }

    [Header("Dynamic Game Objects")]
    [SerializeField] private GameObject bossDoor;

    [Header("Managers")]
    public UIManager UIManager;
    public AudioManager AudioManager;

    private int totalKeys;
    private int keysLeftToCollect;


    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        totalKeys = FindObjectsOfType<CollectableKey>().Length;
        keysLeftToCollect = totalKeys;

        InputManager = new InputManager();
    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect--;
        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if(keysLeftToCollect <= 0)
        {
            Destroy(bossDoor);
        }
    }

}
