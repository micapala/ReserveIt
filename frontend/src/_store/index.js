import Vue from "vue";
import Vuex from "vuex";

import { alert } from "./alert.module";
import { authentication } from "./authentication.module";
import { users } from "./users.module";
import { bands } from "./bands.module";
import { concerts } from "./concerts.module";
import { registration } from "./registration.module";
import { reservation } from "./reservation.module";
import { payment } from "./payment.module";
import { reservations } from "./reservations.module";

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    alert,
    authentication,
    users,
    bands,
    concerts,
    registration,
    reservation,
    payment,
    reservations
  }
});
