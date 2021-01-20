export const reservationService = {
  reserve,
  getAll
};

function reserve(userLogin, concertId) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ userLogin, concertId })
  };

  return fetch(`/api/Reservation/create`, requestOptions)
    .then(handleResponse)
    .then(paymentId => {
      return paymentId;
    });
}

function getAll(userName) {
  const requestOptions = {
    method: "GET"
  };

  return fetch(
    `/api/Reservation/getUserReservations/${userName}`,
    requestOptions
  ).then(handleResponse);
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
