using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Summon_Player_Detect : MonoBehaviour
{
    public bool AwareOfPlayer {get; private set;}
    //used to track player, allowed player to escape detection zone
    public Vector2 DirectionToPlayer {get; private set;}
    [SerializeField]
    private float playerAwarenessDistance;
    private Transform playerLocation;

    private void Awake()
    {
        playerLocation = FindFirstObjectByType<Player_Movement_Update>().transform;
        //when first loaded in, finds object that has the player movement script (the player)
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = playerLocation.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        //script is used to track player, if the player is within distance, it would give chase, otherwise sail straight forward

        if(enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
