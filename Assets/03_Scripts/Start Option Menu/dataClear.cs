using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataClear : MonoBehaviour
{
    [SerializeField] GameObject DBObject;
    [SerializeField] DataBase DBScript;

    private void Start()
    {
        DBObject = GameObject.Find("DataBase");
    }

    public void dataClearProcess()
    {
        DBObject = GameObject.Find("DataBase");
        DBScript = DBObject.GetComponent<DataBase>();
        DBScript.DataClear();
    }
}
