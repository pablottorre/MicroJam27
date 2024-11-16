using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject<Bullet>
{
    [SerializeField] private Color _color;

    private Action<Bullet> _onReturnFunction;
    
    [SerializeField] private float _speed;

    [SerializeField] private float timerDeath;
    private float timer;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= timerDeath)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void SetColor(Color color)
    {
        _color = color;
    }
    
    public void SetForward(Transform _forward)
    {
        transform.forward = _forward.forward;
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

            Debug.Log(444);

            if (enemy == null) return;

            enemy.GetDamage(_color);

            //_onReturnFunction(this);
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