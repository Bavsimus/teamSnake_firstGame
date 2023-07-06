using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class glitchWhenNear : MonoBehaviour
{
    [SerializeField]public DigitalGlitch GlitchFX;
    public AnalogGlitch GlitchFXA;
    public float Intensity;
    private void OnTriggerStay2D(Collider2D collider)
    {
        GlitchFX.enabled = false;
        GlitchFXA.enabled = false;

        if (collider.tag == "Player")
        {
            Vector2 distanceVector = collider.transform.position - transform.position;
            GlitchFX.enabled = true;
            GlitchFXA.enabled = true;
            GlitchFX.intensity = (Intensity / distanceVector.magnitude) / 10;
            GlitchFXA.scanLineJitter = (Intensity / distanceVector.magnitude) / 100;
            GlitchFXA.verticalJump = (Intensity / distanceVector.magnitude) / 100;
            GlitchFXA.horizontalShake = (Intensity / distanceVector.magnitude) / 100;
            GlitchFXA.colorDrift = (Intensity / distanceVector.magnitude) / 100;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Vector2 distanceVector = collider.transform.position - transform.position;
            GlitchFX.enabled = false;
            GlitchFXA.enabled = false;
        }
    }
}
