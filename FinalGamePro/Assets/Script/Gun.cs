using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /*public static Gun instance;
    public Transform gunPos;
    public GameObject bulletPre;*/

    private void Awake()
    {
        //gunPos = GetComponent<Transform>();

    }
    /*private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }*/

    public static void shoot(float speedBull, Transform gunPos, GameObject bulletPre)
    { 
        GameObject bullet = Instantiate(bulletPre,gunPos.position,gunPos.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gunPos.right * speedBull, ForceMode2D.Impulse);
    }
}
