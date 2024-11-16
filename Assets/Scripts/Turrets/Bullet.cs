using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject<Bullet>
{
    [SerializeField] private Color _color;

    private Action<Bullet> _onReturnFunction;
    
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11) // Torreta
        {
            var turret = other.gameObject.GetComponent<Turret>();

            if (turret == null) return;

            _color += turret.Color;
        }
        else if (other.gameObject.layer == 6) // Enemy
        {
            var enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy == null) return;

            enemy.GetDamage(_color);

            _onReturnFunction(this);
        }
    }

    public void OnCreateObject(Action<Bullet> returnFunction)
    {
        _onReturnFunction = returnFunction;
    }

    public void OnEnableSetUp(Transform enablePoint)
    {
        transform.position = enablePoint.position;
        transform.rotation = enablePoint.rotation;
        gameObject.SetActive(true);
    }

    public void OnDisableSetUp()
    {
        _color = Color.white;
        gameObject.SetActive(false);
    }
}