using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20F;
    private Rigidbody2D _myRigidBody2D;
    private PlayerMovement _player;
    private float _xSpeed;

    private void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _player = FindFirstObjectByType<PlayerMovement>();
        _xSpeed = _player.transform.localScale.x * bulletSpeed;
    }

    private void Update()
    {
        _myRigidBody2D.linearVelocityX = _xSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BigEnemy"))
        {
            var bigEnemy = FindAnyObjectByType<BigEnemy>();
            bigEnemy.OnBulletHit();
        }
        Destroy(gameObject, 0.03F);
    }
}
