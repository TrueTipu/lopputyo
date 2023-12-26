using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public Line[] Lines => lines;
    [SerializeField] Line[] lines;

}
[System.Serializable]
public class Line
{
    public string Sentence => sentence;
    [TextArea(3, 10)]
    [SerializeField] string sentence;

    public Sprite Image => image;
    [SerializeField] Sprite image;

}


