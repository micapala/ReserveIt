import { requestOptions, handleResponse } from "@/_helpers";
export const reservationService = {
  reserve,
  getAll
};

function reserve(userLogin, concertId) {
  return fetch(
    `/api/Reservation/create`,
    requestOptions.post({ userLogin, concertId })
  )
    .then(handleResponse)
    .then(paymentId => {
      return paymentId;
    });
}

function getAll(userName) {
  return fetch(
    `/api/Reservation/getUserReservations/${userName}`,
    requestOptions.get()
  ).then(handleResponse);
}
