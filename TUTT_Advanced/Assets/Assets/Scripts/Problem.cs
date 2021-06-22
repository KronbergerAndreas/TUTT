using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Problem
{
    public float firstNumber;           
    public float secondNumber;          
    public MathsOperation operation;    
    public float[] answers;            

    [Range(0, 3)]
    public int correctTube;             
}

public enum MathsOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}