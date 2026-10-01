using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Health_Tough_Var : MonoBehaviour
{
    public int Enemy_Ship_Health = 3;
    public GameObject explosionPrefab;
    public Sprite sunkShip;
    //various sprites used as visual health states of enemies.
    public Sprite damagedShip;
    public Sprite lightDamagedShip;
    public AudioClip cannonHitSound;
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
        else if(Enemy_Ship_Health==1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedShip;
        }
        else if(Enemy_Ship_Health==2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = lightDamagedShip;
        }

    }
    private void Sunk()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sunkShip;
        Vector3 ShipPos = transform.position + new Vector3(0,0,0);
        Instantiate(explosionPrefab, ShipPos,quaternion.identity);
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
        Manager_Script.Score += 350;
        Manager_Script.Enemy_Ships_Sunk +=2;
        //when ship is sunk, acts as if two basic enemies had been sunk. Also meant playing didnt feel you had to kill a set number, harder to read. felt more organic
        Enemy_Spanwer_Script.ShipCount --;
        Destroy(gameObject);
    }

    private void collectibleRandom()
    {
        int randomchance = Random.Range(0,4);
        //generates a random number, if it was 0, spawn the rapid fire collectible
        if(randomchance==0)
        {
            Vector3 ShipPos = transform.position + new Vector3(0,0,0);
            Instantiate(cannonCollectible, ShipPos, quaternion.identity);
        }
    }

}
