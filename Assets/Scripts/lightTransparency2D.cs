using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTransparency2D : MonoBehaviour {

    private Material thisMat;
    private Color baseColor;
    private Color tintedColor;

	// Use this for initialization
	void Start () {
        thisMat = GetComponent<MeshRenderer>().material;
        baseColor = thisMat.color;
	}

    void LateUpdate()
    {
        thisMat.SetColor("_Color", tintedColor);
        tintedColor = baseColor;
    }

    public void updateColor(Color col, float intensity)
    {
        Color newColor = thisMat.color;
        //newColor *= col;
        newColor.r += (col.r * intensity);
        Debug.Log(col.r);
        newColor.g += (col.g * intensity);
        newColor.b += (col.b * intensity);
        tintedColor = newColor;
    }
}
