using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float TimeDestroy = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(TimeDestroy);
        Destroy(gameObject);
    }

}
    
