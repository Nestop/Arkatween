using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Pool
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour
    {
        public event Action<T> ObjectCreation;
        
        public readonly List<T> Objects;
        public readonly List<T> ActiveObjects;
        public readonly Stack<T> InactiveObjects;
        
        private readonly Transform _container;
        private readonly T _prefab;
        private readonly bool _expandable;

        public ObjectPool(Transform container, T prefab, int poolSize = 0, bool fillAtStart = false, bool expandable = true)
        {
            var poolContainer = new GameObject($"[Pool]{prefab.gameObject.name}").transform;
            poolContainer.parent = container;
            _container = poolContainer;
            _container.localPosition = Vector3.zero;
            
            _prefab = prefab;
            Objects = new List<T>();
            ActiveObjects = new List<T>();
            InactiveObjects = new Stack<T>();
            
            if (fillAtStart && poolSize > 0)
                for (var i = 0; i < poolSize; i++)
                    DeactivateObject(InitializeObject());

            _expandable = expandable;
        }

        public T GetObject()
        {
            if (InactiveObjects.Count == 0) 
                return _expandable ? InitializeObject() : GetActiveObjectStrategy();
            
            var obj = InactiveObjects.Pop();
            obj.gameObject.SetActive(true);
            ActiveObjects.Add(obj);
            return obj;
        }

        private T InitializeObject()
        {
            var obj = Object.Instantiate(_prefab, _container);

            if(obj is IDeactivable o) o.ObjectDeactivation += DeactivateObject;
            
            Objects.Add(obj);
            ActiveObjects.Add(obj);
            
            ObjectCreation?.Invoke(obj);
            return obj;
        }

        protected virtual T GetActiveObjectStrategy()
        {
            return null;
        }

        public void DeactivateAllObjects()
        {
            var objs = ActiveObjects.ToArray();
            foreach (var obj in objs)
                DeactivateObject(obj);
        }
        
        private void DeactivateObject(IDeactivable obj)
        {
            DeactivateObject(obj as T);
        }
        
        private void DeactivateObject(T obj)
        {
            if (InactiveObjects.Contains(obj)) return;
            
            obj.gameObject.SetActive(false);
            InactiveObjects.Push(obj);
            ActiveObjects.Remove(obj);
        }
    }
}