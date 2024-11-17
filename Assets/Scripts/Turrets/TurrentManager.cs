using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentManager : MonoBehaviour
{
    [SerializeField] private Turret turretsBlue;
    [SerializeField] private Turret turretsYellow;
    [SerializeField] private Turret turretsRed;

    //Primary
    public readonly Color BlueColor = new Color32(0, 0, 255, 255);
    public readonly Color RedColor = new Color32(255, 0, 0, 255);
    public readonly Color YellowColor = new Color32(255, 255, 0, 255);

    //Secondary
    public readonly Color GreenColor = new Color32(0, 128, 0, 255);
    public readonly Color OrangeColor = new Color32(255, 165, 0, 255);
    public readonly Color VioletColor = new Color32(127, 0, 255, 255);

    [SerializeField] private float minZ, maxZ;

    [SerializeField] private List<GameObject> selectedTurret = new List<GameObject>();
    [SerializeField] private float turretSpeed;
    private int numberTurret = 0;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootBlueTurrets();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShootYellowTurrets();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootRedTurrets();
        }

        #region InputsTurrets

        if (Input.GetKey(KeyCode.A))
        {
            if (selectedTurret[numberTurret].transform.position.z < maxZ)
            {
                selectedTurret[numberTurret].transform.position += new Vector3(0, 0, turretSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (selectedTurret[numberTurret].transform.position.z > minZ)
            {
                selectedTurret[numberTurret].transform.position -= new Vector3(0, 0, turretSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            numberTurret++;
            if (numberTurret >= selectedTurret.Count)
                numberTurret = 0;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            numberTurret--;
            if (numberTurret < 0)
                numberTurret = (selectedTurret.Count - 1);
        }

        #endregion
    }

    #region Turrets
    public void ShootBlueTurrets()
    {
        turretsBlue.Shoot();
    }

    public void ShootYellowTurrets()
    {
        turretsYellow.Shoot();

    }

    public void ShootRedTurrets()
    {
        turretsRed.Shoot();
    }

    #endregion

}
