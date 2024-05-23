using System;
using System.Collections.Generic;
using UnityEngine;

public class GeoJSONLoader : MonoBehaviour
{
    public TextAsset geoJsonFile; // Drag and drop your GeoJSON file in the Unity Editor
    public LineRenderer lineRenderer;
    public Transform earthSphere;
    public float earthRadius = 10f; // Set the radius of your earth sphere model

    public List<Vector2> coords = new List<Vector2>();

    private void Start()
    {
        ParseGeoJSON(geoJsonFile.text);
        SetupLineRenderer();
    }

    private void ParseGeoJSON(string fileContent)
    {
        // Split the file content into lines
        var lines = fileContent.Split(",");

        print(lines[112]);

        for(int i = 0; i < lines.Length; i+=2)
        {
            print(i);
            coords.Add(new Vector2(float.Parse(lines[i]), float.Parse(lines[i+1])));
        }
    }

    private void SetupLineRenderer()
    {
        lineRenderer.positionCount = coords.Count;
        Vector3[] positions = new Vector3[coords.Count];

        for (int i = 0; i < coords.Count; i++)
        {
            // Convert lat, lon to a position on the sphere
            float lat = coords[i].y * Mathf.Deg2Rad;
            float lon = coords[i].x * Mathf.Deg2Rad;

            positions[i] = new Vector3(
                earthRadius * Mathf.Cos(lat) * Mathf.Cos(lon),
                earthRadius * Mathf.Sin(lat),
                earthRadius * Mathf.Cos(lat) * Mathf.Sin(lon)
            );
        }

        lineRenderer.SetPositions(positions);

        //lineRenderer.Simplify(0.1f);
    }
}
