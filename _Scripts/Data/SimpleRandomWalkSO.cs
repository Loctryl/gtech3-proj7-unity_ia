using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkSO")]

public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10, walklength = 10;    
    public bool startRandomEachIteration = true;

}
