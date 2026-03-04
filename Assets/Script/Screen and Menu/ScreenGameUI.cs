using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenGameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;

  
    //void OnEnable()
    //{
    //    WorldStateManager.Instance.OnLoadedWorldState += HandleStarted;
    //}

    //private void HandleStarted()
    //{
    //    moneyText.text = WorldStateManager.Instance.GetCurrentMoney().ToString();
    //}

    //private void OnDisable()
    //{
    //    if (WorldStateManager.Instance != null)
    //        WorldStateManager.Instance.OnLoadedWorldState -= HandleStarted;
    //}

    public void UpdMoneyText(int value)
    {
        moneyText.text = value.ToString();
    }
}
