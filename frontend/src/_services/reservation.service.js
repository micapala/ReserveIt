import { requestOptions, handleResponse } from "@/_helpers";

import * as axios from "axios";

export const reservationService = {
  reserve,
  getAll,
  downloadTicket
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

function downloadTicket(reservationId) {
  axios
    .get(`/api/Reservation/downloadTicket/${reservationId}`, {
      responseType: "blob"
    })
    .then(response => {
      const blob = new Blob([response.data], { type: "application/pdf" });
      const link = document.createElement("a");

      const contentDisposition = response.headers["content-disposition"];
      const filename = contentDisposition.substring(21);
      link.href = URL.createObjectURL(blob);
      link.download = filename;
      link.click();
      URL.revokeObjectURL(link.href);
    })
    .catch(console.error);
}
