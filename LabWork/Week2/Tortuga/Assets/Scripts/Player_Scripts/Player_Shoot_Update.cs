using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Shoot_Update : MonoBehaviour
{
    [SerializeField]
    private GameObject CannonballPrefab;
    public GameObject CannonFirePrefab;
    private bool fireContinuous = false;
    private bool fireSingle = false;
    //Used as checks to see if button is being pressed VS held down. Feels a lot nicer to be able to hold down fire.
    [SerializeField]
    private Transform cannonOffset;
    [SerializeField]
    private float timeBetweenCannons;
    //Used as a fire rate mechanism
    private float lastCannonTime;
    private float cannonDoubleDuration = 10f;
    //how long the fire rate pick up lasts
    public AudioClip cannonFireSound;


    // Update is called once per frame
    void Update()
    {
        if(fireContinuous || fireSingle)
        {
            float timeSinceLastCannon = Time.time - lastCannonTime;
            if(timeSinceLastCannon >= timeBetweenCannons)
            {
                FireCannon();
                //ensures shots cannot be spammed as fast as player can press. allows a nice consisten fire rate

                lastCannonTime = Time.time;
                fireSingle = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cannon_Collectible"))
        {
            RapidFire();
            //if the player hits the fire rate power up, activates the ability
        }
    }
    private void FireCannon()
    {
        Instantiate(CannonFirePrefab, cannonOffset.position, Quaternion.identity);
        Instantiate(CannonballPrefab, cannonOffset.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(cannonFireSound, new Vector3(0,0,0));
        //summons the players cannonball, the fire effect, and plays the cannon fire sound
    }
    private void OnAttack(InputValue inputValue)
    {
        //part of the input system, feeds in the value from keyboard keys and controller.
        fireContinuous = inputValue.isPressed;
        if(inputValue.isPressed)
        {
            fireSingle = true;
        }
    }
    private void RapidFire()
    {
        StartCoroutine(DoubleCannon());
    }
    private IEnumerator DoubleCannon()
    {
        //during duration halfs the time between shots (increased fire rate) and disables after the duration.
        timeBetweenCannons = timeBetweenCannons/2;
        yield return new WaitForSeconds(cannonDoubleDuration);
        timeBetweenCannons = timeBetweenCannons*2;

    }
}
