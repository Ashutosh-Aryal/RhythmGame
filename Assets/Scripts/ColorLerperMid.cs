using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerperMid : MonoBehaviour
{
	public float speed = 0.01f;
	public Color startColor;
	public Color endColor;
	private Material materialColored;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    	float t = (CollisionBehavior.currentScore) * speed;
    	materialColored.color = Color.Lerp(startColor,endColor,t);
        this.GetComponent<Renderer>().material = materialColored;
    }
}
