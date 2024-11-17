using System;
using UnityEngine;

public class Door : MonoBehaviour, IDamageable
{
    public int life;
    private int healthAmount;
    private int maxLife;

    private void Awake()
    {
        healthAmount = life / 4;
        maxLife = life;
        EventManager.SubscribeToEvent(EventNames._OnStartNewDay, OnStartNewDay);
    }

    private void OnStartNewDay(params object[] parameters)
    {
        life += healthAmount;
        life = Mathf.Clamp(life, 0, maxLife);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}