using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public static QuestUI Instance;

    [Header("Panels")]
    public GameObject questFullPanel;
    public GameObject questMiniHUD;

    [Header("Item rows (left list)")]
    public List<ItemRow> itemRows;

    [Header("Right panel references")]
    public Image detailIcon;
    public TextMeshProUGUI detailName;
    public TextMeshProUGUI detailDescription;
    public GameObject collectedBadge;
    public GameObject missingBadge;

    [Header("Mini HUD checkmarks")]
    public List<GameObject> miniCheckmarks;

    ItemRow currentSelected;
    bool fullPanelOpen = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        questFullPanel.SetActive(false);
        questMiniHUD.SetActive(true);

        if (itemRows.Count > 0)
            SelectRow(itemRows[0]);

        RefreshAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ToggleFullPanel();
    }

    void ToggleFullPanel()
    {
        fullPanelOpen = !fullPanelOpen;
        questFullPanel.SetActive(fullPanelOpen);
        questMiniHUD.SetActive(!fullPanelOpen);

        if (fullPanelOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public bool IsFullPanelOpen()
    {
        return fullPanelOpen;
    }

    public void SelectRow(ItemRow row)
    {
        if (currentSelected != null)
            currentSelected.SetSelected(false);

        currentSelected = row;
        row.SetSelected(true);

        bool collected = QuestManager.Instance.IsCollected(row.itemID);

        detailIcon.sprite = row.detailIcon;
        detailIcon.color = collected ? Color.white : new Color(1f, 1f, 1f, 0.45f);
        detailName.text = row.displayName;
        detailDescription.text = row.description;
        collectedBadge.SetActive(collected);
        missingBadge.SetActive(!collected);
    }

    public void RefreshAll()
    {
        for (int i = 0; i < miniCheckmarks.Count; i++)
            miniCheckmarks[i].SetActive(
                QuestManager.Instance.IsCollected(itemRows[i].itemID)
            );

        if (currentSelected != null)
            SelectRow(currentSelected);
    }
}