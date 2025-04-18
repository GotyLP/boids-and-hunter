using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.hunter = this;
    }
}
