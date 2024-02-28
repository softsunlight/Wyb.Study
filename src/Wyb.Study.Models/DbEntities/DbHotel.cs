using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Models.DbEntities
{
    [SugarTable("tb_hotel")]
    public class DbHotel
    {
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "address")]
        public string Address { get; set; }

        [SugarColumn(ColumnName = "price")]
        public int Price { get; set; }

        [SugarColumn(ColumnName = "score")]
        public int Score { get; set; }

        [SugarColumn(ColumnName = "brand")]
        public string Brand { get; set; }

        [SugarColumn(ColumnName = "city")]
        public string City { get; set; }

        [SugarColumn(ColumnName = "star_name")]
        public string StarName { get; set; }

        [SugarColumn(ColumnName = "business")]
        public string Business { get; set; }

        [SugarColumn(ColumnName = "longitude")]
        public string Longitude { get; set; }

        [SugarColumn(ColumnName = "latitude")]
        public string Latitude { get; set; }

        [SugarColumn(ColumnName = "pic")]
        public string Pic { get; set; }
    }
}
