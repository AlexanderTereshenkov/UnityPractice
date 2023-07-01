using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] private bool[] isSlotFull;
    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject activeSlot;
    private GameObject[] objectsInInventory;
    private int slotPosition = 0;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        activeSlot.transform.position = slots[slotPosition].transform.position;
        objectsInInventory = new GameObject[slots.Length];
    }

    private void Update() { 

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 && player.GetComponent<PlayerController>().GetIsMovingPossible())
        {
            if(slotPosition+1 < slots.Length)
            {
                slotPosition++;
            }
            else
            {
                slotPosition = 0;
            }
            activeSlot.transform.position = slots[slotPosition].transform.position;
            if(slotPosition == 0)
            {
                if(objectsInInventory[objectsInInventory.Length - 1] != null) objectsInInventory[objectsInInventory.Length - 1].SetActive(false);
                if(objectsInInventory[slotPosition] != null) objectsInInventory[slotPosition].SetActive(true);
            }
            else
            {
                if(objectsInInventory[slotPosition - 1] != null) objectsInInventory[slotPosition - 1].SetActive(false);
                if(objectsInInventory[slotPosition] != null) objectsInInventory[slotPosition].SetActive(true);
            }

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && player.GetComponent<PlayerController>().GetIsMovingPossible())
        {
            if (slotPosition - 1 >= 0)
            {
                slotPosition--;
            }
            else
            {
                slotPosition = slots.Length - 1;
            }
            activeSlot.transform.position = slots[slotPosition].transform.position;
            if (slotPosition == objectsInInventory.Length - 1)
            {
                if (objectsInInventory[0] != null) objectsInInventory[0].SetActive(false);
                if (objectsInInventory[slotPosition] != null) objectsInInventory[slotPosition].SetActive(true);
            }
            else
            {
                if (objectsInInventory[slotPosition + 1] != null) objectsInInventory[slotPosition + 1].SetActive(false);
                if (objectsInInventory[slotPosition] != null) objectsInInventory[slotPosition].SetActive(true);
            }
        }
    }

    public bool[] GetIsSlotFull()
    {
        return isSlotFull;
    }


    public GameObject[] GetSlotsPositions()
    {
        return slots;
    }

    public GameObject[] GetAllObjects()
    {
        return objectsInInventory;
    }

    public void SetIsSlotFull(int pos, bool value)
    {
        isSlotFull[pos] = value;
    }
    public void SetGameObjectInInventory(int pos, GameObject value)
    {
        objectsInInventory[pos] = value;
    }

    public void RemoveFromInventory(int pos)
    {
        slots[pos].gameObject.GetComponent<Image>().sprite = null;
        objectsInInventory[pos] = null;
        isSlotFull[pos] = false;
        /*
        for(int i = 0; i < objectsInInventory.Length; i++)
        {
            Debug.Log(isSlotFull[i]);
        }
        */
    }

    public int GetActiveSlot()
    {
        return slotPosition;
    }

    public void PutImageIntoSlot(int pos, GameObject thisObject)
    {
        slots[pos].gameObject.GetComponent<Image>().sprite = thisObject.GetComponent<IPickable>().GetSourceImg();
    }


}
