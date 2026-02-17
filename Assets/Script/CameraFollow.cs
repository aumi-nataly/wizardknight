using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float smoothSpeed = 0.3f; // Скорость сглаживания

    [SerializeField]
    private Vector3 offset; // Смещение камеры от игрока

    [SerializeField]
    private Vector2 minV;

    [SerializeField]
    private Vector2 maxV;

    private Vector3 vel = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.z = transform.position.z;

        var SmoothPos = Vector3.SmoothDamp(transform.position, targetPosition,ref vel, smoothSpeed);

        // Ограничиваем позицию по карте
        SmoothPos.x = Mathf.Clamp(SmoothPos.x, minV.x, maxV.x);
        SmoothPos.y = Mathf.Clamp(SmoothPos.y, minV.y, maxV.y);

        transform.position = SmoothPos;

    }
}
