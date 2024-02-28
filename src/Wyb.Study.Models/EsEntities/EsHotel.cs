using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Models.DbEntities;

namespace Wyb.Study.Models.EsEntities
{
    public class EsHotel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Price { get; set; }

        public int Score { get; set; }

        public string Brand { get; set; }

        public string City { get; set; }

        public string StarName { get; set; }

        public string Business { get; set; }

        public string Location { get; set; }

        public string Pic { get; set; }

        public string All { get; set; }

        public EsHotel()
        {

        }

        public EsHotel(DbHotel hotel)
        {
            this.Id = hotel.Id;
            this.Name = hotel.Name;
            this.Address = hotel.Address;
            this.Price = hotel.Price;
            this.Score = hotel.Price;
            this.Brand = hotel.Brand;
            this.City = hotel.City;
            this.StarName = hotel.StarName;
            this.Business = hotel.Business;
            this.Location = hotel.Latitude + "," + hotel.Longitude;
            this.Pic = hotel.Pic;
            this.All = string.Empty;
        }

    }
}
