using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusEffect : MonoBehaviour
{
    public Sprite[] sprite;
    SpriteRenderer spriteRenderer;

    Vector3 targetPosition;
    public float moveVelo = 0.15f;
    public int statusType;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void iconMoving()
    {
        transform.localPosition = new Vector3(0, 6, 0);
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        targetPosition = new Vector3(transform.position.x, transform.position.y + 2.5f, 0);
        switch (statusType)
        {
            //str
            case 0:
                spriteRenderer.sprite = sprite[statusType];
                break;

            //deft
            case 1:
                spriteRenderer.sprite = sprite[statusType];
                break;

            //misukham
            case 2:
                spriteRenderer.sprite = sprite[statusType];
                break;

            default:
                break;
        }
        StartCoroutine(objMoving());
    }

    IEnumerator objMoving()
    {
        while (ScheduleManager.isActing)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveVelo);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
