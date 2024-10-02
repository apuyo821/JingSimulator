using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawingEventReset : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [SerializeField] GameObject drawingWorld;

    public void resetWorld()
    {
        Destroy(drawingWorld);

        drawingWorld = Instantiate(prefab);
        
    }
}
