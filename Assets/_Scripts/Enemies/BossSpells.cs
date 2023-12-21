using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossSpells : MonoBehaviour
{
    [SerializeField] GameObject ElecAoE;
    [SerializeField] int numberLightning;
    [SerializeField] float delayBetweenLightnings;
    [SerializeField] GameObject WindAoE;
    [SerializeField] GameObject MeleeSingleTarget;
    [SerializeField] GameObject MeleeAoE;
    [SerializeField] GameObject TeleportSpell;

    GameObject player;
    Vector3 playerDirection;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        playerDirection = player.transform.position - transform.position;
    }

    public void CastElecAoE()
    {
        StartCoroutine(castElec());
    }
    IEnumerator castElec()
    {
        for (int i = 0; i < numberLightning; i++)
        {
            Vector2 rand = Random.insideUnitCircle * 3 + new Vector2(player.transform.position.x, player.transform.position.y);
            Instantiate(ElecAoE, new Vector3(rand.x,rand.y,player.transform.position.z) ,Quaternion.identity);
            yield return new WaitForSeconds(delayBetweenLightnings);
        }
    }

    public void CastWindAoE()
    {
        Vector3 direction = player.transform.position - transform.position;
        Instantiate(WindAoE, transform.position, Quaternion.FromToRotation(Vector3.forward, direction));
    }

    public void CastMeleeAoE()
    {
        Instantiate(MeleeAoE, transform);
    }

    public void CastMeleeSL()
    {
        Vector3 direction = player.transform.position - transform.position;
        Instantiate(MeleeSingleTarget, transform.position, Quaternion.FromToRotation(Vector3.forward, direction));
    }

    public void CastTeleport()
    {
        Instantiate(TeleportSpell, transform.position, Quaternion.identity);
    }
}
