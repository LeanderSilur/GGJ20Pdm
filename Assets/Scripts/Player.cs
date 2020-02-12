using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public NavMeshAgent avatar;
    public GameManager gm;
    public GameData gd;

    public GameObject clickPointPrefab;
    private GameObject clickPoint;

    float? dist_to_target;
    float? time_last_dist;

    public bool locked = false;

    // Distance of Player from Target-Marker, before it disappears
    private const float DIST_TO_SHOW_TARGET = 1.0f;
    // Distance of Player to NPC that causes Action
    private const float DIST_TO_INTERACT = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        clickPoint = GameObject.Instantiate(clickPointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        clickPoint.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
            return;

        // Wenn Clickpunkt erreicht, entferne Clickpunkt
        if ((avatar.transform.position - clickPoint.transform.position).magnitude < DIST_TO_SHOW_TARGET && clickPoint.activeSelf)
        {
            TargetReached();
        }

        // TODO: Target als Eigenschaft des Players festlegen
        //Vector3 target = 

        if (!avatar.isStopped && clickPoint.activeSelf)
        {
            float dist = (avatar.transform.position - clickPoint.transform.position).magnitude;
            float dist_time = Time.fixedTime;

            // wenn noch gar kein vorheriger Abstand vorliegt, oder sich der Abstand seit dem letzten Mal um mehr als 0.1 verkleinert hat,
            // neu als bisher größte Annäherung speichern
            if (this.dist_to_target == null || this.dist_to_target - dist > 0.08)
            {
                this.dist_to_target = dist;
                this.time_last_dist = dist_time;
            }
            else
            {
                // wenn der Abstand nahezu Null beträgt, oder sogar größer wurde, noch 0.4s warten, dann stoppen.
                if ((dist_time - this.time_last_dist) > 0.8)
                {
                    this.time_last_dist = null;
                    this.dist_to_target = null;
                    StopMoving();
                    TargetReached();
                }
            }

        }
    }

    private void TargetReached()
    {
        clickPoint.SetActive(false);


        // Prüfe Nähe zu Interaktionspartnern
        NPC[] NPCs = gm.GetSceneNPCs();

        float[] distances = new float[NPCs.Length];
        for (int i = 0; i < NPCs.Length; i++)
            distances[i] = (new Vector3(avatar.transform.position.x, 0.0f, avatar.transform.position.z)
                - new Vector3(NPCs[i].transform.position.x, 0.0f, NPCs[i].transform.position.z)).magnitude;
        float minDist = distances.Min();
        if (minDist < DIST_TO_INTERACT)
        {
            int minIndex = Array.IndexOf(distances, minDist);

            NPC closestNpc = NPCs[minIndex];

            //StartCoroutine(Story(closestNpc as Tenta));
            int actionID = 0;
            if (closestNpc.Name == "Time Machine")
            {
                actionID = 3; // TM soll Zeitsprung durchführen
            }
            gm.StartStory(closestNpc, actionID);
            //}
        }
    }

    public void StartMovingTo(Vector3 moveTo)
    {
        this.time_last_dist = null;
        this.dist_to_target = null;
        avatar.SetDestination(moveTo);
        avatar.isStopped = false;
    }

    public void StopMoving(bool deleteMarker = false)
    {
        avatar.isStopped = true;
        if (deleteMarker)
        {
            clickPoint.SetActive(false);
        }
    }

    public void SetTargetMarker(Vector3 markerPos)
    {
        //StopMoving(true);
        clickPoint.transform.position = markerPos;
        clickPoint.SetActive(true);
        StartMovingTo(markerPos);
    }
}
