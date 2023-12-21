using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlackBoard {
    public GameObject gameObject;
    public GameObject player;

    public Dictionary<string, Object> dataContext = new Dictionary<string, Object>();
    
}
