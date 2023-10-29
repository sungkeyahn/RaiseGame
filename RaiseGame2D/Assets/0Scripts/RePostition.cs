using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePostition : MonoBehaviour
{
    Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerpos = GM.Contents.GetPlayer().transform.position;
        Vector3 mypos = transform.position;
        switch (transform.tag)
        {
            case "Ground":
                float diffx = (playerpos.x - mypos.x);
                float diffy = (playerpos.y - mypos.y);
                float dirx = diffx < 0 ? -1 : 1;
                float diry = diffy < 0 ? -1 : 1;
                diffx = Mathf.Abs(diffx);
                diffy = Mathf.Abs(diffy);
                if (diffx > diffy)
                    transform.Translate(Vector3.right * dirx * 40);
                else if (diffx < diffy)
                    transform.Translate(Vector3.up * diry * 40);
                break;
            case "Monster":
                if (col.enabled)
                {
                    Vector3 dist = playerpos - mypos;
                    Vector3 rand = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3),0);
                    transform.Translate(rand+dist*2);
                }
                break;
            default:
                break;
        }
    }
}
