using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmissionModule = UnityEngine.ParticleSystem.EmissionModule;

public class GrowingCircleAnimation : MonoBehaviour
{
    private ParticleSystem particle;
    private ParticleSystem.EmissionModule emission;

    // Update is called once per frame
    void Update ()
    {
        particle = GetComponent<ParticleSystem> ();
        emission = particle.emission;
        particle.Stop ();
        emission.enabled = false;
    }

    public void Trigger ()
    {
        particle.Emit (1);
    }
}
