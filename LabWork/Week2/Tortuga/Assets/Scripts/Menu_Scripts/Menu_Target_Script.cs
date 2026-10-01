using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Menu_Target_Script : MonoBehaviour
{
    public int Enemy_Target_Health = 3;
    public UnityEvent OnSelected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cannonBall_Player"))
        {
            //checks if its being hit by the players cannonball, so nothing else could trigger it (e.g. being rammed by player)
             OnHit();
        }
    }

    private void OnHit()
    {
        --Enemy_Target_Health;
        if(Enemy_Target_Health==0)
            {
                //once a sign has been hit 3 times, trigger its connected script
                Sunk();
            }
    }

    private void Sunk()
    {
        OnSelected.Invoke();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game_Scene");
        //loads the game screen, where our game actually takes place
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
            //used to quit the game, and allowed me to quit out the editor to prove it was working during testing
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
        //used to navigate from game over screen, back to main menu, where a player could play again or quit
    }

    public void EasterEgg()
    {
        Debug.Log("activate easter egg");
        Manager_Script.Easter_Egg = true;
        //If activated turns on the easter eggs, being updated player sprites and music
    }
}
