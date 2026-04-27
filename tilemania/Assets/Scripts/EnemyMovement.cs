using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D _myRigidBody2D;

    private void Start()
    {
        _myRigidBody2D =  GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _myRigidBody2D.linearVelocityX = moveSpeed;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) return;
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
    
    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
