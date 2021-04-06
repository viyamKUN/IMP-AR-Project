using System.Collections;
using System.Collections.Generic;

///<sumary>크리쳐 관련</summary>
namespace Creatures
{
    public class Creature
    {
        public int ID;
        public string Name;
        public string Discription;
        public List<int> FavoriteItemIDs;
        public int DroppedItemID;
        public Creature(int id, string name, string disc, List<int> favorite, int dropped)
        {
            this.ID = id;
            this.Name = name;
            this.Discription = disc;
            this.FavoriteItemIDs = favorite;
            this.DroppedItemID = dropped;
        }
    }
}
