using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] private AnimatorOverrideController playerOverrideController;

    [SerializeField] private Transform weaponTransform;

    public bool canAttack = true;
    public bool isArmWeapon = false;
    public bool isMeleeWeapon = false;

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleWeaponStates();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
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


    private void Attack()
    {
        if (canAttack && isArmWeapon)
        {
            StartCoroutine(PlayerAttackAnim("armAttack", "armAttackIndex", 0.8f, 6, 0));
        }
        else if(canAttack && isMeleeWeapon)
        {
            StartCoroutine(PlayerAttackAnim("meleeAttack", "meleeAttackIndex", 1f, 5, 0));

        }
    }

    private IEnumerator PlayerAttackAnim(string triggerName, string triggerIndex, float delay, int maxTriggerIndex, int minTriggerIndex = 0)
    {
        canAttack = false;
        playerAnimator.SetTrigger(triggerName);
        playerAnimator.SetInteger(triggerIndex, Random.Range(minTriggerIndex, maxTriggerIndex));
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
}
