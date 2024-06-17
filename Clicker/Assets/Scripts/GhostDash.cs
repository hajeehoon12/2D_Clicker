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

                SpriteRenderer ghostSprite = currentGhost.GetComponent<SpriteRenderer>();

                ghostSprite.sprite = currentSprite;
                ghostSprite.flipX = GetComponent<SpriteRenderer>().flipX;


                StartCoroutine(GhostBoom(currentGhost, ghostSprite));
                ghostDelayTime = ghostDelay;
                
            }
        }
    }

    IEnumerator GhostBoom(GameObject currentGhost, SpriteRenderer ghostSprite)
    {
        ghostSprite.DOFade(0.5f, 0f);
        ghostSprite.DOFade(0, fadeDuration);

        
        yield return new WaitForSeconds(fadeDuration);
        Destroy(currentGhost);
    }



}
