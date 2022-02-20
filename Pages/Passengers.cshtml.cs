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
    public class PassengersModel : PageModel
    {
        private readonly AppDbContext _context;
        public Passangers passenger;
        public List<Passangers> Passengers { get; set; }


        public PassengersModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Passengers = _context.Passangers.ToList();
        }


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
            return RedirectToPage("Passengers");

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

            return RedirectToPage("Passengers");

        }

        public IActionResult OnGetEditPassenger(Int32 passengerId)
        {

            Console.WriteLine(1);
            if (passengerId == 0)
                return RedirectToPage("Passengers");

            var record = _context.Passangers.FirstOrDefault(s => s.PassengerId == passengerId);


            if (record == null)

            {
                return RedirectToPage("Passengers");
            }
            passenger = record;

            return RedirectToPage("EditPassenger");
        }

        public IActionResult OnPostEditPassenger(Passangers ePassanger)
        {

            Console.WriteLine(ePassanger.FullName);

            if (passenger == null)
            {
                return RedirectToPage("Passengers");
            }

            var record = _context.Passangers.FirstOrDefault(s => s.PassengerId == passenger.PassengerId);

            record.FullName = passenger.FullName;
            record.Address = passenger.Address;
            record.Phone = passenger.Phone;
            Console.WriteLine(passenger.PassengerId);
            Console.WriteLine(record.PassengerId);


            _context.SaveChanges();

            return RedirectToPage("Passengers");
        }


    }
}
