using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool autoGenerate;

    private Problem autoProblem;

    public Problem[] problems;
    public int curProblem;

    public float timePerProblem;

    public float remainingTime;

    public PlayerController player;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetProblem(0);
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0.0f)
        {
            Lose();
        }
    }

    public void OnPlayerEnterTube(int tube)
    {
        if (tube == autoProblem.correctTube)
                CorrectAnswer();
            else
                IncorrectAnswer();

    }

    void CorrectAnswer()
    {
        if (autoGenerate)
        {
            SetProblem(0);
        }
        else
        {
            if (problems.Length - 1 == curProblem)
                Win();
            else
                SetProblem(curProblem + 1);
        }
    }

    void IncorrectAnswer()
    {
        player.Stun();
    }

    void SetProblem(int problem)
    {
        if (autoGenerate)
        {
            UI.instance.SetProblemText(GenerateProblem());
            remainingTime = timePerProblem;
        }
        else
        {
            curProblem = problem;
            UI.instance.SetProblemText(problems[curProblem]);
            remainingTime = timePerProblem;
        }
    }

    void Win()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(true);
    }

    void Lose()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(false);
    }

    Problem GenerateProblem()
    {
        autoProblem = new Problem();
        autoProblem.firstNumber = Random.Range(1, 11);
        autoProblem.secondNumber = Random.Range(1, 11);
        autoProblem.operation = MathsOperation.Multiplication;
        autoProblem.answers = new float[4];
        for (int i = 0; i < 4; i++)
        {
            autoProblem.answers[i] = (autoProblem.firstNumber - i) * (autoProblem.secondNumber + 4 - i);
            if (autoProblem.answers[i]<0)
            {
                autoProblem.answers[i] = autoProblem.answers[i] * -1;
            }
        }

        int correct = Random.Range(0, 4);
        autoProblem.correctTube = correct;
        autoProblem.answers[correct] = autoProblem.firstNumber * autoProblem.secondNumber;

        for (int i = 0; i < 4; i++)
        {
            if (i!=correct)
            {
                if (autoProblem.answers[correct] == autoProblem.answers[i])
                {
                    autoProblem.answers[i] = autoProblem.answers[i] + 7;
                }
            }
        }
            

        return autoProblem;
    }
}