using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCombat : AbstractCombat
{


    public static DogCombat instance;

    float numberClicks = 0;
    float lastClickedTime = 0;
    float comboDelay = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    protected override void startChildren()
    {

    }

    protected override void updateChildren()
    {
        if (
            !anim.GetCurrentAnimatorStateInfo(1).IsName("FirstAttack") &&
            !anim.GetCurrentAnimatorStateInfo(1).IsName("SecondAttack") &&
            !anim.GetCurrentAnimatorStateInfo(1).IsName("HeavyAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("PathAttack")
            )
        {
            isAttacking = false;
        }
        else
        {
            isAttacking = true;
        }
    }
    public override void resetAllBool()
    {
        anim.ResetTrigger("FirstAttack");
        anim.ResetTrigger("SecondAttack");
        anim.ResetTrigger("HeavyAttack");
        anim.SetBool("PathAttack", false);
    }

    public override void checkAttack()
    {
        // Kalo misalkan kombo ga diselesain ama user tapi mo berentiin animasi
        if (anim.GetAnimatorTransitionInfo(1).normalizedTime >= 0.6f && anim.GetAnimatorTransitionInfo(1).IsName("FirstAttack"))
        {
            anim.SetBool("FirstAttack", false);
        }
        if (anim.GetAnimatorTransitionInfo(1).normalizedTime >= 0.8f && anim.GetAnimatorTransitionInfo(1).IsName("SecondAttack"))
        {
            anim.SetBool("SecondAttack", false);
            numberClicks = 0;
            resetAllBool();
        }

        if (Time.time - lastClickedTime > comboDelay)
        {
            // Reset kombo kalo uda ngelewatin cooldown
            numberClicks = 0;
            resetAllBool();

            isAttacking = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            onAttack();
        }

        if (Input.GetMouseButton(1))
        {
            anim.SetTrigger("HeavyAttack");
        }

    }

    protected override void onAttack()
    {
        lastClickedTime = Time.time;
        numberClicks++;

        if (numberClicks == 1)
        {
            anim.SetBool("SecondAttack", false);
            anim.SetBool("FirstAttack", true);
        }

        numberClicks = Mathf.Clamp(numberClicks, 0, 2);

        if (numberClicks >= 2 && anim.GetCurrentAnimatorStateInfo(1).IsName("FirstAttack"))
        {
            anim.SetBool("FirstAttack", false);
            anim.SetBool("SecondAttack", true);
            numberClicks = 0;
        }
    }

}
