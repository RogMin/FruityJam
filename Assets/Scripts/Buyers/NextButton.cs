using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound; //set in inspector
    private Animator animator;
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void ButtonClick()
    {
        audioSource.PlayOneShot(buttonSound);
        animator.Play("PressButton");
        Buyers.Instance.NextBuyer();
    }
}
