using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] GameObject Bomb;
    public void SpawnBomb()
    {
        Instantiate(Bomb, transform.position, Quaternion.identity);
    }
}
