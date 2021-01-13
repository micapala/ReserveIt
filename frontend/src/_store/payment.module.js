import { paymentService } from "../_services";

export const payment = {
  namespaced: true,
  state: {
    status: {},
  },
  actions: {
    goToPayment({ commit, dispatch }, { controlString }) {
      commit("paymentRequest");
      console.log(controlString);

      paymentService.goToPayment(controlString).then(
        (response) => {
          window.location.href = response.paymentUrl;
        },
        (error) => {
          commit("reservationFailure", error);
          dispatch("alert/error", error, { root: true });
        }
      );
    },
  },
  mutations: {
    paymentRequest(state) {
      state.status = { loading: true };
    },
    paymentSuccess(state) {
      state.status = { loading: false };
    },
    paymentFailure(state, error) {
      state.status = { error };
    },
  },
};
