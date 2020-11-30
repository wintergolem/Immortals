using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorParticleBounce : IndicatorBase
{
    public DaggerBounce bounce;
    public ParticleSystem particle;

    public override void StartAnim()
    {
        bounce.StartBounce();
        particle.Stop();
        particle.Play();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
