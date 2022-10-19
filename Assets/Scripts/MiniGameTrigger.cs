using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MiniGameTrigger : MonoBehaviour
{

    //bool keeping track of whether is triggered.
    public bool triggered = false;
    public bool played = false;
    public bool enable = false;
    public bool miniEnable = false;

    //Maximum distance from player that can be triggered.
    public float distance = 2.5f;

    //A indication that pops up when player is in range of an available dialogue.
    public TMP_Text Interactable;

    //Text displayed by Interactable.
    [TextArea(5, 10)]
    public string interactableName;
    //public TMP_Text dialogueText;

    //transforms of player camera, target camera and return position.
    public View playerView;
    public View miniView;

    Transform playerCamera;
    Transform targetCamera;
    Transform returnCamera;
    public float timer = 0;

    void Start()
    {
        //sets the camera return posiiton.
        playerCamera = playerView.gameObject.transform.Find("Camera");
        returnCamera = playerView.gameObject.transform.Find("ReturnCam");
        targetCamera = gameObject.transform.Find("Camera");
        returnCamera.transform.position = playerCamera.transform.position;
        Interactable = playerCamera.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!played && timer > 0.5f)
            returnCamera.transform.localRotation = playerCamera.transform.localRotation;

        if (timer < 0.5f)
        {
            timer += Time.deltaTime;
        }

        //lerps player camera between target and return positions. depending on whether is playing the minigame.
        if (played)
        {
            playerCamera.transform.position = Vector3.Lerp(returnCamera.transform.position, targetCamera.transform.position, timer / 0.5f);
            playerCamera.transform.rotation = Quaternion.Lerp(returnCamera.transform.rotation, targetCamera.transform.rotation, timer / 0.5f);
            if (timer > 0.5f)
            {
                //miniView.isActive = true;
            }
        }
        else
        {
            playerCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, returnCamera.transform.position, timer / 0.5f);
            playerCamera.transform.rotation = Quaternion.Lerp(targetCamera.transform.rotation, returnCamera.transform.rotation, timer / 0.5f);
            if(timer > 0.5f && enable)
            {
                playerView.isActive = true;
            }
        }

        //casts a ray from player camera, and checks if the hit object contains a minigame trigger.
        //if so, turn on the indication.
        RaycastHit hit;

        if (Physics.Raycast(playerView.playerHead.position, playerView.playerHead.forward, out hit, distance))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                if (!triggered)
                {
                    triggered = true;
                    Interactable.text = interactableName;
                    Interactable.enabled = true;
                }
            }
            else
            {
                triggered = false;
            }

        }
        else
        {
            triggered = false;
            Interactable.enabled = false;
        }


        if (triggered)
        {

            //When key pressed, pauses main player control, and starts minigame player control.
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (!played)
                {
                    timer = 0;
                    Debug.Log("????");
                    Interactable.text = "[E] EXIT";
                    //playerView.enabled = false;
                    //miniView.enabled = true;
                    playerView.isActive = false;
                    miniView.isActive = true;
                    played = true;
                    enable = false;
                    //returnView = playerView;
                }
                //starts main player control, and pauses minigame player control.
                else if (played && (!miniView.trigger.triggered || miniView.trigger.played) && miniView.isActive)
                {
                    timer = 0;
                    Interactable.text = interactableName;
                    //playerView.enabled = true;
                    //miniView.enabled = false;
                    //playerView.isActive = true;
                    miniView.isActive = false;
                    played = false;
                    enable = true;
                    //targetCamera.transform.rotation = playerCamera.transform.rotation;
                }

            }
        }

    }
}