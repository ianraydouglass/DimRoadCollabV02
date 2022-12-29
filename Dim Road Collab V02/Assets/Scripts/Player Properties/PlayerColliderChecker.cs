using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderChecker : MonoBehaviour
{
    //created by Ian D. on 071222
    GameObject playerCapsule;
    // Start is called before the first frame update
    void Start()
    {
        playerCapsule = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Interactable") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            //Debug.Log("detected a collision on player");
            Rigidbody body = collision.collider.gameObject.GetComponent<Rigidbody>();
            if (body.velocity.magnitude > 0.001)
            {
                playerCapsule.GetComponent<StarterAssets.FirstPersonController>().KnockBack(collision.collider.gameObject);
            }
        }

    }
}
