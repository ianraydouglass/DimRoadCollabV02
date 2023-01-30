using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundCubePuzzle : MonoBehaviour
{
    public List<CubePlacementPuzzle> cubePuzzles = new List<CubePlacementPuzzle>();
    public string keyPhrase;
    public string unKeyPhrase;

    public void CheckPuzzleConditions()
    {
        foreach(CubePlacementPuzzle puzzle in cubePuzzles)
        {
            if (!puzzle.IsSatesfied())
            {
                PuzzleReverse();
                return;
            }
        }
        PuzzleComplete();
    }

    public void PuzzleComplete()
    {
        BroadcastMessage("PerformResponse", keyPhrase);
    }

    public void PuzzleReverse()
    {
        BroadcastMessage("PerformResponse", unKeyPhrase);
    }

}
