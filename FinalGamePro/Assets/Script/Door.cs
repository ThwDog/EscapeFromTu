using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    TilemapRenderer tile;

    private void Awake()
    {
        tile = GameObject.Find("Door").GetComponent<TilemapRenderer>();
        tile.enabled = false;
    }

    public void Update()
    {
        if (GameMenager.instance.canPass)
        {
            tile.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameMenager.instance.canPass)
            {
                tile.enabled = true;
                GameMenager.instance._win = true;
                Debug.Log("Win");
            }
            else Debug.Log("Pls Collect Key");
        }
    }
}
