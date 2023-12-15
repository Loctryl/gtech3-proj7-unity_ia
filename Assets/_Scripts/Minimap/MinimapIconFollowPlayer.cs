using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconFollowPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
    }
}
