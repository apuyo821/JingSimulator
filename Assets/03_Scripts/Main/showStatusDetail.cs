using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showStatusDetail : MonoBehaviour
{
    public int statusID;
    public GameObject detailInfoPanel;
    public TMP_Text tmpText;

    private void Start()
    {
        detailInfoPanel.SetActive(false);
    }

    public void showStatusDetailInfo()
    {
        detailInfoPanel.SetActive(true);
        switch (statusID)
        {
            //deft
            case 0:
                tmpText.text = "스탯이 오를 수록\n알바를 했을 때\n돈을 더 받을 \n확률이 높아집니다. ";
                break;

            //가창력
            case 1:
                tmpText.text = "노래를 더욱 잘 부르게 됩니다.";
                break;

            //근력
            case 2:
                tmpText.text = "스탯이 오를 수록\n체력을 소모하지 않을\n확률이 높아집니다.";
                break;

            //매력
            case 3:
                tmpText.text = "더욱 많은 사람이\n당신의 매력에 빠져버립니다.";
                break;

            //댄스
            case 4:
                tmpText.text = "춤을 더 잘 추게 됩니다.";
                break;

            //미숙함
            case 5:
                tmpText.text = "스탯이 오를 수록\n확률 적으로 정신력이\n더 많이 떨어집니다.";
                break;

            default:
                break;
        }
    }

    public void pointerExit()
    {
        detailInfoPanel.SetActive(false);
    }
}
