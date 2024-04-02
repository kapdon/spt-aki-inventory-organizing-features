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
    internal class PostEditTagWindowShow : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EditTagWindow), "Show", new Type[] { typeof(TagComponent), typeof(Action), typeof(Action), typeof(Action<string, int>) });
        }

        [PatchPrefix]
        private static void PatchPrefix(ref EditTagWindow __instance, ref DefaultUIButton ____saveButtonSpawner, ValidationInputField ____tagInput)
        {
            try
            {
                ____tagInput.characterLimit = 256;
                ____saveButtonSpawner.OnClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                {
                    try
                    {
                        string notifMsg = "";
                        if (IsSortLocked(____tagInput.text)) notifMsg += "This item is Sort Locked.";
                        if (IsMoveLocked(____tagInput.text))
                        {
                            if (notifMsg.Length > 0) notifMsg += "\n";
                            notifMsg += "This item is Move Locked.";
                        }
                        if (IsOrganized(____tagInput.text))
                        {
                            if (notifMsg.Length > 0) notifMsg += "\n";
                            // Add pretty notification output
                            var orgParams = ParseOrganizeParams(____tagInput.text);
                            var categoryParams = GetCategoryParams(orgParams);
                            var nameParams = GetNameParams(orgParams);

                            notifMsg += "This item's tag has following organize params:";
                            if (HasOrderParam(orgParams))
                            {
                                notifMsg += $"\n  -  Order #{GetOrderParam(orgParams).GetValueOrDefault()}";
                            }
                            if (HasParamDefault(orgParams))
                            {
                                notifMsg += $"\n  -  Category: default container categories";
                            }
                            else if (categoryParams.Length > 0)
                            {
                                notifMsg += $"\n  -  Category: {string.Join(", ", categoryParams)}";
                            }

                            if (nameParams.Length > 0)
                            {
                                notifMsg += $"\n  -  Name: {string.Join(", ", nameParams)}";
                            }

                            if (HasParamFoundInRaid(orgParams))
                            {
                                notifMsg += "\n  -  Only \"Found in raid\".";
                            }
                            else if (HasParamNotFoundInRaid(orgParams))
                            {
                                notifMsg += "\n  -  Only \"Not found in raid.\"";
                            }
                        }
                        if (notifMsg.Length > 0) NotificationManagerClass.DisplayMessageNotification(notifMsg);
                    }
                    catch (Exception ex)
                    {
                        throw Plugin.ShowErrorNotif(ex);
                    }
                }));
            }
            catch (Exception ex)
            {
                throw Plugin.ShowErrorNotif(ex);
            }
        }
    }
}
