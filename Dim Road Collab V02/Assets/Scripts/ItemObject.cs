using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public GameObject spritePivot;
    public SpriteRenderer displaySprite;
    private Camera mainCamera;
    public GameItem gameItem;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (gameItem)
        {
            displaySprite.sprite = gameItem.GetSprite();
        }

    }

    public void ChangeSprite()
    {
        if (gameItem)
        {
            displaySprite.sprite = gameItem.GetSprite();
        }
    }

    void LateUpdate()
    {
        if (spritePivot)
        {
            spritePivot.transform.LookAt(mainCamera.transform);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }
        Debug.Log("Collided with item object");
        ItemDirector director = collision.gameObject.GetComponent<ItemDirector>();
        if (director.CheckCapacity(gameItem.GetBulk()))
        {
            director.CollectItem(gameItem);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider colliderInfo)
    {
        if (colliderInfo.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }
        Debug.Log("Collided with item object");
        ItemDirector director = colliderInfo.gameObject.GetComponent<ItemDirector>();
        if (director.CheckCapacity(gameItem.GetBulk()))
        {
            director.CollectItem(gameItem);
            Destroy(this.gameObject);
        }
    }

}
