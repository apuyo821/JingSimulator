using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerConsumer : MonoBehaviour
{
    [SerializeField] bool isEventing = false;
    [SerializeField] GameObject prefab;

    public int consumerNum;

    [SerializeField] Sprite[] body;             //컨슈머의 스프라이트    0, 햄버거 가게 앞모습 1, 햄버거 가게 뒷 모습
    SpriteRenderer spriteRenderer;              //컨슈머의 스프라이트 렌더러

    [SerializeField] float plusY = 1.5f;        //말풍선의 위치 조정, 컨슈머의 머리 위로
    [SerializeField] float velo;                //기본 이동 속도
    [SerializeField] float velo3;               //출구로 갈 때의 이동 속도
    [SerializeField] float velo2;                                //기본 이동 속도 저장 용


    [SerializeField] HamEventZone hamEventZoneCS;

    [Header("징버거")]
    [SerializeField] GameObject jingObj;
    [SerializeField] Animator jingAnim;

    private void OnEnable()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        isEventing = false;
        velo2 = velo;

        ToCountProcess();
    }

    private void Start()
    {
        jingObj = GameObject.Find("Jing");
        jingAnim = jingObj.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "EventZone")
        {
            isEventing = true;
            velo = 0;
            //닿자마자 숫자 계산과 말풍선 생성
            consumerNum = Random.Range(0, 3);
            hamEventZoneCS = collision.gameObject.GetComponent<HamEventZone>();
            GameObject speechBallonClone = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + plusY, transform.position.z), transform.rotation);
            speechBallonClone.transform.parent = this.transform;
            speechBallonClone.GetComponent<SpeechBalloon>().setting();

            //손님과 카운터의 숫자를 비교해서 말풍선의 스프라이트 결정
            if (hamEventZoneCS.judge(consumerNum))
            {
                success();
                speechBallonClone.GetComponent<SpeechBalloon>().begin(0);
            }
            else
            {
                fail();
                speechBallonClone.GetComponent<SpeechBalloon>().begin(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "DestroyZone")
        {
            HamEventZone.isHamEnd = true;
            Destroy(this.gameObject);
        }
    }

    void success()
    {
        jingAnim.SetTrigger("hamburgerSuccess");
    }

    void fail()
    {
        jingAnim.SetTrigger("hamburgerMisstake");
    }

    public void ToCountProcess()
    {
        StartCoroutine(ToCount());
    }

    public void ToExitProcess()
    {
        StartCoroutine(ToStoreExit());
    }

    IEnumerator ToCount()
    {
        while (!isEventing)
        {
            transform.Translate(Vector3.left * velo);
            yield return null;
        }
    }

    IEnumerator ToStoreExit()
    {
        Debug.Log("d");
        transform.rotation = Quaternion.Euler(0, 180, 0);
        while (true)
        {
            transform.Translate(Vector3.left * velo3);
            yield return null;
        }
    }
}
