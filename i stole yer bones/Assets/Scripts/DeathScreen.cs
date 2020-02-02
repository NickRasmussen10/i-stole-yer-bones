using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] Text scoreText;
    Score score;
    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
        scoreText.text = "score: " + score.score;
    }
}
