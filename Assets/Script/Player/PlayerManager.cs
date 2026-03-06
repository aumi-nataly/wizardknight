using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField]
    private float PlayerHealth;

    [SerializeField]
    private GameObject player;
    private PlayerHitted hitted;
    private Animator animator;

    private WorldStateManager _worldStateManager;
    private AudioManager _audioManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager, AudioManager audioManager)
    {
        _worldStateManager = worldStateManager;
        _audioManager = audioManager;
    }

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
       var PlayerHealth = _worldStateManager.GetCurrentHealth();

        if (PlayerHealth <= 0)
        {
            _worldStateManager.ResetWorld();
            PlayerHealth = _worldStateManager.GetCurrentMaxHealth();
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
        _worldStateManager.MinusLife(hit);
    }

    private IEnumerator HurtWithAnimation()
    {
        float animationLength = AnimationHurt();
        _audioManager.PlayPlayerHitted();
        yield return new WaitForSeconds(animationLength);

        animator.SetBool("Hitted", false);
    }
}
