using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCardHolder : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject cardContents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayItemInfo(CraftingRecipe recipe)
    {
        cardContents.SetActive(true);
        OutputType o = recipe.GetOutputType();
        if (o == OutputType.Item)
        {
            itemImage.sprite = recipe.GetOutputItem().GetSprite();
            titleText.text = recipe.GetOutputItem().GetName();
        }
        if (o == OutputType.Tool)
        {
            itemImage.sprite = recipe.GetOutputTool().GetSprite();
            titleText.text = recipe.GetOutputTool().GetName();
        }
            
        descriptionText.text = recipe.GetDescription();

        //descriptionText.text = item.TellDescription();
    }

    public void DisplayItemObjectInfo(GameItem item)
    {
        cardContents.SetActive(true);
        itemImage.sprite = item.GetSprite();
        titleText.text = item.GetName();
        string t = item.GetDescription();
        t += "\n" + item.TellDescription();
        descriptionText.text = t;
    }

    public void DisplayPartInfo(GamePart part)
    {
        cardContents.SetActive(true);
        itemImage.sprite = part.GetSprite();
        titleText.text = part.GetName();
        descriptionText.text = part.TellDescription();
    }

    public void HideCardContents()
    {
        cardContents.SetActive(false);
    }
}
