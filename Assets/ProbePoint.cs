using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbePoint : MonoBehaviour
{
    private ContactFilter2D contactFilter2D;
    public bool isOnKey;
    public List<ProbePoint> probePoints;
    private Collider2D _collider2D;
    private SpriteRenderer _renderer;
    private List<Collider2D> _contacts;
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _contacts = new List<Collider2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        _collider2D.OverlapCollider(contactFilter2D.NoFilter(), _contacts);
        
        isOnKey = false;
        foreach (var contact in _contacts)
        {
            if (contact.CompareTag("key"))
            {
                isOnKey = true;
                break;
            }
        }
        if (_renderer)
        {
            if (isOnKey)
                _renderer.color = Color.red;
            else
                _renderer.color = Color.green;
        }
        
    }
}

