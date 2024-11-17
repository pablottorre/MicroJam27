using UnityEngine;

public class Nexus : MonoBehaviour, IDamageable
{
    private int _life = 1;

    public void TakeDamage(int damage)
    {
        _life -= damage;

        if (_life <= 0)
        {
            EventManager.TriggerEvent(EventNames._OnEndOfGame);
        }
    }
}
