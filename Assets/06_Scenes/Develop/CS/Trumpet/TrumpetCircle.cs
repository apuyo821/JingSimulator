using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetCircle : MonoBehaviour
{
    public TrumpetNote noteCs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        noteCs = collision.gameObject.GetComponentInParent<TrumpetNote>();
        switch (collision.transform.tag)
        {
            case "noteHead":
                noteCs.headHit = true;
                break;

            case "noteBody":
                break;

            case "noteFoot":
                noteCs.footHit = true;
                break;

            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "noteBody")
        {
            noteCs.noteTime += 0.1f;
        }
    }

    private void Update()
    {
        CursorMoving();
    }

    void CursorMoving()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = transform.position.x;
        mousePos.z = 0;

        transform.position = mousePos;
    }
}
