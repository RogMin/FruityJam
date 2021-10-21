using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainQuestion : MonoBehaviour
{
    public static event Action GameIsEnd;
    [SerializeField] private GameObject EndGameImage; //set in inspector
    [SerializeField] private TextMeshProUGUI CompleteAmount; //set in inspector 
    [SerializeField] private TextMeshProUGUI FailedAmount; //set in inspector
    [SerializeField] private const int MustOrderCompleteToWin = 8; 
    private MusicPlayer musicPlayer;
    private int OrderComplete;
    private int FailedOrders;
    private void Start()
    {
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        UpdateText();
    }
    private void UpdateText()
    {
        CompleteAmount.text = OrderComplete.ToString();
        FailedAmount.text = FailedOrders.ToString();
    }
    public void PlayerChangeOrder(bool isComplete)
    {
        if (isComplete) OrderComplete++;
        else FailedOrders++;
        if(OrderComplete >= MustOrderCompleteToWin)
        {
            EndGame();
        }
        UpdateText();
    }
    private void EndGame()
    {
        GameIsEnd.Invoke();
        EndGameImage.SetActive(true);
        Time.timeScale = 0;
        GameObject.FindObjectOfType<NoMouseCursor>().SetCursorLock(false);
        musicPlayer.EndGame();
    }
}
