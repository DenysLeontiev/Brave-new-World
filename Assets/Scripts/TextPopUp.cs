using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public static void SpawnTextPopUp(Transform popUpPrefab, Vector3 position,Transform parent, string text)
    {
        popUpPrefab.GetComponent<TextMeshPro>().text = text;
        var instatiatedObject = Instantiate(popUpPrefab, position, Quaternion.identity, parent);
    }
}
