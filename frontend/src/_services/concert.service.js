//import config from "config";

export const concertService = {
  getAll,
  create,
  remove,
  update,
};

function getAll() {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`/api/Concert`, requestOptions).then(handleResponse);
}

function create(name, bandName, price, date) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({name, bandName, price, date})
  };

  console.log(requestOptions);

  return fetch('/api/Concert/create', requestOptions).then(handleResponse);
};

function remove(id) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({id})
  };

  return fetch('/api/Concert/remove', requestOptions).then(handleResponse);
};

function update(id, name, bandName, price, date) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({id, name, bandName, price, date})
  };

  return fetch('/api/Concert/update', requestOptions).then(handleResponse);
};

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
