using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    PlayerCon player;
    GameMenager gm;

    [Header("Text")]
    //[SerializeField] TextMeshProUGUI stmText;
    [SerializeField] TextMeshProUGUI keyCollect;
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] TextMeshProUGUI alert;

    [Header("Img")]
    [SerializeField] GameObject lost;
    [SerializeField] GameObject win;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
        gm = GameObject.Find("GameManager").GetComponent<GameMenager>();

        restart();
    }

    private void Update()
    {
        text();
        img();
    }

    void text()
    {
        int stamina = (int) player.currentStm;

        //stmText.text = "Stamina " + stamina + " / " + player.maxStm;
        bulletText.text = "Bullet " + player.currentAmmo;
        keyCollect.text = gm.key.ToString();
        
        if (player.playerHasDie)
        {
            alert.text = "Press SpaceBar to try agian";
            GameMenager.instance._die = true;
            alert.color = Color.white;
        }
        else if(!player.playerHasDie)
        {
            alert.text = "";
            if (GameMenager.instance.canPass)
            {
                alert.text = "Find Door!!";
                Debug.Log("CanPass");
                alert.color = Color.red;
            }
        }
        if (GameMenager.instance._win)
        {
            alert.text = "Thx for playing press Ese for Quit";
            alert.color = Color.white;
        }
    }

    public void restart()
    {
        win.SetActive(false);
        lost.SetActive(false);
        //FindAnyObjectByType<Audio>().play("Cowboy", false);
    }

    void img()
    {
        if(GameMenager.instance._PlayerLP == 0)
        {
            lost.SetActive(true);
        }
        else if (GameMenager.instance._win)
        {
            win.SetActive(true);
        }
    }
}
