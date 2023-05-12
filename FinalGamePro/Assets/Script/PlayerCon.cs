using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D) , typeof(Gun))]
public class PlayerCon : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    Animator anim;

    bool running = false;
    bool canmove = true;

    [SerializeField]float speed;
    [SerializeField]float rSpeed;

    [Header("Life point")]
    public bool playerHasDie = false;

    [Header("stmina")]
    [Range(0, 100)] public  float maxStm , currentStm;
    [SerializeField] bool isMoving;

    [Header("Aim")]
    [SerializeField] float speedRotetion;
    Vector2 mousePo;
    Inputs playerInput;

    [Header("Gun")]
    [Range(0,2)] public int currentAmmo, maxAmmo;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletPre;
    [SerializeField] Transform gunPos;
    [SerializeField] bool ammoOut;
    [SerializeField] Gun gun;

    [SerializeField] GameMenager gm;

    public float currentSpeed
    {
        get 
        {
            if (_CanMove)
            {
                if (running) return rSpeed;
                else
                {
                    return speed;
                }
            }
            else return 0;
        }
    }

    public bool _isMoveing
    {
        get { return isMoving; }
        set 
        { 
            isMoving = value; 
            anim.SetBool(AnimString.walk,isMoving);
            FindAnyObjectByType<Audio>().play("Walk", isMoving);
        }
    }

    public bool _CanMove
    {
        get
        {
            return anim.GetBool(AnimString.canMove);
        }
        set
        {
            canmove = value;
        }
    }

    public bool _AmmoOut
    {
        get
        {
            return ammoOut;
            /*if (currentAmmo <= 0) return true; 
            else
            {
                return ammoOut;
            }*/
        }
        set
        {
            ammoOut = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        gunPos = GameObject.FindGameObjectWithTag("Gun").GetComponent<Transform>();
        currentStm = maxStm;
        currentAmmo = maxAmmo;
        playerInput = new Inputs();
        playerInput.Player.Enable();
        Cursor.visible = false;
        gm = GameObject.Find("GameManager").GetComponent<GameMenager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _AmmoOut = currentAmmo <= 0;

        if(running == true)
        {
            currentStm -= Time.deltaTime * 40;
            currentStm = Mathf.Clamp(currentStm, 0, maxStm);
        }
        else if(running == false)
        {
            StartCoroutine(stmRegen());
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * currentSpeed, moveInput.y * currentSpeed);
        mouseAim();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        _isMoveing = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(currentStm <= 10)
            {
                running = false;
            }
            else if(currentStm >= 10)
            {
                running = true;
            }
        }
        else if (context.canceled)
        {
            running = false;
        }
    }

    IEnumerator stmRegen()
    {
        yield return new WaitForSeconds(6);
        currentStm += Time.deltaTime * 20;
        currentStm = Mathf.Clamp(currentStm, 0, maxStm);

    }

    public void OnAtk(InputAction.CallbackContext context)
    {
        if(context.started && _AmmoOut == false)
        {
            Debug.Log("Fire");
            FindAnyObjectByType<Audio>().play("Shoot", true);
            anim.SetTrigger(AnimString.shoot);
            Gun.shoot(bulletSpeed,gunPos,bulletPre);
            currentAmmo--;
        }
        else if(context.started && _AmmoOut == true)
        {
            Debug.Log("Ammo Out");
        }
    }

    public void mouseAim()
    {
        Vector2 mousePoWorld = playerInput.Player.MousePo.ReadValue<Vector2>();
        mousePo = Camera.main.ScreenToWorldPoint(mousePoWorld);
        
        float angle = Mathf.Atan2(mousePo.y - transform.position.y, mousePo.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,speedRotetion * Time.deltaTime);
    }

    public IEnumerator hasBeenAtk()
    {
        canmove = false;
        yield return new WaitForSeconds(1);
    }
}
