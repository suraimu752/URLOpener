using Dalamud.ContextMenu;
using Dalamud.Logging;
using System;
using System.Text.RegularExpressions;

namespace SamplePlugin;

public class ContextMenu : IDisposable
{
    public ContextMenu()
    {
        Enable();
    }

    public static void Enable()
    {
        Service.ContextMenu.OnOpenGameObjectContextMenu -= OnOpenContextMenu;
        Service.ContextMenu.OnOpenGameObjectContextMenu += OnOpenContextMenu;
    }

    public static void Disable()
    {
        Service.ContextMenu.OnOpenGameObjectContextMenu -= OnOpenContextMenu;
    }

    public void Dispose()
    {
        Disable();
        GC.SuppressFinalize(this);
    }

    private static bool IsPartyFinder(BaseContextMenuArgs args)
    {
        if (args.ParentAddonName == "LookingForGroupDetail")
        {
            return args.Text != null;
        }
        else return false;
    }

    private static void OnOpenContextMenu(GameObjectContextMenuOpenArgs args)
    {
        if (!IsPartyFinder(args)) return;

        args.AddCustomItem(new GameObjectContextMenuItem("Open URL", OpenURL));
    }

    private static void OpenURL(GameObjectContextMenuItemSelectedArgs args)
    {
        string detail = args.Text.ToString();
        Match url = Regex.Match(detail, @"(https://.*$|https://.*[ 　])");
        if (url.Success)
        {
            Dalamud.Utility.Util.OpenLink(url.Value);
        }
        else
        {
            PluginLog.Log("url not found.");
        }
    }
}
