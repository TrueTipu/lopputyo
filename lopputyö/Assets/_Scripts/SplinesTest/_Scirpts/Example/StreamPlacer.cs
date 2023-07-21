using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

///Editor scriptin omistava testailujuttu
public class StreamPlacer : StreamCreator
{


    [field: SerializeField] public bool AutoUpdate { get; private set; } = true;

    public  void UpdateStream()
    {
        Path _path = GetComponent<PathCreator>().Path;
        base.UpdateStream(_path);
    }
    public void UpdateParticles()
    {
        Path _path = GetComponent<PathCreator>().Path;
        base.UpdateParticles(_path);
    }
   
}
