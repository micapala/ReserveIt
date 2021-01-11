import { concertService } from "../_services";

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

export const concertsByDate = {
  namespaced: true,
  state: {
    byDate: {}
  },
  actions: {
    getByDate({ commit }, { date }) {
      commit("getByDateRequest", { date });

      concertService.getByDate(date).then(
        concertsByDate => commit("getByDateSuccess", concertsByDate),
        error => commit("getByDateFailure", error)
      );
    },
    clear({ commit }) {
      commit("clear");
    }
  },
  mutations: {
    getByDateRequest(state) {
      state.byDate = { loading: true };
    },
    getByDateSuccess(state, concerts) {
      state.byDate = { items: concerts };
    },
    getByDateFailure(state, error) {
      state.byDate = { error };
    },
    clear(state) {
      state.byDate = {}
    }
  }
};
