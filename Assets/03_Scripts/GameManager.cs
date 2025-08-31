using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public enum EBar
{
    HealthPoint,
    MentalPoint
}

[Serializable]
public class Bar
{
    public string Name;
    public EBar Type;
    public Slider Slider;

    public Bar(String name ,EBar type, Slider slider)
    {
        Name = name;
        Type = type;
        Slider = slider;
    }
}

[Serializable]
public class Info
{
    public Info(string _Type, string _conversion1, string _conversion2, string _conversion3, string _conversion4)
    {
        Type = _Type; conversion1 = _conversion1; conversion2 = _conversion2; conversion3 = _conversion3; conversion4 = _conversion4;
    }

    public string Type, conversion1, conversion2, conversion3, conversion4;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] OptionManager optionManager;

    public GameObject[] UIObj;
    public Text[] Texts;

    public Bar[] bars;

    public TextAsset InfoDB;            //각 행동의 이름과 스탯 변환값가 써져있는 텍스트
    public List<Info> InfoList;         //클래스 Info의 List 형태
    public GameObject panel;            //세부사항 패널
    public GameObject[] dragObj;        //슬롯의 포지션 값을 얻기 위한 오브젝트 더미

    public static int slotNum;

    private void Awake()
    {
        UISet(false);
        Invoke("framelimit", 2f);
        //텍스트 파일에 있는 값들 유니티로 불러오기
        //텍스트 파일에 텍스트 들을 \n(엔터)를 기준으로 나누기 - 총 13개의 배열이 생성 된다
        string[] line = InfoDB.text.Substring(0, InfoDB.text.Length - 1).Split('\n');

        //13개의 배열까지, \t(탭)을 기준으로 나누어 각각 row에 저장하고, 이 값들을 각각 리스트에 저장한다.
        //저장할 때 마다 새로운 InfoList에 저장하기에 13개의 List가 생성된다.
        //Line[0] == row[0], row[1], row[2], row[3], row[4] == InfoList[0]
        //Line과 InfoList의 차이점은 Line은 row[0, 4]가 한줄로 직렬로 쭉 이어져있다면, InfoList는 Class이기 때문에
        //각각의 변수에 별개로 저장되어있다
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            InfoList.Add(new Info(row[0], row[1], row[2], row[3], row[4]));
        }
    }

    //  게임 시작 시 Frame 락 걸기
    void framelimit()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        // 게임을 새롭게 시작할 시 볼륨 및 기본 데이터 설정
        SetMaxHPMental(DataBase.DB.playerData.MaxHP, DataBase.DB.playerData.MaxMP);
        daySet(DataBase.DB.playerData.dDay);
        optionManager.volumeSlider[0].value = AudioManager.mainAudioVolume;
        optionManager.volumeSlider[1].value = AudioManager.sfxAudioVolume;
    }

    private void Update()
    {
        SetHPnMP(DataBase.DB.playerData.HP, DataBase.DB.playerData.MP);

        if (DataBase.DB.playerData.HP > 50)
            DataBase.DB.playerData.HP = 50;
        if (DataBase.DB.playerData.MP > 25)
            DataBase.DB.playerData.MP = 25;
        if (DataBase.DB.playerData.HP < 0)
            DataBase.DB.playerData.HP = 0;
        if (DataBase.DB.playerData.MP < 0)
            DataBase.DB.playerData.MP = 0;

        if (Input.GetKeyDown(KeyCode.Escape))
            UISet(false);
    }

    public void SetMaxHPMental(int _health, int _mental)
    {
        bars[0].Slider.maxValue = _health;
        bars[0].Slider.value = _health;
        bars[1].Slider.maxValue = _mental;
        bars[1].Slider.value = _mental;
    }

    public void SetHPnMP(int _HP, int _MP)
    {
        bars[0].Slider.value = _HP;
        bars[1].Slider.value = _MP;
    }

    public void daySet(int _dDay)
    {
        if (_dDay == 0)
        {
            Texts[0].text = "D-Day";
        }
        else
        {
            Texts[0].text = "D" + "-" + _dDay.ToString();
        }
    }

    //IPointerExitHandler로 함수를 만드는게 아닌 유니티의 인스펙터에서 Event Trigger 컴포넌트를 추가하고 거기에서
    //PointerExit 이벤트를 사용하기에 평소에 보던 함수 형태이다
    //포인터가 슬롯에서 벗어날 시 패널 끄기
    public void PointerExit()
    {
        panel.SetActive(false);
    }

    //Input.GetMouseButtonDown(1) - 우클릭 감지, 우클릭 시 패널과 패널을 슬롯의 살짝 위에 띄우고, 행동의 이름과 스탯의 변환치를 텍스트로 보여줌
    //행동에 따라서 InfoList에 공백이 생기는 경우가 있다. 이것은 infoText.text = test.Trim(); 이 코드로 해결
    //텍스트를 바꿀 때 \n을 이용해 줄바꿈을 해준다. 이 경우 공백임에도 줄바꿈을 해주기 때문에 패널이 못생겨진다
    //그때, Trim()으로 \n을 없애기에 줄바꿈이 없어져 텍스트가 패널의 중간에 예쁘게 들어맞게 된다.
    //1, 패널 띄우고 위치 조정. 2, 패널에 넣을 텍스트 준비 및 공백 없애기. 3, 패널에 텍스트 넣기
    public void PointClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            panel.SetActive(true);
            panel.transform.position = new Vector3(dragObj[slotNum].transform.position.x, dragObj[slotNum].transform.position.y + 160, dragObj[slotNum].transform.position.z);

            Text infoText = panel.GetComponentInChildren<Text>();
            string text =
                InfoList[slotNum].Type + "\n" +
                InfoList[slotNum].conversion1 + "\n" +
                InfoList[slotNum].conversion2 + "\n" +
                InfoList[slotNum].conversion3 + "\n" +
                InfoList[slotNum].conversion4;
            infoText.text = text.Trim();
        }
    }

    void UISet(bool p_flag)
    {
        for (int i = 0; i < UIObj.Length; i++)
        {
            UIObj[i].SetActive(p_flag);
        }
    }
}
