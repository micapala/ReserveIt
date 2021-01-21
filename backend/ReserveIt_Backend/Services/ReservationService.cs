using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.Options;
using ReserveIt_Backend.Dtos.Reservation;
using ReserveIt_Backend.Dtos.Ticket;
using ReserveIt_Backend.Entities;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using ReserveIt_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserRepository _userRepository;
        private readonly IConcertRepository _concertRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IConverter _converter;

        private readonly AppSettings _appSettings;

        public ReservationService(IPaymentService paymentService, IPaymentRepository paymentRepository, IUserRepository userRepository, IConcertRepository concertRepository, IReservationRepository reservationRepository, IConverter converter, IOptions<AppSettings> appSettings)
        {
            _converter = converter;
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _concertRepository = concertRepository;
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
            _paymentService = paymentService;
        }
        public async Task<string> Create(ReservationRequest request)
        {
            User user = _userRepository.GetAll().Where(u => u.Username == request.UserLogin).SingleOrDefault();
            if (user == null) return "User with login " + request.UserLogin + " not found";

            Concert concert = _concertRepository.GetAll().Where(c => c.Id == request.ConcertId).SingleOrDefault();
            if (user == null) return "Concert with id " + request.ConcertId + " not found";

            string controlString = GetRandomHexNumber(15);

            Payment payment = new Payment
            {
                ControlString = controlString,
                User = user,
                Concert = concert,
                TotalAmount = concert.TicketPrice,
                TotalDue = concert.TicketPrice
            };

            var payment_success = await _paymentRepository.Create(payment);

            if (!payment_success)
                return "Failed to create payment";

            Reservation reservation = new Reservation
            {
                User = user,
                Concert = concert,
                Payment = payment,
                ReservationDate = DateTime.Now
            };

            var reservation_success = await _reservationRepository.Create(reservation);
            if (!reservation_success)
                return "Failed to create reservation changes";

            return controlString;
        }

        public IQueryable<ReservationResponse> GetAllUserReservations(string userLogin)
        {
            var result = _reservationRepository.GetAllUserReservations(userLogin);

            var reservations = from c in result
                           select new ReservationResponse()
                           {
                               ReservationId = c.Id,
                               ReservationDate = c.ReservationDate,
                               ConcertName = c.Concert.Name,
                               ConcertDate = c.Concert.Date,
                               TicketPrice = c.Concert.TicketPrice,
                               AmountPaid = c.Payment.TotalAmount - c.Payment.TotalDue,
                               PaymentLink = _paymentService.GetPaymentLink(c.Payment.ControlString).PaymentUrl
                           };

            return reservations;
        }

        public TicketFileInfo GenerateTicket(int reservationId)
        {
            var result = _reservationRepository.GetById(reservationId);

            TicketDetails reservationResponse = new TicketDetails
            {
                ReservationDate = result.ReservationDate,
                ConcertName = result.Concert.Name,
                ConcertDate = result.Concert.Date,
                TicketPrice = result.Concert.TicketPrice,
            };


            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TicketGenerator.GetTicketHTMLString(reservationResponse),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            TicketFileInfo info = new TicketFileInfo
            {
                Data = file,
                FileName = "Ticket-" + result.Id + ".pdf"
            };
            return info;
        }

        static Random random = new Random();
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");

        }
    }
}
