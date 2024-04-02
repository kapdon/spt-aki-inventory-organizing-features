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
    internal class PostInitHanbook : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MenuTaskBar), "InitHandbook");
        }

        [PatchPostfix]
        private static void PatchPostfix(ref object handbook)
        {
            try
            {
                Organizer.Handbook ??= new Handbook(handbook);
                //Logger.LogMessage($"Elements: {Organizer.Handbook.NodesTree.Count}");
                //var search = Organizer.Handbook.FindNode("5751496424597720a27126da");
                //if (search != null)
                //{
                //    Logger.LogMessage($"Found: {search.Data.Name.Localized()}");
                //    Logger.LogMessage($"Categories: {string.Join(" > ", search.Category.Select(cat => cat.Localized()))}");
                //}
            }
            catch (Exception ex)
            {
                throw Plugin.ShowErrorNotif(ex);
            }
        }
    }
}
