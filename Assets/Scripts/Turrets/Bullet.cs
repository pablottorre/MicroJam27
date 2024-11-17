using System;
using UnityEngine;
using CustomColors;
using Color = CustomColors.Color;

public class Bullet : MonoBehaviour, IPoolObject<Bullet>
{
    private CustomColor _color;

    private Action<Bullet> _onReturnFunction;
    
    [SerializeField] private float _speed;

    [SerializeField] private float timerDeath;
    private float timer;
    
    [SerializeField] private MeshRenderer _meshRenderer;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= timerDeath)
        {
            _onReturnFunction(this);
        }
    }

    public void SetColor(CustomColor color)
    {
        _color = color;
        _meshRenderer.material.color = (UnityEngine.Color)color;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public float GetterSpeed()
    {
        return _speed;
    }

    public Vector3 GetterSize()
    {
        return transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            // Torreta
            case 11:
            {
                var turret = other.gameObject.GetComponent<Turret>();

                if (turret == null) return;

                _color += turret.Color;
                _meshRenderer.material.color = (UnityEngine.Color)_color;
                break;
            }
            // Enemy
            case 6:
            {
                var enemy = other.gameObject.GetComponent<Enemy>();

                if (enemy == null) return;

                enemy.GetDamage(_color);

                _onReturnFunction(this);
                break;
            }
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
        timer = 0;
        gameObject.SetActive(true);
    }

    public void OnDisableSetUp()
    {
        _color = new CustomColor(Color.None);
        gameObject.SetActive(false);
    }
}