using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Item item;

    public Item Item { get => item; }
}
