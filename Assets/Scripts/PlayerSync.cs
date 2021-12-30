using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
public class PlayerSync : NetworkBehaviour
{
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {

        if (!isLocalPlayer);
        {
            Destroy(GetComponent<CharacterController>());
            Destroy(GetComponent<CastScript>());
            Destroy(GetComponent<MouseLook>());
            Debug.LogError("This isn't the server's player");
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
