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
    public class TrainsModel : PageModel
    {
        private readonly AppDbContext _context;

        public lab_4.Models.Trains train;
        public List<lab_4.Models.Trains> Trains { get; set; }


        public TrainsModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Trains = _context.Trains.ToList();
        }

        
        public IActionResult OnPostAddTrain(lab_4.Models.Trains newTrain)
        {

            _context.Trains.Add(new lab_4.Models.Trains
            {
                TrainId = _context.Trains.Max(item => item.TrainId) + 1,
                Train = newTrain.Train,
                TrainType = newTrain.TrainType
            });

            _context.SaveChanges();
            return RedirectToPage("Trains");

        }

        public IActionResult AddTrain()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteTrain(Int32 trainId)
        {
            var record = _context.Trains.Find(trainId);
            _context.Trains.Remove(record);

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");

        }

        public IActionResult OnGetEditTrain(Int32 trainId)
        {

            Console.WriteLine(1);
            if (trainId == 0)
                return RedirectToPage("Trains");

            var record = _context.Trains.FirstOrDefault(s => s.TrainId == trainId);


            if (record == null)

            {
                return RedirectToPage("Trains");
            }
            train = record;

            return RedirectToPage("EditTrain");
        }

        public IActionResult OnPostEditTrain()
        {
            Console.WriteLine(2);

            if (train == null)
            {
                return RedirectToPage("Trains");
            }

            var record = _context.Trains.FirstOrDefault(s => s.TrainId == train.TrainId);

            record.Train = train.Train;
            record.TrainType = train.TrainType;

            _context.SaveChanges();

            return RedirectToPage("Trains");
        }

        
    }
}
