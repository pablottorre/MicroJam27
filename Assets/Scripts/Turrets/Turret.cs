using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private GameObject bullet;

    [SerializeField] private Color _color;
    public Color Color => _color;


    public void Shoot()
    {

    }

}
