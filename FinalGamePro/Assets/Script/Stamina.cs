using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    PlayerCon player;
    [SerializeField]Image stamina;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
    }
    private void Update()
    {
        stamina.fillAmount = player.currentStm/player.maxStm;
    }
}
