using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper: MonoBehaviour
{
	public float speed = 0.01f;
	public Color startColor;
	public Color endColor;
	public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float t = (CollisionBehavior.currentScore) * speed;
        cam.backgroundColor = Color.Lerp(startColor,endColor,t);
    }
}
