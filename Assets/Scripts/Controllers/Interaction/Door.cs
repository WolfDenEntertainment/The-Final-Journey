using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Item doorKey;
    [SerializeField] bool isLocked = false;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (isLocked && doorKey == null)
            Debug.LogWarning("No key set for " + gameObject.name + ".");
    }

    public void OpenDoor()
    {
        if (isLocked)
        {
            if (InventoryManager.Instance.GetItem(doorKey) != null)
            {
                isLocked = false;
                InventoryManager.Instance.RemoveItem(doorKey);
            }
            else
            {
                Debug.Log("You do not have the required key.");
            }
        }

        if (!isLocked)
            anim.SetBool("IsOpen", true);
    }
}
