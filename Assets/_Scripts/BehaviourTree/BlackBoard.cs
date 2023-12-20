using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlackBoard {
    public GameObject gameObject;
    public GameObject moveToObject;
    
    public float speed;

    public Dictionary<string, object> dataContext = new Dictionary<string, object>();
    
}
