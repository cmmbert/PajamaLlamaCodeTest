using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthHeart : MonoBehaviour
{
    public GameObject HeartFill;
    
    public void SetState(bool filled)
    {
        HeartFill.SetActive(filled);
    }
}
