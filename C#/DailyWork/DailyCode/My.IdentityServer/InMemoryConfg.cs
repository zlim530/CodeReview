using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace My.IdentityServer
{
    public static class InMemoryConfg
    {
        // 这个 Authorization Server 保护了哪些 API（资源）
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("my.core.api","My.Core API")
            };
        }

        // 哪些客户端 Client（应用）可以使用这个 Authorization Server
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                { 
                    // 定义客户端 Id
                    ClientId = "myvuejs",
                    // Client 用来获取 token
                    ClientSecrets = new[] { new Secret("secret".Sha256())},
                    // 这里使用的是通过用户米密码和 ClientCredentials 来获取 token 的方式.ClientCredentials 允许 Client 只使用 ClientSecrets 来获取 token.这比较适合那种没有用户参与的 api 动作
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    // 允许访问的 API 资源
                    AllowedScopes = new[] { "my.core.api"}
                }
            };
        }

        // 指定可以使用 Authorization Server 授权的 Users（用户）
        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "ZLim",
                    Password = "ZLim"
                }
            };
        }

    }
}
