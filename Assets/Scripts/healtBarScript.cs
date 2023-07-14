using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healtBarScript : MonoBehaviour
{
    public mechanics mechanics;
    public Animator animator;
    public enum hAnim { Full, Four, Three, Two, One, Dead};
    hAnim hState;
    
    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
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
            animator.SetTrigger("secondHurt");
            hState = hAnim.Three;
        }
        else if (mechanics.health == 2)
        {
            animator.SetTrigger("thirdHurt");
            hState = hAnim.Two;
        }
        else if (mechanics.health == 1)
        {
            animator.SetTrigger("fourthHurt");
            hState = hAnim.One;
        }
        else if (mechanics.health == 0)
        {
            animator.SetTrigger("deadlyHurt");
            hState = hAnim.Dead;
        }
        animator.SetInteger("hState", (int)hState);
    }
}
