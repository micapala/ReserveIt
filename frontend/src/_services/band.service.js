export const bandService = {
  getAll,
  create,
  remove,
  update
};

function getAll() {
  const requestOptions = {
    method: "GET"
  };

  return fetch('/api/Band', requestOptions).then(handleResponse);
};

function create(name) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({name})
  };

  return fetch('/api/Band/create', requestOptions).then(handleResponse);
};

function remove(id) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({id})
  };

  return fetch('/api/Band/remove', requestOptions).then(handleResponse);
};

function update(id, name) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({id, name})
  };

  return fetch('/api/Band/update', requestOptions).then(handleResponse);
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
};
