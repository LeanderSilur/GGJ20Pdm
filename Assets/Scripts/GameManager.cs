using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    private Camera activeCamera;

    public NPC[] NPCs;
    public NavMeshAgent avatar;
    private bool locked = false;
    public MeshCollider[] floors;

    public GameObject clickPointPrefab;
    private GameObject clickPoint;

    // Heros
    public Timemachine presentMachine;
    public Timemachine pastMachine;
    public Camera presentCam;
    public Camera pastCam;
    private Music music;

    bool dead_body = true;
    bool chair_broken = true;
    bool bed_broken = false;

    // Distance of Player to NPC that causes Action
    private const float DIST_TO_INTERACT = 2.0f;


    private void Awake()
    {

        //Can create musicobjects
        GameObject ob = (GameObject)Instantiate(Resources.Load("Music"));
        music = ob.GetComponent<Music>();

        //SwitchToPast(this, new EventArgs());
        SwitchToPresent(this, new EventArgs());
        presentMachine.Jump += new System.EventHandler(this.SwitchToPast);
        pastMachine.Jump += new System.EventHandler(this.SwitchToPresent);

    }
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
            Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            foreach (var floor in floors)
            {
                if (floor.Raycast(ray, out hit, 200.0f))
                {
                    Debug.Log("hit");

                    //GameObject newInstance = (GameObject)GameObject.Instantiate(clickPoint, hit.point, Quaternion.identity);
                    clickPoint.transform.position = hit.point;
                    clickPoint.SetActive(true);

                    avatar.SetDestination(hit.point);
                    avatar.isStopped = false;

                    //transform.position = ;
                    break;
                }
            }
        }

        // Wenn Clickpunkt erreicht, entferne Clickpunkt
        if ((avatar.transform.position - clickPoint.transform.position).magnitude < DIST_TO_INTERACT && clickPoint.activeSelf) {
            clickPoint.SetActive(false);

            float[] distances = new float[NPCs.Length];
            for (int i = 0; i < NPCs.Length; i++)
                distances[i] = (new Vector3(avatar.transform.position.x, 0.0f, avatar.transform.position.z)
                    - new Vector3(NPCs[i].transform.position.x, 0.0f, NPCs[i].transform.position.z)).magnitude;
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
                if(closestNpc.Name == "Time Machine")
                {
                    actionID = 3; // TM soll Zeitsprung durchführen
                }
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



    void SwitchToPast(object o, EventArgs e)
    {
        music.SwitchToPast();
        pastCam.enabled = true;
        presentCam.enabled = false;
        activeCamera = pastCam;
        PlaceCharacter(pastMachine.transform.position);

    }

    void SwitchToPresent(object o, EventArgs e)
    {
        music.SwitchToPresent();
        pastCam.enabled = false;
        presentCam.enabled = true;
        activeCamera = presentCam;
        PlaceCharacter(presentMachine.transform.position);
    }
    void PlaceCharacter(Vector3 pos)
    {
        // shift

        pos = new Vector3(pos.x, pos.y, pos.z - 2);
        avatar.ResetPath();
        avatar.enabled = false;
        avatar.gameObject.transform.position = pos;
        avatar.enabled = true;
    }
}
