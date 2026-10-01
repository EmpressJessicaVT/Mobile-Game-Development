using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Health : MonoBehaviour
{
    public int Enemy_Ship_Health = 1;
    public GameObject explosionPrefab;
    public Sprite sunkShip;
    //various sprites used as visual health states of enemies.
    public AudioClip cannonHitSound;
    public GameObject healthCollectible;
    public GameObject cannonCollectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cannonBall_Player"))
        {
             OnHit();
             //used to check if was hit by player or their cannonball, otherwise takes no damage (no friendly fire)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHit();
        }
    }

    private void OnHit()
    {
        --Enemy_Ship_Health;
        if(Enemy_Ship_Health==0)
            {
                Sunk();
                //when enemies health is reduced to 0, triggers the sunk state
            }
    }
    private void Sunk()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sunkShip;
        Vector3 ShipPos = transform.position + new Vector3(0,0,0);
        Instantiate(explosionPrefab, ShipPos, quaternion.identity);
        this.GetComponent<Enemy_Move_Script>().enabled = false;
        this.GetComponent<Enemy_Shoot_Script>().enabled = false;
        //stops the destroyed enemy from continuing to move or shoot
        collectibleRandom();

        AudioSource.PlayClipAtPoint(cannonHitSound, new Vector3(0,0,0));
        Invoke ("OnSink",0.8f);
        //waits 0.8 seconds after an enemy has been defeated to delete them, allows the player to feel like they have sunk, rather than just poof gone
    }
    private void OnSink()
    {
        Manager_Script.Score += 100;
        Manager_Script.Enemy_Ships_Sunk +=1;
        Enemy_Spanwer_Script.ShipCount --;
        Destroy(gameObject);
    }

    private void collectibleRandom()
    {
        int randomchance = Random.Range(0,5);
        //generates a random number, if it was 0, spawn the rapid fire collectible, if was 5 spawned a heal collectible
        if(randomchance==5)
        {
            Vector3 ShipPos = transform.position + new Vector3(0,0,0);
            Instantiate(healthCollectible, ShipPos, quaternion.identity);
        }
        if(randomchance==0)
        {
            Vector3 ShipPos = transform.position + new Vector3(0,0,0);
            Instantiate(cannonCollectible, ShipPos, quaternion.identity);
        }
    }
}
