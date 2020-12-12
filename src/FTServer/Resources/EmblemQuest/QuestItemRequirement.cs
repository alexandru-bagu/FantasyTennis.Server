namespace FTServer.Resources.EmblemQuest
{
    public class QuestItemRequirement
    {
        public int Item { get; private set; }
        public int Quantity { get; private set; }
        public QuestItemRequirement(int item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
