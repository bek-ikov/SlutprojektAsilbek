using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;

    public LineRenderer bulletTracer;
    public Transform weaponMuzzle;

    float timeSinceLastShot;
    public void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;
    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f) && this.gameObject.activeSelf;
    public void Shoot()
    {
        if (gunData.currentAmmo > 0) 
        { 
            if (CanShoot())
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);

                    bulletTracer.SetPosition(0, weaponMuzzle.position);
                    bulletTracer.SetPosition(1, weaponMuzzle.position + weaponMuzzle.forward * gunData.maxDistance);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();

                bulletTracer.enabled = true;
                StartCoroutine(FadeTracer());
            }
        }
    }

    IEnumerator FadeTracer()
    {
        yield return new WaitForSeconds(0.1f);
        bulletTracer.enabled = false;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance); // 0:31 på video https://www.youtube.com/watch?v=kasbsBho9ZM
    }
    private void OnGunShot()
    {

    }
}
