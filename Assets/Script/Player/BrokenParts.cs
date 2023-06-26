using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrokenParts : DestroyInSeconds
{
    Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = UnityEngine.Random.onUnitSphere;
        dir.y = Mathf.Abs(dir.y);
        dir.z *= 0.5f;
        rb.angularVelocity = dir;
        rb.AddForce(dir*20, ForceMode.Impulse);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
