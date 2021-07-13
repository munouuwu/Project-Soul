using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeroTrigger : MonoBehaviour, IInteractable
{
    public LoadOutManager lom;
    public int heroIndex;

    public bool playerInTrigger;

    public SpriteRenderer zoneIndicator;
    public Vector2 opacity;
    public CanvasGroup cg;
    public Vector2 alpha;
    public float smooth = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            zoneIndicator.color = new Color(zoneIndicator.color.r, zoneIndicator.color.g, zoneIndicator.color.b, Mathf.Lerp(zoneIndicator.color.a, opacity.y,smooth*Time.deltaTime));
            cg.alpha = Mathf.Lerp(cg.alpha, alpha.y, smooth * Time.deltaTime);


            /*if (Input.GetKeyDown(KeyCode.E))
            {
                lom.ChangeHeroTo(heroIndex);
            }*/
        }
        else
        {
            zoneIndicator.color = new Color(zoneIndicator.color.r, zoneIndicator.color.g, zoneIndicator.color.b, Mathf.Lerp(zoneIndicator.color.a, opacity.x, smooth * Time.deltaTime));
            cg.alpha = Mathf.Lerp(cg.alpha, alpha.x, smooth * Time.deltaTime);
        }
    }

    /*public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("there is a player in the trigger");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("switch character");
                lom.ChangeHeroTo(heroIndex);
            }
        }
        
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    public IEnumerator Interact(Transform playerObj)
    {
        yield return null;
        lom.ChangeHeroTo(heroIndex);
        yield return null;
    }

    public string GetInteractableName()
    {
        return "SwitchHero";
    }
}
