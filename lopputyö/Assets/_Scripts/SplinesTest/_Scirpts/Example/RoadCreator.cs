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
        GetComponent<MeshFilter>().mesh = CreateRoadMesh(_points);

        //asetetaan tiling consistentiksi
        int _textureRepeat = Mathf.RoundToInt(tiling * _points.Length * spacing * 0.05f);
        GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, _textureRepeat);
    }

    Mesh CreateRoadMesh(Vector2[] _points)
    {
        Vector3[] _vertices = new Vector3[_points.Length * 2];//vieruspistetiä 2(n-1)
        int[] _triangles = new int[2 * (_points.Length - 1) * 3]; //kolmioita 2n, kolmion kulmia 3*2n
        Vector2[] _uvs = new Vector2[_vertices.Length];

        int _vertsIndex = 0;
        int _triIndex = 0;

        for (int i = 0; i < _points.Length; i++)
        {
            //muodostetaan välivektorit, vektorimatikalla
            Vector2 _forward = Vector2.zero;
            if(i < _points.Length - 1)
            {
                _forward += _points[i + 1] - _points[i];
            }
            if(i > 0)
            {
                _forward += _points[i] - _points[i - 1];
            }
            _forward.Normalize();
            Vector2 _left = new Vector2(_forward.y * -1, _forward.x); //90 asteen kulmassa oleva vektori

            //asetetaan aina kaksi pistettä, molemmille puolille
            _vertices[_vertsIndex] = _points[i] + _left * roadWidth * 0.5f;
            _vertices[_vertsIndex + 1] = _points[i] - _left * roadWidth * 0.5f; //oikea

            //meshin piirto laskenta
            float _completionPercent = i / (float)(_points.Length - 1);
            _uvs[_vertsIndex] = new Vector2(0, _completionPercent);
            _uvs[_vertsIndex + 1] = new Vector2(1, _completionPercent);

            if(i < _points.Length - 1)
            {
                //katso laskentalogiikka videosta, määritetään siis kolmiot pisteiden avulla
                _triangles[_triIndex] = _vertsIndex;
                _triangles[_triIndex + 1] = _vertsIndex + 2;
                _triangles[_triIndex + 2] = _vertsIndex + 1;

                _triangles[_triIndex + 3] = _vertsIndex + 1;
                _triangles[_triIndex + 4] = _vertsIndex + 2;
                _triangles[_triIndex + 5] = _vertsIndex + 3;
            }

            _vertsIndex += 2;
            _triIndex += 6;
        }

        Mesh _mesh = new Mesh();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;
        return _mesh;
    }
}
