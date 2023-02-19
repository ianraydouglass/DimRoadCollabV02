using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class EndingManager : MonoBehaviour
{
    public Animator endingAnimator;
    public TextMeshProUGUI endingText;
    public CanvasGroup endingGroup;
    public GameEvent endEvent;
    [TextArea(15, 20)]
    public string GameOver = "";
    [TextArea(15, 20)]
    public string Ending1 = "";
    [TextArea(15, 20)]
    public string Ending2 = "";
    [TextArea(15, 20)]
    public string Ending3 = "";

    public void SetEnding(int endingCode)
    {
        if (endingCode == 0)
        {
            endingText.text = GameOver;
        }
        if (endingCode == 1)
        {
            endingText.text = Ending1;
        }
        if (endingCode == 2)
        {
            endingText.text = Ending2;
        }
        if (endingCode == 3)
        {
            endingText.text = Ending3;
        }
        if (endingCode >= 4)
        {
            endingText.text = GameOver;
        }
    }

    public void TrueEnding()
    {
        endEvent.Raise();
        endingAnimator.Play("endStart");

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        return;
#else
        Application.Quit();
#endif
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
