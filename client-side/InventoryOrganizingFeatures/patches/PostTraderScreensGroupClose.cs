using Aki.Reflection.Patching;
using EFT.HandBook;
using EFT.InventoryLogic;
using EFT.UI;
using EFT.UI.DragAndDrop;
using HarmonyLib;
using InventoryOrganizingFeatures.Reflections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static InventoryOrganizingFeatures.Locker;
using static InventoryOrganizingFeatures.Organizer;
using static InventoryOrganizingFeatures.OrganizedContainer;
using static InventoryOrganizingFeatures.UserInterfaceElements;
using InventoryOrganizingFeatures.Reflections.Extensions;
using TMPro;
using BepInEx.Logging;
using Debug = UnityEngine.Debug;

namespace InventoryOrganizingFeatures
{
    internal class PostTraderScreensGroupClose : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(TraderScreensGroup), "Close");
        }

        [PatchPostfix]
        private static void PatchPostfix()
        {
            try
            {
                if (OrganizeButtonTrader == null) return;
                if (OrganizeButtonTrader.IsDestroyed()) return;

                OrganizeButtonTrader.gameObject.SetActive(false);
                GameObject.Destroy(OrganizeButtonTrader);

                // Might need it.
                //GameObject.DestroyImmediate(OrganizeButton);
                //OrganizeButton = null;
            }
            catch (Exception ex)
            {
                throw Plugin.ShowErrorNotif(ex);
            }
        }
    }
}