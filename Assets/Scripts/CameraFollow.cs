using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject PlayerObject;
    const float DISTANCE_TO_FOLLOW = 0.5f; //In world points
    const float INTERP_TIME = 0.1f;
    const float MAX_POS = 2.9f;
    const float MIN_POS = -2.9f;
    const float DISTANCE_FROM_PLAYER = 1.75f;
    void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");

    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var camPos = transform.position;
        var playerPos = PlayerObject.transform.position;
        if(Mathf.Abs(camPos.x - playerPos.x) >= DISTANCE_TO_FOLLOW)
        {
            var newCamPos = Vector3.Lerp(camPos, playerPos, INTERP_TIME);
            if (newCamPos.x > MAX_POS)
                newCamPos.x = MAX_POS;
            else if (newCamPos.x < MIN_POS)
                newCamPos.x = MIN_POS;
            camPos.x = newCamPos.x;
            
        }
        camPos.z = playerPos.z - DISTANCE_FROM_PLAYER;
        transform.position = camPos;
    }
}
