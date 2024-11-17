using System;
using System.Collections;
using System.Collections.Generic;
using CustomColors;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timerSpawner;

    [SerializeField] private WalkingZombie walkingZombie;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] protected GameObject door;
    [SerializeField] protected GameObject nexus;

    [SerializeField] private float minZ, maxZ;

    private SimplePool<Enemy> _WalkingPool;

    private List<Enemy> enemyAlive = new List<Enemy>();

    private void Awake()
    {
        _WalkingPool = new SimplePool<Enemy>(() => Instantiate(walkingZombie, spawnPoint.position, Quaternion.identity));
        EventManager.SubscribeToEvent(EventNames._OnEnemyDead, RemoveEnemy);
    }

    private void Start()
    {

    }

    public void StartSpawningWalking(int value)
    {
        StartCoroutine(SpawningCDWalking(value));
    }

    IEnumerator SpawningCDWalking(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemyWalking();
            yield return new WaitForSecondsRealtime(timerSpawner);
        }
    }

    public void SpawnEnemyWalking()
    {
        var walkingEnemy = _WalkingPool.EnableObject(spawnPoint);
        walkingEnemy.transform.position = new Vector3(walkingEnemy.transform.position.x,
            walkingEnemy.transform.position.y,
            UnityEngine.Random.Range(walkingEnemy.transform.position.z - minZ, walkingEnemy.transform.position.z + maxZ));
        walkingEnemy.SetterDoor(door);
        walkingEnemy.SetterNexus(nexus);
        walkingEnemy.SetColor(CustomColor.GetRandomColor());
        enemyAlive.Add(walkingEnemy);
    }

    public void RemoveEnemy(params object[] parameters)
    {
        enemyAlive.Remove((Enemy)parameters[0]);
        if (enemyAlive.Count == 0)
        {
            EventManager.TriggerEvent(EventNames._OnEndNewDay);
        }
    }
}
