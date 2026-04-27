using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D _myRigidBody2D; 
    
    void Start()
    {
        _myRigidBody2D =  GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _myRigidBody2D.linearVelocityX = moveSpeed;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     moveSpeed = -moveSpeed;
    //     FlipEnemyFacing();
    // }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        // transform.localScale = new Vector2(-Mathf.Sign(_myRigidBody2D.linearVelocityX), 1F);
    }
}
