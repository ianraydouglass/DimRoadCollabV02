using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public int Level;
    public void LoadLevel() 
    {
        SceneManager.LoadScene(Level);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        return;
#else
        Application.Quit();
#endif
    }
}
