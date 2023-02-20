using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SegmentState { None, Intact, toDamaged, Damaged, toDoomed, Doomed, toCollapsed, Collapsed, Remnant}

public class SegmentStability : MonoBehaviour
{
    public Animator animator;
    //public float stability;
    public SegmentState segmentState = SegmentState.Intact;
    private SegmentState pendingState = SegmentState.None;
    public bool isTesting;
    [Range(0, 3)]
    public int damageLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isTesting == true)
        {
            animator.SetBool("Testing", true);
        }
    }
    public void SetDamageLevel(int dLevel)
    {
        if (dLevel <= damageLevel)
        {
            return;
        }
        if (dLevel > damageLevel)
        {
            damageLevel = dLevel;
            TrueBreak();
        }
    }

    public void TrueBreak()
    {
        animator.SetInteger("DamageLevel", damageLevel);
        BroadcastMessage("Damage", damageLevel, SendMessageOptions.DontRequireReceiver);
    }

   

    public void TestBreak()
    {
        if (segmentState == SegmentState.Intact)
        {
            segmentState = SegmentState.toDamaged;
            animator.SetInteger("DamageLevel", 1);
            BroadcastMessage("Damage", 1);
            return;
        }
        if (segmentState == SegmentState.toDamaged)
        {
            pendingState = SegmentState.toDoomed;
            return;
        }
        if (segmentState == SegmentState.Damaged)
        {
            segmentState = SegmentState.toDoomed;
            animator.SetInteger("DamageLevel", 2);
            BroadcastMessage("Damage", 2);
            return;
        }
        if (segmentState == SegmentState.toDoomed)
        {
            pendingState = SegmentState.toCollapsed;
            return;
        }
        if (segmentState == SegmentState.Doomed)
        {
            segmentState = SegmentState.toCollapsed;
            animator.SetInteger("DamageLevel", 3);
            BroadcastMessage("Damage", 3);
            return;
        }
        if (segmentState == SegmentState.toCollapsed)
        {
            pendingState = SegmentState.None;
            return;
        }
        if (segmentState == SegmentState.Collapsed)
        {
            //remove this part when I'm no longer testing
            segmentState = SegmentState.Intact;
            animator.SetInteger("DamageLevel", 0);
            BroadcastMessage("Damage", 0);
            return;
        }
        if (segmentState == SegmentState.Remnant)
        {

        }
    }

    public void BreakComplete()
    {
        if (!isTesting)
        {
            return;
        }
        if (segmentState == SegmentState.toDamaged)
        {
            if (pendingState == SegmentState.toDoomed)
            {
                segmentState = SegmentState.toDoomed;
                animator.SetInteger("DamageLevel", 2);
                BroadcastMessage("Damage", 2);
                return;
            }
            else
            {
                segmentState = SegmentState.Damaged;
                return;
            }
        }
        if (segmentState == SegmentState.toDoomed)
        {
            if (pendingState == SegmentState.toCollapsed)
            {
                segmentState = SegmentState.toCollapsed;
                animator.SetInteger("DamageLevel", 3);
                BroadcastMessage("Damage", 3);
                return;
            }
            else
            {
                segmentState = SegmentState.Doomed;
                return;
            }
            
        }
        if (segmentState == SegmentState.toCollapsed)
        {
            segmentState = SegmentState.Collapsed;
            return;
        }
        else
        {
            Debug.Log(this.gameObject + " corridor was told to complete a break that wasn't expected");
        }
    }
}
