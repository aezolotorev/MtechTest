using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{  
   public UnityEngine.Object  Spawn(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parent = null)
   {
       return Instantiate(prefab, position, rotation, parent);
   }
}
