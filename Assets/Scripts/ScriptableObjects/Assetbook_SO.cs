using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assetbook_SO", menuName = "Scriptable Objects/Assetbook_SO")]
public class Assetbook_SO : ScriptableObject
{
    public List<ScriptableObject> assetList = new();
}
