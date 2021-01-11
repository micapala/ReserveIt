import { bandService } from "../_services";

export const bands = {
  namespaced: true,
  state: {
    all: {}
  },
  actions: {
    getAll({ commit }) {
      commit("getAllRequest");

      bandService.getAll().then(
        bands => commit("getAllSuccess", bands),
        error => commit("getAllFailure", error)
      );
    }
  },
  mutations: {
    getAllRequest(state) {
      state.all = { loading: true };
    },
    getAllSuccess(state, bands) {
      state.all = { items: bands };
    },
    getAllFailure(state, error) {
      state.all = { error };
    }
  }
};
