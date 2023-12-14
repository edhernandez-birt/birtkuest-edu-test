using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

[RequireComponent(typeof(NavMeshAgent))]

//if you use this code you are contractually obligated to like the YT video
public class ChasePlayer : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public PlayerControllerGirl player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        Debug.DrawLine(transform.position, player.transform.position, Color.blue);
        agent.SetDestination(player.transform.position);
    }

}