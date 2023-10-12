using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

///Ei omista editorscriptiä, referensöitävä, muun koodin käytettäväksi
public class StreamCreator : MonoBehaviour
{

    [Range(0.05f, 1.5f)]
    [SerializeField] float meshSpacing = 1;
    [Range(0.05f, 1.5f)]
    [SerializeField] float particleSpacing = 1;

    [SerializeField] GameObject particlePrefab;

    [SerializeField] float width = 1;
    [SerializeField] float tiling = 1;

    List<GameObject> particles = new List<GameObject>();

    [SerializeField] float streamMoveSpeed = 1;
    float streamMove;

    Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        streamMove = (streamMove + Time.deltaTime * streamMoveSpeed) % 1;
        mat.mainTextureOffset = new Vector2(0, streamMove);
    }

    public void UpdateStream(Path _path)
    {

        Vector2[] _points = _path.CalculateEvenlySpacedPoints(meshSpacing);
        this.GetComponent<MeshFilter>().mesh = RoadMeshCreator.CreateRoadMesh(_points, width);

        //asetetaan tiling consistentiksi
        int _textureRepeat = Mathf.RoundToInt(tiling * _points.Length * meshSpacing * 0.05f);
        this.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, _textureRepeat);
    }
    public void UpdateParticles(Path _path)
    {
        Vector2[] _points = _path.CalculateEvenlySpacedPoints(particleSpacing);
        SpawnParticles(_points);
    }

    void SpawnParticles(Vector2[] _points)
    {
        foreach (GameObject _particle in particles)
        {
            DestroyImmediate(_particle);
        }
        particles.Clear();

        for (int i = 0; i < _points.Length; i += 1)
        {
            particles.Add(Instantiate(particlePrefab, _points[i], Quaternion.LookRotation(RoadMeshCreator.CalculateForwardVector(i, _points, 3)), transform));
        }
    }
}
