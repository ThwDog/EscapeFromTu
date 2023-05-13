using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    internal static GameMenager instance;

    [SerializeField] [Range(0,4)] internal int key;
    PlayerCon player;
    Enemy enemy;
    UI ui;
    public List<SpawnItem> spawnItem;

    [Header("Player")]
    public Transform playerSpawn;
    public Transform playerLocation;
    [SerializeField] int playerLP;

    [Header("Enemy")]
    public Transform enemySpawn;
    public Transform enemyLocation;

    public bool canPass { get; private set; }
    public bool win;
    public bool die;

    public int _PlayerLP
    {
        get
        {
            return playerLP;
        }
        set
        {
            playerLP = value;
            
        }
    }

    public int ammo
    {
        get { return player.maxAmmo; }
    }

    public bool _win
    {
        get { return win; }
        set 
        { 
            win = value;
            FindAnyObjectByType<Audio>().play("Cowboy", win);
        }
    }

    public bool _die
    {
        get
        {
            
            return die;
        }
        set 
        { 
            die = value;
            player.playerHasDie = value;
            //FindAnyObjectByType<Audio>().play("Cowboy", value);
        }
    }

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
        playerLocation = player.transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        enemyLocation = enemy.transform;
        ui = GameObject.FindGameObjectWithTag("Ui").GetComponent<UI>();


        GameObject[] s = GameObject.FindGameObjectsWithTag("Spawnner");
        for(int i = 0; i < s.Length; i++)
        {
            Debug.Log(s[i]);
            spawnItem.Add(s[i].GetComponent<SpawnItem>());
        }
    }

    private void Start()
    {
        newGame();
    }


    private void Update()
    {
        _die = _PlayerLP <= 0;
        canPass = key == 4;
        if (_PlayerLP <= 0)
        {
            //Debug.Log("Die");
            enemy.enabled = false;
            player.enabled = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("getKey");   
                newGame();
                enemy.enabled = true;
                player.enabled = true;
            }
            
        }
        
        if (_win)
        {
            enemy.enabled = false;
            player.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Application.Quit();
        }
    }

    //set all value to default
    void newGame()
    {
        setLive(3);
        setAmmo(2);
        setPoint(0);
        foreach(var item in spawnItem)
        {
            Debug.Log(item.name);
            item.spawn(item.item);
        }
        setspawn(playerLocation,playerSpawn);
        setspawn(enemyLocation,enemySpawn);
        player.currentStm = player.maxStm;
        player.currentAmmo = player.maxAmmo;
        ui.restart();
        
    }

    void setLive(int live)
    {
        this.playerLP = live;
        
    }

    void setAmmo(int ammo)
    {
        player.maxAmmo = ammo; 
    }

    void setPoint(int Point)
    {
        this.key = Point;
    }

    public void setspawn(Transform position,Transform spawnPoint)
    {
        position.transform.position = spawnPoint.position;
    }

    

}