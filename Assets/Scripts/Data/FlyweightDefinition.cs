using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefinitionType
{
    Victim,
    Hunter
    
}

public abstract class FlyweightDefinition : ScriptableObject
{
    [SerializeField] public DefinitionType DefinitionType;

    public abstract Flyweight Initialize();
}
