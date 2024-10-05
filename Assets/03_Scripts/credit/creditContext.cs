using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class creditContext : MonoBehaviour
{
    [SerializeField] int memberIndex;
    [SerializeField] Sprite[] favoriteSprite;
    [SerializeField] Image favoriteImage;
    string contextDummy = "";
    [SerializeField] TMP_Text context;
    [SerializeField] TMP_Text nameText;

    private void OnEnable()
    {
        context.text = "";
        nameText.text = "";
        favoriteImage.sprite = null;
    }

    public void showSoGam()
    {
        switch (memberIndex)
        {
            //아퍼
            //비챤
            case 0:
                contextDummy = "팀원분들이 흔쾌히 섭외 제안을 승락해준 것도, 이렇게 게임을 만들어 낸 것도 모든게 꿈만 같습니다. 모든 것이 처음인 상황에서 여러 실수와 팀장으로서 미숙한 모습들을 보여줬음에도 믿고 끝까지 열심히 해주신 모든 분들께 감사를 전합니다. 너무나 귀중한 경험을 하게 해주셔서 감사합니다. 고생하셨어요 모두. 게임 플레이 해주셔서 감사합니다. 생일 축하드려요!!";
                favoriteImage.sprite = favoriteSprite[6];
                context.text = contextDummy;
                nameText.text = "아퍼";
                break;

            //캔버스
            //주르르
            case 1:
                contextDummy = "처음 팀장을 하게 되어 많이 휘청거리고 사고가 나는 일도 있었습니다. 그럼에도 팀원분들께서 많이 도와주시고 믿어주신 덕분에 짧았던 시간이었지만 지금 이 자리에 있는 기분이 듭니다. 게임 재미있게 플레이 하셨으면 합니다! 감사합니다.";
                favoriteImage.sprite = favoriteSprite[4];
                context.text = contextDummy;
                nameText.text = "캔버스도둑피카소";
                break;

            //kiny.
            case 2:
                contextDummy = "";
                favoriteImage.sprite = favoriteSprite[memberIndex];
                context.text = contextDummy;
                nameText.text = "kiny.";
                break;

            //김커일
            case 3:
                contextDummy = "";
                favoriteImage.sprite = favoriteSprite[memberIndex];
                context.text = contextDummy;
                nameText.text = "김커일";
                break;

            //뭉개구름이
            //아이네
            case 4:
                contextDummy = "Woof Woof ( 다들 수고하셨습니다! 킹애 )";
                favoriteImage.sprite = favoriteSprite[1];
                context.text = contextDummy;
                nameText.text = "뭉개구름이";
                break;

            //키밍
            //아이네
            case 5:
                contextDummy = "첫 참가라 많이 미숙했지만 다들 친절하셔서 즐겁게 작업했습니다! 징버거님 생일축하드려요!!";
                favoriteImage.sprite = favoriteSprite[1];
                context.text = contextDummy;
                nameText.text = "키밍";
                break;

            //샤가
            //고세구
            case 6:
                contextDummy = "시뮬레이션 게임은 처음 참여해보는지라 흥미롭고 재밌었습니다. 좋은분들과 좋은 작품 만든거같아 즐거웠습니다 킹아~!";
                favoriteImage.sprite = favoriteSprite[5];
                context.text = contextDummy;
                nameText.text = "샤가";
                break;

            //냐릔
            //징버거
            case 7:
                contextDummy = "생일 축전 게임 개발의 일러스트 부분으로 참여할 수 있게 되어 영광이었습니다! 여러 버전의 버거님 일러스트 작업 너무 너무 재밌었어용 다른 팀원분들도 수고 많으셨습니다!! 버거님 생일 축하드려요!!!";
                favoriteImage.sprite = favoriteSprite[2];
                context.text = contextDummy;
                nameText.text = "냐릔";
                break;

            //오메늘영
            case 8:
                contextDummy = "";
                favoriteImage.sprite = favoriteSprite[memberIndex];
                context.text = contextDummy;
                nameText.text = "오메늘영";
                break;

            //짭짜름
            //형
            case 9:
                contextDummy = "엔딩 일러스트 작업했습니다. 왁타작업은 처음이라 미숙한 점도 많았지만 재밌고, 새로운 경험이었습니다~~ 재미있게 플레이하셨길 바라요";
                favoriteImage.sprite = favoriteSprite[0];
                context.text = contextDummy;
                nameText.text = "짭짜름";
                break;

            //초코아보
            //형
            case 10:
                contextDummy = "처음으로 팬게임에 함께하게 되어서 많이 떨리기도 했는데 너무 재밌고 새로웠습니다! 다들 수고많았습니다~!!";
                favoriteImage.sprite = favoriteSprite[0];
                context.text = contextDummy;
                nameText.text = "초코아보";
                break;

            //디농
            //아이네
            case 11:
                contextDummy = "좋은 기회로 참여할 수 있게 되어 기쁩니다.  다 함께 만들어 낸 선물이 마음에 드셨으면 좋겠어요!";
                favoriteImage.sprite = favoriteSprite[1];
                context.text = contextDummy;
                nameText.text = "디농";
                break;

            //뉸서
            case 12:
                contextDummy = "";
                favoriteImage.sprite = favoriteSprite[memberIndex];
                context.text = contextDummy;
                nameText.text = "뉸서";
                break;

            //마카1117
            //비챤
            case 13:
                contextDummy = "사실 별로 한게 없지만 이번 징육시 팀에서 일할수 있어서 영광이고 징버거님에게 이렇게 크게 조공 할수있게되서 영광이었습니다~!";
                favoriteImage.sprite = favoriteSprite[6];
                context.text = contextDummy;
                nameText.text = "마카1117";
                break;

            //종멸
            case 14:
                contextDummy = "";
                favoriteImage.sprite = favoriteSprite[memberIndex];
                context.text = contextDummy;
                nameText.text = "종멸";
                break;

            default:
                break;
        }
    }

    public void throwMemberIndex(int _memberindex)
    {
        memberIndex = _memberindex;
    }
}
