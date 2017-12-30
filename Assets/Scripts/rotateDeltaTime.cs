using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateDeltaTime : MonoBehaviour {

    public float duration = 3;

    private Transform position;

	// Use this for initialization
	void Start () {
        position = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles = new Vector3(0, 0, Time.time * duration);
	}
}