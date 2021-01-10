//import config from "config";
import { authHeader } from "../_helpers";

export const concertService = {
  getByDate
};

function getByDate(date) {
  const requestOptions = {
    method: "POST",
    headers: authHeader(),
    body: date
  };

  return fetch(`/Concert/byDate`, requestOptions).then(handleResponse);
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
