using System;
using UnityEngine;

namespace Utils.Pool
{
    public interface IDeactivable
    {
        event Action<IDeactivable> ObjectDeactivation;
    }
}