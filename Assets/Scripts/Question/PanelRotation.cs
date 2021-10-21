using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRotation : MonoBehaviour
{
    [SerializeField] private Transform Player;
    void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(Player.gameObject.transform.position - transform.position, Vector3.down);
        gameObject.transform.rotation = new Quaternion(0f, gameObject.transform.rotation.y, 0f, gameObject.transform.rotation.w);
    }
}
