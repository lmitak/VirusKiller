using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension {

    /// <summary>
    /// Adds copy of a component to the GameObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go">This GameObject</param>
    /// <param name="toAdd">Component that needs to be added</param>
    /// <returns>GameObject with a new component</returns>
    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }

    public static GameObject CopyGameObjectComponents(this GameObject go, GameObject target)
    {
        Component[] components = target.GetComponents<Component>();

        foreach(Component cpt in components)
        {
            Type type = cpt.GetType();
            
            go.AddComponent(type);
            Component comp = go.GetComponent(type);
            var copy = comp.GetCopyOf(cpt);
            go.AddComponent(copy);
            Debug.Log(copy);
        }

        return null;
    }
}
