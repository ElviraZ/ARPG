using UnityEngine;
using System.Collections;

public class HUDRoot : MonoBehaviour
{

    public static GameObject go;
    void Awake()
    {
        go = gameObject;
    }
}
