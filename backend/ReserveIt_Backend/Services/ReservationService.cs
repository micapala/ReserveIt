﻿using Microsoft.Extensions.Options;
using ReserveIt_Backend.Dtos.Reservation;
using ReserveIt_Backend.Entities;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using ReserveIt_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services
{
    public class ReservationService : IReservationService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConcertRepository _concertRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;

        private readonly AppSettings _appSettings;

        public ReservationService(IPaymentRepository paymentRepository, IUserRepository userRepository, IConcertRepository concertRepository, IReservationRepository reservationRepository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _concertRepository = concertRepository;
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
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
                               ReservationDate = c.ReservationDate,
                               ConcertName = c.Concert.Name,
                               ConcertDate = c.Concert.Date,
                               TicketPrice = c.Concert.TicketPrice,
                               AmountPaid = c.Payment.TotalAmount - c.Payment.TotalDue
                           };

            return reservations;
        }

        public Task<string> Update(string paymentString, float amount)
        {
            throw new NotImplementedException();
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
