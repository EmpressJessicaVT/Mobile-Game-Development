using UnityEngine;

public class Health_Bar_UI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image healthBarSailAmount;
    public Sprite FullHealth;
    public Sprite LightyDamaged;
    public Sprite Damaged;
    public Sprite Destroyed;
    //the various health states the player could have
public void UpdateHealthBar(Player_Health player_Health)
    {
        if(Manager_Script.Player_Health ==3)
        {
            healthBarSailAmount.sprite = FullHealth;
        }
        else if(Manager_Script.Player_Health == 2)
        {
            healthBarSailAmount.sprite = LightyDamaged;
        }
        else if(Manager_Script.Player_Health == 1)
        {
            healthBarSailAmount.sprite = Damaged;
        }
        else if(Manager_Script.Player_Health == 0)
        {
            healthBarSailAmount.sprite = Destroyed;
        }
        else return;
    }
}
