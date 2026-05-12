using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemRow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string itemID;
    public Image hoverHighlight;
    public Sprite detailIcon;
    public string displayName;
    [TextArea] public string description;

    bool isSelected = false;

    public void OnPointerEnter(PointerEventData e)
    {
        hoverHighlight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (!isSelected) hoverHighlight.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData e)
    {
        QuestUI.Instance.SelectRow(this);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        hoverHighlight.gameObject.SetActive(selected);
    }
}
