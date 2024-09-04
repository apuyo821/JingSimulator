using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jingMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D jingRigid;
    [SerializeField] int veloX;
    [SerializeField] int coolTime;

    public bool movingBool = true;
    public bool veloControlBool = true;

    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Start()
    {
        jingRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        StartCoroutine(Moving());
        StartCoroutine(veloControl());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "RoomWall")
            veloX = veloX * -1;
    }

    IEnumerator Moving()
    {
        while (movingBool)
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
            jingRigid.velocity = new Vector2(veloX*5, jingRigid.velocity.y);
            yield return null;
        }
    }

    IEnumerator veloControl()
    {
        while (veloControlBool)
        {
            coolTime = Random.Range(2, 4);
            yield return new WaitForSeconds(coolTime);
            veloX = Random.Range(-2, 3) * 2;
        }
    }
}
