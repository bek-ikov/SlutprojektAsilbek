using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    public GameObject WEAPON1;
    public GameObject WEAPON2;


    [SerializeField]
    private GunData gunData;
    [SerializeField]
    private GunData gunData2;

    [SerializeField]
    private TextMeshProUGUI ammoCounter;

    private void Update()
    {
        if (WEAPON1.activeInHierarchy == true)
        {
            ammoCounter.text = gunData.currentAmmo + "/" + gunData.magSize;
        }
        if (WEAPON2.activeInHierarchy == true)
        {
            ammoCounter.text = gunData2.currentAmmo + "/" + gunData2.magSize;
        }
    }
}

