﻿using UnityEngine;

public class ObjectsOnRoom : MonoBehaviour
{
    public Material[] materiales;

    public type objectType = type.None;

    public bool isSelected;

    public int indexInList = -1;

    public Material materialBase;
    public enum type
    {
        ConsultDoctor,
        ConsultPatient,
        None,
        RadiologyDoctor,
        RadiologyPatient,
        AnalysisDoctor,
        AnalysisPatient
    }
    void Start()
    {
        materiales[0] = this.gameObject.GetComponent<MeshRenderer>().material;
        //materialBase = this.gameObject.GetComponent<MeshRenderer>().material;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeMaterial(int i)
    {
        this.transform.GetComponent<MeshRenderer>().material = materiales[i];
    }

    public void setNewPosition()
    {

    }
}
