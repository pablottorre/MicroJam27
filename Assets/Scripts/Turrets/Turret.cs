using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomColors;

public class Turret : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float cdTimer;
    private bool canShoot = true;

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
        if (canShoot)
        {
            var bullet = _bulletPool.EnableObject(spawnPoint);
            bullet.SetColor(_color);
            canShoot = false;
            StartCoroutine(CdShooting());
        }
    }

    IEnumerator CdShooting()
    {
        yield return new WaitForSecondsRealtime(cdTimer);
        canShoot = true;
    }
}