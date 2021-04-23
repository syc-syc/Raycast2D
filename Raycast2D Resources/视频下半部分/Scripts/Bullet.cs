using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject boomEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Block")
        {
            Debug.Log("Bullet Hit Wall");

            Instantiate(boomEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.collider.gameObject.name);
            Instantiate(boomEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //DAMAGE
        }
    }


}
