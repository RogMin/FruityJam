using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerAnim : MonoBehaviour
{
    [SerializeField] private Animator Animator; //set in inspector
    [SerializeField] private AudioSource audioSource; //set in inspector
    [HideInInspector] public bool IsWalk; 
    void Start()
    {
        SwitchAnimation(true);
    }
   public void SwitchAnimation(bool walk)
    {
        if (!walk) 
        {
            audioSource.Stop();
        }
        else 
        {
            audioSource.Play();
        }
        IsWalk = walk;
        Animator.SetBool("IsWalk", IsWalk);
    }
}

