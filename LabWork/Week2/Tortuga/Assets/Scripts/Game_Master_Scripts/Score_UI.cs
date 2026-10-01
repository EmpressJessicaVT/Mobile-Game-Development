using UnityEngine;
using TMPro;

public class Score_UI : MonoBehaviour
{
    public TMP_Text scoreText;
    
    public void UpdateScore(Manager_Script manager_Script)
    {
        //used to update the onscreen score
        scoreText.text = $"Score: {Manager_Script.Score}";
    }
}
