import { userService } from "../_services";
import { router } from "../_helpers";

export const registration = {
  namespaced: true,
  state: {
    status: {}
  },
  actions: {
    register(
      { dispatch, commit },
      { username, password, email, name, surname }
    ) {
      commit("registrationRequest");

      userService.register(username, password, email, name, surname).then(
        success => {
          dispatch("alert/success", "Rejestracja zakończona powodzeniem");
          router.push("/login");
        },
        error => {
          dispatch("alert/errorw", error);
        }
      );
    }
  },
  mutations: {
    registrationRequest(state) {
      state.status = { loading: true };
    },
    registrationSuccess(state) {
      state.status = { signingIn: true };
    },
    registrationFailure(state) {
      state.status = { signingIn: false };
    }
  }
};
