using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public NavMeshAgent agent;
    [SerializeField] private Rigidbody _rg;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    Animator animator;

    public Transform GroundCheck;
    public LayerMask groundSwimMask;
    [SerializeField] float GroundCheckDistance = 0.4f;
    bool isMove;
    bool isGrounded;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //chạm vào để hiển thị nút di chuyển
        Touch();

        CheckGroundToSwim();

        //kiểm tra thử về anim mở cử lồng nhốt
        prisonAnim();

        //kiểm tra thử về anim win
        WinAnim();

        //kiểm tra thử về anim close
        CloseAnim();
    }

    public void Touch()
    {

        if (Input.GetMouseButton(0))
        {
            isMove = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerMove();
            animator.SetFloat("MoveAnim", 1);
        }
        else
        {
            _rg.velocity = new Vector3(_joystick.Horizontal, _rg.velocity.y, _joystick.Vertical);
            animator.SetFloat("MoveAnim", 0);
            animator.SetFloat("IdleAnim", 1);
            isMove = false;
        }

    }

    public void PlayerMove()
    {
        // di chuyển nhân vật
        _rg.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rg.velocity.y, _joystick.Vertical * _moveSpeed);

        //Kiểm tra xem nhân vật có di chuyển hay không?
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            //code khả năng xoay người của nhân vật
            transform.rotation = Quaternion.LookRotation(_rg.velocity);

        }
   
   
    }

    public void CheckGroundToSwim()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundCheckDistance, groundSwimMask);
        if(isGrounded)
        {
            animator.SetBool("isGroundSwim", true);
        }
        else
        {
            animator.SetBool("isGroundSwim", false);

        }    
        
        if(isGrounded && isMove ==false)
        {
            animator.SetBool("isGroundSwimIdle", true);
        }

        if (isGrounded && isMove == true)
        {
            animator.SetBool("isGroundSwimIdle", false);
        }
    }  
    
    public void prisonAnim()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isPrison", true);
        }
        else
        {
            animator.SetBool("isPrison", false);
        }    
    }


    public void WinAnim()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("isWin", true);
        }

    }

    public void CloseAnim()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetBool("isClose", true);
        }

    }
}
