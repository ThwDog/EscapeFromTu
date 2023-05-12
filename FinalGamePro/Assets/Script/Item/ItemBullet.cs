using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBullet : MonoBehaviour
{
    [SerializeField] Item item;

    PlayerCon player;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(item.target))
        {
            player.currentAmmo += item.incread;
            _HasCollect = true;
            Destroy(gameObject);
        }
    }
}
