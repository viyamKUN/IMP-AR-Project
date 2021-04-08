using System.Collections;
using System.Collections.Generic;

///<sumary>크리쳐 관련</summary>
namespace Creatures
{
    [System.Serializable]
    public class MyCreature
    {
        public int ID;
        public int Count;
        public float Friendship;
        public MyCreature(int id, int count, float friendship)
        {
            this.ID = id;
            this.Count = count;
            this.Friendship = friendship;
        }
    }
    public class Creature
    {
        public int ID;
        public string Name;
        public string Description;
        public List<int> FavoriteItemIDs;
        public int DroppedItemID;
        public Creature(int id, string name, string desc, List<int> favorite, int dropped)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.FavoriteItemIDs = favorite;
            this.DroppedItemID = dropped;
        }
    }
}
