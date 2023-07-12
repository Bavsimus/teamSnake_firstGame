using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healtBarScript : MonoBehaviour
{
    mechanics mechanics;
    public Animator animator;
    public enum hAnim { Full, Four, Three, Two, One, Dead};
    hAnim hState;
    
    // Start is called before the first frame update
    void Start()
    {
        mechanics = GetComponent<mechanics>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        hAnimations();
    }
    void hAnimations() {
        if (mechanics.health == 5){
            hState = hAnim.Full;
        }
        else if (mechanics.health == 4) {
            animator.SetTrigger("firstHurt");
            hState = hAnim.Four;
        }
        else if (mechanics.health == 3)
        {
            hState = hAnim.Three;
        }
        else if (mechanics.health == 2)
        {
            hState = hAnim.Two;
        }
        else if (mechanics.health == 1)
        {
            hState = hAnim.One;
        }
        else if (mechanics.health == 0)
        {
            hState = hAnim.Dead;
        }
    }
}
