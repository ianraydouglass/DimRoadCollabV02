using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isCube;
    public bool isButton;
    public bool isSifted;
    public bool isDebris;
    public bool isAnchored;
    public int breakLevel = 1;
    public Rigidbody body;
    public GameEvent simpleInteraction;
    public Collider thisCollider;
    public GameObject siftedVersion;
    public GameObject packedVersion;
    public GameObject offsetReference;
    public Vector3 offsetValue;
    public Sprite objectIcon;
    
    void Start()
    {
        if (offsetReference != null)
        {
            offsetValue = offsetReference.transform.localPosition * -1;
        }
    }

    public void LiftThis()
    {
        if (body != null)
        {
            this.body.useGravity = false;
            this.body.freezeRotation = true;
            //10 is the carried object layer
            this.gameObject.layer = 10;
        }
        else
        {
            Debug.Log(this.gameObject + "needs a rigid body reference to be lifted");
        }
        
    }

    public void DropThis()
    {
        if (body != null)
        {
            this.body.useGravity = true;
            this.body.freezeRotation = false;
            //this.body.freezePosition = false;
            //9 is the interactable layer
            this.gameObject.layer = 9;
        }
        else
        {
            Debug.Log(this.gameObject + "needs a rigid body reference to be dropped");
        }
    }

    public void SimpleInteract()
    {
        if (simpleInteraction != null)
        {
            simpleInteraction.Raise();
        }
        else
        {
            Debug.Log(this.gameObject + "needs an event reference to do a simple interaction");
        }
    }

    public void SiftThis()
    {
        if (body != null && thisCollider != null && siftedVersion != null)
        {
            this.body.useGravity = false;
            this.body.freezeRotation = true;
            thisCollider.enabled = false;
            Instantiate(siftedVersion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }


        else
        {
            Debug.Log(this.gameObject + "needs a rigid body, sifted verison, and box collider reference to be lifted");
        }
    }

    public void PackThis()
    {
        if (body != null && thisCollider != null && packedVersion != null)
        {
            this.body.useGravity = false;
            this.body.freezeRotation = true;
            thisCollider.enabled = false;
            Instantiate(packedVersion, this.transform.position, this.transform.rotation);
            Destroy(this.transform.parent.gameObject);
        }
        else
        {
            Debug.Log(this.gameObject + "needs a rigid body, packed verison, and box collider reference to be lifted");
        }

    }

    public void Damage(int damageLevel)
    {
        if (damageLevel >= breakLevel && isAnchored == true)
        {
            this.transform.parent = null;
            thisCollider.enabled = true;
            body.constraints = RigidbodyConstraints.None;
            DropThis();
        }
    }
}
