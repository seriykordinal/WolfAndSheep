using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointForHunter : MonoBehaviour
{
    public int Id;
    public bool IsBusy;

    private void Awake()
    {
        IsBusy = false;
    }
}
