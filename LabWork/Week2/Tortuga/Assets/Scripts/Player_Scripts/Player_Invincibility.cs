using System.Collections;
using UnityEngine;

public class Player_Invincibility : MonoBehaviour
{
    private Player_Health player_Health;

    private void Awake()
    {
        //when scene is loaded, checks the players health.
        player_Health = GetComponent<Player_Health>();
    }

    public void StartInvincibility(float invincibiltyDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invincibiltyDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invincibiltyDuration)
    {
        //sets the "is invincible" to true, used in the health screen, then waits the duration of invincibility before disabling itself.
        player_Health.IsInvincible = true;
        yield return new WaitForSeconds(invincibiltyDuration);
        player_Health.IsInvincible = false;
    }
}
