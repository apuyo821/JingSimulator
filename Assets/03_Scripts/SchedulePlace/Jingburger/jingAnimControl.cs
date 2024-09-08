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

    private void Awake()
    {
        jingAnim = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        jingRigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //jingAnim = this;
        ScheduleManager.isHome = true;
        //StartCoroutine(Moving());
        //StartCoroutine(veloControl());
        transform.position = new Vector3(0f, -2.45f, 0);
        spriteRenderer.sprite = sprites[2];
    }

    public void animPosSet(int scheduleIndex)
    {
        transform.parent = scheduleManager.SchedulePlace[scheduleIndex].transform;
        spriteRenderer.flipX = false;


        switch (scheduleIndex)
        {
            //Main Room
            case 0:
                spriteRenderer.sprite = sprites[2];
                transform.position = new Vector3(0f, -2.45f, 0);
                StartCoroutine(Moving());
                StartCoroutine(veloControl());
                break;

            //Vocal Academy
            case 1:
                transform.position = new Vector3(2f, -2.45f, 0);
                StartCoroutine(Singing());
                break;

            //DanceAcademy
            case 2:
                transform.position = new Vector3(2f, -2.45f, 0);
                break;

            //Gaming
            case 4:
                transform.position = new Vector3(0f, -2.45f, 0);
                StartCoroutine(Gaming());
                break;

            //Work Out
            case 5:
                transform.position = new Vector3(-4.45f, -1.4f, 0);
                StartCoroutine(WorkingOut());
                break;

            //Drawing
            case 6:
                transform.position = new Vector3(-6.35f, -0.5f, 0);
                StartCoroutine(Drawing());
                break;

            //Guitar
            case 7:
                transform.position = new Vector3(-0.45f, -1.75f, 0);
                StartCoroutine(Guitaring());
                break;

            //Hamburger
            case 8:
                transform.position = new Vector3(-6f, -2.45f, 0);
                spriteRenderer.sprite = sprites[0];
                StartCoroutine(Hamburger());
                break;

            //Rest
            case 9:
                transform.position = new Vector3(-6f, -2.45f, 0);
                spriteRenderer.sprite = sprites[2];
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

    IEnumerator Singing()
    {
        anim.SetBool("isSinging", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isSinging", false);
    }

    IEnumerator Gaming()
    {
        anim.SetBool("isGaming", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isGaming", false);
    }

    IEnumerator WorkingOut()
    {
        anim.SetBool("isWorkingOut", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isWorkingOut", false);
    }

    IEnumerator Drawing()
    {
        anim.SetBool("isDrawing", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isDrawing", false);
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
            veloX = Random.Range(-1, 2) * 2;
        }
    }
}
