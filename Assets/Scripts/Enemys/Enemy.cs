using System;
using UnityEngine;
using CustomColors;
using Color = CustomColors.Color;

public class Enemy : MonoBehaviour, IPoolObject<Enemy>
{
    [SerializeField] protected int life;
    [SerializeField] protected float speed;
    [SerializeField] protected Color _color;
    [SerializeField] protected Door door;
    [SerializeField] protected Nexus nexus;
    protected Vector3 doorPosition => new(door.transform.position.x, transform.position.y, transform.position.z);
    protected Vector3 nexusPosition => new(nexus.transform.position.x, transform.position.y, transform.position.z);

    private Action<Enemy> _onReturnFunction;
    private CustomColor _customColor;
    
    [SerializeField] private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _customColor = new CustomColor(_color); 
        _meshRenderer.material.color = (UnityEngine.Color)_customColor;
    }

    protected void Update()
    {
        if (door.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, doorPosition) < 2f)
            {
                //animacion para dmg
                return;
            }
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(door.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else
        {
            if (Vector3.Distance(transform.position, nexusPosition) < 2f)
            {
                nexus.TakeDamage(1);
                return;
            }
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(nexus.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    public void GetDamage(CustomColor color)
    {
        if (color == _customColor)
        {
            life--;
            if (life <= 0)
            {
                EventManager.TriggerEvent(EventNames._OnEnemyDead, this);
                _onReturnFunction(this);
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
        _meshRenderer.material.color = (UnityEngine.Color)_customColor;
    }

    public void OnCreateObject(Action<Enemy> returnFunction)
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
        _customColor = new CustomColor(Color.None);
        gameObject.SetActive(false);
    }

    public void OnAttack()
    {
        door.TakeDamage(1);
    }
}
