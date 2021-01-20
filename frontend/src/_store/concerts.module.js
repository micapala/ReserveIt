import { concertService } from "../_services";
import { alert } from "./alert.module";

export const concerts = {
  namespaced: true,
  state: {
    all: {}
  },
  actions: {
    getAll({ commit }) {
      commit("getAllRequest");

      concertService.getAll().then(
        concerts => commit("getAllSuccess", concerts),
        error => commit("getAllFailure", error)
      );
    },
    create({ dispatch }, { name, bandName, price, date }) {
      concertService.create(name, bandName, price, date).then(
        success => {
          dispatch("alert/success", "Concert created successfully", {
            root: true
          });
          dispatch("getAll");
        },
        error => dispatch("alert/error", error, { root: true })
      );
    },
    update({ dispatch }, { id, name, bandName, price, date }) {
      concertService.update(id, name, bandName, price, date).then(
        success => {
          dispatch("alert/success", "Concert updated successfully", {
            root: true
          });
          dispatch("getAll");
        },
        error => dispatch("alert/error", error, { root: true })
      );
    },
    remove({ dispatch }, { id }) {
      concertService.remove(id).then(
        success => {
          dispatch("alert/success", "Concert deleted successfully", {
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
    getAllSuccess(state, concerts) {
      state.all = { items: concerts };
    },
    getAllFailure(state, error) {
      state.all = { error };
    }
  }
};
