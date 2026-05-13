using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [System.Serializable]
    public class QuestItem
    {
        public string itemID;
        public bool isCollected;
    }

    public List<QuestItem> requiredItems = new List<QuestItem>();
    public UnityEvent onAllItemsCollected;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CollectItem(string itemID)
    {
        QuestItem item = requiredItems.Find(i => i.itemID == itemID);
        if (item != null && !item.isCollected)
        {
            item.isCollected = true;
            CheckAllCollected();
        }
    }

    void CheckAllCollected()
    {
        bool allDone = requiredItems.TrueForAll(i => i.isCollected);
        if (allDone) onAllItemsCollected?.Invoke();
    }

    public bool IsCollected(string itemID)
    {
        QuestItem item = requiredItems.Find(i => i.itemID == itemID);
        return item != null && item.isCollected;
    }
}
