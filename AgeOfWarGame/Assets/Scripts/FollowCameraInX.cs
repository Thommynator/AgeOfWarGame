using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraInX : MonoBehaviour {

    private GameObject mainCamera;

    void Start() {
        this.mainCamera = GameObject.Find("Main Camera");
    }

    void Update() {
        this.transform.position = new Vector3(this.mainCamera.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
