using Elasticsearch.Net;
using Nest;
using SqlSugar;
using Wyb.Study.Models.DbEntities;
using Wyb.Study.Models.EsEntities;

namespace HotelTests
{
    public class HotelTest
    {
        private ElasticClient _elasticClient;
        private SqlSugarClient _sqlClient;


        [SetUp]
        public void Setup()
        {
            _elasticClient = new ElasticClient(new Uri("http://localhost:9200"));
            _sqlClient = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = "server=127.0.0.1;database=wyb_study;user=root;password=123456;"
            });
        }

        [Test]
        public void CreateHotelIndexTest()
        {
            var indexName = "hotel";
            var existResponse = _elasticClient.Indices.Exists(indexName);
            if (!existResponse.Exists)
            {
                var createIndexResponse = _elasticClient.Indices.Create(indexName, s =>
                    s.Map<EsHotel>(m =>
                        m.Properties(p =>
                            p
                            .Keyword(n =>
                                n.Name(n2 => n2.Id)
                               )
                              .Text(t => t
                                .Name(n2 => n2.Name)
                                .Analyzer("ik_max_word")
                                .CopyTo(c => c.Field("all"))
                              )
                               .Keyword(t => t
                                .Name(n2 => n2.Address)
                                .Index(false)
                              )
                                .Number(t => t
                                .Name(n2 => n2.Price)
                                .Type(NumberType.Integer)
                              )
                                .Number(t => t
                                .Name(n2 => n2.Score)
                                .Type(NumberType.Integer)
                              )
                                .Keyword(t => t
                                .Name(n2 => n2.Brand)
                                .CopyTo(c => c.Field("all"))
                              )
                                .Keyword(t => t
                                .Name(n2 => n2.City)
                                .CopyTo(c => c.Field("all"))
                              )
                                .Keyword(t => t
                                .Name(n2 => n2.StarName)
                              )
                                .Keyword(t => t
                                .Name(n2 => n2.Business)
                              )
                                .GeoPoint(t => t
                                .Name(n2 => n2.Location)
                              )
                                 .GeoPoint(t => t
                                .Name(n2 => n2.Pic)
                              )
                                 .Text(t => t
                                 .Name(n2 => n2.All)
                                 .Analyzer("ik_max_word")
                                 )
                        )
                    )
                );
                if (!createIndexResponse.ApiCall.Success)
                {
                    throw new Exception($"创建索引[{indexName}]失败，{createIndexResponse.OriginalException.Message}");
                }
            }
        }

        [Test]
        public void DeleteHotelIndexTest()
        {
            var indexName = "hotel";
            var deleteResponse = _elasticClient.Indices.Delete(indexName);
            Assert.IsTrue(deleteResponse.Acknowledged);
        }

        [Test]
        public void ExistsHotelIndexTest()
        {
            var indexName = "hotel";
            var response = _elasticClient.Indices.Exists(indexName);
            Assert.IsFalse(response.Exists);
        }

        [Test]
        public void AddDocumentTest()
        {
            var indexName = "hotel";
            var dbHotel = _sqlClient.Queryable<DbHotel>().Where(t => t.Id == 61083).First();
            EsHotel esHotel = new EsHotel(dbHotel);
            var response = _elasticClient.Index<EsHotel>(esHotel, s => s.Id(esHotel.Id).Index(indexName));
            Assert.IsTrue(response.ApiCall.Success);
        }

        [Test]
        public void GetDocumentTest()
        {
            var indexName = "hotel";
            var response = _elasticClient.Get<EsHotel>(61083, s => s.Index(indexName));
            Assert.IsTrue(response.ApiCall.Success);
            Assert.That(response.Source.Id, Is.EqualTo(61083));
        }

        [Test]
        public void DeleteDocumentTest()
        {
            var indexName = "hotel";
            var response = _elasticClient.Delete<EsHotel>(61083, s => s.Index(indexName));
            Assert.IsTrue(response.ApiCall.Success);
        }

        [Test]
        public void UpdateDocumentTest()
        {
            var indexName = "hotel";
            var response = _elasticClient.Update<EsHotel>(61083, s => s.Index(indexName)
            .Doc(new EsHotel()
            {
                Id = 61083,
                Price = 953,
                StarName = "四钻"
            }));
            Assert.IsTrue(response.ApiCall.Success);
        }

        [Test]
        public void BatchInsertDocumentTest()
        {
            var indexName = "hotel";
            var dbHotels = _sqlClient.Queryable<DbHotel>().ToList();
            List<EsHotel> esHotels = new List<EsHotel>();

            esHotels = dbHotels.Select(p => new EsHotel(p)).ToList();


            var response = _elasticClient.IndexMany(esHotels, indexName);

            Assert.IsTrue(response.ApiCall.Success);
        }
    }
}