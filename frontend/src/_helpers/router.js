import Vue from "vue";
import Router from "vue-router";

import HomePage  from "../home/HomePage";
import UserPage  from "../home/User";
import LoginPage from "../login/LoginPage";
import SignIn    from "../login/SignIn";
import AdminPage from "../login/Admin";

Vue.use(Router);

export const router = new Router({
  mode: "history",
  routes: [
    { path: "/", component: HomePage },
    { path: "/login", component: LoginPage },
    { path: "/sign", component: SignIn },
    { path: "/user", component: UserPage },
    { path: "/admin", component: AdminPage },

    // otherwise redirect to home
    { path: "*", redirect: "/" }
  ]
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ["/login", "/sign", "/"];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");

  if (authRequired && !loggedIn) {
    return next("/login");
  }

  next();
});