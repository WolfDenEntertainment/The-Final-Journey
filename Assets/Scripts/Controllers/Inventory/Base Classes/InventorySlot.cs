using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemCount;

    void Start()
    {
        if (itemIcon == null) itemIcon = transform.GetChild(2).GetComponent<Image>();
        if (itemCount == null) itemCount = transform.GetComponentInChildren<TextMeshProUGUI>();

        itemIcon.enabled = false;
        itemCount.enabled = false;
    }

    public void SetIcon(Sprite _icon)
    {
        if (_icon != null)
        {
            itemIcon.enabled = true;
            itemIcon.sprite = _icon;
        }
        else
        {
            itemIcon.enabled = false;
        }
    }

    public void SetCount(int count)
    {
        if (count > 2)
        {
            itemCount.enabled = true;
            itemCount.text = count.ToString();
        }
        else
        {
            itemCount.enabled = false;
        }

    }
}
