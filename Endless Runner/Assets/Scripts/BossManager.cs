using UnityEngine;
using System.Collections;
public class BossManager : MonoBehaviour
{
    public GameObject[] Boss;
    public Transform Player;
    public float bossSpawnTimer = 5f;
    public float invokeRepeatTime = 5f;
    public float bossDowntime = 10f;

    public bool isBossAlive = false;
    public bool canSpawn = true;

    private GameObject currentBoss;

    private void Start()
    {   
        InvokeRepeating(nameof(SpawnBoss), bossSpawnTimer, invokeRepeatTime);
    }

    public GameObject SelectRandomBoss()
    {
        int choice = Random.Range(0, Boss.Length);
        return Boss[choice];
    }

    void SpawnBoss()
    {
        if (!isBossAlive && canSpawn)
        {   
            Vector3 spawnPos = new Vector3(Player.position.x, 12f, Player.position.z - 20f);
            currentBoss = Instantiate(SelectRandomBoss(), spawnPos, Quaternion.identity);
            isBossAlive = true;
        }
    }

    public void KillBoss()
    {
        Destroy(currentBoss);
        isBossAlive = false;
        StartCoroutine(BossSpawnDowntime());
    }

    IEnumerator BossSpawnDowntime()
    {
        canSpawn = false;
        yield return new WaitForSeconds(bossDowntime);
        canSpawn = true;
    }
}
