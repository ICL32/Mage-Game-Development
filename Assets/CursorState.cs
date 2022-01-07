using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    [SerializeField]
    private bool cursorLock = true;
    [SerializeField]
    private GameObject fpsCam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = !cursorLock;
            ChangeState();
        }
        
           

    }

    private void ChangeState()
    {
        if (cursorLock == true)
        {
            Cursor.lockState = CursorLockMode.Confined;            
            fpsCam.GetComponent<MouseLook>().enabled = false;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;            
            fpsCam.GetComponent<MouseLook>().enabled = true;

        }
    }
}
