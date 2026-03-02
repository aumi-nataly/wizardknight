using System.Collections;
using UnityEngine;

public class PlatformMoveUpDown : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float targetY;
    [SerializeField] 
    private float pause;

    private Vector2 startPos;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
    }

    private void Start()
    {
        startPos = rb.position;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return MoveTo(new Vector2(startPos.x, targetY));
            yield return new WaitForSeconds(pause);
            yield return MoveTo(startPos);
            yield return new WaitForSeconds(pause);
        }
    }

    private IEnumerator MoveTo(Vector2 target)
    {
        while (Vector2.Distance(rb.position, target) > 0.01f)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position,target,speed * Time.fixedDeltaTime));

            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(target);
    }
}
