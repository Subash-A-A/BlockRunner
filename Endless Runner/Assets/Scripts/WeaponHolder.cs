using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Animator anim;

    private int weaponSlot = 0;

    private void Start()
    {
        DisableChildren();
        weaponSlot = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetBool("usePistol", true);
            transform.GetChild(weaponSlot).gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetBool("usePistol", false);
            transform.GetChild(weaponSlot).gameObject.SetActive(false);
        }
    }

    void DisableChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public bool isHoldingWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
