using UnityEngine;

namespace PlayScene.Common.ObjectPool
{
   public class PoolObject : MonoBehaviour
   {
      private Transform _poolParent;

      public void SaveParentReference(Transform parent)
      {
         _poolParent = parent;
      }
   
      public void ReturnToPool()
      {
         transform.parent = _poolParent;
         gameObject.SetActive(false);
      }
   }
}
