using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadCreator : MonoBehaviour
{
    [Range(0.05f, 1.5f)]
    [SerializeField] float spacing = 1;

    [SerializeField] float roadWidth = 1;
    [SerializeField] float tiling = 1;

    [field: SerializeField] public bool AutoUpdate { get; private set; }

    public void UpdateRoad()
    {
        Path _path = GetComponent<PathCreator>().Path;
        Vector2[] _points = _path.CalculateEvenlySpacedPoints(spacing);
        GetComponent<MeshFilter>().mesh = RoadMeshCreator.CreateRoadMesh(_points, roadWidth);

        //asetetaan tiling consistentiksi
        int _textureRepeat = Mathf.RoundToInt(tiling * _points.Length * spacing * 0.05f);
        GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, _textureRepeat);
    }
}
