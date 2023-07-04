using Data;
using TinyMessenger;
using UI.Cards;

namespace Gameplay.GameEvents
{
    public class GameConfigLoaded : GenericTinyMessage<GameConfig>
    {
        public GameConfigLoaded(object sender, GameConfig content) : base(sender, content)
        {
        }
    }
    
    
    public class CardSpawnMessage : GenericTinyMessage<CardItemData>
    {
        public CardSpawnMessage(object sender, CardItemData content) : base(sender, content)
        {
        }
    }


    public class CardHasSpawnedMessage : TinyMessageBase
    {
        public CardHasSpawnedMessage(object sender) : base(sender)
        {
        }
    }


    public class DistributedData
    {
        public enum Type
        {
            TimeIsOut,
            Eatable,
            Uneatable
        }
        
        
        public CardItemData CardData;
        public Type DistributedType;
    }

    
    public class CardDistributedMessage : GenericTinyMessage<DistributedData>
    {
        public CardDistributedMessage(object sender, DistributedData toEatable) : base(sender, toEatable)
        {
        }
    }
    
    
    public class TimerEndMessage : TinyMessageBase
    {
        public TimerEndMessage(object sender) : base(sender)
        {
        }
    }


    public class ProcessNextItem : TinyMessageBase
    {
        public ProcessNextItem(object sender) : base(sender)
        {
        }
    }


    public class CardUnloadMessage : GenericTinyMessage<CardItem>
    {
        public CardUnloadMessage(object sender, CardItem content) : base(sender, content)
        {
        }
    }


    public class PlayerDataDelta
    {
        public int Lives = 0;
        public int Score = 0;
    }


    public class PlayerDataUpdateMessage : GenericTinyMessage<PlayerDataDelta>
    {
        public PlayerDataUpdateMessage(object sender, PlayerDataDelta content) : base(sender, content)
        {
        }
    }


    public class EndGameMessage : TinyMessageBase
    {
        public EndGameMessage(object sender) : base(sender)
        {
        }
    }


    public class RestartGameMessage : TinyMessageBase
    {
        public RestartGameMessage(object sender) : base(sender)
        {
        }
    }
}
