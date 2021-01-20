import { requestOptions, handleResponse } from "@/_helpers";
import { BehaviorSubject } from "rxjs";

const currentUserSubject = new BehaviorSubject(
  JSON.parse(localStorage.getItem("user"))
);

export const userService = {
  login,
  logout,
  getAll,
  register,
  get currentUserValue() {
    return currentUserSubject.value;
  }
};

function login(username, password) {
  return fetch(
    `/api/Users/authenticate`,
    requestOptions.post({ username, password })
  )
    .then(handleResponse)
    .then(user => {
      if (user.token) {
        localStorage.setItem("user", JSON.stringify(user));
        currentUserSubject.next(user);
      }

      return user;
    });
}

function logout() {
  localStorage.removeItem("user");
  currentUserSubject.next(null);
}

function getAll() {
  return fetch(`/api/Users`, requestOptions.get()).then(handleResponse);
}

function register(username, password, email, name, surname) {
  return fetch(
    `/api/Users/register`,
    requestOptions.post({ username, password, email, name, surname })
  ).then(handleResponse);
}
