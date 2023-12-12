using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.XR.CoreUtils;

public class EnemyHealthBar : MonoBehaviour
{

    private float MaxHealthBarLength = 1.0f;
    private float MaxEnemyHealth;
    private float CurrentEnemyHealth;
    private float HealthPercentage;
    private float CurrentHealthBarLength;

    private RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        MaxEnemyHealth = transform.parent.parent.GetComponent<EnemyStats>().EnemyHealth;
        GetComponent<Image>().color = Color.green;
    }

    public void ChangeHealth(float EnemyHealth)
    {
        CurrentEnemyHealth = EnemyHealth;
        HealthPercentage = CurrentEnemyHealth / MaxEnemyHealth;
        CurrentHealthBarLength = MaxHealthBarLength * HealthPercentage;
        rt.sizeDelta = new Vector2(CurrentHealthBarLength, rt.sizeDelta.y);
        ImageHealthColor();
    }

    private void ImageHealthColor()
    {
        if (CurrentEnemyHealth > 14)
        {
            GetComponent<Image>().color = Color.green;
        }
        else if (CurrentEnemyHealth > 7)
        {
            GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }
    }
}
