using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnimator;

    [SerializeField] private Transform weaponTransform;

    public bool canAttack = true;
    public bool isArmWeapon = false;
    public bool isMeleeWeapon = false;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleWeaponStates();

        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }

    public void Jump()
    {
        playerAnimator.SetTrigger("jump");
    }

    private void HandleWeaponStates()
    {
        isArmWeapon = IsArmWeapon();
        isMeleeWeapon = !isArmWeapon;
    }

    private bool IsArmWeapon()
    {
        if(weaponTransform.childCount == 0)
        {
            return true;
        }
        return false;
    }


    private IEnumerator Attack()
    {
        if (canAttack && isArmWeapon)
        {
            canAttack = false;
            playerAnimator.SetTrigger("armAttack");
            playerAnimator.SetInteger("armAttackIndex", Random.Range(0, 6));
            yield return new WaitForSeconds(0.8f);
            canAttack = true;
        }
    }
}
