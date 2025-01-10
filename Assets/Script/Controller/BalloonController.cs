using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] int startPoint = 3000;
    public void updateParticle(float totalPoint)
    {
        totalPoint -= startPoint;
        if (totalPoint > startPoint)
        {
            if (totalPoint > 3000)
            {
                totalPoint = 3000;
            }
            ParticleSystem.EmissionModule emission = gameObject.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(totalPoint / 3000);
        }
    }
}
