using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LevelSystem 
{   
    public int level=0;
   
    public void FinishLevel() { level++; }   

    public int GetLevel() { return level; }

    public void Reset() {
        level=0;
    }

}
