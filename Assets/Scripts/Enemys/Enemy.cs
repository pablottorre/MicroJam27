using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int life;
    [SerializeField] protected float speed;
    [SerializeField] protected Color colorOwn;
    [SerializeField] protected GameObject door;
    [SerializeField] protected GameObject nexus;

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

    public void GetDamage(Color color)
    {
        if (color == colorOwn)
        {
            life--;
            if (life <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
