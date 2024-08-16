using BBE.CustomClasses;
using DarkMode.Helpers;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.UI;
using MTM101BaldAPI;
using BepInEx;
using BBE.Helpers;
using BepInEx.Bootstrap;

namespace DarkMode
{
    [ConditionalPatchMod("rost.moment.baldiplus.extramod")]
    [HarmonyPatch(typeof(FunSetting))]
    public class PatchBBEUI
    {
        [HarmonyPatch("CreateFunSetting", new Type[] {typeof(CustomFunSettingData)})]
        [HarmonyPostfix]
        private static void Patch(ref FunSetting __result)
        {
            __result.button.gameObject.transform.Find("Box").GetComponent<Image>().sprite = AssetsHelper.TextureFromFile("Box.png").ToSprite();
            __result.button.gameObject.transform.Find("ToggleText").GetComponent<TMP_Text>().color = Color.white;
        }
    }
}
