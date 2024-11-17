using System;
using UnityEngine;
using CustomColors;
using Color = CustomColors.Color;

public class Enemy : MonoBehaviour, IPoolObject<Enemy>
{
    [SerializeField] protected int life;
    [SerializeField] protected float speed;
    protected CustomColor _color;
    [SerializeField] protected GameObject door;
    [SerializeField] protected GameObject nexus;

    private Action<Enemy> _onReturnFunction;


    protected void Update()
    {
        if (door.gameObject.activeInHierarchy)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(door.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(nexus.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    public void GetDamage(CustomColor color)
    {
        if (color == _color)
        {
            life--;
            if (life <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    public void SetterDoor(GameObject _door)
    {
        door = _door;
    }
    public void SetterNexus(GameObject _nexus)
    {
        nexus = _nexus;
    }


    public void SetColor(CustomColor color)
    {
        _color = color;
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
        _color = new CustomColor(Color.None);
        gameObject.SetActive(false);
    }

}
