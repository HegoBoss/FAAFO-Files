using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pra.Freezer.Keeper.Core
{
    public class Product
    {
        // Variabelen van de eigenschappen?
        private string Name;
        private int MaxStorageMonths;
        private DateTime FreezerDate;
        private int Quantity;

        //natuurlijk ga nie werken heb geen props gemaakt stupid me

        public string IsName
        {
            get { return Name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("gelieve iets in te vullen dank u!");
                }
                Name = value;
            }
        }


        public int IsMaxStorageMonths
        {
            get { return MaxStorageMonths; }
            private set
            {
                if (value < 1) MaxStorageMonths = 1;
                else if (value > 12) MaxStorageMonths = 12;
                else MaxStorageMonths = value;
            }
        }

        public DateTime IsFreezerDate
        {
            get { return FreezerDate; }
            private set
            {
                if (value > DateTime.Now)
                {
                    FreezerDate = DateTime.Now;
                }
                else
                {
                    FreezerDate = value;
                }
            }
        }

        public int IsQuantity
        {
            get { return Quantity; }
            private set
            {
                if (value < 0) Quantity = 0;
                else Quantity = value;
            }
        }


        // Constructor
        public Product(string name, int maxStorageMonths, DateTime freezerDate, int quantity)
        {
            this.Name = name;
            this.MaxStorageMonths = maxStorageMonths;
            this.FreezerDate = freezerDate;
            this.Quantity = quantity;

            // telaat gelezen blijkbaar moet er manier zijn om Zorg ervoor dat bij het aanmaken van een nieuw product `Quantity` **minstens 1** is. Indien de waarde kleiner is dan 1, pas je deze aan naar 1.
            if (quantity < 1)
            {
                this.Quantity = 1;
            }
            else
            {
                this.Quantity = quantity;
            }
        }

        // Readonly, automatisch berekend
        public DateTime BestBefore
        {
            get {return FreezerDate.AddMonths(MaxStorageMonths);}
        }

        // methodes

        public bool UseItem()
        {
            Quantity--;
            return Quantity > 0;
        }
        public bool IsSafeToUse(DateTime date)
        {
            return date <= BestBefore;                                                                  
        }
        public override string ToString()
        {
            return $"{Name} (Quantity: {Quantity}, Freezer Date: {FreezerDate.ToShortDateString()}, Best Before: {BestBefore.ToShortDateString()})";
        }
    }
}
