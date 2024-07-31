using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetJudge : MonoBehaviour
{
    public GameObject noteObj;
    public TrumpetNote noteCs;

    public int Perfect;
    public int Good;
    public int OK;
    public int Miss;

    public string rank;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        noteCs = collision.gameObject.GetComponentInParent<TrumpetNote>();
        Collider2D coll = collision.gameObject.GetComponent<Collider2D>();
        switch (collision.transform.tag)
        {
            case "noteHead":
                noteCs.headHit = false;
                Destroy(coll);
                break;

            case "noteBody":
                break;

            case "noteFoot":
                noteCs.footHit = false;
                Destroy(coll);
                scoreCalculate();
                break;

            default:
                break;
        }
    }

    void scoreCalculate()
    {
        if(noteCs.headHit == true && noteCs.noteTime > 5 && noteCs.footHit == true)
        {
            Perfect++;
            rank = "Perfect";
        }
        else if(noteCs.headHit == true && noteCs.noteTime > 3 && noteCs.noteTime < 5 && noteCs.footHit == true)
        {
            Good++;
            rank = "Good";
        }
        else if (noteCs.headHit == false && noteCs.noteTime > 0 && noteCs.noteTime < 3 && noteCs.footHit == true)
        {
            OK++;
            rank = "OK";
        }
        else if (noteCs.headHit == false && noteCs.footHit == false)
        {
            Miss++;
            rank = "Miss";
        }
    }
}
