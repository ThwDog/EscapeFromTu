using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Transform playerPo;
    PlayerCon player;
    Animator anim;

    [Header("Movement")]
    [SerializeField] float chasespeed;
    [SerializeField] float findSpeed;
    [SerializeField] float rSpeed;
    [SerializeField] float findRange;

    public Vector2 targetDir { get; private set; }
    bool die;
    public Transform[] moveDir;
    //path that has been walk
    public List<Transform> oldPath;
    public LayerMask node;
    [SerializeField] bool hastarget;


    public bool Die 
    {
        get
        {
            return die;
        } 
        private set
        {
            if (Die)
            {
                HasTarget = false;
            }

            die = value;
        }
    }

    public bool HasTarget 
    {
        get
        {
            return hastarget;
        }
        set
        {
            hastarget = value;
        }
    }

    protected float currentSpeed
    {
        get 
        {
            if (HasTarget)
            {
                return chasespeed;
            }
            else return findSpeed;
        }
    }

    public bool _isWalk
    {
        set
        {
            anim.SetBool(AnimString.walk,value);
        }
    }

    public float range
    {
        get
        {
            if (HasTarget)
            {
                return 555;
            }
            else return findRange;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
        playerPo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>().transform;
        anim = GetComponent<Animator>();
        findPath();
        resetPath();
    }

    private void Update()
    {
        Vector2 enemyToPlayerPo = playerPo.position - transform.position;
        targetDir = enemyToPlayerPo.normalized;
        HasTarget = enemyToPlayerPo.magnitude < range;
        
        if (!HasTarget) findPath();

        if (Vector2.Distance(transform.position, playerPo.position) > range)
        {
            FindAnyObjectByType<Audio>().play("Hee", true);
        }

        if (Die)
        {
            StartCoroutine(resetSpawn());
        }
    }

    private void FixedUpdate()
    {
        if (HasTarget)
        {
            
            if (Vector2.Distance(transform.position, playerPo.position) < range)
            {
                move(playerPo,targetDir);
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.CircleCast(this.gameObject.transform.position,2f,Vector2.zero, 3f, node);

            Vector2 enemyToPlayerPo = moveDir[0].position - transform.position;
            Vector2 targetDir = enemyToPlayerPo.normalized;
            move(moveDir[0],targetDir);
            if (hit.collider != null)
            {
                Debug.Log("hit");
                moveDir[0].gameObject.SetActive(false);
                if(rb.velocity == Vector2.zero)
                {
                    die = true;
                }
            }

            if (transform.position == moveDir[0].position)
            {
                if (rb.velocity == Vector2.zero)
                {
                   oldPath.Add(this.moveDir[0]);
                   moveDir[0].gameObject.SetActive(false);
                }
            }
        }
    }

    void findPath()
    {
        //getcomponent all node 
        if(this.moveDir != null)
        {
            Transform[] moveDir = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
            //Find availble Way to Move

            

            foreach (Transform moveDirItem in moveDir)
            {
                if (moveDirItem.gameObject.layer == 9 && moveDirItem.CompareTag("Node"))
                {
                    //Random rnd = new Random.Range(0,moveDirItem.childCount);
                    this.moveDir = moveDirItem.GetComponents<Transform>() as Transform[];
                    
                }
                
            }
            
        }
    }

    
    void move(Transform position , Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, position.position, currentSpeed * Time.deltaTime);
        _isWalk = rb.velocity != Vector2.zero;
        rotationToPlayer(target);
    }

    void rotationToPlayer(Vector2 target)
    {
        if(target == Vector2.zero)
        {
            return;
        }

        //make enemy look at target
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, target);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,rSpeed * Time.deltaTime);

        //rb.SetRotation(rotation);
        transform.rotation = rotation; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(player.hasBeenAtk());
            GameMenager.instance._PlayerLP--;
            FindAnyObjectByType<Audio>().play("Scarm", true);
            GameMenager.instance.setspawn(GameMenager.instance.playerLocation,GameMenager.instance.playerSpawn);
        }
    }

    IEnumerator resetSpawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        GameMenager.instance.setspawn(GameMenager.instance.enemyLocation, GameMenager.instance.enemySpawn);
        resetPath();
        yield return new WaitForSeconds(1);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Die = false;
    }

    void resetPath()
    {
        foreach (Transform t in oldPath)
        {
            t.gameObject.SetActive(true);
            oldPath.Remove(t);
        }
        /*Debug.Log(oldPath.Count);
        //reset path
        for(int i = 0; i <= oldPath.Count; i++)
        {
            Debug.Log(oldPath[i]);
            oldPath[i].gameObject.SetActive(true);
            oldPath.Remove(oldPath[i]);
        }*/
    }
}
