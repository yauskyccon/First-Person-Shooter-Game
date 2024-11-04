using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public float currentMoveSpeed;

    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 5, runBackSpeed = 4;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput, vInput;

    CharacterController controller;
    #endregion

    #region GroundCheck
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    #endregion


    #region States
    public MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    #endregion

    [HideInInspector] public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);

        //Vector3 airDir = Vector3.zero;
        //if(!IsGrounded())airDir = transform.forward * vInput + transform.right * hzInput;
        //else dir = transform.forward * vInput + transform.right * hzInput;

        //controller.Move((dir.normalized * currentMoveSpeed + airDir.normalized * airSpeed) * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (controller != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
        }
    }
}
