﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///preset:

//[Serializable] public class CustomDictionary : SerializableDictionary<string, bool> { }

///Laita alempi editor scriptiin

//[CustomPropertyDrawer(typeof(CustomDictionary))] // Name of your class (same as above)
//public class CustomDictionaryDrawer : DictionaryDrawer<string, bool> { } // chose same types as your dictionary

[Serializable] public class DirectionDictionary : SerializableDictionary<Direction, bool> { }
