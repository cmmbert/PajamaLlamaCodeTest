using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    public float TimeToLive = 1;

    private float _timeAlive = 0;
    // Update is called once per frame
    void Update()
    {
        _timeAlive += Time.deltaTime;
        if (_timeAlive > TimeToLive)
            Destroy(gameObject);
    }
}
