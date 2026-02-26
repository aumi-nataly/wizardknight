using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float PlayerHealth;

    [SerializeField]
    private GameObject player;
    private PlayerHitted hitted;

   
    void Start()
    {
        hitted = player.GetComponent<PlayerHitted>();
        hitted.PlayerGetHitted += UpdateHealthPlayerAfterHit;
    }

    private void UpdateHealthPlayerAfterHit(float hit)
    {
        PlayerHealth -= hit;

        if (PlayerHealth <= 0)
        {
            Debug.Log("Конец игры");
        }
    }
}
