using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Image itemIcon;
    [SerializeField] int count;
    [SerializeField] TextMeshProUGUI itemCount;

    public Item Item { get => item; }
    public Image ItemIcon { get => itemIcon; }
    public int Count { get => count; }
    public TextMeshProUGUI ItemCount { get => itemCount; }

    void Start()
    {
        item = null;
        count = 0;

        if (itemIcon == null) itemIcon = transform.GetChild(2).GetComponent<Image>();
        if (itemCount == null) itemCount = transform.GetComponentInChildren<TextMeshProUGUI>();

        itemIcon.enabled = false;
        itemCount.enabled = false;
    }

    public void RefreshInventorySlot()
    {
        if (item != null)
        {
            SetIcon(item.ItemIcon);
            SetCount(count);

            return;
        }

        item = null;
        SetIcon(null);
        SetCount(0);
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

    public void SetCount(int cnt)
    {
        if (cnt >= 2)
        {
            itemCount.enabled = true;
            itemCount.text = cnt.ToString();
        }
        else
        {
            itemCount.enabled = false;
        }

    }

    public void AddItem(Item _item)
    {
        item = _item;
        count = 1;
    }

    public void RemoveItem(Item _item)
    {
        item = null;
        SetIcon(null);
        SetCount(0);
    }

    public void IncreaseCount()
    {
        count++;
    }
}
