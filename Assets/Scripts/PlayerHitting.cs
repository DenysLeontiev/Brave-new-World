using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitting : MonoBehaviour
{
    [SerializeField] private GameObject hitArea;
    [SerializeField] private Transform popUpTextPrefab;

    private void Update()
    {
        HitObjects();
    }

    private void HitObjects()
    {
        Vector3 colliderSize = Vector3.one * 0.3f;

        Collider[] colliders = Physics.OverlapBox(hitArea.transform.position, colliderSize);
        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent<ITreeDamagable>(out ITreeDamagable treeDamagable))
            {
                if(Input.GetMouseButton(0) && canCut)
                {
                    StartCoroutine(CutTreeDown(treeDamagable));
                }
            }
        }
    }

    bool canCut = true;
    private IEnumerator CutTreeDown(ITreeDamagable treeDamagable)
    {
        float cutDelay = 0.7f;
        canCut = false;
        yield return new WaitForSeconds(cutDelay);
        //TODO: check whether we hit with arm/sword etc.
        int randomDamage = Random.Range(10, 15);
        TextPopUp.SpawnTextPopUp(popUpTextPrefab, transform.position, transform, randomDamage.ToString());
        treeDamagable.DamageTree(randomDamage);
        canCut = true;

    }
}
