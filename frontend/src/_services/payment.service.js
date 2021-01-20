export const paymentService = {
  goToPayment
};

function goToPayment(controlString) {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`/api/Payment/getPaymentLink/${controlString}`, requestOptions)
    .then(handleResponse)
    .then(paymentLink => {
      return paymentLink;
    });
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
