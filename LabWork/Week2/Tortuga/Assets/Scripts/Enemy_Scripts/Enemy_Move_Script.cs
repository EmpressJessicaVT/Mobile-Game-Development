using UnityEngine;

public class Enemy_Move_Script : MonoBehaviour
{
     [Range(-5f,5f)]
    public float MoveSpeed = 1.0f;
    //given range so new enemy variants could travel at different speeds
    public Vector2 screenThreshold = new Vector2(10,6);
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //when script is first loaded, assigns the rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().linearVelocityY = -MoveSpeed;
        //each frame moves the player down the Y axis based on their speed.
        //This means no base enemies can move left or right

        Vector2 currentPos = rb.position;

        if(currentPos.y < -screenThreshold.y )
        //used to detect if the enemy goes off screen to either left, right or down, if they do, delete them
        {
            Destroy(gameObject);
        }
        if (currentPos.x < -screenThreshold.x)
        {
            Destroy(gameObject);
        }
        if (currentPos.x > screenThreshold.x)
        {
            Destroy(gameObject);
        }
    }
}
