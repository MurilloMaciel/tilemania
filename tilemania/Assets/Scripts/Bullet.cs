using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20F;
    private Rigidbody2D _myRigidBody2D;
    private PlayerMovement _player;
    private float _xSpeed;
    
    void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _player = FindFirstObjectByType<PlayerMovement>();
        _xSpeed = _player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        _myRigidBody2D.linearVelocityX = _xSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject, 1F);
    }
}
