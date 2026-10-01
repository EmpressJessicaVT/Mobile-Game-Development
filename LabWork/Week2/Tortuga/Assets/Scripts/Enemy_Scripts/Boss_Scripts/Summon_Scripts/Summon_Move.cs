using Unity.Mathematics;
using UnityEngine;

public class Summon_Move : MonoBehaviour
{
    private float summon_speed = 1f;
    //speed ship can move at, and how fast it can spin
    private float rotation_speed = 100f;
    private Rigidbody2D rb;
    private Summon_Player_Detect summon_Player_Detect;
    private Vector2 target_Direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //when first loaded, gets the rigid body and the player detection script
        summon_Player_Detect = GetComponent<Summon_Player_Detect>();
    }
    private void FixedUpdate()
    {
        //used Fixed update, rather than update, to ensure felt consistent despite frame rate, boss fights can get messy
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();

    }
    private void UpdateTargetDirection()
    {
        if(summon_Player_Detect.AwareOfPlayer)
        {
            //if the ship was aware of player, would want to turn towards it, if not, will want to travel straight forward
            target_Direction = summon_Player_Detect.DirectionToPlayer;
        }
        else
        {
            target_Direction = Vector2.zero;
        }
    }
    private void RotateTowardsTarget()
    {
        if(target_Direction == Vector2.zero)
        {
            return;
            //if it can't detect a player, it doesnt need to rotate
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, target_Direction);
        quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotation_speed*Time.deltaTime);
        //used to rotate the summoned ship towards the player, looking smooth rather than instantly facing them
        rb.SetRotation(rotation);
    }
    private void SetVelocity()
    {
        if (target_Direction == Vector2.zero)
        {
            //rb.linearVelocity = Vector2.zero;
            //This was commented out and made the same value, plan was that if it lost detection it would stay still.
            //however this made essentually a mine field! This could have been used in a boss variant
            rb.linearVelocity = transform.up * summon_speed;
        }
        else
        {
            rb.linearVelocity = transform.up * summon_speed;
        }
    }
    private void OnBecameInvisible()
    {
        //if enemy goes off screen, deletes itself
       Destroy(gameObject);
    }
}
