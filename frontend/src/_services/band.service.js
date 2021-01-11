//import config from "config";

export const bandService = {
  getAll,
};

function getAll() {
  const requestOptions = {
    method: "GET"
  };

  return fetch(`/Band`, requestOptions).then(handleResponse);
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
