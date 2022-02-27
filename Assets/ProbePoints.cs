using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProbePoints : MonoBehaviour
{
    
    [SerializeField] private GameObject probePointPrefab;
    private List<ProbePoint> _probePoints;
    private List<ProbePoint> _probePointsOut;
    private PolygonCollider2D _collider2D;


    public bool CheckWin()
    {
        if (Input.GetMouseButton(0))
            return false;
        foreach (var probe in _probePointsOut)
        {
            if (probe.isOnKey)
                return false;
        }
        foreach (var probe in _probePoints)
        {
            if (!probe.isOnKey)
                return false;
        }

        return true;
        //SceneManager.LoadScene("Final");
    }
    
    private void Awake()
    {
        _collider2D = GetComponent<PolygonCollider2D>();
        GenerateProbePoints();
    }

    void GenerateProbeLine(Vector2 start, Vector2 end)
    {
        var probeRadius = probePointPrefab.GetComponent<CircleCollider2D>().radius;
        int probePointCount = (int)((end - start).magnitude / probeRadius * 2);
        var delta = (end - start) / probePointCount;
        var deltaOutPoint = delta.Rotate(90);
        for (int i = 0; i < probePointCount; i++)
        {
            var tmpGO = Instantiate(probePointPrefab, start + delta * i, Quaternion.identity, transform);
            _probePoints.Add(tmpGO.GetComponent<ProbePoint>());
            var tmpPPO = Instantiate(probePointPrefab, tmpGO.transform.position + (Vector3)deltaOutPoint, Quaternion.identity, transform);
            _probePointsOut.Add(tmpPPO.GetComponent<ProbePoint>());
        }
    }
    void GenerateProbePoints()
    {
        _probePoints = new List<ProbePoint>();
        _probePointsOut = new List<ProbePoint>();
        var colliderPoints = _collider2D.points;
        for (int i = 0; i < colliderPoints.Length; i++)
        {
            colliderPoints[i] = transform.TransformPoint(colliderPoints[i]);
        }

        for (int i = 0; i < colliderPoints.Length; i++)
        {
            GenerateProbeLine(colliderPoints[i], colliderPoints[(i + 1) % colliderPoints.Length]);
        }
    }
}
