using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    public bool isEventing = false;             //이벤트 실행 판단
    public GameObject prefab;
    public static int goodsNum;                 //말풍선 번호 0~8

    [SerializeField] Sprite[] body;             //컨슈머의 스프라이트    0, 편의점 1, 햄버거 가게 뒷모습 2, 햄버거 가게 앞 모습
    SpriteRenderer spriteRenderer;              //컨슈머의 스프라이트 렌더러

    [SerializeField] float plusY = 1.5f;        //말풍선의 위치 조정, 컨슈머의 머리 위로
    [SerializeField] float velo;                //기본 이동 속도
    [SerializeField] float velo3;               //출구로 갈 때의 이동 속도
    float velo2;                                //기본 이동 속도 저장 용

    public void begin()
    {
        isEventing = false;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        velo2 = velo;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        isEventing = true;
        
        //트리거에 닿았을 때 편의점인지 햄버거 가게인지 구분
        if(other.name == "SellingEvent")
        {
            goodsNum = Random.Range(0, 5);
        }
        else if (other.name == "HamSellingEvent")
        {
            goodsNum = Random.Range(6,9);
        }
        else if(other.name == "DestroyConsumer")
        {
            Destroy(this.gameObject);
        }
        GameObject speechBallonClone = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + plusY, transform.position.z), transform.rotation);
        speechBallonClone.transform.parent = this.transform;
        speechBallonClone.GetComponent<SpeechBalloon>().setting();
        //speechBallonClone.GetComponent<SpeechBalloon>().begin();
    }

    public void ToExitGate()
    {
        isEventing = false;
        
        //행동에 따라서 구분되는 코드
        if (transform.parent.name == "Store")
            StartCoroutine(ToStoreExit());
        else if (transform.parent.name == "Hamburger")
        {
            StartCoroutine(ToHamExit());
        }
    }

    public void ToHamTrayProcess()
    {
        StartCoroutine(ToHamTray());
    }

    IEnumerator ToStoreCounter()
    {
        while (!isEventing)
        {
            transform.Translate(Vector3.left * velo);
            yield return null;
        }
    }

    IEnumerator ToStoreExit()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-1.5f);
        transform.rotation = Quaternion.Euler(0, 180, 0);
        while (!isEventing)
        {
            transform.Translate(Vector3.left * velo3);
            yield return null;
        }
    }

    IEnumerator ToHamCounter()
    {
        while (!isEventing)
        {
            transform.Translate(Vector3.forward * velo);
            yield return null;
        }
    }

    IEnumerator ToHamTray()
    {
        GameObject HamReceivedObj = GameObject.Find("HamReceivedPosition");
        Transform HamReceivedTransform = HamReceivedObj.GetComponent<Transform>();
        while (transform.position.x >= HamReceivedTransform.position.x)
        {
            transform.Translate(Vector3.left * 0.2f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        GameObject SBobj = transform.GetChild(0).gameObject;
        SpriteRenderer SBspr = SBobj.GetComponent<SpriteRenderer>();
        SBspr.enabled = true;
        SpeechBalloon SBcs = GetComponentInChildren<SpeechBalloon>();
        //SBcs.endSelling();
        yield return null;
    }

    IEnumerator ToHamExit()
    {
        spriteRenderer.sprite = body[2];
        while (!isEventing)
        {
            transform.Translate(Vector3.back*velo3);
            yield return null;
        }
        
    }

    //또 다른 컨슈머 오브젝트에 닿았을 때 자신의 이동 속도를 0으로 만들어서 비비적 대는 경우 차단
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Consumer")
        {
            velo = 0;
        }
    }

    //또 다른 컨슈머 오브젝트에서 벗어나거나 떨어졌을 때 정상 속도로 복귀
    private void OnCollisionExit(Collision collision)
    {
        velo = velo2;
    }
}
