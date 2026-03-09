using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;

public class FireBallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fireballPrefab;

    [SerializeField]
    private int SizePool;
    private Queue<GameObject> fires;



    private void Awake()
    {
        fires = new Queue<GameObject>();
        DontDestroyOnLoad(gameObject);
  
        for (int i = 0; i < SizePool; i++)
        {
            var obj = Instantiate<GameObject>(fireballPrefab,transform);
            LifetimeScope.Find<GameLifetimeScope>().Container.InjectGameObject(obj);

            obj.gameObject.SetActive(false);
            fires.Enqueue(obj);
        }
    }

  
    public GameObject GetFireBall(float dir,Transform playerPos)
    {

        if (fires == null || fires.Count == 0)
        {
            Debug.LogError("FireBall пул не инициализирован или пуст!");
            return null;
        }

       
        var fire = fires.Dequeue();

        fire.transform.position = playerPos.position;
        fire.gameObject.SetActive(true);

        var script = fire.GetComponent<FireBall>();
        script.DirectionBall(dir);

        return fire;
    }



    public void ReturnToPool(FireBall fire)
    {
        fire.gameObject.SetActive(false);
        fires.Enqueue(fire.gameObject);
    }

}
