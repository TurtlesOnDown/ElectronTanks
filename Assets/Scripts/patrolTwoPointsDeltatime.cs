using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolTwoPointsDeltatime : MonoBehaviour {

    public Vector3 PointA;
    public Vector3 PointB;
    public float duration = 1;

    private float length;

	// Use this for initialization
	void Start () {
        length = Vector3.Distance(PointA, PointB);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(PointA, PointB, Mathf.Pow((Mathf.Sin(Time.time * 1/duration) + 1) / 2, duration));
	}
}
