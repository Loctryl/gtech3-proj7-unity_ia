using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int MaxLevelBeforeBoss = 3;
    
    private int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel++;
    }

    


}
