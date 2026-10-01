using UnityEngine;
using TMPro;

public class Game_Over_Script : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    void Awake()
    {
        //used on the Game over screen to set the text to the players scores
        finalScoreText.text = $"Score: {Manager_Script.FinalScore}";
        highScoreText.text = $"High Score: {Manager_Script.HighScore}";
    }
}
