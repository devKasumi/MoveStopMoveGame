using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private static Dictionary<GameObject, Transform> transforms = new Dictionary<GameObject, Transform>();  

    public static Transform GenTransform(GameObject gameObject)
    {
        if (!transforms.ContainsKey(gameObject))
        {
            transforms.Add(gameObject, gameObject.GetComponent<Transform>());
        }

        return transforms[gameObject];
    }

    private static Dictionary<GameObject, Image> images = new Dictionary<GameObject, Image>();

    public static Image GenImage(GameObject gameObject)
    {
        if (!images.ContainsKey(gameObject))
        {
            images.Add(gameObject, gameObject.GetComponent<Image>());
        }

        return images[gameObject];
    }

    private static Dictionary<GameObject, RectTransform> rectTransforms = new Dictionary<GameObject, RectTransform>();

    public static RectTransform GenRectTransform(GameObject gameObject)
    {
        if (!rectTransforms.ContainsKey(gameObject))
        {
            rectTransforms.Add(gameObject, gameObject.GetComponent<RectTransform>());
        }

        return rectTransforms[gameObject];
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
