using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] float spawnOffset = 60f;
    [SerializeField] float positionLag = 2f;
    
    private Transform player;
    private BossManager bm;
    private Vector3 bossPos;

    private void Start()
    {
        bm = FindObjectOfType<BossManager>();
        SetPlayerTransform(bm.Player);
    }

    private void Update()
    {
        BossPositionLerper();
    }

    void BossPositionLerper()
    {   
        bossPos = new Vector3(0f, 12f, player.position.z + spawnOffset);
        transform.position = Vector3.Lerp(transform.position, bossPos, positionLag * Time.deltaTime);
    }

    void SetPlayerTransform(Transform transform)
    {
        player = transform;
    }


}
