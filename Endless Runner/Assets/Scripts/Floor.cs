using UnityEngine;

public class Floor : MonoBehaviour
{
    private Transform spawnPoint;
    private FloorManager floorManager;
    private void Start()
    {
        floorManager = FindObjectOfType<FloorManager>();

        // Last Child will be spawn point
        spawnPoint = transform.GetChild(transform.childCount - 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            SpawnFloor();
            Debug.Log("Player Triggered!");
        }
    }

    void SpawnFloor()
    {
        GameObject randomFloor = floorManager.SelectFloor();
        Instantiate(randomFloor, spawnPoint);
    }
}
