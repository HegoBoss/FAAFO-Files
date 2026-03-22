using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.Freezer.Keeper.Core
{
    public class FreezerService
    {
        public List<Product> FrozenProducts { get; } //is dit readonly?
        public FreezerService(bool seedData = true)
        {
            FrozenProducts = new List<Product>();

            if (seedData)
            {
                SeedData();
            }
        }

        public void SeedData()
        {
            AddProduct(new Product("kipfilet", 6, DateTime.Now.AddMonths(-2), 4));//gaat dit werken?
            AddProduct(new Product("broccoli", 12, DateTime.Now.AddMonths(-1), 2));
            AddProduct(new Product("aardbeien", 8, DateTime.Now.AddMonths(-3), 1));
            AddProduct(new Product("vissticks", 10, DateTime.Now.AddMonths(-4), 3));
        }

        // ah s**t moet dit nu in een methode steken

        public void AddProduct(Product product)
        {
            if (product != null)
            {
                FrozenProducts.Add(product);
            }
            else
            {
                //herinner me nog een .ifnull exception maar vind niet hoe het werkt!
                throw new ArgumentException("Geen Null als argument!");
            }
        }

        public void RemoveProduct(Product product)
        {
            if (product != null && FrozenProducts.Contains(product))
            {
                FrozenProducts.Remove(product);
            }
            else
            {
                throw new ArgumentException("moet een item bevaten!");
            }
        }

        //De filter hoe de f**k begin je ier aan 
        //thx microsoft voor de documentatie
        public List<Product> Filter(string searchTerm, DateTime? safeDate)
        {
            //beginnen met alle producten i guess
            IEnumerable<Product> filteredList = FrozenProducts;

            //Filter op naam als er een zoekterm is
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filteredList = filteredList.Where(p => p.IsName.ToLower().Contains(searchTerm.ToLower()));
            }

            //Filter op houdbaarheidsdatum als er een datum is opgegeven
            if (safeDate.HasValue)
            {
                filteredList = filteredList.Where(p => p.IsSafeToUse(safeDate.Value));
            }

            return filteredList.ToList();
        }
    }
}
