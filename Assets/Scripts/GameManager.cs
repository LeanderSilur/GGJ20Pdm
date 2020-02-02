using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    public NPC[] NPCs;
    public NavMeshAgent avatar;
    private bool locked;
    public MeshCollider floor;

    public GameObject clickPointPrefab;
    private GameObject clickPoint;

    bool dead_body = true;
    bool chair_broken = true;
    bool bed_broken = false;

    // Distance of Player to NPC that causes Action
    private const float DIST_TO_INTERACT = 2.0f; 

    // Start is called before the first frame update
    void Start()
    {
        clickPoint = GameObject.Instantiate(clickPointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        clickPoint.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Close game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (locked)
            return;

        // Move this object to the position clicked by the mouse.
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (floor.Raycast(ray, out hit, 100.0f))
            {
                //if (clickPoint != null)
                //Destroy(instancedChair);

                //GameObject newInstance = (GameObject)GameObject.Instantiate(clickPoint, hit.point, Quaternion.identity);
                clickPoint.transform.position = hit.point;
                clickPoint.SetActive(true);

                avatar.SetDestination(hit.point);
                //transform.position = ;
            }
            //Debug.Log("Click-Pos: " + hit.point);
            //Debug.Log("Play-Pos: " + avatar.transform.position);
        }

        // Wenn Clickpunkt erreicht, entferne Clickpunkt
        if ((avatar.transform.position - clickPoint.transform.position).magnitude < 0.1 && clickPoint.activeSelf) {
            clickPoint.SetActive(false);

            float[] distances = new float[NPCs.Length];
            for (int i = 0; i < NPCs.Length; i++)
                distances[i] = (avatar.transform.position - NPCs[i].transform.position).magnitude;
            float minDist = distances.Min();
            if (minDist < DIST_TO_INTERACT)
            {
                int minIndex = Array.IndexOf(distances, minDist);

                NPC closestNpc = NPCs[minIndex];
                //Debug.Log(minIndex);

                //if (closestNpc is Tenta)
                //{
                //Debug.Log("closest" + closestNpc);

                //StartCoroutine(Story(closestNpc as Tenta));
                int actionID = 0;
                    StartCoroutine(Story(closestNpc, actionID));
                //}
            }
        }
       
        /*
        // Wenn Character Tentacle erreicht, entferne GelatinousCube
        if ((Mathf.Abs(avatar.transform.position.x - clickPoint.transform.position.x) < 0.01)
            && (Mathf.Abs(avatar.transform.position.z - clickPoint.transform.position.z) < 0.01)
            && clickPoint.activeSelf)
        {
            clickPoint.SetActive(false);
        }
        */
    }

    //private IEnumerator Story(Tenta tenta)
    private IEnumerator Story(NPC npc, int actionID)
    {
        locked = true;
        
        yield return npc.Interact(actionID);
        

        /*for (int i = 0; i < 120; i++)
        {
            if (i % 20 == 0)
                Debug.Log(i);
            yield return 0;
        }*/

        //yield return new WaitForSeconds(2.0f);



        locked = false;
    }


}
