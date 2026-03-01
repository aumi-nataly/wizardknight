using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenGameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = WorldStateManager.Instance.GetCurrentMoney().ToString();
    }

    public void UpdMoneyText(int value)
    {
        moneyText.text = value.ToString();
    }
}
