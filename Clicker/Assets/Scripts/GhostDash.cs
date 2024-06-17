using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GhostDash : MonoBehaviour
{
    public float ghostDelay;
    public float fadeDuration;
    private float ghostDelayTime;
    public GameObject ghost;
    public bool makeGhost;

    void Start()
    {
        ghostDelayTime = ghostDelay;
        makeGhost = false;
    }

    void FixedUpdate()
    {
        if (makeGhost)
        {
            if (ghostDelayTime > 0)
            {
                ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                

                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = this.transform.localScale;



                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                currentGhost.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;


                StartCoroutine(GhostBoom(currentGhost));
                ghostDelayTime = ghostDelay;
                
            }
        }
    }

    IEnumerator GhostBoom(GameObject currentGhost)
    {
        SpriteRenderer _spriteRenderer = currentGhost.GetComponent<SpriteRenderer>();

        _spriteRenderer.DOFade(0.5f, 0f);
        _spriteRenderer.DOFade(0, fadeDuration);

        
        yield return new WaitForSeconds(fadeDuration);
        Destroy(currentGhost);
    }



}
