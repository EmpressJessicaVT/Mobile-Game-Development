using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    //prefab object used to summon the explosion when hit by cannonballs.
    public Sprite sunkShip;
    //all the various sprites for the ships health stages, including the "ee" easter egg skins.
    public Sprite eeSunkShip;
    public Sprite damagedShip;
    public Sprite eeDamagedShip;
    public Sprite lightDamagedShip;
    public Sprite eeLightDamagedShip;
    public Sprite HealthyShip;
    public Sprite eeHealthyShip;
    private bool eeCheck;
    //used to check if the easter egg has been activated
    public bool IsInvincible { get; set; }
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    //awake plays the script every time the component is loaded into a scene, rather than the first time loaded in the game.
    private void Awake()
    {
        eeCheck = Manager_Script.Easter_Egg;
        if(eeCheck==true)
        {
            //checking if easter egg has been activated, and updates the players sprite if it has
            gameObject.GetComponent<SpriteRenderer>().sprite = eeHealthyShip;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBall_Enemy"))
        {
             OnHit();
             //checks if player runs into an enemy cannonball, if it does, they take damage.
        }
        if (collision.CompareTag("Health_Collectible"))
        {
            OnHeal();
            //checks if player runs into an health collectible, if it does, they heal.
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_Ships"))
        {
            OnHit();
            //checks if player runs into an enemy ship, if it does, they take damage.
        }
    }
    private void OnHit()
    {
        if (IsInvincible)
        {
            //if player has recently been hit, stops this script, so they don't take damage
            return;
        }
        --Manager_Script.Player_Health;
        OnDamaged.Invoke();
        OnHealthChanged.Invoke();
        if(Manager_Script.Player_Health==0)
        {
            Sunk();
            //if player HP is 0, trigger the game loss script
        }
        else if(Manager_Script.Player_Health==1)
        {
            //sets the sprite based on the health the player has. A lot better ways to do this than numerous IF statements, but it makes do.
            if(eeCheck==true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = eeDamagedShip;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = damagedShip;
            }
        }
        else if(Manager_Script.Player_Health==2)
        {
            if (eeCheck == true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = eeLightDamagedShip;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = lightDamagedShip;
            }
        }

    }

    private void Sunk()
    //only triggered if the players HP is 0 (game over)
    {
        if(eeCheck==true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = eeSunkShip;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sunkShip;
        }
        Vector3 ShipPos = transform.position + new Vector3(0,0,0);
        Instantiate(explosionPrefab, ShipPos, Quaternion.identity);
        //summons the explosion at the players location.
        this.GetComponent<Player_Shoot_Update>().enabled = false;
        this.GetComponent<Player_Movement_Update>().enabled = false;
        //disables both the players ability to move and shoot.
        
        Manager_Script.FinalScore = Manager_Script.Score;
        //updates the final score variable with the players current score.
        if(Manager_Script.FinalScore >= Manager_Script.HighScore)
        {
            Manager_Script.HighScore = Manager_Script.FinalScore;
        }
        Invoke(nameof(GameOverScreen),3f);
        //triggers the game over scene command after 3 seconds.
    }

    private void OnHeal()
    {
        if(Manager_Script.Player_Health == 3)
        {
            return;
            //if player already has full health, can't heal more than max
        }
        Manager_Script.Player_Health++;
        OnHealthChanged.Invoke();

        if(Manager_Script.Player_Health==2)
        {
            //Updates the players sprites based on their amount of HP, a nice visual indicator alongside health bar in UI
            if(eeCheck==true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = eeLightDamagedShip;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = lightDamagedShip;
            }
            
        }
        if(Manager_Script.Player_Health==3)
        {
            if(eeCheck==true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = eeHealthyShip;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = HealthyShip;
            }
        }
    }

    public void GameOverScreen()
    {
        //loads the Game over scene, so players can see their score, and if its the new high score.
        SceneManager.LoadScene("Game_Over_Scene");
    }
}
