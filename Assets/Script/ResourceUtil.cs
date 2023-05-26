using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceUtil
{
    public static T Load<T>(string path) where T :Object
    {
        var resource = Resources.Load<T>(path);
        if (resource == null)
        {
            Debug.Log("Resource not found: " + path);
        }
        return Resources.Load<T>(path);
    }
    public static T[] LoadAll<T>(string path) where T :Object
    {
        return Resources.LoadAll<T>(path);
    }

    public static GameObject Instantiate(string path,Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if(prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        return Object.Instantiate(prefab,parent);
    }

    public static void Destroy(GameObject go)
    {
        if(go ==null)
            return;
        Object.Destroy(go);
    }
}