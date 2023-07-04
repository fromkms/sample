using TinyMessenger;

namespace Gameplay.GameEvents
{
    public static class EventBus
    {
        public static ITinyMessengerHub Hub { get; } = new TinyMessengerHub();
    }
}
