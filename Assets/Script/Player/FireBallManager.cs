using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public  class FireBallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fireballPrefab;

    [SerializeField]
    private int SizePool;

    private Pool pool;
    private Queue<GameObject> fires;
    public static FireBallManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(instance);

        pool = gameObject.AddComponent<Pool>();
        fires = pool.CreatePool(fireballPrefab, SizePool);
    }

    public GameObject GetFireBall(float dir)
    {
        var fire = pool.GetFromPool(fires);
        fire.transform.position = transform.position;
        var script = fire.GetComponent<FireBall>();
        script.DirectionBall(dir);

        return fire;
    }
}
