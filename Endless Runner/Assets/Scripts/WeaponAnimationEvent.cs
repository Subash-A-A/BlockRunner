using UnityEngine;

public class WeaponAnimationEvent : MonoBehaviour
{
    [SerializeField] GameObject WeaponHolder;

    private Animator weaponAnim;
    private void Start()
    {
        weaponAnim = WeaponHolder.GetComponentInChildren<Animator>();
    }
    public void WeaponReloadInit() {
        weaponAnim.SetBool("magFull", false);
    }
    public void WeaponReloadEnd()
    {
        weaponAnim.SetBool("magFull", true);
    }
}
