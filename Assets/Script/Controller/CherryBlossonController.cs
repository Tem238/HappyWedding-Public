using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBlossonController : MonoBehaviour
{
    public void updateParticle(float totalPoint)
    {
        if(totalPoint > 5000)
        {
            totalPoint = 5000;
        }
        if (totalPoint > 2500)
        {
            ParticleSystem.TextureSheetAnimationModule texSheetAnim = gameObject.GetComponent<ParticleSystem>().textureSheetAnimation;
            texSheetAnim.rowMode = ParticleSystemAnimationRowMode.Random;
        }
        ParticleSystem.EmissionModule emission = gameObject.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(totalPoint / 1000);
    }
}
