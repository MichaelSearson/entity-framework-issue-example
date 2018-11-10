using EntityFrameworkIssueExample.Entities;
using EntityFrameworkIssueExample.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkIssueExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly EntityFrameworkDbContext _dbContext;

        public HomeController(EntityFrameworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // Add some data to the database - pretend we're responding to some user action.
            InitialiseDatabase();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void InitialiseDatabase()
        {
            var systemAccount = new Account
            {
                AccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                AddedByAccountId = null,
                AddedOnUtc = DateTime.UtcNow,
                ModifiedByAccountId = null,
                ModifiedOnUtc = DateTime.UtcNow
            };

            var otherAccounts = new List<Account>
            {
                new Account
                {
                    AccountId = Guid.Parse("015b76fc-2833-45d9-85a7-ab1c389c1c11"),
                    AddedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    AddedOnUtc = DateTime.UtcNow,
                    ModifiedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    ModifiedOnUtc = DateTime.UtcNow
                },
                new Account
                {
                    AccountId = Guid.Parse("538ee0dd-531a-41c6-8414-0769ec5990d8"),
                    AddedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    AddedOnUtc = DateTime.UtcNow,
                    ModifiedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    ModifiedOnUtc = DateTime.UtcNow
                },
                new Account
                {
                    AccountId = Guid.Parse("8288d9ac-fbce-417e-89ef-82266b284b78"),
                    AddedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    AddedOnUtc = DateTime.UtcNow,
                    ModifiedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    ModifiedOnUtc = DateTime.UtcNow
                },
                new Account
                {
                    AccountId = Guid.Parse("4bcfe9f8-e4a5-49f0-b6ee-44871632a903"),
                    AddedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    AddedOnUtc = DateTime.UtcNow,
                    ModifiedByAccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
                    ModifiedOnUtc = DateTime.UtcNow
                }
            };

            _dbContext.Add(systemAccount);
            _dbContext.AddRange(otherAccounts);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Just checking to see if anything was being raised.
            }            
        }
    }
}