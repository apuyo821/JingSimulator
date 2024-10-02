using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBalloon : MonoBehaviour
{
    [SerializeField] Sprite[] goods;
    [SerializeField] float yVelo;
    [SerializeField] GameObject consumerObj;
    [SerializeField] HamburgerConsumer consumerCs;
    public SpriteRenderer spriteRenderer;


    public void setting()
    {
        consumerObj = transform.parent.gameObject;
        consumerCs = consumerObj.gameObject.GetComponentInParent<HamburgerConsumer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void begin(int _judgeNum)
    {
        SetSprite(_judgeNum);
        StartCoroutine(move());
    }

    void SetSprite(int _sprtieNum)
    {
        switch (_sprtieNum)
        {
            case 0:
                spriteRenderer.sprite = goods[_sprtieNum];
                break;

            case 1:
                spriteRenderer.sprite = goods[_sprtieNum];
                break;

            default:
                break;
        }
    }

    IEnumerator move()
    {
        yield return new WaitForSeconds(0.5f);
        while (transform.localPosition.y < 4)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), yVelo); ;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        DestroyBalloon();
    }

    void DestroyBalloon()
    {
        consumerCs.ToExitProcess();
        Destroy(this.gameObject);
    }
}
