﻿using BepInEx;
using System;

namespace InventoryOrganizingFeatures
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static bool EnableLogs = false;
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            // Pull handbook from the init method.
            new PostInitHanbook().Enable();
            // Pre-load image from hideout button for organize button
            new PostMenuScreenInit().Enable();
            // Assign tag and show active tags when saving EditTagWindow.
            new PostEditTagWindowShow().Enable();
            // Sort lock
            new PreGridClassRemoveAll().Enable(); // Prevent Sorting
            // Move lock
            new PreItemViewOnPointerDown().Enable(); // Prevent Drag
            new PreItemViewOnBeginDrag().Enable(); // Prevent Drag
            new PostGetFailedProperty().Enable(); // Prevent quick move(Ctrl/Shift+Click)
            new PreQuickFindAppropriatePlace().Enable(); // Don't show warnings when item is Move Locked

            // Clone sort button and make it an organize button
            new PostGridSortPanelShow().Enable();
        }


        public static Exception ShowErrorNotif(Exception ex)
        {
            NotificationManagerClass.DisplayWarningNotification(
                $"InventoryOrganizingFeatures thew an exception. Perhaps version incompatibility? Exception: {ex.Message}",
                duration: EFT.Communications.ENotificationDurationType.Infinite
                );
            return ex;
        }
    }
}
