using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using VContainer;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WorldStateManager : MonoBehaviour
{
    //[SerializeField]
    private ScreenGameUI screen;

    //[SerializeField]
    private LifeUI lifeUI;

    //[SerializeField]
    private GameObject player;
    //private PlayerMovement player;

    // [SerializeField]
     private GameObject tree;
   // private RegenerationTree tree;

    public static WorldStateManager Instance;

    private HashSet<int> DeadEnemy = new HashSet<int>();
    private HashSet<int> СollectedBonus = new HashSet<int>();
    private int CurrentLife;
    private int SumMoney;
    private int MaxLifeHave;

    public event Action OnLoadedWorldState;
    public bool IsLoad;


    //[Inject]
    //public void Construct(ScreenGameUI sc, LifeUI l)
    //{
    //    screen = sc;
    //    lifeUI = l;
    //}

    //private async void Awake()
    //{

    //        DontDestroyOnLoad(gameObject);

    //            CurrentLife = 1;
    //            MaxLifeHave = 1;
    //            SumMoney = 0;

    //        var data = await SaveManager.LoadAsync();
    //        if (data != null)
    //        {

    //            foreach (var item in data.СollectedBonus)
    //            {
    //                AddBonus(item);
    //            }
    //            CurrentLife = data.CountLife;
    //            MaxLifeHave = data.CountLife;
    //            SumMoney = data.CountMoney;
    //        }

    //    IsLoad = true;
    //    OnLoadedWorldState?.Invoke();


    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;

        CurrentLife = 1;
        MaxLifeHave = 1;
        SumMoney = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        Debug.Log("WorldStateManager уничтожен!");
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode) 
    {

        if (!scene.name.Contains("Level"))
            return;

        player = GameObject.FindWithTag("Player");
        tree = GameObject.FindWithTag("Bonfire");
        lifeUI = FindFirstObjectByType<LifeUI>();
        screen = FindFirstObjectByType<ScreenGameUI>();

        IsLoad = true;
        OnLoadedWorldState?.Invoke();
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
    { return DeadEnemy.Contains(deadEnemy); }

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

    public void SetCurrentAndMaxHealth(int life)
    {
        MaxLifeHave = life;
        CurrentLife = life;
    }

    public int GetCurrentHealth()
    {
        return CurrentLife;
    }

    public int GetCurrentMoney()
    {
        return SumMoney;
    }

    public void SetCurrentMoney(int money)
    {
        SumMoney = money;
    }

    public HashSet<int> GetDictionaryCollectedBonus()
    {
        return СollectedBonus;
    }

}
