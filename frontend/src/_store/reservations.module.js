import { store } from ".";
import { reservationService } from "../_services";

export const reservations = {
  namespaced: true,
  state: {
    all: {}
  },
  actions: {
    getAll({ commit }) {
      commit("getAllRequest");
      var userName = store.state.authentication.user.username;

      reservationService.getAll(userName).then(
        reservations => commit("getAllSuccess", reservations),
        error => commit("getAllFailure", error)
      );
    },
  },
  mutations: {
    getAllRequest(state) {
      state.all = { loading: true };
    },
    getAllSuccess(state, reservations) {
      state.all = { items: reservations };
    },
    getAllFailure(state, error) {
      state.all = { error };
    }
  }
};

