using ClubModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tracker.WebAPIClient;

namespace Week82023.MVC.Controllers
{
    public class ClubEventsController : Controller
    {
        ClubsContext db = new ClubsContext();

        private List<SelectListItem> FillClubs()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            {
                var clubs = db.Clubs.ToList();
                foreach (var item in clubs)
                    items.Add(new SelectListItem()
                    {
                        Value = item.ClubId.ToString(),
                        Text = item.ClubName
                    });
            }
            return items;
        }
        public IActionResult Index()
        {
            // Activity Tracker
            ActivityAPIClient.Track(StudentID: "S00227414", StudentName: "Levi Gilmartin",
                activityName: "RAD301 Week 8 Lab 2023", Task: "Implementing All Club Detail Filter");

            List<SelectListItem> items = FillClubs();
            items.First().Selected = true;
            ViewBag.Clubs = items;
            int cid = Int32.Parse(items.First().Value);
            return View(db.Clubs.FirstOrDefault(c => c.ClubId == cid));
        }
        [HttpPost]
        public IActionResult Index(Club model)
        {
            

            List<SelectListItem> items = FillClubs();
            items.First(s => s.Value == model.ClubId.ToString());
            ViewBag.Clubs = items;
            return View(db.Clubs.FirstOrDefault(c => c.ClubId == model.ClubId));
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
