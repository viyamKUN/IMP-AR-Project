namespace Items
{
    public class Item
    {
        public int ID;
        public string Name;
        public string Description;
        public Item(int id, string name, string desc)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
        }
    }
}
