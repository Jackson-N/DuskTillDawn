using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Shootweapon : MonoBehaviour
{
    [SerializeField] private GameObject _lanchPosition, _bulletPrefab;
    private bool _okayToShoot = false;
    private bool _shootingPaused = false;
    private InputData _inputData;

    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_okayToShoot)
        {
            if(!_shootingPaused)
            {
                if(_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool _aButtonPressed))
                {
                    if(_aButtonPressed)
                    {
                        _shootingPaused = true;
                        Fire();
                        StartCoroutine(Pause());
                    }
                }
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(_bulletPrefab) as GameObject;
        bullet.SetActive(true);
        bullet.transform.position = _lanchPosition.transform.position;
        bullet.transform.rotation = _lanchPosition.transform.rotation;

        bullet.GetComponent<Rigidbody>().AddForce(_lanchPosition.transform.forward * 50f, ForceMode.Impulse);
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.5f);
        _shootingPaused = false;
    }

    public void PickedUpWeaponTrigger()
    {
        _okayToShoot = true;
    }

    public void DroppedWeaponTrigger()
    {
        _okayToShoot = false;
    }
}
