using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    public bool isEventing = false;
    public GameObject prefab;
    public static int goodsNum;

    [SerializeField] Sprite[] body;
    SpriteRenderer spriteRenderer;

    [SerializeField] float plusY;
    [SerializeField] float velo;
    [SerializeField] float velo3;
    float velo2;



    public void begin()
    {
        isEventing = false;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (transform.parent.name == "Store")
        {
            spriteRenderer.sprite = body[0];
            StartCoroutine(ToStoreCounter());
        }
        else if (transform.parent.name == "Hamburger")
        {
            spriteRenderer.sprite = body[1];
            StartCoroutine(ToHamCounter());
        }
        velo2 = velo;
    }

    private void OnTriggerEnter(Collider other)
    {
        isEventing = true;
        if(other.name == "SellingEvent")
        {
            Debug.Log(other.name);
            goodsNum = Random.Range(0, 4);
            GameObject speechBallonClone = Instantiate(prefab,new Vector3(transform.position.x, transform.position.y+ plusY, transform.position.z- 0.1f),transform.rotation);
            speechBallonClone.transform.parent = this.transform;
            speechBallonClone.GetComponent<SpeechBalloon>().setting();
            speechBallonClone.GetComponent<SpeechBalloon>().begin();
        }
        else if (other.name == "HamSellingEvent")
        {
            goodsNum = Random.Range(6,8);
            GameObject speechBallonClone = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + plusY, transform.position.z - 0.1f), transform.rotation);
            speechBallonClone.transform.parent = this.transform;
            speechBallonClone.GetComponent<SpeechBalloon>().setting();
            speechBallonClone.GetComponent<SpeechBalloon>().begin();
        }
        else if(other.name == "DestroyConsumer")
        {
            Destroy(this.gameObject);
        }
    }

    public void ToExitGate()
    {
        isEventing = false;
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
        SBcs.endSelling();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Consumer")
        {
            velo = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        velo = velo2;
    }
}
