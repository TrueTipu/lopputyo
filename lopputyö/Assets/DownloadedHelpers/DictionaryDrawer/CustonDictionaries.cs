using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

///preset:

//[Serializable] public class CustomDictionary : SerializableDictionary<string, bool> { }

[Serializable] public class RoomPositionDict : SerializableDictionary<Vector2Int, Room> { }

///Laita alempi editor scriptiin

//[CustomPropertyDrawer(typeof(CustomDictionary))] // Name of your class (same as above)
//public class CustomDictionaryDrawer : DictionaryDrawer<string, bool> { } // chose same types as your dictionary


