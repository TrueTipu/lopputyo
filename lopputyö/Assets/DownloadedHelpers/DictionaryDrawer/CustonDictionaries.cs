using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

///preset:

//[Serializable] public class CustomDictionary : SerializableDictionary<string, bool> { }

[Serializable] public class DirectionGameObjectDict : SerializableDictionary<Direction, GameObject> { }

//[Serializable] public class CoreLinkStreamPathDict : SerializableDictionary<CoreLink, List<VisitedRoom>> { }
//[Serializable] public class NodeLinkDict : SerializableDictionary<PathNodes.Node, PathNodes.Node> { }
///Laita alempi editor scriptiin

//[CustomPropertyDrawer(typeof(CustomDictionary))] // Name of your class (same as above)
//public class CustomDictionaryDrawer : DictionaryDrawer<string, bool> { } // chose same types as your dictionary


