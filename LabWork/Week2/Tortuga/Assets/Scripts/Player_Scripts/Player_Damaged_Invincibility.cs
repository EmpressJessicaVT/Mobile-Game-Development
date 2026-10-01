using UnityEngine;

public class Player_Damaged_Invincibility : MonoBehaviour
{
    [SerializeField]
    private float invincibilityDuration;
    //Allows us to choose how long the player is invincible in the inspector
    private Player_Invincibility Player_Invincibility_Controller;

    private void Awake()
    {
        Player_Invincibility_Controller = GetComponent<Player_Invincibility>();
    }
    public void StartInvincibility()
    {
        Player_Invincibility_Controller.StartInvincibility(invincibilityDuration);
        //feeds how long the player is invincible back to the controller.
        //entire script is used to give invulnerability frames after being hit.
    }
}
