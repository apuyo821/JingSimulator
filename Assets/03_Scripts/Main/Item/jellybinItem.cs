using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class jellybinItem : MonoBehaviour
{
    [SerializeField] TMP_Text tMP_Text;

    public void resetPanel()
    {
        tMP_Text.text = "OO이(가) n만큼\nMM 했습니다.";
        gameObject.SetActive(false);
    }
}
