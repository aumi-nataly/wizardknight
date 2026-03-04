using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField]
    private float PlayerHealth;

    [SerializeField]
    private GameObject player;
    private PlayerHitted hitted;
    private Animator animator;

    void Start()
    {
        hitted = player.GetComponent<PlayerHitted>();

        GameObject girl = player.transform.GetChild(0).gameObject;
        animator = girl.gameObject.GetComponent<Animator>();


        hitted.PlayerGetHitted += UpdateHealthPlayerAfterHit;
    }

    private void UpdateHealthPlayerAfterHit(float hit)
    {       
       Hurt(Convert.ToInt32(hit));
       var PlayerHealth = WorldStateManager.Instance.GetCurrentHealth();

        if (PlayerHealth <= 0)
        {
            WorldStateManager.Instance.ResetWorld();
            PlayerHealth = WorldStateManager.Instance.GetCurrentMaxHealth();
        }
    }

    public float AnimationHurt()
    {
        animator.SetBool("Hitted", true);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }

    public void Hurt(int hit)
    {
        StartCoroutine(HurtWithAnimation());
        WorldStateManager.Instance.MinusLife(hit);
    }

    private IEnumerator HurtWithAnimation()
    {
        float animationLength = AnimationHurt();
      //  AudioManager.instance.PlayPlayerHitted();
        yield return new WaitForSeconds(animationLength);

        animator.SetBool("Hitted", false);
    }
}
