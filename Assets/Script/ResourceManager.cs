using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private void Awake()
    {
        Instance = this;
    }


}
