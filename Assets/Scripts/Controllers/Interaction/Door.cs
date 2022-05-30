using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Item doorKey;
    [SerializeField] bool isLocked = false;


    private void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
        anim.Rebind();

        if (isLocked && doorKey == null)
            Debug.LogWarning("No key set for " + gameObject.name + ".");
    }

    public void OpenDoor()
    {
        if (isLocked)
        {
            bool hasKey = Inventory.Instance.DoesContainItem(doorKey);
            if (hasKey)
            {
                isLocked = false;
                Inventory.Instance.RemoveItem(doorKey);
            }
            else
            {
                Debug.Log("You do not have the required key.");
            }
        }

        if (!isLocked)
        {
            anim.SetBool("IsOpen", true);
        }
    }
}
