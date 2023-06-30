using UnityEngine;
using System.Collections;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Path Path; //pakko olla public koska undo

    [field: SerializeField] public Color AnchorColor { get; private set; } = Color.red;
    [field: SerializeField] public Color ControlCol { get; private set; } = Color.magenta;
    [field: SerializeField] public Color SegmnetCol { get; private set; } = Color.green;
    [field: SerializeField] public Color SelectedSegmentCol { get; private set; } = Color.yellow;
    [field: SerializeField] public float AnchorDiameter { get; private set; } = 0.1f;
    [field: SerializeField] public float ComtrolDiameter { get; private set; } = 0.075f;
    [field: SerializeField] public bool DisplayControlPoints { get; private set; } = true;

    public void CreatePath()
    {
        Path = new Path(transform.position);
    }
    private void Reset() //automaattinen unity kutsu resetistä
    {
        CreatePath();
    }
}
