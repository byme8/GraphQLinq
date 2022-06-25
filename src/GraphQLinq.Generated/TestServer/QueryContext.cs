namespace TestServer
{
    using GraphQLinq;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public class QueryContext : GraphContext
    {
        public QueryContext(HttpClient httpClient) : base(httpClient)
        {
            InputVariablesDefinition["user"] = "($id: Int!)";
            InputVariablesDefinition["failUser"] = "()";
        }

        public GraphItemQuery<User> User(int id)
        {
            var parameterValues = new object[] { id };
            return BuildItemQuery<User>(parameterValues, "user");
        }

        public GraphItemQuery<User> FailUser()
        {
            var parameterValues = new object[] { };
            return BuildItemQuery<User>(parameterValues, "failUser");
        }
    }
}