using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlantAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent; //set in inspector
    [SerializeField] private Transform[] EscapesPoint; //set in inspector
    [SerializeField] private float JumpDelay; //set in inspector
    [SerializeField] private bool CanJump; //set in inspector
    [SerializeField] private Rigidbody rb; //set in inspector
    public GameObject DeathPrefab; //set in inspector
    private Vector3 point;
    private Vector3 jumpForce;
    [HideInInspector] public bool IsRise;
    void Start()
    {
        jumpForce = new Vector3(0f, 1.3f, 0f);
        point = EscapesPoint[Random.Range(0, EscapesPoint.Length)].position;
        navMeshAgent.SetDestination(point);
        if (CanJump)
        {
            StartCoroutine(JumpCoroutine());
        }       
    }
    IEnumerator JumpCoroutine()
    {
        while (true)
        {
            if (IsRise)
            {
                rb.isKinematic = true;
                navMeshAgent.enabled = false;
                break;
            }
            rb.isKinematic = false;
            navMeshAgent.enabled = false;
            rb.AddForce(jumpForce * 3, ForceMode.Impulse);
            yield return new WaitForSeconds(JumpDelay);
            rb.isKinematic = true;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(point);

            yield return new WaitForSeconds(3f);
        }
    }
}
