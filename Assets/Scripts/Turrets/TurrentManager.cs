using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentManager : MonoBehaviour
{
    [SerializeField] private List<Turret> turretsBlue = new List<Turret>();
    [SerializeField] private List<Turret> turretsYellow = new List<Turret>();
    [SerializeField] private List<Turret> turretsRed = new List<Turret>();

    //Primary
    public readonly Color BlueColor = new Color32(0, 0, 255, 255);
    public readonly Color RedColor = new Color32(255, 0, 0, 255);
    public readonly Color YellowColor = new Color32(255, 255, 0, 255);  
    
    //Secondary
    public readonly Color GreenColor = new Color32(0, 128, 0, 255);
    public readonly Color OrangeColor = new Color32(255, 165, 0, 255);
    public readonly Color VioletColor = new Color32(127, 0, 255, 255);
    

    [ContextMenu("bla")]
    public void TestFunc()
    {

    }

    #region Turrets

    public void AddBlueTurret(Turret newTurret)
    {
        turretsBlue.Add(newTurret);
    }

    public void RemoveBlueTurret(Turret newTurret)
    {
        turretsBlue.Remove(newTurret);
    }
    public void ShootBlueTurrets()
    {
        for (int i = 0; i < turretsBlue.Count; i++)
        {
            turretsBlue[i].Shoot();          
        }
    }


    public void AddYellowTurret(Turret newTurret)
    {
        turretsYellow.Add(newTurret);
    }

    public void RemoveYellowTurret(Turret newTurret)
    {
        turretsYellow.Remove(newTurret);
    }
    public void ShootYellowTurrets()
    {
        for (int i = 0; i < turretsYellow.Count; i++)
        {
            turretsYellow[i].Shoot();
        }
    }


    public void AddRedTurret(Turret newTurret)
    {
        turretsRed.Add(newTurret);
    }

    public void RemoveRedTurret(Turret newTurret)
    {
        turretsRed.Remove(newTurret);
    }
    public void ShootRedTurrets()
    {
        for (int i = 0; i < turretsRed.Count; i++)
        {
            turretsRed[i].Shoot();
        }
    }

    #endregion

}
