using UnityEngine;

namespace Utils.Pool
{
    public interface IPool<out T> where T : MonoBehaviour
    {
        T GetObject();
        void DeactivateAllObjects();
    }
}