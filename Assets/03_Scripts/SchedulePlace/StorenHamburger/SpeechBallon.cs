using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBalloon : MonoBehaviour
{
    [SerializeField] Sprite[] goods;
    [SerializeField] int sbSprNum;
    [SerializeField] float yVelo;
    [SerializeField] GameObject storeStaffObj;
    [SerializeField] GameObject consumerObj;
    [SerializeField] Consumer consumerCs;
    [SerializeField] Transform HamReceivedTransform;
    [SerializeField] GameObject Parent;
    
    public static int processIndex = 0;
    public SpriteRenderer spriteRenderer;
    StoreStaff storeStaffCS;
    private static List<GameObject> SBList = new List<GameObject>();

    public void Start()
    {
        storeStaffObj = GameObject.FindWithTag("Player");
    }

    public void setting()
    {
        SBList.Add(gameObject);
        if (transform.parent.tag == "Consumer")
        {
            consumerObj = transform.parent.gameObject;
            consumerCs = consumerObj.gameObject.GetComponentInParent<Consumer>();
        }
        else if (transform.parent.tag == "Player")
        {
            consumerObj = SBList[0].transform.parent.gameObject;
            consumerCs = consumerObj.gameObject.GetComponent<Consumer>();
        }
        storeStaffCS = storeStaffObj.gameObject.GetComponent<StoreStaff>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void begin()
    {
        sbSprNum = Consumer.goodsNum;
        SetSprite(spriteRenderer);
        if (transform.parent.parent.name == "Store")
            StartCoroutine(move());
        else if (transform.parent.parent.name == "Hamburger")
            StartCoroutine(HamMove());
    }

    void SetSprite(SpriteRenderer _spriteRenderer)
    {
        switch (sbSprNum)
        {
            case 0:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 1:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 2:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 3:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 4:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 5:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 6:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 7:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            case 8:
                _spriteRenderer.sprite = goods[sbSprNum];
                break;

            default:
                break;
        }
    }

    IEnumerator move()
    {
        while (transform.position.y < 3.4)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + yVelo, transform.position.z), Time.deltaTime); ;
            yield return null;
        }
        if (transform.parent.tag == "Consumer")
        {
            storeStaffCS.CreateSpeechBalloons();
        }
        processIndex++;
        yield return new WaitForSeconds(0.3f);
        if (processIndex == 2)
            endSelling();
    }


    IEnumerator HamMove()
    {
        while (transform.position.y < 3.4)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + yVelo, transform.position.z), Time.deltaTime); ;
            yield return null;
        }
        if (transform.parent.tag == "Consumer")
        {
            storeStaffCS.CreateSpeechBalloons();
            yield return new WaitForSeconds(0.2f);
            //gameObject.SetActive(false);
            spriteRenderer.enabled = false;
        }
        else if(transform.parent.name == "Staff")
        {
            GameObject HamReceivedObj = GameObject.Find("HamReceivedPosition");
            HamReceivedTransform = HamReceivedObj.GetComponent<Transform>();
            while(transform.position != HamReceivedTransform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, HamReceivedTransform.position, Time.deltaTime*2); ;
                yield return null;
            }
            consumerCs.ToHamTrayProcess();
        }
        processIndex++;
    }

    public void endSellingProcess()
    {
        processIndex = 0;
        sbSprNum = 5;
        SetSprite(spriteRenderer);
        Invoke("DestroyBalloon", 0.5f);
    }

    public void endSelling()
    {
        for(int i = 0; i < SBList.Count; i++)
        {
            SBList[i].GetComponent<SpeechBalloon>().endSellingProcess();
        }
    }

    void DestroyBalloon()
    {
        for (int i = 0; i < SBList.Count; i++)
            SBList.RemoveAt(i);
        if(transform.parent.tag == "Consumer")
            consumerCs.ToExitGate();
        Destroy(this.gameObject);
    }
}
