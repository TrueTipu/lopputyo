using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//https://www.youtube.com/watch?v=d9k97JemYbM&t=3s&ab_channel=SebastianLague
[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector2> points;

    public Path(Vector2 _centre)
    {
        points = new List<Vector2>
        {
            _centre+Vector2.left,
            _centre+(Vector2.left+Vector2.up) * 0.5f,
            _centre+(Vector2.right+Vector2.down) * 0.5f,
            _centre+Vector2.right
        };
    }

    public Vector2 this[int i] //indexer
    {
        get
        {
            return points[i];
        }
        set
        {
            MovePoint(i, value);
        }
    }

    public bool AutoSetControlPoints { get; private set; } = true;

    public void SetAutoSetMode(bool _value)
    {
        if (AutoSetControlPoints != _value)
        {
            AutoSetControlPoints = _value;
            if (AutoSetControlPoints)
            {
                AutoSetAllControlPoints();
            }
        }
    }

    public int NumPoints
    {
        get
        {
            return points.Count;
        }
    }

    public int NumSegments
    {
        get
        {
            return (points.Count - 4) / 3 + 1; //lis‰t‰‰n default 4 yskitt‰isen‰
        }
    }

    public void AddSegment(Vector2 _anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + _anchorPos)* 0.5f);
        points.Add(_anchorPos);

        if (AutoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(points.Count - 1);
        }
    }

    public void SplitSegment(Vector2 _anchorPos, int _segmentIndex) //inserttausta varten
    {
        points.InsertRange(_segmentIndex * 3 + 2, new Vector2[] { Vector2.zero, _anchorPos, Vector2.zero });
        if (AutoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(_segmentIndex * 3 + 3);
        }
        else
        {
            AutoSetAnchorControlPoints(_segmentIndex * 3 + 3); //koska control nodet pit‰‰ asettaa jokatapauksessa johonkin
        }
    }

    public void DeleteSegment(int _anchorIndex)
    {
        if (NumSegments <= 1) return;

        if(_anchorIndex == 0)
        {
            points.RemoveRange(0, 3);
            if (AutoSetControlPoints) AutoSetStartAndEndControls();
        }
        else if (_anchorIndex == points.Count - 1)
        {
            points.RemoveRange(_anchorIndex - 2, 3);
            if (AutoSetControlPoints) AutoSetStartAndEndControls();
        }
        else
        {
            points.RemoveRange(_anchorIndex - 1, 3);

            if (AutoSetControlPoints)
            {
                if(_anchorIndex < points.Count)
                    AutoSetAllAffectedControlPoints(_anchorIndex);
                else
                    AutoSetAllAffectedControlPoints(_anchorIndex - 3);
            }
        }


    }

    public Vector2[] GetPointsInSegment(int _index)
    {
        return points.GetRange(_index*3, 4).ToArray();
    }

    float GetSegmentEstimateLength(int _segmentIndex) //oma funktio, evenly spaced points varten, selketty‰‰ koodia
    {
        Vector2[] _sPoints = GetPointsInSegment(_segmentIndex);
        float _controlNetLength = Vector2.Distance(_sPoints[0], _sPoints[1]) +
            Vector2.Distance(_sPoints[1], _sPoints[2]) + Vector2.Distance(_sPoints[2], _sPoints[3]);
        float _estimatedCurveLength = Vector2.Distance(_sPoints[0], _sPoints[3]) + _controlNetLength / 2f;
        return _estimatedCurveLength;
    }

    //public indexer setter kutsuu t‰t‰ metodia
    void MovePoint(int _index, Vector2 _pos)
    {
        Vector2 _deltaMove = _pos - points[_index];


        if (_index % 3 == 0 || !AutoSetControlPoints)
        {
            points[_index] = _pos;
        }
        else return;
        //else: toteutettu returnilla
        if (AutoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(_index);
            return;
        }

        //ankkurit pyˆrim‰‰n mukana
        if (_index % 3 == 0)
        {
            if (_index + 1 < points.Count)
                points[_index + 1] += _deltaMove;
            if (_index - 1 >= 0)
                points[_index - 1] += _deltaMove;
        }
        else
        {
            //cool maths jota en jaksa ymm‰rt‰‰, E2, pyˆritt‰‰ ja liikuttaa pisteit‰ toisten mukana
            bool _nextIsAnchor = (_index + 1) % 3 == 0;
            int _correspondingControlIndex = (_nextIsAnchor) ? _index + 2 : _index - 2;
            int _anchorIndex = (_nextIsAnchor) ? _index + 1 : _index - 1;

            if (_correspondingControlIndex >= 0 && _correspondingControlIndex < points.Count)
            {
                float _dst = (points[_anchorIndex] - points[_correspondingControlIndex]).magnitude;
                Vector2 _dir = (points[_anchorIndex] - _pos).normalized;
                points[_correspondingControlIndex] = points[_anchorIndex] + _dir * _dst;
            }
        }
              
    }

    public Vector2[] CalculateEvenlySpacedPoints(float _spacing, float _res = 1) //pisteet renderˆinti‰ ja partikkeleita varten
    {
        List<Vector2> _evenPoints = new List<Vector2>();
        _evenPoints.Add(points[0]);
        Vector2 _previousPoint = points[0];
        float _dstSinceLastEventPoint = 0;


        for (int _segmentIndex = 0; _segmentIndex < NumSegments; _segmentIndex++)    //jokainen pistev‰li l‰pi
        {
            Vector2[] _currentPoints = GetPointsInSegment(_segmentIndex);

            int _divisions = Mathf.CeilToInt(GetSegmentEstimateLength(_segmentIndex) * _res * 10); //lasketaan t m‰‰r‰ resoluution, pituuden, ja vakion avulla

            float _controlNetLength = Vector2.Distance(_currentPoints[0], _currentPoints[1]);
            float t = 0;

            while (t <= 1) //iteroidaan t:n avulla pisteit‰ beizerilt‰
            {
                t += 1f/_divisions;
                Vector2 _pointOnCurve = Bezier.EvaluateCubic(_currentPoints[0], _currentPoints[1], _currentPoints[2], _currentPoints[3], t); //oteteaan beizer piste
                _dstSinceLastEventPoint += Vector2.Distance(_previousPoint, _pointOnCurve);

                while(_dstSinceLastEventPoint >= _spacing)//overshoot kaikki l‰pi
                {
                    float _overshootDst = _dstSinceLastEventPoint - _spacing; //lasketaan jos menee yli
                    Vector2 _newEvenPoint = _pointOnCurve + (_previousPoint - _pointOnCurve).normalized * _overshootDst; //perus vektorimatikkaa, lis‰t‰‰n point on curven suuntaan overshootin verran

                    _evenPoints.Add(_newEvenPoint); //Huom, lis‰ykset tehd‰‰n vain ylimenneill‰, koska aina menee eventually yli
                    _dstSinceLastEventPoint = _overshootDst;

                    _previousPoint = _newEvenPoint;
                }
                _previousPoint = _pointOnCurve;
            }
        }

        return _evenPoints.ToArray();
    }

    void AutoSetAllAffectedControlPoints(int _updatedIndex)//vain vaikutetut
    {
        for (int i = _updatedIndex - 3; i <= _updatedIndex + 3; i+=3)
        {
            if(i >= 0 && i < points.Count)
                AutoSetAnchorControlPoints(i);
        }
        AutoSetStartAndEndControls();
    }

    void AutoSetAllControlPoints()//kaikki
    {
        for (int i = 0; i < points.Count; i+=3)
        {
            AutoSetAnchorControlPoints(i);
        }
        AutoSetStartAndEndControls();
    }

    void AutoSetAnchorControlPoints(int _anchorIndex)//kaikki keskell‰
    {
        //lis‰‰ matikkaa E3, josta en kinda ymm‰rr‰, mut siis vaan yksinkertaisesti tekee siit hienon automaattisesti
        Vector2 _anchorPos = points[_anchorIndex];
        Vector2 _dir = Vector2.zero;
        float[] _neighbourDistances = new float[2];

        if(_anchorIndex - 3 >= 0)
        {
            Vector2 _offset = points[_anchorIndex - 3] - _anchorPos;
            _dir += _offset.normalized;
            _neighbourDistances[0] = _offset.magnitude;
        }
        if(_anchorIndex + 3 >= 0)
        {
            Vector2 _offset = points[(_anchorIndex + 3) % points.Count] - _anchorPos;
            _dir -= _offset.normalized;
            _neighbourDistances[1] = -_offset.magnitude;
        }

        _dir.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int _controlIndex = _anchorIndex + i * 2 - 1;
            if(_controlIndex >= 0 && _controlIndex < points.Count)
            {
                points[_controlIndex] = _anchorPos + _dir * _neighbourDistances[i] * 0.5f;
            }
        }
    }

    void AutoSetStartAndEndControls()//eka ja vika
    {
        points[1] = (points[0] + points[2]) * 0.5f;
        points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * 0.5f;
    }
}
