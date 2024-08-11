using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostingParticles : MonoBehaviour{
    [SerializeField] Movement movement;
    [SerializeField] CollisionHandler collisionHander;
    [SerializeField] ParticleSystem boostingParticles;
    [SerializeField] Light burningLight;

    void FixedUpdate() {
        ProcessBoostingParticles();
    }
    void ProcessBoostingParticles() {
        if(movement.GetAmountOfFuel()<=0) {
            burningLight.enabled = false;
            boostingParticles.Stop();
            return;
        }
        if (collisionHander.IsTransitioning()) {
            burningLight.enabled = false;
            boostingParticles.Stop();
            return;
        }
        if (!(Input.GetButton("EngineBoost"))) {
            boostingParticles.Stop();
            burningLight.enabled = false;
            return;
        }
        boostingParticles.Play();
        burningLight.enabled = true;
    }
}
