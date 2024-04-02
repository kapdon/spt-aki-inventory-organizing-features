using Aki.Reflection.Patching;
using EFT.UI;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using static InventoryOrganizingFeatures.UserInterfaceElements;

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