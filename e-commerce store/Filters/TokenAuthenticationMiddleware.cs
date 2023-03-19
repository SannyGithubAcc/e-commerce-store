using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class TokenAuthenticationFilter : IAsyncAuthorizationFilter
{
    private readonly string _keyVaultUrl;
    private readonly string _secretName;

    public TokenAuthenticationFilter(string keyVaultUrl, string secretName)
    {
        _keyVaultUrl = keyVaultUrl;
        _secretName = secretName;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var tokenKey = context.HttpContext.Request.Headers["Authorization"][0].Split(' ')[1];
        var tokenSecret = await GetTokenSecretAsync();
       

        if (tokenKey == tokenSecret)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }

    private async Task<string> GetTokenSecretAsync()
    {
        var client = new SecretClient(new Uri(_keyVaultUrl), new DefaultAzureCredential());
        var secret = await client.GetSecretAsync(_secretName);
        return secret.Value.Value;
    }

   
}

