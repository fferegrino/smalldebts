using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.DataAccess;
using Smalldebts.Core.UI.Services;
using Smalldebts.Core.UI.Views;
using Xamarin.Forms;

namespace Smalldebts.Core.UI
{
	public partial class App : Application
	{


		public async Task LogOut()
		{
			var _secureStorage = DependencyService.Get<ISecureStorage>();
			var _serviceClient = SmalldebtsManager.DefaultManager;
			_secureStorage.Delete(Constants.UserId);
			_secureStorage.Delete(Constants.Token);
			_secureStorage.Delete(Constants.TokenExpirationDate);
			_secureStorage.Delete(Constants.UserEmail);
			_secureStorage.Delete(Constants.UserPassword);
			_serviceClient.SetCredentials(null, null);
			await _serviceClient.CurrentClient.LogoutAsync();
		}

		public async Task Auth(string email, string password)
		{
			var secureStorage = DependencyService.Get<ISecureStorage>();
			var _serviceClient = SmalldebtsManager.DefaultManager;
			var token = await _serviceClient.GetAuthenticationToken(email, password);

			_serviceClient.SetCredentials(token.Guid, token.AccessToken);

			secureStorage.Store(Constants.UserId, token.Guid);
			secureStorage.Store(Constants.Token, token.AccessToken);

			secureStorage.Store(Constants.TokenExpirationDate,
					 token.Expires.Ticks.ToString());
			secureStorage.Store(Constants.UserEmail, email);
			secureStorage.Store(Constants.UserPassword, password);
		}

		public async Task<bool> AutoAuthenticate()
		{
			try
			{
				var _secureStorage = DependencyService.Get<ISecureStorage>();
				if (_secureStorage.Contains(Constants.UserId)
					  && _secureStorage.Contains(Constants.Token)
					  && _secureStorage.Contains(Constants.TokenExpirationDate)
					  && _secureStorage.Contains(Constants.UserEmail)
					  && _secureStorage.Contains(Constants.UserPassword))
				{
					var expirationDateTicks
						= _secureStorage.Retrieve(Constants.TokenExpirationDate);
					DateTime expirationDate
						= new DateTime(long.Parse(expirationDateTicks));

					if (expirationDate < DateTime.Now)
					{
						// you can also check first if email and password exists
						var email = _secureStorage.Retrieve(Constants.UserEmail);
						var password = _secureStorage.Retrieve(Constants.UserPassword);

						await Auth(email, password);
					}
					else
					{
						var id = _secureStorage.Retrieve(Constants.UserId);
						var token = _secureStorage.Retrieve(Constants.Token);

						SmalldebtsManager.DefaultManager.SetCredentials(id, token);
					}
					var user = await SmalldebtsManager.DefaultManager.Me();
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}

		}
	}
}