import Vue from "vue";
import Router from "vue-router";

import HomePage from "../home/HomePage";
import ConcertSelector from "../home/ConcertSelector";
import LoginPage from "../login/LoginPage";
import SignIn from "../login/SignIn";
import AdminPage from "../admin/Admin";
import ReservationPage from "../home/ReservationPage";
import ReservationsPage from "../home/ReservationsPage";

Vue.use(Router);

export const router = new Router({
  mode: "history",
  routes: [
    { path: "/", component: HomePage },
    { path: "/login", component: LoginPage },
    { path: "/sign", component: SignIn },
    {
      path: "/admin",
      component: AdminPage,
      meta: { authorize: ["Admin"] }
    },
    { path: "/concerts", component: ConcertSelector },
    { path: "/reserve", component: ReservationPage },
    { path: "/reservations", component: ReservationsPage },

    // otherwise redirect to home
    { path: "*", redirect: "/" }
  ],
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ["/login", "/sign", "/", "/concerts"];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");

  if (authRequired && !loggedIn) {
    return next("/login");
  }

  if (authRequired && loggedIn) {
    const user = JSON.parse(loggedIn);
    const encodedPayload = user.token.split(".")[1];
    const payload = window.atob(encodedPayload);
    const decodedJwtData = JSON.parse(payload);
    const userRole = decodedJwtData["role"];

    const { authorize } = to.meta;

    if (authorize) {
      if (authorize.length && !authorize.includes(userRole)) {
        return next({ path: "/" });
      }
    }
  }

  next();
});
