using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter {
    class ItemPoint : IComparable{
        public Item Item;
        public bool IsStart;
        public float Value;

        public ItemPoint (Item Item, bool IsStart, float Value) {
            this.Item = Item;
            this.IsStart = IsStart;
            this.Value = Value;
        }

        public int CompareTo(object obj) {
            ItemPoint itemPoint = (ItemPoint) obj;
            if (this.Value < itemPoint.Value) return -1;
            else if (this.Value == itemPoint.Value) return 0;
            else if (this.Value > itemPoint.Value) return 1;
            throw new ArgumentException();
        }
    }
    class Item {
        public ItemPoint Start;
        public ItemPoint End;
        public GameObject gameObject;

        public Item (GameObject gameObject) {
            this.Start = new ItemPoint (this, true, gameObject.Collider.X);
            this.End = new ItemPoint(this, false, gameObject.Collider.X + gameObject.Collider.Width);
            this.gameObject = gameObject;
        }
    }
    public class Axis1D {
        Dictionary<GameObject, Item> items = new Dictionary<GameObject, Item>();
        List<ItemPoint> endPoints = new List<ItemPoint>();
        public void AddGameObject(GameObject gameObject) {
            var item = new Item(gameObject);

            endPoints.Add(item.Start);
            endPoints.Add(item.End);
            
            endPoints.Sort();
        }
        public void UpdateGameObject(GameObject gameObject) {
            var item = items[gameObject];
            item.Start.Value = gameObject.Collider.X;
            item.End.Value = gameObject.Collider.X + gameObject.Collider.Width;

            endPoints.Sort();
        }
        public IEnumerable<GameObject> QueryRange(float start, float end)
        {
            List<GameObject> itemsInRange = new List<GameObject>();
            foreach(var point in endPoints)
            {
                if (point.Value > end) break;

                if(point.IsStart)
                    itemsInRange.Add(point.Item.gameObject);
                else if(point.Value < start)
                    itemsInRange.Remove(point.Item.gameObject);

            }
            return itemsInRange;
        }

        public void Clear() {
            items.Clear();
            endPoints.Clear();
        }
    }
}

