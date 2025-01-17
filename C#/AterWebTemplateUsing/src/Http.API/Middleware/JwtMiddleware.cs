﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Http.API.Middleware;
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RedisService _redis;
    public JwtMiddleware(RequestDelegate next, RedisService redis)
    {
        _next = next;
        _redis = redis;
    }

    public async Task Invoke(HttpContext context)
    {
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        JwtSecurityTokenHandler tokenHandler = new();

        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
        string id = jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        if (id != null)
        {
            bool? isLogin = _redis.GetValue<bool?>(id);
            if (isLogin.HasValue)
            {
                await _next(context);
            }
        }
        context.Response.StatusCode = 401;
        await context.Response.CompleteAsync();
    }
}