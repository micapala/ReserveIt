import Vue from "vue";
import VueMaterial from "vue-material";
//import "vue-material/dist/vue-material.min.css";
//import "vue-material/dist/theme/default.css";

Vue.use(VueMaterial);

import { store } from "./_store";
import { router } from "./_helpers";
import App from "./app/App";

new Vue({
  el: "#app",
  router,
  store,
  render: (h) => h(App),
});
