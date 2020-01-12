using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acclerator : MonoBehaviour
{
    public GameObject target;
    void OnTriggerEnter(Collider collider)
    {
        target.GetComponent<Rigidbody>().velocity *= 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
