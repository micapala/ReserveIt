import { concertService } from "../_services";

export const concertsByDate = {
  namespaced: true,
  state: {
    byDate: {}
  },
  actions: {
    getByDate({ commit }, { date }) {
      commit("getByDateRequest", { date });

      concertService.getByDate(date).then(
        concerts => commit("getByDateSuccess", users),
        error => commit("getByDateFailure", error)
      );
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
    }
  }
};
