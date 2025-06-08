using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class FactoryFlyweight : ManagerSingleton<FactoryFlyweight>
{
    private Dictionary<DefinitionType, List<Flyweight>> _pools = new Dictionary<DefinitionType, List<Flyweight>>();

    public Flyweight Spawn(FlyweightDefinition definition, Vector3 position, Quaternion rotation)
    {
        List<Flyweight> pool = GetPool(definition);
        
        Flyweight flyweight = definition.Initialize();
        flyweight.transform.position = position;
        flyweight.transform.rotation = rotation;

        pool.Add(flyweight);

        return flyweight;
    }
    
    private List<Flyweight> GetPool(FlyweightDefinition definition)
    {
        List<Flyweight> pool;
        if (_pools.TryGetValue(definition.DefinitionType, out pool))
        {
            return pool;
        }

        pool = new List<Flyweight>();
        _pools.Add(definition.DefinitionType, pool);

        return pool;
    
    }

}
