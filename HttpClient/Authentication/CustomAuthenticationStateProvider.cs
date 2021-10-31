using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using WebClient.Data.Implementations;
using WebClient.Models;

namespace WebClient.Authentication
{
	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly IJSRuntime _jsRuntime;
		private readonly RestClient _client;

		private User cachedUser;

		public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, RestClient client)
		{
			_jsRuntime = jsRuntime;
			_client = client;
			Console.WriteLine(_client);
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var identity = new ClaimsIdentity();
			if (cachedUser == null)
			{
				string userAsJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
				if (!string.IsNullOrEmpty(userAsJson))
				{
					User tmp = JsonSerializer.Deserialize<User>(userAsJson);
					ValidateLogin(tmp.Username, tmp.Password);
				}
			}
			else
			{
				identity = SetupClaimsForUser(cachedUser);
			}

			ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
			return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
		}

		public void ValidateLogin(string username, string password)
		{
			Console.WriteLine("Validating log in");
			if (string.IsNullOrEmpty(username))
				throw new Exception("Enter username");
			if (string.IsNullOrEmpty(password))
				throw new Exception("Enter password");
			Console.WriteLine("Possible credentials, checking account");
			ClaimsIdentity identity = new ClaimsIdentity();
			try
			{
				User user = _client.LoginAsync(new User(username, password, 0)).Result;
				identity = SetupClaimsForUser(user);
				string serialisedData = JsonSerializer.Serialize(user);
				_jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
				cachedUser = user;
			}
			catch (Exception e)
			{
				throw e;
			}

			NotifyAuthenticationStateChanged(
				Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
		}

		public void Logout()
		{
			cachedUser = null;
			var user = new ClaimsPrincipal(new ClaimsIdentity());
			_jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
		}

		private ClaimsIdentity SetupClaimsForUser(User user)
		{
			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Name, user.Username));
			claims.Add(new Claim("Level", user.SecurityLevel.ToString()));

			ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
			return identity;
		}
	}
}