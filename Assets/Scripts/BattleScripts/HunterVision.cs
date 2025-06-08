using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterVision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GetComponentInParent<Hunter>() != null)
        {
           GetComponentInParent<Hunter>().OnDetectingPlayer?.Invoke(collision.gameObject);

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GetComponentInParent<Hunter>() != null)
        {
            GetComponentInParent<Hunter>().OnUndetectingPlayer?.Invoke();

        }


    }
}
