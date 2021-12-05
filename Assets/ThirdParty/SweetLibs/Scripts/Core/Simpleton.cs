using UnityEngine;

namespace SweetLibs
{ 
    public class Simpleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get { return instance; }
        }

        protected virtual void Awake()
        {
            instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}