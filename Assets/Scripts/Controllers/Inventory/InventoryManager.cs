using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Item[] inventory;
    [SerializeField] InventorySlot[] inventorySlots;

    static InventoryManager instance;

    public static InventoryManager Instance { get => instance; }
    public Item[] Inventory { get => inventory; }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;


        if (inventorySlots == null) 
            inventorySlots = GetComponentsInChildren<InventorySlot>();

        inventory = new Item[inventorySlots.Length];

        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = null;
        }

        RefreshInventory();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameObject.activeSelf) RefreshInventory();
    }

    void RefreshInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventory[i] != null)
            {
                inventorySlots[i].SetIcon(inventory[i].ItemIcon);
            }
            else
            {
                inventorySlots[i].SetIcon(null);
            }
        }
    }

    int GetEmptyIndex()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
                return i;
        }

        return -1;
    }

    int GetItemIndex(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
                return i;
        }

        return -1;
    }

    public bool AddItem(Item item)
    {
        int index = GetEmptyIndex();

        if (index >= 0)
        {
            inventory[index] = item;
            RefreshInventory();
            return true;
        }
        else
        {
            Debug.Log("Inventory full.");
            return false;
        }
    }

    public void RemoveItem(Item item)
    {
        int index = GetItemIndex(item);

        if (inventory[index] = item)
            inventory[index] = null;

        RefreshInventory();
    }

    public void RemoveItemAtIndex(int index)
    {
        if (inventory[index] != null)
            inventory[index] = null;

        RefreshInventory();
    }

    public Item GetItem(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
                return item;
        }
        return null;
    }
}
