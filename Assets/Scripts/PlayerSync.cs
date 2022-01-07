using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
public class PlayerSync : NetworkBehaviour
{
    
    [SerializeField]
    private GameObject fpsCam;
    // Start is called before the first frame update
   


    // Update is called once per frame
    void Start()
    {
        if (!isLocalPlayer)
        {
            
            gameObject.GetComponent<CastScript>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = false;             
            fpsCam.SetActive(false);
            Debug.LogError("This isn't the client's player");

        }
        return;
    }
}
