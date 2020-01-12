using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;
    public GameObject[] wires;
    private bool shooting = false;
    Animator ga;
    private int timeStep = 10;
    private int nowTime = 0;
    private int spawnCount = 0;
    private int spwanTotal = 20;
    void OnTriggerEnter(Collider collider)
    {
        ga.Play("MachineGin_shoot");

        shooting = true;
        GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        for(int i=0;i<wires.Length;i++)
        {
            wires[i].GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ga = gun.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            nowTime++;
            if (nowTime > timeStep)
            {
                if (spawnCount < spwanTotal)
                {
                    ga.Play("MachineGin_shoot");
                    spawnCount++;
                    GameObject shooted = Instantiate(bullet, new Vector3(1.2f, 1.2f, 46.5f), Quaternion.identity);
                    shooted.GetComponent<Rigidbody>().AddForce(new Vector3(-12, 5.4f, 0), ForceMode.VelocityChange);
                    nowTime = 0;
                }
                else
                {
                    ga.enabled = false;
                    shooting = false;
                }
            }
        }
    }
}
