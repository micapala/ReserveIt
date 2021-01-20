import { requestOptions, handleResponse } from "@/_helpers";

export const concertService = {
  getAll,
  create,
  remove,
  update
};

function getAll() {
  return fetch(`/api/Concert/`, requestOptions.get()).then(handleResponse);
}

function create(name, bandName, price, date) {
  return fetch(
    "/api/Concert/",
    requestOptions.post({ name, bandName, price, date })
  ).then(handleResponse);
}

function remove(id) {
  return fetch(`/api/Concert/${id}`, requestOptions.delete()).then(
    handleResponse
  );
}

function update(id, name, bandName, price, date) {
  return fetch(
    `/api/Concert/${id}`,
    requestOptions.put({ name, bandName, price, date })
  ).then(handleResponse);
}
