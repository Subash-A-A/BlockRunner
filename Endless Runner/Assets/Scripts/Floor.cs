using UnityEngine;

public class Floor : MonoBehaviour
{
    private Transform spawnPoint;
    private FloorManager floorManager;
    private Transform floorParent;
    private void Start()
    {
        floorParent = transform.parent;
        floorManager = FindObjectOfType<FloorManager>();
        
        // Last Child will be spawn point
        spawnPoint = transform.GetChild(transform.childCount - 1);
    }

    // Spawn next floor once player enters this floor
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            SpawnFloor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(gameObject, 0.75f);
        }
    }
    void SpawnFloor()
    {
        GameObject floor = Instantiate(floorManager.SelectFloor(), spawnPoint.position, floorParent.rotation, floorParent);
        floor.transform.localScale = floorParent.transform.localScale;
    }
}
