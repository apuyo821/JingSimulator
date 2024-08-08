using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetJudge : MonoBehaviour
{
    public TrumpetNote noteCs;
    public Collider2D coll;

    public int Miss;

    public string rank;
    public int combo;

    [SerializeField] int noteIndex;
    [SerializeField] int score;
    [SerializeField] string resultRank;

    private void Start()
    {
        Miss = 0;
        combo = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        noteCs = collision.gameObject.GetComponentInParent<TrumpetNote>();
        coll = collision.gameObject.GetComponent<Collider2D>();
        switch (collision.transform.tag)
        {
            case "noteHead":
                Debug.Log(collision.name);
                if (noteCs.headHit  != true)
                {
                    Debug.Log(collision.name);
                    noteCs.headHit = false;
                    Destroy(coll);
                }
                break;

            case "noteBody":
                Debug.Log(collision.name);
                break;

            case "noteFoot":
                Debug.Log(collision.name);
                if (noteCs.footHit != true)
                {
                    noteCs.footHit = false;
                    Destroy(coll);
                }
                noteCs.DestroyTimer();
                scoreJudge();
                break;

            default:
                break;
        }
    }

    void scoreJudge()
    {
        if(noteCs.headHit == true && noteCs.noteTime > 2 && noteCs.footHit == true)
        {
            score += 500;
            combo++;
            noteIndex++;
            rank = "Perfect";
        }
        else if(noteCs.headHit == false && noteCs.noteTime >= 2 && noteCs.footHit == true)
        {
            score += 300;
            combo++;
            noteIndex++;
            rank = "Good";
        }
        else if (noteCs.headHit == false && noteCs.noteTime < 2 && noteCs.footHit == true)
        {
            score += 100;
            combo = 0;
            noteIndex++;
            rank = "OK";
        }
        else if (noteCs.headHit == false && noteCs.footHit == false)
        {
            Miss++;
            combo = 0;
            noteIndex++;
            rank = "Miss";
        }

        if(noteIndex == 5)
            scoreCalculate();
    }

    void scoreCalculate()
    {
        if (score >= 2000)
            resultRank = "S";
        else if (score < 2000 && score >= 1500)
            resultRank = "A";
        else if (score < 1500 && score >= 500)
            resultRank = "B";
        else if (score < 500 && score >= 0)
            resultRank = "C";
        else if (score <= 0)
            resultRank = "D";
    }
}
