using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    Text quizElement;
    Text quizText;
    Button answerButton;
    GameObject answer;
    Button nextButton;
    //마지막 배열칸들은 모두 새로 넣을 용도
    int maxPreIndex = 4;
    int[] previousNumbers = new int[] {0, 0, 0, 0, 0 };

    int maxPreQuizIndex = 2;
    int[] previousQuizNumbers = new int[] {0, 0, 0 };

    int randomNumber = 1;
    int randomQuizNumber = 0;

    string[] texts = new string[] {
        "?", "H", "He",
        "Li", "Be", "B", "C", "N", "O", "F", "Ne",
        "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar",
        "K", "Ca"
    };
    string[] quizType = new string[] {
        "전자가 들어간 s오비탈의 개수",
        "전자가 들어간 p오비탈의 개수",
        "전자가 들어간 오비탈의 개수",
        "s오비탈에 들어간 전자의 개수",
        "p오비탈에 들어간 전자의 개수",
        "홀전자수",
    };
    int[,] answers = new int[,] {
        {0,0,0,0,0,0 },//?
        {1,0,1,1,0,1 },
        {1,0,1,2,0,0 },//He
        {2,0,2,3,0,1 },
        {2,0,2,4,0,0 },
        {2,1,3,4,1,1 },
        {2,2,4,4,2,2 },
        {2,3,5,4,3,3 },
        {2,3,5,4,4,2 },
        {2,3,5,4,5,1 },
        {2,3,5,4,6,0 },//Ne
        {3,3,6,5,6,1 },
        {3,3,6,6,6,0 },
        {3,4,7,6,7,1 },
        {3,5,8,6,8,2 },
        {3,6,9,6,9,3 },
        {3,6,9,6,10,2 },
        {3,6,9,6,11,1 },
        {3,6,9,6,12,0 },//Ar
        {4,6,10,7,12,1 },
        {4,6,10,8,12,0 }
    };

    // Start is called before the first frame update
    void Start()
    {
        quizElement = GameObject.Find("QuizElement").GetComponent<Text>();
        quizText = GameObject.Find("QuizText").GetComponent<Text>();
        answerButton = GameObject.Find("AnswerButton").GetComponent<Button>();
        answer = GameObject.Find("Answer");
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();

        answerButton.onClick.AddListener(ShowAnswer);
        nextButton.onClick.AddListener(NewLevel);
        NewLevel();
    }

    private void ShowAnswer() {
        answer.SetActive(true);
    }

    private void NewLevel() {
        bool flag = true;
        int i;


        previousNumbers[maxPreIndex] = randomNumber;
        for(i = 0; i < maxPreIndex - 1; i++) {
            previousNumbers[i] = previousNumbers[i + 1];
        }
        while (flag) {
            flag = false;
            randomNumber = Random.Range(0, 20) + 1;
            for (i = 0; i < maxPreIndex - 1; i++) {
                if (previousNumbers[i] == randomNumber) flag = true;
            }
        }

        flag = true;

        previousQuizNumbers[maxPreQuizIndex] = randomQuizNumber;
        for(i = 0; i < maxPreQuizIndex - 1; i++) {
            previousQuizNumbers[i] = previousQuizNumbers[i + 1];
        }
        while (flag) {
            flag = false;
            randomQuizNumber = Random.Range(0, 8);
            for(i = 0; i < maxPreQuizIndex - 1; i++) {
                if (previousQuizNumbers[i] == randomQuizNumber) flag = true;
            }
            if (previousQuizNumbers[maxPreQuizIndex - 1] == 6 && (randomQuizNumber == 0 || randomQuizNumber == 1)) flag = true;
            else if (previousQuizNumbers[maxPreQuizIndex - 1] == 7 && (randomQuizNumber == 3 || randomQuizNumber == 4)) flag = true;
        }

        answer.SetActive(false);
        quizElement.text = texts[randomNumber];
        switch (randomQuizNumber) {
            case 6:
                quizText.text = quizType[0] + "/\n" + quizType[1];
                answer.GetComponent<Text>().text = answers[randomNumber, 0].ToString() + "/ " + answers[randomNumber, 1].ToString();
                break;
            case 7:
                quizText.text = quizType[3] + "/\n" + quizType[4];
                answer.GetComponent<Text>().text = answers[randomNumber, 3].ToString()  + "/ " + answers[randomNumber, 4].ToString();
                break;
            default:
                quizText.text = quizType[randomQuizNumber];
                answer.GetComponent<Text>().text = answers[randomNumber, randomQuizNumber].ToString();
                break;
        }
    }
}
