using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Camera activeCamera { get; private set; }
    public GameData gameData;

    public NPC[] NPCs;
    //public NavMeshAgent avatar;
    public Player avatar;
    //private bool locked = false;
    public MeshCollider floor;

    //public GameObject clickPointPrefab;
    //private GameObject clickPoint;

    // Heros
    public Timemachine tm;
    //public Timemachine pastMachine;
    //public Camera presentCam;
    //public Camera pastCam;
    public Camera cam;
    public Music music { get; private set; }

    private bool activeInteraction = false;

    /*
    bool dead_body = true;
    bool chair_broken = true;
    bool bed_broken = false;
    */

    //float? dist_to_target;
    //float? time_last_dist;

    //// Distance of Player from Target-Marker, before it disappears
    //private const float DIST_TO_SHOW_TARGET = 1.0f;
    //// Distance of Player to NPC that causes Action
    //private const float DIST_TO_INTERACT = 2.5f;


    private void Awake()
    {
        //Can create musicobjects
        GameObject ob = (GameObject)Instantiate(Resources.Load("Music"));
        music = ob.GetComponent<Music>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //clickPoint = GameObject.Instantiate(clickPointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //clickPoint.SetActive(false);

        if (GameData.IsPast()){
            music.Play(music.theParty);
        }
        else
        {
            if(GameData.lastPlayedTrack == music.theParty)
            {
                music.Play(music.theAftermath);
            }
            else
            {
                music.Play(music.theIntro);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Close game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (GameData.IsPast() && music.getActiveAudio().Count == 0)
        {
            music.Play(music.theParty);
        }

        if(activeInteraction) return;
        //if (locked)
        //    return;

        // Move this object to the position clicked by the mouse.
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (floor.Raycast(ray, out RaycastHit hit, 200.0f))
            {
                avatar.SetTargetMarker(hit.point);
                //GameObject newInstance = (GameObject)GameObject.Instantiate(clickPoint, hit.point, Quaternion.identity);
                //clickPoint.transform.position = hit.point;
                //clickPoint.SetActive(true);

                //avatar.SetDestination(hit.point);
                //avatar.isStopped = false;
            }

        }

        //// Wenn Clickpunkt erreicht, entferne Clickpunkt
        //if ((avatar.transform.position - clickPoint.transform.position).magnitude < DIST_TO_SHOW_TARGET && clickPoint.activeSelf)
        //{
        //    targetReached();
        //}

        //// TODO: Target als Eigenschaft des Players festlegen
        ////Vector3 target = 

        //if (!avatar.isStopped && clickPoint.activeSelf)
        //{
        //    float dist = (avatar.transform.position - clickPoint.transform.position).magnitude;
        //    float dist_time = Time.fixedTime;

        //    // wenn noch gar kein vorheriger Abstand vorliegt, oder sich der Abstand seit dem letzten Mal um mehr als 0.1 verkleinert hat,
        //    // neu als bisher größte Annäherung speichern
        //    if(this.dist_to_target == null || this.dist_to_target - dist > 0.1)
        //    {
        //        this.dist_to_target = dist;
        //        this.time_last_dist = dist_time;
        //    }
        //    else
        //    {
        //        // wenn der Abstand nahezu Null beträgt, oder sogar größer wurde, noch 0.4s warten, dann stoppen.
        //        if((dist_time - this.time_last_dist) > 0.4)
        //        {
        //            this.time_last_dist = null;
        //            this.dist_to_target = null;
        //            avatar.isStopped = true;
        //            targetReached();
        //        }
        //    }

        //}

    }

    public NPC[] GetSceneNPCs()
    {
        return NPCs;
    }

    //private void targetReached()
    //{
    //    clickPoint.SetActive(false);


    //    // Prüfe Nähe zu Interaktionspartnern

    //    float[] distances = new float[NPCs.Length];
    //    for (int i = 0; i < NPCs.Length; i++)
    //        distances[i] = (new Vector3(avatar.transform.position.x, 0.0f, avatar.transform.position.z)
    //            - new Vector3(NPCs[i].transform.position.x, 0.0f, NPCs[i].transform.position.z)).magnitude;
    //    float minDist = distances.Min();
    //    if (minDist < DIST_TO_INTERACT)
    //    {
    //        int minIndex = Array.IndexOf(distances, minDist);

    //        NPC closestNpc = NPCs[minIndex];

    //        //StartCoroutine(Story(closestNpc as Tenta));
    //        int actionID = 0;
    //        if (closestNpc.Name == "Time Machine")
    //        {
    //            actionID = 3; // TM soll Zeitsprung durchführen
    //        }
    //        StartCoroutine(Story(closestNpc, actionID));
    //        //}
    //    }
    //}

    public void StartStory(NPC npc, int actionID)
    {
        StartCoroutine(Story(npc, actionID));
    }


    //private IEnumerator Story(Tenta tenta)
    private IEnumerator Story(NPC npc, int actionID)
    {
        activeInteraction = true;
        
        yield return npc.Interact(actionID);
        avatar.locked = true;

        /*for (int i = 0; i < 120; i++)
        {
            if (i % 20 == 0)
                Debug.Log(i);
            yield return 0;
        }*/

        //yield return new WaitForSeconds(2.0f);


        avatar.locked = false;
        activeInteraction = false;
        yield return null;
    }

    

    public void SwitchTime()
    {

        if(GameData.IsPresent())
        {
            GameData.SetTime(GameData.TIME.PAST);
            SceneManager.LoadSceneAsync(sceneName: "PartyScene");
        }
        else
        {
            GameData.SetTime(GameData.TIME.PRESENT);
            SceneManager.LoadSceneAsync(sceneName: "PresentScene");
        }
    }
}
