using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public GameObject pullTarget;
    private bool pulling = false;
    void OnTriggerEnter(Collider collider)
    {
        pulling = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pulling)
            pullTarget.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,100));
    }
}
