using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class Exit : MonoBehaviour
{

    [SerializeField]
    private GameObject roomGenerator;
    [SerializeField]
    private GameObject BossRoom;
    public bool levelEnd = false;
    [SerializeField]
    private LevelCounter levelCounter;
  private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == gameObject.CompareTag("Player"))
        {

                if (levelCounter.count < 2)
            {
                roomGenerator.GetComponent<RoomFirstDungeonGenerator>().RunProceduralGeneration();
                levelCounter.count++;
            }
            else
            {
                BossRoom.GetComponent<SimpleRandomWalkDungeonGenerator>().RunProceduralGeneration();
                levelCounter.count = 0;

            }
        }
        
        
    }
}
