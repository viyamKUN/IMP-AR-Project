namespace Items
{
    public enum ItemType
    {
        None, Catch, Food
    }
    public class Item
    {
        public int ID;
        public string Name;
        public string Description;
        public int Price;
        public ItemType MyType;
        public Item(int id, string name, string desc, int price, string typeLetter)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.Price = price;
            switch (typeLetter)
            {
                case "C":
                    this.MyType = ItemType.Catch;
                    break;
                case "F":
                    this.MyType = ItemType.Food;
                    break;
                default:
                    this.MyType = ItemType.None;
                    break;
            }
        }
    }
}
