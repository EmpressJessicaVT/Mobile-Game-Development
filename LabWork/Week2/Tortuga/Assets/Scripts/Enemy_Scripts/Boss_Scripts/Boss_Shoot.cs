using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss_Shoot : MonoBehaviour
{
    public GameObject Summon_Dinghy_Prefab;
    public float DinghySpawnLow = 1f;
    //how long to wait between summoning a new ship to chase the player
    public float DinghySpawnHigh = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Summon_Dinghy());
    }

    IEnumerator Summon_Dinghy()
    {
        while(true)
        {
            if(transform.position.y <=3)
            //once the player is far enough down the screeen, starts summoning rafts
            {
                yield return new WaitForSeconds(Random.Range(DinghySpawnLow, DinghySpawnHigh - Manager_Script.Boss_Ships_Sunk));
                //waits a random amount of time between summons, reducing as more bosses have been defeated
                Vector3 BossPos = transform.position + new Vector3(Random.Range(-2,2),-2,0);
                //summons the enemy randomly to the sides, slight variaty created difficulty
                Instantiate(Summon_Dinghy_Prefab, BossPos, Quaternion.AngleAxis(180, Vector3.forward));
            }
            yield return null;
        }
    }
}
