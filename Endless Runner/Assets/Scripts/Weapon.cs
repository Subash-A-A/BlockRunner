using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] bool fullAuto = false;
    [SerializeField] int maxAmmo = 9;
    [SerializeField] Animator anim;
    [SerializeField] float reloadTime = 3f;
    [SerializeField] int weaponDamage = 1;

    [SerializeField] ParticleSystem ImpactEffect;
    [SerializeField] LayerMask TargetMask;

    private Animator weaponAnim;
    private bool isShooting;
    private bool isReloading;
    private bool canShoot;
    private bool canReload;

    private int currentAmmo;

    private void Start()
    {   
        weaponAnim = GetComponent<Animator>();
        canShoot = true;
        canReload = true;
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        WeaponInput();

        if (isShooting && currentAmmo > 0 && canShoot)
        {
            Shoot();
        }

        if(currentAmmo < maxAmmo && isReloading && canReload)
        {
            StartCoroutine(Reload());
        }

        WeaponSlideState();
    }

    void Shoot()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, TargetMask))
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                Instantiate(ImpactEffect, hitInfo.point, Quaternion.identity);
                BossBehaviour boss = hitInfo.transform.gameObject.GetComponent<BossBehaviour>();
                boss.TakeDamage(weaponDamage, false);
            }
            Fire();
        }
    }

    void WeaponInput()
    {
        isShooting = fullAuto ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
        isReloading = Input.GetKeyDown(KeyCode.R);
    }

    void Fire()
    {
        FireAnimation();
        currentAmmo--;
    }

    void FireAnimation()
    {
        anim.SetTrigger("Fire");
        weaponAnim.SetTrigger("Shoot");
    }
    IEnumerator Reload()
    {
        canReload = false;
        canShoot = false;
        anim.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canShoot = true;
        canReload = true;
    }

    void WeaponSlideState()
    {
        if(currentAmmo == 0 || !canReload)
        {
            weaponAnim.SetBool("magFull", false);
        }
        else
        {
            weaponAnim.SetBool("magFull", true);
        }
    }

    public void ReloadWeaponAnimation()
    {
        weaponAnim.SetTrigger("Reload");
    }
}
