import { userService } from "@/_services";

export const requestOptions = {
  get() {
    return {
      method: "GET",
      ...headers()
    };
  },
  post(body) {
    return {
      method: "POST",
      ...headers(),
      body: JSON.stringify(body)
    };
  },
  patch(body) {
    return {
      method: "PATCH",
      ...headers(),
      body: JSON.stringify(body)
    };
  },
  put(body) {
    return {
      method: "PUT",
      ...headers(),
      body: JSON.stringify(body)
    };
  },
  delete() {
    return {
      method: "DELETE",
      ...headers()
    };
  }
};

function headers() {
  const currentUser = userService.currentUserValue || {};
  const authHeader = currentUser.token
    ? { Authorization: "Bearer " + currentUser.token }
    : {};
  return {
    headers: {
      ...authHeader,
      "Content-Type": "application/json"
    }
  };
}
