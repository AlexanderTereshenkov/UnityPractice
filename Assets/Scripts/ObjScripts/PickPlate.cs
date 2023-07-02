using UnityEngine;

public class PickPlate : MonoBehaviour
{
    [SerializeField] private GameObject platePrefab;
    [SerializeField] private GameObject plateSpawnPoint;
    public GameObject GetPlate()
    {
        return Instantiate(platePrefab, plateSpawnPoint.transform);
    }
}
