using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShapeStack : MonoBehaviour
{
    [SerializeField] private GameObject figurePrefab;
    [SerializeField] private int figuresCount;
    private TextMeshPro _textMeshPro;
    private List<Shape> _shapes = new List<Shape>();
    private bool _isActiveShape;

    private void Awake()
    {
        for (int i = 0; i < figuresCount; i++)
        {
            var tmp = Instantiate(figurePrefab, transform.position, Quaternion.identity, transform);
            _shapes.Add(tmp.GetComponent<Shape>());
            tmp.SetActive(false);
        }
        _shapes[0].gameObject.SetActive(true);
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        //UpdateText();
    }

    private void UpdateText()
    {
        int count = 0;

        foreach (var shape in _shapes)
        {
            if (!shape.gameObject.activeInHierarchy)
                count++;
        }

        if (_isActiveShape)
            count++;
        _textMeshPro.text = count.ToString();
    }

    private void Update()
    {
        _isActiveShape = false;
        foreach (var shape in _shapes)
        {
            if (shape.gameObject.activeInHierarchy && shape.transform.position == shape.defaultPos)
            {
                if (!_isActiveShape)
                {
                    _isActiveShape = true;
                    continue;
                }
                shape.gameObject.SetActive(false);
            }
        }

        if (!_isActiveShape)
        {
            foreach (var shape in _shapes)
            {
                if (!shape.gameObject.activeInHierarchy)
                {
                    shape.gameObject.SetActive(true);
                    break;
                }
            }
        }
        UpdateText();
    }
}
