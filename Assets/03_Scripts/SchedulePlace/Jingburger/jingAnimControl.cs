using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jingAnimControl : MonoBehaviour
{
    public static jingAnimControl jingAnim;
    public ScheduleManager scheduleManager;

    public Rigidbody2D jingRigid;
    public int veloX;
    public int coolTime;

    SpriteRenderer spriteRenderer;
    Animator anim;
    public Sprite[] sprites;

    private void Start()
    {
        jingAnim = this;

        anim = GetComponent<Animator>();
        jingRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ScheduleManager.isHome = true;
        StartCoroutine(Moving());
        StartCoroutine(veloControl());
        transform.position = new Vector3(0f, -2.45f, 0);
    }

    public void animPosSet(int scheduleIndex)
    {
        transform.parent = scheduleManager.SchedulePlace[scheduleIndex].transform;

        switch (scheduleIndex)
        {
            case 0:
                transform.position = new Vector3(0f, -2.45f, 0);
                StartCoroutine(Moving());
                StartCoroutine(veloControl());
                break;

            case 2:
                transform.position = new Vector3(2f, -2.45f, 0);
                StartCoroutine(Moving());
                StartCoroutine(veloControl());
                break;

            case 7:
                transform.position = new Vector3(8f, -2.45f, 0);
                StartCoroutine(Guitaring());
                break;

            case 8:
                transform.position = new Vector3(-6f, -2.45f, 0);
                spriteRenderer.sprite = sprites[0];
                StartCoroutine(Hamburger());
                break;

            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            veloX *= -1;
        }
    }

    IEnumerator Guitaring()
    {
        anim.SetBool("isGuitar", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isGuitar", false);
    }

    IEnumerator Hamburger()
    {
        anim.SetBool("isHamburger", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isHamburger", false);
    }

    IEnumerator Moving()
    {
        while (ScheduleManager.isHome)
        {
            //Direction Sprite
            if (jingRigid.velocity.x < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;

            //Animator
            if (jingRigid.velocity.x == 0)
                anim.SetBool("isWalk", false);
            else
                anim.SetBool("isWalk", true);

            //Moving
            jingRigid.velocity = new Vector2(veloX * 2, jingRigid.velocity.y);
            yield return null;
        }
        veloX = 0;
        coolTime = 0;
        anim.SetBool("isWalk", false);
    }

    IEnumerator veloControl()
    {
        while (ScheduleManager.isHome)
        {
            coolTime = Random.Range(2, 4);
            yield return new WaitForSeconds(coolTime);
            veloX = Random.Range(-2, 3) * 2;
        }
    }
}
