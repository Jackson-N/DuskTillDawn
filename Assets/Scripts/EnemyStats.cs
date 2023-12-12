using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public int EnemyHealth = 20;

    // Update is called once per frame
    void Update()
    {
        if(EnemyHealth <= 0)
        {
            Died();
        }
    }

    public void TakeDamage(int damage)
    {
        EnemyHealth -= damage;

        GameObject CanvasChild = transform.GetChild(0).gameObject;
        GameObject HealthBarChild = CanvasChild.transform.GetChild(0).gameObject;
        HealthBarChild.GetComponent<EnemyHealthBar>().ChangeHealth(EnemyHealth);
    }

    private void Died()
    {
        Destroy(gameObject);
    }
}
