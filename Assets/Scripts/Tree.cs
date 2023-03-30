using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour, ITreeDamagable
{
    [SerializeField] private Slider slider;
    [Range(0, 100)] [SerializeField] private int maxTreeHealth = 100;
    private int currentTreeHealth;

    private void Start()
    {
        currentTreeHealth = maxTreeHealth;
    }

    private void Update()
    {
        slider.value = (float) currentTreeHealth / 100;
        Debug.Log(currentTreeHealth);
    }

    public void DamageTree(int damage)
    {
        currentTreeHealth -= damage;
        if(currentTreeHealth <= 0)
        {
            DestroyTreeAndSpawnPrefab();
        }
    }

    private void DestroyTreeAndSpawnPrefab()
    {
        Debug.Log("Tree called " + transform.name + "is cut down");
    }
}
