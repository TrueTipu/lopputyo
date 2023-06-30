using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class StreamCreator : MonoBehaviour
{
    [Range(0.05f, 1.5f)]
    [SerializeField] float meshSpacing = 1;
    [SerializeField] int particleIntensity = 10;

    [SerializeField] GameObject particlePrefab;

    [SerializeField] float width = 1;
    [SerializeField] float tiling = 1;

    [field: SerializeField] public bool AutoUpdate { get; private set; } = true;

    List<GameObject> particles = new List<GameObject>();

    float streamMove;
    [SerializeField] float streamMoveSpeed = 1;
    private void Update()
    {
        streamMove = (streamMove + Time.deltaTime * streamMoveSpeed) % 1;
        GetComponent<MeshRenderer>().sharedMaterial.mainTextureOffset = new Vector2(0, streamMove);
    }

    public void UpdateStream()
    {
        Path _path = GetComponent<PathCreator>().Path;
        Vector2[] _points = _path.CalculateEvenlySpacedPoints(meshSpacing);
        GetComponent<MeshFilter>().mesh = RoadMeshCreator.CreateRoadMesh(_points, width);

        //asetetaan tiling consistentiksi
        int _textureRepeat = Mathf.RoundToInt(tiling * _points.Length * meshSpacing * 0.05f);
        GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, _textureRepeat);
    }
    public void UpdateParticles()
    {
        Path _path = GetComponent<PathCreator>().Path;
        Vector2[] _points = _path.CalculateEvenlySpacedPoints(meshSpacing);
        SpawnParticles(_points);
    }
    
    void SpawnParticles(Vector2[] _points)
    {
        foreach (GameObject _particle in particles)
        {
            DestroyImmediate(_particle);
        }
        particles.Clear();

        for (int i = 0; i < _points.Length; i += particleIntensity)
        {
            particles.Add(Instantiate(particlePrefab, _points[i], Quaternion.LookRotation(RoadMeshCreator.CalculateForwardVector(i, _points, particleIntensity)), transform));
        }
    }
}
