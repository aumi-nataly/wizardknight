using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

public class ScreenGameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;
    private WorldStateManager _worldStateManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager)
    {
        _worldStateManager = worldStateManager;
    }

    private void Start()
    {

        if (_worldStateManager.IsLoad)
        {
            HandleStarted();
        }
        else
        {

            _worldStateManager.OnLoadedWorldState += HandleStarted;
        }
    }

    private void HandleStarted()
    {
        moneyText.text = _worldStateManager.GetCurrentMoney().ToString();
    }

    private void OnDisable()
    {
        _worldStateManager.OnLoadedWorldState -= HandleStarted;
    }

    public void UpdMoneyText(int value)
    {
        moneyText.text = value.ToString();
    }
}
