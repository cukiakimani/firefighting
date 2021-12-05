using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SweetLibs
{
    public static class Extensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static void DestroyChildrenImmediate(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public static T GetComponentInAncestors<T>(this Component component)
        {
            var componentToFind = component.transform.GetComponent<T>();

            if (componentToFind == null && component.transform.parent != null)
                return component.transform.parent.GetComponentInAncestors<T>();

            return componentToFind;
        }

        public static T GetComponentInHierarchy<T>(this Component component)
        {
            var componentToFind = component.GetComponentInChildren<T>();

            if (componentToFind == null)
                return component.GetComponentInAncestors<T>();

            return componentToFind;
        }

        public static T GetComponentInAncestors<T>(this GameObject gameObject)
        {
            var componentToFind = gameObject.transform.GetComponent<T>();

            if (componentToFind == null && gameObject.transform.parent != null)
                return gameObject.transform.parent.GetComponentInAncestors<T>();

            return componentToFind;
        }

        public static T GetComponentInHierarchy<T>(this GameObject gameObject)
        {
            var componentToFind = gameObject.GetComponentInChildren<T>();

            if (componentToFind == null)
                return gameObject.GetComponentInAncestors<T>();

            return componentToFind;
        }
    }
}