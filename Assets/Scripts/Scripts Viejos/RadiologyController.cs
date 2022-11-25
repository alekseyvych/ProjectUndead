﻿using System.Collections.Generic;
using UnityEngine;

public class RadiologyController : MonoBehaviour
{
    public static RadiologyController Instance { get; private set; } // static singleton

    public List<Transform> arrayForDoctors;
    public List<Transform> arrayForPatients;

    public Queue<GameObject> attendancePriorityRadiology;
    public Queue<GameObject> priorityRadiologyDoctor;

    public TaskManagement taskManagement;
    public TaskManagement.PatientGoTo task;

    WatingRoom waitingRoom;

    private float maxWaitingTime = 1f;
    private float waitingTime = 1f;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }
    void Start()
    {
        taskManagement = TaskManagement.Instance;
        waitingRoom = WatingRoom.Instance;

        attendancePriorityRadiology = new Queue<GameObject>(30);
        priorityRadiologyDoctor = new Queue<GameObject>(30);
    }

    // Update is called once per frame
    void Update()
    {

        if (attendancePriorityRadiology.Count != 0)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;

                GameObject patient = attendancePriorityRadiology.Peek();
                patientSearchIfIsAnEmptyRadiology(patient);
            }
        }

        if (priorityRadiologyDoctor.Count != 0)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;

                GameObject patient = priorityRadiologyDoctor.Peek();
                DoctorSearchIfIsAnEmptyRadiology(patient);
            }
        }

    }

    private void patientSearchIfIsAnEmptyRadiology(GameObject patient)
    {
        for (int i = 0; i < arrayForPatients.Count; i++)
        {
            if (arrayForPatients[i].childCount == 0 && arrayForDoctors[i].childCount != 0)
            {
                attendancePriorityRadiology.Dequeue();
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForPatients[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(arrayForPatients[i]);

                patient.gameObject.GetComponent<Patient>().state = Patient.State.GoingToRadiology;
                PatientInfo.DisplayState(patient.gameObject);
                DisplayStatistics.changeNumberOfPatientsWaitingRadiology(-1);
                DisplayStatistics.changeNumberOfPatientsWaiting(-1);
                break;
            }
        }
    }

    private void DoctorSearchIfIsAnEmptyRadiology(GameObject patient)
    {
        for (int i = 0; i < arrayForDoctors.Count; i++)
        {
            if (arrayForDoctors[i].childCount == 0)
            {
                priorityRadiologyDoctor.Dequeue();
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForDoctors[i].gameObject);
                patient.GetComponent<Radiologist>().goTo(task1.target.transform);
                patient.transform.SetParent(arrayForDoctors[i]);

                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().workers = "";
                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().updateText();
                patient.GetComponent<Worker>().currentstate = Worker.currentState.GoingToRadiology;
                break;
            }
        }
    }

    public void updateIndexOfPatients(int index)
    {
        arrayForPatients.RemoveAt(index);

        for (int i = index; i < arrayForPatients.Count; i++)
        {
            arrayForPatients[i].GetComponent<ObjectsOnRoom>().indexInList--;
        }
    }

    public void updateIndexOfDoctors(int index)
    {
        arrayForDoctors.RemoveAt(index);

        for (int i = index; i < arrayForDoctors.Count; i++)
        {
            arrayForDoctors[i].GetComponent<ObjectsOnRoom>().indexInList--;
        }
    }

    public int addDoctor(Transform position)
    {
        arrayForDoctors.Add(position);
        return arrayForDoctors.Count - 1;
    }

    public int addPatient(Transform position)
    {
        arrayForPatients.Add(position);
        return arrayForPatients.Count - 1;
    }

    public void searchRadiologyPatient(GameObject patient)
    {
        bool found = false;
        for (int i = 0; i < arrayForPatients.Count; i++)
        {
            if (arrayForPatients[i].childCount == 0 && arrayForDoctors[i].childCount != 0)
            {
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForPatients[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(arrayForPatients[i]);
                found = true;
                patient.gameObject.GetComponent<Patient>().state = Patient.State.GoingToRadiology;
                patient.gameObject.GetComponent<Patient>().waiting = false;
                PatientInfo.DisplayState(patient.gameObject);

                break;
            }
        }


        if (!found)
        {
            patient.gameObject.GetComponent<Patient>().state = Patient.State.WaitingForRadiology;
            PatientInfo.DisplayState(patient.gameObject);

            returnToWaitingRoom(patient);
        }
    }

    public void searchRadiologyDoctor(GameObject patient)
    {
        bool found = false;
        for (int i = 0; i < arrayForDoctors.Count; i++)
        {
            if (arrayForDoctors[i].childCount == 0)
            {
                patient.GetComponent<Radiologist>().goTo(arrayForDoctors[i]);
                patient.GetComponent<Radiologist>().indexOfWindow = i;
                patient.transform.SetParent(arrayForDoctors[i]);
                found = true;

                patient.GetComponent<Radiologist>().state = Radiologist.State.DoingTask;
                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().workers = "";
                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().updateText();
                patient.GetComponent<Worker>().currentstate = Worker.currentState.GoingToRadiology;
                break;
            }
        }

        if (!found)
        {
            patient.GetComponent<Radiologist>().state = Radiologist.State.DoingTask;
            goToRestRoom(patient);
        }

    }

    private void goToRestRoom(GameObject patient)
    {
        Debug.Log(priorityRadiologyDoctor.Count);
        patient.GetComponent<Radiologist>().state = Radiologist.State.DoingTask;
        priorityRadiologyDoctor.Enqueue(patient);
        Debug.Log(priorityRadiologyDoctor.Count);
        patient.GetComponent<Worker>().goToRestRoom(patient);
    }

    internal void searchPatient(GameObject patient)
    {
        if (attendancePriorityRadiology.Count == 0)
        {
            searchRadiologyPatient(patient);
        }
        else
        {
            returnToWaitingRoom(patient);
        }

    }

    public void searchDoctor(GameObject doctor)
    {
        if (priorityRadiologyDoctor.Count == 0)
        {
            searchRadiologyDoctor(doctor);
        }
        else
        {
            goToRestRoom(doctor);
        }

    }


    public Transform attend(int i)
    {
        return arrayForPatients[i];
    }


    private void returnToWaitingRoom(GameObject patient)
    {
        foreach (Transform seat in waitingRoom.seats)
        {
            if (seat.childCount == 0)
            {
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(seat.gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                attendancePriorityRadiology.Enqueue(patient);

                patient.gameObject.GetComponent<Patient>().state = Patient.State.WaitingForRadiology;
                PatientInfo.DisplayState(patient.gameObject);
                patient.gameObject.GetComponent<Patient>().waiting = true;
                DisplayStatistics.changeNumberOfPatientsWaitingRadiology(+1);
                DisplayStatistics.changeNumberOfPatientsWaiting(+1);
                break;
            }
        }
    }
}
