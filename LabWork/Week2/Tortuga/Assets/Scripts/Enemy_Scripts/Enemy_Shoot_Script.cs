using System.Collections;
using UnityEngine;

public class Enemy_Shoot_Script : MonoBehaviour
{
    public GameObject EnemyCannonballPrefab;
    public GameObject[] Cannonballs;
    public static int CannonballCount;
    public float CannonballSpawnLow = 1f;
    public float CannonballSpawnHigh = 2f;
    public int MaxCannonballs = 10;
    public AudioClip cannonFireSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Enemy_Fire());
    }

    IEnumerator Enemy_Fire()
    {
        while (transform.position.y > 0)
        //if enemy goes past the midway point, stop shooting. Means player cannot be shot in the back
        {
            yield return new WaitForSeconds(Random.Range(CannonballSpawnLow, CannonballSpawnHigh));
            //waits random amount of time before firing shots
            if (CannonballCount < MaxCannonballs)
            //stops screen becoming full of cannonballs
            {
                Instantiate(EnemyCannonballPrefab,transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(cannonFireSound, new Vector3(0,0,0));
                yield return new WaitForSeconds(Random.Range(CannonballSpawnLow, CannonballSpawnHigh));
                //waits random amount of time before firing shots
                CannonballCount++;
            }
            yield return null;
        }   
    }
    // Update is called once per frame
    void Update()
    {
        Cannonballs = GameObject.FindGameObjectsWithTag("CannonBall_Enemy");
        CannonballCount = Cannonballs.Length;

        if(transform.position.y > 0)
        {
            //if enemy goes past the midway point, stop shooting. Means player cannot be shot in the back
            StopCoroutine(Enemy_Fire());
        }
    }
}
