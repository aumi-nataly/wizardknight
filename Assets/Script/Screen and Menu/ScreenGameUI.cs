using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenGameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;


    private void Start()
    {
        var world = WorldStateManager.Instance;

        if (world.IsLoad)
        {
            HandleStarted();
        }
        else
        {

            WorldStateManager.Instance.OnLoadedWorldState += HandleStarted;
        }
    }

    private void HandleStarted()
    {
        moneyText.text = WorldStateManager.Instance.GetCurrentMoney().ToString();
    }

    private void OnDisable()
    {
        if (WorldStateManager.Instance != null)
            WorldStateManager.Instance.OnLoadedWorldState -= HandleStarted;
    }

    public void UpdMoneyText(int value)
    {
        moneyText.text = value.ToString();
    }
}
