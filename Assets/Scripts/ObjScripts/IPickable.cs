using UnityEngine;

public interface IPickable
{
    void PutObjectIntoInventory(GameObject hand);
    void PutObjectIntoHand(GameObject hand);
    void DropObject(Vector3 forceVector, Vector3 playerVelocity, int pos);
    Sprite GetSourceImg();

}
