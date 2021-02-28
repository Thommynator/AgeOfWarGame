using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleShake : MonoBehaviour {

    public float duration;
    public float rotationAngle;

    // Start is called before the first frame update
    void Start() {
        LeanTween.scale(this.gameObject, 1.1f * Vector3.one, duration).setIgnoreTimeScale(true).setLoopPingPong();
        LeanTween.rotateZ(this.gameObject, this.rotationAngle, 2 * duration).setIgnoreTimeScale(true).setLoopPingPong();
    }

    // Update is called once per frame
    void Update() {

    }
}
