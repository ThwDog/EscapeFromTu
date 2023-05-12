using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    static public Bullet instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Update()
    {
        StartCoroutine(bullet());
    }

    IEnumerator bullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
