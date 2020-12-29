namespace FTServer.Resources
{
    public class ItemTool
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public ItemUseType UseType { get; set; }
        public int MaxUse { get; set; }
        public ItemToolKind Kind { get; set; }
        public bool EnableParcel { get; set; }
    }
}
