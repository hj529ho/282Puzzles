using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ObjectPool : MonoBehaviour
{
   private Queue<GameObject> _pool = new Queue<GameObject>();

   public void Init(GameObject prefab,int size)
   {
      for (int i = 0; i < size; i++)
      {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        _pool.Enqueue(go);
      }
   }
   public GameObject Spawn()
   {
       if (_pool.Count != 0)
       {
           GameObject go =_pool.Dequeue();
           go.SetActive(true);
           return go;
       }
       else
       {
           return null;
       }
   }
   public void Despawn(GameObject go)
   {
       go.SetActive(false);
       _pool.Enqueue(go);
   }
}
