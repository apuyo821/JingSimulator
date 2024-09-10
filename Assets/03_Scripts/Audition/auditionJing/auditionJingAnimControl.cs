using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auditionJingAnimControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D jingRigid;

    [Header("Animation")]
    [SerializeField] Animator anim;

    [Space(10f)]
    [SerializeField] Sprite[] sprites;

    Timer timerCS;

    [Header("First Audition")]
    Vector3 targetPosition01 = new Vector3(600, -150, 0);
    Vector3 targetPosition02 = new Vector3(600, 150, 0);
    Vector3 targetPosition;
    [SerializeField] float moveVelo;
    TrumpetJudge trumpetJudgeCS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "EventZone")
        {
            if (targetPosition == targetPosition01)
                targetPosition = targetPosition02;
            else
                targetPosition = targetPosition01;
        }
    }
    

    public void firstAuditionAC()
    {
        GameObject trumpet = GameObject.Find("JudgeZone");
        trumpetJudgeCS = trumpet.GetComponent<TrumpetJudge>();
        StartCoroutine(faac());
    }

    public void secondAuditionAC()
    {
        GameObject timerObj = GameObject.Find("secondAuditionTimer");
        timerCS = timerObj.GetComponent<Timer>();
        StartCoroutine(saac());
    }

    IEnumerator saac()
    {
        yield return new WaitUntil(() => timerCS.isEventing == true);

        while (timerCS.isEventing)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    anim.SetTrigger("up");
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    anim.SetTrigger("down");
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    anim.SetTrigger("left");
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    anim.SetTrigger("right");
                }
                else if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Y))
                {
                    int type = Random.Range(0, 4);
                    switch (type)
                    {
                        case 0:
                            anim.SetTrigger("up");
                            break;

                        case 1:
                            anim.SetTrigger("down");
                            break;

                        case 2:
                            anim.SetTrigger("left");
                            break;

                        case 3:
                            anim.SetTrigger("right");
                            break;

                        default:
                            break;
                    }
                }
            }
            yield return null;
        }
    }

    IEnumerator faac()
    {
        Debug.Log("ww");
        yield return new WaitUntil(() => trumpetJudgeCS.isAuditioning == true);
        Debug.Log(" dd");
        targetPosition = targetPosition01;
        transform.localPosition = new Vector3(600, 0, 0);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveVelo);
            yield return null;
        }
    }
}
