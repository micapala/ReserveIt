import { reservationService } from "../_services";
import { router } from "../_helpers";

export const reservation = {
  namespaced: true,
  state: {
    status: {},
    concert_id: null,
    concert_name: null,
    band_name: null,
    ticket_price: null,
    reservation_created: false,
    paymentId: null,
  },
  actions: {
    moveToReservation(
      { commit },
      { concert_id, concert_name, band_name, ticket_price }
    ) {
      commit("setSelectedConcertInfo", {
        concert_id,
        concert_name,
        band_name,
        ticket_price,
      });
      router.push("/reserve");
    },
    reserve({ dispatch, commit }, { userName, concertId }) {
      commit("reservationRequest");

      reservationService.reserve(userName, concertId).then(
        paymentId => {
          commit("reservationSuccess", paymentId.result);
          dispatch("alert/success", "Concert reserved", { root: true });
        },
        error => {
          commit("reservationFailure", error);
          dispatch("alert/error", error, { root: true });
        }
      );
    }
  },
  mutations: {
    setSelectedConcertInfo(
      state,
      { concert_id, concert_name, band_name, ticket_price }
    ) {
      state.concert_id = concert_id;
      state.concert_name = concert_name;
      state.band_name = band_name;
      state.ticket_price = ticket_price;
    },
    reservationRequest(state) {
      state.status = { loading: true };
    },
    reservationSuccess(state, paymentId) {
      state.status = { loading: false };
      state.reservation_created = true;
      state.paymentId = paymentId;
    },
    reservationFailure(state, error) {
      state.status = { error };
    },
  },
};
