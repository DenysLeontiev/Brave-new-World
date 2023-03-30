using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtPlayer : MonoBehaviour
{

    private void Update()
    {
        transform.LookAt(PlayerHealth.Instance.transform);
    }
}
