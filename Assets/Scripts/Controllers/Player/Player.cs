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
    Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerControls>();
        mainCamera = Camera.main.transform;
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

        velocity.y = input.GetVerticalMovement;
        move = (mainCamera.right * move.x) + (mainCamera.forward * move.z);
        move.y = 0;
        move += velocity;

        float speed = input.isSprinting ? sprintSpeed : movementSpeed;

        rb.AddForce(move * speed, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(doorTag))
        {
            if (other.gameObject.TryGetComponent<Door>(out Door door))
                door.OpenDoor();
            else
                Debug.Log("No door component.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(doorTag))
        {
            Animator anim = other.GetComponent<Animator>();
            
            if (anim != null) anim.SetBool("IsOpen", false);
        }
    }
}
