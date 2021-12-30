using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
public class PlayerSync : NetworkBehaviour
{
    [SerializeField]
    private CharacterController Controller;
    [SerializeField]
    public GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {

        if (!hasAuthority)
        {
            Destroy(Controller);
            gameObject.GetComponent<CastScript>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<MouseLook>().enabled = false;
            playerCamera.SetActive(false);
            Debug.LogError("This isn't the client's player");
            
        }
        return;

       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
