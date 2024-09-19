using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpriteManager : MonoBehaviour
{
    Image t_image;
    Sprite t_sprite;

    bool CheckSameSprite(Image p_image, Sprite p_sprite)
    {
        if (p_image.sprite == p_sprite)
            return true;
        else
            return false;
    }

    // p_target: 어떤 이미지를 변경할 것인지, p_spriteName: 어떤 이미지로 변경할 것인지, p_direction : 이미지의 위치는 어디인지
    public void SpriteChangeCoroutine(Transform p_target, string p_spriteName)
    {
        // 1. t_image 이미지를 변경
        // Standing Image 오브젝트에는 Image 컴포넌트 X → 그 자식인 Image 오브젝트에는 Image 컴포넌트 O
        t_image = p_target.GetChild(0).GetComponent<Image>();

        // 2. t_sprite 이미지로 변경
        //Characters 안에서 p_spriteName와 같은 이름의 이미지를 가여와서 t_sprite에 덮기
        p_spriteName = p_spriteName.Trim(); // 공백 제거
        string path = "Image/Characters/" + p_spriteName;
        t_sprite = Resources.Load<Sprite>(path);

        //SettingAcP(t_image.rectTransform, p_direction);     //화자에 따라서 이미지 위치 바꾸기

        // 두 이미지(Sprite)가 같지 않으면 새 이미지로 변경
        if (!CheckSameSprite(t_image, t_sprite))
        {
            t_image.sprite = t_sprite;     // 이미지 교체
        }
    }

    /*
    void SettingAcP(RectTransform a_rectTransform, int a_direction)
    {
        Vector2 direction = new Vector2();

        switch (a_direction)
        {
            case 0:
                //왼쪽, Left
                direction = new Vector2(0, 0);
                break;

            case 1:
                //오른쪽, Right
                direction = new Vector2(1, 0);
                break;

            default:
                break;
        }
        setMMP(a_rectTransform, direction);
    }*/

    void setMMP(RectTransform b_rectTransform, Vector2 _direction)
    {
        b_rectTransform.anchorMin = _direction;
        b_rectTransform.anchorMax = _direction;
        b_rectTransform.pivot = _direction;
    }
}
