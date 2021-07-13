using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    private float strikeSize = 0.35f;
    private CircleCollider2D col;

    private void OnEnable()
    {
        col = GetComponent<CircleCollider2D>();
        StartCoroutine(Strike());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") return;
        Health health = collision.GetComponent<Health>();
        if (health == null) return;
        health.TakeDamage(90);
    }

    private IEnumerator Strike()
    {
        while (col.radius < strikeSize)
        {

            col.radius = Mathf.Max(strikeSize, col.radius + strikeSize * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
