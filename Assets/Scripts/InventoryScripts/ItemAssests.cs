using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssests : MonoBehaviour
{
    public static ItemAssests Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject WeakSwordPrefab;
    public GameObject ApplePrefab;
    public GameObject WoodPrefab;

    public Sprite appleSprite;
    public Sprite swordSprite;
    public Sprite woodSprite;
    public Sprite objectSprite;
}
