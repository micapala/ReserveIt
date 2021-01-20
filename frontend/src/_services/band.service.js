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
  return fetch("/api/Band/create", requestOptions.post({ name })).then(
    handleResponse
  );
}

function remove(id) {
  return fetch("/api/Band/remove", requestOptions.post({ id })).then(
    handleResponse
  );
}

function update(id, name) {
  return fetch("/api/Band/update", requestOptions.post({ id, name })).then(
    handleResponse
  );
}
