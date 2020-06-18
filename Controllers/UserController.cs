using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Service;

namespace UserManagement.Controllers
{
	public class UserController : Controller
	{

		// GET: User
		public async Task<ActionResult> IndexAsync()
		{
			UserService userService = new UserService();
			var UserList = await userService.GetTblUsersAsync();
			return View(UserList);
		}

		// GET: User/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: User/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: User/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(IFormCollection collection)
		{
			try
			{
				TblUser user = new TblUser();
				user.Name = collection["Name"];
				user.Email = collection["Email"];
				user.DateOfBirth = Convert.ToDateTime(collection["DateOfBirth"]);
				user.Address = collection["Address"];
				UserService userService = new UserService();
				var result = await userService.CreateNewUser(user);

				if (result)
					return RedirectToAction(nameof(IndexAsync));
				else
					return View();
			}
			catch
			{
				return View();
			}
		}

		// GET: User/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			UserService userService = new UserService();
			var user = await userService.GetUser(id);
			return View(user);
		}

		// POST: User/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
		{
			try
			{
				TblUser user = new TblUser();
				user.Id = id;
				user.Name = collection["Name"];
				user.Email = collection["Email"];
				user.DateOfBirth = Convert.ToDateTime(collection["DateOfBirth"]);
				user.Address = collection["Address"];
				UserService userService = new UserService();
				var result = await userService.UpdateUser(user);
				// TODO: Add update logic here

				return RedirectToAction(nameof(IndexAsync));
			}
			catch
			{
				return View();
			}
		}

		// GET: User/Delete/5
		public async Task<ActionResult> Delete(int id)
		{
			UserService userService = new UserService();
			var user = await userService.GetUser(id);
			return View(user);
		}

		// POST: User/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, IFormCollection collection)
		{
			try
			{
				UserService userService = new UserService();
				var user = await userService.DeleteUser(id);
				return RedirectToAction(nameof(IndexAsync));
			}
			catch
			{
				return View();
			}
		}
	}
}