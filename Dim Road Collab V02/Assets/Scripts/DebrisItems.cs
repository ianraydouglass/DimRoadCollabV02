using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisItems : MonoBehaviour
{
    public float positionOffset = 1f;
    public float itemSpacing = 0.125f;
    public List<GameItem> items = new List<GameItem>();
    public GameObject itemHolder;
    public void ReleaseItems(Vector3 cubePosition)
    {
        if(items.Count == 0)
        {
            return;
        }
        Vector3 releasePosition = cubePosition;
        float hostPosition = releasePosition.y;
        releasePosition.y = (hostPosition + positionOffset);
        Vector3 dropPosition = releasePosition;
        foreach(GameItem item in items)
        {
            GameObject i = Instantiate(itemHolder, dropPosition, transform.rotation);
            ItemObject itemDropping = i.GetComponent<ItemObject>();
            itemDropping.gameItem = item;
            itemDropping.ChangeSprite();
            float horizontalSpacing = dropPosition.x + itemSpacing;
            dropPosition.x = horizontalSpacing;
        }

    }
}
