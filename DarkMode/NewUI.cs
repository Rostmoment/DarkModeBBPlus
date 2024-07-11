using BBE.Helpers;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using UnityEngine;
using MTM101BaldAPI;
using UnityEngine.UI;
using BBE;
using NPOI.SS.Formula.Functions;
using System.Linq;

namespace DarkMode
{
    [HarmonyPatch]
    public class UIPatches
    {
        [HarmonyPatch(typeof(NameButton), "Unhighlight")]
        [HarmonyFinalizer]
        private static void WhiteButton(NameButton __instance)
        {
            __instance.text.color = Color.white;
        }
        [HarmonyPatch(typeof(MainMenu), "Start")]
        [HarmonyFinalizer]
        private static void NewMenu(MainMenu __instance)
        {
            List<string> transforms = new List<string>() { "Play", "Options", "About", "Exit" };
            if (AssetsHelper.ModInstalled("mtm101.rulerp.baldiplus.leveleditor"))
            {
                transforms.Add("New Game Object"); // MissingTextureMan WHAT THE HECK IS THIS??!?!?!?
            }
            __instance.gameObject.transform.Find("Image").GetComponent<Image>().sprite = AssetsHelper.TextureFromFile("Menu.png").ToSprite();
            foreach (string s in transforms)
            {
                __instance.gameObject.transform.Find(s).GetComponent<Image>().sprite = AssetsHelper.TextureFromFile(s + "Normal.png").ToSprite();
                __instance.gameObject.transform.Find(s).GetComponent<StandardMenuButton>().unhighlightedSprite = AssetsHelper.TextureFromFile(s + "Normal.png").ToSprite();
                __instance.gameObject.transform.Find(s).GetComponent<StandardMenuButton>().highlightedSprite = AssetsHelper.TextureFromFile(s + "Pressed.png").ToSprite();
            }
            __instance.gameObject.transform.Find("Version").GetComponent<TextMeshProUGUI>().color = Color.white;
            __instance.gameObject.transform.Find("ChangelogButton").GetComponent<Image>().color = Color.black;
        }
        [HarmonyPatch(typeof(MainModeButtonController), "OnEnable")]
        [HarmonyFinalizer]
        public static void AddNewUI(MainModeButtonController __instance)
        {
            if (__instance.gameObject.GetComponent<NewUI>().IsNull())
            {
                __instance.gameObject.AddComponent<NewUI>();
            }
        }
        [HarmonyPatch(typeof(ChallengeWin), "Start")]
        [HarmonyFinalizer]
        private static void Test(ChallengeWin __instance)
        {
            __instance.bg.gameObject.transform.parent.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.white;
            __instance.bg.GetComponent<Image>().sprite = AssetsHelper.TextureFromFile("ChallengeWin.png").ToSprite();
        }
    }
    public class NewUI : MonoBehaviour
    {
        public static void OnOptionsMenuOpen(OptionsMenu optionsMenu)
        {
            Transform b = optionsMenu.transform.Find("Base");
            b.Find("White").GetComponent<Image>().color = Color.black; // White is not white
            foreach (TextMeshProUGUI tmp in b.GetComponentsInChildren<TextMeshProUGUI>())
            {
                tmp.color = Color.white;
            }
            b.Find("BG").GetComponent<Image>().sprite = AssetsHelper.TextureFromFile("Options.png").ToSprite();
            Transform tooltip = optionsMenu.transform.Find("TooltipBase").Find("Tooltip");
            tooltip.Find("BG").GetComponent<Image>().color = Color.black;
            tooltip.Find("Tmp").GetComponent<TextMeshProUGUI>().color = Color.white;
            for (int i = 0; i < optionsMenu.transform.childCount; i++)
            {
                Transform child = optionsMenu.transform.GetChild(i);
                foreach (MenuToggle menuToggle in child.GetComponentsInChildren<MenuToggle>())
                {
                    menuToggle.gameObject.transform.Find("Box").GetComponent<Image>().sprite = AssetsHelper.TextureFromFile("Box.png").ToSprite();
                }
                foreach (TextMeshProUGUI tmp in child.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    tmp.color = Color.white;
                }
                foreach (TextLocalizer tmp in child.GetComponentsInChildren<TextLocalizer>())
                {
                    try
                    {
                        tmp.GetComponent<TextMeshProUGUI>().color = Color.white;
                    }
                    catch { }
                }
            }
        }
        public static void NewMenu()
        {
            AssetsHelper.LoadAsset<Transform>("NameEntry").Find("Image").GetComponent<Image>().color = Color.black;
            AssetsHelper.LoadAsset<Transform>("NameEntry").Find("BG").GetComponent<Image>().sprite = AssetsHelper.TextureFromFile("NameMenu.png").ToSprite();
            AssetsHelper.LoadAsset<Transform>("NameEntry").Find("NameButton0").GetComponent<NameButton>().text.color = Color.white;
            for (int i = 1; i <= 7; i++)
            {
                AssetsHelper.LoadAsset<Transform>("NameEntry").Find("NameButton0 (" + i + ")").GetComponent<NameButton>().text.color = Color.white;
            }
            Transform t = (from x in Resources.FindObjectsOfTypeAll<Transform>()
                          where x.name.ToLower() == "about" && x.childCount > 0
                          select x).First();
            t.Find("BG").GetComponent<Image>().color = Color.black;
            foreach (TextMeshProUGUI tmp in t.GetComponentsInChildren<TextMeshProUGUI>())
            {
                tmp.color = Color.white;
            }
        }
        void Start()
        {
            List<string> transforms = new List<string>() { "PickEndlessMap", "PickChallenge", "PickFieldTrip", "EndlessMapOverview", "FieldTripOverview", "" };
            transform.Find("BG").GetComponent<Image>().color = Color.black;
            foreach (TextMeshProUGUI tmp in transform.GetComponentsInChildren<TextMeshProUGUI>())
            {
                tmp.color = Color.white;
            }
            foreach (string s in transforms)
            {
                Transform t = AssetsHelper.LoadAsset<Transform>(s);
                t.Find("BG").GetComponent<Image>().color = Color.black;
                foreach (TextMeshProUGUI tmp in t.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    tmp.color = Color.white;
                }
                if (s == "EndlessMapOverview")
                {
                    for (int i = 1; i <= 10; i++) 
                    {
                        t.Find("HighScore" + i).Find("Button").GetComponent<Image>().color = Color.black;
                    }
                }
            }
        }
    }
}
