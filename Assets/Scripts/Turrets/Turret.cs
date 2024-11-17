using System;
using UnityEngine;
using CustomColors;

public class Turret : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private CustomColors.Color _turretColor; 
    
    private CustomColor _color;
    public CustomColor Color => _color;

    private SimplePool<Bullet> _bulletPool;
    
    [SerializeField] private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _bulletPool = new SimplePool<Bullet>(() => Instantiate(bullet, spawnPoint.position, Quaternion.identity));
        _color = new CustomColor(_turretColor);
        _meshRenderer.material.color = (UnityEngine.Color)_color;
    }


    public void Shoot()
    {
        var bullet = _bulletPool.EnableObject(spawnPoint);
        bullet.SetColor(_color);
    }
}