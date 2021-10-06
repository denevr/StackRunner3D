using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMPro.TextMeshProUGUI>();
        GameManager.OnCubeSpawned += UpdateScoreboard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= UpdateScoreboard;
    }

    private void UpdateScoreboard()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
