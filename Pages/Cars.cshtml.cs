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
    public class CarsModel : PageModel
    {
        private readonly AppDbContext _context;

        public lab_4.Models.Cars car;
        public List<lab_4.Models.Cars> Cars { get; set; }


        public CarsModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Cars = _context.Cars.ToList();
        }

        /*
        public IActionResult OnPostAddPassenger(Passangers newPassanger)
        {

            _context.Passangers.Add(new Passangers
            {
                PassengerId = _context.Passangers.Max(item => item.PassengerId) + 1,
                FullName = newPassanger.FullName,
                Address = newPassanger.Address,
                Phone = newPassanger.Phone
            });

            _context.SaveChanges();
            return RedirectToPage("Index");

        }

        public IActionResult AddPassenger()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostDeletePassenger(Int32 passengerId)
        {
            var record = _context.Passangers.Find(passengerId);
            _context.Passangers.Remove(record);

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");

        }

        public IActionResult OnGetEditPassenger(Int32 passengerId)
        {

            Console.WriteLine(1);
            if (passengerId == 0)
                return RedirectToPage("Index");

            var record = _context.Passangers.FirstOrDefault(s => s.PassengerId == passengerId);


            if (record == null)

            {
                return RedirectToPage("Index");
            }
            passenger = record;

            return RedirectToPage("EditPassenger", record);
        }

        public IActionResult OnPostEditPassenger(Passangers newPassanger, Int32 id)
        {
            Console.WriteLine(2);

            if (newPassanger == null)
            {
                return RedirectToPage("Index");
            }

            var record = _context.Passangers.FirstOrDefault(s => s.PassengerId == id);

            record.FullName = newPassanger.FullName;
            record.Address = newPassanger.Address;
            record.Phone = newPassanger.Phone;

            _context.SaveChanges();

            return RedirectToPage("Index");
        }

        */
    }
}
