using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface ITileNode
{
    void SetName(string name);

    Vector2Int TileCords { get; }
}
