import { bandService } from "../_services";

export const bands = {
  namespaced: true,
  state: {
    all: {},
    status: {}
  },
  actions: {
    getAll({ commit }) {
      commit("getAllRequest");

      bandService.getAll().then(
        bands => commit("getAllSuccess", bands),
        error => commit("getAllFailure", error)
      );
    },
    create({ commit }, { name }) {
      commit("statusRequest");

      bandService.create(name).then(
        success => {
          commit("statusSuccess", "Created successfully"),
          dispatch("getAll")
        },
        error => commit("statusFailure", error)
      );
    },
    update({ commit }, { id, name }) {
      commit("statusRequest");

      bandService.update(id, name).then(
        success => {
          commit("statusSuccess", "Updated successfully"),
          dispatch("getAll")
        },
        error => commit("statusFailure", error)
      );
    },
    remove({ commit, dispatch }, { id }) {
      commit("statusRequest");

      bandService.remove(id).then(
        success => {
          commit("statusSuccess", "Removed successfully"),
          dispatch("getAll")
        },
        error => commit("statusFailure", error)
      );
    },
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
    },
    statusRequest(state) {
      state.status = { loading: true };
    },
    statusSuccess(state, success) {
      state.status = { success };
    },
    statusFailure(state, error) {
      state.status = { error };
    },
  }
};

