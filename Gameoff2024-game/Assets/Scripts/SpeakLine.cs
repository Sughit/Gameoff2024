using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakLine : MonoBehaviour
{
    [SerializeField] int numEnters;
    [SerializeField] AudioClip[] linesAud;
    [SerializeField] string[] linesText;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && numEnters < linesAud.Length)
        {
            other.gameObject.GetComponent<HearLine>().Hear(linesAud[numEnters]);
            other.gameObject.GetComponent<SeeLine>().See(linesText[numEnters]);
            numEnters++;
        }
    }
}
