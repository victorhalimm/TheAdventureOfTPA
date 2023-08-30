using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCombat : MonoBehaviour
{

    public bool isAttacking = false;
    
    protected Animator anim;

    public abstract void resetAllBool();

    public abstract void checkAttack();

    protected abstract void onAttack();

    protected abstract void startChildren();

    protected abstract void updateChildren();

    public void disableScript()
    {
        this.enabled = false;
    }

    public void enableScript()
    {
        this.enabled = true;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        startChildren();
    }

    private void Update()
    {

        updateChildren();
    }

}
