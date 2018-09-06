using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUIScript : MonoBehaviour {

    public Sprite currentSprite, answerSprite;
    public Text text;
    public int QuizLevel;
    int currentQuizLevel;
    private string[] quizString = { "Question: ", "السؤال رقم1", "Frage: ", "Вопрос: ", "Question: ", "Pregunta: ", "Domanda: ", "Câu hỏi: " };
    private int[] reward = { 0, 200, 400, 600, 800, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000 };
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("Language") == 1)
        {
            text.text = reward[QuizLevel].ToString();
        }
        else
        {
            text.text = reward[QuizLevel].ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
        currentQuizLevel = GameObject.Find("Quiz").GetComponent<Quiz>().id;
        if(QuizLevel< currentQuizLevel)
        {
            this.gameObject.GetComponent<Image>().sprite = answerSprite;
        }
        else if(QuizLevel == currentQuizLevel)
        {
            this.gameObject.GetComponent<Image>().sprite = currentSprite;
        }
	}
}
