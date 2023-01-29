using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolHUDManager : MonoBehaviour
{
    public GameObject toolHud;
    public Image toolImage;
    public TextMeshProUGUI toolNameText;
    // Start is called before the first frame update
    
    public void EquipTool(ToolItem tool)
    {
        toolHud.SetActive(true);
        toolImage.sprite = tool.GetSprite();
        toolNameText.text = tool.GetName();
    }
    public void UnEquipTool()
    {
        toolHud.SetActive(false);
    }

}
