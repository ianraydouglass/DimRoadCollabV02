using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkManager : MonoBehaviour
{
    public BonkBox standBonk;
    public BonkBox crouchBonk;
    public BonkBox holdBonk;
    public BonkBox stowBonk;
    // Start is called before the first frame update
    
    public bool StandClear()
    {
        return standBonk.IsClear();
    }
    public bool CrouchClear()
    {
        return crouchBonk.IsClear();

    }
    public bool HoldClear()
    {
        return holdBonk.IsClear();
    }
    public bool StowClear()
    {
        return stowBonk.IsClear();
    }
}
