using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEST
{
    class Program
    {
        public static Uri node;
        public static ConnectionSettings settings;
        public static ElasticClient client;
        static void Main(string[] args)
        {
            node = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(node).DefaultIndex("caracas");

            client = new ElasticClient(settings);

            //   var indexSettings = new IndexState();

            //   indexSettings.Settings = new IndexSettings();
            //   indexSettings.Settings.NumberOfReplicas = 1;
            //   indexSettings.Settings.NumberOfShards = 1;

            //  var response= client.CreateIndex("caracas", x => x.Mappings(ms => ms
            //      .Map<Post>(m => m
            //          .AutoMap<Post>()
            //      )
            //));
            Create();
            //Query();
            //QueryMatchPhrase();
            Console.ReadKey();
        }

        static void Create()
        {
            var post = new Post() {
                post_date=DateTime.Now,
                user_id=98,
                post_text= "maracay 2019"
            };

            var post2 = new Post()
            {
                post_date = DateTime.Now,
                user_id = 78,
                post_text = "valencia 2019"
            };

            var post3 = new Post()
            {
                post_date = DateTime.Now,
                user_id = 78,
                post_text = "maracaibo 2019"
            };

            client.Index(post,x =>x.Index("caracas"));
            client.Index(post2, x => x.Index("caracas"));
            client.Index(post3, x => x.Index("caracas"));
        }

        static void Query()
        {
            var result = client.Search<Post>(x=>x.Query(q=>q.Term(qq=>qq.post_text, "caracas")));
        }
        static void QueryMatchPhrase()
        {
            var result = client.Search<Post>(x => x.Query(q => q.MatchPhrase(m=>m.Field("post_text").Query("caracas"))));
        }

        static void QueryFilter()
        {
           // var result = client.Search<Post>(x => x.Query(q => q.Term(qq => qq.post_text, "caracas")).PostFilter(ff=>ff.Range(r=>r.Field(fi).gr)));

        }
    }
}
