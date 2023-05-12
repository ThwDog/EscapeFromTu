using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Image[] heart;
    [SerializeField] Sprite fullheart;
    [SerializeField] Sprite emptyheart;

    void Update()
    {
        //Debug.Log(GameMenager.instance._PlayerLP);
        foreach(var heart in heart)
        {
            heart.sprite = emptyheart;
        }
        for(int i = 0; i < GameMenager.instance._PlayerLP; i++)
        {
            heart[i].sprite = fullheart;
        }
    }
}
