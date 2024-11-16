using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Color _color;
    public Color Color => _color;


    public void Shoot()
    {
        GameObject a = Instantiate(bullet);
        a.transform.position = spawnPoint.position;
        a.GetComponent<Bullet>().SetColor(_color);
        a.GetComponent<Bullet>().SetForward(this.transform);
    }

}
