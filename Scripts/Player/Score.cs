using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    public GameObject player;
    public TextMeshProUGUI scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "ScorE : " + score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        


    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = "ScorE : " + score.ToString();
    }
}
