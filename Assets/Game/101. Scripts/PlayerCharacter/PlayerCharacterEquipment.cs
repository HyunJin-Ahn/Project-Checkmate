﻿using System;
using System.Linq;
using UnityEngine;

class PlayerCharacterEquipment : MonoBehaviour
{
    [Header("오브젝트 설정")]
    //[SerializeField] DamageTriggerManager damageTriggerManager;
    [SerializeField] Transform weaponJoint;
    [SerializeField] Transform currentWeaponTransform;

    [Header("데이터")]
    [SerializeField] WeaponData weaponData;

    DamageTrigger[] damageTriggers = Array.Empty<DamageTrigger>();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Correctness", "UNT0008:Null propagation on Unity objects", Justification = "<보류 중>")]
    public WeaponData WeaponData
    {
        get => weaponData;
        set
        {
            var weaponData = this.weaponData.ToReferenceNull();
            value = value.ToReferenceNull();
            if (weaponData?.ID != value?.ID)
            {
                weaponData = value;

                // 기존 무기 제거
                if (currentWeaponTransform != null)
                {
                    //currentWeaponTransform.gameObject.SetActive(false);
                    var currentWeaponObject = currentWeaponTransform.gameObject;

                    damageTriggers = null;
                    Destroy(currentWeaponObject);
                }

                // 무기 오브젝트 생성
                if (weaponData != null)
                {
                    var currentWeaponObject = Instantiate(weaponData.WeaponPrefab);

                    // 부모 설정
                    currentWeaponTransform = currentWeaponObject.transform;
                    currentWeaponTransform.parent = weaponJoint;

                    damageTriggers = currentWeaponObject.GetComponents<DamageTrigger>();
                }
            }
        } // end of setter
    } // end of WeaponData

    public void StartTrigger()
    {
        foreach (var damageTrigger in damageTriggers)
        {
            damageTrigger.StartTrigger();
        }
    }

    public void EndTrigger()
    {
        foreach (var damageTrigger in damageTriggers)
        {
            damageTrigger.EndTrigger();
        }
    }

    void DamageTrigger_StartTrigger()
    {
        StartTrigger();
    }

    void DamageTrigger_EndTrigger()
    {
        EndTrigger();
    }
}