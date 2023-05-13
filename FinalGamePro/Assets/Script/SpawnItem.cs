using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class SpawnItem : MonoBehaviour
{
    public GameObject item;
    [SerializeField]float reSpawnTime;
    [SerializeField] bool canRespawn;

    private void Awake()
    {
        //spawn(item);
    }

    public void spawn(GameObject item)
    {
        Instantiate(item,gameObject.transform.position,gameObject.transform.rotation); 
    }

    IEnumerator reSpawn(GameObject item)
    {
        yield return new WaitForSeconds(reSpawnTime);
        Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canRespawn)
        {
            StartCoroutine(reSpawn(item));
        }
    }
}
