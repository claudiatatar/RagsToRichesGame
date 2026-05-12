using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemID;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.CollectItem(itemID);
            QuestUI.Instance.RefreshAll();
            Destroy(gameObject);
        }
    }
}
