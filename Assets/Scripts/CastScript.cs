using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class CastScript : NetworkBehaviour
{
    //projectile 
    public GameObject bullet;

    //projectile force
    [SerializeField] [SyncVar]   
    private float shootForce = 30f;
    [SerializeField] [SyncVar]
    private float upwardForce = 0f;

    //Cast stats
    [SyncVar]
    [SerializeField]
    private float timeBetweenCast = 2f;
    [SerializeField] [SyncVar]   
    private float spread = 1f;
    [SerializeField]
    [SyncVar]
    private float timeBetweenShots = 2f;
    [SerializeField]
    [SyncVar]
    private bool allowButtonHold = false;
    [SerializeField]
    [SyncVar]
    private float TimeDestroy = 5;

   

    //Recoil
    
    public Rigidbody playerRb;
    [SyncVar]
    public float recoilForce;

    //bools
    [SerializeField]
    [SyncVar]
    bool casting, readyToCast = true;

    //Reference
    [SerializeField]
    private Camera fpsCam;
    public Transform attackPoint;
    private bool debugged;


    private void Start()
    {
        
    }

    
    private void Awake()
    {
        
        if (!debugged)
        {


            if (!hasAuthority)
            {
                Debug.Log("You don't have authority");
            }
            debugged = true;
            return;
           
        }
        
        
    }

    
    private void Update()
    {
        MyInput();

 
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) casting = Input.GetKey(KeyCode.Mouse0);
        else casting = Input.GetKeyDown(KeyCode.Mouse0);

        if (casting)
        {
            CmdBeginCasting();
        }

      
    }

    [Command]
    private void CmdBeginCasting()
    {
        //Shooting
        if (readyToCast)
        {
            Cast();
        }
        else
        {
                Debug.LogError("Not ready to cast: " + casting + readyToCast);
        }
    }

    [ClientRpc]
    private void Cast()
    {
        Debug.Log("Casting");
        readyToCast = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Destroy bullet after time
        Destroy(currentBullet, TimeDestroy);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;
       

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);


        StartCoroutine(CastDelay());

        IEnumerator CastDelay()
        {
            yield return new WaitForSecondsRealtime(timeBetweenCast);
            readyToCast = true;
        }
    }

   
}