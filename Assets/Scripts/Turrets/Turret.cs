using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Color _color;
    public Color Color => _color;

    private SimplePool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new SimplePool<Bullet>(() => Instantiate(bullet, spawnPoint.position, Quaternion.identity));
    }


    public void Shoot()
    {
        var bullet = _bulletPool.EnableObject(spawnPoint);
        bullet.SetColor(_color);
    }
}