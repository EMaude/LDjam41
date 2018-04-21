using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public Color NormalColor;
    public Color SpottedColor;

    SpriteRenderer sr;
    private bool spotted = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = NormalColor;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            GetComponentInParent<EnemyController>().CanSeePlayer();
           
            spotted = true;
            sr.color = SpottedColor;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            spotted = false;
            StartCoroutine(UnSpotTimer());
        }
    }

    IEnumerator UnSpotTimer()
    {
        yield return new WaitForSecondsRealtime(2);
        if (!spotted)
        {
            sr.color = NormalColor;
            GetComponentInParent<EnemyController>().LostPlayer();
        }
    }

    public void Flip()
    {
        sr.flipY = (sr.flipY == false) ? true : false;
        gameObject.transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }
}
