using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "CoreDataManager", menuName = "ScriptableObjects/CoreDataManager")]
[System.Serializable]
public class CoreDataManager : PlaytimeObject
{
    [SerializeField] List<CoreDataObject> coreStorage = new List<CoreDataObject>();
    [SerializeField, HideInInspector] CoreData[] coreDataStorage = new CoreData[100];
    public List<CoreDataObject> CoreStorage { get; private set; }




    public bool FindSavedCore(CoreData _core, out CoreDataObject _savedCore)
    {
        bool _val = FindSavedCore(_core.name, out int _i);
        if (_val)
        {
            _savedCore = CoreStorage[_i];
            coreDataStorage[_i] = _core;
            return true;
        }
        else
        {
            _savedCore = null;
            return false;
        }

    }
    public bool FindSavedCoreData(string _coreName, out CoreData _savedCoreData)
    {
        bool _val = FindSavedCore(_coreName, out int _i);
        if (_val)
        {
            _savedCoreData = coreDataStorage[_i];
            return true;
        }
        else
        {
            _savedCoreData = null;
            return false;
        }

    }

    public bool FindSavedCore(string _coreName, out int _savedCoreIndex)
    {
        if (CoreStorage == null) LoadInspectorData();


        _savedCoreIndex = CoreStorage.FindIndex((x) => x.Name.Equals(_coreName));
        return (_savedCoreIndex != -1);
    }

    protected override void LoadInspectorData()
    {
        CoreStorage = coreStorage.ConvertAll((x) => new CoreDataObject(x));
    }

    protected override void InitSO(ScriptableObject _obj)
    {
        CoreDataManager _oldData = _obj as CoreDataManager;


        coreStorage = _oldData.CoreStorage.ConvertAll((x) => new CoreDataObject(x));
    }

    [System.Serializable]
    public class CoreDataObject
    {
        [SerializeField] bool main;
        [SerializeField] string name;
        [SerializeField] PlayerAbility currentAbility = PlayerAbility.None;

        public bool IsMain => main;
        public string Name => name;
        public PlayerAbility CurrentAbility => currentAbility;

        public void SetCurrentAbility(PlayerAbility _currentAbility)
        {
            currentAbility = _currentAbility;
        }
        public void SetName(string _name)
        {
            name = _name;
        }
        public void SetMain(bool _value)
        {
            main = _value;
        }

        public CoreDataObject(bool _main, string _name, PlayerAbility _currentAbility)
        {
            main = _main;
            name = _name;
            currentAbility = _currentAbility;
        }

        public CoreDataObject()
        {
            main = false;
            name = "";
            currentAbility = PlayerAbility.None;
        }

        public CoreDataObject(CoreData _core)
        {
            main = _core.IsMainCore;
            name = _core.name;
            currentAbility = _core.CurrentAbility;
        }
        public CoreDataObject(CoreDataObject _core)
        {
            main = _core.IsMain;
            name = _core.Name;
            currentAbility = _core.CurrentAbility;
        }
    }
}

