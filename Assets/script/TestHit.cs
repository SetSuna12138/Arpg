using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void beHit()
    {
        transform.DOShakePosition( 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
