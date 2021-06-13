using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;

public class AWSManager : MonoBehaviour
{
    void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
    }
}
