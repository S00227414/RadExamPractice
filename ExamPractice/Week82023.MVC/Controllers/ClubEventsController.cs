using ClubModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.WebAPIClient;

namespace Week82023.MVC.Controllers
{
    public class ClubEventsController : Controller
    {
        ClubsContext db = new ClubsContext();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AllClubDetails(string ClubName = null)
        {
            // Activity Tracker
            ActivityAPIClient.Track(StudentID: "S00227414", StudentName: "Levi Gilmartin",
                activityName: "RAD301 Week 8 Lab 2023", Task: "Implementing All Club Detail Filter");

            ViewBag.cname = ClubName;
            var fullClub = db.Clubs.Include(c => c.clubEvents)
                .Where(c => ClubName == null ||  c.ClubName.StartsWith(ClubName))
                .ToListAsync();
            return View(await fullClub);

        }
    }
}
