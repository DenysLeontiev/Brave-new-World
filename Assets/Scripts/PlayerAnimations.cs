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
            AttackAnimation();
        }
    }

    public void JumpAnimation()
    {
        playerAnimator.SetTrigger("jump");
    }

    public void RollAnimation()
    {
        playerAnimator.SetTrigger("roll");
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


    private void AttackAnimation()
    {
        if (canAttack && isArmWeapon)
        {
            float armAttackDelay = 0.8f; 
            StartCoroutine(PlayerAttackAnim("armAttack", "armAttackIndex", armAttackDelay, 6, 0));
        }
        else if(canAttack && isMeleeWeapon)
        {
            float meleeAttackDelay = 0.8f;
            StartCoroutine(PlayerAttackAnim("meleeAttack", "meleeAttackIndex", meleeAttackDelay, 5, 0));

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
