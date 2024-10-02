using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class EndingTransition : MonoBehaviour
{
    int movingIndex = 0, endingType;

    public Sprite[] sprites;
    public GameObject[] spritesObjs;
    public SpriteRenderer whiteImageRender;
    Color changeColor;
    public float startSpotTime;
    public float endSpotTime;

    public GameObject volumeObj;
    public Volume volume;
    Vignette vignette;
    DepthOfField depth;
    Bloom bloom;

    public GameObject cameraObject;
    Vector3 targetPosition = new Vector3();
    public float moveVelo = 0.02f;

    bool isFirstFlash = true;
    bool isMoving = true;
    bool isCamObjMoving = true;
    bool isBluring = true;

    Camera mainCamera;

    [SerializeField] GameObject goTitleButton;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraObject.SetActive(false);

        //필요한 컴포넌트들 얻기
        volume = volumeObj.gameObject.GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out depth);
        volume.profile.TryGet(out bloom);

        //포스트 프로세싱 적용 및 해제
        depth.active = false;
        vignette.active = false;
        bloom.active = true;

        for (int i = 0; i < spritesObjs.Length; i++)
        {
            spritesObjs[i].SetActive(false);
        }
    }

    public void ProcessStart(int _endingType)
    {
        cameraObject.SetActive(true);
        mainCamera.orthographic = false;
        Debug.Log(_endingType);
        endingType = _endingType;
        if (endingType == 4 || endingType == 1 || endingType == 2 || endingType == 3)
        {
            SpriteRenderer fullIllustRender = spritesObjs[6].GetComponent<SpriteRenderer>();  //엔딩 일러스트
            fullIllustRender.sprite = sprites[_endingType];
        }
        else
        {
            SpriteRenderer spriteRenderer = spritesObjs[2].GetComponent<SpriteRenderer>();  //엔딩 일러스트
            spriteRenderer.sprite = sprites[_endingType];
            spriteRenderer = spritesObjs[3].GetComponent<SpriteRenderer>();  //엔딩 일러스트
            spriteRenderer.sprite = sprites[_endingType];
        }
        for (int i = 0; i < spritesObjs.Length; i++)
        {
            spritesObjs[i].SetActive(true);
        }
        spritesObjs[6].SetActive(false);
        spritesObjs[2].SetActive(false);
        spritesObjs[4].SetActive(false);
        volumeObj.SetActive(true);
        StartCoroutine(Process());
    }

    //index를 확인하여 카메라 무빙 설정
    void checkIndex(int _index)
    {
        switch (_index)
        {
            //오른쪽 아래
            case 0:
                cameraObject.transform.position = new Vector3(240, -290, 275);
                targetPosition = new Vector3(590, -290, 275);
                break;

            //왼쪽 위
            case 1:
                cameraObject.transform.position = new Vector3(-528, -85, 275);
                targetPosition = new Vector3(-265, -85, 275);
                break;

            //풀 샷
            case 2:
                cameraObject.transform.position = new Vector3(0, 0, 350);
                targetPosition = new Vector3(0, 0, 0);
                break;

            default:
                break;
        }
    }

    IEnumerator Process()
    {
        //First Moving

        if(endingType == 4 || endingType == 1 || endingType == 2 || endingType == 3)
        {
            spritesObjs[6].SetActive(true);
            spritesObjs[5].SetActive(false);
            spritesObjs[3].SetActive(false);

        }
        else
        {
            spritesObjs[5].SetActive(true); //사진관 배경
        }

        checkIndex(movingIndex);
        StartCoroutine(flash());
        //yield return new WaitUntil(() => isBluring == false);
        yield return new WaitForSeconds(2.7f);
        StartCoroutine(CameraMoving());

        yield return new WaitUntil(() => isMoving == false);
        //Second Moving
        checkIndex(movingIndex);
        vignette.active = true;
        depth.active = true;
        StartCoroutine(blurEffect());
        yield return new WaitUntil(() => depth.focusDistance.value >= 0.8f);
        StartCoroutine(CameraMoving());

        yield return new WaitUntil(() => isMoving == false);
        //Last Moving
        checkIndex(movingIndex);
        StartCoroutine(flash());

        if (endingType == 4 || endingType == 1 || endingType == 2 || endingType == 3)
        {
            
        }
        else
        {
            spritesObjs[6].SetActive(false);
            spritesObjs[3].SetActive(false);
            spritesObjs[2].SetActive(true);
            spritesObjs[4].SetActive(true);
        }
        depth.active = true;
        depth.focusDistance.value = 0.1f;
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => isBluring == false);
        StartCoroutine(CameraMoving());
        yield return new WaitUntil(() => isMoving == false);
        depth.active = false;
        vignette.active = false;
        bloom.intensity.value = 0.3f;
    }

    //Flash Light Effect
    IEnumerator flash()
    {
        isFirstFlash = true;
        spritesObjs[0].SetActive(true);
        spritesObjs[1].SetActive(true);
        if (movingIndex == 0)
            yield return new WaitForSeconds(2.5f);
        else
            yield return null;

        whiteImageRender.color = new Color(255, 255, 255, 0.5f);
        changeColor = new Color(255, 255, 255, 0.5f);
        spritesObjs[0].gameObject.SetActive(false);
        while (changeColor.a <= 1f)
        {
            changeColor.a += startSpotTime;
            yield return null;
            whiteImageRender.color = changeColor;
        }
        yield return new WaitForSeconds(0.23f);
        vignette.active = true;
        depth.active = true;
        while (changeColor.a >= 0.1f)
        {
            changeColor.a -= endSpotTime;    //0:0.068
            yield return null;
            whiteImageRender.color = changeColor;
            if (changeColor.a <= 0.9f && isFirstFlash)
            {
                isFirstFlash = false;
                StartCoroutine(blurEffect());
                yield return null;
            }
        }
        spritesObjs[1].SetActive(false);
        whiteImageRender.color = new Color(255, 255, 255, 0);
    }

    //Blur Effect & choice Moving type
    IEnumerator blurEffect()
    {
        isBluring = true;
        depth.focusDistance.value = 0.1f;
        vignette.intensity.value = 0.54f;
        while (depth.focusDistance.value <= 2.45f)
        {
            depth.focusDistance.value += 0.07f;
            vignette.intensity.value -= 0.015f;
            yield return null;
            if (depth.focusDistance.value >= 0.8f && isBluring)
            {
                isBluring = false;
            }
        }
        depth.active = false;
        vignette.active = false;
    }

    //카메라 무빙 조절
    IEnumerator CameraMoving()
    {
        isMoving = true;
        isCamObjMoving = true;
        if (movingIndex < 2)
            StartCoroutine(objMoving(isCamObjMoving));
        else if (movingIndex == 2)
            StartCoroutine(lastCameraMoving());
        movingIndex++;
        yield return new WaitForSeconds(2f);
        isCamObjMoving = false;
        isMoving = false;
    }

    //첫, 두번째 카메라 무빙
    IEnumerator objMoving(bool _move)
    {
        while (_move)
        {
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, targetPosition, moveVelo);
            yield return null;
        }
    }

    //마지막 카메라 무빙
    IEnumerator lastCameraMoving()
    {
        if (endingType == 4 || endingType == 1 || endingType == 2 || endingType == 3)
        {

        }
        else
        {
            spritesObjs[6].SetActive(false);
            spritesObjs[3].SetActive(false);
            spritesObjs[2].SetActive(true);
            spritesObjs[4].SetActive(true);
        }

        moveVelo = 0.0001f;
        yield return new WaitForSeconds(2f);
        while (cameraObject.transform.position.z <= targetPosition.z -2)
        {
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, targetPosition, moveVelo);
            yield return null;
        }
        goTitleButton.SetActive(true);
    }

    public void goTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
