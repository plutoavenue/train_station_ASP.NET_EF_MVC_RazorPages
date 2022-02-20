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
    public class BookingModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<Tickets> Tickets { get; set; }
        public List<BookingViewModel> Booking { get; set; }


        public BookingModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Booking = (from ticket in _context.Tickets
                                     join passenger in _context.Passangers on ticket.PassengerId equals passenger.PassengerId
                                     join train in _context.Trains on ticket.TrainId equals train.TrainId
                                     join car in _context.Cars on ticket.CarId equals car.CarId
                                     join station in _context.Stations on ticket.StationId equals station.StationId

                                     select new BookingViewModel()
                                     {
                                         TicketId = ticket.TicketId,
                                         PassengerId = ticket.PassengerId,
                                         TrainId = ticket.TrainId,
                                         CarId = ticket.CarId,
                                         StationId = ticket.StationId,

                                         FullName = passenger.FullName,
                                         //   Address = passenger.Address,
                                         Phone = passenger.Phone,

                                         Train = train.Train,
                                         //  TrainType = train.TrainType,

                                         Car = car.Car,
                                         CarType = car.CarType,
                                         ExtraCarFee = car.ExtraCarFee,

                                         DepartureDate = ticket.DepartureDate,
                                         DepartureTime = ticket.DepartureTime,
                                         ArrivalTime = ticket.ArrivalTime,

                                         Station = station.Station,
                                         //    Distance = station.Distance,
                                         Cost = station.Cost,

                                         ExtraTimeFee = ticket.ExtraTimeFee
                                     }).ToList();
        }


            public IActionResult OnPostAddTicket(Tickets newTicket)
            {

                _context.Tickets.Add(new Tickets
                {
                    TicketId = _context.Tickets.Max(item => item.TicketId) + 1,
                    PassengerId = newTicket.PassengerId,
                    TrainId = newTicket.TrainId,
                    CarId = newTicket.CarId,
                    DepartureDate = newTicket.DepartureDate,
                    DepartureTime = newTicket.DepartureTime,
                    ArrivalTime = newTicket.ArrivalTime,
                    StationId = newTicket.StationId,
                    ExtraTimeFee = newTicket.ExtraTimeFee
                });

                _context.SaveChanges();
                return RedirectToPage("Tickets");

            }

            public IActionResult AddPosition()
            {
                return Page();
            }


            public async Task<IActionResult> OnPostDeleteTicket(Int32 ticketId)
            {
                var record = _context.Tickets.Find(ticketId);
                _context.Tickets.Remove(record);

                await _context.SaveChangesAsync();

                return RedirectToPage("Tickets");

            }
            public IActionResult OnGetEditTicket(Int32 ticketId)
            {
                if (ticketId == 0)
                    return RedirectToPage("Tickets");

                var record = _context.Tickets.FirstOrDefault(s => s.TicketId == ticketId);


                if (record == null)

                {
                    return RedirectToPage("Tickets");
                }


                return RedirectToPage("EditTicket", record);
            }

            
            public IActionResult OnPostEditTicket(Tickets newTicket, Int32 id)
            {
                if (newTicket == null)
                {
                    return RedirectToPage("Tickets");
                }

                var record = _context.Tickets.FirstOrDefault(s => s.TicketId == id);

                record.PassengerId = newTicket.PassengerId;
                record.TrainId = newTicket.TrainId;
                record.CarId = newTicket.CarId;
                record.DepartureDate = newTicket.DepartureDate;
                record.DepartureTime = newTicket.DepartureTime;
                record.ArrivalTime = newTicket.ArrivalTime;
                record.StationId = newTicket.StationId;
                record.ExtraTimeFee = newTicket.ExtraTimeFee;


                _context.SaveChanges();

                return RedirectToPage("Tickets");
            }

        }
}
