using UnityEngine;
public interface IInteractible
{
    void SpawnFoodObject();
    void StartAction(GameObject gameObject);
    void StopAction();
    void MakeAction(string objName);
    bool IsObjectBusy();
    bool IsObjectFull();

    bool IsPossibleToInteract(GameObject gameObject);
}
