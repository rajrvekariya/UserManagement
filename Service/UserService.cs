using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Service
{
	public class UserService
	{
		string Baseurl = "https://localhost:44377/api/User/";
		public async Task<List<TblUser>> GetTblUsersAsync()
		{
			List<TblUser> UserList = new List<TblUser>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Baseurl);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage Res = await client.GetAsync("GetUsers");

				if (Res.IsSuccessStatusCode)
				{
					var UserResponse = Res.Content.ReadAsStringAsync().Result;
					UserList = JsonConvert.DeserializeObject<List<TblUser>>(UserResponse);
				}
			}
			return UserList;
		}

		public async Task<TblUser> GetUser(int Id)
		{
			var result = new TblUser();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Baseurl);
				client.DefaultRequestHeaders.Clear();

				HttpResponseMessage Res = await client.GetAsync("GetUser?userID=" + Id);

				if (Res.IsSuccessStatusCode)
				{
					var UserResponse = Res.Content.ReadAsStringAsync().Result;
					result = JsonConvert.DeserializeObject<TblUser>(UserResponse);
				}
			}
			return result;
		}

		public async Task<bool> CreateNewUser(TblUser user)
		{
			var result = false;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Baseurl);
				client.DefaultRequestHeaders.Clear();

				HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

				HttpResponseMessage Res = await client.PostAsync("AddUser", content);

				if (Res.IsSuccessStatusCode)
				{
					result = true;
				}
			}
			return result;
		}

		public async Task<bool> UpdateUser(TblUser user)
		{
			var result = false;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Baseurl);
				client.DefaultRequestHeaders.Clear();

				HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

				HttpResponseMessage Res = await client.PostAsync("UpdateUser", content);

				if (Res.IsSuccessStatusCode)
				{
					result = true;
				}
			}
			return result;
		}

		public async Task<bool> DeleteUser(int Id)
		{
			var result = false;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Baseurl);
				client.DefaultRequestHeaders.Clear();

				HttpContent content = new StringContent(JsonConvert.SerializeObject(Id), Encoding.UTF8, "application/json");

				HttpResponseMessage Res = await client.PostAsync("DeleteUser", content);

				if (Res.IsSuccessStatusCode)
				{
					result = true;
				}
			}
			return result;
		}
	}
}