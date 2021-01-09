<template>
  <div class="navbar">
    <div class="row">
      <item title="Home" link="/"></item>
      <item v-if="!user" title="Login" link="/login"></item>
      <item title="3" link="/TODO"></item>
      <item v-if="user" title="Logout" link="/login"></item>
      <item v-if="admin" title="Admin" link="/admin"></item>
    </div>
  </div>
</template>

<script>
import item from "./NavBar-item"

export default {
  name: "NavBar",
  components: { item },
  computed: {
    user() {
      return this.$store.state.authentication.user;
    },
    admin() {
      return this.$store.state.authentication.user;//TODO: proper admin check
    }
  }
};
</script>

<style lang="css">
.navbar{
  width: 100%;
  height: 60px;
  background-color: #8e8e8e;
  top: 0;
  /* position: fixed; */
  border-radius:10px;
  display: flex;
}
.row{
  display: flex;
  width: 95%;
  height: 40px;
  margin-top: 10px;
  margin-left: 10px;
}
</style>
