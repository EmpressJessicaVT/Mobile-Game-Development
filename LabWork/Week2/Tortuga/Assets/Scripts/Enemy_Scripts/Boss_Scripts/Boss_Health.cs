using Unity.Mathematics;
using UnityEngine;

public class Boss_Health : MonoBehaviour
{
    public int Enemy_Ship_Health = 50;
    public GameObject explosionPrefab;
    public Sprite sunkShip;
    //various sprites used as visual health states of enemies.
    public Sprite damagedShip;
    public Sprite lightDamagedShip;
    public AudioClip cannonHitSound;
    public GameObject healthCollectible;
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
        else if(Enemy_Ship_Health==20)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedShip;
            //updates the players sprite as they get hit, visual dmage identifier
        }
        else if(Enemy_Ship_Health==35)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = lightDamagedShip;
        }
    }
    private void Sunk()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sunkShip;
        Vector3 ShipPos = transform.position + new Vector3(0,0,0);
        Instantiate(explosionPrefab, ShipPos, quaternion.identity);
        this.GetComponent<Boss_Move>().enabled = false;
        this.GetComponent<Boss_Shoot>().enabled = false;
        //stops the destroyed enemy from continuing to move or shoot

        Instantiate(healthCollectible, ShipPos, quaternion.identity);
        //when defeated, always spawns a health collectible

        AudioSource.PlayClipAtPoint(cannonHitSound, new Vector3(0,0,0));
        Invoke ("OnSink",0.8f);
        //waits 0.8 seconds after an enemy has been defeated to delete them, allows the player to feel like they have sunk, rather than just poof gone
    }
    private void OnSink()
    {
        //when boss is sunk, provides large amount of score, adds 1 to boss defeated tally, and resets amount of enemies destroyed
        Manager_Script.Score += 10000;
        Manager_Script.Enemy_Ships_Sunk = 0;
        Manager_Script.Boss_Ships_Sunk += 1;
        Enemy_Spanwer_Script.BossShipCount = 0;
        Manager_Script.Enemy_Spawn = true;
        Manager_Script.Boss_Spawn = false;
        Destroy(gameObject);
    }

}
