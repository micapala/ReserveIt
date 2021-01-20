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
    },
    create({ dispatch }, { name }) {
      bandService.create(name).then(
        () => {
          dispatch("alert/success", "Band created successfully", {
            root: true
          });
          dispatch("getAll");
        },
        error => dispatch("alert/error", error, { root: true })
      );
    },
    update({ dispatch }, { id, name }) {
      bandService.update(id, name).then(
        () => {
          dispatch("alert/success", "Band updated successfully", {
            root: true
          });
          dispatch("getAll");
        },
        error => dispatch("alert/error", error, { root: true })
      );
    },
    remove({ dispatch }, { id }) {
      bandService.remove(id).then(
        () => {
          dispatch("alert/success", "Band deleted successfully", {
            root: true
          });
          dispatch("getAll");
        },
        error => dispatch("alert/error", error, { root: true })
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
