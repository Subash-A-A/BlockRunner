using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] bool fullAuto = false;
    [SerializeField] int maxAmmo = 9;
    [SerializeField] Animator anim;
    [SerializeField] float reloadTime = 3f;

    private bool isShooting;
    private bool isReloading;
    private bool canShoot;
    private bool canReload;

    private int currentAmmo;

    private void Start()
    {
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
    }

    void Shoot()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 200f))
        {
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
        Debug.Log(currentAmmo);
    }

    void FireAnimation()
    {
        anim.SetTrigger("Fire");
    }
    IEnumerator Reload()
    {
        canReload = false;
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canReload = true;
        canShoot = true;
    }
}
