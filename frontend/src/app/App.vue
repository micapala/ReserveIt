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
/* Global css rules */
:root {
  --primary-color: #bf616a;
  --background-color: #2b303b;
  --font-color: #eff1f5;
  --navbar-color: #65737e;
  --green-color: #a3be8c;
  --purple-color: #b48ead;
}
.bg {
  margin-bottom: 20%;
}
body {
  background-color: var(--background-color);
  color: var(--font-color);
}
.header {
  background-color: var(--primary-color);
  color: black;
  padding: 10px;
  text-align: center;
}
.main_column {
  max-width: 960px;
  margin: 0 auto;
  padding: 2rem 2.5rem;
}
.btn-primary {
  color: var(--font-color);
  background-color: var(--primary-color);
  border-color: var(--primary-color);
}
.btn-primary:hover {
  background-color: var(--purple-color);
  border-color: var(--purple-color);
}
.forms_container {
  max-width: 560px;
  margin: 2rem auto;
}
</style>
