using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.KeyCollect);
        GameManager.Instance.UpdateKeysLeft();
        Destroy(this.gameObject);
    }
}
