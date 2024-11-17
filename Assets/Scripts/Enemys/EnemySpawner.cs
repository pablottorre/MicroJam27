using CustomColors;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WalkingZombie walkingZombie;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] protected GameObject door;
    [SerializeField] protected GameObject nexus;

    private SimplePool<Enemy> _WalkingPool;

    private void Awake()
    {
        _WalkingPool = new SimplePool<Enemy>(() => Instantiate(walkingZombie, spawnPoint.position, Quaternion.identity));
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemy",2 ,8 );
    }

    public void SpawnEnemy()
    {
        var walkingEnemy = _WalkingPool.EnableObject(spawnPoint);
        walkingEnemy.SetterDoor(door);
        walkingEnemy.SetterNexus(nexus);
        walkingEnemy.SetColor(CustomColor.GetRandomColor());
    }
}
