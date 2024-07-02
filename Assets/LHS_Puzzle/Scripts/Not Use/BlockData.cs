using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "Scriptable Object/Block Data", order = int.MaxValue)]
public class BlockData : ScriptableObject
{
    [SerializeField]
    private string blockName;
    public string BlockName { get { return blockName; } }

    
}
