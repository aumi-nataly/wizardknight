using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateManager : MonoBehaviour
{
    public static WorldStateManager Instance;
    private HashSet<int> DeadEnemy = new HashSet<int>();
    private HashSet<int> СollectedBonus = new HashSet<int>();
    private int SumLife;
    private int SumMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            SumLife = 1;
            SumMoney = 0;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
         Destroy(gameObject);
        }

    }

    public void AddLife(int amount)
    {
        SumLife += amount;
    }

    public void AddMoney(int amount)
    {
        SumMoney += amount;
    }

    /// <summary>
    /// Добавить убитого врага
    /// </summary>
    /// <param name="deadEnemy"></param>
    public void AddDeathEnemy(int deadEnemy)
    { 
        if (!DeadEnemy.Contains(deadEnemy)) 
            DeadEnemy.Add(deadEnemy);
    }

    /// <summary>
    /// Добавить собранный бонус
    /// </summary>
    /// <param name="bonus"></param>
    public void AddBonus(int bonus)
    {
        if (!СollectedBonus.Contains(bonus))
            СollectedBonus.Add(bonus);
    }

    /// <summary>
    /// Проверить, мерт ли враг
    /// </summary>
    /// <param name="deadEnemy"></param>
    /// <returns></returns>
    public bool IsDeadEnemy(int deadEnemy)
    {  return DeadEnemy.Contains(deadEnemy); }

    /// <summary>
    /// Проверить, собран ли бонус
    /// </summary>
    /// <param name="bonus"></param>
    /// <returns></returns>
    public bool IsCollectedBonus(int bonus)
    { return СollectedBonus.Contains(bonus); }

    /// <summary>
    /// Перезагрузить мир, воскресить врагов, а бонусы - нет
    /// </summary>
    public void ResetWorld()
    {
        DeadEnemy.Clear();
    }
}
