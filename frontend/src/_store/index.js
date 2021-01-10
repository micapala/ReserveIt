import Vue from "vue";
import Vuex from "vuex";

import { alert } from "./alert.module";
import { authentication } from "./authentication.module";
import { users } from "./users.module";
import { concertsByDate } from "./concerts.module";
import { registration } from "./registration.module";

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    alert,
    authentication,
    users,
    concertsByDate,
    registration
  }
});
