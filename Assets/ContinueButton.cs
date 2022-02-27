using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Sprite activeSprite;

    private void Awake()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void Enable()
    {
        GetComponent<SpriteRenderer>().sprite = activeSprite;
        GetComponent<Collider2D>().enabled = true;
    }

    private void OnMouseUpAsButton()
    {
        door.Resume();
    }
}
