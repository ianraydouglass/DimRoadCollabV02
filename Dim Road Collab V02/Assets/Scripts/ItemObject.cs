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

}
