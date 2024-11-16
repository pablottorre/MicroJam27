using UnityEngine;

public class Door : MonoBehaviour
{
    public int life;

    public void ReduceLife(int lifeToLose)
    {
        life -= lifeToLose;

        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
