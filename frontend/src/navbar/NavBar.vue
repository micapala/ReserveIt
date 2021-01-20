<template>
  <div class="navbar">
    <item title="Home" link="/"></item>
    <!--item title="Concerts" link="/concerts"></item-->
    <item v-if="!user" title="Login" link="/login"></item>
    <item v-if="user" title="Reservations" link="/reservations"></item>
    <item v-if="admin" title="Admin" link="/admin"></item>
    <item v-if="user" title="Logout" link="/login"></item>
  </div>
</template>

<script>
import item from "./NavBar-item";

export default {
  name: "NavBar",
  components: { item },
  computed: {
    user() {
      return this.$store.state.authentication.user;
    },
    admin() {
      if (this.user) {
        return this.$store.state.authentication.user.role == "Admin";
      }
      return null;
    }
  }
};
</script>

<style lang="css">
.navbar {
  width: 100%;
  background-color: var(--navbar-color);
  display: flex;
  justify-content: flex-start;
  padding: 0;
}
</style>
