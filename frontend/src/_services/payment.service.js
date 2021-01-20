import { requestOptions, handleResponse } from "@/_helpers";
export const paymentService = {
  goToPayment
};

function goToPayment(controlString) {
  return fetch(
    `/api/Payment/getPaymentLink/${controlString}`,
    requestOptions.get()
  )
    .then(handleResponse)
    .then(paymentLink => {
      return paymentLink;
    });
}
