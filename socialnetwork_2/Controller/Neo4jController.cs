using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace socialnetwork_2
{
    class Neo4jController
    {
        internal static async Task<GraphClient> GetClient()
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "qwerty");
            return client;
        }
        internal static async void RegisterUser(SingUpUser singUpUser)
        {
            var client = await GetClient();
            await client.ConnectAsync();
            var user = client.Cypher
                .Create("(u:User {email:{userEmail}})")
                .WithParam("userEmail", singUpUser.tBoxRegEmail.Text)
                .Return((u) => u.As<User>())
                .Results;
            var t = client.ToString();

        }

        internal static async void GetDistance(UserPageStream userPageStream)
        {
            var window = ViewsController.GetParentWindow(userPageStream);
            var client = await GetClient();
            int distance = 0;
            await client.ConnectAsync();
            User user1 = new User
            {
                Email = window.User.Email
            };
            User user2 = new User
            {
                Email = userPageStream.User.Email
            };
            if (user1.Email == user2.Email)
            {
                distance = 0;
            }
            else
            {
                var path = client.Cypher
               .Match("path = shortestPath((u1:User)-[:Follows*]->(u2:User))")
               .Where((User u1) => u1.Email == user1.Email)
               .AndWhere((User u2) => u2.Email == user2.Email)
               .Return(() => Return.As<IEnumerable<string>>("[n IN nodes(path) | n.email]"));

                if (!(path.Results.SingleOrDefault() is null))
                {
                    distance = path.Results.Single().ToList().Count() - 1;
                }
                else
                {
                    distance = -1;
                }
            }


            ViewsController.ShowDistance(userPageStream, distance);
        }

        internal static async void ClickFollow(UserPageStream userPageStream, int index)
        {
            var client = await GetClient();
            var window = ViewsController.GetParentWindow(userPageStream);
            await client.ConnectAsync();
            if (index == -2)
            {
                client.Cypher
                    .Match("(u1:User {email:{user1email}})")
                    .Match("(u2:User {email:{user2email}})")
                    .WithParam("user1email", window.User.Email)
                    .WithParam("user2email", userPageStream.User.Email)
                    .Create("(u1)-[:Follows]->(u2)")
                    .ExecuteWithoutResults();
                ViewsController.ShowDistance(userPageStream, 1);

                //userPageStream.bFollow.Content = "Following";
                //userPageStream.bFollow.Tag = -1;
            }
            else if (index == 1)
            {
                client.Cypher
                       .Match("(u1:User {email:{user1email}})")
                       .Match("(u2:User {email:{user2email}})")
                       .WithParam("user1email", window.User.Email)
                       .WithParam("user2email", userPageStream.User.Email)
                       .Create("(u1)-[:Follows]->(u2)")
                       .ExecuteWithoutResults();
                ViewsController.ShowDistance(userPageStream, 1);

                //userPageStream.bFollow.Content = "Following";
                //userPageStream.bFollow.Tag = 2;
            }
            else if (index == 2)
            {
                client.Cypher
                     .Match("(:User {email:{user1email} })-[r:Follows]->(:User {email:{user2email}})")
                     .WithParam("user1email", window.User.Email)
                     .WithParam("user2email", userPageStream.User.Email)
                     .Delete("r")
                     .ExecuteWithoutResults();

                GetDistance(userPageStream);
                //userPageStream.bFollow.Content = "Follow Back";
                //userPageStream.bFollow.Tag = 1;
            }
            else if (index == -1)
            {
                client.Cypher
                      .Match("(:User {email:{user1email} })-[r:Follows]->(:User {email:{user2email}})")
                      .WithParam("user1email", window.User.Email)
                      .WithParam("user2email", userPageStream.User.Email)
                      .Delete("r")
                      .ExecuteWithoutResults();

                GetDistance(userPageStream);
                //userPageStream.bFollow.Content = "Follow";
                //userPageStream.bFollow.Tag = -2;
            }
        }
    }
}
