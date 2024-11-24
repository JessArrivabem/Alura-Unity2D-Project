using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int totalKeys = 8;
    private int keysLeftToCollect = 0;


    // Start is called before the first frame update
    private void Awake() 
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        InputManager = new InputManager();

        //totalKeys = FindObjectsOfType<CollectableKey>().Length;
        //keysLeftToCollect = totalKeys;
        UIManager.UpdateKeysLeftText(totalKeys, keysLeftToCollect);

        bossFightTrigger.OnPlayerEnterBossFight += ActivateBossBehavior;

        player.GetComponent<Health>().OnDead += HandleGameOver;
        boss.GetComponent<Health>().OnDead += HandleVictory;

    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect++;
        UIManager.UpdateKeysLeftText(totalKeys, keysLeftToCollect);
        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if(keysLeftToCollect >= 8)
        {
            Destroy(bossDoor);
        }
    }

    private void ActivateBossBehavior()
    {
        boss.StartChasing();
    }
    private void HandleGameOver()
    {
        UIManager.OpenGameOverPanel();
    }

    private void HandleVictory()
    {
        UIManager.ShowVictoryText();
        StartCoroutine(GoToCreditsScene());
    }

    private IEnumerator GoToCreditsScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
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
