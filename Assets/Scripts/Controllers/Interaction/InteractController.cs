using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractController : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float pushForce = 3;
    [SerializeField] float interactionDistance = 5;

    [Header("End Game")]
    [SerializeField] Item requiredItem;
    [SerializeField] float requiredCount;
    [SerializeField] Image decisionEffect;
    [SerializeField] Sprite ascension;
    [SerializeField] Sprite descension;
    [SerializeField] AudioClip ascensionSound;
    [SerializeField] AudioClip descensionSound;
    [SerializeField] AudioSource playerAudio;

    GameObject reticle;
    Transform mainCamera;
    Inventory inventory;
    float percentage;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Second);
        mainCamera = Camera.main.transform;
        inventory = Inventory.Instance;
        inventory.gameObject.SetActive(false);

        reticle = GameObject.Find("Reticle");
        percentage = Random.Range(40, 75);
    }

    public void Raycast()
    {
        Ray ray = new(transform.position, mainCamera.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, interactionDistance, interactionLayer, QueryTriggerInteraction.Collide))
        {
            Vector3 direction = hitInfo.collider.transform.position - transform.position;

            Interact(hitInfo.collider.gameObject, direction);
        }
    }

    void Push(Rigidbody rb, Vector3 fwd)
    {
        rb.AddForce(pushForce * fwd, ForceMode.VelocityChange);
    }

    void Interact(GameObject go, Vector3 fwd)
    {        
        Animator anim;

        Debug.Log("Item:  " + go.name);

        if (go.CompareTag("Item"))
        {
            Item _item = go.GetComponent<ItemObject>().Item;

            Debug.Log(_item.ItemName);
            if (Inventory.Instance.AddItem(_item))
                Destroy(go);
        }

        if (go.CompareTag("Grimoire"))
        {
            int index = Inventory.Instance.GetItemIndex(requiredItem);
            
            if (index != -1)
            {
                if (Inventory.Instance.Slots[index].Count == requiredCount)
                    Ascend();
            }
        }

        if (go.CompareTag("Interactable Object"))
        {
            Push(go.GetComponent<Rigidbody>(), fwd);
        }

        if (go.CompareTag("Cabinet"))
        {
            if (go.name.Equals("Cabinet Door 01"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null) 
                    anim.SetBool("Cabinet Door 01", !anim.GetBool("Cabinet Door 01"));
            }    
            else if (go.name.Equals("Cabinet Door 02"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null) 
                    anim.SetBool("Cabinet Door 02", !anim.GetBool("Cabinet Door 02"));
            }
        }

        if (go.CompareTag("Drawer"))
        {
            if (go.name.Equals("Drawer 01"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 01", !anim.GetBool("Drawer 01"));
            }
            else if (go.name.Equals("Drawer 02"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 02", !anim.GetBool("Drawer 02"));
            }
            else if (go.name.Equals("Drawer 03"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 03", !anim.GetBool("Drawer 03"));
            }
            else if (go.name.Equals("Drawer 04"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 04", !anim.GetBool("Drawer 04"));
            }
            else if (go.name.Equals("Drawer 05"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 05", !anim.GetBool("Drawer 05"));
            }
            else if (go.name.Equals("Drawer 06"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 06", !anim.GetBool("Drawer 06"));
            }
            else if (go.name.Equals("Drawer 07"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 01", !anim.GetBool("Drawer 01"));
            }
            else if (go.name.Equals("Drawer 08"))
            {
                go.transform.parent.parent.TryGetComponent<Animator>(out anim);

                if (anim != null)
                    anim.SetBool("Drawer 01", !anim.GetBool("Drawer 08"));
            }
        }
    }

    public void Ascend()
    {
        reticle.SetActive(false);
        Inventory.Instance.Readout.gameObject.SetActive(false);
        float decision = Random.Range(0, 100);

        playerAudio.Stop();
        if (decision < percentage)
        {
            Debug.Log("Welcome To The Inferno!");
            decisionEffect.sprite = descension;
            decisionEffect.color = new Color(1, 1, 1, 1);
            StartCoroutine(Descension());
        }
        else
        {
            Debug.Log("Welcome To Paradise.");
            decisionEffect.sprite = ascension;
            decisionEffect.color = new Color(1,1,1,1);
            StartCoroutine(Ascension());
        }

    }

    IEnumerator Ascension()
    {
        playerAudio.PlayOneShot(ascensionSound);

        yield return new WaitForSeconds(ascensionSound.length);
        SceneManager.LoadScene(0);
    }

    IEnumerator Descension()
    {
        playerAudio.PlayOneShot(descensionSound);

        yield return new WaitForSeconds(descensionSound.length);
        SceneManager.LoadScene(0);
    }
}
