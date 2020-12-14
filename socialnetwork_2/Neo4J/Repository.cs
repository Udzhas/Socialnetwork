﻿using Neo4jClient;
using socialnetwork_2.Controller.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2.Neo4J
{
    class Repository
    {
        private readonly IGraphClient _graphClient;

        public Repository()
        {
            _graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "socialnetwork");
            _graphClient.Connect();
        }

        public IEnumerable<Person> FriendsOfAFriend(Person person)
        {
            /*
                MATCH (p:Person)-[:KNOWS]-(friend)-[:KNOWS]-(foaf)
                WHERE p.name = {p1}
                AND NOT (p)-[:KNOWS]-(foaf)
                RETURN foaf
            */

            var query = _graphClient.Cypher
                .Match("(p:Person)-[:FOLLOW]->(friend)-[:FOLLOW]->(foaf)")
                .Where((Person p) => p.NickName == person.NickName)
                .AndWhere("NOT (p)-[:FOLLOW]-(foaf)")
                .Return(foaf => foaf.As<Person>());
            return query.Results;
        }

        public IEnumerable<Person> CommonFriends(Person person1, Person person2)
        {

            var query = _graphClient.Cypher
                .Match("(p:Person)-[:FOLLOW]->(friend)-[:FOLLOW]->(foaf:Person)")
                .Where((Person p) => p.NickName == person1.NickName)
                .AndWhere((Person foaf) => foaf.NickName == person2.NickName)
                .Return(friend => friend.As<Person>());

            return query.Results;
        }

        public IEnumerable<string> ConnectingPaths(Person person1, Person person2)
        {
          

            var query = _graphClient.Cypher
                .Match("path = shortestPath((p1:Person)-[:FOLLOW*..6]->(p2:Person))")
                .Where((Person p1) => p1.NickName == person1.NickName)
                .AndWhere((Person p2) => p2.NickName == person2.NickName)
                .Return(() => Return.As<IEnumerable<string>>("[n IN nodes(path) | n.nickname]"));

            return query.Results.Single();
        }

        public void CreatePerson(Person person)
        {
            _graphClient.Cypher
                .Create("(np:Person {newPerson})")
                .WithParam("newPerson", person)
                .ExecuteWithoutResults();
        }
        public void CreatRelationShip(Person whoStartFollow, Person whomFollow)
        {
            _graphClient.Cypher
                .Match("(p1:Person {nickname: {p1NickName}})", "(p2:Person {nickname: {p2NickName}})")
                .WithParam("p1NickName", whoStartFollow.NickName)
                .WithParam("p2NickName", whomFollow.NickName)
                .Create("(p1)-[:FOLLOW]->(p2)")
                .ExecuteWithoutResults();
        }
        public void DeleteRelationShip(Person whoStopFollow, Person whomFollow)
        {
            _graphClient.Cypher
              .Match("(p1:Person {nickname: {p1NickName}})-[r:FOLLOW]->(p2:Person {nickname: {p2NickName}})")
              .WithParam("p1NickName", whoStopFollow.NickName)
              .WithParam("p2NickName", whomFollow.NickName)
              .Delete("r")
              .ExecuteWithoutResults();
        }
    }
}
