using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartTest());
    }


    IEnumerator StartTest()
    {
        yield return new WaitForSeconds(8f);
        PlayerHealth.OnTakeDamage(15);
    }
}
