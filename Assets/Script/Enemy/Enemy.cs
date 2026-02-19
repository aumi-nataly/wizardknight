using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int MaxHealth;
    private GameObject player;

    public int CurrentHealth;

    private StateMachine _m;
    private Transform _startLocation;
    private GameObject lumineSleep;
   

    private void Awake()
    {
        _m = new StateMachine();
        _startLocation = GetComponent<Transform>();

    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _m.StateChange(new IdleState(_m, this));
    }

    private void Update()
    {
        _m.Tick();
    }



    /// <summary>
    /// Вернуться врагу на стартовую позицию
    /// </summary>
    public void ReturnToStartLocation()
    {

        //решить что делать
        transform.position = _startLocation.position;
    }

    /// <summary>
    /// Враг спит
    /// </summary>
    public void Sleep()
    {
        //звук сна
    }

    /// <summary>
    /// Завершить сон
    /// </summary>
    public void SleepOff()
    {

    }

    /// <summary>
    /// Проверка как близок игрок для пробуждения/засыпания
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerHereForAwakening()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance < 5f;
    }

    /// <summary>
    /// Пробудиться - сменить подсветку
    /// </summary>
    public void Detection(bool turn)
    {
        lumineSleep = transform.Find("LumineDetection")?.gameObject;
        lumineSleep.gameObject.SetActive(turn);

    }

}
