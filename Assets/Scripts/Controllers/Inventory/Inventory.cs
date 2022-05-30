using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventorySlot[] inventorySlots;

    static Inventory instance;

    public static Inventory Instance { get => instance;}
    public InventorySlot[] Slots { get => inventorySlots; }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        if (inventorySlots == null)
            inventorySlots = GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < inventorySlots.Length; i++)
            inventorySlots[i].RefreshInventorySlot();
    }
    
    // Public methods
    public bool AddItem(Item item)
    {
        int index = GetItemIndex(item);

        if (index == -1) 
            index = GetEmptyIndex();

        if (index == -1)
        {
            Debug.Log("Inventory full.");
            return false;
        }

        // If there is an index available, and the item can be stacked
        if (index >= 0 && item.CanStack)
        {
            Debug.Log(item.ItemName + " " + (item.CanStack ? "can stack" : "can't be stacked."));
            // If the item exists in the inventory, increase the count.
            if (inventorySlots[index].Item == item)
            {
                Debug.Log("Increasing item count of " + item.ItemName);
                inventorySlots[index].IncreaseCount();

                RefreshInventory();
                return true;
            }

            else if (inventorySlots[index].Item == null)
            {
                Debug.Log("Can stack, but not found.  Adding item:  " + item.ItemName);
                inventorySlots[index].AddItem(item);

                RefreshInventory();
                return true;
            }

        }
        // Item can't be stacked, add item
        else if (index >= 0)
        {
            Debug.Log("Adding item:  " + item.ItemName);
            inventorySlots[index].AddItem(item);

            RefreshInventory();
            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        int index = GetItemIndex(item);

        if (inventorySlots[index].Item == item)
            inventorySlots[index].RemoveItem(item);

        RefreshInventory();
    }

    public void RemoveItemAtIndex(int index)
    {
        if (inventorySlots[index].Item != null)
            inventorySlots[index].RemoveItem(inventorySlots[index].Item);

        RefreshInventory();
    }

    public Item GetItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].Item == item)
                return item;
        }
        return null;
    }

    public bool DoesContainItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].Item == item)
                return true;
        }
        return false;
    }

    public Item GetItemFromIndex(int index)
    {
        return inventorySlots[index].Item;
    }

    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].Item == item)
                return i;
        }

        return -1;
    }

    // Private methods
    int GetEmptyIndex()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].Item == null)
                return i;
        }

        return -1;
    }

    void RefreshInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].RefreshInventorySlot();
        }
    }
}
