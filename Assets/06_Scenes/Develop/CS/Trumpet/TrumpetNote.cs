using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetNote : MonoBehaviour
{
    public GameObject[] trumObj;

    public GameObject Head;
    public GameObject Body;
    public GameObject Foot;

    public bool headHit = false;
    public bool footHit = false;

    public float noteTime = 0;

    public float velo = 1;

    private void Update()
    {
        transform.Translate(Vector3.left * velo);
    }

    //여기 아래는 나중에 활성화 할 예정
    //랜덤으로 노트 사이즈 정할 때 사용할 코드들 
    /*
    float scale, halfYN, i;

    void noteSet()
    {
        i = Random.Range(0, 3);
        if(i == 0)
            halfYN = 1;
        else
            halfYN = Random.Range(0,2);

        if (halfYN == 1)
        {
            i += 0.5f;
            scale = i;
        }
        else if (halfYN == 0)
        {
            scale = i;
        }

        Body.transform.localScale = new Vector2(scale, Body.transform.localScale.y);

        float M = (((scale / 0.5f) * -5) + (-2.75f));

        Head.transform.position = new Vector2(M, transform.position.y);
        Foot.transform.position = new Vector2(M * -1, transform.position.y);
    }
    */
}
