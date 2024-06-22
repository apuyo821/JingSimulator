using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ViewHPMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Text[] Texts;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Texts[0].gameObject.SetActive(true);
        Texts[1].gameObject.SetActive(true);

        Texts[0].text = DataBase.DB.playerData.HP + "/50";
        Texts[1].text = DataBase.DB.playerData.MP + "/25";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Texts[0].gameObject.SetActive(false);
        Texts[1].gameObject.SetActive(false);
    }
}
