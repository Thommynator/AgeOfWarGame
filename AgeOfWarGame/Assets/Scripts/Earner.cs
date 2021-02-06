using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earner : MonoBehaviour {

    private ParticleSystem[] particleSystems;
    void Awake() {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }


    public void Play() {
        foreach (ParticleSystem ps in particleSystems) {
            ps.Play();
        }
        Destroy(this.gameObject, 10);
    }

}
