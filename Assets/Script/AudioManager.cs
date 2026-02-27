using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip jumpSoundClip;

    [SerializeField]
    private AudioClip runSoundClip;

    [SerializeField]
    private AudioClip enemyDetectionSoundClip;

    [SerializeField]
    private AudioClip enemyGrowlSoundClip;

    [SerializeField]
    private AudioClip playerHittedSoundClip;

    [SerializeField]
    private AudioClip TreeSoundClip;

    [SerializeField]
    private AudioClip HappyGetLifeSoundClip;

    private AudioSource audioPlayerSource;
    private AudioSource audioRunPlayerSource;
    private AudioSource audioEnemyDetectionSource;
    private AudioSource audioEnemyGrowlSource;
    private AudioSource playerHittedSource;
    private AudioSource TreeSource;
    private AudioSource HappyGetLifeSource;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(instance);

        audioPlayerSource = gameObject.AddComponent<AudioSource>();
        audioPlayerSource.clip = jumpSoundClip;
        audioPlayerSource.volume = 0.8f;

        audioRunPlayerSource = gameObject.AddComponent<AudioSource>();
        audioRunPlayerSource.clip = runSoundClip;
        audioRunPlayerSource.loop = true;
        audioRunPlayerSource.volume = 0.3f;
        audioRunPlayerSource.pitch = 2f;

        audioEnemyDetectionSource = gameObject.AddComponent<AudioSource>();
        audioEnemyDetectionSource.clip = enemyDetectionSoundClip;
        audioEnemyDetectionSource.volume = 0.8f;

        audioEnemyGrowlSource = gameObject.AddComponent<AudioSource>();
        audioEnemyGrowlSource.clip = enemyGrowlSoundClip;
        audioEnemyGrowlSource.volume = 0.6f;
        audioEnemyGrowlSource.pitch = 2f;

        playerHittedSource = gameObject.AddComponent<AudioSource>();
        playerHittedSource.clip = playerHittedSoundClip;
        playerHittedSource.volume = 0.8f;

        TreeSource = gameObject.AddComponent<AudioSource>();
        TreeSource.clip = TreeSoundClip;

        HappyGetLifeSource = gameObject.AddComponent<AudioSource>();
        HappyGetLifeSource.clip = HappyGetLifeSoundClip;
        HappyGetLifeSource.volume = 0.8f;

    }

    public void PlayJumpPlayerSound()
    {
        if (audioPlayerSource != null && jumpSoundClip != null)
        {
            audioPlayerSource.PlayOneShot(jumpSoundClip);
        }
    }


    public void PlayRunPlayerSound(float speed, bool isGround)
    {
        if (audioRunPlayerSource != null && runSoundClip != null)
        {
            float currentSpeed = Mathf.Abs(speed);
            bool shouldPlay = currentSpeed > 0.1 && isGround;

            if (shouldPlay && !audioRunPlayerSource.isPlaying)
            {
                audioRunPlayerSource.Play();
            }
            else if (!shouldPlay && audioRunPlayerSource.isPlaying)
            {
                audioRunPlayerSource.Stop();
            }

        }
    }

    public void PlayDetectionEnemy()
    {
        if (audioEnemyDetectionSource != null && enemyDetectionSoundClip != null)
        {
            audioEnemyDetectionSource.PlayOneShot(enemyDetectionSoundClip);
        }
    }

    public void PlayGrowlEnemy()
    {
        if (audioEnemyGrowlSource != null && enemyDetectionSoundClip != null)
        {
            audioEnemyGrowlSource.PlayOneShot(enemyGrowlSoundClip);
        }
    }

    public void PlayPlayerHitted()
    {
        if (playerHittedSource != null && playerHittedSoundClip != null)
        {
            playerHittedSource.PlayOneShot(playerHittedSoundClip);
        }
    }
    public void PlayTreeLoader()
    {
        if (TreeSource != null && TreeSoundClip != null)
        {
            TreeSource.PlayOneShot(TreeSoundClip);
        }
    }

    public void PlayHappyGetLife()
    {
        if (HappyGetLifeSource != null && HappyGetLifeSoundClip != null)
        {
            HappyGetLifeSource.PlayOneShot(HappyGetLifeSoundClip);
        }
    }

}
