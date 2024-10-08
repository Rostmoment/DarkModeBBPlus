﻿using BepInEx;
using HarmonyLib;
using System;
using BBE.Helpers;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI;
using System.Linq;
using System.Collections;
using UnityEngine;
using DarkMode;
using MTM101BaldAPI.OptionsAPI;

namespace DarkMode
{
    [BepInPlugin("rost.moment.baldiplus.darkmode", "Baldi Basics Plus Dark Mode", "0.1")]
    [BepInDependency("mtm101.rulerp.bbplus.baldidevapi", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("rost.moment.baldiplus.extramod", BepInDependency.DependencyFlags.SoftDependency)]
    public class BasePlugin : BaseUnityPlugin
    {
        public static BasePlugin Instance = null;
        private IEnumerator Enumerator()
        {
            yield return 1;
            yield return "Changing theme to dark...";
            NewUI.NewMenu();
            yield break;
        }
        private void Awake()
        {
            Harmony harmony = new Harmony("rost.moment.baldiplus.darkmode");
            harmony.PatchAllConditionals();
            if (Instance.IsNull())
            {
                Instance = this;
            }
            LoadingEvents.RegisterOnLoadingScreenStart(Info, Enumerator());
            CustomOptionsCore.OnMenuInitialize += NewUI.OnOptionsMenuOpen; // First ever on menu open not for new category
            
        }
    }
}