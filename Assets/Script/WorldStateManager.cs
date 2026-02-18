using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateManager : MonoBehaviour
{
    public static WorldStateManager Instance;
    private HashSet<int> DeadEnemy = new HashSet<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
         Destroy(gameObject);
        }
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
    /// Проверить, мерт ли враг
    /// </summary>
    /// <param name="deadEnemy"></param>
    /// <returns></returns>
    public bool IsDeadEnemy(int deadEnemy)
    {  return DeadEnemy.Contains(deadEnemy); }


    /// <summary>
    /// Перезагрузить мир
    /// </summary>
    public void ResetWorld()
    {
        DeadEnemy.Clear();
    }
}
