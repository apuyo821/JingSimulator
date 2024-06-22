using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStaff : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject staffObj;
    [SerializeField] Transform staffTransform;
    
    public float plusY;

    private void Start()
    {
    }

    public void CreateSpeechBalloons()
    {
        staffObj = GameObject.FindWithTag("Player");
        GameObject staffSB = Instantiate(prefab, new Vector3(staffObj.transform.position.x, staffObj.transform.position.y + plusY, staffObj.transform.position.z - 0.1f),transform.rotation);
        staffSB.transform.parent = staffObj.transform;
        staffSB.GetComponent<SpeechBalloon>().setting();
        staffSB.GetComponent<SpeechBalloon>().begin();
    }
}
