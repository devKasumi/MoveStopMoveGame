using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Player player;
    [SerializeField] private List<GameObject> weaponObjects = new List<GameObject>();
    [SerializeField] private List<Vector3> weaponObjectPos = new List<Vector3>();

    private void Start()
    {
        //for (int i = 0; i < weaponObjectPos.Count; i++)
        //{
        //    GameObject weaponObject = Instantiate(weaponObjects[3], weaponObjectPos[i], weaponObjects[3].transform.rotation);
        //    if (i == weaponObjectPos.Count - 1)
        //    {
        //        Debug.LogError("reach last pos!!!");
        //        Transform tf = weaponObject.transform;
        //        tf.localScale = new Vector3(tf.localScale.x * 4, tf.localScale.y * 4, tf.localScale.z * 4);
        //    }
        //}

        for (int i = 0; i < weaponObjects.Count; i++)
        {
            //Debug.LogError(player.Weapon.WeaponType);
            for (int j = 0; j < weaponObjectPos.Count; j++)
            {
                GameObject weaponObject = Instantiate(weaponObjects[i], weaponObjectPos[j], weaponObjects[i].transform.rotation);
                if (i == (int)player.Weapon.WeaponType)
                {
                    weaponObject.SetActive(true);
                }
                else weaponObject.SetActive(false);
            }
        }
    }
}
