using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeUI : MonoBehaviour
{

    [SerializeField]
    private GameObject flowerLifPrefab;

    [SerializeField]
    private Transform container;

    private List<GameObject> flowers = new List<GameObject>();

    void Start()
    {
        UpdateAmountLifeFlowers(WorldStateManager.Instance.GetCurrentMaxHealth());
    }


    public void UpdateAmountLifeFlowers(int sum)
    {
        while (flowers.Count < sum)
        {
            var l = Instantiate(flowerLifPrefab, container);
            flowers.Add(l);
        }

        while (flowers.Count > sum)
        {
            var l = flowers[flowers.Count - 1];
            flowers.RemoveAt(flowers.Count - 1);
            Destroy(l);
        }

    }
   
}
