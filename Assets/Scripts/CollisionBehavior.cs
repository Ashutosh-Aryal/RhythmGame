using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionBehavior : MonoBehaviour
{
    [SerializeField] Slider m_ScoreSlider;

    public static int currentScore = 0;

    private void Start()
    {
        m_ScoreSlider.minValue = 0;
        m_ScoreSlider.maxValue = CenterThing.MAX_POINTS;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("greenPellet"))
        {
            Destroy(collision.gameObject);
            currentScore++;
            m_ScoreSlider.value = currentScore;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("greenPellet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
