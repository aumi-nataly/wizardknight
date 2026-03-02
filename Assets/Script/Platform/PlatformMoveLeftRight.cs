using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveLeftRight : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float targetX;
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
            yield return MoveTo(new Vector2(targetX, startPos.y));
            yield return new WaitForSeconds(pause);
            yield return MoveTo(startPos);
            yield return new WaitForSeconds(pause);
        }
    }

    private IEnumerator MoveTo(Vector2 target)
    {
        while (Vector2.Distance(rb.position, target) > 0.01f)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));

            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(target);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
