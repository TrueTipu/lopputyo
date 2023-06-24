using UnityEngine;
using System.Collections;

//yksinkertainen koodi prefabille ja sille spesifeille toiminnoille, ks Room.cs
public class RoomObject : MonoBehaviour
{
    [SerializeField] GameObject leftBlock;
    [SerializeField] GameObject rightBlock;
    [SerializeField] GameObject downBlock;
    [SerializeField] GameObject upBlock;

    public void Clear()
    {
        if (leftBlock != null)
            leftBlock?.SetActive(false);
        if (rightBlock != null)
            rightBlock?.SetActive(false);
        if (upBlock != null)
            upBlock?.SetActive(false);
        if(downBlock != null)
            downBlock?.SetActive(false);
    }

    public void BlockPath(Direction _dir)
    {
        switch (_dir)
        {
            case Direction.Left:
                if (leftBlock != null)
                    leftBlock.SetActive(true);
                break;
            case Direction.Right:
                if (rightBlock != null)
                    rightBlock.SetActive(true);
                break;
            case Direction.Up:
                if (upBlock != null)
                    upBlock.SetActive(true);
                break;
            case Direction.Down:
                if(downBlock != null)
                    downBlock.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetActive(bool _active)
    {
        if (_active)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
