using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputManager InputManager { get; private set; }

    [Header("Dynamic Game Objects")]
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private PlayerBehavior player;
    [SerializeField] private BossBehavior boss;
    [SerializeField] private BossFightTrigger bossFightTrigger;

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

        InputManager = new InputManager();

        totalKeys = FindObjectsOfType<CollectableKey>().Length;
        keysLeftToCollect = totalKeys;
        UIManager.UpdateKeysLeftText(totalKeys, keysLeftToCollect);

        bossFightTrigger.OnPlayerEnterBossFight += ActivateBossBehavior;

    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect--;
        UIManager.UpdateKeysLeftText(totalKeys, keysLeftToCollect);
        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if(keysLeftToCollect <= 0)
        {
            Destroy(bossDoor);
        }
    }

    private void ActivateBossBehavior()
    {
        boss.StartChasing();
    }

    public void UpdateLives(int amount)
    {
        UIManager.UpdateLivesText(amount);
    }

    public PlayerBehavior GetPlayer()
    {
        return player;
    }

}
