import { paymentService } from "../_services";
import { router } from "../_helpers";

export const payment = {
  namespaced: true,
  state: {
    status: {}
  },
  actions: {
    goToPayment({ commit, dispatch }, { controlString }) {
      commit("paymentRequest");
      console.log(controlString);

      paymentService.goToPayment(controlString).then(
        response => {
          router.push("/reservations");
          var win = window.open(response.paymentUrl, "_blank");
          win.focus();
        },
        error => {
          commit("reservationFailure", error);
          dispatch("alert/error", error, { root: true });
        }
      );
    }
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
    }
  }
};
