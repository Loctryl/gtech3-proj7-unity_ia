using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class Exit : MonoBehaviour
{

    [SerializeField]
    private GameObject roomGenerator;
    public bool levelEnd = false;
  private void OnTriggerEnter2D(Collider2D other)
    {
        roomGenerator.GetComponent<RoomFirstDungeonGenerator>().RunProceduralGeneration();
    }
}
