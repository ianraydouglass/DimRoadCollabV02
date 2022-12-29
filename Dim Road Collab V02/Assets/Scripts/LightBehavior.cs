using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public Animator animator;
    public string flickerAnimation;
    public Light thisLight;
    public bool damaged;
    public bool shook;
    public float startingRange;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startingRange = thisLight.range;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value > 0.9 && damaged == true) //a random chance
        {
            if (thisLight.range > 0) //if the light is on...
            {
                thisLight.range = 0; //turn it off
            }
            else
            {
                thisLight.range = startingRange; //turn it on
            }
        }
        else if (Random.value > 0.9 && shook == true) //a random chance
        {
            if (thisLight.range > 0) //if the light is on...
            {
                thisLight.range = 0; //turn it off
            }
            else
            {
                thisLight.range = startingRange; //turn it on
            }
        }
    }

    public void Damage(int damageLevel)
    {
        if (damageLevel >= 3)
        {
            thisLight.range = 0;
        }
        if (damageLevel == 0)
        {
            damaged = false;
            thisLight.range = startingRange;
        }
        else
        {
            
            shook = true;
            StartCoroutine(FlickerDelay());
        }
        
    }

    IEnumerator FlickerDelay()
    {
        
        yield return new WaitForSeconds(1);

        shook = false;
        thisLight.range = startingRange;
    }
}
