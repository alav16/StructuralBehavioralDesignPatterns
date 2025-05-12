/*
You are building a 2D shooter game.
 Thousands of bullets are fired by players and enemies every second.
✅ Each bullet type shares:
Appearance (image/sprite)
Damage value
✅ Each bullet instance has unique:
Position (x, y)
Direction (angle)
Speed
You must use the Flyweight Pattern to avoid creating thousands of heavy objects.
 ✅ Share bullet types through a BulletTypeFactory.
✅ Store position, direction, speed externally.
🛠 Tasks:
Create an interface BulletType:
Intrinsic data: sprite name, damage value
Method render(x: number, y: number, direction: number): void
Create a concrete class ConcreteBulletType implementing BulletType.
Create a BulletTypeFactory:
Reuse ConcreteBulletType objects by sprite name + damage combination.
Create a Bullet class:
Holds extrinsic data:
Position (x, y)
Direction (angle)
Speed
Holds a reference to a BulletType. 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prob6Flyweight
{
    public interface IBulletType
    {
        void Render(int x, int y, int direction);
    }

    public class ConcreteBulletType : IBulletType
    {
        public string Name { get; }
        public int DamageValue { get; }

        public ConcreteBulletType(string name, int damageValue)
        {
            Name = name;
            DamageValue = damageValue;
        }
        public void Render(int x, int y, int direction)
        {
            Console.WriteLine($"{Name} at {x} {y}  {direction}  {DamageValue}");
        }
    }

    public class BulletTypeFactory
    {
        private Dictionary<string, IBulletType> _bulletTypes = new Dictionary<string, IBulletType> ();

        public IBulletType GetBulletType(string name, int damageValue)
        {
            string key = $"{name}_{damageValue}";
            if (!_bulletTypes.ContainsKey(key))
            {
                _bulletTypes[key] = new ConcreteBulletType (name, damageValue);
            }
            return _bulletTypes[key];
        }
    }


    public class Bullet
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public int Direction {  get; set; }
        public int Speed { get; set; }
        private IBulletType _bulletType;
        public Bullet(int x, int y, int direction, int speed, IBulletType bulletType)
        {
            X = x;
            Y = y;
            Direction = direction;
            Speed = speed;
            _bulletType = bulletType;
        }

        public void Render()
        {
            _bulletType.Render(X, Y, Direction);
        }

       
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new BulletTypeFactory();

            var bullets = new List<Bullet>
            {
                new Bullet(0, 0, 45, 10, factory.GetBulletType("YellowLaser", 10)),
                new Bullet(5, 2, 45, 10, factory.GetBulletType("YellowLaser", 10)),
                new Bullet(10, 5, 90, 8, factory.GetBulletType("RedFireball", 25)),
                new Bullet(15, 10, 90, 8, factory.GetBulletType("RedFireball", 25)),
                new Bullet(20, 20, 30, 12, factory.GetBulletType("BlueIceShard", 15)),
            };
           
            foreach (Bullet b in bullets)
            {
                b.Render();
            }
        }
    }
}
