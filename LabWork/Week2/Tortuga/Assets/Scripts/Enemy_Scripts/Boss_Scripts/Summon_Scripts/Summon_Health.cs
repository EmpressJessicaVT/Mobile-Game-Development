using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Summon_Health : MonoBehaviour
{
    public int Summon_Ship_Health = 1;
    public GameObject explosionPrefab;
    public GameObject cannonCollectible;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cannonBall_Player"))
        {
             OnHit();
             //used to check if was hit by player or their cannonball, or even other summoned ships, we have friendly fire!
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHit();
        }
        if (collision.gameObject.CompareTag("Enemy_Ships"))
        {
            OnHit();
        }
    }

    private void OnHit()
    {
        --Summon_Ship_Health;
        if(Summon_Ship_Health==0)
            {
                Sunk();
                //when enemies health is reduced to 0, triggers the sunk state
            }
    }

    private void Sunk()
    {
        Vector3 ShipPos = transform.position + new Vector3(0,0,0);
        Instantiate(explosionPrefab, ShipPos, quaternion.identity);
        this.GetComponent<Summon_Move>().enabled = false;
        this.GetComponent<Summon_Player_Detect>().enabled = false;
        //stops the destroyed enemy from continuing to move or turn towards the player

        collectibleRandom();
        Invoke ("OnSink",0.1f);
        //waits 0.1 seconds after an enemy has been defeated to delete them, allows the player to feel like they have sunk, rather than just poof gone
        //less time than normal, as expect lots of these to be dying each second!
    }

    private void OnSink()
    {
        Manager_Script.Score += 50;
        Destroy(gameObject);
    }
    private void collectibleRandom()
    {
        int randomchance = Random.Range(0,10);
        //generates a random number, if it was 0, spawn the rapid fire collectible.
        //given large range to reduce amount of these at once, as their effect can stack, but is crucial to help defeat bosses in later runs
        if(randomchance==0)
        {
            Vector3 ShipPos = transform.position + new Vector3(0,0,0);
            Instantiate(cannonCollectible, ShipPos, quaternion.identity);
        }
    }
}
