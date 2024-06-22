using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionManager : MonoBehaviour
{
    public GameObject targetPosition;
    public GameObject CamObj;

    float RotationFloat = 20.0f;
    public float ChangeValue = 0.119f;

    IEnumerator MoveCam()
    {
        while(CamObj.transform.position != targetPosition.transform.position)
        {
            //Move To Target Position
            CamObj.transform.position = Vector3.MoveTowards(CamObj.transform.position, targetPosition.transform.position, 0.1f);

            //Move To Target Rotation
            if (RotationFloat > 0)
            {
                CamObj.transform.rotation = Quaternion.Euler(RotationFloat, 0, 0);
                RotationFloat -= ChangeValue;
            }
            yield return null;
        }
        //도착하는 시점
        Debug.Log("도착");
    }

    public void AuditionStart()
    {
        //코루틴은 따로 진행되기 때문에 일련의 작업을 하려면 같은 함수 내에서 코드를 계속 써줘야함
        //코루틴은 함수 내에서 2개 이상의 함수를 실행시킬 때 쓰기 좋은 듯
        StartCoroutine(MoveCam());
        //매끄럽게 목표 위치까지 옵젝을 옮기는게 목적이라서 굳이 코루틴을 쓸 필요가 없다가 내 가정 = 틀린 가정
        //코루틴에서 빼내어서 while을 돌려본 결과, 0.1초보다 빠른 속도로 타겟 좌표로 가있음
        //return null로 1프레임이라도 지연시키는게 Update와 같아진다.
        //return null을 어디에 넣냐에 따라서 지연되고 지연이 안된다
        //Update의 방식과 똑같이 하고 싶으면 While이 끝나는 직전에 넣어주면 Update와 거의 동일한 방식이된다.
    }
}