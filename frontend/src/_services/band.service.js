import { requestOptions, handleResponse } from "@/_helpers";

export const bandService = {
  getAll,
  create,
  remove,
  update
};

function getAll() {
  return fetch("/api/Band", requestOptions.get()).then(handleResponse);
}

function create(name) {
  return fetch("/api/Band/", requestOptions.post({ name })).then(
    handleResponse
  );
}

function remove(id) {
  return fetch(`/api/Band/${id}`, requestOptions.delete()).then(
    handleResponse
  );
}

function update(id, name) {
  return fetch(`/api/Band/${id}`, requestOptions.put({ name })).then(
    handleResponse
  );
}
