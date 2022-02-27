using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOnBridge : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private float playerYMove;
    [SerializeField] private SpriteRenderer shadow;
    private void OnMouseUpAsButton()
    {
        GameManager.Instance.TurnOffDoorColliders();
        GameManager.Instance.doorShadow = shadow;
        GameManager.Instance.playerYMove = playerYMove;
        GameManager.Instance.openingDoor = transform;
        door.gameObject.SetActive(true);
    }
}
