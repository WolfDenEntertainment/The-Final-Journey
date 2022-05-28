using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerControls))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float sprintSpeed = 3;
    [SerializeField] float maxSpeed = 3;

    // Constant fields
    const string doorTag = "Door";

    // Private fields
    Transform mainCamera;
    Rigidbody rb;
    PlayerControls input;
    Door door;
    Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerControls>();
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (input.escape)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void FixedUpdate()
    {
        PhysicsMove();
    }

    void PhysicsMove()
    {
        float max = input.isSprinting ? maxSpeed * 3: maxSpeed;
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = Vector3.ClampMagnitude(rb.velocity, max);

        if (input.GetVerticalMovement == 0)
            velocity.y = 0;

        Vector2 movement = input.GetMovement;
        Vector3 move = new(movement.x, 0f, movement.y) ;
        move = (mainCamera.right * move.x) + (mainCamera.forward * move.z);
        move.y = 0;
        velocity.y = input.GetVerticalMovement;
        move += velocity;

        float speed = input.isSprinting ? sprintSpeed : movementSpeed;

        rb.AddForce(move * speed, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(doorTag))
        {
            other.GetComponent<Door>().OpenDoor();
        }

        if (other.gameObject.CompareTag("Item"))
        {
            InventoryManager.Instance.AddItem(other.GetComponent<ItemObject>().Item);
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(doorTag))
        {
            other.GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }
}
