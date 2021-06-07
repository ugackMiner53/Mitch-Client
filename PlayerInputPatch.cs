﻿using UnityEngine;
using HarmonyLib;
using BepInEx;

namespace MitchClient
{
    [HarmonyPatch(typeof(PlayerInput), "MyInput")]
    class PlayerInputPatch
    {


		static T GetValue<T>(object instance, string fieldname)
		{
			return (T)Traverse.Create(instance).Field(fieldname).GetValue();
		}

		static void SetValue<T>(object instance, string fieldname, object value)
		{
			// Thank you to @funnynumber#3171 for the good code
			Traverse.Create(instance).Field(fieldname).SetValue(value);
		}



		static bool hovering = false;
		static bool invincibility = false;
		static bool infiniteStamina = false;
		static bool strength = false;
		static bool attackSpeed = false;
		static int selectedPlayer = 0;




		static void Postfix(PlayerInput __instance)
        {

			if (Input.GetKeyDown(KeyCode.C))
			{
				Debug.Log("[Mitch] Pressed Clipping Key");
				GetValue<PlayerMovement>(__instance, "playerMovement").GetPlayerCollider().enabled = false;
			}
			if (Input.GetKeyUp(KeyCode.C))
			{
				Debug.Log("[Mitch] Released Clipping Key");
				GetValue<PlayerMovement>(__instance, "playerMovement").GetPlayerCollider().enabled = true;
			}
			if (Input.GetKey(KeyCode.R))
			{
				GetValue<PlayerMovement>(__instance, "playerMovement").GetRb().velocity = Vector3.zero;
				__instance.gameObject.transform.position += GetValue<Transform>(__instance, "playerCam").gameObject.transform.forward * Time.deltaTime * 30f;
			}
			if (Input.GetKeyDown(KeyCode.Y))
			{
				hovering = !hovering;
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				invincibility = !invincibility;
			}
			if (Input.GetKeyDown(KeyCode.Z))
			{
				infiniteStamina = !infiniteStamina;
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				strength = !strength;
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				attackSpeed = !attackSpeed;
			}
			if (strength && Hotbar.Instance.currentItem != null)
			{
				Hotbar.Instance.currentItem.attackDamage = 6969;
			}
			else if (!strength && Hotbar.Instance.currentItem != null)
			{
				Hotbar.Instance.currentItem.attackDamage = 1;
			}
			if (attackSpeed && Hotbar.Instance.currentItem != null)
			{
				Hotbar.Instance.currentItem.attackSpeed = 100f;
			}
			else if (!attackSpeed && Hotbar.Instance.currentItem != null)
			{
				Hotbar.Instance.currentItem.attackSpeed = 1f;
			}
			if (hovering)
			{
				GetValue<PlayerMovement>(__instance, "playerMovement").GetRb().velocity = new Vector3(0f, 1f, 0f);
			}
			if (invincibility)
			{
				__instance.GetComponent<PlayerStatus>().hp = (float)__instance.GetComponent<PlayerStatus>().maxHp;
			}
			if (infiniteStamina)
			{
				__instance.GetComponent<PlayerStatus>().stamina = __instance.GetComponent<PlayerStatus>().maxStamina;
			}
			if (Input.GetKey(KeyCode.V))
			{
				foreach (Item item in Object.FindObjectsOfType<Item>())
				{
					__instance.transform.position = item.transform.position;
				}
			}
			if (Input.GetKey(KeyCode.B))
			{
				foreach (PickupInteract pickupInteract in Object.FindObjectsOfType<PickupInteract>())
				{
					pickupInteract.Interact();
				}
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				selectedPlayer++;
				if (selectedPlayer > Object.FindObjectsOfType<OnlinePlayer>().Length)
				{
					selectedPlayer = 1;
				}
				__instance.transform.position = Object.FindObjectsOfType<OnlinePlayer>()[selectedPlayer].transform.position;
			}
		}
    }
}
