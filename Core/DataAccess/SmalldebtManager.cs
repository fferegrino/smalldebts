/*
* To add Offline Sync Support:
*  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
*  2) Uncomment the #define OFFLINE_SYNC_ENABLED
*
* For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
*/
//#define OFFLINE_SYNC_ENABLED

using System;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.ItermediateObjects;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HttpLogger;
using ModernHttpClient;
using Newtonsoft.Json;
using Smalldebts.IntermediateObjects;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace Smalldebts.Core.UI.DataAccess
{
    public partial class SmalldebtsManager
    {
        static SmalldebtsManager defaultInstance = new SmalldebtsManager();
        MobileServiceClient client;

        //#if OFFLINE_SYNC_ENABLED
        //        IMobileServiceSyncTable<TodoItem> todoTable;
        //#else
        //            IMobileServiceTable<TodoItem> todoTable;
        //#endif

        const string offlineDbPath = @"localstore.db";

        private SmalldebtsManager()
        {
#if DEBUG
            this.client = new MobileServiceClient(Constants.ApplicationUrl, new HttpLoggingHandler(new HttpClientHandler()));
#else
            this.client = new MobileServiceClient(Constants.ApplicationUrl, new NativeMessageHandler());
#endif

            //#if OFFLINE_SYNC_ENABLED
            //            var store = new MobileServiceSQLiteStore(offlineDbPath);
            //            store.DefineTable<TodoItem>();

            //            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            //            this.client.SyncContext.InitializeAsync(store);

            //            this.todoTable = client.GetSyncTable<TodoItem>();
            //#else
            //                this.todoTable = client.GetTable<TodoItem>();
            //#endif
        }

        public static SmalldebtsManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

		public void SetCredentials(string id, string token)
		{
			MobileServiceUser user = new MobileServiceUser(id);
			user.MobileServiceAuthenticationToken = token;
			client.CurrentUser = user;
		}

        public async Task<Debt> AddMovementToDebt(Debt debt)
        {
            return await client.InvokeApiAsync<Debt, Debt>("debts", debt, HttpMethod.Put, null);
        }

        public async Task<Debt> AddNewDebt(Debt debt)
        {
            return await client.InvokeApiAsync<Debt, Debt>("debts", debt);
        }

        public async Task<List<Movement>> GetMovementsForDebt(string debtId)
        {
            return await client.InvokeApiAsync<List<Movement>>("movements/" + debtId, HttpMethod.Get, null);
        }

        public async Task<List<Debt>> GetDebts()
        {
            return await client.InvokeApiAsync<List<Debt>>("debts", HttpMethod.Get, null);
        }

        public async Task<SimpleUser> RegisterUser(string email, string username, string password, string passwordConfirmation)
        {
            var m = new AccountModelBinding()
            {
                Email = email,
                Username = username,
                Password = password,
                ConfirmPassword = passwordConfirmation
            };


            try
            {
                return await client.InvokeApiAsync<AccountModelBinding, SimpleUser>("accounts", m);
            }
            catch (MobileServiceInvalidOperationException exception)
            {
                if (exception.Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    /* If you have the latest framework */
                    string content = await exception.Response.Content.ReadAsStringAsync();

                    if (content.Contains("is already taken."))
                        throw new AccountAlreadyTakenException(exception);
                    if (content.Contains("least 6 characters"))
                        throw new PasswordTooShortException(exception);
                    else
                        throw;
                }
                else
                    throw;
            }
        }

		public async Task<SimpleUser> Me()
		{
			
				return await client.InvokeApiAsync<SimpleUser>("me", HttpMethod.Get, null);
		}

        public async Task<AuthenticationToken> GetAuthenticationToken(string email,
             string password)
        {
            try
            {
                // define request content
                HttpContent content = new StringContent(
            string.Format("username={0}&password={1}&grant_type=password",
                          email.ToLower(),
                          password));

                // set header
                content.Headers.ContentType
                = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                // invoke Api
                HttpResponseMessage response 
                    = await client.InvokeApiAsync("oauth/token",
                                                   content,
                                                   HttpMethod.Post,
                                                   null,
                                                   null);

                // read and parse token
                string flatToken = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthenticationToken>(flatToken);

            }
            catch (MobileServiceInvalidOperationException exception)
            {
				if (string.Equals(exception.Message, "invalid_grant"))
					throw new InvalidGrantException("Wrong credentails",
													exception);
				else if (exception.Response.StatusCode == HttpStatusCode.InternalServerError)
					throw new InternalServerException("Error en el servidor", exception);
				else
                    throw;
            }
        }
    }

    //public bool IsOfflineEnabled
    //{
    //    get { return todoTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<TodoItem>; }
    //}

    //            public async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync(bool syncItems = false)
    //            {
    //                try
    //                {
    //#if OFFLINE_SYNC_ENABLED
    //                if (syncItems)
    //                {
    //                    await this.SyncAsync();
    //                }
    //#endif
    //                    IEnumerable<TodoItem> items = await todoTable
    //                        .Where(todoItem => !todoItem.Done)
    //                        .ToEnumerableAsync();

    //                    return new ObservableCollection<TodoItem>(items);
    //                }
    //                catch (MobileServiceInvalidOperationException msioe)
    //                {
    //                    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
    //                }
    //                catch (Exception e)
    //                {
    //                    Debug.WriteLine(@"Sync error: {0}", e.Message);
    //                }
    //                return null;
    //            }

    //            public async Task SaveTaskAsync(TodoItem item)
    //            {
    //                if (item.Id == null)
    //                {
    //                    await todoTable.InsertAsync(item);
    //                }
    //                else
    //                {
    //                    await todoTable.UpdateAsync(item);
    //                }
    //            }

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.todoTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allTodoItems",
                    this.todoTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif


    public class AccountAlreadyTakenException : Exception
    {
        public AccountAlreadyTakenException(MobileServiceInvalidOperationException exception)
            : base("This account is already taken", exception)
        {
        }
    }

    public class PasswordTooShortException : Exception
    {
        public PasswordTooShortException(MobileServiceInvalidOperationException exception)
            : base("Your password is too short", exception)
        {
        }
    }

	public class InvalidGrantException : Exception
	{
		public InvalidGrantException(string message, MobileServiceInvalidOperationException exception)
			: base(message, exception)
		{
		}
	}

	public class InternalServerException : Exception
	{
		public InternalServerException(string message, MobileServiceInvalidOperationException exception)
			: base(message, exception)
		{
		}
	}
}
