using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject fxSource;

    public Vector3 GetSourcePos() {
        return fxSource.transform.position;
    }
}
