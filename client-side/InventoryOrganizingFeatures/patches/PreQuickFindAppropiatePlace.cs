using System;
using System.Reflection;
using EFT.InventoryLogic;
using EFT.UI;
using HarmonyLib;
using Aki.Reflection.Patching;
using InventoryOrganizingFeatures.Reflections;

using static InventoryOrganizingFeatures.Locker;

namespace InventoryOrganizingFeatures
{
    internal class PreQuickFindAppropriatePlace : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(ItemUiContext), "QuickFindAppropriatePlace");
        }

        [PatchPrefix]
        private static void PatchPrefix(object itemContext, ref bool displayWarnings)
        {
            try
            {
                var item = itemContext.GetFieldValue<Item>("Item");
                // Don't display warnings if item IsMoveLocked
                if (IsMoveLocked(item)) displayWarnings = false;
            }
            catch (Exception ex)
            {
                throw Plugin.ShowErrorNotif(ex);
            }
        }
    }
}