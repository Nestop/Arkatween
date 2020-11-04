﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Pool
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly List<T> _objects;
        private readonly Stack<T> _inactiveObjects;
        private readonly Transform _container;
        private readonly T _prefab;
        private readonly bool _expandable;

        public List<T> Objects => _objects;

        public ObjectPool(Transform container, T prefab, int poolSize = 0, bool fillAtStart = false, bool expandable = false)
        {
            var poolContainer = new GameObject($"[Pool]{prefab.gameObject.name}").transform;
            poolContainer.parent = container;
            _container = poolContainer;
            _container.localPosition = Vector3.zero;
            
            _prefab = prefab;
            _objects = new List<T>(poolSize);
            _inactiveObjects = new Stack<T>(poolSize);
            
            if (fillAtStart && poolSize > 0)
                for (var i = 0; i < poolSize; i++)
                    DeactivateObject(InitializeObject());

            _expandable = expandable;
        }

        public T GetObject()
        {
            if (_inactiveObjects.Count == 0) 
                return _expandable ? InitializeObject() : GetActiveObjectStrategy();
            
            var obj = _inactiveObjects.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }

        private T InitializeObject()
        {
            var obj = Object.Instantiate(_prefab, _container);
            
            if(obj is IDeactivable o) o.ObjectDeactivation += DeactivateObject;
            
            _objects.Add(obj);
            
            return obj;
        }

        protected virtual T GetActiveObjectStrategy()
        {
            return null;
        }

        public void DeactivateAllObjects()
        {
            _inactiveObjects.Clear();
            
            foreach (var obj in _objects)
                DeactivateObject(obj);
        }
        
        private void DeactivateObject(IDeactivable obj)
        {
            DeactivateObject(obj as T);
        }
        
        private void DeactivateObject(T obj)
        {
            if (_inactiveObjects.Contains(obj)) return;
            
            obj.gameObject.SetActive(false);
            _inactiveObjects.Push(obj);
        }
    }
}