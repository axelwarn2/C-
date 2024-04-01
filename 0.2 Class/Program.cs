using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Class
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var a = Superhero.GetHero("Бетмен");
            var a2 = Superhero.GetHero("Бетмен");
            var a3 = Superhero.GetHero("Бетмен");
            var a4 = Superhero.GetHero("Супермен");

         

            Console.WriteLine(a == a5);

        }
    }
    public class Superhero
    {
        public static Dictionary<string, Superhero> hero = new Dictionary<string, Superhero>();

        public static string Name { get; private set; }

        private Superhero(string name)
        {
            Name = name;
        }

        static public Superhero GetHero(string name)
        {
            if (!hero.ContainsKey(name))
            {
                hero[name] = new Superhero(name);
            }
            return hero[name];          
        }
    }
}
