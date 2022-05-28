using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] bool isInventoryOpen = false;
    [HideInInspector] public bool isGamepad = false;
    [HideInInspector] public bool escape = false;
    [HideInInspector] public bool isSprinting = false;

    PlayerInputActions actions;
    InteractController interact;

    void Awake()
    {
        actions = new PlayerInputActions();
        interact = GetComponent<InteractController>();

        actions.Player.Inventory.performed += _ => ControlInventory();
        actions.Player.Interact.performed += _ => interact.Raycast();

        actions.Player.Escape.started += _ => escape = true;
        actions.Player.Escape.canceled += _ => escape = false;

        actions.Player.Sprint.started += _ => isSprinting = true;
        actions.Player.Sprint.canceled += _ => isSprinting = false;

        if (inventory == null)
            inventory = InventoryManager.Instance.gameObject;

    }

    void OnEnable()
    {
        actions.Player.Enable();
    }

    void OnDisable()
    {
        actions.Player.Disable();
    }

    void Update()
    {
        isInventoryOpen = inventory.activeSelf;
    }

    void ControlInventory()
    {
        if (isInventoryOpen)
        {
            inventory.SetActive(false);
            isInventoryOpen = false;
        }
        else
        {
            inventory.SetActive(true);
            isInventoryOpen = true;
        }
    }

    public float GetVerticalMovement { get => actions.Player.Vertical.ReadValue<float>(); }
    public Vector2 GetMovement { get => actions.Player.Move.ReadValue<Vector2>(); }
    public Vector2 GetMouseDelta { get => actions.Player.Look.ReadValue<Vector2>(); }

    public void OnControlSchemeChanged(PlayerInput input)
    {
        isGamepad = input.currentControlScheme.Equals("Gamepad");
    }
}
