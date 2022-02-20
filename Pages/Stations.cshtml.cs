using lab_4.EFCore;
using lab_4.Models;
using lab_4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5.Pages
{
    public class StationsModel : PageModel
    {
        private readonly AppDbContext _context;

        public lab_4.Models.Stations station;
        public List<lab_4.Models.Stations> Stations { get; set; }


        public StationsModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Stations = _context.Stations.ToList();
        }

        
        public IActionResult OnPostAddStation(Stations newStation)
        {

            _context.Stations.Add(new Stations
            {
                StationId = _context.Stations.Max(item => item.StationId) + 1,
                Station = newStation.Station,
                Distance = newStation.Distance,
                Cost = newStation.Cost
            });

            _context.SaveChanges();
            return RedirectToPage("Stations");

        }

        public IActionResult AddStation()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteStation(Int32 stationId)
        {
            var record = _context.Stations.Find(stationId);
            _context.Stations.Remove(record);

            await _context.SaveChangesAsync();

            return RedirectToPage("Stations");

        }

        public IActionResult OnGetEditStation(Int32 stationId)
        {

            Console.WriteLine(1);
            if (stationId == 0)
                return RedirectToPage("Stations");

            var record = _context.Stations.FirstOrDefault(s => s.StationId == stationId);


            if (record == null)

            {
                return RedirectToPage("Stations");
            }
            station = record;

            return RedirectToPage("EditStation");
        }

        public IActionResult OnPostEditStation()
        {
            Console.WriteLine(2);

            if (station == null)
            {
                return RedirectToPage("Stations");
            }

            var record = _context.Stations.FirstOrDefault(s => s.StationId == station.StationId);

            record.Station = station.Station;
            record.Distance = station.Distance;
            record.Cost = station.Cost;
           

            _context.SaveChanges();

            return RedirectToPage("Stations");
        }

    }
}
