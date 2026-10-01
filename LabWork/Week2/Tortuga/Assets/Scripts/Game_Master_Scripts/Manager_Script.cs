using UnityEngine;
using UnityEngine.Events;

public class Manager_Script : MonoBehaviour
{
    public AudioSource Blackmoor;
    public AudioSource EasterEgg;
    public static int Score;
    public static int FinalScore;
    public static int HighScore;
    public static int Player_Health;
    public static int Enemy_Ships_Sunk;
    public static bool Enemy_Spawn;
    public static bool Boss_Spawn;
    public static bool Easter_Egg;
    public static int Boss_Ships_Sunk;
    public UnityEvent OnScoreChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player_Health = 3;
        //when game first starts, sets the player health to full, and all other variables to 0 (restarts the game fresh)
        Score = 0;
        Enemy_Ships_Sunk = 0;
        Boss_Ships_Sunk = 0;
        Enemy_Spawn = true;
        Boss_Spawn = false;
    }
    void Awake()
    {
        //when script is loaded into each scene, checks if the player has activated easter egg mode
        if(Easter_Egg==true)
        {
            EasterEgg.Play();
        }
        else
        {
            Blackmoor.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {   
        OnScoreChanged.Invoke();
        //each frame updates the score on screen (Could have been better done each action a score could be updated, rather than frame)
        if(Enemy_Ships_Sunk >= 10 + (Boss_Ships_Sunk*5))
        //after enough enemies have been defeated, activates boss wave, increases amount required after each boss has been killed
        {
            Enemy_Spawn = false;
            Boss_Spawn = true;
        } 
    }
}
