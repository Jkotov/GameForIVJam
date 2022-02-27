using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlackScreenIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<SpriteRenderer>().DOColor(Color.clear, 10);
    }

}
