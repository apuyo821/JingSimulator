using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetNote : MonoBehaviour
{
    public bool headHit = false;
    public bool footHit = false;

    public float noteTime = 0;

    public float velo = 1;

    private void Update()
    {
        transform.Translate(Vector3.left * velo);
    }

    public void DestroyTimer()
    {
        Invoke("Destroy", 3.0f);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
