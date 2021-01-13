//import config from "config";

export const concertService = {
  getByDate,
  getAll,
  create,
  remove,
  update,
};

function getAll() {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`/Concert`, requestOptions).then(handleResponse);
}

function getByDate(date) {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`/Concert/byDate/${date}`, requestOptions).then(handleResponse);
}

function create(name, bandName, price, date) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({name, bandName, price, date})
  };

  console.log(requestOptions);

  return fetch('/Concert/create', requestOptions).then(handleResponse);
};

function remove(id) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({id})
  };

  return fetch('/Concert/remove', requestOptions).then(handleResponse);
};

function update(id, name, bandName, price, date) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({id, name, bandName, price, date})
  };

  return fetch('/Concert/update', requestOptions).then(handleResponse);
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
