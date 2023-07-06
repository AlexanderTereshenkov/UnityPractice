using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float maxMouseAngle;
    [SerializeField] private float minMouseAngle;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject handPos;
    [SerializeField] private LayerMask interactibleLayer;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 screenCenterPos = new Vector2(Screen.width / 2, Screen.height / 2);
    private Vector2 inputDirection;
    private float rotationX;
    private float maxRayDistance = 2f;
    private bool isMovingPossible;
    private Inventory inventory;
    private GameManager gameManager;
    private MoneyManager moneyManager;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventory = GetComponent<Inventory>();
        isMovingPossible = true;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        moneyManager = GetComponent<MoneyManager>();
    }


    private void Update()
    {
        if (isMovingPossible)
        {
            InputMovment();
            ApplyMovement();
            MouseMovement();
            InputAction();
        }
    }

    private void InputMovment()
    {
        inputDirection = new Vector2(Input.GetAxis("Vertical") * playerSpeed, Input.GetAxis("Horizontal") * playerSpeed);
        inputDirection.x = Mathf.Clamp(inputDirection.x, -1 * playerSpeed, playerSpeed);
        inputDirection.y = Mathf.Clamp(inputDirection.y, -1 * playerSpeed, playerSpeed);
        float moveDirY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * inputDirection.x) + (transform.TransformDirection(Vector3.right) * inputDirection.y);
        moveDirection.y = moveDirY;
    }

    private void MouseMovement()
    {
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, minMouseAngle, maxMouseAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
    }

    private void ApplyMovement()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= 9.8f * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void InputAction()
    {
        Ray ray = cam.ScreenPointToRay(screenCenterPos);
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {

                if (hit.transform.GetComponent<IPickable>() != null)
                {
                    if(hit.transform.GetComponent<PickFood>() != null)
                    {
                        if(hit.transform.GetComponent<PickFood>().GetCurrentInteractibleObject() != null)
                        {
                            hit.transform.GetComponent<PickFood>().GetCurrentInteractibleObject().GetComponent<IInteractible>().StopAction();
                        }
                    }
                    
                    if (inventory.GetIsSlotFull()[inventory.GetActiveSlot()] == false)
                    {
                        hit.transform.GetComponent<IPickable>().PutObjectIntoHand(handPos);
                    }
                    hit.transform.GetComponent<IPickable>().PutObjectIntoInventory(handPos);
                }

                if (hit.transform.GetComponent<FridjeInteraction>() != null)
                {
                    hit.transform.GetComponent<FridjeInteraction>().StartAction();
                }
                if(hit.transform.TryGetComponent<PickPlate>(out PickPlate plate))
                {
                    plate.GetPlate();
                }
                if(hit.transform.TryGetComponent<GiveOrder>(out GiveOrder giveOrder))
                {
                    giveOrder.GiveOrderToPlayer(Time.time);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxRayDistance, interactibleLayer))
            {
                if (inventory.GetAllObjects()[inventory.GetActiveSlot()] != null)
                {
                    if (inventory.GetAllObjects()[inventory.GetActiveSlot()].TryGetComponent<PickFood>(out PickFood pickFood))
                    {
                        GameObject currnetGameObject = inventory.GetAllObjects()[inventory.GetActiveSlot()];
                        if (hit.transform.TryGetComponent<IInteractible>(out IInteractible interactible))
                        {
                            if (!interactible.IsObjectFull() && interactible.IsPossibleToInteract(currnetGameObject))
                            {
                                currnetGameObject.transform.SetParent(null);
                                interactible.StartAction(currnetGameObject);
                                inventory.RemoveFromInventory(inventory.GetActiveSlot());
                            }
                        }
                        if (hit.transform.TryGetComponent<DishManager>(out DishManager dishManager))
                        {
                            inventory.GetAllObjects()[inventory.GetActiveSlot()].transform.SetParent(null);
                            dishManager.PutFoodInPlate(currnetGameObject);
                            inventory.RemoveFromInventory(inventory.GetActiveSlot());
                        }
                        if (hit.transform.TryGetComponent<TrashCan>(out TrashCan trashCan))
                        {
                            trashCan.DestroyGameObject(currnetGameObject);
                        }
                    }
                    else if (inventory.GetAllObjects()[inventory.GetActiveSlot()].TryGetComponent<PickObject>(out PickObject pickObject))
                    {
                        GameObject currnetGameObject = inventory.GetAllObjects()[inventory.GetActiveSlot()];
                        if (hit.transform.TryGetComponent<IInteractible>(out IInteractible interactible))
                        {
                            if (interactible.IsObjectFull())
                            {
                                interactible.MakeAction(pickObject.name);
                            }
                        }
                        if (hit.transform.TryGetComponent<TrashCan>(out TrashCan trashCan))
                        {
                            trashCan.DestroyGameObject(currnetGameObject);
                        }
                        if(currnetGameObject.TryGetComponent<Order>(out Order order))
                        {
                            if(hit.transform.CompareTag("Ready dish") && hit.transform.GetComponent<PickObject>().GetObjectName() == order.GetCurrentRecipie())
                            {
                                moneyManager.ChangeMoneyValue(hit.transform.GetComponent<ReadyDishManager>().GetPrice());
                                Destroy(hit.transform.gameObject);
                                currnetGameObject.transform.SetParent(null);
                                inventory.RemoveFromInventory(inventory.GetActiveSlot());
                                Destroy(currnetGameObject);
                                gameManager.SetActiveCheques(-1);
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && inventory.GetIsSlotFull()[inventory.GetActiveSlot()] != false)
        {
            int position = inventory.GetActiveSlot();
            inventory.GetAllObjects()[position].GetComponent<IPickable>().DropObject(playerCamera.transform.forward,
                characterController.velocity, position);
        }

    }

    public void SetIsMovingPossible(bool value)
    {
        isMovingPossible = value;
    }

    public bool GetIsMovingPossible()
    {
        return isMovingPossible;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
