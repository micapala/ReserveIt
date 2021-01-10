<template>
  <div class="navbar">
    <item title="Home" link="/"></item>
    <item v-if="!user" title="Login" link="/login"></item>
    <item title="3" link="/TODO"></item>
    <item v-if="user" title="Logout" link="/login"></item>
    <item v-if="admin" title="Admin" link="/admin"></item>
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
      return this.$store.state.authentication.user; //TODO: proper admin check
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
