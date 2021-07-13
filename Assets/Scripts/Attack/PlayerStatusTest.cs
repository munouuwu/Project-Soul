using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusTest : MonoBehaviour
{

    public Health health;

    //public CameraShake.Properties camerashakeHurt;

    public CanvasGroup canvasGroup;
    public float max = 0.6f;
    public float min = 0f;
    public float smooth = 0.2f;

    private Animator animatorHurt;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        animatorHurt = GetComponent<Animator>();

        health.OnDamageEvent -= Hurt;
        health.OnDamageEvent += Hurt;
    }


    // Update is called once per frame
    void Update()
    {
       canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, min, smooth * Time.deltaTime);
    }

    public void Hurt(float damage)
    {
        Debug.Log("hurt");
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Hit Knife");
        }

        canvasGroup.alpha = max;
        animatorHurt.SetTrigger("Hurt");

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animatorHurt.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animatorHurt.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        //FindObjectOfType<CameraShake>().StartShake(camerashakeHurt);
    }
}
