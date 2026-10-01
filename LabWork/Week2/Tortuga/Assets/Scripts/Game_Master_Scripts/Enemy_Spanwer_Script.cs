using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Spanwer_Script : MonoBehaviour
{

public GameObject[] EnemyShips;
public GameObject[] BossShips;
public static int ShipCount;
public float ShipSpawnLow = 1f;
public float ShipSpawnHigh = 3f;
//How long to wait before spawning another ship
public int MaxShips = 15;
public static int BossShipCount = 0;
public int MaxBossShips = 1;

public float maxShipWidth =9f;
public float minShipWidth = -9f;
//the space its allowed to spawn enemies within (should use screen space rather than game coords, and different screen sizes will impact this!)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Enemy_Spawn());
    }

    IEnumerator Enemy_Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(ShipSpawnLow, ShipSpawnHigh - Manager_Script.Boss_Ships_Sunk));
            //waits a random amount of time before running the script, each boss defeated speeds up spawn rate.

            if(Manager_Script.Enemy_Spawn==true)
            //only spawns enemies while true, cannot happen same time as boss fights
            {
                if(ShipCount<MaxShips)
                {
                    ShipCount++;
                    float randomX = Random.Range(minShipWidth,maxShipWidth);
                    //chooses a random number, used to choose where the enemy ship spawns
                    int EnemyShipID = Random.Range(0,EnemyShips.Length);
                    //randomly chooses which enemy ship variant to spawn, allows new variants to easily be added
                    Instantiate(EnemyShips[EnemyShipID],new Vector3 (randomX, 6, 0),Quaternion.Euler(0, 0, 0) );
                    yield return new WaitForSeconds(Random.Range(ShipSpawnLow, ShipSpawnHigh - Manager_Script.Boss_Ships_Sunk));
                    //waits a random amount of time before running the script, each boss defeated speeds up spawn rate.
                }
            }
            else if(Manager_Script.Boss_Spawn==true)
            //will only spawn a boss once enough enemies have been defeated
            {
                if(BossShipCount<MaxBossShips)
                {
                    BossShipCount++;
                    int BossShipID = Random.Range(0,BossShips.Length);
                    //allowed the creation of new boss variants
                    Instantiate(BossShips[BossShipID], new Vector3 (0,7,0), quaternion.Euler(0,0,0));
                    yield return null;
                }
                
            }
            yield return null;
        }
    }
}
