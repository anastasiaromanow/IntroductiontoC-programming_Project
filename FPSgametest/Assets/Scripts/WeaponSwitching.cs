using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    public int  SelectedWeapon = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.GetInstance().IsItPlayerTurn(_inputManager.PlayerIndex))
        {

            int previousSelectedWeapon = SelectedWeapon;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (SelectedWeapon >= transform.childCount - 1)
                    SelectedWeapon = 0;
                else
                    SelectedWeapon++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (SelectedWeapon <= 0)
                    SelectedWeapon = transform.childCount - 1;
                else
                    SelectedWeapon--;
            }
            if (previousSelectedWeapon != SelectedWeapon)
            {
                SelectWeapon();
            }
        }
    }

    void SelectWeapon() //loop through weapon indexes, disable/enable
    {
        int i = 0;
        foreach (Transform weapon in transform) //all childs to WeaponHolder
        {
            if (i == SelectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
