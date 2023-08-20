using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class CoreLink
{
    [SerializeField] CoreData core1;
    [SerializeField] CoreData core2;


    public CoreLink()
    {

    }
    public CoreLink(CoreData _core1, CoreData _core2)
    {
        core1 = _core1;
        core2 = _core2;
    }

    public bool Compare(CoreData _data)
    {
        if(core1 == _data || core2 == _data)
        {
            return true;
        }
        return false;
    }
}

