using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10F;
    [SerializeField] private float jumpSpeed = 20F;
    [SerializeField] private float climbSpeed = 5F;
    
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsClimbing = Animator.StringToHash("isClimbing");
    private Vector2 _moveInput;
    private Rigidbody2D _myRigidBody;
    private Animator _myAnimator;
    private CapsuleCollider2D _myCapsuleCollider;
    private BoxCollider2D _myBoxCollider;
    private float _initialPlayerGravity;
    
    private void Start()
    {
           _myRigidBody = GetComponent<Rigidbody2D>();
           _myAnimator = GetComponent<Animator>();
           _myCapsuleCollider = GetComponent<CapsuleCollider2D>();
           _myBoxCollider = GetComponent<BoxCollider2D>();
           _initialPlayerGravity =  _myRigidBody.gravityScale;
    }

    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
    }

    private void OnJump(InputValue input)
    {
        var groundLayer = LayerMask.GetMask("Ground");
        var isTouchingGround = _myBoxCollider.IsTouchingLayers(groundLayer);
        if (input.isPressed && isTouchingGround)
        {
            _myRigidBody.linearVelocity += new Vector2(0F, jumpSpeed);
        }
    }

    private void Run()
    {
        var playerVelocity = new Vector2(_moveInput.x * moveSpeed, _myRigidBody.linearVelocity.y);
        _myRigidBody.linearVelocity = playerVelocity;
        
        var hasHorizontalSpeed = Mathf.Abs(_myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        _myAnimator.SetBool(IsRunning, hasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        var hasHorizontalSpeed = Mathf.Abs(_myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_myRigidBody.linearVelocity.x), transform.localScale.y);
        }
    }

    private void ClimbLadder()
    {
        var climbingLayer = LayerMask.GetMask("Climbing");
        var isTouchingClimbing = _myCapsuleCollider.IsTouchingLayers(climbingLayer);
    
        var groundLayer = LayerMask.GetMask("Ground");
        var isTouchingGround = _myBoxCollider.IsTouchingLayers(groundLayer);

        if (isTouchingClimbing)
        {
            _myRigidBody.gravityScale = 0F;
            _myAnimator.SetBool(IsClimbing, !isTouchingGround);
        }
        else
        {
            _myAnimator.SetBool(IsClimbing, false);
            _myRigidBody.gravityScale = _initialPlayerGravity;
            return;
        }
        
        var climbVelocity = new Vector2(_myRigidBody.linearVelocity.x, _moveInput.y * climbSpeed);
        _myRigidBody.linearVelocity = climbVelocity;
    }
}
