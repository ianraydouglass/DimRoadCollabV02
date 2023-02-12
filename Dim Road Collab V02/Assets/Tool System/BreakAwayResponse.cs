using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAwayResponse : MonoBehaviour
{

    public string bustKeyphrase = "spent";
    private Rigidbody body;
    public GameObject breakTarget;
    public float forceFactor = 3f;



    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    public void PerformResponse(string keyPhrase)
    {
        if (keyPhrase != bustKeyphrase)
        {
            return;
        }
        body.useGravity = true;
        //12 is phased, won't collide with player
        gameObject.layer = 12;
        body.AddForce((breakTarget.transform.position - transform.position) * forceFactor);
        body.AddTorque((breakTarget.transform.position - transform.position) * (forceFactor * 10));
    }
}
