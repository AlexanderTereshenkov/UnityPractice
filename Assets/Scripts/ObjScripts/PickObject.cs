using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour, IPickable
{
    [SerializeField] private Sprite objSprite;
    [SerializeField] private string objectName;
    private Rigidbody rigidBody;
    private GameObject player;
    private Inventory inventory;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }

    public void PutObjectIntoInventory(GameObject hand)
    {
        if (inventory.GetIsSlotFull()[inventory.GetActiveSlot()] == false)
        {
            int pos = inventory.GetActiveSlot();

            inventory.SetGameObjectInInventory(pos, this.gameObject);
            inventory.PutImageIntoSlot(pos, this.gameObject);
            inventory.SetIsSlotFull(pos, true);
            //Instantiate(sourceImg, inventory.GetSlotsPositions()[pos].transform);

        }
        else
        {
            for (int i = 0; i < inventory.GetIsSlotFull().Length; i++)
            {
                if (inventory.GetIsSlotFull()[i] == false)
                {
                    inventory.SetIsSlotFull(i, true);
                    inventory.PutImageIntoSlot(i, this.gameObject);
                    inventory.SetGameObjectInInventory(i, this.gameObject);
                    gameObject.SetActive(false);
                    PutObjectIntoHand(hand);
                    break;
                }
            }
        }
    }


    public void PutObjectIntoHand(GameObject hand)
    {
        transform.SetParent(hand.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        rigidBody.isKinematic = true;
        rigidBody.interpolation = RigidbodyInterpolation.None;
    }

    public void DropObject(Vector3 forceVector, Vector3 playerVelocity, int pos)
    {
        transform.SetParent(null);
        rigidBody.isKinematic = false;
        rigidBody.velocity = playerVelocity;
        rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        rigidBody.AddForce(forceVector * 3.5f, ForceMode.Impulse);
        inventory.RemoveFromInventory(pos);
    }

    public Sprite GetSourceImg()
    {
        return objSprite;
    }

    public string GetObjectName()
    {
        return objectName;
    }
}
