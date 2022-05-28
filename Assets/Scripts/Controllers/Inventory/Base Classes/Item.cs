using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] GameObject itemObject;
    [SerializeField] Sprite itemIcon;
    [SerializeField] ItemType type;
    [SerializeField] bool canStack;
    [SerializeField] int itemCount;

    public Item()
    {
        itemName = "New Item";
        itemObject = null;
        itemIcon = null;
        type = ItemType.RitualResource;
        canStack = false;
        itemCount = 0;
    }

    public string ItemName { get => itemName; }
    public GameObject ItemObject { get => itemObject; }
    public Sprite ItemIcon { get => itemIcon; }
    public ItemType Type { get => type; set => type = value; }
    public bool CanStack { get => canStack; }
    public int ItemCount { get => itemCount; }
}

public enum ItemType
{
    Ambience,
    RitualResource,
    RitualTool,
    Key
}
