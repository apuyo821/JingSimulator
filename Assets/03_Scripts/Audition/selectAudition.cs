using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectAudition : MonoBehaviour
{
    public void VocalGYM()
    {
        DataBase.DB.playerData.vocalCount = 22;
        DataBase.DB.playerData.GYMCount = 22;
        DataBase.DB.playerData.auditionIndex = 3;
    }
}
