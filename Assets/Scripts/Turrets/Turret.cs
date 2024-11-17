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
    private Vector3 currentScale;
    private float speedBullets;

    [SerializeField] private CustomColors.Color _turretColor;

    private CustomColor _color;
    public CustomColor Color => _color;

    private SimplePool<Bullet> _bulletPool;


    private void Awake()
    {
        _bulletPool = new SimplePool<Bullet>(() => Instantiate(bullet, spawnPoint.position, Quaternion.identity));
        _color = new CustomColor(_turretColor);

    }

    private void Start()
    {
        currentScale = bullet.GetterSize();
        speedBullets = bullet.GetterSpeed();
    }

    public void Shoot()
    {
        if (canShoot)
        {
            SoundManager.instance.PlaySound(SoundID.shoot);
            var bullet = _bulletPool.EnableObject(spawnPoint);
            bullet.gameObject.transform.localScale = currentScale;
            bullet.SetSpeed(speedBullets);
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

    public void IncreaseCurrentScale(Vector3 newScale)
    {
        currentScale += newScale;
    }

    public void ReduceCDUpgrade()
    {
        cdTimer -= (cdTimer * 0.25f);
    } 
    
    public void UpgradeBulletSpeed()
    {
        speedBullets += (speedBullets * 0.2f);
    }
}