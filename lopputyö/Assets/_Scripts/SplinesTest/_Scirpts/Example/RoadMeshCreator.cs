using UnityEngine;
using System.Collections;

public static class RoadMeshCreator
{
    public static Vector2 CalculateForwardVector(int _index, Vector2[] _points, int _lookPointsAmount = 1)
    {
        //muodostetaan välivektorit, vektorimatikalla
        Vector2 _forward = Vector2.zero;

        for (int i = 0; i < _lookPointsAmount; i++)
        {
            if (_index + i < _points.Length - 1)
            {
                _forward += _points[_index + i + 1] - _points[_index + i];
            }

            if (_index - i > 0)
            {
                _forward += _points[_index - i] - _points[_index - i - 1];
            }
        }
        _forward.Normalize();
        return _forward;
    }
    public static Mesh CreateRoadMesh(Vector2[] _points, float _roadWidth)
    {
        Vector3[] _vertices = new Vector3[_points.Length * 2];//vieruspistetiä 2(n-1)
        int[] _triangles = new int[2 * (_points.Length - 1) * 3]; //kolmioita 2n, kolmion kulmia 3*2n
        Vector2[] _uvs = new Vector2[_vertices.Length];

        int _vertsIndex = 0;
        int _triIndex = 0;

        for (int i = 0; i < _points.Length; i++)
        {
            Vector2 _forward = CalculateForwardVector(i, _points);

            Vector2 _left = new Vector2(_forward.y * -1, _forward.x); //90 asteen kulmassa oleva vektori

            //asetetaan aina kaksi pistettä, molemmille puolille
            _vertices[_vertsIndex] = _points[i] + _left * _roadWidth * 0.5f;
            _vertices[_vertsIndex + 1] = _points[i] - _left * _roadWidth * 0.5f; //oikea

            //meshin piirto laskenta
            float _completionPercent = i / (float)(_points.Length - 1);
            _uvs[_vertsIndex] = new Vector2(0, _completionPercent);
            _uvs[_vertsIndex + 1] = new Vector2(1, _completionPercent);

            if (i < _points.Length - 1)
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
