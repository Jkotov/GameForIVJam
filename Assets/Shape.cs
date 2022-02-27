using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Camera _camera;
    private bool _followingMouse;
    private Vector3 _prevMousePos;
    private Collider2D _collider2D;
    [HideInInspector] public Vector3 defaultPos;
    private List<Collider2D> _contacts;
    private List<Collider2D> _pinContacts;
    private List<Collider2D> _pins;

    private void Awake()
    {
        _camera = Camera.main;
        _collider2D = GetComponent<Collider2D>();
        defaultPos = transform.position;
        _pins = GetComponentsInChildren<Collider2D>().ToList();
        _pins.RemoveAll(item => !item.CompareTag("Pin"));
        _contacts = new List<Collider2D>();
        _pinContacts = new List<Collider2D>();
    }

    void Update()
    {
        if (_followingMouse)
        {
            var deltaMouse = _camera.ScreenToWorldPoint(Input.mousePosition) - _prevMousePos;
            transform.position += deltaMouse;
            _prevMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.mouseScrollDelta.y != 0)
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Mathf.Sign(Input.mouseScrollDelta.y));
            }
        }
    }
    
    private void OnMouseDown()
    {
        transform.position -= Vector3.forward;
        _followingMouse = true;
        _collider2D.isTrigger = true;
        _prevMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void CheckPins()
    {
        float minMagnitude = Single.MaxValue;
        Vector3 minDelta = Vector3.zero;
        foreach (var pin in _pins)
        {
            pin.GetContacts(_pinContacts);
            foreach (var contact in _pinContacts)
            {
                if (contact.CompareTag("Pin"))
                {
                    var delta = pin.transform.position - contact.transform.position;
                    delta.z = 0;
                    if (minMagnitude > delta.magnitude)
                    {
                        minMagnitude = delta.magnitude;
                        minDelta = delta;
                    }
                }
            }
        }
        transform.position -= minDelta;
    }
    private void OnMouseUp()
    {
        transform.position += Vector3.forward;
        _followingMouse = false;
        _collider2D.GetContacts(_contacts);
        _collider2D.isTrigger = false;
        foreach (var contact in _contacts)
        {
            if (contact.CompareTag("Keyhole"))
            {
                CheckPins();
                return;
            }
        }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _collider2D.isTrigger = false;
        transform.position = defaultPos;
        transform.rotation = Quaternion.identity;
    }
}
