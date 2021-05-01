using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
     //scene UI menu
    public int ID{get;set;}
    public string LevelName{get;set;}
    public bool Completed{get;set;}
    public int Times{get;set;}
    public bool Locked{get;set;}
    public Level(int id, string levelName, bool completed, int times, bool locked ){
        this.ID = id;
        this.LevelName = levelName;
        this.Completed = completed;
        this.Times = times;
        this.Locked = locked;
    }
    public void Complete(int times){
        this.Completed = true;
        this.Times = times;
    }
    public void Lock(){
        this.Locked = true;
    }
    public void Unlock(){
        this.Locked = false;
    }
}
