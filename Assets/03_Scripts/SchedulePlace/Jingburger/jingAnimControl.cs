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

    [SerializeField] audioSet audioSet;

    [Header("슬라이드")]
    [SerializeField] GameObject sliderObject;

    //With Game
    bool isGameResult = false;

    private void Awake()
    {
        jingAnim = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        jingRigid = GetComponent<Rigidbody2D>();
        spriteRenderer.flipX = false;
    }

    private void Start()
    {
        ScheduleManager.isHome = true;
        transform.position = new Vector3(0f, -2.45f, 0);
        spriteRenderer.sprite = sprites[2];
    }

    public void animPosSet(int scheduleIndex)
    {
        anim.enabled = true;
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
                transform.position = new Vector3(-3f, -1.5f, 0);
                sliderObject.transform.localPosition = new Vector3(sliderObject.transform.localPosition.x, sliderObject.transform.localPosition.y + 100, sliderObject.transform.localPosition.z);
                StartCoroutine(Singing());
                break;

            //DanceAcademy
            case 2:
                transform.position = new Vector3(0f, -2.45f, 0);
                StartCoroutine(Dancing());
                break;

            //BroadCast
            case 3:
                transform.position = new Vector3(0f, -2.45f, 0);
                StartCoroutine(broadcasting());
                break;

            //Gaming
            case 4:
                transform.position = new Vector3(-6.5f, -1.5f, 0);
                StartCoroutine(Gaming());
                break;

            //Work Out
            case 5:
                transform.position = new Vector3(-4.45f, -1.4f, 0);
                StartCoroutine(WorkingOut());
                break;

            //Drawing
            case 6:
                transform.position = new Vector3(-6.35f, -1.06f, 0);
                StartCoroutine(Drawing());
                break;

            //Hamburger
            case 7:
                transform.position = new Vector3(-6f, -0.21f, 0);
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
            coolTime = 3;
        }
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
        spriteRenderer.flipX = true;
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        spriteRenderer.flipX = false;
        sliderObject.transform.localPosition = new Vector3(sliderObject.transform.localPosition.x, sliderObject.transform.localPosition.y - 100, sliderObject.transform.localPosition.z);
        anim.SetBool("isSinging", false);
    }

    IEnumerator broadcasting()
    {
        anim.SetBool("isbroadCast", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isbroadCast", false);
    }

    IEnumerator Gaming()
    {
        audioSet.actSoundEffect[1].Play(); //게임 메인 브금 실행 => 조이스틱 클릭하는 소리
        isGameResult = false;
        StartCoroutine(GamingSoundEffectPlay());
        anim.SetBool("isGaming", true);
        yield return new WaitForSeconds((float)scheduleManager.actFlowTIme / 4 * 3);
        isGameResult = true;
        audioSet.actSoundEffect[1].Stop();
        audioSet.actSoundEffect[0].Stop();

        anim.SetBool("isGaming", false);
        anim.enabled = false;
        int wL = Random.Range(0, 2);
        switch (wL)
        {
            case 0:
                audioSet.actSoundEffect[2].Play();
                spriteRenderer.sprite = sprites[3];
                break;

            case 1:
                audioSet.actSoundEffect[3].Play();
                spriteRenderer.sprite = sprites[4];
                break;

            default:
                break;
        }
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.enabled = true;
    }

    IEnumerator GamingSoundEffectPlay()
    {
        while (!isGameResult)
        {
            audioSet.actSoundEffect[0].Play();

            int coolTime = Random.Range(5, 10);

            yield return new WaitForSeconds((float)coolTime / 10);
        }
    }

    IEnumerator WorkingOut()
    {
        anim.SetBool("isWorkingOut", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isWorkingOut", false);
    }

    IEnumerator Drawing()
    {
        StartCoroutine(DrawingSoundEffect());
        anim.SetBool("isDrawing", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isDrawing", false);
    }

    IEnumerator DrawingSoundEffect()
    {
        while(ScheduleManager.isActing == true)
        {
            int random = Random.Range(4, 7);
            audioSet.actSoundEffect[random].Play();
            yield return new WaitForSeconds(0.3f);
        }

        for (int i = 4; i < 7; i++)
        {
            audioSet.actSoundEffect[i].Stop();
        }
    }

    IEnumerator Dancing()
    {
        anim.SetBool("isDancing", true);
        yield return new WaitUntil(() => ScheduleManager.isActing == false);
        anim.SetBool("isDancing", false);
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
