using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GenCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    }

    private static Dictionary<GameObject, MeshFilter> meshfilters = new Dictionary<GameObject, MeshFilter>();

    public static MeshFilter GenMeshFilter(GameObject gameObject)
    {
        if (!meshfilters.ContainsKey(gameObject))
        {
            meshfilters.Add(gameObject, gameObject.GetComponent<MeshFilter>());
        }

        return meshfilters[gameObject];
    }

    private static Dictionary<GameObject, MeshRenderer> meshRenderers = new Dictionary<GameObject, MeshRenderer>();

    public static MeshRenderer GenMeshRenderer(GameObject gameObject)
    {
        if (!meshRenderers.ContainsKey(gameObject))
        {
            meshRenderers.Add(gameObject, gameObject.GetComponent<MeshRenderer>());
        }

        return meshRenderers[gameObject];
    }

    private static Dictionary<GameUnit, Weapon> weapons = new Dictionary<GameUnit, Weapon>();   

    public static Weapon GenWeapon(GameUnit unit)
    {
        if (!weapons.ContainsKey(unit))
        {
            weapons.Add(unit, unit.GetComponent<Weapon>());
        }

        return weapons[unit];
    }

    //private static Dictionary<GameObject, Weapon> weapons = new Dictionary<GameObject, Weapon>();

    //public static Weapon GenWeapon(GameObject gameObject)
    //{
    //    if (!weapons.ContainsKey(gameObject))
    //    {
    //        weapons.Add(gameObject, gameObject.GetComponent<Weapon>());
    //    }

    //    return weapons[gameObject];
    //}
}
