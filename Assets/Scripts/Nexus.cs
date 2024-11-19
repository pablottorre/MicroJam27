using UnityEngine;

public class Nexus : MonoBehaviour, IDamageable
{
    private int _life = 1;
    
    private bool isDestroyed = false;

    public void TakeDamage(int damage)
    {
        if(isDestroyed) return;
        
        _life -= damage;

        if (_life <= 0)
        {
            EventManager.TriggerEvent(EventNames._OnEndOfGame);
            isDestroyed = true;
        }
    }
}
