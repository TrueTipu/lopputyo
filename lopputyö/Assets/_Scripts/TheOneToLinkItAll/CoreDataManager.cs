using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "CoreDataManager", menuName = "ScriptableObjects/CoreDataManager")]
[System.Serializable]
public class CoreDataManager : PlaytimeObject
{
    [SerializeField] List<CoreDataObject> coreStorage = new List<CoreDataObject>();
    public List<CoreDataObject> CoreStorage { get; private set; }



    public bool FindSavedCore(CoreData _core, out CoreDataObject _savedCore)
    {
        if (CoreStorage == null) LoadInspectorData();

        _savedCore = null;
        int _i = CoreStorage.FindIndex((x) => x.Name.Equals(_core.name));
        if(_i != -1)
        {
            _savedCore = CoreStorage[_i];
            return true;
        }
        else
        {
            return false;
        }
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

