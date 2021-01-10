<template>
  <div class="bg">
    <div class="header">
      <h1>ReserveIt</h1>
    </div>
    <NavBar></NavBar>

    <router-view></router-view>
  </div>
</template>

<script>
import NavBar from "../navbar/NavBar";

export default {
  name: "app",
  computed: {
    alert() {
      return this.$store.state.alert;
    }
  },
  watch: {
    $route() {
      this.$store.dispatch("alert/clear");
    }
  },
  components: { NavBar }
};
</script>

<style lang="css">
:root {
  --primary-color: #bf616a;
  --background-color: #2b303b;
  --font-color: #eff1f5;
  --navbar-color: #65737e;
  --green-color: #a3be8c;
  --purple-color: #b48ead;
}

body {
  background-color: var(--background-color);
}
.header {
  background-color: var(--primary-color);
  padding: 10px;
  text-align: center;
}
</style>
