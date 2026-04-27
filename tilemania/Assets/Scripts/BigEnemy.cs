using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    public void OnBulletHit()
    {
        if (--lives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
