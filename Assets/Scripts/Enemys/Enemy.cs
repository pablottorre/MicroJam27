using System;
using UnityEngine;
using CustomColors;
using UnityEngine.Serialization;
using Color = CustomColors.Color;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IPoolObject<Enemy>
{
    [SerializeField] protected int life;
    [SerializeField] protected float speed;
    [SerializeField] protected Color _color;
    [SerializeField] protected Door door;
    [SerializeField] protected Nexus nexus;
    protected Vector3 doorPosition => new(transform.position.x, transform.position.y, door.transform.position.z);
    protected Vector3 nexusPosition => new(transform.position.x, transform.position.y, nexus.transform.position.z);

    private Action<Enemy> _onReturnFunction;
    private CustomColor _customColor;

    [SerializeField] private SkinnedMeshRenderer _colorMeshRenderer;

    [SerializeField] private Material[] _materials;

    [SerializeField] private SkinnedMeshRenderer _baseMeshRenderer;

    [SerializeField] private Animator _animator;

    private bool _isDead = false;

    [SerializeField] private Material _rainbowMaterial;

    private void Awake()
    {
        _customColor = new CustomColor(_color);
    }

    protected void Update()
    {
        if (_isDead) return;

        if (door.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, doorPosition) < 8f)
            {
                _animator.SetTrigger("Attacking");
                return;
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position,
                new Vector3(transform.position.x, transform.position.y, door.transform.position.z),
                speed * Time.deltaTime);
        }
        else
        {
            if (Vector3.Distance(transform.position, nexusPosition) < 1f)
            {
                nexus.TakeDamage(1);
                return;
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position,
                new Vector3(transform.position.x, transform.position.y, nexus.transform.position.z),
                speed * Time.deltaTime);
        }
    }

    public void GetDamage(CustomColor color)
    {
        if (color == _customColor)
        {
            life--;
            SoundManager.instance.PlaySound(SoundID.hitEnemy);
            if (life <= 0)
            {
                _isDead = true;
                _animator.SetTrigger("Die");
            }
        }
    }

    public void SetterDoor(Door _door)
    {
        door = _door;
    }

    public void SetterNexus(Nexus _nexus)
    {
        nexus = _nexus;
    }


    public void SetColor(CustomColor color)
    {
        _customColor = color;
        if (color.ColorId != 7)
        {
            _colorMeshRenderer.material.color = (UnityEngine.Color)_customColor;
        }
        else
        {
            _colorMeshRenderer.material = _rainbowMaterial;
        }
    }

    public void OnCreateObject(Action<Enemy> returnFunction)
    {
        _onReturnFunction = returnFunction;
    }

    public void OnEnableSetUp(Transform enablePoint)
    {
        transform.position = enablePoint.position;
        transform.rotation = enablePoint.rotation;

        var index = Random.Range(0, _materials.Length);
        _baseMeshRenderer.material = _materials[index];

        gameObject.SetActive(true);
    }

    public void OnDisableSetUp()
    {
        _customColor = new CustomColor(Color.None);
        gameObject.SetActive(false);
    }

    public void OnAttack()
    {
        SoundManager.instance.PlaySound(SoundID.enemyPunch);
        door.TakeDamage(1);
    }

    public void OnDeathEvent()
    {
        _onReturnFunction(this);
        EventManager.TriggerEvent(EventNames._OnEnemyDead, this);
    }
}