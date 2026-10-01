using System.Collections;
using UnityEngine;

public class Enemy_Cannonball_Script : MonoBehaviour
{
    public float CannonBall_Speed = 2;

    GameObject target;
    Rigidbody2D rb;
    Vector2 CannonBallDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Player_Ship");
        //when first loaded in, detects the players position

        rb = GetComponent<Rigidbody2D>();
        CannonBallDirection = (target.transform.position - transform.position).normalized * CannonBall_Speed;
        //fires the cannonball towards the players location when it was first fired
        rb.linearVelocity = new Vector2 (CannonBallDirection.x, CannonBallDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cannonBall_Player"))
        {
            Destroy(gameObject);
            //cannonball is destroyed if it hits the player, or their cannonball
        }

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
       Destroy(gameObject);
       //cannonball is destroyed if it goes off screen
    }
}
