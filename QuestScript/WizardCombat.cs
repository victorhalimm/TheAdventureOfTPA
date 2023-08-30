using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCombat : AbstractCombat
{

    [SerializeField] private Camera cam;
    [SerializeField] private GameObject normalProjectiles;
    [SerializeField] private float projSpeed = 30;

    [SerializeField] private Transform firepoint;

    private Vector3 destination;



    public override void checkAttack()
    {
        if (Input.GetMouseButton(0)) normalAttack();
    }

    void normalAttack()
    {
        anim.SetTrigger("NormalAttack");

        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.8f) shootProjectiles();
    }

    void shootProjectiles()
    {   
        destination = transform.position + transform.forward * 1000;

        createProjectiles(firepoint, normalProjectiles);
    }

    void createProjectiles(Transform point, GameObject chosenProj)
    {
        var projectile = Instantiate(chosenProj, point.position, Quaternion.identity);
        Vector3 projDirection = destination - point.position;
        //Make sure that the projectile will shoot forward with no velocity making it to fall or fly above
        projectile.GetComponent<Rigidbody>().velocity = projDirection.normalized * projSpeed;
        iTween.PunchPosition(projectile, new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), Random.Range(-0.5f, 2));
    }
    protected override void onAttack()
    {
        
    }

    public override void resetAllBool()
    {
        
    }

    protected override void startChildren()
    {
        
    }

    protected override void updateChildren()
    {
        
    }
}
