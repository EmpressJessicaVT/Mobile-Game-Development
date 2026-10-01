using UnityEngine;

public class Collectibles_Script : MonoBehaviour
{
    public float Collectible_Speed = 0.5f;
    //sets speed collectibles should move down the screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //item will only be deleted by touching the player, not other items such as enemies or cannons
        }
    }
    private void OnBecameInvisible()
    {
       Destroy(gameObject);
       //if object falls off screen, deletes itself
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocityY = -Collectible_Speed;
        //each second moves the item down by the speed set earlier
    }
}
