using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateManager : MonoBehaviour
{
    [SerializeField]
    private ScreenGameUI screen;

    [SerializeField]
    private LifeUI lifeUI;

    [SerializeField]
    private GameObject player;


    [SerializeField]
    private GameObject tree;

    public static WorldStateManager Instance;
    private HashSet<int> DeadEnemy = new HashSet<int>();
    private HashSet<int> СollectedBonus = new HashSet<int>();
    private int CurrentLife;
    private int SumMoney;
    private int MaxLifeHave;

    private void Awake()
    {
        if (Instance == null)
        {
            CurrentLife = 1;
            MaxLifeHave = 1;
            SumMoney = 0;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
         Destroy(gameObject);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddLife(int amount)
    {
        CurrentLife += amount;
        MaxLifeHave = CurrentLife;
        lifeUI.UpdateAmountLifeFlowers(CurrentLife);
    }

    public void MinusLife(int amount)
    {
        CurrentLife -= amount;
        lifeUI.UpdateAmountLifeFlowers(CurrentLife);
    }

    public void AddMoney(int amount)
    {
        SumMoney += amount;
        screen.UpdMoneyText(SumMoney);
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
        CurrentLife = MaxLifeHave;
        lifeUI.UpdateAmountLifeFlowers(CurrentLife);

        player.transform.position = tree.transform.position;
    }

    public int GetCurrentMaxHealth()
    {
        return MaxLifeHave;
    }

    public int GetCurrentHealth()
    {
        return CurrentLife;
    }
}
