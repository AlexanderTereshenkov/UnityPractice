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
    [SerializeField] private LayerMask ignoreLayer;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 screenCenterPos = new Vector2(Screen.width / 2, Screen.height / 2);
    private Vector2 inputDirection;
    private float rotationX;
    private float maxRayDistance = 2f;
    private bool isMovingPossible;
    private Inventory inventory;
    private PlayerUI playerUI;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventory = GetComponent<Inventory>();
        playerUI = GetComponent<PlayerUI>();
        isMovingPossible = true;
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
                     //�������� ����� ������� ���� ����������� �������, ������� ����� ����������, ����� ����� �������� ���� �������
                     /*
                    if (inventory.GetIsSlotFull()[inventory.GetActiveSlot()] == false)
                    {
                        newPlate.GetComponent<IPickable>().PutObjectIntoHand(handPos);
                    }
                    newPlate.GetComponent<IPickable>().PutObjectIntoInventory(handPos);
                     */
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxRayDistance, ignoreLayer))
            {
                if(hit.transform.GetComponent<IInteractible>() != null && !hit.transform.GetComponent<IInteractible>().IsObjectFull())
                {
                    GameObject currentObject = inventory.GetAllObjects()[inventory.GetActiveSlot()];
                    if (currentObject != null && currentObject.GetComponent<PickFood>() != null)
                    {
                        if (hit.transform.GetComponent<IInteractible>().IsPossibleToInteract(currentObject))
                        {
                            currentObject.transform.SetParent(null);
                            hit.transform.GetComponent<IInteractible>().StartAction(currentObject);
                            inventory.RemoveFromInventory(inventory.GetActiveSlot());
                        }
                    }
                }
                if(inventory.GetAllObjects()[inventory.GetActiveSlot()] != null)
                {
                    if (inventory.GetAllObjects()[inventory.GetActiveSlot()].GetComponent<PickObject>() != null)
                    {
                        if (hit.transform.GetComponent<IInteractible>() != null && hit.transform.GetComponent<IInteractible>().IsObjectFull())
                        {
                            hit.transform.GetComponent<IInteractible>().MakeAction(inventory.GetAllObjects()[inventory.GetActiveSlot()].GetComponent<PickObject>().GetObjectName());
                        }
                    }
                    if(inventory.GetAllObjects()[inventory.GetActiveSlot()].GetComponent<PickFood>() != null)
                    {
                        if(hit.transform.GetComponent<DishManager>() != null)
                        {
                            inventory.GetAllObjects()[inventory.GetActiveSlot()].transform.SetParent(null);
                            hit.transform.GetComponent<DishManager>().PutFoodInPlate(inventory.GetAllObjects()[inventory.GetActiveSlot()]);
                            inventory.RemoveFromInventory(inventory.GetActiveSlot());
                        }

                    }
                    if (hit.transform.GetComponent<TrashCan>() != null)
                    {
                        hit.transform.GetComponent<TrashCan>().DestroyGameObject(inventory.GetAllObjects()[inventory.GetActiveSlot()]);
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
