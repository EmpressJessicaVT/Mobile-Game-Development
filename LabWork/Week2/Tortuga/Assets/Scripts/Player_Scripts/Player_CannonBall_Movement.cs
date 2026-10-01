using UnityEngine;

public class Player_CannonBall_Movement : MonoBehaviour
{
    public float Cannonball_Speed = 2f;
    //sets the speed of the projectile.

    void Update()
    {
        GetComponent<Rigidbody2D>().linearVelocityY = Cannonball_Speed;
        //by limiting to linear velocity Y the players cannons can only ever go upwards.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("CannonBall_Enemy"))
        {
            Manager_Script.Score += 10;
            Destroy(gameObject);
            //Deletes itself if it comes into contact with an enemy cannonball, and rewards score.
        }
        if(collision.CompareTag("Enemy_Ships"))
        {
            Destroy(gameObject);
            //deletes itself if it comes into contact with an enemy ship
        }
        if(collision.CompareTag("Menu_Targets"))
        {
            Destroy(gameObject);
            //deletes itself if it comes into contact with any of the menu targetting system.
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        //if cannonball goes off the players screen, delete it
    }

}
