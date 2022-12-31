using Dalamud.ContextMenu;
using Dalamud.IoC;
using Dalamud.Plugin;

namespace SamplePlugin
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "URLOpener";
        private readonly ContextMenu contextMenu;

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
        {
            pluginInterface.Create<Service>();

            Service.ContextMenu = new DalamudContextMenu();
            this.contextMenu = new ContextMenu();
        }

        public void Dispose()
        {
            Service.ContextMenu.Dispose();
            this.contextMenu.Dispose();
        }
    }
}
