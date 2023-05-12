using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKey : MonoBehaviour
{
    [SerializeField] Item item;

    PlayerCon player;
    GameMenager gm;

    bool hasCollect;

    public bool _HasCollect
    {
        get
        {
            return hasCollect;
        }
        set
        {
            hasCollect = value;
        }
    }

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameMenager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(item.target))
        {
            gm.key += item.incread;
            _HasCollect = true;
            Destroy(gameObject);
        }
    }
}
